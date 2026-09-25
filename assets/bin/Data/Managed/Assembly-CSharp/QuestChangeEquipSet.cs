// Decompiled with JetBrains decompiler
// Type: QuestChangeEquipSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestChangeEquipSet : QuestRoomUserInfoDetail
{
  protected int equipSetMax;

  protected override bool IsFriendInfo => false;

  public override void Initialize()
  {
    this.isChangeEquip = true;
    this.isVisualMode = false;
    InGameRecorder.PlayerRecord playerRecord = this.InitializePlayerRecord();
    object[] eventData = GameSection.GetEventData() as object[];
    GameSection.SetEventData((object) new object[3]
    {
      (object) playerRecord,
      eventData[0],
      eventData[1]
    });
    base.Initialize();
  }

  protected InGameRecorder.PlayerRecord InitializePlayerRecord()
  {
    Network.UserInfo userInfo = MonoBehaviourSingleton<UserInfoManager>.I.userInfo;
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    InGameRecorder.PlayerRecord playerRecord = new InGameRecorder.PlayerRecord();
    playerRecord.id = 0;
    playerRecord.isNPC = false;
    playerRecord.isSelf = true;
    MonoBehaviourSingleton<StatusManager>.I.CreateLocalEquipSetData();
    playerRecord.playerLoadInfo = this.CreatePlayerLoadInfo(userStatus.eSetNo);
    playerRecord.animID = 90;
    playerRecord.charaInfo = new CharaInfo();
    playerRecord.charaInfo.userId = userInfo.id;
    playerRecord.charaInfo.name = userInfo.name;
    playerRecord.charaInfo.comment = userInfo.comment;
    playerRecord.charaInfo.code = userInfo.code;
    playerRecord.charaInfo.level = userStatus.level;
    playerRecord.charaInfo.atk = userStatus.atk;
    playerRecord.charaInfo.def = userStatus.def;
    playerRecord.charaInfo.hp = userStatus.hp;
    playerRecord.charaInfo.faceId = userStatus.faceId;
    playerRecord.charaInfo.sex = userStatus.sex;
    playerRecord.charaInfo.equipSet = (List<CharaInfo.EquipItem>) null;
    MonoBehaviourSingleton<StatusManager>.I.CreateLocalVisualEquipData();
    StatusManager.LocalVisual localVisualEquip = MonoBehaviourSingleton<StatusManager>.I.GetLocalVisualEquip();
    playerRecord.charaInfo.aId = localVisualEquip.visualItem[0] != null ? (int) localVisualEquip.visualItem[0].tableID : 0;
    playerRecord.charaInfo.hId = localVisualEquip.visualItem[1] != null ? (int) localVisualEquip.visualItem[1].tableID : 0;
    playerRecord.charaInfo.rId = localVisualEquip.visualItem[2] != null ? (int) localVisualEquip.visualItem[2].tableID : 0;
    playerRecord.charaInfo.lId = localVisualEquip.visualItem[3] != null ? (int) localVisualEquip.visualItem[3].tableID : 0;
    playerRecord.charaInfo.showHelm = localVisualEquip.isVisibleHelm ? 1 : 0;
    this.equipSetMax = MonoBehaviourSingleton<StatusManager>.I.EquipSetNum();
    return playerRecord;
  }

  public override void UpdateUI()
  {
    this.UpdateEquipSetUI();
    base.UpdateUI();
  }

  protected void UpdateEquipSetUI()
  {
    MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(this.selfCharaEquipSetNo);
    this.SetLabelText(this.transRoot, (Enum) QuestChangeEquipSet.UI.LBL_NOW, (this.selfCharaEquipSetNo + 1).ToString());
    this.SetLabelText(this.transRoot, (Enum) QuestChangeEquipSet.UI.LBL_MAX, this.equipSetMax.ToString());
    this.SetLabelText(this.transRoot, (Enum) QuestChangeEquipSet.UI.LBL_SET_NAME, this.localEquipSet.name);
  }

  protected override void OnClose()
  {
  }

  protected override void UpdateUserIDLabel()
  {
  }

  public override void SetupCommentText()
  {
  }

  public override void SetupFollowButton()
  {
  }

  protected override void CreateDegree()
  {
  }

  protected override void UpdateEquipIcon(List<CharaInfo.EquipItem> equip_set_info)
  {
    this.SetActive(this.transRoot, (Enum) QuestChangeEquipSet.UI.LBL_CHANGE_MODE, this.isVisualMode);
    int index1 = 0;
    for (int index2 = 7; index1 < index2; ++index1)
    {
      this.SetEvent(this.FindCtrl(this.transRoot, (Enum) this.icons[index1]), "EMPTY", 0);
      this.SetEvent(this.FindCtrl(this.transRoot, (Enum) this.icons_btn[index1]), "EMPTY", 0);
      this.SetLabelText(this.FindCtrl(this.transRoot, (Enum) this.icons_level[index1]), string.Empty);
    }
    bool flag1 = this.isVisualMode;
    bool flag2 = this.isVisualMode;
    bool flag3 = this.isVisualMode;
    bool flag4 = this.isVisualMode;
    if (this.localEquipSet != null)
    {
      int index3 = 0;
      for (int length = this.localEquipSet.item.Length; index3 < length; ++index3)
      {
        int num = -1;
        EquipItemInfo equipItemInfo = this.localEquipSet.item[index3];
        EquipItemTable.EquipItemData equipItemData = (EquipItemTable.EquipItemData) null;
        if (equipItemInfo != null)
        {
          switch (equipItemInfo.tableData.type)
          {
            case EQUIPMENT_TYPE.ARMOR:
              flag2 = false;
              break;
            case EQUIPMENT_TYPE.HELM:
              flag1 = false;
              break;
            case EQUIPMENT_TYPE.ARM:
              flag3 = false;
              break;
            case EQUIPMENT_TYPE.LEG:
              flag4 = false;
              break;
          }
          equipItemData = !this.isVisualMode ? Singleton<EquipItemTable>.I.GetEquipItemData(equipItemInfo.tableID) : this.GetVisualModeTargetTable(equipItemInfo.tableData.id, equipItemInfo.tableData.type, this.record.charaInfo);
        }
        if (this.isVisualMode)
        {
          if (equipItemData != null)
          {
            int itemIconType = (int) ItemIcon.GetItemIconType(equipItemData.type);
            int rarity = (int) equipItemData.rarity;
            int elementPriorityToTable = (int) equipItemData.GetTargetElementPriorityToTable();
            num = equipItemData.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
            this.SetActive(this.FindCtrl(this.transRoot, (Enum) this.icons_level[index3]), false);
          }
        }
        else if (equipItemInfo != null && equipItemInfo.tableID != 0U)
        {
          num = equipItemData.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
          this.SetActive(this.FindCtrl(this.transRoot, (Enum) this.icons_level[index3]), true);
          string text = string.Format(StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 1U), (object) equipItemInfo.level.ToString());
          this.SetLabelText(this.FindCtrl(this.transRoot, (Enum) this.icons_level[index3]), text);
        }
        Transform ctrl = this.FindCtrl(this.transRoot, (Enum) this.icons[index3]);
        ((Component) ctrl).GetComponentsInChildren<ItemIcon>(true, Temporary.itemIconList);
        int index4 = 0;
        for (int count = Temporary.itemIconList.Count; index4 < count; ++index4)
          ((Component) Temporary.itemIconList[index4]).gameObject.SetActive(true);
        Temporary.itemIconList.Clear();
        ItemIcon iconByEquipItemInfo = ItemIcon.CreateEquipItemIconByEquipItemInfo(equipItemInfo, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex, ctrl, event_name: "EQUIP", event_data: index3);
        this.SetLongTouch(iconByEquipItemInfo.transform, "DETAIL", (object) index3);
        this.SetEvent(this.FindCtrl(this.transRoot, (Enum) this.icons_btn[index3]), "DETAIL", index3);
        ((Component) iconByEquipItemInfo).gameObject.SetActive(num != -1);
        if (num != -1)
          iconByEquipItemInfo.SetEquipExtInvertedColor(equipItemInfo, this.GetComponent<UILabel>((Enum) this.icons_level[index3]));
        this.UpdateEquipSkillButton(equipItemInfo, index3);
      }
      this.ResetTween(this.transRoot, (Enum) QuestChangeEquipSet.UI.OBJ_EQUIP_ROOT);
      this.PlayTween(this.transRoot, (Enum) QuestChangeEquipSet.UI.OBJ_EQUIP_ROOT, is_input_block: false);
    }
    if (flag1 && this.record.charaInfo.hId != 0)
      this.SetVisualModeIcon(4, this.record.charaInfo.hId, EQUIPMENT_TYPE.HELM, this.record.charaInfo);
    if (flag2 && this.record.charaInfo.aId != 0)
      this.SetVisualModeIcon(3, this.record.charaInfo.aId, EQUIPMENT_TYPE.ARMOR, this.record.charaInfo);
    if (flag3 && this.record.charaInfo.rId != 0)
      this.SetVisualModeIcon(5, this.record.charaInfo.rId, EQUIPMENT_TYPE.ARM, this.record.charaInfo);
    if (!flag4 || this.record.charaInfo.lId == 0)
      return;
    this.SetVisualModeIcon(6, this.record.charaInfo.lId, EQUIPMENT_TYPE.LEG, this.record.charaInfo);
  }

  protected virtual void UpdateEquipSkillButton(EquipItemInfo item, int i)
  {
  }

  protected virtual void OnQuery_DECISION()
  {
    GameSection.ChangeEvent("[BACK]");
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.CheckChangeEquipSet(this.selfCharaEquipSetNo, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  protected override void OnQuery_SECTION_BACK()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(-1);
    base.OnQuery_SECTION_BACK();
    MonoBehaviourSingleton<PartyManager>.I.SendIsEquip(false, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  protected void OnQuery_EQUIP_SET_L()
  {
    --this.selfCharaEquipSetNo;
    if (this.selfCharaEquipSetNo < 0)
      this.selfCharaEquipSetNo = this.equipSetMax - 1;
    this.RefreshUI();
    this.StartCoroutine(this.ReloadModelCoroutine());
  }

  protected void OnQuery_EQUIP_SET_R()
  {
    ++this.selfCharaEquipSetNo;
    if (this.selfCharaEquipSetNo >= this.equipSetMax)
      this.selfCharaEquipSetNo = 0;
    this.RefreshUI();
    this.StartCoroutine(this.ReloadModelCoroutine());
  }

  protected override void OnQuery_CHANGE_MODE()
  {
    this.RefreshUI();
    this.ReloadModel();
  }

  protected IEnumerator ReloadModelCoroutine()
  {
    while (UIModelRenderTexture.Get(this.FindCtrl(this.transRoot, (Enum) QuestChangeEquipSet.UI.TEX_MODEL)).IsLoadingPlayer())
      yield return (object) null;
    this.ReloadModel();
  }

  protected virtual void ReloadModel()
  {
    this.reloadModel = true;
    this.record.playerLoadInfo = this.CreatePlayerLoadInfo(this.selfCharaEquipSetNo);
    this.record.charaInfo.showHelm = MonoBehaviourSingleton<StatusManager>.I.GetEquippingShowHelm(this.selfCharaEquipSetNo);
    this.SetLabelText(this.transRoot, (Enum) QuestChangeEquipSet.UI.LBL_SET_NAME, this.localEquipSet.name);
    this.LoadModel();
    this.reloadModel = false;
  }

  private PlayerLoadInfo CreatePlayerLoadInfo(int set_no)
  {
    PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    this.localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetEquipSet(set_no);
    StatusManager.LocalVisual localVisual = new StatusManager.LocalVisual();
    localVisual.visualItem[0] = this.isVisualMode ? MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(ulong.Parse(userStatus.armorUniqId)) : (EquipItemInfo) null;
    localVisual.visualItem[1] = this.isVisualMode ? MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(ulong.Parse(userStatus.helmUniqId)) : (EquipItemInfo) null;
    localVisual.visualItem[2] = this.isVisualMode ? MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(ulong.Parse(userStatus.armUniqId)) : (EquipItemInfo) null;
    localVisual.visualItem[3] = this.isVisualMode ? MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(ulong.Parse(userStatus.legUniqId)) : (EquipItemInfo) null;
    localVisual.isVisibleHelm = MonoBehaviourSingleton<StatusManager>.I.GetEquippingShowHelm(set_no) > 0;
    playerLoadInfo.SetupLoadInfo(this.localEquipSet, 0UL, localVisual.VisialID(0), localVisual.VisialID(1), localVisual.VisialID(2), localVisual.VisialID(3), localVisual.isVisibleHelm);
    return playerLoadInfo;
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
  }
}
