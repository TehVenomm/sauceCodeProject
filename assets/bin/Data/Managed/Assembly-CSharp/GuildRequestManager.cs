using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GuildRequestManager : MonoBehaviourSingleton<GuildRequestManager>
{
	public enum StringKey
	{
		START_CONFIRM,
		PAY_CONFIRM,
		PAY_RESULT,
		CANCEL_CONFIRM,
		CONTINUE_CONFIRM,
		REMAIN_TIME_WARNING,
		NEED_POINT_AND_TIME,
		CONTINUE_BUTTON,
		END_BUTTON,
		PUSH_TITLE,
		PUSH_COMPLETE,
		REMAIN_TIME,
		NONE_LIMITED,
		SORTIEING_NUM,
		BONUS_TIME,
		ADDITIONAL_HOUND,
		HOUND_NO_1,
		HOUND_NO_2,
		HOUND_NO_3,
		HOUND_NO_4,
		PUSH_REMAIN
	}

	public GuildRequest guildRequestData;

	private GuildRequestItem selectedItem;

	private uint beforeQuestId;

	public bool isCompleteMulti;

	private bool firstSetGetList = true;

	public int GetNeedPoint(RARITY_TYPE rarity)
	{
		ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
		switch (rarity)
		{
		case RARITY_TYPE.B:
			return constDefine.GUILD_REQUEST_NEED_POINT_B;
		case RARITY_TYPE.A:
			return constDefine.GUILD_REQUEST_NEED_POINT_A;
		case RARITY_TYPE.S:
			return constDefine.GUILD_REQUEST_NEED_POINT_S;
		case RARITY_TYPE.SS:
			return constDefine.GUILD_REQUEST_NEED_POINT_SS;
		case RARITY_TYPE.SSS:
			return constDefine.GUILD_REQUEST_NEED_POINT_SSS;
		default:
			return constDefine.GUILD_REQUEST_NEED_POINT_SS;
		}
	}

	public TimeSpan GetNeedTime(RARITY_TYPE rarity)
	{
		int needPoint = GetNeedPoint(rarity);
		return CalcTimeSpanFromPoint(needPoint);
	}

	public string GetNeedTimeWithFormat(RARITY_TYPE rarity)
	{
		TimeSpan needTime = GetNeedTime(rarity);
		return new DateTime(0L).Add(needTime).ToString("H:mm:ss");
	}

	public TimeSpan CalcTimeSpanFromPoint(int point)
	{
		ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
		float num = (float)point / (float)constDefine.GUILD_POINT_PER_MIN;
		int seconds = (int)(num * 60f);
		return new TimeSpan(0, 0, seconds);
	}

	public int CalcPointFromTimeSpan(TimeSpan time)
	{
		ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
		double num = time.TotalMinutes * (double)constDefine.GUILD_POINT_PER_MIN;
		return (int)num;
	}

	public void SetList()
	{
		if (firstSetGetList)
		{
			firstSetGetList = false;
			List<GuildRequestItem> guildRequestItemList = MonoBehaviourSingleton<OnceManager>.I.result.guildRequestItemList;
			guildRequestData = new GuildRequest(guildRequestItemList);
		}
	}

	public GuildRequestItem GetSelectedItem()
	{
		return selectedItem;
	}

	public void SetSelectedItem(GuildRequestItem guildRequestItem)
	{
		selectedItem = guildRequestItem;
	}

	public uint GetBeforeQuestId()
	{
		return beforeQuestId;
	}

	public void SendGuildRequestList(Action<bool> call_back)
	{
		guildRequestData = null;
		Protocol.Send(GuildRequestListModel.URL, delegate(GuildRequestListModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				guildRequestData = ret.result;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendGuildRequestStart(QuestInfoData questInfoData, bool isQuestItem, Action<bool> call_back)
	{
		GuildRequestStartModel.RequestSendForm requestSendForm = new GuildRequestStartModel.RequestSendForm();
		requestSendForm.slotNo = selectedItem.slotNo;
		requestSendForm.questId = (int)questInfoData.questData.tableData.questID;
		requestSendForm.num = 1;
		requestSendForm.isQuestItem = (isQuestItem ? 1 : 0);
		Protocol.Send(GuildRequestStartModel.URL, requestSendForm, delegate(GuildRequestStartModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back(obj);
		}, string.Empty);
	}

	public void SendGuildRequestComplete(Action<GuildRequestCompleteModel.Param> call_back)
	{
		GuildRequestCompleteModel.RequestSendForm requestSendForm = new GuildRequestCompleteModel.RequestSendForm();
		requestSendForm.slotNo = selectedItem.slotNo;
		beforeQuestId = (uint)selectedItem.questId;
		GuildRequestCompleteModel.Param completeData = null;
		Protocol.Send(GuildRequestCompleteModel.URL, requestSendForm, delegate(GuildRequestCompleteModel ret)
		{
			if (ret.Error == Error.None)
			{
				completeData = ret.result;
			}
			call_back(completeData);
		}, string.Empty);
	}

	public void SendGuildRequestCompleteAll(Action<GuildRequestCompleteModel.Param> call_back)
	{
		GuildRequestCompleteModel.Param completeData = null;
		Protocol.Send(GuildRequestCompleteAllModel.URL, delegate(GuildRequestCompleteAllModel ret)
		{
			if (ret.Error == Error.None)
			{
				completeData = ret.result;
			}
			call_back(completeData);
		}, string.Empty);
	}

	public void SendGuildRequestExtendAndSortie(QuestInfoData questInfoData, bool isQuestItem, Action<bool> call_back)
	{
		GuildRequestExtendAndStartModel.RequestSendForm requestSendForm = new GuildRequestExtendAndStartModel.RequestSendForm();
		requestSendForm.slotNo = selectedItem.slotNo;
		requestSendForm.questId = (int)questInfoData.questData.tableData.questID;
		requestSendForm.num = 1;
		requestSendForm.isQuestItem = (isQuestItem ? 1 : 0);
		requestSendForm.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		Protocol.Send(GuildRequestExtendAndStartModel.URL, requestSendForm, delegate(GuildRequestExtendAndStartModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back(obj);
		}, string.Empty);
	}

	public void SendGuildRequestExtend(Action<bool> call_back)
	{
		GuildRequestExtendModel.RequestSendForm requestSendForm = new GuildRequestExtendModel.RequestSendForm();
		requestSendForm.slotNo = selectedItem.slotNo;
		requestSendForm.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		Protocol.Send(GuildRequestExtendModel.URL, requestSendForm, delegate(GuildRequestExtendModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back(obj);
		}, string.Empty);
	}

	public void SendGuildRequestRetire(Action<bool> call_back)
	{
		GuildRequestRetireModel.RequestSendForm requestSendForm = new GuildRequestRetireModel.RequestSendForm();
		requestSendForm.slotNo = selectedItem.slotNo;
		Protocol.Send(GuildRequestRetireModel.URL, requestSendForm, delegate(GuildRequestRetireModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			call_back(obj);
		}, string.Empty);
	}

	public void RegisterGuildRequestLocalNotification()
	{
		if (MonoBehaviourSingleton<GuildRequestManager>.I.GetLocalPushFlag() && guildRequestData != null && guildRequestData.guildRequestItemList != null)
		{
			List<DateTime> list = new List<DateTime>(5);
			foreach (GuildRequestItem guildRequestItem in guildRequestData.guildRequestItemList)
			{
				TimeSpan questRemainTime = guildRequestItem.GetQuestRemainTime();
				if (!(questRemainTime.TotalSeconds <= 0.0))
				{
					TimeSpan houndRemainTime = guildRequestItem.GetHoundRemainTime();
					if (guildRequestItem.crystalNum <= 0 || !(questRemainTime.TotalSeconds > houndRemainTime.TotalSeconds))
					{
						DateTime item = DateTime.Now.AddTicks(questRemainTime.Ticks);
						list.Add(item);
					}
				}
			}
			MonoBehaviourSingleton<AppMain>.I.SetGuildRequestConstructLocalNotification(list);
		}
	}

	public void ClearGuildRequestLocalNotification()
	{
		MonoBehaviourSingleton<AppMain>.I.SetGuildRequestConstructLocalNotification(new List<DateTime>());
	}

	public void SetLocalPushFlag(bool flag)
	{
		PlayerPrefs.SetInt("LOCAL_PUSH_GUILD_REQUEST_KEY", flag ? 1 : 0);
	}

	public bool GetLocalPushFlag()
	{
		if (!PlayerPrefs.HasKey("LOCAL_PUSH_GUILD_REQUEST_KEY"))
		{
			SetLocalPushFlag(flag: true);
			return true;
		}
		return PlayerPrefs.GetInt("LOCAL_PUSH_GUILD_REQUEST_KEY") == 1;
	}

	public void ToggleLocalPushFlag()
	{
		bool localPushFlag = GetLocalPushFlag();
		SetLocalPushFlag(!localPushFlag);
	}

	public void Dirty()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_CHANGE);
	}

	public void OnDiff(BaseModelDiff.DiffGuildRequest diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.add))
		{
			diff.add.ForEach(delegate(GuildRequestItem data)
			{
				if (guildRequestData == null)
				{
					guildRequestData = new GuildRequest();
				}
				guildRequestData.guildRequestItemList.Add(data);
			});
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			diff.update.ForEach(delegate(GuildRequestItem data)
			{
				GuildRequestItem guildRequestItem = guildRequestData.guildRequestItemList.Find((GuildRequestItem list_data) => list_data.slotNo == data.slotNo);
				guildRequestItem.slotNo = data.slotNo;
				guildRequestItem.crystalNum = data.crystalNum;
				guildRequestItem.questId = data.questId;
				guildRequestItem.num = data.num;
				guildRequestItem.endAt = data.endAt;
				guildRequestItem.expiredAt = data.expiredAt;
			});
			flag = true;
		}
		if (flag)
		{
			Dirty();
		}
	}
}
