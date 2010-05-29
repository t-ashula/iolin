use strict;
use warnings;
use Test::More;
use Net::OperaLink::Data::TypedHistory;

plan tests => 6;

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  is $h->del(), -1;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  is $h->del( 'hoge' ), -1;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  is $h->del( { aa => 'aa' } ), -1;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  is $h->del( { content => '' } ), 0;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  $h->add( { content => '1' } );
  is $h->del( { content => '2' } ), 1;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  $h->add( { content => '1' } );
  is $h->del( { content => '1' } ), 0;
}



