package Net::OperaLink::Data::SearchEngine;

use warnings;
use strict;
use Carp;
use utf8;

use base qw/Net::OperaLink::Data/;

use version; our $VERSION = '0.0.1';
use XML::LibXML qw/:all/;
use Data::GUID;

sub _is_same_content {
  my ( $self, $l, $r ) = @_;
  return $l->{id} eq $r->{id};
}

sub _is_valid_content {
  my ( $self, $content ) = @_;
  return !( !defined $content || !ref $content || !defined $content->{id} );
}

sub add {
  my ( $self, $arg ) = @_;
  return -1 if ( !defined $arg || !ref $arg || ( !defined $arg->{uri} || !defined $arg->{key} ) );

  my $ug = Data::GUID->new;
  my $uuid = $ug->as_string();
  $uuid =~ s/-//g;
  my $item = {
	      id         => $arg->{id}    || $uuid,
	      type       => $arg->{type}  || 'normal',
	      group      => $arg->{group} || 'custom',
	      hidden     => 0,
	      is_post    => $arg->{is_post} || 0,
	      pb_pos     => defined $arg->{personal_bar_pos}     ? $arg->{personal_bar_pos}     : -1,
	      in_pb      => defined $arg->{show_in_personal_bar} ? $arg->{show_in_personal_bar} : 0,
	      title      => $arg->{title} || q{},
	      uri        => $arg->{uri},
	      key        => $arg->{key},
	      encoding   => $arg->{encoding}   || 'UTF-8',
	      post_query => $arg->{post_query} || q{},
	      icon       => $arg->{icon}       || q{},
	     };
  return $self->SUPER::add( $item );
}

sub mod {
  my ( $self, $arg ) = @_;
  return -1 unless $self->_is_valid_content( $arg );

  my $item = { 
	      id         => $arg->{id},
	      type       => $arg->{type},
	      group      => $arg->{group},
	      hidden     => $arg->{hidden} || 0,
	      is_post    => $arg->{is_post} || 0,
	      pb_pos     => $arg->{personal_bar_pos} || -1,
	      in_pb      => $arg->{show_in_personal_bar} || 0,
	      title      => $arg->{title} || q{},
	      uri        => $arg->{uri} || q{},
	      key        => $arg->{key} || q{},
	      encoding   => $arg->{encoding} || 'UTF-8',
	      post_query => $arg->{post_query} || q{},
	      icon       => $arg->{icon} || q{},
	     };
  return $self->SUPER::mod( $item );
}

sub _mod_content {
  my ( $self, $item, $arg ) = @_;
  # id, type, and group can  not change
  my @keys = ( 'hidden', 'is_post', 'pb_pos', 'in_pb', 'title', 'uri', 'key', 'encoding', 'post_query', 'icon' );
  foreach my $k ( @keys ) {
    $item->{content}->{$k} = defined $arg->{$k} ? $arg->{$k} : $item->{content}->{$k};
  }
}

sub del {
  my ($self, $arg) = @_;
  return -1 unless $self->_is_valid_content( $arg );

  my $item = {
	      id   => $arg->{id},
	      type => $arg->{type},
	     };
  return $self->SUPER::del( $item );
}


sub from_opera_link_xml {
  my ( $self, $xml_string ) = @_;
  return -1 if ( !defined $xml_string );

  my $xml_dom = XML::LibXML->load_xml( string => '<o>' . $xml_string . '</o>' );
  my @ses = $xml_dom->getElementsByTagName( 'search_engine' );
  foreach my $h ( @ses ) {
    my $status = $h->getAttribute( 'status' );
    my $id = $h->getAttribute( 'id' );
    my $type = $h->getAttribute( 'type' );
    my $content = { id => $id, type => $type, };
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
  foreach my $item ( @{$self->{_inner_items}} ) {
    my $content = $item->{content};
    $xml_string .= '<search_engine ' . 'status="' . $item->{status} . '" id="' . $content->{id} . '" type="' . $content->{type} . '"' . '>';
    $xml_string .= '<group>' .                $content->{group}    . '</group>';
    $xml_string .= '<hidden>'.                $content->{hidden}   .'</hidden>';
    $xml_string .= '<is_post>'.               $content->{is_post}  .'</is_post>';
    $xml_string .= '<personal_bar_pos>' .     $content->{pb_pos}   . '</personal_bar_pos>';
    $xml_string .= '<show_in_personal_bar>' . $content->{in_pb}    . '</show_in_personal_bar>';
    $xml_string .= '<title>' .                $content->{title}    . '</title>';
    $xml_string .= '<uri>'.                   $content->{uri}      . '</uri>';
    $xml_string .= '<key>'.                   $content->{key}      . '</key>';
    $xml_string .= '<encoding>' .             $content->{encoding} . '</encoding>';
    $xml_string .= ( $content->{post_query} ne q{} ) ? '<post_query>' . $content->{post_query} . '</post_query>' : '<post_query/>';
    $xml_string .= ( $content->{icon} ne q{} ) ? '<icon>' . $content->{icon} . '</icon>' : '<icon/>';
    $xml_string .= '</search_engine>';
  }
  return $xml_string;
}

1; # Magic true value required at end of module
__END__

#  /*
#   * <search_engine status="added" id="91BA29D03CBE8A40B439D0AEAC790289" type="normal">
#   * <group>custom</group>
#   * <hidden>0</hidden>
#   * <is_post>0</is_post>
#   * <personal_bar_pos>-1</personal_bar_pos>
#   * <show_in_personal_bar>0</show_in_personal_bar>
#   * <title>Wikipedia</title>
#   * <uri>http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s</uri>
#   * <key>jw</key>
#   * <encoding>UTF-8</encoding>
#   * <post_query/>
#   * <icon/>
#   * </search_engine>
#   */

=head1 NAME

Net::OperaLink::Data::SearchEngine - [One line description of module's purpose here]


=head1 VERSION

This document describes Net::OperaLink::Data::SearchEngine version 0.0.1

=head1 SYNOPSIS

    use Net::OperaLink::Data::SearchEngine;
    my $se = Net::OperaLink::Data::SearchEngine->new;
    $se->add( { } );

=head1 DESCRIPTION

    OperaLink Search Engine content.


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

Net::OperaLink::Data::SearchEngine requires no configuration files or environment variables.

=head1 DEPENDENCIES

XML::LibXML
URI

=head1 INCOMPATIBILITIES

None reported.


=head1 BUGS AND LIMITATIONS

No bugs have been reported.

Please report any bugs or feature requests to
C<bug-net-operalink-data-searchengine@rt.cpan.org>, or through the web interface at
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
