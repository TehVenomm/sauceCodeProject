using Network;
using System;
using System.Collections.Generic;

public class BlackListManager : MonoBehaviourSingleton<BlackListManager>
{
	private List<int> blackUserIdList = new List<int>();

	private bool firstSetAllList = true;

	private void _addBlackUserId(List<int> add)
	{
		blackUserIdList.AddRange(add);
	}

	private void _delBlackUserId(List<int> del)
	{
		del.ForEach(delegate(int userId)
		{
			blackUserIdList.Remove(userId);
		});
	}

	public bool CheckBlackList(int userId)
	{
		return blackUserIdList.Contains(userId);
	}

	public int GetBlackListUserNum()
	{
		return blackUserIdList.Count;
	}

	public void SetAllList()
	{
		if (firstSetAllList)
		{
			firstSetAllList = false;
			blackUserIdList = MonoBehaviourSingleton<OnceManager>.I.result.blacklist;
		}
	}

	public void SendList(int page, Action<bool, BlackListListModel.Param> call_back)
	{
		BlackListListModel.RequestSendForm requestSendForm = new BlackListListModel.RequestSendForm();
		requestSendForm.page = page;
		Protocol.Send(BlackListListModel.URL, requestSendForm, delegate(BlackListListModel ret)
		{
			bool arg = false;
			if (ret.Error == Error.None)
			{
				arg = true;
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST);
			}
			call_back(arg, ret.result);
		}, string.Empty);
	}

	public void SendAdd(int targetId, Action<bool> call_back)
	{
		BlackListAddModel.RequestSendForm requestSendForm = new BlackListAddModel.RequestSendForm();
		requestSendForm.id = targetId;
		Protocol.Send(BlackListAddModel.URL, requestSendForm, delegate(BlackListAddModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				if (MonoBehaviourSingleton<FriendManager>.IsValid())
				{
					MonoBehaviourSingleton<FriendManager>.I.SetFollowToHomeCharaInfo(targetId, false);
				}
				if (MonoBehaviourSingleton<QuestManager>.IsValid())
				{
					MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.SetResultBlacklistInfo(targetId);
				}
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM);
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendDelete(int targetId, Action<bool> call_back)
	{
		BlackListDeleteModel.RequestSendForm requestSendForm = new BlackListDeleteModel.RequestSendForm();
		requestSendForm.id = targetId;
		Protocol.Send(BlackListDeleteModel.URL, requestSendForm, delegate(BlackListDeleteModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM);
			}
			call_back(obj);
		}, string.Empty);
	}

	public void Dirty()
	{
	}

	public void OnDiff(BaseModelDiff.DiffBlackList diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.add))
		{
			_addBlackUserId(diff.add);
			flag = true;
		}
		if (Utility.IsExist(diff.del))
		{
			_delBlackUserId(diff.del);
			flag = true;
		}
		if (flag)
		{
			Dirty();
		}
	}
}
