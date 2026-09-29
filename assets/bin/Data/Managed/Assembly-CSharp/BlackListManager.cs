// Decompiled with JetBrains decompiler
// Type: BlackListManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
public class BlackListManager : MonoBehaviourSingleton<BlackListManager>
{
  private List<int> blackUserIdList = new List<int>();
  private bool firstSetAllList = true;

  private void _addBlackUserId(List<int> add)
  {
    this.blackUserIdList.AddRange((IEnumerable<int>) add);
  }

  private void _delBlackUserId(List<int> del)
  {
    del.ForEach((Action<int>) (userId => this.blackUserIdList.Remove(userId)));
  }

  public bool CheckBlackList(int userId) => this.blackUserIdList.Contains(userId);

  public int GetBlackListUserNum() => this.blackUserIdList.Count;

  public void SetAllList()
  {
    if (!this.firstSetAllList)
      return;
    this.firstSetAllList = false;
    this.blackUserIdList = MonoBehaviourSingleton<OnceManager>.I.result.blacklist;
  }

  public void SendList(int page, Action<bool, BlackListListModel.Param> call_back)
  {
    Protocol.Send<BlackListListModel.RequestSendForm, BlackListListModel>(BlackListListModel.URL, new BlackListListModel.RequestSendForm()
    {
      page = page
    }, (Action<BlackListListModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST);
      }
      call_back(flag, ret.result);
    }));
  }

  public void SendAdd(int targetId, Action<bool> call_back)
  {
    Protocol.Send<BlackListAddModel.RequestSendForm, BlackListAddModel>(BlackListAddModel.URL, new BlackListAddModel.RequestSendForm()
    {
      id = targetId
    }, (Action<BlackListAddModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        if (MonoBehaviourSingleton<FriendManager>.IsValid())
          MonoBehaviourSingleton<FriendManager>.I.SetFollowToHomeCharaInfo(targetId, false);
        if (MonoBehaviourSingleton<QuestManager>.IsValid())
          MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.SetResultBlacklistInfo(targetId);
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM);
      }
      call_back(flag);
    }));
  }

  public void SendDelete(int targetId, Action<bool> call_back)
  {
    Protocol.Send<BlackListDeleteModel.RequestSendForm, BlackListDeleteModel>(BlackListDeleteModel.URL, new BlackListDeleteModel.RequestSendForm()
    {
      id = targetId
    }, (Action<BlackListDeleteModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM);
      }
      call_back(flag);
    }));
  }

  public void Dirty()
  {
  }

  public void OnDiff(BaseModelDiff.DiffBlackList diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      this._addBlackUserId(diff.add);
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.del))
    {
      this._delBlackUserId(diff.del);
      flag = true;
    }
    if (!flag)
      return;
    this.Dirty();
  }
}
