package Net::OperaLink::Data::TypedHistory;

use warnings;
use strict;
use utf8;
use Carp;

use version; our $VERSION = '0.0.1';
use XML::LibXML qw/:all/;
use DateTime;
use DateTime::Format::W3CDTF;

# Module implementation here

sub new {
  my ($c) = @_;
  my $self = bless{
		   _inner_items => [],
		   _to_sync_items => [],
		  }, $c;
  return $self;
}

sub items {
  my ( $self ) = @_;
  my @copy = ();
  foreach my $item ( @{$self->{_inner_items}} ) {
    #$item->{content} = $self->_encode_content( $item->{content} );
    push @copy, $item;
  }
  return @copy;
}

sub add {
  my ($self, $arg) = @_;
  return -1 if ( !defined $arg || !ref $arg || !defined $arg->{content} );

  my $item = { 
	      status => 'added', 
	      type => 'text',
	      last_typed => $arg->{last_typed}, 
	      content => $arg->{content} 
	     };
  my $added = $self->_add_item( $item );
  if ( $added != 0 ) {
    $self->_add_sync_item( $item );
  }
  return scalar @{$self->{_inner_items}};
}

sub _add_item {
  my ( $self, $arg ) = @_;
  foreach my $item ( @{$self->{_inner_items} } ) {
    if ( $item->{content} eq $arg->{content} ) {
      return 0;
    }
  }
  push @{$self->{_inner_items}}, $arg;
  return 1;
}

sub _add_sync_item {
  my ( $self, $arg ) = @_;
  push @{$self->{_to_sync_items}}, $arg;
}

sub mod {
  my ($self, $arg) = @_;
  return -1 if ( !defined $arg || !ref $arg || !defined $arg->{content} );
  my $item = { status => 'modified', type => 'selected',
	       last_typed => $arg->{last_typed}, 
	       content => $arg->{content} };
  $self->_mod_item( $item );
  $self->_mod_sync_item( $item );
  return scalar @{$self->{_inner_items}};
}

sub _mod_item {
  my ( $self, $arg ) = @_;
  foreach my $item ( @{$self->{_inner_items}} ) {
    if ( $item->{content} eq $arg->{content} ) {
      $item->{last_typed} = $arg->{last_typed};
      $item->{status} = $arg->{status};
      $item->{type} = $arg->{type};
      next;
    }
  }
}

sub _mod_sync_item {
  my ( $self, $arg ) = @_;
  foreach my $item ( @{$self->{_to_sync_items}} ) {
    if ( $item->{content} eq $arg->{content} ) {
      if ( $item->{status} ne 'deleted' ) {
	$item->{last_typed} = $arg->{last_typed} ;
	$item->{status} = $arg->{stauts};
	$item->{type} = $arg->{type};
	next;
      }
    }
  }
}

sub del {
  my ($self, $arg) = @_;
  return -1 if ( !defined $arg || !ref $arg || ( !defined $arg->{content} ) );

  my $item = { status => 'deleted', type => 'selected',
	       last_typed => $arg->{last_typed}, 
	       content => $arg->{content} };

  $self->_del_sync_item( $item ) if $self->_del_item( $item );
  return scalar @{$self->{_inner_items}};
}

sub _del_item {
  my ( $self, $arg ) = @_;
  my $i = 0;
  my $found = 0;
  foreach my $item ( @{$self->{_inner_items}} ) {
    if ( $item->{content} eq $arg->{content} ) {
      $found = 1;
      next;
    }
    $i ++;
  }
  splice @{$self->{_inner_items}}, $i, 1 if $found;
  return $found;
}

sub _del_sync_item {
  my ( $self, $arg ) = @_;
  my $found = 0;
  foreach my $item ( @{$self->{_to_sync_items}} ) {
    if ( $item->{content} eq $arg->{content} ) {
      $item->{status} = 'deleted';
      $found = 1;
      next;
    }
  }
  push @{$self->{_to_sync_items}}, $arg unless $found;
}

sub from_opera_link_xml {
  my ( $self, $xml_string ) = @_;
  return -1 if ( !defined $xml_string );

  my $xml_dom = XML::LibXML->load_xml( string => '<o>' . $xml_string . '</o>' );
  my @histories = $xml_dom->getElementsByTagName( 'typed_history' );
  foreach my $h ( @histories ) { 
    my $last_typed = $h->firstChild()->firstChild()->textContent();
    my $status = $h->getAttribute( 'status' );
    my $content = $self->_decode_content( $h->getAttribute( 'content' ) );
    my $type = $h->getAttribute( 'type' );
    my $arg = { content => $content, last_typed => $last_typed, type => $type, status => $status } ;
    if ( $status eq 'added' ) {
      $self->_add_item ( $arg );
    } elsif ( $status eq 'modified' ) {
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
  foreach my $item ( @{$self->{_inner_items}} ) {
    $xml_string .= join('', ( '<typed_history ', 
			      'status="', $item->{status}, '" ', 
			      'content="', $self->_encode_content( $item->{content} ), '" ',
			      'type="', $item->{type}, '">',
			      '<last_typed>', $item->{last_typed}, '</last_typed>',
			      '</typed_history>' ));
  }
  return $xml_string;
}

sub _encode_content{
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

sub _decode_content{
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
