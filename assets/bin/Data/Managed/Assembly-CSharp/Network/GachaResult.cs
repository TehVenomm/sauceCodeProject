// Decompiled with JetBrains decompiler
// Type: Network.GachaResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class GachaResult
{
  public List<GachaResult.GachaReward> reward;
  public int remainCount = -1;
  public int counter;
  public bool oncePurchasedState;
  public GachaGuaranteeCampaignInfo gachaGuaranteeCampaignInfo;
  public GachaResult.OncePurchaseItemToShop oncePurchaseItemToShop;
  public string buttonImg = "";
  public string detailButtonImg = "";

  public class GachaReward
  {
    public int rewardType;
    public int itemId;
    public int param_0;
    public int lotGroupNo;
  }

  public class OncePurchaseItemToShop
  {
    public string productId = "";
  }
}
