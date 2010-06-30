use strict;
use warnings;
use ExtUtils::MakeMaker;

WriteMakefile(
    NAME                => 'Net::OperaLink::Data::Bookmark',
    AUTHOR              => 't.ashula <t.ashula@gmail.com>',
    VERSION_FROM        => 'lib/Net/OperaLink/Data/Bookmark.pm',
    ABSTRACT_FROM       => 'lib/Net/OperaLink/Data/Bookmark.pm',
    PL_FILES            => {},
    PREREQ_PM => {
        'Test::More' => 0,
        'version'    => 0,
    },
    dist                => { COMPRESS => 'gzip -9f', SUFFIX => 'gz', },
    clean               => { FILES => 'Net-OperaLink-Data-Bookmark-*' },
);
