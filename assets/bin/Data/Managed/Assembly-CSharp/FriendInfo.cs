// Decompiled with JetBrains decompiler
// Type: FriendInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FriendInfo : SkillInfoBase
{
  protected FriendInfo.UI[] icons = new FriendInfo.UI[7]
  {
    FriendInfo.UI.OBJ_ICON_WEAPON_1,
    FriendInfo.UI.OBJ_ICON_WEAPON_2,
    FriendInfo.UI.OBJ_ICON_WEAPON_3,
    FriendInfo.UI.OBJ_ICON_ARMOR,
    FriendInfo.UI.OBJ_ICON_HELM,
    FriendInfo.UI.OBJ_ICON_ARM,
    FriendInfo.UI.OBJ_ICON_LEG
  };
  protected FriendInfo.UI[] icons_btn = new FriendInfo.UI[7]
  {
    FriendInfo.UI.BTN_ICON_WEAPON_1,
    FriendInfo.UI.BTN_ICON_WEAPON_2,
    FriendInfo.UI.BTN_ICON_WEAPON_3,
    FriendInfo.UI.BTN_ICON_ARMOR,
    FriendInfo.UI.BTN_ICON_HELM,
    FriendInfo.UI.BTN_ICON_ARM,
    FriendInfo.UI.BTN_ICON_LEG
  };
  protected FriendInfo.UI[] icons_level = new FriendInfo.UI[7]
  {
    FriendInfo.UI.LBL_LEVEL_WEAPON_1,
    FriendInfo.UI.LBL_LEVEL_WEAPON_2,
    FriendInfo.UI.LBL_LEVEL_WEAPON_3,
    FriendInfo.UI.LBL_LEVEL_ARMOR,
    FriendInfo.UI.LBL_LEVEL_HELM,
    FriendInfo.UI.LBL_LEVEL_ARM,
    FriendInfo.UI.LBL_LEVEL_LEG
  };
  protected CharaInfo data;
  protected FriendCharaInfo friendCharaInfo;
  protected FriendMessageUserListModel.MessageUserInfo m_msgUserInfo;
  protected CharaInfo clanCharaInfo;
  protected ClanData userClanData;
  protected PlayerLoader loader;
  protected Transform transRoot;
  protected string nowSectionName = string.Empty;
  protected DegreePlate degree;
  protected bool isVisualMode;
  protected const string STR_VISUAL_EQUIP_EVENT_NAME = "VISUAL_DETAIL";
  protected bool isFollowerList;
  protected bool isFollowerListChengeTrans;
  protected bool dataFollower;
  protected bool dataFollowing;
  protected bool m_isInitMoveMessageButton;
  private SymbolMarkCtrl symbolMark;

  protected virtual string GetCreatePrefabName() => "FriendInfoBase";

  protected virtual bool IsFriendInfo => true;

  protected virtual List<int> SelectedDegrees
  {
    get
    {
      return this.IsFriendInfo ? this.data.selectedDegrees : MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds;
    }
  }

  protected virtual bool showMagiButton => false;

  protected bool IsInitMoveMessageButton => this.m_isInitMoveMessageButton;

  public override void Initialize() => this.InitializeBase();

  protected void InitializeBase() => this.StartCoroutine(this.DoInitialize());

  protected IEnumerator DoInitialize()
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
    {
      bool isWait = true;
      MonoBehaviourSingleton<ClanMatchingManager>.I.RequestDetail("0", (Action<ClanDetailModel.Param>) (result => isWait = false));
      while (isWait)
        yield return (object) null;
      this.userClanData = MonoBehaviourSingleton<ClanMatchingManager>.I.clanData;
    }
    base.Initialize();
  }

  protected override void OnOpen()
  {
    this.ReOpen();
    base.OnOpen();
  }

  protected void ReOpen()
  {
    if (!(GameSection.GetEventData() is CharaInfo))
      return;
    this.friendCharaInfo = GameSection.GetEventData() as FriendCharaInfo;
    this.data = GameSection.GetEventData() as CharaInfo;
    this.m_msgUserInfo = GameSection.GetEventData() as FriendMessageUserListModel.MessageUserInfo;
    if (this.friendCharaInfo != null)
    {
      this.dataFollower = this.friendCharaInfo.follower;
      this.dataFollowing = this.friendCharaInfo.following;
    }
    this.nowSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
    this.isFollowerList = Object.op_Implicit(Object.FindObjectOfType(typeof (FriendFollowerList)));
    this.DisableClanInfo();
  }

  protected void OnEnable()
  {
    InputManager.OnDragAlways += new InputManager.OnTouchDelegate(this.OnDrag);
  }

  protected void OnDisable()
  {
    InputManager.OnDragAlways -= new InputManager.OnTouchDelegate(this.OnDrag);
    this.nowSectionName = string.Empty;
  }

  private void OnDrag(InputManager.TouchInfo touch_info)
  {
    if (Object.op_Equality((Object) this.loader, (Object) null) || MonoBehaviourSingleton<UIManager>.I.IsDisable())
      return;
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == this.nowSectionName)
      ((Component) this.loader).transform.Rotate(GameDefine.GetCharaRotateVector(touch_info));
    else if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "QuestResultFriendDetail")
    {
      ((Component) this.loader).transform.Rotate(GameDefine.GetCharaRotateVector(touch_info));
    }
    else
    {
      if (!(this.nowSectionName == "ClanDetail"))
        return;
      ((Component) this.loader).transform.Rotate(GameDefine.GetCharaRotateVector(touch_info));
    }
  }

  public override void UpdateUI()
  {
    this.transRoot = this.SetPrefab((Enum) FriendInfo.UI.OBJ_EQUIP_SET_ROOT, this.GetCreatePrefabName());
    this.UpdateUserIDLabel();
    this.UpdateHeader();
    this.LoadModel();
    this.UpdateEquipIcon(this.data.equipSet);
    this.UpdateBottomButton();
    this.CreateDegree();
    if (this.data != null && this.data.userClanData != null)
      this.UpdateClanInfo(this.data);
    else
      this.DisableClanInfo();
  }

  protected virtual void UpdateUserIDLabel()
  {
    this.SetLabelText(this.transRoot, (Enum) FriendInfo.UI.LBL_USER_ID, this.data.code);
  }

  protected void UpdateHeader()
  {
    EquipSetCalculator equipSetCalculator;
    if (MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex == -1)
    {
      MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = 0;
      equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(0);
      equipSetCalculator.SetEquipSet(this.data.equipSet);
    }
    else
      equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex);
    SimpleStatus finalStatus = equipSetCalculator.GetFinalStatus(0, (int) this.data.hp, (int) this.data.atk, (int) this.data.def);
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.OBJ_LAST_LOGIN, true);
    this.SetLabelText(this.transRoot, (Enum) FriendInfo.UI.LBL_NAME, this.data.name);
    this.SetLabelText(this.transRoot, (Enum) FriendInfo.UI.LBL_COMMENT, this.data.comment);
    this.SetLabelText(this.transRoot, (Enum) FriendInfo.UI.LBL_LAST_LOGIN, this.sectionData.GetText("LAST_LOGIN"));
    this.SetLabelText(this.transRoot, (Enum) FriendInfo.UI.LBL_LAST_LOGIN_TIME, this.data.lastLogin);
    this.SetLabelText(this.transRoot, (Enum) FriendInfo.UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
    this.SetLabelText(this.transRoot, (Enum) FriendInfo.UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
    this.SetLabelText(this.transRoot, (Enum) FriendInfo.UI.LBL_HP, finalStatus.hp.ToString());
    this.SetLabelText(this.transRoot, (Enum) FriendInfo.UI.LBL_LEVEL, this.data.level.ToString());
    bool black_list_user = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(this.data.userId);
    bool same_clan_user = false;
    if (this.data.userClanData != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
      same_clan_user = this.data.userClanData.cId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId;
    this.SetFollowStatus(this.dataFollowing, this.dataFollower, black_list_user, same_clan_user);
  }

  protected void SetFollowStatus(
    bool following,
    bool follower,
    bool black_list_user,
    bool same_clan_user)
  {
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.SPR_FOLLOW_ARROW, !black_list_user & following);
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.SPR_FOLLOWER_ARROW, !black_list_user & follower);
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.SPR_BLACKLIST_ICON, black_list_user);
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.SPR_SAME_CLAN_ICON, same_clan_user);
  }

  protected virtual void LoadModel()
  {
    this.SetRenderPlayerModel(this.transRoot, (Enum) FriendInfo.UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(this.data, true, true, true, this.isVisualMode), PLAYER_ANIM_TYPE.GetStatus(this.data.sex), new Vector3(0.0f, -0.75f, 14f), new Vector3(0.0f, 180f, 0.0f), this.isVisualMode, (Action<PlayerLoader>) (player_loader =>
    {
      if (!Object.op_Inequality((Object) player_loader, (Object) null))
        return;
      this.loader = player_loader;
    }));
  }

  protected virtual void CreateDegree()
  {
    ((Component) this.GetCtrl((Enum) FriendInfo.UI.OBJ_DEGREE_PLATE_ROOT)).GetComponent<DegreePlate>().Initialize(this.SelectedDegrees, false, (Action<DegreePlate>) (x => { }));
  }

  protected virtual void UpdateEquipIcon(List<CharaInfo.EquipItem> equip_set_info)
  {
    int weapon_cnt = 0;
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.LBL_CHANGE_MODE, this.isVisualMode);
    int index1 = 0;
    for (int index2 = 7; index1 < index2; ++index1)
    {
      this.SetEvent(this.FindCtrl(this.transRoot, (Enum) this.icons[index1]), "EMPTY", 0);
      this.SetEvent(this.FindCtrl(this.transRoot, (Enum) this.icons_btn[index1]), "EMPTY", 0);
      this.SetLabelText(this.FindCtrl(this.transRoot, (Enum) this.icons_level[index1]), string.Empty);
      this.SetActive(this.FindCtrl(this.transRoot, (Enum) this.icons[index1]), false);
    }
    bool need_visual_helm_icon = this.isVisualMode;
    bool need_visual_armor_icon = this.isVisualMode;
    bool need_visual_arm_icon = this.isVisualMode;
    bool need_visual_leg_icon = this.isVisualMode;
    equip_set_info.ForEach((Action<CharaInfo.EquipItem>) (equip_data =>
    {
      EquipItemTable.EquipItemData equipItemTableData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) equip_data.eId);
      if (equipItemTableData == null)
        return;
      if (this.isVisualMode && !equipItemTableData.IsWeapon())
      {
        equipItemTableData = this.GetVisualModeTargetTable(equipItemTableData.id, equipItemTableData.type, this.data);
        if (equipItemTableData == null)
          return;
      }
      Transform parent = (Transform) null;
      int event_data = -1;
      if (equipItemTableData.IsWeapon())
      {
        parent = this.FindCtrl(this.transRoot, (Enum) this.icons[weapon_cnt]);
        event_data = weapon_cnt;
        ++weapon_cnt;
      }
      else
      {
        switch (equipItemTableData.type)
        {
          case EQUIPMENT_TYPE.ARMOR:
          case EQUIPMENT_TYPE.VISUAL_ARMOR:
            event_data = 3;
            need_visual_armor_icon = false;
            break;
          case EQUIPMENT_TYPE.HELM:
          case EQUIPMENT_TYPE.VISUAL_HELM:
            event_data = 4;
            need_visual_helm_icon = false;
            break;
          case EQUIPMENT_TYPE.ARM:
          case EQUIPMENT_TYPE.VISUAL_ARM:
            event_data = 5;
            need_visual_arm_icon = false;
            break;
          case EQUIPMENT_TYPE.LEG:
          case EQUIPMENT_TYPE.VISUAL_LEG:
            event_data = 6;
            need_visual_leg_icon = false;
            break;
        }
        if (event_data != -1)
          parent = this.FindCtrl(this.transRoot, (Enum) this.icons[event_data]);
      }
      if (Object.op_Equality((Object) parent, (Object) null))
        return;
      this.SetActive(this.FindCtrl(this.transRoot, (Enum) this.icons[event_data]), true);
      string event_name = this.isVisualMode ? "VISUAL_DETAIL" : "DETAIL";
      ItemIcon byEquipItemTable = ItemIcon.CreateEquipItemIconByEquipItemTable(equipItemTableData, this.GetCharaSex(), parent, event_name: event_name, event_data: event_data);
      this.SetLongTouch(byEquipItemTable.transform, event_name, (object) event_data);
      this.SetEvent(this.FindCtrl(this.transRoot, (Enum) this.icons_btn[event_data]), event_name, event_data);
      this.SetLongTouch(this.FindCtrl(this.transRoot, (Enum) this.icons_btn[event_data]), event_name, (object) event_data);
      EquipItemInfo info = new EquipItemInfo(equip_data);
      byEquipItemTable.SetEquipExtInvertedColor(info, this.GetComponent<UILabel>((Enum) this.icons_level[event_data]));
      this.SetActive(this.FindCtrl(this.transRoot, (Enum) this.icons_level[event_data]), !this.isVisualMode);
      if (equip_data == null)
        return;
      string text = string.Format(StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 1U), (object) equip_data.lv.ToString());
      this.SetLabelText(this.FindCtrl(this.transRoot, (Enum) this.icons_level[event_data]), text);
    }));
    if (need_visual_helm_icon && this.data.hId != 0)
      this.SetVisualModeIcon(4, this.data.hId, EQUIPMENT_TYPE.HELM, this.data);
    if (need_visual_armor_icon && this.data.aId != 0)
      this.SetVisualModeIcon(3, this.data.aId, EQUIPMENT_TYPE.ARMOR, this.data);
    if (need_visual_arm_icon && this.data.rId != 0)
      this.SetVisualModeIcon(5, this.data.rId, EQUIPMENT_TYPE.ARM, this.data);
    if (!need_visual_leg_icon || this.data.lId == 0)
      return;
    this.SetVisualModeIcon(6, this.data.lId, EQUIPMENT_TYPE.LEG, this.data);
  }

  protected void SetVisualModeIcon(
    int index,
    int table_id,
    EQUIPMENT_TYPE e_type,
    CharaInfo chara_info)
  {
    string event_name = "VISUAL_DETAIL";
    Transform ctrl = this.FindCtrl(this.transRoot, (Enum) this.icons[index]);
    EquipItemTable.EquipItemData visualModeTargetTable = this.GetVisualModeTargetTable((uint) table_id, e_type, chara_info);
    if (visualModeTargetTable == null)
      return;
    ((Component) ctrl).GetComponentsInChildren<ItemIcon>(true, Temporary.itemIconList);
    int index1 = 0;
    for (int count = Temporary.itemIconList.Count; index1 < count; ++index1)
      ((Component) Temporary.itemIconList[index1]).gameObject.SetActive(true);
    Temporary.itemIconList.Clear();
    this.SetActive(this.FindCtrl(this.transRoot, (Enum) this.icons[index]), true);
    this.SetLongTouch(ItemIcon.CreateEquipItemIconByEquipItemTable(visualModeTargetTable, this.GetCharaSex(), ctrl, event_name: event_name, event_data: index).transform, event_name, (object) index);
    this.SetEvent(this.FindCtrl(this.transRoot, (Enum) this.icons_btn[index]), event_name, index);
    this.SetLongTouch(this.FindCtrl(this.transRoot, (Enum) this.icons_btn[index]), event_name, (object) index);
    this.SetActive(this.FindCtrl(this.transRoot, (Enum) this.icons_level[index]), !this.isVisualMode);
  }

  protected void UpdateBottomButton()
  {
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.OBJ_FRIEND_INFO_ROOT, this.IsFriendInfo);
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.OBJ_CHANGE_EQUIP_INFO_ROOT, !this.IsFriendInfo);
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_MAGI, this.showMagiButton);
    bool flag = MonoBehaviourSingleton<FriendManager>.I.followNum == MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxFollow;
    this.SetEvent(this.transRoot, (Enum) FriendInfo.UI.BTN_FOLLOW, "FOLLOW", 0);
    if (!this.isFollowerList)
    {
      if (flag && !this.dataFollowing)
      {
        this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_FOLLOW, true);
        this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_UNFOLLOW, false);
        this.SetEvent(this.transRoot, (Enum) FriendInfo.UI.BTN_FOLLOW, "INVALID_FOLLOW", 0);
      }
      else
      {
        this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_FOLLOW, !this.dataFollowing);
        this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_UNFOLLOW, this.dataFollowing);
      }
      this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_DELETEFOLLOWER, false);
    }
    else
    {
      this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_DELETEFOLLOWER, this.dataFollower);
      if (!flag && !this.dataFollowing)
      {
        this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_FOLLOW, true);
        if (!this.isFollowerListChengeTrans)
        {
          Transform ctrl = this.FindCtrl(this.transRoot, (Enum) FriendInfo.UI.BTN_FOLLOW);
          ctrl.localPosition = new Vector3(ctrl.localPosition.x - 167f, ctrl.localPosition.y - 502f, ctrl.localPosition.z);
          ctrl.localScale = new Vector3(1f, 1f, 1f);
          this.isFollowerListChengeTrans = true;
        }
      }
      else
        this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_FOLLOW, false);
      this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_UNFOLLOW, false);
    }
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.OBJ_BLACKLIST_ROOT, true);
    bool is_visible = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(this.data.userId);
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_BLACKLIST_IN, !is_visible);
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_BLACKLIST_OUT, is_visible);
    this.SetMoveMessageButton();
  }

  protected void SetMoveMessageButton()
  {
    if (this.IsInitMoveMessageButton)
      return;
    this.m_isInitMoveMessageButton = true;
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_MOVE_TO_MSG, this.m_msgUserInfo != null & UserInfoManager.IsEnableCommunication() & MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName().Contains(nameof (FriendInfo)));
  }

  protected EquipItemTable.EquipItemData GetVisualModeTargetTable(
    uint base_table_id,
    EQUIPMENT_TYPE e_type,
    CharaInfo chara_info)
  {
    EquipItemTable.EquipItemData visualModeTargetTable = (EquipItemTable.EquipItemData) null;
    if (this.isVisualMode)
    {
      uint id = base_table_id;
      switch (e_type)
      {
        case EQUIPMENT_TYPE.ARMOR:
        case EQUIPMENT_TYPE.VISUAL_ARMOR:
          if (chara_info.aId != 0)
          {
            id = (uint) chara_info.aId;
            break;
          }
          break;
        case EQUIPMENT_TYPE.HELM:
        case EQUIPMENT_TYPE.VISUAL_HELM:
          if (chara_info.showHelm != 0)
          {
            if (chara_info.hId != 0)
            {
              id = (uint) chara_info.hId;
              break;
            }
            break;
          }
          id = 0U;
          break;
        case EQUIPMENT_TYPE.ARM:
        case EQUIPMENT_TYPE.VISUAL_ARM:
          if (chara_info.rId != 0)
          {
            id = (uint) chara_info.rId;
            break;
          }
          break;
        case EQUIPMENT_TYPE.LEG:
        case EQUIPMENT_TYPE.VISUAL_LEG:
          if (chara_info.lId != 0)
          {
            id = (uint) chara_info.lId;
            break;
          }
          break;
      }
      if (id != 0U)
        visualModeTargetTable = Singleton<EquipItemTable>.I.GetEquipItemData(id);
    }
    return visualModeTargetTable;
  }

  private bool IsLoopClanDetail()
  {
    List<GameSectionHistory.HistoryData> historyList = MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList();
    for (int index = 0; index < historyList.Count; ++index)
    {
      if (historyList[index].sectionName == "ClanDetail")
        return true;
    }
    return false;
  }

  protected void UpdateClanInfo(CharaInfo charaInfo)
  {
    this.clanCharaInfo = charaInfo;
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.OBJ_CLAN_ROOT, false);
    if (this.clanCharaInfo == null || this.IsLoopClanDetail())
      return;
    if (charaInfo.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
    {
      if (this.clanCharaInfo.userClanData == null || this.clanCharaInfo.userClanData.stat == 0)
        return;
      this._showClanDetail();
    }
    else if (this.clanCharaInfo.userClanData != null && this.clanCharaInfo.userClanData.stat != 0)
    {
      this._showClanDetail();
    }
    else
    {
      if (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || MonoBehaviourSingleton<UserInfoManager>.I.userClan == null || !MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsSubLeader() && !MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsLeader())
        return;
      this._showClanScout();
    }
  }

  protected void DisableClanInfo()
  {
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.OBJ_CLAN_ROOT, false);
  }

  private void _showClanDetail()
  {
    if (this.clanCharaInfo == null)
      return;
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.OBJ_CLAN_ROOT, true);
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_CLAN_SCOUT, false);
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_CLAN_SCOUT_CANCEL, false);
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_CLAN_DETAIL, true);
    this.SetLabelText(this.transRoot, (Enum) FriendInfo.UI.SPR_CLAN_NAME, this.clanCharaInfo.userClanData.name);
    if (!Object.op_Equality((Object) this.FindCtrl(this.transRoot, (Enum) FriendInfo.UI.OBJ_SYMBOL_MARK), (Object) null))
      return;
    this.StartCoroutine(this.CreateSymbolMark());
  }

  private IEnumerator CreateSymbolMark()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject symbolMarkLoadObj = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ClanSymbolMark");
    yield return (object) loadingQueue.Wait();
    Transform transform = ResourceUtility.Realizes((Object) (symbolMarkLoadObj.loadedObject as GameObject), 5);
    transform.parent = this.FindCtrl(this.transRoot, (Enum) FriendInfo.UI.OBJ_SYMBOL);
    transform.localScale = Vector3.one;
    transform.localPosition = Vector3.zero;
    ((Object) transform).name = "OBJ_SYMBOL_MARK";
    this.symbolMark = ((Component) transform).GetComponent<SymbolMarkCtrl>();
    this.symbolMark.Initilize();
    this.symbolMark.LoadSymbol(this.clanCharaInfo.userClanData.sym);
    this.symbolMark.SetSize(20);
  }

  private void _showClanScout()
  {
    if (this.clanCharaInfo == null)
      return;
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.OBJ_CLAN_ROOT, true);
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_CLAN_SCOUT, false);
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_CLAN_SCOUT_CANCEL, false);
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_CLAN_SCOUT_OFF, false);
    this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_CLAN_DETAIL, false);
    if ((int) this.clanCharaInfo.level < 15)
      this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_CLAN_SCOUT_OFF, true);
    else if (this.userClanData != null && this.userClanData.num >= MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.CLAN_MAX_MEMBER_NUM)
    {
      this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_CLAN_SCOUT_OFF, true);
      this.SetEvent(this.transRoot, (Enum) FriendInfo.UI.BTN_CLAN_SCOUT_OFF, "CLAN_SCOUT_MAX_MEMBER", 0);
    }
    else if (this.clanCharaInfo.isInviteToClan)
      this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_CLAN_SCOUT_CANCEL, true);
    else
      this.SetActive(this.transRoot, (Enum) FriendInfo.UI.BTN_CLAN_SCOUT, true);
  }

  public virtual int GetCharaSex() => this.data.sex;

  protected virtual void OnQuery_FOLLOW()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.data.name
    });
    this.SendFollow(new List<int>() { this.data.userId }, (Action<bool>) (is_success =>
    {
      if (!is_success)
        return;
      this.UpdateFollowing();
    }));
  }

  protected virtual void OnQuery_UNFOLLOW()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.data.name
    });
  }

  protected virtual void OnQuery_FriendUnFollowMessage_YES()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.data.name
    });
    this.SendUnFollow(this.data.userId, (Action<bool>) (is_success =>
    {
      if (!is_success)
        return;
      this.UpdateFollowing();
    }));
  }

  private void UpdateFollowing()
  {
    this.dataFollowing = !this.dataFollowing;
    if (this.friendCharaInfo == null)
      return;
    this.friendCharaInfo.following = this.dataFollowing;
  }

  private void UpdateFollower()
  {
    this.dataFollower = !this.dataFollower;
    if (this.friendCharaInfo == null)
      return;
    this.friendCharaInfo.follower = this.dataFollower;
  }

  private void UpdateClanInvite()
  {
    this.clanCharaInfo.isInviteToClan = !this.clanCharaInfo.isInviteToClan;
  }

  protected virtual void OnQuery_DELETEFOLLOWER()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.data.name
    });
  }

  protected virtual void OnQuery_FriendDeleteFollowerMessage_YES()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.data.name
    });
    this.SendDeleteFollower(this.data.userId, (Action<bool>) (is_success =>
    {
      if (!is_success)
        return;
      this.UpdateFollower();
    }));
  }

  protected void SendFollow(List<int> send_follow_list, Action<bool> callback = null)
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<FriendManager>.I.SendFollowUser(send_follow_list, (Action<Error, List<int>>) ((err, follow_list) =>
    {
      bool is_resume = err == Error.None && follow_list.Count > 0;
      if (callback != null)
        callback(is_resume);
      GameSection.ResumeEvent(is_resume);
    }));
  }

  protected void SendUnFollow(int send_unfollow, Action<bool> callback = null)
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<FriendManager>.I.SendUnfollowUser(send_unfollow, (Action<bool>) (is_success =>
    {
      if (callback != null)
        callback(is_success);
      GameSection.ResumeEvent(is_success);
    }));
  }

  protected void SendDeleteFollower(int send_deletefollower, Action<bool> callback = null)
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<FriendManager>.I.SendDeleteFollower(send_deletefollower, (Action<bool>) (is_success =>
    {
      if (callback != null)
        callback(is_success);
      GameSection.ResumeEvent(is_success);
    }));
  }

  protected void SendClanInvite(int send_userid, Action<bool> callback = null)
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<ClanMatchingManager>.I.SendClanInvite(send_userid, (Action<bool>) (is_success =>
    {
      if (callback != null)
        callback(is_success);
      GameSection.ResumeEvent(is_success);
    }));
  }

  protected void SendClanCancelInvite(int send_userid, Action<bool> callback = null)
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<ClanMatchingManager>.I.SendClanCancelInvite(send_userid, (Action<bool>) (is_success =>
    {
      if (callback != null)
        callback(is_success);
      GameSection.ResumeEvent(true);
    }));
  }

  protected virtual void OnQuery_BLACK_LIST_IN()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.data.name
    });
    GameSection.StayEvent();
    MonoBehaviourSingleton<BlackListManager>.I.SendAdd(this.data.userId, (Action<bool>) (is_success =>
    {
      if (is_success)
      {
        this.dataFollowing = false;
        if (this.friendCharaInfo != null)
          this.friendCharaInfo.following = false;
      }
      GameSection.ResumeEvent(is_success);
    }));
  }

  protected virtual void OnQuery_BLACK_LIST_OUT()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.data.name
    });
    GameSection.StayEvent();
    MonoBehaviourSingleton<BlackListManager>.I.SendDelete(this.data.userId, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  protected virtual void OnQuery_CHANGE_MODE()
  {
    this.RefreshUI();
    this.LoadModel();
  }

  protected virtual void OnQuery_DETAIL()
  {
    if (this.isVisualMode)
    {
      GameSection.ChangeEvent("VISUAL_DETAIL");
      this.OnQuery_VISUAL_DETAIL();
    }
    else
    {
      int eventData = (int) GameSection.GetEventData();
      GameSection.SetEventData((object) new object[3]
      {
        (object) new object[4]
        {
          (object) ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT,
          (object) this.GetEquipSetAttachSkillListData(this.data.equipSet)[eventData],
          (object) this.data.sex,
          (object) this.data.faceId
        },
        (object) false,
        (object) false
      });
    }
  }

  protected virtual void OnQuery_VISUAL_DETAIL() => GameSection.StopEvent();

  protected virtual void OnQuery_SKILL_LIST()
  {
    GameSection.SetEventData((object) new object[3]
    {
      (object) new object[5]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT,
        (object) this.GetEquipSetAttachSkillListData(this.data.equipSet),
        (object) true,
        (object) this.data.sex,
        (object) this.data.faceId
      },
      (object) false,
      (object) false
    });
  }

  protected virtual void OnQuery_ABILITY()
  {
    EquipSetInfo equipSetData = MonoBehaviourSingleton<StatusManager>.I.CreateEquipSetData(this.data.equipSet);
    GameSection.SetEventData((object) new object[3]
    {
      (object) new object[3]
      {
        (object) equipSetData,
        (object) MonoBehaviourSingleton<StatusManager>.I.GetEquipSetAbility(equipSetData),
        (object) new EquipSetDetailStatusAndAbilityTable.BaseStatus((int) this.data.atk, (int) this.data.def, (int) this.data.hp, this.data.equipSet)
      },
      (object) false,
      (object) false
    });
  }

  protected virtual void OnQuery_STATUS()
  {
    EquipSetInfo equipSetData = MonoBehaviourSingleton<StatusManager>.I.CreateEquipSetData(this.data.equipSet);
    GameSection.SetEventData((object) new object[3]
    {
      (object) new object[3]
      {
        (object) equipSetData,
        (object) MonoBehaviourSingleton<StatusManager>.I.GetEquipSetAbility(equipSetData),
        (object) new EquipSetDetailStatusAndAbilityTable.BaseStatus((int) this.data.atk, (int) this.data.def, (int) this.data.hp, this.data.equipSet)
      },
      (object) false,
      (object) false
    });
  }

  protected virtual void OnQuery_SECTION_BACK()
  {
    MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = -1;
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM;
  }

  public void OnQuery_TO_MESSAGE()
  {
    if (this.m_msgUserInfo == null)
    {
      GameSection.StopEvent();
    }
    else
    {
      if (this.m_msgUserInfo.isPermitted)
        return;
      GameSection.ChangeEvent("NOT_PERMITTED");
    }
  }

  public void OnQuery_FriendMessageConfirmMessage_YES()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<FriendManager>.I.SendGetMessageDetailList(this.m_msgUserInfo.userId, 0, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
    MonoBehaviourSingleton<FriendManager>.I.SetNoReadMessageNum(MonoBehaviourSingleton<FriendManager>.I.noReadMessageNum - this.m_msgUserInfo.noReadNum);
    this.m_msgUserInfo.noReadNum = 0;
  }

  protected virtual void OnQuery_CLAN_SCOUT()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.clanCharaInfo.name
    });
  }

  protected virtual void OnQuery_CLAN_SCOUT_OFF()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.clanCharaInfo.name
    });
  }

  protected virtual void OnQuery_ClanScoutDialog_YES()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.clanCharaInfo.name
    });
    this.SendClanInvite(this.clanCharaInfo.userId, (Action<bool>) (is_success =>
    {
      if (is_success)
      {
        this.UpdateClanInvite();
        GameSection.ChangeStayEvent("COMPLETE");
        MonoBehaviourSingleton<FriendManager>.I.SetClanInviteToHomeCharaInfo(this.clanCharaInfo.userId, this.clanCharaInfo.isInviteToClan);
      }
      else
      {
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
        {
          new EventData("[BACK]")
        });
        this._showClanScout();
      }
    }));
  }

  protected virtual void OnQuery_CLAN_SCOUT_CANCEL()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.clanCharaInfo.name
    });
  }

  protected virtual void OnQuery_ClanScoutCancelDialog_YES()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.clanCharaInfo.name
    });
    this.SendClanCancelInvite(this.clanCharaInfo.userId, (Action<bool>) (is_success =>
    {
      if (!is_success)
        return;
      this.UpdateClanInvite();
      GameSection.ChangeStayEvent("COMPLETE");
      MonoBehaviourSingleton<FriendManager>.I.SetClanInviteToHomeCharaInfo(this.clanCharaInfo.userId, this.clanCharaInfo.isInviteToClan);
    }));
  }

  public void OnQuery_CLAN_DETAIL()
  {
    if (this.clanCharaInfo == null)
      return;
    if ((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 15)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) this.clanCharaInfo.userClanData.cId);
  }

  protected enum UI
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
    SPR_SAME_CLAN_ICON,
    LBL_LEVEL_WEAPON_1,
    LBL_LEVEL_WEAPON_2,
    LBL_LEVEL_WEAPON_3,
    LBL_LEVEL_ARMOR,
    LBL_LEVEL_HELM,
    LBL_LEVEL_ARM,
    LBL_LEVEL_LEG,
    LBL_CHANGE_MODE,
    BTN_MAGI,
    LBL_SET_NAME,
    OBJ_DEGREE_PLATE_ROOT,
    BTN_DELETEFOLLOWER,
    BTN_KICK,
    BTN_JOIN,
    BTN_MOVE_TO_MSG,
    OBJ_CLAN_ROOT,
    BTN_CLAN_SCOUT,
    SPR_CLAN_SCOUT,
    BTN_CLAN_DETAIL,
    TXT_CLAN_TITLE,
    SPR_CLAN_NAME,
    BTN_CLAN_SCOUT_CANCEL,
    BTN_CLAN_SCOUT_OFF,
    OBJ_SYMBOL,
    OBJ_SYMBOL_MARK,
  }
}
