use strict;
use warnings;
use Test::More tests => 7;

use Net::OperaLink::Data::SearchEngine;

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  is $h->add(), -1, "no arg add test";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  is $h->add( 'hoge' ), -1, "no ref arg add test";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  is $h->add( { aa => 'aa' } ), -1, "invalid arg add test";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  is $h->add( { uri => 'http://example.com/%s' } ), -1, "missing 'key' test";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  is $h->add( { key => 'h' } ), -1, "missing 'uri' test";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  is $h->add( { uri => 'http://example.com/%s', key => 'k' } ), 1, "valid arg add test";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { uri => 'http://example.com/%s', key => 'k' } );
  is $h->add( { uri => 'http://example.org/%s', key => 'kk' } ), 2, "two content add test";
}

