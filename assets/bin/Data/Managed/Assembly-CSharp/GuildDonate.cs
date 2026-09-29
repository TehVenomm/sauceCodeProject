// Decompiled with JetBrains decompiler
// Type: GuildDonate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class GuildDonate
{
  public class GuildDonateSendModel : BaseModel
  {
    public static string URL = "clan/DonateSend.go";

    public class Form
    {
      public int id;
      public int quantity;
    }
  }

  public class GuildDonateRequestModel : BaseModel
  {
    public static string URL = "clan/DonateCreate.go";
    public GuildDonate.GuildDonateRequestModel.Param result = new GuildDonate.GuildDonateRequestModel.Param();

    public class Form
    {
      public int itemId;
      public string itemName;
      public string msg;
      public int quantity;
      public string nickName;
    }

    public class Param
    {
      public string msg;
      public int itemId;
      public int itemNum;
      public int quantity;
      public string expired;
    }
  }

  public class GuildDonateReceiveModel : BaseModel
  {
    public static string URL = "clan/DonateReceive.go";
  }

  public class GuildDonateListModel : BaseModel
  {
    public static string URL = "clan/DonateList.go";
    public GuildDonate.GuildDonateListModel.Param result = new GuildDonate.GuildDonateListModel.Param();

    [Serializable]
    public class Param
    {
      public List<DonateInfo> array = new List<DonateInfo>();
      public DonateInfo pinDonate;
    }
  }

  public class GuildDonateInviteModel : BaseModel
  {
    public static string URL = "clan/DonateInvite.go";

    public class Form
    {
      public int id;
      public int userId;
    }
  }

  public class GuildDonateInviteListModel : BaseModel
  {
    public static string URL = "clan/DonateInviteList.go";
    public GuildDonate.GuildDonateInviteListModel.Result result;

    public class Form
    {
      public int id;
    }

    [Serializable]
    public class Result
    {
      public List<FriendCharaInfo> list = new List<FriendCharaInfo>();
      public List<GuildDonate.GuildDonateInviteListModel.DonateListInfo> donate_list = new List<GuildDonate.GuildDonateInviteListModel.DonateListInfo>();
    }

    [Serializable]
    public class DonateListInfo
    {
      public bool isInvited;
      public int userId;
    }
  }

  public class GuildDonateInvitationListModel : BaseModel
  {
    public static string URL = "clan/DonateAskList.go";
    public GuildDonate.GuildDonateInvitationListModel.Result result;

    [Serializable]
    public class Result
    {
      public List<DonateInvitationInfo> array = new List<DonateInvitationInfo>();
    }
  }

  public class GuildDonateFobbidenModel : BaseModel
  {
    public static string URL = "clan/DonateForbiddenList.go";
    public GuildDonate.GuildDonateFobbidenModel.Param result = new GuildDonate.GuildDonateFobbidenModel.Param();

    [Serializable]
    public class Param
    {
      public List<int> array = new List<int>();
    }
  }
}
