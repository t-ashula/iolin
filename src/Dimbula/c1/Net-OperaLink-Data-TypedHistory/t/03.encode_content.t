use strict;
use warnings;
use Test::More tests => 7;

use Net::OperaLink::Data::TypedHistory;

{
  my $t = Net::OperaLink::Data::TypedHistory->new;
  is $t->_encode_content(), '';
}

{
  my $t = Net::OperaLink::Data::TypedHistory->new;
  is $t->_encode_content('hoge'), 'hoge';
}

{
  my $t = Net::OperaLink::Data::TypedHistory->new;
  is $t->_encode_content('<'), '&lt;';
}

{
  my $t = Net::OperaLink::Data::TypedHistory->new;
  is $t->_encode_content('"'), '&quot;';
}

{
  my $t = Net::OperaLink::Data::TypedHistory->new;
  is $t->_encode_content('&'), '&amp;';
}

{
  my $t = Net::OperaLink::Data::TypedHistory->new;
  is $t->_encode_content('<"'), '&lt;&quot;';
}

{
  my $t = Net::OperaLink::Data::TypedHistory->new;
  is $t->_encode_content('<"&'), '&lt;&quot;&amp;';
}


