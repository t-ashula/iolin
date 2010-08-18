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
//function currentServicePath(){ return _L.pathname.split( '/' )[ 1 ]; }

var IDataView = function( o ) {
  this.init(o);
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
  var frm = $( self.id );
  if ( !frm ) {
    frm = new IOLIN.forms( self.id, self.title );
  } else {
    frm.clean();
  }
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
  var grid = N( 'table', { 'id' : self.gridViewId, 'class' : 'grid data maximized' } );
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
  return N( 'textarea', { 'id' : self.rawViewId_, 'class' : 'raw data maximized' }, T( JSON.stringify( data ) ) );
};
IDataView.prototype.createRichView = function( data ) {
  return N( 'div', { 'class' : 'rich maximized' }, N( 'h2', {}, T( this.key ) ) );
};

var dataViewers = {};
dataViewers['typed_history'] = new IDataView({
  'id'    : 'th'
  ,'title' : 'Typed History'
  ,'key'   : 'typed_history'
  ,'cols'  : [
    { 'h' : 'Status',  'k' : 'data_status', 'toStr' : null }
    ,{ 'h' : 'Date',    'k' : 'last_typed',  'toStr' : null }
    ,{ 'h' : 'Type',    'k' : 'type',        'toStr' : null }
    ,{ 'h' : 'Content', 'k' : 'content',     'toStr' : null }
  ]
});
dataViewers['speeddial'] = new IDataView( {
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
dataViewers['search_engine'] = new IDataView({
  'id'     : 'se'
  ,'title' : 'Search Engine'
  ,'key'   : 'search_engine'
  ,'cols'  : [
    { 'h' : 'Status',               'k' : 'data_status', 'toStr' : null }
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
dataViewers['note'] = new IDataView( {
  'id'    : 'nt'
  ,'title' : 'Notes'
  ,'key'   : 'note'
  ,'cols'  : [
    { 'h' : 'Status',  'k' : 'data_status', 'toStr' : null }
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
dataViewers['bookmark'] = new IDataView( {
  'id'    : 'bm'
  ,'title' : 'Bookmarks'
  ,'key'   : 'bookmark'
  ,'cols'  : [
    { 'h' : 'Status',  'k' : 'data_status', 'toStr' : null }
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

/**/
function createOperaLinkView( raw ){
  for( var type in dataViewers ) {
    if ( dataViewers.hasOwnProperty( type ) ){
      var h = dataViewers[ type ];
      var hbox = $( h.id );
      if ( !hbox ){
      }
      else {
        hbox.parentNode.removeChild( hbox );
      }
      _D.body.appendChild( h.createView( raw ).div );
    }
  }
}

function pullLinkData( type ){
  var url = 'get/';
  var handler = null;
  if ( !type || type === '' ) {
    handler = createOperaLinkView;
  }
  else {
    url += type + '/';
    handler = dataViewers[ type ].createView;
  }
  var xhr = new XMLHttpRequest();
  xhr.open( 'GET', url );
  xhr.onreadystatechange = function(){
    if( xhr.readyState === 4 ){
      if ( xhr.status === 200 ) {
        if ( handler ) {
          handler( xhr.responseText );
        }
      }
    }
  };
  xhr.send( null );  
}

function createDataForms(){
  createOperaLinkView();
}

function createConfigForm(){
  var div = $( 'configs' );
  if ( !div ){
    div = N( 'div', { 'id' : 'configs' } );
    _D.body.appendChild( div );
  }
  while ( div.firstChild ) {
    div.removeChild( div.firstChild );
  }
  div.appendChild(
    N( 'dl', { 'id' : 'account'},
      N( 'dt',{}, N( 'label', { 'for'  : 'username' }, T( 'username' ) ) ),
      N( 'dd',{}, N( 'input', { 'type' : 'text', 'id' : 'username' } ) ),
      N( 'dt',{}, N( 'label', { 'for'  : 'password' }, T( 'password' ) ) ),
      N( 'dd',{}, N( 'input', { 'type' : 'password', 'id' : 'password' } ) ) ) );
  div.appendChild(
    N( 'input', { 'type' : 'hidden', 'id' : 'syncstate' } ) );
  div.querySelector('#username').value = acc.username;
  div.querySelector('#password').value = acc.password;
  div.querySelector('#syncstate').value = acc.syncstate;
  
  var btn = N( 'button', { 'id' : 'conf', 'value' : 'config' }, T( 'config' ) ) ;
  btn.addEventListener(
    'click',
    function( e ){
      var xhr = new XMLHttpRequest();
      var u = $( 'username' ).value, p = $( 'password' ).value, s = $( 'syncstate' ).value || 0;
      var params = 'username=' + u + '&password=' + p ;
      xhr.open( 'POST', 'conf' );
      xhr.setRequestHeader( 'Content-Type', 'application/x-www-form-urlencoded' );
      //  xhr.setRequestHeader( 'Content-Length', params.length );
      xhr.onreadystatechange = function(ev) {
        console.log( xhr );
        if ( xhr.readyState === 4 ) {
          if ( xhr.status === 200 ) {
            acc = { username : u, password : p, syncstate : s };        
            createConfigForm();
          }
        }
      };
      xhr.send( params );
    }, false );
  div.appendChild( btn );
  return div;
}

function createDebugForm(){
  return ;
}

_W.addEventListener(
  'DOMContentLoaded',
  function(e){
    createConfigForm();
    createDataForms();
    createDebugForm();
  },
  false
);