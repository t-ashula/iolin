/*
 * dom/gui
 */

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

if ( !IOLIN ){
  var IOLIN = {};
}
if ( !IOLIN.forms ){
  (function(I){
    var _F = function( id, title ){
      this.id_ = 'F_' + id ;
      this.title = title;
      this.div = null;
      this.init();
    };
    _F.prototype.clean = function(){
      if ( this.div ) {
        this.div.querySelector('.mainpanel').innerHTML = "";
      }
    };
    _F.prototype.init = function(){
      function minimize( ele ){
        ele.setAttribute(
          'class',
          ele.getAttribute( 'class' ).replace( /maximized/, 'minimized' ) ) ;
      }
      function maximize( ele ){
        ele.setAttribute(
          'class',
          ele.getAttribute( 'class' ).replace( /minimized/, 'maximized' ) ) ;
      }
      var f = $( this.id_ );
      if ( f ) {
      }
      else {
        f = N( 'div', { 'class' : 'wform', 'id' : this.id_ } );
        var cbox = N( 'div', { 'class' : 'controlbox' } );
        [{ c : 'minimize', t : '_' }, { c : 'maximize', t : '[]' }, { c : 'close', t : 'X' } ].forEach(
          function( o ){
            var cb = N( 'span', { 'class' : o.c }, T( o.t ) );
            cb.addEventListener(
              'click',
              function( e ){
                var t = e.target, pp = findParentForm( t );
                if ( pp ){
                  switch ( t.getAttribute( 'class' ) ){
                   case 'minimize': minimize( pp.querySelector('.mainpanel') ); break;
                   case 'maximize': maximize( pp.querySelector('.mainpanel') ); break;
                   case 'close' : pp.style.height = 0; break;
                  }
                }
              },
              false );
            cbox.appendChild( cb );
          }
        );
        f.appendChild( N( 'h2', { 'class' : 'titlebar' }, T( this.title ) ) );
        f.appendChild( cbox );
        f.appendChild( N( 'div', { 'class' : 'mainpanel' } ) );
      }
      this.div = f;
    };
    _F.prototype.addContent = function( ele ) {
      this.div.querySelector( '.mainpanel' ).appendChild( ele );
    };
    I.forms = _F;
  })(IOLIN);
}

function errmsg(){
  var args = Array.prototype.slice.call( arguments, 0 ).join( ',' );
  console.log( args );
}

function findParentForm( ele ){
  for ( ;ele.parentNode && ele.getAttribute( 'class' ) !== 'wform'; ele = ele.prantNode );
  return ele.parentNode ? ele : null;
}
