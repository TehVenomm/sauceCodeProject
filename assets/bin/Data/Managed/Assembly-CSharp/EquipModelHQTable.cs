using System;
using UnityEngine;

public class EquipModelHQTable : ScriptableObject
{
	[Serializable]
	public class Data
	{
		public int id;

		public byte flag;

		public Data(int id, byte flag)
		{
			this.id = id;
			this.flag = flag;
		}
	}

	public Data[] weaponDatas;

	public EquipModelHQTable()
		: this()
	{
	}

	public byte GetWeaponFlag(int id)
	{
		byte result = 0;
		if (weaponDatas != null)
		{
			Data data = Array.Find(weaponDatas, (Data o) => o.id == id);
			if (data != null)
			{
				result = data.flag;
			}
		}
		return result;
	}
}
