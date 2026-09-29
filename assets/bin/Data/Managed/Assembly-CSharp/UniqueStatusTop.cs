// Decompiled with JetBrains decompiler
// Type: UniqueStatusTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UniqueStatusTop : SkillInfoBase
{
  public EquipSetInfo[] localEquipSet;
  public int equipSetNo;
  private int SET_NO_MAX = MonoBehaviourSingleton<StatusManager>.I.UniqueEquipSetNum();
  private UniqueStatusTop.UI[] icons = new UniqueStatusTop.UI[7]
  {
    UniqueStatusTop.UI.OBJ_ICON_WEAPON_1,
    UniqueStatusTop.UI.OBJ_ICON_WEAPON_2,
    UniqueStatusTop.UI.OBJ_ICON_WEAPON_3,
    UniqueStatusTop.UI.OBJ_ICON_ARMOR,
    UniqueStatusTop.UI.OBJ_ICON_HELM,
    UniqueStatusTop.UI.OBJ_ICON_ARM,
    UniqueStatusTop.UI.OBJ_ICON_LEG
  };
  private UniqueStatusTop.UI[] iconsBtn = new UniqueStatusTop.UI[7]
  {
    UniqueStatusTop.UI.BTN_ICON_WEAPON_1,
    UniqueStatusTop.UI.BTN_ICON_WEAPON_2,
    UniqueStatusTop.UI.BTN_ICON_WEAPON_3,
    UniqueStatusTop.UI.BTN_ICON_ARMOR,
    UniqueStatusTop.UI.BTN_ICON_HELM,
    UniqueStatusTop.UI.BTN_ICON_ARM,
    UniqueStatusTop.UI.BTN_ICON_LEG
  };
  private UniqueStatusTop.UI[] lblEquipLevel = new UniqueStatusTop.UI[7]
  {
    UniqueStatusTop.UI.LBL_LEVEL_WEAPON_1,
    UniqueStatusTop.UI.LBL_LEVEL_WEAPON_2,
    UniqueStatusTop.UI.LBL_LEVEL_WEAPON_3,
    UniqueStatusTop.UI.LBL_LEVEL_ARMOR,
    UniqueStatusTop.UI.LBL_LEVEL_HELM,
    UniqueStatusTop.UI.LBL_LEVEL_ARM,
    UniqueStatusTop.UI.LBL_LEVEL_LEG
  };
  private UniqueStatusTop.UI[] lblShadowEquipLevel = new UniqueStatusTop.UI[7]
  {
    UniqueStatusTop.UI.LBL_LEVEL_WEAPON_1_SHADOW,
    UniqueStatusTop.UI.LBL_LEVEL_WEAPON_2_SHADOW,
    UniqueStatusTop.UI.LBL_LEVEL_WEAPON_3_SHADOW,
    UniqueStatusTop.UI.LBL_LEVEL_ARMOR_SHADOW,
    UniqueStatusTop.UI.LBL_LEVEL_HELM_SHADOW,
    UniqueStatusTop.UI.LBL_LEVEL_ARM_SHADOW,
    UniqueStatusTop.UI.LBL_LEVEL_LEG_SHADOW
  };
  private UniqueStatusTop.UI? tweenTarget;
  private UICenterOnChild uiCenterOnChild;
  private int showHelm;
  private int detailEquipSetNo = -1;
  private EquipItemInfo visualDetailEquip;
  private int visualDetailItemIndex = -1;

  public override bool useOnPressBackKey => true;

  public override void OnPressBackKey() => this.DispatchEvent(GameSection.GetGoingHomeEvent());

  public override void Initialize()
  {
    this.tweenTarget = new UniqueStatusTop.UI?();
    this.SetActive((Enum) UniqueStatusTop.UI.OBJ_WITH_MONSTER_ROOT, true);
    this.SetActive((Enum) UniqueStatusTop.UI.OBJ_WITHOUT_MONSTER_ROOT, false);
    this.SettingEquipSetInfo();
    this.SetDynamicList((Enum) UniqueStatusTop.UI.GRD_DRUM, "equipno", this.SET_NO_MAX, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, isRecycle) => this.SetLabelText(t, (Enum) UniqueStatusTop.UI.LBL_EQUIP_NO, (i + 1).ToString())));
    this.SetCenterOnChildFunc((Enum) UniqueStatusTop.UI.GRD_DRUM, (SpringPanel.OnFinished) (() =>
    {
      int result = this.equipSetNo;
      if (int.TryParse(this.GetLabel(this.GetCenter(this.GetCtrl((Enum) UniqueStatusTop.UI.GRD_DRUM)), (Enum) UniqueStatusTop.UI.LBL_EQUIP_NO), out result))
        this.equipSetNo = Mathf.Clamp(result - 1, 0, this.SET_NO_MAX - 1);
      MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(this.equipSetNo);
      this.tweenTarget = new UniqueStatusTop.UI?(UniqueStatusTop.UI.OBJ_EQUIP_ROOT);
      this.RefreshUI();
    }));
    this.SetCenter(this.GetCtrl((Enum) UniqueStatusTop.UI.GRD_DRUM), this.equipSetNo, true);
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
        this.localEquipSet[changeEquipData.setNo].item[changeEquipData.index] = changeEquipData.item;
        MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipItem(this.localEquipSet[changeEquipData.setNo], changeEquipData.setNo, changeEquipData.index);
        break;
      case StatusEquip.ChangeEquipData[] _:
        StatusEquip.ChangeEquipData[] changeEquipDataArray = eventData as StatusEquip.ChangeEquipData[];
        for (int index = 0; index < changeEquipDataArray.Length; ++index)
        {
          if ((changeEquipDataArray[index].index != 0 || changeEquipDataArray[index].item != null) && (changeEquipDataArray[index].index != 3 || changeEquipDataArray[index].item != null))
          {
            this.localEquipSet[changeEquipDataArray[index].setNo].item[changeEquipDataArray[index].index] = changeEquipDataArray[index].item;
            MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipItem(this.localEquipSet[changeEquipDataArray[index].setNo], changeEquipDataArray[index].setNo, changeEquipDataArray[index].index);
          }
        }
        break;
    }
    this.localEquipSetUpdate();
    this.SetDynamicList((Enum) UniqueStatusTop.UI.GRD_DRUM, "equipno", this.SET_NO_MAX, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, isRecycle) => this.SetLabelText(t, (Enum) UniqueStatusTop.UI.LBL_EQUIP_NO, (i + 1).ToString())));
    this.SetCenter(this.GetCtrl((Enum) UniqueStatusTop.UI.GRD_DRUM), this.equipSetNo, true);
  }

  protected override void OnCloseStart()
  {
    this.ResetDetailTarget();
    this.OnClose();
  }

  private void UpdateModel()
  {
    PlayerLoadInfo load_info = new PlayerLoadInfo();
    EquipItemInfo equipItemInfo = this.localEquipSet[this.equipSetNo].item[3];
    load_info.SetupLoadInfo(this.localEquipSet[this.equipSetNo], 0UL, 0UL, 0UL, 0UL, 0UL, this.localEquipSet[this.equipSetNo].showHelm == 1);
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    int anim_id = 0;
    if (load_info.bodyModelID <= 0)
      load_info.SetEquipBody(userStatus.sex, (uint) MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerArmEquipItemID);
    if (this.localEquipSet[this.equipSetNo].item[0] == null)
      anim_id = 98;
    if (!MonoBehaviourSingleton<StatusStageManager>.IsValid())
      return;
    MonoBehaviourSingleton<StatusStageManager>.I.LoadPlayer(load_info, anim_id);
  }

  public override void Exit()
  {
    MonoBehaviourSingleton<StatusManager>.I.isEquipSetCalcUpdate = true;
    MonoBehaviourSingleton<StatusManager>.I.CheckChangeUniqueEquip((Action<bool>) (is_success => base.Exit()));
  }

  public void SettingEquipSetInfo()
  {
    if (this.localEquipSet != null)
      return;
    this.localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
    this.equipSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ueSetNo;
    MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(this.equipSetNo);
    this.showHelm = this.localEquipSet[this.equipSetNo].showHelm;
  }

  public void ForceSettingEquipSetInfo()
  {
    this.localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
    this.SET_NO_MAX = MonoBehaviourSingleton<StatusManager>.I.UniqueEquipSetNum();
    this.DrawEquipSetModel();
  }

  public override void UpdateUI()
  {
    this.SetBadge((Enum) UniqueStatusTop.UI.BTN_STUDIO, MonoBehaviourSingleton<SmithManager>.I.GetBadgeTotalNum(), (SpriteAlignment) 1, 8, -8, true);
    this.DrawEquipModeButton();
    int sex = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
    EquipSetInfo localEquip = this.localEquipSet[this.equipSetNo];
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
        this.SetSkillIconButton(ctrl, (Enum) UniqueStatusTop.UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButtonTOP", equipItemInfo.tableData, this.GetSkillSlotData(equipItemInfo), button_event_data: index1);
      ((Component) this.FindCtrl(ctrl, (Enum) UniqueStatusTop.UI.OBJ_SKILL_BUTTON_ROOT)).gameObject.SetActive(flag);
    }
    this.DrawEquipSetModel();
    if (this.tweenTarget.HasValue)
    {
      this.ResetTween((Enum) (ValueType) this.tweenTarget);
      this.PlayTween((Enum) (ValueType) this.tweenTarget, is_input_block: false);
    }
    if (this.localEquipSet[this.equipSetNo].showHelm != this.showHelm)
    {
      this.ResetTween((Enum) UniqueStatusTop.UI.BTN_VISIBLE_HELM);
      this.ResetTween((Enum) UniqueStatusTop.UI.BTN_INVISIBLE_HELM);
      if (this.localEquipSet[this.equipSetNo].showHelm == 1)
        this.PlayTween((Enum) UniqueStatusTop.UI.BTN_INVISIBLE_HELM, is_input_block: false);
      else
        this.PlayTween((Enum) UniqueStatusTop.UI.BTN_VISIBLE_HELM, is_input_block: false);
      this.showHelm = this.localEquipSet[this.equipSetNo].showHelm;
    }
    this.SetToggleButton((Enum) UniqueStatusTop.UI.TGL_VISIBLE_HELM_BUTTON, this.showHelm == 1, (Action<bool>) (is_active =>
    {
      this.localEquipSet[this.equipSetNo].showHelm = is_active ? 1 : 0;
      this.showHelm = this.localEquipSet[this.equipSetNo].showHelm;
      this.ResetTween((Enum) UniqueStatusTop.UI.BTN_VISIBLE_HELM);
      this.ResetTween((Enum) UniqueStatusTop.UI.BTN_INVISIBLE_HELM);
      if (is_active)
        this.PlayTween((Enum) UniqueStatusTop.UI.BTN_INVISIBLE_HELM, is_input_block: false);
      else
        this.PlayTween((Enum) UniqueStatusTop.UI.BTN_VISIBLE_HELM, is_input_block: false);
      this.UpdateModel();
    }));
    this.SetDynamicList((Enum) UniqueStatusTop.UI.GRD_DRUM, "equipno", this.SET_NO_MAX, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, isRecycle) => this.SetLabelText(t, (Enum) UniqueStatusTop.UI.LBL_EQUIP_NO, (i + 1).ToString())));
    this.SetButtonEnabled((Enum) UniqueStatusTop.UI.BTN_MAGI_REMOVE, MonoBehaviourSingleton<StatusManager>.I.checkEquipMagi(this.equipSetNo));
    base.UpdateUI();
  }

  public void DrawEquipSetModel()
  {
    SimpleStatus finalStatus = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquipSetCalculator(this.equipSetNo).GetFinalStatus(0, MonoBehaviourSingleton<UserInfoManager>.I.userStatus);
    this.SetLabelText((Enum) UniqueStatusTop.UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
    this.SetLabelText((Enum) UniqueStatusTop.UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
    this.SetLabelText((Enum) UniqueStatusTop.UI.LBL_HP, finalStatus.hp.ToString());
    this.SetLabelText((Enum) UniqueStatusTop.UI.LBL_NOW, (this.equipSetNo + 1).ToString());
    this.SetLabelText((Enum) UniqueStatusTop.UI.LBL_MAX, this.SET_NO_MAX.ToString());
    this.SetLabelText((Enum) UniqueStatusTop.UI.LBL_SET_NAME, this.localEquipSet[this.equipSetNo].name);
    this.UpdateModel();
  }

  private void DrawEquipModeButton()
  {
    this.SetActive((Enum) UniqueStatusTop.UI.OBJ_EQUIP_ROOT, true);
    this.SetActive((Enum) UniqueStatusTop.UI.OBJ_EQUIP_SET_SELECT, true);
    this.SetActive((Enum) UniqueStatusTop.UI.SPR_PARAMETER_ACTIVE, true);
  }

  private void OnQuery_EQUIP()
  {
    this.tweenTarget = new UniqueStatusTop.UI?();
    int eventData = (int) GameSection.GetEventData();
    int equipSetNo = this.equipSetNo;
    int index = equipSetNo == 0 ? eventData : eventData % (equipSetNo << 16 /*0x10*/);
    if (equipSetNo >= this.localEquipSet.Length || index >= 7)
      return;
    MonoBehaviourSingleton<StatusManager>.I.SetEquippingItem(this.localEquipSet[equipSetNo].item[index]);
    MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = this.GetInventoryType(index);
    GameSection.SetEventData((object) new StatusEquip.LocalEquipSetData(equipSetNo, index, this.localEquipSet[equipSetNo]));
  }

  private void OnQuery_MODE_EQUIP()
  {
    this.ResetDetailTarget();
    this.tweenTarget = new UniqueStatusTop.UI?(UniqueStatusTop.UI.OBJ_EQUIP_ROT_ROOT);
    if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
      MonoBehaviourSingleton<StatusStageManager>.I.SetViewMode(StatusStageManager.VIEW_MODE.EQUIP);
    this.ResetTween((Enum) UniqueStatusTop.UI.OBJ_PARAMETER_BUTTON_ROOT);
    this.PlayTween((Enum) UniqueStatusTop.UI.OBJ_PARAMETER_BUTTON_ROOT, is_input_block: false);
    this.RefreshUI();
  }

  private void OnQuery_TO_EVENT() => this.ToSeriesArena();

  private void OnQuery_TO_STORAGE() => this.CheckEquipChange();

  private void OnQuery_STUDIO() => this.CheckEquipChange();

  protected override void OnQuery_MAIN_MENU_QUEST()
  {
    this.tweenTarget = new UniqueStatusTop.UI?();
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.CheckChangeUniqueEquip((Action<bool>) (is_success =>
    {
      GameSection.ResumeEvent(is_success);
      base.OnQuery_MAIN_MENU_QUEST();
    }));
  }

  private void CheckEquipChange()
  {
    this.tweenTarget = new UniqueStatusTop.UI?();
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.CheckChangeUniqueEquip((Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  private void OnQuery_SKILL_LIST()
  {
    this.tweenTarget = new UniqueStatusTop.UI?();
    GameSection.SetEventData((object) new object[4]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.STATUS_TOP,
      (object) this.GetLocalEquipSetAttachSkillListData(this.equipSetNo),
      (object) false,
      (object) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex
    });
  }

  private void OnQuery_ABILITY()
  {
    this.tweenTarget = new UniqueStatusTop.UI?();
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
    this.tweenTarget = new UniqueStatusTop.UI?();
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    GameSection.SetEventData((object) new object[3]
    {
      (object) this.localEquipSet[this.equipSetNo],
      (object) MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSetAbility(this.equipSetNo),
      (object) new EquipSetDetailStatusAndAbilityTable.BaseStatus((int) userStatus.atk, (int) userStatus.def, (int) userStatus.hp, (List<CharaInfo.EquipItem>) null)
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

  private void OnCloseDialog_UniqueStatusChangedEquipSetName()
  {
    this.SetLabelText((Enum) UniqueStatusTop.UI.LBL_SET_NAME, this.localEquipSet[this.equipSetNo].name);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_GROW | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE | GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE | GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_SMITH_BADGE | GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET_INFO;
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE) != (GameSection.NOTIFY_FLAG) 0)
      this.localEquipSetUpdate();
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE) != (GameSection.NOTIFY_FLAG) 0)
      MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipSets(this.localEquipSet);
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET_INFO) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.ForceSettingEquipSetInfo();
      this.DrawEquipSetModel();
    }
    base.OnNotify(flags);
  }

  private void localEquipSetUpdate()
  {
    if (this.localEquipSet == null)
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
    }
    if (!MonoBehaviourSingleton<StatusManager>.I.isEquipSetCalcUpdate)
      return;
    MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipSets(this.localEquipSet);
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
      this.SetCenter(this.GetCtrl((Enum) UniqueStatusTop.UI.GRD_DRUM), this.equipSetNo, true);
    }
  }

  private void OnQuery_MAGI_REMOVE()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) (this.equipSetNo + 1)
    });
  }

  private void OnQuery_StatusMagiAllRemoveConfirm_YES()
  {
    this.StartCoroutine(this.sendMagiAllRemove());
  }

  protected IEnumerator sendMagiAllRemove()
  {
    bool wait = true;
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.RemoveOrderNo(this.equipSetNo, (Action<bool>) (is_succses => wait = false));
    while (wait)
      yield return (object) null;
    wait = true;
    MonoBehaviourSingleton<StatusManager>.I.CheckChangeUniqueEquipSet((Action<bool>) (is_success => wait = false));
    while (wait)
      yield return (object) null;
    wait = true;
    MonoBehaviourSingleton<StatusManager>.I.SendDetachAllSkillFromEvery(this.equipSetNo, (Action<bool>) (issuccess => wait = false));
    while (wait)
      yield return (object) null;
    GameSection.ResumeEvent(true);
  }

  private void OnQuery_EQUIP_SET_L()
  {
    if (this.equipSetNo > 0)
      --this.equipSetNo;
    else
      this.equipSetNo = this.SET_NO_MAX - 1;
    MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(this.equipSetNo);
    this.tweenTarget = new UniqueStatusTop.UI?(UniqueStatusTop.UI.OBJ_EQUIP_ROOT);
    this.SetCenter(this.GetCtrl((Enum) UniqueStatusTop.UI.GRD_DRUM), this.equipSetNo, true);
  }

  private void OnQuery_EQUIP_SET_R()
  {
    if (this.equipSetNo < this.SET_NO_MAX - 1)
      ++this.equipSetNo;
    else
      this.equipSetNo = 0;
    MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(this.equipSetNo);
    this.tweenTarget = new UniqueStatusTop.UI?(UniqueStatusTop.UI.OBJ_EQUIP_ROOT);
    this.SetCenter(this.GetCtrl((Enum) UniqueStatusTop.UI.GRD_DRUM), this.equipSetNo, true);
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
    SPR_PARAMETER_ACTIVE,
    BTN_PARAMETER_INACTIVE,
    OBJ_EQUIP_SET_SELECT,
    OBJ_STUDIO_BUTTON_ROOT,
    OBJ_PARAMETER_BUTTON_ROOT,
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
    LBL_SET_NAME,
    SCR_DRUM,
    GRD_DRUM,
    LBL_EQUIP_NO,
    OBJ_BACK,
    BTN_STATUS,
    BTN_MAGI_REMOVE,
  }
}
