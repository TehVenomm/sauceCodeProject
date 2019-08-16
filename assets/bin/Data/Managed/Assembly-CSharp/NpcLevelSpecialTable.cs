using Network;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class NpcLevelSpecialTable : Singleton<NpcLevelSpecialTable>, IDataTable
{
	[Serializable]
	public class NpcLevelSpecialData : NpcLevelTable.NpcLevelData
	{
		public uint id;

		public int[] npcids;

		public int[] questids;

		public new const string NT = "id,npcids,questids,lv,hp,atk,atk_1,atk_2,atk_3,atk_4,atk_5,atk_6,def,def_1,def_2,def_3,def_4,def_5,def_6,w1,w1Lv,w2,w2Lv,w3,w3Lv,armor,armorLv,helm,helmLv,arm,armLv,leg,legLv";

		public static bool cb_special(CSVReader csv, NpcLevelSpecialData data, ref uint key1)
		{
			data.id = key1;
			string value = string.Empty;
			csv.Pop(ref value);
			data.npcids = TableUtility.ParseStringToIntArray(value);
			value = string.Empty;
			csv.Pop(ref value);
			data.questids = TableUtility.ParseStringToIntArray(value);
			csv.Pop(ref data.lv);
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
			return true;
		}

		public bool ContainNPCID(int npcid)
		{
			return containInArray(npcids, npcid);
		}

		public bool ContainQuestID(int questid)
		{
			return containInArray(questids, questid);
		}

		public bool HasNPCIds()
		{
			return !isArrayNullOrEmpty(npcids);
		}

		public bool HasQuestIds()
		{
			return !isArrayNullOrEmpty(questids);
		}

		private bool containInArray(int[] array, int id)
		{
			if (array == null)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == id)
				{
					return true;
				}
			}
			return false;
		}

		private bool isArrayNullOrEmpty(int[] array)
		{
			if (array == null)
			{
				return true;
			}
			if (array.Length == 0)
			{
				return true;
			}
			return false;
		}
	}

	private UIntKeyTable<NpcLevelSpecialData> dataTable;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<NpcLevelSpecialData> _003C_003Ef__mg_0024cache0;

	public void CreateTable(string csv_text)
	{
		dataTable = TableUtility.CreateUIntKeyTable<NpcLevelSpecialData>(csv_text, NpcLevelSpecialData.cb_special, "id,npcids,questids,lv,hp,atk,atk_1,atk_2,atk_3,atk_4,atk_5,atk_6,def,def_1,def_2,def_3,def_4,def_5,def_6,w1,w1Lv,w2,w2Lv,w3,w3Lv,armor,armorLv,helm,helmLv,arm,armLv,leg,legLv");
		dataTable.TrimExcess();
	}

	public NpcLevelSpecialData GetNPCLevelSpecial(uint lv, int npcid, int questid)
	{
		if (dataTable == null || dataTable.GetCount() <= 0)
		{
			return null;
		}
		NpcLevelSpecialData npcLevelSpecialData = null;
		List<NpcLevelSpecialData> candidates = new List<NpcLevelSpecialData>();
		dataTable.ForEach(delegate(NpcLevelSpecialData npc)
		{
			if (npc.ContainNPCID(npcid) && npc.ContainQuestID(questid))
			{
				candidates.Add(npc);
			}
		});
		npcLevelSpecialData = _getNpcInCandidates(candidates, lv);
		if (npcLevelSpecialData != null)
		{
			return npcLevelSpecialData;
		}
		candidates.Clear();
		dataTable.ForEach(delegate(NpcLevelSpecialData npc)
		{
			if (npc.ContainNPCID(npcid) && !npc.HasQuestIds())
			{
				candidates.Add(npc);
			}
		});
		npcLevelSpecialData = _getNpcInCandidates(candidates, lv);
		if (npcLevelSpecialData != null)
		{
			return npcLevelSpecialData;
		}
		candidates.Clear();
		dataTable.ForEach(delegate(NpcLevelSpecialData npc)
		{
			if (!npc.HasNPCIds() && npc.ContainQuestID(questid))
			{
				candidates.Add(npc);
			}
		});
		npcLevelSpecialData = _getNpcInCandidates(candidates, lv);
		if (npcLevelSpecialData != null)
		{
			return npcLevelSpecialData;
		}
		return null;
	}

	private NpcLevelSpecialData _getNpcInCandidates(List<NpcLevelSpecialData> candidates, uint lv)
	{
		NpcLevelSpecialData npcLevelSpecialData = null;
		if (candidates.Count > 0)
		{
			for (int i = 0; i < candidates.Count; i++)
			{
				NpcLevelSpecialData npcLevelSpecialData2 = candidates[i];
				if (npcLevelSpecialData2.lv <= lv)
				{
					if (npcLevelSpecialData == null)
					{
						npcLevelSpecialData = npcLevelSpecialData2;
					}
					else if (npcLevelSpecialData.lv < npcLevelSpecialData2.lv)
					{
						npcLevelSpecialData = npcLevelSpecialData2;
					}
				}
			}
		}
		return npcLevelSpecialData;
	}
}
