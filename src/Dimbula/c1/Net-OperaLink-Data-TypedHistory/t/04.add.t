use strict;
use warnings;
use Test::More tests => 7;

use Net::OperaLink::Data::TypedHistory;

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  is $h->add(), -1;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  is $h->add( 'hoge'), -1;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  is $h->add( { aa => 'aa' } ), -1;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  is $h->add( { content => '' } ), 1;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  is $h->add( { content => 'a a', type => 'search' } ), 1;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  $h->add( { content => '1' } );
  is $h->add( { content => '2' } ), 2;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  $h->add( { content => '1' } );
  is $h->add( { content => '1' } ), 1;
}



