package Net::OperaLink::Data::SpeedDial;

use strict;
use warnings;
use Carp;
use utf8;

use XML::LibXML;

use base qw/Net::OperaLink::Data/;
use version; our $VERSION = '0.0.1';

sub _is_same_content {
  my ( $self, $l, $r ) = @_;
  return $l->{position} eq $r->{position};
}

sub _is_valid_content {
  my ( $self, $content ) = @_;
  return !( !defined $content || !ref $content || !defined $content->{position} || !defined $content->{uri} );
}

sub add {
  my ( $self, $arg ) = @_;
  return -1 if ! $self->_is_valid_content( $arg );
  my $content = {
		 icon  => $arg->{icon} || q{},
		 title => $arg->{title} || q{},
		 uri   => $arg->{uri},
		 reload_enabled => $arg->{reload_enabled} || 0,
		 reload_interval => $arg->{reload_interval} || 0,
		 reload_only_if_expired => $arg->{reload_only_if_expired} || 0,
		 position => $arg->{position},
		};
  $self->SUPER::add( $content );
}

sub mod {
  my ( $self, $arg ) = @_;
  return -1 if ! $self->_is_valid_content( $arg );
  $self->SUPER::mod( $arg );
}

sub _mod_content {
  my ( $self, $item, $arg ) = @_;
  my @keys = ( 'position', 'icon', 'title', 'uri', 'reload_enabled', 'reload_interval', 'reload_only_if_expired' );
  foreach my $k ( @keys ) {
    $item->{content}->{$k} = defined $arg->{$k} ? $arg->{$k} : $item->{content}->{$k};
  }
}

sub del {
  my ( $self, $arg ) = @_;
  return -1 if !$self->_is_valid_content( $arg );
  my $content = { 
		 position => $arg->{position},
		};
  $self->SUPER::del( $arg );
}

sub from_opera_link_xml {
  my ( $self, $xml_string ) = @_;
    return -1 if ( !defined $xml_string );

  my $xml_dom = XML::LibXML->load_xml( string => '<o>' . $xml_string . '</o>' );
  my @ses = $xml_dom->getElementsByTagName( 'speeddial' );
  foreach my $h ( @ses ) {
    my $status   = $h->getAttribute( 'status' );
    my $content = {
		   position               => $h->getAttribute( 'position' ),
		   icon                   => ($h->getElementsByTagName( 'icon' ) )[0]->textContent,
		   title                  => ($h->getElementsByTagName( 'title' ) )[0]->textContent,
		   uri                    => ($h->getElementsByTagName( 'uri' ) )[0]->textContent,
		   reload_enabled         => ($h->getElementsByTagName( 'reload_enabled' ) )[0]->textContent,
		   reload_interval        => ($h->getElementsByTagName( 'reload_interval' ) )[0]->textContent,
		   reload_only_if_expired => ($h->getElementsByTagName( 'reload_only_if_expired' ) )[0]->textContent,
		  };
    if ( $status eq 'added' ) {
      $self->_add_item ( $content );
    }
    elsif ( $status eq 'modified' ) {
      $self->_mod_item ( $content );
    }
    elsif ( $status eq 'deleted' ) {
      $self->_del_item ( $content );
    }
    else {
      warn 'unknown status arrived<' . $status . '>';
    }
  }
  return scalar @{$self->{_inner_items}};
}

sub to_opera_link_xml {
  my ( $self ) = @_;
  my $xml_string = '';
  foreach my $item ( @{$self->{_to_sync_items}} ) {
    my $status = $item->{status} ;
    my $content = $item->{content};
    if ( $status eq 'deleted' ) {
      $xml_string .= join(q{}, ( q{<speeddial status="deleted" position="}, $content->{position}, q{" />} ) );
    } else {
      $xml_string .= join(q{}, ( '<speeddial ', 
				'status="', $status, '" ', 
				'position="', $content->{position}, '">',
				'<icon>', $content->{icon}, '</icon>',
				'<reload_only_if_expired>', $content->{reload_only_if_expired} == 0 ? 0 : 1, '</reload_only_if_expired>',
				'<reload_enabled>', $content->{reload_enabled} == 0 ? 0 : 1, '</reload_enabled>',
				'<reload_interval>', $content->{reload_interval}, '</reload_interval>',
				'<uri>', $content->{uri}, '</uri>',
				'<title>', $content->{title}, '</title>',
				'</speeddial>' ) );
    }
  }
  return $xml_string;
}


1; # Magic true value required at end of module

#<speeddial status="added" position="7">
#<title>t. @ hatena</title>
#<uri>http://d.hatena.ne.jp/t_ashula/</uri>
#<reload_enabled>0</reload_enabled>
#<reload_interval>0</reload_interval>
#<reload_only_if_expired>0</reload_only_if_expired>
#<icon>iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAADjUlEQVR42h2T6VKaBxhGuZzG6dQlmzZjNFp1NIALUXRABAEFRVxwwYCiUhQSRDRBcYlLA4nGqIlKdOIaQ+LSNu04rVprOpNp0h9N+qO9gdOvvYL3Oed5XpFWnkxG4mcopBdwt90gPFLBw9FyxnyF2KozsKiuYsxLQCOJxWsXE+iW0tcpZTxoxGLOR6QpyKQ4OxmzKpu7Lg1zUxaehhsZ8ajx2UqY8Orw1OfSWZNLT8sNbtsL8XaoGPJX096iRmRWyTCXSvE5tCzPdLG3fZeDFwEWxpsIdpYx0WvkjqMUf5sKr62UMX8No/5aBnqq6LYbEHkdBgY9VWxEfLz9JcIf73c4+uERK9NtjLkr8bQo6Gr4D0eGv6OcyKyTpVkXU4Ot+Jy1iJ4v9/H6RZB379b59PGAvz694ex4if1NP09DDuwmGXXlYnpaVUwIl7dWBjj8cY7dlyEeh32IotuD7EfHeP/hNX//c8jHP3c5+ekxBzsCRqgda4WYZr2UULDxf8Tvdh9werrOr6db7L2aQ3R/qJ6ZKRsvoyEWl0YIBrvo7THhdmi41a4WuEvwtSvp79HzYMLGt/sz/PZ2m98/fM/R0Rai2clmpscbmA53olblEhNzjosJn1MivUqdTozLKsdWK8Oguk65UkjTVMHMzBA70XmeLY8jmuzXMuCU42mToy/NIiUpgS/Px6FT5GCvL8JalUeV+jpFuV9xITaemHNfIMsTY2uu5GZDOaJhdwlt5kyadNdoMWbT3S5E7zTi69IyFaynz6XDrJOSm5VCalIiaVcSyctKRSXsp7WhEtHWym1sZgkmVQqDXi2ba16iG/08CdnY27nH5uodvF066vR53LSo0CslyHNSMQg4oW/8iI6PFhgNWGmqKaL/loHFWRtri52sLXRzdhLh58N5JgLN9HZoCAsSXXYtVpOCZ0vDnJwJEg9eBVhe8OB2VuOwahj2V7H0yM52xMub6D12VgcIuKtx25T4eyporFXQbNHxfP0+x6fbwg42XUQWHPQJq2sUUljrigj0Gng40sSkt5pBpw6HINPeIKfVUoZWr0ImL0JeLMNkEn4huvE1c6FaOiwStMUpKPOT0chTMakzMRSno5enoci/hkySjqJEhr5CQ6VRh7JUTkFBjiBxtYP5cAN9TgWNBgnF4iTEqedJS4wj+WIsKZfjhVrjuRQXx5XLl8jOSKekMJ8yZSG1ZjX/AkWtf6r1qUEqAAAAAElFTkSuQmCC</icon>
#</speeddial>


=head1 NAME

Net::OperaLink::Data::SpeedDial - [One line description of module's purpose here]


=head1 VERSION

This document describes Net::OperaLink::Data::SpeedDial version 0.0.1


=head1 SYNOPSIS

    use Net::OperaLink::Data::SpeedDial;
    my $sd = Net::OperaLink::Data::SpeedDial->new;
    $sd->add({});
 
=head1 DESCRIPTION

=head1 INTERFACE 

=over 4

=item new

=item add

=item mod

=item del

=item items

=item from_opera_link_xml

=item to_opera_link_xml

=back 

=head1 DIAGNOSTICS

=over 4

=item C<< Error message here, perhaps with %s placeholders >>

[Description of error here]

=item C<< Another error message here >>

[Description of error here]

[Et cetera, et cetera]

=back


=head1 CONFIGURATION AND ENVIRONMENT

Net::OperaLink::Data::SpeedDial requires no configuration files or environment variables.

=head1 DEPENDENCIES

Net::OperaLink::Data, XML::LibXML

=head1 INCOMPATIBILITIES

None reported.

=head1 BUGS AND LIMITATIONS

No bugs have been reported.

Please report any bugs or feature requests to
C<bug-net-operalink-data-speeddial@rt.cpan.org>, or through the web interface at
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
