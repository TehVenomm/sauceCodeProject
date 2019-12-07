using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusAccessory : SkillInfoBase
{
	public enum UI
	{
		SCR_INVENTORY,
		GRD_INVENTORY,
		LBL_SORT,
		TGL_ICON_ASC,
		LIST_ROOT,
		ACCESSORY_ROOT,
		LBL_ON,
		LBL_OFF,
		LBL_LIMIT,
		BTN_ICON_HEAD,
		BTN_ICON_FACE,
		BTN_ICON_R_SHOULDER,
		BTN_ICON_L_SHOULDER,
		BTN_ICON_R_ARM,
		BTN_ICON_L_ARM,
		BTN_ICON_CHEST,
		BTN_ICON_WAIST,
		BTN_ICON_R_LEG,
		BTN_ICON_L_LEG,
		OBJ_ICON_HEAD,
		OBJ_ICON_FACE,
		OBJ_ICON_R_SHOULDER,
		OBJ_ICON_L_SHOULDER,
		OBJ_ICON_R_ARM,
		OBJ_ICON_L_ARM,
		OBJ_ICON_CHEST,
		OBJ_ICON_WAIST,
		OBJ_ICON_R_LEG,
		OBJ_ICON_L_LEG,
		BTN_DECIDE,
		LBL_NON_LIST
	}

	private enum eDispState
	{
		List,
		Part
	}

	private enum ePutMode
	{
		On,
		Off,
		Limit
	}

	private UI[] partButton = new UI[10]
	{
		UI.BTN_ICON_HEAD,
		UI.BTN_ICON_FACE,
		UI.BTN_ICON_R_SHOULDER,
		UI.BTN_ICON_L_SHOULDER,
		UI.BTN_ICON_R_ARM,
		UI.BTN_ICON_L_ARM,
		UI.BTN_ICON_CHEST,
		UI.BTN_ICON_WAIST,
		UI.BTN_ICON_R_LEG,
		UI.BTN_ICON_L_LEG
	};

	private UI[] partParent = new UI[10]
	{
		UI.OBJ_ICON_HEAD,
		UI.OBJ_ICON_FACE,
		UI.OBJ_ICON_R_SHOULDER,
		UI.OBJ_ICON_L_SHOULDER,
		UI.OBJ_ICON_R_ARM,
		UI.OBJ_ICON_L_ARM,
		UI.OBJ_ICON_CHEST,
		UI.OBJ_ICON_WAIST,
		UI.OBJ_ICON_R_LEG,
		UI.OBJ_ICON_L_LEG
	};

	private int maxNum = 1;

	private SortCompareData[] localInventory;

	private SortSettings sortSettings;

	private eDispState dispState;

	private ePutMode putMode;

	private UITweenCtrl tweenCtrl;

	private bool isLoading;

	private PlayerLoader playerLoader;

	private int renderLayer = -1;

	private bool isRefresh;

	private Transform selectIconTrans;

	private Transform selectModelTrans;

	private AccessoryInfo selectItem;

	private string selectUUID = "";

	private uint putUID;

	private ACCESSORY_PART putPart = ACCESSORY_PART.NONE;

	private Transform originParent;

	private Vector3 originPos;

	private Vector3 originScale;

	private Quaternion originRot = Quaternion.identity;

	private Dictionary<ulong, Transform> iconDic = new Dictionary<ulong, Transform>();

	protected override void OnClose()
	{
		TryFinalize();
		base.OnClose();
	}

	public override void Initialize()
	{
		maxNum = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.MAX_ATTACH_ACCESSORY_NUM;
		playerLoader = MonoBehaviourSingleton<StatusStageManager>.I.GetPlayerLoader();
		renderLayer = MonoBehaviourSingleton<StatusStageManager>.I.GetPlayerLayer();
		InitSort();
		InitLocalInventory();
		Transform ctrl = GetCtrl(UI.ACCESSORY_ROOT);
		if (ctrl != null)
		{
			tweenCtrl = ctrl.GetComponent<UITweenCtrl>();
		}
		base.Initialize();
	}

	private void TryFinalize()
	{
		if ((bool)selectModelTrans)
		{
			if (originParent != null)
			{
				selectModelTrans.SetParent(originParent);
				selectModelTrans.localPosition = originPos;
				selectModelTrans.localScale = originScale;
				selectModelTrans.localRotation = originRot;
			}
			else
			{
				playerLoader.DeleteAccessoryModel(selectModelTrans.name);
			}
		}
		selectModelTrans = null;
		originParent = null;
		if ((bool)selectIconTrans)
		{
			selectIconTrans.SetParent(base.transform);
			selectIconTrans.gameObject.SetActive(value: false);
		}
		selectIconTrans = null;
		selectItem = null;
		selectUUID = "";
		putUID = 0u;
		putPart = ACCESSORY_PART.NONE;
	}

	protected virtual void Update()
	{
		ObserveItemList();
	}

	private void InitSort()
	{
		sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.ACCESSORY, SortSettings.SETTINGS_TYPE.STORAGE_ACCESSORY);
	}

	private void InitLocalInventory()
	{
		AccessoryInfo[] target_ary = MonoBehaviourSingleton<InventoryManager>.I.accessoryInventory.GetAll().ToArray();
		SortCompareData[] array = localInventory = sortSettings.CreateSortAry<AccessoryInfo, AccessorySortData>(target_ary);
	}

	public override void UpdateUI()
	{
		if (dispState != eDispState.Part)
		{
			if (localInventory == null)
			{
				InitLocalInventory();
			}
			SetLabelText(UI.LBL_SORT, sortSettings.GetSortLabel());
			SetToggle(UI.TGL_ICON_ASC, sortSettings.orderTypeAsc);
			m_generatedIconList.Clear();
			ResetIcon();
			UpdateNewIconInfo();
			int num = localInventory.Length;
			EquipSetInfo equipSet = MonoBehaviourSingleton<StatusManager>.I.GetCurrentLocalEquipSet();
			bool isRemoveBtn = equipSet.acc.ids.Count >= maxNum;
			if (isRemoveBtn)
			{
				num++;
			}
			SetActive(UI.LBL_NON_LIST, num <= 0);
			SetDynamicList(UI.GRD_INVENTORY, null, num, reset: false, delegate(int i)
			{
				if (isRemoveBtn && i == 0)
				{
					return true;
				}
				int num3 = isRemoveBtn ? (i - 1) : i;
				SortCompareData sortCompareData = localInventory[num3];
				return (sortCompareData != null && sortCompareData.IsPriority(sortSettings.orderTypeAsc)) ? true : false;
			}, null, delegate(int i, Transform t, bool is_recycle)
			{
				if (isRemoveBtn && i == 0)
				{
					ItemIconDetail.CreateRemoveButton(t, "TRY_ON", -1, 100, is_select: false, StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 44u));
				}
				else
				{
					int num2 = isRemoveBtn ? (i - 1) : i;
					if (num2 >= localInventory.Length)
					{
						SetActive(t, is_visible: false);
					}
					else
					{
						AccessorySortData accessorySortData = localInventory[num2] as AccessorySortData;
						if (accessorySortData == null || accessorySortData.GetTableID() == 0)
						{
							SetActive(t, is_visible: false);
						}
						else
						{
							SetActive(t, is_visible: true);
							bool isNew = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(ITEM_ICON_TYPE.ACCESSORY, accessorySortData.GetUniqID());
							bool isEquipping = false;
							int j = 0;
							for (int count = equipSet.acc.ids.Count; j < count; j++)
							{
								if (equipSet.acc.ids[j].Equals(accessorySortData.GetUniqID().ToString()))
								{
									isEquipping = true;
									break;
								}
							}
							ItemIcon itemIcon = ItemIconDetail.CreateAccessoryIcon(accessorySortData.itemData.tableData, t, "TRY_ON", num2, isNew, isEquipping);
							itemIcon.SetInitData(accessorySortData);
							SetLongTouch(itemIcon.transform, "DETAIL", accessorySortData);
							if (!m_generatedIconList.Contains(itemIcon))
							{
								m_generatedIconList.Add(itemIcon);
							}
						}
					}
				}
			});
			base.UpdateUI();
			SetActive(UI.LIST_ROOT, is_visible: true);
			SetActive(UI.ACCESSORY_ROOT, is_visible: false);
		}
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return base.GetUpdateUINotifyFlags() | NOTIFY_FLAG.UPDATE_EQUIP_GROW;
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_EQUIP_GROW) != (NOTIFY_FLAG)0L)
		{
			InitLocalInventory();
			isRefresh = true;
		}
		base.OnNotify(flags);
	}

	private void ChangeDisp(eDispState state)
	{
		if (dispState != state)
		{
			dispState = state;
			SetActive(UI.LIST_ROOT, dispState == eDispState.List);
			SetActive(UI.ACCESSORY_ROOT, dispState == eDispState.Part);
		}
	}

	private void ChangeMode(ePutMode mode)
	{
		putMode = mode;
		SetActive(UI.LBL_ON, putMode == ePutMode.On);
		SetActive(UI.LBL_OFF, putMode == ePutMode.Off);
		SetActive(UI.LBL_LIMIT, putMode == ePutMode.Limit);
	}

	private Transform CreateIcon(ulong _uuid)
	{
		Transform transform = null;
		if (!iconDic.ContainsKey(_uuid))
		{
			int i = 0;
			for (int num = localInventory.Length; i < num; i++)
			{
				AccessorySortData accessorySortData = localInventory[i] as AccessorySortData;
				if (accessorySortData.GetUniqID() == _uuid)
				{
					transform = AccessoryIcon.Create(accessorySortData.itemData.tableData.accessoryId, accessorySortData.itemData.tableData.rarity, accessorySortData.itemData.tableData.getType);
					break;
				}
			}
			if (transform != null)
			{
				iconDic.Add(_uuid, transform);
			}
		}
		else
		{
			transform = iconDic[_uuid];
		}
		return transform;
	}

	private void ResetIcon()
	{
		foreach (KeyValuePair<ulong, Transform> item in iconDic)
		{
			if (!(item.Value == null))
			{
				item.Value.SetParent(base.transform);
				item.Value.gameObject.SetActive(value: false);
			}
		}
		iconDic.Clear();
	}

	private void SetIconParent(int part, Transform iconTrans)
	{
		iconTrans.SetParent(GetCtrl(partParent[part]));
		iconTrans.localPosition = Vector3.zero;
		iconTrans.localRotation = Quaternion.identity;
		iconTrans.localScale = Vector3.one;
		iconTrans.gameObject.SetActive(value: true);
	}

	private void RemoveIconParent(int part)
	{
		Transform ctrl = GetCtrl(partParent[part]);
		if (ctrl.childCount > 0)
		{
			Transform child = ctrl.GetChild(0);
			if (!(child == null))
			{
				child.gameObject.SetActive(value: false);
			}
		}
	}

	private void SetupIcon(int index)
	{
		if (tweenCtrl != null)
		{
			tweenCtrl.Reset();
			tweenCtrl.Play();
		}
		if (index == -1)
		{
			_SetupIconOff();
		}
		else
		{
			_SetupIconOn(index);
		}
		SetActive(UI.BTN_DECIDE, is_visible: false);
	}

	private void _SetupIconOff()
	{
		for (int i = 0; i <= 9; i++)
		{
			SetActive(partButton[i], is_visible: false);
		}
		EquipSetInfo currentLocalEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetCurrentLocalEquipSet();
		int j = 0;
		for (int count = currentLocalEquipSet.acc.ids.Count; j < count; j++)
		{
			int part = currentLocalEquipSet.acc.GetPart(j);
			if (part >= 0)
			{
				SetActive(partButton[part], is_visible: true);
				SetIconParent(part, CreateIcon(currentLocalEquipSet.acc.GetId(j)));
			}
		}
		ChangeMode(ePutMode.Off);
	}

	private void _SetupIconOn(int index)
	{
		AccessorySortData accessorySortData = localInventory[index] as AccessorySortData;
		selectItem = accessorySortData.itemData;
		selectUUID = accessorySortData.itemData.uniqueID.ToString();
		selectIconTrans = CreateIcon(selectItem.uniqueID);
		selectIconTrans.gameObject.SetActive(value: false);
		bool flag = false;
		EquipSetInfo currentLocalEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetCurrentLocalEquipSet();
		int i = 0;
		for (int count = currentLocalEquipSet.acc.ids.Count; i < count; i++)
		{
			if (currentLocalEquipSet.acc.ids[i] == selectUUID)
			{
				AccessoryTable.AccessoryInfoData info = selectItem.GetInfo((ACCESSORY_PART)currentLocalEquipSet.acc.GetPart(i));
				if (info != null)
				{
					selectModelTrans = playerLoader.GetAccessoryModel(ResourceName.GetPlayerAccessory(info.accessoryId));
					originParent = selectModelTrans.parent;
					originPos = selectModelTrans.localPosition;
					originScale = selectModelTrans.localScale;
					originRot = selectModelTrans.localRotation;
					SetIconParent(currentLocalEquipSet.acc.GetPart(i), selectIconTrans);
					selectIconTrans.gameObject.SetActive(value: true);
					flag = true;
					break;
				}
			}
		}
		if (!flag && currentLocalEquipSet.acc.ids.Count >= maxNum)
		{
			for (int j = 0; j <= 9; j++)
			{
				SetActive(partButton[j], is_visible: false);
			}
			int k = 0;
			for (int count2 = currentLocalEquipSet.acc.ids.Count; k < count2; k++)
			{
				int part = currentLocalEquipSet.acc.GetPart(k);
				if (part >= 0)
				{
					SetActive(partButton[part], is_visible: true);
					SetIconParent(part, CreateIcon(currentLocalEquipSet.acc.GetId(k)));
				}
			}
			ChangeMode(ePutMode.Limit);
		}
		else
		{
			for (int l = 0; l <= 9; l++)
			{
				SetActive(partButton[l], (selectItem.tableData.attachPlaceBit & (1 << l)) != 0);
			}
			ChangeMode(ePutMode.On);
		}
	}

	private void OnQuery_BACK()
	{
		if (dispState == eDispState.List)
		{
			_BackList();
		}
		else
		{
			_BackPart();
		}
	}

	private void _BackList()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeSectionBack();
	}

	private void _BackPart()
	{
		TryFinalize();
		ChangeDisp(eDispState.List);
		if (isRefresh)
		{
			RefreshUI();
			isRefresh = false;
		}
	}

	private void OnQuery_DETAIL()
	{
		GameSection.ChangeEvent("ACCESSORY_SELECT", new object[2]
		{
			ItemDetailEquip.CURRENT_SECTION.ITEM_STORAGE,
			GameSection.GetEventData()
		});
	}

	private void OnQuery_TRY_ON()
	{
		SetupIcon((int)GameSection.GetEventData());
		ChangeDisp(eDispState.Part);
	}

	private void OnQuery_ACC_DECIDE()
	{
		if (putMode == ePutMode.On)
		{
			MonoBehaviourSingleton<StatusManager>.I.AccessoryOn(selectUUID, putPart);
			playerLoader.loadInfo.accUIDs.Clear();
			playerLoader.loadInfo.accUIDs.Add(putUID);
		}
		selectModelTrans = null;
		selectIconTrans = null;
		selectItem = null;
		selectUUID = "";
		putUID = 0u;
		putPart = ACCESSORY_PART.NONE;
		originParent = null;
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeSectionBack();
	}

	private void OnQuery_ACC_HEAD()
	{
		if (putMode == ePutMode.On)
		{
			PutOn(ACCESSORY_PART.HEAD);
		}
		else
		{
			PutOff(ACCESSORY_PART.HEAD);
		}
	}

	private void OnQuery_ACC_FACE()
	{
		if (putMode == ePutMode.On)
		{
			PutOn(ACCESSORY_PART.FACE);
		}
		else
		{
			PutOff(ACCESSORY_PART.FACE);
		}
	}

	private void OnQuery_ACC_R_SHOULDER()
	{
		if (putMode == ePutMode.On)
		{
			PutOn(ACCESSORY_PART.R_SHOULDER);
		}
		else
		{
			PutOff(ACCESSORY_PART.R_SHOULDER);
		}
	}

	private void OnQuery_ACC_L_SHOULDER()
	{
		if (putMode == ePutMode.On)
		{
			PutOn(ACCESSORY_PART.L_SHOULDER);
		}
		else
		{
			PutOff(ACCESSORY_PART.L_SHOULDER);
		}
	}

	private void OnQuery_ACC_R_ARM()
	{
		if (putMode == ePutMode.On)
		{
			PutOn(ACCESSORY_PART.R_ARM);
		}
		else
		{
			PutOff(ACCESSORY_PART.R_ARM);
		}
	}

	private void OnQuery_ACC_L_ARM()
	{
		if (putMode == ePutMode.On)
		{
			PutOn(ACCESSORY_PART.L_ARM);
		}
		else
		{
			PutOff(ACCESSORY_PART.L_ARM);
		}
	}

	private void OnQuery_ACC_CHEST()
	{
		if (putMode == ePutMode.On)
		{
			PutOn(ACCESSORY_PART.CHEST);
		}
		else
		{
			PutOff(ACCESSORY_PART.CHEST);
		}
	}

	private void OnQuery_ACC_WAIST()
	{
		if (putMode == ePutMode.On)
		{
			PutOn(ACCESSORY_PART.WAIST);
		}
		else
		{
			PutOff(ACCESSORY_PART.WAIST);
		}
	}

	private void OnQuery_ACC_R_LEG()
	{
		if (putMode == ePutMode.On)
		{
			PutOn(ACCESSORY_PART.R_LEG);
		}
		else
		{
			PutOff(ACCESSORY_PART.R_LEG);
		}
	}

	private void OnQuery_ACC_L_LEG()
	{
		if (putMode == ePutMode.On)
		{
			PutOn(ACCESSORY_PART.L_LEG);
		}
		else
		{
			PutOff(ACCESSORY_PART.L_LEG);
		}
	}

	private void PutOn(ACCESSORY_PART part)
	{
		if (isLoading || selectItem == null)
		{
			return;
		}
		AccessoryTable.AccessoryInfoData info = selectItem.GetInfo(part);
		if (info != null)
		{
			putUID = info.id;
			putPart = part;
			SetIconParent((int)part, selectIconTrans);
			if (selectModelTrans != null)
			{
				SetupModel(info);
			}
			else
			{
				StartCoroutine(_Load(info.accessoryId, delegate(Transform t)
				{
					selectModelTrans = t;
					playerLoader.AddAccessoryModel(t);
					SetupModel(info);
				}));
			}
		}
	}

	private void SetupModel(AccessoryTable.AccessoryInfoData info)
	{
		if (!(selectModelTrans == null))
		{
			PlayerLoader.SetLightProbes(selectModelTrans, enable_light_probes: false);
			if (renderLayer != -1)
			{
				PlayerLoader.SetLayerWithChildren_SecondaryNoChange(selectModelTrans, renderLayer);
			}
			selectModelTrans.SetParent(playerLoader.GetNodeTrans(info.node));
			selectModelTrans.localPosition = info.offset;
			selectModelTrans.localRotation = info.rotation;
			selectModelTrans.localScale = info.scale;
			SetActive(UI.BTN_DECIDE, is_visible: true);
		}
	}

	private IEnumerator _Load(uint id, Action<Transform> callback)
	{
		isLoading = true;
		LoadingQueue queue = new LoadingQueue(this);
		LoadObject lo = queue.Load(RESOURCE_CATEGORY.PLAYER_ACCESSORY, ResourceName.GetPlayerAccessory(id));
		if (queue.IsLoading())
		{
			yield return queue.Wait();
		}
		if (lo == null)
		{
			Debug.LogWarning("StatusAccessory::_Load() cant load [" + ResourceName.GetPlayerAccessory(id) + "]");
			yield break;
		}
		Transform trans = lo.Realizes();
		if (trans == null)
		{
			Debug.LogWarning("StatusAccessory::_Load() cant realizes [" + ResourceName.GetPlayerAccessory(id) + "]");
			yield break;
		}
		yield return StartCoroutine(ItemLoader.InitRoopEffect(queue, trans));
		isLoading = false;
		callback?.Invoke(trans);
	}

	private void PutOff(ACCESSORY_PART _part)
	{
		uint num = 0u;
		EquipSetInfo currentLocalEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetCurrentLocalEquipSet();
		int i = 0;
		for (int count = currentLocalEquipSet.acc.ids.Count; i < count; i++)
		{
			if (currentLocalEquipSet.acc.GetPart(i) == (int)_part)
			{
				num = currentLocalEquipSet.acc.GetId(i);
				MonoBehaviourSingleton<StatusManager>.I.AccessoryOff(currentLocalEquipSet.acc.ids[i], _part);
				break;
			}
		}
		int j = 0;
		for (int num2 = localInventory.Length; j < num2; j++)
		{
			AccessorySortData accessorySortData = localInventory[j] as AccessorySortData;
			if (accessorySortData.itemData.uniqueID == num)
			{
				uint tableID = accessorySortData.GetTableID();
				playerLoader.DeleteAccessoryModel(ResourceName.GetPlayerAccessory(tableID));
				break;
			}
		}
		RemoveIconParent((int)_part);
		if (putMode == ePutMode.Off)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ChangeSectionBack();
			return;
		}
		isRefresh = true;
		for (int k = 0; k <= 9; k++)
		{
			SetActive(partButton[k], (selectItem.tableData.attachPlaceBit & (1 << k)) != 0);
		}
		ChangeMode(ePutMode.On);
	}
}
