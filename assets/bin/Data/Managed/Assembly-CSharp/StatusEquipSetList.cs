// Decompiled with JetBrains decompiler
// Type: StatusEquipSetList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class StatusEquipSetList : SkillInfoBase
{
  private int equipSetNo;
  private int equipSetMax;
  private int equipSetCopyNo;
  private int equipSetPastedNo;
  private EquipSetInfo[] localEquipSet;
  private StatusEquipSetList.EQUIP_SET_COPY_MODE equipSetCopyMode;
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
      this.localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
    }
    this.equipSetNo = (int) GameSection.GetEventData();
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetDynamicList((Enum) StatusEquipSetList.UI.GRD_SET_LIST, "StatusEquipSetListItem", this.equipSetMax, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, isRecycle) => this.SetEquipSetInfo(t, i)));
    if (this.isInitializedScroll)
      return;
    int num = Mathf.Max(0, this.equipSetNo - 1);
    UIGrid component = this.GetComponent<UIGrid>((Enum) StatusEquipSetList.UI.GRD_SET_LIST);
    if (Object.op_Inequality((Object) component, (Object) null))
      this.MoveRelativeScrollView((Enum) StatusEquipSetList.UI.SCR_SET_LIST, Vector3.op_Multiply(Vector3.op_Multiply(Vector3.up, component.cellHeight), (float) num));
    this.isInitializedScroll = true;
  }

  protected override int GetCurrentEquipSetNo()
  {
    return this.equipSetCopyMode == StatusEquipSetList.EQUIP_SET_COPY_MODE.COPY ? this.equipSetCopyNo : base.GetCurrentEquipSetNo();
  }

  private void SetEquipSetInfo(Transform t, int setNo)
  {
    SimpleStatus finalStatus = MonoBehaviourSingleton<StatusManager>.I.GetEquipSetCalculator(setNo).GetFinalStatus(0, MonoBehaviourSingleton<UserInfoManager>.I.userStatus);
    this.SetLabelText(t, (Enum) StatusEquipSetList.UI.LBL_SET_NAME, this.localEquipSet[setNo].name);
    this.SetLabelText(t, (Enum) StatusEquipSetList.UI.LBL_SET_NO, (setNo + 1).ToString());
    this.SetLabelText(t, (Enum) StatusEquipSetList.UI.LBL_HP, finalStatus.hp.ToString());
    this.SetLabelText(t, (Enum) StatusEquipSetList.UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
    this.SetLabelText(t, (Enum) StatusEquipSetList.UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
    EQUIPMENT_TYPE[] weaponTypes = this.localEquipSet[setNo].GetWeaponTypes();
    this.SetSprite(t, (Enum) StatusEquipSetList.UI.SPR_WEAPON_ICON_0, this.GetWeaponIconSpriteName(weaponTypes[0]));
    this.SetSprite(t, (Enum) StatusEquipSetList.UI.SPR_WEAPON_ICON_1, this.GetWeaponIconSpriteName(weaponTypes[1]));
    this.SetSprite(t, (Enum) StatusEquipSetList.UI.SPR_WEAPON_ICON_2, this.GetWeaponIconSpriteName(weaponTypes[2]));
    ELEMENT_TYPE[] weaponElementTypes = this.localEquipSet[setNo].GetWeaponElementTypes();
    this.SetSprite(t, (Enum) StatusEquipSetList.UI.SPR_ELEMENT_ICON_0, this.GetWeaponElementIconSpriteName(weaponElementTypes[0]));
    this.SetSprite(t, (Enum) StatusEquipSetList.UI.SPR_ELEMENT_ICON_1, this.GetWeaponElementIconSpriteName(weaponElementTypes[1]));
    this.SetSprite(t, (Enum) StatusEquipSetList.UI.SPR_ELEMENT_ICON_2, this.GetWeaponElementIconSpriteName(weaponElementTypes[2]));
    this.DrawEquipSetCopyModeButton(t, setNo);
    this.SetEvent(t, "CHANGE_SET", setNo);
    this.SetEvent(t, (Enum) StatusEquipSetList.UI.BTN_EQUIP_SET_NAME, "CHANGE_SET_NAME", setNo);
    this.SetEvent(t, (Enum) StatusEquipSetList.UI.BTN_EQUIP_SET_COPY, "EQUIP_SET_COPY", setNo);
    this.SetEvent(t, (Enum) StatusEquipSetList.UI.BTN_EQUIP_SET_PASTE, "EQUIP_SET_PASTE", setNo);
    this.SetActive(t, (Enum) StatusEquipSetList.UI.SPR_SELECT_FRAME, setNo == this.equipSetNo);
  }

  private string GetWeaponIconSpriteName(EQUIPMENT_TYPE eqType)
  {
    return eqType > EQUIPMENT_TYPE.ARROW ? string.Empty : this.WEAPON_TYPE_ICON_SPRITE_NAME[(int) eqType];
  }

  private string GetWeaponElementIconSpriteName(ELEMENT_TYPE elemType)
  {
    return elemType > ELEMENT_TYPE.MAX ? string.Empty : this.ELEMENT_ICON_NAME[(int) elemType];
  }

  private void DrawEquipSetCopyModeButton(Transform t, int setNo)
  {
    bool flag1 = setNo == this.equipSetCopyNo;
    bool flag2 = this.equipSetCopyMode == StatusEquipSetList.EQUIP_SET_COPY_MODE.COPY;
    this.SetActive(t, (Enum) StatusEquipSetList.UI.BTN_EQUIP_SET_COPY, !flag2);
    this.SetActive(t, (Enum) StatusEquipSetList.UI.BTN_EQUIP_SET_PASTE, flag2 && !flag1);
    this.SetActive(t, (Enum) StatusEquipSetList.UI.BTN_EQUIP_SET_DELETE, flag2 & flag1);
  }

  private void ResetEquipSetCopy()
  {
    this.equipSetCopyMode = StatusEquipSetList.EQUIP_SET_COPY_MODE.NONE;
    this.equipSetCopyNo = 0;
    this.equipSetPastedNo = 0;
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
    int eventData = (int) GameSection.GetEventData();
    this.equipSetCopyMode = StatusEquipSetList.EQUIP_SET_COPY_MODE.COPY;
    this.equipSetCopyNo = eventData;
    this.equipSetCopyForm = this.CopyEquipSetInfo(this.localEquipSet[eventData], eventData);
    this.RefreshUI();
  }

  private void OnQuery_EQUIP_SET_DELETE()
  {
    this.ResetEquipSetCopy();
    this.RefreshUI();
  }

  private void OnQuery_EQUIP_SET_PASTE()
  {
    this.equipSetPastedNo = (int) GameSection.GetEventData();
    GameSection.ChangeEvent("EQUIP_SET_PASTE_CONFIRM");
  }

  private void OnQuery_StatusTopEquipSetPasteConfirm_YES()
  {
    GameSection.SetEventData((object) null);
    GameSection.StayEvent();
    this.equipSetCopyForm.no = this.equipSetPastedNo;
    MonoBehaviourSingleton<InventoryManager>.I.SendInventoryEquipSetCopy(this.equipSetCopyForm, (Action<bool>) (is_success =>
    {
      if (is_success)
      {
        if (MonoBehaviourSingleton<StatusManager>.IsValid())
          MonoBehaviourSingleton<StatusManager>.I.UpdateLocalEquipSet(this.equipSetPastedNo);
        this.ResetEquipSetCopy();
        this.RefreshUI();
      }
      GameSection.ResumeEvent(is_success);
    }));
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
