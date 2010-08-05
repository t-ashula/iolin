use strict;
use warnings;
use Test::More;
use Net::OperaLink::Data::SearchEngine;

plan tests => 6;

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  is $h->del(), -1, "no arg del test";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  is $h->del( 'hoge' ), -1, "invalid arg (no ref) del test";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  is $h->del( { aa => 'aa' } ), -1, "invalid arg del test";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  is $h->del( { id => '' } ), 0, "no item delete test";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { uri => 'example.com', key => 'a', id => 'aaa' } );
  is $h->del( { id => 'bbb' } ), 1, "no item delete test";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { uri => 'example.com', key => 'a', id => 'aaa' } );
  is $h->del( { id => 'aaa' } ), 0, "delete test";
}



