// Decompiled with JetBrains decompiler
// Type: FriendArenaRankingInfoScore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class FriendArenaRankingInfoScore : GameSection
{
  private static readonly FriendArenaRankingInfoScore.UI[] GROUPSCORES = new FriendArenaRankingInfoScore.UI[5]
  {
    FriendArenaRankingInfoScore.UI.OBJ_GROUP_A,
    FriendArenaRankingInfoScore.UI.OBJ_GROUP_B,
    FriendArenaRankingInfoScore.UI.OBJ_GROUP_C,
    FriendArenaRankingInfoScore.UI.OBJ_GROUP_D,
    FriendArenaRankingInfoScore.UI.OBJ_GROUP_E
  };
  private ArenaUserRecordModel.Param record;
  private Network.EventData eventData;
  private int userId;

  public override void Initialize()
  {
    object[] eventData = (object[]) GameSection.GetEventData();
    this.eventData = eventData[0] as Network.EventData;
    this.userId = (int) eventData[1];
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    bool isFinishGetRecord = false;
    MonoBehaviourSingleton<QuestManager>.I.SendGetArenaUserRecord(this.userId, this.eventData.eventId, (Action<bool, ArenaUserRecordModel.Param>) ((b, result) =>
    {
      isFinishGetRecord = true;
      this.record = result;
    }));
    while (!isFinishGetRecord)
      yield return (object) null;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.UpdateScores();
    base.UpdateUI();
  }

  private void UpdateScores()
  {
    int index = 0;
    for (int count = this.record.clearMilliSecList.Count; index < count; ++index)
    {
      Transform ctrl = this.GetCtrl((Enum) FriendArenaRankingInfoScore.GROUPSCORES[index]);
      string stringByMilliSec = QuestUtility.CreateTimeStringByMilliSec(this.record.clearMilliSecList[index]);
      bool is_visible = QuestUtility.IsDefaultArenaTime(this.record.clearMilliSecList[index]);
      this.SetActive(ctrl, (Enum) FriendArenaRankingInfoScore.UI.LBL_SCORE, !is_visible);
      this.SetActive(ctrl, (Enum) FriendArenaRankingInfoScore.UI.LBL_TIME_DEFAULT, is_visible);
      if (is_visible)
        this.SetLabelText(ctrl, (Enum) FriendArenaRankingInfoScore.UI.LBL_TIME_DEFAULT, stringByMilliSec);
      else
        this.SetLabelText(ctrl, (Enum) FriendArenaRankingInfoScore.UI.LBL_SCORE, stringByMilliSec);
    }
    this.SetLabelText(this.GetCtrl((Enum) FriendArenaRankingInfoScore.UI.OBJ_TOTAL), (Enum) FriendArenaRankingInfoScore.UI.LBL_SCORE, QuestUtility.CreateTimeStringByMilliSec(this.record.totalMilliSec));
  }

  private enum UI
  {
    OBJ_GROUP_A,
    OBJ_GROUP_B,
    OBJ_GROUP_C,
    OBJ_GROUP_D,
    OBJ_GROUP_E,
    OBJ_TOTAL,
    LBL_SCORE,
    LBL_TIME_DEFAULT,
  }
}
