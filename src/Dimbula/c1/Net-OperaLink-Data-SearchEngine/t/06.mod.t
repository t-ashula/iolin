use strict;
use warnings;
use Test::More;
use Net::OperaLink::Data::SearchEngine;

plan tests => 14;
{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  is $h->mod(), -1, "no arg mod test";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  is $h->mod( 'hoge' ), -1, "invalid arg test";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  is $h->mod( { aa => 'aa' } ), -1, "invalid arg test 2";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  is $h->mod( { id => '' } ), 0, "no mod item found test";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { uri=>'hoge', key => 'foo', id => 'barbaz' } );
  is $h->mod( { id => 'piyo' } ), 1, "no mod item found test";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { uri=>'hoge', key => 'foo', id => 'barbaz' } );
  $h->del( { id =>'barbaz' } );
  is $h->mod( { id => 'barbaz' } ), 0, "item already deleted";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { uri => 'hoge', key => 'foo', id => 'barbaz' } );
  is $h->mod( { id => 'barbaz', key => 'bar' } ), 1;
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { uri => 'hoge', key => 'foo', id => 'barbaz', encoding => 'Shift_JIS' } );
  is $h->mod( { id => 'barbaz', encoding => 'UTF-8' } ), 1;
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { uri => 'hoge', key => 'foo', id => 'barbaz', post_query => 'Aaa%s' } );
  is $h->mod( { id => 'barbaz', post_query => 'BBB%s' } ), 1;
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { uri => 'hoge', key => 'foo', id => 'barbaz', icon => 'Aaa' } );
  is $h->mod( { id => 'barbaz', icon => 'BBB' } ), 1;
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { uri => 'hoge', key => 'foo', id => 'barbaz', is_post => 0 } );
  is $h->mod( { id => 'barbaz', is_post => 1 } ), 1;
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { uri => 'hoge', key => 'foo', id => 'barbaz' } );
  is $h->mod( { id => 'barbaz', personal_bar_pos => 4 } ), 1;
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { uri => 'hoge', key => 'foo', id => 'barbaz' } );
  is $h->mod( { id => 'barbaz', show_in_personal_bar => 1 } ), 1;
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { uri => 'hoge', key => 'foo', id => 'barbaz' } );
  is $h->mod( { id => 'barbaz', hidden => 1 } ), 1;
}



