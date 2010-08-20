package Net::OperaLink::Client;

use 5.010;
use strict;
use warnings;
use Carp;
use utf8;
use version; our $VERSION = '0.0.1';

use LWP::UserAgent;
use HTTP::Request;
use URI;
use XML::Simple qw/XMLin/;

use base qw/Class::Accessor::Fast/;
__PACKAGE__->mk_accessors( qw/
  state user pass
  sync_bookmark sync_personalbar sync_panel sync_speeddial sync_typed sync_notes sync_searches 
  platform_build platform_system platform_device/ 
);

my $OPERA_UA_STRING = 'Opera/9.80 (Windows NT 6.0; U; en) Presto/2.5.24 Version/10.52';
my $LOGIN_API = 'https://auth.opera.com/xml';
my $LINK_API = 'https://link-server.opera.com/pull';

sub new {
  my ( $c, $r ) = @_;

  my ( $state, $user, $pass );
  my ( $sync_bookmark, $sync_personalbar, $sync_panel, $sync_speeddial, $sync_typed, $sync_notes, $sync_searches );
  my ( $build_number, $system_name, $device_type );
  if ( defined $r ) {
    $state = $r->{state};
    $user = $r->{user} ;
    $pass = $r->{pass} ;
    $sync_bookmark = $r->{sync_bookmark};
    $sync_personalbar = $r->{sync_bookmark};
    $sync_panel  = $r->{sync_bookmark} ;
    $sync_speeddial = $r->{sync_bookmark} ;
    $sync_typed = $r->{sync_bookmark};
    $sync_notes = $r->{sync_bookmark};
    $sync_searches = $r->{sync_bookmark};
    $system_name = $r->{system_name};
    $build_number = $r->{build_number};
    $device_type = $r->{device_type};
  }

  my $ua = LWP::UserAgent->new;
  $ua->agent( $OPERA_UA_STRING );

  my $self = bless { _ua => $ua }, $c;
  $self->{state}            = $state            || 0;
  $self->{user}             = $user             || '';
  $self->{pass}             = $pass             || '';
  $self->{sync_bookmark}    = $sync_bookmark    || 1;
  $self->{sync_personalbar} = $sync_personalbar || 1;
  $self->{sync_panel}       = $sync_panel       || 1;
  $self->{sync_speeddial}   = $sync_speeddial   || 1;
  $self->{sync_typed}       = 1;#$sync_typed       || 1;
  $self->{sync_notes}       = $sync_notes       || 1;
  $self->{sync_searches}    = $sync_searches    || 1;
  $self->{platform_build}   = $build_number     || '3344';
  $self->{platform_system}  = $system_name      || 'win32';
  $self->{platform_device}  = $device_type      || 'desktop';
  $self->{token} = '';
  $self->{short_interval} = 15;
  $self->{long_interval} = 60;

  $self->{typed_history_items} = [];

  return $self;
}

sub login {
  my ( $self ) = @_;

  if ( !defined $self->user || !defined $self->pass() ) {
    carp 'username or password not defined.';
    return { state => 'failed' };
  }

  my $res = $self->_request_login; #( $user, $pass );

#  warn Dumper $res;
  if ( !$res->is_success ) {
    carp $res->status_line;
    return { state => 'failed' };
  }
  my $xml = XMLin( $res->content );
  $self->{token} = $xml->{token};
  return {
	  state => $xml->{code} eq '200' ? 'ok' : 'failed' ,
	  auth => {
		   token => $xml->{token},
		   message => $xml->{message},
		   code => $xml->{code}
		  }
	 };
}

sub _create_login_post {
  my ( $self ) = @_;
  my $login_xml = '';
  $login_xml .= '<?xml version="1.0" encoding="utf-8"?>';
  $login_xml .= '<auth version="1.1" xmlns="http://xmlns.opera.com/2007/auth">';
  $login_xml .= '<login>';
  $login_xml .= '<username>' . $self->user() . '</username>';
  $login_xml .= '<password>' . $self->pass() . '</password>';
  $login_xml .= '</login>';
  $login_xml .= '</auth>';
  return $login_xml;
}

sub _request_login {
  my ( $self ) = @_;
  my $request = HTTP::Request->new( 'POST', $LOGIN_API );
  $request->content( $self->_create_login_post() );
  return $self->{_ua}->request( $request );
}

sub sync {
  my ( $self ) = @_;
  if ( ! defined $self->{token} || $self->{token} !~ m/[A-Fa-f0-9]{32}/ ) {
    return { state => 'notlogin' };
  }
  carp $self->{token};
  return { state => 'false' };
}

sub _create_sync_xml {
  my ( $self ) = @_;
  my $user = $self->user();
  my $pass = $self->pass();
  my $state = $self->sync_state() || 0;
  my $sync_xml = '<?xml version="1.0" encoding="utf-8"?>' ;
  $sync_xml .= '<link user="' . $user . '" password="' . $pass . '" syncstate="'.  $state .
               '" dirty="0" version="1.0" xmlns="http://xmlns.opera.com/2006/link">' ;
  $sync_xml .= $self->_create_sync_xml_clientinfo();
  $sync_xml .= $self->_create_sync_xml_data();
  $sync_xml .= '</link>';
  return $sync_xml;
}

sub _create_sync_xml_clientinfo{
  my ( $self ) = @_;
  my $ci = '';
  $ci .= '<clientinfo>';
  $ci .=  '<supports>bookmark</supports>'                       if $self->sync_bookmark;
  $ci .=  '<supports>speeddial</supports>'                      if $self->sync_speeddial;
  $ci .=  '<supports>note</supports>'                           if $self->sync_notes;
  $ci .=  '<supports target="' . $self->device_type() . '">search_engine</supports>' if $self->sync_searches;
  $ci .=  '<supports>typed_history</supports>'                  if $self->sync_typed;
  $ci .=  '<build>' . $self->build_number() . '</build>';
  $ci .=  '<system>' . $self->system_name() . '</system>';
  $ci .= '</clientinfo>';
  return $ci;
}

sub _create_sync_xml_data{
  my ( $self ) = @_;
  return '<data />' unless $self->_changed();

  my $data = '';
  $data .= '<data>';
  $data .= $self->_create_bookmark_diff()  if $self->sync_bookmark;
  $data .= $self->_create_speeddial_diff() if $self->sync_speeddial;
  $data .= $self->_create_notes_diff()     if $self->sync_notes;
  $data .= $self->_create_searches_diff()  if $self->sync_searches;
  $data .= $self->_create_typed_diff()     if $self->sync_typed;
  $data .= '</data>';
}

sub _changed{
  my ( $self ) = @_;
  return 0;
}

sub add_typed_history{
  my ( $self, $typed ) = @_;
  return { state => 'not_support' } if ( $self->sync_typed == 0 );
  my $content = $typed->{content};
  return { state => 'ok' };
}

sub mod_typed_history{
}



1; # Magic true value required at end of module


__END__

=encoding utf8

=head1 NAME

Net::OperaLink::Client - Opera Link Client library


=head1 VERSION

This document describes Net::OperaLink::Client version 0.0.1


=head1 SYNOPSIS

    use Net::OperaLink::Client;

=for author to fill in:
    my $client = Net::OperaLinke::Client->new( { user => $use, pass => $pass, sync_bookmark => 1, sync_parsonalbar => 0 } );
    $client->login;

=head1 DESCRIPTION

=for author to fill in:
    Write a full description of the module and its features here.
    Use subsections (=head2, =head3) as appropriate.

=head1 SUBROUTINES/METHODS

=for author to fill in:

=head2 new
     hoge

=head2 login
     hoge

=head2 sync
     hoge

=head2 add_typed_history
     add typed history content 
     my $client->add_typed_history( { content => 'some text' } );

=head2 mod_typed_history
     mod typed history content 
     my $client->add_typed_history( { content => 'some text', last_typed => '2010-04-01T11:03:02Z' } );

=head1 INTERFACE 

=for author to fill in:
    Write a separate section listing the public components of the modules
    interface. These normally consist of either subroutines that may be
    exported, or methods that may be called on objects belonging to the
    classes provided by the module.


=head1 DIAGNOSTICS

=for author to fill in:
    List every single error and warning message that the module can
    generate (even the ones that will "never happen"), with a full
    explanation of each problem, one or more likely causes, and any
    suggested remedies.

=over

=item C<< Error message here, perhaps with %s placeholders >>

[Description of error here]

=item C<< Another error message here >>

[Description of error here]

[Et cetera, et cetera]

=back


=head1 CONFIGURATION AND ENVIRONMENT

=for author to fill in:
    A full explanation of any configuration system(s) used by the
    module, including the names and locations of any configuration
    files, and the meaning of any environment variables or properties
    that can be set. These descriptions must also include details of any
    configuration language used.

Net::OperaLink requires no configuration files or environment variables.


=head1 DEPENDENCIES

=for author to fill in:
    A list of all the other modules that this module relies upon,
    including any restrictions on versions, and an indication whether
    the module is part of the standard Perl distribution, part of the
    module's distribution, or must be installed separately. ]

None.


=head1 INCOMPATIBILITIES

=for author to fill in:
    A list of any modules that this module cannot be used in conjunction
    with. This may be due to name conflicts in the interface, or
    competition for system or program resources, or due to internal
    limitations of Perl (for example, many modules that use source code
    filters are mutually incompatible).

None reported.


=head1 BUGS AND LIMITATIONS

=for author to fill in:

No bugs have been reported.

Please report any bugs or feature requests to
C<bug-net-operalink@rt.cpan.org>, or through the web interface at
L<http://rt.cpan.org>.


=head1 AUTHOR

t.ashula  C<< <t.ashula@gmail.com> >>

=head1 LICENCE AND COPYRIGHT

Copyright (c) 2010, t.ashula C<< <t.ashula@gmail.com> >>. All rights reserved.

This module is free software; you can redistribute it and/or
modify it under the same terms as Perl itself. See L<perlartistic>.


=head1 DISCLAIMER OF WARRANTY

BECAUSE THIS SOFTWARE IS LICENSED FREE OF CHARGE, THERE IS NO WARRANTY
FOR THE SOFTWARE, TO THE EXTENT PERMITTED BY APPLICABLE LAW. EXCEPT WHEN
OTHERWISE STATED IN WRITING THE COPYRIGHT HOLDERS AND/OR OTHER PARTIES
PROVIDE THE SOFTWARE "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER
EXPRESSED OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. THE
ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE SOFTWARE IS WITH
YOU. SHOULD THE SOFTWARE PROVE DEFECTIVE, YOU ASSUME THE COST OF ALL
NECESSARY SERVICING, REPAIR, OR CORRECTION.

IN NO EVENT UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING
WILL ANY COPYRIGHT HOLDER, OR ANY OTHER PARTY WHO MAY MODIFY AND/OR
REDISTRIBUTE THE SOFTWARE AS PERMITTED BY THE ABOVE LICENCE, BE
LIABLE TO YOU FOR DAMAGES, INCLUDING ANY GENERAL, SPECIAL, INCIDENTAL,
OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE USE OR INABILITY TO USE
THE SOFTWARE (INCLUDING BUT NOT LIMITED TO LOSS OF DATA OR DATA BEING
RENDERED INACCURATE OR LOSSES SUSTAINED BY YOU OR THIRD PARTIES OR A
FAILURE OF THE SOFTWARE TO OPERATE WITH ANY OTHER SOFTWARE), EVEN IF
SUCH HOLDER OR OTHER PARTY HAS BEEN ADVISED OF THE POSSIBILITY OF
SUCH DAMAGES.
