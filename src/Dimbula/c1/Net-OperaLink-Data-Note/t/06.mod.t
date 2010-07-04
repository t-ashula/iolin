use strict;
use warnings;
use Test::More;
use Net::OperaLink::Data::Note;

plan tests => 5;
{
  my $h = Net::OperaLink::Data::Note->new;
  is $h->mod(), -1;
}

{
  my $h = Net::OperaLink::Data::Note->new;
  is $h->mod( 'hoge' ), -1, 'invalid arg test';
}

{
  my $h = Net::OperaLink::Data::Note->new;
  is $h->mod( { aa => 'aa' } ), -1, 'invalid ref arg test';
}
my $note1 = {
	     content => '1',
	     id => 'DD6212DB87BD0C44A683EF1B4B6F9A03',
	     parent => 'A9AAFED0976111DC85AC8946BEF8D2DC',
	    };
my $note1_mod = {
		 content => '1mod',
		 id => 'DD6212DB87BD0C44A683EF1B4B6F9A03',
		 parent => 'A9AAFED0976111DC85AC8946BEF8D2DC',
		};
{
  my $h = Net::OperaLink::Data::Note->new;
  is $h->mod( $note1 ), 0, 'no mod item test';
}

{
  my $h = Net::OperaLink::Data::Note->new;
  $h->add( $note1 );
  is $h->mod( $note1_mod ), 1, 'mod item test';
}




