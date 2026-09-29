// Decompiled with JetBrains decompiler
// Type: QuestAcceptSeriesArenaRoomChangeEquipSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestAcceptSeriesArenaRoomChangeEquipSet : QuestOffLineChangeEquipSet
{
  protected int order;

  public override void Initialize()
  {
    this.order = (int) GameSection.GetEventData();
    base.Initialize();
    this.SetActive(this.transRoot, (Enum) QuestAcceptSeriesArenaRoomChangeEquipSet.UI.OBJ_BACK, true);
  }

  protected override void GetUserRecordStatus()
  {
    this.selfCharaEquipSetNo = MonoBehaviourSingleton<StatusManager>.I.selectUniqueEquipSetNo;
    this.record = this.InitializePlayerRecord();
  }

  private void OnEnable()
  {
    InputManager.OnDragAlways += new InputManager.OnTouchDelegate(this.OnDrag);
  }

  private void OnDisable()
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
    return MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == nameof (QuestAcceptSeriesArenaRoomChangeEquipSet);
  }

  protected override void UpdateCopyModeButton()
  {
  }

  private bool IsReadyCheck()
  {
    return this.localEquipSet.item[0] != null && this.localEquipSet.item[3] != null && this.localEquipSet.item[0].uniqueID != 0UL && this.localEquipSet.item[3].uniqueID != 0UL;
  }

  protected new InGameRecorder.PlayerRecord InitializePlayerRecord()
  {
    Network.UserInfo userInfo = MonoBehaviourSingleton<UserInfoManager>.I.userInfo;
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    InGameRecorder.PlayerRecord record = new InGameRecorder.PlayerRecord();
    record.id = 0;
    record.isNPC = false;
    record.isSelf = true;
    MonoBehaviourSingleton<StatusManager>.I.CreateLocalUniqueEquipSetData();
    record.animID = 90;
    record.playerLoadInfo = new PlayerLoadInfo();
    this.localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquipSet(this.selfCharaEquipSetNo);
    this.PlayerLoad(record);
    record.charaInfo = new CharaInfo();
    record.charaInfo.userId = userInfo.id;
    record.charaInfo.name = userInfo.name;
    record.charaInfo.comment = userInfo.comment;
    record.charaInfo.code = userInfo.code;
    record.charaInfo.level = userStatus.level;
    record.charaInfo.atk = userStatus.atk;
    record.charaInfo.def = userStatus.def;
    record.charaInfo.hp = userStatus.hp;
    record.charaInfo.faceId = userStatus.faceId;
    record.charaInfo.sex = userStatus.sex;
    record.charaInfo.equipSet = (List<CharaInfo.EquipItem>) null;
    this.equipSetMax = MonoBehaviourSingleton<StatusManager>.I.UniqueEquipSetNum();
    return record;
  }

  protected override void PlayerLoad(InGameRecorder.PlayerRecord record)
  {
    PlayerLoadInfo playerLoadInfo = record.playerLoadInfo;
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    playerLoadInfo.SetupLoadInfo(this.localEquipSet, 0UL, 0UL, 0UL, 0UL, 0UL, this.localEquipSet.showHelm == 1);
    if (playerLoadInfo.bodyModelID <= 0)
      playerLoadInfo.SetEquipBody(userStatus.sex, (uint) MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerBodyEquipItemID);
    if (this.localEquipSet.item[0] == null)
      record.animID = 98;
    else
      record.animID = 90;
  }

  protected override PlayerLoadInfo GetFromUserStatus()
  {
    return PlayerLoadInfo.FromUserUniqueStatus(true, this.isVisualMode, this.selfCharaEquipSetNo);
  }

  protected override void OnQuery_SECTION_BACK()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.CheckChangeUniqueEquipSet((Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  private void OnQuery_MAGI_REMOVE()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) (this.selfCharaEquipSetNo + 1)
    });
  }

  private void OnQuery_QuestAcceptStatusMagiAllRemoveConfirm_YES()
  {
    this.StartCoroutine(this.sendMagiAllRemove());
  }

  protected override void OnQuery_DECISION()
  {
    if (!this.IsReadyCheck())
    {
      GameSection.ChangeEvent("NOT_EQUIP");
    }
    else
    {
      GameSection.ChangeEvent("[BACK]");
      GameSection.StayEvent();
      MonoBehaviourSingleton<StatusManager>.I.CheckChangeUniqueEquipSet((Action<bool>) (is_success =>
      {
        if (is_success)
          this.ChangeOrderNo();
        else
          GameSection.ResumeEvent(false);
      }));
    }
  }

  protected override string GetEquipSetBasePrefabName() => "QuestSeriesArenaChangeEquipSetBase";

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE) != (GameSection.NOTIFY_FLAG) 0)
      MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipSets(MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet());
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET_INFO) != (GameSection.NOTIFY_FLAG) 0)
      this.ReloadPlayerModelByLocalEquipSet();
    if ((this.GetUpdateUINotifyFlags() & flags) == (GameSection.NOTIFY_FLAG) 0)
      return;
    this.RefreshUI();
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE | GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM;
  }

  protected override void ReplaceEquipItem(EquipSetInfo equipSetInfo, int setNo, int index)
  {
    MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipItem(equipSetInfo, setNo, index);
  }

  public override void localEquipCalcUpdate(EquipSetInfo[] equipSets)
  {
    if (!MonoBehaviourSingleton<StatusManager>.I.isEquipSetCalcUpdate)
      return;
    MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipSets(equipSets);
  }

  protected IEnumerator sendMagiAllRemove()
  {
    bool wait = true;
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.RemoveOrderNo(MonoBehaviourSingleton<StatusManager>.I.GetCurrentUniqueEquipSetNo(), (Action<bool>) (is_succses => wait = false));
    while (wait)
      yield return (object) null;
    wait = true;
    MonoBehaviourSingleton<StatusManager>.I.CheckChangeUniqueEquipSet((Action<bool>) (is_success => wait = false));
    while (wait)
      yield return (object) null;
    wait = true;
    MonoBehaviourSingleton<StatusManager>.I.SendDetachAllSkillFromEvery(this.selfCharaEquipSetNo, (Action<bool>) (issuccess => wait = false));
    while (wait)
      yield return (object) null;
    GameSection.ResumeEvent(true);
  }

  protected override bool IsNullWeaponSloat(int id) => false;

  protected void ChangeOrderNo()
  {
    MonoBehaviourSingleton<StatusManager>.I.ChangeOrderNo(this.order, this.selfCharaEquipSetNo, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  protected override void ViewDetailUI()
  {
    SimpleStatus finalStatus = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquipSetCalculator(this.selfCharaEquipSetNo).GetFinalStatus(0, MonoBehaviourSingleton<UserInfoManager>.I.userStatus);
    int attacksSum = finalStatus.GetAttacksSum();
    int defencesSum = finalStatus.GetDefencesSum();
    int hp = finalStatus.hp;
    int level = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
    this.SetLabelText(this.transRoot, (Enum) QuestAcceptSeriesArenaRoomChangeEquipSet.UI.LBL_ATK, attacksSum.ToString());
    this.SetLabelText(this.transRoot, (Enum) QuestAcceptSeriesArenaRoomChangeEquipSet.UI.LBL_DEF, defencesSum.ToString());
    this.SetLabelText(this.transRoot, (Enum) QuestAcceptSeriesArenaRoomChangeEquipSet.UI.LBL_HP, hp.ToString());
    this.SetLabelText(this.transRoot, (Enum) QuestAcceptSeriesArenaRoomChangeEquipSet.UI.LBL_LEVEL, level.ToString());
    this.SetupInfo();
    this.UpdateEquipIcon((List<CharaInfo.EquipItem>) null);
    this.CreateDegree();
    this.SetMoveMessageButton();
    if (this.record != null && this.record.charaInfo != null && this.record.charaInfo.userClanData != null)
      this.UpdateClanInfo(this.record.charaInfo);
    else
      this.DisableClanInfo();
    this.SetButtonEnabled((Enum) QuestAcceptSeriesArenaRoomChangeEquipSet.UI.BTN_MAGI_REMOVE, MonoBehaviourSingleton<StatusManager>.I.checkEquipMagi(this.selfCharaEquipSetNo));
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
    BTN_AUTO_EQUIP,
    OBJ_BACK,
    BTN_MAGI_REMOVE,
  }
}
