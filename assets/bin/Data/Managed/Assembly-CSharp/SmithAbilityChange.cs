using Network;
using System;
using UnityEngine;

public class SmithAbilityChange : GameSection
{
	private enum UI
	{
		STR_TITLE_MONEY,
		STR_DECISION_REFLECT,
		STR_LIST_REFLECT,
		OBJ_ABILITY_LIST_ROOT,
		LBL_GOLD,
		BTN_USE_ABILITY_ITEM,
		BTN_DECISION,
		OBJ_NEED_MONEY
	}

	private enum BACK_TO
	{
		STATUS_TOP,
		STATUS_TOP_EQUIPDETAIL
	}

	private EquipItemInfo equipItemInfo;

	private AbilityChangeAbilityList abilityList;

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_USER_STATUS;
	}

	public override void Initialize()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		equipItemInfo = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData;
		Transform val = SetPrefab((Enum)UI.OBJ_ABILITY_LIST_ROOT, "AbilityChangeAbilityList");
		abilityList = val.get_gameObject().AddComponent<AbilityChangeAbilityList>();
		abilityList.uiVisible = true;
		SetLabelText((Enum)UI.STR_DECISION_REFLECT, base.sectionData.GetText("STR_DECISION"));
		SetLabelText((Enum)UI.STR_LIST_REFLECT, base.sectionData.GetText("STR_LIST"));
		base.Initialize();
	}

	public override void InitializeReopen()
	{
		equipItemInfo = (GameSection.GetEventData() as EquipItemInfo);
		base.InitializeReopen();
	}

	protected override void OnOpen()
	{
		abilityList.InitUI();
		abilityList.Open(UITransition.TYPE.OPEN);
		base.OnOpen();
	}

	public override void UpdateUI()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		SetFontStyle((Enum)UI.STR_TITLE_MONEY, 2);
		abilityList.SetParameter(equipItemInfo);
		int needMoney = GetNeedMoney();
		SetLabelText((Enum)UI.LBL_GOLD, needMoney.ToString());
		Color color = Color.get_white();
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < needMoney)
		{
			color = Color.get_red();
		}
		SetColor((Enum)UI.LBL_GOLD, color);
		bool is_visible = equipItemInfo.tableData.IsEquipableAbilityItem();
		SetActive((Enum)UI.BTN_USE_ABILITY_ITEM, is_visible);
		SetActive((Enum)UI.BTN_DECISION, abilityList.EnableAbilityChange);
		SetActive((Enum)UI.OBJ_NEED_MONEY, abilityList.EnableAbilityChange);
	}

	private int GetNeedMoney()
	{
		return MonoBehaviourSingleton<SmithManager>.I.GetAbilityChangeNeedMoney(equipItemInfo);
	}

	private void OnQuery_START()
	{
		SmithManager.ERR_SMITH_SEND eRR_SMITH_SEND = MonoBehaviourSingleton<SmithManager>.I.CheckAbilityChange(equipItemInfo);
		if (eRR_SMITH_SEND != 0)
		{
			GameSection.ChangeEvent(eRR_SMITH_SEND.ToString(), null);
		}
		else
		{
			GameSection.SetEventData(new string[1]
			{
				GetNeedMoney().ToString()
			});
		}
	}

	private unsafe void OnQuery_SmithConfirmAbilityChange_YES()
	{
		GameSection.StayEvent();
		SmithManager i = MonoBehaviourSingleton<SmithManager>.I;
		ulong uniqueID = equipItemInfo.uniqueID;
		if (_003C_003Ef__am_0024cache2 == null)
		{
			_003C_003Ef__am_0024cache2 = new Action<Error, EquipItemInfo>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		i.SendAbilityChangeEquipItem(uniqueID, _003C_003Ef__am_0024cache2);
	}

	private void OnQuery_SECTION_BACK()
	{
		if (!MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList().Exists((GameSectionHistory.HistoryData x) => x.sectionName == "SmithAbilityChangeSelect"))
		{
			GameSection.StopEvent();
			DispatchEvent("MAIN_MENU_STATUS", null);
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
}
