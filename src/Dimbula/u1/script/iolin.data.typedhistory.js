/**
 * typed history data
 */
(function( I ){

  var _THD = function( t, c, l ) {
    this.type = t || 'text';
    this.content = c || '';
    this.last_typed = l || (new Date()).toW3CDTF();
  };

  
  var _THDW = function(){};

  _THDW.prototype = new I.Data.Wrapper();

  _THDW.prototype.createContent = _THD;
    
  _THDW.prototype.IsSameContent = function( other ){
    return this.Content.content === other.Content.content;
  };

  _THDW.prototype.ToOperaLinkXml = function(){
    var self = this;
    var s = self.Status, c = self.Content;
    var cc = c[ 'Content' ];
    if ( !cc || cc === '' ) {
      return '<typed_history />';
    }
    var t = c[ 'type' ], l = c[ 'last_typed' ];
    return [ '<typed_history '
             ,'status="', s, '" '
             ,'content="', cc, '" '
             ,'type="', t, '" ', '>'
             ,'<last_typed>', l, '</last_typed>'
             ,'</typed_history>'
           ].join( '' );
  };

  _THDW.prototype.ModContent = function( next ){
    var self = this;
    self.Status = self.SYNC_DATA_STATUS_MOD;
    self.Content.last_typed = other.Content.last_typed;
    self.Content.type = 'selected';
  };

  _THDW.prototype.FromOperaLinkXml = function( ele ){
    var self = this;
    var status = ele.getAttribute( 'status' );
    self.Status = status;
    self.Content =
      new self.createContent(
        ele.getAttribute( 'type' ),
        ele.getAttribute( 'content' ),
        ( status !== 'deleted' ) ?
          ele.getElementsByTagName( 'last_typed' )[ 0 ].textContent : '' ) ;
  };

  I.Data.TypedHistoryManager = new I.Data.Manager(
    'TypedHistory', ['typed_history'], _THDW );
  
})( IOLIN );
