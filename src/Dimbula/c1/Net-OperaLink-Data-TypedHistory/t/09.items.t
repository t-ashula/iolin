use strict;
use warnings;
use Test::More;
use Net::OperaLink::Data::TypedHistory;

plan tests => 3;
{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  my @i = $h->items;
  is @i, 0, 'empty array';
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  $h->add( { content => 'ashula.info', last_typed => '2010-04-14T18:22:42Z' } );
  my @i = $h->items;
  is_deeply @i, ( { content => 'ashula.info', last_typed => '2010-04-14T18:22:42Z', type => 'text' } );
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  $h->add( { content => '<"&>', last_typed => '2010-04-14T18:22:42Z' } );
  my @i = $h->items;
  is_deeply @i, ( { content => '<"&>', last_typed => '2010-04-14T18:22:42Z', type => 'text' } );
}
