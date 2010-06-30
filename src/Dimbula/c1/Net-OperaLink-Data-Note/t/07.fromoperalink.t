use strict;
use warnings;
use Test::More ;
use Test::Warn;

plan tests => 6;

use Net::OperaLink::Data::Note;

{
  my $h = Net::OperaLink::Data::Note->new;
  is $h->from_opera_link_xml(), -1, 'no arg failed test';
}

my $note1_add = q{<note status="added" id="DD6212DB87BD0C44A683EF1B4B6F9A03" parent="" previous="" created="2010-03-25T16:15:23Z"><uri>http://d.hatena.ne.jp/t_ashula/</uri><content xml:space="preserve"> font-family:normal; </content></note>};
{
  my $h = Net::OperaLink::Data::Note->new;
  is $h->from_opera_link_xml( $note1_add ), 1, 'valid note test';
}
my $folder1_add = q{<note_folder status="added" id="A9AAFED0976111DC85AC8946BEF8D2DC" parent="" previous="" type="trash" created="2010-05-25T16:03:01Z"><title>Trash</title></note_folder>};
{
  my $h = Net::OperaLink::Data::Note->new;
  is $h->from_opera_link_xml( $folder1_add ), 1, 'valid note_folder test';
}

my $note1_mod = q{<note status="modified" id="DD6212DB87BD0C44A683EF1B4B6F9A03" parent="" previous="" created="2010-03-25T16:15:23Z"><uri>http://d.hatena.ne.jp/t_ashula/</uri><content xml:space="preserve"> normal; </content></note>};
{
  my $h = Net::OperaLink::Data::Note->new;
  $h->from_opera_link_xml( $note1_add );
  is $h->from_opera_link_xml( $note1_mod ), 1, 'valid mod test';
}

my $note1_del = q{<note status="deleted" id="DD6212DB87BD0C44A683EF1B4B6F9A03" parent="" previous="" created="2010-03-25T16:15:23Z"><uri>http://d.hatena.ne.jp/t_ashula/</uri><content xml:space="preserve"> normal; </content></note>};
{
  my $h = Net::OperaLink::Data::Note->new;
  $h->from_opera_link_xml( $note1_add );
  is $h->from_opera_link_xml( $note1_del ), 0, 'valid del test';
}

{
  my $h = Net::OperaLink::Data::Note->new;
  warning_like { 
    $h->from_opera_link_xml( q{<note status="boo" ></note>} );
  } qr/unknown status/;
}

__END__

