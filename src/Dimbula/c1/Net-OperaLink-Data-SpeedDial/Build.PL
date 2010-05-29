use strict;
use warnings;
use Module::Build;

my $builder = Module::Build->new(
    module_name         => 'Net::OperaLink::Data::SpeedDial',
    license             => 'perl',
    dist_author         => 't.ashula <t.ashula@gmail.com>',
    dist_version_from   => 'lib/Net/OperaLink/Data/SpeedDial.pm',
    requires => {
        'Test::More' => 0,
        'version'    => 0,
    },
    add_to_cleanup      => [ 'Net-OperaLink-Data-SpeedDial-*' ],
);

$builder->create_build_script();
