use strict;
use warnings;
use Test::More tests => 1;

use Net::OperaLink::Data::SpeedDial;

my $s = Net::OperaLink::Data::SpeedDial->new;
isa_ok $s, 'Net::OperaLink::Data::SpeedDial';
