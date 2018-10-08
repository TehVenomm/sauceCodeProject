using Network;
using System;
using System.Collections.Generic;

public class GatherManager : MonoBehaviourSingleton<GatherManager>
{
	public int gathering;

	public List<GatherPointData> gatherPointList = new List<GatherPointData>();

	private bool firstSendGatherList = true;

	public void addGatherPoint(List<GatherPointData> list)
	{
		gatherPointList.AddRange(list);
	}

	public void updateGatherPoint(GatherPointData gatherPoint)
	{
		GatherPointData gatherPointData = gatherPointList.Find((GatherPointData list_data) => list_data.gatherPointId == gatherPoint.gatherPointId);
		if (gatherPointData != null)
		{
			gatherPointData.gatherObjectId = gatherPoint.gatherObjectId;
			gatherPointData.gatherCount = gatherPoint.gatherCount;
			gatherPointData.status = gatherPoint.status;
			gatherPointData.rest = gatherPoint.rest;
			gatherPointData.attackTime = gatherPoint.attackTime;
			gatherPointData.appearAt = gatherPoint.appearAt;
			gatherPointData.disappearAt = gatherPoint.disappearAt;
			gatherPointData.gatherEndAt = gatherPoint.gatherEndAt;
		}
	}

	public void updateGatherPoint(List<GatherPointData> list)
	{
		list.ForEach(delegate(GatherPointData gatherPoint)
		{
			updateGatherPoint(gatherPoint);
		});
	}

	public void updateGatherPointTime(int pointId, int rest, int attackTime)
	{
		GatherPointData gatherPointData = gatherPointList.Find((GatherPointData list_data) => list_data.gatherPointId == pointId);
		if (gatherPointData != null)
		{
			gatherPointData.rest = rest;
			gatherPointData.attackTime = attackTime;
		}
	}

	public void SendGatherList(Action<bool> call_back)
	{
		if (!firstSendGatherList)
		{
			call_back(true);
		}
		else
		{
			firstSendGatherList = false;
			Protocol.Send(OnceGatherListModel.URL, delegate(OnceGatherListModel ret)
			{
				bool obj = false;
				if (ret.Error == Error.None)
				{
					obj = true;
					gathering = ret.result.gathering;
					gatherPointList = ret.result.gather;
				}
				call_back(obj);
			}, string.Empty);
		}
	}

	public void SendGatherEnter(Action<bool, GatherEnterData> call_back)
	{
		Protocol.Send(GatherEnterModel.URL, delegate(GatherEnterModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
			}
			call_back.Invoke(flag, ret.result);
		}, string.Empty);
	}

	public void SendGatherUpdate(Action<bool, GatherEnterData> call_back)
	{
		Protocol.Send(GatherUpdateModel.URL, delegate(GatherUpdateModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
			}
			call_back.Invoke(flag, ret.result);
		}, string.Empty);
	}

	public void SendGatherStart(int pointId, Action<bool, bool, int> call_back)
	{
		GatherStartModel.RequestSendForm requestSendForm = new GatherStartModel.RequestSendForm();
		requestSendForm.pid = pointId;
		Protocol.Send(GatherStartModel.URL, requestSendForm, delegate(GatherStartModel ret)
		{
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			if (ret.Error == Error.None)
			{
				flag = true;
				if (ret.result.disappear.Count > 0)
				{
					flag2 = true;
					num = ret.result.fairy.lost;
				}
			}
			call_back.Invoke(flag, flag2, num);
		}, string.Empty);
	}

	public void SendGatherComplete(int pointId, Action<bool, bool, int, GatherRewardList> call_back)
	{
		GatherCompleteModel.RequestSendForm requestSendForm = new GatherCompleteModel.RequestSendForm();
		requestSendForm.pid = pointId;
		Protocol.Send(GatherCompleteModel.URL, requestSendForm, delegate(GatherCompleteModel ret)
		{
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			GatherRewardList gatherRewardList = null;
			if (ret.Error == Error.None)
			{
				flag = true;
				flag2 = ret.result.isNewOpen;
				num = ret.result.fairy.lost;
				gatherRewardList = ret.result.reward;
			}
			call_back.Invoke(flag, flag2, num, gatherRewardList);
		}, string.Empty);
	}

	public void SendGatherShortcut(int pointId, Action<bool> call_back)
	{
		GatherShortcutModel.RequestSendForm requestSendForm = new GatherShortcutModel.RequestSendForm();
		requestSendForm.pid = pointId;
		requestSendForm.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		Protocol.Send(GatherShortcutModel.URL, requestSendForm, delegate(GatherShortcutModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void Dirty()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_GATHER_OBJECT);
	}

	public void OnDiff(BaseModelDiff.DiffStatus diff)
	{
		if (Utility.IsExist(diff.gathering))
		{
			gathering = diff.gathering[0];
			Dirty();
		}
	}

	public void OnDiff(BaseModelDiff.DiffGatherPoint diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.add))
		{
			addGatherPoint(diff.add);
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			updateGatherPoint(diff.update);
			flag = true;
		}
		if (Utility.IsExist(diff.rest))
		{
			diff.rest.ForEach(delegate(BaseModelDiff.DiffGatherPoint.RestTime rest)
			{
				updateGatherPointTime(rest.gatherPointId, rest.rest, rest.attackTime);
			});
			flag = true;
		}
		if (flag)
		{
			Dirty();
		}
	}
}
