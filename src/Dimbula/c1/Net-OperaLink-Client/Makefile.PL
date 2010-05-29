use strict;
use warnings;
use ExtUtils::MakeMaker;

WriteMakefile(
    NAME                => 'Net::OperaLink::Client',
    AUTHOR              => 't.ashula <t.ashula@gmail.com>',
    VERSION_FROM        => 'lib/Net/OperaLink/Client.pm',
    ABSTRACT_FROM       => 'lib/Net/OperaLink/Client.pm',
    PL_FILES            => {},
    PREREQ_PM => {
		  'Test::More' => 0,
		  'version'    => 0,
		  'URI' => 0,
		  'LWP' => 0,
		  'XML::Simple' => 0,
    },
    dist                => { COMPRESS => 'gzip -9f', SUFFIX => 'gz', },
    clean               => { FILES => 'Net-OperaLink-Client-*' },
);
