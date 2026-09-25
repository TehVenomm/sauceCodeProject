// Decompiled with JetBrains decompiler
// Type: GuildInformationStep3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GuildInformationStep3 : UserListBase<FriendCharaInfo>
{
  private List<int> mInviteFriendIDs = new List<int>();

  public override void Initialize() => base.Initialize();

  public virtual void ListUI()
  {
    FriendCharaInfo[] friendCharaInfoArray = (FriendCharaInfo[]) null;
    if (this.recvList != null && this.recvList.Count > 0)
      friendCharaInfoArray = this.recvList.ToArray();
    if (friendCharaInfoArray == null || friendCharaInfoArray.Length == 0)
    {
      this.SetActive((Enum) GuildInformationStep3.UI.STR_NON_LIST, true);
      this.SetActive((Enum) GuildInformationStep3.UI.GRD_LIST, false);
    }
    else
    {
      this.SetActive((Enum) GuildInformationStep3.UI.STR_NON_LIST, false);
      this.SetActive((Enum) GuildInformationStep3.UI.GRD_LIST, true);
      this.UpdateDynamicList();
    }
  }

  protected virtual void UpdateDynamicList()
  {
    FriendCharaInfo[] info = (FriendCharaInfo[]) null;
    int item_num = 0;
    if (this.recvList != null && this.recvList.Count > 0)
    {
      info = this.recvList.ToArray();
      if (info != null)
        item_num = info.Length;
    }
    this.SetDynamicList((Enum) GuildInformationStep3.UI.GRD_LIST, this.GetListItemName, item_num, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) => this.SetListItem(i, t, is_recycle, info[i])));
  }

  protected virtual void SetListItem(int i, Transform t, bool is_recycle, FriendCharaInfo data)
  {
    this.SetFollowStatus(t, data.userId, data.following, data.follower);
    this.SetCharaInfo(data, i, t, is_recycle, data.userId == 0);
  }

  protected void SetCharaInfo(
    FriendCharaInfo data,
    int i,
    Transform t,
    bool is_recycle,
    bool isGM)
  {
    object[] event_data = new object[2]
    {
      (object) data.userId,
      (object) t
    };
    this.SetEvent(t, "SELECT_FRIEND", (object) event_data);
    if (isGM)
      this.SetRenderNPCModel(t, (Enum) GuildInformationStep3.UI.TEX_MODEL, 0, new Vector3(0.0f, -1.49f, 1.87f), new Vector3(0.0f, 154f, 0.0f), 10f);
    else
      this.SetRenderPlayerModel(t, (Enum) GuildInformationStep3.UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo((CharaInfo) data, false, true, false, true), 99, new Vector3(0.0f, -1.536f, 1.87f), new Vector3(0.0f, 154f, 0.0f), true);
    this.SetLabelText(t, (Enum) GuildInformationStep3.UI.LBL_NAME, data.name);
    this.SetLabelText(t, (Enum) GuildInformationStep3.UI.LBL_LEVEL, data.level.ToString());
    this.SetLabelText(t, (Enum) GuildInformationStep3.UI.LBL_COMMENT, data.comment);
    this.SetLabelText(t, (Enum) GuildInformationStep3.UI.LBL_LAST_LOGIN, this.sectionData.GetText("LAST_LOGIN"));
    this.SetLabelText(t, (Enum) GuildInformationStep3.UI.LBL_LAST_LOGIN_TIME, data.lastLogin);
    EquipSetCalculator equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(i + 4);
    equipSetCalculator.SetEquipSet(data.equipSet);
    SimpleStatus finalStatus = equipSetCalculator.GetFinalStatus(0, (int) data.hp, (int) data.atk, (int) data.def);
    this.SetLabelText(t, (Enum) GuildInformationStep3.UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
    this.SetLabelText(t, (Enum) GuildInformationStep3.UI.LBL_DEF, finalStatus.defences[0].ToString());
    this.SetLabelText(t, (Enum) GuildInformationStep3.UI.LBL_HP, finalStatus.hp.ToString());
    if ((int) data.level < 15)
    {
      this.SetActive(t, (Enum) GuildInformationStep3.UI.OBJ_DISABLE_USER_MASK, true);
      this.SetActive(t, (Enum) GuildInformationStep3.UI.SPR_SELECTED, false);
      this.SetActive(t, (Enum) GuildInformationStep3.UI.STR_LIMIT_LVL, true);
      this.SetEvent(t, "_", (object) null);
    }
    else if (this.mInviteFriendIDs.Contains(data.userId))
    {
      this.SetActive(t, (Enum) GuildInformationStep3.UI.OBJ_DISABLE_USER_MASK, true);
      this.SetActive(t, (Enum) GuildInformationStep3.UI.SPR_SELECTED, true);
      this.SetActive(t, (Enum) GuildInformationStep3.UI.STR_LIMIT_LVL, false);
    }
    else
    {
      this.SetActive(t, (Enum) GuildInformationStep3.UI.OBJ_DISABLE_USER_MASK, false);
      this.SetActive(t, (Enum) GuildInformationStep3.UI.SPR_SELECTED, false);
      this.SetActive(t, (Enum) GuildInformationStep3.UI.STR_LIMIT_LVL, false);
    }
  }

  protected void SetFollowStatus(Transform t, int user_id, bool following, bool follower)
  {
    bool is_visible = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(user_id);
    this.SetActive(t, (Enum) GuildInformationStep3.UI.SPR_BLACKLIST_ICON, is_visible);
    this.SetActive(t, (Enum) GuildInformationStep3.UI.SPR_FOLLOW, !is_visible & following);
    this.SetActive(t, (Enum) GuildInformationStep3.UI.SPR_FOLLOWER, !is_visible & follower);
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
        aId = chara_info.aId,
        hId = chara_info.hId,
        rId = chara_info.rId,
        lId = chara_info.lId,
        showHelm = chara_info.showHelm,
        equipSet = chara_info.equipSet,
        following = chara_info.following,
        follower = chara_info.follower,
        selectedDegrees = chara_info.selectedDegrees
      });
    }));
    return new_list;
  }

  public virtual void OnQuery_SELECT_FRIEND()
  {
    object[] eventData = (object[]) GameSection.GetEventData();
    if (eventData == null || eventData.Length < 2)
      return;
    int num = (int) eventData[0];
    if (!this.mInviteFriendIDs.Contains(num))
      this.mInviteFriendIDs.Add(num);
    Transform root = eventData[1] as Transform;
    this.SetActive(root, (Enum) GuildInformationStep3.UI.OBJ_DISABLE_USER_MASK, true);
    this.SetActive(root, (Enum) GuildInformationStep3.UI.SPR_SELECTED, true);
    this.SetActive(root, (Enum) GuildInformationStep3.UI.STR_LIMIT_LVL, false);
  }

  protected void SetDirtyTable() => this.SetDirty((Enum) GuildInformationStep3.UI.GRD_LIST);

  protected virtual string GetListItemName => "GuildListBaseItem";

  public override void UpdateUI() => this.ListUI();

  protected override void SendGetList(int page, Action<bool> callback)
  {
    MonoBehaviourSingleton<FriendManager>.I.SendGetFollowList(page, (Action<bool, FriendFollowListModel.Param>) ((is_success, recv_data) =>
    {
      if (is_success)
      {
        this.recvList = recv_data.follow;
        this.nowPage = page;
        this.pageNumMax = recv_data.pageNumMax;
      }
      if (callback == null)
        return;
      callback(is_success);
    }));
  }

  protected override void PostSendGetListByReopen(int page)
  {
    this.SetDirtyTable();
    base.PostSendGetListByReopen(page);
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM) != (GameSection.NOTIFY_FLAG) 0)
      this.isInitializeSendReopen = true;
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST) != (GameSection.NOTIFY_FLAG) 0)
      this.SetDirtyTable();
    base.OnNotify(flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST;
  }

  private void OnQuery_CREATE()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendCreate(this.mInviteFriendIDs, (Action<bool, Error>) ((is_success, err) => this.DoWaitProtocolBusyFinish((System.Action) (() =>
    {
      GameSection.ResumeEvent(true);
      if (!is_success)
        return;
      MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (GuildInformationStep3), ((Component) this).gameObject, "STORY", (object) new object[3]
      {
        (object) 80000001,
        (object) 0,
        (object) 0
      });
    }))));
  }

  private void OnQuery_CLOSE()
  {
    MonoBehaviourSingleton<GuildManager>.I.ClearCreateGuildRequestParam();
  }

  protected enum UI
  {
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
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    LBL_LEVEL,
    LBL_NAME,
    OBJ_DISABLE_USER_MASK,
    SPR_SELECTED,
    STR_LIMIT_LVL,
  }
}
