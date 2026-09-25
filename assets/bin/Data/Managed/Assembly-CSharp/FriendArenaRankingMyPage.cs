// Decompiled with JetBrains decompiler
// Type: FriendArenaRankingMyPage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class FriendArenaRankingMyPage : GameSection
{
  private Network.EventData eventData;
  private bool isExistArena = true;
  private static readonly FriendArenaRankingMyPage.UI[] Groups = new FriendArenaRankingMyPage.UI[5]
  {
    FriendArenaRankingMyPage.UI.OBJ_GROUP_A,
    FriendArenaRankingMyPage.UI.OBJ_GROUP_B,
    FriendArenaRankingMyPage.UI.OBJ_GROUP_C,
    FriendArenaRankingMyPage.UI.OBJ_GROUP_D,
    FriendArenaRankingMyPage.UI.OBJ_GROUP_E
  };
  private bool IsFinishRecieveDelivery;
  private ArenaUserRecordModel.Param record;
  private int userRank = -1;
  private readonly string[] RankingNumbers = new string[10]
  {
    "RankingNumber_0",
    "RankingNumber_1",
    "RankingNumber_2",
    "RankingNumber_3",
    "RankingNumber_4",
    "RankingNumber_5",
    "RankingNumber_6",
    "RankingNumber_7",
    "RankingNumber_8",
    "RankingNumber_9"
  };
  private readonly FriendArenaRankingMyPage.UI[] RankingNumUIs = new FriendArenaRankingMyPage.UI[7]
  {
    FriendArenaRankingMyPage.UI.SPR_RANK_NUM_0,
    FriendArenaRankingMyPage.UI.SPR_RANK_NUM_1,
    FriendArenaRankingMyPage.UI.SPR_RANK_NUM_2,
    FriendArenaRankingMyPage.UI.SPR_RANK_NUM_3,
    FriendArenaRankingMyPage.UI.SPR_RANK_NUM_4,
    FriendArenaRankingMyPage.UI.SPR_RANK_NUM_5,
    FriendArenaRankingMyPage.UI.SPR_RANK_NUM_6
  };

  public override void Initialize()
  {
    this.eventData = GameSection.GetEventData() as Network.EventData;
    this.IsFinishRecieveDelivery = true;
    if (this.eventData == null)
    {
      this.isExistArena = false;
      base.Initialize();
    }
    else if (this.IsRankingJoin())
      this.StartCoroutine(this.SendGetMyRcord());
    else
      base.Initialize();
  }

  private IEnumerator SendGetMyRcord()
  {
    while (!this.IsFinishRecieveDelivery)
      yield return (object) null;
    bool isFinishGetRecord = false;
    MonoBehaviourSingleton<QuestManager>.I.SendGetArenaUserRecord(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, this.eventData.eventId, (Action<bool, ArenaUserRecordModel.Param>) ((b, result) =>
    {
      isFinishGetRecord = true;
      this.record = result;
      this.userRank = this.record.userRank;
    }));
    while (!isFinishGetRecord)
      yield return (object) null;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.UpdateTitle();
    this.UpdateRecord();
    base.UpdateUI();
  }

  private void UpdateTitle()
  {
    if (!this.isExistArena)
    {
      this.SetLabelText((Enum) FriendArenaRankingMyPage.UI.LBL_ARENA_NAME, "");
      this.SetLabelText((Enum) FriendArenaRankingMyPage.UI.LBL_END_DATE, "");
    }
    else
    {
      this.SetLabelText((Enum) FriendArenaRankingMyPage.UI.LBL_ARENA_NAME, this.eventData.name);
      this.SetLabelText((Enum) FriendArenaRankingMyPage.UI.LBL_END_DATE, QuestUtility.GetEndDateString(this.eventData));
    }
  }

  private void UpdateRecord()
  {
    this.SetActive((Enum) FriendArenaRankingMyPage.UI.OBJ_NO_SCORE, !this.IsRankingJoin());
    this.SetActive((Enum) FriendArenaRankingMyPage.UI.OBJ_SCORE, this.IsRankingJoin());
    this.SetActive((Enum) FriendArenaRankingMyPage.UI.OBJ_MY_RANK, this.IsRankingJoin());
    this.SetActive((Enum) FriendArenaRankingMyPage.UI.OBJ_NOT_EXIST, false);
    if (!this.isExistArena)
    {
      this.SetActive((Enum) FriendArenaRankingMyPage.UI.OBJ_NO_SCORE, false);
      this.SetActive((Enum) FriendArenaRankingMyPage.UI.OBJ_SCORE, false);
      this.SetActive((Enum) FriendArenaRankingMyPage.UI.OBJ_MY_RANK, false);
      this.SetActive((Enum) FriendArenaRankingMyPage.UI.OBJ_NOT_EXIST, true);
    }
    else if (!this.IsRankingJoin())
    {
      this.SetLabelText((Enum) FriendArenaRankingMyPage.UI.LBL_NO_TOTAL, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 29U), (object) ARENA_RANK.S.ToString()));
    }
    else
    {
      this.UpdateRank();
      int index = 0;
      for (int count = this.record.clearMilliSecList.Count; index < count; ++index)
      {
        Transform ctrl = this.GetCtrl((Enum) FriendArenaRankingMyPage.Groups[index]);
        bool is_visible = QuestUtility.IsDefaultArenaTime(this.record.clearMilliSecList[index]);
        string stringByMilliSec = QuestUtility.CreateTimeStringByMilliSec(this.record.clearMilliSecList[index]);
        this.SetActive(ctrl, (Enum) FriendArenaRankingMyPage.UI.LBL_GROUP_TIME, !is_visible);
        this.SetActive(ctrl, (Enum) FriendArenaRankingMyPage.UI.LBL_TIME_DEFAULT, is_visible);
        if (is_visible)
          this.SetLabelText(ctrl, (Enum) FriendArenaRankingMyPage.UI.LBL_TIME_DEFAULT, stringByMilliSec);
        else
          this.SetLabelText(ctrl, (Enum) FriendArenaRankingMyPage.UI.LBL_GROUP_TIME, stringByMilliSec);
      }
      this.SetLabelText(this.GetCtrl((Enum) FriendArenaRankingMyPage.UI.OBJ_TOTAL), (Enum) FriendArenaRankingMyPage.UI.LBL_GROUP_TIME, QuestUtility.CreateTimeStringByMilliSec(this.record.totalMilliSec));
    }
  }

  private void UpdateRank()
  {
    int userRank = this.record.userRank;
    string str = userRank.ToString();
    for (int index = 0; index < this.RankingNumUIs.Length; ++index)
      this.SetActive((Enum) this.RankingNumUIs[index], false);
    if (userRank <= 0)
    {
      this.SetActive((Enum) FriendArenaRankingMyPage.UI.SPR_RANK, false);
      this.SetActive((Enum) FriendArenaRankingMyPage.UI.SPR_OUT_OF_RANK, true);
    }
    else
    {
      this.SetActive((Enum) FriendArenaRankingMyPage.UI.SPR_RANK, true);
      this.SetActive((Enum) FriendArenaRankingMyPage.UI.SPR_OUT_OF_RANK, false);
      int num = (this.RankingNumUIs.Length - str.Length) / 2;
      for (int index1 = 0; index1 < str.Length; ++index1)
      {
        int index2 = int.Parse(str[index1].ToString());
        if (index1 >= this.RankingNumUIs.Length)
          break;
        int index3 = index1 + num;
        this.SetSprite(this.GetCtrl((Enum) this.RankingNumUIs[index3]), this.RankingNumbers[index2]);
        this.SetActive((Enum) this.RankingNumUIs[index3], true);
      }
    }
  }

  private void OnQuery_FOLLOWER()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.eventData,
      (object) this.userRank
    });
  }

  private void OnQuery_WORLD()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.eventData,
      (object) this.userRank
    });
  }

  private void OnQuery_LAST()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.eventData,
      (object) this.userRank
    });
  }

  private void OnQuery_LEGEND()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.eventData,
      (object) this.userRank
    });
  }

  private bool IsRankingJoin() => MonoBehaviourSingleton<UserInfoManager>.I.isJoinedArenaRanking;

  private enum UI
  {
    LBL_ARENA_NAME,
    LBL_END_DATE,
    OBJ_GROUP_A,
    OBJ_GROUP_B,
    OBJ_GROUP_C,
    OBJ_GROUP_D,
    OBJ_GROUP_E,
    OBJ_TOTAL,
    OBJ_SCORE,
    OBJ_NO_SCORE,
    OBJ_MY_RANK,
    LBL_GROUP_TIME,
    LBL_TIME_DEFAULT,
    LBL_MY_RANK,
    OBJ_NOT_EXIST,
    SPR_RANK_NUM_0,
    SPR_RANK_NUM_1,
    SPR_RANK_NUM_2,
    SPR_RANK_NUM_3,
    SPR_RANK_NUM_4,
    SPR_RANK_NUM_5,
    SPR_RANK_NUM_6,
    SPR_RANK,
    SPR_OUT_OF_RANK,
    LBL_NO_TOTAL,
  }
}
