// Decompiled with JetBrains decompiler
// Type: InventorySellQuestModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class InventorySellQuestModel : BaseModel
{
  public static string URL = "ajax/inventory/sellquest";
  public InventorySellQuestModel.Param result = new InventorySellQuestModel.Param();

  [Serializable]
  public class Param
  {
    public SellQuestItemReward reward = new SellQuestItemReward();
  }

  public class RequestSendForm
  {
    public List<string> uids = new List<string>();
    public List<int> nums;
  }
}
