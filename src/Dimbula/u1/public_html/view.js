/*
 *
 *  
 */
/// global/alias
if ( !_D ){ var _D = document; }
if ( !_O ){ var _O = opera; }
if ( !_L ){ var _L = location; }
if ( !_W ){ var _W = window; }

/// application
var IDataView = function( o ) {
  this.init( o );
};
IDataView.prototype.init = function(o){
  var self = this;
  for ( var k in o ){
    if ( o.hasOwnProperty( k ) ) {
      self[ k ] = o[ k ];
    }
  }
  self.heads = self.cols.map( function( c ){ return c.h; } );
  self.keys = self.cols.map( function( c ){ return { 'key' : c.k, 'toStr' : c.toStr }; } );
};
IDataView.prototype.gridViewId = function(){ return this.id + '_gird'; };
IDataView.prototype.rawViewId  = function(){ return this.id + '_raw'; };
IDataView.prototype.richViewId = function(){ return this.id + '_rich'; };
IDataView.prototype.createView = function( raw ) {
  var self = this;
  var frm = new IOLIN.forms( self.id, self.title );
  if ( !raw ){
    return frm;
  }
  frm.clean();
  try{
    var data = JSON.parse( raw )[ self.key ];      
    frm.addContent( self.createGridView( data ) );
    frm.addContent( self.createRawView( data ) );
    frm.addContent( self.createRichView( data ) );
  } catch (x) {
    errmsg( 'WRANING', self.title + ' json parse error.' + x + '\n' + raw );
  }
  return frm;
};
IDataView.prototype.createGridView = function( data ){
  var self = this;
  var grid = N( 'table', { 'id' : self.gridViewId(), 'class' : 'grid data maximized' } );
  var heads = self.heads;
  var keys = self.keys;
  heads.forEach( function ( ele ) {
    grid.firstOrDefault( 'thead' ).firstOrDefault( 'tr' ).appendChild( N( 'th', {}, T( ele ) ) );
  } );
  data.forEach( function( ele ) {
    var row = N( 'tr' );
    keys.forEach( function( k ){ 
      row.appendChild( N( 'td', {}, T( k.toStr ? k.toStr( ele[ k.key ] ) : ele[ k.key ] ) ) );
    } );
    grid.firstOrDefault( 'tbody' ).appendChild( row );
  } );
  return grid;
};
IDataView.prototype.createRawView = function( data ) {
  return N( 'div', { 'id' : this.rawViewId(), 'class' : 'raw data maximized' }, N( 'textarea', {}, T( JSON.stringify( data ) ) ) );
};
IDataView.prototype.createRichViewBox = function ( data ){
  return N( 'div', { 'id' : this.richViewId(), 'class' : 'rich data maximized' }, N( 'h2', {}, T( this.key ) ) );  
};
IDataView.prototype.createRichView = function( data ) {
  return this.createRichViewBox( data );
};

var dataViewers = {};
dataViewers[ 'urlfilter' ] = new IDataView({
  'id'     : 'uf'
  ,'title' : 'Content Block Rule'
  ,'key'   : 'urlfilter'
  ,'cols'  : [
    {  'h' : 'Status',  'k' : 'data_status', 'toStr' : null }
    ,{ 'h' : 'Type',    'k' : 'type',        'toStr' : null }
    ,{ 'h' : 'Content', 'k' : 'content',     'toStr' : null }
    ,{ 'h' : 'UUID',    'k' : 'id',          'toStr' : null }
  ]
  ,'createRichView' : function( data ) {
    var self = this;
    var exlist = N( 'ul', { 'class' : 'ufexclude' } );
    var inlist = N( 'ul', { 'class' : 'ufinclude' } );
    var delev = function( e ){
      for ( var s = e.target; !( /ufdetail/.test( s.getAttribute( 'class' ) ) ); s = s.parentNode );
      var p = s.querySelector( '.delkey' );
      deleteLinkData( { 'urlfilter' : [ p.innerText ] } );
    };
    data.forEach(
      function( d ) {
        if ( d.type ) {
          var delbtn = N( 'span', { 'class' : 'del' }, T( 'X' ) );
          delbtn.addEventListener( 'click', delev, false );
          var li = N( 'li', { 'class' : 'ufdetail' },
                      N( 'div', { 'class' : 'cb' },  delbtn ),
                      N( 'p', {},
                         N( 'span', { 'class':'delkey' }, T( d.id ) ),
                         N( 'span', {}, T( d.content ) ) ) ) ;
          if ( d.type === 'exclude' ) {
            exlist.appendChild( li );           
          }
          else if( d.type === 'include' ){
            inlist.appendChild( li );
          }
        }
      });
    var rich = self.createRichViewBox( data );
    rich.appendChild( exlist );
    rich.appendChild( inlist );
    return rich;
  }
});

dataViewers[ 'typed_history' ] = new IDataView({
  'id'    : 'th'
  ,'title' : 'Typed History'
  ,'key'   : 'typed_history'
  ,'cols'  : [
    { 'h' : 'Status',  'k' : 'data_status', 'toStr' : null }
    ,{ 'h' : 'Date',    'k' : 'last_typed',  'toStr' : null }
    ,{ 'h' : 'Type',    'k' : 'type',        'toStr' : null }
    ,{ 'h' : 'Content', 'k' : 'content',     'toStr' : null }
  ]
  ,'createRichView' : function( data ) {
    var self = this;
    var rich = self.createRichViewBox( data );
    //N( 'div', { 'id' : this.richViewId(), 'class' : 'rich data maximized' }, N( 'h3', {}, T( self.title  ) ) );
    var createTHBox = function( d ){
      var delbtn = N( 'span', { 'class' : 'del' }, T( 'X' ) );
      delbtn.addEventListener( 'click', function( e ){
        for ( var s = e.target; !( /thdetail/.test( s.getAttribute( 'class' ) ) ); s = s.parentNode );
        var p = s.querySelector( '.delkey' );
        deleteLinkData( { 'typed_history' : [ p.innerText ] } );
      }, false );
      return N( 'div', { 'class' : 'thdetail' },
                N( 'div', { 'class' : 'cb' },  delbtn ),
                N( 'p', {'class' : 'thcontent' + ' ' + d[ 'type' ] },
                   N( 'span', { 'class' : 'datetime' }, T( d[ 'last_typed' ] ) ),
                   N( 'span', { 'class' : 'delkey content' }, T( d[ 'content' ] ) )
                 ) );
    };
    data.forEach( function( d ){
      rich.appendChild( createTHBox( d ) );
    });
    return rich;
  }
});

dataViewers[ 'speeddial' ] = new IDataView( {
  'id'    : 'sd'
  ,'title' : 'Speed Dial'
  ,'key'   : 'speeddial'
  ,'cols'  : [
     { 'h' : 'Status',      'k' : 'data_status',  'toStr' : null }
    ,{ 'h' : 'Position',    'k' : 'position',     'toStr' : null }
    ,{ 'h' : 'Title',       'k' : 'title',        'toStr' : null }
    ,{ 'h' : 'URI',         'k' : 'uri',          'toStr' : null }
    ,{ 'h' : 'Reload',      'k' : 'reload',       'toStr' : function(v){ return v === '0' ? 'Disable' : 'Enable'; } }
    ,{ 'h' : 'Only Expire', 'k' : 'expired_only', 'toStr' : function(v){ return v === '0' ? 'Interval' : 'Expire Only'; } }
    ,{ 'h' : 'Interval',    'k' : 'interval',     'toStr' : null }
  ]
});
dataViewers[ 'search_engine' ] = new IDataView({
  'id'     : 'se'
  ,'title' : 'Search Engine'
  ,'key'   : 'search_engine'
  ,'cols'  : [
    {  'h' : 'Status',               'k' : 'data_status', 'toStr' : null }
    ,{ 'h' : 'ID',                   'k' : 'uuid',        'toStr' : null  }
    ,{ 'h' : 'Type',                 'k' : 'type',        'toStr' : null  }
    ,{ 'h' : 'Group',                'k' : 'group',       'toStr' : null  }
    ,{ 'h' : 'Key',                  'k' : 'key',         'toStr' : null  }
    ,{ 'h' : 'Title',                'k' : 'title',       'toStr' : null  }
    ,{ 'h' : 'URI',                  'k' : 'uri' ,        'toStr' : null  }
    ,{ 'h' : 'Encoding',             'k' : 'encoding',    'toStr' : null  }
    ,{ 'h' : 'Post?',                'k' : 'is_post',     'toStr' : null  }
    ,{ 'h' : 'Post Query',           'k' : 'post_query',  'toStr' : null  }
    ,{ 'h' : 'Show in Personal Bar', 'k' : 'show_in_personal_bar', 'toStr' : null  }
    ,{ 'h' : 'Personal Bar Postion', 'k' : 'personal_bar_pos',     'toStr' : null  }
    ,{ 'h' : 'Deleted?',             'k' : 'deleted', 'toStr' : null  }
    ,{ 'h' : 'ICON',                 'k' : 'icon',    'toStr' : null  }
  ]
});
dataViewers[ 'note' ] = new IDataView( {
  'id'    : 'nt'
  ,'title' : 'Notes'
  ,'key'   : 'note'
  ,'cols'  : [
    {  'h' : 'Status',  'k' : 'data_status', 'toStr' : null }
    ,{ 'h' : 'Type',    'k' : 'type',        'toStr' : null }
    ,{ 'h' : 'Content', 'k' : 'content',     'toStr' : null }
    ,{ 'h' : 'Title',   'k' : 'title',       'toStr' : null }
    ,{ 'h' : 'URI',     'k' : 'uri',         'toStr' : null }
    ,{ 'h' : 'Created', 'k' : 'created',     'toStr' : null }
    ,{ 'h' : 'ID',      'k' : 'id',          'toStr' : null }
    ,{ 'h' : 'Parent',  'k' : 'parent',      'toStr' : null }
    ,{ 'h' : 'Prev',    'k' : 'previous',    'toStr' : null }
  ]
});
dataViewers[ 'bookmark' ] = new IDataView( {
  'id'    : 'bm'
  ,'title' : 'Bookmarks'
  ,'key'   : 'bookmark'
  ,'cols'  : [
    {  'h' : 'Status',  'k' : 'data_status', 'toStr' : null }
    ,{ 'h' : 'Type',    'k' : 'type',        'toStr' : null }
    ,{ 'h' : 'URI',     'k' : 'uri',         'toStr' : null }
    ,{ 'h' : 'Title',   'k' : 'title',       'toStr' : null }
    ,{ 'h' : 'Created', 'k' : 'created',     'toStr' : null }
    ,{ 'h' : 'ID',      'k' : 'id',          'toStr' : null }
    ,{ 'h' : 'Show in Personal Bar',  'k' : 'show_in_personal_bar', 'toStr' : null  }
    ,{ 'h' : 'Personal Bar Position', 'k' : 'personal_bar_pos',     'toStr' : null  }
    ,{ 'h' : 'Show in Panel',         'k' : 'show_in_panel',        'toStr' : null  }
    ,{ 'h' : 'Panel Position',        'k' : 'panel_pos',     'toStr' : null  }
    ,{ 'h' : 'Parent',  'k' : 'parent',      'toStr' : null }
    ,{ 'h' : 'Prev',    'k' : 'previous',    'toStr' : null }
  ]
});


