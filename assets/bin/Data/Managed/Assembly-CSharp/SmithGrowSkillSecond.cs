// Decompiled with JetBrains decompiler
// Type: SmithGrowSkillSecond
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SmithGrowSkillSecond : ItemDetailSkill
{
  public int MATERIAL_SELECT_MAX = 10;
  private SkillItemInfo skillItem;
  private SkillItemInfo[] material;
  private int needGold;
  private Color goldColor = Color.white;
  private bool isNoticeSendGrow;
  private bool isExceed;
  private bool isSortTypeReset;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.skillItem = eventData[0] as SkillItemInfo;
    this.material = eventData[1] as SkillItemInfo[];
    GameSection.SetEventData((object) new object[2]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.UI_PARTS,
      (object) this.skillItem
    });
    UILabel component1 = this.GetComponent<UILabel>((Enum) SmithGrowSkillSecond.UI.LBL_GOLD);
    if (Object.op_Inequality((Object) component1, (Object) null))
      this.goldColor = component1.color;
    UITweenCtrl component2 = ((Component) this.GetCtrl((Enum) SmithGrowSkillSecond.UI.OBJ_CAPTION)).gameObject.GetComponent<UITweenCtrl>();
    if (Object.op_Inequality((Object) component2, (Object) null))
    {
      component2.Reset();
      int index = 0;
      for (int length = component2.tweens.Length; index < length; ++index)
        component2.tweens[index].ResetToBeginning();
      component2.Play();
    }
    this.isExceed = this.skillItem.IsLevelMax();
    this.isSortTypeReset = this.isExceed;
    base.Initialize();
  }

  protected override void OnOpen()
  {
    if (GameSection.GetEventData() is object[] eventData && eventData.Length > 1 && eventData[1] is SkillItemInfo[] skillItemInfoArray)
      this.material = skillItemInfoArray;
    this.isNoticeSendGrow = false;
    base.OnOpen();
  }

  public override void UpdateUI()
  {
    this.isExceed = this.skillItem.IsLevelMax();
    this.MATERIAL_SELECT_MAX = this.isExceed ? 10 : 10;
    this.SetFontStyle((Enum) SmithGrowSkillSecond.UI.STR_TITLE_MATERIAL, (FontStyle) 2);
    this.SetFontStyle((Enum) SmithGrowSkillSecond.UI.STR_TITLE_MONEY, (FontStyle) 2);
    if (Object.op_Inequality((Object) this.detailBase, (Object) null))
      this.SetActive(this.detailBase, (Enum) SmithGrowSkillSecond.UI.OBJ_FAVORITE_ROOT, false);
    this.UpdateMaterial();
    Transform ctrl = this.GetCtrl((Enum) SmithGrowSkillSecond.UI.GRD_MATERIAL);
    while (ctrl.childCount != 0)
    {
      Transform child = ctrl.GetChild(0);
      child.parent = (Transform) null;
      ((Component) child).gameObject.SetActive(false);
      Object.Destroy((Object) ((Component) child).gameObject);
    }
    int material_num = this.material != null ? this.material.Length : 0;
    this.SetGrid((Enum) SmithGrowSkillSecond.UI.GRD_MATERIAL, (string) null, this.MATERIAL_SELECT_MAX, false, (Func<int, Transform, Transform>) ((index, parent) => index < material_num ? Utility.CreateGameObject(index.ToString(), parent) : this.Realizes("SkillGrowSecondSelectItem", parent)), (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (i >= material_num)
        return;
      SkillItemInfo skillItemInfo = this.material[i];
      SkillItemTable.SkillItemData tableData = skillItemInfo.tableData;
      this.SetLongTouch(ItemIcon.Create(ItemIcon.GetItemIconType(tableData.type), tableData.iconID, new RARITY_TYPE?(tableData.rarity), t, tableData.skillAtkType, skillItemInfo.tableData.GetEnableEquipType(), event_name: "SELECT", event_data: i, is_equipping: skillItemInfo.isAttached || skillItemInfo.isUniqueAttached, element2: tableData.GetAttackElementByIndex(1), isSameSkillExceed: this.isExceed && (int) this.skillItem.tableData.id == (int) tableData.id).transform, "DETAIL", (object) i);
    }));
    int exceedCnt = this.skillItem.exceedCnt;
    this.SetActive((Enum) SmithGrowSkillSecond.UI.OBJ_LV_EX, exceedCnt > 0);
    if (exceedCnt > 0)
      this.SetLabelText((Enum) SmithGrowSkillSecond.UI.LBL_LV_EX, exceedCnt.ToString());
    if (this.material != null && this.material.Length != 0)
    {
      this.SetActive((Enum) SmithGrowSkillSecond.UI.BTN_DECISION_ON, true);
      this.SetActive((Enum) SmithGrowSkillSecond.UI.BTN_DECISION_OFF, false);
    }
    else
    {
      this.SetActive((Enum) SmithGrowSkillSecond.UI.BTN_DECISION_ON, false);
      this.SetActive((Enum) SmithGrowSkillSecond.UI.BTN_DECISION_OFF, true);
    }
    this.SetLabelText((Enum) SmithGrowSkillSecond.UI.LBL_CAPTION, !this.isExceed ? this.sectionData.GetText("CAPTION_GROW") : this.sectionData.GetText("CAPTION_EXCEED"));
    this.SetActive((Enum) SmithGrowSkillSecond.UI.SPR_BG_NORMAL, !this.isExceed);
    this.SetActive((Enum) SmithGrowSkillSecond.UI.SPR_BG_EXCEED, this.isExceed);
  }

  public static SkillItemInfo ParamCopy(SkillItemInfo _ref, bool isLevelUp = false, bool isExceedUp = false)
  {
    int lv = !isLevelUp ? _ref.level : _ref.level + 1;
    int exceed = !isExceedUp ? _ref.exceedCnt : _ref.exceedCnt + 1;
    SkillItemInfo skillItemInfo = new SkillItemInfo(0, (int) _ref.tableID, lv, exceed);
    skillItemInfo.uniqueID = _ref.uniqueID;
    skillItemInfo.exp = _ref.exp;
    skillItemInfo.expPrev = MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(skillItemInfo.tableData.baseNeedExp, skillItemInfo.growData.needExp);
    skillItemInfo.expNext = MonoBehaviourSingleton<SmithManager>.I.GetGrowResultValue(skillItemInfo.tableData.baseNeedExp, skillItemInfo.nextGrowData.needExp);
    skillItemInfo.exceedExp = _ref.exceedExp;
    skillItemInfo.growCost = _ref.growCost;
    return skillItemInfo;
  }

  private void UpdateMaterial()
  {
    int length1 = this.material != null ? this.material.Length : 0;
    // ISSUE: variable of a boxed type
    __Boxed<SmithGrowSkillSecond.UI> label_enum1 = (Enum) SmithGrowSkillSecond.UI.LBL_SELECT_NUM;
    int num = this.MATERIAL_SELECT_MAX - length1;
    string text1 = num.ToString();
    this.SetLabelText((Enum) label_enum1, text1);
    this.needGold = this.isExceed ? 0 : (int) ((double) this.skillItem.growCost * (double) length1);
    this.SetLabelText((Enum) SmithGrowSkillSecond.UI.LBL_GOLD, this.needGold.ToString("N0"));
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < this.needGold)
      this.SetColor((Enum) SmithGrowSkillSecond.UI.LBL_GOLD, Color.red);
    else
      this.SetColor((Enum) SmithGrowSkillSecond.UI.LBL_GOLD, this.goldColor);
    this.SetActive((Enum) SmithGrowSkillSecond.UI.OBJ_GOLD, !this.isExceed);
    SkillItemInfo skillItemInfo = SmithGrowSkillSecond.ParamCopy(this.skillItem);
    SkillItemInfo _ref = SmithGrowSkillSecond.ParamCopy(this.skillItem);
    if (this.material != null)
    {
      int index = 0;
      for (int length2 = this.material.Length; index < length2; ++index)
      {
        if (this.isExceed)
        {
          if (!_ref.IsMaxExceed())
          {
            if ((int) this.skillItem.tableData.id == (int) this.material[index].tableData.id)
              _ref.exceedExp += this.material[index].giveSameSkillExceedExp;
            else
              _ref.exceedExp += this.material[index].giveExceedExp;
            while (_ref.exceedExpNext <= _ref.exceedExp)
            {
              _ref = SmithGrowSkillSecond.ParamCopy(_ref, isExceedUp: true);
              if (_ref.IsMaxExceed())
              {
                _ref.exceedExp = _ref.expPrev;
                break;
              }
            }
          }
        }
        else if (!_ref.IsLevelMax() && this.material[index].level <= this.material[index].GetMaxLevel())
        {
          _ref.exp += this.material[index].giveExp;
          while (_ref.expNext <= _ref.exp)
          {
            _ref = SmithGrowSkillSecond.ParamCopy(_ref, true);
            if (_ref.IsLevelMax())
            {
              _ref.exp = _ref.expPrev;
              break;
            }
          }
        }
      }
    }
    bool is_visible = !this.isExceed ? skillItemInfo.level != _ref.level : skillItemInfo.exceedCnt != _ref.exceedCnt;
    this.SetActive(this.detailBase, (Enum) SmithGrowSkillSecond.UI.LBL_DESCRIPTION, !is_visible);
    this.SetActive((Enum) SmithGrowSkillSecond.UI.LBL_BASE_DESCRIPTION, is_visible);
    this.SetActive((Enum) SmithGrowSkillSecond.UI.LBL_NEXT_DESCRIPTION, is_visible);
    this.SetActive((Enum) SmithGrowSkillSecond.UI.SPR_STATUS_UP, is_visible);
    this.itemData = (object) _ref;
    base.UpdateUI();
    this.SetLabelText(this.detailBase, (Enum) SmithGrowSkillSecond.UI.LBL_LV_NOW, _ref.level.ToString());
    Transform detailBase1 = this.detailBase;
    // ISSUE: variable of a boxed type
    __Boxed<SmithGrowSkillSecond.UI> label_enum2 = (Enum) SmithGrowSkillSecond.UI.LBL_LV_MAX;
    num = _ref.GetMaxLevel();
    string text2 = num.ToString();
    this.SetLabelText(detailBase1, (Enum) label_enum2, text2);
    this.SetActive(this.detailBase, (Enum) SmithGrowSkillSecond.UI.OBJ_LV_EX, _ref.IsExceeded());
    this.SetLabelText(this.detailBase, (Enum) SmithGrowSkillSecond.UI.LBL_LV_EX, _ref.exceedCnt.ToString());
    Transform detailBase2 = this.detailBase;
    // ISSUE: variable of a boxed type
    __Boxed<SmithGrowSkillSecond.UI> label_enum3 = (Enum) SmithGrowSkillSecond.UI.LBL_ATK;
    num = _ref.atk;
    string text3 = num.ToString();
    this.SetLabelText(detailBase2, (Enum) label_enum3, text3);
    Transform detailBase3 = this.detailBase;
    // ISSUE: variable of a boxed type
    __Boxed<SmithGrowSkillSecond.UI> label_enum4 = (Enum) SmithGrowSkillSecond.UI.LBL_DEF;
    num = _ref.def;
    string text4 = num.ToString();
    this.SetLabelText(detailBase3, (Enum) label_enum4, text4);
    this.SetLabelText(this.detailBase, (Enum) SmithGrowSkillSecond.UI.LBL_HP, _ref.hp.ToString());
    this.SetLabelText(this.detailBase, (Enum) SmithGrowSkillSecond.UI.LBL_SELL, this.needGold.ToString());
    this.SetLabelText(this.detailBase, (Enum) SmithGrowSkillSecond.UI.STR_SELL, this.sectionData.GetText("STR_SELL"));
    this.SetSupportEncoding((Enum) SmithGrowSkillSecond.UI.LBL_DESCRIPTION, true);
    this.SetSupportEncoding((Enum) SmithGrowSkillSecond.UI.LBL_BASE_DESCRIPTION, true);
    this.SetSupportEncoding((Enum) SmithGrowSkillSecond.UI.LBL_NEXT_DESCRIPTION, true);
    string explanationText = skillItemInfo.GetExplanationText(true);
    this.SetLabelText(this.detailBase, (Enum) SmithGrowSkillSecond.UI.LBL_DESCRIPTION, explanationText);
    this.SetLabelText((Enum) SmithGrowSkillSecond.UI.LBL_BASE_DESCRIPTION, explanationText);
    this.SetLabelText((Enum) SmithGrowSkillSecond.UI.LBL_NEXT_DESCRIPTION, _ref.GetExplanationStatusUpText(this.sectionData.GetText("STR_STATUS_UP_FORMAT"), this.isExceed, skillItemInfo.exceedCnt > 0));
    SkillGrowProgress component = ((Component) this.FindCtrl(this.detailBase, (Enum) SmithGrowSkillSecond.UI.PRG_EXP_BAR)).GetComponent<SkillGrowProgress>();
    if (this.isExceed)
    {
      component.SetExceedMode();
      float fill_amount = (float) (this.skillItem.exceedExp - this.skillItem.exceedExpPrev) / (float) (this.skillItem.exceedExpNext - this.skillItem.exceedExpPrev);
      component.SetBaseGauge(_ref.exceedCnt == skillItemInfo.exceedCnt, fill_amount);
      this.SetProgressInt(this.detailBase, (Enum) SmithGrowSkillSecond.UI.PRG_EXP_BAR, _ref.exceedExp, _ref.exceedExpPrev, _ref.exceedExpNext);
    }
    else
    {
      float fill_amount = (float) (this.skillItem.exp - this.skillItem.expPrev) / (float) (this.skillItem.expNext - this.skillItem.expPrev);
      component.SetGrowMode();
      component.SetBaseGauge(_ref.level == skillItemInfo.level, fill_amount);
      this.SetProgressInt(this.detailBase, (Enum) SmithGrowSkillSecond.UI.PRG_EXP_BAR, _ref.exp, _ref.expPrev, _ref.expNext);
    }
    this.UpdateAnchors();
  }

  private bool IsEnableSelect(SortCompareData item)
  {
    return item != null && !item.IsFavorite() && (long) item.GetUniqID() != (long) this.skillItem.uniqueID;
  }

  private void OnQuery_DECISION()
  {
    if (this.needGold > MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money)
      GameSection.ChangeEvent("NOT_ENOUGH_MONEY");
    else if (this.material == null || this.material.Length == 0)
      GameSection.ChangeEvent("NOT_MATERIAL");
    else if (this.skillItem.IsLevelMax() && !this.skillItem.IsExistNextExceed())
    {
      GameSection.ChangeEvent("NOT_INCLUDE_EXCEED");
    }
    else
    {
      this.isNoticeSendGrow = true;
      GameSection.SetEventData((object) new object[2]
      {
        (object) this.skillItem,
        (object) this.material
      });
    }
  }

  private void OnCloseDialog_SmithGrowSkillConfirm() => this.isNoticeSendGrow = false;

  private void OnQuery_DETAIL()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.SMITH_SKILL_GROW,
      (object) this.material[(int) GameSection.GetEventData()]
    });
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & (GameSection.NOTIFY_FLAG.UPDATE_SKILL_FAVORITE | GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY)) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.skillItem = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(this.skillItem.uniqueID);
      if (this.material != null)
      {
        List<SkillItemInfo> skillItemInfoList = new List<SkillItemInfo>();
        int index = 0;
        for (int length = this.material.Length; index < length; ++index)
        {
          SkillItemInfo skillItemInfo = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(this.material[index].uniqueID);
          if (skillItemInfo != null && !skillItemInfo.isFavorite)
            skillItemInfoList.Add(this.material[index]);
        }
        this.material = skillItemInfoList.ToArray();
      }
      this.SetDirty((Enum) SmithGrowSkillSecond.UI.GRD_MATERIAL);
    }
    base.OnNotify(flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return this.isNoticeSendGrow ? (GameSection.NOTIFY_FLAG) 0 : GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS | GameSection.NOTIFY_FLAG.UPDATE_SKILL_FAVORITE | GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_SKILL_INVENTORY;
  }

  private void OnQuery_SELECT()
  {
    GameSection.SetEventData((object) new object[4]
    {
      (object) this.skillItem,
      (object) this.material,
      (object) this.isExceed,
      (object) this.isSortTypeReset
    });
    this.isSortTypeReset = false;
  }

  private void OnQuery_SECTION_BACK()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("SmithGrowSkillSelect"))
      return;
    GameSection.StopEvent();
    this.TO_UNIQUE_OR_MAIN_STATUS();
  }

  protected new enum UI
  {
    OBJ_DETAIL_ROOT,
    TEX_MODEL,
    TEX_INNER_MODEL,
    LBL_NAME,
    LBL_LV_NOW,
    LBL_LV_MAX,
    OBJ_LV_EX,
    LBL_LV_EX,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    LBL_SELL,
    LBL_DESCRIPTION,
    OBJ_FAVORITE_ROOT,
    TWN_FAVORITE,
    TWN_UNFAVORITE,
    OBJ_SUB_STATUS,
    SPR_SKILL_TYPE_ICON,
    SPR_SKILL_TYPE_ICON_BG,
    SPR_SKILL_TYPE_ICON_RARITY,
    STR_TITLE_ITEM_INFO,
    STR_TITLE_DESCRIPTION,
    STR_TITLE_STATUS,
    STR_TITLE_SELL,
    PRG_EXP_BAR,
    OBJ_NEXT_EXP_ROOT,
    LBL_EQUIP_ITEM_NAME,
    GRD_MATERIAL,
    BTN_DECISION_ON,
    BTN_DECISION_OFF,
    BTN_BACK,
    OBJ_GOLD,
    LBL_GOLD,
    LBL_SELECT_NUM,
    STR_SELL,
    STR_TITLE_MATERIAL,
    STR_TITLE_MONEY,
    LBL_BASE_DESCRIPTION,
    LBL_NEXT_DESCRIPTION,
    SPR_STATUS_UP,
    SPR_BG_NORMAL,
    SPR_BG_EXCEED,
    OBJ_CAPTION,
    LBL_CAPTION,
  }
}
