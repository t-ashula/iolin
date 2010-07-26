use strict;
use warnings;
use Test::More;
use Net::OperaLink::Data::TypedHistory;

plan tests => 7;
{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  is $h->mod(), -1;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  is $h->mod( 'hoge'), -1;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  is $h->mod( { aa => 'aa' } ), -1;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  is $h->mod( { content => '' } ), 0;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  $h->add( { content => '3' } );
  is $h->mod( { content => '4' } ), 1;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  $h->add( { content => '3' } );
  $h->del( { content => '3' } );
  is $h->mod( { content => '3' } ), 0;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  $h->add( { content => '1' } );
  is $h->mod( { content => '1' } ), 1;
}



