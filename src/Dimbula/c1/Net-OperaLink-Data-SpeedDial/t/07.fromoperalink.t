use strict;
use warnings;
use Test::More ;
use Test::Warn;

plan tests => 6;

use Net::OperaLink::Data::SpeedDial;

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->from_opera_link_xml(), -1, 'no xml test';
}
my $sd1_add = q{<speeddial status="added" position="7"><title>t. @ hatena</title><uri>http://d.hatena.ne.jp/t_ashula/</uri><reload_enabled>0</reload_enabled><reload_interval>0</reload_interval><reload_only_if_expired>0</reload_only_if_expired><icon>iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAADjUlEQVR42h2T6VKaBxhGuZzG6dQlmzZjNFp1NIALUXRABAEFRVxwwYCiUhQSRDRBcYlLA4nGqIlKdOIaQ+LSNu04rVprOpNp0h9N+qO9gdOvvYL3Oed5XpFWnkxG4mcopBdwt90gPFLBw9FyxnyF2KozsKiuYsxLQCOJxWsXE+iW0tcpZTxoxGLOR6QpyKQ4OxmzKpu7Lg1zUxaehhsZ8ajx2UqY8Orw1OfSWZNLT8sNbtsL8XaoGPJX096iRmRWyTCXSvE5tCzPdLG3fZeDFwEWxpsIdpYx0WvkjqMUf5sKr62UMX8No/5aBnqq6LYbEHkdBgY9VWxEfLz9JcIf73c4+uERK9NtjLkr8bQo6Gr4D0eGv6OcyKyTpVkXU4Ot+Jy1iJ4v9/H6RZB379b59PGAvz694ex4if1NP09DDuwmGXXlYnpaVUwIl7dWBjj8cY7dlyEeh32IotuD7EfHeP/hNX//c8jHP3c5+ekxBzsCRqgda4WYZr2UULDxf8Tvdh9werrOr6db7L2aQ3R/qJ6ZKRsvoyEWl0YIBrvo7THhdmi41a4WuEvwtSvp79HzYMLGt/sz/PZ2m98/fM/R0Rai2clmpscbmA53olblEhNzjosJn1MivUqdTozLKsdWK8Oguk65UkjTVMHMzBA70XmeLY8jmuzXMuCU42mToy/NIiUpgS/Px6FT5GCvL8JalUeV+jpFuV9xITaemHNfIMsTY2uu5GZDOaJhdwlt5kyadNdoMWbT3S5E7zTi69IyFaynz6XDrJOSm5VCalIiaVcSyctKRSXsp7WhEtHWym1sZgkmVQqDXi2ba16iG/08CdnY27nH5uodvF066vR53LSo0CslyHNSMQg4oW/8iI6PFhgNWGmqKaL/loHFWRtri52sLXRzdhLh58N5JgLN9HZoCAsSXXYtVpOCZ0vDnJwJEg9eBVhe8OB2VuOwahj2V7H0yM52xMub6D12VgcIuKtx25T4eyporFXQbNHxfP0+x6fbwg42XUQWHPQJq2sUUljrigj0Gng40sSkt5pBpw6HINPeIKfVUoZWr0ImL0JeLMNkEn4huvE1c6FaOiwStMUpKPOT0chTMakzMRSno5enoci/hkySjqJEhr5CQ6VRh7JUTkFBjiBxtYP5cAN9TgWNBgnF4iTEqedJS4wj+WIsKZfjhVrjuRQXx5XLl8jOSKekMJ8yZSG1ZjX/AkWtf6r1qUEqAAAAAElFTkSuQmCC</icon></speeddial>};
{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->from_opera_link_xml( $sd1_add ), 1, 'add one test';
}
my $sd2_add = q{<speeddial status="added" position="1"><icon/><reload_only_if_expired>0</reload_only_if_expired><reload_enabled>0</reload_enabled><reload_interval>0</reload_interval><uri>http://redir.opera.com/speeddials/portal/</uri><title>Opera Portal beta</title></speeddial>};

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->from_opera_link_xml( $sd1_add . $sd2_add ), 2, 'add two test';
}

my $sd2_mod = q{<speeddial status="modified" position="1"><title>Opera Portal beta</title><uri>http://redir.opera.com/speeddials/portal/</uri><reload_enabled>0</reload_enabled><reload_interval>2147483646</reload_interval><reload_only_if_expired>0</reload_only_if_expired><icon/></speeddial>};
{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  $h->from_opera_link_xml( $sd2_add );
  is $h->from_opera_link_xml( $sd2_mod ), 1, 'add and mod test';
}

my $sd2_del = q{<speeddial status="deleted" position="1"><title>Opera Portal beta</title><uri>http://redir.opera.com/speeddials/portal/</uri><reload_enabled>0</reload_enabled><reload_interval>2147483646</reload_interval><reload_only_if_expired>0</reload_only_if_expired><icon/></speeddial>};
{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  $h->from_opera_link_xml( $sd2_add );
  $h->from_opera_link_xml( $sd2_mod );
  is $h->from_opera_link_xml( $sd2_del ), 0, 'add, mod and del test';
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  warning_like { 
    $h->from_opera_link_xml( q{<speeddial status="kicked" position="1"><title>Opera Portal beta</title><uri>http://redir.opera.com/speeddials/portal/</uri><reload_enabled>0</reload_enabled><reload_interval>2147483646</reload_interval><reload_only_if_expired>0</reload_only_if_expired><icon/></speeddial>} );
  } qr/unknown status/;
}


__END__


