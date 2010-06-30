use strict;
use warnings;
use Test::More tests => 1;

use Net::OperaLink::Data::SearchEngine;

my $ses = Net::OperaLink::Data::SearchEngine->new;
isa_ok $ses, 'Net::OperaLink::Data::SearchEngine';
