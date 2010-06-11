use strict;
use warnings;
use Test::More;
use Net::OperaLink::Data::SpeedDial;

plan tests => 3;
{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  my @i = $h->items;
  is @i, 0, 'empty array';
}

my $sd3_add = { position => 1, uri => 'http://redir.opera.com/speeddials/portal/', title => 'Opera Portal beta' } ;
my $sd3_full = {
		position => 1, 
		uri => 'http://redir.opera.com/speeddials/portal/', 
		title => 'Opera Portal beta',
		icon => '',
		reload_enabled => 0,
		reload_interval => 0,
		reload_only_if_expired => 0 };
		 

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  $h->add( $sd3_add );
  my @i = $h->items;
  is_deeply @i, ( $sd3_full );
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  $h->add( $sd3_add );
  $h->mod( { position => 1, uri => 'http://ashula.info' } );
  my @i = $h->items;
  my $p = $sd3_full;
  $p->{uri} = 'http://ashula.info';
  is_deeply @i, ( $p );
}
