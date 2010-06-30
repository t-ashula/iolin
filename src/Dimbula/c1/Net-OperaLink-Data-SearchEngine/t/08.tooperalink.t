use strict;
use warnings;
use Test::More tests => 4;

use Net::OperaLink::Data::SearchEngine;

my $se1_add =<< 'SEARCH_ENGINE_1';
<search_engine status="added" id="9CEC3B4F1BA36745AB1C839046963CE3" type="normal"><group>custom</group><hidden>0</hidden><is_post>0</is_post><personal_bar_pos>-1</personal_bar_pos><show_in_personal_bar>0</show_in_personal_bar><title>Wikipedia</title><uri>http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA</uri><key>jw</key><encoding>UTF-8</encoding><post_query/><icon/></search_engine>
SEARCH_ENGINE_1
my $se2_add =<< 'SEARCH_ENGINE_2';
<search_engine status="added" id="9CEC3B4F1BA36745AB1C839046963CE3" type="normal"><group>custom</group><hidden>0</hidden><is_post>0</is_post><personal_bar_pos>-1</personal_bar_pos><show_in_personal_bar>0</show_in_personal_bar><title>Wikipedia</title><uri>http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA</uri><key>jw</key><encoding>UTF-8</encoding><post_query/><icon>AA</icon></search_engine>
SEARCH_ENGINE_2

my $se3_add =<< 'SEARCH_ENGINE_3';
<search_engine status="added" id="9CEC3B4F1BA36745AB1C839046963CE3" type="normal"><group>custom</group><hidden>0</hidden><is_post>0</is_post><personal_bar_pos>-1</personal_bar_pos><show_in_personal_bar>0</show_in_personal_bar><title>Wikipedia</title><uri>http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA</uri><key>jw</key><encoding>UTF-8</encoding><post_query>%s</post_query><icon/></search_engine>
SEARCH_ENGINE_3
$se1_add =~ s{\n}{}g;
$se2_add =~ s{\n}{}g;
$se3_add =~ s{\n}{}g;

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  is $h->to_opera_link_xml(), "", "empty xml";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { 
	    uri   => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
	    key   => 'jw',
	    title => 'Wikipedia',
	    id    => '9CEC3B4F1BA36745AB1C839046963CE3',
	   } );
  is $h->to_opera_link_xml(), $se1_add;
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { 
	    uri   => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
	    key   => 'jw',
	    title => 'Wikipedia',
	    id    => '9CEC3B4F1BA36745AB1C839046963CE3',
	    icon => 'AA',
	   } );
  is $h->to_opera_link_xml(), $se2_add;
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { 
	    uri   => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
	    key   => 'jw',
	    title => 'Wikipedia',
	    id    => '9CEC3B4F1BA36745AB1C839046963CE3',
	    post_query  => '%s',
	   } );
  is $h->to_opera_link_xml(), $se3_add;
}

__END__




