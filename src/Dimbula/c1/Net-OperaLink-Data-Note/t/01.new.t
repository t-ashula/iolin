use strict;
use warnings;
use Test::More tests => 1;

use Net::OperaLink::Data::Note;

my $h = Net::OperaLink::Data::Note->new;
isa_ok $h, 'Net::OperaLink::Data::Note';
