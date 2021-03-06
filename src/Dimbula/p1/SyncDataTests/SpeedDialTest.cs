﻿using OperaLink.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace SyncDataTests
{
    
    
    /// <summary>
    ///SpeedDialTest のテスト クラスです。すべての
    ///SpeedDialTest 単体テストをここに含めます
    ///</summary>
  [TestClass()]
  public class SpeedDialTest
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
      SpeedDial target = new SpeedDial(); // TODO: 適切な値に初期化してください
      SyncState expected = new SyncState(); // TODO: 適切な値に初期化してください
      SyncState actual;
      target.State = expected;
      actual = target.State;
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("このテストメソッドの正確性を確認します。");
    }

    /// <summary>
    ///Content のテスト
    ///</summary>
    [TestMethod()]
    public void ContentTest()
    {
      SpeedDial target = new SpeedDial(); // TODO: 適切な値に初期化してください
      SpeedDialContent expected = null; // TODO: 適切な値に初期化してください
      SpeedDialContent actual;
      target.Content = expected;
      actual = target.Content;
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("このテストメソッドの正確性を確認します。");
    }

    /// <summary>
    ///ToOperaLinkXml のテスト
    ///</summary>
    [TestMethod()]
    public void ToOperaLinkXmlTest()
    {
      SpeedDial target = new SpeedDial(); // TODO: 適切な値に初期化してください
      string expected = string.Empty; // TODO: 適切な値に初期化してください
      string actual;
      actual = target.ToOperaLinkXml();
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("このテストメソッドの正確性を確認します。");
    }

    /// <summary>
    ///StringToState のテスト
    ///</summary>
    [TestMethod()]
    public void StringToStateTest()
    {
      SpeedDial target = new SpeedDial(); // TODO: 適切な値に初期化してください
      string s = string.Empty; // TODO: 適切な値に初期化してください
      SyncState expected = new SyncState(); // TODO: 適切な値に初期化してください
      SyncState actual;
      actual = target.StringToState(s);
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("このテストメソッドの正確性を確認します。");
    }

    /// <summary>
    ///StateToString のテスト
    ///</summary>
    [TestMethod()]
    public void StateToStringTest()
    {
      SpeedDial target = new SpeedDial(); // TODO: 適切な値に初期化してください
      SyncState s = new SyncState(); // TODO: 適切な値に初期化してください
      string expected = string.Empty; // TODO: 適切な値に初期化してください
      string actual;
      actual = target.StateToString(s);
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("このテストメソッドの正確性を確認します。");
    }

    /// <summary>
    ///Modified のテスト
    ///</summary>
    [TestMethod()]
    public void ModifiedTest()
    {
      SpeedDial target = new SpeedDial(); // TODO: 適切な値に初期化してください
      SpeedDialContent d = null; // TODO: 適切な値に初期化してください
      target.Modified(d);
      Assert.Inconclusive("値を返さないメソッドは確認できません。");
    }

    /// <summary>
    ///FromOperaLinkXml のテスト
    ///</summary>
    [TestMethod()]
    public void FromOperaLinkXmlTest()
    {
      SpeedDial target = new SpeedDial(); // TODO: 適切な値に初期化してください
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
      SpeedDial target = new SpeedDial(); // TODO: 適切な値に初期化してください
      target.Deleted();
      Assert.Inconclusive("値を返さないメソッドは確認できません。");
    }

    /// <summary>
    ///Added のテスト
    ///</summary>
    [TestMethod()]
    public void AddedTest()
    {
      SpeedDial target = new SpeedDial(); // TODO: 適切な値に初期化してください
      SpeedDialContent d = null; // TODO: 適切な値に初期化してください
      target.Added(d);
      Assert.Inconclusive("値を返さないメソッドは確認できません。");
    }

    /// <summary>
    ///SpeedDial コンストラクタ のテスト
    ///</summary>
    [TestMethod()]
    public void SpeedDialConstructorTest()
    {
      SpeedDial target = new SpeedDial();
      Assert.Inconclusive("TODO: ターゲットを確認するためのコードを実装してください");
    }
  }
}
