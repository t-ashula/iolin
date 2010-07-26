using OperaLink;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperaLink.Data;

namespace SyncDataTests
{
    
    
    /// <summary>
    ///UtilsTest のテスト クラスです。すべての
    ///UtilsTest 単体テストをここに含めます
    ///</summary>
  [TestClass()]
  public class UtilsTest
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
    ///StateToString のテスト
    ///</summary>
    [TestMethod()]
    public void StateToStringTest()
    {
      Assert.AreEqual("added", Utils.StateToString(SyncState.Added));
      Assert.AreEqual("deleted", Utils.StateToString(SyncState.Deleted));
      Assert.AreEqual("modified", Utils.StateToString(SyncState.Modified));
    }

    /// <summary>
    ///StringToState のテスト
    ///</summary>
    [TestMethod()]
    public void StringToStateTest()
    {
      Assert.AreEqual(SyncState.Added, Utils.StringToState("added"));
      Assert.AreEqual(SyncState.Deleted, Utils.StringToState("deleted"));
      Assert.AreEqual(SyncState.Modified, Utils.StringToState("modified"));
      Assert.AreEqual(SyncState.Modified, Utils.StringToState("unknown"));
      try
      {
        Assert.AreEqual(SyncState.Modified, Utils.StringToState(string.Empty));
      }
      catch (System.Exception ex)
      {
        Assert.IsInstanceOfType(ex, typeof(System.ArgumentException));
      }
    }

    /// <summary>
    ///XmlEntitize のテスト
    ///</summary>
    [TestMethod()]
    public void XmlEntitizeTest()
    {
      string raw = "foo bar &<>\"'"; 
      string expected = "foo bar &amp;&lt;>&quot;'"; 
      string actual;
      actual = Utils.XmlEntitize(raw);
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///XmlUnEntitize のテスト
    ///</summary>
    [TestMethod()]
    public void XmlUnEntitizeTest()
    {
      string cooked = "foo bar &amp;&lt;>&quot;'";
      string expected = "foo bar &<>\"'";
      string actual;
      actual = Utils.XmlUnEntitize(cooked);
      Assert.AreEqual(expected, actual);
    }
  }
}
