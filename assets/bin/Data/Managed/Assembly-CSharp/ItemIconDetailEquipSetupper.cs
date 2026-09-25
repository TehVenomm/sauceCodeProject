// Decompiled with JetBrains decompiler
// Type: ItemIconDetailEquipSetupper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ItemIconDetailEquipSetupper : ItemIconDetailSetuperBase
{
  public UISprite spRegistedAchievement;
  public UISprite spGrowMax;
  public UISprite spIsValidEvolve;
  public UISprite spEquipIndex;
  public UISprite spEquipFavorite;
  public UISprite spGrayOut;
  public GameObject lvRoot;
  public GameObject strVisualEquip;
  public UILabel lblLv;
  public UISprite spValueType;
  public UILabel lblValue;
  public UISprite spElem;
  public UILabel lblElem;
  public UISprite spSkillBG;
  public UIGrid grdSkillRoot;
  public UILabel lblNonSkillSlot;
  public GameObject objAbilityRoot;
  public UILabel lblNonAbility;
  public UISprite spSellSelectNumber;
  protected UIGrid gridEquipMark;
  private static readonly string SPR_TYPE_ATK = "EquipStatusATK_W";
  private static readonly string SPR_TYPE_DEF = "EquipStatusDEF_W";
  public static readonly string[] SPR_EQUIP_INDEX = new string[4]
  {
    "ItemIconEquipMark01",
    "ItemIconEquipMark02",
    "ItemIconEquipMark03",
    "ItemIconEquipMark"
  };

  protected override UISprite selectSP => this.spSellSelectNumber;

  public override void Set(object[] data = null)
  {
    base.Set();
    if (data == null)
      return;
    SkillSlotUIData[] slot_data = data[1] as SkillSlotUIData[];
    bool is_show_main_status = (bool) data[2];
    ItemIconDetail.ICON_STATUS icon_status = (ItemIconDetail.ICON_STATUS) data[3];
    int equipping_sp_index = (int) data[4] - 1;
    this.infoRootAry[1].SetActive(true);
    this.SetupSelectNumberSprite((int) data[5]);
    this.SetIconStatusSprite(icon_status);
    if (Object.op_Equality((Object) this.gridEquipMark, (Object) null))
      this.gridEquipMark = ((Component) this.spEquipIndex).gameObject.GetComponentInParent<UIGrid>();
    if (data[0] is EquipItemInfo)
      this.Set(data[0] as EquipItemInfo, slot_data, is_show_main_status, equipping_sp_index);
    else
      this.Set(data[0] as EquipItemTable.EquipItemData, slot_data, is_show_main_status);
  }

  protected void Set(
    EquipItemInfo item,
    SkillSlotUIData[] slot_data,
    bool is_show_main_status,
    int equipping_sp_index)
  {
    this.SetEquipIndexSprite(equipping_sp_index);
    this.SetFavorite(item.isFavorite);
    bool is_weapon = item.tableData.IsWeapon();
    if (is_weapon)
    {
      this.SetElement(item.GetTargetElement(), item.elemAtk, true);
    }
    else
    {
      int elemDef = item.elemDef;
      if (item.tableData.isFormer)
        elemDef = Mathf.FloorToInt((float) elemDef * 0.1f);
      this.SetElement(item.GetTargetElement(), elemDef, false);
    }
    if (is_show_main_status)
    {
      this.infoRootAry[2].SetActive(true);
      this.infoRootAry[3].SetActive(false);
      this.SetVisibleBG(true);
      this.SetName(item.tableData.name);
      this.SetLevel(item.level, item.tableData.maxLv, item.tableData.IsVisual());
      this.SetEquipValue(is_weapon, is_weapon ? item.atk : item.def);
    }
    else
    {
      this.infoRootAry[2].SetActive(false);
      this.infoRootAry[3].SetActive(true);
      this.SetVisibleBG(false);
      this.SetName(string.Empty);
      int length = slot_data == null || slot_data.Length == 0 ? 0 : slot_data.Length;
      bool flag1 = length > 0;
      ((Component) this.spSkillBG).gameObject.SetActive(flag1);
      ((Component) this.grdSkillRoot).gameObject.SetActive(flag1);
      ((Component) this.lblNonSkillSlot).gameObject.SetActive(!flag1);
      if (flag1)
      {
        int childCount = ((Component) this.grdSkillRoot).transform.childCount;
        for (int index = 0; index < childCount; ++index)
        {
          bool flag2 = false;
          UISprite component = ((Component) ((Component) this.grdSkillRoot).transform.GetChild(index)).GetComponent<UISprite>();
          if (index < length && Object.op_Inequality((Object) component, (Object) null) && slot_data[index] != null && slot_data[index].slotData != null)
            flag2 = true;
          if (flag2)
          {
            bool is_attached = slot_data[index].itemData != null && (slot_data[index].itemData.isAttached || slot_data[index].itemData.isUniqueAttached) && slot_data[index].itemData.tableData.type == slot_data[index].slotData.slotType;
            ((Component) component).gameObject.SetActive(true);
            component.spriteName = UIBehaviour.GetSkillIconSpriteName(slot_data[index].slotData.slotType, is_attached, false);
          }
          else
          {
            ((Component) component).gameObject.SetActive(false);
            component.spriteName = string.Empty;
          }
        }
      }
      this.grdSkillRoot.Reposition();
      bool flag3 = true;
      EquipItemAbility[] ability = item.ability;
      this.objAbilityRoot.GetComponentsInChildren<UILabel>(Temporary.uiLabelList);
      int index1 = 0;
      for (int count = Temporary.uiLabelList.Count; index1 < count; ++index1)
      {
        UILabel uiLabel = Temporary.uiLabelList[index1];
        ((Behaviour) uiLabel).enabled = index1 < ability.Length && ability[index1].id != 0U && ability[index1].ap > 0;
        if (((Behaviour) uiLabel).enabled)
        {
          uiLabel.text = ability[index1].GetNameAndAP();
          flag3 = false;
        }
      }
      Temporary.uiLabelList.Clear();
      ((Behaviour) this.lblNonAbility).enabled = flag3;
    }
  }

  protected void Set(
    EquipItemTable.EquipItemData table,
    SkillSlotUIData[] slot_data,
    bool is_show_main_status)
  {
    this.SetEquipIndexSprite(-1);
    this.SetFavorite(false);
    bool flag1 = table.IsWeapon();
    this.SetElement(table.GetTargetElement(0), flag1 ? table.baseElemAtk : table.baseElemDef, flag1);
    if (is_show_main_status)
    {
      this.infoRootAry[2].SetActive(true);
      this.infoRootAry[3].SetActive(false);
      this.SetVisibleBG(true);
      this.SetName(table.name);
      this.SetLevel(1, table.maxLv, table.IsVisual());
      this.SetEquipValue(flag1, (int) (flag1 ? table.baseAtk : table.baseDef));
    }
    else
    {
      this.infoRootAry[2].SetActive(false);
      this.infoRootAry[3].SetActive(true);
      this.SetVisibleBG(false);
      this.SetName(string.Empty);
      int length = slot_data == null || slot_data.Length == 0 ? 0 : slot_data.Length;
      bool flag2 = length > 0;
      ((Component) this.spSkillBG).gameObject.SetActive(flag2);
      ((Component) this.grdSkillRoot).gameObject.SetActive(flag2);
      ((Component) this.lblNonSkillSlot).gameObject.SetActive(!flag2);
      if (flag2)
      {
        int childCount = ((Component) this.grdSkillRoot).transform.childCount;
        for (int index = 0; index < childCount; ++index)
        {
          bool flag3 = false;
          UISprite component = ((Component) ((Component) this.grdSkillRoot).transform.GetChild(index)).GetComponent<UISprite>();
          if (index < length && Object.op_Inequality((Object) component, (Object) null) && slot_data[index] != null && slot_data[index].slotData != null)
            flag3 = true;
          if (flag3)
          {
            bool is_attached = slot_data[index].itemData != null && (slot_data[index].itemData.isAttached || slot_data[index].itemData.isUniqueAttached) && slot_data[index].itemData.tableData.type == slot_data[index].slotData.slotType;
            ((Component) component).gameObject.SetActive(true);
            component.spriteName = UIBehaviour.GetSkillIconSpriteName(slot_data[index].slotData.slotType, is_attached, false);
          }
          else
          {
            ((Component) component).gameObject.SetActive(false);
            component.spriteName = string.Empty;
          }
        }
      }
      this.grdSkillRoot.Reposition();
      bool flag4 = true;
      this.objAbilityRoot.GetComponentsInChildren<UILabel>(Temporary.uiLabelList);
      EquipItemAbility[] equipItemAbilityArray = new EquipItemAbility[Temporary.uiLabelList.Count];
      int index1 = 0;
      for (int count = Temporary.uiLabelList.Count; index1 < count; ++index1)
      {
        UILabel uiLabel = Temporary.uiLabelList[index1];
        equipItemAbilityArray[index1] = (EquipItemAbility) null;
        if (index1 < table.fixedAbility.Length)
          equipItemAbilityArray[index1] = new EquipItemAbility((uint) table.fixedAbility[index1].id, table.fixedAbility[index1].pt);
        ((Behaviour) uiLabel).enabled = equipItemAbilityArray[index1] != null && equipItemAbilityArray[index1].id != 0U && equipItemAbilityArray[index1].ap > 0;
        if (((Behaviour) uiLabel).enabled)
        {
          uiLabel.text = equipItemAbilityArray[index1].GetNameAndAP();
          flag4 = false;
        }
      }
      Temporary.uiLabelList.Clear();
      ((Behaviour) this.lblNonAbility).enabled = flag4;
    }
  }

  protected void SetLevel(int lv, int lv_max, bool is_visual_equip = false)
  {
    this.lvRoot.SetActive(!is_visual_equip);
    this.strVisualEquip.SetActive(is_visual_equip);
    if (is_visual_equip)
      return;
    this.lblLv.text = $"{lv}/{lv_max}";
  }

  protected void SetEquipValue(bool is_weapon, int value)
  {
    this.spValueType.spriteName = is_weapon ? ItemIconDetailEquipSetupper.SPR_TYPE_ATK : ItemIconDetailEquipSetupper.SPR_TYPE_DEF;
    ((Component) this.lblValue).gameObject.SetActive(true);
    this.lblValue.text = value.ToString();
  }

  protected void SetElement(ELEMENT_TYPE elem_type, int value, bool isWeapon)
  {
    ((Component) this.spElem).gameObject.SetActive(value > 0);
    if (value <= 0)
      return;
    this.spElem.spriteName = isWeapon ? UIBehaviour.GetElemSpriteName((int) elem_type) : UIBehaviour.GetElemDefSpriteName((int) elem_type);
    this.lblElem.text = value.ToString();
  }

  protected void SetIconStatusSprite(ItemIconDetail.ICON_STATUS icon_status)
  {
    this.SetRegistedIcon(false);
    switch (icon_status)
    {
      case ItemIconDetail.ICON_STATUS.NONE:
        ((Component) this.spIsValidEvolve).gameObject.SetActive(false);
        ((Behaviour) this.spGrowMax).enabled = false;
        ((Behaviour) this.spGrayOut).enabled = false;
        break;
      case ItemIconDetail.ICON_STATUS.NOT_ENOUGH_MATERIAL:
      case ItemIconDetail.ICON_STATUS.GRAYOUT:
        ((Component) this.spIsValidEvolve).gameObject.SetActive(false);
        ((Behaviour) this.spGrowMax).enabled = false;
        ((Behaviour) this.spGrayOut).enabled = true;
        break;
      case ItemIconDetail.ICON_STATUS.VALID_EVOLVE:
        ((Component) this.spIsValidEvolve).gameObject.SetActive(true);
        ((Behaviour) this.spGrowMax).enabled = false;
        ((Behaviour) this.spGrayOut).enabled = false;
        break;
      case ItemIconDetail.ICON_STATUS.GROW_MAX:
        ((Component) this.spIsValidEvolve).gameObject.SetActive(false);
        ((Behaviour) this.spGrowMax).enabled = true;
        ((Behaviour) this.spGrayOut).enabled = false;
        break;
    }
  }

  protected void SetEquipIndexSprite(int index)
  {
    if (index < 0 || ItemIconDetailEquipSetupper.SPR_EQUIP_INDEX.Length <= index)
      this.spEquipIndex.spriteName = string.Empty;
    else
      this.spEquipIndex.spriteName = ItemIconDetailEquipSetupper.SPR_EQUIP_INDEX[index];
  }

  protected void SetFavorite(bool is_favorite)
  {
    ((Component) this.spEquipFavorite).gameObject.SetActive(is_favorite);
    if (!Object.op_Inequality((Object) this.gridEquipMark, (Object) null))
      return;
    this.gridEquipMark.Reposition();
  }

  public void SetRegistedIcon(bool is_visible)
  {
    ((Behaviour) this.spRegistedAchievement).enabled = is_visible;
  }
}
