use strict;
use warnings;
use Test::More;
use Net::OperaLink::Data::TypedHistory;

plan tests => 2;
{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  $h->from_opera_link_xml( 
    q{<typed_history status="added" content="ashula.info" type="text"><last_typed>2010-04-14T18:22:42Z</last_typed></typed_history>} .
    q{<typed_history status="added" content="&quot;foo bar &amp;'()*&lt;>" type="text"><last_typed>2010-04-14T18:50:18Z</last_typed></typed_history>} );
  $h->add( { content => 'あ' } );
  is $h->del( { content => 'あ' } ), 2;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  $h->from_opera_link_xml( 
    q{<typed_history status="added" content="ashula.info" type="text"><last_typed>2010-04-14T18:22:42Z</last_typed></typed_history>} .
    q{<typed_history status="added" content="&quot;foo bar &amp;'()*&lt;>" type="text"><last_typed>2010-04-14T18:50:18Z</last_typed></typed_history>} );
  $h->add( { content => 'あ' } );
  is $h->del( { content => 'ashula.info' } ), 2;
}
