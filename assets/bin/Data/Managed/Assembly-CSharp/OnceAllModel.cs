// Decompiled with JetBrains decompiler
// Type: OnceAllModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;

#nullable disable
public class OnceAllModel : BaseModel
{
  public static string URL = "ajax/once/all";
  public OnceAllModel.Param result = new OnceAllModel.Param();

  public class Param
  {
    public OnceStatusInfoModel.Param statusinfo;
    public OnceTraveledListModel.Param traveledlist;
    public OnceInventoryModel.Param inventory;
    public OnceDeliveryModel.Param delivery;
    public OnceClearStatusModel.Param clearstatus;
    public List<int> blacklist;
    public OnceAchievementModel.Param achievement;
    public List<TaskInfo> task;
    public List<int> region;
    public List<GuildRequestItem> guildRequestItemList;
  }

  public class RequestSendForm
  {
    public int req_e;
    public int req_s;
    public int req_i;
    public int req_qi;
    public int req_ai;
    public int req_ac;
    public string d;
  }
}
