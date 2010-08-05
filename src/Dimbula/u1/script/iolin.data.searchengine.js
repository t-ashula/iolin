/**
 * SeachEngine Data
 */
(function(I){
  
  var _SED = function( o ){
    this.uuid     = o.uuid     || '';
    this.type     = o.type     || 'normal';
    this.group    = o.group    || 'custome';
    this.title    = o.title    || '';
    this.uri      = o.uri      || '';
    this.key      = o.key      || '';
    this.encoding = o.encoding || 'utf-8';
    this.icon     = o.icon     || '';
    this.hidden   = o.hidden   || false;
    
    this.is_post    = o.post       || '-1';
    this.post_query = o.post_query || '';

    this.show_in_personal_bar = o.show_in_personal_bar || '-1';
    this.personal_bar_pos     = o.personal_bar_pos     || 0;
  };
  
  var _SEDW = function(){};

  _SEDW.prototype = new I.Data.Wrapper();

  _SEDW.prototype.createContent = _SED;
  
  _SEDW.prototype.IsSameContent = function( other ){
    return this.Content.uuid === other.Content.uuid;
  };

  _SEDW.prototype.FromOperaLinkXml = function( ele ){
    var self = this;
    self.Status = ele.getAttribute( 'status' );
    self.Content = new self.createContent({
       'uuid'                 : ele.getAttribute('id')
      ,'type'                 : ele.getAttribute('type')
      ,'encoding'             : fod( ele, 'encoding' )
      ,'title'                : fod( ele, 'title' )
      ,'key'                  : fod( ele, 'key' )
      ,'group'                : fod( ele, 'group' )
      ,'is_post'              : fod( ele, 'is_post' )
      ,'post_query'           : fod( ele, 'post_query' )
      ,'personal_bar_pos'     : fod( ele, 'personal_bar_pos' )
      ,'show_in_personal_bar' : fod( ele, 'show_in_personal_bar' )
      ,'icon'                 : fod( ele, 'icon' )
      ,'hidden'               : fod( ele, 'hidden' )
    });
  };

  _SEDW.prototype.ToOperaLinkXml = function(){
    var self = this;
    var s = self.Status;
    var c = self.Content;
    return [
      '<search_engine'
      , ' status="', s, '"'
      , ' type="', i.type, '"',
      , ' id="', i.uuid, '"', '>'
      , '<group>', i.group, '</group>'
      , '<hidden>', !!i.hidden ? '1' : '-1', '</hidden>'
      , '<personal_bar_pos>'
      ,   !!i.show_in_personal_bar && !!i.personal_bar_pos ?
            i.personal_bar_pos : 0, '</personal_bar_pos>'
      , '<show_in_personal_bar>', !!i.show_in_personal_bar ? 1 : -1, '</show_in_personal_bar>'
      , '<title>', !!i.title ? i.title : '', '</title>'
      , '<uri>', !!i.uri ? i.uri : '', '</uri>'
      , '<key>', !!i.key ? i.key : '', '</key>'
      , '<encoding>', !!i.encoding ? i.encoding : 'utf-8', '</encoding>'
      , '<is_post>', !!i.is_post ? '1' : '-1', '</is_post>'
      , '<post_query>',
          !!i.is_post && !!i.post_query ? i.post_query : '', '</post_query>'
      , '<icon>', !!i.icon ? i.icon : '', '</icon>'
      ,'</search_engine>'
    ].join( '' );
  };

  I.Data.SearchEngineManager = new I.Data.Manager(
    'SearchEngine', [ 'search_engine' ], _SEDW );  
}( IOLIN ));

