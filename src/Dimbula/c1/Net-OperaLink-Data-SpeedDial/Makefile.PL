use strict;
use warnings;
use ExtUtils::MakeMaker;

WriteMakefile(
    NAME                => 'Net::OperaLink::Data::SpeedDial',
    AUTHOR              => 't.ashula <t.ashula@gmail.com>',
    VERSION_FROM        => 'lib/Net/OperaLink/Data/SpeedDial.pm',
    ABSTRACT_FROM       => 'lib/Net/OperaLink/Data/SpeedDial.pm',
    PL_FILES            => {},
    PREREQ_PM => {
        'Test::More' => 0,
        'version'    => 0,
    },
    dist                => { COMPRESS => 'gzip -9f', SUFFIX => 'gz', },
    clean               => { FILES => 'Net-OperaLink-Data-SpeedDial-*' },
);
