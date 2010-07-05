use strict;
use warnings;
use Test::More ;
use Test::Warn;

plan tests => 6;

use Net::OperaLink::Data::TypedHistory;

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  is $h->from_opera_link_xml(), -1;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  is $h->from_opera_link_xml( q{<typed_history status="added" content="ashula.info" type="text"><last_typed>2010-04-14T18:22:42Z</last_typed></typed_history>} ), 1;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  is $h->from_opera_link_xml( 
			     q{<typed_history status="added" content="ashula.info" type="text"><last_typed>2010-04-14T18:22:42Z</last_typed></typed_history>} .
			     q{<typed_history status="added" content="&quot;foo bar &amp;'()*&lt;>" type="text"><last_typed>2010-04-14T18:50:18Z</last_typed></typed_history>}
			    ), 2;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  $h->from_opera_link_xml( q{<typed_history status="added" content="ashula.info" type="text"><last_typed>2010-04-14T18:22:42Z</last_typed></typed_history>} );
  is $h->from_opera_link_xml( q{<typed_history status="modified" content="ashula.info" type="selected"><last_typed>2010-04-14T18:24:12Z</last_typed></typed_history>} ), 1;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  $h->from_opera_link_xml( q{<typed_history status="added" content="ashula.info" type="text"><last_typed>2010-04-14T18:22:42Z</last_typed></typed_history>} );
  $h->from_opera_link_xml( q{<typed_history status="modified" content="ashula.info" type="selected"><last_typed>2010-04-14T18:24:12Z</last_typed></typed_history>} );
  is $h->from_opera_link_xml( q{<typed_history status="deleted" content="ashula.info" type="selected" />} ), 0;
}

{
  my $h = Net::OperaLink::Data::TypedHistory->new;
  warning_like { 
    $h->from_opera_link_xml( q{<typed_history status="kicked" content="ashula.info" type="selected"><last_typed>2010-04-14T18:24:12Z</last_typed></typed_history>} );
  } qr/unknown status/;
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
