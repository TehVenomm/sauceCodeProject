// Decompiled with JetBrains decompiler
// Type: SmithEvolve
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SmithEvolve : EquipGenerateBase
{
  private List<SmithEvolve.AdapterAbility> adapterAbilityList = new List<SmithEvolve.AdapterAbility>();
  private string modifyText;
  private Vector3 defaultCenterPos;
  private Vector3 defaultRightPos;
  private Vector3 defaultLeftPos;
  private TweenPosition centerAnim;
  private TweenPosition rightAnim;
  private TweenPosition leftAnim;
  private bool isButtonChange;
  private bool isRightChange;
  private Transform[] itemModels;
  private Transform itemModelRoot;
  private float targetAngle;
  private float nowAngle;
  private float rotateSign;
  private float localRotateWait;
  private float localRotate;
  private bool isModelScrolling;
  private const float RADIUS = 3f;
  private const float SPEED = 4f;
  private const float LOCAL_ROTATE_START = 1f;
  private const float LOCAL_ROTATE_SPEED = 10f;
  private List<TweenPosition> animPosList;
  private Vector3 defaultModelPos;

  public override void Initialize()
  {
    SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
    EquipItemInfo selectEquipData = smithData.selectEquipData;
    if (selectEquipData != null)
    {
      EvolveEquipItemTable.EvolveEquipItemData[] evolveTable = selectEquipData.tableData.GetEvolveTable();
      if (evolveTable != null)
      {
        SmithManager.SmithEvolveData smithEvolveData = new SmithManager.SmithEvolveData();
        smithEvolveData.selectIndex = 0;
        smithEvolveData.evolveBeforeEquipData = selectEquipData;
        smithEvolveData.evolveTable = evolveTable;
        smithEvolveData.evolveEquipDataTable = new EquipItemTable.EquipItemData[evolveTable.Length];
        for (int index = 0; index < evolveTable.Length; ++index)
          smithEvolveData.evolveEquipDataTable[index] = Singleton<EquipItemTable>.I.GetEquipItemData(evolveTable[index].equipEvolveItemID);
        smithData.evolveData = smithEvolveData;
      }
    }
    Transform root = smithData.selectEquipData.tableData.IsWeapon() ? this.GetCtrl((Enum) SmithEvolve.UI.OBJ_WEAPON_ROOT) : this.GetCtrl((Enum) SmithEvolve.UI.OBJ_ARMOR_ROOT);
    Transform ctrl1 = this.FindCtrl(root, (Enum) SmithEvolve.UI.OBJ_ORDER_CENTER_ANIM_ROOT);
    Transform ctrl2 = this.FindCtrl(root, (Enum) SmithEvolve.UI.OBJ_ORDER_L_ANIM_ROOT);
    Transform ctrl3 = this.FindCtrl(root, (Enum) SmithEvolve.UI.OBJ_ORDER_R_ANIM_ROOT);
    this.defaultCenterPos = ctrl1.localPosition;
    this.defaultRightPos = ctrl2.localPosition;
    this.defaultLeftPos = ctrl3.localPosition;
    this.centerAnim = ((Component) ctrl1).gameObject.AddComponent<TweenPosition>();
    this.rightAnim = ((Component) ctrl3).gameObject.AddComponent<TweenPosition>();
    this.leftAnim = ((Component) ctrl2).gameObject.AddComponent<TweenPosition>();
    this.smithType = SmithEquipBase.SmithType.EVOLVE;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
    Transform root = smithData.selectEquipData.tableData.IsWeapon() ? this.GetCtrl((Enum) SmithEvolve.UI.OBJ_WEAPON_ROOT) : this.GetCtrl((Enum) SmithEvolve.UI.OBJ_ARMOR_ROOT);
    bool is_visible = smithData.evolveData.evolveEquipDataTable.Length > 1;
    this.SetActive(root, (Enum) SmithEvolve.UI.BTN_EVO_L_INACTIVE, !is_visible);
    this.SetActive(root, (Enum) SmithEvolve.UI.BTN_EVO_R_INACTIVE, !is_visible);
    this.SetColor(root, (Enum) SmithEvolve.UI.SPR_EVO_L, is_visible ? Color.white : Color.clear);
    this.SetColor(root, (Enum) SmithEvolve.UI.SPR_EVO_R, is_visible ? Color.white : Color.clear);
    this.SetLabelText((Enum) SmithEvolve.UI.LBL_EVO_INDEX, (smithData.evolveData.selectIndex + 1).ToString());
    this.SetLabelText((Enum) SmithEvolve.UI.LBL_EVO_INDEX_MAX, smithData.evolveData.evolveTable.Length.ToString());
    int selectIndex = smithData.evolveData.selectIndex;
    this.SetActive(root, (Enum) SmithEvolve.UI.OBJ_ORDER_NORMAL_CENTER, true);
    this.SetActive(root, (Enum) SmithEvolve.UI.OBJ_ORDER_ATTRIBUTE_CENTER, true);
    this.SetActive(root, (Enum) SmithEvolve.UI.OBJ_ORDER_NORMAL_R, true);
    this.SetActive(root, (Enum) SmithEvolve.UI.OBJ_ORDER_NORMAL_L, true);
    this.SetActive(root, (Enum) SmithEvolve.UI.OBJ_ORDER_ATTRIBUTE_R, true);
    this.SetActive(root, (Enum) SmithEvolve.UI.OBJ_ORDER_ATTRIBUTE_L, true);
    this.SetActive(root, (Enum) SmithEvolve.UI.OBJ_ORDER_L2, true);
    this.SetActive(root, (Enum) SmithEvolve.UI.OBJ_ORDER_R2, true);
    this.SetActive(root, (Enum) SmithEvolve.UI.BTN_EVO_R2, is_visible);
    this.SetActive(root, (Enum) SmithEvolve.UI.BTN_EVO_L2, is_visible);
    this.SetActive(root, (Enum) SmithEvolve.UI.BTN_EVO_L2_INACTIVE, !is_visible);
    this.SetActive(root, (Enum) SmithEvolve.UI.BTN_EVO_R2_INACTIVE, !is_visible);
    this.SetEvolveText(smithData.evolveData.evolveEquipDataTable[selectIndex]);
    if (!this.isButtonChange)
      this.SetModifyPanel(smithData);
    else if (this.isRightChange)
      this.StartCoroutine("ChangePanelRight", (object) smithData);
    else
      this.StartCoroutine("ChangePanelLeft", (object) smithData);
    this.isButtonChange = false;
    base.UpdateUI();
  }

  private void SetModifyPanel(SmithManager.SmithGrowData smith_data)
  {
    int length = smith_data.evolveData.evolveEquipDataTable.Length;
    int selectIndex = smith_data.evolveData.selectIndex;
    Transform transform = smith_data.evolveData.evolveEquipDataTable[selectIndex].IsWeapon() ? this.GetCtrl((Enum) SmithEvolve.UI.OBJ_WEAPON_ROOT) : this.GetCtrl((Enum) SmithEvolve.UI.OBJ_ARMOR_ROOT);
    switch (length)
    {
      case 0:
        this.SetActive((Enum) SmithEvolve.UI.OBJ_EVOLVE_ROOT, false);
        break;
      case 1:
        this.SetModifyCenterPanel(smith_data.evolveData.evolveEquipDataTable[selectIndex], transform);
        this.SetActive(transform, (Enum) SmithEvolve.UI.OBJ_ORDER_L2, false);
        this.SetActive(transform, (Enum) SmithEvolve.UI.OBJ_ORDER_R2, false);
        break;
      case 2:
        this.SetModifyCenterPanel(smith_data.evolveData.evolveEquipDataTable[selectIndex], transform);
        if (selectIndex == 0)
        {
          this.SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex + 1], true, transform);
          this.SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex + 1], false, transform);
          break;
        }
        this.SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex - 1], true, transform);
        this.SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex - 1], false, transform);
        break;
      default:
        if (length <= 2)
          break;
        this.SetModifyCenterPanel(smith_data.evolveData.evolveEquipDataTable[selectIndex], transform);
        if (selectIndex == 0)
        {
          this.SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex + 1], true, transform);
          this.SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[length - 1], false, transform);
          break;
        }
        if (selectIndex == length - 1)
        {
          this.SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[0], true, transform);
          this.SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex - 1], false, transform);
          break;
        }
        this.SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex + 1], true, transform);
        this.SetModifySidePanel(smith_data.evolveData.evolveEquipDataTable[selectIndex - 1], false, transform);
        break;
    }
  }

  private IEnumerator ChangePanelLeft(SmithManager.SmithGrowData data)
  {
    ((Behaviour) this.centerAnim).enabled = true;
    this.centerAnim.ResetToBeginning();
    this.centerAnim.duration = 0.1f;
    this.centerAnim.from = this.defaultCenterPos;
    this.centerAnim.to = new Vector3(this.defaultCenterPos.x + 200f, this.defaultCenterPos.y, this.defaultCenterPos.z);
    this.centerAnim.PlayForward();
    ((Behaviour) this.rightAnim).enabled = true;
    this.rightAnim.ResetToBeginning();
    this.rightAnim.duration = 0.1f;
    this.rightAnim.from = this.defaultRightPos;
    this.rightAnim.to = new Vector3(this.defaultRightPos.x + 200f, this.defaultRightPos.y, this.defaultRightPos.z);
    this.rightAnim.PlayForward();
    ((Behaviour) this.leftAnim).enabled = true;
    this.leftAnim.ResetToBeginning();
    this.leftAnim.duration = 0.1f;
    this.leftAnim.from = this.defaultLeftPos;
    this.leftAnim.to = new Vector3(this.defaultLeftPos.x + 200f, this.defaultLeftPos.y, this.defaultLeftPos.z);
    this.leftAnim.PlayForward();
    while (((Behaviour) this.centerAnim).enabled)
      yield return (object) null;
    this.SetModifyPanel(data);
    ((Behaviour) this.centerAnim).enabled = true;
    this.centerAnim.ResetToBeginning();
    this.centerAnim.duration = 0.1f;
    this.centerAnim.from = new Vector3(this.defaultCenterPos.x - 200f, this.defaultCenterPos.y, this.defaultCenterPos.z);
    this.centerAnim.to = this.defaultCenterPos;
    this.centerAnim.PlayForward();
    ((Behaviour) this.rightAnim).enabled = true;
    this.rightAnim.ResetToBeginning();
    this.rightAnim.duration = 0.1f;
    this.rightAnim.from = new Vector3(this.defaultRightPos.x - 200f, this.defaultRightPos.y, this.defaultRightPos.z);
    this.rightAnim.to = this.defaultRightPos;
    this.rightAnim.PlayForward();
    ((Behaviour) this.leftAnim).enabled = true;
    this.leftAnim.ResetToBeginning();
    this.leftAnim.duration = 0.1f;
    this.leftAnim.from = new Vector3(this.defaultLeftPos.x - 200f, this.defaultLeftPos.y, this.defaultLeftPos.z);
    this.leftAnim.to = this.defaultLeftPos;
    this.leftAnim.PlayForward();
  }

  private IEnumerator ChangePanelRight(SmithManager.SmithGrowData data)
  {
    ((Behaviour) this.centerAnim).enabled = true;
    this.centerAnim.ResetToBeginning();
    this.centerAnim.duration = 0.1f;
    this.centerAnim.from = this.defaultCenterPos;
    this.centerAnim.to = new Vector3(this.defaultCenterPos.x - 200f, this.defaultCenterPos.y, this.defaultCenterPos.z);
    this.centerAnim.PlayForward();
    ((Behaviour) this.rightAnim).enabled = true;
    this.rightAnim.ResetToBeginning();
    this.rightAnim.duration = 0.1f;
    this.rightAnim.from = this.defaultRightPos;
    this.rightAnim.to = new Vector3(this.defaultRightPos.x - 200f, this.defaultRightPos.y, this.defaultRightPos.z);
    this.rightAnim.PlayForward();
    ((Behaviour) this.leftAnim).enabled = true;
    this.leftAnim.ResetToBeginning();
    this.leftAnim.duration = 0.1f;
    this.leftAnim.from = this.defaultLeftPos;
    this.leftAnim.to = new Vector3(this.defaultLeftPos.x - 200f, this.defaultLeftPos.y, this.defaultLeftPos.z);
    this.leftAnim.PlayForward();
    while (((Behaviour) this.centerAnim).enabled)
      yield return (object) null;
    this.SetModifyPanel(data);
    ((Behaviour) this.centerAnim).enabled = true;
    this.centerAnim.ResetToBeginning();
    this.centerAnim.duration = 0.1f;
    this.centerAnim.from = new Vector3(this.defaultCenterPos.x + 200f, this.defaultCenterPos.y, this.defaultCenterPos.z);
    this.centerAnim.to = this.defaultCenterPos;
    this.centerAnim.PlayForward();
    ((Behaviour) this.rightAnim).enabled = true;
    this.rightAnim.ResetToBeginning();
    this.rightAnim.duration = 0.1f;
    this.rightAnim.from = new Vector3(this.defaultRightPos.x + 200f, this.defaultRightPos.y, this.defaultRightPos.z);
    this.rightAnim.to = this.defaultRightPos;
    this.rightAnim.PlayForward();
    ((Behaviour) this.leftAnim).enabled = true;
    this.leftAnim.ResetToBeginning();
    this.leftAnim.duration = 0.1f;
    this.leftAnim.from = new Vector3(this.defaultLeftPos.x + 200f, this.defaultLeftPos.y, this.defaultLeftPos.z);
    this.leftAnim.to = this.defaultLeftPos;
    this.leftAnim.PlayForward();
  }

  private void SetEvolveText(EquipItemTable.EquipItemData data)
  {
    this.modifyText = StringTable.Get(STRING_CATEGORY.EVOLVE, (data.IsWeapon() ? 1 : 0) == 0 ? (uint) data.GetElemDefTypePriorityToTable() : (uint) data.GetElemAtkTypePriorityToTable());
  }

  private void SetModifyCenterPanel(EquipItemTable.EquipItemData data, Transform rootObj)
  {
    bool is_visible = data.IsWeapon();
    this.SetActive((Enum) SmithEvolve.UI.OBJ_ARMOR_ROOT, !is_visible);
    this.SetActive((Enum) SmithEvolve.UI.OBJ_WEAPON_ROOT, is_visible);
    int typePriorityToTable;
    if (is_visible)
    {
      typePriorityToTable = data.GetElemAtkTypePriorityToTable();
      string typeTextSpriteName = data.spAttackType.GetSpTypeTextSpriteName();
      this.SetSprite(rootObj, (Enum) SmithEvolve.UI.SPR_ORDER_ACTIONTYPE_CENTER, typeTextSpriteName);
    }
    else
      typePriorityToTable = data.GetElemDefTypePriorityToTable();
    if (typePriorityToTable == 6 || typePriorityToTable == 6)
    {
      this.SetActive(rootObj, (Enum) SmithEvolve.UI.OBJ_ORDER_ATTRIBUTE_CENTER, false);
    }
    else
    {
      this.SetElementSprite(rootObj, (Enum) SmithEvolve.UI.SPR_ORDER_ELEM_CENTER, typePriorityToTable);
      this.SetActive(rootObj, (Enum) SmithEvolve.UI.OBJ_ORDER_NORMAL_CENTER, false);
    }
  }

  private void SetModifySidePanel(
    EquipItemTable.EquipItemData data,
    bool isRight,
    Transform rootObj)
  {
    bool is_visible = data.IsWeapon();
    this.SetActive((Enum) SmithEvolve.UI.OBJ_ARMOR_ROOT, !is_visible);
    this.SetActive((Enum) SmithEvolve.UI.OBJ_WEAPON_ROOT, is_visible);
    int typePriorityToTable;
    if (is_visible)
    {
      typePriorityToTable = data.GetElemAtkTypePriorityToTable();
      string typeTextSpriteName = data.spAttackType.GetSpTypeTextSpriteName();
      int spAttackType = (int) data.spAttackType;
      if (isRight)
        this.SetSprite(rootObj, (Enum) SmithEvolve.UI.SPR_ORDER_ACTIONTYPE_RIGHT, typeTextSpriteName);
      else
        this.SetSprite(rootObj, (Enum) SmithEvolve.UI.SPR_ORDER_ACTIONTYPE_LEFT, typeTextSpriteName);
    }
    else
      typePriorityToTable = data.GetElemDefTypePriorityToTable();
    if (typePriorityToTable == 6 || typePriorityToTable == 6)
    {
      if (isRight)
        this.SetActive(rootObj, (Enum) SmithEvolve.UI.OBJ_ORDER_ATTRIBUTE_R, false);
      else
        this.SetActive(rootObj, (Enum) SmithEvolve.UI.OBJ_ORDER_ATTRIBUTE_L, false);
    }
    else if (isRight)
    {
      this.SetElementSprite(rootObj, (Enum) SmithEvolve.UI.SPR_ORDER_ELEM_R, typePriorityToTable);
      this.SetActive(rootObj, (Enum) SmithEvolve.UI.OBJ_ORDER_NORMAL_R, false);
    }
    else
    {
      this.SetElementSprite(rootObj, (Enum) SmithEvolve.UI.SPR_ORDER_ELEM_L, typePriorityToTable);
      this.SetActive(rootObj, (Enum) SmithEvolve.UI.OBJ_ORDER_NORMAL_L, false);
    }
  }

  protected override void InitNeedMaterialData()
  {
    SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
    this.needMaterial = smithData?.evolveData.GetEvolveTable().needMaterial;
    this.needMaterial = this.MaterialSort(this.needMaterial);
    this.needMoney = smithData != null ? (int) smithData.evolveData.GetEvolveTable().needMoney : 0;
    this.needEquip = smithData?.evolveData.GetEvolveTable().needEquip;
    this.CheckNeedMaterialNumFromInventory();
  }

  protected override void EquipTableParam()
  {
    base.EquipTableParam();
    SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
    EquipItemTable.EquipItemData equipTable = smithData.evolveData.GetEquipTable();
    SkillSlotUIData[] inheritanceSkill = this.GetEvolveInheritanceSkill(this.GetSkillSlotData(smithData.selectEquipData), equipTable, smithData.selectEquipData.exceed);
    AbilityItemInfo abilityItem = smithData.selectEquipData.GetAbilityItem();
    this.SetSkillIconButton((Enum) SmithEvolve.UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButton", equipTable, inheritanceSkill);
    this.adapterAbilityList.Clear();
    if (equipTable.fixedAbility != null && equipTable.fixedAbility.Length != 0)
    {
      int index = 0;
      for (int length = equipTable.fixedAbility.Length; index < length; ++index)
      {
        SmithEvolve.AdapterAbility adapterAbility = new SmithEvolve.AdapterAbility();
        adapterAbility.Set(equipTable.fixedAbility[index]);
        this.adapterAbilityList.Add(adapterAbility);
      }
    }
    EquipItemInfo selectEquipData = smithData.selectEquipData;
    if (selectEquipData.ability != null && selectEquipData.ability.Length != 0)
    {
      int index = 0;
      for (int length = selectEquipData.ability.Length; index < length; ++index)
      {
        if (!selectEquipData.IsFixedAbility(index))
        {
          SmithEvolve.AdapterAbility adapterAbility = new SmithEvolve.AdapterAbility();
          adapterAbility.Set(selectEquipData.ability[index]);
          this.adapterAbilityList.Add(adapterAbility);
        }
      }
    }
    if (this.adapterAbilityList.Count > 0 || abilityItem != null)
    {
      bool empty_ability = true;
      long num = 0;
      if (smithData != null && smithData.evolveData != null && smithData.evolveData.evolveBeforeEquipData != null && smithData.evolveData.evolveBeforeEquipData.tableData != null)
        num = (long) smithData.evolveData.evolveBeforeEquipData.tableData.id;
      int numAbility = this.adapterAbilityList.Count;
      if (MonoBehaviourSingleton<GoGameSettingsManager>.IsValid() && MonoBehaviourSingleton<GoGameSettingsManager>.I.weaponLimitedNumAbilities != null && MonoBehaviourSingleton<GoGameSettingsManager>.I.weaponLimitedNumAbilities.Contains(num) && numAbility >= MonoBehaviourSingleton<GoGameSettingsManager>.I.numAbilityCheck)
        numAbility = MonoBehaviourSingleton<GoGameSettingsManager>.I.limitAbility;
      this.SetTable((Enum) SmithEvolve.UI.TBL_ABILITY, "ItemDetailEquipAbilityItem", numAbility + (abilityItem != null ? 1 : 0), false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        if (i < numAbility)
        {
          SmithEvolve.AdapterAbility adapterAbility = this.adapterAbilityList[i];
          if (adapterAbility.GetId() == 0U)
          {
            this.SetActive(t, false);
          }
          else
          {
            empty_ability = false;
            this.SetActive(t, true);
            if (adapterAbility.isFix)
            {
              this.SetActive(t, (Enum) SmithEvolve.UI.OBJ_ABILITY, false);
              this.SetActive(t, (Enum) SmithEvolve.UI.OBJ_FIXEDABILITY, true);
              this.SetLabelText(t, (Enum) SmithEvolve.UI.LBL_FIXEDABILITY, adapterAbility.GetName());
              this.SetLabelText(t, (Enum) SmithEvolve.UI.LBL_FIXEDABILITY_NUM, adapterAbility.GetAP());
            }
            else
            {
              this.SetLabelText(t, (Enum) SmithEvolve.UI.LBL_ABILITY, adapterAbility.GetName());
              this.SetLabelText(t, (Enum) SmithEvolve.UI.LBL_ABILITY_NUM, adapterAbility.GetAP());
            }
            this.SetAbilityItemEvent(t, i, this.touchAndReleaseButtons);
          }
        }
        else
        {
          if (abilityItem == null)
            return;
          this.SetActive(t, (Enum) SmithEvolve.UI.OBJ_ABILITY, false);
          this.SetActive(t, (Enum) SmithEvolve.UI.OBJ_ABILITY_ITEM, true);
          this.SetLabelText(t, (Enum) SmithEvolve.UI.LBL_ABILITY_ITEM, abilityItem.GetName());
          this.SetTouchAndRelease(((Component) ((Component) t).GetComponentInChildren<UIButton>()).transform, "ABILITY_ITEM_DATA_POPUP", "RELEASE_ABILITY", (object) t);
        }
      }));
      if (empty_ability)
        this.SetActive((Enum) SmithEvolve.UI.STR_NON_ABILITY, true);
      else
        this.SetActive((Enum) SmithEvolve.UI.STR_NON_ABILITY, false);
    }
    else
      this.SetActive((Enum) SmithEvolve.UI.STR_NON_ABILITY, true);
  }

  protected override void EquipImg()
  {
  }

  protected override string GetEquipItemName()
  {
    string equipItemName = string.Empty;
    SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
    if (smithData != null)
      equipItemName = smithData.evolveData.evolveBeforeEquipData.tableData.name;
    return equipItemName;
  }

  protected override void OnQuery_SKILL_ICON_BUTTON()
  {
    EquipItemAndSkillData itemAndSkillData = new EquipItemAndSkillData()
    {
      equipItemInfo = new EquipItemInfo()
    };
    itemAndSkillData.equipItemInfo.tableData = this.GetEquipTableData();
    SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
    SkillSlotUIData[] inheritanceSkill = this.GetEvolveInheritanceSkill(this.GetSkillSlotData(smithData.selectEquipData), smithData.evolveData.GetEquipTable(), smithData.selectEquipData.exceed);
    itemAndSkillData.skillSlotUIData = inheritanceSkill;
    GameSection.SetEventData((object) new object[2]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.SMITH_EVOLVE,
      (object) itemAndSkillData
    });
  }

  private void OnQuery_BACK()
  {
    if (MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().evolveData.evolveTable.Length != 1)
      return;
    GameSection.ChangeEvent("TO_SELECT");
  }

  protected override void OnQuery_START()
  {
    SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
    if (smithData == null)
      return;
    SmithManager.ERR_SMITH_SEND errSmithSend = MonoBehaviourSingleton<SmithManager>.I.CheckEvolveEquipItem(smithData.evolveData.evolveBeforeEquipData, smithData.evolveData.GetEvolveTable().id, this.selectedUniqueIdList);
    if (errSmithSend != SmithManager.ERR_SMITH_SEND.NONE)
    {
      GameSection.ChangeEvent(errSmithSend.ToString());
    }
    else
    {
      this.isDialogEventYES = false;
      GameSection.SetEventData((object) new object[2]
      {
        (object) (this.GetEquipItemName() + " "),
        (object) $" {this.modifyText} "
      });
    }
  }

  private void OnQuery_SmithConfirmEvolve_YES() => this.OnQueryConfirmYES();

  protected override void Send()
  {
    SmithManager.SmithGrowData data = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
    if (data == null)
    {
      GameSection.StopEvent();
    }
    else
    {
      SmithManager.ResultData result_data = new SmithManager.ResultData();
      GameSection.SetEventData((object) result_data);
      GameSection.StayEvent();
      MonoBehaviourSingleton<SmithManager>.I.SendEvolveEquipItem(data.evolveData.evolveBeforeEquipData.uniqueID, data.evolveData.GetEvolveTable().id, this.selectedUniqueIdList, (Action<Error, EquipItemInfo>) ((err, evolve_item) =>
      {
        if (err == Error.None)
        {
          result_data.itemData = (object) evolve_item;
          result_data.beforeRarity = (int) data.evolveData.evolveBeforeEquipData.tableData.rarity;
          result_data.beforeLevel = data.evolveData.evolveBeforeEquipData.level;
          result_data.beforeMaxLevel = data.evolveData.evolveBeforeEquipData.tableData.maxLv;
          result_data.beforeExceedCnt = data.evolveData.evolveBeforeEquipData.exceed;
          result_data.beforeAtk = data.evolveData.evolveBeforeEquipData.atk;
          result_data.beforeDef = data.evolveData.evolveBeforeEquipData.def;
          result_data.beforeHp = data.evolveData.evolveBeforeEquipData.hp;
          result_data.beforeElemAtk = data.evolveData.evolveBeforeEquipData.elemAtk;
          result_data.beforeElemDef = data.evolveData.evolveBeforeEquipData.elemDef;
          MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData = evolve_item;
          MonoBehaviourSingleton<SmithManager>.I.CreateLocalInventory();
          MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = true;
          GameSection.ResumeEvent(true);
        }
        else
          GameSection.ResumeEvent(false);
      }));
    }
  }

  private void OnQuery_EVO_L()
  {
    if (this.isModelScrolling)
      return;
    this.selectedUniqueIdList = (ulong[]) null;
    this.MoveLeftIndex();
    this.RefreshUI();
  }

  private void OnQuery_EVO_R()
  {
    if (this.isModelScrolling)
      return;
    this.selectedUniqueIdList = (ulong[]) null;
    this.MoveRightIndex();
    this.RefreshUI();
  }

  private void MoveLeftIndex()
  {
    SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
    int length = smithData.evolveData.evolveEquipDataTable.Length;
    int selectIndex = smithData.evolveData.selectIndex;
    if (--smithData.evolveData.selectIndex < 0)
      smithData.evolveData.selectIndex = length - 1;
    this.rotateSign = -1f;
    this.StartCoroutine(this.ChangeLeftModel(selectIndex, smithData.evolveData.selectIndex));
    this.isButtonChange = true;
    this.isRightChange = false;
  }

  private void MoveRightIndex()
  {
    SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
    int length = smithData.evolveData.evolveEquipDataTable.Length;
    int selectIndex = smithData.evolveData.selectIndex;
    if (++smithData.evolveData.selectIndex >= length)
      smithData.evolveData.selectIndex = 0;
    this.rotateSign = 1f;
    this.StartCoroutine(this.ChangeRightModel(selectIndex, smithData.evolveData.selectIndex));
    this.isButtonChange = true;
    this.isRightChange = true;
  }

  private void UpdateModel()
  {
    this.DeleteItemModelObject();
    this.StopCoroutine("DoLoadModel");
    this.StartCoroutine("DoLoadModel");
  }

  private IEnumerator DoLoadModel()
  {
    this.InitRenderTexture((Enum) SmithEvolve.UI.TEX_MODEL, 45f);
    int max = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().evolveData.evolveEquipDataTable.Length;
    this.itemModelRoot = Utility.CreateGameObject("ItemModelRoot", this.GetRenderTextureModelTransform((Enum) SmithEvolve.UI.TEX_MODEL));
    this.itemModelRoot.localPosition = Vector3.op_Multiply(Vector3.up, 100f);
    this.itemModelRoot.localEulerAngles = Vector3.zero;
    this.itemModels = new Transform[max];
    bool[] load_complete = new bool[max];
    for (int i = 0; i < max; ++i)
      this.LoadItemModelData(i, max, (Action<int>) (index => load_complete[index] = true));
    while (true)
    {
      bool flag = false;
      for (int index = 0; index < max; ++index)
      {
        if (!load_complete[index])
        {
          flag = true;
          break;
        }
      }
      if (flag)
        yield return (object) null;
      else
        break;
    }
    SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
    this.defaultModelPos = ((Component) this.itemModels[smithData.evolveData.selectIndex]).transform.localPosition;
    this.animPosList = new List<TweenPosition>();
    for (int index = 0; index < max; ++index)
    {
      TweenPosition tweenPosition = ((Component) this.itemModels[index]).gameObject.AddComponent<TweenPosition>();
      this.animPosList.Add(tweenPosition);
      ((Behaviour) tweenPosition).enabled = false;
    }
    for (int index = 0; index < max; ++index)
    {
      if (index != smithData.evolveData.selectIndex)
        this.itemModels[index].localPosition = new Vector3(10f, 0.0f, 0.0f);
    }
    this.EnableRenderTexture((Enum) SmithEvolve.UI.TEX_MODEL);
  }

  private void LoadItemModelData(int i, int max, Action<int> callback)
  {
    SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
    ItemLoader loader;
    if (Object.op_Equality((Object) this.itemModels[i], (Object) null))
    {
      this.itemModels[i] = Utility.CreateGameObject("ItemModel", this.itemModelRoot);
      this.itemModels[i].localPosition = new Vector3((float) (i * 10), 0.0f, 0.0f);
    }
    else
    {
      loader = ((Component) this.itemModels[i]).gameObject.GetComponent<ItemLoader>();
      if (Object.op_Inequality((Object) loader, (Object) null))
      {
        loader.Clear();
        Object.DestroyImmediate((Object) loader);
      }
    }
    loader = ((Component) this.itemModels[i]).gameObject.AddComponent<ItemLoader>();
    loader.LoadEquip(smithData.evolveData.evolveEquipDataTable[i].id, this.GetRenderTextureModelTransform((Enum) SmithEvolve.UI.TEX_MODEL), this.GetRenderTextureLayer((Enum) SmithEvolve.UI.TEX_MODEL), -1, -1, (System.Action) (() =>
    {
      this.itemModelRoot.localPosition = new Vector3(0.0f, 0.0f, loader.displayInfo.zFromCamera);
      callback(i);
    }));
  }

  protected override void OnOpen()
  {
    if (Object.op_Equality((Object) this.itemModelRoot, (Object) null))
      this.UpdateModel();
    this.localRotateWait = 0.0f;
    this.localRotate = 0.0f;
    this.selectedUniqueIdList = GameSection.GetEventData() as ulong[];
  }

  protected override void OnClose()
  {
    this.DeleteItemModelObject();
    this.adapterAbilityList.Clear();
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    this.DeleteItemModelObject();
    this.adapterAbilityList.Clear();
  }

  private void DeleteItemModelObject()
  {
    this.DeleteRenderTexture((Enum) SmithEvolve.UI.TEX_MODEL);
    this.itemModels = (Transform[]) null;
    this.itemModelRoot = (Transform) null;
  }

  private void TurnItemModel(int before_index, int index, int max, bool is_immediate = true)
  {
    float num1 = 360f / (float) max * (float) before_index;
    float num2 = 360f / (float) max * (float) index;
    this.nowAngle = 10f;
    if ((double) this.rotateSign > 0.0)
    {
      if ((double) num2 < (double) num1)
        num2 += 360f;
    }
    else if ((double) num1 < (double) num2)
      num1 += 360f;
    this.targetAngle = Mathf.Abs(num2 - num1);
    if (!is_immediate)
    {
      this.isModelScrolling = true;
    }
    else
    {
      int index1 = 0;
      for (int length = this.itemModels.Length; index1 < length; ++index1)
        ((Component) this.itemModels[index1]).transform.localPosition = index == index1 ? Vector3.zero : Vector3.op_Multiply(Vector3.back, 100f);
      this.isModelScrolling = false;
    }
  }

  private IEnumerator ChangeRightModel(int beforeIndex, int selectIndex)
  {
    if (Object.op_Inequality((Object) this.animPosList[beforeIndex], (Object) null))
    {
      ((Behaviour) this.animPosList[beforeIndex]).enabled = true;
      this.animPosList[beforeIndex].ResetToBeginning();
      this.animPosList[beforeIndex].duration = 0.15f;
      this.animPosList[beforeIndex].from = this.defaultModelPos;
      this.animPosList[beforeIndex].to = new Vector3(this.defaultModelPos.x - 2f, this.defaultModelPos.y, this.defaultModelPos.z);
      this.animPosList[beforeIndex].PlayForward();
    }
    if (Object.op_Inequality((Object) this.animPosList[selectIndex], (Object) null))
    {
      ((Behaviour) this.animPosList[selectIndex]).enabled = true;
      this.animPosList[selectIndex].ResetToBeginning();
      this.animPosList[selectIndex].duration = 0.15f;
      this.animPosList[selectIndex].from = new Vector3(this.defaultModelPos.x + 2f, this.defaultModelPos.y, this.defaultModelPos.z);
      this.animPosList[selectIndex].to = this.defaultModelPos;
      this.animPosList[selectIndex].PlayForward();
    }
    if (Object.op_Inequality((Object) this.animPosList[beforeIndex], (Object) null))
    {
      while (((Behaviour) this.animPosList[beforeIndex]).enabled)
        yield return (object) null;
    }
    if (Object.op_Inequality((Object) this.animPosList[selectIndex], (Object) null))
    {
      while (((Behaviour) this.animPosList[selectIndex]).enabled)
        yield return (object) null;
    }
  }

  private IEnumerator ChangeLeftModel(int beforeIndex, int selectIndex)
  {
    if (Object.op_Inequality((Object) this.animPosList[beforeIndex], (Object) null))
    {
      ((Behaviour) this.animPosList[beforeIndex]).enabled = true;
      this.animPosList[beforeIndex].ResetToBeginning();
      this.animPosList[beforeIndex].duration = 0.15f;
      this.animPosList[beforeIndex].from = this.defaultModelPos;
      this.animPosList[beforeIndex].to = new Vector3(this.defaultModelPos.x + 2f, this.defaultModelPos.y, this.defaultModelPos.z);
      this.animPosList[beforeIndex].PlayForward();
    }
    if (Object.op_Inequality((Object) this.animPosList[selectIndex], (Object) null))
    {
      ((Behaviour) this.animPosList[selectIndex]).enabled = true;
      this.animPosList[selectIndex].ResetToBeginning();
      this.animPosList[selectIndex].duration = 0.15f;
      this.animPosList[selectIndex].from = new Vector3(this.defaultModelPos.x - 2f, this.defaultModelPos.y, this.defaultModelPos.z);
      this.animPosList[selectIndex].to = this.defaultModelPos;
      this.animPosList[selectIndex].PlayForward();
    }
    if (Object.op_Inequality((Object) this.animPosList[beforeIndex], (Object) null))
    {
      while (((Behaviour) this.animPosList[beforeIndex]).enabled)
        yield return (object) null;
    }
    if (Object.op_Inequality((Object) this.animPosList[selectIndex], (Object) null))
    {
      while (((Behaviour) this.animPosList[selectIndex]).enabled)
        yield return (object) null;
    }
  }

  public void LateUpdate()
  {
    if (this.itemModels == null)
      return;
    if (this.isModelScrolling)
    {
      float num = (float) ((double) this.targetAngle * (double) Time.deltaTime * 4.0);
      if ((double) this.nowAngle + (double) num >= (double) this.targetAngle)
      {
        num = this.targetAngle - this.nowAngle;
        this.isModelScrolling = false;
      }
      else
        this.nowAngle += num;
      int index = 0;
      for (int length = this.itemModels.Length; index < length; ++index)
      {
        this.itemModels[index].RotateAround(this.itemModelRoot.position, this.itemModelRoot.up, num * this.rotateSign);
        this.itemModels[index].eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
      }
    }
    if ((double) this.localRotateWait < 1.0)
    {
      this.localRotateWait += Time.deltaTime;
    }
    else
    {
      this.localRotate += Time.deltaTime * 10f;
      int index = 0;
      for (int length = this.itemModels.Length; index < length; ++index)
      {
        this.itemModels[index].eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        this.itemModels[index].Rotate(this.itemModels[index].up, this.localRotate);
      }
    }
  }

  private void OnQuery_SECTION_BACK()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("SmithGrowItemSelect"))
      return;
    GameSection.StopEvent();
    this.TO_UNIQUE_OR_MAIN_STATUS();
  }

  protected override void OnQuery_ABILITY_DATA_POPUP()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    int index = (int) eventData[0];
    Transform targetTrans = eventData[1] as Transform;
    if (Object.op_Equality((Object) this.abilityDetailPopUp, (Object) null))
      this.abilityDetailPopUp = this.CreateAndGetAbilityDetail((Enum) SmithEvolve.UI.OBJ_DETAIL_ROOT);
    this.abilityDetailPopUp.ShowAbilityDetail(targetTrans);
    this.abilityDetailPopUp.SetAbilityDetailText(this.adapterAbilityList[index].ability);
    GameSection.StopEvent();
  }

  private void OnQuery_ABILITY_ITEM_DATA_POPUP()
  {
    Transform eventData = GameSection.GetEventData() as Transform;
    AbilityItemInfo abilityItem = this.GetEquipData().GetAbilityItem();
    if (Object.op_Equality((Object) this.abilityDetailPopUp, (Object) null))
      this.abilityDetailPopUp = this.CreateAndGetAbilityDetail((Enum) SmithEvolve.UI.OBJ_DETAIL_ROOT);
    this.abilityDetailPopUp.ShowAbilityDetail(eventData);
    this.abilityDetailPopUp.SetAbilityDetailText(abilityItem.GetName(), "", abilityItem.GetDescription());
  }

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

  public class AdapterAbility
  {
    public EquipItemAbility ability;
    public bool isFix;

    public void Set(EquipItem.Ability a)
    {
      this.ability = new EquipItemAbility((uint) a.id, a.pt);
      this.isFix = true;
    }

    public void Set(EquipItemAbility a)
    {
      this.ability = a;
      this.isFix = false;
    }

    public uint GetId() => this.ability == null ? 0U : this.ability.id;

    public string GetName() => this.ability == null ? "" : this.ability.GetName();

    public string GetAP() => this.ability == null ? "+0" : this.ability.GetAP();
  }
}
