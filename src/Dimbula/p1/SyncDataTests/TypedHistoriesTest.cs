using OperaLink.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SyncDataTests
{
    
    
    /// <summary>
    ///TypedHistoriesTest のテスト クラスです。すべての
    ///TypedHistoriesTest 単体テストをここに含めます
    ///</summary>
  [TestClass()]
  public class TypedHistoriesTest
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
    ///Items のテスト
    ///</summary>
    [TestMethod()]
    public void ItemsTest()
    {
      TypedHistories target = new TypedHistories(); // TODO: 適切な値に初期化してください
      List<ISyncDataContent<TypedHistoryContent>> expected = null; // TODO: 適切な値に初期化してください
      List<ISyncDataContent<TypedHistoryContent>> actual;
      target.Items = expected;
      actual = target.Items;
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("このテストメソッドの正確性を確認します。");
    }

    /// <summary>
    ///ToOperaLinkXml のテスト
    ///</summary>
    [TestMethod()]
    public void ToOperaLinkXmlTest()
    {
      TypedHistories target = new TypedHistories(); // TODO: 適切な値に初期化してください
      string expected = string.Empty; // TODO: 適切な値に初期化してください
      string actual;
      actual = target.ToOperaLinkXml();
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("このテストメソッドの正確性を確認します。");
    }

    /// <summary>
    ///SaveToLocalStorage のテスト
    ///</summary>
    [TestMethod()]
    public void SaveToLocalStorageTest()
    {
      TypedHistories target = new TypedHistories(); // TODO: 適切な値に初期化してください
      string storagePath = string.Empty; // TODO: 適切な値に初期化してください
      bool expected = false; // TODO: 適切な値に初期化してください
      bool actual;
      actual = target.SaveToLocalStorage(storagePath);
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("このテストメソッドの正確性を確認します。");
    }

    /// <summary>
    ///Modified のテスト
    ///</summary>
    [TestMethod()]
    public void ModifiedTest()
    {
      TypedHistories target = new TypedHistories(); // TODO: 適切な値に初期化してください
      TypedHistoryContent d = new TypedHistoryContent(); // TODO: 適切な値に初期化してください
      target.Modified(d);
      Assert.Inconclusive("値を返さないメソッドは確認できません。");
    }

    /// <summary>
    ///LoadFromLocalStorage のテスト
    ///</summary>
    [TestMethod()]
    public void LoadFromLocalStorageTest()
    {
      TypedHistories target = new TypedHistories(); // TODO: 適切な値に初期化してください
      string storagePath = string.Empty; // TODO: 適切な値に初期化してください
      int expected = 0; // TODO: 適切な値に初期化してください
      int actual;
      actual = target.LoadFromLocalStorage(storagePath);
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("このテストメソッドの正確性を確認します。");
    }

    /// <summary>
    ///FromOperaLinkXml のテスト
    ///</summary>
    [TestMethod()]
    public void FromOperaLinkXmlTest()
    {
      TypedHistories target = new TypedHistories(); // TODO: 適切な値に初期化してください
      string xmlString = string.Empty; // TODO: 適切な値に初期化してください
      target.FromOperaLinkXml(xmlString);
      Assert.Inconclusive("値を返さないメソッドは確認できません。");
    }

    /// <summary>
    ///Deleted のテスト
    ///</summary>
    [TestMethod()]
    public void DeletedTest()
    {
      TypedHistories target = new TypedHistories(); // TODO: 適切な値に初期化してください
      TypedHistoryContent d = new TypedHistoryContent(); // TODO: 適切な値に初期化してください
      target.Deleted(d);
      Assert.Inconclusive("値を返さないメソッドは確認できません。");
    }

    /// <summary>
    ///Added のテスト
    ///</summary>
    [TestMethod()]
    public void AddedTest()
    {
      TypedHistories target = new TypedHistories(); // TODO: 適切な値に初期化してください
      TypedHistoryContent d = new TypedHistoryContent(); // TODO: 適切な値に初期化してください
      target.Added(d);
      Assert.Inconclusive("値を返さないメソッドは確認できません。");
    }

    /// <summary>
    ///TypedHistories コンストラクタ のテスト
    ///</summary>
    [TestMethod()]
    public void TypedHistoriesConstructorTest()
    {
      TypedHistories target = new TypedHistories();
      Assert.Inconclusive("TODO: ターゲットを確認するためのコードを実装してください");
    }
  }
}
