use strict;
use warnings;
use Test::More;
use Net::OperaLink::Data::SpeedDial;

plan tests => 9;

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->del(), -1, 'empty arg test';
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->del( 'hoge' ), -1, 'no ref arg test';
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->del( { aa => 'aa' } ), -1, 'invalid hash test';
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->del( { position => 1 } ), -1, 'missing uri test';
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->del( { uri => 'hoge' } ), -1, 'missing position test';
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->del( { uri => 'hoge', position => 2 } ), 0, 'valid arg but no item test';
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  $h->add( { position => 1, uri => 'hoge' } );
  is $h->del( { position => 2, uri => 'huga' } ), 1, 'no delete item test';
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  $h->add( { position => 1, uri => 'hoge' } );
  is $h->del( { position => 1, uri => 'huga' } ), 0, 'valid delete test';
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  $h->add( { position => 1, uri => 'hoge' } );
  $h->del( { position => 1, uri => 'huga' } );
  is $h->del( { position => 1, uri => 'huga' } ), 0, 'cant delete twice test';
}
