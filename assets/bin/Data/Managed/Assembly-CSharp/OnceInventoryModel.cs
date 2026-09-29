// Decompiled with JetBrains decompiler
// Type: OnceInventoryModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class OnceInventoryModel : BaseModel
{
  public static string URL = "ajax/once/inventory";
  public OnceInventoryModel.Param result = new OnceInventoryModel.Param();

  [Serializable]
  public class Param
  {
    public List<EquipItem> equipItem = new List<EquipItem>();
    public List<Network.SkillItem> skillItem = new List<Network.SkillItem>();
    public List<Network.Item> item = new List<Network.Item>();
    public List<ExpiredItem> expiredItem = new List<ExpiredItem>();
    public List<QuestItem> questItem = new List<QuestItem>();
    public List<AbilityItem> abilityItem = new List<AbilityItem>();
    public List<Accessory> accessory = new List<Accessory>();
  }

  public class RequestSendForm
  {
    public int req_e;
    public int req_s;
    public int req_i;
    public int req_qi;
    public int req_ai;
    public int req_ac;
  }
}
