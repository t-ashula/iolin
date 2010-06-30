use strict;
use warnings;
use Test::More;
use Net::OperaLink::Data::Note;

plan tests => 7;

{
  my $h = Net::OperaLink::Data::Note->new;
  is $h->del(), -1, 'no arg test';
}

{
  my $h = Net::OperaLink::Data::Note->new;
  is $h->del( 'hoge' ), -1, 'not ref arg test';
}

{
  my $h = Net::OperaLink::Data::Note->new;
  is $h->del( { aa => 'aa' } ), -1, 'not valid test';
}

my $note1 = {
	     content => '1',
	     id => 'DD6212DB87BD0C44A683EF1B4B6F9A03',
	     parent => 'A9AAFED0976111DC85AC8946BEF8D2DC',
	    };
my $note2 = {
	     content => '2',
	     id => 'AA6212DB87BD0C44A683EF1B4B6F9A03',
	     parent => 'A9AAFED0976111DC85AC8946BEF8D2DC',
	    };

{
  my $h = Net::OperaLink::Data::Note->new;
  is $h->del( $note1 ), 0, 'no content test';
}

{
  my $h = Net::OperaLink::Data::Note->new;
  $h->add( $note1 );
  is $h->del( $note2 ), 1, 'not delete test';
}

{
  my $h = Net::OperaLink::Data::Note->new;
  $h->add( $note1 );
  is $h->del( $note1 ), 0, 'valid delete test';
}

{
  my $h = Net::OperaLink::Data::Note->new;
  $h->add( $note1 );
  $h->add( $note2 );
  is $h->del( $note2 ), 1, 'valid delete test 2';
}



