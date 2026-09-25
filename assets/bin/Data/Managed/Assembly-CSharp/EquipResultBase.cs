// Decompiled with JetBrains decompiler
// Type: EquipResultBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public abstract class EquipResultBase : SmithEquipBase
{
  private static readonly float STATUS_WINDOW_DELAY = 0.5f;
  private AbilityDetailPopUp abilityDetailPopUp;
  private List<Transform> touchAndReleaseButtons = new List<Transform>();
  private int tabIndex;
  protected SmithManager.ResultData resultData;
  private Transform detailBase;
  private int noticeNum;
  private EquipItemAbility[] addAbility;
  private string[] exceedDescriptions;

  public override string overrideBackKeyEvent => "TO_SELECT";

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return base.GetUpdateUINotifyFlags() | GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE;
  }

  public override void Initialize()
  {
    this.tabIndex = 0;
    this.resultData = (SmithManager.ResultData) GameSection.GetEventData();
    this.type = SmithEquipBase.EquipDialogType.RESULT;
    base.Initialize();
  }

  protected override void OnOpen()
  {
    this.InitUITweener<UITweener>((Enum) EquipResultBase.UI.OBJ_DELAY, false);
    this.StartCoroutine(this.DelayedOpenStatus());
    MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = false;
    base.OnOpen();
  }

  private IEnumerator DelayedOpenStatus()
  {
    float t = EquipResultBase.STATUS_WINDOW_DELAY;
    while ((double) t > 0.0)
    {
      t -= Time.deltaTime;
      yield return (object) null;
    }
    ((Component) this.GetCtrl((Enum) EquipResultBase.UI.OBJ_DELAY)).GetComponent<UITweener>().PlayForward();
    bool flag = false;
    if (this.resultData != null)
    {
      EquipItemInfo itemData = this.resultData.itemData as EquipItemInfo;
      if (this.resultData.isExceed && itemData != null && itemData.exceed > 0)
        flag = true;
    }
    SoundManager.PlayOneShotUISE(flag ? 40000157 : 40000049);
  }

  public override void UpdateUI()
  {
    this.detailBase = this.SetPrefab(this.GetCtrl((Enum) EquipResultBase.UI.OBJ_DETAIL_ROOT), "ItemDetailEquipBase");
    this.SetFontStyle(this.detailBase, (Enum) EquipResultBase.UI.STR_TITLE_ITEM_INFO, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) EquipResultBase.UI.STR_TITLE_SKILL_SLOT, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) EquipResultBase.UI.STR_TITLE_STATUS, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) EquipResultBase.UI.STR_TITLE_ABILITY, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) EquipResultBase.UI.STR_TITLE_SELL, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) EquipResultBase.UI.STR_TITLE_ATK, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) EquipResultBase.UI.STR_TITLE_ELEM_ATK, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) EquipResultBase.UI.STR_TITLE_DEF, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) EquipResultBase.UI.STR_TITLE_ELEM_DEF, (FontStyle) 2);
    this.SetFontStyle(this.detailBase, (Enum) EquipResultBase.UI.STR_TITLE_HP, (FontStyle) 2);
    base.UpdateUI();
  }

  protected override void ResultEquipInfo()
  {
    if (this.resultData.itemData == null)
      return;
    EquipItemInfo item = this.resultData.itemData as EquipItemInfo;
    EquipItemTable.EquipItemData tableData = item.tableData;
    bool is_visible = tableData.IsVisual();
    this.SetActive(this.detailBase, (Enum) EquipResultBase.UI.BTN_SELL, false);
    this.SetActive(this.detailBase, (Enum) EquipResultBase.UI.BTN_GROW, false);
    this.SetActive(this.detailBase, (Enum) EquipResultBase.UI.BTN_GRAY, false);
    this.SetActive(this.detailBase, (Enum) EquipResultBase.UI.OBJ_FAVORITE_ROOT, false);
    this.SetActive(this.detailBase, (Enum) EquipResultBase.UI.SPR_IS_EVOLVE, item.tableData.IsEvolve());
    this.SetActive(this.detailBase, (Enum) EquipResultBase.UI.STR_LV, !is_visible);
    this.SetActive(this.detailBase, (Enum) EquipResultBase.UI.STR_ONLY_VISUAL, is_visible);
    this.SetupBottomButton();
    this.SetLabelText(this.detailBase, (Enum) EquipResultBase.UI.LBL_NAME, tableData.name);
    this.SetLabelText(this.detailBase, (Enum) EquipResultBase.UI.LBL_LV_MAX, tableData.maxLv.ToString());
    this.SetSprite(this.detailBase, (Enum) EquipResultBase.UI.SPR_SP_ATTACK_TYPE, tableData.IsWeapon() ? tableData.spAttackType.GetBigFrameSpriteName() : "");
    if (this.smithType == SmithEquipBase.SmithType.GROW)
    {
      string text = this.sectionData.GetText("STATUS_DIFF_FORMAT");
      this.SetLabelCompareParam(this.detailBase, (Enum) EquipResultBase.UI.LBL_LV_NOW, item.level, this.resultData.beforeLevel);
      this.SetLabelDiffParam(this.detailBase, (Enum) EquipResultBase.UI.LBL_AFTER_ATK, item.atk, (Enum) EquipResultBase.UI.LBL_DIFF_ATK, this.resultData.beforeAtk, (Enum) EquipResultBase.UI.LBL_ATK, text);
      this.SetLabelDiffParam(this.detailBase, (Enum) EquipResultBase.UI.LBL_AFTER_DEF, item.def, (Enum) EquipResultBase.UI.LBL_DIFF_DEF, this.resultData.beforeDef, (Enum) EquipResultBase.UI.LBL_DEF, text);
      this.SetLabelDiffParam(this.detailBase, (Enum) EquipResultBase.UI.LBL_AFTER_HP, item.hp, (Enum) EquipResultBase.UI.LBL_DIFF_HP, this.resultData.beforeHp, (Enum) EquipResultBase.UI.LBL_HP, text);
      this.SetLabelDiffParam(this.detailBase, (Enum) EquipResultBase.UI.LBL_AFTER_ELEM, item.elemAtk, (Enum) EquipResultBase.UI.LBL_DIFF_ELEM, this.resultData.beforeElemAtk, (Enum) EquipResultBase.UI.LBL_ELEM, text);
      this.SetDiffElementSprite(this.detailBase, item.GetElemAtkType(), this.resultData.beforeElemAtk, item.elemAtk, EquipResultBase.UI.SPR_ELEM, EquipResultBase.UI.SPR_DIFF_ELEM, true);
      int elemDef = item.elemDef;
      int beforeElemDef = this.resultData.beforeElemDef;
      if (item.tableData.isFormer)
      {
        elemDef = Mathf.FloorToInt((float) elemDef * 0.1f);
        beforeElemDef = Mathf.FloorToInt((float) beforeElemDef * 0.1f);
      }
      this.SetLabelDiffParam(this.detailBase, (Enum) EquipResultBase.UI.LBL_AFTER_ELEM_DEF, elemDef, (Enum) EquipResultBase.UI.LBL_DIFF_ELEM_DEF, beforeElemDef, (Enum) EquipResultBase.UI.LBL_ELEM_DEF, text);
      this.SetDiffElementSprite(this.detailBase, item.GetElemDefType(), this.resultData.beforeElemDef, item.elemDef, EquipResultBase.UI.SPR_ELEM_DEF, EquipResultBase.UI.SPR_DIFF_ELEM_DEF, false);
    }
    else
    {
      Transform detailBase1 = this.detailBase;
      // ISSUE: variable of a boxed type
      __Boxed<EquipResultBase.UI> label_enum1 = (Enum) EquipResultBase.UI.LBL_LV_NOW;
      int num = item.level;
      string text1 = num.ToString();
      this.SetLabelText(detailBase1, (Enum) label_enum1, text1);
      Transform detailBase2 = this.detailBase;
      // ISSUE: variable of a boxed type
      __Boxed<EquipResultBase.UI> label_enum2 = (Enum) EquipResultBase.UI.LBL_ATK;
      num = item.atk;
      string text2 = num.ToString();
      this.SetLabelText(detailBase2, (Enum) label_enum2, text2);
      Transform detailBase3 = this.detailBase;
      // ISSUE: variable of a boxed type
      __Boxed<EquipResultBase.UI> label_enum3 = (Enum) EquipResultBase.UI.LBL_DEF;
      num = item.def;
      string text3 = num.ToString();
      this.SetLabelText(detailBase3, (Enum) label_enum3, text3);
      Transform detailBase4 = this.detailBase;
      // ISSUE: variable of a boxed type
      __Boxed<EquipResultBase.UI> label_enum4 = (Enum) EquipResultBase.UI.LBL_HP;
      num = item.hp;
      string text4 = num.ToString();
      this.SetLabelText(detailBase4, (Enum) label_enum4, text4);
      Transform detailBase5 = this.detailBase;
      // ISSUE: variable of a boxed type
      __Boxed<EquipResultBase.UI> label_enum5 = (Enum) EquipResultBase.UI.LBL_ELEM;
      num = item.elemAtk;
      string text5 = num.ToString();
      this.SetLabelText(detailBase5, (Enum) label_enum5, text5);
      this.SetElementSprite(this.detailBase, (Enum) EquipResultBase.UI.SPR_ELEM, item.GetElemAtkType());
      int elemDef = item.elemDef;
      if (item.tableData.isFormer)
        elemDef = Mathf.FloorToInt((float) elemDef * 0.1f);
      this.SetLabelText(this.detailBase, (Enum) EquipResultBase.UI.LBL_ELEM_DEF, elemDef.ToString());
      this.SetDefElementSprite(this.detailBase, (Enum) EquipResultBase.UI.SPR_ELEM_DEF, item.GetElemDefType());
    }
    this.SetSkillIconButton(this.detailBase, (Enum) EquipResultBase.UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButton", item.tableData, this.GetSkillSlotData(item));
    this.SetLabelText(this.detailBase, (Enum) EquipResultBase.UI.LBL_SELL, tableData.sale.ToString());
    this.SetEquipmentTypeIcon(this.detailBase, (Enum) EquipResultBase.UI.SPR_TYPE_ICON, (Enum) EquipResultBase.UI.SPR_TYPE_ICON_BG, (Enum) EquipResultBase.UI.SPR_TYPE_ICON_RARITY, item.tableData);
    AbilityItemInfo abilityItem = item.GetAbilityItem();
    bool flag = abilityItem != null;
    if (((item.ability == null ? 0 : (item.ability.Length != 0 ? 1 : 0)) | (flag ? 1 : 0)) != 0)
    {
      bool empty_ability = true;
      string allAbilityName = "";
      string allAp = "";
      string allAbilityDesc = "";
      this.SetTable(this.detailBase, (Enum) EquipResultBase.UI.TBL_ABILITY, "ItemDetailEquipAbilityItem", item.ability.Length + (flag ? 1 : 0), false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        if (i < item.ability.Length)
        {
          EquipItemAbility equipItemAbility = item.ability[i];
          if (equipItemAbility.id == 0U)
          {
            this.SetActive(t, false);
          }
          else
          {
            empty_ability = false;
            this.SetActive(t, true);
            if (equipItemAbility.IsNeedUpdate())
            {
              this.SetActive(t, (Enum) EquipResultBase.UI.OBJ_ABILITY, false);
              this.SetActive(t, (Enum) EquipResultBase.UI.OBJ_FIXEDABILITY, false);
              this.SetActive(t, (Enum) EquipResultBase.UI.OBJ_NEED_UPDATE_ABILITY, true);
              this.SetButtonEnabled(t, false);
            }
            else if (item.IsFixedAbility(i))
            {
              this.SetActive(t, (Enum) EquipResultBase.UI.OBJ_ABILITY, false);
              this.SetActive(t, (Enum) EquipResultBase.UI.OBJ_FIXEDABILITY, true);
              this.SetLabelText(t, (Enum) EquipResultBase.UI.LBL_FIXEDABILITY, equipItemAbility.GetName());
              this.SetLabelText(t, (Enum) EquipResultBase.UI.LBL_FIXEDABILITY_NUM, equipItemAbility.GetAP());
            }
            else
            {
              this.SetLabelText(t, (Enum) EquipResultBase.UI.LBL_ABILITY, equipItemAbility.GetName());
              this.SetLabelText(t, (Enum) EquipResultBase.UI.LBL_ABILITY_NUM, equipItemAbility.GetAP());
            }
            this.SetAbilityItemEvent(t, i, this.touchAndReleaseButtons);
            allAbilityName += equipItemAbility.GetName();
            allAp += equipItemAbility.GetAP();
            allAbilityDesc += equipItemAbility.GetDescription();
          }
        }
        else
        {
          this.SetActive(t, (Enum) EquipResultBase.UI.OBJ_ABILITY, false);
          this.SetActive(t, (Enum) EquipResultBase.UI.OBJ_ABILITY_ITEM, true);
          this.SetLabelText(t, (Enum) EquipResultBase.UI.LBL_ABILITY_ITEM, abilityItem.GetName());
          this.SetTouchAndRelease(((Component) ((Component) t).GetComponentInChildren<UIButton>()).transform, "ABILITY_ITEM_DATA_POPUP", "RELEASE_ABILITY", (object) t);
          allAbilityName += abilityItem.GetName();
          allAbilityDesc += abilityItem.GetDescription();
        }
      }));
      this.PreCacheAbilityDetail(allAbilityName, allAp, allAbilityDesc);
      if (empty_ability)
        this.SetActive(this.detailBase, (Enum) EquipResultBase.UI.STR_NON_ABILITY, true);
      else
        this.SetActive(this.detailBase, (Enum) EquipResultBase.UI.STR_NON_ABILITY, false);
    }
    else
      this.SetActive(this.detailBase, (Enum) EquipResultBase.UI.STR_NON_ABILITY, true);
  }

  private void SetDiffElementSprite(
    Transform t,
    int elem_type,
    int before,
    int after,
    EquipResultBase.UI no_diff,
    EquipResultBase.UI diff,
    bool is_weapon)
  {
    bool is_visible = before != after;
    this.SetActive((Enum) no_diff, !is_visible);
    this.SetActive((Enum) diff, is_visible);
    if (is_weapon)
      this.SetElementSprite(t, (Enum) (EquipResultBase.UI) (is_visible ? (int) diff : (int) no_diff), elem_type);
    else
      this.SetDefElementSprite(t, (Enum) (EquipResultBase.UI) (is_visible ? (int) diff : (int) no_diff), elem_type);
  }

  private void SetupBottomButton()
  {
    Transform ctrl = this.GetCtrl((Enum) EquipResultBase.UI.BTN_NEXT);
    switch (this.smithType)
    {
      case SmithEquipBase.SmithType.GENERATE:
        ctrl.localPosition = new Vector3(-34f, ctrl.localPosition.y, ctrl.localPosition.z);
        this.SetActive((Enum) EquipResultBase.UI.BTN_NEXT_GRAY, false);
        this.SetActive((Enum) EquipResultBase.UI.BTN_NEXT_RIGHT, true);
        this.SetEventName((Enum) EquipResultBase.UI.BTN_NEXT_RIGHT, "TO_GROW");
        this.SetActive((Enum) EquipResultBase.UI.BTN_TO_SELECT, true);
        this.SetActive((Enum) EquipResultBase.UI.BTN_TO_SELECT_CENTER, false);
        this.SetLabelText((Enum) EquipResultBase.UI.LBL_NEXT_BTN, this.sectionData.GetText("CONTINUE"));
        break;
      case SmithEquipBase.SmithType.GROW:
        ctrl.localPosition = new Vector3(0.0f, ctrl.localPosition.y, ctrl.localPosition.z);
        this.SetActive((Enum) EquipResultBase.UI.BTN_NEXT_GRAY, false);
        this.SetActive((Enum) EquipResultBase.UI.BTN_NEXT_RIGHT, false);
        this.SetActive((Enum) EquipResultBase.UI.BTN_TO_SELECT, true);
        this.SetActive((Enum) EquipResultBase.UI.BTN_TO_SELECT_CENTER, false);
        bool flag = false;
        if (this.resultData.itemData is EquipItemInfo itemData1 && itemData1.IsLevelMax())
        {
          if (itemData1.tableData.IsEvolve())
          {
            this.SetActive((Enum) EquipResultBase.UI.BTN_NEXT, true);
            this.SetActive((Enum) EquipResultBase.UI.BTN_NEXT_GRAY, false);
            this.SetEvent((Enum) EquipResultBase.UI.BTN_NEXT, "NEXT_EVOLVE_AUTO", 0);
            flag = true;
          }
          else if (!itemData1.IsExceedMax() || itemData1.tableData.IsShadow())
          {
            this.SetActive((Enum) EquipResultBase.UI.BTN_NEXT, true);
            this.SetActive((Enum) EquipResultBase.UI.BTN_NEXT_GRAY, false);
          }
          else
          {
            this.SetActive((Enum) EquipResultBase.UI.BTN_NEXT, false);
            this.SetActive((Enum) EquipResultBase.UI.BTN_NEXT_GRAY, true);
          }
        }
        if (flag)
        {
          this.SetLabelText((Enum) EquipResultBase.UI.LBL_NEXT_BTN, this.sectionData.GetText("NEXT_EVOLVE"));
          this.SetLabelText((Enum) EquipResultBase.UI.LBL_NEXT_GRAY_BTN, this.sectionData.GetText("NEXT_EVOLVE"));
          break;
        }
        this.SetLabelText((Enum) EquipResultBase.UI.LBL_NEXT_BTN, this.sectionData.GetText("CONTINUE"));
        this.SetLabelText((Enum) EquipResultBase.UI.LBL_NEXT_GRAY_BTN, this.sectionData.GetText("CONTINUE"));
        break;
      case SmithEquipBase.SmithType.EVOLVE:
        this.SetActive((Enum) EquipResultBase.UI.BTN_NEXT, false);
        this.SetActive((Enum) EquipResultBase.UI.BTN_NEXT_GRAY, false);
        this.SetActive((Enum) EquipResultBase.UI.BTN_NEXT_RIGHT, false);
        this.SetActive((Enum) EquipResultBase.UI.BTN_TO_SELECT, false);
        this.SetActive((Enum) EquipResultBase.UI.BTN_TO_SELECT_CENTER, true);
        if (this.resultData.itemData is EquipItemInfo itemData2 && (!itemData2.IsLevelMax() || !itemData2.IsExceedMax() || itemData2.tableData.IsShadow()))
        {
          this.SetActive((Enum) EquipResultBase.UI.BTN_NEXT, true);
          ctrl.localPosition = new Vector3(0.0f, ctrl.localPosition.y, ctrl.localPosition.z);
          this.SetEvent((Enum) EquipResultBase.UI.BTN_NEXT, "NEXT_GROW_AUTO", 0);
          this.SetActive((Enum) EquipResultBase.UI.BTN_TO_SELECT, true);
          this.SetActive((Enum) EquipResultBase.UI.BTN_TO_SELECT_CENTER, false);
          this.SetLabelText((Enum) EquipResultBase.UI.LBL_NEXT_BTN, this.sectionData.GetText("CONTINUE"));
          break;
        }
        break;
      case SmithEquipBase.SmithType.SKILL_GROW:
        ctrl.localPosition = new Vector3(0.0f, ctrl.localPosition.y, ctrl.localPosition.z);
        this.SetActive((Enum) EquipResultBase.UI.BTN_NEXT_GRAY, false);
        this.SetActive((Enum) EquipResultBase.UI.BTN_NEXT_RIGHT, false);
        this.SetActive((Enum) EquipResultBase.UI.BTN_TO_SELECT, true);
        this.SetActive((Enum) EquipResultBase.UI.BTN_TO_SELECT_CENTER, false);
        if (this.resultData.itemData is SkillItemInfo itemData3 && itemData3.IsLevelMax())
        {
          this.SetActive((Enum) EquipResultBase.UI.BTN_NEXT, false);
          this.SetActive((Enum) EquipResultBase.UI.BTN_NEXT_GRAY, true);
        }
        this.SetLabelText((Enum) EquipResultBase.UI.LBL_NEXT_BTN, this.sectionData.GetText("CONTINUE"));
        this.SetLabelText((Enum) EquipResultBase.UI.LBL_NEXT_GRAY_BTN, this.sectionData.GetText("CONTINUE"));
        break;
    }
    this.SetLabelText((Enum) EquipResultBase.UI.LBL_NEXT_BTN_R, this.GetComponent<UILabel>((Enum) EquipResultBase.UI.LBL_NEXT_BTN).text);
    this.SetLabelText((Enum) EquipResultBase.UI.LBL_NEXT_GRAY_BTN_R, this.GetComponent<UILabel>((Enum) EquipResultBase.UI.LBL_NEXT_GRAY_BTN).text);
    this.SetLabelText((Enum) EquipResultBase.UI.LBL_TO_SELECT_CENTER_R, this.GetComponent<UILabel>((Enum) EquipResultBase.UI.LBL_TO_SELECT_CENTER).text);
  }

  protected override void EquipImg()
  {
    if (this.smithType != SmithEquipBase.SmithType.SKILL_GROW)
      this.SetRenderEquipModel((Enum) EquipResultBase.UI.TEX_MODEL, (this.resultData.itemData as EquipItemInfo).tableID);
    else
      this.SetRenderSkillItemModel((Enum) EquipResultBase.UI.TEX_MODEL, (this.resultData.itemData as SkillItemInfo).tableID);
  }

  private void OnQuery_SKILL_ICON_BUTTON()
  {
    if (this.tabIndex != 0)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) new object[2]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.SMITH_GROW,
        (object) (this.resultData.itemData as EquipItemInfo)
      });
  }

  private void OnQuery_ABILITY()
  {
    int eventData = (int) GameSection.GetEventData();
    EquipItemAbility event_data = (EquipItemAbility) null;
    if (this.resultData.itemData is EquipItemInfo itemData)
      event_data = new EquipItemAbility(itemData.ability[eventData].id, -1);
    else if (!(this.resultData.itemData is SkillItemInfo))
      Debug.LogError((object) $"err : result data is unknown : atk {(object) this.resultData.beforeAtk} : def {(object) this.resultData.beforeDef}");
    if (event_data == null)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) event_data);
  }

  private void OnQuery_NEXT_EVOLVE_AUTO()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
    {
      new EventData("NEXT_EVOLVE", (object) 1),
      new EventData("TRY_ON", (object) (this.resultData.itemData as EquipItemInfo).uniqueID)
    });
  }

  private void OnQuery_NEXT_GROW_AUTO()
  {
    if (!MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("SmithGrowItemSelect"))
      GameSection.ChangeEvent("CONTINUE_GROW");
    else
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[3]
      {
        new EventData("TO_SELECT", (object) 1),
        new EventData("TRY_ON", (object) (this.resultData.itemData as EquipItemInfo).uniqueID),
        new EventData("CLEARLEVEL")
      });
  }

  protected void StartAddAbilityDirection(EquipItemAbility[] abulity)
  {
    if (abulity == null || abulity.Length == 0)
      return;
    this.addAbility = abulity;
    this.noticeNum = abulity.Length;
    this.SetActive((Enum) EquipResultBase.UI.SPR_TITLE_ABILITY, true);
    this.SetActive((Enum) EquipResultBase.UI.SPR_TITLE_EXCEED, false);
    this.OnFinishedAddAbilityDirection();
  }

  public void OnFinishedAddAbilityDirection()
  {
    if (this.noticeNum > 0)
    {
      this.SetFontStyle((Enum) EquipResultBase.UI.LBL_ADD_ABILITY, (FontStyle) 2);
      this.SetLabelText((Enum) EquipResultBase.UI.LBL_ADD_ABILITY, this.addAbility[this.addAbility.Length - this.noticeNum].GetNameAndAP());
      --this.noticeNum;
      this.SetActive((Enum) EquipResultBase.UI.OBJ_ADD_ABILITY, true);
      this.ResetTween((Enum) EquipResultBase.UI.OBJ_ADD_ABILITY);
      this.PlayTween((Enum) EquipResultBase.UI.OBJ_ADD_ABILITY, callback: new EventDelegate.Callback(this.OnFinishedAddAbilityDirection), is_input_block: false);
    }
    else
      this.SetActive((Enum) EquipResultBase.UI.OBJ_ADD_ABILITY, false);
  }

  protected void StartExceedDirection(string[] descriptions)
  {
    if (descriptions == null || descriptions.Length == 0)
      return;
    this.exceedDescriptions = descriptions;
    this.noticeNum = descriptions.Length;
    this.SetActive((Enum) EquipResultBase.UI.SPR_TITLE_ABILITY, false);
    this.SetActive((Enum) EquipResultBase.UI.SPR_TITLE_EXCEED, true);
    this.OnFinishedExceedDirection();
  }

  public void OnFinishedExceedDirection()
  {
    if (this.noticeNum > 0)
    {
      this.SetFontStyle((Enum) EquipResultBase.UI.LBL_ADD_ABILITY, (FontStyle) 2);
      this.SetLabelText((Enum) EquipResultBase.UI.LBL_ADD_ABILITY, this.exceedDescriptions[this.exceedDescriptions.Length - this.noticeNum]);
      --this.noticeNum;
      this.ResetTween((Enum) EquipResultBase.UI.SPR_TITLE_EXCEED);
      this.PlayTween((Enum) EquipResultBase.UI.SPR_TITLE_EXCEED, is_input_block: false);
      EquipItemInfo itemData = this.resultData.itemData as EquipItemInfo;
      int tween_ctrl_id = 1;
      for (int index = 4; tween_ctrl_id <= index; ++tween_ctrl_id)
      {
        this.ResetTween((Enum) EquipResultBase.UI.SPR_TITLE_EXCEED, tween_ctrl_id);
        if (tween_ctrl_id < itemData.exceed)
          this.SkipTween((Enum) EquipResultBase.UI.SPR_TITLE_EXCEED, tween_ctrl_id: tween_ctrl_id);
        else if (tween_ctrl_id == itemData.exceed)
          this.PlayTween((Enum) EquipResultBase.UI.SPR_TITLE_EXCEED, is_input_block: false, tween_ctrl_id: tween_ctrl_id);
      }
      this.SetActive((Enum) EquipResultBase.UI.OBJ_ADD_ABILITY, true);
      this.ResetTween((Enum) EquipResultBase.UI.OBJ_ADD_ABILITY, 1);
      this.PlayTween((Enum) EquipResultBase.UI.OBJ_ADD_ABILITY, is_input_block: false, tween_ctrl_id: 1);
    }
    else
      this.SetActive((Enum) EquipResultBase.UI.OBJ_ADD_ABILITY, false);
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
    EquipItemAbility ability = (this.resultData.itemData as EquipItemInfo).ability[(int) eventData[0]];
    Transform targetTrans = eventData[1] as Transform;
    if (Object.op_Equality((Object) this.abilityDetailPopUp, (Object) null))
      this.abilityDetailPopUp = this.CreateAndGetAbilityDetail((Enum) EquipResultBase.UI.OBJ_DETAIL_ROOT);
    this.abilityDetailPopUp.ShowAbilityDetail(targetTrans);
    this.abilityDetailPopUp.SetAbilityDetailText(ability);
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
      this.abilityDetailPopUp = this.CreateAndGetAbilityDetail((Enum) EquipResultBase.UI.OBJ_DETAIL_ROOT);
    this.abilityDetailPopUp.PreCacheAbilityDetail(name, ap, desc);
  }

  private void OnQuery_TO_GROW()
  {
    EquipItemInfo itemData = this.resultData.itemData as EquipItemInfo;
    if (itemData.IsLevelMax())
    {
      if (itemData.tableData.IsEvolve())
      {
        MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>().selectEquipData = itemData;
        GameSection.ChangeEvent("EVOLVE");
        return;
      }
      if (itemData.IsExceedMax() && !itemData.tableData.IsShadow())
      {
        GameSection.ChangeEvent("ALREADY_LV_MAX");
        return;
      }
    }
    MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>().selectEquipData = itemData;
  }

  private enum UI
  {
    BTN_NEXT,
    BTN_NEXT_GRAY,
    BTN_TO_SELECT,
    BTN_TO_SELECT_CENTER,
    LBL_NEXT_BTN,
    LBL_NEXT_GRAY_BTN,
    LBL_TO_SELECT,
    LBL_TO_SELECT_CENTER,
    LBL_NEXT_BTN_R,
    LBL_NEXT_GRAY_BTN_R,
    LBL_TO_SELECT_R,
    LBL_TO_SELECT_CENTER_R,
    OBJ_ADD_ABILITY,
    LBL_ADD_ABILITY,
    SPR_TITLE_ABILITY,
    SPR_TITLE_EXCEED,
    OBJ_DETAIL_ROOT,
    TEX_MODEL,
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
    LBL_AFTER_ATK,
    LBL_AFTER_DEF,
    LBL_AFTER_HP,
    LBL_AFTER_ELEM,
    LBL_AFTER_ELEM_DEF,
    LBL_DIFF_ATK,
    LBL_DIFF_DEF,
    LBL_DIFF_HP,
    LBL_DIFF_ELEM,
    LBL_DIFF_ELEM_DEF,
    SPR_DIFF_ELEM,
    SPR_DIFF_ELEM_DEF,
    LBL_SELL,
    OBJ_SKILL_BUTTON_ROOT,
    BTN_SELL,
    BTN_GROW,
    BTN_GRAY,
    LBL_GRAY_BTN,
    OBJ_FAVORITE_ROOT,
    SPR_FAVORITE,
    SPR_UNFAVORITE,
    SPR_IS_EVOLVE,
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
    STR_TITLE_ELEM_ATK,
    STR_TITLE_DEF,
    STR_TITLE_ELEM_DEF,
    STR_TITLE_HP,
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
    OBJ_DELAY,
    OBJ_NEED_UPDATE_ABILITY,
    LBL_NEED_UPDATE_ABILITY,
    SPR_SP_ATTACK_TYPE,
    BTN_NEXT_RIGHT,
  }

  public enum AUDIO
  {
    RESULT = 40000049, // 0x02625A31
    RESULT_EXCEEED = 40000157, // 0x02625A9D
  }
}
