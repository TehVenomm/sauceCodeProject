// Decompiled with JetBrains decompiler
// Type: ClanDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ClanDetail : GameSection
{
  private const int ITEM_COUNT_PER_PAGE = 10;
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
  private string sendClanId;
  private List<FriendCharaInfo> members;
  private ClanData clanData;
  private bool isRequested;
  private bool isInvited;
  private SymbolMarkCtrl symbolMark;
  private int nowPage;
  private int pageNumMax;

  public override void Initialize()
  {
    this.sendClanId = (string) GameSection.GetEventData();
    this.StartCoroutine(this.Reload((System.Action) (() => base.Initialize())));
  }

  private IEnumerator Reload(System.Action cb = null)
  {
    bool is_recv = false;
    this.SendRequest((System.Action) (() => is_recv = true));
    while (!is_recv)
      yield return (object) null;
    this.SetDirtyTable();
    this.RefreshUI();
    if (cb != null)
      cb();
  }

  private void SendRequest(System.Action onFinish)
  {
    MonoBehaviourSingleton<ClanMatchingManager>.I.RequestDetail(this.sendClanId, (Action<ClanDetailModel.Param>) (result =>
    {
      this.members = result.memberList;
      this.clanData = result.clan;
      this.isRequested = result.isRequested;
      this.isInvited = result.isInvited;
      this.nowPage = 0;
      this.pageNumMax = Mathf.CeilToInt((float) this.members.Count / 10f);
      onFinish();
    }));
  }

  public override void UpdateUI()
  {
    this.UpdateHeaderUI();
    this.UpdateListUI();
    this.UpdateFooterUI();
    this.SetActive((Enum) ClanDetail.UI.LBL_NON_LIST, this.members.Count <= 0);
  }

  public void UpdateHeaderUI()
  {
    this.SetLabelText((Enum) ClanDetail.UI.LBL_CLAN_NAME, this.clanData.name);
    this.SetLabelText((Enum) ClanDetail.UI.LBL_CLAN_LEVEL, this.clanData.lv.ToString());
    this.SetLabelText((Enum) ClanDetail.UI.LBL_APLLY_TYPE, StringTable.Get(STRING_CATEGORY.JOIN_TYPE, (uint) this.clanData.jt));
    this.SetLabelText((Enum) ClanDetail.UI.LBL_MODE, StringTable.Get(STRING_CATEGORY.CLAN_LABEL, (uint) this.clanData.lbl));
    this.SetLabelText((Enum) ClanDetail.UI.LBL_COMMENT, this.clanData.cmt);
    this.SetLabelText((Enum) ClanDetail.UI.LBL_MEMBER_NUMBER_NOW, this.members.Count.ToString());
    this.SetLabelText((Enum) ClanDetail.UI.LBL_MEMBER_NUMBER_MAX, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.CLAN_MAX_MEMBER_NUM.ToString());
    this.StartCoroutine(this.CreateSymbolMark());
  }

  private IEnumerator CreateSymbolMark()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject symbolMarkLoadObj = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ClanSymbolMark");
    yield return (object) loadingQueue.Wait();
    Transform transform = ResourceUtility.Realizes((Object) (symbolMarkLoadObj.loadedObject as GameObject), 5);
    transform.parent = this.GetCtrl((Enum) ClanDetail.UI.OBJ_SYMBOL);
    transform.localScale = Vector3.one;
    transform.localPosition = Vector3.zero;
    this.symbolMark = ((Component) transform).GetComponent<SymbolMarkCtrl>();
    this.symbolMark.Initilize();
    this.symbolMark.LoadSymbol(this.clanData.sym);
  }

  private void UpdateListUI()
  {
    this.SetPageNumText((Enum) ClanDetail.UI.LBL_NOW, this.nowPage + 1);
    this.SetPageNumText((Enum) ClanDetail.UI.LBL_MAX, this.pageNumMax);
    this.SetActive((Enum) ClanDetail.UI.OBJ_ACTIVE_ROOT, this.pageNumMax != 1);
    this.SetActive((Enum) ClanDetail.UI.OBJ_INACTIVE_ROOT, this.pageNumMax == 1);
    this.UpdateDynamicList();
  }

  private void UpdateFooterUI()
  {
    bool is_visible = this.clanData.cId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId;
    MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsNotRegistered();
    this.SetActive((Enum) ClanDetail.UI.BTN_LEAVE, is_visible);
    this.SetActive((Enum) ClanDetail.UI.BTN_INVITE, false);
    if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered() || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "ClanScene")
    {
      this.SetActive((Enum) ClanDetail.UI.BTN_APPLY, false);
      this.SetActive((Enum) ClanDetail.UI.BTN_APPLY_CANCEL, false);
    }
    else if (this.isRequested)
    {
      this.SetActive((Enum) ClanDetail.UI.BTN_APPLY, false);
      this.SetActive((Enum) ClanDetail.UI.BTN_APPLY_CANCEL, true);
    }
    else
    {
      Enum ctrl_enum1 = (Enum) ClanDetail.UI.BTN_APPLY;
      Enum ctrl_enum2 = (Enum) ClanDetail.UI.BTN_INVITE;
      if (this.isInvited)
      {
        ctrl_enum1 = (Enum) ClanDetail.UI.BTN_INVITE;
        ctrl_enum2 = (Enum) ClanDetail.UI.BTN_APPLY;
      }
      if (this.members.Count < MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.CLAN_MAX_MEMBER_NUM)
      {
        this.SetActive(ctrl_enum1, true);
        this.SetActive(ctrl_enum2, false);
      }
      else
        this.SetActive((Enum) ClanDetail.UI.BTN_APPLY, false);
      this.SetActive((Enum) ClanDetail.UI.BTN_APPLY_CANCEL, false);
    }
  }

  protected virtual void UpdateDynamicList()
  {
    FriendCharaInfo[] currentUserArray = this.GetCurrentUserArray();
    int pageItemLength = this.GetPageItemLength(this.nowPage);
    int num = this.nowPage * 10;
    FriendCharaInfo[] info = new FriendCharaInfo[pageItemLength];
    for (int index = 0; index < pageItemLength; ++index)
      info[index] = currentUserArray[num + index];
    this.SetDynamicList((Enum) ClanDetail.UI.GRD_LIST, "ClanMemberListItem", pageItemLength, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) => this.SetupListItem(info[i], i, t, is_recycle)));
  }

  protected virtual FriendCharaInfo[] GetCurrentUserArray()
  {
    return this.members == null ? (FriendCharaInfo[]) null : this.members.ToArray();
  }

  private int GetPageItemLength(int currentPage)
  {
    return currentPage + 1 < this.pageNumMax || this.members.Count % 10 <= 0 ? 10 : this.members.Count % 10;
  }

  private void SetupListItem(FriendCharaInfo data, int i, Transform t, bool is_recycle)
  {
    this.SetEvent(t, "DETAIL", i);
    this.SetMemberInfo(data, i, t);
    if (!(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene"))
      return;
    this.SetButtonColliderEnabled(t, false);
  }

  private void SetMemberInfo(FriendCharaInfo data, int i, Transform t)
  {
    FriendCharaInfo friendCharaInfo = data;
    MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(i + 4).SetEquipSet(friendCharaInfo.equipSet);
    this.SetRenderPlayerModel(t, (Enum) ClanDetail.UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo((CharaInfo) friendCharaInfo, false, true, false, true), 99, new Vector3(0.0f, -1.536f, 1.87f), new Vector3(0.0f, 154f, 0.0f), true);
    this.SetLabelText(t, (Enum) ClanDetail.UI.LBL_NAME, friendCharaInfo.name);
    this.SetLabelText(t, (Enum) ClanDetail.UI.LBL_LEVEL, friendCharaInfo.level.ToString());
    this.SetFollowStatus(t, friendCharaInfo.userId, friendCharaInfo.following, friendCharaInfo.follower);
    this.SetJoinInfo(t, i, friendCharaInfo.userId, friendCharaInfo.joinStatus, "");
    this.SetStatusSprite(t, friendCharaInfo.userClanData);
    this.SetBtnMemberSetting(t, i, friendCharaInfo);
    ((Component) this.FindCtrl(t, (Enum) ClanDetail.UI.OBJ_DEGREE_FRAME_ROOT)).GetComponent<DegreePlate>().Initialize(friendCharaInfo.selectedDegrees, false, (Action<DegreePlate>) (x => ((Component) this.GetCtrl((Enum) ClanDetail.UI.GRD_LIST)).GetComponent<UIGrid>().Reposition()));
  }

  private void SetStatusSprite(Transform root, UserClanData userClan)
  {
    if (userClan.IsLeader())
    {
      this.SetActive(root, (Enum) ClanDetail.UI.SPR_STATUS, true);
      this.SetSprite(root, (Enum) ClanDetail.UI.SPR_STATUS, "Clan_HeadmasterIcon");
    }
    else if (userClan.IsSubLeader())
    {
      this.SetActive(root, (Enum) ClanDetail.UI.SPR_STATUS, true);
      this.SetSprite(root, (Enum) ClanDetail.UI.SPR_STATUS, "Clan_DeputyHeadmasterIcon");
    }
    else
      this.SetActive(root, (Enum) ClanDetail.UI.SPR_STATUS, false);
  }

  private void SetBtnMemberSetting(Transform root, int index, FriendCharaInfo userInfo)
  {
    if ((!(userInfo.userClanData.cId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId) ? 0 : (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsLeader() ? 1 : 0)) == 0)
    {
      this.SetActive(root, (Enum) ClanDetail.UI.BTN_MEMBER_SETTING, false);
    }
    else
    {
      this.SetActive(root, (Enum) ClanDetail.UI.BTN_MEMBER_SETTING, !userInfo.userClanData.IsLeader());
      this.SetEvent(root, (Enum) ClanDetail.UI.BTN_MEMBER_SETTING, "MEMBER_SETTING", index);
    }
  }

  private void SetFollowStatus(Transform t, int user_id, bool following, bool follower)
  {
    bool is_visible1 = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(user_id);
    bool is_visible2 = false;
    if (MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
      is_visible2 = this.clanData.cId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId;
    bool is_visible3 = !is_visible1 && following | follower;
    this.SetActive(t, (Enum) ClanDetail.UI.SPR_BLACKLIST_ICON, is_visible1);
    this.SetActive(t, (Enum) ClanDetail.UI.OBJ_FOLLOW, is_visible3);
    this.SetActive(t, (Enum) ClanDetail.UI.SPR_FOLLOW, is_visible3 & following);
    this.SetActive(t, (Enum) ClanDetail.UI.SPR_FOLLOWER, is_visible3 & follower);
    this.SetActive(t, (Enum) ClanDetail.UI.SPR_SAME_CLAN_ICON, is_visible2);
    this.SetActive(t, (Enum) ClanDetail.UI.OBJ_FOLLOW, following | follower);
    this.SetActive(t, (Enum) ClanDetail.UI.SPR_FOLLOW, following);
    this.SetActive(t, (Enum) ClanDetail.UI.SPR_FOLLOWER, follower);
    UIGrid component = this.GetComponent<UIGrid>(t, (Enum) ClanDetail.UI.GRD_FOLLOW_ARROW);
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Reposition();
  }

  protected virtual void SetJoinInfo(
    Transform t,
    int _itemIndex,
    int userId,
    FriendCharaInfo.JoinInfo _joinStatus,
    string _lastLoginText)
  {
    if (Object.op_Equality((Object) t, (Object) null))
      return;
    if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == userId)
      this.SetActive(t, (Enum) ClanDetail.UI.JOIN_STATUS_ROOT, false);
    else if (_joinStatus == null)
    {
      this.SetActive(t, (Enum) ClanDetail.UI.JOIN_STATUS_ROOT, false);
    }
    else
    {
      this.SetActive(t, (Enum) ClanDetail.UI.JOIN_STATUS_ROOT, true);
      this.SetJoinTextInfo(t, _joinStatus);
      this.SetJoinButtonSettings(t, (ONLINE_STATUS) _joinStatus.joinType, _itemIndex);
      this.SetEvent(t, (Enum) ClanDetail.UI.BTN_JOIN_BUTTON, "JOIN_FRIEND", _itemIndex);
    }
  }

  private void SetJoinTextInfo(Transform _target, FriendCharaInfo.JoinInfo _joinStatus)
  {
    UILabel component1 = ((Component) this.FindCtrl(_target, (Enum) ClanDetail.UI.ONLINE_TEXT)).GetComponent<UILabel>();
    UILabel component2 = ((Component) this.FindCtrl(_target, (Enum) ClanDetail.UI.DETAIL_TEXT)).GetComponent<UILabel>();
    if (Object.op_Inequality((Object) component1, (Object) null))
      component1.text = StringTable.Get(STRING_CATEGORY.FRIEND_JOIN, (uint) _joinStatus.joinType);
    if (Object.op_Inequality((Object) component2, (Object) null))
      component2.text = "";
    switch (_joinStatus.joinType)
    {
      case 1:
        if (!Object.op_Inequality((Object) component1, (Object) null))
          break;
        component1.color = ClanDetail.TEXT_BASE_COLOR_ONLINE;
        component1.effectColor = ClanDetail.TEXT_OUTLINE_COLOR_ONLINE;
        break;
      case 2:
        if (!Object.op_Inequality((Object) component1, (Object) null))
          break;
        component1.color = ClanDetail.TEXT_BASE_COLOR_GO_LOUNGE;
        component1.effectColor = ClanDetail.TEXT_OUTLINE_COLOR_GO_LOUNGE;
        break;
      case 3:
        if (Object.op_Inequality((Object) component1, (Object) null))
        {
          component1.color = ClanDetail.TEXT_BASE_COLOR_GO_QUEST;
          component1.effectColor = ClanDetail.TEXT_OUTLINE_COLOR_GO_QUEST;
        }
        if (!Object.op_Inequality((Object) component2, (Object) null))
          break;
        component2.text = this.GetQuestName(_joinStatus.targetParam);
        break;
      case 4:
        if (Object.op_Inequality((Object) component1, (Object) null))
        {
          component1.color = ClanDetail.TEXT_BASE_COLOR_GO_FIELD;
          component1.effectColor = ClanDetail.TEXT_OUTLINE_COLOR_GO_FIELD;
        }
        if (!Object.op_Inequality((Object) component2, (Object) null))
          break;
        component2.text = this.GetMapFieldNameText(_joinStatus.targetParam);
        break;
      default:
        if (!Object.op_Inequality((Object) component1, (Object) null))
          break;
        component1.color = ClanDetail.TEXT_BASE_COLOR_DEFAULT;
        component1.effectColor = ClanDetail.TEXT_OUTLINE_COLOR_DEFAULT;
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

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST) != (GameSection.NOTIFY_FLAG) 0)
      this.SetDirtyTable();
    base.OnNotify(flags);
  }

  protected void SetJoinButtonSettings(Transform _t, ONLINE_STATUS _status, int _itemIndex)
  {
    if (Object.op_Equality((Object) _t, (Object) null))
      return;
    UIButton component = ((Component) this.FindCtrl(_t, (Enum) ClanDetail.UI.BTN_JOIN_BUTTON)).GetComponent<UIButton>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    if (!MonoBehaviourSingleton<GameSceneManager>.I.IsCurrentSceneMejorOutGameScene())
    {
      ((Component) component).gameObject.SetActive(false);
    }
    else
    {
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
  }

  protected void SetDirtyTable() => this.SetDirty((Enum) ClanDetail.UI.GRD_LIST);

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST;
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

  private void OnQuery_APPLY()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<ClanMatchingManager>.I.RequestUserDetail(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, (Action<UserClanData>) (userClanData =>
    {
      if (userClanData.IsNotRegistered())
      {
        GameSection.ResumeEvent(true);
        GameSection.SetEventData((object) new string[1]
        {
          this.clanData.name
        });
      }
      else
      {
        GameSection.ResumeEvent(false);
        MonoBehaviourSingleton<UserInfoManager>.I.SetUserClan(userClanData);
        MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.GetErrorCodeText(42007U)), (Action<string>) (ret => this.RefreshUI()));
      }
    }));
  }

  private void OnQuery_ClanApplyConfirmDialog_YES()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<ClanMatchingManager>.I.RequestApply(new ClanApplyModel.RequestSendForm()
    {
      cId = this.clanData.cId
    }, (Action<bool>) (isSuccess =>
    {
      if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsNotRegistered())
      {
        this.isRequested = true;
        this.RefreshUI();
        GameSection.ChangeStayEvent("APPLIED", (object) new string[1]
        {
          this.clanData.name
        });
        GameSection.ResumeEvent(isSuccess);
      }
      else
      {
        GameSection.ChangeStayEvent("REGISTERED", (object) new string[1]
        {
          this.clanData.name
        });
        GameSection.ResumeEvent(isSuccess);
      }
    }));
  }

  private void OnQuery_APPLY_CANCEL()
  {
    GameSection.SetEventData((object) new string[1]
    {
      this.clanData.name
    });
  }

  private void OnQuery_ClanApplyCancelConfirmDialog_YES()
  {
    GameSection.SetEventData((object) new string[1]
    {
      this.clanData.name
    });
    GameSection.StayEvent();
    MonoBehaviourSingleton<ClanMatchingManager>.I.RequestApplyCancel(this.clanData.cId, (Action<bool>) (isSuccess =>
    {
      if (isSuccess)
      {
        this.isRequested = false;
        this.RefreshUI();
      }
      GameSection.ResumeEvent(isSuccess);
    }));
  }

  private void OnQuery_LEAVE()
  {
    GameSection.SetEventData(this.members.Count <= 1 ? (object) string.Format(StringTable.Get(STRING_CATEGORY.CLAN_LEAVE, 1U), (object) this.clanData.name) : (object) string.Format(StringTable.Get(STRING_CATEGORY.CLAN_LEAVE, 0U), (object) this.clanData.name));
  }

  private void OnQuery_ClanLeaveConfirmDialog_YES()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<ClanMatchingManager>.I.RequestLeave((Action<bool>) (isSuccess =>
    {
      GameSection.ResumeEvent(isSuccess);
      if (!isSuccess)
        return;
      MonoBehaviourSingleton<UserInfoManager>.I.LeaveClan();
      MonoBehaviourSingleton<ClanMatchingManager>.I.ClearClanData();
    }));
  }

  private void OnQuery_ClanLeaveCompleteDialog_OK()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
    {
      new EventData(!MonoBehaviourSingleton<ClanManager>.IsValid() ? GameSection.GetGoingHomeEvent() : "MAIN_MENU_HOME", (object) null)
    });
  }

  private void OnQuery_DETAIL()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
    {
      GameSection.StopEvent();
    }
    else
    {
      int eventData = (int) GameSection.GetEventData();
      FriendCharaInfo member = this.members[10 * this.nowPage + eventData];
      MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = eventData + 4;
      if (member.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
        GameSection.StopEvent();
      GameSection.SetEventData((object) member);
    }
  }

  private void OnQuery_MEMBER_SETTING()
  {
    GameSection.SetEventData((object) this.members[10 * this.nowPage + (int) GameSection.GetEventData()]);
  }

  protected virtual void OnQuery_JOIN_FRIEND()
  {
    FriendCharaInfo.JoinInfo joinStatus = this.members[10 * this.nowPage + (int) GameSection.GetEventData()].joinStatus;
    if (joinStatus == null)
      return;
    GameSection.StayEvent();
    switch (joinStatus.joinType)
    {
      case 2:
        this.JoinLounge(joinStatus);
        break;
      case 3:
        if (!MonoBehaviourSingleton<PartyManager>.IsValid())
          break;
        MonoBehaviourSingleton<PartyManager>.I.SendApply(joinStatus.conditionParam, (Action<bool, Error>) ((isSucceed, error) =>
        {
          if (isSucceed)
            MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("QuestAccept", "QuestAcceptRoom");
          GameSection.ResumeEvent(true);
        }), joinStatus.targetParam);
        break;
      case 4:
        int _toUserId = int.Parse(joinStatus.conditionParam);
        this.JoinField(joinStatus.targetParam, _toUserId, (Action<bool, bool, bool>) ((is_matching, is_connect, is_regist) =>
        {
          if (!is_matching)
            GameSection.StopEvent();
          else if (!is_connect)
          {
            GameSection.StopEvent();
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
      default:
        GameSection.ResumeEvent(true);
        break;
    }
  }

  protected void OnQuery_PAGE_PREV()
  {
    this.nowPage = (this.nowPage - 1 + this.pageNumMax) % this.pageNumMax;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(this.GetUpdateUINotifyFlags());
  }

  protected void OnQuery_PAGE_NEXT()
  {
    this.nowPage = (this.nowPage + 1) % this.pageNumMax;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(this.GetUpdateUINotifyFlags());
  }

  protected void OnQuery_SCOUT_APPLY()
  {
    GameSection.SetEventData((object) new string[1]
    {
      this.clanData.name
    });
  }

  protected void OnQuery_ClanScoutApplyConfirmDialog_YES()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<ClanMatchingManager>.I.SendClanAcceptInvite(int.Parse(this.clanData.cId), (Action<bool>) (isSuccess =>
    {
      if (isSuccess)
      {
        GameSection.ChangeStayEvent("REGISTERED", (object) new string[1]
        {
          this.clanData.name
        });
        GameSection.ResumeEvent(true);
      }
      else
      {
        MonoBehaviourSingleton<ClanMatchingManager>.I.RemoveClanScoutList(this.clanData.cId);
        GameSection.ChangeStayEvent("ERROR");
        GameSection.ResumeEvent(true);
      }
    }));
  }

  private void OnCloseDialog_ClanMemberStatusChangeConfirmDialog()
  {
    this.StartCoroutine(this.Reload((System.Action) (() => { })));
  }

  private void OnCloseDialog_ClanKickConfirmDialog()
  {
    this.StartCoroutine(this.Reload((System.Action) (() => { })));
  }

  private enum UI
  {
    LBL_CLAN_NAME,
    LBL_CLAN_LEVEL,
    LBL_APLLY_TYPE,
    LBL_MODE,
    LBL_COMMENT,
    OBJ_STAMP,
    BTN_STAMP,
    LBL_MEMBER_NUMBER_NOW,
    LBL_MEMBER_NUMBER_MAX,
    GRD_LIST,
    TEX_MODEL,
    LBL_LEVEL,
    LBL_NAME,
    OBJ_FOLLOW,
    GRD_FOLLOW_ARROW,
    SPR_FOLLOW,
    SPR_FOLLOWER,
    SPR_BLACKLIST_ICON,
    SPR_SAME_CLAN_ICON,
    SPR_STATUS,
    JOIN_STATUS_ROOT,
    ONLINE_TEXT_ROOT,
    ONLINE_TEXT,
    DETAIL_TEXT,
    JOIN_BUTTON_ROOT,
    BTN_JOIN_BUTTON,
    LBL_BUTTON_TEXT,
    LBL_PLAYING_FIELD,
    LBL_AREA_NAME,
    LBL_PLAYING_QUEST,
    LBL_PLAYING_READY,
    LBL_QUEST_NAME,
    LBL_IN_LOUNGE,
    LBL_PLAYING_ARENA,
    BTN_MEMBER_SETTING,
    LBL_NON_LIST,
    STR_LIST_NUM,
    OBJ_DEGREE_FRAME_ROOT,
    BTN_LEAVE,
    BTN_APPLY,
    BTN_APPLY_CANCEL,
    LBL_NOW,
    LBL_MAX,
    OBJ_ACTIVE_ROOT,
    OBJ_INACTIVE_ROOT,
    BTN_INVITE,
    OBJ_SYMBOL,
  }
}
