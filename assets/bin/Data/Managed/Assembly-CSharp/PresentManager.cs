using Network;
using System;
using System.Collections.Generic;

public class PresentManager : MonoBehaviourSingleton<PresentManager>
{
	public int presentNum
	{
		get;
		private set;
	}

	public PresentList presentData
	{
		get;
		private set;
	}

	public int page
	{
		get;
		private set;
	}

	public int pageMax
	{
		get;
		private set;
	}

	public PresentManager()
	{
		presentData = new PresentList();
	}

	public void DirtyPresentNum()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_PRESENT_NUM);
	}

	public void SendGetPresent(int _page, Action<bool> call_back)
	{
		presentData = null;
		page = 0;
		pageMax = 0;
		PresentListModel.RequestSendForm requestSendForm = new PresentListModel.RequestSendForm();
		requestSendForm.page = _page;
		Protocol.Send(PresentListModel.URL, requestSendForm, delegate(PresentListModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				presentData = ret.result;
				page = _page;
				pageMax = ret.result.pageNumMax;
				presentNum = ret.result.totalCount;
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_PRESENT_LIST);
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendReceivePresent(List<string> uniqIds, Action<bool, Error, int> call_back)
	{
		PresentReceiveModel.RequestSendForm requestSendForm = new PresentReceiveModel.RequestSendForm();
		requestSendForm.uids = uniqIds;
		requestSendForm.page = page;
		Protocol.Send(PresentReceiveModel.URL, requestSendForm, delegate(PresentReceiveModel ret)
		{
			bool arg = false;
			int arg2 = 0;
			if (ret.Error == Error.None)
			{
				arg = true;
				arg2 = ret.result.receivePresentNum;
				if (ret.result.list != null)
				{
					presentData = ret.result.list;
					page = ret.result.list.page;
					pageMax = ret.result.list.pageNumMax;
					presentNum = ret.result.list.totalCount;
					MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_PRESENT_LIST);
				}
				else
				{
					DirtyPresentNum();
				}
			}
			call_back(arg, ret.Error, arg2);
		}, string.Empty);
	}

	public void SendGetPresentTotalCount(Action<bool> call_back)
	{
		Protocol.Send(PresentGetTotalCountModel.URL, delegate(PresentGetTotalCountModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				if (presentNum != ret.result.totalCount)
				{
					SetPresentNum(ret.result.totalCount);
				}
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendDebugAddPresent(int rewardType, int actionType, string comment, int num, int id, int p0, int p1, Action<bool> call_back)
	{
		DebugAddPresentModel.RequestSendForm requestSendForm = new DebugAddPresentModel.RequestSendForm();
		requestSendForm.type = rewardType;
		requestSendForm.actionType = actionType;
		requestSendForm.comment = comment;
		requestSendForm.num = num;
		requestSendForm.id = id;
		requestSendForm.p0 = p0;
		requestSendForm.p1 = p1;
		Protocol.Send(DebugAddPresentModel.URL, requestSendForm, delegate(DebugAddPresentModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				presentData.presents.Add(ret.result);
				DirtyPresentNum();
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendDebugAddCrystal(int num, Action<bool> call_back)
	{
		DebugAddPresentModel.RequestSendForm requestSendForm = new DebugAddPresentModel.RequestSendForm();
		requestSendForm.type = 1;
		requestSendForm.actionType = 0;
		requestSendForm.comment = "仮魔晶石購入";
		requestSendForm.num = num;
		requestSendForm.id = 0;
		requestSendForm.p0 = 0;
		requestSendForm.p1 = 0;
		Protocol.Send(DebugAddPresentModel.URL, requestSendForm, delegate(DebugAddPresentModel ret)
		{
			List<string> list = new List<string>();
			if (ret.Error == Error.None)
			{
				list.Add(ret.result.uniqId);
				SendReceivePresent(list, delegate(bool is_success, Error network_err, int recv_num)
				{
					call_back(is_success);
				});
			}
			else
			{
				call_back(obj: false);
			}
		}, string.Empty);
	}

	public void SetPresentNum(int presentNum)
	{
		this.presentNum = presentNum;
		DirtyPresentNum();
	}

	public void OnDiff(BaseModelDiff.DiffStatus diff)
	{
		if (Utility.IsExist(diff.present))
		{
			presentNum = diff.present[0];
			DirtyPresentNum();
		}
	}
}
