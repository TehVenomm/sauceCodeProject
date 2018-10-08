using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SmithManager : MonoBehaviourSingleton<SmithManager>
{
	public enum ERR_SMITH_SEND
	{
		NONE = 0,
		ALREADY_LV_MAX = -1,
		NOT_LV_MAX = -2,
		NOT_ENOUGH_MATERIAL = -3,
		NOT_ENOUGH_MONEY = -4,
		ALREADY_INVENTORY_MAX = -5,
		NOT_FOUND_EVOLVE_DATA = -6,
		NOT_SET_EQUIP_MATERIAL = -7
	}

	public class SmithDataBase
	{
		public virtual void ResetData()
		{
		}
	}

	public class SmithCreateData : SmithDataBase
	{
		public SortBase.TYPE selectCreateEquipItemType;

		public EquipItemInfo selectEquipData;

		public EquipItemTable.EquipItemData generateTableData;

		public CreateEquipItemTable.CreateEquipItemData createEquipItemTable;

		public override void ResetData()
		{
			selectCreateEquipItemType = SortBase.TYPE.ONE_HAND_SWORD;
			selectEquipData = null;
			generateTableData = null;
			createEquipItemTable = null;
			base.ResetData();
		}
	}

	public class SmithGrowData : SmithDataBase
	{
		public EquipItemInfo selectEquipData;

		public SmithEvolveData evolveData;

		public override void ResetData()
		{
			selectEquipData = null;
			evolveData = null;
			base.ResetData();
		}
	}

	public class SmithEvolveData
	{
		public EquipItemInfo evolveBeforeEquipData;

		public EquipItemTable.EquipItemData[] evolveEquipDataTable;

		public EvolveEquipItemTable.EvolveEquipItemData[] evolveTable;

		public int selectIndex;

		public EquipItemTable.EquipItemData GetEquipTable()
		{
			return evolveEquipDataTable[selectIndex];
		}

		public EvolveEquipItemTable.EvolveEquipItemData GetEvolveTable()
		{
			return evolveTable[selectIndex];
		}
	}

	public class ResultData
	{
		public object itemData;

		public int beforeRarity;

		public int beforeLevel;

		public int beforeMaxLevel;

		public int beforeExceedCnt;

		public int beforeExp;

		public int beforeAtk;

		public int beforeDef;

		public int beforeHp;

		public int beforeElemAtk;

		public int beforeElemDef;

		public bool isExceed;
	}

	public class SmithBadgeData
	{
		public int[] weaponsBadgeNum;

		public int[] defenseBadgeNum;

		public int[] visualBadgeNum;

		public int[] pickupBadgeNum;

		public List<int>[] weaponsBadgeIds;

		public List<int>[] defenseBadgeIds;

		public List<int>[] visualBadgeIds;

		public List<int>[] pickupBadgeIds;

		public int totalNum => GetAllWeaponBadgeNum() + GetAllDefenseBadgeNum();

		public SmithBadgeData()
		{
			weaponsBadgeNum = new int[5];
			defenseBadgeNum = new int[4];
			visualBadgeNum = new int[4];
			pickupBadgeNum = new int[2];
			weaponsBadgeIds = new List<int>[5];
			defenseBadgeIds = new List<int>[4];
			visualBadgeIds = new List<int>[4];
			pickupBadgeIds = new List<int>[2];
		}

		public int GetBadgeNum(EQUIPMENT_TYPE type)
		{
			int num = 0;
			int equipmentTypeIndex = UIBehaviour.GetEquipmentTypeIndex(type);
			if (!Singleton<EquipItemTable>.I.IsWeapon(type))
			{
				if (!Singleton<EquipItemTable>.I.IsVisual(type))
				{
					equipmentTypeIndex -= 5;
					return defenseBadgeNum[equipmentTypeIndex];
				}
				equipmentTypeIndex -= 5;
				return visualBadgeNum[equipmentTypeIndex];
			}
			return weaponsBadgeNum[equipmentTypeIndex];
		}

		public int GetPickupBadgeNum(bool is_weapon)
		{
			int num = (!is_weapon) ? 1 : 0;
			return pickupBadgeNum[num];
		}

		public int GetAllWeaponBadgeNum()
		{
			int num = 0;
			int i = 0;
			for (int num2 = weaponsBadgeNum.Length; i < num2; i++)
			{
				num += weaponsBadgeNum[i];
			}
			return num + pickupBadgeNum[0];
		}

		public int GetAllDefenseBadgeNum()
		{
			int num = 0;
			int i = 0;
			for (int num2 = defenseBadgeNum.Length; i < num2; i++)
			{
				num += defenseBadgeNum[i];
			}
			int j = 0;
			for (int num3 = visualBadgeNum.Length; j < num3; j++)
			{
				num += visualBadgeNum[j];
			}
			return num + pickupBadgeNum[1];
		}

		public void DebugShowCount()
		{
		}

		private void _DebugShowCount(int[] tmp, int[] now)
		{
			int i = 0;
			for (int num = tmp.Length; i < num; i++)
			{
				Debug.LogWarning("[" + i + "] = " + (now[i] - tmp[i]));
				tmp[i] = now[i];
			}
		}
	}

	public object[] localInventoryEquipData;

	private object smithData;

	public bool initialized
	{
		get;
		private set;
	}

	public bool isEnableSmithBlur
	{
		get;
		private set;
	}

	public SmithBadgeData smithBadgeData
	{
		get;
		private set;
	}

	public T CreateSmithData<T>() where T : SmithDataBase, new()
	{
		T val = smithData as T;
		if (val != null)
		{
			val.ResetData();
		}
		else
		{
			smithData = new T();
		}
		return smithData as T;
	}

	public T GetSmithData<T>() where T : SmithDataBase
	{
		return smithData as T;
	}

	private void DeleteSmithData()
	{
		(smithData as SmithDataBase)?.ResetData();
		smithData = null;
	}

	public EquipItemInfo GetSmithEquipItemInfo(SmithEquipBase.SmithType type)
	{
		EquipItemInfo result = null;
		if (type != 0)
		{
			SmithGrowData smithGrowData = GetSmithData<SmithGrowData>();
			if (smithGrowData != null)
			{
				result = smithGrowData.selectEquipData;
			}
		}
		return result;
	}

	public EquipItemTable.EquipItemData GetSmithEquipItemTable(SmithEquipBase.SmithType type)
	{
		EquipItemTable.EquipItemData result = null;
		switch (type)
		{
		case SmithEquipBase.SmithType.GROW:
		case SmithEquipBase.SmithType.ABILITY_CHANGE:
		{
			SmithGrowData smithGrowData = GetSmithData<SmithGrowData>();
			if (smithGrowData != null && smithGrowData.selectEquipData != null)
			{
				result = smithGrowData.selectEquipData.tableData;
			}
			break;
		}
		case SmithEquipBase.SmithType.EVOLVE:
		{
			SmithGrowData smithGrowData2 = GetSmithData<SmithGrowData>();
			if (smithGrowData2 != null && smithGrowData2.evolveData != null)
			{
				result = smithGrowData2.evolveData.GetEquipTable();
			}
			break;
		}
		default:
		{
			SmithCreateData smithCreateData = GetSmithData<SmithCreateData>();
			if (smithCreateData != null)
			{
				result = smithCreateData.generateTableData;
			}
			break;
		}
		}
		return result;
	}

	public void CreateLocalInventory()
	{
		localInventoryEquipData = MonoBehaviourSingleton<InventoryManager>.I.GetEquipInventoryClone();
	}

	private void DeleteLocalInventory()
	{
		localInventoryEquipData = null;
	}

	public void UpdateLocalInventoryItem(EquipItemInfo item)
	{
		if (item != null && item.uniqueID != 0L && item.tableID != 0 && localInventoryEquipData != null && localInventoryEquipData.Length != 0 && localInventoryEquipData[0] is EquipItemInfo)
		{
			int num = 0;
			int num2 = localInventoryEquipData.Length;
			while (true)
			{
				if (num >= num2)
				{
					return;
				}
				EquipItemInfo equipItemInfo = localInventoryEquipData[num] as EquipItemInfo;
				if (equipItemInfo != null && equipItemInfo.uniqueID == item.uniqueID)
				{
					break;
				}
				num++;
			}
			localInventoryEquipData[num] = item;
		}
	}

	public void InitSmithData()
	{
		DeleteSmithData();
		DeleteLocalInventory();
	}

	public ERR_SMITH_SEND CheckCreateEquipItem(uint create_id)
	{
		CreateEquipItemTable.CreateEquipItemData createEquipItemTableData = Singleton<CreateEquipItemTable>.I.GetCreateEquipItemTableData(create_id);
		if (!MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterial(createEquipItemTableData.needMaterial))
		{
			return ERR_SMITH_SEND.NOT_ENOUGH_MATERIAL;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < (int)createEquipItemTableData.needMoney)
		{
			return ERR_SMITH_SEND.NOT_ENOUGH_MONEY;
		}
		return ERR_SMITH_SEND.NONE;
	}

	public ERR_SMITH_SEND CheckGrowEquipItem(EquipItemInfo item)
	{
		if (item.IsLevelMax())
		{
			return ERR_SMITH_SEND.ALREADY_LV_MAX;
		}
		GrowEquipItemTable.GrowEquipItemNeedItemData nextNeedTableData = item.nextNeedTableData;
		if (!MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterial(nextNeedTableData.needMaterial))
		{
			return ERR_SMITH_SEND.NOT_ENOUGH_MATERIAL;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < nextNeedTableData.needMoney)
		{
			return ERR_SMITH_SEND.NOT_ENOUGH_MONEY;
		}
		return ERR_SMITH_SEND.NONE;
	}

	public ERR_SMITH_SEND CheckEvolveEquipItem(EquipItemInfo item, uint evolve_id, ulong[] uniqIdList)
	{
		if (!item.IsLevelMax())
		{
			return ERR_SMITH_SEND.NOT_LV_MAX;
		}
		EvolveEquipItemTable.EvolveEquipItemData[] evolveTable = item.tableData.GetEvolveTable();
		EvolveEquipItemTable.EvolveEquipItemData evolveEquipItemData = null;
		EvolveEquipItemTable.EvolveEquipItemData[] array = evolveTable;
		foreach (EvolveEquipItemTable.EvolveEquipItemData evolveEquipItemData2 in array)
		{
			if (evolveEquipItemData2.id == evolve_id)
			{
				evolveEquipItemData = evolveEquipItemData2;
				break;
			}
		}
		if (evolveEquipItemData == null)
		{
			return ERR_SMITH_SEND.NOT_FOUND_EVOLVE_DATA;
		}
		if (evolveEquipItemData.needEquip != null && !MonoBehaviourSingleton<InventoryManager>.I.IsSetEquipMaterial(uniqIdList))
		{
			return ERR_SMITH_SEND.NOT_SET_EQUIP_MATERIAL;
		}
		if (!MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterial(evolveEquipItemData.needMaterial))
		{
			return ERR_SMITH_SEND.NOT_ENOUGH_MATERIAL;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < (int)evolveEquipItemData.needMoney)
		{
			return ERR_SMITH_SEND.NOT_ENOUGH_MONEY;
		}
		return ERR_SMITH_SEND.NONE;
	}

	public ERR_SMITH_SEND CheckAbilityChange(EquipItemInfo item)
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < GetAbilityChangeNeedMoney(item))
		{
			return ERR_SMITH_SEND.NOT_ENOUGH_MONEY;
		}
		return ERR_SMITH_SEND.NONE;
	}

	public int GetAbilityChangeNeedMoney(EquipItemInfo item)
	{
		ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
		switch (item.tableData.rarity)
		{
		case RARITY_TYPE.D:
			return constDefine.ABILITY_CHANGE_COST_RARITY_D;
		case RARITY_TYPE.C:
			return constDefine.ABILITY_CHANGE_COST_RARITY_C;
		case RARITY_TYPE.B:
			return constDefine.ABILITY_CHANGE_COST_RARITY_B;
		case RARITY_TYPE.A:
			return constDefine.ABILITY_CHANGE_COST_RARITY_A;
		case RARITY_TYPE.S:
			return constDefine.ABILITY_CHANGE_COST_RARITY_S;
		case RARITY_TYPE.SS:
			return constDefine.ABILITY_CHANGE_COST_RARITY_SS;
		default:
			return constDefine.ABILITY_CHANGE_COST_RARITY_SSS;
		}
	}

	public int GetGrowResultValue(int base_value, GrowRate rate_data, bool is_element = false)
	{
		int num = rate_data.add;
		return Mathf.FloorToInt((float)(base_value * (int)rate_data.rate) * 0.01f) + num;
	}

	public float GetGrowResultValue(float base_value, GrowRateFloat rate_data, bool is_element = false)
	{
		float num = rate_data.add;
		return base_value * (float)(int)rate_data.rate * 0.01f + num;
	}

	public int GetBadgeTotalNum()
	{
		if (smithBadgeData == null)
		{
			return 0;
		}
		return smithBadgeData.totalNum;
	}

	public void CreateBadgeData(bool is_force = false)
	{
		if (smithBadgeData == null || is_force)
		{
			smithBadgeData = new SmithBadgeData();
			EQUIPMENT_TYPE[] array = (EQUIPMENT_TYPE[])Enum.GetValues(typeof(EQUIPMENT_TYPE));
			int i = 0;
			for (int num = array.Length; i < num; i++)
			{
				EQUIPMENT_TYPE type = array[i];
				SmithCreateItemInfo[] createEquipItemDataAry = Singleton<CreateEquipItemTable>.I.GetCreateEquipItemDataAry(type);
				if (createEquipItemDataAry != null && createEquipItemDataAry.Length > 0)
				{
					int j = 0;
					for (int num2 = createEquipItemDataAry.Length; j < num2; j++)
					{
						SmithCreateItemInfo create_info = createEquipItemDataAry[j];
						CheckAndAddSmithBadge(create_info, false);
					}
				}
			}
			smithBadgeData.DebugShowCount();
			SmithCreateItemInfo[] pickupItemAry = Singleton<CreatePickupItemTable>.I.GetPickupItemAry(SortBase.TYPE.WEAPON_ALL);
			int k = 0;
			for (int num3 = pickupItemAry.Length; k < num3; k++)
			{
				SmithCreateItemInfo create_info2 = pickupItemAry[k];
				CheckAndAddSmithBadge(create_info2, true);
			}
			smithBadgeData.DebugShowCount();
			SmithCreateItemInfo[] pickupItemAry2 = Singleton<CreatePickupItemTable>.I.GetPickupItemAry(SortBase.TYPE.ARMOR_ALL);
			int l = 0;
			for (int num4 = pickupItemAry2.Length; l < num4; l++)
			{
				SmithCreateItemInfo create_info3 = pickupItemAry2[l];
				CheckAndAddSmithBadge(create_info3, true);
			}
			smithBadgeData.DebugShowCount();
		}
	}

	public bool NeedSmithBadge(SmithCreateItemInfo create_info, bool is_pickup = false)
	{
		if (smithBadgeData == null)
		{
			return false;
		}
		if (create_info == null || create_info.equipTableData.id == 0)
		{
			return false;
		}
		if (create_info.equipTableData.listId == 0)
		{
			return false;
		}
		if ((int)create_info.smithCreateTableData.researchLv > MonoBehaviourSingleton<UserInfoManager>.I.userStatus.researchLv)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<InventoryManager>.I.IsHaveingKeyMaterial(create_info.smithCreateTableData.needKeyOrder, create_info.smithCreateTableData.needMaterial))
		{
			return false;
		}
		int id = (int)create_info.equipTableData.id;
		return !GameSaveData.instance.IsCheckedSmithCreateRecipe(id);
	}

	private void CheckAndAddSmithBadge(SmithCreateItemInfo create_info, bool is_pickup = false)
	{
		int id = (int)create_info.equipTableData.id;
		if (NeedSmithBadge(create_info, is_pickup))
		{
			if (create_info.equipTableData.IsWeapon())
			{
				int equipmentTypeIndex = UIBehaviour.GetEquipmentTypeIndex(create_info.equipTableData.type);
				if (is_pickup)
				{
					if (smithBadgeData.pickupBadgeIds[0] == null)
					{
						smithBadgeData.pickupBadgeIds[0] = new List<int>();
					}
					smithBadgeData.pickupBadgeIds[0].Add(id);
					smithBadgeData.pickupBadgeNum[0]++;
				}
				else
				{
					if (smithBadgeData.weaponsBadgeIds[equipmentTypeIndex] == null)
					{
						smithBadgeData.weaponsBadgeIds[equipmentTypeIndex] = new List<int>();
					}
					smithBadgeData.weaponsBadgeIds[equipmentTypeIndex].Add(id);
					smithBadgeData.weaponsBadgeNum[equipmentTypeIndex]++;
				}
			}
			else if (create_info.equipTableData.IsVisual())
			{
				int num = UIBehaviour.GetEquipmentTypeIndex(create_info.equipTableData.type) - 5;
				if (is_pickup)
				{
					if (smithBadgeData.pickupBadgeIds[1] == null)
					{
						smithBadgeData.pickupBadgeIds[1] = new List<int>();
					}
					smithBadgeData.pickupBadgeIds[1].Add(id);
					smithBadgeData.pickupBadgeNum[1]++;
				}
				else
				{
					if (smithBadgeData.visualBadgeIds[num] == null)
					{
						smithBadgeData.visualBadgeIds[num] = new List<int>();
					}
					smithBadgeData.visualBadgeIds[num].Add(id);
					smithBadgeData.visualBadgeNum[num]++;
				}
			}
			else
			{
				int num2 = UIBehaviour.GetEquipmentTypeIndex(create_info.equipTableData.type) - 5;
				if (is_pickup)
				{
					if (smithBadgeData.pickupBadgeIds[1] == null)
					{
						smithBadgeData.pickupBadgeIds[1] = new List<int>();
					}
					smithBadgeData.pickupBadgeIds[1].Add(id);
					smithBadgeData.pickupBadgeNum[1]++;
				}
				else
				{
					if (smithBadgeData.defenseBadgeIds[num2] == null)
					{
						smithBadgeData.defenseBadgeIds[num2] = new List<int>();
					}
					smithBadgeData.defenseBadgeIds[num2].Add(id);
					smithBadgeData.defenseBadgeNum[num2]++;
				}
			}
		}
	}

	public void RemoveSmithBadge(EQUIPMENT_TYPE type, bool is_pickup)
	{
		if (Singleton<EquipItemTable>.I.IsWeapon(type))
		{
			int equipmentTypeIndex = UIBehaviour.GetEquipmentTypeIndex(type);
			if (is_pickup)
			{
				if (smithBadgeData.pickupBadgeIds[0] != null)
				{
					GameSaveData.instance.AddCheckedSmithCreateRecipe(smithBadgeData.pickupBadgeIds[0].ToArray());
					smithBadgeData.pickupBadgeIds[0].Clear();
				}
				smithBadgeData.pickupBadgeNum[0] = 0;
			}
			else
			{
				if (smithBadgeData.weaponsBadgeIds[equipmentTypeIndex] != null)
				{
					GameSaveData.instance.AddCheckedSmithCreateRecipe(smithBadgeData.weaponsBadgeIds[equipmentTypeIndex].ToArray());
					smithBadgeData.weaponsBadgeIds[equipmentTypeIndex].Clear();
				}
				smithBadgeData.weaponsBadgeNum[equipmentTypeIndex] = 0;
			}
		}
		else if (Singleton<EquipItemTable>.I.IsVisual(type))
		{
			int num = UIBehaviour.GetEquipmentTypeIndex(type) - 5;
			if (is_pickup)
			{
				if (smithBadgeData.pickupBadgeIds[1] != null)
				{
					GameSaveData.instance.AddCheckedSmithCreateRecipe(smithBadgeData.pickupBadgeIds[1].ToArray());
					smithBadgeData.pickupBadgeIds[1].Clear();
				}
				smithBadgeData.pickupBadgeNum[1] = 0;
			}
			else
			{
				if (smithBadgeData.visualBadgeIds[num] != null)
				{
					GameSaveData.instance.AddCheckedSmithCreateRecipe(smithBadgeData.visualBadgeIds[num].ToArray());
					smithBadgeData.visualBadgeIds[num].Clear();
				}
				smithBadgeData.visualBadgeNum[num] = 0;
			}
		}
		else
		{
			int num2 = UIBehaviour.GetEquipmentTypeIndex(type) - 5;
			if (is_pickup)
			{
				if (smithBadgeData.pickupBadgeIds[1] != null)
				{
					GameSaveData.instance.AddCheckedSmithCreateRecipe(smithBadgeData.pickupBadgeIds[1].ToArray());
					smithBadgeData.pickupBadgeIds[1].Clear();
				}
				smithBadgeData.pickupBadgeNum[1] = 0;
			}
			else
			{
				if (smithBadgeData.defenseBadgeIds[num2] != null)
				{
					GameSaveData.instance.AddCheckedSmithCreateRecipe(smithBadgeData.defenseBadgeIds[num2].ToArray());
					smithBadgeData.defenseBadgeIds[num2].Clear();
				}
				smithBadgeData.defenseBadgeNum[num2] = 0;
			}
		}
		GameSaveData.Save();
	}

	public void SendCreateEquipItem(uint create_table_id, Action<Error, EquipItemInfo> call_back)
	{
		SmithCreateModel.RequestSendForm requestSendForm = new SmithCreateModel.RequestSendForm();
		requestSendForm.cid = (int)create_table_id;
		Protocol.Send(SmithCreateModel.URL, requestSendForm, delegate(SmithCreateModel ret)
		{
			EquipItemInfo arg = null;
			if (ret.Error == Error.None)
			{
				arg = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(ulong.Parse(ret.result.equipUniqId));
			}
			call_back(ret.Error, arg);
		}, string.Empty);
	}

	public void SendGrowEquipItem(ulong uid, int target_lv, Action<Error, EquipItemInfo> call_back)
	{
		SmithGrowModel.RequestSendForm requestSendForm = new SmithGrowModel.RequestSendForm();
		requestSendForm.euid = uid.ToString();
		requestSendForm.lv = target_lv;
		Protocol.Send(SmithGrowModel.URL, requestSendForm, delegate(SmithGrowModel ret)
		{
			EquipItemInfo equipItemInfo = null;
			if (ret.Error == Error.None)
			{
				equipItemInfo = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uid);
				if (equipItemInfo != null && MonoBehaviourSingleton<StatusManager>.I.IsEquipping(equipItemInfo, -1))
				{
					MonoBehaviourSingleton<StatusManager>.I.UpdateEquip(equipItemInfo);
				}
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_GROW);
			}
			call_back(ret.Error, equipItemInfo);
		}, string.Empty);
	}

	public void SendEvolveEquipItem(ulong uid, uint evolve_id, ulong[] uniq_ids, Action<Error, EquipItemInfo> call_back)
	{
		SmithEvolveModel.RequestSendForm requestSendForm = new SmithEvolveModel.RequestSendForm();
		requestSendForm.euid = uid.ToString();
		requestSendForm.vid = (int)evolve_id;
		List<string> list = new List<string>();
		for (int i = 0; i < uniq_ids.Length; i++)
		{
			list.Add(uniq_ids[i].ToString());
		}
		requestSendForm.meids = list;
		Protocol.Send(SmithEvolveModel.URL, requestSendForm, delegate(SmithEvolveModel ret)
		{
			EquipItemInfo equipItemInfo = null;
			if (ret.Error == Error.None)
			{
				equipItemInfo = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uid);
				if (equipItemInfo != null)
				{
					if (MonoBehaviourSingleton<StatusManager>.I.IsEquipping(equipItemInfo, -1))
					{
						MonoBehaviourSingleton<StatusManager>.I.UpdateEquip(equipItemInfo);
					}
					if (GameSaveData.instance.AddNewItem(ItemIcon.GetItemIconType(equipItemInfo.tableData.type), equipItemInfo.uniqueID.ToString()))
					{
						GameSaveData.Save();
					}
				}
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE);
			}
			call_back(ret.Error, equipItemInfo);
		}, string.Empty);
	}

	public void SendShadowEvolveEquipItem(ulong uid, uint itemId, Action<Error, EquipItemInfo> call_back)
	{
		SmithShadowEvolveModel.RequestSendForm requestSendForm = new SmithShadowEvolveModel.RequestSendForm();
		requestSendForm.euid = uid.ToString();
		requestSendForm.iid = (int)itemId;
		Protocol.Send(SmithShadowEvolveModel.URL, requestSendForm, delegate(SmithShadowEvolveModel ret)
		{
			EquipItemInfo equipItemInfo = null;
			if (ret.Error == Error.None)
			{
				equipItemInfo = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uid);
				if (equipItemInfo != null)
				{
					if (MonoBehaviourSingleton<StatusManager>.I.IsEquipping(equipItemInfo, -1))
					{
						MonoBehaviourSingleton<StatusManager>.I.UpdateEquip(equipItemInfo);
					}
					if (GameSaveData.instance.AddNewItem(ItemIcon.GetItemIconType(equipItemInfo.tableData.type), equipItemInfo.uniqueID.ToString()))
					{
						GameSaveData.Save();
					}
				}
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE);
			}
			call_back(ret.Error, equipItemInfo);
		}, string.Empty);
	}

	public void SendExceedEquipItem(ulong uid, uint itemId, Action<Error, EquipItemInfo> call_back)
	{
		SmithExceedModel.RequestSendForm requestSendForm = new SmithExceedModel.RequestSendForm();
		requestSendForm.euid = uid.ToString();
		requestSendForm.iid = (int)itemId;
		Protocol.Send(SmithExceedModel.URL, requestSendForm, delegate(SmithExceedModel ret)
		{
			EquipItemInfo equipItemInfo = null;
			if (ret.Error == Error.None)
			{
				equipItemInfo = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uid);
				if (equipItemInfo != null && MonoBehaviourSingleton<StatusManager>.I.IsEquipping(equipItemInfo, -1))
				{
					MonoBehaviourSingleton<StatusManager>.I.UpdateEquip(equipItemInfo);
				}
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_GROW);
			}
			call_back(ret.Error, equipItemInfo);
		}, string.Empty);
	}

	public void SendAbilityChangeEquipItem(ulong euid, Action<Error, EquipItemInfo> call_back)
	{
		SmithAbilityChangeModel.RequestSendForm requestSendForm = new SmithAbilityChangeModel.RequestSendForm();
		requestSendForm.euid = euid.ToString();
		Protocol.Send(SmithAbilityChangeModel.URL, requestSendForm, delegate(SmithAbilityChangeModel ret)
		{
			EquipItemInfo equipItemInfo = null;
			if (ret.Error == Error.None)
			{
				equipItemInfo = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(euid);
				if (equipItemInfo != null && MonoBehaviourSingleton<StatusManager>.I.IsEquipping(equipItemInfo, -1))
				{
					MonoBehaviourSingleton<StatusManager>.I.UpdateEquip(equipItemInfo);
				}
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_ABILITY);
			}
			call_back(ret.Error, equipItemInfo);
		}, string.Empty);
	}

	public void SendUseAbilityItem(ulong equipUniqueId, ulong abilityItemId, Action<Error, EquipItemInfo> call_back)
	{
		SmithAbilityItemUseModel.RequestSendForm requestSendForm = new SmithAbilityItemUseModel.RequestSendForm();
		requestSendForm.euid = equipUniqueId.ToString();
		requestSendForm.auid = abilityItemId.ToString();
		Protocol.Send(SmithAbilityItemUseModel.URL, requestSendForm, delegate(SmithAbilityItemUseModel ret)
		{
			EquipItemInfo equipItemInfo = null;
			if (ret.Error == Error.None)
			{
				equipItemInfo = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(equipUniqueId);
				if (equipItemInfo != null && MonoBehaviourSingleton<StatusManager>.I.IsEquipping(equipItemInfo, -1))
				{
					MonoBehaviourSingleton<StatusManager>.I.UpdateEquip(equipItemInfo);
				}
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_ABILITY);
			}
			call_back(ret.Error, equipItemInfo);
		}, string.Empty);
	}

	public void SendGetAbilityList(ulong equipUniqueId, Action<Error, List<SmithGetAbilityList.Param>> call_back)
	{
		SmithGetAbilityList.RequestSendForm requestSendForm = new SmithGetAbilityList.RequestSendForm();
		requestSendForm.euid = equipUniqueId.ToString();
		Protocol.Send(SmithGetAbilityList.URL, requestSendForm, delegate(SmithGetAbilityList ret)
		{
			List<SmithGetAbilityList.Param> arg = null;
			if (ret.Error == Error.None)
			{
				arg = ret.result;
			}
			call_back(ret.Error, arg);
		}, string.Empty);
	}

	public void SendGetAbilityListPreGenerate(uint createId, Action<Error, List<SmithGetAbilityListForCreateModel.Param>> call_back)
	{
		SmithGetAbilityListForCreateModel.RequestSendForm requestSendForm = new SmithGetAbilityListForCreateModel.RequestSendForm();
		requestSendForm.cid = createId.ToString();
		Protocol.Send(SmithGetAbilityListForCreateModel.URL, requestSendForm, delegate(SmithGetAbilityListForCreateModel ret)
		{
			List<SmithGetAbilityListForCreateModel.Param> arg = null;
			if (ret.Error == Error.None)
			{
				arg = ret.result;
			}
			call_back(ret.Error, arg);
		}, string.Empty);
	}

	public void SendGrowSkill(SkillItemInfo base_skill, SkillItemInfo[] material, Action<SkillItemInfo, bool> call_back)
	{
		string suid = base_skill.uniqueID.ToString();
		List<string> list = new List<string>();
		List<AlchemyGrowModel.RequestSendForm.MagiItem> list2 = new List<AlchemyGrowModel.RequestSendForm.MagiItem>();
		int i = 0;
		for (int num = material.Length; i < num; i++)
		{
			if (material[i].tableID >= 401900001 && material[i].tableID <= 401900005)
			{
				bool flag = false;
				int j = 0;
				for (int count = list2.Count; j < count; j++)
				{
					if (list2[j].uiuid == material[i].uniqueID.ToString())
					{
						list2[j].num++;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					list2.Add(new AlchemyGrowModel.RequestSendForm.MagiItem
					{
						uiuid = material[i].uniqueID.ToString(),
						num = 1
					});
				}
			}
			else
			{
				list.Add(material[i].uniqueID.ToString());
			}
		}
		AlchemyGrowModel.RequestSendForm requestSendForm = new AlchemyGrowModel.RequestSendForm();
		requestSendForm.suid = suid;
		requestSendForm.uuids = list;
		requestSendForm.uiuids = list2;
		Protocol.Send(AlchemyGrowModel.URL, requestSendForm, delegate(AlchemyGrowModel ret)
		{
			SkillItemInfo skillItemInfo = null;
			bool arg = false;
			if (ret.Error == Error.None)
			{
				arg = ret.result.greatSuccess;
				skillItemInfo = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(base_skill.uniqueID);
				if (skillItemInfo != null)
				{
					GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.SKILL_ATTACK, skillItemInfo.uniqueID);
				}
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_GROW);
			}
			call_back(skillItemInfo, arg);
		}, string.Empty);
	}

	public void SendExceedSkill(SkillItemInfo base_skill, SkillItemInfo[] material, Action<SkillItemInfo, bool> call_back)
	{
		string suid = base_skill.uniqueID.ToString();
		List<string> list = new List<string>();
		int i = 0;
		for (int num = material.Length; i < num; i++)
		{
			list.Add(material[i].uniqueID.ToString());
		}
		AlchemyExceedModel.RequestSendForm requestSendForm = new AlchemyExceedModel.RequestSendForm();
		requestSendForm.suid = suid;
		requestSendForm.uuids = list;
		Protocol.Send(AlchemyExceedModel.URL, requestSendForm, delegate(AlchemyExceedModel ret)
		{
			SkillItemInfo skillItemInfo = null;
			bool arg = false;
			if (ret.Error == Error.None)
			{
				arg = ret.result.greatSuccess;
				skillItemInfo = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(base_skill.uniqueID);
				if (skillItemInfo != null)
				{
					GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.SKILL_ATTACK, skillItemInfo.uniqueID);
				}
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_GROW);
			}
			call_back(skillItemInfo, arg);
		}, string.Empty);
	}

	public void SendRevertLithograph(ulong euid, Action<bool> call_back)
	{
		SmithRestoreModel.RequestSendForm requestSendForm = new SmithRestoreModel.RequestSendForm();
		requestSendForm.euid = euid.ToString();
		requestSendForm.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		Protocol.Send(SmithRestoreModel.URL, requestSendForm, delegate(InventorySellEquipModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void CheckSmithSectionBlur(string scene_name, string section_name, GameSceneTables.SectionData section_data)
	{
		if (!(section_data == (GameSceneTables.SectionData)null) && !section_data.type.IsDialog())
		{
			switch (section_name)
			{
			case "SmithGrowItemSelect":
			case "SmithGrow":
			case "SmithCreateItemSelect":
			case "SmithCreateItem":
				if (MonoBehaviourSingleton<FilterManager>.IsValid() && !MonoBehaviourSingleton<FilterManager>.I.IsEnabledBlur())
				{
					EnableSmithBlur();
				}
				break;
			default:
				if (MonoBehaviourSingleton<FilterManager>.IsValid() && MonoBehaviourSingleton<FilterManager>.I.IsEnabledBlur() && section_name != "StatusToSmith")
				{
					DisableSmithBlur(scene_name != "SmithScene");
				}
				break;
			}
		}
	}

	public void EnableSmithBlur()
	{
		isEnableSmithBlur = true;
	}

	public void DisableSmithBlur(bool is_instans_end_anim)
	{
		if (isEnableSmithBlur)
		{
			isEnableSmithBlur = false;
		}
	}
}
