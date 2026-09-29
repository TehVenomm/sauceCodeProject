// Decompiled with JetBrains decompiler
// Type: FollowListBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public abstract class FollowListBase : UserListBase<FriendCharaInfo>
{
  protected const int ITEM_COUNT_PER_PAGE = 10;
  public static readonly Color TEXT_BASE_COLOR_DEFAULT = new Color(1f, 1f, 1f, 1f);
  public static readonly Color TEXT_BASE_COLOR_ONLINE = new Color(0.858823538f, 1f, 0.8627451f, 1f);
  public static readonly Color TEXT_BASE_COLOR_GO_FIELD = new Color(1f, 0.8901961f, 0.8901961f, 1f);
  public static readonly Color TEXT_BASE_COLOR_GO_QUEST = new Color(1f, 0.8901961f, 0.8901961f, 1f);
  public static readonly Color TEXT_BASE_COLOR_GO_LOUNGE = new Color(0.858823538f, 1f, 0.8627451f, 1f);
  public static readonly Color TEXT_OUTLINE_COLOR_DEFAULT = new Color(0.3529412f, 0.3529412f, 0.3529412f, 1f);
  public static readonly Color TEXT_OUTLINE_COLOR_ONLINE = new Color(0.0392156877f, 0.5058824f, 0.09803922f, 1f);
  public static readonly Color TEXT_OUTLINE_COLOR_GO_FIELD = new Color(0.933333337f, 0.478431374f, 0.129411772f, 1f);
  public static readonly Color TEXT_OUTLINE_COLOR_GO_QUEST = new Color(0.933333337f, 0.478431374f, 0.129411772f, 1f);
  public static readonly Color TEXT_OUTLINE_COLOR_GO_LOUNGE = new Color(0.0392156877f, 0.5058824f, 0.09803922f, 1f);
  private List<Transform> m_generatedItemList = new List<Transform>();
  protected bool m_isVisibleDefaultInfo = true;
  private Action<int> m_onJoinButtonPressedCallback;
  protected FollowListBase.TITLE_TYPE titleType;
  protected USER_SORT_TYPE m_currentSortType;
  private UIGrid m_scrollGrid;

  protected UIGrid ScrollGrid
  {
    get
    {
      return this.m_scrollGrid ?? (this.m_scrollGrid = ((Component) this.GetCtrl((Enum) FollowListBase.UI.GRD_LIST)).GetComponent<UIGrid>());
    }
  }

  public override void Initialize()
  {
    this.SetActive((Enum) FollowListBase.UI.SPR_TITLE_FOLLOW_LIST, this.titleType == FollowListBase.TITLE_TYPE.FOLLOW);
    this.SetActive((Enum) FollowListBase.UI.SPR_TITLE_FOLLOWER_LIST, this.titleType == FollowListBase.TITLE_TYPE.FOLLOWER);
    this.SetActive((Enum) FollowListBase.UI.SPR_TITLE_MESSAGE, this.titleType == FollowListBase.TITLE_TYPE.MESSAGE);
    this.SetActive((Enum) FollowListBase.UI.SPR_TITLE_BLACKLIST, this.titleType == FollowListBase.TITLE_TYPE.BLACKLIST);
    this.SetActive((Enum) FollowListBase.UI.OBJ_FOLLOW_NUMBER_ROOT, false);
    if (this.IsHideSwitchInfoButton())
      this.SetActive(this._transform, (Enum) FollowListBase.UI.OBJ_SWITCH_INFO, false);
    base.Initialize();
  }

  public virtual void ListUI()
  {
    this.SetLabelText((Enum) FollowListBase.UI.STR_TITLE, this.sectionData.GetText("STR_TITLE"));
    this.SetLabelText((Enum) FollowListBase.UI.STR_TITLE_REFLECT, this.sectionData.GetText("STR_TITLE"));
    if (((IList<FriendCharaInfo>) this.GetCurrentUserArray()).IsNullOrEmpty<FriendCharaInfo>())
    {
      this.SetActive((Enum) FollowListBase.UI.STR_NON_LIST, true);
      this.SetActive((Enum) FollowListBase.UI.GRD_LIST, false);
      this.SetActive((Enum) FollowListBase.UI.OBJ_ACTIVE_ROOT, false);
      this.SetActive((Enum) FollowListBase.UI.OBJ_INACTIVE_ROOT, true);
      this.SetLabelText((Enum) FollowListBase.UI.LBL_NOW, "0");
      this.SetLabelText((Enum) FollowListBase.UI.LBL_MAX, "0");
    }
    else
    {
      this.SetLabelText((Enum) FollowListBase.UI.LBL_SORT, StringTable.Get(STRING_CATEGORY.USER_SORT, (uint) this.m_currentSortType));
      this.SetPageNumText((Enum) FollowListBase.UI.LBL_NOW, this.nowPage + 1);
      this.SetPageNumText((Enum) FollowListBase.UI.LBL_MAX, this.pageNumMax);
      this.SetActive((Enum) FollowListBase.UI.STR_NON_LIST, false);
      this.SetActive((Enum) FollowListBase.UI.GRD_LIST, true);
      this.SetActive((Enum) FollowListBase.UI.OBJ_ACTIVE_ROOT, this.pageNumMax != 1);
      this.SetActive((Enum) FollowListBase.UI.OBJ_INACTIVE_ROOT, this.pageNumMax == 1);
      this.UpdateDynamicList();
    }
  }

  protected virtual void UpdateDynamicList()
  {
    FriendCharaInfo[] info = this.GetCurrentUserArray();
    int length = ((IList<FriendCharaInfo>) info).IsNullOrEmpty<FriendCharaInfo>() ? 0 : info.Length;
    if (GameDefine.ACTIVE_DEGREE)
      this.ScrollGrid.cellHeight = (float) GameDefine.DEGREE_FRIEND_LIST_HEIGHT;
    this.CleanItemList();
    this.SetDynamicList((Enum) FollowListBase.UI.GRD_LIST, this.GetListItemName, length, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) => this.SetListItem(i, t, is_recycle, info[i])));
  }

  protected virtual void SetListItem(int i, Transform t, bool is_recycle, FriendCharaInfo data)
  {
    if (data == null)
      return;
    this.SetFollowStatus(t, data.userId, data.following, data.follower, data.userClanData.cId);
    this.SetCharaInfo(data, i, t, is_recycle, data.userId == 0);
    if (!LoungeMatchingManager.IsValidInLounge())
      return;
    this.SetActive(t, (Enum) FollowListBase.UI.SPR_ICON_FIRST_MET, MonoBehaviourSingleton<LoungeMatchingManager>.I.CheckFirstMet(data.userId));
  }

  protected virtual void SetCharaInfo(
    FriendCharaInfo data,
    int i,
    Transform t,
    bool is_recycle,
    bool isGM)
  {
    if (isGM)
    {
      this.SetEvent(t, "DIRECT_VIEW_MESSAGE", i);
      this.SetRenderNPCModel(t, (Enum) FollowListBase.UI.TEX_MODEL, 0, new Vector3(0.0f, -1.49f, 1.87f), new Vector3(0.0f, 154f, 0.0f), 10f);
    }
    else
    {
      this.SetEvent(t, "FOLLOW_INFO", i);
      this.ForceSetRenderPlayerModel(t, (Enum) FollowListBase.UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo((CharaInfo) data, false, true, false, true), 99, new Vector3(0.0f, -1.536f, 1.87f), new Vector3(0.0f, 154f, 0.0f), true);
    }
    CharaInfo.ClanInfo clanInfo = data.clanInfo;
    if (clanInfo == null)
    {
      clanInfo = new CharaInfo.ClanInfo();
      clanInfo.clanId = -1;
      clanInfo.tag = string.Empty;
    }
    bool isSameTeam = clanInfo.clanId > -1 && MonoBehaviourSingleton<GuildManager>.I.guildData != null && clanInfo.clanId == MonoBehaviourSingleton<GuildManager>.I.guildData.clanId;
    this.SetSupportEncoding(t, (Enum) FollowListBase.UI.LBL_NAME, true);
    this.SetLabelText(t, (Enum) FollowListBase.UI.LBL_NAME, Utility.GetNameWithColoredClanTag(clanInfo.tag, data.name, data.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, isSameTeam));
    this.SetLabelText(t, (Enum) FollowListBase.UI.LBL_LEVEL, data.level.ToString());
    this.SetLabelText(t, (Enum) FollowListBase.UI.LBL_COMMENT, data.comment);
    this.SetLabelText(t, (Enum) FollowListBase.UI.LBL_LAST_LOGIN, this.sectionData.GetText("LAST_LOGIN"));
    this.SetLabelText(t, (Enum) FollowListBase.UI.LBL_LAST_LOGIN_TIME, data.lastLogin);
    EquipSetCalculator equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(i + 4);
    equipSetCalculator.SetEquipSet(data.equipSet);
    SimpleStatus finalStatus = equipSetCalculator.GetFinalStatus(0, (int) data.hp, (int) data.atk, (int) data.def);
    this.SetLabelText(t, (Enum) FollowListBase.UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
    this.SetLabelText(t, (Enum) FollowListBase.UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
    this.SetLabelText(t, (Enum) FollowListBase.UI.LBL_HP, finalStatus.hp.ToString());
    ((Component) this.FindCtrl(t, (Enum) FollowListBase.UI.OBJ_DEGREE_FRAME_ROOT)).GetComponent<DegreePlate>().Initialize(data.selectedDegrees, false, (Action<DegreePlate>) (x => this.ScrollGrid.Reposition()));
    this.SetJoinInfo(t, i, data.joinStatus, data.lastLogin);
    if (this.m_generatedItemList.Contains(t))
      return;
    this.m_generatedItemList.Add(t);
  }

  protected void SetFollowStatus(
    Transform t,
    int user_id,
    bool following,
    bool follower,
    string clanId)
  {
    bool is_visible1 = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(user_id);
    bool is_visible2 = false;
    if (MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
      is_visible2 = clanId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId;
    bool is_visible3 = !is_visible1 && following | follower;
    this.SetActive(t, (Enum) FollowListBase.UI.SPR_BLACKLIST_ICON, is_visible1);
    this.SetActive(t, (Enum) FollowListBase.UI.OBJ_FOLLOW, is_visible3);
    this.SetActive(t, (Enum) FollowListBase.UI.SPR_FOLLOW, is_visible3 & following);
    this.SetActive(t, (Enum) FollowListBase.UI.SPR_FOLLOWER, is_visible3 & follower);
    this.SetActive(t, (Enum) FollowListBase.UI.SPR_SAME_CLAN_ICON, is_visible2);
    UIGrid component = this.GetComponent<UIGrid>(t, (Enum) FollowListBase.UI.GRD_FOLLOW_ARROW);
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Reposition();
  }

  protected List<FriendCharaInfo> ChangeData(List<FriendCharaInfo> chara_list)
  {
    List<FriendCharaInfo> new_list = new List<FriendCharaInfo>();
    chara_list.ForEach((Action<FriendCharaInfo>) (chara_info =>
    {
      if (chara_info == null)
        return;
      new_list.Add(new FriendCharaInfo()
      {
        userId = chara_info.userId,
        name = chara_info.name,
        comment = chara_info.comment,
        lastLogin = chara_info.lastLogin,
        code = chara_info.code,
        hp = chara_info.hp,
        atk = chara_info.atk,
        def = chara_info.def,
        level = chara_info.level,
        sex = chara_info.sex,
        faceId = chara_info.faceId,
        hairId = chara_info.hairId,
        hairColorId = chara_info.hairColorId,
        skinId = chara_info.skinId,
        voiceId = chara_info.voiceId,
        isInviteToClan = chara_info.isInviteToClan,
        aId = chara_info.aId,
        hId = chara_info.hId,
        rId = chara_info.rId,
        lId = chara_info.lId,
        showHelm = chara_info.showHelm,
        equipSet = chara_info.equipSet,
        following = chara_info.following,
        follower = chara_info.follower,
        selectedDegrees = chara_info.selectedDegrees,
        clanInfo = chara_info.clanInfo,
        accessory = chara_info.accessory,
        userClanData = chara_info.userClanData
      });
    }));
    return new_list;
  }

  public virtual void OnQuery_FOLLOW_INFO()
  {
    int eventData = (int) GameSection.GetEventData();
    FriendCharaInfo[] currentUserArray = this.GetCurrentUserArray();
    if (eventData < 0 || ((IList<FriendCharaInfo>) currentUserArray).IsNullOrEmpty<FriendCharaInfo>() || currentUserArray.Length <= eventData)
      return;
    FriendCharaInfo event_data = currentUserArray[eventData];
    MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = eventData + 4;
    GameSection.SetEventData((object) event_data);
  }

  protected void SetDirtyTable() => this.SetDirty((Enum) FollowListBase.UI.GRD_LIST);

  protected virtual string GetListItemName => "FollowListBaseItem";

  protected virtual FriendCharaInfo[] GetCurrentUserArray()
  {
    return this.recvList == null ? (FriendCharaInfo[]) null : this.recvList.ToArray();
  }

  protected virtual List<FriendCharaInfo> GetCurrentUserList() => this.recvList;

  protected virtual bool IsHideSwitchInfoButton() => true;

  protected void SwitchUserStatusInfo(bool _isVisibleDefaultInfo)
  {
    if (this.m_isVisibleDefaultInfo == _isVisibleDefaultInfo)
      return;
    this.m_isVisibleDefaultInfo = _isVisibleDefaultInfo;
    int index = 0;
    for (int count = this.m_generatedItemList.Count; index < count; ++index)
    {
      if (!Object.op_Equality((Object) this.m_generatedItemList[index], (Object) null))
        this.SwitchInfoRootObject(this.m_generatedItemList[index], this.m_isVisibleDefaultInfo);
    }
  }

  protected virtual void SetJoinInfo(
    Transform t,
    int _itemIndex,
    FriendCharaInfo.JoinInfo _joinStatus,
    string _lastLoginText)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    this.SwitchInfoRootObject(t, this.m_isVisibleDefaultInfo);
    if (_joinStatus == null)
      return;
    this.SetJoinTextInfo(t, _joinStatus);
    this.SetJoinButtonSettings(t, (ONLINE_STATUS) _joinStatus.joinType, _itemIndex);
    this.SetEvent(t, (Enum) FollowListBase.UI.BTN_JOIN_BUTTON, "JOIN_FRIEND", _itemIndex);
  }

  private void SetJoinTextInfo(Transform _target, FriendCharaInfo.JoinInfo _joinStatus)
  {
    UILabel component1 = ((Component) this.FindCtrl(_target, (Enum) FollowListBase.UI.ONLINE_TEXT)).GetComponent<UILabel>();
    UILabel component2 = ((Component) this.FindCtrl(_target, (Enum) FollowListBase.UI.DETAIL_TEXT)).GetComponent<UILabel>();
    if (Object.op_Inequality((Object) component1, (Object) null))
      component1.text = StringTable.Get(STRING_CATEGORY.FRIEND_JOIN, (uint) _joinStatus.joinType);
    if (Object.op_Inequality((Object) component2, (Object) null))
      component2.text = "";
    switch (_joinStatus.joinType)
    {
      case 1:
        if (!Object.op_Inequality((Object) component1, (Object) null))
          break;
        component1.color = FollowListBase.TEXT_BASE_COLOR_ONLINE;
        component1.effectColor = FollowListBase.TEXT_OUTLINE_COLOR_ONLINE;
        break;
      case 2:
        if (!Object.op_Inequality((Object) component1, (Object) null))
          break;
        component1.color = FollowListBase.TEXT_BASE_COLOR_GO_LOUNGE;
        component1.effectColor = FollowListBase.TEXT_OUTLINE_COLOR_GO_LOUNGE;
        break;
      case 3:
        if (Object.op_Inequality((Object) component1, (Object) null))
        {
          component1.color = FollowListBase.TEXT_BASE_COLOR_GO_QUEST;
          component1.effectColor = FollowListBase.TEXT_OUTLINE_COLOR_GO_QUEST;
        }
        if (!Object.op_Inequality((Object) component2, (Object) null))
          break;
        component2.text = this.GetQuestName(_joinStatus.targetParam);
        break;
      case 4:
        if (Object.op_Inequality((Object) component1, (Object) null))
        {
          component1.color = FollowListBase.TEXT_BASE_COLOR_GO_FIELD;
          component1.effectColor = FollowListBase.TEXT_OUTLINE_COLOR_GO_FIELD;
        }
        if (!Object.op_Inequality((Object) component2, (Object) null))
          break;
        component2.text = this.GetMapFieldNameText(_joinStatus.targetParam);
        break;
      default:
        if (!Object.op_Inequality((Object) component1, (Object) null))
          break;
        component1.color = FollowListBase.TEXT_BASE_COLOR_DEFAULT;
        component1.effectColor = FollowListBase.TEXT_OUTLINE_COLOR_DEFAULT;
        break;
    }
  }

  private string GetMapFieldNameText(int _fieldMapId)
  {
    FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint) _fieldMapId);
    if (fieldMapData == null)
      return string.Empty;
    RegionTable.Data data = Singleton<RegionTable>.I.GetData(fieldMapData.regionId);
    return data == null ? fieldMapData.mapName : $"{data.regionName}-{fieldMapData.mapName}";
  }

  private string GetQuestName(int _questId)
  {
    if (!Singleton<QuestTable>.IsValid())
      return string.Empty;
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint) _questId);
    return questData != null ? questData.questText : string.Empty;
  }

  protected void SwitchInfoRootObject(Transform t, bool _activeFlag)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    this.SetActive(t, (Enum) FollowListBase.UI.DEFAULT_STATUS_ROOT, _activeFlag);
    this.SetActive(t, (Enum) FollowListBase.UI.JOIN_STATUS_ROOT, !_activeFlag);
  }

  protected void SetJoinButtonSettings(Transform _t, ONLINE_STATUS _status, int _itemIndex)
  {
    if (Object.op_Equality((Object) _t, (Object) null))
      return;
    UIButton component = ((Component) this.FindCtrl(_t, (Enum) FollowListBase.UI.BTN_JOIN_BUTTON)).GetComponent<UIButton>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    switch (_status)
    {
      case ONLINE_STATUS.ONLINE_LOUNGE:
      case ONLINE_STATUS.ONLINE_QUEST:
      case ONLINE_STATUS.ONLINE_FIELD:
        ((Component) component).gameObject.SetActive(true);
        break;
      default:
        ((Component) component).gameObject.SetActive(false);
        break;
    }
    UIGameSceneEventSender componentInChildren = ((Component) component).GetComponentInChildren<UIGameSceneEventSender>(true);
    if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
      return;
    componentInChildren.eventName = "JOIN_FRIEND";
    componentInChildren.eventData = (object) _itemIndex;
  }

  protected void OnQuery_SWITCH_INFO() => this.SwitchUserStatusInfo(!this.m_isVisibleDefaultInfo);

  protected virtual void OnQuery_JOIN_FRIEND()
  {
    int eventData = (int) GameSection.GetEventData();
    FriendCharaInfo[] currentUserArray = this.GetCurrentUserArray();
    if (currentUserArray == null || currentUserArray.Length <= eventData || eventData < 0)
      return;
    FriendCharaInfo.JoinInfo joinStatus = currentUserArray[eventData].joinStatus;
    if (joinStatus == null)
      return;
    GameSection.StayEvent();
    switch (joinStatus.joinType)
    {
      case 0:
        if (!MonoBehaviourSingleton<PartyManager>.IsValid())
          break;
        MonoBehaviourSingleton<PartyManager>.I.SendApply(joinStatus.conditionParam, (Action<bool, Error>) ((isSucceed, error) => GameSection.ResumeEvent(true)), joinStatus.targetParam);
        break;
      case 1:
        this.JoinLounge(joinStatus);
        break;
      case 2:
        int _toUserId = int.Parse(joinStatus.conditionParam);
        this.JoinField(joinStatus.targetParam, _toUserId, (Action<bool, bool, bool>) ((is_matching, is_connect, is_regist) =>
        {
          if (!is_matching)
            return;
          if (!is_connect)
          {
            GameSection.ResumeEvent(true);
            MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => this.DispatchEvent("CLOSE"));
          }
          else
          {
            GameSection.ResumeEvent(is_regist);
            if (!is_regist)
              return;
            MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame");
          }
        }));
        break;
    }
  }

  protected void CleanItemList()
  {
    for (int index = this.m_generatedItemList.Count - 1; 0 <= index; --index)
    {
      if (Object.op_Equality((Object) this.m_generatedItemList[index], (Object) null))
        this.m_generatedItemList.RemoveAt(index);
    }
  }

  private bool IsEnableJoinField(
    int _fieldMapId,
    out FieldMapTable.FieldMapTableData _fieldMapTableData)
  {
    _fieldMapTableData = (FieldMapTable.FieldMapTableData) null;
    if (!MonoBehaviourSingleton<FieldManager>.IsValid())
      return false;
    FieldManager i = MonoBehaviourSingleton<FieldManager>.I;
    if ((long) _fieldMapId == (long) i.currentMapID)
      return false;
    _fieldMapTableData = Singleton<FieldMapTable>.I.GetFieldMapData((uint) _fieldMapId);
    if (_fieldMapTableData == null || _fieldMapTableData.jumpPortalID == 0U)
    {
      Log.Error("RegionMap.OnQuery_SELECT() jumpPortalID is not found.");
      return false;
    }
    if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(_fieldMapTableData.jumpPortalID, false))
      return false;
    if (i.CanJumpToMap(_fieldMapTableData))
      return true;
    GameSceneEvent.PushStay();
    MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.GetErrorCodeText(30301U)), (Action<string>) (ret =>
    {
      GameSceneEvent.PopStay();
      GameSection.ResumeEvent(false);
    }));
    return false;
  }

  protected void JoinField(int fieldMapId, int _toUserId, Action<bool, bool, bool> _callback)
  {
    FieldMapTable.FieldMapTableData _fieldMapTableData = (FieldMapTable.FieldMapTableData) null;
    if (!this.IsEnableJoinField(fieldMapId, out _fieldMapTableData))
    {
      if (_callback == null)
        return;
      _callback(false, false, false);
    }
    else
      CoopApp.EnterField(_fieldMapTableData.jumpPortalID, 0U, _toUserId, _callback);
  }

  protected void JoinLounge(FriendCharaInfo.JoinInfo _joinData)
  {
    if (!MonoBehaviourSingleton<LoungeMatchingManager>.IsValid())
      GameSection.ResumeEvent(true);
    else if (_joinData == null)
      GameSection.ResumeEvent(true);
    else if (!LoungeMatchingManager.IsValidInLounge())
    {
      GameSection.ResumeEvent(true);
      GameSection.SetEventData((object) _joinData.conditionParam);
      this.OnQuery_FORCE_MOVETO_LOUNGE();
    }
    else if (MonoBehaviourSingleton<LoungeMatchingManager>.I.GetLoungeNumber() == _joinData.conditionParam)
    {
      GameSection.ResumeEvent(true);
    }
    else
    {
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
      {
        new EventData("FORCE_MOVETO_HOME", (object) null),
        new EventData("FORCE_MOVETO_LOUNGE", (object) _joinData.conditionParam)
      });
      GameSection.ResumeEvent(true);
    }
  }

  protected void Sort<T>(List<T> _userList) where T : FriendCharaInfo
  {
    if (_userList == null || _userList.Count == 0)
      return;
    switch (this.m_currentSortType)
    {
      case USER_SORT_TYPE.NAME:
        _userList.Sort(new Comparison<T>(this.UserCompareByName<T>));
        break;
      case USER_SORT_TYPE.LEVEL:
        _userList.Sort(new Comparison<T>(this.UserCompareByLevel<T>));
        break;
      case USER_SORT_TYPE.LOGIN:
        _userList.Sort(new Comparison<T>(this.UserCompareByLoginTime<T>));
        break;
      case USER_SORT_TYPE.PLAY_COUNT:
        _userList.Sort(new Comparison<T>(this.UserCompareByPlayCount<T>));
        break;
      case USER_SORT_TYPE.REGISTER:
        _userList.Sort(new Comparison<T>(this.UserCompareByResistered<T>));
        break;
    }
  }

  protected int UserCompareByName<T>(T a, T b) where T : CharaInfo
  {
    return string.Compare(a.name, b.name);
  }

  protected int UserCompareByLevel<T>(T a, T b) where T : CharaInfo
  {
    return (int) b.level - (int) a.level;
  }

  protected int UserCompareByLoginTime<T>(T a, T b) where T : CharaInfo
  {
    return b.lastLoginTm - a.lastLoginTm;
  }

  protected int UserCompareByPlayCount<T>(T a, T b) where T : FriendCharaInfo
  {
    return b.playedCount - a.playedCount;
  }

  protected int UserCompareByResistered<T>(T a, T b) where T : FriendCharaInfo
  {
    return a.follower_id > 0 && b.follower_id > 0 ? b.follower_id - a.follower_id : b.following_id - a.following_id;
  }

  protected virtual void OnQuery_SORT()
  {
    this.UpdateSortType();
    this.Sort<FriendCharaInfo>(this.GetCurrentUserList());
    this.RefreshUI();
  }

  protected virtual void UpdateSortType()
  {
    ++this.m_currentSortType;
    if (this.m_currentSortType == USER_SORT_TYPE.PLAY_COUNT)
      ++this.m_currentSortType;
    if (this.m_currentSortType < USER_SORT_TYPE.MAX)
      return;
    this.m_currentSortType = USER_SORT_TYPE.NAME;
  }

  protected enum UI
  {
    SPR_TITLE_FOLLOW_LIST,
    SPR_TITLE_FOLLOWER_LIST,
    SPR_TITLE_MESSAGE,
    SPR_TITLE_BLACKLIST,
    OBJ_FOLLOW_NUMBER_ROOT,
    LBL_FOLLOW_NUMBER_NOW,
    LBL_FOLLOW_NUMBER_MAX,
    OBJ_DISABLE_USER_MASK,
    LBL_NAME,
    GRD_LIST,
    TEX_MODEL,
    STR_NON_LIST,
    GRD_FOLLOW_ARROW,
    OBJ_FOLLOW,
    SPR_FOLLOW,
    SPR_FOLLOWER,
    SPR_BLACKLIST_ICON,
    SPR_SAME_CLAN_ICON,
    OBJ_COMMENT,
    LBL_COMMENT,
    LBL_LAST_LOGIN,
    LBL_LAST_LOGIN_TIME,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    LBL_LEVEL,
    LBL_NOW,
    LBL_MAX,
    OBJ_ACTIVE_ROOT,
    OBJ_INACTIVE_ROOT,
    BTN_PAGE_PREV,
    BTN_PAGE_NEXT,
    STR_TITLE,
    STR_TITLE_REFLECT,
    OBJ_DEGREE_FRAME_ROOT,
    SPR_ICON_FIRST_MET,
    OBJ_SWITCH_INFO,
    DEFAULT_STATUS_ROOT,
    JOIN_STATUS_ROOT,
    ONLINE_TEXT_ROOT,
    ONLINE_TEXT,
    DETAIL_TEXT,
    JOIN_BUTTON_ROOT,
    BTN_JOIN_BUTTON,
    LBL_BUTTON_TEXT,
    BTN_SORT,
    LBL_SORT,
  }

  protected enum TITLE_TYPE
  {
    FOLLOW,
    FOLLOWER,
    MESSAGE,
    BLACKLIST,
  }
}
