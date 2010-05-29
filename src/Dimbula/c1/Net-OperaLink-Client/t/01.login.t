
use strict;
use warnings;
use Config::Pit qw/pit_get/;
use Test::More tests => 2;

use Net::OperaLink::Client;

{
  my $ua = Net::OperaLink::Client->new;
  my $state = $ua->login;
  is $state->{'state'}, 'failed';
  undef $ua;
}

{
  my $account = pit_get("link.opera.com",
			require =>{ "username" => "user name on my.opera.com",
				    "password" => "password on my.opera.com" } );
  my $ua = Net::OperaLink::Client->new( { user => $account->{username}, pass => $account->{password} }  );
  my $state = $ua->login;
  is $state->{'state'}, 'ok';
  undef $ua;
}





