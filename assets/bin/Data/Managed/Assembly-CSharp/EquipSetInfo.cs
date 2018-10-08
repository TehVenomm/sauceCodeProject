using Network;
using System.Collections.Generic;

public class EquipSetInfo
{
	public const int INVISIBLE_HELM = 0;

	public const int VISIBLE_HELM = 1;

	public const int COMPLY_USER_STATUS_SHOW_HELM = 2;

	public EquipItemInfo[] item = new EquipItemInfo[7];

	public string name = "装備セット";

	public int showHelm;

	public EquipSetInfo(EquipSet recv_data)
	{
		item[0] = (string.IsNullOrEmpty(recv_data.weapon_0.uniqId) ? null : new EquipItemInfo(recv_data.weapon_0));
		item[1] = (string.IsNullOrEmpty(recv_data.weapon_1.uniqId) ? null : new EquipItemInfo(recv_data.weapon_1));
		item[2] = (string.IsNullOrEmpty(recv_data.weapon_2.uniqId) ? null : new EquipItemInfo(recv_data.weapon_2));
		item[3] = (string.IsNullOrEmpty(recv_data.armor.uniqId) ? null : new EquipItemInfo(recv_data.armor));
		item[4] = (string.IsNullOrEmpty(recv_data.helm.uniqId) ? null : new EquipItemInfo(recv_data.helm));
		item[5] = (string.IsNullOrEmpty(recv_data.arm.uniqId) ? null : new EquipItemInfo(recv_data.arm));
		item[6] = (string.IsNullOrEmpty(recv_data.leg.uniqId) ? null : new EquipItemInfo(recv_data.leg));
		name = recv_data.setName;
		showHelm = recv_data.showHelm;
	}

	public EquipSetInfo(EquipSetSimple recv_data)
	{
		item[0] = (string.IsNullOrEmpty(recv_data.weapon_0) ? null : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(ulong.Parse(recv_data.weapon_0)));
		item[1] = (string.IsNullOrEmpty(recv_data.weapon_1) ? null : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(ulong.Parse(recv_data.weapon_1)));
		item[2] = (string.IsNullOrEmpty(recv_data.weapon_2) ? null : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(ulong.Parse(recv_data.weapon_2)));
		item[3] = (string.IsNullOrEmpty(recv_data.armor) ? null : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(ulong.Parse(recv_data.armor)));
		item[4] = (string.IsNullOrEmpty(recv_data.helm) ? null : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(ulong.Parse(recv_data.helm)));
		item[5] = (string.IsNullOrEmpty(recv_data.arm) ? null : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(ulong.Parse(recv_data.arm)));
		item[6] = (string.IsNullOrEmpty(recv_data.leg) ? null : MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(ulong.Parse(recv_data.leg)));
		name = recv_data.setName;
		showHelm = recv_data.showHelm;
	}

	public EquipSetInfo(EquipItemInfo[] equip_item_info_ary, string equipName, int showHelm)
	{
		if (equip_item_info_ary == null || equip_item_info_ary.Length < 7)
		{
			Log.Warning("EquipSetInfo data is short or null");
		}
		else
		{
			int i = 0;
			for (int num = 7; i < num; i++)
			{
				item[i] = equip_item_info_ary[i];
			}
			name = equipName;
			this.showHelm = showHelm;
		}
	}

	public EquipSetInfo SwapArmorAndHelm()
	{
		EquipSetInfo equipSetInfo = new EquipSetInfo(item, name, showHelm);
		equipSetInfo.item[3] = item[4];
		equipSetInfo.item[4] = item[3];
		return equipSetInfo;
	}

	public ItemStatus GetTotalEquipTypeBuff(EQUIPMENT_TYPE type)
	{
		ItemStatus itemStatus = new ItemStatus();
		int equipmentTypeIndex = MonoBehaviourSingleton<StatusManager>.I.GetEquipmentTypeIndex(type);
		EquipItemInfo[] array = item;
		foreach (EquipItemInfo equipItemInfo in array)
		{
			if (equipItemInfo != null)
			{
				ItemStatus[] equipTypeSkillParam = equipItemInfo.GetEquipTypeSkillParam();
				ItemStatus param = equipTypeSkillParam[equipmentTypeIndex + 1];
				itemStatus.Add(param);
			}
		}
		return itemStatus;
	}

	public ItemStatus GetTotalEquipTypeBuffFromSlot(int slot)
	{
		return GetTotalEquipTypeBuff(item[slot].tableData.type);
	}

	public CharaInfo.EquipItem ConvertSelfEquipSetItem(int index, int setNo)
	{
		if (index >= item.Length || object.ReferenceEquals(item[index], null))
		{
			return null;
		}
		CharaInfo.EquipItem equipItem = new CharaInfo.EquipItem();
		equipItem.eId = (int)item[index].tableID;
		equipItem.lv = item[index].level;
		equipItem.exceed = item[index].exceed;
		int i = 0;
		for (int maxSlot = item[index].GetMaxSlot(); i < maxSlot; i++)
		{
			SkillItemInfo skillItemInfo = (setNo != -1) ? item[index].GetSkillItem(i, setNo) : item[index].GetSkillItem(i);
			if (skillItemInfo != null)
			{
				equipItem.sIds.Add((int)skillItemInfo.tableID);
				equipItem.sLvs.Add(skillItemInfo.level);
				equipItem.sExs.Add(skillItemInfo.exceedCnt);
			}
		}
		return equipItem;
	}

	public List<CharaInfo.EquipItem> ConvertSelfEquipSetItemList(int setNo = -1, bool isAddNull = false)
	{
		List<CharaInfo.EquipItem> list = new List<CharaInfo.EquipItem>();
		int i = 0;
		for (int num = item.Length; i < num; i++)
		{
			CharaInfo.EquipItem objA = ConvertSelfEquipSetItem(i, setNo);
			if (!object.ReferenceEquals(objA, null) || isAddNull)
			{
				list.Add(objA);
			}
		}
		return list;
	}

	public void ChangeName(string setName)
	{
		name = setName;
	}
}
