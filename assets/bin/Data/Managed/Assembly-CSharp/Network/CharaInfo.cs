// Decompiled with JetBrains decompiler
// Type: Network.CharaInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class CharaInfo
{
  public int userId;
  public string name;
  public string comment;
  public string lastLogin;
  public int lastLoginTm;
  public string code;
  public XorInt hp = (XorInt) 0;
  public XorInt atk = (XorInt) 0;
  public XorInt def = (XorInt) 0;
  public XorInt level = (XorInt) 0;
  public int sex;
  public int faceId;
  public int hairId;
  public int hairColorId;
  public int skinId;
  public int voiceId;
  public int aId;
  public int hId;
  public int rId;
  public int lId;
  public int showHelm;
  public string equipSetName;
  public List<CharaInfo.EquipItem> equipSet = new List<CharaInfo.EquipItem>();
  public List<int> selectedDegrees;
  public CharaInfo.ClanInfo clanInfo;
  public UserClanData userClanData;
  public bool isInviteToClan;
  public List<CharaInfo.UserAccessory> accessory = new List<CharaInfo.UserAccessory>();

  public bool isEqualAccessory(List<CharaInfo.UserAccessory> src)
  {
    if (this.accessory.IsNullOrEmpty<CharaInfo.UserAccessory>() && src.IsNullOrEmpty<CharaInfo.UserAccessory>())
      return true;
    if (this.accessory.IsNullOrEmpty<CharaInfo.UserAccessory>() || src.IsNullOrEmpty<CharaInfo.UserAccessory>() || this.accessory.Count != src.Count)
      return false;
    for (int index1 = 0; index1 < this.accessory.Count; ++index1)
    {
      CharaInfo.UserAccessory userAccessory1 = this.accessory[index1];
      for (int index2 = 0; index2 < src.Count; ++index2)
      {
        CharaInfo.UserAccessory userAccessory2 = src[index2];
        if (userAccessory1.uniqId == userAccessory2.uniqId && userAccessory1.accessoryId == userAccessory2.accessoryId && userAccessory1.place == userAccessory2.place)
          return true;
      }
    }
    return false;
  }

  public override string ToString()
  {
    string str = "";
    str = $"{str}{(object) this.userId},";
    str = $"{str}{this.name},";
    str = $"{str}{this.comment},";
    str = $"{str}{this.lastLogin},";
    str = $"{str}{(object) this.lastLoginTm},";
    str = $"{str}{this.code},";
    str = $"{str}{(object) this.hp},";
    str = $"{str}{(object) this.atk},";
    str = $"{str}{(object) this.def},";
    str = $"{str}{(object) this.level},";
    str = $"{str}{(object) this.sex},";
    str = $"{str}{(object) this.faceId},";
    str = $"{str}{(object) this.hairId},";
    str = $"{str}{(object) this.hairColorId},";
    str = $"{str}{(object) this.skinId},";
    str = $"{str}{(object) this.voiceId},";
    str = $"{str}{(object) this.aId},";
    str = $"{str}{(object) this.hId},";
    str = $"{str}{(object) this.rId},";
    str = $"{str}{(object) this.lId},";
    str = $"{str}{(object) this.showHelm},";
    if (this.equipSet != null)
      this.equipSet.ForEach((Action<CharaInfo.EquipItem>) (e => str = $"{str}[{(object) e}],"));
    if (!this.accessory.IsNullOrEmpty<CharaInfo.UserAccessory>())
      this.accessory.ForEach((Action<CharaInfo.UserAccessory>) (a => str = $"{str}[{(object) a}],"));
    return base.ToString() + str;
  }

  [Serializable]
  public class EquipItem
  {
    public int eId;
    public int lv;
    public int exceed;
    public List<int> sIds = new List<int>();
    public List<int> sLvs = new List<int>();
    public List<int> sExs = new List<int>();
    public List<int> aIds = new List<int>();
    public List<int> aPts = new List<int>();
    public AbilityItem ai = new AbilityItem();

    public override string ToString()
    {
      string str1 = $"{$"{(object) this.eId},"}{(object) this.lv},";
      int index1 = 0;
      for (int count = this.sIds.Count; index1 < count; ++index1)
      {
        string str2 = $"{$"{str1 + "s("}{(object) this.sIds[index1]},"}{(object) this.sLvs[index1]},";
        int num = 0;
        if (index1 < this.sExs.Count)
          num = this.sExs[index1];
        str1 = $"{str2}{num.ToString()}," + "),";
      }
      int index2 = 0;
      for (int count = this.aIds.Count; index2 < count; ++index2)
        str1 = $"{$"{str1 + "a("}{(object) this.aIds[index2]},"}{(object) this.aPts[index2]}," + "),";
      string str3 = $"{str1}ai({(object) this.ai})";
      return base.ToString() + str3;
    }

    public int GetSkillExceed(int idx)
    {
      return this.sExs == null || this.sExs.Count <= idx ? 0 : this.sExs[idx];
    }
  }

  [Serializable]
  public class ClanInfo
  {
    public int clanId;
    public int[] emblem;
    public string tag;
  }

  [Serializable]
  public class UserAccessory
  {
    public string uniqId;
    public int accessoryId;
    public int place;
  }
}
