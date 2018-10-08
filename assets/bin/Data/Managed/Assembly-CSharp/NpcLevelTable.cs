using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NpcLevelTable : Singleton<NpcLevelTable>, IDataTable
{
	[Serializable]
	public class NpcLevelData
	{
		public const string NT = "lv,hp,atk,atk_1,atk_2,atk_3,atk_4,atk_5,atk_6,def,def_1,def_2,def_3,def_4,def_5,def_6,w1,w1Lv,w2,w2Lv,w3,w3Lv,armor,armorLv,helm,helmLv,arm,armLv,leg,legLv";

		public uint lv;

		public int hp;

		public int atk;

		public int[] atk_attribute = new int[6];

		public int def;

		public int[] tolerance = new int[6];

		public CharaInfo.EquipItem[] equipItems = new CharaInfo.EquipItem[7];

		public int lvIndex;

		public static bool CB(CSVReader csv, NpcLevelData data, ref uint key1)
		{
			data.lv = key1;
			csv.Pop(ref data.hp);
			csv.Pop(ref data.atk);
			for (int i = 0; i < 6; i++)
			{
				csv.Pop(ref data.atk_attribute[i]);
			}
			csv.Pop(ref data.def);
			for (int j = 0; j < 6; j++)
			{
				csv.Pop(ref data.tolerance[j]);
			}
			int k = 0;
			for (int num = data.equipItems.Length; k < num; k++)
			{
				data.equipItems[k] = new CharaInfo.EquipItem();
				csv.Pop(ref data.equipItems[k].eId);
				csv.Pop(ref data.equipItems[k].lv);
			}
			data.lvIndex = 0;
			return true;
		}

		public void CopyHomeCharaInfo(CharaInfo info, StageObjectManager.CreatePlayerInfo.ExtentionInfo extentionInfo, float atkRate = 1f)
		{
			info.level = (int)lv;
			info.hp = hp;
			info.atk = (int)((float)atk * atkRate);
			info.def = def;
			int i = 0;
			for (int num = equipItems.Length; i < num; i++)
			{
				CharaInfo.EquipItem equipItem = equipItems[i];
				if (equipItem.eId > 0)
				{
					info.equipSet.Add(equipItem);
					if (extentionInfo != null && i >= 0 && i < 3)
					{
						int item = info.equipSet.Count - 1;
						extentionInfo.weaponIndexList.Add(item);
					}
				}
			}
			if (extentionInfo != null)
			{
				int index = Utility.Random(extentionInfo.weaponIndexList.Count);
				int value = extentionInfo.weaponIndexList[0];
				extentionInfo.weaponIndexList[0] = extentionInfo.weaponIndexList[index];
				extentionInfo.weaponIndexList[index] = value;
			}
		}

		public override string ToString()
		{
			string empty = string.Empty;
			string text = empty;
			empty = text + lv + "," + hp + "," + atk + "," + def;
			int i = 0;
			for (int num = equipItems.Length; i < num; i++)
			{
				empty += ",";
				text = empty;
				empty = text + "[" + i + "]";
				text = empty;
				empty = text + "id=" + equipItems[i].eId + ",";
				empty = empty + "lv=" + equipItems[i].lv;
			}
			return empty;
		}
	}

	private UIntKeyTable<List<NpcLevelData>> dataTable;

	private List<uint> lvList = new List<uint>();

	public void CreateTable(string csv)
	{
		dataTable = TableUtility.CreateUIntKeyListTable<NpcLevelData>(csv, NpcLevelData.CB, "lv,hp,atk,atk_1,atk_2,atk_3,atk_4,atk_5,atk_6,def,def_1,def_2,def_3,def_4,def_5,def_6,w1,w1Lv,w2,w2Lv,w3,w3Lv,armor,armorLv,helm,helmLv,arm,armLv,leg,legLv");
		dataTable.TrimExcess();
		dataTable.ForEach(delegate(List<NpcLevelData> list)
		{
			lvList.Add(list[0].lv);
			int i = 0;
			for (int count = list.Count; i < count; i++)
			{
				list[i].lvIndex = i;
			}
		});
		lvList.Sort();
	}

	public List<NpcLevelData> GetNpcLevelList(uint lv)
	{
		return dataTable.Get(lv);
	}

	public NpcLevelData GetNpcLevelRandom(uint lv)
	{
		uint lv2 = lvList.FindLast((uint l) => l <= lv);
		List<NpcLevelData> npcLevelList = GetNpcLevelList(lv2);
		if (npcLevelList == null || npcLevelList.Count <= 0)
		{
			return null;
		}
		int index = (int)(Random.get_value() * (float)npcLevelList.Count);
		return npcLevelList[index];
	}

	public NpcLevelData GetNpcLevel(uint lv, int lv_index)
	{
		List<NpcLevelData> npcLevelList = GetNpcLevelList(lv);
		if (npcLevelList == null || npcLevelList.Count <= 0)
		{
			return null;
		}
		if (lv_index < 0 || npcLevelList.Count <= lv_index)
		{
			return null;
		}
		return npcLevelList[lv_index];
	}
}
