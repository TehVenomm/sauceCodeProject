// Decompiled with JetBrains decompiler
// Type: OnceDeliveryModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class OnceDeliveryModel : BaseModel
{
  public static string URL = "ajax/once/delivery";
  public OnceDeliveryModel.Param result = new OnceDeliveryModel.Param();

  [Serializable]
  public class Param
  {
    public List<Delivery> delivery = new List<Delivery>();
    public List<ClearStatusDelivery> clearStatusDelivery = new List<ClearStatusDelivery>();
  }
}
