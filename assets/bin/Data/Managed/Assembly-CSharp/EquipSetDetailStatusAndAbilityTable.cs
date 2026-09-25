// Decompiled with JetBrains decompiler
// Type: EquipSetDetailStatusAndAbilityTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EquipSetDetailStatusAndAbilityTable : GameSection
{
  private EquipSetDetailStatusAndAbilityTable.UI[] uiAbility = new EquipSetDetailStatusAndAbilityTable.UI[7]
  {
    EquipSetDetailStatusAndAbilityTable.UI.LBL_AP_0,
    EquipSetDetailStatusAndAbilityTable.UI.LBL_AP_1,
    EquipSetDetailStatusAndAbilityTable.UI.LBL_AP_2,
    EquipSetDetailStatusAndAbilityTable.UI.LBL_AP_3,
    EquipSetDetailStatusAndAbilityTable.UI.LBL_AP_4,
    EquipSetDetailStatusAndAbilityTable.UI.LBL_AP_5,
    EquipSetDetailStatusAndAbilityTable.UI.LBL_AP_6
  };
  private EquipSetDetailStatusAndAbilityTable.UI[] uiAtkElem = new EquipSetDetailStatusAndAbilityTable.UI[7]
  {
    EquipSetDetailStatusAndAbilityTable.UI.LBL_ATK_ELEM_NONE,
    EquipSetDetailStatusAndAbilityTable.UI.LBL_ATK_ELEM_FIRE,
    EquipSetDetailStatusAndAbilityTable.UI.LBL_ATK_ELEM_WATER,
    EquipSetDetailStatusAndAbilityTable.UI.LBL_ATK_ELEM_THUNDER,
    EquipSetDetailStatusAndAbilityTable.UI.LBL_ATK_ELEM_EARTH,
    EquipSetDetailStatusAndAbilityTable.UI.LBL_ATK_ELEM_LIGHT,
    EquipSetDetailStatusAndAbilityTable.UI.LBL_ATK_ELEM_DARK
  };
  private EquipSetDetailStatusAndAbilityTable.UI[] uiDefElem = new EquipSetDetailStatusAndAbilityTable.UI[7]
  {
    EquipSetDetailStatusAndAbilityTable.UI.LBL_DEF_ELEM_NONE,
    EquipSetDetailStatusAndAbilityTable.UI.LBL_DEF_ELEM_FIRE,
    EquipSetDetailStatusAndAbilityTable.UI.LBL_DEF_ELEM_WATER,
    EquipSetDetailStatusAndAbilityTable.UI.LBL_DEF_ELEM_THUNDER,
    EquipSetDetailStatusAndAbilityTable.UI.LBL_DEF_ELEM_EARTH,
    EquipSetDetailStatusAndAbilityTable.UI.LBL_DEF_ELEM_LIGHT,
    EquipSetDetailStatusAndAbilityTable.UI.LBL_DEF_ELEM_DARK
  };
  private EquipSetDetailStatusAndAbilityTable.UI[] uiToggleStatusIndex = new EquipSetDetailStatusAndAbilityTable.UI[3]
  {
    EquipSetDetailStatusAndAbilityTable.UI.TGL_STATUS_WINDOW_INDEX0,
    EquipSetDetailStatusAndAbilityTable.UI.TGL_STATUS_WINDOW_INDEX1,
    EquipSetDetailStatusAndAbilityTable.UI.TGL_STATUS_WINDOW_INDEX2
  };
  private EquipSetDetailStatusAndAbilityTable.UI[] uiToggleWindowIconIndex = new EquipSetDetailStatusAndAbilityTable.UI[3]
  {
    EquipSetDetailStatusAndAbilityTable.UI.TGL_WINDOW_ICON_INDEX0,
    EquipSetDetailStatusAndAbilityTable.UI.TGL_WINDOW_ICON_INDEX1,
    EquipSetDetailStatusAndAbilityTable.UI.TGL_WINDOW_ICON_INDEX2
  };
  private EquipSetDetailStatusAndAbilityTable.UI[] uiToggleButtonIndex = new EquipSetDetailStatusAndAbilityTable.UI[3]
  {
    EquipSetDetailStatusAndAbilityTable.UI.TGL_BUTTON_INDEX0,
    EquipSetDetailStatusAndAbilityTable.UI.TGL_BUTTON_INDEX1,
    EquipSetDetailStatusAndAbilityTable.UI.TGL_BUTTON_INDEX2
  };
  private EquipSetDetailStatusAndAbilityTable.BaseStatus baseStatus;
  protected EquipItemAbilityCollection[] abilityCollection;
  protected EquipSetInfo equipSet;
  protected int selectEquipIndex;
  private bool isEquipSubWeapon;
  protected List<AbilityItemInfo> abilityItems = new List<AbilityItemInfo>();
  protected object[] currentEventData;
  private EquipSetDetailStatusAndAbilityTable.UI[] spr = new EquipSetDetailStatusAndAbilityTable.UI[10]
  {
    EquipSetDetailStatusAndAbilityTable.UI.SPR_BG0,
    EquipSetDetailStatusAndAbilityTable.UI.SPR_BG1,
    EquipSetDetailStatusAndAbilityTable.UI.SPR_BG2,
    EquipSetDetailStatusAndAbilityTable.UI.SPR_BG3,
    EquipSetDetailStatusAndAbilityTable.UI.SPR_BG4,
    EquipSetDetailStatusAndAbilityTable.UI.SPR_BG5,
    EquipSetDetailStatusAndAbilityTable.UI.SPR_BG6,
    EquipSetDetailStatusAndAbilityTable.UI.SPR_BG7,
    EquipSetDetailStatusAndAbilityTable.UI.SPR_BG8,
    EquipSetDetailStatusAndAbilityTable.UI.SPR_BG9
  };
  private SpringPanel spring;

  public override void Initialize()
  {
    this.currentEventData = GameSection.GetEventData() as object[];
    this.equipSet = this.currentEventData[0] as EquipSetInfo;
    this.abilityCollection = this.currentEventData[1] as EquipItemAbilityCollection[];
    this.baseStatus = this.currentEventData[2] as EquipSetDetailStatusAndAbilityTable.BaseStatus;
    foreach (EquipItemInfo equipItemInfo in this.equipSet.item)
    {
      if (equipItemInfo != null)
      {
        AbilityItemInfo abilityItem = equipItemInfo.GetAbilityItem();
        if (abilityItem != null)
          this.abilityItems.Add(abilityItem);
      }
    }
    Array.Sort<EquipItemAbilityCollection>(this.abilityCollection, (Comparison<EquipItemAbilityCollection>) ((l, r) => r.ability.ap != l.ability.ap ? r.ability.ap - l.ability.ap : (int) l.ability.id - (int) r.ability.id));
    this.isEquipSubWeapon = this.equipSet.item[1] != null || this.equipSet.item[2] != null;
    this.selectEquipIndex = 0;
    this.SetSupportEncoding((Enum) EquipSetDetailStatusAndAbilityTable.UI.LBL_HP, true);
    this.SetSupportEncoding((Enum) EquipSetDetailStatusAndAbilityTable.UI.LBL_ATK, true);
    this.SetSupportEncoding((Enum) EquipSetDetailStatusAndAbilityTable.UI.LBL_DEF, true);
    int index = 0;
    for (int length = this.uiAtkElem.Length; index < length; ++index)
    {
      this.SetSupportEncoding((Enum) this.uiAtkElem[index], true);
      this.SetSupportEncoding((Enum) this.uiDefElem[index], true);
    }
    this.InitializeCaption();
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) EquipSetDetailStatusAndAbilityTable.UI.OBJ_EQUIP_BTN_ROOT_ACTIVE, this.isEquipSubWeapon);
    this.SetActive((Enum) EquipSetDetailStatusAndAbilityTable.UI.OBJ_EQUIP_BTN_ROOT_INACTIVE, !this.isEquipSubWeapon);
  }

  protected void UpdateAbilityTable()
  {
    int item_num = Mathf.Max(this.abilityCollection.Length + this.abilityItems.Count, 5);
    bool is_scroll = true;
    string allAbilityName = "";
    string allAp = "";
    string allAbilityDesc = "";
    bool isEmpty = true;
    this.SetGrid((Enum) EquipSetDetailStatusAndAbilityTable.UI.GRD_ABILITY, "EquipSetDetailAbilityTableItem", item_num, true, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (i >= this.abilityCollection.Length + this.abilityItems.Count)
      {
        is_scroll = false;
        this.SetActive(t, (Enum) EquipSetDetailStatusAndAbilityTable.UI.OBJ_ABILITY_ITEM_ROOT, false);
      }
      else
      {
        isEmpty = false;
        this.SetActive(this._transform, (Enum) EquipSetDetailStatusAndAbilityTable.UI.OBJ_EMPTY, false);
        this.SetActive(t, (Enum) EquipSetDetailStatusAndAbilityTable.UI.OBJ_ABILITY_ITEM_ROOT, true);
        int index1 = i;
        if (i < this.abilityCollection.Length)
        {
          this.SetActive(t, (Enum) EquipSetDetailStatusAndAbilityTable.UI.OBJ_ABILITY_ITEM_ITEM_ROOT, false);
          EquipItemAbilityCollection ability = this.abilityCollection[index1];
          if (ability.ability.id == 0U || ability.ability.IsNeedUpdate() || !ability.ability.IsActiveAbility())
          {
            this.SetActive(t, false);
          }
          else
          {
            this.SetActive(t, true);
            int index2 = 0;
            for (int length = ability.equip.Length; index2 < length; ++index2)
              this.SetAPLabel(t, (Enum) this.uiAbility[index2], ability.GetAP(index2), ability.swapValue[index2]);
            this.SetLabelText(t, (Enum) EquipSetDetailStatusAndAbilityTable.UI.LBL_AP_TOTAL, ability.ability.GetAP());
            Color color = Color.white;
            if (ability.GetSwapBalance() < 0)
              color = Color.red;
            else if (ability.GetSwapBalance() > 0)
              color = Color.green;
            this.GetComponent<UILabel>(t, (Enum) EquipSetDetailStatusAndAbilityTable.UI.LBL_AP_TOTAL).color = color;
            this.SetLabelText(t, (Enum) EquipSetDetailStatusAndAbilityTable.UI.LBL_ABILITY_NAME, ability.ability.GetName());
            this.SetAbilityItemEvent(t, index1);
            allAbilityName += ability.ability.GetName();
            allAp += ability.ability.GetAP();
            allAbilityDesc += ability.ability.GetDescription();
            this.SetToggle(t, (Enum) EquipSetDetailStatusAndAbilityTable.UI.TGL_NAME_TAG, ability.IsAbilityOn());
          }
        }
        else
        {
          int index3 = 0;
          for (int length = this.uiAbility.Length; index3 < length; ++index3)
            this.SetAPLabel(t, (Enum) this.uiAbility[index3], "", 0);
          this.SetLabelText(t, (Enum) EquipSetDetailStatusAndAbilityTable.UI.LBL_AP_TOTAL, "");
          AbilityItemInfo abilityItem = this.abilityItems[i - this.abilityCollection.Length];
          this.SetActive(t, (Enum) EquipSetDetailStatusAndAbilityTable.UI.OBJ_ABILITY_ITEM_ITEM_ROOT, true);
          this.SetLabelText(t, (Enum) EquipSetDetailStatusAndAbilityTable.UI.LBL_ABILITY_NAME, abilityItem.GetName());
          this.SetAbilityItemItemEvent(t, i);
          allAbilityName += abilityItem.GetName();
          allAbilityDesc += abilityItem.GetDescription();
        }
      }
    }));
    this.SetActive(this._transform, (Enum) EquipSetDetailStatusAndAbilityTable.UI.OBJ_EMPTY, isEmpty);
    this.SetLabelText(this.GetCtrl((Enum) EquipSetDetailStatusAndAbilityTable.UI.OBJ_EMPTY), (Enum) EquipSetDetailStatusAndAbilityTable.UI.LBL_NO_ITEM, StringTable.Get(STRING_CATEGORY.COMMON, 19800U));
    this.PreCacheAbilityDetail(allAbilityName, allAp, allAbilityDesc);
    ((Behaviour) this.GetComponent<UIScrollView>((Enum) EquipSetDetailStatusAndAbilityTable.UI.SCR_ABILITY)).enabled = is_scroll;
  }

  protected virtual void SetAbilityItemEvent(Transform t, int index)
  {
    this.SetEvent(t, (Enum) EquipSetDetailStatusAndAbilityTable.UI.BTN_ABILITY, "ABILITY_DATA", index);
  }

  protected virtual void SetAbilityItemItemEvent(Transform t, int index)
  {
    this.SetEvent(t, (Enum) EquipSetDetailStatusAndAbilityTable.UI.BTN_ABILITY, "ABILITY_ITEM_DATA", index);
  }

  protected void SetLabelSeparateText(Enum label_enum, int baseValue, int finalValue)
  {
    bool flag = this.IsSupportEncoding(label_enum);
    int num = finalValue - baseValue;
    string text = num <= 0 ? (num >= 0 ? $"{baseValue}" : string.Format(flag ? "{0}[FF0000]{1}[-]" : "{0}{1}", (object) baseValue, (object) num)) : string.Format(flag ? "{0}[35FF00]+{1}[-]" : "{0}+{1}", (object) baseValue, (object) num);
    this.SetLabelText(label_enum, text);
  }

  protected virtual void UpdateUIStatus()
  {
    int hp;
    int atk;
    int def;
    EquipSetCalculator equipSetCalculator;
    if (this.baseStatus.charaListEquip == null)
    {
      hp = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.hp;
      atk = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.atk;
      def = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.def;
      equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSetCalculator(MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo());
    }
    else
    {
      hp = this.baseStatus.hp;
      atk = this.baseStatus.atk;
      def = this.baseStatus.def;
      if (MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex == -1)
      {
        MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = 0;
        equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(0);
        equipSetCalculator.SetEquipSet(this.baseStatus.charaListEquip);
      }
      else
        equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex);
    }
    StatusFactor statusFactor = equipSetCalculator.GetStatusFactor(this.selectEquipIndex);
    SimpleStatus finalStatus = equipSetCalculator.GetFinalStatus(this.selectEquipIndex, hp, atk, def);
    this.SetLabelSeparateText((Enum) EquipSetDetailStatusAndAbilityTable.UI.LBL_HP, hp + statusFactor.baseStatus.hp, finalStatus.hp);
    this.SetLabelText((Enum) EquipSetDetailStatusAndAbilityTable.UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
    this.SetLabelText((Enum) EquipSetDetailStatusAndAbilityTable.UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
    int index = 0;
    for (int length = this.uiAtkElem.Length; index < length; ++index)
    {
      if (index == 0)
      {
        this.SetLabelSeparateText((Enum) this.uiAtkElem[index], atk + statusFactor.baseStatus.attacks[index], finalStatus.attacks[index]);
        this.SetLabelSeparateText((Enum) this.uiDefElem[index], def + statusFactor.baseStatus.defences[index], finalStatus.defences[index]);
      }
      else
      {
        this.SetLabelSeparateText((Enum) this.uiAtkElem[index], statusFactor.baseStatus.attacks[index], finalStatus.attacks[index]);
        this.SetLabelSeparateText((Enum) this.uiDefElem[index], statusFactor.baseStatus.tolerances[index - 1], finalStatus.tolerances[index - 1]);
      }
    }
    this.SetToggle((Enum) this.uiToggleStatusIndex[this.selectEquipIndex], true);
    this.SetToggle((Enum) this.uiToggleWindowIconIndex[this.selectEquipIndex], true);
    this.SetToggle((Enum) this.uiToggleButtonIndex[this.selectEquipIndex], true);
  }

  private void SetAPLabel(Transform parent, Enum _enum, string ap, int swap_value)
  {
    this.SetActive(parent, _enum, !string.IsNullOrEmpty(ap));
    this.SetLabelText(parent, _enum, ap);
    UILabel component = this.GetComponent<UILabel>(parent, _enum);
    if (swap_value == 0)
      component.color = Color.white;
    else if (swap_value < 0)
      component.color = Color.red;
    else
      component.color = Color.green;
  }

  protected virtual void OnQuery_ABILITY_DATA()
  {
    GameSection.SetEventData((object) this.abilityCollection[(int) GameSection.GetEventData()].ability);
  }

  protected virtual void OnQuery_ABILITY_ITEM_DATA()
  {
    GameSection.SetEventData((object) this.abilityItems[this.abilityCollection.Length - (int) GameSection.GetEventData()]);
  }

  protected virtual void OnQuery_INDEX_L()
  {
    this.selectEquipIndex = this.selectEquipIndex == 0 ? 2 : this.selectEquipIndex - 1;
    if (this.equipSet.item[this.selectEquipIndex] == null)
      this.selectEquipIndex = this.selectEquipIndex == 0 ? 2 : this.selectEquipIndex - 1;
    this.UpdateUIStatus();
  }

  protected virtual void OnQuery_INDEX_R()
  {
    this.selectEquipIndex = this.selectEquipIndex == 2 ? 0 : this.selectEquipIndex + 1;
    if (this.equipSet.item[this.selectEquipIndex] == null)
      this.selectEquipIndex = this.selectEquipIndex == 2 ? 0 : this.selectEquipIndex + 1;
    this.UpdateUIStatus();
  }

  private void LateUpdate()
  {
    if (Object.op_Equality((Object) this.spring, (Object) null))
      this.spring = this.GetComponent<SpringPanel>((Enum) EquipSetDetailStatusAndAbilityTable.UI.SCR_ABILITY);
    if (!Object.op_Inequality((Object) this.spring, (Object) null) || !((Behaviour) this.spring).enabled)
      return;
    int index = 0;
    for (int length = this.spr.Length; index < length; ++index)
      this.GetComponent<UISprite>((Enum) this.spr[index]).UpdateAnchors();
  }

  protected virtual void OnQuery_TO_STATUS()
  {
    GameSection.SetEventData((object) this.currentEventData);
  }

  private void InitializeCaption()
  {
    Transform ctrl = this.GetCtrl((Enum) EquipSetDetailStatusAndAbilityTable.UI.OBJ_CAPTION_3);
    string text = this.sectionData.GetText("CAPTION");
    this.SetLabelText(ctrl, (Enum) EquipSetDetailStatusAndAbilityTable.UI.LBL_CAPTION, text);
    UITweenCtrl component = ((Component) ctrl).gameObject.GetComponent<UITweenCtrl>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Reset();
    int index = 0;
    for (int length = component.tweens.Length; index < length; ++index)
      component.tweens[index].ResetToBeginning();
    component.Play();
  }

  protected virtual void PreCacheAbilityDetail(string name, string ap, string desc)
  {
  }

  protected enum UI
  {
    GRD_ABILITY,
    SCR_ABILITY,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    LBL_ATK_ELEM_FIRE,
    LBL_ATK_ELEM_WATER,
    LBL_ATK_ELEM_THUNDER,
    LBL_ATK_ELEM_EARTH,
    LBL_ATK_ELEM_LIGHT,
    LBL_ATK_ELEM_DARK,
    LBL_ATK_ELEM_NONE,
    LBL_DEF_ELEM_FIRE,
    LBL_DEF_ELEM_WATER,
    LBL_DEF_ELEM_THUNDER,
    LBL_DEF_ELEM_EARTH,
    LBL_DEF_ELEM_LIGHT,
    LBL_DEF_ELEM_DARK,
    LBL_DEF_ELEM_NONE,
    TGL_STATUS_WINDOW_INDEX0,
    TGL_STATUS_WINDOW_INDEX1,
    TGL_STATUS_WINDOW_INDEX2,
    TGL_WINDOW_ICON_INDEX0,
    TGL_WINDOW_ICON_INDEX1,
    TGL_WINDOW_ICON_INDEX2,
    TGL_BUTTON_INDEX0,
    TGL_BUTTON_INDEX1,
    TGL_BUTTON_INDEX2,
    OBJ_EQUIP_BTN_ROOT_ACTIVE,
    OBJ_EQUIP_BTN_ROOT_INACTIVE,
    SPR_BG0,
    SPR_BG1,
    SPR_BG2,
    SPR_BG3,
    SPR_BG4,
    SPR_BG5,
    SPR_BG6,
    SPR_BG7,
    SPR_BG8,
    SPR_BG9,
    OBJ_DETAIL_ROOT,
    OBJ_ABILITY_ITEM_ROOT,
    OBJ_ABILITY_ITEM_ITEM_ROOT,
    BTN_ABILITY,
    LBL_ABILITY_NAME,
    LBL_AP_0,
    LBL_AP_1,
    LBL_AP_2,
    LBL_AP_3,
    LBL_AP_4,
    LBL_AP_5,
    LBL_AP_6,
    LBL_AP_TOTAL,
    SPR_NAME_TAG,
    SPR_NAME_TAG_OFF,
    TGL_BG,
    TGL_NAME_TAG,
    ICON_WEAPON,
    LBL_NAME,
    LBL_LV_NOW,
    LBL_LV_MAX,
    SPR_TYPE_ICON,
    SPR_TYPE_ICON_BG,
    SPR_TYPE_ICON_RARITY,
    OBJ_CAPTION_3,
    LBL_CAPTION,
    OBJ_EMPTY,
    LBL_NO_ITEM,
  }

  public class BaseStatus
  {
    public int atk;
    public int def;
    public int hp;
    public List<CharaInfo.EquipItem> charaListEquip;

    public BaseStatus(
      int _atk,
      int _def,
      int _hp,
      List<CharaInfo.EquipItem> chara_list_equip_data)
    {
      this.atk = _atk;
      this.def = _def;
      this.hp = _hp;
      this.charaListEquip = chara_list_equip_data;
    }
  }
}
