use strict;
use warnings;
use Test::More ;
use Test::Warn;

plan tests => 6;

use Net::OperaLink::Data::SearchEngine;

my $se1_add =<< 'SEARCH_ENGINE_1';
  <search_engine status="added" id="9CEC3B4F1BA36745AB1C839046963CE3" type="normal">
    <group>custom</group>
    <hidden>0</hidden>
    <is_post>0</is_post>
    <personal_bar_pos>-1</personal_bar_pos>
    <show_in_personal_bar>0</show_in_personal_bar>
    <title>Wikipedia</title>
    <uri>http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA</uri>
    <key>jw</key>
    <encoding>UTF-8</encoding>
    <post_query/>
    <icon/>
  </search_engine>
SEARCH_ENGINE_1

my $se2_add = <<'SEARCH_ENGINE_2';
  <search_engine status="added" id="91BA29D03CBE8A40B439D0AEAC790289" type="normal">
    <group>custom</group>
    <hidden>0</hidden>
    <is_post>0</is_post>
    <personal_bar_pos>-1</personal_bar_pos>
    <show_in_personal_bar>0</show_in_personal_bar>
    <title>Wikipedia</title>
    <uri>http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s</uri>
    <key>jw</key>
    <encoding>UTF-8</encoding>
    <post_query/>
    <icon/>
  </search_engine>
SEARCH_ENGINE_2

my $se1_mod = <<'SE1_MOD';
<search_engine status="modified" id="9CEC3B4F1BA36745AB1C839046963CE3" type="normal">
  <group>custom</group><key>jwiki</key>
</search_engine>
SE1_MOD

my $se1_del = <<'SE1_DEL';
<search_engine status="deleted" id="9CEC3B4F1BA36745AB1C839046963CE3"/>
SE1_DEL

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  is $h->from_opera_link_xml(), -1;
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  is $h->from_opera_link_xml( $se1_add ), 1, "add one engine from operalinke";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  is $h->from_opera_link_xml( $se1_add . $se2_add ), 2, "add two engine from operalink";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->from_opera_link_xml( $se1_add );
  is $h->from_opera_link_xml( $se1_mod ), 1, "add and mod item from opera link ";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->from_opera_link_xml( $se1_add );
  $h->from_opera_link_xml( $se1_mod );
  is $h->from_opera_link_xml( $se1_del ), 0, "add / mod / delete ";
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  warning_like { 
    $h->from_opera_link_xml( q{'<search_engine status="unknown" id="9CEC3B4F1BA36745AB1C839046963CE3"/>} );
  } qr/unknown status/;
}

__END__

