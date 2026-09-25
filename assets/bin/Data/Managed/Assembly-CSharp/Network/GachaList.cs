// Decompiled with JetBrains decompiler
// Type: Network.GachaList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace Network;

[Serializable]
public class GachaList
{
  public List<GachaList.GachaType> types = new List<GachaList.GachaType>();
  public string note = string.Empty;

  public class GachaType
  {
    public int type;
    public List<GachaList.GachaGroup> groups = new List<GachaList.GachaGroup>();
    public string url;

    public GACHA_TYPE Type => (GACHA_TYPE) this.type;

    public GACHA_TYPE ViewType
    {
      get
      {
        GACHA_TYPE viewType = (GACHA_TYPE) this.type;
        if (viewType == GACHA_TYPE.TUTORIAL1)
          viewType = GACHA_TYPE.QUEST;
        if (viewType == GACHA_TYPE.TUTORIAL2)
          viewType = GACHA_TYPE.SKILL;
        return viewType;
      }
    }
  }

  public class GachaGroup
  {
    public int group;
    public int priority;
    public string note;
    public string bannerImg;
    public string url;
    public List<GachaList.Gacha> gachas = new List<GachaList.Gacha>();
    public List<GachaGuaranteeCampaignInfo> gachaGuaranteeCampaignInfo = new List<GachaGuaranteeCampaignInfo>();
    public List<GachaFriendPromotionInfo> friendPromotionInfo = new List<GachaFriendPromotionInfo>();
    public List<GachaList.GachaLineup> pickupLineups = new List<GachaList.GachaLineup>();
    public int counter = -1;
    public string expireAt;
  }

  public class Gacha
  {
    public int gachaId;
    public int subGroup;
    public string productId;
    public int priority;
    public string name;
    public int requiredItemId;
    public int needItemNum;
    public int crystalNum;
    public int num;
    public float yen;
    public float yenIncludeTax;
    public string buttonImg;
    public string eventTitleImg;
    public int remainCount = -1;
    public string seriesStartDate = "";
    public string endDate = "";
    public int seriesId = -1;
    public string description = "";
    public string detailButtonImg = "";
    public string link = "";
    public string campaignDetailImg;
    public string caption = "";

    public bool IsEnd
    {
      get
      {
        if (string.IsNullOrEmpty(this.endDate))
          return false;
        DateTime result;
        return !DateTime.TryParse(this.endDate, out result) || result < TimeManager.GetNow();
      }
    }

    public bool IsDirectPurchase() => this.productId != "";

    public bool IsOncePurchase() => this.subGroup == 0;

    public void SetCrystalNum(int num) => this.crystalNum = num;

    public string GetTitleImageName() => this.detailButtonImg;

    public DateTime GetStartDateTime()
    {
      return this.seriesStartDate == "" ? new DateTime(0L) : DateTime.Parse(this.seriesStartDate);
    }
  }

  public class GachaLineup
  {
    public int rewardType;
    public int itemId;
    public int orderNo;
    public GachaList.GachaPickupAnim anim;
    public List<QuestItem.SellItem> sellItems;
  }

  public class GachaPickupAnim
  {
    public string pattern = "";
    public GachaList.GachaPickupAnim.TextStyle name = new GachaList.GachaPickupAnim.TextStyle();
    public GachaList.GachaPickupAnim.TextStyle description = new GachaList.GachaPickupAnim.TextStyle();
    public GachaList.GachaPickupAnim.TextStyle sub = new GachaList.GachaPickupAnim.TextStyle();
    public GachaList.GachaPickupAnim.TextStyle adda = new GachaList.GachaPickupAnim.TextStyle();
    public GachaList.GachaPickupAnim.TextStyle addb = new GachaList.GachaPickupAnim.TextStyle();

    public class TextStyle
    {
      public string text;
      public int size;
      public int italic;
      public string color;
      public string outColor;

      public Color toColor()
      {
        Color color;
        ColorUtility.TryParseHtmlString(this.color, ref color);
        return color;
      }

      public Color toOutColor()
      {
        Color outColor;
        ColorUtility.TryParseHtmlString(this.outColor, ref outColor);
        return outColor;
      }
    }
  }
}
