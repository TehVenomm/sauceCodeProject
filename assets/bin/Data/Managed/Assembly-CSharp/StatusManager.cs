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

	public const int OFFSET_FRIENDLIST_EQUIPSET = 4;

	public int otherEquipSetSaveIndex;

	public int selectUniqueEquipSetNo;

	private bool is_unique;

	private EquipSetInfo[] uniqueEquipSet;

	private EquipSetCalculator[] uniqueEquipSetCalc;

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
				int num2 = ++ENABLE_EQUIP_TYPE_MAX;
			}
		}
	}

	private bool IsWeapon(EQUIPMENT_TYPE type)
	{
		if (type >= EQUIPMENT_TYPE.ONE_HAND_SWORD)
		{
			return type <= EQUIPMENT_TYPE.ARROW;
		}
		return false;
	}

	private bool IsArmor(EQUIPMENT_TYPE type)
	{
		if (type >= EQUIPMENT_TYPE.ARMOR)
		{
			return type <= EQUIPMENT_TYPE.LEG;
		}
		return false;
	}

	public void _LoadTable()
	{
		StartCoroutine(_LoadTableData());
	}

	private IEnumerator _LoadTableData()
	{
		if (!initialized)
		{
			LoadingQueue loadingQueue = new LoadingQueue(this);
			LoadObject lo_equip_model_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "EquipModelTable");
			LoadObject lo_equip_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "EquipItemTable");
			LoadObject lo_equip_exceed_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "EquipItemExceedTable");
			LoadObject lo_skill_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "SkillItemTable");
			LoadObject lo_ability_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "AbilityTable");
			LoadObject lo_ability_data_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "AbilityDataTable");
			LoadObject lo_ability_item_lot_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "AbilityItemLotTable");
			LoadObject lo_assigned_equipment_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "AssignedEquipmentTable");
			if (loadingQueue.IsLoading())
			{
				yield return loadingQueue.Wait();
			}
			if (!Singleton<EquipModelTable>.IsValid())
			{
				Singleton<EquipModelTable>.Create();
			}
			Singleton<EquipModelTable>.I.CreateTable(((TextAsset)lo_equip_model_table.loadedObject).text);
			if (!Singleton<EquipItemTable>.IsValid())
			{
				Singleton<EquipItemTable>.Create();
			}
			Singleton<EquipItemTable>.I.CreateTable(((TextAsset)lo_equip_table.loadedObject).text);
			if (!Singleton<EquipItemExceedTable>.IsValid())
			{
				Singleton<EquipItemExceedTable>.Create();
			}
			Singleton<EquipItemExceedTable>.I.CreateTable(((TextAsset)lo_equip_exceed_table.loadedObject).text);
			if (!Singleton<SkillItemTable>.IsValid())
			{
				Singleton<SkillItemTable>.Create();
			}
			Singleton<SkillItemTable>.I.CreateTable(((TextAsset)lo_skill_table.loadedObject).text);
			if (!Singleton<AbilityTable>.IsValid())
			{
				Singleton<AbilityTable>.Create();
			}
			Singleton<AbilityTable>.I.CreateTable(((TextAsset)lo_ability_table.loadedObject).text);
			if (!Singleton<AbilityDataTable>.IsValid())
			{
				Singleton<AbilityDataTable>.Create();
			}
			Singleton<AbilityDataTable>.I.CreateTable(((TextAsset)lo_ability_data_table.loadedObject).text);
			if (!Singleton<AbilityItemLotTable>.IsValid())
			{
				Singleton<AbilityItemLotTable>.Create();
			}
			Singleton<AbilityItemLotTable>.I.CreateTable(((TextAsset)lo_ability_item_lot_table.loadedObject).text);
			if (!Singleton<AssignedEquipmentTable>.IsValid())
			{
				Singleton<AssignedEquipmentTable>.Create();
			}
			Singleton<AssignedEquipmentTable>.I.CreateTable(((TextAsset)lo_assigned_equipment_table.loadedObject).text);
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
		is_unique = false;
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
			if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
			{
				return 0;
			}
			return MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
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
		if (item == null)
		{
			return;
		}
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
		if (localVisual == null)
		{
			return;
		}
		int k = 0;
		for (int num3 = localVisual.visualItem.Length; k < num3; k++)
		{
			if (localVisual.visualItem[k] != null && localVisual.visualItem[k].uniqueID == item.uniqueID)
			{
				localVisual.visualItem[k] = item;
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
				array[j] = ((equipSetInfo.item[j] != null) ? equipSetInfo.item[j].uniqueID : 0);
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
						callback(obj: true);
					}
				}
				else if (callback != null)
				{
					callback(obj: false);
				}
			});
			MonoBehaviourSingleton<UserInfoManager>.I.userStatus.showHelm = localEquipSet[localEquipSetNo].showHelm;
			return;
		}
		if (set_no != MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo)
		{
			MonoBehaviourSingleton<StatusManager>.I.SendEquipSetNo(set_no, delegate(Error err)
			{
				if (err == Error.None)
				{
					if (callback != null)
					{
						callback(obj: true);
					}
				}
				else if (callback != null)
				{
					callback(obj: false);
				}
			});
			MonoBehaviourSingleton<UserInfoManager>.I.userStatus.showHelm = localEquipSet[localEquipSetNo].showHelm;
			return;
		}
		MonoBehaviourSingleton<PartyManager>.I.SendIsEquip(isEquip: false, delegate
		{
		});
		if (callback != null)
		{
			callback(obj: true);
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
			if (((localVisual.visualItem[i] != null) ? localVisual.visualItem[i].uniqueID : 0) != array[i])
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
			callback(obj: true);
		}
	}

	public void CheckChangeEquip(int equip_set_no, Action<bool> callback)
	{
		StartCoroutine(_CheckChangeEquipCoroutine(equip_set_no, callback));
	}

	private IEnumerator _CheckChangeEquipCoroutine(int equip_set_no, Action<bool> callback)
	{
		bool recv_break = false;
		bool wait_visual_equip = true;
		MonoBehaviourSingleton<StatusManager>.I.CheckChangeVisualEquip(delegate(bool is_success)
		{
			if (!is_success)
			{
				if (callback != null)
				{
					callback(obj: false);
				}
				recv_break = true;
			}
			else
			{
				wait_visual_equip = false;
			}
		});
		bool wait_equip = true;
		MonoBehaviourSingleton<StatusManager>.I.CheckChangeEquipSet(equip_set_no, delegate(bool is_success)
		{
			if (!is_success)
			{
				if (callback != null)
				{
					callback(obj: false);
				}
				recv_break = true;
			}
			wait_equip = false;
		});
		while (wait_equip | wait_visual_equip)
		{
			if (recv_break)
			{
				if (callback != null)
				{
					callback(obj: false);
				}
				yield break;
			}
			yield return null;
		}
		if (callback != null)
		{
			callback(obj: true);
		}
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

	public EquipSetInfo[] GetEquipSets()
	{
		EquipSetInfo[] array = new EquipSetInfo[equipSet.Length];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			EquipSetInfo equipSetInfo = GetEquipSet(i);
			array[i] = new EquipSetInfo(new EquipItemInfo[7]
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
		return array;
	}

	public bool IsEquipping(EquipItemInfo item, int set_no = -1)
	{
		if (item == null || item.uniqueID == 0L)
		{
			return false;
		}
		if (set_no != -1)
		{
			return IsEquipping(set_no, item);
		}
		int i = 0;
		for (int num = equipSet.Length; i < num; i++)
		{
			if (IsEquipping(i, item))
			{
				return true;
			}
		}
		return false;
	}

	public void UpdateEquip(EquipItemInfo equip)
	{
		int i = 0;
		for (int num = equipSet.Length; i < num; i++)
		{
			IsEquipping(i, equip, delegate(int set_no, int index)
			{
				EquipSetInfo equipSetInfo = MonoBehaviourSingleton<StatusManager>.I.GetEquipSet(set_no);
				equipSetInfo.item[index] = equip;
				ReplaceEquipItem(equipSetInfo, set_no, index);
			});
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
			ulong num2 = (equipSetInfo.item[i] != null) ? equipSetInfo.item[i].uniqueID : 0;
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
			createPlayerInfo.charaInfo.name = (PlayerPrefs.HasKey("Tut_Name") ? PlayerPrefs.GetString("Tut_Name") : "???");
			createPlayerInfo.charaInfo.comment = "";
			createPlayerInfo.charaInfo.hp = 200;
			createPlayerInfo.charaInfo.atk = 100;
			createPlayerInfo.charaInfo.def = 100;
			createPlayerInfo.charaInfo.level = 1;
			createPlayerInfo.charaInfo.aId = PlayerPrefs.GetInt("Tut_Armor");
			createPlayerInfo.charaInfo.hId = PlayerPrefs.GetInt("Tut_Head");
			createPlayerInfo.charaInfo.rId = PlayerPrefs.GetInt("Tut_Arm");
			createPlayerInfo.charaInfo.lId = PlayerPrefs.GetInt("Tut_Leg");
			createPlayerInfo.charaInfo.sex = PlayerPrefs.GetInt("Tut_Sex");
			createPlayerInfo.charaInfo.showHelm = 1;
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
			if (MonoBehaviourSingleton<PartyManager>.IsValid())
			{
				PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByUserId(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
				if (slotInfoByUserId != null && slotInfoByUserId.userInfo != null)
				{
					createPlayerInfo.charaInfo.userClanData = slotInfoByUserId.userInfo.userClanData;
				}
			}
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
			EquipItemInfo equippingItemInfo = GetEquippingItemInfo(i);
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
				equipItem.eId = PlayerPrefs.GetInt("Tut_Weapon");
				equipItem.lv = 1;
				equipItem.sIds.Add(105200200);
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
		AccessoryPlaceInfo equippingAccessoryInfo = GetEquippingAccessoryInfo();
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
			if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
			{
				return 1;
			}
			return MonoBehaviourSingleton<UserInfoManager>.I.userStatus.showHelm;
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
		return GetEquippingItemInfo(equip_slot, set_no)?.tableID ?? 0;
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
		EquipSetInfo[] array = is_local ? localEquipSet : equipSet;
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
		if (IsArmor(type))
		{
			return _weaponEndIndex + 1 - _weaponStartIndex;
		}
		if (num > _weaponEndIndex)
		{
			return -1;
		}
		return num - _weaponStartIndex;
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
				continue;
			}
			array[i] = new CharaInfo.EquipItem();
			array[i].eId = (int)set_info.item[i].tableID;
			array[i].lv = set_info.item[i].level;
			array[i].exceed = set_info.item[i].exceed;
			int j = 0;
			for (int maxSlot = set_info.item[i].GetMaxSlot(); j < maxSlot; j++)
			{
				SkillItemInfo uniqueOrHomeEquipSkill = MonoBehaviourSingleton<StatusManager>.I.GetUniqueOrHomeEquipSkill(set_info.item[i], j, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo);
				if (uniqueOrHomeEquipSkill != null)
				{
					array[i].sIds.Add((int)uniqueOrHomeEquipSkill.tableID);
					array[i].sLvs.Add(uniqueOrHomeEquipSkill.level);
					array[i].sExs.Add(uniqueOrHomeEquipSkill.exceedCnt);
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
				int num6 = ++index;
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
							for (int num7 = growParamElemAtk.Length; j < num7; j++)
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
						for (int num8 = atkElement.Length; k < num8; k++)
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
						for (int num9 = equip[index].elemAtk.Length; l < num9; l++)
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
								GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillItemData.growID, array2[m], data.GetSkillExceed(m));
								if (growSkillItemData != null)
								{
									skill.atk += growSkillItemData.GetGrowParamAtk(skillItemData.baseAtk);
									int[] growParamElemAtk2 = growSkillItemData.GetGrowParamElemAtk(skillItemData.atkElement);
									int n = 0;
									for (int num10 = growParamElemAtk2.Length; n < num10; n++)
									{
										skill.elemAtk[n] += growParamElemAtk2[n];
									}
									skill.def += growSkillItemData.GetGrowParamDef(skillItemData.baseDef);
									skill.hp += growSkillItemData.GetGrowParamHp(skillItemData.baseHp);
									int[] growParamElemDef2 = growSkillItemData.GetGrowParamElemDef(skillItemData.defElement);
									int num11 = 0;
									for (int num12 = growParamElemDef2.Length; num11 < num12; num11++)
									{
										skill.elemDef[num11] += growParamElemDef2[num11];
									}
								}
								else
								{
									skill.atk += skillItemData.baseAtk;
									int[] atkElement2 = skillItemData.atkElement;
									int num13 = 0;
									for (int num14 = atkElement2.Length; num13 < num14; num13++)
									{
										skill.elemAtk[num13] += atkElement2[num13];
									}
									skill.def += skillItemData.baseDef;
									skill.hp += skillItemData.baseHp;
									int[] defElement2 = skillItemData.defElement;
									int num15 = 0;
									for (int num16 = defElement2.Length; num15 < num16; num15++)
									{
										skill.elemDef[num15] += defElement2[num15];
									}
								}
								if (mainWeaponItemData != null && skillItemData.IsMatchSupportEquipType(mainWeaponItemData.type))
								{
									for (int num17 = 0; num17 < skillItemData.supportValue.Length; num17++)
									{
										if (growSkillItemData != null)
										{
											skill.atk += growSkillItemData.GetGrowParamSupprtValue(skillItemData.supportValue, num17);
										}
										else
										{
											skill.atk += skillItemData.supportValue[num17];
										}
									}
								}
							}
						}
					}
					int num6 = ++index;
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
			return;
		}
		_elem_type_atk = equip_atk_elem_type[main_weapon_index];
		_atk = status_atk + equip[main_weapon_index].atk + equip[main_weapon_index].GetElemAtk(_elem_type_atk);
		_elem_type_def = 6;
		_def = status_def;
		_hp = status_hp;
		int i = 0;
		for (int num2 = equip.Length; i < num2; i++)
		{
			if (equip[i] == null)
			{
				continue;
			}
			if (!equip_is_weapon[i])
			{
				int num3 = 0;
				int num4 = equip_atk_elem_type[i];
				if (num4 != 6)
				{
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
		int num5 = skill.atk + skill.GetAllAtkElem();
		_atk += num5;
		_def += skill.def;
		_hp += skill.hp;
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
			if (equipItemInfo == null || equipItemInfo.ability == null || equipItemInfo.ability.Length == 0)
			{
				continue;
			}
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
			if (GetBoostStatus(type) != null)
			{
				return true;
			}
		}
		return false;
	}

	public void SetUserStatus()
	{
		if (!firstSetUserStatus)
		{
			return;
		}
		firstSetUserStatus = false;
		OnceStatusInfoModel.Param statusinfo = MonoBehaviourSingleton<OnceManager>.I.result.statusinfo;
		MonoBehaviourSingleton<UserInfoManager>.I.SetUserInfoAndUserStatus(statusinfo.user, statusinfo.userStatus, statusinfo.userClan, statusinfo.unlockStamps, statusinfo.selectedDegrees, statusinfo.unlockDegrees);
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
		SetUserUniqueEquipStatus();
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
			Debug.LogWarning("AccessoryOn() : invalid");
			return;
		}
		localEquipSet[localEquipSetNo].acc.Clear();
		AccessoryPlaceInfo acc = localEquipSet[localEquipSetNo].acc;
		int num = (int)_part;
		acc.Add(_uuid, num.ToString());
	}

	public void AccessoryOff(string _uuid, ACCESSORY_PART _part)
	{
		if (localEquipSet == null || localEquipSetNo == -1)
		{
			Debug.LogWarning("AccessoryOff() : invalid");
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
		});
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
		});
	}

	public void SendMainSetSkill(ulong equip_uniq_id, ulong skill_uniq_id, int slot_index, int setNo, Action<bool> call_back)
	{
		StatusEquipSkillModel.RequestSendForm requestSendForm = new StatusEquipSkillModel.RequestSendForm();
		requestSendForm.euid = equip_uniq_id.ToString();
		requestSendForm.suid = skill_uniq_id.ToString();
		requestSendForm.slot = slot_index;
		requestSendForm.no = setNo;
		if (MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(equip_uniq_id) == null)
		{
			call_back(obj: false);
			return;
		}
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
		});
	}

	public void SendDetachMainSkill(ulong equip_uniq_id, int slot, int setNo, Action<bool> call_back)
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
		});
	}

	public void SendDetachAllMainSkill(ulong equip_uniq_id, int _setNo, Action<bool> _callback)
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
		});
	}

	public void SendDetachMainAllSkillFromEvery(int _setNo, Action<bool> _callback)
	{
		StatusDetachAllSkillFromEveryModel.RequestSendForm requestSendForm = new StatusDetachAllSkillFromEveryModel.RequestSendForm();
		requestSendForm.no = _setNo;
		Protocol.Send(StatusDetachAllSkillFromEveryModel.URL, requestSendForm, delegate(StatusDetachAllSkillFromEveryModel ret)
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
		});
	}

	public void SendInventoryEquipLock(ulong equip_uniq_id, Action<bool, EquipItemInfo> call_back)
	{
		InventoryEquipLockModel.RequestSendForm requestSendForm = new InventoryEquipLockModel.RequestSendForm();
		requestSendForm.euid = equip_uniq_id.ToString();
		Protocol.Send(InventoryEquipLockModel.URL, requestSendForm, delegate(InventoryEquipLockModel ret)
		{
			bool arg = false;
			EquipItemInfo arg2 = null;
			if (ret.Error == Error.None)
			{
				arg = true;
				arg2 = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(equip_uniq_id);
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE);
			}
			call_back(arg, arg2);
		});
	}

	public void SendInventorySkillLock(ulong skill_uniq_id, Action<bool, SkillItemInfo> call_back)
	{
		InventorySkillLockModel.RequestSendForm requestSendForm = new InventorySkillLockModel.RequestSendForm();
		requestSendForm.suid = skill_uniq_id.ToString();
		Protocol.Send(InventorySkillLockModel.URL, requestSendForm, delegate(InventorySkillLockModel ret)
		{
			bool arg = false;
			SkillItemInfo arg2 = null;
			if (ret.Error == Error.None)
			{
				arg = true;
				arg2 = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(skill_uniq_id);
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_FAVORITE);
			}
			call_back(arg, arg2);
		});
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
		});
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
		});
	}

	public EquipSetCalculator GetEquipSetCalculator(int setNo)
	{
		if (equipSetCalc == null || setNo >= equipSetCalc.Length)
		{
			return null;
		}
		return equipSetCalc[setNo];
	}

	public void ReplaceEquipItem(EquipSetInfo info, int setNo, int index)
	{
		if (equipSetCalc != null && setNo < equipSetCalc.Length)
		{
			CharaInfo.EquipItem item = info.ConvertSelfEquipSetItem(index, setNo);
			equipSetCalc[setNo].SetEquipItem(item, index);
		}
	}

	public void ReplaceEquipSet(EquipSetInfo info, int setNo)
	{
		if (equipSetCalc != null && setNo < equipSetCalc.Length)
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

	public static bool IsUnique()
	{
		if (MonoBehaviourSingleton<StatusManager>.IsValid())
		{
			return MonoBehaviourSingleton<StatusManager>.I.is_unique;
		}
		return false;
	}

	public void InitUniqueEquip()
	{
		is_unique = false;
	}

	public void SetSelectUniqueEquipSetNo(int setNo)
	{
		selectUniqueEquipSetNo = setNo;
		if (selectUniqueEquipSetNo == -1)
		{
			selectUniqueEquipSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ueSetNo;
		}
	}

	public void SetUserUniqueEquipStatus()
	{
		OnceStatusInfoModel.Param statusinfo = MonoBehaviourSingleton<OnceManager>.I.result.statusinfo;
		uniqueEquipSet = new EquipSetInfo[statusinfo.uniqueEquipSets.Count];
		statusinfo.uniqueEquipSets.ForEach(delegate(EquipSetSimple o)
		{
			uniqueEquipSet[o.setNo] = new EquipSetInfo(o);
		});
		int i = 0;
		for (int count = statusinfo.uniqueAccessorySets.Count; i < count; i++)
		{
			AccessorySet accessorySet = statusinfo.uniqueAccessorySets[i];
			if (accessorySet.attachPlace == "-1")
			{
				uniqueEquipSet[accessorySet.setNo].acc.Clear();
			}
			else
			{
				uniqueEquipSet[accessorySet.setNo].acc.Add(accessorySet.uniqId, accessorySet.attachPlace);
			}
		}
		int num = uniqueEquipSet.Length;
		uniqueEquipSetCalc = new EquipSetCalculator[num];
		for (int j = 0; j < num; j++)
		{
			uniqueEquipSetCalc[j] = new EquipSetCalculator();
			uniqueEquipSetCalc[j].SetEquipSet(uniqueEquipSet[j], j, isUnique: true);
		}
	}

	public void CreateLocalUniqueEquipSetData()
	{
		localEquipSet = new EquipSetInfo[uniqueEquipSet.Length];
		localEquipSetNo = (MonoBehaviourSingleton<UserInfoManager>.IsValid() ? MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ueSetNo : 0);
		is_unique = true;
		int i = 0;
		for (int num = localEquipSet.Length; i < num; i++)
		{
			EquipSetInfo equipSetInfo = GetUniqueEquipSet(i);
			localEquipSet[i] = new EquipSetInfo(new EquipItemInfo[7]
			{
				equipSetInfo.item[0],
				equipSetInfo.item[1],
				equipSetInfo.item[2],
				equipSetInfo.item[3],
				equipSetInfo.item[4],
				equipSetInfo.item[5],
				equipSetInfo.item[6]
			}, equipSetInfo.name, equipSetInfo.showHelm, equipSetInfo.order, equipSetInfo.acc);
		}
	}

	public bool checkEquipMagi(int set_no)
	{
		EquipSetInfo equipSetInfo = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet()[set_no];
		for (int i = 0; i < equipSetInfo.item.Length; i++)
		{
			if (equipSetInfo.item[i] == null)
			{
				continue;
			}
			for (int j = 0; j < equipSetInfo.item[i].GetMaxSlot(); j++)
			{
				if (equipSetInfo.item[i].GetUniqueSkillItem(j) != null)
				{
					return true;
				}
			}
		}
		return false;
	}

	public EquipSetInfo GetUniqueEquipSet(int set_no)
	{
		if (set_no >= uniqueEquipSet.Length)
		{
			return null;
		}
		return uniqueEquipSet[set_no];
	}

	public EquipSetInfo GetOrderUniqueEquipSet(int order)
	{
		for (int i = 0; i < uniqueEquipSet.Length; i++)
		{
			if (uniqueEquipSet[i].order == order)
			{
				return uniqueEquipSet[i];
			}
		}
		return null;
	}

	public int GetOrderUniqueEquipSetNo(int order)
	{
		for (int i = 0; i < uniqueEquipSet.Length; i++)
		{
			if (uniqueEquipSet[i].order == order)
			{
				return i;
			}
		}
		return -1;
	}

	public EquipSetCalculator GetLocalEquipSetCalculator(int setNo)
	{
		if (is_unique)
		{
			return GetUniqueEquipSetCalculator(setNo);
		}
		return GetEquipSetCalculator(setNo);
	}

	public SkillItemInfo GetUniqueOrHomeEquipSkill(EquipItemInfo equip, int slotNo, int setNo)
	{
		if (is_unique)
		{
			return equip.GetUniqueSkillItem(slotNo);
		}
		return equip.GetSkillItem(slotNo, setNo);
	}

	public void SendUniqueEquipSet(List<SendSetEquipData> change_data, Action<Error> call_back)
	{
		UniqueStatusEquipModel.RequestSendForm send_form = new UniqueStatusEquipModel.RequestSendForm();
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
		Protocol.Send(UniqueStatusEquipModel.URL, send_form, delegate(UniqueStatusEquipModel ret)
		{
			if (ret.Error == Error.None)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_CHANGE);
			}
			call_back(ret.Error);
		});
	}

	public void OnDiff(BaseModelDiff.DiffUniqueEquipSet diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.update))
		{
			diff.update.ForEach(delegate(EquipSetSimple o)
			{
				if (o.setNo < uniqueEquipSet.Length)
				{
					AccessoryPlaceInfo acc = uniqueEquipSet[o.setNo].acc;
					uniqueEquipSet[o.setNo] = new EquipSetInfo(o);
					uniqueEquipSet[o.setNo].acc = acc;
					uniqueEquipSet[o.setNo].order = o.order;
				}
			});
			flag = true;
		}
		if (flag)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET);
		}
	}

	public void SendSetSkill(ulong equip_uniq_id, ulong skill_uniq_id, int slot_index, int setNo, Action<bool> call_back)
	{
		if (IsUnique())
		{
			SendUniqueSetSkill(equip_uniq_id, skill_uniq_id, slot_index, delegate(bool is_success)
			{
				call_back(is_success);
			});
		}
		else
		{
			SendMainSetSkill(equip_uniq_id, skill_uniq_id, slot_index, setNo, delegate(bool is_success)
			{
				call_back(is_success);
			});
		}
	}

	public void SendDetachSkill(ulong equip_uniq_id, int slot, int setNo, Action<bool> call_back)
	{
		if (IsUnique())
		{
			SendUniqueDetachSkill(equip_uniq_id, slot, delegate(bool is_success)
			{
				call_back(is_success);
			});
		}
		else
		{
			SendDetachMainSkill(equip_uniq_id, slot, setNo, delegate(bool is_success)
			{
				call_back(is_success);
			});
		}
	}

	public void SendUniqueSetSkillMultiple(List<ulong> equip_uniq_ids, List<ulong> skill_uniq_ids, List<int> slots_index, Action<bool> call_back)
	{
		UniqueEquipSkillMultiple.RequestSendForm requestSendForm = new UniqueEquipSkillMultiple.RequestSendForm();
		requestSendForm.euids = equip_uniq_ids.ConvertAll((ulong x) => x.ToString());
		requestSendForm.suids = skill_uniq_ids.ConvertAll((ulong x) => x.ToString());
		requestSendForm.slots = slots_index.ConvertAll((int x) => x.ToString());
		List<ulong> remove_equip_skill = new List<ulong>();
		LinkedListNode<SkillItemInfo> linkedListNode = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.GetFirstNode();
		for (int i = 0; i < equip_uniq_ids.Count; i++)
		{
			ulong num = equip_uniq_ids[i];
			int num2 = slots_index[i];
			ulong num3 = skill_uniq_ids[i];
			while (linkedListNode != null)
			{
				EquipSetSkillData uniqueEquipSetSkill = linkedListNode.Value.uniqueEquipSetSkill;
				if (uniqueEquipSetSkill != null && uniqueEquipSetSkill.equipItemUniqId == num && uniqueEquipSetSkill.equipSlotNo == num2 && linkedListNode.Value.uniqueID != num3)
				{
					remove_equip_skill.Add(linkedListNode.Value.uniqueID);
					break;
				}
				linkedListNode = linkedListNode.Next;
			}
		}
		Protocol.Send(UniqueEquipSkillMultiple.URL, requestSendForm, delegate(UniqueEquipSkillMultiple ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				for (int j = 0; j < skill_uniq_ids.Count; j++)
				{
					GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.SKILL_ATTACK, skill_uniq_ids[j]);
				}
				for (int k = 0; k < remove_equip_skill.Count; k++)
				{
					SkillItemInfo skillItemInfo = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(remove_equip_skill[k]);
					if (skillItemInfo != null)
					{
						skillItemInfo.uniqueEquipSetSkill.equipItemUniqId = 0uL;
						skillItemInfo.uniqueEquipSetSkill.equipSlotNo = 0;
					}
				}
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE);
			}
			call_back(obj);
		});
	}

	public void SendUniqueSetSkill(ulong equip_uniq_id, ulong skill_uniq_id, int slot_index, Action<bool> call_back)
	{
		UniqueStatusEquipSkillModel.RequestSendForm requestSendForm = new UniqueStatusEquipSkillModel.RequestSendForm();
		requestSendForm.euid = equip_uniq_id.ToString();
		requestSendForm.suid = skill_uniq_id.ToString();
		requestSendForm.slot = slot_index;
		if (MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(equip_uniq_id) == null)
		{
			call_back(obj: false);
			return;
		}
		ulong now_equip_skill_uniq_id = 0uL;
		for (LinkedListNode<SkillItemInfo> linkedListNode = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			EquipSetSkillData uniqueEquipSetSkill = linkedListNode.Value.uniqueEquipSetSkill;
			if (uniqueEquipSetSkill != null && uniqueEquipSetSkill.equipItemUniqId == equip_uniq_id && uniqueEquipSetSkill.equipSlotNo == slot_index)
			{
				now_equip_skill_uniq_id = linkedListNode.Value.uniqueID;
				break;
			}
		}
		Protocol.Send(UniqueStatusEquipSkillModel.URL, requestSendForm, delegate(UniqueStatusEquipSkillModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.SKILL_ATTACK, skill_uniq_id);
				SkillItemInfo skillItemInfo = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(now_equip_skill_uniq_id);
				if (skillItemInfo != null)
				{
					skillItemInfo.uniqueEquipSetSkill.equipItemUniqId = 0uL;
					skillItemInfo.uniqueEquipSetSkill.equipSlotNo = 0;
				}
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE);
			}
			call_back(obj);
		});
	}

	public void SendUniqueDetachSkill(ulong equip_uniq_id, int slot, Action<bool> call_back)
	{
		UniqueStatusDetachSkillModel.RequestSendForm requestSendForm = new UniqueStatusDetachSkillModel.RequestSendForm();
		requestSendForm.euid = equip_uniq_id.ToString();
		requestSendForm.slot = slot;
		Protocol.Send(UniqueStatusDetachSkillModel.URL, requestSendForm, delegate(UniqueStatusDetachSkillModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SKILL_CHANGE);
			}
			call_back(obj);
		});
	}

	public int GetCurrentUniqueEquipSetNo()
	{
		if (GetLocalEquipSetNo() == -1)
		{
			if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
			{
				return 0;
			}
			return MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ueSetNo;
		}
		return GetLocalEquipSetNo();
	}

	public void SendDetachAllSkill(ulong equip_uniq_id, int _setNo, Action<bool> _callback)
	{
		if (IsUnique())
		{
			SendDetachUniqueAllSkill(equip_uniq_id, _setNo, delegate(bool isSucces)
			{
				_callback(isSucces);
			});
		}
		else
		{
			SendDetachAllMainSkill(equip_uniq_id, _setNo, delegate(bool isSucces)
			{
				_callback(isSucces);
			});
		}
	}

	public void SendDetachUniqueAllSkill(ulong equip_uniq_id, int _setNo, Action<bool> _callback)
	{
		UniqueStatusDetachAllSkillModel.RequestSendForm requestSendForm = new UniqueStatusDetachAllSkillModel.RequestSendForm();
		requestSendForm.euid = equip_uniq_id.ToString();
		Protocol.Send(UniqueStatusDetachAllSkillModel.URL, requestSendForm, delegate(UniqueStatusDetachAllSkillModel ret)
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
		});
	}

	public void SendDetachAllSkillFromEvery(int _setNo, Action<bool> _callback)
	{
		if (IsUnique())
		{
			SendDetachUniqueAllSkillFromEvery(_setNo, delegate(bool is_success)
			{
				_callback(is_success);
			});
		}
		else
		{
			SendDetachMainAllSkillFromEvery(_setNo, delegate(bool is_success)
			{
				_callback(is_success);
			});
		}
	}

	public void SendDetachUniqueAllSkillFromEvery(int _setNo, Action<bool> _callback)
	{
		UniqueStatusDetachSkillAllEquipSet.RequestSendForm requestSendForm = new UniqueStatusDetachSkillAllEquipSet.RequestSendForm();
		requestSendForm.setNo = _setNo;
		Protocol.Send(UniqueStatusDetachSkillAllEquipSet.URL, requestSendForm, delegate(UniqueStatusDetachSkillAllEquipSet ret)
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
		});
	}

	public void SendUniqueEquipSetName(string name, int setNo, Action<bool> callback)
	{
		UniqueEquipSetNameChangeModel.RequestSendForm requestSendForm = new UniqueEquipSetNameChangeModel.RequestSendForm();
		requestSendForm.name = name;
		requestSendForm.setNo = setNo;
		Protocol.Send(UniqueEquipSetNameChangeModel.URL, requestSendForm, delegate(UniqueEquipSetNameChangeModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET);
			}
			callback(flag);
		});
	}

	public bool IsUniqueEquipping(EquipItemInfo item, int set_no = -1)
	{
		if (item == null || item.uniqueID == 0L)
		{
			return false;
		}
		if (set_no != -1)
		{
			return IsUniqueEquipping(set_no, item);
		}
		int i = 0;
		for (int num = uniqueEquipSet.Length; i < num; i++)
		{
			if (IsUniqueEquipping(i, item))
			{
				return true;
			}
		}
		return false;
	}

	private bool IsUniqueEquipping(int set_no, EquipItemInfo item, Action<int, int> callback = null)
	{
		if (item == null || item.uniqueID == 0L)
		{
			return false;
		}
		EquipSetInfo equipSetInfo = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquipSet(set_no);
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

	public bool IsUniqueLocalEquipping(EquipItemInfo item, int set_no = -1)
	{
		if (item == null || item.uniqueID == 0L)
		{
			return false;
		}
		int i = 0;
		for (int num = localEquipSet.Length; i < num; i++)
		{
			if (set_no != i && IsUniqueLocalEquipping(i, item))
			{
				return true;
			}
		}
		return false;
	}

	private bool IsUniqueLocalEquipping(int set_no, EquipItemInfo item, Action<int, int> callback = null)
	{
		if (item == null || item.uniqueID == 0L)
		{
			return false;
		}
		EquipSetInfo equipSetInfo = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet()[set_no];
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

	public void CheckChangeUniqueEquip(Action<bool> callback)
	{
		StartCoroutine(_CheckChangeUniqueEquipCoroutine(callback));
	}

	private IEnumerator _CheckChangeUniqueEquipCoroutine(Action<bool> callback)
	{
		bool recv_break = false;
		bool wait_equip = true;
		MonoBehaviourSingleton<StatusManager>.I.CheckChangeUniqueEquipSet(delegate(bool is_success)
		{
			if (!is_success)
			{
				if (callback != null)
				{
					callback(obj: false);
				}
				recv_break = true;
			}
			wait_equip = false;
		});
		while (wait_equip)
		{
			if (recv_break)
			{
				if (callback != null)
				{
					callback(obj: false);
				}
				yield break;
			}
			yield return null;
		}
		if (callback != null)
		{
			callback(obj: true);
		}
	}

	public void CheckChangeUniqueEquipSet(Action<bool> callback)
	{
		List<SendSetEquipData> list = new List<SendSetEquipData>();
		int i = 0;
		for (int num = localEquipSet.Length; i < num; i++)
		{
			EquipSetInfo equipSetInfo = localEquipSet[i];
			ulong[] array = new ulong[7];
			for (int j = 0; j < 7; j++)
			{
				array[j] = ((equipSetInfo.item[j] != null) ? equipSetInfo.item[j].uniqueID : 0);
			}
			if (MonoBehaviourSingleton<StatusManager>.I.IsChangeUniqueEquipSetInfo(i, array, equipSetInfo.showHelm, equipSetInfo.acc))
			{
				list.Add(new SendSetEquipData(i, array, equipSetInfo.showHelm, equipSetInfo.acc));
			}
		}
		if (list.Count > 0 && !PartyManager.IsValidInParty())
		{
			MonoBehaviourSingleton<StatusManager>.I.SendUniqueEquipSet(list, delegate(Error err)
			{
				if (err == Error.None)
				{
					if (callback != null)
					{
						callback(obj: true);
					}
				}
				else if (callback != null)
				{
					callback(obj: false);
				}
			});
		}
		else if (callback != null)
		{
			callback(obj: true);
		}
	}

	public bool IsChangeUniqueEquipSetInfo(int set_no, ulong[] change_equip_items, int show_helm, AccessoryPlaceInfo acc)
	{
		if (set_no >= uniqueEquipSet.Length)
		{
			return false;
		}
		EquipSetInfo equipSetInfo = uniqueEquipSet[set_no];
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
			ulong num2 = (equipSetInfo.item[i] != null) ? equipSetInfo.item[i].uniqueID : 0;
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

	public int UniqueEquipSetNum()
	{
		if (uniqueEquipSet == null)
		{
			return 0;
		}
		return uniqueEquipSet.Length;
	}

	public EquipItemInfo GetUniqueEquippingItemInfo(int equip_slot, int set_no = -1)
	{
		if (set_no == -1 && MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			set_no = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ueSetNo;
		}
		if (uniqueEquipSet == null)
		{
			return null;
		}
		if (set_no < 0)
		{
			return null;
		}
		if (set_no >= uniqueEquipSet.Length)
		{
			return null;
		}
		if (equip_slot >= 7)
		{
			return null;
		}
		if (uniqueEquipSet[set_no] == null)
		{
			return null;
		}
		if (uniqueEquipSet[set_no].item == null)
		{
			return null;
		}
		return uniqueEquipSet[set_no].item[equip_slot];
	}

	public StageObjectManager.CreatePlayerInfo GetCreateUniquePlayerInfo(int order)
	{
		StageObjectManager.CreatePlayerInfo createPlayerInfo = new StageObjectManager.CreatePlayerInfo();
		createPlayerInfo.charaInfo = new CharaInfo();
		createPlayerInfo.extentionInfo = new StageObjectManager.CreatePlayerInfo.ExtentionInfo();
		createPlayerInfo.extentionInfo.uniqueEquipmentIndex = order;
		EquipSetInfo orderUniqueEquipSet = GetOrderUniqueEquipSet(order);
		if (orderUniqueEquipSet == null)
		{
			createPlayerInfo.charaInfo = null;
			return createPlayerInfo;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
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
			EquipItemInfo equipItemInfo = orderUniqueEquipSet.item[3];
			EquipItemInfo equipItemInfo2 = orderUniqueEquipSet.item[4];
			EquipItemInfo equipItemInfo3 = orderUniqueEquipSet.item[5];
			EquipItemInfo equipItemInfo4 = orderUniqueEquipSet.item[6];
			string str_uniq_id = (equipItemInfo != null) ? equipItemInfo.uniqueID.ToString() : "0";
			string str_uniq_id2 = (equipItemInfo2 != null) ? equipItemInfo2.uniqueID.ToString() : "0";
			string str_uniq_id3 = (equipItemInfo3 != null) ? equipItemInfo3.uniqueID.ToString() : "0";
			string str_uniq_id4 = (equipItemInfo4 != null) ? equipItemInfo4.uniqueID.ToString() : "0";
			createPlayerInfo.charaInfo.aId = (int)MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(str_uniq_id);
			createPlayerInfo.charaInfo.hId = (int)MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(str_uniq_id2);
			createPlayerInfo.charaInfo.rId = (int)MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(str_uniq_id3);
			createPlayerInfo.charaInfo.lId = (int)MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.GetTableID(str_uniq_id4);
			createPlayerInfo.charaInfo.showHelm = orderUniqueEquipSet.showHelm;
			if (MonoBehaviourSingleton<PartyManager>.IsValid())
			{
				PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<PartyManager>.I.GetSlotInfoByUserId(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
				if (slotInfoByUserId != null && slotInfoByUserId.userInfo != null)
				{
					createPlayerInfo.charaInfo.userClanData = slotInfoByUserId.userInfo.userClanData;
				}
			}
			createPlayerInfo.charaInfo.equipSetName = orderUniqueEquipSet.name;
		}
		for (int i = 0; i < 7; i++)
		{
			CharaInfo.EquipItem equipItem = null;
			EquipItemInfo equipItemInfo5 = orderUniqueEquipSet.item[i];
			if (equipItemInfo5 != null)
			{
				equipItem = new CharaInfo.EquipItem();
				equipItem.eId = (int)equipItemInfo5.tableID;
				equipItem.lv = equipItemInfo5.level;
				equipItem.exceed = equipItemInfo5.exceed;
				int j = 0;
				for (int maxSlot = equipItemInfo5.GetMaxSlot(); j < maxSlot; j++)
				{
					SkillItemInfo uniqueSkillItem = equipItemInfo5.GetUniqueSkillItem(j);
					if (uniqueSkillItem != null)
					{
						equipItem.sIds.Add((int)uniqueSkillItem.tableID);
						equipItem.sLvs.Add(uniqueSkillItem.level);
						equipItem.sExs.Add(uniqueSkillItem.exceedCnt);
					}
				}
				EquipItemAbility[] lotteryAbility = equipItemInfo5.GetLotteryAbility();
				int k = 0;
				for (int num = lotteryAbility.Length; k < num; k++)
				{
					if (lotteryAbility[k].id != 0)
					{
						equipItem.aIds.Add((int)lotteryAbility[k].id);
						equipItem.aPts.Add(lotteryAbility[k].ap);
					}
				}
				AbilityItemInfo abilityItem = equipItemInfo5.GetAbilityItem();
				if (abilityItem != null && abilityItem.tableID != 0)
				{
					equipItem.ai = abilityItem.originalData;
				}
				if (equipItem != null)
				{
					createPlayerInfo.charaInfo.equipSet.Add(equipItem);
				}
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
		AccessoryPlaceInfo acc = orderUniqueEquipSet.acc;
		if (acc != null)
		{
			createPlayerInfo.charaInfo.accessory = acc.ConvertAccessory();
		}
		return createPlayerInfo;
	}

	public void OnDiff(BaseModelDiff.DiffUniqueAccessorySet diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.add))
		{
			int i = 0;
			for (int count = diff.add.Count; i < count; i++)
			{
				AccessorySet accessorySet = diff.add[i];
				if (accessorySet.setNo < uniqueEquipSet.Length)
				{
					uniqueEquipSet[accessorySet.setNo].acc.Clear();
					if (accessorySet.attachPlace != "-1")
					{
						uniqueEquipSet[accessorySet.setNo].acc.Add(accessorySet.uniqId, accessorySet.attachPlace);
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
				if (accessorySet2.setNo < uniqueEquipSet.Length)
				{
					uniqueEquipSet[accessorySet2.setNo].acc.Clear();
					if (accessorySet2.attachPlace != "-1")
					{
						uniqueEquipSet[accessorySet2.setNo].acc.Add(accessorySet2.uniqId, accessorySet2.attachPlace);
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

	public AccessoryPlaceInfo GetUniqueEquippingAccessoryInfo(int set_no = -1)
	{
		if (set_no == -1 && MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			set_no = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ueSetNo;
		}
		if (uniqueEquipSet == null)
		{
			return null;
		}
		if (set_no < 0)
		{
			return null;
		}
		if (set_no >= uniqueEquipSet.Length)
		{
			return null;
		}
		if (uniqueEquipSet[set_no] == null)
		{
			return null;
		}
		return uniqueEquipSet[set_no].acc;
	}

	public void ReplaceUniqueEquipSet(EquipSetInfo info, int setNo)
	{
		if (uniqueEquipSetCalc != null && setNo < uniqueEquipSetCalc.Length)
		{
			uniqueEquipSetCalc[setNo].SetEquipSet(info, setNo, isUnique: true);
		}
	}

	public void ReplaceUniqueEquipSets(EquipSetInfo[] info)
	{
		int i = 0;
		for (int num = info.Length; i < num; i++)
		{
			uniqueEquipSetCalc[i].SetEquipSet(info[i], i, isUnique: true);
		}
		isEquipSetCalcUpdate = false;
	}

	public bool IsEquipSkillSlotCheck(SkillItemInfo skillInfo, EquipItemInfo equipInfo, int slotNo)
	{
		if (skillInfo == null || equipInfo == null || skillInfo.uniqueEquipSetSkill == null)
		{
			return false;
		}
		ulong equipItemUniqId = skillInfo.uniqueEquipSetSkill.equipItemUniqId;
		int equipSlotNo = skillInfo.uniqueEquipSetSkill.equipSlotNo;
		if (skillInfo.isUniqueAttached)
		{
			if (equipItemUniqId == equipInfo.uniqueID)
			{
				return equipSlotNo != slotNo;
			}
			return true;
		}
		return false;
	}

	public bool CopyEquipSetCheck(int setNo)
	{
		EquipSetInfo equipSetInfo = GetEquipSet(setNo);
		for (int i = 0; i < equipSetInfo.item.Length; i++)
		{
			if (IsUniqueLocalEquipping(equipSetInfo.item[i], localEquipSetNo))
			{
				return false;
			}
			int num = (equipSetInfo.item[i] != null) ? equipSetInfo.item[i].GetMaxSlot() : 0;
			for (int j = 0; j < num; j++)
			{
				int num2 = j;
				if (equipSetInfo.item[i].IsExceedSkillSlot(num2))
				{
					num2 = equipSetInfo.item[i].GetExceedSkillSlotNo(num2);
				}
				SkillItemInfo skillItem = equipSetInfo.item[i].GetSkillItem(num2, setNo);
				if (IsEquipSkillSlotCheck(skillItem, equipSetInfo.item[i], num2))
				{
					return false;
				}
			}
		}
		return true;
	}

	public void CopyEquipSet(int setNo, Action<bool> call_back)
	{
		EquipSetInfo equipSetInfo = GetEquipSets()[setNo];
		EquipSetInfo equipSetInfo2 = localEquipSet[localEquipSetNo];
		List<ulong> list = new List<ulong>();
		List<ulong> list2 = new List<ulong>();
		List<int> list3 = new List<int>();
		List<ulong> list4 = new List<ulong>();
		for (int i = 0; i < equipSetInfo2.item.Length; i++)
		{
			int num = (equipSetInfo2.item[i] != null) ? equipSetInfo2.item[i].GetMaxSlot() : 0;
			for (int j = 0; j < num; j++)
			{
				SkillItemInfo uniqueSkillItem = equipSetInfo2.item[i].GetUniqueSkillItem(j);
				if (uniqueSkillItem != null)
				{
					list4.Add(uniqueSkillItem.uniqueID);
				}
			}
		}
		for (int k = 0; k < equipSetInfo.item.Length; k++)
		{
			if (!IsUniqueLocalEquipping(equipSetInfo.item[k], localEquipSetNo))
			{
				equipSetInfo2.item[k] = equipSetInfo.item[k];
			}
			else
			{
				equipSetInfo2.item[k] = null;
			}
		}
		for (int l = 0; l < equipSetInfo2.item.Length; l++)
		{
			EquipItemInfo equipItemInfo = equipSetInfo2.item[l];
			int num2 = equipItemInfo?.GetMaxSlot() ?? 0;
			for (int m = 0; m < num2; m++)
			{
				SkillItemInfo skillItem = equipItemInfo.GetSkillItem(m, setNo);
				ulong item = skillItem?.uniqueID ?? 0;
				int num3 = m;
				if (equipItemInfo.IsExceedSkillSlot(num3))
				{
					num3 = equipItemInfo.GetExceedSkillSlotNo(num3);
				}
				if (!IsEquipSkillSlotCheck(skillItem, equipItemInfo, num3) || list4.Contains(item))
				{
					list.Add(item);
					list2.Add(equipItemInfo.uniqueID);
					list3.Add(num3);
				}
				else
				{
					list.Add(0uL);
					list2.Add(equipItemInfo.uniqueID);
					list3.Add(num3);
				}
			}
		}
		if (list2.Count > 0)
		{
			SendUniqueSetSkillMultiple(list2, list, list3, call_back);
		}
		else
		{
			call_back(obj: true);
		}
		ReplaceUniqueEquipSet(equipSetInfo2, localEquipSetNo);
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET_INFO);
	}

	public void ReplaceUniqueEquipItem(EquipSetInfo info, int setNo, int index)
	{
		if (uniqueEquipSetCalc != null && setNo < uniqueEquipSetCalc.Length)
		{
			CharaInfo.EquipItem item = info.ConvertSelfUniqueEquipSetItem(index, setNo);
			uniqueEquipSetCalc[setNo].SetEquipItem(item, index);
		}
	}

	public void UpdateUniqueEquip(EquipItemInfo equip)
	{
		if (IsUniqueEquipping(equip))
		{
			int i = 0;
			for (int num = uniqueEquipSet.Length; i < num; i++)
			{
				IsUniqueEquipping(i, equip, delegate(int set_no, int index)
				{
					EquipSetInfo equipSetInfo = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquipSet(set_no);
					equipSetInfo.item[index] = equip;
					ReplaceUniqueEquipItem(equipSetInfo, set_no, index);
				});
			}
		}
	}

	public EquipSetCalculator GetUniqueEquipSetCalculator(int setNo)
	{
		if (uniqueEquipSetCalc == null || setNo >= uniqueEquipSetCalc.Length || setNo < 0)
		{
			return null;
		}
		return uniqueEquipSetCalc[setNo];
	}

	public void SwapUniqueWeapon(int swapIndex, int nowIndex)
	{
		uniqueEquipSetCalc[localEquipSetNo].SwapWeapon(swapIndex, nowIndex);
	}

	public void SwapClosetUniqueWeapon(int swapSetNo, int nowSetNo)
	{
	}

	public void ChangeOrderNo(int order, int setNo, Action<bool> call_back)
	{
		EquipSetInfo orderUniqueEquipSet = GetOrderUniqueEquipSet(order);
		if (orderUniqueEquipSet != null)
		{
			orderUniqueEquipSet.order = 0;
		}
		uniqueEquipSet[setNo].order = order;
		SendUniqueEquipSetOrder(call_back);
	}

	public void RemoveOrderNo(int setNo, Action<bool> call_back)
	{
		uniqueEquipSet[setNo].order = 0;
		SendUniqueEquipSetOrder(call_back);
		localEquipSet[setNo].order = 0;
	}

	public void SendUniqueEquipSetOrder(Action<bool> call_back)
	{
		UniqueEquipSetOrderModel.RequestSendForm requestSendForm = new UniqueEquipSetOrderModel.RequestSendForm();
		requestSendForm.selects = new List<int>();
		requestSendForm.nos = new List<int>();
		for (int i = 0; i < uniqueEquipSet.Length; i++)
		{
			requestSendForm.selects.Add(uniqueEquipSet[i].order);
			requestSendForm.nos.Add(i);
		}
		Protocol.Send(UniqueEquipSetOrderModel.URL, requestSendForm, delegate(UniqueEquipSetOrderModel ret)
		{
			if (ret.Error == Error.None)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_CHANGE);
			}
			call_back(ret.Error == Error.None);
		});
	}

	public uint GetUniqueEquippingItemTableID(int equip_slot, int set_no = -1)
	{
		return GetUniqueEquippingItemInfo(equip_slot, set_no)?.tableID ?? 0;
	}

	public int GetUniqueEquippingShowHelm(int set_no = -1)
	{
		if (set_no == -1 && MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			set_no = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ueSetNo;
		}
		if (set_no < 0)
		{
			return 1;
		}
		if (set_no >= uniqueEquipSet.Length)
		{
			return 1;
		}
		if (uniqueEquipSet[set_no] == null)
		{
			return 1;
		}
		return uniqueEquipSet[set_no].showHelm;
	}
}
