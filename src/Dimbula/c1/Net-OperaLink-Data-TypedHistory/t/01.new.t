use strict;
use warnings;
use Test::More tests => 1;

use Net::OperaLink::Data::TypedHistory;

my $histories = Net::OperaLink::Data::TypedHistory->new;
isa_ok $histories, 'Net::OperaLink::Data::TypedHistory';
