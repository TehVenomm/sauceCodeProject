using Network;
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
		equipItemInfo = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData;
		Transform transform = SetPrefab(UI.OBJ_ABILITY_LIST_ROOT, "AbilityChangeAbilityList");
		abilityList = transform.gameObject.AddComponent<AbilityChangeAbilityList>();
		abilityList.uiVisible = true;
		SetLabelText(UI.STR_DECISION_REFLECT, base.sectionData.GetText("STR_DECISION"));
		SetLabelText(UI.STR_LIST_REFLECT, base.sectionData.GetText("STR_LIST"));
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
		SetFontStyle(UI.STR_TITLE_MONEY, FontStyle.Italic);
		abilityList.SetParameter(equipItemInfo);
		int needMoney = GetNeedMoney();
		SetLabelText(UI.LBL_GOLD, needMoney.ToString());
		Color color = Color.white;
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < needMoney)
		{
			color = Color.red;
		}
		SetColor(UI.LBL_GOLD, color);
		bool is_visible = equipItemInfo.tableData.IsEquipableAbilityItem();
		SetActive(UI.BTN_USE_ABILITY_ITEM, is_visible);
		SetActive(UI.BTN_DECISION, abilityList.EnableAbilityChange);
		SetActive(UI.OBJ_NEED_MONEY, abilityList.EnableAbilityChange);
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

	private void OnQuery_SmithConfirmAbilityChange_YES()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<SmithManager>.I.SendAbilityChangeEquipItem(equipItemInfo.uniqueID, delegate(Error error, EquipItemInfo model)
		{
			if (error == Error.None)
			{
				SmithManager.SmithGrowData smithGrowData = MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>();
				smithGrowData.selectEquipData = model;
				MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = true;
				GameSection.ResumeEvent(true, null);
			}
			else
			{
				GameSection.ResumeEvent(false, null);
			}
		});
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
