// Decompiled with JetBrains decompiler
// Type: ItemDetailQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ItemDetailQuest : QuestSelect
{
  private Transform questPrefab;
  private QuestSortData data;
  private Color backupAmbientLight = RenderSettings.ambientLight;
  private bool isGachaResult;
  private bool isInProgressMultiResultGacha;

  public override void Initialize()
  {
    this.data = GameSection.GetEventData() as QuestSortData;
    QuestItemInfo itemData = this.data.GetItemData() as QuestItemInfo;
    GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.QUEST_ITEM, this.data.GetUniqID());
    GameSection.SetEventData((object) itemData.infoData);
    this.isGachaResult = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName().Contains("Gacha");
    if (this.isGachaResult)
    {
      this.isInProgressMultiResultGacha = false;
      if (MonoBehaviourSingleton<GachaManager>.IsValid())
      {
        if (MonoBehaviourSingleton<GachaManager>.I.IsMultiResult() && MonoBehaviourSingleton<GachaManager>.I.IsExistNextGachaResult())
          this.isInProgressMultiResultGacha = true;
        else if (MonoBehaviourSingleton<GachaManager>.I.IsResultBonus() && !MonoBehaviourSingleton<GachaManager>.I.enableFeverDirector)
          this.isInProgressMultiResultGacha = true;
      }
      this.backupAmbientLight = RenderSettings.ambientLight;
      RenderSettings.ambientLight = Utility.MakeColorByInt(201, 210, 226);
    }
    base.Initialize();
  }

  protected override void OnClose()
  {
    if (this.isGachaResult)
      RenderSettings.ambientLight = this.backupAmbientLight;
    base.OnClose();
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.SetActive((Enum) ItemDetailQuest.UI.BTN_NEXT, false);
    this.SetActive((Enum) ItemDetailQuest.UI.OBJ_NEXT_OPT, false);
    this.SetActive((Enum) ItemDetailQuest.UI.BTN_PARTY, false);
    this.SetActive((Enum) ItemDetailQuest.UI.OBJ_PARTY_OPT, false);
    this.SetActive((Enum) ItemDetailQuest.UI.BTN_BACK, false);
    this.questPrefab = this.SetPrefab(this.collectUI, nameof (ItemDetailQuest));
    this.SetActive(this.questPrefab, (Enum) ItemDetailQuest.UI.BTN_SELL, !this.isGachaResult);
    this.SetActive(this.questPrefab, (Enum) ItemDetailQuest.UI.BTN_BATTLE, this.isGachaResult && !this.isInProgressMultiResultGacha);
    this.SetActive((Enum) ItemDetailQuest.UI.OBJ_PARTY_BTN_ROOT, false);
    this.SetLabelText(this.questPrefab, (Enum) ItemDetailQuest.UI.STR_BTN_SELL, this.sectionData.GetText("TEXT_EXCHANGE"));
    this.SetLabelText(this.questPrefab, (Enum) ItemDetailQuest.UI.STR_BTN_SELL_D, this.sectionData.GetText("TEXT_EXCHANGE"));
    this.SetActive(this.questPrefab, (Enum) ItemDetailQuest.UI.OBJ_ICON, !this.isGachaResult);
  }

  protected override void SetClearStatus(CLEAR_STATUS clear_status)
  {
    if (this.isGachaResult)
      return;
    base.SetClearStatus(clear_status);
  }

  private void OnQuery_SELL()
  {
    if (!this.data.CanSale())
      GameSection.ChangeEvent("NOT_SELL");
    else
      GameSection.SetEventData((object) this.data);
  }

  private void OnQuery_BATTLE()
  {
    if (this.data != null)
      return;
    GameSection.StopEvent();
  }

  protected void OnQuery_ItemDetailJumpQuestConfirm_YES()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[4]
    {
      new EventData(GameSection.GetGoingHomeEvent(), (object) null),
      new EventData("GACHA_QUEST_COUNTER", (object) null),
      new EventData("TO_GACHA_QUEST_COUNTER", (object) null),
      new EventData("SELECT_ORDER", (object) this.data.GetTableID())
    });
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY) != (GameSection.NOTIFY_FLAG) 0)
    {
      QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(this.data.GetTableID());
      if (questItem != null && questItem.infoData != null && questItem.infoData.questData.num > 0)
      {
        this.data = new QuestSortData();
        this.data.SetItem((object) questItem);
      }
    }
    base.OnNotify(flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY;
  }

  protected new enum UI
  {
    OBJ_FRAME,
    TEX_ENEMY,
    SPR_LOAD_ROTATE_CIRCLE,
    OBJ_LOADING,
    OBJ_QUEST_NORMAL_ROOT,
    LBL_QUEST_TYPE,
    LBL_QUEST_NAME,
    LBL_QUEST_NUM,
    LBL_LIMIT_TIME,
    OBJ_TOP_CROWN_ROOT,
    OBJ_TOP_CROWN_1,
    OBJ_TOP_CROWN_2,
    OBJ_TOP_CROWN_3,
    STR_MISSION_EMPTY,
    SPR_CROWN_1,
    SPR_CROWN_2,
    SPR_CROWN_3,
    OBJ_MISSION_INFO_ROOT,
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
    TWN_CHANGE_BTN,
    OBJ_CHANGE_INFO_TREASURE_ROOT,
    OBJ_CHANGE_INFO_MISSION_ROOT,
    OBJ_CHANGE_INFO_SELL_ROOT,
    OBJ_TREASURE,
    STR_TREASURE,
    GRD_REWARD_QUEST,
    OBJ_SELL_ITEM,
    STR_SELL,
    GRD_REWARD_SELL,
    OBJ_ENEMY,
    SPR_MONSTER_ICON,
    SPR_MONSTER_ICON_GRADE_FRAME,
    SPR_ELEMENT_ROOT,
    SPR_ELEMENT,
    SPR_WEAK_ELEMENT,
    STR_NON_ELEMENT,
    STR_NON_WEAK_ELEMENT,
    SPR_ELEMENT_ROOT_2,
    SPR_ELEMENT_2,
    SPR_WEAK_ELEMENT_2,
    STR_NON_ELEMENT_2,
    STR_NON_WEAK_ELEMENT_2,
    BTN_PARTY,
    BTN_BACK,
    OBJ_PARTY_OPT,
    TWN_DIFFICULT_STAR,
    OBJ_DIFFICULT_STAR_1,
    OBJ_DIFFICULT_STAR_2,
    OBJ_DIFFICULT_STAR_3,
    OBJ_DIFFICULT_STAR_4,
    OBJ_DIFFICULT_STAR_5,
    OBJ_DIFFICULT_STAR_6,
    OBJ_DIFFICULT_STAR_7,
    OBJ_DIFFICULT_STAR_8,
    OBJ_DIFFICULT_STAR_9,
    OBJ_DIFFICULT_STAR_10,
    OBJ_ICON,
    OBJ_ICON_NEW,
    OBJ_ICON_CLEARED,
    OBJ_ICON_COMPLETE,
    SPR_ICON_NEW,
    SPR_ICON_CLEARED,
    SPR_ICON_COMPLETE,
    OBJ_BACK_BTN_ROOT,
    OBJ_PARTY_BTN_ROOT,
    BTN_NEXT,
    OBJ_NEXT_OPT,
    OBJ_NEXT_BTN_ROOT,
    BTN_SELL,
    STR_BTN_SELL,
    STR_BTN_SELL_D,
    BTN_BATTLE,
    OBJ_REWARD_ICON_ROOT,
    OBJ_MATERIAL_ICON_ROOT,
    LBL_ENEMY_LEVEL,
    OBJ_LEVEL_R,
    OBJ_LEVEL_L,
    OBJ_LEVEL_INACTIVE_R,
    OBJ_LEVEL_INACTIVE_L,
  }
}
