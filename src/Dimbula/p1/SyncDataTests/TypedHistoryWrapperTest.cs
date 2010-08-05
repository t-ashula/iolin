using OperaLink;
using OperaLink.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace SyncDataTests
{
    
    
    /// <summary>
    ///TypedHistoryWrapperTest のテスト クラスです。すべての
    ///TypedHistoryWrapperTest 単体テストをここに含めます
    ///</summary>
  [TestClass()]
  public class TypedHistoryWrapperTest
  {


    private TestContext testContextInstance;

    /// <summary>
    ///現在のテストの実行についての情報および機能を
    ///提供するテスト コンテキストを取得または設定します。
    ///</summary>
    public TestContext TestContext
    {
      get
      {
        return testContextInstance;
      }
      set
      {
        testContextInstance = value;
      }
    }

    #region 追加のテスト属性
    // 
    //テストを作成するときに、次の追加属性を使用することができます:
    //
    //クラスの最初のテストを実行する前にコードを実行するには、ClassInitialize を使用
    //[ClassInitialize()]
    //public static void MyClassInitialize(TestContext testContext)
    //{
    //}
    //
    //クラスのすべてのテストを実行した後にコードを実行するには、ClassCleanup を使用
    //[ClassCleanup()]
    //public static void MyClassCleanup()
    //{
    //}
    //
    //各テストを実行する前にコードを実行するには、TestInitialize を使用
    //[TestInitialize()]
    //public void MyTestInitialize()
    //{
    //}
    //
    //各テストを実行した後にコードを実行するには、TestCleanup を使用
    //[TestCleanup()]
    //public void MyTestCleanup()
    //{
    //}
    //
    #endregion


    /// <summary>
    ///State のテスト
    ///</summary>
    [TestMethod()]
    public void StateTest()
    {
      TypedHistoryWrapper target = new TypedHistoryWrapper();
      SyncState expected = SyncState.Added;
      SyncState actual;
      target.State = expected;
      actual = target.State;
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///Content のテスト
    ///</summary>
    [TestMethod()]
    public void ContentTest()
    {
      TypedHistoryWrapper target = new TypedHistoryWrapper();
      TypedHistory expected = new TypedHistory { Content = "", Type = "text", LastTyped = System.DateTime.Now };
      TypedHistory actual;
      target.Content = expected;
      actual = target.Content;
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///ToOperaLinkXml のテスト
    ///</summary>
    [TestMethod()]
    public void ToOperaLinkXmlTest()
    {
      var lt = new System.DateTime(2010, 4, 30, 20, 21, 22, System.DateTimeKind.Local);
      TypedHistoryWrapper target = new TypedHistoryWrapper
      {
        Content = new TypedHistory
        {
          Content = "foo bar &<>\"'",
          Type = "text",
          LastTyped = lt
        },
        State = SyncState.Added
      };
      string d = lt.ToW3cDtfInUtc();
      string expected = "<typed_history status=\"added\" content=\"foo bar &amp;&lt;>&quot;'\" type=\"text\"><last_typed>" + d + "</last_typed></typed_history>";
      string actual;
      actual = target.ToOperaLinkXml();
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///TypedHistoryWrapper コンストラクタ のテスト
    ///</summary>
    [TestMethod()]
    public void TypedHistoryWrapperConstructorTest()
    {
      TypedHistoryWrapper target = new TypedHistoryWrapper();
      Assert.AreEqual(SyncState.Added, target.State);
      Assert.AreEqual("<typed_history />", target.ToOperaLinkXml());
    }

    /// <summary>
    ///ModContent のテスト
    ///</summary>
    [TestMethod()]
    public void ModContentTest()
    {
      var n = System.DateTime.Now;
      TypedHistoryWrapper target = new TypedHistoryWrapper
      {
        Content = new TypedHistory { Content = "hoge" }
      };
      TypedHistoryWrapper other = new TypedHistoryWrapper
      {
        Content = new TypedHistory { Content = "mod", LastTyped = n, }
      };
      target.ModContent(other);
      Assert.AreEqual(SyncState.Modified, target.State); // state is modified
      Assert.AreEqual("hoge", target.Content.Content); // content.content not change
      Assert.AreEqual(n, target.Content.LastTyped); // change datetime
      Assert.AreEqual("selected", target.Content.Type); // type is selected
    }

    /// <summary>
    ///IsSameContent のテスト
    ///</summary>
    [TestMethod()]
    public void IsSameContentTest()
    {
      TypedHistory hoge = new TypedHistory { Content = "hoge" };
      TypedHistory huga = new TypedHistory { Content = "huga" };
      TypedHistoryWrapper target = new TypedHistoryWrapper { Content = hoge };
      TypedHistoryWrapper other = new TypedHistoryWrapper { Content = huga };
      bool expected = false;
      bool actual;
      actual = target.IsSameContent(other);
      Assert.AreEqual(expected, actual);

      other = new TypedHistoryWrapper { Content = hoge };
      expected = true;
      actual = target.IsSameContent(other);
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///FromOperaLinkXml のテスト
    ///</summary>
    [TestMethod()]
    public void FromOperaLinkXmlTest()
    {
      TypedHistoryWrapper target = new TypedHistoryWrapper(); 
      string xmlString = "<typed_history status=\"added\" content=\"ashula.info\" type=\"text\"><last_typed>2010-04-14T18:22:42Z</last_typed></typed_history>";
      target.FromOperaLinkXml(xmlString);
      Assert.AreEqual(SyncState.Added, target.State);
      Assert.AreEqual("ashula.info", target.Content.Content);
      Assert.AreEqual("2010-04-14T18:22:42Z", target.Content.LastTyped.ToW3cDtfInUtc());
    }
  }
}
