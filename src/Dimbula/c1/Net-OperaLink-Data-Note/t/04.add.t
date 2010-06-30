use strict;
use warnings;
use Test::More tests => 9;

use Net::OperaLink::Data::Note;

{
  my $h = Net::OperaLink::Data::Note->new;
  is $h->add(), -1, 'no arg. test';
}

{
  my $h = Net::OperaLink::Data::Note->new;
  is $h->add( 'hoge' ), -1, 'not ref arg test';
}

{
  my $h = Net::OperaLink::Data::Note->new;
  is $h->add( { aa => 'aa' } ), -1, 'invalid ref arg test';
}

{
  my $h = Net::OperaLink::Data::Note->new;
  is $h->add( {
	       content => 'note',
	      } ), 1, 'valid content arg test';
}

{
  my $h = Net::OperaLink::Data::Note->new;
  is $h->add( {
	       type => 'note',
	       content => 'note',
	      } ), 1, 'note valid content arg test';
}

{
  my $h = Net::OperaLink::Data::Note->new;
  is $h->add( { 
	       type => 'folder',
	       title => 'a a',
	      } ), 1, 'folder valid content arg test';
}

{
  my $h = Net::OperaLink::Data::Note->new;
  $h->add( { content => '1' } );
  is $h->add( { content => '2' } ), 2, 'two content add test';
}

{
  my $h = Net::OperaLink::Data::Note->new;
  $h->add( { 
	    content => '1', 
	    id => 'DD6212DB87BD0C44A683EF1B4B6F9A03',
	   } );
  is $h->add( { 
	       content => '1', 
	       id => 'DD6212DB87BD0C44A683EF1B4B6F9A03',
	       parent => 'A9AAFED0976111DC85AC8946BEF8D2DC',
	      } ), 2, 'add same id but different parent content test';
}

{
  my $h = Net::OperaLink::Data::Note->new;
  $h->add( { 
	    content => '1', 
	    id => 'DD6212DB87BD0C44A683EF1B4B6F9A03',
	    parent => 'A9AAFED0976111DC85AC8946BEF8D2DC',
	   } );
  is $h->add( { 
	       content => '1', 
	       id => 'DD6212DB87BD0C44A683EF1B4B6F9A03',
	       parent => 'A9AAFED0976111DC85AC8946BEF8D2DC',
	      } ), 1, 'cant add same content test';
}

__END__
public Guid ID { get; set; }
  public Guid Parent { get; set; }
  public Guid Previous { get; set; }
  public DateTime Created { get; set; }
  public Uri Uri { get; set; }
  public String Content { get; set; } /// as Title when Type == Folder or Type == Trash
  public enum NoteType { Note, Folder, Trash } ;
public NoteType Type { get; set; }
  
