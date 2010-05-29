use strict;
use warnings;
use Test::More;
use Net::OperaLink::Data::SpeedDial;

plan tests => 9;
{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->mod(), -1, 'no arg test';
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->mod( 'hoge' ), -1, 'invalid arg type test';
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->mod( { aa => 'aa' } ), -1, 'invalid hash test';
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->mod( { position => 1 } ), -1, 'missing uri test';
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->mod( { uri => 'hoge' } ), -1, 'missing position test';
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->mod( { uri => 'hoge', position => 1 } ), 0, 'valid test';
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  $h->add( { position => 2, uri => 'hoge' } );
  is $h->mod( { position => 3, uri => 'hoge'  } ), 1, 'no item found';
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  $h->add( { position => 3, uri => 'hoge' } );
  $h->del( { position => 3, uri => 'hoge' } );
  is $h->mod( { position => 3, uri => 'huga' } ), 0, 'item deleted';
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  $h->add( { position => 3, uri => 'hoge' } );
  is $h->mod( { position => 3, uri => 'huga' } ), 1, 'mod item test';
}
