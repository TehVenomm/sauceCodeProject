// Decompiled with JetBrains decompiler
// Type: StatusTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class StatusTop : SkillInfoBase
{
  public EquipSetInfo[] localEquipSet;
  public int equipSetNo;
  private int SET_NO_MAX = MonoBehaviourSingleton<StatusManager>.I.EquipSetNum();
  private StatusTop.EQUIP_SET_COPY_MODE equipSetCopyMode;
  private int equipSetCopyNo;
  private StatusEquipSetCopyModel.RequestSendForm equipSetCopyForm;
  private bool showEquipMode = true;
  private StatusTop.UI[] icons = new StatusTop.UI[7]
  {
    StatusTop.UI.OBJ_ICON_WEAPON_1,
    StatusTop.UI.OBJ_ICON_WEAPON_2,
    StatusTop.UI.OBJ_ICON_WEAPON_3,
    StatusTop.UI.OBJ_ICON_ARMOR,
    StatusTop.UI.OBJ_ICON_HELM,
    StatusTop.UI.OBJ_ICON_ARM,
    StatusTop.UI.OBJ_ICON_LEG
  };
  private EQUIPMENT_TYPE[] visualType = new EQUIPMENT_TYPE[4]
  {
    EQUIPMENT_TYPE.ARMOR,
    EQUIPMENT_TYPE.HELM,
    EQUIPMENT_TYPE.ARM,
    EQUIPMENT_TYPE.LEG
  };
  private StatusTop.UI[] iconsBtn = new StatusTop.UI[7]
  {
    StatusTop.UI.BTN_ICON_WEAPON_1,
    StatusTop.UI.BTN_ICON_WEAPON_2,
    StatusTop.UI.BTN_ICON_WEAPON_3,
    StatusTop.UI.BTN_ICON_ARMOR,
    StatusTop.UI.BTN_ICON_HELM,
    StatusTop.UI.BTN_ICON_ARM,
    StatusTop.UI.BTN_ICON_LEG
  };
  private StatusTop.UI[] iconsVisual = new StatusTop.UI[4]
  {
    StatusTop.UI.OBJ_ICON_VISUAL_ARMOR,
    StatusTop.UI.OBJ_ICON_VISUAL_HELM,
    StatusTop.UI.OBJ_ICON_VISUAL_ARM,
    StatusTop.UI.OBJ_ICON_VISUAL_LEG
  };
  private StatusTop.UI[] iconsVisualBtn = new StatusTop.UI[4]
  {
    StatusTop.UI.BTN_ICON_VISUAL_ARMOR_BASE,
    StatusTop.UI.BTN_ICON_VISUAL_HELM_BASE,
    StatusTop.UI.BTN_ICON_VISUAL_ARM_BASE,
    StatusTop.UI.BTN_ICON_VISUAL_LEG_BASE
  };
  private StatusTop.UI[] lblEquipLevel = new StatusTop.UI[7]
  {
    StatusTop.UI.LBL_LEVEL_WEAPON_1,
    StatusTop.UI.LBL_LEVEL_WEAPON_2,
    StatusTop.UI.LBL_LEVEL_WEAPON_3,
    StatusTop.UI.LBL_LEVEL_ARMOR,
    StatusTop.UI.LBL_LEVEL_HELM,
    StatusTop.UI.LBL_LEVEL_ARM,
    StatusTop.UI.LBL_LEVEL_LEG
  };
  private StatusTop.UI[] lblShadowEquipLevel = new StatusTop.UI[7]
  {
    StatusTop.UI.LBL_LEVEL_WEAPON_1_SHADOW,
    StatusTop.UI.LBL_LEVEL_WEAPON_2_SHADOW,
    StatusTop.UI.LBL_LEVEL_WEAPON_3_SHADOW,
    StatusTop.UI.LBL_LEVEL_ARMOR_SHADOW,
    StatusTop.UI.LBL_LEVEL_HELM_SHADOW,
    StatusTop.UI.LBL_LEVEL_ARM_SHADOW,
    StatusTop.UI.LBL_LEVEL_LEG_SHADOW
  };
  private StatusManager.LocalVisual visualEquip;
  private StatusTop.UI? tweenTarget;
  private UICenterOnChild uiCenterOnChild;
  private int detailEquipSetNo = -1;
  private EquipItemInfo visualDetailEquip;
  private int visualDetailItemIndex = -1;

  public override bool useOnPressBackKey => true;

  public override void OnPressBackKey() => this.DispatchEvent(GameSection.GetGoingHomeEvent());

  public override void Initialize()
  {
    this.showEquipMode = true;
    this.tweenTarget = new StatusTop.UI?();
    this.SetActive((Enum) StatusTop.UI.OBJ_WITH_MONSTER_ROOT, true);
    this.SetActive((Enum) StatusTop.UI.OBJ_WITHOUT_MONSTER_ROOT, false);
    this.SetActive((Enum) StatusTop.UI.BTN_UNIQUE, GameSaveData.instance.IsOpenUniqueStatus());
    this.SettingEquipSetInfo();
    this.SetDynamicList((Enum) StatusTop.UI.GRD_DRUM, "equipno", this.SET_NO_MAX, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, isRecycle) => this.SetLabelText(t, (Enum) StatusTop.UI.LBL_EQUIP_NO, (i + 1).ToString())));
    this.SetCenterOnChildFunc((Enum) StatusTop.UI.GRD_DRUM, (SpringPanel.OnFinished) (() =>
    {
      int result = this.equipSetNo;
      if (int.TryParse(this.GetLabel(this.GetCenter(this.GetCtrl((Enum) StatusTop.UI.GRD_DRUM)), (Enum) StatusTop.UI.LBL_EQUIP_NO), out result))
        this.equipSetNo = Mathf.Clamp(result - 1, 0, this.SET_NO_MAX - 1);
      MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(this.equipSetNo);
      this.tweenTarget = new StatusTop.UI?(StatusTop.UI.OBJ_EQUIP_ROOT);
      this.RefreshUI();
    }));
    this.SetCenter(this.GetCtrl((Enum) StatusTop.UI.GRD_DRUM), this.equipSetNo, true);
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_WIN) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM))
    {
      MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_11_weapon_tut, "Tutorial");
      Debug.LogWarning((object) ("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_11_weapon_tut.ToString()));
      MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_11_weapon_tut, "Tutorial");
    }
    else if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SHADOW_QUEST_WIN) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
    {
      MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_15_upgrading_tut, "Tutorial");
      Debug.LogWarning((object) ("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_15_upgrading_tut.ToString()));
      MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_15_upgrading_tut, "Tutorial");
    }
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    Singleton<EquipItemTable>.I.CreateTableForEquipList();
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    base.Initialize();
  }

  protected override void OnOpen()
  {
    object eventData = GameSection.GetEventData();
    switch (eventData)
    {
      case StatusEquip.ChangeEquipData _:
        StatusEquip.ChangeEquipData changeEquipData = eventData as StatusEquip.ChangeEquipData;
        if (this.showEquipMode)
        {
          this.localEquipSet[changeEquipData.setNo].item[changeEquipData.index] = changeEquipData.item;
          MonoBehaviourSingleton<StatusManager>.I.ReplaceEquipItem(this.localEquipSet[changeEquipData.setNo], changeEquipData.setNo, changeEquipData.index);
          break;
        }
        int index1 = changeEquipData.index;
        long uniqueId1 = this.visualEquip.visualItem[index1] != null ? (long) this.visualEquip.visualItem[index1].uniqueID : 0L;
        this.visualEquip.visualItem[index1] = changeEquipData.item;
        long uniqueId2 = this.visualEquip.visualItem[index1] != null ? (long) this.visualEquip.visualItem[index1].uniqueID : 0L;
        if (uniqueId1 != uniqueId2)
        {
          this.UpdateModel();
          break;
        }
        break;
      case StatusEquip.ChangeEquipData[] _:
        StatusEquip.ChangeEquipData[] changeEquipDataArray = eventData as StatusEquip.ChangeEquipData[];
        for (int index2 = 0; index2 < changeEquipDataArray.Length; ++index2)
        {
          if ((changeEquipDataArray[index2].index != 0 || changeEquipDataArray[index2].item != null) && (changeEquipDataArray[index2].index != 3 || changeEquipDataArray[index2].item != null))
          {
            this.localEquipSet[changeEquipDataArray[index2].setNo].item[changeEquipDataArray[index2].index] = changeEquipDataArray[index2].item;
            MonoBehaviourSingleton<StatusManager>.I.ReplaceEquipItem(this.localEquipSet[changeEquipDataArray[index2].setNo], changeEquipDataArray[index2].setNo, changeEquipDataArray[index2].index);
          }
        }
        break;
      default:
        this.ResetEquipSetCopy();
        break;
    }
    this.localEquipSetUpdate();
    this.SetDynamicList((Enum) StatusTop.UI.GRD_DRUM, "equipno", this.SET_NO_MAX, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, isRecycle) => this.SetLabelText(t, (Enum) StatusTop.UI.LBL_EQUIP_NO, (i + 1).ToString())));
    this.SetCenter(this.GetCtrl((Enum) StatusTop.UI.GRD_DRUM), this.equipSetNo, true);
  }

  protected override void OnCloseStart()
  {
    this.ResetDetailTarget();
    this.OnClose();
  }

  private void UpdateModel()
  {
    PlayerLoadInfo load_info = new PlayerLoadInfo();
    load_info.SetupLoadInfo(this.localEquipSet[this.equipSetNo], 0UL, this.visualEquip.VisialID(0), this.visualEquip.VisialID(1), this.visualEquip.VisialID(2), this.visualEquip.VisialID(3), this.localEquipSet[this.equipSetNo].showHelm == 1);
    if (!MonoBehaviourSingleton<StatusStageManager>.IsValid())
      return;
    MonoBehaviourSingleton<StatusStageManager>.I.LoadPlayer(load_info);
  }

  public override void Exit()
  {
    MonoBehaviourSingleton<StatusManager>.I.isEquipSetCalcUpdate = true;
    MonoBehaviourSingleton<StatusManager>.I.CheckChangeEquip(this.equipSetNo, (Action<bool>) (is_success => base.Exit()));
  }

  public void SettingEquipSetInfo()
  {
    if (this.localEquipSet == null)
    {
      this.localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
      this.equipSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
      MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(this.equipSetNo);
    }
    if (this.visualEquip != null)
      return;
    this.visualEquip = MonoBehaviourSingleton<StatusManager>.I.GetLocalVisualEquip();
  }

  public void ForceSettingEquipSetInfo()
  {
    this.localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
    this.SET_NO_MAX = MonoBehaviourSingleton<StatusManager>.I.EquipSetNum();
    this.DrawEquipSetModel();
  }

  public override void UpdateUI()
  {
    this.SetBadge((Enum) StatusTop.UI.BTN_STUDIO, MonoBehaviourSingleton<SmithManager>.I.GetBadgeTotalNum(), (SpriteAlignment) 1, 8, -8, true);
    this.DrawEquipModeButton();
    int sex = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
    if (this.showEquipMode)
    {
      EquipSetInfo localEquip = this.localEquipSet[this.equipSetNo];
      if (localEquip != null && localEquip.item[4] == null)
        this.SetActive((Enum) StatusTop.UI.TGL_VISIBLE_HELM_BUTTON, false);
      else
        this.SetActive((Enum) StatusTop.UI.TGL_VISIBLE_HELM_BUTTON, true);
      int index1 = 0;
      for (int length = this.icons.Length; index1 < length; ++index1)
      {
        EquipItemInfo equipItemInfo = localEquip.item[index1];
        ((Component) this.GetCtrl((Enum) this.icons[index1])).GetComponentsInChildren<ItemIcon>(true, Temporary.itemIconList);
        int index2 = 0;
        for (int count = Temporary.itemIconList.Count; index2 < count; ++index2)
          ((Component) Temporary.itemIconList[index2]).gameObject.SetActive(true);
        Temporary.itemIconList.Clear();
        ItemIcon iconByEquipItemInfo = ItemIcon.CreateEquipItemIconByEquipItemInfo(equipItemInfo, sex, this.GetCtrl((Enum) this.icons[index1]), event_name: "DETAIL", event_data: index1);
        int num = -1;
        string text = string.Empty;
        if (equipItemInfo != null && equipItemInfo.tableID != 0U)
        {
          num = equipItemInfo.tableData.GetIconID(sex);
          text = string.Format(StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 1U), (object) equipItemInfo.level);
        }
        ((Component) iconByEquipItemInfo).gameObject.SetActive(num != -1);
        this.SetEvent((Enum) this.iconsBtn[index1], num != -1 ? "DETAIL" : "EQUIP", index1);
        this.SetLabelText((Enum) this.lblEquipLevel[index1], text);
        this.SetLabelText((Enum) this.lblShadowEquipLevel[index1], text);
        if (num != -1)
          iconByEquipItemInfo.SetEquipExt(equipItemInfo, this.GetComponent<UILabel>((Enum) this.lblEquipLevel[index1]));
        Transform ctrl = this.GetCtrl((Enum) this.iconsBtn[index1]);
        bool flag = equipItemInfo != null && equipItemInfo.tableID > 0U;
        if (flag)
          this.SetSkillIconButton(ctrl, (Enum) StatusTop.UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButtonTOP", equipItemInfo.tableData, this.GetSkillSlotData(equipItemInfo), button_event_data: index1);
        ((Component) this.FindCtrl(ctrl, (Enum) StatusTop.UI.OBJ_SKILL_BUTTON_ROOT)).gameObject.SetActive(flag);
      }
    }
    else
    {
      int event_data = 0;
      for (int length = this.visualEquip.visualItem.Length; event_data < length; ++event_data)
      {
        EquipItemInfo equipItemInfo = this.visualEquip.visualItem[event_data];
        Transform ctrl = this.GetCtrl((Enum) this.iconsVisual[event_data]);
        ((Component) ctrl).GetComponentsInChildren<ItemIcon>(true, Temporary.itemIconList);
        int index = 0;
        for (int count = Temporary.itemIconList.Count; index < count; ++index)
          ((Component) Temporary.itemIconList[index]).gameObject.SetActive(true);
        Temporary.itemIconList.Clear();
        ItemIcon iconByEquipItemInfo = ItemIcon.CreateEquipItemIconByEquipItemInfo(equipItemInfo, sex, ctrl, event_name: "AVATAR", event_data: event_data);
        this.SetLongTouch(iconByEquipItemInfo.transform, "VISUAL_DETAIL", (object) event_data);
        int num = -1;
        if (equipItemInfo != null)
          num = equipItemInfo.tableData.GetIconID(sex);
        ((Component) iconByEquipItemInfo).gameObject.SetActive(num != -1);
        this.SetEvent((Enum) this.iconsVisualBtn[event_data], "AVATAR", event_data);
        this.SetLongTouch((Enum) this.iconsVisualBtn[event_data], "VISUAL_DETAIL", (object) event_data);
      }
    }
    this.DrawEquipSetModel();
    if (this.tweenTarget.HasValue)
    {
      this.ResetTween((Enum) (ValueType) this.tweenTarget);
      this.PlayTween((Enum) (ValueType) this.tweenTarget, is_input_block: false);
    }
    this.SetActive((Enum) StatusTop.UI.OBJ_STUDIO_BUTTON_ROOT, this.showEquipMode);
    this.SetActive((Enum) StatusTop.UI.TGL_VISIBLE_UI_BUTTON, !this.showEquipMode);
    this.SetToggle((Enum) StatusTop.UI.TGL_SHOW_EQUIP_TYPE, this.showEquipMode);
    if (this.visualEquip.isVisibleHelm != (this.localEquipSet[this.equipSetNo].showHelm == 1))
    {
      this.ResetTween((Enum) StatusTop.UI.BTN_VISIBLE_HELM);
      this.ResetTween((Enum) StatusTop.UI.BTN_INVISIBLE_HELM);
      if (this.localEquipSet[this.equipSetNo].showHelm == 1)
        this.PlayTween((Enum) StatusTop.UI.BTN_INVISIBLE_HELM, is_input_block: false);
      else
        this.PlayTween((Enum) StatusTop.UI.BTN_VISIBLE_HELM, is_input_block: false);
      this.visualEquip.isVisibleHelm = this.localEquipSet[this.equipSetNo].showHelm == 1;
    }
    this.SetToggleButton((Enum) StatusTop.UI.TGL_VISIBLE_HELM_BUTTON, this.visualEquip.isVisibleHelm, (Action<bool>) (is_active =>
    {
      this.visualEquip.isVisibleHelm = is_active;
      this.localEquipSet[this.equipSetNo].showHelm = this.visualEquip.isVisibleHelm ? 1 : 0;
      this.ResetTween((Enum) StatusTop.UI.BTN_VISIBLE_HELM);
      this.ResetTween((Enum) StatusTop.UI.BTN_INVISIBLE_HELM);
      if (is_active)
        this.PlayTween((Enum) StatusTop.UI.BTN_INVISIBLE_HELM, is_input_block: false);
      else
        this.PlayTween((Enum) StatusTop.UI.BTN_VISIBLE_HELM, is_input_block: false);
      this.UpdateModel();
    }));
    this.DrawEquipSetCopyModeButton();
    this.SetDynamicList((Enum) StatusTop.UI.GRD_DRUM, "equipno", this.SET_NO_MAX, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, isRecycle) => this.SetLabelText(t, (Enum) StatusTop.UI.LBL_EQUIP_NO, (i + 1).ToString())));
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
      ((Behaviour) ((Component) this.GetCtrl((Enum) StatusTop.UI.SCR_DRUM)).gameObject.GetComponent<UIScrollView>()).enabled = false;
    base.UpdateUI();
  }

  public void DrawEquipSetModel()
  {
    SimpleStatus finalStatus = MonoBehaviourSingleton<StatusManager>.I.GetEquipSetCalculator(this.equipSetNo).GetFinalStatus(0, MonoBehaviourSingleton<UserInfoManager>.I.userStatus);
    this.SetLabelText((Enum) StatusTop.UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
    this.SetLabelText((Enum) StatusTop.UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
    this.SetLabelText((Enum) StatusTop.UI.LBL_HP, finalStatus.hp.ToString());
    this.SetLabelText((Enum) StatusTop.UI.LBL_NOW, (this.equipSetNo + 1).ToString());
    this.SetLabelText((Enum) StatusTop.UI.LBL_MAX, this.SET_NO_MAX.ToString());
    this.SetLabelText((Enum) StatusTop.UI.LBL_SET_NAME, this.localEquipSet[this.equipSetNo].name);
    this.UpdateModel();
  }

  private void DrawEquipModeButton()
  {
    this.SetActive((Enum) StatusTop.UI.OBJ_EQUIP_ROOT, this.showEquipMode);
    this.SetActive((Enum) StatusTop.UI.BTN_AVATAR_INACTIVE, this.showEquipMode);
    this.SetActive((Enum) StatusTop.UI.OBJ_EQUIP_SET_SELECT, this.showEquipMode);
    this.SetActive((Enum) StatusTop.UI.SPR_PARAMETER_ACTIVE, this.showEquipMode);
    this.SetActive((Enum) StatusTop.UI.OBJ_VISUAL_ROOT, !this.showEquipMode);
    this.SetActive((Enum) StatusTop.UI.BTN_PARAMETER_INACTIVE, !this.showEquipMode);
    this.SetActive((Enum) StatusTop.UI.SPR_AVATAR_ACTIVE, !this.showEquipMode);
  }

  private void DrawEquipSetCopyModeButton()
  {
    bool flag1 = this.equipSetNo == this.equipSetCopyNo;
    bool flag2 = this.equipSetCopyMode == StatusTop.EQUIP_SET_COPY_MODE.COPY;
    this.SetActive((Enum) StatusTop.UI.BTN_EQUIP_SET_COPY, !flag2);
    this.SetActive((Enum) StatusTop.UI.BTN_EQUIP_SET_PASTE, flag2 && !flag1);
    this.SetActive((Enum) StatusTop.UI.BTN_EQUIP_SET_DELETE, flag2 & flag1);
  }

  private void OnQuery_EQUIP_SET_L()
  {
    if (this.equipSetNo > 0)
      --this.equipSetNo;
    else
      this.equipSetNo = this.SET_NO_MAX - 1;
    MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(this.equipSetNo);
    this.tweenTarget = new StatusTop.UI?(StatusTop.UI.OBJ_EQUIP_ROOT);
    this.SetCenter(this.GetCtrl((Enum) StatusTop.UI.GRD_DRUM), this.equipSetNo, true);
  }

  private void OnQuery_EQUIP_SET_R()
  {
    if (this.equipSetNo < this.SET_NO_MAX - 1)
      ++this.equipSetNo;
    else
      this.equipSetNo = 0;
    MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(this.equipSetNo);
    this.tweenTarget = new StatusTop.UI?(StatusTop.UI.OBJ_EQUIP_ROOT);
    this.SetCenter(this.GetCtrl((Enum) StatusTop.UI.GRD_DRUM), this.equipSetNo, true);
  }

  private void OnQuery_EQUIP()
  {
    this.tweenTarget = new StatusTop.UI?();
    int eventData = (int) GameSection.GetEventData();
    int equipSetNo = this.equipSetNo;
    int index = equipSetNo == 0 ? eventData : eventData % (equipSetNo << 16 /*0x10*/);
    if (equipSetNo >= this.localEquipSet.Length || index >= 7)
      return;
    MonoBehaviourSingleton<StatusManager>.I.SetEquippingItem(this.localEquipSet[equipSetNo].item[index]);
    MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = this.GetInventoryType(index);
    GameSection.SetEventData((object) new StatusEquip.LocalEquipSetData(equipSetNo, index, this.localEquipSet[equipSetNo]));
  }

  private void OnQuery_AUTO_EQUIP()
  {
    int equipSetNo = this.equipSetNo;
    if (equipSetNo >= this.localEquipSet.Length)
      return;
    GameSection.SetEventData((object) new StatusEquip.LocalEquipSetData(equipSetNo, 0, this.localEquipSet[equipSetNo]));
  }

  private void OnQuery_AVATAR()
  {
    int eventData = (int) GameSection.GetEventData();
    StatusEquip.LocalEquipSetData localEquipSetData = new StatusEquip.LocalEquipSetData(this.equipSetNo, eventData, this.localEquipSet[this.equipSetNo]);
    GameSection.SetEventData((object) new object[3]
    {
      (object) this.visualType[eventData],
      (object) this.visualEquip.visualItem[eventData],
      (object) localEquipSetData
    });
  }

  private void OnQuery_MODE_EQUIP()
  {
    this.ResetDetailTarget();
    this.showEquipMode = true;
    this.tweenTarget = new StatusTop.UI?(StatusTop.UI.OBJ_EQUIP_ROT_ROOT);
    if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
      MonoBehaviourSingleton<StatusStageManager>.I.SetViewMode(StatusStageManager.VIEW_MODE.EQUIP);
    this.ResetTween((Enum) StatusTop.UI.OBJ_PARAMETER_BUTTON_ROOT);
    this.PlayTween((Enum) StatusTop.UI.OBJ_PARAMETER_BUTTON_ROOT, is_input_block: false);
    this.RefreshUI();
  }

  private void OnQuery_MODE_VISUAL()
  {
    this.ResetDetailTarget();
    this.showEquipMode = false;
    this.tweenTarget = new StatusTop.UI?(StatusTop.UI.OBJ_VISUAL_ROOT);
    if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
      MonoBehaviourSingleton<StatusStageManager>.I.SetViewMode(StatusStageManager.VIEW_MODE.AVATAR);
    this.ResetTween((Enum) StatusTop.UI.OBJ_AVATAR_BUTTON_ROOT);
    this.PlayTween((Enum) StatusTop.UI.OBJ_AVATAR_BUTTON_ROOT, is_input_block: false);
    this.RefreshUI();
  }

  private void OnQuery_TO_STORAGE() => this.CheckEquipChange();

  private void OnQuery_STUDIO() => this.CheckEquipChange();

  protected override void OnQuery_MAIN_MENU_QUEST()
  {
    this.tweenTarget = new StatusTop.UI?();
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.CheckChangeEquip(this.equipSetNo, (Action<bool>) (is_success =>
    {
      GameSection.ResumeEvent(is_success);
      base.OnQuery_MAIN_MENU_QUEST();
    }));
  }

  private void CheckEquipChange()
  {
    this.tweenTarget = new StatusTop.UI?();
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.CheckChangeEquip(this.equipSetNo, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  private void OnQuery_SKILL_LIST()
  {
    this.tweenTarget = new StatusTop.UI?();
    if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA2))
      MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP);
    GameSection.SetEventData((object) new object[4]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.STATUS_TOP,
      (object) this.GetLocalEquipSetAttachSkillListData(this.equipSetNo),
      (object) false,
      (object) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex
    });
  }

  private void ChangeWeaponForSkillTutorial()
  {
    int weaponId = 10000000;
    if ((long) this.localEquipSet[this.equipSetNo].item[0].tableID == (long) weaponId)
      return;
    EquipItemInfo equipItemInfo1 = this.localEquipSet[this.equipSetNo].item[1];
    if (equipItemInfo1 != null && (long) equipItemInfo1.tableID == (long) weaponId)
    {
      this.SwapWeapon(0, 1);
      this.RefreshUI();
    }
    else
    {
      EquipItemInfo equipItemInfo2 = this.localEquipSet[this.equipSetNo].item[2];
      if (equipItemInfo2 != null && (long) equipItemInfo2.tableID == (long) weaponId)
      {
        this.SwapWeapon(0, 2);
        this.RefreshUI();
      }
      else
      {
        EquipItemInfo weaponFromInventory = this.GetWeaponFromInventory(weaponId);
        if (weaponFromInventory == null)
          return;
        if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
          MonoBehaviourSingleton<StatusStageManager>.I.SetEquipInfo(weaponFromInventory);
        this.localEquipSet[this.equipSetNo].item[0] = weaponFromInventory;
        MonoBehaviourSingleton<StatusManager>.I.ReplaceEquipItem(this.localEquipSet[this.equipSetNo], this.equipSetNo, 0);
        this.RefreshUI();
      }
    }
  }

  private void SwapWeapon(int swapIndex, int nowIndex)
  {
    EquipItemInfo equipItemInfo = this.localEquipSet[this.equipSetNo].item[nowIndex];
    this.localEquipSet[this.equipSetNo].item[nowIndex] = this.localEquipSet[this.equipSetNo].item[swapIndex];
    this.localEquipSet[this.equipSetNo].item[swapIndex] = equipItemInfo;
    MonoBehaviourSingleton<StatusManager>.I.SwapWeapon(swapIndex, nowIndex);
  }

  private EquipItemInfo GetWeaponFromInventory(int weaponId)
  {
    MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = InventoryManager.INVENTORY_TYPE.ALL_WEAPON;
    MonoBehaviourSingleton<SmithManager>.I.CreateLocalInventory();
    EquipItemInfo[] inventoryEquipData = MonoBehaviourSingleton<SmithManager>.I.localInventoryEquipData as EquipItemInfo[];
    int length = inventoryEquipData.Length;
    for (int index = 0; index < length; ++index)
    {
      if ((long) inventoryEquipData[index].tableID == (long) weaponId)
        return inventoryEquipData[index];
    }
    return (EquipItemInfo) null;
  }

  private void OnQuery_ABILITY()
  {
    this.tweenTarget = new StatusTop.UI?();
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    GameSection.SetEventData((object) new object[3]
    {
      (object) this.localEquipSet[this.equipSetNo],
      (object) MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSetAbility(this.equipSetNo),
      (object) new EquipSetDetailStatusAndAbilityTable.BaseStatus((int) userStatus.atk, (int) userStatus.def, (int) userStatus.hp, (List<CharaInfo.EquipItem>) null)
    });
  }

  private void OnQuery_STATUS()
  {
    this.tweenTarget = new StatusTop.UI?();
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    GameSection.SetEventData((object) new object[3]
    {
      (object) this.localEquipSet[this.equipSetNo],
      (object) MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSetAbility(this.equipSetNo),
      (object) new EquipSetDetailStatusAndAbilityTable.BaseStatus((int) userStatus.atk, (int) userStatus.def, (int) userStatus.hp, (List<CharaInfo.EquipItem>) null)
    });
  }

  private void OnQuery_CHARA_MAKE()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) MonoBehaviourSingleton<UserInfoManager>.I.userInfo,
      (object) MonoBehaviourSingleton<UserInfoManager>.I.userStatus
    });
  }

  private void OnQuery_CHANGE_SET_NAME()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.equipSetNo,
      (object) this.localEquipSet[this.equipSetNo]
    });
  }

  private void OnQuery_EQUIP_SET_COPY()
  {
    this.equipSetCopyMode = StatusTop.EQUIP_SET_COPY_MODE.COPY;
    this.equipSetCopyNo = this.equipSetNo;
    this.equipSetCopyForm = this.CopyEquipSetInfo(this.localEquipSet[this.equipSetNo], this.equipSetNo);
    this.DrawEquipSetCopyModeButton();
  }

  private void OnQuery_EQUIP_SET_PASTE() => GameSection.ChangeEvent("EQUIP_SET_PASTE_CONFIRM");

  private void ResetEquipSetCopy()
  {
    this.equipSetCopyMode = StatusTop.EQUIP_SET_COPY_MODE.NONE;
    this.equipSetCopyNo = 0;
  }

  protected void OnQuery_StatusTopEquipSetPasteConfirm_YES()
  {
    GameSection.SetEventData((object) null);
    GameSection.StayEvent();
    this.equipSetCopyForm.no = this.equipSetNo;
    MonoBehaviourSingleton<InventoryManager>.I.SendInventoryEquipSetCopy(this.equipSetCopyForm, (Action<bool>) (is_success =>
    {
      if (is_success)
      {
        if (MonoBehaviourSingleton<StatusManager>.IsValid())
          MonoBehaviourSingleton<StatusManager>.I.UpdateLocalEquipSet(this.equipSetNo);
        this.RefreshUI();
        this.ResetEquipSetCopy();
        this.DrawEquipSetCopyModeButton();
      }
      GameSection.ResumeEvent(is_success);
    }));
  }

  private void OnQuery_EQUIP_SET_DELETE()
  {
    this.ResetEquipSetCopy();
    this.DrawEquipSetCopyModeButton();
  }

  private void OnCloseDialog_StatusChangedEquipSetName()
  {
    this.SetLabelText((Enum) StatusTop.UI.LBL_SET_NAME, this.localEquipSet[this.equipSetNo].name);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_GROW | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE | GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE | GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_SMITH_BADGE | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET_INFO;
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.localEquipSetUpdate();
      if (this.visualDetailEquip != null && this.visualDetailItemIndex != -1)
        this.visualEquip.visualItem[this.visualDetailItemIndex] = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(this.visualDetailEquip.uniqueID);
    }
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE) != (GameSection.NOTIFY_FLAG) 0)
      MonoBehaviourSingleton<StatusManager>.I.ReplaceEquipSet(this.localEquipSet[this.equipSetNo], this.equipSetNo);
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET_INFO) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.ForceSettingEquipSetInfo();
      this.DrawEquipSetModel();
    }
    base.OnNotify(flags);
  }

  private void localEquipSetUpdate()
  {
    int index1 = 0;
    for (int length1 = this.localEquipSet.Length; index1 < length1; ++index1)
    {
      int index2 = 0;
      for (int length2 = this.localEquipSet[index1].item.Length; index2 < length2; ++index2)
      {
        EquipItemInfo equipItemInfo = this.localEquipSet[index1].item[index2];
        if (equipItemInfo != null)
          this.localEquipSet[index1].item[index2] = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(equipItemInfo.uniqueID);
      }
    }
    if (!MonoBehaviourSingleton<StatusManager>.I.isEquipSetCalcUpdate)
      return;
    MonoBehaviourSingleton<StatusManager>.I.ReplaceEquipSets(this.localEquipSet);
  }

  private InventoryManager.INVENTORY_TYPE GetInventoryType(int index)
  {
    switch (index)
    {
      case 3:
        return InventoryManager.INVENTORY_TYPE.ARMOR;
      case 4:
        return InventoryManager.INVENTORY_TYPE.HELM;
      case 5:
        return InventoryManager.INVENTORY_TYPE.ARM;
      case 6:
        return InventoryManager.INVENTORY_TYPE.LEG;
      default:
        EquipItemInfo equipItemInfo = this.localEquipSet[this.equipSetNo].item[index];
        return equipItemInfo != null ? (InventoryManager.INVENTORY_TYPE) (UIBehaviour.GetEquipmentTypeIndex(equipItemInfo.tableData.type) + 1) : InventoryManager.INVENTORY_TYPE.ONE_HAND_SWORD;
    }
  }

  private void OnQuery_DETAIL()
  {
    int eventData = (int) GameSection.GetEventData();
    EquipItemInfo equipItemInfo = this.localEquipSet[this.equipSetNo].item[eventData];
    if (equipItemInfo == null)
    {
      GameSection.StopEvent();
    }
    else
    {
      this.detailEquipSetNo = this.equipSetNo;
      StatusEquip.LocalEquipSetData localEquipSetData = new StatusEquip.LocalEquipSetData(this.detailEquipSetNo, eventData, this.localEquipSet[this.detailEquipSetNo]);
      GameSection.SetEventData((object) new object[4]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.STATUS_TOP,
        (object) equipItemInfo,
        (object) this.equipSetNo,
        (object) localEquipSetData
      });
    }
  }

  private void OnQuery_SKILL_ICON_BUTTON()
  {
    EquipItemInfo equipItemInfo = this.localEquipSet[this.equipSetNo].item[(int) GameSection.GetEventData()];
    if (equipItemInfo == null)
    {
      GameSection.StopEvent();
    }
    else
    {
      this.detailEquipSetNo = this.equipSetNo;
      GameSection.SetEventData((object) new object[2]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.STATUS_TOP,
        (object) equipItemInfo
      });
    }
  }

  private void OnQuery_VISUAL_DETAIL()
  {
    int eventData = (int) GameSection.GetEventData();
    if (this.visualEquip.visualItem.Length <= eventData || this.visualEquip.visualItem[eventData] == null)
      return;
    this.visualDetailEquip = this.visualEquip.visualItem[eventData];
    this.visualDetailItemIndex = eventData;
    GameSection.ChangeEvent("DETAIL");
    GameSection.SetEventData((object) new object[3]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.STATUS_TOP,
      (object) this.visualEquip.visualItem[eventData],
      (object) this.equipSetNo
    });
  }

  private void ResetDetailTarget()
  {
    this.detailEquipSetNo = -1;
    this.visualDetailEquip = (EquipItemInfo) null;
    this.visualDetailItemIndex = -1;
  }

  public static InventoryManager.INVENTORY_TYPE GetInventoryType(EquipSetInfo setInfo, int index)
  {
    switch (index)
    {
      case 3:
        return InventoryManager.INVENTORY_TYPE.ARMOR;
      case 4:
        return InventoryManager.INVENTORY_TYPE.HELM;
      case 5:
        return InventoryManager.INVENTORY_TYPE.ARM;
      case 6:
        return InventoryManager.INVENTORY_TYPE.LEG;
      default:
        EquipItemInfo equipItemInfo = setInfo.item[index];
        return equipItemInfo != null ? (InventoryManager.INVENTORY_TYPE) (UIBehaviour.GetEquipmentTypeIndex(equipItemInfo.tableData.type) + 1) : InventoryManager.INVENTORY_TYPE.ONE_HAND_SWORD;
    }
  }

  private void OnQuery_EQUIP_SET_LIST() => GameSection.SetEventData((object) this.equipSetNo);

  private void OnCloseDialog_StatusEquipSetList()
  {
    if (GameSection.GetEventData() == null)
    {
      this.RefreshUI();
    }
    else
    {
      int eventData = (int) GameSection.GetEventData();
      if (eventData == this.equipSetNo)
        return;
      this.equipSetNo = eventData;
      this.SetCenter(this.GetCtrl((Enum) StatusTop.UI.GRD_DRUM), this.equipSetNo, true);
    }
  }

  private enum UI
  {
    OBJ_STATUS_UI_ROOT,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    BTN_EQUIP_SET_L,
    BTN_EQUIP_SET_R,
    LBL_NOW,
    LBL_MAX,
    OBJ_EQUIP_ROOT,
    OBJ_EQUIP_ROT_ROOT,
    OBJ_ICON_WEAPON_1,
    OBJ_ICON_WEAPON_2,
    OBJ_ICON_WEAPON_3,
    OBJ_ICON_ARMOR,
    OBJ_ICON_HELM,
    OBJ_ICON_ARM,
    OBJ_ICON_LEG,
    BTN_ICON_WEAPON_1,
    BTN_ICON_WEAPON_2,
    BTN_ICON_WEAPON_3,
    BTN_ICON_ARMOR,
    BTN_ICON_HELM,
    BTN_ICON_ARM,
    BTN_ICON_LEG,
    OBJ_VISUAL_ROOT,
    OBJ_ICON_VISUAL_ARMOR,
    OBJ_ICON_VISUAL_HELM,
    OBJ_ICON_VISUAL_ARM,
    OBJ_ICON_VISUAL_LEG,
    BTN_ICON_VISUAL_ARMOR_BASE,
    BTN_ICON_VISUAL_HELM_BASE,
    BTN_ICON_VISUAL_ARM_BASE,
    BTN_ICON_VISUAL_LEG_BASE,
    SPR_AVATAR_ACTIVE,
    SPR_PARAMETER_ACTIVE,
    BTN_AVATAR_INACTIVE,
    BTN_PARAMETER_INACTIVE,
    OBJ_EQUIP_SET_SELECT,
    OBJ_STUDIO_BUTTON_ROOT,
    OBJ_AVATAR_BUTTON_ROOT,
    OBJ_PARAMETER_BUTTON_ROOT,
    BTN_VISIBLE_UI,
    BTN_INVISIBLE_UI,
    BTN_VISIBLE_HELM,
    BTN_INVISIBLE_HELM,
    TGL_VISIBLE_HELM_BUTTON,
    TGL_VISIBLE_UI_BUTTON,
    LBL_LEVEL_WEAPON_1,
    LBL_LEVEL_WEAPON_2,
    LBL_LEVEL_WEAPON_3,
    LBL_LEVEL_ARMOR,
    LBL_LEVEL_HELM,
    LBL_LEVEL_ARM,
    LBL_LEVEL_LEG,
    LBL_LEVEL_WEAPON_1_SHADOW,
    LBL_LEVEL_WEAPON_2_SHADOW,
    LBL_LEVEL_WEAPON_3_SHADOW,
    LBL_LEVEL_ARMOR_SHADOW,
    LBL_LEVEL_HELM_SHADOW,
    LBL_LEVEL_ARM_SHADOW,
    LBL_LEVEL_LEG_SHADOW,
    TGL_SHOW_EQUIP_TYPE,
    BTN_STUDIO,
    BTN_EQUIPLIST,
    OBJ_SKILL_BUTTON_ROOT,
    OBJ_WITH_MONSTER_ROOT,
    OBJ_WITHOUT_MONSTER_ROOT,
    BTN_EQUIP_SET_COPY,
    BTN_EQUIP_SET_PASTE,
    BTN_EQUIP_SET_DELETE,
    LBL_SET_NAME,
    SCR_DRUM,
    GRD_DRUM,
    LBL_EQUIP_NO,
    ANCHOR_RIGHT_TOP,
    BTN_UNIQUE,
  }

  private enum EQUIP_SET_COPY_MODE
  {
    NONE,
    COPY,
  }
}
