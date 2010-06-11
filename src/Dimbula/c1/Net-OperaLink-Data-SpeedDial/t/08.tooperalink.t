use strict;
use warnings;
use Test::More tests => 3;

use Net::OperaLink::Data::SpeedDial;

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->to_opera_link_xml(), "";
}

my $sd2_add = q{<speeddial status="added" position="1"><icon></icon><reload_only_if_expired>0</reload_only_if_expired><reload_enabled>0</reload_enabled><reload_interval>0</reload_interval><uri>http://redir.opera.com/speeddials/portal/</uri><title>Opera Portal beta</title></speeddial>};

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  $h->add( { position => 1, uri => 'http://redir.opera.com/speeddials/portal/', title => 'Opera Portal beta' } );
  is $h->to_opera_link_xml(), $sd2_add;
}

my $sd2_del = q{<speeddial status="deleted" position="1" />};
{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  $h->add( { position => 1, uri => 'http://redir.opera.com/speeddials/portal/', title => 'Opera Portal beta' } );
  $h->del( { position => 1, uri => 'http://redir.opera.com/speeddials/portal/' } );
  is $h->to_opera_link_xml(), $sd2_del;
}

__END__




