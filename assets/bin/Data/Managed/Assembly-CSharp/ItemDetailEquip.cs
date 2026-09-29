// Decompiled with JetBrains decompiler
// Type: ItemDetailEquip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class ItemDetailEquip : SkillInfoBase
{
  private AbilityDetailPopUp abilityDetailPopUp;
  private List<Transform> touchAndReleaseButtons = new List<Transform>();
  protected Transform detailBase;
  private object eventData;
  protected ItemDetailEquip.CURRENT_SECTION callSection;
  protected object detailItemData;
  private SkillSlotUIData[] equipAttachSkill;
  protected int sex = -1;
  protected int faceID = -1;
  protected StatusEquip.LocalEquipSetData localEquipSetData;
  protected object[] gameEventData;

  protected virtual bool IsShowFrameBG() => false;

  public override void Initialize()
  {
    this.gameEventData = GameSection.GetEventData() as object[];
    this.callSection = (ItemDetailEquip.CURRENT_SECTION) this.gameEventData[0];
    this.eventData = this.gameEventData[1];
    this.localEquipSetData = this.gameEventData.OfType<StatusEquip.LocalEquipSetData>().FirstOrDefault<StatusEquip.LocalEquipSetData>();
    switch (this.callSection)
    {
      case ItemDetailEquip.CURRENT_SECTION.STATUS_TOP:
      case ItemDetailEquip.CURRENT_SECTION.STATUS_EQUIP:
      case ItemDetailEquip.CURRENT_SECTION.STATUS_AVATAR:
      case ItemDetailEquip.CURRENT_SECTION.EQUIP_SET_DETAIL_STATUS:
        if (this.eventData is EquipItemInfo eventData1)
        {
          this.detailItemData = (object) eventData1;
          this.equipAttachSkill = this.GetSkillSlotData(this.detailItemData as EquipItemInfo);
          break;
        }
        break;
      case ItemDetailEquip.CURRENT_SECTION.ITEM_STORAGE:
      case ItemDetailEquip.CURRENT_SECTION.SMITH_SELL:
        if (this.eventData is SortCompareData eventData2)
        {
          EquipItemInfo itemData = eventData2.GetItemData() as EquipItemInfo;
          this.detailItemData = (object) itemData;
          this.equipAttachSkill = this.GetSkillSlotData(itemData);
          MonoBehaviourSingleton<StatusManager>.I.SetSelectEquipItem(itemData);
          break;
        }
        break;
      case ItemDetailEquip.CURRENT_SECTION.SMITH_CREATE:
      case ItemDetailEquip.CURRENT_SECTION.GACHA_EQUIP_PREVIEW:
        this.detailItemData = this.eventData;
        this.equipAttachSkill = this.GetSkillSlotData(this.detailItemData as EquipItemTable.EquipItemData, 0);
        break;
      case ItemDetailEquip.CURRENT_SECTION.SMITH_EVOLVE:
      case ItemDetailEquip.CURRENT_SECTION.SMITH_GROW:
        if (this.eventData is EquipItemInfo eventData3)
        {
          this.detailItemData = (object) eventData3;
          this.equipAttachSkill = this.GetSkillSlotData(this.detailItemData as EquipItemInfo);
          break;
        }
        break;
      case ItemDetailEquip.CURRENT_SECTION.QUEST_ROOM:
        if (this.eventData is EquipItemInfo eventData4)
        {
          this.detailItemData = (object) eventData4;
          this.equipAttachSkill = this.GetSkillSlotData(this.detailItemData as EquipItemInfo);
        }
        if (this.gameEventData.Length > 2)
        {
          this.sex = (int) this.gameEventData[2];
          this.faceID = (int) this.gameEventData[3];
          break;
        }
        break;
      case ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT:
        if (this.eventData is EquipItemAndSkillData eventData5)
        {
          this.detailItemData = (object) eventData5.equipItemInfo;
          this.equipAttachSkill = eventData5.skillSlotUIData;
        }
        if (this.gameEventData.Length > 2)
        {
          this.sex = (int) this.gameEventData[2];
          this.faceID = (int) this.gameEventData[3];
          break;
        }
        break;
      case ItemDetailEquip.CURRENT_SECTION.EQUIP_LIST:
        this.detailItemData = this.eventData;
        this.equipAttachSkill = this.GetSkillSlotData(this.detailItemData as EquipItemTable.EquipItemData, 0);
        for (int index = 0; index < this.equipAttachSkill.Length; ++index)
          this.equipAttachSkill[index].slotData.skill_id = 0U;
        break;
    }
    if (this.detailItemData != null && this.detailItemData is EquipItemInfo detailItemData1)
      GameSaveData.instance.RemoveNewIconAndSave(ItemIcon.GetItemIconType(detailItemData1.tableData.type), detailItemData1.uniqueID);
    if (this.sex == -1)
      this.sex = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
    Transform ctrl = this.GetCtrl((Enum) ItemDetailEquip.UI.BTN_GRAPH);
    if (Object.op_Inequality((Object) ctrl, (Object) null))
    {
      int num = -1;
      if (this.detailItemData is EquipItemInfo detailItemData3)
        num = detailItemData3.tableData.damageDistanceId;
      else if (this.detailItemData is EquipItemTable.EquipItemData detailItemData2)
        num = detailItemData2.damageDistanceId;
      bool flag = num >= 0;
      ((Component) ctrl).gameObject.SetActive(flag);
    }
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) ItemDetailEquip.UI.OBJ_FRAME_BG, this.IsShowFrameBG());
    this.detailBase = this.SetPrefab(this.GetCtrl((Enum) ItemDetailEquip.UI.OBJ_DETAIL_ROOT), "ItemDetailEquipBase");
    this.SetFontStyle(this.detailBase, (Enum) ItemDetailEquip.UI.STR_TITLE_ITEM_INFO, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) ItemDetailEquip.UI.STR_TITLE_STATUS, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) ItemDetailEquip.UI.STR_TITLE_SKILL_SLOT, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) ItemDetailEquip.UI.STR_TITLE_ABILITY, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) ItemDetailEquip.UI.STR_TITLE_SELL, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) ItemDetailEquip.UI.STR_TITLE_ATK, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) ItemDetailEquip.UI.STR_TITLE_ELEM_ATK, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) ItemDetailEquip.UI.STR_TITLE_DEF, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) ItemDetailEquip.UI.STR_TITLE_ELEM_DEF, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) ItemDetailEquip.UI.STR_TITLE_HP, (FontStyle) 2);
    EquipItemInfo equip = this.detailItemData as EquipItemInfo;
    this.SetActive((Enum) ItemDetailEquip.UI.BTN_CHANGE, this.localEquipSetData != null && ItemDetailEquip.CanSmithSection(this.callSection));
    this.SetActive((Enum) ItemDetailEquip.UI.BTN_CREATE, ItemDetailEquip.CanSmithSection(this.callSection));
    this.SetActive((Enum) ItemDetailEquip.UI.BTN_GROW, ItemDetailEquip.CanSmithSection(this.callSection));
    this.SetActive((Enum) ItemDetailEquip.UI.BTN_GROW_OFF, ItemDetailEquip.CanSmithSection(this.callSection));
    this.SetActive((Enum) ItemDetailEquip.UI.BTN_ABILITY, ItemDetailEquip.CanSmithSection(this.callSection));
    this.SetActive((Enum) ItemDetailEquip.UI.BTN_ABILITY_OFF, ItemDetailEquip.CanSmithSection(this.callSection));
    this.SetActive((Enum) ItemDetailEquip.UI.BTN_SELL, ItemDetailEquip.IsEnableDispSellButton(this.callSection) && MonoBehaviourSingleton<ItemExchangeManager>.I.IsExchangeScene());
    if (equip != null)
    {
      int exceed = equip.exceed;
      this.SetActive((Enum) ItemDetailEquip.UI.BTN_EXCEED, equip.tableData.exceedID > 0U);
      this.SetActive((Enum) ItemDetailEquip.UI.SPR_COUNT_0_ON, exceed > 0);
      this.SetActive((Enum) ItemDetailEquip.UI.SPR_COUNT_1_ON, exceed > 1);
      this.SetActive((Enum) ItemDetailEquip.UI.SPR_COUNT_2_ON, exceed > 2);
      this.SetActive((Enum) ItemDetailEquip.UI.SPR_COUNT_3_ON, exceed > 3);
      this.EquipParam(equip);
      this.SetSkillIconButton(this.detailBase, (Enum) ItemDetailEquip.UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButton", equip.tableData, this.equipAttachSkill);
      this.SetSprite((Enum) ItemDetailEquip.UI.SPR_SP_ATTACK_TYPE, equip.tableData.IsWeapon() ? equip.tableData.spAttackType.GetBigFrameSpriteName() : "");
      AbilityItemInfo abilityItem = equip.GetAbilityItem();
      bool flag = abilityItem != null;
      if (((equip.ability == null ? 0 : (equip.ability.Length != 0 ? 1 : 0)) | (flag ? 1 : 0)) != 0)
      {
        bool empty_ability = true;
        int validAbilityLength = equip.GetValidAbilityLength();
        string allAbilityName = "";
        string allAp = "";
        string allAbilityDesc = "";
        this.SetTable(this.detailBase, (Enum) ItemDetailEquip.UI.TBL_ABILITY, "ItemDetailEquipAbilityItem", equip.ability.Length + (flag ? 1 : 0), false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
        {
          if (i < equip.ability.Length)
          {
            EquipItemAbility equipItemAbility = equip.ability[i];
            if (equipItemAbility.id == 0U)
            {
              this.SetActive(t, false);
            }
            else
            {
              this.SetActive(t, true);
              if (equipItemAbility.IsNeedUpdate())
              {
                this.SetActive(t, (Enum) ItemDetailEquip.UI.OBJ_ABILITY, false);
                this.SetActive(t, (Enum) ItemDetailEquip.UI.OBJ_FIXEDABILITY, false);
                this.SetActive(t, (Enum) ItemDetailEquip.UI.OBJ_NEED_UPDATE_ABILITY, true);
                this.SetLabelText(t, (Enum) ItemDetailEquip.UI.LBL_NEED_UPDATE_ABILITY, StringTable.Get(STRING_CATEGORY.ABILITY, 0U));
                this.SetButtonEnabled(t, false);
              }
              else if (!equipItemAbility.IsActiveAbility())
              {
                this.SetActive(t, (Enum) ItemDetailEquip.UI.OBJ_ABILITY, false);
                this.SetActive(t, (Enum) ItemDetailEquip.UI.OBJ_FIXEDABILITY, false);
                this.SetActive(t, (Enum) ItemDetailEquip.UI.OBJ_NEED_UPDATE_ABILITY, true);
                this.SetLabelText(t, (Enum) ItemDetailEquip.UI.LBL_NEED_UPDATE_ABILITY, StringTable.Get(STRING_CATEGORY.ABILITY, 1U));
                this.SetButtonEnabled(t, false);
              }
              else if (equip.IsFixedAbility(i))
              {
                this.SetActive(t, (Enum) ItemDetailEquip.UI.OBJ_ABILITY, false);
                this.SetActive(t, (Enum) ItemDetailEquip.UI.OBJ_FIXEDABILITY, true);
                this.SetActive(t, (Enum) ItemDetailEquip.UI.OBJ_NEED_UPDATE_ABILITY, false);
                this.SetLabelText(t, (Enum) ItemDetailEquip.UI.LBL_FIXEDABILITY, Utility.TrimText(equipItemAbility.GetName(), ((Component) this.FindCtrl(t, (Enum) ItemDetailEquip.UI.LBL_FIXEDABILITY)).GetComponent<UILabel>()));
                this.SetLabelText(t, (Enum) ItemDetailEquip.UI.LBL_FIXEDABILITY_NUM, equipItemAbility.GetAP());
              }
              else
              {
                empty_ability = false;
                this.SetActive(t, (Enum) ItemDetailEquip.UI.OBJ_NEED_UPDATE_ABILITY, false);
                this.SetLabelText(t, (Enum) ItemDetailEquip.UI.LBL_ABILITY, Utility.TrimText(equipItemAbility.GetName(), ((Component) this.FindCtrl(t, (Enum) ItemDetailEquip.UI.LBL_ABILITY)).GetComponent<UILabel>()));
                this.SetLabelText(t, (Enum) ItemDetailEquip.UI.LBL_ABILITY_NUM, equipItemAbility.GetAP());
              }
              this.SetAbilityItemEvent(t, i, this.touchAndReleaseButtons);
              allAbilityName += equipItemAbility.GetName();
              allAp += equipItemAbility.GetAP();
              allAbilityDesc += equipItemAbility.GetDescription();
            }
          }
          else
          {
            this.SetActive(t, (Enum) ItemDetailEquip.UI.OBJ_ABILITY, false);
            this.SetActive(t, (Enum) ItemDetailEquip.UI.OBJ_ABILITY_ITEM, true);
            this.SetLabelText(t, (Enum) ItemDetailEquip.UI.LBL_ABILITY_ITEM, abilityItem.GetName());
            this.SetTouchAndRelease(((Component) ((Component) t).GetComponentInChildren<UIButton>()).transform, "ABILITY_ITEM_DATA_POPUP", "RELEASE_ABILITY", (object) t);
            allAbilityName += abilityItem.GetName();
            allAbilityDesc += abilityItem.GetDescription();
          }
        }));
        this.PreCacheAbilityDetail(allAbilityName, allAp, allAbilityDesc);
        if (empty_ability)
        {
          this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.STR_NON_ABILITY, validAbilityLength == 0);
          this.SetActive((Enum) ItemDetailEquip.UI.BTN_ABILITY, false);
          this.SetActive((Enum) ItemDetailEquip.UI.BTN_ABILITY_OFF, ItemDetailEquip.CanSmithSection(this.callSection));
        }
        else
        {
          this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.STR_NON_ABILITY, false);
          this.SetActive((Enum) ItemDetailEquip.UI.BTN_ABILITY_OFF, false);
        }
        if (!equip.tableData.IsShadow())
          return;
        this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.STR_NON_ABILITY, false);
        this.SetActive((Enum) ItemDetailEquip.UI.BTN_ABILITY, ItemDetailEquip.CanSmithSection(this.callSection));
        this.SetActive((Enum) ItemDetailEquip.UI.BTN_ABILITY_OFF, ItemDetailEquip.CanSmithSection(this.callSection));
      }
      else
      {
        this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.STR_NON_ABILITY, true);
        this.SetActive((Enum) ItemDetailEquip.UI.BTN_ABILITY, false);
        this.SetActive((Enum) ItemDetailEquip.UI.BTN_ABILITY_OFF, ItemDetailEquip.CanSmithSection(this.callSection));
      }
    }
    else
    {
      this.SetActive((Enum) ItemDetailEquip.UI.SPR_COUNT_0_ON, false);
      this.SetActive((Enum) ItemDetailEquip.UI.SPR_COUNT_1_ON, false);
      this.SetActive((Enum) ItemDetailEquip.UI.SPR_COUNT_2_ON, false);
      this.SetActive((Enum) ItemDetailEquip.UI.SPR_COUNT_3_ON, false);
      EquipItemTable.EquipItemData table = this.detailItemData as EquipItemTable.EquipItemData;
      this.SetActive((Enum) ItemDetailEquip.UI.BTN_EXCEED, table.exceedID > 0U);
      this.EquipTableParam(table);
      EquipItemTable.EquipItemData detailItemData = this.detailItemData as EquipItemTable.EquipItemData;
      if (detailItemData.id == 81160110U || detailItemData.id == 82160110U || detailItemData.id == 83160110U || detailItemData.id == 84160110U || detailItemData.id == 21160111U || detailItemData.id == 22160111U || detailItemData.id == 23160111U || detailItemData.id == 24160111U)
      {
        SkillSlotUIData[] skillSlotData = this.GetSkillSlotData(this.detailItemData as EquipItemTable.EquipItemData, 0);
        this.SetSkillIconButton(this.detailBase, (Enum) ItemDetailEquip.UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButton", table, skillSlotData);
      }
      else
        this.SetSkillIconButton(this.detailBase, (Enum) ItemDetailEquip.UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButton", table, this.equipAttachSkill);
      this.SetSprite((Enum) ItemDetailEquip.UI.SPR_SP_ATTACK_TYPE, table.IsWeapon() ? table.spAttackType.GetBigFrameSpriteName() : "");
      if (table.fixedAbility.Length != 0)
      {
        string allAbilityName = "";
        string allAp = "";
        string allAbilityDesc = "";
        this.SetTable(this.detailBase, (Enum) ItemDetailEquip.UI.TBL_ABILITY, "ItemDetailEquipAbilityItem", table.fixedAbility.Length, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
        {
          EquipItemAbility equipItemAbility = new EquipItemAbility((uint) table.fixedAbility[i].id, table.fixedAbility[i].pt);
          this.SetActive(t, true);
          this.SetActive(t, (Enum) ItemDetailEquip.UI.OBJ_ABILITY, false);
          this.SetActive(t, (Enum) ItemDetailEquip.UI.OBJ_FIXEDABILITY, true);
          this.SetLabelText(t, (Enum) ItemDetailEquip.UI.LBL_FIXEDABILITY, Utility.TrimText(equipItemAbility.GetName(), ((Component) this.FindCtrl(t, (Enum) ItemDetailEquip.UI.LBL_FIXEDABILITY)).GetComponent<UILabel>()));
          this.SetLabelText(t, (Enum) ItemDetailEquip.UI.LBL_FIXEDABILITY_NUM, equipItemAbility.GetAP());
          this.SetAbilityItemEvent(t, i, this.touchAndReleaseButtons);
          allAbilityName += equipItemAbility.GetName();
          allAp += equipItemAbility.GetAP();
          allAbilityDesc += equipItemAbility.GetDescription();
        }));
        this.PreCacheAbilityDetail(allAbilityName, allAp, allAbilityDesc);
        this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.STR_NON_ABILITY, false);
      }
      else
        this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.STR_NON_ABILITY, true);
    }
  }

  private void EquipParam(EquipItemInfo item)
  {
    EquipItemTable.EquipItemData tableData = item?.tableData;
    if (item != null && tableData != null)
    {
      bool is_visible = item.tableData.IsVisual();
      this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_NAME, tableData.name);
      this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.STR_LV, !is_visible);
      this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.STR_ONLY_VISUAL, is_visible);
      this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_LV_NOW, item.level.ToString());
      this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_LV_MAX, tableData.maxLv.ToString());
      this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_ATK, item.atk.ToString());
      this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_ELEM, item.elemAtk.ToString());
      this.SetElementSprite(this.detailBase, (Enum) ItemDetailEquip.UI.SPR_ELEM, item.GetElemAtkType());
      this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_DEF, item.def.ToString());
      int elemDef = item.elemDef;
      if (tableData.isFormer)
        elemDef = Mathf.FloorToInt((float) elemDef * 0.1f);
      this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_ELEM_DEF, elemDef.ToString());
      this.SetDefElementSprite(this.detailBase, (Enum) ItemDetailEquip.UI.SPR_ELEM_DEF, item.GetElemDefType());
      this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_HP, item.hp.ToString());
      this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_SELL, item.sellPrice.ToString());
      this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.OBJ_FAVORITE_ROOT, (this.callSection & (ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT | ItemDetailEquip.CURRENT_SECTION.EQUIP_LIST)) == ItemDetailEquip.CURRENT_SECTION.NONE);
      this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.SPR_IS_EVOLVE, item.tableData.IsEvolve());
      this.SetEquipmentTypeIcon(this.detailBase, (Enum) ItemDetailEquip.UI.SPR_TYPE_ICON, (Enum) ItemDetailEquip.UI.SPR_TYPE_ICON_BG, (Enum) ItemDetailEquip.UI.SPR_TYPE_ICON_RARITY, item.tableData);
      this.SetRenderEquipModel((Enum) ItemDetailEquip.UI.TEX_MODEL, tableData.id, this.sex, this.faceID);
      this.ResetTween(this.detailBase, (Enum) ItemDetailEquip.UI.TWN_FAVORITE);
      this.ResetTween(this.detailBase, (Enum) ItemDetailEquip.UI.TWN_UNFAVORITE);
      this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.TWN_UNFAVORITE, !item.isFavorite);
      this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.TWN_FAVORITE, item.isFavorite);
      bool flag = !item.IsLevelMax() || !item.IsExceedMax() || item.tableData.IsEvolve() || item.tableData.IsShadow();
      this.SetActive((Enum) ItemDetailEquip.UI.BTN_GROW, flag && ItemDetailEquip.CanSmithSection(this.callSection));
      this.SetActive((Enum) ItemDetailEquip.UI.BTN_GROW_OFF, !flag && ItemDetailEquip.CanSmithSection(this.callSection));
    }
    else
      this.NotDataEquipParam();
  }

  protected virtual void EquipTableParam(EquipItemTable.EquipItemData table_data)
  {
    if (table_data != null)
    {
      bool is_visible = table_data.IsVisual();
      int baseAtk = (int) table_data.baseAtk;
      int baseDef = (int) table_data.baseDef;
      int baseElemAtk = table_data.baseElemAtk != 0 ? table_data.baseElemAtk : 0;
      int baseElemDef = table_data.baseElemDef != 0 ? table_data.baseElemDef : 0;
      int baseHp = (int) table_data.baseHp;
      this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_NAME, table_data.name);
      this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.STR_LV, !is_visible);
      this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.STR_ONLY_VISUAL, is_visible);
      this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_LV_NOW, "1");
      this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_LV_MAX, table_data.maxLv.ToString());
      this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_ATK, baseAtk.ToString());
      this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_ELEM, baseElemAtk.ToString());
      this.SetElementSprite(this.detailBase, (Enum) ItemDetailEquip.UI.SPR_ELEM, table_data.GetElemAtkType());
      this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_DEF, baseDef.ToString());
      this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_ELEM_DEF, baseElemDef.ToString());
      this.SetDefElementSprite(this.detailBase, (Enum) ItemDetailEquip.UI.SPR_ELEM_DEF, table_data.GetElemDefType());
      this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_HP, baseHp.ToString());
      this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_SELL, table_data.sale.ToString());
      this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.OBJ_FAVORITE_ROOT, false);
      this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.SPR_IS_EVOLVE, table_data.IsEvolve());
      this.SetEquipmentTypeIcon(this.detailBase, (Enum) ItemDetailEquip.UI.SPR_TYPE_ICON, (Enum) ItemDetailEquip.UI.SPR_TYPE_ICON_BG, (Enum) ItemDetailEquip.UI.SPR_TYPE_ICON_RARITY, table_data);
      this.SetRenderEquipModel((Enum) ItemDetailEquip.UI.TEX_MODEL, table_data.id, this.sex, this.faceID);
    }
    else
      this.NotDataEquipParam();
  }

  private void NotDataEquipParam()
  {
    Log.Error("ItemDetailEquip is Not Item Data");
    string text = "----";
    this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_NAME, text);
    this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_LV_NOW, text);
    this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_LV_MAX, text);
    this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_ATK, text);
    this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_DEF, text);
    this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_ELEM, text);
    this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.OBJ_ATK_ROOT, false);
    this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.OBJ_DEF_ROOT, false);
    this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.OBJ_ELEM_ROOT, false);
    this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.SPR_IS_EVOLVE, false);
  }

  protected void OnQuery_SWITCH_FAVORITE()
  {
    this.OnQueryFavorite(this.detailItemData as EquipItemInfo, (Action<EquipItemInfo>) (item =>
    {
      this.detailItemData = (object) item;
      this.eventData = (object) item;
      this.gameEventData[1] = (object) item;
    }));
  }

  protected void OnQueryFavorite(EquipItemInfo select_item, Action<EquipItemInfo> callback)
  {
    if (select_item == null)
      return;
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.SendInventoryEquipLock(select_item.uniqueID, (Action<bool, EquipItemInfo>) ((is_success, recv_equip_item) =>
    {
      if (is_success)
      {
        callback(recv_equip_item);
        if (recv_equip_item.isFavorite)
        {
          this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.TWN_UNFAVORITE, false);
          this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.TWN_FAVORITE, true);
          this.ResetTween(this.detailBase, (Enum) ItemDetailEquip.UI.TWN_FAVORITE);
          this.PlayTween(this.detailBase, (Enum) ItemDetailEquip.UI.TWN_FAVORITE, callback: (EventDelegate.Callback) (() =>
          {
            GameSection.ChangeStayEvent("FAVORITE");
            GameSection.ResumeEvent(is_success);
          }));
        }
        else
        {
          this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.TWN_FAVORITE, false);
          this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.TWN_UNFAVORITE, true);
          this.ResetTween(this.detailBase, (Enum) ItemDetailEquip.UI.TWN_UNFAVORITE);
          this.PlayTween(this.detailBase, (Enum) ItemDetailEquip.UI.TWN_UNFAVORITE, callback: (EventDelegate.Callback) (() =>
          {
            GameSection.ChangeStayEvent("RELEASE_FAVORITE");
            GameSection.ResumeEvent(is_success);
          }));
        }
      }
      else
        GameSection.ResumeEvent(is_success);
    }));
  }

  protected virtual void OnQuery_ABILITY()
  {
    switch (this.callSection)
    {
      case ItemDetailEquip.CURRENT_SECTION.STATUS_TOP:
      case ItemDetailEquip.CURRENT_SECTION.STATUS_SKILL_LIST:
      case ItemDetailEquip.CURRENT_SECTION.STATUS_EQUIP:
      case ItemDetailEquip.CURRENT_SECTION.STATUS_EQUIP_SKILL:
      case ItemDetailEquip.CURRENT_SECTION.ITEM_STORAGE:
      case ItemDetailEquip.CURRENT_SECTION.EQUIP_SET_DETAIL_STATUS:
        if ((!(this.detailItemData is EquipItemInfo detailItemData) ? 0 : (MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(detailItemData.uniqueID) != null ? 1 : 0)) == 0)
        {
          GameSection.StopEvent();
          break;
        }
        MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>().selectEquipData = detailItemData;
        break;
      default:
        GameSection.StopEvent();
        break;
    }
  }

  protected void OnQuery_SELL()
  {
    if (!(this.detailItemData is EquipItemInfo detailItemData))
    {
      GameSection.StopEvent();
    }
    else
    {
      EquipItemSortData equipItemSortData = new EquipItemSortData();
      equipItemSortData.SetItem((object) detailItemData);
      if (!equipItemSortData.CanSale())
      {
        if (equipItemSortData.IsFavorite())
          GameSection.ChangeEvent("NOT_SALE_FAVORITE");
        else if (equipItemSortData.IsHomeEquipping())
          GameSection.ChangeEvent("NOT_SALE_EQUIPPING");
        else
          GameSection.ChangeEvent("NOT_SALE_UNIQUE_EQUIPPING");
      }
      else if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP) && equipItemSortData.GetTableID() == 10000000U)
        GameSection.ChangeEvent("NOT_SELL_DEFAULT_WEAPON");
      else
        GameSection.ChangeEvent("SELL", (object) new object[2]
        {
          (object) ItemStorageTop.TAB_MODE.EQUIP,
          (object) new List<SortCompareData>()
          {
            (SortCompareData) equipItemSortData
          }
        });
    }
  }

  protected void OnQuery_EXCEED()
  {
    int num = 0;
    EquipItemTable.EquipItemData equipItemData;
    if (!(this.detailItemData is EquipItemInfo detailItemData))
    {
      equipItemData = this.detailItemData as EquipItemTable.EquipItemData;
    }
    else
    {
      equipItemData = detailItemData.tableData;
      num = detailItemData.exceed;
    }
    GameSection.SetEventData((object) new object[2]
    {
      (object) equipItemData,
      (object) num
    });
  }

  protected virtual void OnQuery_SKILL_ICON_BUTTON()
  {
    GameSection.SetEventData((object) new object[3]
    {
      (object) this.callSection,
      this.callSection != ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT ? this.detailItemData : this.eventData,
      (object) this.sex
    });
  }

  protected void OnQuery_CHANGE()
  {
    if (this.localEquipSetData == null)
    {
      GameSection.StopEvent();
    }
    else
    {
      if (StatusManager.IsUnique())
        GameSection.ChangeEvent("UNIQUE_CHANGE");
      MonoBehaviourSingleton<StatusManager>.I.SetEquippingItem(this.localEquipSetData.equipSetInfo.item[this.localEquipSetData.index]);
      MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = StatusTop.GetInventoryType(this.localEquipSetData.equipSetInfo, this.localEquipSetData.index);
      if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SHADOW_QUEST_WIN))
      {
        List<EquipItemInfo> weaponInventory = MonoBehaviourSingleton<InventoryManager>.I.GetWeaponInventory();
        for (int index = 0; index < weaponInventory.Count; ++index)
        {
          if (weaponInventory[index].tableData.rarity >= RARITY_TYPE.S)
          {
            MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = (InventoryManager.INVENTORY_TYPE) (UIBehaviour.GetEquipmentTypeIndex(weaponInventory[index].tableData.type) + 1);
            break;
          }
        }
      }
      GameSection.SetEventData((object) new ItemDetailEquip.DetailEquipEventData(this.gameEventData, this.localEquipSetData));
    }
  }

  protected void OnQuery_GROW()
  {
    if (MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>() == null)
      MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>();
    EquipItemInfo detailItemData = this.detailItemData as EquipItemInfo;
    MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData = detailItemData;
    if (detailItemData.IsLevelMax() && detailItemData.tableData.IsEvolve())
    {
      GameSection.SetEventData((object) new object[2]
      {
        (object) SmithEquipBase.SmithType.EVOLVE,
        (object) (this.detailItemData as EquipItemInfo).tableData.type
      });
      GameSection.ChangeEvent("EVOLVE");
    }
    else
      GameSection.SetEventData((object) new object[2]
      {
        (object) SmithEquipBase.SmithType.GROW,
        (object) (this.detailItemData as EquipItemInfo).tableData.type
      });
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE) != (GameSection.NOTIFY_FLAG) 0 && this.detailItemData is EquipItemInfo detailItemData)
      this.equipAttachSkill = this.GetSkillSlotData(MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(detailItemData.uniqueID));
    if ((flags & GameSection.NOTIFY_FLAG.PRETREAT_SCENE) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.NoEventReleaseTouchAndReleases(this.touchAndReleaseButtons);
      this.OnQuery_RELEASE_ABILITY();
    }
    base.OnNotify(flags);
  }

  private void OnQuery_CREATE()
  {
    GameSection.SetEventData((object) (this.detailItemData as EquipItemInfo).tableData.type);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE | GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE;
  }

  public static bool CanSmithSection(ItemDetailEquip.CURRENT_SECTION section)
  {
    switch (section)
    {
      case ItemDetailEquip.CURRENT_SECTION.STATUS_TOP:
      case ItemDetailEquip.CURRENT_SECTION.STATUS_SKILL_LIST:
      case ItemDetailEquip.CURRENT_SECTION.STATUS_EQUIP:
      case ItemDetailEquip.CURRENT_SECTION.STATUS_EQUIP_SKILL:
      case ItemDetailEquip.CURRENT_SECTION.ITEM_STORAGE:
      case ItemDetailEquip.CURRENT_SECTION.EQUIP_SET_DETAIL_STATUS:
        return true;
      default:
        return false;
    }
  }

  public static bool IsEnableDispSellButton(ItemDetailEquip.CURRENT_SECTION _section)
  {
    return _section != ItemDetailEquip.CURRENT_SECTION.SMITH_SELL;
  }

  protected void OnQuery_DISTANCE_GRAPH()
  {
    int num = -1;
    if (this.detailItemData is EquipItemInfo detailItemData2)
      num = detailItemData2.tableData.damageDistanceId;
    else if (this.detailItemData is EquipItemTable.EquipItemData detailItemData1)
      num = detailItemData1.damageDistanceId;
    GameSection.SetEventData((object) new object[1]
    {
      (object) num
    });
  }

  protected void OnQuery_RELEASE_ABILITY()
  {
    if (Object.op_Equality((Object) this.abilityDetailPopUp, (Object) null))
      return;
    this.abilityDetailPopUp.Hide();
    GameSection.StopEvent();
  }

  protected void OnQuery_ABILITY_DATA_POPUP()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    int index = (int) eventData[0];
    Transform targetTrans = eventData[1] as Transform;
    EquipItemAbility ability1 = (EquipItemAbility) null;
    if (this.detailItemData is EquipItemInfo)
      ability1 = (this.detailItemData as EquipItemInfo).ability[index];
    else if (this.detailItemData is EquipItemTable.EquipItemData)
    {
      EquipItem.Ability ability2 = (this.detailItemData as EquipItemTable.EquipItemData).fixedAbility[index];
      ability1 = new EquipItemAbility((uint) ability2.id, ability2.pt);
    }
    if (ability1 == null)
      return;
    if (Object.op_Equality((Object) this.abilityDetailPopUp, (Object) null))
      this.abilityDetailPopUp = this.CreateAndGetAbilityDetail((Enum) ItemDetailEquip.UI.OBJ_DETAIL_ROOT);
    this.abilityDetailPopUp.ShowAbilityDetail(targetTrans);
    this.abilityDetailPopUp.SetAbilityDetailText(ability1);
    GameSection.StopEvent();
  }

  protected void OnQuery_ABILITY_ITEM_DATA_POPUP()
  {
    Transform eventData = GameSection.GetEventData() as Transform;
    if (!(this.detailItemData is EquipItemInfo))
      return;
    if (Object.op_Equality((Object) this.abilityDetailPopUp, (Object) null))
      this.abilityDetailPopUp = this.CreateAndGetAbilityDetail((Enum) ItemDetailEquip.UI.OBJ_DETAIL_ROOT);
    this.abilityDetailPopUp.ShowAbilityDetail(eventData);
    AbilityItemInfo abilityItem = (this.detailItemData as EquipItemInfo).GetAbilityItem();
    this.abilityDetailPopUp.SetAbilityDetailText(abilityItem.GetName(), "", abilityItem.GetDescription());
  }

  private void PreCacheAbilityDetail(string name, string ap, string desc)
  {
    if (Object.op_Equality((Object) this.abilityDetailPopUp, (Object) null))
      this.abilityDetailPopUp = this.CreateAndGetAbilityDetail((Enum) ItemDetailEquip.UI.OBJ_DETAIL_ROOT);
    this.abilityDetailPopUp.PreCacheAbilityDetail(name, ap, desc);
  }

  protected enum UI
  {
    OBJ_DETAIL_ROOT,
    TEX_MODEL,
    OBJ_FRAME_BG,
    STR_LV,
    LBL_NAME,
    LBL_LV_NOW,
    LBL_LV_MAX,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    LBL_ELEM,
    LBL_ELEM_DEF,
    SPR_ELEM,
    SPR_ELEM_DEF,
    LBL_SELL,
    OBJ_SKILL_BUTTON_ROOT,
    SPR_IS_EVOLVE,
    OBJ_FAVORITE_ROOT,
    TWN_FAVORITE,
    TWN_UNFAVORITE,
    OBJ_ATK_ROOT,
    OBJ_DEF_ROOT,
    OBJ_ELEM_ROOT,
    STR_ONLY_VISUAL,
    SPR_TYPE_ICON,
    SPR_TYPE_ICON_BG,
    SPR_TYPE_ICON_RARITY,
    STR_TITLE_ITEM_INFO,
    STR_TITLE_STATUS,
    STR_TITLE_SKILL_SLOT,
    STR_TITLE_ABILITY,
    STR_TITLE_SELL,
    STR_TITLE_ATK,
    STR_TITLE_DEF,
    STR_TITLE_HP,
    STR_TITLE_ELEM_ATK,
    STR_TITLE_ELEM_DEF,
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
    BTN_SELL,
    BTN_EXCEED,
    SPR_COUNT_0_ON,
    SPR_COUNT_1_ON,
    SPR_COUNT_2_ON,
    SPR_COUNT_3_ON,
    BTN_CHANGE,
    BTN_CREATE,
    BTN_GROW,
    BTN_ABILITY,
    BTN_GROW_OFF,
    BTN_ABILITY_OFF,
    STR_BTN_CHANGE,
    STR_BTN_CREATE,
    STR_BTN_GROW,
    STR_BTN_ABILITY,
    STR_BTN_CHANGE_D,
    STR_BTN_CREATE_D,
    STR_BTN_GROW_D,
    STR_BTN_ABILITY_D,
    OBJ_NEED_UPDATE_ABILITY,
    LBL_NEED_UPDATE_ABILITY,
    BTN_GRAPH,
    SPR_SP_ATTACK_TYPE,
    OBJ_EVOLVE_SELECT,
    LBL_EVOLVE_NORMAL,
    SPR_EVOLVE_ELEM,
    LBL_EVOLVE_ATTRIBUTE,
    OBJ_ARROW_BTN_ROOT,
  }

  public class DetailEquipEventData
  {
    public object[] currentEventData { get; protected set; }

    public StatusEquip.LocalEquipSetData localEquipSetData { get; protected set; }

    public DetailEquipEventData(object[] currentEvnet, StatusEquip.LocalEquipSetData localEquip)
    {
      this.currentEventData = currentEvnet;
      this.localEquipSetData = localEquip;
    }
  }

  [Flags]
  public enum CURRENT_SECTION
  {
    NONE = 0,
    STATUS_TOP = 1,
    STATUS_SKILL_LIST = 2,
    STATUS_EQUIP = 4,
    STATUS_EQUIP_SKILL = 8,
    STATUS_AVATAR = 16, // 0x00000010
    ITEM_STORAGE = 32, // 0x00000020
    SMITH_CREATE = 64, // 0x00000040
    SMITH_EVOLVE = 128, // 0x00000080
    SMITH_GROW = 256, // 0x00000100
    SMITH_SKILL_GROW = 512, // 0x00000200
    SMITH_SKILL_MATERIAL = 1024, // 0x00000400
    QUEST_ROOM = 2048, // 0x00000800
    QUEST_RESULT = 4096, // 0x00001000
    UI_PARTS = 8192, // 0x00002000
    GACHA_RESULT = 16384, // 0x00004000
    EQUIP_LIST = 32768, // 0x00008000
    EQUIP_SET_DETAIL_STATUS = 65536, // 0x00010000
    GACHA_EQUIP_PREVIEW = 131072, // 0x00020000
    SHOP_TOP = 262144, // 0x00040000
    SMITH_SELL = 524288, // 0x00080000
  }
}
