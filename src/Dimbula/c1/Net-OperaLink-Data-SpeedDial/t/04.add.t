use strict;
use warnings;
use Test::More tests => 8;

use Net::OperaLink::Data::SpeedDial;

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->add(), -1;
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->add( 'hoge' ), -1;
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->add( { aa => 'aa' } ), -1, "invalid data";
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->add( { position => 1 } ), -1, "invalid data;missing uri";
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->add( { uri => 'http://example.com' } ), -1, "invalid data;missing position";
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->add( { uri => 'http://example.com', position => 1 } ), 1, "add one dial";
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  $h->add( { uri => 'http://example.com', position => 1 } );
  is $h->add( { uri => 'http://example.com', position => 2 } ), 2, "add two dials";
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  $h->add( { uri => 'http://example.com', position => 1 } );
  is $h->add( { uri => 'http://example.com', position => 1 } ), 1, "can not add same position";
}



