/**
 * IOLIN / JS OperaLink Client 
 */

///

/// client class

(function( I ){
  var _C = function( o ) {
    var cl = this;
    cl.username_ = o.username;
    cl.password_ = o.password;
    cl.syncstate_ = o.syncstate || 0;
    cl.conf_ = o.conf || {};
    for ( var c in cl.CONFIG ){
      if ( cl.CONFIG.hasOwnProperty( c ) ) {
        cl.conf_[ c ] = cl.conf_[ c ] || cl.CONFIG[ c ];
      }
    }
    cl.init();
  };
  // static
  _C.prototype.CONFIG = {
    'supports' :  {
      'typed_history'  : true
      ,'speeddial'     : false
      ,'search_engine' : false
      ,'note'          : false
      ,'bookmark'      : false
      ,'urlfilter'     : true
    }
  };
  _C.prototype.LINK_API = 'https://link-server.opera.com/pull';
  _C.prototype.AUTH_API = 'https://auth.opera.com/';
  // properties
  _C.prototype.Platform    = function(){ return window.navigator.platform; };
  _C.prototype.BuildNumber = function(){ return window.opera.buildNumber(); };
  _C.prototype.UserName    = function( v ){
    if ( !!v ) { this.username_ = v; }
    return this.username_;
  };
  _C.prototype.PassWord    = function( v ){
    if ( !!v ) { this.password_ = v; }
    return this.password_;
  };
  _C.prototype.SyncState   = function( v ){
    if ( !!v ) { this.syncstate_ = v; }
    return this.syncstate_;
  };
  _C.prototype.lasterror_ = '';
  _C.prototype.LastError = function(){
    return this.lasterror_;
  };
  _C.prototype.linkdata_ = {};
  _C.prototype.LinkData  = function( type ){
    var cl = this;
    if ( cl.needSync() ) {
      cl.SyncLinkData();
    }
    if ( cl.lasterror_ === '' ) {
      if ( cl.isKnownDataType( type ) ) {
        var obj = { 'response' : 200 };
        obj[ type ] = cl.linkdata_[ type ] ;
        return obj;
      }
      return cl.linkdata_;
    }
    return cl.lasterror_;
  };
  _C.prototype.init = function() {
    var cl = this;
    var s = cl.CONFIG[ 'supports' ];
    for ( var key in s ){
      if ( s.hasOwnProperty( key ) ) {
        cl.linkdata_[ key ] = [];
      }
    }
    cl.linkdata_[ 'response' ] = 500;
    cl.lasterror_ = '';
  };
  _C.prototype.needSync = function(){
    return true;
  };
  _C.prototype.isKnownDataType = function( type ) {
    return ( type in this.conf_[ 'supports' ] ) && ( this.conf_[ 'supports' ].hasOwnProperty( type ) );
  };
  _C.prototype.AddSupport    = function( type ) { 
    if ( this.isKnownDataType( type ) ) {
      conf_[ 'supports' ][ type ] = true;
    } 
  };
  _C.prototype.RemoveSupport = function( type ) { 
    if ( this.isKnownDataType( type ) ) {
      conf_[ 'supports' ][ type ] = false;
    } 
  };

  /// to sync data
  _C.prototype.createLinkDataXml = function() {
    return '<data />';
  };
  
  _C.prototype.createClientInfoXml = function() {
    var cl = this;
    var s = cl.conf_[ 'supports' ];
    var sxml = [ '<clientinfo>' ];
    for ( var t in s ){
      if ( s.hasOwnProperty( t ) ) {
        if ( s[ t ] ) {
          sxml[ sxml.length ] = '<supports';
          if ( t === 'search_engine' ) {
            sxml[ sxml.length ] = ' target="desktop"';
          }
          sxml[ sxml.length ] = '>';
          sxml[ sxml.length ] = t;
          sxml[ sxml.length ] = '</supports>';
        }
      }
    }
    sxml[ sxml.length ] = '<build>';
    sxml[ sxml.length ] = cl.BuildNumber();
    sxml[ sxml.length ] = '</build>';
    sxml[ sxml.length ] = '<system>';
    sxml[ sxml.length ] = cl.Platform();
    sxml[ sxml.length ] = '</system>';
    sxml[ sxml.length ] = '</clientinfo>';
    return sxml.join( '' );
  };
  
  _C.prototype.createLinkXml = function() {
    var cl = this;
    return [
       '<?xml version="1.0" encoding="utf-8"?>'
      ,'<link dirty="0" version="1.0" xmlns="http://xmlns.opera.com/2006/link"'
      ,' user="', cl.username_, '"'
      ,' password="', cl.password_, '"'
      ,' syncstate="', cl.syncstate_, '"'
      ,' >'
      ,cl.createClientInfoXml()
      ,cl.createLinkDataXml()
      ,'</link>'].join('');
  };

  _C.prototype.SyncLinkData = function(){
    var cl = this;
    var xhr = new XMLHttpRequest();
    cl.lasterror_ = '';
    xhr.open( 'POST', cl.LINK_API, false );
    xhr.setRequestHeader( 'Content-Type', 'application/x-www-form-urlencoded' );
    xhr.onreadystatechange = function( ev ){
      if( xhr.readyState === 4 ) {
        if ( xhr.status === 200 ) {        
          cl.parseLinkData( xhr.responseText );
        }
        else {
          cl.lasterror_ = 'something error occured. try later. ' + xhr.responseText;
        }
      };      
    };
    xhr.send( cl.createLinkXml() );
  };

  _C.prototype.managers_ = [
  ];

  _C.prototype.addManager = function( k, mng ) {
    var cl = this;
    var idx = cl.managers_.indexOfPred( function( e ){ return e.k === k; } );
    if ( idx < 0 ) {
      idx = cl.managers_.length;
    }
    cl.managers_[ idx ] = { 'k' : k, 'mng' : mng };
  };
  _C.prototype.parseLinkError = function( xml ) {
    var cl = this;
    var dom = new DOMParser().parseFromString( xml, "application/xml" );
    cl.lasterror_ = JSON.stringify( dom );
  };
  _C.prototype.parseLinkData = function( xml ) {
    var cl = this;
    var dom = new DOMParser().parseFromString( xml, "application/xml" );
    for ( var i = 0, mng; mng = cl.managers_[ i ]; ++i ) {
      mng.mng.FromOperaLinkXml( dom );
      cl.linkdata_[ mng.k ] = mng.mng.Items();
    }
  };
  I.client = _C;
})( IOLIN );
