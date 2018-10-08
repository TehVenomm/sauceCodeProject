public class SmithGrowResult : EquipResultBase
{
	private bool direction;

	public override void Initialize()
	{
		smithType = SmithType.GROW;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (!direction)
		{
			EquipItemInfo equipItemInfo = resultData.itemData as EquipItemInfo;
			if (resultData.isExceed && equipItemInfo != null && equipItemInfo.exceed > 0)
			{
				string[] descriptions = new string[1]
				{
					equipItemInfo.tableData.GetExceedParamName(equipItemInfo.exceed)
				};
				StartExceedDirection(descriptions);
			}
			else
			{
				OnFinishedAddAbilityDirection();
			}
			direction = true;
		}
		base.UpdateUI();
	}

	private void OnQuery_NEXT()
	{
		SmithManager.SmithGrowData smithGrowData = MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>();
		smithGrowData.selectEquipData = (resultData.itemData as EquipItemInfo);
	}

	private void OnQuery_NEXT_EVOLVE()
	{
		if (!MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("SmithGrowItemSelect"))
		{
			GameSection.SetEventData(new object[2]
			{
				SmithType.GROW,
				(resultData.itemData as EquipItemInfo).tableData.type
			});
		}
	}

	private void OnQuery_TO_SELECT()
	{
		if (!MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("SmithGrowItemSelect"))
		{
			GameSection.StopEvent();
			OnQuery_MAIN_MENU_STATUS();
		}
	}
}
