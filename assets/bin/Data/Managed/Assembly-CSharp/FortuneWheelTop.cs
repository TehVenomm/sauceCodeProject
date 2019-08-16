using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FortuneWheelTop : GameSection
{
	private enum UI
	{
		LBL_GOLD_NUM,
		LBL_CRYSTAL_NUM,
		SCR_USER_REWARD,
		SCR_SERVER_REWARD,
		GRD_SERVER_LOG,
		GRD_REWARD_LOG,
		ITEM_ICON,
		JACKPOT_NUMBER,
		SPIN_TICKET_NUM,
		SPIN_ICON_POINT_GROUP,
		BTN_SPIN_X1_ENABLE,
		BTN_SPIN_X1_DISABLE,
		BTN_SPIN_X10_ENABLE,
		BTN_SPIN_X10_DISABLE,
		BTN_SPIN_X50_ENABLE,
		BTN_SPIN_X50_DISABLE,
		BTN_SPIN_X100_ENABLE,
		BTN_SPIN_X100_DISABLE,
		OBJ_COUNT_GROUP,
		OBJ_COUNTS,
		OBJ_COUNT_DAY_1,
		OBJ_COUNT_DAY_2,
		OBJ_COUNT_DAY_3,
		OBJ_COUNT_DAY_4,
		OBJ_COUNT_DAY_5,
		OBJ_COUNT_DAY_6,
		OBJ_COUNT_DAY_7,
		SPR_COUNT_DAY_ACTIVE,
		SPR_COUNT_DAY_INACTIVE,
		SPIN_UNLOCK_GROUP,
		SPIN_LOCK_GROUP,
		SPR_BAR_ACTIVE,
		SPR_FREE_TXT
	}

	private enum AUDIO
	{
		GEAR_STOP = 40000133,
		GEAR_SPIN = 10000079,
		REWARD_ADDED = 10000063
	}

	private const int MAX_DAY_ACTIVE = 7;

	private const float INTERVAL_UPDATE_TIME = 10f;

	private const int MAX_USER_REWARD_LOG = 30;

	private JackportNumber jackportNumber;

	private SpinTicketNumber spinTicketNumber;

	private FortuneWheelSpinHandle spinHandle;

	private List<FortuneWheelServerLog> serverLogList = new List<FortuneWheelServerLog>();

	private List<FortuneWheelUserLog> userLogList;

	private GameObject m_SpinItemPrefab;

	private FortuneWheelManager.SPIN_TYPE spinType;

	private bool isX10Spin;

	private int spinX10Count;

	private bool isUserSkip;

	private int spinMultiRewardAddedIndex;

	private int spinX10ServerAddedIndex;

	private bool isSpinning;

	private UISprite activeBar;

	private bool updatingView;

	private bool skipUpdate;

	private bool isAddItem;

	private float delay10Spin = 2f;

	private float delayMuliSpin = 0.7f;

	private int countMultipleSpin;

	private bool spinMultiple;

	private float timeUpdate;

	public override void Initialize()
	{
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		spinType = FortuneWheelManager.SPIN_TYPE.X1;
		jackportNumber = GetCtrl(UI.JACKPOT_NUMBER).GetComponent<JackportNumber>();
		spinTicketNumber = GetCtrl(UI.SPIN_TICKET_NUM).GetComponent<SpinTicketNumber>();
		spinHandle = GetCtrl(UI.SPIN_ICON_POINT_GROUP).GetComponent<FortuneWheelSpinHandle>();
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_quest_spinitem = load_queue.Load(RESOURCE_CATEGORY.UI, "JackpotSpinItem");
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
		}
		m_SpinItemPrefab = (lo_quest_spinitem.loadedObject as GameObject);
		bool wait = true;
		MonoBehaviourSingleton<FortuneWheelManager>.I.SendInfo(delegate(bool is_success)
		{
			wait = false;
			if (is_success)
			{
				serverLogList = new List<FortuneWheelServerLog>(MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.history.server);
				userLogList = new List<FortuneWheelUserLog>(MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.history.user);
				userLogList.Reverse();
			}
		});
		while (wait)
		{
			yield return null;
		}
		base.Initialize();
		yield return (object)new WaitForEndOfFrame();
		MonoBehaviourSingleton<FortuneWheelManager>.I.OnJackpotWin += OnJackpotWinHandler;
		MonoBehaviourSingleton<FortuneWheelManager>.I.OnRequestUpdateUI += OnRequestUpdateUIHandler;
		spinHandle.IniSpin(MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.itemList, m_SpinItemPrefab);
		SetWheelState(IsOpenWheel());
		SetButtonState(MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.freeSpin);
		SetSpinState();
		SoundManager.RequestBGM(13);
	}

	private void SetSpinState(bool force = false)
	{
		if (force)
		{
			SetActive((Enum)UI.SPR_FREE_TXT, force);
			SetActive((Enum)UI.SPIN_TICKET_NUM, !force);
		}
		else
		{
			SetActive((Enum)UI.SPR_FREE_TXT, MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.freeSpin);
			SetActive((Enum)UI.SPIN_TICKET_NUM, !MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.freeSpin);
		}
	}

	private void DebugLog(List<FortuneWheelUserLog> list)
	{
		foreach (FortuneWheelUserLog item in list)
		{
			Debug.Log((object)("item " + item.rewardString));
		}
	}

	private void OnRequestUpdateUIHandler()
	{
		this.StartCoroutine(RequestRefreshUI());
	}

	private void OnJackpotWinHandler(FortuneWheelManager.JackpotWinData data)
	{
		this.StartCoroutine(ShowJackpot(data));
	}

	private IEnumerator ShowJackpot(FortuneWheelManager.JackpotWinData data)
	{
		yield return (object)new WaitForSeconds(2f);
		while (isSpinning)
		{
			yield return null;
		}
		DispatchEvent("JACKPOT_WIN", data);
	}

	private IEnumerator RequestRefreshUI()
	{
		yield return (object)new WaitForEndOfFrame();
		RefreshUI();
	}

	private IEnumerator CloseWinJackpotDialog()
	{
		yield return (object)new WaitForSeconds(6f);
		GameSection.BackSection();
	}

	private void SetButtonState(bool isFree = false)
	{
		if (!isFree)
		{
			DisableAllButton();
			switch (spinType)
			{
			case FortuneWheelManager.SPIN_TYPE.X1:
				EnableSpinTypeButton(UI.BTN_SPIN_X1_ENABLE, UI.BTN_SPIN_X1_DISABLE);
				break;
			case FortuneWheelManager.SPIN_TYPE.X10:
				EnableSpinTypeButton(UI.BTN_SPIN_X10_ENABLE, UI.BTN_SPIN_X10_DISABLE);
				break;
			case FortuneWheelManager.SPIN_TYPE.X50:
				EnableSpinTypeButton(UI.BTN_SPIN_X50_ENABLE, UI.BTN_SPIN_X50_DISABLE);
				break;
			case FortuneWheelManager.SPIN_TYPE.X100:
				EnableSpinTypeButton(UI.BTN_SPIN_X100_ENABLE, UI.BTN_SPIN_X100_DISABLE);
				break;
			}
		}
		else
		{
			DisableAllButton();
		}
	}

	private void DisableAllButton()
	{
		SetActive((Enum)UI.BTN_SPIN_X1_ENABLE, is_visible: false);
		SetActive((Enum)UI.BTN_SPIN_X1_DISABLE, is_visible: true);
		SetActive((Enum)UI.BTN_SPIN_X10_ENABLE, is_visible: false);
		SetActive((Enum)UI.BTN_SPIN_X10_DISABLE, is_visible: true);
		SetActive((Enum)UI.BTN_SPIN_X50_ENABLE, is_visible: false);
		SetActive((Enum)UI.BTN_SPIN_X50_DISABLE, is_visible: true);
		SetActive((Enum)UI.BTN_SPIN_X100_ENABLE, is_visible: false);
		SetActive((Enum)UI.BTN_SPIN_X100_DISABLE, is_visible: true);
	}

	private void EnableSpinTypeButton(UI btnEnable, UI btnDisable)
	{
		SetActive((Enum)btnEnable, is_visible: true);
		SetActive((Enum)btnDisable, is_visible: false);
	}

	public override void UpdateUI()
	{
		SetLabelText((Enum)UI.LBL_CRYSTAL_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal.ToString("N0"));
		SetLabelText((Enum)UI.LBL_GOLD_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money.ToString("N0"));
		UpdateLog(isSkipAnim: true);
	}

	private bool IsOpenWheel()
	{
		return MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData != null && MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.loyaltyPoint >= MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.loyaltyPointRequired && MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.isOpen;
	}

	private void SetWheelState(bool isOpen)
	{
		SetActive((Enum)UI.SPIN_UNLOCK_GROUP, isOpen);
		SetActive((Enum)UI.SPIN_LOCK_GROUP, !isOpen);
		if (!isOpen)
		{
			SetActiveDay(MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.loyaltyPoint);
		}
	}

	private void SetActiveDay(int numDayActive)
	{
		for (int i = 0; i < 7; i++)
		{
			UI uI = (UI)Enum.Parse(typeof(UI), "OBJ_COUNT_DAY_" + (i + 1));
			Transform ctrl = GetCtrl(uI);
			if (i < numDayActive)
			{
				SetActive(ctrl.get_transform(), UI.SPR_COUNT_DAY_ACTIVE, is_visible: true);
				SetActive(ctrl.get_transform(), UI.SPR_COUNT_DAY_INACTIVE, is_visible: false);
			}
			else
			{
				SetActive(ctrl.get_transform(), UI.SPR_COUNT_DAY_ACTIVE, is_visible: false);
				SetActive(ctrl.get_transform(), UI.SPR_COUNT_DAY_INACTIVE, is_visible: true);
			}
		}
		SetActiveBarState(numDayActive);
	}

	private void SetActiveBarState(int numDayActive)
	{
		if (activeBar == null)
		{
			activeBar = GetCtrl(UI.SPR_BAR_ACTIVE).GetComponent<UISprite>();
		}
		activeBar.fillAmount = 355f / (678f * (float)Math.PI) * (float)(numDayActive - 1);
	}

	private IEnumerator IEUpdateView()
	{
		if (spinMultiRewardAddedIndex >= MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.spinRewards.Count)
		{
			yield break;
		}
		if (isUserSkip)
		{
			if (spinMultiRewardAddedIndex < MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.spinRewards.Count && spinType != FortuneWheelManager.SPIN_TYPE.X1)
			{
				for (int i = spinMultiRewardAddedIndex; i < MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.spinRewards.Count; i++)
				{
					FortuneWheelUserLog item = GenerateRewardItem();
					userLogList.Add(item);
					CheckWinJackpot(MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.spinRewards[spinMultiRewardAddedIndex]);
					spinMultiRewardAddedIndex++;
				}
				if (MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.history.server != null && MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.history.server.Count > 0)
				{
					serverLogList.InsertRange(0, MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.history.server);
					MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.history.server.Clear();
				}
				UpdateLog();
				updatingView = false;
			}
			yield break;
		}
		yield return (object)new WaitForSeconds(0.5f);
		yield return (object)new WaitForEndOfFrame();
		if (!isUserSkip)
		{
			FortuneWheelUserLog item2 = GenerateRewardItem();
			userLogList.Add(item2);
			isAddItem = true;
			CheckWinJackpot(MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.spinRewards[spinMultiRewardAddedIndex]);
			spinMultiRewardAddedIndex++;
			if (MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.history.server != null && MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.history.server.Count > 0 && ((spinType != FortuneWheelManager.SPIN_TYPE.X1 && countMultipleSpin == (int)spinType) || spinType == FortuneWheelManager.SPIN_TYPE.X1))
			{
				serverLogList.InsertRange(0, MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.history.server);
				MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.history.server.Clear();
			}
		}
		UpdateLog();
		updatingView = false;
	}

	private FortuneWheelUserLog GenerateRewardItem()
	{
		IEnumerable<FortuneWheelItem> source = from s in MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.vaultInfo.itemList
		where s.id == MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.spinRewards[spinMultiRewardAddedIndex].spinItemId
		select s;
		FortuneWheelUserLog fortuneWheelUserLog = new FortuneWheelUserLog();
		fortuneWheelUserLog.rewardId = source.First().rewardId;
		fortuneWheelUserLog.rewardType = source.First().rewardType;
		fortuneWheelUserLog.rewardNum = source.First().rewardNum;
		return fortuneWheelUserLog;
	}

	private void CheckWinJackpot(FortuneWheelReward data)
	{
		if (data.value >= 100)
		{
			string userId = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString();
			string name = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name;
			string jackpot = data.num.ToString();
			int percentage = data.percentage;
			skipUpdate = true;
			DispatchEvent("JACKPOT_WIN", new FortuneWheelManager.JackpotWinData(userId, jackpot, name, percentage));
		}
	}

	private void UpdateView()
	{
		this.StartCoroutine(IEUpdateView());
	}

	private void UpdateLog(bool isSkipAnim = false)
	{
		ReloadUserRewardLog(isSkipAnim);
		ReloadServerLog();
	}

	private void UpdateStat()
	{
		jackportNumber.ShowNumber(MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.jackpot.ToString());
		spinTicketNumber.ShowNumber(MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.curTicket.ToString());
	}

	private void ReloadUserRewardLog(bool isSkipAnim)
	{
		if (userLogList != null && userLogList.Count != 0)
		{
			if (isAddItem)
			{
				PlayAudio(AUDIO.REWARD_ADDED);
				isAddItem = false;
			}
			if (userLogList.Count > 30)
			{
				int count = userLogList.Count - 30;
				userLogList.RemoveRange(0, count);
			}
			SetGrid(UI.GRD_REWARD_LOG, "FortuneWheelRewardLogItem", userLogList.Count, reset: true, delegate(int i, Transform t, bool b)
			{
				//IL_008f: Unknown result type (might be due to invalid IL or missing references)
				//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
				FortuneWheelUserLog fortuneWheelUserLog = userLogList[i];
				FortuneWheelRewardLogItem component2 = t.GetComponent<FortuneWheelRewardLogItem>();
				if (fortuneWheelUserLog.rewardType == 550)
				{
					component2.InitJackpot();
				}
				else
				{
					component2.InitLog((REWARD_TYPE)fortuneWheelUserLog.rewardType, (uint)fortuneWheelUserLog.rewardId, fortuneWheelUserLog.rewardNum);
				}
				if (!isSkipAnim && !isUserSkip)
				{
					if (i == userLogList.Count - 1)
					{
						t.set_localScale(new Vector3(0.5f, 0.5f, 0.5f));
						PlayTween(t, forward: true, delegate
						{
						}, is_input_block: false);
					}
				}
				else
				{
					t.set_localScale(Vector3.get_one());
				}
			});
			UIScrollView component = GetCtrl(UI.SCR_USER_REWARD).GetComponent<UIScrollView>();
			if (component.CurrentFit())
			{
				component.SetDragAmount(1f, 0f, updateScrollbars: false);
			}
		}
	}

	private void ReloadServerLog()
	{
		if (serverLogList != null && serverLogList.Count != 0)
		{
			SetGrid(UI.GRD_SERVER_LOG, "FortuneWheelServerLogItem", serverLogList.Count, reset: true, delegate(int i, Transform t, bool b)
			{
				FortuneWheelServerLog fortuneWheelServerLog = serverLogList[i];
				FortuneWheelServerLogItem component = t.GetComponent<FortuneWheelServerLogItem>();
				string serverLogString = GetServerLogString(fortuneWheelServerLog);
				if (fortuneWheelServerLog.rewardType == 550)
				{
					component.InitJackpot(serverLogString);
				}
				else
				{
					component.InitLog(serverLogString, (REWARD_TYPE)fortuneWheelServerLog.rewardType, (uint)fortuneWheelServerLog.rewardId);
				}
			});
		}
	}

	private string GetServerLogString(FortuneWheelServerLog data)
	{
		DateTime dateTime = DateTime.Parse(data.createdDate).ToLocalTime();
		string arg = $"{dateTime.Hour}:{dateTime.Minute}:{dateTime.Second}";
		return string.Format(StringTable.Get(STRING_CATEGORY.DRAGON_VAULT, 2u), arg, data.userName, data.rewardString);
	}

	private void OnQuery_SPIN()
	{
		if (!isSpinning && countMultipleSpin == 0)
		{
			isUserSkip = false;
			if (IsSpinAvailable(spinType))
			{
				HandleSpin();
			}
			else
			{
				GameSection.ChangeEvent("CONFIRM_BUY_JACKPOT", base.sectionData.GetText("STR_CONFIRM_BUY"));
			}
		}
		else if (spinType != FortuneWheelManager.SPIN_TYPE.X1)
		{
			GameSection.ChangeEvent("SKIP_SPIN_X10", "Skip?");
		}
	}

	private void OnQuery_SPIN_X1()
	{
		SetSpinType(FortuneWheelManager.SPIN_TYPE.X1);
	}

	private void OnQuery_SPIN_X10()
	{
		SetSpinType(FortuneWheelManager.SPIN_TYPE.X10);
	}

	private void OnQuery_SPIN_X50()
	{
		SetSpinType(FortuneWheelManager.SPIN_TYPE.X50);
	}

	private void OnQuery_SPIN_X100()
	{
		SetSpinType(FortuneWheelManager.SPIN_TYPE.X100);
	}

	private void SetSpinType(FortuneWheelManager.SPIN_TYPE spinType)
	{
		if (!MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.freeSpin)
		{
			if (!isSpinning && !spinMultiple)
			{
				Debug.Log((object)("Change Spintype = " + spinType));
				this.spinType = spinType;
				SetButtonState();
			}
			else if (this.spinType != FortuneWheelManager.SPIN_TYPE.X1)
			{
				GameSection.ChangeEvent("SKIP_SPIN_X10", "Skip?");
			}
		}
	}

	private void OnQuery_SkipX10Dialog_YES()
	{
		if (spinType != FortuneWheelManager.SPIN_TYPE.X1)
		{
			spinHandle.Skip();
			this.StopCoroutine(IESpinMultiIMP(MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData));
			isUserSkip = true;
			isSpinning = false;
			UpdateView();
		}
	}

	private void OnQuery_DETAIL_JACKPOT()
	{
		GameSection.SetEventData(WebViewManager.FortuneWheel);
	}

	private IEnumerator IEBack()
	{
		yield return (object)new WaitForSeconds(2f);
		GameSection.ChangeEvent("OK");
	}

	private bool IsX10Available()
	{
		return MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.curTicket >= 10;
	}

	private bool IsSpinAvailable(FortuneWheelManager.SPIN_TYPE type)
	{
		return MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.curTicket >= (int)type;
	}

	private bool IsX1Available()
	{
		return MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.curTicket >= 1;
	}

	private void IncrestCount()
	{
		spinX10Count++;
	}

	private void Spin(FortuneWheelManager.SPIN_TYPE spinType, Action endSpinAct = null)
	{
		if (!isSpinning)
		{
			Protocol.Force(delegate
			{
				MonoBehaviourSingleton<FortuneWheelManager>.I.SendSpin(spinType, delegate(bool success)
				{
					if (success)
					{
						spinHandle.StartSpin(MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData, spinType, delegate(bool b)
						{
							isSpinning = b;
							if (!isSpinning)
							{
								if (endSpinAct != null)
								{
									endSpinAct();
								}
								UpdateView();
							}
							PlayAudio(AUDIO.GEAR_STOP);
						});
						UpdateAfterSpin();
						PlayAudio(AUDIO.GEAR_SPIN);
					}
				});
			});
		}
	}

	private void UpdateAfterSpin()
	{
		MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.curTicket = MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData.vaultInfo.curTicket;
		UpdateStat();
		SetButtonState(MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.freeSpin);
		SetSpinState();
	}

	private void HandleSpin()
	{
		if (spinType == FortuneWheelManager.SPIN_TYPE.X1)
		{
			spinMultiRewardAddedIndex = 0;
			Spin(FortuneWheelManager.SPIN_TYPE.X1);
		}
		else if (IsSpinAvailable(spinType))
		{
			countMultipleSpin = 0;
			spinMultiRewardAddedIndex = 0;
			HandleSpinMulti();
		}
		else
		{
			GameSection.ChangeEvent("CAN_NOT_SPIN_X10", base.sectionData.GetText("STR_SPIN_X10_ERROR_MESSAGE"));
			isX10Spin = false;
			spinType = FortuneWheelManager.SPIN_TYPE.X1;
			SetButtonState();
		}
	}

	private void HandleSpinMulti()
	{
		if (!isSpinning)
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<FortuneWheelManager>.I.SendSpin(spinType, delegate(bool success)
			{
				if (success)
				{
					this.StartCoroutine(IESpinMultiIMP(MonoBehaviourSingleton<FortuneWheelManager>.I.SpinData));
				}
				UpdateAfterSpin();
				GameSection.ResumeEvent(is_resume: true);
			});
		}
	}

	private IEnumerator IESpinMultiIMP(FortuneWheelData data)
	{
		spinMultiple = true;
		while (countMultipleSpin < (int)spinType && !isUserSkip)
		{
			while (isSpinning || updatingView)
			{
				yield return null;
			}
			if (countMultipleSpin >= (int)spinType || isUserSkip)
			{
				break;
			}
			SpinMulti(data, countMultipleSpin, SpinMultiEndAct);
			yield return (object)new WaitForSeconds((spinType != FortuneWheelManager.SPIN_TYPE.X10) ? delayMuliSpin : delay10Spin);
		}
		countMultipleSpin = 0;
		spinMultiple = false;
	}

	private void SpinMultiEndAct(bool b)
	{
		isSpinning = b;
		if (!isSpinning && !isUserSkip)
		{
			countMultipleSpin++;
		}
		else if (isUserSkip)
		{
			countMultipleSpin = 0;
		}
	}

	private void SpinMulti(FortuneWheelData data, int rewardIndex, Action<bool> endSpinAct)
	{
		updatingView = true;
		spinHandle.StartSpin(data, spinType, delegate(bool b)
		{
			isSpinning = b;
			if (!isSpinning)
			{
				if (endSpinAct != null)
				{
					endSpinAct(b);
				}
				UpdateView();
			}
			PlayAudio(AUDIO.GEAR_STOP);
		}, rewardIndex);
		PlayAudio(AUDIO.GEAR_SPIN);
	}

	private void Update()
	{
		timeUpdate += Time.get_deltaTime();
		if (timeUpdate >= 10f)
		{
			timeUpdate = 0f;
			if (!isSpinning && countMultipleSpin == 0 && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "FortuneWheelTop" && !skipUpdate)
			{
				Protocol.Force(delegate
				{
					MonoBehaviourSingleton<FortuneWheelManager>.I.UpdateData(delegate(bool success)
					{
						if (success)
						{
							UpdateJackpot();
						}
					});
				});
			}
			skipUpdate = false;
		}
	}

	private void UpdateJackpot()
	{
		this.StartCoroutine(IEUpdateJackpot());
	}

	private IEnumerator IEUpdateJackpot()
	{
		yield return (object)new WaitForEndOfFrame();
		jackportNumber.ShowNumber(MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.jackpot.ToString());
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.CHANGED_SCENE) != (NOTIFY_FLAG)0L)
		{
			SetLabelText((Enum)UI.LBL_CRYSTAL_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal.ToString("N0"));
			SetLabelText((Enum)UI.LBL_GOLD_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money.ToString("N0"));
			UpdateStat();
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (MonoBehaviourSingleton<FortuneWheelManager>.I != null)
		{
			MonoBehaviourSingleton<FortuneWheelManager>.I.OnJackpotWin -= OnJackpotWinHandler;
			MonoBehaviourSingleton<FortuneWheelManager>.I.OnRequestUpdateUI -= OnRequestUpdateUIHandler;
		}
		SoundManager.RequestBGM(2);
	}
}
