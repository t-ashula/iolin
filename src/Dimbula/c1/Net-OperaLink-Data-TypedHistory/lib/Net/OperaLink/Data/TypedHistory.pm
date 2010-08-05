package Net::OperaLink::Data::TypedHistory;

use warnings;
use strict;
use utf8;
use Carp;

use base qw/Net::OperaLink::Data/;
use version; our $VERSION = '0.0.1';
use XML::LibXML qw/:all/;

sub _is_same_content {
  my ( $s, $l, $r ) = @_;
  return $l->{content} eq $r->{content};
}

sub _is_valid_content {
  my ( $self, $content ) = @_;
  return !( !defined $content || !ref $content || !defined $content->{content} );
}

sub add {
  my ( $self, $arg ) = @_;
  return -1 unless $self->_is_valid_content( $arg );
  my $content = { type => $arg->{type} || 'text', last_typed => $arg->{last_typed}, content => $arg->{content} };
  return $self->SUPER::add( $content );
}

sub mod {
  my ( $self, $arg ) = @_;
  return -1 unless $self->_is_valid_content( $arg );
  my $content = { type => 'selected', last_typed => $arg->{last_typed}, content => $arg->{content} };
  return $self->SUPER::mod( $content );
}
sub _mod_content {
  my ( $self, $item, $arg ) = @_;
  $item->{content} = $arg;
}

sub del {
  my ( $self, $arg ) = @_;
  return -1 unless $self->_is_valid_content( $arg );
  my $content = { content => $arg->{content}, type => 'selected' };
  return $self->SUPER::del( $content );
}

sub from_opera_link_xml {
  my ( $self, $xml_string ) = @_;
  return -1 if ( !defined $xml_string );

  my $xml_dom = XML::LibXML->load_xml( string => '<o>' . $xml_string . '</o>' );
  my @histories = $xml_dom->getElementsByTagName( 'typed_history' );
  foreach my $h ( @histories ) { 
    my $status = $h->getAttribute( 'status' );
    my $content = $self->_decode_content( $h->getAttribute( 'content' ) );
    my $type = $h->getAttribute( 'type' );
    my $arg = { content => $content, type => $type } ;
    if ( $status eq 'added' ) {
      my $last_typed = $h->firstChild()->firstChild()->textContent();
      $arg->{last_typed} = $last_typed;
      $self->_add_item ( $arg );
    } elsif ( $status eq 'modified' ) {
      my $last_typed = $h->firstChild()->firstChild()->textContent();
      $arg->{last_typed} = $last_typed;
      $self->_mod_item ( $arg );
    } elsif ( $status eq 'deleted' ) {
      $self->_del_item ( $arg );
    } else {
      warn 'unknown status arrived<' . $status . '>';
    }
  }
  return scalar @{$self->{_inner_items}};
}

sub to_opera_link_xml {
  my ( $self ) = @_;
  my $xml_string = '';
  foreach my $item ( @{$self->{_to_sync_items}} ) {
    my $content = $item->{content};
    my $type = $item->{status} eq 'deleted' ? 'selected' : $content->{type};
    $xml_string .= join('', ( '<typed_history ', 
			      'status="', $item->{status}, '" ', 
			      'content="', $self->_encode_content( $content->{content} ), '" ',
			      'type="', $type, '">',
			      '<last_typed>', $content->{last_typed}, '</last_typed>',
			      '</typed_history>' ));
  }
  return $xml_string;
}

sub _encode_content {
  my ( $self, $raw ) = @_;
  my $cooked = '';
  if ( defined $raw ) {
    $cooked = $raw;
    $cooked =~ s{&}{&amp;}g;
    $cooked =~ s{"}{&quot;}g;
    $cooked =~ s{<}{&lt;}g;
  }
  return $cooked;
}

sub _decode_content {
  my ( $self, $cooked ) = @_;
  my $raw = '';
  if ( defined $cooked ) {
    $raw = $cooked;
    $raw =~ s{&lt;}{<}g;
    $raw =~ s{&quot;}{"}g;
    $raw =~ s{&amp;}{&}g;
  }
  return $raw;
}


1; # Magic true value required at end of module
__END__

# <typed_history status="added" content="ashula.info" type="text">
#  <last_typed>2010-04-14T18:22:42Z</last_typed>
# </typed_history>
# <typed_history status="modified" content="ashula.info" type="selected">
#  <last_typed>2010-04-14T18:24:12Z</last_typed>
# </typed_history>
# <typed_history status="deleted" content="ashula.info" type="selected">
#  <last_typed>2010-04-14T18:24:12Z</last_typed>
# </typed_history>
# <typed_history status="added" content="&quot;foo bar &amp;'()*&lt;>" type="text">
#  <last_typed>2010-04-14T18:50:18Z</last_typed>
# </typed_history>
# <typed_history status="added" content="あ" type="text">
#  <last_typed>2010-04-14T18:51:25Z</last_typed>
# </typed_history>

=head1 NAME

Net::OperaLink::Data::TypedHistory - [One line description of module's purpose here]

=head1 VERSION

This document describes Net::OperaLink::Data::TypedHistory version 0.0.1


=head1 SYNOPSIS

    use Net::OperaLink::Data::TypedHistory;
    my $types = Net::OperaLink::Data::TypedHistory->new;
    $types->add( { content => 'some text' } );

    my $from_operalink = "<typed_hisotry ></typed_hisotry>";
    $types->from_opera_link_xml( $from_operalink );

    my $to_operalink = $types->to_opera_link_xml;

    my @items = $types->items;


=head1 DESCRIPTION

    Net::OperaLink::Data::TypedHistory handle OperaLink's typed history data.

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

=over

=item C<< Error message here, perhaps with %s placeholders >>

[Description of error here]

=item C<< Another error message here >>

[Description of error here]

[Et cetera, et cetera]

=back


=head1 CONFIGURATION AND ENVIRONMENT

Net::OperaLink::Data::TypedHistory requires no configuration files or environment variables.

=head1 DEPENDENCIES

=over 4

=item Net::OperaLink::Data

=item XML::LibXML

=back


=head1 INCOMPATIBILITIES

None reported.

=head1 BUGS AND LIMITATIONS

No bugs have been reported.

Please report any bugs or feature requests to
C<bug-net-operalink-data-typedhistory@rt.cpan.org>, or through the web interface at
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
