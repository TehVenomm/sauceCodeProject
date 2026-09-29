// Decompiled with JetBrains decompiler
// Type: FriendManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FriendManager : MonoBehaviourSingleton<FriendManager>
{
  private bool _isHomeCharaCached;
  public const int DRAW_FOLLOW_MAX = 10;
  private FriendFollowListModel.Param recvFollowList;
  private FriendFollowListModel.Param recvFollowerList;
  private FriendMessageUserListModel.Param recvMessageUserList;
  private List<FriendMessageUserListModel.MessageUserInfo> recvUserListAtLeastGetMessageOnce;
  private string mutualFollowValue = "";
  private FriendSearchResult recvSearchList;

  public HomeCharaInfoList homeCharas { get; private set; }

  public bool IsHomeCharaCached => this._isHomeCharaCached;

  public void SetFollowToHomeCharaInfo(int userId, bool follow)
  {
    FriendCharaInfo friendCharaInfo = this.homeCharas.chara.Find((Predicate<FriendCharaInfo>) (c => c.userId == userId));
    if (friendCharaInfo == null)
      return;
    friendCharaInfo.following = follow;
  }

  public void SetFollowerToHomeCharaInfo(int userId, bool follower)
  {
    FriendCharaInfo friendCharaInfo = this.homeCharas.chara.Find((Predicate<FriendCharaInfo>) (c => c.userId == userId));
    if (friendCharaInfo == null)
      return;
    friendCharaInfo.follower = follower;
  }

  public void SetClanInviteToHomeCharaInfo(int userId, bool isInvite)
  {
    FriendCharaInfo friendCharaInfo = this.homeCharas.chara.Find((Predicate<FriendCharaInfo>) (c => c.userId == userId));
    if (friendCharaInfo == null)
      return;
    friendCharaInfo.isInviteToClan = isInvite;
  }

  public FriendMessageUserListModel.MessageUserInfo talkUser { private set; get; }

  public FriendFollowLinkResult followLinkResult { get; private set; }

  public FriendMutualFollowResult mutualFollowResult { get; private set; }

  public string MutualFollowValue
  {
    get => this.mutualFollowValue;
    set => this.mutualFollowValue = value;
  }

  public int messagePageMax { private set; get; }

  public List<FriendMessageData> messageDetailList { private set; get; }

  private void AddMessageDetailList(List<FriendMessageData> addMessageList)
  {
    addMessageList.ForEach((Action<FriendMessageData>) (message =>
    {
      if (this.messageDetailList.Find((Predicate<FriendMessageData>) (m => m.id == message.id)) != null)
        return;
      this.messageDetailList.Add(message);
    }));
    this.messageDetailList.Sort((Comparison<FriendMessageData>) ((l, r) => l.lid.CompareTo(r.lid)));
  }

  public int followNum { get; private set; }

  public int followerNum { get; private set; }

  public void SetFollowNum(int num) => this.followNum = num;

  public void SetFollowerNum(int num) => this.followerNum = num;

  public int noReadMessageNum { get; private set; }

  public void SetNoReadMessageNum(int num)
  {
    if (0 > num)
      num = 0;
    this.noReadMessageNum = num;
  }

  protected override void Awake()
  {
    base.Awake();
    this.homeCharas = new HomeCharaInfoList();
    this.messageDetailList = new List<FriendMessageData>();
    this.noReadMessageNum = 0;
  }

  public void SendHomeCharaList(Action<bool> callback)
  {
    Protocol.SendAsync<HomeCharaListModel>("ajax/home/charalist", (Action<HomeCharaListModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
      {
        this.homeCharas = ret.result;
        this._isHomeCharaCached = true;
      }
      if (callback == null)
        return;
      callback(flag);
    }));
  }

  public void SendGetChara(int[] userIds, Action<bool, List<FriendCharaInfo>> callback)
  {
    HomeGetCharaModel.RequestSendForm postData = new HomeGetCharaModel.RequestSendForm();
    int index = 0;
    for (int length = userIds.Length; index < length; ++index)
      postData.ids.Add(userIds[index]);
    Protocol.Send<HomeGetCharaModel.RequestSendForm, HomeGetCharaModel>(HomeGetCharaModel.URL, postData, (Action<HomeGetCharaModel>) (ret => callback(ErrorCodeChecker.IsSuccess(ret.Error), ret.result)));
  }

  public void SendGetFollowList(int page, Action<bool, FriendFollowListModel.Param> callback)
  {
    Protocol.Send<FriendFollowListModel.RequestSendForm, FriendFollowListModel>(FriendFollowListModel.URL, new FriendFollowListModel.RequestSendForm()
    {
      page = page
    }, (Action<FriendFollowListModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
      {
        this.recvFollowList = ret.result;
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST);
      }
      callback(flag, this.recvFollowList);
    }));
  }

  public void SendGetFollowerList(
    int _chunkIndex,
    int _sortTypeIndex,
    Action<bool, FriendFollowerListModel.Param> callback)
  {
    Protocol.Send<FriendFollowerListModel.RequestSendForm, FriendFollowerListModel>(FriendFollowerListModel.URL, new FriendFollowerListModel.RequestSendForm()
    {
      page = _chunkIndex,
      sortType = _sortTypeIndex
    }, (Action<FriendFollowerListModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
      {
        this.recvFollowerList = (FriendFollowListModel.Param) ret.result;
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST);
      }
      callback(flag, ret.result);
    }));
  }

  public void SendGetFollowLink(Action<bool> callback)
  {
    Protocol.Send<FriendFollowLinkModel>(FriendFollowLinkModel.URL, (Action<FriendFollowLinkModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (ret.Error == Error.None)
      {
        this.followLinkResult = ret.result;
        flag = true;
      }
      callback(flag);
    }));
  }

  public void SendFollowUser(List<int> id_list, Action<Error, List<int>> callback)
  {
    Protocol.Send<FriendFollowModel.RequestSendForm, FriendFollowModel>(FriendFollowModel.URL, new FriendFollowModel.RequestSendForm()
    {
      ids = id_list
    }, (Action<FriendFollowModel>) (ret =>
    {
      List<int> intList = new List<int>();
      if (ErrorCodeChecker.IsSuccess(ret.Error))
      {
        intList = ret.result.success;
        if (MonoBehaviourSingleton<QuestManager>.IsValid())
          MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.SetResultFollowInfo(ret.result);
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM);
      }
      callback(ret.Error, intList);
    }));
    MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("Friend_request", "Social");
  }

  public void SendMutualFollow(string targetCode, Action<bool> callback)
  {
    Protocol.Send<FriendMutualFollowModel.RequestSendForm, FriendMutualFollowModel>(FriendMutualFollowModel.URL, new FriendMutualFollowModel.RequestSendForm()
    {
      targetCode = targetCode
    }, (Action<FriendMutualFollowModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (ret.Error == Error.None)
      {
        this.mutualFollowResult = ret.result;
        flag = true;
      }
      callback(flag);
    }));
  }

  public void SendUnfollowUser(int user_id, Action<bool> callback)
  {
    Protocol.Send<FriendUnfollowModel.RequestSendForm, FriendUnfollowModel>(FriendUnfollowModel.URL, new FriendUnfollowModel.RequestSendForm()
    {
      followUserId = user_id
    }, (Action<FriendUnfollowModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
      {
        flag = ret.result.success == 1;
        if (MonoBehaviourSingleton<QuestManager>.IsValid())
          MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.SetResultUnfollowInfo(ret.result, user_id);
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM);
      }
      callback(flag);
    }));
  }

  public void SendDeleteFollower(int user_id, Action<bool> callback)
  {
    Protocol.Send<FriendDeleteFollowerModel.RequestSendForm, FriendDeleteFollowerModel>(FriendDeleteFollowerModel.URL, new FriendDeleteFollowerModel.RequestSendForm()
    {
      followerUserId = user_id
    }, (Action<FriendDeleteFollowerModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
      {
        flag = ret.result.success == 1;
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM);
      }
      callback(flag);
    }));
  }

  public void SendGetUserListMessagedOnce(
    bool isCalledByOther,
    Action<bool, FriendMessagedMutualFollowerListModel.Param> _callback)
  {
    if (!isCalledByOther)
      this.SendGetUserListMessagedOnce(_callback);
    else
      Protocol.Try((System.Action) (() => this.SendGetUserListMessagedOnce(_callback)));
  }

  public void SendGetUserListMessagedOnce(
    Action<bool, FriendMessagedMutualFollowerListModel.Param> callback)
  {
    FriendMessagedMutualFollowerListModel.RequestSendForm postData = new FriendMessagedMutualFollowerListModel.RequestSendForm();
    Protocol.Send<FriendMessagedMutualFollowerListModel.RequestSendForm, FriendMessagedMutualFollowerListModel>(FriendMessagedMutualFollowerListModel.URL, postData, (Action<FriendMessagedMutualFollowerListModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
        this.recvUserListAtLeastGetMessageOnce = ret.result.messageFollowList;
      callback(flag, ret.result);
    }));
  }

  public void SendGetMessageUserList(
    int page,
    Action<bool, FriendMessageUserListModel.Param> callback)
  {
    Protocol.Send<FriendMessageUserListModel.RequestSendForm, FriendMessageUserListModel>(FriendMessageUserListModel.URL, new FriendMessageUserListModel.RequestSendForm()
    {
      page = page
    }, (Action<FriendMessageUserListModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
      {
        this.recvMessageUserList = ret.result;
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST);
      }
      callback(flag, this.recvMessageUserList);
    }));
  }

  public void SendGetMessageUserList(
    int page,
    bool isCalledByOther,
    Action<bool, FriendMessageUserListModel.Param> callback)
  {
    if (!isCalledByOther)
      this.SendGetMessageUserList(page, callback);
    else
      Protocol.Try((System.Action) (() => this.SendGetMessageUserList(page, callback)));
  }

  public void SendGetMessageDetailList(
    int user_id,
    int page,
    bool isCalledByOther,
    Action<bool> callback)
  {
    if (!isCalledByOther)
      this.SendGetMessageDetailList(user_id, page, callback);
    else
      Protocol.Try((System.Action) (() => this.SendGetMessageDetailList(user_id, page, callback)));
  }

  public void SendGetMessageDetailList(int user_id, int page, Action<bool> callback)
  {
    FriendMessageDetailListModel.RequestSendForm postData = new FriendMessageDetailListModel.RequestSendForm();
    postData.userId = user_id;
    postData.page = page;
    if (this.talkUser == null || this.talkUser.userId != user_id)
    {
      this.talkUser = (FriendMessageUserListModel.MessageUserInfo) null;
      this.messageDetailList.Clear();
    }
    Protocol.Send<FriendMessageDetailListModel.RequestSendForm, FriendMessageDetailListModel>(FriendMessageDetailListModel.URL, postData, (Action<FriendMessageDetailListModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
      {
        this.messagePageMax = ret.result.pageNumMax;
        this.AddMessageDetailList(ret.result.message);
        FriendMessageUserListModel.MessageUserInfo messageUser = this.FindMessageUser(user_id);
        if (messageUser != null)
          this.talkUser = messageUser;
      }
      callback(flag);
    }));
  }

  private FriendMessageUserListModel.MessageUserInfo FindMessageUser(int userId)
  {
    if (this.recvMessageUserList != null && this.recvMessageUserList.messageUser != null)
    {
      FriendMessageUserListModel.MessageUserInfo messageUser = this.recvMessageUserList.messageUser.Find((Predicate<FriendMessageUserListModel.MessageUserInfo>) (user => user.userId == userId));
      if (messageUser != null)
        return messageUser;
    }
    if (this.recvUserListAtLeastGetMessageOnce != null)
    {
      FriendMessageUserListModel.MessageUserInfo messageUser = this.recvUserListAtLeastGetMessageOnce.Find((Predicate<FriendMessageUserListModel.MessageUserInfo>) (user => user.userId == userId));
      if (messageUser != null)
        return messageUser;
    }
    return (FriendMessageUserListModel.MessageUserInfo) null;
  }

  public void SendFriendMessage(
    int user_id,
    string message,
    bool isCalledByOther,
    Action<bool> callback)
  {
    if (!isCalledByOther)
      this.SendFriendMessage(user_id, message, callback);
    else
      Protocol.Try((System.Action) (() => this.SendFriendMessage(user_id, message, callback)));
  }

  public void SendFriendMessage(int user_id, string message, Action<bool> callback)
  {
    Protocol.Send<FriendSendMessageModel.RequestSendForm, FriendSendMessageModel>(FriendSendMessageModel.URL, new FriendSendMessageModel.RequestSendForm()
    {
      toUserId = user_id,
      message = message
    }, (Action<FriendSendMessageModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
        flag = ret.result.success == 1;
      callback(flag);
    }));
  }

  public void SendGetNoreadMessage(bool isCalledByOther, Action<bool> callback)
  {
    if (!isCalledByOther)
      this.SendGetNoreadMessage(callback);
    else
      Protocol.Try((System.Action) (() => this.SendGetNoreadMessage(callback)));
  }

  public void SendGetNoreadMessage(Action<bool> callback)
  {
    if (this.talkUser == null)
      callback(false);
    else
      Protocol.Send<FriendGetNoReadMessageModel.RequestSendForm, FriendGetNoReadMessageModel>(FriendGetNoReadMessageModel.URL, new FriendGetNoReadMessageModel.RequestSendForm()
      {
        userId = this.talkUser.userId
      }, (Action<FriendGetNoReadMessageModel>) (ret => callback(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  public void SendSearchName(string name, int page, Action<bool, FriendSearchResult> callback)
  {
    Protocol.Send<FriendSearchByNameModel.RequestSendForm, FriendSearchByNameModel>(FriendSearchByNameModel.URL, new FriendSearchByNameModel.RequestSendForm()
    {
      name = name,
      page = page
    }, (Action<FriendSearchByNameModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
      {
        this.recvSearchList = ret.result;
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST);
      }
      callback(flag, this.recvSearchList);
    }));
  }

  public void SendSearchLevel(int page, Action<bool, FriendSearchResult> callback)
  {
    Protocol.Send<FriendSearchByLevelModel.RequestSendForm, FriendSearchByLevelModel>(FriendSearchByLevelModel.URL, new FriendSearchByLevelModel.RequestSendForm()
    {
      page = page
    }, (Action<FriendSearchByLevelModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
      {
        this.recvSearchList = ret.result;
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST);
      }
      callback(flag, this.recvSearchList);
    }));
  }

  public void SendSearchID(string code, Action<bool, FriendSearchResult> callback)
  {
    Protocol.Send<FriendSearchByCodeModel.RequestSendForm, FriendSearchByCodeModel>(FriendSearchByCodeModel.URL, new FriendSearchByCodeModel.RequestSendForm()
    {
      code = code
    }, (Action<FriendSearchByCodeModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
      {
        this.recvSearchList = ret.result;
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST);
      }
      callback(flag, this.recvSearchList);
    }));
  }

  public void SendGetArenaRanking(
    int group,
    int isContaionSelf,
    Action<bool, List<ArenaRankingData>> callback)
  {
    ArenaRankingModel.RequestSendForm postData = new ArenaRankingModel.RequestSendForm();
    postData.groupId = group;
    postData.isContainSelf = isContaionSelf;
    List<ArenaRankingData> rankingDataList = (List<ArenaRankingData>) null;
    Protocol.Send<ArenaRankingModel.RequestSendForm, ArenaRankingModel>(ArenaRankingModel.URL, postData, (Action<ArenaRankingModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
        rankingDataList = ret.result;
      callback(flag, rankingDataList);
    }));
  }

  public void SendGetLastRanking(
    int group,
    int isContaionSelf,
    Action<bool, ArenaLastRankingModel.Param> callback)
  {
    ArenaLastRankingModel.RequestSendForm postData = new ArenaLastRankingModel.RequestSendForm();
    postData.groupId = group;
    postData.isContainSelf = isContaionSelf;
    ArenaLastRankingModel.Param result = (ArenaLastRankingModel.Param) null;
    Protocol.Send<ArenaLastRankingModel.RequestSendForm, ArenaLastRankingModel>(ArenaLastRankingModel.URL, postData, (Action<ArenaLastRankingModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
        result = ret.result;
      callback(flag, result);
    }));
  }

  public void SendGetFriendRanking(
    int group,
    int isContaionSelf,
    Action<bool, List<ArenaRankingData>> callback)
  {
    ArenaFriendRankingModel.RequestSendForm postData = new ArenaFriendRankingModel.RequestSendForm();
    postData.groupId = group;
    postData.isContainSelf = isContaionSelf;
    List<ArenaRankingData> rankingDataList = (List<ArenaRankingData>) null;
    Protocol.Send<ArenaFriendRankingModel.RequestSendForm, ArenaFriendRankingModel>(ArenaFriendRankingModel.URL, postData, (Action<ArenaFriendRankingModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
        rankingDataList = ret.result;
      callback(flag, rankingDataList);
    }));
  }

  public void SendGetLegendRanking(
    Action<bool, List<ArenaLegendRankingModel.Param>> callback)
  {
    List<ArenaLegendRankingModel.Param> result = new List<ArenaLegendRankingModel.Param>();
    Protocol.Send<ArenaLegendRankingModel>(ArenaLegendRankingModel.URL, (WWWForm) null, (Action<ArenaLegendRankingModel>) (ret =>
    {
      bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
      if (flag)
        result = ret.result;
      callback(flag, result);
    }));
  }

  public void Dirty()
  {
  }

  public void OnDiff(BaseModelDiff.DiffFriend diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.follow))
    {
      this.followNum = diff.follow[0];
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.follower))
    {
      this.followerNum = diff.follower[0];
      flag = true;
    }
    if (!flag)
      return;
    this.Dirty();
  }

  public void DirtyMessage()
  {
  }

  public void OnDiff(BaseModelDiff.DiffMessage diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      this.AddMessageDetailList(diff.add);
      flag = true;
    }
    if (!flag)
      return;
    this.DirtyMessage();
  }

  public void ResetUser()
  {
    this.talkUser = (FriendMessageUserListModel.MessageUserInfo) null;
    if (this.messageDetailList == null)
      return;
    this.messageDetailList.Clear();
  }
}
