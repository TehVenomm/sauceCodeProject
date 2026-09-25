// Decompiled with JetBrains decompiler
// Type: Network.UserClanData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace Network;

public class UserClanData
{
  public string cId;
  public string name;
  public int stat;
  public int isInBase;
  public int level;
  public int exp;
  public int expNext;
  public int expPrev;
  public bool isMaxLevel;
  public ClanSymbolData sym;

  public bool IsNotRegistered() => this.stat == 0;

  public bool IsRegistered() => this.stat != 0;

  public bool IsNormalMember() => this.stat == 1;

  public bool IsSubLeader() => this.stat == 2;

  public bool IsLeader() => this.stat == 3;

  public bool IsInBase() => this.isInBase == 1;

  public void Clear()
  {
    this.cId = "0";
    this.name = "";
    this.stat = 0;
    this.isInBase = 0;
    this.level = 1;
  }
}
