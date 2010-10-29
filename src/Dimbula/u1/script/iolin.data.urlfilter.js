/**
 * content blocker rule/urlfilter.ini
 */
(function( I ){

  var _UFD = function( id, t, c ) {
    this.id = id || '';
    this.type = t || 'exclude';
    this.content = c || '';
  };
  
  var _UFDW = function(){};
  _UFDW.prototype = new I.Data.Wrapper();
  _UFDW.prototype.createContent = _UFD;
  _UFDW.prototype.IsSameContent = function( other ){
    return this.Content.id === other.Content.id;
  };

  _UFDW.prototype.ToOperaLinkXml = function(){
    var self = this, s = self.Status, c = self.Content,
      cc = c[ 'content' ], t = c[ 'type' ], i = c[ 'id' ];
    return [
      '<urlfilter '
      ,'status="', s, '" '
      ,'id="', i, '" '
      ,'type="', t, '" '
      ,'>'
      ,'<content>', cc, '</content>'
      ,'</urlfilter>'
    ].join( '' );
  };

  _UFDW.prototype.ModContent = function( next ){
    var self = this;
    self.Status = self.SYNC_DATA_STATUS_MOD;
    self.Content.last_typed = other.Content.last_typed;
    self.Content.type = 'selected';
  };

  _UFDW.prototype.FromOperaLinkXml = function( ele ){
    var self = this, status = ele.getAttribute( 'status' );
    self.Status = status;
    self.Content =
      new self.createContent(
        ele.getAttribute( 'id' ),
        ele.getAttribute( 'type' ),
        status != 'deleted' ? 
          ele.getElementsByTagName( 'content' )[ 0 ].textContent : '' ) ;
  };

  I.Data.UrlFilterManager = new I.Data.Manager( 'UrlFilter', ['urlfilter'], _UFDW );
  
})( IOLIN );
