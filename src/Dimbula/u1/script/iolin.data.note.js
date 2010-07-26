/**
 * typed history data
 */
(function( I ){

  var _NND = function( o ) {
    this.id = o.id;
    this.type = o.type || 'note';
    this.parent = o.parent || '';
    this.previous = o.previous || '';
    this.created = o.created || (new Date()).toW3CDTF();
    this.uri = o.uri || '';
    this.content = o.content || '';
    this.title = o.title || '';
  };
  
  var _NNDW = function(){};

  _NNDW.prototype = new I.Data.Wrapper();

  _NNDW.prototype.createContent = _NND;
    
  _NNDW.prototype.IsSameContent = function( other ){
    return this.Content.id === other.Content.id
      &&  this.Content.parent === other.Content.parent;
  };

  _NNDW.prototype.FromOperaLinkXml = function( ele ){
    var self = this;
    self.Status = ele.getAttribute( 'status' );
    var tag = ele.tagName;
    var ty = ele.getAttribute( 'type' );
    if ( tag === 'note_folder' ) {
      ty = ( !!ty && ty === 'trash' ) ? 'trash' : 'folder';
    } else {
      ty = 'note';
    }
    self.Content = new self.createContent({
      'id'        : ele.getAttribute( 'id' )
      ,'type'     : ty
      ,'parent'   : ele.getAttribute( 'parent' )
      ,'previous' : ele.getAttribute( 'previous' )
      ,'created'  : ele.getAttribute( 'created' )
      ,'uri'      : fod( ele, 'uri' )
      ,'content'  : fod( ele, 'content' )
      ,'title'    : fod( ele, 'title' )
    });
  };

  _NNDW.prototype.ToOperaLinkXml = function(){
    var self = this;
    var s = self.Status, c = self.Content;
    var t = c.type;
    var ele = ( t === 'note' ) ? 'note' : 'note_folder';
    var lxml = [];
    if ( t === 'note' ) {
      lxml = [
        '<note'
        ,' status="', s, '"'
        ,' id="',      c.id, '"'
        ,' parent="',   !!c.parent   ? c.parent : '"'
        ,' previous="', !!c.previous ? c.previous : '"'
        ,' created="',  !!c.created  ? c.created : '"'
        ,'>'
        ,'<content>',   !!c.content  ? c.content : '', '</content>'
        ,'<uri>',       !!c.uri      ? c.uri     : '', '</uri>'
        ,'</note>'
      ];
    } else {
      lxml = [
        '<note_folder'
        ,' status="',   s, '"'
        ,' id="',       c.id, '"'
        ,' parent="',   !!c.parent   ? c.parent : '"'
        ,' previous="', !!c.previous ? c.previous : '"'
        ,' created="',  !!c.created  ? c.created : '"'
        ,' type="', ( !!c.type && c.type === 'trash' ) ? 'trash' : 'folder', '"'
        ,'>'
        ,'<title>',     !!c.title    ? c.title : '', '</title>'
        ,'</note_folder>'
      ];
    }
    return lxml.join( '' );
  };

  I.Data.NoteManager = new I.Data.Manager(
    'Notes', [ 'note', 'note_folder' ], _NNDW );
  
})( IOLIN );
