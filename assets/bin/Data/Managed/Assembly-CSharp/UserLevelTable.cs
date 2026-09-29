// Decompiled with JetBrains decompiler
// Type: UserLevelTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class UserLevelTable : Singleton<UserLevelTable>, IDataTable
{
  private UIntKeyTable<UserLevelTable.UserLevelData> userLevelTable;
  private int maxLevel;

  public void CreateTable(string csv_text)
  {
    this.userLevelTable = TableUtility.CreateUIntKeyTable<UserLevelTable.UserLevelData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<UserLevelTable.UserLevelData>(UserLevelTable.UserLevelData.cb), "lv,needExp");
    this.userLevelTable.TrimExcess();
  }

  public UserLevelTable.UserLevelData GetLevelTable(int level)
  {
    if (this.userLevelTable == null)
      return (UserLevelTable.UserLevelData) null;
    UserLevelTable.UserLevelData userLevelData = this.userLevelTable.Get((uint) level);
    if (userLevelData == null)
    {
      if (level <= this.GetMaxLevel())
        Log.Error("UserLevelData is NULL :: id(Lv) = " + (object) level);
      return (UserLevelTable.UserLevelData) null;
    }
    return level > this.GetMaxLevel() ? (UserLevelTable.UserLevelData) null : userLevelData;
  }

  public int GetMaxLevel()
  {
    if (this.maxLevel != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.USER_LEVEL_MAX && this.userLevelTable != null && this.userLevelTable.Get((uint) MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.USER_LEVEL_MAX) != null)
      this.maxLevel = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.USER_LEVEL_MAX;
    if (this.maxLevel > 0)
      return this.maxLevel;
    if (this.userLevelTable == null)
      return 0;
    this.userLevelTable.ForEach((Action<UserLevelTable.UserLevelData>) (data => this.maxLevel = Mathf.Max(this.maxLevel, (int) data.lv)));
    return this.maxLevel;
  }

  public class UserLevelData
  {
    public XorInt lv = (XorInt) 0;
    public XorInt needExp = (XorInt) 0;
    public const string NT = "lv,needExp";

    public static bool cb(CSVReader csv_reader, UserLevelTable.UserLevelData data, ref uint key)
    {
      data.lv = (XorInt) (int) key;
      csv_reader.Pop(ref data.needExp);
      return true;
    }
  }
}
