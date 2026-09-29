// Decompiled with JetBrains decompiler
// Type: GatherManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
public class GatherManager : MonoBehaviourSingleton<GatherManager>
{
  public int gathering;
  public List<GatherPointData> gatherPointList = new List<GatherPointData>();
  private bool firstSendGatherList = true;

  public void addGatherPoint(List<GatherPointData> list)
  {
    this.gatherPointList.AddRange((IEnumerable<GatherPointData>) list);
  }

  public void updateGatherPoint(GatherPointData gatherPoint)
  {
    GatherPointData gatherPointData = this.gatherPointList.Find((Predicate<GatherPointData>) (list_data => list_data.gatherPointId == gatherPoint.gatherPointId));
    if (gatherPointData == null)
      return;
    gatherPointData.gatherObjectId = gatherPoint.gatherObjectId;
    gatherPointData.gatherCount = gatherPoint.gatherCount;
    gatherPointData.status = gatherPoint.status;
    gatherPointData.rest = gatherPoint.rest;
    gatherPointData.attackTime = gatherPoint.attackTime;
    gatherPointData.appearAt = gatherPoint.appearAt;
    gatherPointData.disappearAt = gatherPoint.disappearAt;
    gatherPointData.gatherEndAt = gatherPoint.gatherEndAt;
  }

  public void updateGatherPoint(List<GatherPointData> list)
  {
    list.ForEach((Action<GatherPointData>) (gatherPoint => this.updateGatherPoint(gatherPoint)));
  }

  public void updateGatherPointTime(int pointId, int rest, int attackTime)
  {
    GatherPointData gatherPointData = this.gatherPointList.Find((Predicate<GatherPointData>) (list_data => list_data.gatherPointId == pointId));
    if (gatherPointData == null)
      return;
    gatherPointData.rest = rest;
    gatherPointData.attackTime = attackTime;
  }

  public void SendGatherList(Action<bool> call_back)
  {
    if (!this.firstSendGatherList)
    {
      call_back(true);
    }
    else
    {
      this.firstSendGatherList = false;
      Protocol.Send<OnceGatherListModel>(OnceGatherListModel.URL, (Action<OnceGatherListModel>) (ret =>
      {
        bool flag = false;
        if (ret.Error == Error.None)
        {
          flag = true;
          this.gathering = ret.result.gathering;
          this.gatherPointList = ret.result.gather;
        }
        call_back(flag);
      }));
    }
  }

  public void SendGatherEnter(Action<bool, GatherEnterData> call_back)
  {
    Protocol.Send<GatherEnterModel>(GatherEnterModel.URL, (Action<GatherEnterModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag, ret.result);
    }));
  }

  public void SendGatherUpdate(Action<bool, GatherEnterData> call_back)
  {
    Protocol.Send<GatherUpdateModel>(GatherUpdateModel.URL, (Action<GatherUpdateModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag, ret.result);
    }));
  }

  public void SendGatherStart(int pointId, Action<bool, bool, int> call_back)
  {
    Protocol.Send<GatherStartModel.RequestSendForm, GatherStartModel>(GatherStartModel.URL, new GatherStartModel.RequestSendForm()
    {
      pid = pointId
    }, (Action<GatherStartModel>) (ret =>
    {
      bool flag1 = false;
      bool flag2 = false;
      int num = 0;
      if (ret.Error == Error.None)
      {
        flag1 = true;
        if (ret.result.disappear.Count > 0)
        {
          flag2 = true;
          num = ret.result.fairy.lost;
        }
      }
      call_back(flag1, flag2, num);
    }));
  }

  public void SendGatherComplete(
    int pointId,
    Action<bool, bool, int, GatherRewardList> call_back)
  {
    Protocol.Send<GatherCompleteModel.RequestSendForm, GatherCompleteModel>(GatherCompleteModel.URL, new GatherCompleteModel.RequestSendForm()
    {
      pid = pointId
    }, (Action<GatherCompleteModel>) (ret =>
    {
      bool flag1 = false;
      bool flag2 = false;
      int num = 0;
      GatherRewardList gatherRewardList = (GatherRewardList) null;
      if (ret.Error == Error.None)
      {
        flag1 = true;
        flag2 = ret.result.isNewOpen;
        num = ret.result.fairy.lost;
        gatherRewardList = ret.result.reward;
      }
      call_back(flag1, flag2, num, gatherRewardList);
    }));
  }

  public void SendGatherShortcut(int pointId, Action<bool> call_back)
  {
    Protocol.Send<GatherShortcutModel.RequestSendForm, GatherShortcutModel>(GatherShortcutModel.URL, new GatherShortcutModel.RequestSendForm()
    {
      pid = pointId,
      crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal
    }, (Action<GatherShortcutModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      call_back(flag);
    }));
  }

  public void Dirty()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_GATHER_OBJECT);
  }

  public void OnDiff(BaseModelDiff.DiffStatus diff)
  {
    if (!Utility.IsExist((ICollection) diff.gathering))
      return;
    this.gathering = diff.gathering[0];
    this.Dirty();
  }

  public void OnDiff(BaseModelDiff.DiffGatherPoint diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      this.addGatherPoint(diff.add);
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      this.updateGatherPoint(diff.update);
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.rest))
    {
      diff.rest.ForEach((Action<BaseModelDiff.DiffGatherPoint.RestTime>) (rest => this.updateGatherPointTime(rest.gatherPointId, rest.rest, rest.attackTime)));
      flag = true;
    }
    if (!flag)
      return;
    this.Dirty();
  }
}
