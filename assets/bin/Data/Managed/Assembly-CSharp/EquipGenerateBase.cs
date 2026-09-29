// Decompiled with JetBrains decompiler
// Type: EquipGenerateBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public abstract class EquipGenerateBase : EquipMaterialBase
{
  protected SkillItemTable.SkillItemData[] skillDataTable;
  protected AbilityDetailPopUp abilityDetailPopUp;
  protected List<Transform> touchAndReleaseButtons = new List<Transform>();

  public override void Initialize() => base.Initialize();

  protected override string GetEquipItemName() => this.GetEquipTableData().name;

  protected override void EquipTableParam()
  {
    int exceed = 0;
    EquipItemInfo equipData = this.GetEquipData();
    if (equipData != null)
      exceed = equipData.exceed;
    EquipItemTable.EquipItemData table_data = this.GetEquipTableData();
    if (table_data == null)
      return;
    EquipItemExceedParamTable.EquipItemExceedParamAll itemExceedParamAll = table_data.GetExceedParam((uint) exceed) ?? new EquipItemExceedParamTable.EquipItemExceedParamAll();
    this.SetLabelText((Enum) EquipGenerateBase.UI.LBL_NAME, table_data.name);
    this.SetLabelText((Enum) EquipGenerateBase.UI.LBL_LV_NOW, "1");
    this.SetLabelText((Enum) EquipGenerateBase.UI.LBL_LV_MAX, table_data.maxLv.ToString());
    int num = (int) table_data.baseAtk + (int) itemExceedParamAll.atk;
    int elemAtk = itemExceedParamAll.GetElemAtk(table_data.atkElement);
    this.SetElementSprite((Enum) EquipGenerateBase.UI.SPR_ELEM, itemExceedParamAll.GetElemAtkType(table_data.atkElement));
    this.SetLabelText((Enum) EquipGenerateBase.UI.LBL_ATK, num.ToString());
    this.SetLabelText((Enum) EquipGenerateBase.UI.LBL_ELEM, elemAtk.ToString());
    this.SetLabelText((Enum) EquipGenerateBase.UI.LBL_DEF, ((int) table_data.baseDef + (int) itemExceedParamAll.def).ToString());
    int elemDef = itemExceedParamAll.GetElemDef(table_data.defElement);
    this.SetDefElementSprite((Enum) EquipGenerateBase.UI.SPR_ELEM_DEF, itemExceedParamAll.GetElemDefType(table_data.defElement));
    this.SetLabelText((Enum) EquipGenerateBase.UI.LBL_ELEM_DEF, elemDef.ToString());
    this.SetLabelText((Enum) EquipGenerateBase.UI.LBL_HP, ((int) table_data.baseHp + (int) itemExceedParamAll.hp).ToString());
    this.SetActive((Enum) EquipGenerateBase.UI.SPR_IS_EVOLVE, table_data.IsEvolve());
    this.SetEquipmentTypeIcon((Enum) EquipGenerateBase.UI.SPR_TYPE_ICON, (Enum) EquipGenerateBase.UI.SPR_TYPE_ICON_BG, (Enum) EquipGenerateBase.UI.SPR_TYPE_ICON_RARITY, table_data);
    this.SetLabelText((Enum) EquipGenerateBase.UI.LBL_SELL, table_data.sale.ToString());
    if (this.smithType == SmithEquipBase.SmithType.EVOLVE)
      return;
    this.SetSkillIconButton((Enum) EquipGenerateBase.UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButton", table_data, this.GetSkillSlotData(table_data, 0), (string) null);
    if (table_data.fixedAbility.Length != 0)
    {
      string allAbilityName = "";
      string allAp = "";
      string allAbilityDesc = "";
      this.SetTable((Enum) EquipGenerateBase.UI.TBL_ABILITY, "ItemDetailEquipAbilityItem", table_data.fixedAbility.Length, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        EquipItemAbility equipItemAbility = new EquipItemAbility((uint) table_data.fixedAbility[i].id, table_data.fixedAbility[i].pt);
        this.SetActive(t, true);
        this.SetActive(t, (Enum) EquipGenerateBase.UI.OBJ_FIXEDABILITY, true);
        this.SetActive(t, (Enum) EquipGenerateBase.UI.OBJ_ABILITY, false);
        this.SetLabelText(t, (Enum) EquipGenerateBase.UI.LBL_FIXEDABILITY, equipItemAbility.GetName());
        this.SetLabelText(t, (Enum) EquipGenerateBase.UI.LBL_FIXEDABILITY_NUM, equipItemAbility.GetAP());
        this.SetAbilityItemEvent(t, i, this.touchAndReleaseButtons);
        allAbilityName += equipItemAbility.GetName();
        allAp += equipItemAbility.GetAP();
        allAbilityDesc += equipItemAbility.GetDescription();
      }));
      this.SetActive((Enum) EquipGenerateBase.UI.STR_NON_ABILITY, false);
      this.PreCacheAbilityDetail(allAbilityName, allAp, allAbilityDesc);
    }
    else
      this.SetActive((Enum) EquipGenerateBase.UI.STR_NON_ABILITY, true);
  }

  protected override void OnQuery_SKILL_ICON_BUTTON()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.SMITH_CREATE,
      (object) this.GetEquipTableData()
    });
  }

  protected override void OnQuery_START()
  {
    SmithManager.ERR_SMITH_SEND equipItem = MonoBehaviourSingleton<SmithManager>.I.CheckCreateEquipItem(this.GetCreateEquiptableID());
    if (equipItem != SmithManager.ERR_SMITH_SEND.NONE)
    {
      GameSection.ChangeEvent(equipItem.ToString());
    }
    else
    {
      this.isDialogEventYES = false;
      GameSection.SetEventData((object) new object[1]
      {
        (object) this.GetEquipItemName()
      });
    }
  }

  protected override void Send()
  {
    SmithManager.ResultData result_data = new SmithManager.ResultData();
    GameSection.SetEventData((object) result_data);
    GameSection.StayEvent();
    MonoBehaviourSingleton<SmithManager>.I.SendCreateEquipItem(this.GetCreateEquiptableID(), (Action<Error, EquipItemInfo>) ((err, create_item) =>
    {
      if (err != Error.None)
      {
        if (err == Error.WRN_SMITH_OVER_EQUIP_ITEM_NUM)
        {
          GameSection.ChangeStayEvent("CREATE_OVER_EQUIP");
          GameSection.ResumeEvent(true);
        }
        else
          GameSection.ResumeEvent(false);
      }
      else
      {
        result_data.itemData = (object) create_item;
        MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = true;
        GameSection.ResumeEvent(true);
      }
    }));
  }

  public void OnQuery_SmithCreateOverEquipItem_GO_ITEM_STORAGE()
  {
    EventData[] event_datas = new EventData[3]
    {
      new EventData("SECTION_BACK", (object) null),
      new EventData("SELL", (object) null),
      new EventData("TAB_" + (object) 2, (object) null)
    };
    GameSection.StopEvent();
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
  }

  public void OnQuery_SmithCreateOverEquipItem_EXPAND_STORAGE()
  {
    this.DispatchEvent("EXPAND_STORAGE");
  }

  protected virtual void OnQuery_ABILITY_DATA_POPUP()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    int index = (int) eventData[0];
    EquipItem.Ability ability1 = this.GetEquipTableData().fixedAbility[index];
    Transform targetTrans = eventData[1] as Transform;
    EquipItemAbility ability2 = new EquipItemAbility((uint) ability1.id, ability1.pt);
    if (Object.op_Equality((Object) this.abilityDetailPopUp, (Object) null))
      this.abilityDetailPopUp = this.CreateAndGetAbilityDetail((Enum) EquipGenerateBase.UI.OBJ_DETAIL_ROOT);
    this.abilityDetailPopUp.ShowAbilityDetail(targetTrans);
    this.abilityDetailPopUp.SetAbilityDetailText(ability2);
    GameSection.StopEvent();
  }

  protected void OnQuery_RELEASE_ABILITY()
  {
    if (Object.op_Equality((Object) this.abilityDetailPopUp, (Object) null))
      return;
    this.abilityDetailPopUp.Hide();
    GameSection.StopEvent();
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    base.OnNotify(flags);
    if ((flags & GameSection.NOTIFY_FLAG.PRETREAT_SCENE) == (GameSection.NOTIFY_FLAG) 0)
      return;
    this.NoEventReleaseTouchAndReleases(this.touchAndReleaseButtons);
    this.OnQuery_RELEASE_ABILITY();
  }

  private void PreCacheAbilityDetail(string name, string ap, string desc)
  {
    if (Object.op_Equality((Object) this.abilityDetailPopUp, (Object) null))
      this.abilityDetailPopUp = this.CreateAndGetAbilityDetail((Enum) EquipGenerateBase.UI.OBJ_DETAIL_ROOT);
    this.abilityDetailPopUp.PreCacheAbilityDetail(name, ap, desc);
  }

  protected virtual uint GetCreateEquiptableID() => 0;

  protected new enum UI
  {
    BTN_DECISION,
    BTN_INACTIVE,
    LBL_NEXT_BTN,
    LBL_TO_SELECT,
    BTN_TO_SELECT,
    BTN_TO_SELECT_CENTER,
    OBJ_ADD_ABILITY,
    LBL_ADD_ABILITY,
    TEX_MODEL,
    TEX_DETAIL_BASE_MODEL,
    OBJ_DETAIL_ROOT,
    OBJ_DETAIL_BASE_ROOT,
    OBJ_ITEM_INFO_ROOT,
    OBJ_AIM_GROW,
    BTN_AIM_L,
    BTN_AIM_R,
    BTN_AIM_L_INACTIVE,
    BTN_AIM_R_INACTIVE,
    SPR_AIM_L,
    SPR_AIM_R,
    LBL_AIM_LV,
    OBJ_EVOLVE_ROOT,
    LBL_EVO_INDEX,
    LBL_EVO_INDEX_MAX,
    BTN_EVO_L,
    BTN_EVO_R,
    BTN_EVO_L_INACTIVE,
    BTN_EVO_R_INACTIVE,
    SPR_EVO_L,
    SPR_EVO_R,
    BTN_EVO_R2,
    BTN_EVO_L2,
    BTN_EVO_L2_INACTIVE,
    BTN_EVO_R2_INACTIVE,
    SPR_EVO_R2,
    SPR_EVO_L2,
    OBJ_ORDER_L2,
    OBJ_ORDER_R2,
    OBJ_ORDER_NORMAL_CENTER,
    OBJ_ORDER_ATTRIBUTE_CENTER,
    SPR_ORDER_ELEM_CENTER,
    OBJ_ORDER_NORMAL_R,
    OBJ_ORDER_ATTRIBUTE_R,
    SPR_ORDER_ELEM_R,
    OBJ_ORDER_NORMAL_L,
    OBJ_ORDER_ATTRIBUTE_L,
    SPR_ORDER_ELEM_L,
    OBJ_ORDER_CENTER_ANIM_ROOT,
    OBJ_ORDER_L_ANIM_ROOT,
    OBJ_ORDER_R_ANIM_ROOT,
    STR_INACTIVE,
    STR_INACTIVE_REFLECT,
    STR_DECISION,
    STR_DECISION_REFLECT,
    STR_TITLE_MATERIAL,
    STR_TITLE_MONEY,
    STR_TITLE_ATK,
    STR_TITLE_ELEM,
    STR_TITLE_DEF,
    STR_TITLE_ELEM_DEF,
    STR_TITLE_HP,
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
    BTN_SELL,
    BTN_GROW,
    OBJ_FAVORITE_ROOT,
    SPR_FAVORITE,
    SPR_UNFAVORITE,
    SPR_IS_EVOLVE,
    TWN_FAVORITE,
    TWN_UNFAVORITE,
    OBJ_ATK_ROOT,
    OBJ_DEF_ROOT,
    OBJ_ELEM_ROOT,
    SPR_TYPE_ICON,
    SPR_TYPE_ICON_BG,
    SPR_TYPE_ICON_RARITY,
    STR_TITLE_ITEM_INFO,
    STR_TITLE_STATUS,
    STR_TITLE_SKILL_SLOT,
    STR_TITLE_ABILITY,
    STR_TITLE_SELL,
    STR_TITLE_ELEMENT,
    TBL_ABILITY,
    STR_NON_ABILITY,
    LBL_ABILITY,
    LBL_ABILITY_NUM,
    BTN_EXCEED,
    SPR_COUNT_0_ON,
    SPR_COUNT_1_ON,
    SPR_COUNT_2_ON,
    SPR_COUNT_3_ON,
    STR_ONLY_EXCEED,
    LBL_AFTER_ATK,
    LBL_AFTER_DEF,
    LBL_AFTER_HP,
    LBL_AFTER_ELEM,
    LBL_AFTER_ELEM_DEF,
    GRD_NEED_MATERIAL,
    LBL_GOLD,
    LBL_CAPTION,
    BTN_GRAPH,
    BTN_LIST,
    SPR_SP_ATTACK_TYPE,
    SPR_ORDER_ACTIONTYPE_CENTER,
    SPR_ORDER_ACTIONTYPE_LEFT,
    SPR_ORDER_ACTIONTYPE_RIGHT,
    BTN_SHADOW_EVOLVE,
    OBJ_ABILITY,
    OBJ_FIXEDABILITY,
    LBL_FIXEDABILITY,
    LBL_FIXEDABILITY_NUM,
    OBJ_ABILITY_ITEM,
    LBL_ABILITY_ITEM,
    OBJ_WEAPON_ROOT,
    OBJ_ARMOR_ROOT,
    LinePartsR01,
  }
}
