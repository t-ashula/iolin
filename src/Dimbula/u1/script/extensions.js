/**
 * extention
 *
 */

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

if ( !Array.prototype.indexOfPred ){
  Array.prototype.indexOfPred = function( pred, start ) {
    var l = this.length >>> 0;
    start = ( Number( start ) || 0 ) < 0 ? Math.ceil( start ) + l : Math.floor( start );
    for ( ;start < l;++start ) {
      if ( start in this && pred( this[ start ] ) ) {
        return start;
      }
    }
    return -1;
  };
}

if ( !Date.prototype.toW3CDTF ){
  Date.prototype.toW3CDTF = function() {
    var YYYY = this.getYear(), MM = this.getMonth() + 1, DD = this.getDay();
    var HH = this.getHours(), mm = this.getMinutes(), ss = this.getSeconds();
    return ''
      + YYYY.toString() + '-' + MM.toString() + '-' + DD.toString() + 'T'
      + HH.toString() + ':' + mm.toString() + ':' + ss.toString() + 'Z';
  };
}

var fod = function( p, e, d ){
  var es = p.getElementsByTagName( e )[ 0 ];
  return ( es ) ? es.textContent : d;
};
