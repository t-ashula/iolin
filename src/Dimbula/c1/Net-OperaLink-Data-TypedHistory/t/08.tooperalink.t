use strict;
use warnings;
use Test::More tests => 3;

use Net::OperaLink::Data::TypedHistory;

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  is $h->to_opera_link_xml(), "";
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  $h->add( {content => 'ashula.info', last_typed => '2010-04-14T18:22:42Z' } );
  is $h->to_opera_link_xml(), 
    q{<typed_history status="added" content="ashula.info" type="text"><last_typed>2010-04-14T18:22:42Z</last_typed></typed_history>};
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  $h->add( { content => 'ashula.info', last_typed => '2010-04-14T18:22:42Z' } );
  $h->del( { content => 'ashula.info' } );
  is $h->to_opera_link_xml(), 
    q{<typed_history status="deleted" content="ashula.info" type="selected"><last_typed>2010-04-14T18:22:42Z</last_typed></typed_history>};
}

__END__

# <typed_history status="added" content="ashula.info" type="text">
#  <last_typed>2010-04-14T18:22:42Z</last_typed>
# </typed_history>
# <typed_history status="modified" content="ashula.info" type="selected">
#  <last_typed>2010-04-14T18:24:12Z</last_typed>
# </typed_history>
# <typed_history status="deleted" content="ashula.info" type="selected">
#  <last_typed>2010-04-14T18:24:12Z</last_typed>
# </typed_history>
# <typed_history status="added" content="&quot;foo bar &amp;'()*&lt;>" type="text">
#  <last_typed>2010-04-14T18:50:18Z</last_typed>
# </typed_history>
# <typed_history status="added" content="あ" type="text">
#  <last_typed>2010-04-14T18:51:25Z</last_typed>
# </typed_history>




