// Decompiled with JetBrains decompiler
// Type: ItemIconDetailSmall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ItemIconDetailSmall : ItemIcon
{
  private const string STR_X = "×";
  public UISprite spriteValueType;
  public UISprite spRegistedAchievement;
  public UISprite spGrowMax;
  public UISprite spriteValidEvolve;
  public UISprite spriteGrayOut;
  public UISprite spriteEquipIndex;
  public UISprite spriteSelectNumber;
  public UISprite spriteEnableExceed;
  public UISprite[] spriteBgSmall;

  public static ItemIcon CreateSmallRemoveButton(
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    int toggle_group = -1,
    bool is_select = false,
    string name = null)
  {
    ItemIconDetailSmall icon = ItemIcon.CreateIcon<ItemIconDetailSmall>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailSmallPrefab, ITEM_ICON_TYPE.NONE, ItemIcon.GetRemoveButtonIconID(), new RARITY_TYPE?(), parent, event_name: event_name, event_data: event_data, toggle_group: toggle_group, is_select: is_select, icon_under_text: name);
    icon.EquipTypeIconInit();
    icon.SetEquipIndexSprite(-1);
    icon.SetIconStatusSprite();
    icon.SetupSelectNumberSprite();
    icon.SetFavoriteIcon(false);
    return (ItemIcon) icon;
  }

  public static ItemIcon CreateSmallMaterialIcon(
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    Transform parent = null,
    int num = -1,
    string name = null,
    string event_name = null,
    int event_data = 0,
    int toggle_group = -1,
    bool is_select = false,
    bool is_new = false,
    int enemy_icon_id = 0,
    int enemy_icon_id2 = 0,
    ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE)
  {
    string icon_under_text = "×" + num.ToString();
    ItemIconDetailSmall icon = ItemIcon.CreateIcon<ItemIconDetailSmall>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailSmallPrefab, icon_type, icon_id, rarity, parent, event_name: event_name, event_data: event_data, is_new: is_new, toggle_group: toggle_group, is_select: is_select, icon_under_text: icon_under_text, enemy_icon_id: enemy_icon_id, enemy_icon_id2: enemy_icon_id2);
    icon.EquipTypeIconInit();
    icon.SetEquipIndexSprite(-1);
    icon.SetIconStatusSprite(icon_status);
    icon.SetupSelectNumberSprite();
    icon.SetFavoriteIcon(false);
    return (ItemIcon) icon;
  }

  public static ItemIcon CreateSmallEquipDetailIcon(
    EquipItemSortData item_data,
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE,
    bool is_new = false,
    int toggle_group = -1,
    bool is_select = false,
    int equip_index = -1)
  {
    ItemIcon smallEquipDetailIcon = ItemIconDetailSmall._CreateSmallEquipDetailIcon(item_data, parent, event_name, event_data, icon_status, is_new, toggle_group, is_select ? 0 : -1, equip_index);
    smallEquipDetailIcon.SetFavoriteIcon(item_data.IsFavorite());
    return smallEquipDetailIcon;
  }

  public static ItemIcon CreateSmallEquipSelectDetailIcon(
    EquipItemSortData item_data,
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE,
    bool is_new = false,
    int toggle_group = -1,
    int select_number = -1,
    int equip_index = -1)
  {
    ItemIcon smallEquipDetailIcon = ItemIconDetailSmall._CreateSmallEquipDetailIcon(item_data, parent, event_name, event_data, icon_status, is_new, toggle_group, select_number, equip_index);
    smallEquipDetailIcon.SetFavoriteIcon(item_data.IsFavorite());
    return smallEquipDetailIcon;
  }

  private static ItemIcon _CreateSmallEquipDetailIcon(
    EquipItemSortData item_data,
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE,
    bool is_new = false,
    int toggle_group = -1,
    int select_number = -1,
    int equip_index = -1)
  {
    int sex = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
    bool is_equipping = equip_index == 0;
    string icon_under_text = item_data.equipData.tableData.IsWeapon() ? item_data.equipData.atk.ToString() : item_data.equipData.def.ToString();
    EquipItemInfo itemData = item_data.GetItemData() as EquipItemInfo;
    ItemIconDetailSmall itemIconDetailSmall = ItemIconDetailSmall.CreateEquipItemIconDetailSmall(itemData, sex, parent, event_name: event_name, event_data: event_data, is_new: is_new, toggle_group: toggle_group, is_select: select_number > -1, icon_under_text: icon_under_text, is_equipping: is_equipping);
    itemIconDetailSmall.EquipTypeIconInit(itemData.tableData);
    itemIconDetailSmall.SetEquipIndexSprite(equip_index - 1);
    itemIconDetailSmall.SetIconStatusSprite(icon_status);
    itemIconDetailSmall.SetupSelectNumberSprite(select_number);
    itemIconDetailSmall.SetFavoriteIcon(itemData.isFavorite);
    itemIconDetailSmall.SetEquipExt(item_data.equipData);
    return (ItemIcon) itemIconDetailSmall;
  }

  private static ItemIconDetailSmall CreateEquipItemIconDetailSmall(
    EquipItemInfo eauipItemInfo,
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
    return ItemIcon.CreateEquipIconByEquipItemInfo<ItemIconDetailSmall>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailSmallPrefab, eauipItemInfo, sex, parent, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, disable_rarity_text);
  }

  public static ItemIcon CreateSmithCreateEquipDetailIcon(
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    SmithCreateSortData item_data,
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
    string icon_under_text = itemData.equipTableData.IsWeapon() ? itemData.equipTableData.baseAtk.ToString() : itemData.equipTableData.baseDef.ToString();
    ItemIconDetailSmall icon = ItemIcon.CreateIcon<ItemIconDetailSmall>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailSmallPrefab, icon_type, icon_id, rarity, parent, itemData.equipTableData.GetTargetElementPriorityToTable(), event_name: event_name, event_data: event_data, is_new: is_new, toggle_group: toggle_group, is_select: is_select, icon_under_text: icon_under_text, is_equipping: is_equipping, getType: getType);
    icon.EquipTypeIconInit(itemData.equipTableData);
    icon.SetEquipIndexSprite(-1);
    icon.SetIconStatusSprite(icon_status);
    icon.SetupSelectNumberSprite();
    icon.SetFavoriteIcon(false);
    return (ItemIcon) icon;
  }

  public static ItemIcon CreateSmallSkillDetailIcon(
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    SkillItemSortData item_data,
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
    ItemIcon smallSkillDetailIcon = ItemIconDetailSmall._CreateSmallSkillDetailIcon(icon_type, icon_id, rarity, item_data, parent, event_name, event_data, is_new, toggle_group, is_select ? 0 : -1, is_equipping, icon_status);
    smallSkillDetailIcon.SetFavoriteIcon(item_data.IsFavorite());
    return smallSkillDetailIcon;
  }

  public static ItemIcon CreateSmallSkillSelectDetailIcon(
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    SkillItemSortData item_data,
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    bool is_new = false,
    int toggle_group = -1,
    int select_number = -1,
    bool is_equipping = false,
    ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE)
  {
    ItemIcon smallSkillDetailIcon = ItemIconDetailSmall._CreateSmallSkillDetailIcon(icon_type, icon_id, rarity, item_data, parent, event_name, event_data, is_new, toggle_group, select_number, is_equipping, icon_status);
    smallSkillDetailIcon.SetFavoriteIcon(item_data.IsFavorite());
    return smallSkillDetailIcon;
  }

  private static ItemIcon _CreateSmallSkillDetailIcon(
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    SkillItemSortData item_data,
    Transform parent = null,
    string event_name = null,
    int event_data = 0,
    bool is_new = false,
    int toggle_group = -1,
    int select_number = -1,
    bool is_equipping = false,
    ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE)
  {
    string icon_under_text = string.Format(item_data.skillData.IsExceeded() ? "Lv. {0}/" + UIUtility.GetColorText("{1}", ExceedSkillItemTable.color) : "Lv. {0}/{1}", (object) item_data.GetLevel(), (object) item_data.skillData.GetMaxLevel());
    ItemIconDetailSmall icon = ItemIcon.CreateIcon<ItemIconDetailSmall>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailSmallPrefab, icon_type, icon_id, rarity, parent, item_data.GetIconElement(), item_data.skillData.tableData.GetEnableEquipType(), event_name: event_name, event_data: event_data, is_new: is_new, toggle_group: toggle_group, is_select: select_number > -1, icon_under_text: icon_under_text, is_equipping: is_equipping);
    icon.EquipTypeIconInit();
    icon.SetEquipIndexSprite(-1);
    icon.SetIconStatusSprite(icon_status);
    icon.SetupSelectNumberSprite(select_number);
    icon.SetFavoriteIcon(item_data.IsFavorite());
    return (ItemIcon) icon;
  }

  public static ItemIcon CreateSmallQuestItemIcon(
    ITEM_ICON_TYPE icon_type,
    int icon_id,
    RARITY_TYPE? rarity,
    Transform parent = null,
    ELEMENT_TYPE element_type = ELEMENT_TYPE.MAX,
    int num = -1,
    string name = null,
    string event_name = null,
    int event_data = 0,
    int toggle_group = -1,
    bool is_select = false,
    bool is_new = false)
  {
    string icon_under_text = "×" + num.ToString();
    ItemIconDetailSmall icon = ItemIcon.CreateIcon<ItemIconDetailSmall>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailSmallPrefab, icon_type, icon_id, rarity, parent, element_type, event_name: event_name, event_data: event_data, is_new: is_new, toggle_group: toggle_group, is_select: is_select, icon_under_text: icon_under_text);
    icon.EquipTypeIconInit();
    icon.SetEquipIndexSprite(-1);
    icon.SetIconStatusSprite();
    icon.SetupSelectNumberSprite();
    icon.SetFavoriteIcon(false);
    return (ItemIcon) icon;
  }

  public static ItemIcon CreateSmallListItemIcon(
    ITEM_ICON_TYPE iconType,
    EquipItemSortData sortData,
    Transform parent,
    bool isNew,
    int no,
    GET_TYPE getType)
  {
    EquipItemTable.EquipItemData tableData = sortData.equipData.tableData;
    EquipItemInfo itemData = sortData.GetItemData() as EquipItemInfo;
    ItemIconDetailSmall icon = ItemIcon.CreateIcon<ItemIconDetailSmall>((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailSmallPrefab, iconType, tableData.GetIconID(), new RARITY_TYPE?(tableData.rarity), parent, itemData.GetTargetElementPriorityToTable(), event_name: string.Empty, is_new: isNew, icon_under_text: "No." + no.ToString("D4"), getType: getType);
    icon.EquipTypeIconInit(tableData);
    icon.SetEquipIndexSprite(-1);
    icon.EquipTypeIconInit();
    icon.SetIconStatusSprite();
    icon.SetupSelectNumberSprite();
    icon.SetFavoriteIcon(false);
    return (ItemIcon) icon;
  }

  public void EquipTypeIconInit(EquipItemTable.EquipItemData equip_table = null)
  {
    if (equip_table == null)
    {
      ((Behaviour) this.spriteValueType).enabled = false;
    }
    else
    {
      ((Behaviour) this.spriteValueType).enabled = true;
      this.spriteValueType.spriteName = equip_table.IsWeapon() ? ItemIcon.SPR_TYPE_ATK : ItemIcon.SPR_TYPE_DEF;
    }
  }

  private void SetIconStatusSprite(ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE)
  {
    this.SetRegistedIcon(false);
    ((Component) this.spriteValidEvolve).gameObject.SetActive(icon_status == ItemIconDetail.ICON_STATUS.VALID_EVOLVE);
    ((Behaviour) this.spGrowMax).enabled = icon_status == ItemIconDetail.ICON_STATUS.GROW_MAX;
    ((Behaviour) this.spriteGrayOut).enabled = icon_status == ItemIconDetail.ICON_STATUS.GRAYOUT || icon_status == ItemIconDetail.ICON_STATUS.NOT_ENOUGH_MATERIAL;
    ((Component) this.spriteEnableExceed).gameObject.SetActive(icon_status == ItemIconDetail.ICON_STATUS.VALID_EXCEED_0);
    bool flag = icon_status == ItemIconDetail.ICON_STATUS.VALID_EXCEED || icon_status == ItemIconDetail.ICON_STATUS.VALID_EXCEED_0;
    ((Component) this.spriteBgSmall[0]).gameObject.SetActive(!flag);
    ((Component) this.spriteBgSmall[1]).gameObject.SetActive(flag);
  }

  public void SetEquipIndexSprite(int index)
  {
    if (index < 0 || ItemIconDetailEquipSetupper.SPR_EQUIP_INDEX.Length <= index)
      this.spriteEquipIndex.spriteName = string.Empty;
    else
      this.spriteEquipIndex.spriteName = ItemIconDetailEquipSetupper.SPR_EQUIP_INDEX[index];
  }

  public void SetupSelectNumberSprite(int select_number = -1)
  {
    if (Object.op_Equality((Object) this.spriteSelectNumber, (Object) null))
      return;
    int index = select_number - 1;
    if ((index < 0 ? 0 : (index < ItemIconDetailSetuperBase.SPR_SKILL_MATERIAL_NUMBER.Length ? 1 : 0)) != 0)
    {
      ((Behaviour) this.spriteSelectNumber).enabled = true;
      this.spriteSelectNumber.spriteName = ItemIconDetailSetuperBase.SPR_SKILL_MATERIAL_NUMBER[index];
    }
    else
      ((Behaviour) this.spriteSelectNumber).enabled = false;
  }

  public void SetRegistedIcon(bool is_visible)
  {
    ((Behaviour) this.spRegistedAchievement).enabled = is_visible;
  }

  public override void SetGrayout(bool isActive)
  {
    if (!Object.op_Inequality((Object) this.spriteGrayOut, (Object) null))
      return;
    ((Behaviour) this.spriteGrayOut).enabled = isActive;
  }
}
