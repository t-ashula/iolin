using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperaLink.Data;
using System.Linq;
using System.Collections.Generic;

namespace SyncDataTests
{
/// <summary>
///TypedHistoryManagerTest のテスト クラスです。すべての
///TypedHistoryManagerTest 単体テストをここに含めます
///</summary>
  [TestClass()]
  public class TypedHistoryManagerTest
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
    ///SaveToLocalStorage のテスト
    ///</summary>
    [TestMethod()]
    public void SaveToLocalStorageTest()
    {
      TypedHistoryManager target = new TypedHistoryManager(); // TODO: 適切な値に初期化してください
      string storagePath = string.Empty; // TODO: 適切な値に初期化してください
      bool expected = false; // TODO: 適切な値に初期化してください
      bool actual;
      actual = target.SaveToLocalStorage(storagePath);
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///LoadFromLocalStorage のテスト
    ///</summary>
    [TestMethod()]
    public void LoadFromLocalStorageTest()
    {
      TypedHistoryManager target = new TypedHistoryManager(); // TODO: 適切な値に初期化してください
      string storagePath = string.Empty; // TODO: 適切な値に初期化してください
      int expected = -1;
      int actual;
      actual = target.LoadFromLocalStorage(storagePath);
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///FromOperaLinkXml のテスト
    ///</summary>
    [TestMethod()]
    public void FromOperaLinkXmlTest()
    {
      TypedHistoryManager target = new TypedHistoryManager(); 
      string xmlString = "<o>"
        + "<typed_history status=\"added\" content=\"&quot;foo bar &amp;'()*&lt;>\" type=\"text\">"
        + "<last_typed>2010-04-14T18:50:18Z</last_typed></typed_history>"
        + "<typed_history status=\"added\" content=\"あ\" type=\"text\"><last_typed>2010-04-14T18:51:25Z</last_typed></typed_history>"
        + "<typed_history status=\"added\" content=\"あ\" type=\"text\"><last_typed>2010-04-14T18:51:25Z</last_typed></typed_history>"
        + "</o>";
      target.FromOperaLinkXml(xmlString);
      Assert.AreEqual(2, target.Items.Count());
      xmlString = "<o>"
        + "<typed_history status=\"deleted\" content=\"あ\" type=\"text\" />"
        + "</o>";
      target.FromOperaLinkXml(xmlString);
      Assert.AreEqual(1, target.Items.Count());
    }

    /// <summary>
    ///TypedHistoryManager コンストラクタ のテスト
    ///</summary>
    [TestMethod()]
    public void TypedHistoryManagerConstructorTest()
    {
      TypedHistoryManager target = new TypedHistoryManager();
      Assert.AreEqual(0, target.Items.Count());
    }
  }
}
