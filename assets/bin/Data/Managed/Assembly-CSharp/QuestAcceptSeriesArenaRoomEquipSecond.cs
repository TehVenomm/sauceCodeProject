// Decompiled with JetBrains decompiler
// Type: QuestAcceptSeriesArenaRoomEquipSecond
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class QuestAcceptSeriesArenaRoomEquipSecond : UniqueStatusEquipSecond
{
  protected override void OnQuery_SELECT_ITEM()
  {
    if (!this.OnSelectItemAndChekIsGoStatus())
      return;
    GameSection.ChangeEvent("USER_EQUIP");
  }

  protected override void ChangeSelectItem(EquipItemInfo select_item, EquipItemInfo old_item)
  {
    GameSection.SetEventData((object) new StatusEquip.ChangeEquipData(this.selectEquipSetData.setNo, this.selectEquipSetData.index, select_item));
    if (old_item == null || select_item == null)
    {
      GameSection.ChangeEvent("USER_EQUIP");
    }
    else
    {
      bool flag = false;
      for (int index = 0; index < old_item.GetMaxSlot(); ++index)
      {
        if (old_item.GetUniqueSkillItem(index) != null)
        {
          flag = true;
          break;
        }
      }
      if (flag)
      {
        this.migrationOldItem = old_item;
        this.migrationSelectItem = select_item;
        GameSection.GetEventData();
        GameSection.ChangeEvent("MIGRATION_SKILL_CONFIRM");
      }
      else
        GameSection.ChangeEvent("USER_EQUIP");
    }
  }

  private void OnQuery_QuestAcceptUniqueMigrationSkillConfirm_YES()
  {
    this.OnQuery_StatusMigrationSkillConfirm_YES();
  }

  private void OnQuery_QuestAcceptUniqueMigrationSkillConfirm_NO()
  {
    this.OnQuery_StatusMigrationSkillConfirm_NO();
  }

  private void OnCloseDialog_QuestAcceptEquipSort() => this.OnCloseSortDialog();

  private void OnQuery_QuestAcceptUniqueOrderSwapEquipConfirm_YES()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET_INFO);
    this.OnQuery_UniqueStatusOrderSwapEquipConfirm_YES();
  }

  private void OnQuery_QuestAcceptUniqueClosetSwapEquipConfirm_YES()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET_INFO);
    this.OnQuery_UniqueStatusClosetSwapEquipConfirm_YES();
  }

  private void OnQuery_QuestAcceptUniqueSwapEquipConfirm_YES()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET_INFO);
    this.OnQuery_UniqueStatusSwapEquipConfirm_YES();
  }

  private void OnQuery_QuestAcceptUniqueRemoveEquipConfirm_YES()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET_INFO);
    this.OnQuery_UniqueStatusRemoveEquipConfirm_YES();
  }

  public new enum UI
  {
    LBL_NAME,
    LBL_LV_NOW,
    LBL_LV_MAX,
    LBL_ATK,
    LBL_DEF,
    LBL_ELEM,
    SPR_ELEM,
    LBL_SELL,
    TEX_MODEL,
    SCR_INVENTORY,
    GRD_INVENTORY,
    GRD_INVENTORY_SMALL,
    LBL_SORT,
    BTN_SORT,
    BTN_BACK,
    TGL_CHANGE_INVENTORY,
    TGL_ICON_ASC,
    OBJ_SKILL_BUTTON_ROOT,
    OBJ_DETAIL_ROOT,
    BTN_SELL,
    BTN_GROW,
    OBJ_FAVORITE_ROOT,
    SPR_IS_EVOLVE,
    OBJ_ATK_ROOT,
    OBJ_DEF_ROOT,
    OBJ_ELEM_ROOT,
    OBJ_SELL_ROOT,
    SPR_SELECT_WEAPON,
    SPR_SELECT_DEF,
    SPR_TYPE_ICON,
    SPR_TYPE_ICON_BG,
    SPR_TYPE_ICON_RARITY,
    LBL_SELECT_TYPE,
    OBJ_STATUS_ROOT,
    LBL_STATUS_ATK,
    LBL_STATUS_DEF,
    LBL_STATUS_HP,
    LBL_STATUS_ADD_ATK,
    LBL_STATUS_ADD_DEF,
    LBL_STATUS_ADD_HP,
    STR_TITLE_ITEM_INFO,
    STR_TITLE_STATUS,
    STR_TITLE_SKILL_SLOT,
    STR_TITLE_ABILITY,
    STR_TITLE_MONEY,
    STR_TITLE_MATERIAL,
    STR_TITLE_ELEMENT,
    TBL_ABILITY,
    STR_NON_ABILITY,
    OBJ_ABILITY,
    LBL_ABILITY,
    LBL_ABILITY_NUM,
    OBJ_FIXEDABILITY,
    LBL_FIXEDABILITY,
    LBL_FIXEDABILITY_NUM,
    OBJ_ABILITY_ITEM,
    LBL_ABILITY_ITEM,
    OBJ_WEAPON_WINDOW,
    OBJ_DEFENSE_WINDOW,
    SCR_INVENTORY_DEF,
    GRD_INVENTORY_DEF,
    GRD_INVENTORY_SMALL_DEF,
    BTN_WEAPON_1,
    BTN_WEAPON_2,
    BTN_WEAPON_3,
    BTN_WEAPON_4,
    BTN_WEAPON_5,
    OBJ_CAPTION_3,
    LBL_CAPTION,
  }
}
