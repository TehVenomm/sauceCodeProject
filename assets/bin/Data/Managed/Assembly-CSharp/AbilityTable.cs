using System;

public class AbilityTable : Singleton<AbilityTable>, IDataTable
{
	public class Ability
	{
		public uint id;

		public string name;

		public int iconId;

		public DateTime? startDate;

		public DateTime? endDate;

		public int unlockEventId;

		public const string NT = "abilityId,name,iconId,startDate,endDate,unlockEventId";

		public static bool cb(CSVReader csv_reader, Ability data, ref uint key)
		{
			data.id = key;
			csv_reader.Pop(ref data.name);
			csv_reader.Pop(ref data.iconId);
			string value = "";
			csv_reader.Pop(ref value);
			if (!string.IsNullOrEmpty(value) && DateTime.TryParse(value, out DateTime result))
			{
				data.startDate = result;
			}
			string value2 = "";
			csv_reader.Pop(ref value2);
			if (!string.IsNullOrEmpty(value2) && DateTime.TryParse(value2, out result))
			{
				data.endDate = result;
			}
			int value3 = 0;
			csv_reader.Pop(ref value3);
			data.unlockEventId = value3;
			return true;
		}

		public bool IsActive(DateTime checkedTime)
		{
			if (startDate.HasValue && checkedTime < startDate.Value)
			{
				return false;
			}
			if (endDate.HasValue && checkedTime >= endDate.Value)
			{
				return false;
			}
			return true;
		}

		public bool IsActive()
		{
			DateTime now = TimeManager.GetNow();
			return IsActive(now);
		}
	}

	public const int INFO_MAX = 3;

	private UIntKeyTable<Ability> abilityTable;

	public void CreateTable(string csv_text)
	{
		abilityTable = TableUtility.CreateUIntKeyTable<Ability>(csv_text, Ability.cb, "abilityId,name,iconId,startDate,endDate,unlockEventId");
		abilityTable.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddUIntKeyTable(abilityTable, csv_text, Ability.cb, "abilityId,name,iconId,startDate,endDate,unlockEventId");
	}

	public Ability GetAbility(uint id)
	{
		if (abilityTable == null)
		{
			return null;
		}
		Ability ability = abilityTable.Get(id);
		if (ability == null)
		{
			Log.TableError(this, id);
			ability = new Ability();
			ability.name = Log.NON_DATA_NAME;
		}
		return ability;
	}

	public void ForEach(Action<Ability> cb)
	{
		abilityTable.ForEach(cb);
	}
}
