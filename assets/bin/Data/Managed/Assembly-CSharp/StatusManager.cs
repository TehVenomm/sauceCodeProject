using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviourSingleton<StatusManager>
{
	public class LocalVisual
	{
		public bool isVisibleHelm;

		public EquipItemInfo[] visualItem = new EquipItemInfo[4];

		public ulong VisialID(int index)
		{
			if (visualItem == null || visualItem.Length <= index)
			{
				return 0uL;
			}
			if (visualItem[index] == null)
			{
				return 0uL;
			}
			return visualItem[index].uniqueID;
		}
	}

	public class SendSetEquipData
	{
		public int set_no;

		public ulong[] item = new ulong[7];

		public int show_helm;

		public AccessoryPlaceInfo accs = new AccessoryPlaceInfo();

		public SendSetEquipData(int _set_no, ulong[] _item, int _show_helm, AccessoryPlaceInfo _acc)
		{
			set_no = _set_no;
			item = _item;
			show_helm = _show_helm;
			accs.Copy(_acc);
		}
	}

	public const int OFFSET_FRIENDLIST_EQUIPSET = 4;

	private EquipSetInfo[] equipSet;

	private EquipSetCalculator[] equipSetCalc;

	private List<EquipSetCalculator> otherEquipSetCalc = new List<EquipSetCalculator>(14);

	private EquipItemInfo equipData;

	private EquipItemInfo selectEquipData;

	public bool recommended_update_icon;

	private EquipSetInfo[] localEquipSet;

	private LocalVisual localVisual;

	private int localEquipSetNo = -1;

	private EQUIPMENT_TYPE[] _enum;

	private int _weaponStartIndex = -1;

	private int _weaponEndIndex = -1;

	private bool firstSetUserStatus = true;

	public bool isEquipSetCalcUpdate = true;

	public int otherEquipSetSaveIndex = -1;

	public bool initialized
	{
		get;
		private set;
	}

	public List<BoostStatus> boostStatus
	{
		get;
		private set;
	}

	public List<TimeSlotEvent> timeSlotEvents
	{
		get;
		private set;
	}

	public int ENABLE_EQUIP_TYPE_MAX
	{
		get;
		private set;
	}

	public CharaInfo assignedCharaInfo
	{
		get;
		private set;
	}

	public AssignedEquipmentTable.AssignedEquipmentData assignedEquipmentData
	{
		get;
		private set;
	}

	public AssignedEquipmentTable.AssignedEquipmentData EventEquipSet
	{
		get;
		private set;
	}

	public StatusManager()
	{
		ENABLE_EQUIP_TYPE_MAX = 1;
		EQUIPMENT_TYPE[] array = (EQUIPMENT_TYPE[])Enum.GetValues(typeof(EQUIPMENT_TYPE));
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			int type = (int)array[i];
			if (IsWeapon((EQUIPMENT_TYPE)type))
			{
				ENABLE_EQUIP_TYPE_MAX++;
			}
		}
	}

	private bool IsWeapon(EQUIPMENT_TYPE type)
	{
		return type >= EQUIPMENT_TYPE.ONE_HAND_SWORD && type <= EQUIPMENT_TYPE.ARROW;
	}

	private bool IsArmor(EQUIPMENT_TYPE type)
	{
		return type >= EQUIPMENT_TYPE.ARMOR && type <= EQUIPMENT_TYPE.LEG;
	}

	public void _LoadTable()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(_LoadTableData());
	}

	private IEnumerator _LoadTableData()
	{
		if (!initialized)
		{
			LoadingQueue load_queue = new LoadingQueue(this);
			LoadObject lo_equip_model_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "EquipModelTable", false);
			LoadObject lo_equip_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "EquipItemTable", false);
			LoadObject lo_equip_exceed_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "EquipItemExceedTable", false);
			LoadObject lo_skill_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "SkillItemTable", false);
			LoadObject lo_ability_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "AbilityTable", false);
			LoadObject lo_ability_data_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "AbilityDataTable", false);
			LoadObject lo_ability_item_lot_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "AbilityItemLotTable", false);
			LoadObject lo_assigned_equipment_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "AssignedEquipmentTable", false);
			if (load_queue.IsLoading())
			{
				yield return (object)load_queue.Wait();
			}
			if (!Singleton<EquipModelTable>.IsValid())
			{
				Singleton<EquipModelTable>.Create();
			}
			Singleton<EquipModelTable>.I.CreateTable(lo_equip_model_table.loadedObject.get_text());
			if (!Singleton<EquipItemTable>.IsValid())
			{
				Singleton<EquipItemTable>.Create();
			}
			Singleton<EquipItemTable>.I.CreateTable(lo_equip_table.loadedObject.get_text());
			if (!Singleton<EquipItemExceedTable>.IsValid())
			{
				Singleton<EquipItemExceedTable>.Create();
			}
			Singleton<EquipItemExceedTable>.I.CreateTable(lo_equip_exceed_table.loadedObject.get_text());
			if (!Singleton<SkillItemTable>.IsValid())
			{
				Singleton<SkillItemTable>.Create();
			}
			Singleton<SkillItemTable>.I.CreateTable(lo_skill_table.loadedObject.get_text());
			if (!Singleton<AbilityTable>.IsValid())
			{
				Singleton<AbilityTable>.Create();
			}
			Singleton<AbilityTable>.I.CreateTable(lo_ability_table.loadedObject.get_text());
			if (!Singleton<AbilityDataTable>.IsValid())
			{
				Singleton<AbilityDataTable>.Create();
			}
			Singleton<AbilityDataTable>.I.CreateTable(lo_ability_data_table.loadedObject.get_text());
			if (!Singleton<AbilityItemLotTable>.IsValid())
			{
				Singleton<AbilityItemLotTable>.Create();
			}
			Singleton<AbilityItemLotTable>.I.CreateTable(lo_ability_item_lot_table.loadedObject.get_text());
			if (!Singleton<AssignedEquipmentTable>.IsValid())
			{
				Singleton<AssignedEquipmentTable>.Create();
			}
			Singleton<AssignedEquipmentTable>.I.CreateTable(lo_assigned_equipment_table.loadedObject.get_text());
			initialized = true;
		}
	}

	public void SetAssignedEquipmentData(AssignedEquipmentTable.AssignedEquipmentData tableData, CharaInfo info)
	{
		assignedEquipmentData = tableData;
		assignedCharaInfo = info;
	}

	public void ClearTrial()
	{
		assignedEquipmentData = null;
		assignedCharaInfo = null;
	}

	public void SetupEventEquipSet(uint questid)
	{
		EventEquipSet = Singleton<AssignedEquipmentTable>.I.GetAssignedEquipmentDataFromQuestId(questid);
	}

	public void ClearEventEquipSet()
	{
		EventEquipSet = null;
	}

	public bool HasEventEquipSet()
	{
		if (EventEquipSet != null)
		{
			return true;
		}
		return false;
	}

	public EquipItemInfo GetEquippingItem()
	{
		return equipData;
	}

	public EquipItemInfo GetSelectEquipItem()
	{
		return selectEquipData;
	}

	public void SetEquippingItem(EquipItemInfo select_item)
	{
		equipData = select_item;
	}

	public void SetSelectEquipItem(EquipItemInfo select_item)
	{
		selectEquipData = select_item;
	}

	public void InitStatusEquipData()
	{
		equipData = (selectEquipData = null);
		localEquipSet = null;
		localVisual = null;
		localEquipSetNo = -1;
	}

	public void SetLocalEquipSetNo(int no)
	{
		localEquipSetNo = no;
	}

	public void SetTimeSlotEvents(List<TimeSlotEvent> statuses)
	{
		timeSlotEvents = statuses;
	}

	public void CreateLocalEquipSetData()
	{
		localEquipSet = new EquipSetInfo[equipSet.Length];
		localEquipSetNo = (MonoBehaviourSingleton<UserInfoManager>.IsValid() ? MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo : 0);
		int i = 0;
		for (int num = localEquipSet.Length; i < num; i++)
		{
			EquipSetInfo equipSetInfo = GetEquipSet(i);
			localEquipSet[i] = new EquipSetInfo(new EquipItemInfo[7]
			{
				equipSetInfo.item[0],
				equipSetInfo.item[1],
				equipSetInfo.item[2],
				equipSetInfo.item[3],
				equipSetInfo.item[4],
				equipSetInfo.item[5],
				equipSetInfo.item[6]
			}, equipSetInfo.name, equipSetInfo.showHelm, equipSetInfo.acc);
		}
		int showHelm;
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && localEquipSet[localEquipSetNo].showHelm != (showHelm = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.showHelm))
		{
			int j = 0;
			for (int num2 = localEquipSet.Length; j < num2; j++)
			{
				localEquipSet[j].showHelm = showHelm;
			}
		}
	}

	public EquipSetInfo[] GetLocalEquipSet()
	{
		return localEquipSet;
	}

	public EquipSetInfo GetCurrentLocalEquipSet()
	{
		return localEquipSet[GetCurrentEquipSetNo()];
	}

	private int GetLocalEquipSetNo()
	{
		return localEquipSetNo;
	}

	public int GetCurrentEquipSetNo()
	{
		if (GetLocalEquipSetNo() == -1)
		{
			return MonoBehaviourSingleton<UserInfoManager>.IsValid() ? MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo : 0;
		}
		return GetLocalEquipSetNo();
	}

	public void UpdateLocalEquipSet(int equip_no)
	{
		EquipSetInfo equipSetInfo = GetEquipSet(equip_no);
		localEquipSet[equip_no] = new EquipSetInfo(new EquipItemInfo[7]
		{
			equipSetInfo.item[0],
			equipSetInfo.item[1],
			equipSetInfo.item[2],
			equipSetInfo.item[3],
			equipSetInfo.item[4],
			equipSetInfo.item[5],
			equipSetInfo.item[6]
		}, equipSetInfo.name, equipSetInfo.showHelm, equipSetInfo.acc);
		if (localEquipSet[equip_no].showHelm == 2)
		{
			localEquipSet[equip_no].showHelm = ((!MonoBehaviourSingleton<UserInfoManager>.IsValid()) ? 1 : MonoBehaviourSingleton<UserInfoManager>.I.userStatus.showHelm);
		}
		ReplaceEquipSet(localEquipSet[equip_no], equip_no);
	}

	public void UpdateLocalInventory(EquipItemInfo item)
	{
		if (item != null)
		{
			if (localEquipSet != null)
			{
				int i = 0;
				for (int num = localEquipSet.Length; i < num; i++)
				{
					int j = 0;
					for (int num2 = 7; j < num2; j++)
					{
						if (localEquipSet[i].item[j] != null && localEquipSet[i].item[j].uniqueID == item.uniqueID)
						{
							localEquipSet[i].item[j] = item;
						}
					}
				}
			}
			if (localVisual != null)
			{
				int k = 0;
				for (int num3 = localVisual.visualItem.Length; k < num3; k++)
				{
					if (localVisual.visualItem[k] != null && localVisual.visualItem[k].uniqueID == item.uniqueID)
					{
						localVisual.visualItem[k] = item;
					}
				}
			}
		}
	}

	public bool IsEquippingLocal(EquipItemInfo item)
	{
		if (localEquipSet == null || item == null)
		{
			return false;
		}
		int i = 0;
		for (int num = localEquipSet.Length; i < num; i++)
		{
			int j = 0;
			for (int num2 = 7; j < num2; j++)
			{
				if (localEquipSet[i].item[j] != null && localEquipSet[i].item[j].uniqueID == item.uniqueID)
				{
					return true;
				}
			}
		}
		return false;
	}

	public void CheckChangeEquipSet(int set_no, Action<bool> callback)
	{
		List<SendSetEquipData> list = new List<SendSetEquipData>();
		int i = 0;
		for (int num = localEquipSet.Length; i < num; i++)
		{
			EquipSetInfo equipSetInfo = localEquipSet[i];
			ulong[] array = new ulong[7];
			for (int j = 0; j < 7; j++)
			{
				array[j] = ((equipSetInfo.item[j] == null) ? 0 : equipSetInfo.item[j].uniqueID);
			}
			if (MonoBehaviourSingleton<StatusManager>.I.IsChangeEquipSetInfo(i, array, equipSetInfo.showHelm, equipSetInfo.acc))
			{
				list.Add(new SendSetEquipData(i, array, equipSetInfo.showHelm, equipSetInfo.acc));
			}
		}
		if (list.Count > 0 && !PartyManager.IsValidInParty())
		{
			MonoBehaviourSingleton<StatusManager>.I.SendEquipSet(set_no, list, delegate(Error err)
			{
				if (err == Error.None)
				{
					if (callback != null)
					{
						callback(true);
					}
				}
				else if (callback != null)
				{
					callback(false);
				}
			});
			MonoBehaviourSingleton<UserInfoManager>.I.userStatus.showHelm = localEquipSet[localEquipSetNo].showHelm;
		}
		else if (set_no != MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo)
		{
			MonoBehaviourSingleton<StatusManager>.I.SendEquipSetNo(set_no, delegate(Error err)
			{
				if (err == Error.None)
				{
					if (callback != null)
					{
						callback(true);
					}
				}
				else if (callback != null)
				{
					callback(false);
				}
			});
			MonoBehaviourSingleton<UserInfoManager>.I.userStatus.showHelm = localEquipSet[localEquipSetNo].showHelm;
		}
		else
		{
			MonoBehaviourSingleton<PartyManager>.I.SendIsEquip(false, delegate
			{
			});
			if (callback != null)
			{
				callback(true);
			}
		}
	}

	public void CreateLocalVisualEquipData()
	{
		localVisual = new LocalVisual();
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		localVisual.visualItem[0] = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(ulong.Parse(userStatus.armorUniqId));
		localVisual.visualItem[1] = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(ulong.Parse(userStatus.helmUniqId));
		localVisual.visualItem[2] = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(ulong.Parse(userStatus.armUniqId));
		localVisual.visualItem[3] = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(ulong.Parse(userStatus.legUniqId));
		localVisual.isVisibleHelm = (localEquipSet[localEquipSetNo].showHelm == 1);
	}

	public LocalVisual GetLocalVisualEquip()
	{
		return localVisual;
	}

	public bool IsEquippingLocalVisual(EquipItemInfo item)
	{
		if (localVisual == null || localVisual.visualItem == null || item == null)
		{
			return false;
		}
		int i = 0;
		for (int num = localVisual.visualItem.Length; i < num; i++)
		{
			if (localVisual.visualItem[i] != null && localVisual.visualItem[i].uniqueID == item.uniqueID)
			{
				return true;
			}
		}
		return false;
	}

	public void CheckChangeVisualEquip(Action<bool> callback)
	{
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		ulong num = ulong.Parse(userStatus.armorUniqId);
		ulong num2 = ulong.Parse(userStatus.helmUniqId);
		ulong num3 = ulong.Parse(userStatus.armUniqId);
		ulong num4 = ulong.Parse(userStatus.legUniqId);
		ulong[] array = new ulong[4]
		{
			num,
			num2,
			num3,
			num4
		};
		bool flag = false;
		int i = 0;
		for (int num5 = localVisual.visualItem.Length; i < num5; i++)
		{
			ulong num6 = (localVisual.visualItem[i] == null) ? 0 : localVisual.visualItem[i].uniqueID;
			if (num6 != array[i])
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			MonoBehaviourSingleton<StatusManager>.I.SendVisualEquip(localVisual.VisialID(0), localVisual.VisialID(1), localVisual.VisialID(2), localVisual.VisialID(3), localVisual.isVisibleHelm, delegate(bool is_success)
			{
				if (callback != null)
				{
					callback(is_success);
				}
			});
		}
		else if (callback != null)
		{
			callback(true);
		}
	}

	public void CheckChangeEquip(int equip_set_no, Action<bool> callback)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(_CheckChangeEquipCoroutine(equip_set_no, callback));
	}

	private IEnumerator _CheckChangeEquipCoroutine(int equip_set_no, Action<bool> callback)
	{
		bool recv_break = false;
		bool wait_visual_equip = true;
		MonoBehaviourSingleton<StatusManager>.I.CheckChangeVisualEquip(delegate(bool is_success)
		{
			if (!is_success)
			{
				if (((_003C_CheckChangeEquipCoroutine_003Ec__Iterator24C)/*Error near IL_0034: stateMachine*/).callback != null)
				{
					((_003C_CheckChangeEquipCoroutine_003Ec__Iterator24C)/*Error near IL_0034: stateMachine*/).callback(false);
				}
				((_003C_CheckChangeEquipCoroutine_003Ec__Iterator24C)/*Error near IL_0034: stateMachine*/)._003Crecv_break_003E__0 = true;
			}
			else
			{
				((_003C_CheckChangeEquipCoroutine_003Ec__Iterator24C)/*Error near IL_0034: stateMachine*/)._003Cwait_visual_equip_003E__1 = false;
			}
		});
		bool wait_equip = true;
		MonoBehaviourSingleton<StatusManager>.I.CheckChangeEquipSet(equip_set_no, delegate(bool is_success)
		{
			if (!is_success)
			{
				if (((_003C_CheckChangeEquipCoroutine_003Ec__Iterator24C)/*Error near IL_0057: stateMachine*/).callback != null)
				{
					((_003C_CheckChangeEquipCoroutine_003Ec__Iterator24C)/*Error near IL_0057: stateMachine*/).callback(false);
				}
				((_003C_CheckChangeEquipCoroutine_003Ec__Iterator24C)/*Error near IL_0057: stateMachine*/)._003Crecv_break_003E__0 = true;
			}
			((_003C_CheckChangeEquipCoroutine_003Ec__Iterator24C)/*Error near IL_0057: stateMachine*/)._003Cwait_equip_003E__2 = false;
		});
		while (wait_equip || wait_visual_equip)
		{
			if (recv_break)
			{
				callback?.Invoke(false);
				yield break;
			}
			yield return (object)null;
		}
		callback?.Invoke(true);
	}

	public int EquipSetNum()
	{
		if (equipSet == null)
		{
			return 0;
		}
		return equipSet.Length;
	}

	public EquipSetInfo GetEquipSet(int set_no)
	{
		if (set_no >= equipSet.Length)
		{
			return null;
		}
		return equipSet[set_no];
	}

	public bool IsEquipping(EquipItemInfo item, int set_no = -1)
	{
		if (item == null || item.uniqueID == 0L)
		{
			return false;
		}
		if (set_no != -1)
		{
			return IsEquipping(set_no, item, null);
		}
		int i = 0;
		for (int num = equipSet.Length; i < num; i++)
		{
			if (IsEquipping(i, item, null))
			{
				return true;
			}
		}
		return false;
	}

	public unsafe void UpdateEquip(EquipItemInfo equip)
	{
		int i = 0;
		_003CUpdateEquip_003Ec__AnonStorey6BE _003CUpdateEquip_003Ec__AnonStorey6BE;
		for (int num = equipSet.Length; i < num; i++)
		{
			IsEquipping(i, equip, new Action<int, int>((object)_003CUpdateEquip_003Ec__AnonStorey6BE, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private bool IsEquipping(int set_no, EquipItemInfo item, Action<int, int> callback = null)
	{
		if (item == null || item.uniqueID == 0L)
		{
			return false;
		}
		EquipSetInfo equipSetInfo = MonoBehaviourSingleton<StatusManager>.I.GetEquipSet(set_no);
		if (equipSetInfo != null)
		{
			int i = 0;
			for (int num = equipSetInfo.item.Length; i < num; i++)
			{
				if (equipSetInfo.item[i] != null && equipSetInfo.item[i].uniqueID != 0L && equipSetInfo.item[i].uniqueID == item.uniqueID)
				{
					callback?.Invoke(set_no, i);
					return true;
				}
			}
		}
		return false;
	}

	public bool IsChangeEquipSetInfo(int set_no, ulong[] change_equip_items, int show_helm, AccessoryPlaceInfo acc)
	{
		if (set_no >= equipSet.Length)
		{
			return false;
		}
		EquipSetInfo equipSetInfo = equipSet[set_no];
		if (change_equip_items != null && change_equip_items.Length != equipSetInfo.item.Length)
		{
			return false;
		}
		if (equipSetInfo.showHelm != show_helm)
		{
			return true;
		}
		int i = 0;
		for (int num = equipSetInfo.item.Length; i < num; i++)
		{
			ulong num2 = (equipSetInfo.item[i] == null) ? 0 : equipSetInfo.item[i].uniqueID;
			if (change_equip_items[i] != num2)
			{
				return true;
			}
		}
		if (!equipSetInfo.acc.IsEqual(acc))
		{
			return true;
		}
		return false;
	}

	public StageObjectManager.CreatePlayerInfo GetCreatePlayerInfo()
	{
		StageObjectManager.CreatePlayerInfo createPlayerInfo = new StageObjectManager.CreatePlayerInfo();
		createPlayerInfo.charaInfo = new CharaInfo();
		createPlayerInfo.extentionInfo = new StageObjectManager.CreatePlayerInfo.ExtentionInfo();
		if (MonoBehaviourSingleton<FieldManager>.I.isTutorialField)
		{
			createPlayerInfo.charaInfo.name = "???";
			createPlayerInfo.charaInfo.comment = string.Empty;
			createPlayerInfo.charaInfo.hp = 200;
			createPlayerInfo.charaInfo.atk = 100;
			createPlayerInfo.charaInfo.def = 100;
			createPlayerInfo.charaInfo.level = 1;
			createPlayerInfo.charaInfo.aId = 11000001;
			createPlayerInfo.charaInfo.hId = 0;
			createPlayerInfo.charaInfo.rId = 0;
			createPlayerInfo.charaInfo.lId = 0;
		}
		else if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
			createPlayerInfo.charaInfo.userId = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
			createPlayerInfo.charaInfo.name = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name;
			createPlayerInfo.charaInfo.comment = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.comment;
			createPlayerInfo.charaInfo.code = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.code;
			createPlayerInfo.charaInfo.hp = userStatus.hp;
			createPlayerInfo.charaInfo.atk = userStatus.atk;
			createPlayerInfo.charaInfo.def = userStatus.def;
			createPlayerInfo.charaInfo.level = userStatus.level;
			createPlayerInfo.charaInfo.sex = userStatus.sex;
			createPlayerInfo.charaInfo.faceId = userStatus.faceId;
			createPlayerInfo.charaInfo.hairId = userStatus.hairId;
			createPlayerInfo.charaInfo.hairColorId = userStatus.hairColorId;
			createPlayerInfo.charaInfo.skinId = userStatus.skinId;
			createPlayerInfo.charaInfo.voiceId = userStatus.voiceId;
			createPlayerInfo.charaInfo.aId = (int)MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.armorUniqId);
			createPlayerInfo.charaInfo.hId = (int)MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.helmUniqId);
			createPlayerInfo.charaInfo.rId = (int)MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.armUniqId);
			createPlayerInfo.charaInfo.lId = (int)MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(userStatus.legUniqId);
			createPlayerInfo.charaInfo.showHelm = userStatus.showHelm;
			if (MonoBehaviourSingleton<GuildManager>.I.guildData != null)
			{
				createPlayerInfo.charaInfo.clanInfo = new CharaInfo.ClanInfo();
				createPlayerInfo.charaInfo.clanInfo.clanId = MonoBehaviourSingleton<GuildManager>.I.guildData.clanId;
				createPlayerInfo.charaInfo.clanInfo.tag = MonoBehaviourSingleton<GuildManager>.I.guildData.tag;
				createPlayerInfo.charaInfo.clanInfo.emblem = MonoBehaviourSingleton<GuildManager>.I.guildData.emblem;
			}
		}
		for (int i = 0; i < 7; i++)
		{
			CharaInfo.EquipItem equipItem = null;
			EquipItemInfo equippingItemInfo = GetEquippingItemInfo(i, -1);
			if (equippingItemInfo != null)
			{
				equipItem = new CharaInfo.EquipItem();
				equipItem.eId = (int)equippingItemInfo.tableID;
				equipItem.lv = equippingItemInfo.level;
				equipItem.exceed = equippingItemInfo.exceed;
				int j = 0;
				for (int maxSlot = equippingItemInfo.GetMaxSlot(); j < maxSlot; j++)
				{
					SkillItemInfo skillItem = equippingItemInfo.GetSkillItem(j, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo);
					if (skillItem != null)
					{
						equipItem.sIds.Add((int)skillItem.tableID);
						equipItem.sLvs.Add(skillItem.level);
						equipItem.sExs.Add(skillItem.exceedCnt);
					}
				}
				EquipItemAbility[] lotteryAbility = equippingItemInfo.GetLotteryAbility();
				int k = 0;
				for (int num = lotteryAbility.Length; k < num; k++)
				{
					if (lotteryAbility[k].id != 0)
					{
						equipItem.aIds.Add((int)lotteryAbility[k].id);
						equipItem.aPts.Add(lotteryAbility[k].ap);
					}
				}
				AbilityItemInfo abilityItem = equippingItemInfo.GetAbilityItem();
				if (abilityItem != null && abilityItem.tableID != 0)
				{
					equipItem.ai = abilityItem.originalData;
				}
			}
			if (MonoBehaviourSingleton<FieldManager>.I.isTutorialField && i == 0)
			{
				equipItem = new CharaInfo.EquipItem();
				equipItem.eId = 10000001;
				equipItem.lv = 1;
				equipItem.sIds.Add(100200001);
				equipItem.sLvs.Add(1);
				equipItem.sExs.Add(0);
			}
			if (equipItem != null)
			{
				createPlayerInfo.charaInfo.equipSet.Add(equipItem);
			}
			if (i >= 0 && i < 3)
			{
				int item = -1;
				if (equipItem != null)
				{
					item = createPlayerInfo.charaInfo.equipSet.Count - 1;
				}
				createPlayerInfo.extentionInfo.weaponIndexList.Add(item);
			}
		}
		AccessoryPlaceInfo equippingAccessoryInfo = GetEquippingAccessoryInfo(-1);
		if (equippingAccessoryInfo != null)
		{
			createPlayerInfo.charaInfo.accessory = equippingAccessoryInfo.ConvertAccessory();
		}
		return createPlayerInfo;
	}

	public StageObjectManager.CreatePlayerInfo GetAssignedCreatePlayerInfo()
	{
		StageObjectManager.CreatePlayerInfo createPlayerInfo = new StageObjectManager.CreatePlayerInfo();
		if (assignedCharaInfo != null)
		{
			createPlayerInfo.charaInfo = assignedCharaInfo;
		}
		return createPlayerInfo;
	}

	public int GetEquippingShowHelm(int set_no = -1)
	{
		if (set_no == -1 && MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			set_no = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
		}
		if (equipSet == null)
		{
			return 1;
		}
		if (set_no < 0)
		{
			return 1;
		}
		if (set_no >= equipSet.Length)
		{
			return 1;
		}
		if (equipSet[set_no] == null)
		{
			return 1;
		}
		if (equipSet[set_no].showHelm == 2)
		{
			return (!MonoBehaviourSingleton<UserInfoManager>.IsValid()) ? 1 : MonoBehaviourSingleton<UserInfoManager>.I.userStatus.showHelm;
		}
		return equipSet[set_no].showHelm;
	}

	public EquipItemInfo GetEquippingItemInfo(int equip_slot, int set_no = -1)
	{
		if (set_no == -1 && MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			set_no = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
		}
		if (equipSet == null)
		{
			return null;
		}
		if (set_no < 0)
		{
			return null;
		}
		if (set_no >= equipSet.Length)
		{
			return null;
		}
		if (equip_slot >= 7)
		{
			return null;
		}
		if (equipSet[set_no] == null)
		{
			return null;
		}
		if (equipSet[set_no].item == null)
		{
			return null;
		}
		return equipSet[set_no].item[equip_slot];
	}

	public AccessoryPlaceInfo GetEquippingAccessoryInfo(int set_no = -1)
	{
		if (set_no == -1 && MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			set_no = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
		}
		if (equipSet == null)
		{
			return null;
		}
		if (set_no < 0)
		{
			return null;
		}
		if (set_no >= equipSet.Length)
		{
			return null;
		}
		if (equipSet[set_no] == null)
		{
			return null;
		}
		return equipSet[set_no].acc;
	}

	public uint GetEquippingItemTableID(int equip_slot, int set_no = -1)
	{
		EquipItemInfo equippingItemInfo = GetEquippingItemInfo(equip_slot, set_no);
		if (equippingItemInfo == null)
		{
			return 0u;
		}
		return equippingItemInfo.tableID;
	}

	public EquipItemInfo GetEquipmentWeaponInfo(int equip_slot = 0, int set_no = -1)
	{
		if (equip_slot >= 3)
		{
			return null;
		}
		return GetEquippingItemInfo(equip_slot, set_no);
	}

	public string GetEquipItemGroupString(EQUIPMENT_TYPE type)
	{
		if (Singleton<StringTable>.IsValid())
		{
			return StringTable.Get(STRING_CATEGORY.EQUIP, (uint)type);
		}
		return null;
	}

	public string GetSkillItemGroupString(SKILL_SLOT_TYPE type)
	{
		if (Singleton<StringTable>.IsValid())
		{
			return StringTable.Get(STRING_CATEGORY.SKILL, (uint)type);
		}
		return null;
	}

	public EquipItemStatus GetEquipSetAllSkillParam(int equip_set_no, bool is_local = false, params int[] exclusion_index)
	{
		EquipItemStatus equipItemStatus = new EquipItemStatus();
		EquipSetInfo[] array = (!is_local) ? equipSet : localEquipSet;
		if (array == null)
		{
			return null;
		}
		for (int i = 0; i < 7; i++)
		{
			if (exclusion_index != null)
			{
				bool flag = false;
				if (exclusion_index != null)
				{
					int j = 0;
					for (int num = exclusion_index.Length; j < num; j++)
					{
						if (exclusion_index[j] == i)
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					continue;
				}
			}
			EquipItemInfo equipItemInfo = array[equip_set_no].item[i];
			if (equipItemInfo != null)
			{
				equipItemStatus.Add(equipItemInfo.GetEquipSkillParam());
			}
		}
		return equipItemStatus;
	}

	public int GetEquipmentTypeIndex(EQUIPMENT_TYPE type)
	{
		int num = -1;
		if (_enum == null)
		{
			_enum = (EQUIPMENT_TYPE[])Enum.GetValues(typeof(EQUIPMENT_TYPE));
			int i = 0;
			for (int num2 = _enum.Length; i < num2; i++)
			{
				if (_enum[i] == EQUIPMENT_TYPE.ONE_HAND_SWORD)
				{
					_weaponStartIndex = i;
				}
				else if (_enum[i] == EQUIPMENT_TYPE.ARROW)
				{
					_weaponEndIndex = i;
				}
				if (_enum[i] == type)
				{
					num = i;
				}
				if (_weaponStartIndex >= 0 && _weaponEndIndex >= 0 && num >= 0)
				{
					break;
				}
			}
		}
		else if (IsWeapon(type))
		{
			int j = 0;
			for (int num3 = _enum.Length; j < num3; j++)
			{
				if (_enum[j] == type)
				{
					num = j;
					break;
				}
			}
		}
		if (!IsArmor(type))
		{
			if (num <= _weaponEndIndex)
			{
				return num - _weaponStartIndex;
			}
			return -1;
		}
		return _weaponEndIndex + 1 - _weaponStartIndex;
	}

	public void CalcSelfStatusParam(EquipSetInfo set_info, out int _atk, out int _def, out int _hp)
	{
		CalcSelfStatusParam(set_info, out _atk, out _def, out _hp, out int _, out int _);
	}

	public void CalcSelfStatusParam(EquipSetInfo set_info, out int _atk, out int _def, out int _hp, out int _elem_type_atk, out int _elem_type_def)
	{
		CharaInfo.EquipItem[] array = new CharaInfo.EquipItem[set_info.item.Length];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			if (set_info.item[i] == null)
			{
				array[i] = null;
			}
			else
			{
				array[i] = new CharaInfo.EquipItem();
				array[i].eId = (int)set_info.item[i].tableID;
				array[i].lv = set_info.item[i].level;
				array[i].exceed = set_info.item[i].exceed;
				int j = 0;
				for (int maxSlot = set_info.item[i].GetMaxSlot(); j < maxSlot; j++)
				{
					SkillItemInfo skillItem = set_info.item[i].GetSkillItem(j, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo);
					if (skillItem != null)
					{
						array[i].sIds.Add((int)skillItem.tableID);
						array[i].sLvs.Add(skillItem.level);
						array[i].sExs.Add(skillItem.exceedCnt);
					}
				}
			}
		}
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		_CalcUserStatusParam(array, userStatus.atk, userStatus.def, userStatus.hp, out _atk, out _def, out _hp, out _elem_type_atk, out _elem_type_def);
	}

	public void CalcUserStatusParam(CharaInfo user_info, out int _atk, out int _def, out int _hp)
	{
		_CalcUserStatusParam(user_info.equipSet.ToArray(), user_info.atk, user_info.def, user_info.hp, out _atk, out _def, out _hp, out int _, out int _);
	}

	public void CalcUserStatusParam(List<CharaInfo.EquipItem> equip_set, int base_atk, int base_def, int base_hp, out int _atk, out int _def, out int _hp)
	{
		_CalcUserStatusParam(equip_set.ToArray(), base_atk, base_def, base_hp, out _atk, out _def, out _hp, out int _, out int _);
	}

	private void _CalcUserStatusParam(CharaInfo.EquipItem[] set_item, int status_atk, int status_def, int status_hp, out int _atk, out int _def, out int _hp, out int _elem_type_atk, out int _elem_type_def)
	{
		_atk = 0;
		_def = 0;
		_hp = 0;
		_elem_type_atk = 0;
		_elem_type_def = 0;
		int num = set_item.Length;
		EquipItemStatus skill = new EquipItemStatus();
		EquipItemStatus[] equip = new EquipItemStatus[num];
		bool[] equip_is_weapon = new bool[num];
		int[] equip_atk_elem_type = new int[num];
		int[] equip_def_elem_type = new int[num];
		int index = 0;
		int main_weapon_index = -1;
		EquipItemTable.EquipItemData mainWeaponItemData = null;
		Array.ForEach(set_item, delegate(CharaInfo.EquipItem data)
		{
			if (data == null)
			{
				equip[index] = null;
				index++;
			}
			else
			{
				equip[index] = new EquipItemStatus();
				equip_atk_elem_type[index] = 6;
				equip_def_elem_type[index] = 6;
				EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)data.eId);
				if (equipItemData != null)
				{
					bool flag = equipItemData.type < EQUIPMENT_TYPE.ARMOR;
					equip_is_weapon[index] = flag;
					if (main_weapon_index < 0 && flag)
					{
						main_weapon_index = index;
						mainWeaponItemData = equipItemData;
					}
					if (data.lv > 1)
					{
						GrowEquipItemTable.GrowEquipItemData growEquipItemData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(equipItemData.growID, (uint)data.lv);
						if (equipItemData != null && growEquipItemData != null)
						{
							equip[index].atk = growEquipItemData.GetGrowParamAtk(equipItemData.baseAtk);
							equip[index].def = growEquipItemData.GetGrowParamDef(equipItemData.baseDef);
							equip[index].hp = growEquipItemData.GetGrowParamHp(equipItemData.baseHp);
							int[] growParamElemAtk = growEquipItemData.GetGrowParamElemAtk(equipItemData.atkElement);
							int[] growParamElemDef = growEquipItemData.GetGrowParamElemDef(equipItemData.defElement);
							int j = 0;
							for (int num6 = growParamElemAtk.Length; j < num6; j++)
							{
								equip[index].elemAtk[j] = growParamElemAtk[j];
								equip[index].elemDef[j] = growParamElemDef[j];
							}
						}
					}
					else
					{
						equip[index].atk = equipItemData.baseAtk;
						equip[index].def = equipItemData.baseDef;
						equip[index].hp = equipItemData.baseHp;
						int[] atkElement = equipItemData.atkElement;
						int[] defElement = equipItemData.defElement;
						int k = 0;
						for (int num7 = atkElement.Length; k < num7; k++)
						{
							equip[index].elemAtk[k] = atkElement[k];
							equip[index].elemDef[k] = defElement[k];
						}
					}
					int[] exceed_elem = null;
					int[] exceed_elem2 = null;
					EquipItemExceedParamTable.EquipItemExceedParamAll exceedParam = equipItemData.GetExceedParam((uint)data.exceed);
					if (exceedParam != null)
					{
						exceed_elem = exceedParam.atkElement;
						exceed_elem2 = exceedParam.defElement;
						int l = 0;
						for (int num8 = equip[index].elemAtk.Length; l < num8; l++)
						{
							equip[index].elemAtk[l] += exceedParam.atkElement[l];
							equip[index].elemDef[l] += exceedParam.defElement[l];
						}
						equip[index].atk += exceedParam.atk;
						equip[index].def += exceedParam.def;
						equip[index].hp += exceedParam.hp;
					}
					equip_atk_elem_type[index] = equipItemData.GetElemAtkType(exceed_elem);
					equip_def_elem_type[index] = equipItemData.GetElemDefType(exceed_elem2);
					int count = data.sIds.Count;
					if (count > 0 && count == data.sLvs.Count)
					{
						int[] array = data.sIds.ToArray();
						int[] array2 = data.sLvs.ToArray();
						for (int m = 0; m < count; m++)
						{
							SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)array[m]);
							if (skillItemData != null)
							{
								GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillItemData.growID, array2[m]);
								if (growSkillItemData != null)
								{
									skill.atk += growSkillItemData.GetGrowParamAtk(skillItemData.baseAtk);
									int[] growParamElemAtk2 = growSkillItemData.GetGrowParamElemAtk(skillItemData.atkElement);
									int n = 0;
									for (int num9 = growParamElemAtk2.Length; n < num9; n++)
									{
										skill.elemAtk[n] += growParamElemAtk2[n];
									}
									skill.def += growSkillItemData.GetGrowParamDef(skillItemData.baseDef);
									skill.hp += growSkillItemData.GetGrowParamHp(skillItemData.baseHp);
									int[] growParamElemDef2 = growSkillItemData.GetGrowParamElemDef(skillItemData.defElement);
									int num10 = 0;
									for (int num11 = growParamElemDef2.Length; num10 < num11; num10++)
									{
										skill.elemDef[num10] += growParamElemDef2[num10];
									}
								}
								else
								{
									skill.atk += skillItemData.baseAtk;
									int[] atkElement2 = skillItemData.atkElement;
									int num12 = 0;
									for (int num13 = atkElement2.Length; num12 < num13; num12++)
									{
										skill.elemAtk[num12] += atkElement2[num12];
									}
									skill.def += skillItemData.baseDef;
									skill.hp += skillItemData.baseHp;
									int[] defElement2 = skillItemData.defElement;
									int num14 = 0;
									for (int num15 = defElement2.Length; num14 < num15; num14++)
									{
										skill.elemDef[num14] += defElement2[num14];
									}
								}
								if (mainWeaponItemData != null && skillItemData.IsMatchSupportEquipType(mainWeaponItemData.type))
								{
									for (int num16 = 0; num16 < skillItemData.supportValue.Length; num16++)
									{
										if (growSkillItemData != null)
										{
											skill.atk += growSkillItemData.GetGrowParamSupprtValue(skillItemData.supportValue, num16);
										}
										else
										{
											skill.atk += skillItemData.supportValue[num16];
										}
									}
								}
							}
						}
					}
					index++;
				}
			}
		});
		if (main_weapon_index < 0)
		{
			_atk = 0;
			_def = 0;
			_hp = 0;
			_elem_type_atk = 6;
			_elem_type_def = 6;
		}
		else
		{
			_elem_type_atk = equip_atk_elem_type[main_weapon_index];
			_atk = status_atk + equip[main_weapon_index].atk + equip[main_weapon_index].GetElemAtk(_elem_type_atk);
			_elem_type_def = 6;
			_def = status_def;
			_hp = status_hp;
			int i = 0;
			for (int num2 = equip.Length; i < num2; i++)
			{
				if (equip[i] != null)
				{
					if (!equip_is_weapon[i])
					{
						int num3 = 0;
						int num4 = equip_atk_elem_type[i];
						switch (num4)
						{
						case -1:
							num3 = equip[i].elemAtk[0];
							break;
						default:
							num3 = equip[i].elemAtk[num4];
							break;
						case 6:
							break;
						}
						_atk += equip[i].atk + num3;
					}
					if (!equip_is_weapon[i] || i == main_weapon_index)
					{
						_def += equip[i].def;
						_hp += equip[i].hp;
					}
					if (equip_def_elem_type[i] != 6)
					{
						if (_elem_type_def == 6)
						{
							_elem_type_def = equip_def_elem_type[i];
						}
						else
						{
							_elem_type_def = -1;
						}
					}
				}
			}
			int num5 = skill.atk + skill.GetAllAtkElem();
			_atk += num5;
			_def += skill.def;
			_hp += skill.hp;
		}
	}

	public EquipItemAbilityCollection[] GetLocalEquipSetAbility(int set_no, EquipItemAbilityCollection.SwapData swap_data = null)
	{
		EquipSetInfo set_info = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet()[set_no];
		return GetEquipSetAbility(set_info, swap_data);
	}

	public EquipItemAbilityCollection[] GetEquipSetAbility(EquipSetInfo set_info, EquipItemAbilityCollection.SwapData swap_data = null)
	{
		List<EquipItemAbilityCollection> list = new List<EquipItemAbilityCollection>();
		int num = swap_data?.index ?? (-1);
		EquipSetInfo equipSetInfo = set_info.SwapArmorAndHelm();
		switch (num)
		{
		case 3:
			num = 4;
			break;
		case 4:
			num = 3;
			break;
		}
		int i = 0;
		for (int num2 = equipSetInfo.item.Length; i < num2; i++)
		{
			EquipItemInfo equipItemInfo = equipSetInfo.item[i];
			EquipItemAbilityCollection.COLLECTION_TYPE type = EquipItemAbilityCollection.COLLECTION_TYPE.NORMAL;
			if (num == i)
			{
				if (swap_data.item != null)
				{
					int j = 0;
					for (int num3 = swap_data.item.ability.Length; j < num3; j++)
					{
						EquipItemAbility swap_a = swap_data.item.ability[j];
						EquipItemAbilityCollection equipItemAbilityCollection = list.Find((EquipItemAbilityCollection data) => data.ability.id == swap_a.id);
						if (equipItemAbilityCollection == null)
						{
							list.Add(new EquipItemAbilityCollection(swap_a, i, EquipItemAbilityCollection.COLLECTION_TYPE.SWAP_IN));
						}
						else
						{
							equipItemAbilityCollection.Add(swap_a.ap, i, EquipItemAbilityCollection.COLLECTION_TYPE.SWAP_IN);
						}
					}
				}
				type = EquipItemAbilityCollection.COLLECTION_TYPE.SWAP_OUT;
			}
			if (equipItemInfo != null && equipItemInfo.ability != null && equipItemInfo.ability.Length != 0)
			{
				int k = 0;
				for (int num4 = equipItemInfo.ability.Length; k < num4; k++)
				{
					EquipItemAbility a = equipItemInfo.ability[k];
					if (a.id != 0 && !a.IsNeedUpdate() && a.IsActiveAbility())
					{
						if (num == i)
						{
							a = a.Inverse();
						}
						EquipItemAbilityCollection equipItemAbilityCollection2 = list.Find((EquipItemAbilityCollection data) => data.ability.id == a.id);
						if (equipItemAbilityCollection2 == null)
						{
							list.Add(new EquipItemAbilityCollection(a, i, type));
						}
						else
						{
							equipItemAbilityCollection2.Add(a.ap, i, type);
						}
					}
				}
			}
		}
		return list.ToArray();
	}

	public EquipSetInfo CreateEquipSetData(List<CharaInfo.EquipItem> list)
	{
		int num = 0;
		EquipItemInfo[] array = new EquipItemInfo[7];
		for (int i = 0; i < 7; i++)
		{
			EquipItemInfo equipItemInfo = null;
			if (i < list.Count)
			{
				int num2 = 0;
				equipItemInfo = new EquipItemInfo(list[i]);
				switch (equipItemInfo.tableData.type)
				{
				case EQUIPMENT_TYPE.ARMOR:
					num2 = 3;
					break;
				case EQUIPMENT_TYPE.HELM:
					num2 = 4;
					break;
				case EQUIPMENT_TYPE.ARM:
					num2 = 5;
					break;
				case EQUIPMENT_TYPE.LEG:
					num2 = 6;
					break;
				default:
					num2 = num++;
					break;
				}
				array[num2] = equipItemInfo;
			}
		}
		return new EquipSetInfo(array, "装備セット", 1, new AccessoryPlaceInfo());
	}

	public BoostStatus GetBoostStatus(USE_ITEM_EFFECT_TYPE type)
	{
		if (boostStatus == null)
		{
			return null;
		}
		DateTime now = TimeManager.GetNow();
		int i = 0;
		for (int count = boostStatus.Count; i < count; i++)
		{
			if (boostStatus[i].Type == type && boostStatus[i].value != 0)
			{
				if (boostStatus[i].endDate == null)
				{
					return boostStatus[i];
				}
				if (DateTime.Parse(boostStatus[i].endDate.date).CompareTo(now) > 0)
				{
					return boostStatus[i];
				}
			}
		}
		return null;
	}

	public int GetBoostStatusEndTimestamp(USE_ITEM_EFFECT_TYPE type)
	{
		return GetBoostStatus(type)?.endTimestamp ?? 0;
	}

	public bool IsEffectedItem(ItemInfo item)
	{
		int i = 0;
		for (int num = item.tableData.useEffectTypes.Length; i < num; i++)
		{
			USE_ITEM_EFFECT_TYPE type = item.tableData.useEffectTypes[i];
			BoostStatus boostStatus = GetBoostStatus(type);
			if (boostStatus != null)
			{
				return true;
			}
		}
		return false;
	}

	public void SetUserStatus()
	{
		if (firstSetUserStatus)
		{
			firstSetUserStatus = false;
			OnceStatusInfoModel.Param statusinfo = MonoBehaviourSingleton<OnceManager>.I.result.statusinfo;
			MonoBehaviourSingleton<UserInfoManager>.I.SetUserInfoAndUserStatus(statusinfo.user, statusinfo.userStatus, statusinfo.unlockStamps, statusinfo.selectedDegrees, statusinfo.unlockDegrees);
			equipSet = new EquipSetInfo[statusinfo.equipSets.Count];
			statusinfo.equipSets.ForEach(delegate(EquipSetSimple o)
			{
				equipSet[o.setNo] = new EquipSetInfo(o);
			});
			boostStatus = statusinfo.boost;
			MonoBehaviourSingleton<PresentManager>.I.SetPresentNum(statusinfo.userStatus.present);
			MonoBehaviourSingleton<FriendManager>.I.SetFollowNum(statusinfo.followNum);
			MonoBehaviourSingleton<FriendManager>.I.SetFollowerNum(statusinfo.followerNum);
			MonoBehaviourSingleton<GlobalSettingsManager>.I.SetHasVisuals(statusinfo.hasVisuals);
			int i = 0;
			for (int count = statusinfo.accessorySets.Count; i < count; i++)
			{
				AccessorySet accessorySet = statusinfo.accessorySets[i];
				if (accessorySet.attachPlace == "-1")
				{
					equipSet[accessorySet.setNo].acc.Clear();
				}
				else
				{
					equipSet[accessorySet.setNo].acc.Add(accessorySet.uniqId, accessorySet.attachPlace);
				}
			}
			int num = equipSet.Length;
			equipSetCalc = new EquipSetCalculator[num];
			for (int j = 0; j < num; j++)
			{
				equipSetCalc[j] = new EquipSetCalculator();
				equipSetCalc[j].SetEquipSet(equipSet[j], j);
			}
		}
	}

	public void ResetEquipSetInfo()
	{
		CreateLocalEquipSetData();
		int num = equipSet.Length;
		equipSetCalc = new EquipSetCalculator[num];
		for (int i = 0; i < num; i++)
		{
			equipSetCalc[i] = new EquipSetCalculator();
			equipSetCalc[i].SetEquipSet(equipSet[i], i);
		}
	}

	public void AccessoryOn(string _uuid, ACCESSORY_PART _part)
	{
		if (localEquipSet == null || localEquipSetNo == -1)
		{
			Debug.LogWarning((object)"AccessoryOn() : invalid");
		}
		else
		{
			localEquipSet[localEquipSetNo].acc.Clear();
			AccessoryPlaceInfo acc = localEquipSet[localEquipSetNo].acc;
			int num = (int)_part;
			acc.Add(_uuid, num.ToString());
		}
	}

	public void AccessoryOff(string _uuid, ACCESSORY_PART _part)
	{
		if (localEquipSet == null || localEquipSetNo == -1)
		{
			Debug.LogWarning((object)"AccessoryOff() : invalid");
		}
		else
		{
			localEquipSet[localEquipSetNo].acc.Clear();
		}
	}

	public void SendEquipSet(int equip_set_no, List<SendSetEquipData> change_data, Action<Error> call_back)
	{
		StatusEquipModel.RequestSendForm send_form = new StatusEquipModel.RequestSendForm();
		send_form.select = equip_set_no;
		change_data.ForEach(delegate(SendSetEquipData data)
		{
			send_form.nos.Add(data.set_no);
			send_form.wuids0.Add(data.item[0].ToString());
			send_form.wuids1.Add(data.item[1].ToString());
			send_form.wuids2.Add(data.item[2].ToString());
			send_form.auids.Add(data.item[3].ToString());
			send_form.huids.Add(data.item[4].ToString());
			send_form.ruids.Add(data.item[5].ToString());
			send_form.luids.Add(data.item[6].ToString());
			send_form.shows.Add(data.show_helm);
			send_form.accs.Add(data.accs);
		});
		Protocol.Send(StatusEquipModel.URL, send_form, delegate(StatusEquipModel ret)
		{
			if (ret.Error == Error.None)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_CHANGE);
			}
			call_back(ret.Error);
		}, string.Empty);
	}

	public void SendEquipSetNo(int set_no, Action<Error> call_back)
	{
		StatusEquipSetModel.RequestSendForm requestSendForm = new StatusEquipSetModel.RequestSendForm();
		requestSendForm.no = set_no;
		if (PartyManager.IsValidInParty())
		{
			requestSendForm.partyId = MonoBehaviourSingleton<PartyManager>.I.GetPartyId();
		}
		Protocol.Send(StatusEquipSetModel.URL, requestSendForm, delegate(StatusEquipSetModel ret)
		{
			if (ret.Error == Error.None)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_CHANGE);
			}
			call_back(ret.Error);
		}, string.Empty);
	}

	public void SendSetSkill(ulong equip_uniq_id, ulong skill_uniq_id, int slot_index, int setNo, Action<bool> call_back)
	{
		StatusEquipSkillModel.RequestSendForm requestSendForm = new StatusEquipSkillModel.RequestSendForm();
		requestSendForm.euid = equip_uniq_id.ToString();
		requestSendForm.suid = skill_uniq_id.ToString();
		requestSendForm.slot = slot_index;
		requestSendForm.no = setNo;
		EquipItemInfo equipItemInfo = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(equip_uniq_id);
		if (equipItemInfo == null)
		{
			call_back(false);
		}
		else
		{
			ulong now_equip_skill_uniq_id = 0uL;
			for (LinkedListNode<SkillItemInfo> linkedListNode = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				EquipSetSkillData equipSetSkillData = linkedListNode.Value.equipSetSkill.Find((EquipSetSkillData x) => x.equipSetNo == MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo());
				if (equipSetSkillData != null && equipSetSkillData.equipItemUniqId == equip_uniq_id && equipSetSkillData.equipSlotNo == slot_index)
				{
					now_equip_skill_uniq_id = linkedListNode.Value.uniqueID;
					break;
				}
			}
			Protocol.Send(StatusEquipSkillModel.URL, requestSendForm, delegate(StatusEquipSkillModel ret)
			{
				bool obj = false;
				if (ret.Error == Error.None)
				{
					obj = true;
					GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.SKILL_ATTACK, skill_uniq_id);
					SkillItemInfo skillItemInfo = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(now_equip_skill_uniq_id);
					if (skillItemInfo != null)
					{
						EquipSetSkillData item = skillItemInfo.equipSetSkill.Find((EquipSetSkillData x) => x.equipSetNo == MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo());
						skillItemInfo.equipSetSkill.Remove(item);
					}
					MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE);
				}
				call_back(obj);
			}, string.Empty);
		}
	}

	public void SendDetachSkill(ulong equip_uniq_id, int slot, int setNo, Action<bool> call_back)
	{
		StatusDetachSkillModel.RequestSendForm requestSendForm = new StatusDetachSkillModel.RequestSendForm();
		requestSendForm.euid = equip_uniq_id.ToString();
		requestSendForm.slot = slot;
		requestSendForm.no = setNo;
		Protocol.Send(StatusDetachSkillModel.URL, requestSendForm, delegate(StatusDetachSkillModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE);
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendDetachAllSkill(ulong equip_uniq_id, int _setNo, Action<bool> _callback)
	{
		StatusDetachAllSkillModel.RequestSendForm requestSendForm = new StatusDetachAllSkillModel.RequestSendForm();
		requestSendForm.euid = equip_uniq_id.ToString();
		requestSendForm.no = _setNo;
		Protocol.Send(StatusDetachAllSkillModel.URL, requestSendForm, delegate(StatusDetachAllSkillModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE);
			}
			if (_callback != null)
			{
				_callback(obj);
			}
		}, string.Empty);
	}

	public void SendInventoryEquipLock(ulong equip_uniq_id, Action<bool, EquipItemInfo> call_back)
	{
		InventoryEquipLockModel.RequestSendForm requestSendForm = new InventoryEquipLockModel.RequestSendForm();
		requestSendForm.euid = equip_uniq_id.ToString();
		Protocol.Send(InventoryEquipLockModel.URL, requestSendForm, delegate(InventoryEquipLockModel ret)
		{
			bool flag = false;
			EquipItemInfo equipItemInfo = null;
			if (ret.Error == Error.None)
			{
				flag = true;
				equipItemInfo = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(equip_uniq_id);
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE);
			}
			call_back.Invoke(flag, equipItemInfo);
		}, string.Empty);
	}

	public void SendInventorySkillLock(ulong skill_uniq_id, Action<bool, SkillItemInfo> call_back)
	{
		InventorySkillLockModel.RequestSendForm requestSendForm = new InventorySkillLockModel.RequestSendForm();
		requestSendForm.suid = skill_uniq_id.ToString();
		Protocol.Send(InventorySkillLockModel.URL, requestSendForm, delegate(InventorySkillLockModel ret)
		{
			bool flag = false;
			SkillItemInfo skillItemInfo = null;
			if (ret.Error == Error.None)
			{
				flag = true;
				skillItemInfo = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(skill_uniq_id);
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_FAVORITE);
			}
			call_back.Invoke(flag, skillItemInfo);
		}, string.Empty);
	}

	public void SendVisualEquip(ulong armor_id, ulong helm_id, ulong arm_id, ulong leg_id, bool is_visible_helm, Action<bool> call_back)
	{
		StatusVisualEquipModel.RequestSendForm requestSendForm = new StatusVisualEquipModel.RequestSendForm();
		requestSendForm.auid = armor_id.ToString();
		requestSendForm.huid = helm_id.ToString();
		requestSendForm.ruid = arm_id.ToString();
		requestSendForm.luid = leg_id.ToString();
		Protocol.Send(StatusVisualEquipModel.URL, requestSendForm, delegate(StatusVisualEquipModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void OnDiff(BaseModelDiff.DiffEquipSet diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.add))
		{
			diff.add.ForEach(delegate(EquipSetSimple o)
			{
				if (o.setNo <= equipSet.Length)
				{
					Array.Resize(ref equipSet, o.setNo + 1);
					equipSet[o.setNo] = new EquipSetInfo(o);
				}
			});
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			diff.update.ForEach(delegate(EquipSetSimple o)
			{
				if (o.setNo < equipSet.Length)
				{
					AccessoryPlaceInfo acc = equipSet[o.setNo].acc;
					equipSet[o.setNo] = new EquipSetInfo(o);
					equipSet[o.setNo].acc = acc;
				}
			});
			flag = true;
		}
		if (flag)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET);
		}
	}

	public void OnDiff(BaseModelDiff.DiffBoost diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.add))
		{
			boostStatus.AddRange(diff.add);
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			diff.update.ForEach(delegate(BoostStatus boost)
			{
				StatusManager statusManager = this;
				BoostStatus boostStatus = this.boostStatus.Find((BoostStatus b) => b.type == boost.type);
				boostStatus.value = boost.value;
				boostStatus.endDate = boost.endDate;
				boostStatus.endTimestamp = boost.endTimestamp;
			});
			flag = true;
		}
		if (flag)
		{
			if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIPlayerStatus>.I.SetUpBoostAnimator();
			}
			if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIEnduranceStatus>.I.SetUpBoostAnimator();
			}
		}
	}

	public void OnDiff(BaseModelDiff.DiffAccessorySet diff)
	{
		bool flag = false;
		bool flag2 = false;
		if (Utility.IsExist(diff.add))
		{
			int i = 0;
			for (int count = diff.add.Count; i < count; i++)
			{
				AccessorySet accessorySet = diff.add[i];
				if (accessorySet.setNo < equipSet.Length)
				{
					equipSet[accessorySet.setNo].acc.Clear();
					if (accessorySet.attachPlace != "-1")
					{
						equipSet[accessorySet.setNo].acc.Add(accessorySet.uniqId, accessorySet.attachPlace);
					}
				}
			}
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			int j = 0;
			for (int count2 = diff.update.Count; j < count2; j++)
			{
				AccessorySet accessorySet2 = diff.update[j];
				if (accessorySet2.setNo < equipSet.Length)
				{
					equipSet[accessorySet2.setNo].acc.Clear();
					if (accessorySet2.attachPlace != "-1")
					{
						equipSet[accessorySet2.setNo].acc.Add(accessorySet2.uniqId, accessorySet2.attachPlace);
					}
				}
			}
			flag = true;
		}
		if (flag)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET);
		}
	}

	public void SendEquipSetName(string name, int setNo, Action<bool> callback)
	{
		StatusEquipSetNameChangeModel.RequestSendForm requestSendForm = new StatusEquipSetNameChangeModel.RequestSendForm();
		requestSendForm.name = name;
		requestSendForm.setNo = setNo;
		Protocol.Send(StatusEquipSetNameChangeModel.URL, requestSendForm, delegate(BaseModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET);
			}
			callback(flag);
		}, string.Empty);
	}

	public EquipSetCalculator GetEquipSetCalculator(int setNo)
	{
		if (object.ReferenceEquals(equipSetCalc, null) || setNo >= equipSetCalc.Length)
		{
			return null;
		}
		return equipSetCalc[setNo];
	}

	public void ReplaceEquipItem(EquipSetInfo info, int setNo, int index)
	{
		if (!object.ReferenceEquals(equipSetCalc, null) && setNo < equipSetCalc.Length)
		{
			CharaInfo.EquipItem item = info.ConvertSelfEquipSetItem(index, setNo);
			equipSetCalc[setNo].SetEquipItem(item, index);
		}
	}

	public void ReplaceEquipSet(EquipSetInfo info, int setNo)
	{
		if (!object.ReferenceEquals(equipSetCalc, null) && setNo < equipSetCalc.Length)
		{
			equipSetCalc[setNo].SetEquipSet(info, setNo);
		}
	}

	public void ReplaceEquipSets(EquipSetInfo[] info)
	{
		int i = 0;
		for (int num = info.Length; i < num; i++)
		{
			equipSetCalc[i].SetEquipSet(info[i], i);
		}
		isEquipSetCalcUpdate = false;
	}

	public void SwapWeapon(int swapIndex, int nowIndex)
	{
		equipSetCalc[localEquipSetNo].SwapWeapon(swapIndex, nowIndex);
	}

	public EquipSetCalculator GetOtherEquipSetCalculator(int index)
	{
		if (index >= otherEquipSetCalc.Count)
		{
			int i = 0;
			for (int num = index + 1 - otherEquipSetCalc.Count; i < num; i++)
			{
				otherEquipSetCalc.Add(new EquipSetCalculator());
			}
		}
		return otherEquipSetCalc[index];
	}
}
