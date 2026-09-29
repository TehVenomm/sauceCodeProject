// Decompiled with JetBrains decompiler
// Type: DeliveryCompleteModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class DeliveryCompleteModel : BaseModel
{
  public static string URL = "ajax/delivery/complete";
  public DeliveryCompleteModel.Param result = new DeliveryCompleteModel.Param();

  [Serializable]
  public class Param
  {
    public DeliveryRewardList reward = new DeliveryRewardList();
    public List<int> openRegionIds;
    public List<int> openEventIds;
  }

  public class RequestSendForm
  {
    public string uId;
  }
}
