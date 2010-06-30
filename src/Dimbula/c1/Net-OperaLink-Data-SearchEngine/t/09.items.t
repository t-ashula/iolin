use strict;
use warnings;
use Test::More;
use Net::OperaLink::Data::SearchEngine;

plan tests => 12;
{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  my @i = $h->items;
  is @i, 0, 'empty array';
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { 
	    uri   => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
	    key   => 'jw',
	    title => 'Wikipedia',
	    id    => '9CEC3B4F1BA36745AB1C839046963CE3',
	   } );
  my @i = $h->items;
  is_deeply @i, ({
		  uri   => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
		  key   => 'jw',
		  title => 'Wikipedia',
		  id    => '9CEC3B4F1BA36745AB1C839046963CE3',
		  icon  => '',
		  pb_pos => -1,
		  in_pb  => 0,
		  group  => 'custom',
		  type   => 'normal',
		  is_post => 0,
		  post_query => '',
		  hidden => 0,
		  encoding => 'UTF-8',
		 });
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { 
	    uri   => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
	    key   => 'jw',
	    title => 'Wikipedia',
	    id    => '9CEC3B4F1BA36745AB1C839046963CE3',
	   } );
 $h->mod( { 
	    uri   => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
	    key   => 'jwjw',
	    title => 'Wikipedia',
	    id    => '9CEC3B4F1BA36745AB1C839046963CE3',
	   } );
  my @i = $h->items;
  is_deeply @i, ({ 
		  uri        => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
		  key        => 'jwjw',
		  title      => 'Wikipedia',
		  id         => '9CEC3B4F1BA36745AB1C839046963CE3',
		  icon       => '',
		  pb_pos     => -1,
		  in_pb      => 0,
		  group      => 'custom',
		  type       => 'normal',
		  is_post    => 0,
		  post_query => '',
		  hidden     => 0,
		  encoding   => 'UTF-8',
		 });
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { 
	    uri   => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
	    key   => 'jw',
	    title => 'Wikipedia',
	    id    => '9CEC3B4F1BA36745AB1C839046963CE3',
	    personal_bar_pos    => 2,
	   } ), 1;
  my @i = $h->items;
  is_deeply @i, ({ 
		  uri        => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
		  key        => 'jw',
		  title      => 'Wikipedia',
		  id         => '9CEC3B4F1BA36745AB1C839046963CE3',
		  icon       => '',
		  pb_pos     => 2,
		  in_pb      => 0,
		  group      => 'custom',
		  type       => 'normal',
		  is_post    => 0,
		  post_query => '',
		  hidden     => 0,
		  encoding   => 'UTF-8',
		 });
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { 
	    uri   => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
	    key   => 'jw',
	    title => 'Wikipedia',
	    id    => '9CEC3B4F1BA36745AB1C839046963CE3',
	    personal_bar_pos    => 2,
	    show_in_personal_bar    => 1,
	   } );
  my @i = $h->items;
  is_deeply @i, ({ 
		  uri        => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
		  key        => 'jw',
		  title      => 'Wikipedia',
		  id         => '9CEC3B4F1BA36745AB1C839046963CE3',
		  icon       => '',
		  pb_pos     => 2,
		  in_pb      => 1,
		  group      => 'custom',
		  type       => 'normal',
		  is_post    => 0,
		  post_query => '',
		  hidden     => 0,
		  encoding   => 'UTF-8',
		 });
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { 
	    uri   => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
	    key   => 'jw',
	    title => 'Wikipedia',
	    id    => '9CEC3B4F1BA36745AB1C839046963CE3',
	    personal_bar_pos    => 2,
	    show_in_personal_bar    => 1,
	    group => 'desktop_default',
	   } );
  my @i = $h->items;
  is_deeply @i, ({ 
		  uri        => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
		  key        => 'jw',
		  title      => 'Wikipedia',
		  id         => '9CEC3B4F1BA36745AB1C839046963CE3',
		  icon       => '',
		  pb_pos     => 2,
		  in_pb      => 1,
		  group      => 'desktop_default',
		  type       => 'normal',
		  is_post    => 0,
		  post_query => '',
		  hidden     => 0,
		  encoding   => 'UTF-8',
		 });
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { 
	       uri   => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
	       key   => 'jw',
	       title => 'Wikipedia',
	       id    => '9CEC3B4F1BA36745AB1C839046963CE3',
	       personal_bar_pos    => 2,
	       show_in_personal_bar    => 1,
	       is_post => 1,
	      } );
  my @i = $h->items;
  is_deeply @i, ({ 
		  uri        => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
		  key        => 'jw',
		  title      => 'Wikipedia',
		  id         => '9CEC3B4F1BA36745AB1C839046963CE3',
		  icon       => '',
		  pb_pos     => 2,
		  in_pb      => 1,
		  group      => 'custom',
		  type       => 'normal',
		  is_post    => 1,
		  post_query => '',
		  hidden     => 0,
		  encoding   => 'UTF-8',
		 });
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { 
	       uri   => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
	       key   => 'jw',
	       title => 'Wikipedia',
	       id    => '9CEC3B4F1BA36745AB1C839046963CE3',
	       personal_bar_pos    => 2,
	       show_in_personal_bar    => 1,
	       is_post => 1,
	       post_query => '%s',
	      } );
  my @i = $h->items;
  is_deeply @i, ({ 
		  uri        => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
		  key        => 'jw',
		  title      => 'Wikipedia',
		  id         => '9CEC3B4F1BA36745AB1C839046963CE3',
		  icon       => '',
		  pb_pos     => 2,
		  in_pb      => 1,
		  group      => 'custom',
		  type       => 'normal',
		  is_post    => 1,
		  post_query => '%s',
		  hidden     => 0,
		  encoding   => 'UTF-8',
		 });
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { 
	       uri   => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
	       key   => 'jw',
	       title => 'Wikipedia',
	       id    => '9CEC3B4F1BA36745AB1C839046963CE3',
	       personal_bar_pos    => 2,
	       show_in_personal_bar    => 1,
	       is_post => 1,
	       post_query => '%s',
	       icon => 'A',
	   } );
  my @i = $h->items;
  is_deeply @i, ({ 
		  uri        => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
		  key        => 'jw',
		  title      => 'Wikipedia',
		  id         => '9CEC3B4F1BA36745AB1C839046963CE3',
		  icon       => 'A',
		  pb_pos     => 2,
		  in_pb      => 1,
		  group      => 'custom',
		  type       => 'normal',
		  is_post    => 1,
		  post_query => '%s',
		  hidden     => 0,
		  encoding   => 'UTF-8',
		 });
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { 
	       uri   => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
	       key   => 'jw',
	       title => 'Wikipedia',
	       id    => '9CEC3B4F1BA36745AB1C839046963CE3',
	       personal_bar_pos    => 2,
	       show_in_personal_bar    => 1,
	       group => 'custom',
	       is_post => 1,
	       post_query => '%s',
	       icon => 'A',
	       encoding => 'Shift_JIS',
	   } );
  my @i = $h->items;
  is_deeply @i, ({ 
		  uri        => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
		  key        => 'jw',
		  title      => 'Wikipedia',
		  id         => '9CEC3B4F1BA36745AB1C839046963CE3',
		  icon       => 'A',
		  pb_pos     => 2,
		  in_pb      => 1,
		  group      => 'custom',
		  type       => 'normal',
		  is_post    => 1,
		  post_query => '%s',
		  hidden     => 0,
		  encoding   => 'Shift_JIS',
		 });
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { 
	       uri   => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
	       key   => 'jw',
	       title => 'Wikipedia',
	       id    => '9CEC3B4F1BA36745AB1C839046963CE3',
	       personal_bar_pos    => 2,
	       show_in_personal_bar    => 1,
	       group => 'custom',
	       is_post => 1,
	       post_query => '%s',
	       icon => 'A',
	       encoding => 'Shift_JIS',
	       hidden => 1,
	   } );
  my @i = $h->items;
  is_deeply @i, ({ 
		  uri        => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
		  key        => 'jw',
		  title      => 'Wikipedia',
		  id         => '9CEC3B4F1BA36745AB1C839046963CE3',
		  icon       => 'A',
		  pb_pos     => 2,
		  in_pb      => 1,
		  group      => 'custom',
		  type       => 'normal',
		  is_post    => 1,
		  post_query => '%s',
		  hidden     => 0,
		  encoding   => 'Shift_JIS',
		 });
}

{
  my $h = Net::OperaLink::Data::SearchEngine->new;
  $h->add( { 
	       uri   => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
	       key   => 'jw',
	       title => 'Wikipedia',
	       id    => '9CEC3B4F1BA36745AB1C839046963CE3',
	       type => 'history_search',
	   } );
  my @i = $h->items;
  is_deeply @i, ({ 
		  uri        => 'http://ja.wikipedia.org/w/index.php?title=%E7%89%B9%E5%88%A5%3A%E6%A4%9C%E7%B4%A2&amp;search=%s&amp;go=%E8%A1%A8%E7%A4%BA',
		  key        => 'jw',
		  title      => 'Wikipedia',
		  id         => '9CEC3B4F1BA36745AB1C839046963CE3',
		  icon       => '',
		  pb_pos     => -1,
		  in_pb      => 0,
		  group      => 'custom',
		  type       => 'history_search',
		  is_post    => 0,
		  post_query => '',
		  hidden     => 0,
		  encoding   => 'UTF-8',
		 });
}
