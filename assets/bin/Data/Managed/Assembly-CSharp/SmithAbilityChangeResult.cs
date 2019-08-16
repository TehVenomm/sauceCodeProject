using System;
using System.Collections;
using UnityEngine;

public class SmithAbilityChangeResult : GameSection
{
	private enum UI
	{
		TEX_MODEL,
		STR_NEXT_REFLECT,
		OBJ_DELAY_1,
		OBJ_DELAY_2,
		OBJ_ABILITY_LIST_ROOT,
		OBJ_DIRECTION_1,
		OBJ_DIRECTION_2,
		OBJ_DIRECTION_3,
		LBL_ADD_ABILITY_1,
		LBL_ADD_ABILITY_2,
		LBL_ADD_ABILITY_3,
		BTN_TAP_FULL_SCREEN
	}

	private EquipItemInfo equipItemInfo;

	private AbilityChangeAbilityList abilityList;

	private int endDirectionCount;

	private bool m_isFirstOpen;

	private bool isFirstTap = true;

	private bool isTimerStart;

	private float timer;

	private static readonly UI[] UiDirection = new UI[3]
	{
		UI.OBJ_DIRECTION_1,
		UI.OBJ_DIRECTION_2,
		UI.OBJ_DIRECTION_3
	};

	public override string overrideBackKeyEvent => "GO_BACK";

	public override void Initialize()
	{
		equipItemInfo = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData;
		Transform val = SetPrefab((Enum)UI.OBJ_ABILITY_LIST_ROOT, "AbilityChangeAbilityList");
		abilityList = val.get_gameObject().AddComponent<AbilityChangeAbilityList>();
		abilityList.uiVisible = true;
		SetRenderEquipModel((Enum)UI.TEX_MODEL, equipItemInfo.tableID, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex, -1, 1f);
		SetLabelText((Enum)UI.STR_NEXT_REFLECT, base.sectionData.GetText("STR_NEXT"));
		ResetTween((Enum)UI.OBJ_DELAY_2, 0);
		MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = false;
		SetFullScreenButton((Enum)UI.BTN_TAP_FULL_SCREEN);
		m_isFirstOpen = true;
		base.Initialize();
	}

	protected override void OnOpen()
	{
		endDirectionCount = 0;
		abilityList.InitUI();
		abilityList.Open();
		PlayTween((Enum)UI.OBJ_DELAY_1, forward: true, (EventDelegate.Callback)PlayDirection, is_input_block: true, 0);
		SetActive((Enum)UI.OBJ_DELAY_2, is_visible: false);
		base.OnOpen();
	}

	public override void UpdateUI()
	{
		abilityList.SetParameter(equipItemInfo);
	}

	private void EndAllDirection()
	{
		SetActive((Enum)UI.OBJ_DELAY_2, is_visible: true);
		ResetTween((Enum)UI.OBJ_DELAY_2, 0);
		PlayTween((Enum)UI.OBJ_DELAY_2, forward: true, (EventDelegate.Callback)null, is_input_block: true, 0);
		SetActive((Enum)UI.TEX_MODEL, is_visible: false);
		RefreshUI();
	}

	private void PlayDirection()
	{
		this.StartCoroutine(_PlayDirection());
	}

	private IEnumerator _PlayDirection()
	{
		UI[] ui_add_ability = new UI[3]
		{
			UI.LBL_ADD_ABILITY_1,
			UI.LBL_ADD_ABILITY_2,
			UI.LBL_ADD_ABILITY_3
		};
		EquipItemAbility[] abilities = equipItemInfo.GetLotteryAbility();
		for (int i = 0; i < abilities.Length; i++)
		{
			PlayDirection(UiDirection[i], ui_add_ability[i], abilities[i]);
			yield return (object)new WaitForSeconds(0.46f);
			if (m_isFirstOpen)
			{
				SoundManager.PlayOneShotUISE(40000049);
			}
		}
		isTimerStart = true;
		m_isFirstOpen = false;
	}

	private void PlayDirection(UI directionObj, UI label, EquipItemAbility ability)
	{
		SetFontStyle((Enum)label, 2);
		SetLabelText((Enum)label, ability.GetNameAndAP());
		PlayTween((Enum)directionObj, forward: true, (EventDelegate.Callback)EndDirection, is_input_block: false, 0);
	}

	private void EndDirection()
	{
		endDirectionCount++;
		if (endDirectionCount >= equipItemInfo.GetValidLotAbility())
		{
			EndAllDirection();
		}
	}

	private void OnQuery_NEXT()
	{
		SmithManager.ERR_SMITH_SEND eRR_SMITH_SEND = MonoBehaviourSingleton<SmithManager>.I.CheckAbilityChange(equipItemInfo);
		if (eRR_SMITH_SEND != 0)
		{
			GameSection.ChangeEvent(eRR_SMITH_SEND.ToString());
		}
		else
		{
			GameSection.SetEventData(new string[1]
			{
				MonoBehaviourSingleton<SmithManager>.I.GetAbilityChangeNeedMoney(equipItemInfo).ToString()
			});
		}
	}

	private void OnQuery_GO_BACK()
	{
		if (!MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList().Exists((GameSectionHistory.HistoryData x) => x.sectionName == "SmithAbilityChangeSelect"))
		{
			GameSection.StopEvent();
			DispatchEvent("MAIN_MENU_STATUS");
		}
	}

	private void OnQuery_SmithConfirmAbilityChangeRetry_YES()
	{
		EventData[] autoEvents = new EventData[3]
		{
			new EventData("RETRY", equipItemInfo),
			new EventData("SmithConfirmAbilityChange_YES", null),
			new EventData("START_FROM_RESULT", null)
		};
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	private void OnQuery_TAP_FULL_SCREEN()
	{
		EquipItemAbility[] lotteryAbility = equipItemInfo.GetLotteryAbility();
		if (isFirstTap)
		{
			for (int i = 0; i < lotteryAbility.Length; i++)
			{
				UITweenCtrl.SetDurationWithRate(GetCtrl(UiDirection[i]), 0.5f);
			}
		}
		float num = 0.5f;
		if (!isFirstTap)
		{
			num = 0.35f;
		}
		isFirstTap = false;
		if (!(timer <= num))
		{
			for (int j = 0; j < lotteryAbility.Length; j++)
			{
				SkipTween((Enum)UiDirection[j], forward: true, 0);
			}
		}
	}

	protected void OnQuery_LOTTERY_LIST()
	{
		EquipItemInfo selectEquipData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData;
		GameSection.SetEventData(new object[2]
		{
			selectEquipData,
			SmithEquipBase.SmithType.ABILITY_CHANGE
		});
	}

	private void Update()
	{
		if (isTimerStart)
		{
			timer += Time.get_deltaTime();
		}
	}
}
