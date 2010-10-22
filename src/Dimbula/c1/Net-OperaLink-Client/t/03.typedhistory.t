use strict;
use warnings;

use utf8;
use Config::Pit qw/pit_get/;
use Test::More tests => 4;

use Net::OperaLink::Client;

{
  my $account = pit_get("link.opera.com",
			require =>{ "username" => "user name on my.opera.com",
				    "password" => "password on my.opera.com" } );
  my $ua = Net::OperaLink::Client->new( { user => $account->{username}, pass => $account->{password}, state => 0 }  );
  $ua->sync_typed( 0 );
  my $state = $ua->add_typed_history();
  is $state->{'state'}, 'not_support';
  is $state->{'state'}, 'not_support';
  is $state->{'state'}, 'not_support';
  is $state->{'state'}, 'not_support';
  undef $ua;
}

