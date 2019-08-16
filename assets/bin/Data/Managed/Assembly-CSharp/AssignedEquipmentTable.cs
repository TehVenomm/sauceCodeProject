using Network;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class AssignedEquipmentTable : Singleton<AssignedEquipmentTable>, IDataTable
{
	public class AssignedEquipmentData
	{
		public int id;

		public string setName;

		public uint deliveryId;

		public int[] questids;

		public string helpUrl;

		public EquipmentData[] equipmentData;

		public const string NT = "id,setName,deliveryId,questids,helpUrl,weapon1Id,w1A1Id,w1A1Pt,w1A2Id,w1A2Pt,w1A3Id,w1A3Pt,w1M1Id,w1M2Id,w1M3Id,armorId,arA1Id,arA1Pt,arA2Id,arA2Pt,arA3Id,arA3Pt,arM1Id,arM2Id,arM3Id,helmId,heA1Id,heA1Pt,heA2Id,heA2Pt,heA3Id,heA3Pt,heM1Id,heM2Id,heM3Id,armId,armA1Id,armA1Pt,armA2Id,armA2Pt,armA3Id,armA3Pt,armM1Id,armM2Id,armM3Id,legId,legA1Id,legA1Pt,legA2Id,legA2Pt,legA3Id,legA3Pt,legM1Id,legM2Id,legM3Id";

		public static bool cb(CSVReader csv_reader, AssignedEquipmentData data, ref uint key)
		{
			data.id = (int)key;
			csv_reader.Pop(ref data.setName);
			csv_reader.Pop(ref data.deliveryId);
			string value = string.Empty;
			csv_reader.Pop(ref value);
			data.questids = TableUtility.ParseStringToIntArray(value);
			csv_reader.Pop(ref data.helpUrl);
			data.equipmentData = new EquipmentData[5];
			for (int i = 0; i < 5; i++)
			{
				uint value2 = 0u;
				csv_reader.Pop(ref value2);
				uint[] array = new uint[3];
				int[] array2 = new int[3];
				for (int j = 0; j < 3; j++)
				{
					csv_reader.Pop(ref array[j]);
					csv_reader.Pop(ref array2[j]);
				}
				uint[] array3 = new uint[3];
				for (int k = 0; k < 3; k++)
				{
					csv_reader.Pop(ref array3[k]);
				}
				data.equipmentData[i] = new EquipmentData(value2, array, array2, array3);
			}
			return true;
		}
	}

	public class EquipmentData
	{
		public uint id;

		public uint[] abilityIds;

		public int[] abilityPts;

		public uint[] skillIds;

		public EquipmentData()
		{
		}

		public EquipmentData(uint id, uint[] abilityIds, int[] abilityPts, uint[] skillIds)
		{
			this.id = id;
			this.abilityIds = abilityIds;
			this.abilityPts = abilityPts;
			this.skillIds = skillIds;
		}
	}

	public class AssignedSet
	{
		public CharaInfo.EquipItem weapon_0;

		public CharaInfo.EquipItem weapon_1;

		public CharaInfo.EquipItem weapon_2;

		public CharaInfo.EquipItem armor;

		public CharaInfo.EquipItem arm;

		public CharaInfo.EquipItem leg;

		public CharaInfo.EquipItem helm;
	}

	private UIntKeyTable<AssignedEquipmentData> dataTable;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<AssignedEquipmentData> _003C_003Ef__mg_0024cache0;

	public void CreateTable(string csv_text)
	{
		dataTable = TableUtility.CreateUIntKeyTable<AssignedEquipmentData>(csv_text, AssignedEquipmentData.cb, "id,setName,deliveryId,questids,helpUrl,weapon1Id,w1A1Id,w1A1Pt,w1A2Id,w1A2Pt,w1A3Id,w1A3Pt,w1M1Id,w1M2Id,w1M3Id,armorId,arA1Id,arA1Pt,arA2Id,arA2Pt,arA3Id,arA3Pt,arM1Id,arM2Id,arM3Id,helmId,heA1Id,heA1Pt,heA2Id,heA2Pt,heA3Id,heA3Pt,heM1Id,heM2Id,heM3Id,armId,armA1Id,armA1Pt,armA2Id,armA2Pt,armA3Id,armA3Pt,armM1Id,armM2Id,armM3Id,legId,legA1Id,legA1Pt,legA2Id,legA2Pt,legA3Id,legA3Pt,legM1Id,legM2Id,legM3Id");
		dataTable.TrimExcess();
	}

	public AssignedEquipmentData GetAssignedEquipmentData(int id)
	{
		if (dataTable == null)
		{
			return null;
		}
		AssignedEquipmentData assignedEquipmentData = dataTable.Get((uint)id);
		if (assignedEquipmentData == null)
		{
			Log.TableError(this, (uint)id);
		}
		return assignedEquipmentData;
	}

	public AssignedEquipmentData GetAssignedEquipmentDataFromDeliveryId(uint deliveryId)
	{
		if (dataTable == null)
		{
			return null;
		}
		AssignedEquipmentData data = null;
		dataTable.ForEach(delegate(AssignedEquipmentData o)
		{
			if (data == null && o.deliveryId == deliveryId)
			{
				data = o;
			}
		});
		return data;
	}

	public bool HasAssignedEquip(uint questid)
	{
		if (GetAssignedEquipmentDataFromQuestId(questid) != null)
		{
			return true;
		}
		return false;
	}

	public AssignedEquipmentData GetAssignedEquipmentDataFromQuestId(uint questid)
	{
		if (dataTable == null)
		{
			return null;
		}
		AssignedEquipmentData data = null;
		dataTable.ForEach(delegate(AssignedEquipmentData o)
		{
			if (data == null && o.questids != null && o.questids.Length > 0)
			{
				int num = 0;
				while (true)
				{
					if (num >= o.questids.Length)
					{
						return;
					}
					if (o.questids[num] == (int)questid)
					{
						break;
					}
					num++;
				}
				data = o;
			}
		});
		return data;
	}

	public static AssignedSet CreateAssignedSet(AssignedEquipmentData assignedEquipment)
	{
		AssignedSet assignedSet = new AssignedSet();
		for (int i = 0; i < assignedEquipment.equipmentData.Length; i++)
		{
			EquipmentData equipmentData = assignedEquipment.equipmentData[i];
			if (equipmentData.id == 0)
			{
				continue;
			}
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(equipmentData.id);
			CharaInfo.EquipItem equipItem = new CharaInfo.EquipItem();
			equipItem.eId = (int)equipItemData.id;
			equipItem.lv = equipItemData.maxLv;
			equipItem.exceed = ((equipItemData.exceedID != 0) ? 4 : 0);
			if (equipmentData.skillIds != null)
			{
				uint[] skillIds = equipmentData.skillIds;
				foreach (uint num in skillIds)
				{
					if (num != 0)
					{
						SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(num);
						equipItem.sIds.Add((int)num);
						if (skillItemData == null)
						{
							equipItem.sLvs.Add(1);
						}
						else
						{
							equipItem.sLvs.Add(skillItemData.GetMaxLv(0));
						}
						equipItem.sExs.Add(0);
					}
				}
			}
			if (equipmentData.abilityIds != null)
			{
				for (int k = 0; k < equipmentData.abilityIds.Length; k++)
				{
					uint num2 = equipmentData.abilityIds[k];
					if (num2 != 0)
					{
						int item = 1;
						if (equipmentData.abilityPts != null && equipmentData.abilityPts.Length > k)
						{
							item = equipmentData.abilityPts[k];
						}
						equipItem.aIds.Add((int)num2);
						equipItem.aPts.Add(item);
					}
				}
			}
			switch (equipItemData.type)
			{
			case EQUIPMENT_TYPE.ARMOR:
				assignedSet.armor = equipItem;
				continue;
			case EQUIPMENT_TYPE.HELM:
				assignedSet.helm = equipItem;
				continue;
			case EQUIPMENT_TYPE.ARM:
				assignedSet.arm = equipItem;
				continue;
			case EQUIPMENT_TYPE.LEG:
				assignedSet.leg = equipItem;
				continue;
			}
			if (equipItemData.IsWeapon())
			{
				assignedSet.weapon_0 = equipItem;
			}
		}
		return assignedSet;
	}

	public static void MergeAssignedEquip(ref CharaInfo org, AssignedEquipmentData assignedEquipment)
	{
		AssignedSet assignedSet = CreateAssignedSet(assignedEquipment);
		List<CharaInfo.EquipItem> org_weapons = new List<CharaInfo.EquipItem>();
		org.equipSet.ForEach(delegate(CharaInfo.EquipItem data)
		{
			if (data != null)
			{
				EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)data.eId);
				if (equipItemData != null && equipItemData.IsWeapon())
				{
					org_weapons.Add(data);
				}
			}
		});
		List<CharaInfo.EquipItem> list = new List<CharaInfo.EquipItem>();
		if (assignedSet.weapon_0 != null)
		{
			list.Add(assignedSet.weapon_0);
		}
		else if (org_weapons.Count > 0)
		{
			list.Add(org_weapons[0]);
		}
		if (assignedSet.helm != null)
		{
			list.Add(assignedSet.helm);
		}
		if (assignedSet.armor != null)
		{
			list.Add(assignedSet.armor);
		}
		if (assignedSet.arm != null)
		{
			list.Add(assignedSet.arm);
		}
		if (assignedSet.leg != null)
		{
			list.Add(assignedSet.leg);
		}
		org.equipSet = list;
	}

	public static void MergeAssignedEquip(ref StageObjectManager.CreatePlayerInfo createinfo, AssignedEquipmentData assignedEquipment)
	{
		if (createinfo != null && createinfo.charaInfo != null)
		{
			MergeAssignedEquip(ref createinfo.charaInfo, assignedEquipment);
			if (createinfo.extentionInfo != null && createinfo.extentionInfo.weaponIndexList != null)
			{
				createinfo.extentionInfo.weaponIndexList.Clear();
				createinfo.extentionInfo.weaponIndexList.Add(0);
				createinfo.extentionInfo.weaponIndexList.Add(-1);
				createinfo.extentionInfo.weaponIndexList.Add(-1);
			}
		}
	}
}
