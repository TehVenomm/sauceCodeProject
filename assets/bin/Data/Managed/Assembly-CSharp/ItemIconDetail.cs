// Decompiled with JetBrains decompiler
// Type: ItemIconDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ItemIconDetail : ItemIcon
{
  public ItemIconDetailEquipSetupper setupperEquip;
  public ItemIconDetailEquipAbilitySetupper setupperEquipAbility;
  public ItemIconDetailSkillSetupper setupperSkill;
  public ItemIconDetailMaterialSetupper setupperMaterial;
  public ItemIconDetailQuestItemSetupper setupperQuestItem;
  public ItemIconDetailAccessorySetupper setupperAccessory;
  public ItemIconDetailRemoveBtnSetupper setupperRemoveBtn;
  public UISprite spriteGrayout;

  public static ItemIcon CreateRemoveButton(
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    int toggle_group = -1,
    bool is_select = false,
    string name = null)
  {
    ItemIconDetail icon = ItemIcon.CreateIcon<ItemIconDetail>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailPrefab, ITEM_ICON_TYPE.NONE, ItemIcon.GetRemoveButtonIconID(), new RARITY_TYPE?(), parent, event_name: event_name, event_data: event_data, toggle_group: toggle_group, is_select: is_select);
    icon.setupperRemoveBtn.Set(new object[1]
    {
      (object) name
    });
    icon.SetFavoriteIcon(false);
    return (ItemIcon) icon;
  }

  public static ItemIcon CreateMaterialIcon(
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    ItemTable.ItemData item_table,
    bool is_show_main_status,
    Transform parent = null,
    int num = -1,
    string name = null,
    string event_name = null,
    int event_data = 0,
    int toggle_group = -1,
    bool is_select = false,
    bool is_new = false)
  {
    ItemIconDetail icon = ItemIcon.CreateIcon<ItemIconDetail>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailPrefab, icon_type, icon_id, rarity, parent, event_name: event_name, event_data: event_data, is_new: is_new, toggle_group: toggle_group, is_select: is_select, icon_under_text: string.Empty, enemy_icon_id: item_table.enemyIconID, enemy_icon_id2: item_table.enemyIconID2);
    icon.setupperMaterial.Set(new object[3]
    {
      (object) item_table,
      (object) num,
      (object) is_show_main_status
    });
    return (ItemIcon) icon;
  }

  public static ItemIcon CreateEquipDetailIcon(
    EquipItemSortData item_data,
    SkillSlotUIData[] skill_slot_data,
    bool is_show_main_status,
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE,
    bool is_new = false,
    int toggle_group = -1,
    bool is_select = false,
    int equipping_sp_index = -1)
  {
    ItemIcon equipDetailIcon = ItemIconDetail._CreateEquipDetailIcon(item_data, skill_slot_data, is_show_main_status, parent, event_name, event_data, icon_status, is_new, toggle_group, is_select ? 0 : -1, equipping_sp_index);
    equipDetailIcon.SetFavoriteIcon(item_data.IsFavorite());
    return equipDetailIcon;
  }

  public static ItemIcon CreateEquipDetailSelectNumberIcon(
    EquipItemSortData item_data,
    SkillSlotUIData[] skill_slot_data,
    bool is_show_main_status,
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE,
    bool is_new = false,
    int toggle_group = -1,
    int select_number = -1,
    int equipping_sp_index = -1,
    GET_TYPE getType = GET_TYPE.PAY)
  {
    ItemIcon equipDetailIcon = ItemIconDetail._CreateEquipDetailIcon(item_data, skill_slot_data, is_show_main_status, parent, event_name, event_data, icon_status, is_new, toggle_group, select_number, equipping_sp_index);
    equipDetailIcon.SetFavoriteIcon(item_data.IsFavorite());
    return equipDetailIcon;
  }

  private static ItemIcon _CreateEquipDetailIcon(
    EquipItemSortData item_data,
    SkillSlotUIData[] skill_slot_data,
    bool is_show_main_status,
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE,
    bool is_new = false,
    int toggle_group = -1,
    int select_number = -1,
    int equipping_sp_index = -1)
  {
    int sex = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
    bool is_equipping = equipping_sp_index == 0;
    ItemIconDetail equipItemIconDetail = ItemIconDetail.CreateEquipItemIconDetail(item_data.GetItemData() as EquipItemInfo, sex, parent, event_name: event_name, event_data: event_data, is_new: is_new, toggle_group: toggle_group, is_select: select_number > -1, icon_under_text: string.Empty, is_equipping: is_equipping);
    equipItemIconDetail.setupperEquip.Set(new object[6]
    {
      (object) (item_data.GetItemData() as EquipItemInfo),
      (object) skill_slot_data,
      (object) is_show_main_status,
      (object) icon_status,
      (object) equipping_sp_index,
      (object) select_number
    });
    equipItemIconDetail.SetFavoriteIcon(item_data.IsFavorite());
    if (Object.op_Implicit((Object) equipItemIconDetail.setupperEquip.lvRoot))
    {
      UILabel[] componentsInChildren = equipItemIconDetail.setupperEquip.lvRoot.GetComponentsInChildren<UILabel>();
      equipItemIconDetail.SetEquipExt(item_data.equipData, componentsInChildren);
    }
    return (ItemIcon) equipItemIconDetail;
  }

  public static ItemIcon CreateSmithCreateEquipDetailIcon(
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    SmithCreateSortData item_data,
    SkillSlotUIData[] skill_slot_data,
    bool is_show_main_status,
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE,
    bool is_new = false,
    int toggle_group = -1,
    bool is_select = false,
    bool is_equipping = false,
    GET_TYPE getType = GET_TYPE.PAY)
  {
    SmithCreateItemInfo itemData = item_data.GetItemData() as SmithCreateItemInfo;
    ItemIconDetail icon = ItemIcon.CreateIcon<ItemIconDetail>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailPrefab, icon_type, icon_id, rarity, parent, itemData.equipTableData.GetTargetElementPriorityToTable(), event_name: event_name, event_data: event_data, is_new: is_new, toggle_group: toggle_group, is_select: is_select, icon_under_text: string.Empty, is_equipping: is_equipping, getType: getType);
    EquipItemTable.EquipItemData equipTableData = item_data.createData.equipTableData;
    icon.setupperEquip.Set(new object[6]
    {
      (object) equipTableData,
      (object) skill_slot_data,
      (object) is_show_main_status,
      (object) icon_status,
      (object) -1,
      (object) -1
    });
    return (ItemIcon) icon;
  }

  public static ItemIcon CreateEquipAbilityIcon(
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    EquipItemSortData item_data,
    SkillSlotUIData[] skill_slot_data,
    bool is_show_main_status,
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE,
    bool is_new = false,
    int toggle_group = -1,
    bool is_select = false,
    int equipping_sp_index = -1,
    GET_TYPE getType = GET_TYPE.PAY)
  {
    bool is_equipping = equipping_sp_index == 0;
    EquipItemInfo itemData = item_data.GetItemData() as EquipItemInfo;
    ItemIconDetail icon = ItemIcon.CreateIcon<ItemIconDetail>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailPrefab, icon_type, icon_id, rarity, parent, itemData.GetTargetElementPriorityToTable(), event_name: event_name, event_data: event_data, is_new: is_new, toggle_group: toggle_group, icon_under_text: string.Empty, is_equipping: is_equipping, getType: getType);
    icon.setupperEquipAbility.Set(new object[5]
    {
      (object) (item_data.GetItemData() as EquipItemInfo),
      (object) skill_slot_data,
      (object) is_show_main_status,
      (object) icon_status,
      (object) equipping_sp_index
    });
    icon.SetFavoriteIcon(item_data.IsFavorite());
    if (Object.op_Implicit((Object) icon.setupperEquip.lvRoot))
    {
      UILabel[] componentsInChildren = icon.setupperEquipAbility.lvRoot.GetComponentsInChildren<UILabel>();
      icon.SetEquipExt(item_data.equipData, componentsInChildren);
    }
    return (ItemIcon) icon;
  }

  public static ItemIcon CreateEquipRevertLithographIcon(
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    EquipItemSortData item_data,
    SkillSlotUIData[] skill_slot_data,
    bool is_show_main_status,
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE,
    bool is_new = false,
    int toggle_group = -1,
    bool is_select = false,
    int equipping_sp_index = -1,
    GET_TYPE getType = GET_TYPE.PAY)
  {
    bool is_equipping = equipping_sp_index == 0;
    EquipItemInfo itemData = item_data.GetItemData() as EquipItemInfo;
    ItemIconDetail icon = ItemIcon.CreateIcon<ItemIconDetail>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailPrefab, icon_type, icon_id, rarity, parent, itemData.GetTargetElementPriorityToTable(), event_name: event_name, event_data: event_data, is_new: is_new, toggle_group: toggle_group, icon_under_text: string.Empty, is_equipping: is_equipping, getType: getType);
    icon.setupperEquipAbility.Set(new object[5]
    {
      (object) (item_data.GetItemData() as EquipItemInfo),
      (object) skill_slot_data,
      (object) is_show_main_status,
      (object) icon_status,
      (object) equipping_sp_index
    });
    icon.SetFavoriteIcon(item_data.IsFavorite());
    if (Object.op_Implicit((Object) icon.setupperEquip.lvRoot))
    {
      UILabel[] componentsInChildren = icon.setupperEquipAbility.lvRoot.GetComponentsInChildren<UILabel>();
      icon.SetEquipExt(item_data.equipData, componentsInChildren);
    }
    return (ItemIcon) icon;
  }

  public static ItemIcon CreateSkillDetailIcon(
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    SkillItemSortData item_data,
    bool is_show_main_status,
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    bool is_new = false,
    int toggle_group = -1,
    bool is_select = false,
    bool is_equipping = false,
    bool isValidExceed = false,
    bool isShowEnableExceed = false)
  {
    ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE;
    if (isShowEnableExceed)
      icon_status = ItemIconDetail.ICON_STATUS.VALID_EXCEED_0;
    else if (isValidExceed)
      icon_status = ItemIconDetail.ICON_STATUS.VALID_EXCEED;
    ItemIcon skillDetailIcon = ItemIconDetail._CreateSkillDetailIcon(icon_type, icon_id, rarity, item_data, is_show_main_status, parent, event_name, event_data, icon_status, is_new, toggle_group, is_select ? 0 : -1, is_equipping);
    skillDetailIcon.SetFavoriteIcon(item_data.IsFavorite());
    return skillDetailIcon;
  }

  public static ItemIcon CreateSkillDetailSelectNumberIcon(
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    SkillItemSortData item_data,
    bool is_show_main_status,
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    bool is_new = false,
    int toggle_group = -1,
    int select_number = -1,
    bool is_equipping = false,
    ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE,
    bool isSameSkillExceed = false)
  {
    ItemIcon skillDetailIcon = ItemIconDetail._CreateSkillDetailIcon(icon_type, icon_id, rarity, item_data, is_show_main_status, parent, event_name, event_data, icon_status, is_new, toggle_group, select_number, is_equipping, isSameSkillExceed);
    skillDetailIcon.SetFavoriteIcon(item_data.IsFavorite());
    return skillDetailIcon;
  }

  private static ItemIcon _CreateSkillDetailIcon(
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    SkillItemSortData item_data,
    bool is_show_main_status,
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE,
    bool is_new = false,
    int toggle_group = -1,
    int select_number = -1,
    bool is_equipping = false,
    bool isSameSkillExceed = false)
  {
    ItemIconDetail icon = ItemIcon.CreateIcon<ItemIconDetail>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailPrefab, icon_type, icon_id, rarity, parent, item_data.GetIconElement(), item_data.skillData.tableData.GetEnableEquipType(), event_name: event_name, event_data: event_data, is_new: is_new, toggle_group: toggle_group, is_select: select_number > -1, icon_under_text: string.Empty, is_equipping: is_equipping, element2: item_data.GetIconElementSub());
    icon.setupperSkill.Set(new object[5]
    {
      (object) item_data,
      (object) is_show_main_status,
      (object) select_number,
      (object) icon_status,
      (object) isSameSkillExceed
    });
    icon.SetFavoriteIcon(item_data.IsFavorite());
    return (ItemIcon) icon;
  }

  public static ItemIcon CreateQuestItemIcon(
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    QuestSortData quest_item,
    bool is_show_main_status,
    Transform parent = null,
    int num = -1,
    string name = null,
    string event_name = null,
    int event_data = 0,
    int toggle_group = -1,
    bool is_select = false,
    bool is_new = false)
  {
    ItemIconDetail icon = ItemIcon.CreateIcon<ItemIconDetail>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailPrefab, icon_type, icon_id, rarity, parent, quest_item.GetEnemyElement(), event_name: event_name, event_data: event_data, is_new: is_new, toggle_group: toggle_group, is_select: is_select, icon_under_text: string.Empty);
    icon.setupperQuestItem.Set(new object[2]
    {
      (object) quest_item,
      (object) is_show_main_status
    });
    return (ItemIcon) icon;
  }

  public static ItemIconDetail CreateEquipItemIconDetail(
    EquipItemInfo equipItemInfo,
    int sex,
    Transform parent,
    EQUIPMENT_TYPE? magi_enable_icon_type = null,
    int num = -1,
    string event_name = null,
    int event_data = 0,
    bool is_new = false,
    int toggle_group = -1,
    bool is_select = false,
    string icon_under_text = null,
    bool is_equipping = false,
    bool disable_rarity_text = false)
  {
    return ItemIcon.CreateEquipIconByEquipItemInfo<ItemIconDetail>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailPrefab, equipItemInfo, sex, parent, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, disable_rarity_text);
  }

  public static ItemIcon CreateAccessoryIcon(
    AccessoryTable.AccessoryData data,
    Transform _parent,
    string _eventName,
    int _eventData,
    bool _isNew,
    bool _isEquipping)
  {
    ItemIconDetail accessoryIcon = ItemIcon.CreateAccessoryIcon((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailPrefab, (int) data.accessoryId, data.rarity, _parent, _eventName, _eventData, _isNew, _isEquipping, data.getType);
    accessoryIcon.setupperAccessory.Set(new object[1]
    {
      (object) data
    });
    return (ItemIcon) accessoryIcon;
  }

  public override void SetGrayout(bool isActive)
  {
    if (!Object.op_Inequality((Object) this.spriteGrayout, (Object) null))
      return;
    ((Behaviour) this.spriteGrayout).enabled = isActive;
  }

  public enum ICON_STATUS
  {
    NONE,
    NOT_ENOUGH_MATERIAL,
    VALID_EVOLVE,
    GRAYOUT,
    GROW_MAX,
    VALID_EXCEED_0,
    VALID_EXCEED,
  }
}
