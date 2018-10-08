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
	protected Color[] colors = (Color[])new Color[11];

	[SerializeField]
	protected UIStaticPanelChanger panelChange;

	protected bool onClicked;

	protected bool isDisableButtons;

	private Vector3 orgRetirePos;

	private TweenPosition retireTweenPos;

	public bool IsEnableButtonAll => !isDisableButtons;

	protected override void Awake()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		retireTweenPos = retireButton.GetComponent<TweenPosition>();
		orgRetirePos = retireTweenPos.to;
		this.get_gameObject().SetActive(false);
	}

	public void Initialize()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			if (!(self == null))
			{
				MonoBehaviourSingleton<InGameProgress>.I.CloseDialog();
				MonoBehaviourSingleton<InGameProgress>.I.SetDisableUIOpen(false);
				this.get_gameObject().SetActive(true);
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
					SetupContinueButton(isEnableContinue, num2, num);
				}
				continueButton.get_gameObject().SetActive(!flag);
				restartButton.get_gameObject().SetActive(flag);
				float rescueTime = self.rescueTime;
				if (continueCount != null)
				{
					continueCount.color = Color.get_white();
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
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		if (continueButton != null)
		{
			continueButton.isEnabled = isEnableContinue;
		}
		if (continueButtonLabel != null)
		{
			if (isEnableContinue)
			{
				continueButtonLabel.applyGradient = true;
				continueButtonLabel.gradientTop = colors[0];
				continueButtonLabel.gradientBottom = colors[1];
				continueButtonLabel.effectColor = colors[2];
				continueButtonLabel.color = Color.get_white();
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
		if (continueButtonCrystalNum != null)
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
		if (continueButtonIcon != null)
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
		if (continueButtonEffect != null)
		{
			continueButtonEffect.SetActive(isEnableContinue);
		}
	}

	private void SetupRestartButton(bool isEnableRestart)
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		if (restartButton != null)
		{
			restartButton.isEnabled = isEnableRestart;
		}
		if (continueButtonLabel != null)
		{
			if (isEnableRestart)
			{
				restartButtonLabel.applyGradient = true;
				restartButtonLabel.gradientTop = colors[0];
				restartButtonLabel.gradientBottom = colors[1];
				restartButtonLabel.effectColor = colors[2];
				restartButtonLabel.color = Color.get_white();
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
		if (panelChange != null)
		{
			panelChange.UnLock();
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (panelChange != null)
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
		if (self == null)
		{
			return false;
		}
		if (self.actionID != (Character.ACTION_ID)22)
		{
			return false;
		}
		if (!self.isDead)
		{
			return false;
		}
		if (self.continueTime <= 0f)
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
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		if (!CheckVisible())
		{
			MonoBehaviourSingleton<InGameProgress>.I.CloseDialog();
			this.get_gameObject().SetActive(false);
		}
		else
		{
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			if (!(self == null) && continueCount != null)
			{
				float num = self.rescueTime;
				if (num > 0f)
				{
					continueCount.text = StringTable.Format(STRING_CATEGORY.IN_GAME, 1000u, num.ToString("F2"));
					continueCount.color = Color.get_white();
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
					continueCount.color = Color.get_red();
				}
			}
		}
	}

	public void OnClickRetire()
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Expected O, but got Unknown
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			if (FieldManager.IsValidInGameNoBoss())
			{
				Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
				if (self != null && self.rescueTime > 0f)
				{
					MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickRetire", this.get_gameObject(), "RETIRE", StringTable.Get(STRING_CATEGORY.IN_GAME, 1008u), null, true);
				}
				else
				{
					MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickRetire", this.get_gameObject(), "RETIRE", StringTable.Get(STRING_CATEGORY.IN_GAME, 1009u), null, true);
				}
			}
			else
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickRetire", this.get_gameObject(), "RETIRE", null, null, true);
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
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickContinue", this.get_gameObject(), "CONTINUE", null, null, true);
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
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.isWaitContinueProtocol = false;
		}
		if (res)
		{
			OnContinueSelf();
			this.get_gameObject().SetActive(false);
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
			if (self != null && self.actionID == (Character.ACTION_ID)22 && self.continueTime <= 0f && MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
			}
		}
	}

	public void OnClickRestart()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("UIContinueButton.OnClickRestart", this.get_gameObject(), "RESTART", null, null, true);
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
					if (player != self)
					{
						int heal_hp = (int)((float)player.hpMax * continueHealRate);
						player.OnHealReceive(heal_hp, HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.BASIS, true);
					}
				}
			}
		}
	}

	public void SetDisableButtons(bool disable)
	{
		isDisableButtons = disable;
		if (retireButton != null)
		{
			retireButton.isEnabled = !disable;
		}
		if (continueButton != null)
		{
			continueButton.isEnabled = !disable;
		}
	}

	public void SetContinueButton(bool enable)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		if (enable)
		{
			continueButton.get_gameObject().SetActive(true);
			retireTweenPos.to = orgRetirePos;
		}
		else
		{
			continueButton.get_gameObject().SetActive(false);
			Vector3 to = retireTweenPos.to;
			to.x = 0f;
			retireTweenPos.to = to;
		}
	}
}
