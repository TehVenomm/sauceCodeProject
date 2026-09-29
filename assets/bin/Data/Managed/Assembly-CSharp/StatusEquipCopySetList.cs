// Decompiled with JetBrains decompiler
// Type: StatusEquipCopySetList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class StatusEquipCopySetList : SkillInfoBase
{
  private int equipSetNo;
  private int equipSetMax;
  private int copyEquipSetNo;
  private EquipSetInfo[] localEquipSet;
  private StatusEquipSetCopyModel.RequestSendForm equipSetCopyForm;
  private bool isInitializedScroll;
  internal string[] WEAPON_TYPE_ICON_SPRITE_NAME = new string[6]
  {
    "ItemIconKind_Sword",
    "ItemIconKind_Brade",
    "ItemIconKind_Lance",
    "",
    "ItemIconKind_Edge",
    "ItemIconKind_Allow"
  };
  internal string[] ELEMENT_ICON_NAME = new string[7]
  {
    "IconElementFire",
    "IconElementWater",
    "IconElementThunder",
    "IconElementSoil",
    "IconElementLight",
    "IconElementDark",
    ""
  };

  public override void Initialize()
  {
    if (MonoBehaviourSingleton<StatusManager>.IsValid())
    {
      this.equipSetMax = MonoBehaviourSingleton<StatusManager>.I.EquipSetNum();
      this.localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetEquipSets();
    }
    this.equipSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetDynamicList((Enum) StatusEquipCopySetList.UI.GRD_SET_LIST, "StatusEquipSetListItem", this.equipSetMax, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, isRecycle) => this.SetEquipSetInfo(t, i)));
    if (this.isInitializedScroll)
      return;
    int num = Mathf.Max(0, this.equipSetNo - 1);
    UIGrid component = this.GetComponent<UIGrid>((Enum) StatusEquipCopySetList.UI.GRD_SET_LIST);
    if (Object.op_Inequality((Object) component, (Object) null))
      this.MoveRelativeScrollView((Enum) StatusEquipCopySetList.UI.SCR_SET_LIST, Vector3.op_Multiply(Vector3.op_Multiply(Vector3.up, component.cellHeight), (float) num));
    this.isInitializedScroll = true;
  }

  private void SetEquipSetInfo(Transform t, int setNo)
  {
    SimpleStatus finalStatus = MonoBehaviourSingleton<StatusManager>.I.GetEquipSetCalculator(setNo).GetFinalStatus(0, MonoBehaviourSingleton<UserInfoManager>.I.userStatus);
    this.SetLabelText(t, (Enum) StatusEquipCopySetList.UI.LBL_SET_NAME, this.localEquipSet[setNo].name);
    this.SetLabelText(t, (Enum) StatusEquipCopySetList.UI.LBL_SET_NO, (setNo + 1).ToString());
    this.SetLabelText(t, (Enum) StatusEquipCopySetList.UI.LBL_HP, finalStatus.hp.ToString());
    this.SetLabelText(t, (Enum) StatusEquipCopySetList.UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
    this.SetLabelText(t, (Enum) StatusEquipCopySetList.UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
    EQUIPMENT_TYPE[] weaponTypes = this.localEquipSet[setNo].GetWeaponTypes();
    this.SetSprite(t, (Enum) StatusEquipCopySetList.UI.SPR_WEAPON_ICON_0, this.GetWeaponIconSpriteName(weaponTypes[0]));
    this.SetSprite(t, (Enum) StatusEquipCopySetList.UI.SPR_WEAPON_ICON_1, this.GetWeaponIconSpriteName(weaponTypes[1]));
    this.SetSprite(t, (Enum) StatusEquipCopySetList.UI.SPR_WEAPON_ICON_2, this.GetWeaponIconSpriteName(weaponTypes[2]));
    ELEMENT_TYPE[] weaponElementTypes = this.localEquipSet[setNo].GetWeaponElementTypes();
    this.SetSprite(t, (Enum) StatusEquipCopySetList.UI.SPR_ELEMENT_ICON_0, this.GetWeaponElementIconSpriteName(weaponElementTypes[0]));
    this.SetSprite(t, (Enum) StatusEquipCopySetList.UI.SPR_ELEMENT_ICON_1, this.GetWeaponElementIconSpriteName(weaponElementTypes[1]));
    this.SetSprite(t, (Enum) StatusEquipCopySetList.UI.SPR_ELEMENT_ICON_2, this.GetWeaponElementIconSpriteName(weaponElementTypes[2]));
    this.SetEvent(t, "EQUIP_SET_COPY", setNo);
    this.SetEvent(t, (Enum) StatusEquipCopySetList.UI.BTN_EQUIP_SET_COPY, "EQUIP_SET_COPY", setNo);
    this.SetActive(t, (Enum) StatusEquipCopySetList.UI.BTN_EQUIP_SET_NAME, false);
    this.SetActive(t, (Enum) StatusEquipCopySetList.UI.BTN_EQUIP_SET_COPY, true);
    this.SetActive(t, (Enum) StatusEquipCopySetList.UI.BTN_EQUIP_SET_DELETE, false);
    this.SetActive(t, (Enum) StatusEquipCopySetList.UI.BTN_EQUIP_SET_PASTE, false);
    this.SetActive(t, (Enum) StatusEquipCopySetList.UI.SPR_SELECT_FRAME, setNo == this.equipSetNo);
  }

  private string GetWeaponIconSpriteName(EQUIPMENT_TYPE eqType)
  {
    return eqType > EQUIPMENT_TYPE.ARROW ? string.Empty : this.WEAPON_TYPE_ICON_SPRITE_NAME[(int) eqType];
  }

  private string GetWeaponElementIconSpriteName(ELEMENT_TYPE elemType)
  {
    return elemType > ELEMENT_TYPE.MAX ? string.Empty : this.ELEMENT_ICON_NAME[(int) elemType];
  }

  private void OnQuery_CHANGE_SET()
  {
    GameSection.SetEventData((object) (int) GameSection.GetEventData());
  }

  private void OnQuery_CHANGE_SET_NAME()
  {
    int eventData = (int) GameSection.GetEventData();
    GameSection.SetEventData((object) new object[2]
    {
      (object) eventData,
      (object) this.localEquipSet[eventData]
    });
  }

  private void OnQuery_EQUIP_SET_COPY()
  {
    this.copyEquipSetNo = (int) GameSection.GetEventData();
    if (MonoBehaviourSingleton<StatusManager>.I.CopyEquipSetCheck(this.copyEquipSetNo))
    {
      if (MonoBehaviourSingleton<StatusManager>.I.GetCurrentLocalEquipSet().order > 0)
        GameSection.ChangeEvent("EQUIP_COPY_CONFIRM", (object) new object[1]
        {
          (object) (this.copyEquipSetNo + 1)
        });
      else
        this.TO_EQUIP_TOP();
    }
    else if (MonoBehaviourSingleton<StatusManager>.I.GetCurrentLocalEquipSet().order > 0)
      GameSection.ChangeEvent("EQUIPING_ORDER_COPY_CONFIRM", (object) new object[1]
      {
        (object) (this.copyEquipSetNo + 1)
      });
    else
      GameSection.ChangeEvent("EQUIPING_COPY_CONFIRM", (object) new object[1]
      {
        (object) (this.copyEquipSetNo + 1)
      });
  }

  private void OnQuery_StatusOrderEquipCopyConfirm_YES() => this.OrderEquipSetCopy();

  private void OnQuery_StatusEquipingCopyConfirm_YES() => this.EquipSetCopy();

  private void OnQuery_StatusOrderEquipingCopyConfirm_YES() => this.OrderEquipSetCopy();

  protected virtual void TO_EQUIP_TOP()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("UniqueStatus", "UniqueStatusTop");
    this.EquipSetCopy();
  }

  protected void OrderEquipSetCopy()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.RemoveOrderNo(MonoBehaviourSingleton<StatusManager>.I.GetCurrentUniqueEquipSetNo(), (Action<bool>) (is_succses => this.EquipSetCopy()));
  }

  protected virtual void EquipSetCopy()
  {
    if (!GameSceneEvent.IsStay())
      GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.CopyEquipSet(this.copyEquipSetNo, (Action<bool>) (isSucces => GameSection.ResumeEvent(isSucces)));
  }

  private void OnCloseDialog_StatusChangedEquipSetName() => this.RefreshUI();

  public enum UI
  {
    SCR_SET_LIST,
    GRD_SET_LIST,
    LBL_SET_NAME,
    BTN_EQUIP_SET_NAME,
    LBL_SET_NO,
    LBL_HP,
    LBL_ATK,
    LBL_DEF,
    SPR_WEAPON_ICON_0,
    SPR_WEAPON_ICON_1,
    SPR_WEAPON_ICON_2,
    SPR_ELEMENT_ICON_0,
    SPR_ELEMENT_ICON_1,
    SPR_ELEMENT_ICON_2,
    BTN_EQUIP_SET_COPY,
    BTN_EQUIP_SET_PASTE,
    BTN_EQUIP_SET_DELETE,
    SPR_SELECT_FRAME,
  }

  private enum EQUIP_SET_COPY_MODE
  {
    NONE,
    COPY,
  }
}
