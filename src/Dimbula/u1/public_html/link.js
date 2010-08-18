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
/**/
function createOperaLinkView( raw ){
  for( var type in dataViewers ) {
    if ( dataViewers.hasOwnProperty( type ) ){
      var h = dataViewers[ type ];
      var hbox = $( h.id );
      if ( hbox ){
        hbox.parentNode.removeChild( hbox );
      }
      _D.body.appendChild( h.createView( raw ).div );
    }
  }
}

function deleteLinkData( data ){
  var url = 'del/';
  /*
  var xhr = new XMLHttpRequest();
  xhr.open( 'POST', url );
  xhr.send( JSON.Stringify( data ) );
  */
  alert( JSON.stringify( data ) );
};

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