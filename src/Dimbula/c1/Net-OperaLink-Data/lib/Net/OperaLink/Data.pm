package Net::OperaLink::Data;

use strict;
use warnings;
use utf8;
use Carp;
use version; our $VERSION = '0.0.1';

sub new {
  my ( $c ) = @_;
  my $self = bless{ _inner_items => [], _to_sync_items => [], }, $c;
  return $self;
}


sub items {
  my ( $self ) = @_;
  my @copy = ();
  foreach my $item ( @{$self->{_inner_items}} ) {
    push @copy, $item->{content};
  }
  return @copy;
}

sub add {
  my ( $self, $item ) = @_;
  $self->_add_sync_item( $item ) if $self->_add_item( $item );
  return scalar @{$self->{_inner_items}};
}

sub _add_item {
  my ( $self, $arg ) = @_;
  foreach my $item ( @{$self->{_inner_items} } ) {
    my $content = $item->{content};
    if ( $self->_is_same_content( $content, $arg ) ) {
      return 0;
    }
  }
  push @{$self->{_inner_items}}, { status => 'added', content => $arg };
  return 1;
}

sub _add_sync_item {
  my ( $self, $arg ) = @_; 
  push @{$self->{_to_sync_items}}, { status => 'added', content => $arg };
}

sub mod {
  my ( $self, $item ) = @_;
  $self->_mod_item( $item );
  $self->_mod_sync_item( $item );
  return scalar @{$self->{_inner_items}};
}

sub _mod_item {
  my ( $self, $arg ) = @_;
  foreach my $item ( @{$self->{_inner_items}} ) {
    my $content = $item->{content};
    if ( $self->_is_same_content( $content, $arg ) ) {
      $item->{status} = 'modified';
      $self->_mod_content( $item, $arg );
      next;
    }
  }
}

sub _mod_sync_item {
  my ( $self, $arg ) = @_;
  foreach my $item ( @{$self->{_to_sync_items}} ) {
    my $content = $item->{content};
    if ( $self->_is_same_content( $content, $arg ) ) {
      if ( $item->{status} ne 'deleted' ) {
	$item->{status} = 'modified';
	$self->_mod_content( $item, $arg );
      }
      next;
    }
  }
}


sub del {
  my ( $self, $item ) = @_;
  $self->_del_sync_item( $item ) if $self->_del_item( $item );
  return scalar @{$self->{_inner_items}};
}

sub _del_item {
  my ( $self, $arg ) = @_;
  my $i = 0;
  my $found = 0;
  foreach my $item ( @{$self->{_inner_items}} ) {
    my $content = $item->{content};
    if ( $self->_is_same_content( $content, $arg ) ) {
      $found = 1;
      next;
    }
    $i++;
  }
  splice @{$self->{_inner_items}}, $i, 1 if $found;
  return $found;
}

sub _del_sync_item {
  my ( $self, $arg ) = @_;
  my $found = 0;
  foreach my $item ( @{$self->{_to_sync_items}} ) {
    my $content = $item->{content};
    if ( $self->_is_same_content( $content, $arg ) ) {
      $item->{status} = 'deleted';
      $found = 1;
      next;
    }
  }
  push @{$self->{_to_sync_items}}, $arg unless $found;
}

#sub sub _mod_content { my ( $self, $item, $arg ) = @_; $item->{content} = $arg; }
#sub from_opera_link_xml { croak "no converter implemented."; }
#sub to_opera_link_xml { croak "no converter implemented"; }
#sub _is_valid_content { croak "no validator implemented"; }
#sub _is_same_content { croak "no comparator implemented"; }

1;
__END__

=head1 NAME

Net::OperaLink::Data - Net::OperaLink::Data::Foobar base class

=head1 VERSION

This document describes Net::OperaLink::Data version 0.0.1

=head1 SYNOPSIS

    #use Net::OperaLink::Data;

=head1 DESCRIPTION

=head1 INTERFACE 

=over 4

=item new

=item add

=item mod

=item del

=item items

=item from_opera_link_xml

opera link xml to internal data set
must be implement in derive class

=item to_opera_link_xml

internal data set to opera link xml
must be implement in derive class

=item _is_same_content

item comparator, shuld be implement in derive class

=item _is_valid_content

item validator, shuld be implement in derive class

=back

=head1 DIAGNOSTICS

=over

=item C<< Error message here, perhaps with %s placeholders >>

nothing

=item C<< Another error message here >>

nothing

=back


=head1 CONFIGURATION AND ENVIRONMENT

Net::OperaLink::Data requires no configuration files or environment variables.

=head1 DEPENDENCIES

None.

=head1 INCOMPATIBILITIES

None reported.

=head1 BUGS AND LIMITATIONS

No bugs have been reported.

Please report any bugs or feature requests to
C<bug-net-operalink-data@rt.cpan.org>, or through the web interface at
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
