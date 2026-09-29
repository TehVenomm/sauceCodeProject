// Decompiled with JetBrains decompiler
// Type: QuestResultUserCollection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class QuestResultUserCollection
{
  private List<QuestResultUserCollection.ResultUserInfo> list;

  public void Init(PartyModel.Party party)
  {
    this.list = new List<QuestResultUserCollection.ResultUserInfo>();
    party.slotInfos.ForEach((Action<PartyModel.SlotInfo>) (slot =>
    {
      if (slot == null || slot.userInfo == null)
        return;
      this.list.Add(new QuestResultUserCollection.ResultUserInfo(slot.userInfo));
    }));
  }

  public void Init(FieldModel.Field field)
  {
    this.list = new List<QuestResultUserCollection.ResultUserInfo>();
    field.slotInfos.ForEach((Action<FieldModel.SlotInfo>) (slot =>
    {
      if (slot == null || slot.userInfo == null)
        return;
      this.list.Add(new QuestResultUserCollection.ResultUserInfo((CharaInfo) slot.userInfo));
    }));
  }

  public void AddSelf()
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || !MonoBehaviourSingleton<StatusManager>.IsValid())
      return;
    this.AddPlayer(MonoBehaviourSingleton<StatusManager>.I.GetCreatePlayerInfo().charaInfo);
  }

  public void AddPlayer(CharaInfo chara_info)
  {
    if (chara_info == null)
      return;
    if (this.list == null)
      this.list = new List<QuestResultUserCollection.ResultUserInfo>();
    QuestResultUserCollection.ResultUserInfo userInfo = this.GetUserInfo(chara_info.userId);
    if (userInfo != null)
      userInfo.userId = chara_info.userId;
    else
      this.list.Add(new QuestResultUserCollection.ResultUserInfo(chara_info));
  }

  public void Clear() => this.list = (List<QuestResultUserCollection.ResultUserInfo>) null;

  public QuestResultUserCollection.ResultUserInfo GetUserInfo(int user_id)
  {
    return this.list == null ? (QuestResultUserCollection.ResultUserInfo) null : this.list.Find((Predicate<QuestResultUserCollection.ResultUserInfo>) (u => u != null && u.userId == user_id));
  }

  public void SetResultFollowInfo(FriendFollowModel.Param follow)
  {
    if (this.list == null || this.list.Count <= 0 || follow.success == null || follow.success.Count <= 0)
      return;
    follow.success.ForEach((Action<int>) (id => this.GetUserInfo(id)?.SetFollowEnable(false)));
  }

  public void SetResultUnfollowInfo(FriendUnfollowModel.Param unfollow, int user_id)
  {
    if (this.list == null || this.list.Count <= 0 || unfollow.success == 0)
      return;
    this.GetUserInfo(user_id)?.SetFollowEnable(true);
  }

  public void SetResultBlacklistInfo(int user_id)
  {
    if (this.list == null || this.list.Count <= 0)
      return;
    this.GetUserInfo(user_id)?.SetFollowEnable(true);
  }

  public void SetPartyFollowInfo(List<FollowPartyMember> follows)
  {
    if (this.list == null || this.list.Count <= 0 || follows == null || follows.Count <= 0)
      return;
    follows.ForEach((Action<FollowPartyMember>) (f =>
    {
      QuestResultUserCollection.ResultUserInfo userInfo = this.GetUserInfo(f.userId);
      if (userInfo == null)
        return;
      userInfo.SetFollowEnable(!f.following);
      userInfo.SetFollower(f.follower);
      userInfo.SetSelectDegrees(f.selectedDegrees);
    }));
  }

  public List<int> GetUserIdList(int my_userid = 0)
  {
    List<int> member_list = new List<int>();
    if (my_userid > 0)
      member_list.Add(my_userid);
    if (this.list == null)
      return member_list;
    this.list.ForEach((Action<QuestResultUserCollection.ResultUserInfo>) (info =>
    {
      if (info == null || my_userid > 0 && my_userid == info.userId)
        return;
      member_list.Add(info.userId);
    }));
    return member_list;
  }

  public class ResultUserInfo
  {
    public int userId;
    public List<int> selectDegrees;
    private bool? is_follow_enable;

    public ResultUserInfo(CharaInfo chara_info)
    {
      this.userId = chara_info.userId;
      this.selectDegrees = chara_info.selectedDegrees;
    }

    public bool IsFollower { private set; get; }

    public bool CanSendFollow
    {
      get
      {
        if (!this.is_follow_enable.HasValue)
          return false;
        bool? isFollowEnable = this.is_follow_enable;
        bool flag = true;
        return isFollowEnable.GetValueOrDefault() == flag & isFollowEnable.HasValue;
      }
    }

    public void SetFollowEnable(bool is_enable) => this.is_follow_enable = new bool?(is_enable);

    public void SetFollower(bool is_follower) => this.IsFollower = is_follower;

    public void SetSelectDegrees(List<int> degrees) => this.selectDegrees = degrees;
  }
}
