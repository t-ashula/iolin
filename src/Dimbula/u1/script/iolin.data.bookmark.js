/**
 * typed history data
 */
(function( I ){

  var _BMD = function( o ) {
    this.id = o.id;
    this.type = o.type || 'item';
    this.parent = o.parent || '';
    this.previous = o.previous || '';
    this.created = o.created || (new Date()).toW3CDTF();
    this.uri = o.uri || '';
    this.title = o.title || '';
    this.personal_bar_pos = o.personal_bar_pos || -1;
    this.show_in_personal_bar = o.show_in_personal_bar || 0;
    this.panel_pos = o.panel_pos || -1;
    this.show_in_panel = o.show_in_panel || 0;
    this.nick_name = o.nick_naem || '';
  };
  
  var _BMDW = function(){};
  _BMDW.prototype = new I.Data.Wrapper();
  _BMDW.prototype.createContent = _BMD;
  _BMDW.prototype.IsSameContent = function( other ){
    return this.Content.id === other.Content.id
      &&  this.Content.parent === other.Content.parent;
  };

  _BMDW.prototype.FromOperaLinkXml = function( ele ){
    var self = this, tag = ele.tagName, ty = ele.getAttribute( 'type' );
    ty = ( tag === 'bookmark_folder' ) ?
      ( ( !!ty && ty === 'trash' ) ? 'trash' : 'folder' ) : 'item';
    self.Status = ele.getAttribute( 'status' );
    self.Content = new self.createContent({
      'id'                    : ele.getAttribute( 'id' )
      ,'type'                 : ty
      ,'parent'               : ele.getAttribute( 'parent' )
      ,'previous'             : ele.getAttribute( 'previous' )
      ,'created'              : ele.getAttribute( 'created' )
      ,'uri'                  : fod( ele, 'uri' )
      ,'title'                : fod( ele, 'title' )
      ,'personal_bar_pos'     : fod( ele, 'personal_bar_pos' )
      ,'show_in_personal_bar' : fod( ele, 'show_in_personal_bar' )
      ,'panel_pos'            : fod( ele, 'panel_pos' )
      ,'show_in_panel'        : fod( ele, 'show_in_panel' )
      ,'nickname'             : fod( ele, 'nickname' )
    });
  };

  _BMDW.prototype.ToOperaLinkXml = function(){
    var self = this, s = self.Status, c = self.Content, t = c.type,  lxml = [];
    if ( t === 'bookmark' || t === 'separator' ) {
      lxml = [
        '<bookmark'
        ,' status="',   s, '"'
        ,' id="',       c.id, '"'
        ,' parent="',   !!c.parent   ? c.parent : '"'
        ,' previous="', !!c.previous ? c.previous : '"'
        ,' created="',  !!c.created  ? c.created : '"'
        ,'>'
        ,'<uri>',       !!c.uri      ? c.uri     : '', '</uri>'
        ,'<title>',     !!c.content  ? c.content : '', '</title>'
        ,'<nickname>',     !!c.nickname  ? c.nickname : '', '</nickname>'
        ,'<show_in_parsonal_bar>', !!c.show_in_personal_bar  ? c.show_in_personal_bar : '', '</show_in_parsonal_bar>'
        ,'<parsonal_bar_pos>', ( !!c.show_in_personal_bar && !!c.personal_bar_pos ) ? c.personal_bar_pos : '-1', '</parsonal_bar_pos>'
        ,'<show_in_panel>', !!c.show_in_panel ? c.show_in_penel : '', '</show_in_panel>'
        ,'<panel_pos>', ( !!c.show_in_panel && !!c.panel_pos ) ? c.panel_pos : '-1', '</panel_pos>'        
        ,'</bookmark>'
      ];
    }
    else {
      lxml = [
        '<bookmark_folder'
        ,' status="',   s, '"'
        ,' id="',       c.id, '"'
        ,' parent="',   !!c.parent   ? c.parent : '"'
        ,' previous="', !!c.previous ? c.previous : '"'
        ,' created="',  !!c.created  ? c.created : '"'
        ,' type="', ( !!c.type && c.type === 'trash' ) ? 'trash' : 'folder', '"'
        ,'>'
        ,'<uri>',       !!c.uri      ? c.uri     : '', '</uri>'
        ,'<title>',     !!c.content  ? c.content : '', '</title>'
        ,'<nickname>',     !!c.nickname  ? c.nickname : '', '</nickname>'
        ,'<show_in_parsonal_bar>', !!c.show_in_personal_bar  ? c.show_in_personal_bar : '', '</show_in_parsonal_bar>'
        ,'<parsonal_bar_pos>', ( !!c.show_in_personal_bar && !!c.personal_bar_pos ) ? c.personal_bar_pos : '-1', '</parsonal_bar_pos>'
        ,'<show_in_panel>', !!c.show_in_panel ? c.show_in_penel : '', '</show_in_panel>'
        ,'<panel_pos>', ( !!c.show_in_panel && !!c.panel_pos ) ? c.panel_pos : '-1', '</panel_pos>'        
        ,'</bookmark_folder>'
      ];
    }
    return lxml.join( '' );
  };

  I.Data.BookmarkManager = new I.Data.Manager( 'Bookmark', [ 'bookmark', 'bookmark_folder' ], _BMDW );
  
})( IOLIN );
