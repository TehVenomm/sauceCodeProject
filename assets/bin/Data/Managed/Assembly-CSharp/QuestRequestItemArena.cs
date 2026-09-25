// Decompiled with JetBrains decompiler
// Type: QuestRequestItemArena
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class QuestRequestItemArena : QuestRequestItem
{
  private ArenaTable.ArenaData arenaData;

  public void SetupComplete(
    Transform t,
    DeliveryTable.DeliveryData info,
    ArenaUserRecordModel.Param record)
  {
    this.SetActive(t, (Enum) QuestRequestItemArena.UI.SPR_TYPE_DIFFICULTY, info.difficulty >= DIFFICULTY_MODE.HARD);
    this.InitArenaData(info);
    base.Setup(t, info);
    this.SetBestTimeOrStamp(t, record);
  }

  public override void Setup(Transform t, DeliveryTable.DeliveryData info)
  {
    this.SetActive(t, (Enum) QuestRequestItemArena.UI.SPR_TYPE_DIFFICULTY, info.difficulty >= DIFFICULTY_MODE.HARD);
    this.SetActive(t, (Enum) QuestRequestItemArena.UI.SPR_BEST_TIME, false);
    this.SetActive(t, (Enum) QuestRequestItemArena.UI.LBL_BEST_TIME, false);
    this.InitArenaData(info);
    base.Setup(t, info);
  }

  protected override void SetFrame(Transform t, DeliveryTable.DeliveryData info)
  {
  }

  protected override void SetDeliveryName(Transform t, DeliveryTable.DeliveryData info)
  {
    string arenaTitle = QuestUtility.GetArenaTitle(this.arenaData.group, info.name);
    this.SetLabelText(t, (Enum) QuestRequestItemArena.UI.LBL_DELIVERY_COMMENT, arenaTitle);
  }

  protected override void SetIcon(Transform t, DeliveryTable.DeliveryData info)
  {
    ResourceLoad.LoadWithSetUITexture(((Component) this.FindCtrl(t, (Enum) QuestRequestItemArena.UI.TEX_NPC)).GetComponent<UITexture>(), RESOURCE_CATEGORY.ARENA_RANK_ICON, ResourceName.GetArenaRankIconName(this.arenaData.rank));
  }

  private void InitArenaData(DeliveryTable.DeliveryData info)
  {
    this.arenaData = info.GetArenaData();
  }

  private void SetBestTimeOrStamp(Transform t, ArenaUserRecordModel.Param record)
  {
    if (this.arenaData.rank == ARENA_RANK.S)
    {
      string text = record == null ? QuestUtility.CreateTimeStringByMilliSec(QuestUtility.GetDefaultArenaTime()) : QuestUtility.CreateTimeStringByMilliSec(record.clearMilliSecList[(int) this.arenaData.group]);
      this.SetActive(t, (Enum) QuestRequestItemArena.UI.SPR_BEST_TIME, true);
      this.SetActive(t, (Enum) QuestRequestItemArena.UI.LBL_BEST_TIME, true);
      this.SetLabelText(t, (Enum) QuestRequestItemArena.UI.LBL_BEST_TIME, text);
      this.SetActive(t, (Enum) QuestRequestItemArena.UI.GRD_ICON_ROOT, false);
      this.SetActive(t, (Enum) QuestRequestItemArena.UI.SPR_MAIN_REWARD, false);
      this.SetActive(t, (Enum) QuestRequestItemArena.UI.OBJ_NEED, false);
    }
    else
    {
      this.SetActive(t, (Enum) QuestRequestItemArena.UI.SPR_BEST_TIME, false);
      this.SetActive(t, (Enum) QuestRequestItemArena.UI.LBL_BEST_TIME, false);
      this.SetActive(t, (Enum) QuestRequestItemArena.UI.OBJ_REQUEST_COMPLETED, true);
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
    OBJ_LEVEL_LIMIT,
    LBL_LEVEL_LIMIT,
    SPR_TYPE_TEXT_STORY,
    SPR_TYPE_TEXT_STORY_HARD,
    SPR_TYPE_TEXT_MODE_SECRET,
    SPR_TYPE_TEXT_MODE_HARD,
    LBL_BEST_TIME,
    SPR_BEST_TIME,
    SPR_MAIN_REWARD,
    OBJ_NEED,
    SPR_TYPE_DIFFICULTY,
  }
}
