using Network;
using System;
using UnityEngine;

public class UIAutoBattleButton : UIBehaviour
{
	protected Self self;

	private bool isAbleCountCycle;

	private bool cachedAutoFlg;

	private bool updateTimer;

	private bool btnEnable = true;

	private bool needUpdateUI;

	[SerializeField]
	private GameObject sprAutoOn;

	[SerializeField]
	private GameObject sprAutoOff;

	[SerializeField]
	private GameObject sprAutoPlay;

	[SerializeField]
	private GameObject sprAutoPause;

	[SerializeField]
	private UILabel lblAutoTime;

	[SerializeField]
	private BoxCollider btnCollider;

	private AutoModeStatus automodeStatus = new AutoModeStatus();

	private bool canUseAutoMode;

	public double stampCircle
	{
		get;
		private set;
	}

	private void SetupAutoButton(double timeLeft)
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		self = (MonoBehaviourSingleton<UIPlayerStatus>.I.targetPlayer as Self);
		if (timeLeft < 0.0)
		{
			timeLeft = 0.0;
		}
		Initialize(timeLeft, GameSaveData.instance.isAutoMode);
		if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.USER_CREATE_02) && !QuestManager.IsValidInGame() && automodeStatus.IsRemain())
		{
			canUseAutoMode = true;
		}
		if (self == null)
		{
			canUseAutoMode = false;
		}
		this.get_gameObject().SetActive(canUseAutoMode);
		if (canUseAutoMode)
		{
			if (GameSaveData.instance.isAutoMode)
			{
				if (!cachedAutoFlg)
				{
					StartAutoMode();
				}
			}
			else if (cachedAutoFlg)
			{
				StopAutoMode();
			}
			UpdateButton();
		}
		else if (GameSaveData.instance.isAutoMode)
		{
			if (cachedAutoFlg)
			{
				PauseAutoMode();
			}
		}
		else if (cachedAutoFlg)
		{
			StopAutoMode();
		}
	}

	private void Initialize(double second, bool timerState)
	{
		updateTimer = timerState;
		automodeStatus.Init(second);
		lblAutoTime.text = automodeStatus.GetRemainTime();
		resetStampCircle();
	}

	private void Update()
	{
		if (!cachedAutoFlg)
		{
			updateTimer = false;
		}
		if (updateTimer)
		{
			automodeStatus.SubTime((double)Time.get_deltaTime());
			lblAutoTime.text = automodeStatus.GetRemainTime();
			if (!automodeStatus.IsRemain())
			{
				PauseAutoMode();
			}
			if (isAbleCountCycle)
			{
				stampCircle -= (double)Time.get_deltaTime();
				if (stampCircle < 0.0)
				{
					isAbleCountCycle = false;
					AutoPlayTimestamp(delegate(bool b)
					{
						if (b)
						{
							resetStampCircle();
							isAbleCountCycle = true;
						}
					});
				}
			}
		}
	}

	private void UpdateButton()
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		if (cachedAutoFlg)
		{
			sprAutoOn.SetActive(false);
			sprAutoOff.SetActive(true);
			sprAutoPlay.SetActive(false);
			sprAutoPause.SetActive(true);
		}
		else
		{
			sprAutoOn.SetActive(true);
			sprAutoOff.SetActive(false);
			sprAutoPlay.SetActive(true);
			sprAutoPause.SetActive(false);
		}
		if (!automodeStatus.IsRemain())
		{
			canUseAutoMode = false;
		}
		this.get_gameObject().SetActive(canUseAutoMode);
	}

	private bool IsAuto()
	{
		return self.isAutoMode;
	}

	public void OnBtnClick()
	{
		if (IsAuto())
		{
			SoundManager.PlaySystemSE(SoundID.UISE.CANCEL, 1f);
			StopAutoMode();
		}
		else if (automodeStatus.IsRemain())
		{
			SoundManager.PlaySystemSE(SoundID.UISE.CLICK, 1f);
			StartAutoMode();
		}
		else
		{
			SoundManager.PlaySystemSE(SoundID.UISE.INVALID, 1f);
		}
	}

	private void ForcePauseAutoMode()
	{
		self.SwitchAutoBattle(false);
		cachedAutoFlg = false;
		UpdateButton();
	}

	private void ForceResumeAutoMode()
	{
		self.SwitchAutoBattle(true);
		cachedAutoFlg = true;
		updateTimer = true;
		UpdateButton();
	}

	private void PauseAutoMode()
	{
		self.SwitchAutoBattle(false);
		cachedAutoFlg = false;
		AutoPlayStopConn(delegate(bool is_success)
		{
			if (is_success)
			{
				UpdateButton();
			}
		});
	}

	private void StopAutoMode()
	{
		self.SwitchAutoBattle(false);
		GameSaveData.instance.isAutoMode = false;
		cachedAutoFlg = false;
		AutoPlayStopConn(delegate(bool is_success)
		{
			if (is_success)
			{
				UpdateButton();
			}
		});
	}

	private void StartAutoMode()
	{
		resetStampCircle();
		AutoPlayStartConn(delegate(bool is_success)
		{
			if (is_success)
			{
				self.SwitchAutoBattle(true);
				GameSaveData.instance.isAutoMode = true;
				cachedAutoFlg = true;
				updateTimer = true;
				UpdateButton();
			}
		});
	}

	public void AutoPlaySwitch(int playState, Action<bool> call_back)
	{
		AutoPlaySwitchModel.RequestSendForm requestSendForm = new AutoPlaySwitchModel.RequestSendForm();
		requestSendForm.type = playState;
		if (btnEnable)
		{
			if (btnCollider != null)
			{
				btnCollider.set_enabled(false);
			}
			btnEnable = false;
			Protocol.Send(AutoPlaySwitchModel.URL, requestSendForm, delegate(AutoPlaySwitchModel ret)
			{
				bool obj = false;
				if (ret.Error == Error.None)
				{
					obj = true;
					btnEnable = true;
					if (btnCollider != null)
					{
						btnCollider.set_enabled(true);
					}
					Initialize(ret.result.timeLeft, playState == 0);
				}
				call_back(obj);
			}, string.Empty);
		}
	}

	public void AutoPlayStartConn(Action<bool> call_back = null)
	{
		int playState = 0;
		AutoPlaySwitch(playState, delegate(bool b)
		{
			if (b)
			{
				isAbleCountCycle = true;
			}
			if (call_back != null)
			{
				call_back(b);
			}
		});
	}

	public void AutoPlayStopConn(Action<bool> call_back = null)
	{
		int playState = 1;
		isAbleCountCycle = false;
		AutoPlaySwitch(playState, delegate(bool b)
		{
			if (call_back != null)
			{
				call_back(b);
			}
		});
	}

	public void AutoPlayTimestamp(Action<bool> call_back)
	{
		AutoPlaySwitchModel.RequestSendForm requestSendForm = new AutoPlaySwitchModel.RequestSendForm();
		requestSendForm.type = 0;
		Protocol.Send(AutoPlayTimestampModel.URL, requestSendForm, delegate(AutoPlayTimestampModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
				if (ret.result.timeLeft == 0.0)
				{
					self.SwitchAutoBattle(false);
					GameSaveData.instance.isAutoMode = false;
					cachedAutoFlg = false;
					Initialize(0.0, false);
				}
				else if (needUpdateUI)
				{
					needUpdateUI = false;
					ForceResumeAutoMode();
					Initialize(ret.result.timeLeft, true);
				}
			}
			if (!flag)
			{
				needUpdateUI = true;
				ForcePauseAutoMode();
			}
			call_back(flag);
		}, string.Empty);
	}

	public void GetAutoPlayTime(Action<bool> call_back)
	{
		if (!TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.USER_CREATE_02) || QuestManager.IsValidInGame())
		{
			SetupAutoButton(0.0);
			call_back(true);
		}
		else
		{
			Protocol.Send(AutoPlayTimeModel.URL, null, delegate(AutoPlayTimeModel ret)
			{
				bool obj = false;
				if (ret.Error == Error.None)
				{
					obj = true;
					SetupAutoButton(ret.result.timeLeft);
				}
				call_back(obj);
			}, string.Empty);
		}
	}

	public void OnUseItem(double timeleft)
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		Initialize(timeleft, GameSaveData.instance.isAutoMode);
		if (automodeStatus.IsRemain())
		{
			if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.USER_CREATE_02) && !QuestManager.IsValidInGame())
			{
				canUseAutoMode = true;
			}
			if (canUseAutoMode)
			{
				if (GameSaveData.instance.isAutoMode)
				{
					if (!cachedAutoFlg)
					{
						StartAutoMode();
					}
				}
				else
				{
					UpdateButton();
				}
			}
			else
			{
				this.get_gameObject().SetActive(false);
			}
		}
	}

	public void EnableButton()
	{
		if (!btnEnable)
		{
			btnEnable = true;
		}
		if (!btnCollider.get_enabled())
		{
			btnCollider.set_enabled(true);
		}
	}

	public void DisableButton()
	{
		if (btnEnable)
		{
			btnEnable = false;
		}
		if (btnCollider.get_enabled())
		{
			btnCollider.set_enabled(false);
		}
	}

	private void resetStampCircle()
	{
		stampCircle = 10.0;
	}

	protected override void OnDestroy()
	{
		if (MonoBehaviourSingleton<InGameManager>.I.isQuestHappen)
		{
			if (cachedAutoFlg)
			{
				if (self != null)
				{
					self.SwitchAutoBattle(false);
				}
				cachedAutoFlg = false;
				AutoPlayForceStop();
			}
		}
		else if (cachedAutoFlg)
		{
			if (self != null)
			{
				self.SwitchAutoBattle(false);
			}
			GameSaveData.instance.isAutoMode = false;
			cachedAutoFlg = false;
			AutoPlayForceStop();
		}
		base.OnDestroy();
	}

	private void OnApplicationQuit()
	{
		if (cachedAutoFlg)
		{
			if (self != null)
			{
				self.SwitchAutoBattle(false);
			}
			GameSaveData.instance.isAutoMode = false;
			cachedAutoFlg = false;
			AutoPlayForceStop();
		}
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause && IsAuto())
		{
			StopAutoMode();
		}
	}

	public void AutoPlayForceStop()
	{
		AutoPlaySwitchModel.RequestSendForm requestSendForm = new AutoPlaySwitchModel.RequestSendForm();
		requestSendForm.type = 1;
		Protocol.Send<AutoPlaySwitchModel.RequestSendForm, AutoPlaySwitchModel>(AutoPlaySwitchModel.URL, requestSendForm, delegate
		{
		}, string.Empty);
	}
}
