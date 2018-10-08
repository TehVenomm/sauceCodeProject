public class SmithCreateItemResult : EquipResultBase
{
	private bool direction;

	public override void Initialize()
	{
		smithType = SmithType.GENERATE;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (!direction)
		{
			EquipItemInfo equipItemInfo = resultData.itemData as EquipItemInfo;
			if (equipItemInfo.GetValidLotAbility() > 0)
			{
				StartAddAbilityDirection(equipItemInfo.GetValidAbility());
			}
			else
			{
				OnFinishedAddAbilityDirection();
			}
			direction = true;
		}
		base.UpdateUI();
	}
}
