// Decompiled with JetBrains decompiler
// Type: QuestOffLineChangeEquipSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class QuestOffLineChangeEquipSet : QuestChangeEquipSet
{
  private bool showEquipMode = true;
  private EquipSetInfo[] localEquipSets;
  private QuestOffLineChangeEquipSet.EQUIP_SET_COPY_MODE equipSetCopyMode;
  private int equipSetCopyNo = -1;
  private StatusEquipSetCopyModel.RequestSendForm equipSetCopyForm;

  protected override bool IsFriendInfo => false;

  public override void Initialize()
  {
    this.isChangeEquip = true;
    this.isVisualMode = false;
    this.isSelfData = true;
    this.GetUserRecordStatus();
    this.localEquipSets = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
    this.localEquipSet = this.localEquipSets[this.selfCharaEquipSetNo];
    this.transRoot = this.SetPrefab((Enum) QuestOffLineChangeEquipSet.UI.OBJ_EQUIP_SET_ROOT, this.GetEquipSetBasePrefabName());
    this.StartCoroutine(this.DoInitialize());
  }

  protected virtual string GetEquipSetBasePrefabName() => "OfflineChangeEquipSetBase";

  protected virtual void GetUserRecordStatus()
  {
    this.selfCharaEquipSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
    this.record = this.InitializePlayerRecord();
  }

  public override void UpdateUI()
  {
    this.localEquipSet = this.localEquipSets[this.selfCharaEquipSetNo];
    this.UpdateEquipSetUI();
    this.ViewDetailUI();
    this.ResetTween((Enum) QuestOffLineChangeEquipSet.UI.OBJ_EQUIP_ROOT);
    this.PlayTween((Enum) QuestOffLineChangeEquipSet.UI.OBJ_EQUIP_ROOT, callback: (EventDelegate.Callback) (() => { }), is_input_block: false);
    this.UpdateCopyModeButton();
  }

  protected virtual void ViewDetailUI() => this.OnUpdateFriendDetailUI();

  private new void OnEnable()
  {
    InputManager.OnDragAlways += new InputManager.OnTouchDelegate(this.OnDrag);
  }

  private new void OnDisable()
  {
    InputManager.OnDragAlways -= new InputManager.OnTouchDelegate(this.OnDrag);
    this.nowSectionName = string.Empty;
  }

  private void OnDrag(InputManager.TouchInfo touch_info)
  {
    if (Object.op_Equality((Object) this.loader, (Object) null) || MonoBehaviourSingleton<UIManager>.I.IsDisable() || !this.CanRotateSection())
      return;
    ((Component) this.loader).transform.Rotate(GameDefine.GetCharaRotateVector(touch_info));
  }

  private bool CanRotateSection()
  {
    string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
    return currentSectionName == "QuestDeliveryEquipChangeEquipSet" || currentSectionName == "QuestAcceptArenaRoomChangeEquipSet";
  }

  protected override void UpdateEquipSkillButton(EquipItemInfo item, int i)
  {
    Transform ctrl = this.GetCtrl((Enum) this.icons_btn[i]);
    bool flag = item != null && item.tableID > 0U;
    if (flag)
      this.SetSkillIconButton(ctrl, (Enum) QuestOffLineChangeEquipSet.UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButtonTOP", item.tableData, this.GetSkillSlotData(item), button_event_data: i);
    ((Component) this.FindCtrl(ctrl, (Enum) QuestOffLineChangeEquipSet.UI.OBJ_SKILL_BUTTON_ROOT)).gameObject.SetActive(flag);
  }

  protected override void ReloadModel()
  {
    this.SetLabelText(this.transRoot, (Enum) QuestOffLineChangeEquipSet.UI.LBL_SET_NAME, this.localEquipSet.name);
    this.ReloadPlayerModelByLocalEquipSet();
  }

  private IEnumerator ReloadModelByLocalEquipSetCoroutine()
  {
    while (UIModelRenderTexture.Get(this.FindCtrl(this.transRoot, (Enum) QuestOffLineChangeEquipSet.UI.TEX_MODEL)).IsLoadingPlayer())
      yield return (object) null;
    this.ReloadPlayerModelByLocalEquipSet();
  }

  protected void ReloadPlayerModelByLocalEquipSet()
  {
    this.SetLabelText(this.transRoot, (Enum) QuestOffLineChangeEquipSet.UI.LBL_SET_NAME, this.localEquipSet.name);
    this.record.playerLoadInfo = PlayerLoadInfo.FromUserStatus(true, this.isVisualMode, this.selfCharaEquipSetNo);
    this.PlayerLoad(this.record);
    this.SetRenderPlayerModel(this.record.playerLoadInfo);
  }

  protected virtual PlayerLoadInfo GetFromUserStatus()
  {
    return PlayerLoadInfo.FromUserStatus(true, this.isVisualMode, this.selfCharaEquipSetNo);
  }

  protected virtual void PlayerLoad(InGameRecorder.PlayerRecord record)
  {
    record.playerLoadInfo.SetupLoadInfo(this.localEquipSet, 0UL, 0UL, 0UL, 0UL, 0UL, this.localEquipSet.showHelm == 1);
  }

  protected override void OnQuery_SKILL_LIST()
  {
    GameSection.SetEventData((object) new object[4]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT,
      (object) this.GetLocalEquipSetAttachSkillListData(this.selfCharaEquipSetNo),
      (object) false,
      (object) this.record.charaInfo.sex
    });
  }

  protected override void OnQuery_ABILITY() => this.OnQueryAbilityBase();

  protected override void OnQuery_STATUS() => this.OnQueryStatusBase();

  private void OnQuery_SKILL_ICON_BUTTON()
  {
    EquipItemInfo equipItemInfo = this.localEquipSet.item[(int) GameSection.GetEventData()];
    if (equipItemInfo == null)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) new object[2]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.STATUS_TOP,
        (object) equipItemInfo
      });
  }

  protected override void OnQuery_DETAIL()
  {
    int eventData = (int) GameSection.GetEventData();
    if (this.isVisualMode)
    {
      GameSection.ChangeEvent("VISUAL_DETAIL");
      this.OnQuery_VISUAL_DETAIL();
    }
    else
    {
      StatusEquip.LocalEquipSetData localEquip = new StatusEquip.LocalEquipSetData(this.selfCharaEquipSetNo, eventData, this.localEquipSet);
      object[] selfEventData = this.CreateSelfEventData(eventData);
      if (this.localEquipSet.item[eventData] == null)
      {
        MonoBehaviourSingleton<StatusManager>.I.SetEquippingItem((EquipItemInfo) null);
        MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = StatusTop.GetInventoryType(this.localEquipSet, eventData);
        GameSection.ChangeEvent("CHANGE_EQUIP", (object) new ItemDetailEquip.DetailEquipEventData(selfEventData, localEquip));
      }
      else
      {
        object[] event_data = new object[selfEventData.Length + 1];
        int index = 0;
        for (int length = selfEventData.Length; index < length; ++index)
          event_data[index] = selfEventData[index];
        event_data[1] = (object) this.GetLocalEquipSetAttachSkillListData(this.selfCharaEquipSetNo)[eventData];
        event_data[event_data.Length - 1] = (object) localEquip;
        GameSection.SetEventData((object) event_data);
      }
    }
  }

  private void OnQuery_CHANGE_SET_NAME()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.selfCharaEquipSetNo,
      (object) this.localEquipSets[this.selfCharaEquipSetNo]
    });
  }

  private void OnCloseDialog_QuestAcceptChangedEquipSetName()
  {
    this.SetLabelText((Enum) QuestOffLineChangeEquipSet.UI.LBL_SET_NAME, this.localEquipSets[this.selfCharaEquipSetNo].name);
  }

  protected override void OnQuery_SECTION_BACK()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.CheckChangeEquipSet(this.selfCharaEquipSetNo, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
    MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(-1);
    base.OnQuery_SECTION_BACK();
  }

  private void OnQuery_EQUIP_SET_COPY() => this.CopyEquipSet();

  private void CopyEquipSet()
  {
    this.equipSetCopyMode = QuestOffLineChangeEquipSet.EQUIP_SET_COPY_MODE.COPY;
    this.equipSetCopyNo = this.selfCharaEquipSetNo;
    this.equipSetCopyForm = this.CopyEquipSetInfo(this.localEquipSets[this.selfCharaEquipSetNo], this.selfCharaEquipSetNo);
    this.UpdateCopyModeButton();
  }

  private void OnQuery_EQUIP_SET_DELETE()
  {
    this.ResetEquipSetCopy();
    this.UpdateCopyModeButton();
  }

  protected virtual void UpdateCopyModeButton()
  {
    bool flag1 = this.selfCharaEquipSetNo == this.equipSetCopyNo;
    bool flag2 = this.equipSetCopyMode == QuestOffLineChangeEquipSet.EQUIP_SET_COPY_MODE.COPY;
    this.SetActive(this.transRoot, (Enum) QuestOffLineChangeEquipSet.UI.BTN_EQUIP_SET_COPY, !flag2);
    this.SetActive(this.transRoot, (Enum) QuestOffLineChangeEquipSet.UI.BTN_EQUIP_SET_PASTE, flag2 && !flag1);
    this.SetActive(this.transRoot, (Enum) QuestOffLineChangeEquipSet.UI.BTN_EQUIP_SET_DELETE, flag2 & flag1);
  }

  protected void OnQuery_QuestAcceptEquipSetPasteConfirm_YES()
  {
    GameSection.SetEventData((object) null);
    GameSection.StayEvent();
    this.equipSetCopyForm.no = this.selfCharaEquipSetNo;
    MonoBehaviourSingleton<InventoryManager>.I.SendInventoryEquipSetCopy(this.equipSetCopyForm, (Action<bool>) (is_success =>
    {
      if (is_success)
      {
        if (MonoBehaviourSingleton<StatusManager>.IsValid())
          MonoBehaviourSingleton<StatusManager>.I.UpdateLocalEquipSet(this.selfCharaEquipSetNo);
        this.ResetEquipSetCopy();
        this.localEquipSetUpdate();
        this.localEquipSet = this.localEquipSets[this.selfCharaEquipSetNo];
        this.StartCoroutine(this.ReloadModelByLocalEquipSetCoroutine());
        this.RefreshUI();
      }
      GameSection.ResumeEvent(is_success);
    }));
  }

  private void ResetEquipSetCopy()
  {
    this.equipSetCopyMode = QuestOffLineChangeEquipSet.EQUIP_SET_COPY_MODE.NONE;
    this.equipSetCopyNo = 0;
  }

  private void OnCloseDialog()
  {
    object eventData = GameSection.GetEventData();
    bool flag = false;
    if (eventData is StatusEquip.ChangeEquipData)
    {
      StatusEquip.ChangeEquipData changeEquipData = eventData as StatusEquip.ChangeEquipData;
      flag = changeEquipData.item != this.localEquipSets[changeEquipData.setNo].item[changeEquipData.index];
      if (this.showEquipMode)
      {
        this.localEquipSets[changeEquipData.setNo].item[changeEquipData.index] = changeEquipData.item;
        this.ReplaceEquipItem(this.localEquipSets[changeEquipData.setNo], changeEquipData.setNo, changeEquipData.index);
      }
      else
      {
        StatusManager.LocalVisual localVisualEquip = MonoBehaviourSingleton<StatusManager>.I.GetLocalVisualEquip();
        int index = changeEquipData.index;
        if (localVisualEquip.visualItem[index] != null)
        {
          long uniqueId1 = (long) localVisualEquip.visualItem[index].uniqueID;
        }
        localVisualEquip.visualItem[index] = changeEquipData.item;
        if (localVisualEquip.visualItem[index] != null)
        {
          long uniqueId2 = (long) localVisualEquip.visualItem[index].uniqueID;
        }
      }
      if (flag)
        this.localEquipSet = this.localEquipSets[changeEquipData.setNo];
    }
    else if (eventData is StatusEquip.ChangeEquipData[])
    {
      StatusEquip.ChangeEquipData[] changeEquipDataArray = eventData as StatusEquip.ChangeEquipData[];
      for (int index = 0; index < changeEquipDataArray.Length; ++index)
      {
        if ((changeEquipDataArray[index].index != 0 || changeEquipDataArray[index].item != null) && (changeEquipDataArray[index].index != 3 || changeEquipDataArray[index].item != null))
        {
          this.localEquipSets[changeEquipDataArray[index].setNo].item[changeEquipDataArray[index].index] = changeEquipDataArray[index].item;
          this.ReplaceEquipItem(this.localEquipSets[changeEquipDataArray[index].setNo], changeEquipDataArray[index].setNo, changeEquipDataArray[index].index);
          flag = true;
        }
      }
    }
    if (!flag)
      return;
    this.localEquipSetUpdate();
    this.RefreshUI();
    this.StartCoroutine(this.ReloadModelByLocalEquipSetCoroutine());
  }

  protected virtual void ReplaceEquipItem(EquipSetInfo equipSetInfo, int setNo, int index)
  {
    MonoBehaviourSingleton<StatusManager>.I.ReplaceEquipItem(equipSetInfo, setNo, index);
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE) != (GameSection.NOTIFY_FLAG) 0)
      MonoBehaviourSingleton<StatusManager>.I.ReplaceEquipSet(this.localEquipSet, this.selfCharaEquipSetNo);
    base.OnNotify(flags);
  }

  private void localEquipSetUpdate()
  {
    int index1 = 0;
    for (int length1 = this.localEquipSets.Length; index1 < length1; ++index1)
    {
      int index2 = 0;
      for (int length2 = this.localEquipSets[index1].item.Length; index2 < length2; ++index2)
      {
        EquipItemInfo equipItemInfo = this.localEquipSets[index1].item[index2];
        if (equipItemInfo != null)
          this.localEquipSets[index1].item[index2] = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(equipItemInfo.uniqueID);
      }
    }
    this.localEquipCalcUpdate(this.localEquipSets);
  }

  public virtual void localEquipCalcUpdate(EquipSetInfo[] equipSets)
  {
    if (!MonoBehaviourSingleton<StatusManager>.I.isEquipSetCalcUpdate)
      return;
    MonoBehaviourSingleton<StatusManager>.I.ReplaceEquipSets(equipSets);
  }

  private void OnQuery_AUTO_EQUIP()
  {
    int selfCharaEquipSetNo = this.selfCharaEquipSetNo;
    if (selfCharaEquipSetNo >= this.localEquipSets.Length)
      return;
    GameSection.SetEventData((object) new StatusEquip.LocalEquipSetData(selfCharaEquipSetNo, 0, this.localEquipSets[selfCharaEquipSetNo]));
  }

  protected new enum UI
  {
    LBL_NAME,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    SPR_COMMENT,
    LBL_COMMENT,
    OBJ_LAST_LOGIN,
    LBL_LAST_LOGIN,
    LBL_LAST_LOGIN_TIME,
    LBL_LEVEL,
    OBJ_LEVEL_ROOT,
    LBL_USER_ID,
    OBJ_USER_ID_ROOT,
    TEX_MODEL,
    BTN_FOLLOW,
    BTN_UNFOLLOW,
    OBJ_BLACKLIST_ROOT,
    BTN_BLACKLIST_IN,
    BTN_BLACKLIST_OUT,
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
    OBJ_EQUIP_ROOT,
    OBJ_EQUIP_SET_ROOT,
    OBJ_FRIEND_INFO_ROOT,
    OBJ_CHANGE_EQUIP_INFO_ROOT,
    LBL_MAX,
    LBL_NOW,
    OBJ_FOLLOW_ARROW_ROOT,
    SPR_FOLLOW_ARROW,
    SPR_FOLLOWER_ARROW,
    SPR_BLACKLIST_ICON,
    LBL_LEVEL_WEAPON_1,
    LBL_LEVEL_WEAPON_2,
    LBL_LEVEL_WEAPON_3,
    LBL_LEVEL_ARMOR,
    LBL_LEVEL_HELM,
    LBL_LEVEL_ARM,
    LBL_LEVEL_LEG,
    LBL_CHANGE_MODE,
    LBL_SET_NAME,
    OBJ_DEGREE_PLATE_ROOT,
    OBJ_SKILL_BUTTON_ROOT,
    OBJ_EQUIP_ROT_ROOT,
    BTN_EQUIP_SET_COPY,
    BTN_EQUIP_SET_PASTE,
    BTN_EQUIP_SET_DELETE,
  }

  private enum EQUIP_SET_COPY_MODE
  {
    NONE,
    COPY,
  }
}
