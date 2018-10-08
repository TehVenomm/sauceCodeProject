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
			bool arg = false;
			if (ret.Error == Error.None)
			{
				arg = true;
			}
			call_back(arg, ret.result);
		}, string.Empty);
	}

	public void SendGatherUpdate(Action<bool, GatherEnterData> call_back)
	{
		Protocol.Send(GatherUpdateModel.URL, delegate(GatherUpdateModel ret)
		{
			bool arg = false;
			if (ret.Error == Error.None)
			{
				arg = true;
			}
			call_back(arg, ret.result);
		}, string.Empty);
	}

	public void SendGatherStart(int pointId, Action<bool, bool, int> call_back)
	{
		GatherStartModel.RequestSendForm requestSendForm = new GatherStartModel.RequestSendForm();
		requestSendForm.pid = pointId;
		Protocol.Send(GatherStartModel.URL, requestSendForm, delegate(GatherStartModel ret)
		{
			bool arg = false;
			bool arg2 = false;
			int arg3 = 0;
			if (ret.Error == Error.None)
			{
				arg = true;
				if (ret.result.disappear.Count > 0)
				{
					arg2 = true;
					arg3 = ret.result.fairy.lost;
				}
			}
			call_back(arg, arg2, arg3);
		}, string.Empty);
	}

	public void SendGatherComplete(int pointId, Action<bool, bool, int, GatherRewardList> call_back)
	{
		GatherCompleteModel.RequestSendForm requestSendForm = new GatherCompleteModel.RequestSendForm();
		requestSendForm.pid = pointId;
		Protocol.Send(GatherCompleteModel.URL, requestSendForm, delegate(GatherCompleteModel ret)
		{
			bool arg = false;
			bool arg2 = false;
			int arg3 = 0;
			GatherRewardList arg4 = null;
			if (ret.Error == Error.None)
			{
				arg = true;
				arg2 = ret.result.isNewOpen;
				arg3 = ret.result.fairy.lost;
				arg4 = ret.result.reward;
			}
			call_back(arg, arg2, arg3, arg4);
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

	public void SendDebugSetFairyNum(int num, Action<bool> call_back)
	{
		DebugSetFairyNumModel.RequestSendForm requestSendForm = new DebugSetFairyNumModel.RequestSendForm();
		requestSendForm.num = num;
		Protocol.Send(DebugSetFairyNumModel.URL, requestSendForm, delegate(DebugSetFairyNumModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendDebugSetGather(int pid, int gid, int interval, Action<bool> call_back)
	{
		DebugSetGatherModel.RequestSendForm requestSendForm = new DebugSetGatherModel.RequestSendForm();
		requestSendForm.pid = pid;
		requestSendForm.gid = gid;
		requestSendForm.interval = interval;
		Protocol.Send(DebugSetGatherModel.URL, requestSendForm, delegate(DebugSetGatherModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendDebugChangeGatherTime(int pid, string appear, string disappear, string gatherStart, string gatherEnd, Action<bool> call_back)
	{
		DebugChangeGatherTimeModel.RequestSendForm requestSendForm = new DebugChangeGatherTimeModel.RequestSendForm();
		requestSendForm.pid = pid;
		requestSendForm.appear = appear;
		requestSendForm.disappear = disappear;
		requestSendForm.gatherStart = gatherStart;
		requestSendForm.gatherEnd = gatherEnd;
		Protocol.Send(DebugChangeGatherTimeModel.URL, requestSendForm, delegate(DebugChangeGatherTimeModel ret)
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
