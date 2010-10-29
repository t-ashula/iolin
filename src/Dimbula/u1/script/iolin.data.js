/**
 * IOLIN.Data
 */

if ( !IOLIN.Data ) {
  IOLIN.Data = {};
}

(function( I ){
  
  var _DW = function( c, s ){  
    this.Content = c;
    this.Status = s;
  };

  _DW.prototype.SYNC_DATA_STATUS_ADD = 'added';
  _DW.prototype.SYNC_DATA_STATUS_MOD = 'modifled';
  _DW.prototype.SYNC_DATA_STATUS_DEL = 'deleted';
  _DW.prototype.ToOperaLinkXml = function(){ return ""; };
  _DW.prototype.FromOperaLinkXml = function( dom ){};
  _DW.prototype.IsSameContent = function( other ){ return false; };
  _DW.prototype.ModContent = function( next ){
    var dw = this;
    dw.Content = next.Content;
    dw.Status = dw.SYNC_DATA_STATUS_MOD;
  };
  ///
  I.Data.Wrapper = _DW;
})( IOLIN );

(function( I ){
  var _DM = function ( name, owns, w ) {
    this.name_ = name;
    this.ownElements_ = owns;
    this.innerItems_ = [];
    this.toSyncItems_ = [];
    this.dw_ = w;
  };

  _DM.prototype.Name = function(){ return this.name_; };
  _DM.prototype.ownElements_ = [];
  _DM.prototype.innerItems_ = [];
  _DM.prototype.toSyncItems_ = [];
  _DM.prototype.Items = function(){
    var dm = this;
    return dm.innerItems_.map(
      function( d ) {
        var f = { 'data_status' : d.Status }, c = d.Content, k;
        for ( k in c ) {
          if ( c.hasOwnProperty( k ) ) {
            f[ k ] = c[ k ];
          }
        }
        return f;
      }
    );
  };

  _DM.prototype.ToOperaLinkXml = function(){
    return toSyncItems_.aggregate( "", function( x, ele ){ return x + ele.ToOperaLinkXml(); } );
  };

  _DM.prototype.FromOperaLinkXml = function( dom ) {
    var dm = this, i, j, tag, eles, ele, item;
    for ( j = 0; tag = dm.ownElements_[ j ]; ++j ){
      eles = dom.getElementsByTagName( tag );
      for ( i = 0; ele = eles[ i ]; ++i ){
        item = new dm.dw_;
        item.FromOperaLinkXml( ele );
        dm.changeInnerList( item );
      }
    }
  };

  _DM.prototype.changeInnerList = function( d ){
    var dm = this;
    switch( d.Status ) {
     case d.SYNC_DATA_STATUS_ADD : dm.addItem( d ); break;
     case d.SYNC_DATA_STATUS_MOD : dm.modItem( d ); break;
     case d.SYNC_DATA_STATUS_DEL : dm.delItem( d ); break;
    }
  };
  
  _DM.prototype.Add = function( c ){
    var dm = this, item = new dm.dw_( c, dm.dw_.SYNC_DATA_STATUS_ADD );
    if ( dm.addItem( item ) ) {
      dm.addSyncItem( item );
    }
    return dm.innerItems_.length;
  };

  _DM.prototype.addItem = function( d ){
    var dm = this;
    if ( dm.innerItems_.some( function( e ){ return e.IsSameContent( d ); } ) ) {
      return false;
    }
    dm.innerItems_[ dm.innerItems_.length ] = d;
    return true;
  };
  
  _DM.prototype.addSyncItem = function( d ) {
    var dm = this;
    dm.toSyncItems_[ dm.toSyncItems_.length ] = d;
    return true;
  };
  
  _DM.prototype.Mod = function( c ) {
    var dm = this, item = new dm.dw_( c, dm.dw_.SYNC_DATA_STATUS_MOD );
    dm.modItem( item ) ;
    dm.modSyncItem( item );
    return dm.innerItems_.length;
  };
  
  _DM.prototype.modItem = function( d ) {
    var dm = this, idx = innerItems_.indexOfPred( function( e ){ return e.IsSameContent( d ); } );
    if ( idx < 0 ){
      return false;
    }
    dm.innerItems_[ idx ].ModContent( d );
    return true;
  };
  
  _DM.prototype.modSyncItem = function( d ) {
    var dm = this, idx =  dm.toSyncItems_.indexOfPred( function( e ){ return e.IsSameContent( d ); } );
    if ( idx < 0 ){
      return false;
    }
    if ( dm.toSyncItems_[ idx ].SyncState === dm.dw_.SYNC_DATA_STATUS_DEL ){
      return false;
    }
    dm.toSyncItems_[ idx ].ModContent( d );
    return true;
  };
  
  _DM.prototype.Del = function( c ) {
    var dm = this, item = new dm.dw_( c, dm.dw_.SYNC_DATA_STATUS_DEL );
    if ( dm.delItem( item ) ) {
      dm.delSyncItem( item );
    }
    return dm.innerItems_.length;
  };
  
  _DM.prototype.delItem = function( d ) {
    var dm = this, idx = dm.innerItems_.indexOfPred( function( e ){ return e.IsSameContent( d ); } );
    if ( idx < 0 ){ return false; }
    dm.innerItems_ = dm.innerItems.filter( function( e ){ return !e.IsSameContent( d ); } );
    return true;
  };

  _DM.prototype.delSyncItem = function( d ) {
    var dm = this, idx = dm.toSyncItems_.indexOfPred( function( e ){ return e.IsSameContent( d ); } );
    if ( idx < 0 ){
      d.SyncState = dm.dw_.SYNC_DATA_STATUS_DEL ;
      dm.toSyncItems_[ dm.toSyncItems_.length ] = d;
    }
    else {
      dm.toSyncItems_[ idx ].Status = dm.dw_.SYNC_DATA_STATUS_DEL;
    }
  };

  _DM.prototype.SyncDone = function(){
    this.toSyncItems_ = [];
  };
  
  I.Data.Manager = _DM;
})( IOLIN );
