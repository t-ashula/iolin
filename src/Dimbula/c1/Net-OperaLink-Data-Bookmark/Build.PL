use strict;
use warnings;
use Module::Build;

my $builder = Module::Build->new(
    module_name         => 'Net::OperaLink::Data::Bookmark',
    license             => 'perl',
    dist_author         => 't.ashula <t.ashula@gmail.com>',
    dist_version_from   => 'lib/Net/OperaLink/Data/Bookmark.pm',
    requires => {
        'Test::More' => 0,
        'version'    => 0,
    },
    add_to_cleanup      => [ 'Net-OperaLink-Data-Bookmark-*' ],
);

$builder->create_build_script();
