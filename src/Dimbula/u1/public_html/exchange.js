/*
 *
 *  
 */
/// global/alias
if ( !_D ){ var _D = document; }
if ( !_O ){ var _O = opera; }
if ( !_L ){ var _L = location; }
if ( !_W ){ var _W = window; }

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

_W.addEventListener(
  'DOMContentLoaded',
  function(e){
    createDataForms();
    pullLinkData();
  },
  false
);