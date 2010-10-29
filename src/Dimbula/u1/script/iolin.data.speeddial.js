/**
 * Speeddial data
 */
(function(I){
  
  var _SDD = function( pos, title, uri, rel, icon ) {
    this.position = pos;
    this.title = title || '';
    this.uri = uri || '';
    this.icon = icon || '';
    var r = !!rel, e = false, i = -1;
    if ( r ) {
      e = rel.expired || false;
      i = rel.interval || -1;
    } 
    this.reload = r;
    this.expired_only = e;
    this.interval = i;
  };
  
  var _SDDW = function(){};
  _SDDW.prototype = new I.Data.Wrapper();
  _SDDW.prototype.createContent = _SDD;
  _SDDW.prototype.IsSameContent = function( other ){
    return this.Content.position === other.Content.position;
  };

  _SDDW.prototype.FromOperaLinkXml = function( ele ){
    var self = this, pos = Number( ele.getAttribute( 'position' ) ),
      title = fod( ele, 'title', '' ), uri = fod( ele, 'uri', '' ), icon = fod( ele, 'icon', '' ),
      rel = void 0;
    if ( fod( ele, 'reload_enabled', '0' ) !== '1' )      {
      rel = {
        'expired'   : fod( ele, 'reload_only_if_expire', '0' ) === '1'
        ,'interval' : fod( ele, 'reload_interval', '-1' )
      } ;
    }
    self.Status = ele.getAttribute( 'status' );
    self.Content = new self.createContent(
      pos, title, uri, rel, icon
    );
  };

  _SDDW.prototype.ToOperaLinkXml = function(){
    var self = this, s = self.Status, c = self.Content;
    return [
      '<speeddial ', 'status="', s, '" ', 'position="', i.position, '" ', '>'
      ,'<title>', c.title, '</title>'
      ,'<uri>', c.uri, '</url>'
      ,'<reload_enabled>', !!c.reload ? '1' : '0', '</reload_enabled>',
      ,'<reload_only_if_expired>', !!c.expired ? '1' : '0', '</reload_only_if_expired>'
      ,'<reload_interval>', !!c.interval ? c.interval : '-1', '</reload_interval>'
      ,'</speeddial>'
    ].join( '' );
  };

  I.Data.SpeedDialManager = new I.Data.Manager( 'Speeddial', [ 'speeddial' ], _SDDW );  
}( IOLIN ));

