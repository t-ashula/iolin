use strict;
use warnings;
use Test::More tests => 9;

use Net::OperaLink::Data::SpeedDial;

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->add(), -1;
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->add( 'hoge' ), -1;
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->add( { aa => 'aa' } ), -1, "invalid data";
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->add( { position => 1 } ), -1, "invalid data;missing uri";
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->add( { uri => 'http://example.com' } ), -1, "invalid data;missing position";
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  is $h->add( { uri => 'http://example.com', position => 1 } ), 1, "add one dial";
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  $h->add( { uri => 'http://example.com', position => 1 } );
  is $h->add( { uri => 'http://example.com', position => 2 } ), 2, "add two dials";
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  $h->add( { uri => 'http://example.com', position => 1 } );
  is $h->add( { uri => 'http://example.com', position => 1 } ), 1, "can not add same position";
}

{
  my $h = Net::OperaLink::Data::SpeedDial->new;
  $h->add( { uri => 'http://example.com', position => 1, icon => 'iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAADjUlEQVR42h2T6VKaBxhGuZzG6dQlmzZjNFp1NIALUXRABAEFRVxwwYCiUhQSRDRBcYlLA4nGqIlKdOIaQ+LSNu04rVprOpNp0h9N+qO9gdOvvYL3Oed5XpFWnkxG4mcopBdwt90gPFLBw9FyxnyF2KozsKiuYsxLQCOJxWsXE+iW0tcpZTxoxGLOR6QpyKQ4OxmzKpu7Lg1zUxaehhsZ8ajx2UqY8Orw1OfSWZNLT8sNbtsL8XaoGPJX096iRmRWyTCXSvE5tCzPdLG3fZeDFwEWxpsIdpYx0WvkjqMUf5sKr62UMX8No/5aBnqq6LYbEHkdBgY9VWxEfLz9JcIf73c4+uERK9NtjLkr8bQo6Gr4D0eGv6OcyKyTpVkXU4Ot+Jy1iJ4v9/H6RZB379b59PGAvz694ex4if1NP09DDuwmGXXlYnpaVUwIl7dWBjj8cY7dlyEeh32IotuD7EfHeP/hNX//c8jHP3c5+ekxBzsCRqgda4WYZr2UULDxf8Tvdh9werrOr6db7L2aQ3R/qJ6ZKRsvoyEWl0YIBrvo7THhdmi41a4WuEvwtSvp79HzYMLGt/sz/PZ2m98/fM/R0Rai2clmpscbmA53olblEhNzjosJn1MivUqdTozLKsdWK8Oguk65UkjTVMHMzBA70XmeLY8jmuzXMuCU42mToy/NIiUpgS/Px6FT5GCvL8JalUeV+jpFuV9xITaemHNfIMsTY2uu5GZDOaJhdwlt5kyadNdoMWbT3S5E7zTi69IyFaynz6XDrJOSm5VCalIiaVcSyctKRSXsp7WhEtHWym1sZgkmVQqDXi2ba16iG/08CdnY27nH5uodvF066vR53LSo0CslyHNSMQg4oW/8iI6PFhgNWGmqKaL/loHFWRtri52sLXRzdhLh58N5JgLN9HZoCAsSXXYtVpOCZ0vDnJwJEg9eBVhe8OB2VuOwahj2V7H0yM52xMub6D12VgcIuKtx25T4eyporFXQbNHxfP0+x6fbwg42XUQWHPQJq2sUUljrigj0Gng40sSkt5pBpw6HINPeIKfVUoZWr0ImL0JeLMNkEn4huvE1c6FaOiwStMUpKPOT0chTMakzMRSno5enoci/hkySjqJEhr5CQ6VRh7JUTkFBjiBxtYP5cAN9TgWNBgnF4iTEqedJS4wj+WIsKZfjhVrjuRQXx5XLl8jOSKekMJ8yZSG1ZjX/AkWtf6r1qUEqAAAAAElFTkSuQmCC' } );
  is $h->add( { uri => 'http://example.com', position => 1 } ), 1, "can not add same position";
}
