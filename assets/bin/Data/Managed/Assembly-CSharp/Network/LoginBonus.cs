// Decompiled with JetBrains decompiler
// Type: Network.LoginBonus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class LoginBonus
{
  public string name;
  public int type;
  public int total;
  public int rotate;
  public int nowCount;
  public List<LoginBonus.LoginBonusReward> reward = new List<LoginBonus.LoginBonusReward>();
  public List<LoginBonus.NextReward> next = new List<LoginBonus.NextReward>();
  public bool isBeginner2Pop;
  public int priority;
  public string period_announce;
  public int boardType;
  public int loginBonusId;
  public int usePickUp;

  public class LoginBonusReward
  {
    public string name;
    public int type;
    public int itemId;
    public int itemNum;
    public bool isGet;
    public bool isPickUp;
    public string pickUpText;
    public string day;
    public int frameType;
    public float scale;

    public float GetScale() => (double) this.scale == 0.0 ? 1f : this.scale;
  }

  public class NextReward
  {
    public int count;
    public List<LoginBonus.LoginBonusReward> reward = new List<LoginBonus.LoginBonusReward>();
  }
}
