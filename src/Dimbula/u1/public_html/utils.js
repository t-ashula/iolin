/*
 * utilities
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
