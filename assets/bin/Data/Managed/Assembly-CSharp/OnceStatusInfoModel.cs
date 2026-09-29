// Decompiled with JetBrains decompiler
// Type: OnceStatusInfoModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class OnceStatusInfoModel : BaseModel
{
  public static string URL = "ajax/once/statusinfo";
  public OnceStatusInfoModel.Param result = new OnceStatusInfoModel.Param();

  [Serializable]
  public class Param
  {
    public Network.UserInfo user = new Network.UserInfo();
    public UserStatus userStatus = new UserStatus();
    public List<EquipSetSimple> equipSets = new List<EquipSetSimple>();
    public List<BoostStatus> boost = new List<BoostStatus>();
    public int followNum;
    public int followerNum;
    public GlobalSettingsManager.HasVisuals hasVisuals;
    public List<int> unlockStamps = new List<int>();
    public List<int> selectedDegrees = new List<int>();
    public List<int> unlockDegrees = new List<int>();
    public List<AccessorySet> accessorySets = new List<AccessorySet>();
    public UserClanData userClan = new UserClanData();
    public List<EquipSetSimple> uniqueEquipSets = new List<EquipSetSimple>();
    public List<AccessorySet> uniqueAccessorySets = new List<AccessorySet>();
  }
}
