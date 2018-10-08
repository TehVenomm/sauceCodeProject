using Network;
using System.Collections.Generic;
using UnityEngine;

public class UIContinueButton : MonoBehaviourSingleton<UIContinueButton>
{
	protected enum COLOR_SET
	{
		CONTINUE_TOP,
		CONTINUE_BOTTOM,
		CONTINUE_ENABLE_EFF,
		CONTINUE_DISABLE,
		CONTINUE_DISABLE_EFF,
		CRYSTAL_ENABLE,
		CRYSTAL_ENABLE_EFF,
		CRYSTAL_DISABLE,
		CRYSTAL_DISABLE_EFF,
		ICON_ENABLE,
		ICON_DISABLE,
		MAX
	}

	[SerializeField]
	protected UIButton retireButton;

	[SerializeField]
	protected UIButton restartButton;

	[SerializeField]
	protected UILabel restartButtonLabel;

	[SerializeField]
	protected UIButton continueButton;

	[SerializeField]
	protected UILabel continueButtonLabel;

	[SerializeField]
	protected UILabel continueButtonCrystalNum;

	[SerializeField]
	protected UISprite continueButtonIcon;

	[SerializeField]
	protected GameObject continueButtonEffect;

	[SerializeField]
	protected UILabel continueCount;

	[SerializeField]
	protected TweenPosition[] startAction;

	[SerializeField]
	protected Color[] colors = new Color[11];

	[SerializeField]
	protected UIStaticPanelChanger panelChange;

	protected bool onClicked;

	protected bool isDisableButtons;

	private Vector3 orgRetirePos;

	private TweenPosition retireTweenPos;

	public bool IsEnableButtonAll => !isDisableButtons;

	protected override void Awake()
	{
		base.Awake();
		retireTweenPos = retireButton.GetComponent<TweenPosition>();
		orgRetirePos = retireTweenPos.to;
		base.gameObject.SetActive(false);
	}

	public void Initialize()
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			if (!((Object)self == (Object)null))
			{
				MonoBehaviourSingleton<InGameProgress>.I.CloseDialog();
				MonoBehaviourSingleton<InGameProgress>.I.SetDisableUIOpen(false);
				base.gameObject.SetActive(true);
				int num = 0;
				int num2 = 0;
				if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
				{
					num = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
					num2 = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.QUEST_CONTINUE_USE_CRYSTAL;
					if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsTutorialCurrentQuest())
					{
						num2 = 0;
					}
				}
				bool isEnableContinue = num >= num2;
				bool flag = QuestManager.IsValidInGameExplore();
				bool flag2 = false;
				if (flag)
				{
					isEnableContinue = false;
					flag2 = MonoBehaviourSingleton<StageObjectManager>.I.self.IsAbleToRescueByRemainRescueTime();
					SetupRestartButton(flag2);
				}
				else
				{
					if (MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest())
					{
						isEnableContinue = false;
					}
					SetupContinueButton(isEnableContinue, num2, num);
				}
				continueButton.gameObject.SetActive(!flag);
				restartButton.gameObject.SetActive(flag);
				float rescueTime = self.rescueTime;
				if ((Object)continueCount != (Object)null)
				{
					continueCount.color = Color.white;
					continueCount.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 1000u, rescueTime.ToString("F2"));
				}
				int i = 0;
				for (int num3 = startAction.Length; i < num3; i++)
				{
					startAction[i].ResetToBeginning();
					startAction[i].PlayForward();
				}
			}
		}
	}

	private void SetupContinueButton(bool isEnableContinue, int crystal_use, int crystal_num)
	{
		if ((Object)continueButton != (Object)null)
		{
			continueButton.isEnabled = isEnableContinue;
		}
		if ((Object)continueButtonLabel != (Object)null)
		{
			if (isEnableContinue)
			{
				continueButtonLabel.applyGradient = true;
				continueButtonLabel.gradientTop = colors[0];
				continueButtonLabel.gradientBottom = colors[1];
				continueButtonLabel.effectColor = colors[2];
				continueButtonLabel.color = Color.white;
			}
			else
			{
				continueButtonLabel.applyGradient = false;
				continueButtonLabel.color = colors[3];
				continueButtonLabel.effectColor = colors[4];
			}
			if (crystal_use <= 0)
			{
				continueButtonLabel.text = StringTable.Get(STRING_CATEGORY.IN_GAME, 1006u);
			}
			else
			{
				continueButtonLabel.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 1003u, crystal_use);
			}
		}
		if ((Object)continueButtonCrystalNum != (Object)null)
		{
			continueButtonCrystalNum.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 1004u, crystal_num);
			if (isEnableContinue)
			{
				continueButtonCrystalNum.color = colors[5];
				continueButtonCrystalNum.effectColor = colors[6];
			}
			else
			{
				continueButtonCrystalNum.color = colors[7];
				continueButtonCrystalNum.effectColor = colors[8];
			}
		}
		if ((Object)continueButtonIcon != (Object)null)
		{
			if (isEnableContinue)
			{
				continueButtonIcon.color = colors[9];
			}
			else
			{
				continueButtonIcon.color = colors[10];
			}
		}
		if ((Object)continueButtonEffect != (Object)null)
		{
			continueButtonEffect.SetActive(isEnableContinue);
		}
	}

	private void SetupRestartButton(bool isEnableRestart)
	{
		if ((Object)restartButton != (Object)null)
		{
			restartButton.isEnabled = isEnableRestart;
		}
		if ((Object)continueButtonLabel != (Object)null)
		{
			if (isEnableRestart)
			{
				restartButtonLabel.applyGradient = true;
				restartButtonLabel.gradientTop = colors[0];
				restartButtonLabel.gradientBottom = colors[1];
				restartButtonLabel.effectColor = colors[2];
				restartButtonLabel.color = Color.white;
			}
			else
			{
				restartButtonLabel.applyGradient = false;
				restartButtonLabel.color = colors[3];
				restartButtonLabel.effectColor = colors[4];
			}
		}
	}

	private void OnEnable()
	{
		if ((Object)panelChange != (Object)null)
		{
			panelChange.UnLock();
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if ((Object)panelChange != (Object)null)
		{
			panelChange.Lock();
		}
	}

	public bool CheckVisible()
	{
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || !MonoBehaviourSingleton<InGameProgress>.I.isBattleStart || MonoBehaviourSingleton<InGameProgress>.I.progressEndType != 0)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return false;
		}
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if ((Object)self == (Object)null)
		{
			return false;
		}
		if (self.actionID != (Character.ACTION_ID)23)
		{
			return false;
		}
		if (!self.isDead)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest() && self.continueTime <= 0f)
		{
			return false;
		}
		if (QuestManager.IsValidInGameExplore() && !MonoBehaviourSingleton<StageObjectManager>.I.self.IsAbleToRescueByRemainRescueTime())
		{
			DoRetire();
			return false;
		}
		return true;
	}

	private void Update()
	{
		if (!CheckVisible())
		{
			MonoBehaviourSingleton<InGameProgress>.I.CloseDialog();
			base.gameObject.SetActive(false);
		}
		else
		{
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			if (!((Object)self == (Object)null))
			{
				if (IsContiueable())
				{
					float num = self.rescueTime;
					if (num > 0f)
					{
						continueCount.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 1000u, num.ToString("F2"));
						continueCount.color = Color.white;
					}
					else
					{
						if (QuestManager.IsValidInGameExplore())
						{
							if (MonoBehaviourSingleton<StageObjectManager>.I.self.IsAbleToRescueByRemainRescueTime())
							{
								DoRestart();
							}
							else
							{
								DoRetire();
							}
						}
						num = self.continueTime;
						continueCount.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 1001u, num.ToString("F2"));
						continueCount.color = Color.red;
					}
				}
				else
				{
					continueCount.text = string.Empty;
				}
			}
		}
	}

	protected bool IsContiueable()
	{
		return (Object)continueCount != (Object)null && !MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest();
	}

	public void OnClickRetire()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			if (FieldManager.IsValidInGameNoBoss())
			{
				Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
				if ((Object)self != (Object)null && self.rescueTime > 0f && !MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest())
				{
					MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickRetire", base.gameObject, "RETIRE", StringTable.Get(STRING_CATEGORY.IN_GAME, 1008u), null, true);
				}
				else
				{
					MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickRetire", base.gameObject, "RETIRE", StringTable.Get(STRING_CATEGORY.IN_GAME, 1009u), null, true);
				}
			}
			else
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickRetire", base.gameObject, "RETIRE", null, null, true);
			}
		}
	}

	public void DoRetire()
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
		}
	}

	public void DoRetry()
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.BattleRetry();
		}
	}

	public void OnClickContinue()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickContinue", base.gameObject, "CONTINUE", null, null, true);
		}
	}

	public void DoContinue()
	{
		if (CheckVisible())
		{
			continueButton.isEnabled = false;
			retireButton.isEnabled = false;
			MonoBehaviourSingleton<InGameProgress>.I.isWaitContinueProtocol = true;
			if (QuestManager.IsValidInGame())
			{
				MonoBehaviourSingleton<QuestManager>.I.SendQuestContinue(CallBackContinue);
			}
			else
			{
				MonoBehaviourSingleton<FieldManager>.I.SendFieldContinue(CallBackContinue);
			}
		}
	}

	private void CallBackContinue(bool res, Error error)
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.isWaitContinueProtocol = false;
		}
		if (res)
		{
			OnContinueSelf();
			base.gameObject.SetActive(false);
		}
		else
		{
			continueButton.isEnabled = true;
			retireButton.isEnabled = true;
			string empty = string.Empty;
			empty = StringTable.Format(STRING_CATEGORY.COMMON_DIALOG, 1001u, (int)error);
			if (!string.IsNullOrEmpty(empty))
			{
				UIInGamePopupDialog.PushOpen(empty, true, 1.8f);
			}
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			if ((Object)self != (Object)null && self.actionID == (Character.ACTION_ID)23 && self.continueTime <= 0f && MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
			}
		}
	}

	public void OnClickRestart()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickRestart", base.gameObject, "RESTART", null, null, true);
		}
	}

	public void DoRestart()
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			int exploreStartMapId = MonoBehaviourSingleton<QuestManager>.I.GetExploreStartMapId();
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)exploreStartMapId);
			if (fieldMapData == null || fieldMapData.jumpPortalID == 0)
			{
				Log.Error("RegionMap.OnQuery_SELECT() jumpPortalID is not found.");
			}
			else
			{
				MonoBehaviourSingleton<StageObjectManager>.I.self.RestartExplore();
				if (MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap() && MonoBehaviourSingleton<CoopManager>.I.isStageHost)
				{
					Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
					MonoBehaviourSingleton<QuestManager>.I.UpdateExploreBossStatus(boss);
					MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendSyncExploreBoss(MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus(), -1);
				}
				MonoBehaviourSingleton<InGameProgress>.I.PortalNext(fieldMapData.jumpPortalID);
				MonoBehaviourSingleton<FieldManager>.I.useFastTravel = true;
			}
		}
	}

	private void OnContinueSelf()
	{
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		int hpMax = self.hpMax;
		self.ActDeadStandup(hpMax, Player.eContinueType.CONTINUE);
		if (QuestManager.IsValidInGame())
		{
			float continueHealRate = MonoBehaviourSingleton<InGameSettingsManager>.I.player.continueHealRate;
			if (continueHealRate > 0f)
			{
				List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
				int i = 0;
				for (int count = playerList.Count; i < count; i++)
				{
					Player player = playerList[i] as Player;
					if ((Object)player != (Object)self)
					{
						Character.HealData healData = new Character.HealData(Mathf.FloorToInt((float)player.hpMax * continueHealRate), HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.BASIS, null);
						player.OnHealReceive(healData);
					}
				}
			}
		}
	}

	public void SetDisableButtons(bool disable)
	{
		isDisableButtons = disable;
		if ((Object)retireButton != (Object)null)
		{
			retireButton.isEnabled = !disable;
		}
		if ((Object)continueButton != (Object)null)
		{
			continueButton.isEnabled = !disable;
		}
	}

	public void SetContinueButton(bool enable)
	{
		if (enable)
		{
			continueButton.gameObject.SetActive(true);
			retireTweenPos.to = orgRetirePos;
		}
		else
		{
			continueButton.gameObject.SetActive(false);
			Vector3 to = retireTweenPos.to;
			to.x = 0f;
			retireTweenPos.to = to;
		}
	}
}
