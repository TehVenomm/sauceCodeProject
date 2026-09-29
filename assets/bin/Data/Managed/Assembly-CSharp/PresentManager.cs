// Decompiled with JetBrains decompiler
// Type: PresentManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
public class PresentManager : MonoBehaviourSingleton<PresentManager>
{
  public int presentNum { get; private set; }

  public PresentList presentData { get; private set; }

  public int page { get; private set; }

  public int pageMax { get; private set; }

  public PresentManager() => this.presentData = new PresentList();

  public void DirtyPresentNum()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_PRESENT_NUM);
  }

  public void SendGetPresent(int _page, Action<bool> call_back)
  {
    this.presentData = (PresentList) null;
    this.page = 0;
    this.pageMax = 0;
    Protocol.Send<PresentListModel.RequestSendForm, PresentListModel>(PresentListModel.URL, new PresentListModel.RequestSendForm()
    {
      page = _page
    }, (Action<PresentListModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.presentData = ret.result;
        this.page = _page;
        this.pageMax = ret.result.pageNumMax;
        this.presentNum = ret.result.totalCount;
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_PRESENT_LIST);
      }
      call_back(flag);
    }));
  }

  public void SendReceivePresent(List<string> uniqIds, Action<bool, Error, int> call_back)
  {
    Protocol.Send<PresentReceiveModel.RequestSendForm, PresentReceiveModel>(PresentReceiveModel.URL, new PresentReceiveModel.RequestSendForm()
    {
      uids = uniqIds,
      page = this.page
    }, (Action<PresentReceiveModel>) (ret =>
    {
      bool flag = false;
      int num = 0;
      if (ret.Error == Error.None)
      {
        flag = true;
        num = ret.result.receivePresentNum;
        if (ret.result.list != null)
        {
          this.presentData = ret.result.list;
          this.page = ret.result.list.page;
          this.pageMax = ret.result.list.pageNumMax;
          this.presentNum = ret.result.list.totalCount;
          MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_PRESENT_LIST);
        }
        else
          this.DirtyPresentNum();
      }
      call_back(flag, ret.Error, num);
    }));
  }

  public void SendGetPresentTotalCount(Action<bool> call_back)
  {
    Protocol.Send<PresentGetTotalCountModel>(PresentGetTotalCountModel.URL, (Action<PresentGetTotalCountModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        if (this.presentNum != ret.result.totalCount)
          this.SetPresentNum(ret.result.totalCount);
      }
      call_back(flag);
    }));
  }

  public void SendDebugAddPresent(
    int rewardType,
    int actionType,
    string comment,
    int num,
    int id,
    int p0,
    int p1,
    Action<bool> call_back)
  {
    Protocol.Send<DebugAddPresentModel.RequestSendForm, DebugAddPresentModel>(DebugAddPresentModel.URL, new DebugAddPresentModel.RequestSendForm()
    {
      type = rewardType,
      actionType = actionType,
      comment = comment,
      num = num,
      id = id,
      p0 = p0,
      p1 = p1
    }, (Action<DebugAddPresentModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.presentData.presents.Add(ret.result);
        this.DirtyPresentNum();
      }
      call_back(flag);
    }));
  }

  public void SendDebugAddCrystal(int num, Action<bool> call_back)
  {
    Protocol.Send<DebugAddPresentModel.RequestSendForm, DebugAddPresentModel>(DebugAddPresentModel.URL, new DebugAddPresentModel.RequestSendForm()
    {
      type = 1,
      actionType = 0,
      comment = "仮魔晶石購入",
      num = num,
      id = 0,
      p0 = 0,
      p1 = 0
    }, (Action<DebugAddPresentModel>) (ret =>
    {
      List<string> uniqIds = new List<string>();
      if (ret.Error == Error.None)
      {
        uniqIds.Add(ret.result.uniqId);
        this.SendReceivePresent(uniqIds, (Action<bool, Error, int>) ((is_success, network_err, recv_num) => call_back(is_success)));
      }
      else
        call_back(false);
    }));
  }

  public void SetPresentNum(int presentNum)
  {
    this.presentNum = presentNum;
    this.DirtyPresentNum();
  }

  public void OnDiff(BaseModelDiff.DiffStatus diff)
  {
    if (!Utility.IsExist((ICollection) diff.present))
      return;
    this.presentNum = diff.present[0];
    this.DirtyPresentNum();
  }
}
