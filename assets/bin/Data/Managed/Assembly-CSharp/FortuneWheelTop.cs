using Network;
using System;
using System.Collections;
using UnityEngine;

public class FortuneWheelTop : GameSection
{
	private enum UI
	{
		LBL_GOLD_NUM,
		LBL_CRYSTAL_NUM,
		GRD_SERVER_LOG,
		GRD_REWARD_LOG,
		ITEM_ICON,
		JACKPOT_NUMBER,
		SPIN_TICKET_NUM,
		SPIN_ICON_POINT_GROUP
	}

	private JackportNumber jackportNumber;

	private SpinTicketNumber spinTicketNumber;

	private FortuneWheelSpinHandle spinHandle;

	private FortuneWheelData wheelData;

	private GameObject m_SpinItemPrefab;

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		jackportNumber = GetCtrl(UI.JACKPOT_NUMBER).GetComponent<JackportNumber>();
		spinTicketNumber = GetCtrl(UI.SPIN_TICKET_NUM).GetComponent<SpinTicketNumber>();
		spinHandle = GetCtrl(UI.SPIN_ICON_POINT_GROUP).GetComponent<FortuneWheelSpinHandle>();
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_quest_spinitem = load_queue.Load(RESOURCE_CATEGORY.UI, "JackpotSpinItem", false);
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
		}
		m_SpinItemPrefab = (lo_quest_spinitem.loadedObject as GameObject);
		bool wait = true;
		SendInfo(delegate
		{
			((_003CDoInitialize_003Ec__Iterator3B)/*Error near IL_0107: stateMachine*/)._003Cwait_003E__2 = false;
		});
		while (wait)
		{
			yield return (object)null;
		}
		SetLabelText(UI.LBL_CRYSTAL_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal.ToString("N0"));
		SetLabelText(UI.LBL_GOLD_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money.ToString("N0"));
		jackportNumber.ShowNumber(wheelData.vaultInfo.jackpot.ToString());
		spinTicketNumber.ShowNumber("769");
		base.Initialize();
	}

	public override void UpdateUI()
	{
		UpdateLog();
		spinHandle.IniSpin(wheelData.vaultInfo.itemList, m_SpinItemPrefab);
	}

	private void UpdateLog()
	{
		ReloadRewardLog();
		ReloadServerLog();
	}

	private void ReloadRewardLog()
	{
		SetGrid(UI.GRD_REWARD_LOG, "FortuneWheelRewardLogItem", 6, true, delegate(int i, Transform t, bool b)
		{
			Transform parent = FindCtrl(t, UI.ITEM_ICON);
			ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.MONEY, 1u, parent, -1, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
			if ((UnityEngine.Object)itemIcon != (UnityEngine.Object)null)
			{
				itemIcon.SetJackpotIcon();
			}
		});
	}

	private void ReloadServerLog()
	{
		SetGrid(UI.GRD_SERVER_LOG, "FortuneWheelServerLogItem", 6, true, delegate(int i, Transform t, bool b)
		{
			FortuneWheelServerLogItem component = t.GetComponent<FortuneWheelServerLogItem>();
			if (i == 4)
			{
				component.InitJackpot("13:15 [CCVN] MaxCmus has won Jackpot");
			}
			else
			{
				component.InitLog("13:15 [CCVN] MaxCmus has won 10x Rainbow", REWARD_TYPE.MONEY, 1u);
			}
		});
	}

	private void OnQuery_SPIN()
	{
		spinHandle.StartSpin(11);
	}

	public void SendInfo(Action<bool> call_back)
	{
		Protocol.Send(FortuneWheelHomeModel.URL, delegate(FortuneWheelHomeModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				wheelData = ret.result;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendSpin(Action<bool> call_back)
	{
		Protocol.Send(FortuneWheelSpinModel.URL, delegate(FortuneWheelSpinModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendHistory(Action<bool> call_back)
	{
		Protocol.Send(FortuneWheelHistoryModel.URL, delegate(FortuneWheelHistoryModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}
}
