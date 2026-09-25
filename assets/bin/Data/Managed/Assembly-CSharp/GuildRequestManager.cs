// Decompiled with JetBrains decompiler
// Type: GuildRequestManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GuildRequestManager : MonoBehaviourSingleton<GuildRequestManager>
{
  public GuildRequest guildRequestData;
  private GuildRequestItem selectedItem;
  private uint beforeQuestId;
  public bool isCompleteMulti;
  private bool firstSetGetList = true;

  public int GetNeedPoint(RARITY_TYPE rarity)
  {
    ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
    switch (rarity)
    {
      case RARITY_TYPE.B:
        return constDefine.GUILD_REQUEST_NEED_POINT_B;
      case RARITY_TYPE.A:
        return constDefine.GUILD_REQUEST_NEED_POINT_A;
      case RARITY_TYPE.S:
        return constDefine.GUILD_REQUEST_NEED_POINT_S;
      case RARITY_TYPE.SS:
        return constDefine.GUILD_REQUEST_NEED_POINT_SS;
      case RARITY_TYPE.SSS:
        return constDefine.GUILD_REQUEST_NEED_POINT_SSS;
      default:
        return constDefine.GUILD_REQUEST_NEED_POINT_SS;
    }
  }

  public TimeSpan GetNeedTime(RARITY_TYPE rarity)
  {
    return this.CalcTimeSpanFromPoint(this.GetNeedPoint(rarity));
  }

  public string GetNeedTimeWithFormat(RARITY_TYPE rarity)
  {
    TimeSpan needTime = this.GetNeedTime(rarity);
    DateTime dateTime = new DateTime(0L);
    dateTime = dateTime.Add(needTime);
    return dateTime.ToString("H:mm:ss");
  }

  public TimeSpan CalcTimeSpanFromPoint(int point)
  {
    ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
    return new TimeSpan(0, 0, (int) ((double) point / (double) constDefine.GUILD_POINT_PER_MIN * 60.0));
  }

  public int CalcPointFromTimeSpan(TimeSpan time)
  {
    ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
    return (int) (time.TotalMinutes * (double) constDefine.GUILD_POINT_PER_MIN);
  }

  public void SetList()
  {
    if (!this.firstSetGetList)
      return;
    this.firstSetGetList = false;
    this.guildRequestData = new GuildRequest(MonoBehaviourSingleton<OnceManager>.I.result.guildRequestItemList);
  }

  public GuildRequestItem GetSelectedItem() => this.selectedItem;

  public void SetSelectedItem(GuildRequestItem guildRequestItem)
  {
    this.selectedItem = guildRequestItem;
  }

  public uint GetBeforeQuestId() => this.beforeQuestId;

  public void SendGuildRequestList(Action<bool> call_back)
  {
    this.guildRequestData = (GuildRequest) null;
    Protocol.Send<GuildRequestListModel>(GuildRequestListModel.URL, (Action<GuildRequestListModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.guildRequestData = ret.result;
      }
      call_back(flag);
    }));
  }

  public void SendGuildRequestStart(
    QuestInfoData questInfoData,
    bool isQuestItem,
    Action<bool> call_back)
  {
    Protocol.Send<GuildRequestStartModel.RequestSendForm, GuildRequestStartModel>(GuildRequestStartModel.URL, new GuildRequestStartModel.RequestSendForm()
    {
      slotNo = this.selectedItem.slotNo,
      questId = (int) questInfoData.questData.tableData.questID,
      num = 1,
      isQuestItem = isQuestItem ? 1 : 0
    }, (Action<GuildRequestStartModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  public void SendGuildRequestComplete(Action<GuildRequestCompleteModel.Param> call_back)
  {
    GuildRequestCompleteModel.RequestSendForm postData = new GuildRequestCompleteModel.RequestSendForm();
    postData.slotNo = this.selectedItem.slotNo;
    this.beforeQuestId = (uint) this.selectedItem.questId;
    GuildRequestCompleteModel.Param completeData = (GuildRequestCompleteModel.Param) null;
    Protocol.Send<GuildRequestCompleteModel.RequestSendForm, GuildRequestCompleteModel>(GuildRequestCompleteModel.URL, postData, (Action<GuildRequestCompleteModel>) (ret =>
    {
      if (ret.Error == Error.None)
        completeData = ret.result;
      call_back(completeData);
    }));
  }

  public void SendGuildRequestCompleteAll(Action<GuildRequestCompleteModel.Param> call_back)
  {
    GuildRequestCompleteModel.Param completeData = (GuildRequestCompleteModel.Param) null;
    Protocol.Send<GuildRequestCompleteAllModel>(GuildRequestCompleteAllModel.URL, (Action<GuildRequestCompleteAllModel>) (ret =>
    {
      if (ret.Error == Error.None)
        completeData = ret.result;
      call_back(completeData);
    }));
  }

  public void SendGuildRequestExtendAndSortie(
    QuestInfoData questInfoData,
    bool isQuestItem,
    Action<bool> call_back)
  {
    Protocol.Send<GuildRequestExtendAndStartModel.RequestSendForm, GuildRequestExtendAndStartModel>(GuildRequestExtendAndStartModel.URL, new GuildRequestExtendAndStartModel.RequestSendForm()
    {
      slotNo = this.selectedItem.slotNo,
      questId = (int) questInfoData.questData.tableData.questID,
      num = 1,
      isQuestItem = isQuestItem ? 1 : 0,
      crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal
    }, (Action<GuildRequestExtendAndStartModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  public void SendGuildRequestExtend(Action<bool> call_back)
  {
    Protocol.Send<GuildRequestExtendModel.RequestSendForm, GuildRequestExtendModel>(GuildRequestExtendModel.URL, new GuildRequestExtendModel.RequestSendForm()
    {
      slotNo = this.selectedItem.slotNo,
      crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal
    }, (Action<GuildRequestExtendModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  public void SendGuildRequestRetire(Action<bool> call_back)
  {
    Protocol.Send<GuildRequestRetireModel.RequestSendForm, GuildRequestRetireModel>(GuildRequestRetireModel.URL, new GuildRequestRetireModel.RequestSendForm()
    {
      slotNo = this.selectedItem.slotNo
    }, (Action<GuildRequestRetireModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  public void RegisterGuildRequestLocalNotification()
  {
    if (!MonoBehaviourSingleton<GuildRequestManager>.I.GetLocalPushFlag() || this.guildRequestData == null || this.guildRequestData.guildRequestItemList == null)
      return;
    List<DateTime> time = new List<DateTime>(5);
    foreach (GuildRequestItem guildRequestItem in this.guildRequestData.guildRequestItemList)
    {
      TimeSpan questRemainTime = guildRequestItem.GetQuestRemainTime();
      if (questRemainTime.TotalSeconds > 0.0)
      {
        TimeSpan houndRemainTime = guildRequestItem.GetHoundRemainTime();
        if (guildRequestItem.crystalNum <= 0 || questRemainTime.TotalSeconds <= houndRemainTime.TotalSeconds)
        {
          DateTime dateTime = DateTime.Now;
          dateTime = dateTime.AddTicks(questRemainTime.Ticks);
          time.Add(dateTime);
        }
      }
    }
    MonoBehaviourSingleton<AppMain>.I.SetGuildRequestConstructLocalNotification(time);
  }

  public void ClearGuildRequestLocalNotification()
  {
    MonoBehaviourSingleton<AppMain>.I.SetGuildRequestConstructLocalNotification(new List<DateTime>());
  }

  public void SetLocalPushFlag(bool flag)
  {
    PlayerPrefs.SetInt("LOCAL_PUSH_GUILD_REQUEST_KEY", flag ? 1 : 0);
  }

  public bool GetLocalPushFlag()
  {
    if (PlayerPrefs.HasKey("LOCAL_PUSH_GUILD_REQUEST_KEY"))
      return PlayerPrefs.GetInt("LOCAL_PUSH_GUILD_REQUEST_KEY") == 1;
    this.SetLocalPushFlag(true);
    return true;
  }

  public void ToggleLocalPushFlag() => this.SetLocalPushFlag(!this.GetLocalPushFlag());

  public void Dirty()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_CHANGE);
  }

  public void OnDiff(BaseModelDiff.DiffGuildRequest diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      diff.add.ForEach((Action<GuildRequestItem>) (data =>
      {
        if (this.guildRequestData == null)
          this.guildRequestData = new GuildRequest();
        this.guildRequestData.guildRequestItemList.Add(data);
      }));
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      diff.update.ForEach((Action<GuildRequestItem>) (data =>
      {
        GuildRequestItem guildRequestItem = this.guildRequestData.guildRequestItemList.Find((Predicate<GuildRequestItem>) (list_data => list_data.slotNo == data.slotNo));
        guildRequestItem.slotNo = data.slotNo;
        guildRequestItem.crystalNum = data.crystalNum;
        guildRequestItem.questId = data.questId;
        guildRequestItem.num = data.num;
        guildRequestItem.endAt = data.endAt;
        guildRequestItem.expiredAt = data.expiredAt;
      }));
      flag = true;
    }
    if (!flag)
      return;
    this.Dirty();
  }

  public enum StringKey
  {
    START_CONFIRM,
    PAY_CONFIRM,
    PAY_RESULT,
    CANCEL_CONFIRM,
    CONTINUE_CONFIRM,
    REMAIN_TIME_WARNING,
    NEED_POINT_AND_TIME,
    CONTINUE_BUTTON,
    END_BUTTON,
    PUSH_TITLE,
    PUSH_COMPLETE,
    REMAIN_TIME,
    NONE_LIMITED,
    SORTIEING_NUM,
    BONUS_TIME,
    ADDITIONAL_HOUND,
    HOUND_NO_1,
    HOUND_NO_2,
    HOUND_NO_3,
    HOUND_NO_4,
    PUSH_REMAIN,
  }
}
