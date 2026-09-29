// Decompiled with JetBrains decompiler
// Type: QuestRequestItemSeriesArena
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class QuestRequestItemSeriesArena : QuestRequestItem
{
  public override void Setup(Transform t, DeliveryTable.DeliveryData info)
  {
    this.SetActive(t, (Enum) QuestRequestItemSeriesArena.UI.SPR_TYPE_DIFFICULTY, info.difficulty >= DIFFICULTY_MODE.HARD);
    this.SetBestTimeOrStamp(t, info.GetQuestData().questID);
    base.Setup(t, info);
  }

  protected override void SetFrame(Transform t, DeliveryTable.DeliveryData info)
  {
  }

  protected override void SetIcon(Transform t, DeliveryTable.DeliveryData info)
  {
    ResourceLoad.LoadWithSetUITexture(((Component) this.FindCtrl(t, (Enum) QuestRequestItemSeriesArena.UI.TEX_NPC)).GetComponent<UITexture>(), RESOURCE_CATEGORY.SERIES_ARENA_RANK_ICON, ResourceName.GetSeriesArenaRankIconName(info.GetQuestData().rarity));
  }

  private void SetBestTimeOrStamp(Transform t, uint questId)
  {
    ClearStatusQuest clearStatusQuestData = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestData(questId);
    if (clearStatusQuestData != null && clearStatusQuestData.clearTime > 0)
    {
      int clearTime = clearStatusQuestData.clearTime;
      bool is_visible = MonoBehaviourSingleton<QuestManager>.I.CheckMissionAllClear(questId);
      this.SetActive(t, (Enum) QuestRequestItemSeriesArena.UI.SPR_MISSION_CROWN_OFF, !is_visible);
      this.SetActive(t, (Enum) QuestRequestItemSeriesArena.UI.SPR_MISSION_CROWN_ON, is_visible);
      this.SetActive(t, (Enum) QuestRequestItemSeriesArena.UI.SPR_BEST_TIME, true);
      this.SetActive(t, (Enum) QuestRequestItemSeriesArena.UI.LBL_BEST_TIME, true);
      string milliSecSeriesArena = QuestUtility.CreateTimeStringByMilliSecSeriesArena(clearStatusQuestData.clearTime);
      this.SetLabelText(t, (Enum) QuestRequestItemSeriesArena.UI.LBL_BEST_TIME, milliSecSeriesArena);
    }
    else
    {
      this.SetActive(t, (Enum) QuestRequestItemSeriesArena.UI.SPR_BEST_TIME, false);
      this.SetActive(t, (Enum) QuestRequestItemSeriesArena.UI.LBL_BEST_TIME, false);
    }
  }

  private new enum UI
  {
    TEX_NPC,
    LBL_DELIVERY_COMMENT,
    OBJ_REQUEST_OK,
    OBJ_REQUEST_COMPLETED,
    LBL_HAVE,
    LBL_NEED,
    LBL_NEED_ITEM_NAME,
    LBL_LIMIT,
    SPR_TYPE_NORMAL,
    SPR_TYPE_EVENT,
    SPR_TYPE_STORY,
    SPR_TYPE_HARD,
    SPR_TYPE_SUB_EVENT,
    SPR_TYPE_EVENT_TEXT,
    SPR_TYPE_DAILY_TEXT,
    SPR_TYPE_WEEKLY_TEXT,
    SPR_DROP_DIFFICULTY_RARE,
    SPR_DROP_DIFFICULTY_SUPER_RARE,
    SPR_FRAME,
    OBJ_ICON_ROOT_1,
    OBJ_ICON_ROOT_2,
    GRD_ICON_ROOT,
    SPR_TYPE_TEXT_STORY,
    SPR_TYPE_TEXT_STORY_HARD,
    SPR_TYPE_TEXT_MODE_SECRET,
    SPR_TYPE_TEXT_MODE_HARD,
    LBL_BEST_TIME,
    SPR_BEST_TIME,
    SPR_MAIN_REWARD,
    OBJ_NEED,
    SPR_TYPE_DIFFICULTY,
    TEX_RANK_ICON,
    SPR_MISSION_CROWN_ON,
    SPR_MISSION_CROWN_OFF,
  }
}
