/**
 * OperaLink On Unite
 */

/// global
var _O = opera;
var _S = opera.io.webserver;
var _W = widget;

if ( ! Array.prototype.last ) {
  Array.prototype.last = function() {
    var l = this.length >>> 0;
    return l > 0 ? this[ l - 1 ] : null;
  };
}

/// utils
var inspect=function( o, s ) {
  for( var i in o ){
    s += i + ":" + o[ i ] + ";\n";
  }
  return s;
};

function args( ev ) {
  return {
     'con' : ev.connection
    ,'res' : ev.connection.response
    ,'req' : ev.connection.request
  };
}

// setPreferenceForKey
// preferenceForKey
function GlobalPreference( key ) {
  return unescape( window.localStorage.getItem( key ) );
}
function setGlobalPreference( key, val ) {
  window.localStorage.setItem( key, escape( val ) );
}

/// application
var username = GlobalPreference( 'username' );
var password = GlobalPreference( 'password' );
var syncstate = Number( GlobalPreference( 'syncstate' ) ) || 0;
var client = new IOLIN.client( {
  'username':username
  ,'password':password
  ,'syncstate':syncstate
  ,'conf' : {} } );
client.addManager( 'typed_history', IOLIN.Data.TypedHistoryManager );
client.addManager( 'speeddial',     IOLIN.Data.SpeedDialManager );
client.addManager( 'search_engine', IOLIN.Data.SearchEngineManager );
client.addManager( 'note',          IOLIN.Data.NoteManager );
client.addManager( 'bookmark',      IOLIN.Data.BookmarkManager );

function showIndex( ev ){
  var arg = args( ev );
  var con = arg.con, res = arg.res;
  if ( con.isOwner ){
    setPrivateIndexResponse( arg );
  }
  else {
    setPublicIndexResponse( arg );
  }
}

function setPublicIndexResponse( arg ) {
  var res = arg.res, req = arg.req;
  res.setStatusCode( '403', 'forbidden' );
  res.write( 
    ['<title>403</title>'
     ,'<p>forbidden</p>'
     ,'<p><a href="http://admin.', req.host, _S.currentServicePath, '">Admin view</a></p>'
    ].join('') );
  res.close(); 
}

function setPrivateIndexResponse( arg ) {
  var res = arg.res, req = arg.req, con = arg.con;
  var tmpl = new Markuper( 'templates/index.html' );
  username = GlobalPreference( 'username' );
  password = GlobalPreference( 'password' );
  var data = {
    'title'       : 'Opera Link On Opera Unite'
    ,'servicePath' : _S.currentServicePath
    ,'inspect'     : '' //inspect( , "Widget\n" )
    ,'stylesheet'  : 'style.css'
    ,'username'    : username
    ,'password'    : password
    ,'pullkey'     : ''
  };
  tmpl.parse( data );
  res.write( tmpl.html() );
  res.close(); 
}

function getLinkData( ev ) {
  var arg = args( ev );
  var con = arg.con, req = arg.req, res = arg.res;
  var type = req.uri.split( '/' )[ 3 ];
  var link = client.LinkData( type );
  if ( link.response === 200 ){
    setGlobalPreference( 'username', client.UserName() );
    setGlobalPreference( 'password', client.PassWord() );
    setGlobalPreference( 'syncstate', client.SyncState() );
  }
  res.write( JSON.stringify( link ) );
  res.close();
}

function postLinkData( ev ) {
  var arg = args( ev );
  var res = arg.res;
  res.write( '' );
  res.close();
}

function setConfigData( ev ) {
  var arg = args( ev );
  var res = arg.res, req = arg.req;
  var params = req.bodyItems;
  for ( var param in params ) { console.log( param + ':' + params[param] ); }
  var u = params[ 'username' ][ 0 ], p = params[ 'password' ][ 0 ];
  setGlobalPreference( 'username', u );
  setGlobalPreference( 'password', p );
  client.UserName( u );
  client.PassWord( p );
  res.close();
}

function catchOtherRequest( ev ) {
  var arg = args( ev );
  var res = arg.res, req = arg.req, con = arg.con;
  var tmpl = new Markuper( 'templates/index.html' );
  var data = {
    'title'       : 'Opera Link On Opera Unite'
    ,'servicePath' : _S.currentServicePath
    ,'inspect'     : inspect( req, "Request\n" )
    ,'stylesheet'  : 'style.css'
    ,'username'    : username
    ,'password'    : password
  };
  tmpl.parse( data );
  res.write( tmpl.html() );
  res.close();   
}

/// entry points
window.onload = function(){
  _S.addEventListener( '_index', showIndex,    false );
  _S.addEventListener( 'get',    getLinkData,  false );
  _S.addEventListener( 'post',   postLinkData, false );
  _S.addEventListener( 'conf',   setConfigData, false );
//  _S.addEventListener( '_request', catchOtherRequest,    false );
};

