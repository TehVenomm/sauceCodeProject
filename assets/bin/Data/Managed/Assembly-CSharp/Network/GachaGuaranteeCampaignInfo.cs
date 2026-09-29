// Decompiled with JetBrains decompiler
// Type: Network.GachaGuaranteeCampaignInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
namespace Network;

[Serializable]
public class GachaGuaranteeCampaignInfo
{
  public int gachaId;
  public int guaranteeCampaignId;
  public int campaignType;
  public bool completeStatus;
  public int count;
  public int remainCount;
  public int type;
  public int itemId;
  public int param_0;
  public int param_1;
  public string buttonImg;
  public string startAt;
  public string endAt;
  public string description = "";
  public string detailButtonImg = "";
  public string link = "";
  public int userCount;
  public int probabilityChange;
  public int probabilityRarity;
  public int freeGachaReward;
  public bool hasFreeGachaReward;
  public string campaignDetailImg;
  public int crystalNum;

  public bool IsValid()
  {
    return this.gachaId > 0 && this.guaranteeCampaignId > 0 && !this.IsGetInsentive();
  }

  public bool IsNextGuaranteed() => this.remainCount == 1;

  public bool IsGetInsentive()
  {
    return (this.IsSSConfirmed() || this.IsItemConfirmed()) && this.completeStatus;
  }

  public string GetButtonImageName()
  {
    return (this.IsSSConfirmed() || this.IsItemConfirmed()) && this.IsNextGuaranteed() && this.buttonImg != "" || this.IsChangeableButtonAndOpenInfo() ? this.buttonImg : "";
  }

  public int GetStep() => Mathf.Min(this.userCount + 1, this.count);

  public int GetImageCount()
  {
    return this.IsStepUp() || this.IsFever() ? this.GetStep() : this.remainCount;
  }

  public string GetTitleImageName()
  {
    int imageCount = this.GetImageCount();
    if (this.detailButtonImg == "")
      this.detailButtonImg = "GGC_000000000";
    return $"{this.detailButtonImg}_{(object) imageCount}";
  }

  public DateTime GetStartDateTime() => DateTime.Parse(this.startAt);

  public bool IsSSConfirmed() => this.campaignType == 0;

  public bool IsItemConfirmed() => this.campaignType == 1;

  public bool IsStepUp() => this.campaignType == 2;

  public bool IsFever() => this.campaignType == 3;

  public bool IsStepUpWithPresent() => this.campaignType == 4;

  public bool IsChangeableButtonAndOpenInfo()
  {
    return this.IsStepUp() || this.IsFever() || this.IsStepUpWithPresent();
  }
}
