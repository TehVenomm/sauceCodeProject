using System;

public class AbilityItemLotTable : Singleton<AbilityItemLotTable>, IDataTable
{
	public class AbilityItemLot
	{
		public uint id;

		public uint itemId;

		public ABILITY_TYPE abilityType;

		public string target;

		public string spTarget;

		public string spAttackType;

		public int unlockEventId;

		public string format;

		public int minValue;

		public int maxValue;

		public int lotGroup;

		public int rate;

		public const string NT = "id,itemId,abilityType,target,spTarget,spAttackType,unlockEventId,format,minValue,maxValue,lotGroup,rate";

		public static bool cb(CSVReader csv_reader, AbilityItemLot data, ref uint key)
		{
			data.id = key;
			csv_reader.Pop(ref data.itemId);
			if (!CSVReader.PopResult.IsParseSucceeded(csv_reader.PopEnum(ref data.abilityType, ABILITY_TYPE.NONE)))
			{
				data.abilityType = ABILITY_TYPE.NEED_UPDATE;
			}
			csv_reader.Pop(ref data.target);
			csv_reader.Pop(ref data.spTarget);
			csv_reader.Pop(ref data.spAttackType);
			int value = 0;
			csv_reader.Pop(ref value);
			data.unlockEventId = value;
			csv_reader.Pop(ref data.format);
			csv_reader.Pop(ref data.minValue);
			csv_reader.Pop(ref data.maxValue);
			csv_reader.Pop(ref data.lotGroup);
			csv_reader.Pop(ref data.rate);
			return true;
		}
	}

	public const int INFO_MAX = 3;

	private UIntKeyTable<AbilityItemLot> abilityItemLot;

	public void CreateTable(string csv_text)
	{
		abilityItemLot = TableUtility.CreateUIntKeyTable<AbilityItemLot>(csv_text, AbilityItemLot.cb, "id,itemId,abilityType,target,spTarget,spAttackType,unlockEventId,format,minValue,maxValue,lotGroup,rate");
		abilityItemLot.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddUIntKeyTable(abilityItemLot, csv_text, AbilityItemLot.cb, "id,itemId,abilityType,target,spTarget,spAttackType,unlockEventId,format,minValue,maxValue,lotGroup,rate");
	}

	public AbilityItemLot GetAbilityItemLot(uint id)
	{
		if (this.abilityItemLot == null)
		{
			return null;
		}
		AbilityItemLot abilityItemLot = this.abilityItemLot.Get(id);
		if (abilityItemLot == null)
		{
			Log.TableError(this, id);
			abilityItemLot = new AbilityItemLot();
		}
		return abilityItemLot;
	}

	public void ForEach(Action<AbilityItemLot> cb)
	{
		abilityItemLot.ForEach(cb);
	}
}
