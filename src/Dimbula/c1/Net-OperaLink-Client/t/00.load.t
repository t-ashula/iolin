use Test::More tests => 26;

BEGIN {
use_ok( 'Net::OperaLink::Client' );
}

diag( "Testing Net::OperaLink::Client $Net::OperaLink::Client::VERSION" );

my $ua = Net::OperaLink::Client->new;
isa_ok $ua, 'Net::OperaLink::Client';

is $ua->state, 0;
is $ua->user, '';
is $ua->pass, '';
is $ua->sync_bookmark, 1;
is $ua->sync_panel, 1;
is $ua->sync_speeddial, 1;
is $ua->sync_typed, 1;
is $ua->sync_notes, 1;
is $ua->sync_searches, 1;
is $ua->platform_build, 3344;
is $ua->platform_system, 'win32';
is $ua->platform_device, 'desktop';

$ua->state( 20 );             is $ua->state, 20;
$ua->user( 'user' );          is $ua->user, 'user';
$ua->pass( 'pass' );          is $ua->pass, 'pass';
$ua->sync_bookmark( 0 );      is $ua->sync_bookmark, 0;
$ua->sync_panel( 0 );         is $ua->sync_panel, 0;
$ua->sync_speeddial( 0 );     is $ua->sync_speeddial, 0;
$ua->sync_typed( 0 );         is $ua->sync_typed, 0;
$ua->sync_notes( 0 );         is $ua->sync_notes, 0;
$ua->sync_searches( 0 );      is $ua->sync_searches, 0;
$ua->platform_build('1234');  is $ua->platform_build, 1234;
$ua->platform_system('mac');  is $ua->platform_system, 'mac';
$ua->platform_device('mini'); is $ua->platform_device, 'mini';

__END__

