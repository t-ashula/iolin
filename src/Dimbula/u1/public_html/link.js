/*
 *
 *  
 */
/// global/alias
var _D = document;
var _O = opera;
var _L = location;
var _W = window;

/// Extends
if ( !Array.prototype.aggregate ){
  Array.prototype.aggregate = function(){
    var agg = arguments[ 0 ], func = arguments[ 1 ], s = 0;
    if ( typeof agg === 'function' ){
      func = agg;
      agg = this[ 0 ];
      s = 1;
    }
    for ( var i = s, l = this.length; i < l; ++i ) { 
      agg = func( agg, this[ i ] ); 
    }
    return agg;
  };
}

/// tiny utils
function N() {
  var e = document.createElement( arguments[ 0 ] );
  var l = arguments.length;
  if ( l > 1 ) {
    var attrs = arguments[ 1 ];
    for ( var k in attrs ) {
      if ( attrs.hasOwnProperty( k ) ) {
        e.setAttribute( k, attrs[ k ] );
      }
    }
    for ( var i = 2; i < l; ++i ) {
      e.appendChild( arguments[ i ] );
    }
  }
  return e;
}
function T( txt ) { 
  return document.createTextNode( txt ); 
}
function $( id, ctx ) {
  return (ctx || document).getElementById( id ); 
}
function firstOrDefault( p, f ) {
  var e = p.getElementsByTagName( f )[ 0 ];
  if ( !e ) { 
    e = N( f );
    p.appendChild( e );
  }
  return e;
}
if ( !HTMLElement.prototype.firstOrDefault ){
  HTMLElement.prototype.firstOrDefault = function( tag ){
    return firstOrDefault( this, tag );
  };
}

function currentServicePath(){
  return _L.pathname.split( '/' )[ 1 ];
}

/// GUI
if ( ! dimbula ){
  var dimbula = {};
}
if ( !dimbula.ui ) {
  dimbula.ui = {};
}
(function(){
  var _F = function(){};
  _F.prototype._title = "";
  _F.prototype.title = function( t ){ return ( t ) ? ( this._title = t ) : this._title; };
  dimbula.ui.forms = _F;
}(_W));

var ListView = function(){};

/// application
var IDataView = function( o ) {
  return function( raw ) { 
    this.id_    = o.id;
    this.title_ = o.title;
    this.key_   = o.key;
    this.cols_  = o.cols;
    this.gridViewId_ = this.id_ + '_grid'; 
    this.rawViewId_ = this.id_ + '_raw';
    this.pullId_ = this.id_ + '_pull';
    this.heads_ = this.cols_.map( function(c){ return c.h; } );
    this.keys_ = this.cols_.map( function(c){ return { 'key' : c.k, 'toStr' : c.toStr }; } );
    var self = this;
    var div = $( this.id_ );
    if ( !div ) {
      div = N( 'div', { 'id' : this.id_ } );      
      document.body.appendChild( div );
    }
    while ( div.firstChild ) {
      div.removeChild( div.firstChild );
    }
    // create control box
    div.appendChild( N('h2', {'class':'titlebar'}, T( self.title_ ) ) );
    function minimize(ele){
      ele.setAttribute(
        'class',
        ele.getAttribute('class').replace( /maximized/, 'minimized' ) ) ;
    }
    function maximize(ele){
      ele.setAttribute(
        'class',
        ele.getAttribute('class').replace( /minimized/, 'maximized' ) ) ;
    }
    (function(){
      var ce = function(e){
        var t = e.target, pp = t.parentNode.parentNode;
        switch ( t.getAttribute('class') ){
          case 'minimize': minimize(pp.querySelector('.detail')); minimize(pp.querySelector('.raw')); break;
          case 'maximize': maximize(pp.querySelector('.detail')); maximize(pp.querySelector('.raw')); break;
          case 'close' : pp.style.height = 0; break;
        }
      };
      
      var cbox = N('div', { 'class' : 'controlbox' } );
      [{ c : 'minimize', t : '_'},{ c : 'maximize', t : '[]'},{ c : 'close', t : 'X' } ].forEach(
        function(o){
          var cb = N( 'span', { 'class' : o.c }, T( o.t ) );
          cb.addEventListener('click', ce, false );
          cbox.appendChild( cb );
        }
      );
      div.appendChild( cbox );
    }());
    // create grid view
    var data = JSON.parse( raw )[ this.key_ ];    
    var grid = N( 'table', { 'id': this.gridViewId_, 'class':'detail data maximized' } );
    var heads = this.heads_;
    var keys = this.keys_;
    heads.forEach(
      function ( ele ) {
        grid.firstOrDefault( 'thead' ).firstOrDefault( 'tr' ).appendChild( N( 'th', {}, T( ele ) ) );
      }
    );
    try {
      data.forEach(
        function( ele ) {
          var row = N( 'tr' );
          keys.forEach( 
            function( k ){ 
              var v = k.toStr ? k.toStr( ele[ k.key ] ) : ele[ k.key ];
              row.appendChild( N( 'td', {}, T( v ) ) );
            } 
          );
          grid.firstOrDefault( 'tbody' ).appendChild( row );
        }   
      );
    } catch ( x ) {
      alert( this.title_ + ' json parse error.' + x + '\n' + raw );
    }
    div.appendChild( grid );
    // create raw view
    div.appendChild( N( 'textarea', { 'id' : this.rawViewId_, 'class' : 'raw data maximized' }, T( JSON.stringify( data ) ) ) );
    // create pull button
    var btn = N( 'button', { 'id': this.pullId_ }, T( 'Pull ' + this.title_ ) );
    var key = this.key_;
    btn.addEventListener( 'click', function(e){ pullLinkData( key ); }, false );
    div.appendChild( btn );
    return div;
  };
};

var createViewHandlers = {
  'urlfilter' : new IDataView({
    'id'    : 'uf'
    ,'title' : 'Content Block Rule'
    ,'key'   : 'urlfilter'
    ,'cols'  : [
       { 'h' : 'Status',  'k' : 'data_status', 'toStr' : null }
      ,{ 'h' : 'Type',    'k' : 'type',        'toStr' : null }
      ,{ 'h' : 'Content', 'k' : 'content',     'toStr' : null }
    ]
  })
  ,'typed_history' : new IDataView({
    'id'    : 'th'
    ,'title' : 'Typed History'
    ,'key'   : 'typed_history'
    ,'cols'  : [
       { 'h' : 'Status',  'k' : 'data_status', 'toStr' : null }
      ,{ 'h' : 'Date',    'k' : 'last_typed',  'toStr' : null }
      ,{ 'h' : 'Type',    'k' : 'type',        'toStr' : null }
      ,{ 'h' : 'Content', 'k' : 'content',     'toStr' : null }
    ]
  })
  ,'speeddial' : new IDataView( {
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
  })
  ,'search_engine' : new IDataView({
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
  })
  ,'note' : new IDataView( {
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
  })
  ,'bookmark' : new IDataView( {
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
  })
};
/**/
function createOperaLinkView( raw ){
  for( var key in createViewHandlers ) {
    if ( createViewHandlers.hasOwnProperty( key ) ){
      createViewHandlers[ key ]( raw );
    }
  }
}

function pullLinkData( type ){
  var url = 'get/';
  var handler = null;
  if ( !type || type === '' ) {
    handler = createOperaLinkView;
  } else {
    url += type + '/';
    handler = createViewHandlers[ type ];
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
  
  var btn = N( 'button', { 'id' : 'conf', 'value' : 'config' }, T( 'config' ) ) ;
  btn.addEventListener('click', function(e){ setConfigs(); }, false );
  div.appendChild( btn );
  div.querySelector('#username').value = acc.username;
  div.querySelector('#password').value = acc.password;
  return div;
}

function setConfigs(){
  var xhr = new XMLHttpRequest();
  var u = $( 'username' ).value, p = $( 'password' ).value;
  var params = 'username=' + u + '&password=' + p ;
  xhr.open( 'POST', 'conf' );
  xhr.setRequestHeader( 'Content-Type', 'application/x-www-form-urlencoded' );
  //  xhr.setRequestHeader( 'Content-Length', params.length );
  xhr.onreadystatechange = function(ev) {
    console.log( xhr );
    if ( xhr.readyState === 4 ) {
      if ( xhr.status === 200 ) {
        acc = { username : u, password : p };        
        createConfigForm();
      }
    }
  };
  xhr.send( params );
}
