using System;

public class EquipItemAbilityCollection
{
	public class SwapData
	{
		public int index;

		public EquipItemInfo item;

		public SwapData(int _index, EquipItemInfo _item)
		{
			index = _index;
			item = _item;
		}
	}

	public enum COLLECTION_TYPE
	{
		NORMAL,
		SWAP_IN,
		SWAP_OUT
	}

	private static int EQUIP_LENGTH;

	public EquipItemAbility ability
	{
		get;
		private set;
	}

	public int[] equip
	{
		get;
		private set;
	}

	public int[] swapValue
	{
		get;
		private set;
	}

	public EquipItemAbilityCollection(EquipItemAbility a, int equip_index, COLLECTION_TYPE type = COLLECTION_TYPE.NORMAL)
	{
		if (EQUIP_LENGTH == 0)
		{
			EQUIP_LENGTH = 3;
			EQUIPMENT_TYPE[] array = (EQUIPMENT_TYPE[])Enum.GetValues(typeof(EQUIPMENT_TYPE));
			int i = 0;
			for (int num = array.Length; i < num; i++)
			{
				int num2 = (int)array[i];
				if (num2 >= 100 && num2 <= 400)
				{
					EQUIP_LENGTH++;
				}
			}
		}
		equip = new int[EQUIP_LENGTH];
		swapValue = new int[EQUIP_LENGTH];
		if (type != 0)
		{
			swapValue[equip_index] += a.ap;
		}
		if (type != COLLECTION_TYPE.SWAP_OUT && equip_index < EQUIP_LENGTH && equip_index >= 0)
		{
			equip[equip_index] += a.ap;
			ability = new EquipItemAbility(a.id, a.ap);
		}
		else
		{
			ability = new EquipItemAbility(a.id, 0);
		}
	}

	public void Add(int ad_ap, int equip_index, COLLECTION_TYPE type = COLLECTION_TYPE.NORMAL)
	{
		if (type != 0)
		{
			swapValue[equip_index] += ad_ap;
		}
		if (type != COLLECTION_TYPE.SWAP_OUT)
		{
			ability.ap += ad_ap;
			equip[equip_index] += ad_ap;
		}
	}

	public string GetAP(int index)
	{
		if (index >= EQUIP_LENGTH || index < 0)
		{
			return string.Empty;
		}
		if (equip[index] == 0 && swapValue[index] == 0)
		{
			return string.Empty;
		}
		if (swapValue[index] != 0 && equip[index] == 0)
		{
			return "-";
		}
		int num = equip[index];
		return string.Format((num < 0) ? "{0}" : "+{0}", num);
	}

	public int GetSwapBalance()
	{
		int total = 0;
		Array.ForEach(swapValue, delegate(int num)
		{
			total += num;
		});
		return total;
	}

	public bool IsAbilityOn()
	{
		AbilityDataTable.AbilityData abilityData = Singleton<AbilityDataTable>.I.GetAbilityData(this.ability.id, this.ability.ap);
		if (abilityData == null)
		{
			return false;
		}
		AbilityTable.Ability ability = Singleton<AbilityTable>.I.GetAbility(this.ability.id);
		return !abilityData.HasNeedUpdateAbility() && ability.IsActive();
	}
}
