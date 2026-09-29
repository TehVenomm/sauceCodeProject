// Decompiled with JetBrains decompiler
// Type: GuildMemberList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class GuildMemberList : GameSection
{
  protected List<FriendCharaInfo> members;
  protected List<FriendCharaInfo> requestMembers;
  protected List<FriendCharaInfo> allMember;
  protected FriendCharaInfo memberInfoData;
  protected List<int> status;

  public virtual string ListItemEvent => "GUILD_INFO";

  public override void Initialize()
  {
    this.SetActive((Enum) GuildMemberList.UI.OBJ_GUILD_NUMBER_ROOT, false);
    this.StartCoroutine(this.DoInitialize());
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    MonoBehaviourSingleton<ChatManager>.IsValid();
  }

  private IEnumerator DoInitialize()
  {
    bool is_finish = false;
    this.GetListItem((Action<bool, object>) ((succes, obj) => is_finish = true));
    while (!is_finish)
      yield return (object) null;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) GuildMemberList.UI.OBJ_GUILD_NUMBER_ROOT, true);
    this.SetLabelText((Enum) GuildMemberList.UI.LBL_GUILD_NUMBER_MAX, (object) this.allMember.Count);
    if (this.allMember == null || this.allMember.Count == 0)
    {
      this.SetActive((Enum) GuildMemberList.UI.STR_NON_LIST, true);
      this.SetActive((Enum) GuildMemberList.UI.GRD_LIST, false);
    }
    else
    {
      int online_count = 0;
      this.SetDynamicList((Enum) GuildMemberList.UI.GRD_LIST, "GuildMemberListItem", this.allMember.Count, true, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        FriendCharaInfo member = this.allMember[i];
        bool is_visible = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(member.userId);
        this.SetActive(t, (Enum) GuildMemberList.UI.SPR_BLACKLIST_ICON, is_visible);
        this.SetActive(t, (Enum) GuildMemberList.UI.SPR_FOLLOW, !is_visible && member.following);
        this.SetActive(t, (Enum) GuildMemberList.UI.SPR_FOLLOWER, !is_visible && member.follower);
        this.SetActive(t, (Enum) GuildMemberList.UI.SPR_ICON_FIRST_MET, false);
        this.SetCharaInfo(member, i, t, is_recycle, member.userId == 0);
        bool flag = this.status.Any<int>((Func<int, bool>) (st => st == member.userId));
        if (flag)
          ++online_count;
        if (this.requestMembers.Contains(member))
          this.SetActive(t, (Enum) GuildMemberList.UI.OBJ_OFFLINE_MASK, false);
        else
          this.SetActive(t, (Enum) GuildMemberList.UI.OBJ_OFFLINE_MASK, !flag);
        this.SetActive(t, (Enum) GuildMemberList.UI.OBJ_REQUEST_PENDING, this.requestMembers.Contains(member));
        this.SetListItem(i, t, this.ListItemEvent, member);
      }));
      this.SetLabelText((Enum) GuildMemberList.UI.LBL_GUILD_NUMBER_NOW, online_count.ToString());
      this.SetActive((Enum) GuildMemberList.UI.STR_NON_LIST, false);
      this.SetActive((Enum) GuildMemberList.UI.GRD_LIST, true);
    }
  }

  protected virtual void GetListItem(Action<bool, object> callback)
  {
    MonoBehaviourSingleton<GuildManager>.I.SendMemberList(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, (Action<bool, GuildMemberListModel>) ((success, ret) =>
    {
      this.allMember = new List<FriendCharaInfo>();
      this.members = new List<FriendCharaInfo>((IEnumerable<FriendCharaInfo>) ret.result.list);
      this.members.Remove(this.members.FirstOrDefault<FriendCharaInfo>((Func<FriendCharaInfo, bool>) (o => o.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)));
      this.requestMembers = new List<FriendCharaInfo>((IEnumerable<FriendCharaInfo>) ret.result.requesters);
      this.requestMembers.Remove(this.requestMembers.FirstOrDefault<FriendCharaInfo>((Func<FriendCharaInfo, bool>) (o => o.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)));
      this.allMember.AddRange((IEnumerable<FriendCharaInfo>) this.requestMembers);
      this.allMember.AddRange((IEnumerable<FriendCharaInfo>) this.members);
      MonoBehaviourSingleton<GuildManager>.I.SendClanChatOnlineStatus((Action<bool, List<GuildMemberChatStatus>>) ((is_success, list) =>
      {
        this.status = list.Select<GuildMemberChatStatus, int>((Func<GuildMemberChatStatus, int>) (o => o.id)).ToList<int>();
        callback(is_success, (object) null);
      }));
    }));
  }

  protected virtual void SetListItem(
    int i,
    Transform t,
    string event_name,
    FriendCharaInfo member)
  {
    this.SetEvent(t, event_name, (object) member);
  }

  protected void SetCharaInfo(
    FriendCharaInfo data,
    int i,
    Transform t,
    bool is_recycle,
    bool isGM)
  {
    if (isGM)
      this.SetRenderNPCModel(t, (Enum) GuildMemberList.UI.TEX_MODEL, 0, new Vector3(0.0f, -1.49f, 1.87f), new Vector3(0.0f, 154f, 0.0f), 10f);
    else
      this.SetRenderPlayerModel(t, (Enum) GuildMemberList.UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo((CharaInfo) data, false, true, false, true), 99, new Vector3(0.0f, -1.536f, 1.87f), new Vector3(0.0f, 154f, 0.0f), true);
    this.SetLabelText(t, (Enum) GuildMemberList.UI.LBL_NAME, data.name);
    this.SetLabelText(t, (Enum) GuildMemberList.UI.LBL_LEVEL, data.level.ToString());
    this.SetLabelText(t, (Enum) GuildMemberList.UI.LBL_COMMENT, data.comment);
    this.SetLabelText(t, (Enum) GuildMemberList.UI.LBL_LAST_LOGIN, this.sectionData.GetText("LAST_LOGIN"));
    this.SetLabelText(t, (Enum) GuildMemberList.UI.LBL_LAST_LOGIN_TIME, data.lastLogin);
    EquipSetCalculator equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(i + 4);
    equipSetCalculator.SetEquipSet(data.equipSet);
    SimpleStatus finalStatus = equipSetCalculator.GetFinalStatus(0, (int) data.hp, (int) data.atk, (int) data.def);
    this.SetLabelText(t, (Enum) GuildMemberList.UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
    this.SetLabelText(t, (Enum) GuildMemberList.UI.LBL_DEF, finalStatus.defences[0].ToString());
    this.SetLabelText(t, (Enum) GuildMemberList.UI.LBL_HP, finalStatus.hp.ToString());
    ((Component) this.FindCtrl(t, (Enum) GuildMemberList.UI.OBJ_DEGREE_FRAME_ROOT)).GetComponent<DegreePlate>().Initialize(data.selectedDegrees, false, (Action<DegreePlate>) (x => ((Component) this.GetCtrl((Enum) GuildMemberList.UI.GRD_LIST)).GetComponent<UIGrid>().Reposition()));
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_GUILD_LIST) != (GameSection.NOTIFY_FLAG) 0)
      this.RefreshUI();
    base.OnNotify(flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_GUILD_LIST;
  }

  private void OnJoinClanChat(CHAT_ERROR_TYPE errorType, string userId)
  {
    int num = int.Parse(userId);
    if (this.status.Contains(num))
      return;
    this.status.Add(num);
    this.RefreshUI();
  }

  private void OnLeaveClanChat(CHAT_ERROR_TYPE errorType, string userId)
  {
    int num = int.Parse(userId);
    if (!this.status.Contains(num))
      return;
    this.status.Remove(num);
    this.RefreshUI();
  }

  protected void OnQuery_GUILD_INFO()
  {
    this.memberInfoData = (FriendCharaInfo) GameSection.GetEventData();
    this.SetButton();
    GameObject sender = GameSceneEvent.current.sender;
    this.FindCtrl(this._transform, (Enum) GuildMemberList.UI.SPR_GUILD_INFO).SetParent(this.FindCtrl(sender.transform, (Enum) GuildMemberList.UI.OBJ_GUILD_INFO));
    this.FindCtrl(this._transform, (Enum) GuildMemberList.UI.SPR_GUILD_INFO).localPosition = Vector3.zero;
    this.FindCtrl(this._transform, (Enum) GuildMemberList.UI.SPR_GUILD_INFO).localScale = Vector3.one;
    this.SetActive(this._transform, (Enum) GuildMemberList.UI.SPR_GUILD_INFO, !((Component) this.FindCtrl(this._transform, (Enum) GuildMemberList.UI.SPR_GUILD_INFO)).gameObject.activeSelf);
  }

  private void SetButton()
  {
    bool is_visible = this.requestMembers != null && this.requestMembers.Count > 0 && this.memberInfoData.requestId != 0;
    Transform ctrl = this.FindCtrl(this._transform, (Enum) GuildMemberList.UI.SPR_GUILD_INFO);
    this.SetActive(ctrl, (Enum) GuildMemberList.UI.BTN_ACCEPT, is_visible);
    this.SetActive(ctrl, (Enum) GuildMemberList.UI.BTN_REJECT, is_visible);
    this.SetActive(ctrl, (Enum) GuildMemberList.UI.BTN_BAN, !is_visible && MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
    this.SetActive(ctrl, (Enum) GuildMemberList.UI.BTN_MESSAGE, !is_visible);
  }

  protected void OnQuery_MEMBER_PROFILE()
  {
    GameSection.SetEventData((object) this.memberInfoData);
    this.SetActive(this._transform, (Enum) GuildMemberList.UI.SPR_GUILD_INFO, false);
  }

  protected void OnQuery_MEMBER_MESSAGE()
  {
    MonoBehaviourSingleton<GuildManager>.I.SetTalkUser(this.memberInfoData);
    this.SetActive(this._transform, (Enum) GuildMemberList.UI.SPR_GUILD_INFO, false);
  }

  private void OnQuery_MEMBER_BAN()
  {
    GameSection.SetEventData((object) this.memberInfoData);
    this.SetActive(this._transform, (Enum) GuildMemberList.UI.SPR_GUILD_INFO, false);
  }

  private void OnQuery_MEMBER_ACCEPT()
  {
    this.SetActive(this._transform, (Enum) GuildMemberList.UI.SPR_GUILD_INFO, false);
    GameSection.StayEvent();
    if (this.memberInfoData == null)
      return;
    MonoBehaviourSingleton<GuildManager>.I.SendAdminJoin(this.memberInfoData.requestId, 1, (Action<bool, Error>) ((is_success, err) => MonoBehaviourSingleton<GuildManager>.I.SendMemberList(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, (Action<bool, GuildMemberListModel>) ((success, ret) => GameSection.ResumeEvent(is_success)))));
  }

  private void OnQuery_MEMBER_REJECT()
  {
    this.SetActive(this._transform, (Enum) GuildMemberList.UI.SPR_GUILD_INFO, false);
    GameSection.StayEvent();
    if (this.memberInfoData == null)
      return;
    MonoBehaviourSingleton<GuildManager>.I.SendAdminJoin(this.memberInfoData.requestId, 0, (Action<bool, Error>) ((is_success, err) => MonoBehaviourSingleton<GuildManager>.I.SendMemberList(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, (Action<bool, GuildMemberListModel>) ((success, ret) => GameSection.ResumeEvent(is_success)))));
  }

  private void OnQuery_GuildKickMember_YES()
  {
    GameSection.SetEventData((object) this.memberInfoData);
  }

  private void OnQuery_BanReasonConfirm_YES()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendKick(this.memberInfoData.userId, (Action<bool, Error>) ((is_success, err) => GameSection.ResumeEvent(is_success)));
  }

  private void OnQuery_BanReasonConfirm_NO()
  {
  }

  protected enum UI
  {
    OBJ_GUILD_NUMBER_ROOT,
    LBL_GUILD_NUMBER_NOW,
    LBL_GUILD_NUMBER_MAX,
    GRD_LIST,
    TEX_MODEL,
    STR_NON_LIST,
    SPR_FOLLOW,
    SPR_FOLLOWER,
    SPR_BLACKLIST_ICON,
    OBJ_COMMENT,
    LBL_COMMENT,
    LBL_LAST_LOGIN,
    LBL_LAST_LOGIN_TIME,
    LBL_NAME,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    LBL_LEVEL,
    OBJ_DEGREE_FRAME_ROOT,
    SPR_ICON_FIRST_MET,
    OBJ_OFFLINE_MASK,
    OBJ_REQUEST_PENDING,
    OBJ_GUILD_INFO,
    SPR_GUILD_INFO,
    BTN_ACCEPT,
    BTN_REJECT,
    BTN_MESSAGE,
    BTN_BAN,
  }
}
