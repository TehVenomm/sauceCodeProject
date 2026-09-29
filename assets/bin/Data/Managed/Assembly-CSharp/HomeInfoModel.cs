// Decompiled with JetBrains decompiler
// Type: HomeInfoModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class HomeInfoModel : BaseModel
{
  public const string URL = "ajax/home/info";
  public readonly HomeInfoModel.Param result = new HomeInfoModel.Param();

  public class SendForm
  {
    public string appStr;
  }

  [Serializable]
  public class Param
  {
    public string productId;
    public int notice;
    public int message;
    public bool loginBonus;
    public int mysteryTime;
    public string nextDonationTime;
    public string askUpdate;
    public bool party;
    public bool isPointShopOpen;
    public bool isLoungeOpen;
    public int pointShopBanner;
    public bool tradingEnable;
    public int tradingDay;
    public int tradingStatus;
    public int tradingAccept;
    public int tradingConditionDay;
    public int tradingSellMinGem;
    public int tradingSellMaxGem;
    public string tradingLastSold;
    public bool isOneTimesOfferActive;
    public ClanAdvisaryData advisory;
    public List<string> alertMessages = new List<string>();
    public List<EventBanner> banner = new List<EventBanner>();
    public List<GachaDeco> gachaDeco = new List<GachaDeco>();
    public ChatChannelInfo chat;
    public List<Network.EventData> events = new List<Network.EventData>();
    public List<int> futureEventIds = new List<int>();
    public List<Network.EventData> bingoEvents = new List<Network.EventData>();
    public List<EventItemCounts> eventItemCounts = new List<EventItemCounts>();
    public int task;
    public int newsId = 1;
    public float dailyRemainTime = -1f;
    public float weeklyRemainTime = -1f;
    public int isDisplayReview = -1;
    public int isShadowChallengeFirst;
    public bool isArenaOpen;
    public bool isJoinedArenaRanking;
    public List<TimeSlotEvent> timeSlotEvents = new List<TimeSlotEvent>();
    public int crystalChangeName;
    public bool isGuildRequestOpen;
    public bool isTheaterRenewal;
    public int clanDisplayType;
    public int clanRequestNum = -1;
    public int clanInviteNum;
    public string blackShopEndDate;
    public bool isWheelOfFortuneOn = true;
    public List<Network.HomeBanner> homeBanner = new List<Network.HomeBanner>();
  }
}
