public class EquipItemAbility
{
	public uint id;

	public int ap;

	public EquipItemAbility(uint _id, int _ap)
	{
		id = _id;
		ap = _ap;
	}

	public string GetName()
	{
		AbilityTable.Ability ability = Singleton<AbilityTable>.I.GetAbility(id);
		if (ability == null)
		{
			return string.Empty;
		}
		return ability.name;
	}

	public string GetNameAndAP()
	{
		if (ap >= 0)
		{
			return $"{GetName()} +{ap}";
		}
		return $"{GetName()} {ap}";
	}

	public string GetAP()
	{
		if (ap >= 0)
		{
			return $"+{ap}";
		}
		return $"{ap}";
	}

	public string GetDescription()
	{
		AbilityDataTable.AbilityData abilityData = Singleton<AbilityDataTable>.I.GetAbilityData(id, ap);
		if (abilityData == null)
		{
			abilityData = Singleton<AbilityDataTable>.I.GetMinimumAbilityData(id);
		}
		return abilityData.description;
	}

	public bool IsNeedUpdate()
	{
		return Singleton<AbilityDataTable>.I.GetMinimumAbilityData(id)?.HasNeedUpdateAbility() ?? false;
	}

	public bool IsActiveAbility()
	{
		return Singleton<AbilityTable>.I.GetAbility(id)?.IsActive() ?? false;
	}

	public EquipItemAbility Inverse()
	{
		return new EquipItemAbility(id, -ap);
	}
}
