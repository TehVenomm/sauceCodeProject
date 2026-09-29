// Decompiled with JetBrains decompiler
// Type: QuestAcceptDeliveryDetailForHappen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class QuestAcceptDeliveryDetailForHappen : QuestAcceptDeliveryDetail
{
  protected override void SetBaseFrame()
  {
    this.baseRoot = this.GetCtrl((Enum) QuestAcceptDeliveryDetailForHappen.UI.OBJ_BASE_FRAME);
  }

  protected override void SetTargetFrame()
  {
    this.targetFrame = this.GetCtrl((Enum) QuestAcceptDeliveryDetailForHappen.UI.OBJ_TARGET_FRAME);
  }

  protected override void SetSubmissionFrame()
  {
    this.submissionFrame = this.GetCtrl((Enum) QuestAcceptDeliveryDetailForHappen.UI.OBJ_SUBMISSION_FRAME);
  }

  protected override Vector3 GetEquipBtnPos() => new Vector3(170f, -350f, 0.0f);

  public override void UpdateUI()
  {
    base.UpdateUI();
    QuestTable.QuestTableData questData = this.info.GetQuestData();
    if (questData == null)
      return;
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) questData.GetMainEnemyID());
    if (enemyData == null)
      return;
    string text = this.info.enemyName;
    if (string.IsNullOrEmpty(text))
      text = enemyData.name;
    this.SetLabelText((Enum) QuestAcceptDeliveryDetailForHappen.UI.LBL_ENEMY_NAME, text);
    ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, new RARITY_TYPE?(), this.GetCtrl((Enum) QuestAcceptDeliveryDetailForHappen.UI.OBJ_ENEMY)).SetDepth(7);
    this.SetElementSprite((Enum) QuestAcceptDeliveryDetailForHappen.UI.SPR_ENM_ELEMENT, (int) enemyData.element);
    this.SetElementSprite((Enum) QuestAcceptDeliveryDetailForHappen.UI.SPR_WEAK_ELEMENT, (int) enemyData.weakElement);
    this.SetActive((Enum) QuestAcceptDeliveryDetailForHappen.UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
  }

  private void OnQuery_SWITCH_SUBMISSION()
  {
    if (!Object.op_Implicit((Object) this.targetFrame) || !Object.op_Implicit((Object) this.submissionFrame))
      return;
    bool activeSelf = ((Component) this.targetFrame).gameObject.activeSelf;
    ((Component) this.targetFrame).gameObject.SetActive(!activeSelf);
    ((Component) this.submissionFrame).gameObject.SetActive(activeSelf);
    this.isCompletedEventDelivery = true;
    this.RefreshUI();
  }

  protected new enum UI
  {
    OBJ_BASE_ROOT,
    OBJ_BACK,
    OBJ_COMPLETE_ROOT,
    BTN_COMPLETE,
    CHARA_ALL,
    OBJ_UNLOCK_PORTAL_ROOT,
    LBL_UNLOCK_PORTAL,
    LBL_QUEST_TITLE,
    LBL_CHARA_MESSAGE,
    LBL_PERSON_NAME,
    TEX_NPC,
    BTN_JUMP_QUEST,
    BTN_JUMP_INVALID,
    BTN_JUMP_MAP,
    BTN_JUMP_GACHATOP,
    GRD_REWARD,
    LBL_MONEY,
    LBL_EXP,
    SPR_WINDOW,
    SPR_MESSAGE_BG,
    OBJ_NEED_ITEM_ROOT,
    LBL_NEED_ITEM_NAME,
    LBL_NEED,
    LBL_HAVE,
    LBL_PLACE_NAME,
    LBL_ENEMY_NAME,
    OBJ_DIFFICULTY_ROOT,
    OBJ_ENEMY_NAME_ROOT,
    LBL_GET_PLACE,
    OBJ_ENEMY,
    SPR_ELEMENT_ROOT,
    SPR_ENM_ELEMENT,
    SPR_WEAK_ELEMENT,
    STR_NON_WEAK_ELEMENT,
    BTN_SUBMISSION,
    STR_BTN_SUBMISSION,
    STR_BTN_SUBMISSION_BACK,
    OBJ_TOP_CROWN_ROOT,
    OBJ_TOP_CROWN_1,
    OBJ_TOP_CROWN_2,
    OBJ_TOP_CROWN_3,
    STR_MISSION_EMPTY,
    SPR_CROWN_1,
    SPR_CROWN_2,
    SPR_CROWN_3,
    OBJ_SUBMISSION_ROOT,
    OBJ_MISSION_INFO,
    OBJ_MISSION_INFO_1,
    OBJ_MISSION_INFO_2,
    OBJ_MISSION_INFO_3,
    LBL_MISSION_INFO_1,
    LBL_MISSION_INFO_2,
    LBL_MISSION_INFO_3,
    SPR_MISSION_INFO_CROWN_1,
    SPR_MISSION_INFO_CROWN_2,
    SPR_MISSION_INFO_CROWN_3,
    STR_MISSION,
    OBJ_BASE_FRAME,
    OBJ_TARGET_FRAME,
    OBJ_SUBMISSION_FRAME,
    OBJ_NORMAL_ROOT,
    OBJ_EVENT_ROOT,
    LBL_POINT_NORMAL,
    TEX_NORMAL_ICON,
    LBL_POINT_EVENT,
    TEX_EVENT_ICON,
    BTN_CREATE,
    BTN_JOIN,
    BTN_MATCHING,
  }
}
