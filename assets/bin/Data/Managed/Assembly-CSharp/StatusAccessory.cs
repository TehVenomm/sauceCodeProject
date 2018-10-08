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

	private string selectUUID = string.Empty;

	private uint putUID;

	private ACCESSORY_PART putPart = ACCESSORY_PART.NONE;

	private Transform originParent;

	private Vector3 originPos = default(Vector3);

	private Vector3 originScale = default(Vector3);

	private Quaternion originRot = Quaternion.get_identity();

	private Dictionary<ulong, Transform> iconDic = new Dictionary<ulong, Transform>();

	protected override void OnClose()
	{
		Finalize();
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

	private new void Finalize()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit(selectModelTrans))
		{
			if (originParent != null)
			{
				selectModelTrans.SetParent(originParent);
				selectModelTrans.set_localPosition(originPos);
				selectModelTrans.set_localScale(originScale);
				selectModelTrans.set_localRotation(originRot);
			}
			else
			{
				playerLoader.DeleteAccessoryModel(selectModelTrans.get_name());
			}
		}
		selectModelTrans = null;
		originParent = null;
		if (Object.op_Implicit(selectIconTrans))
		{
			selectIconTrans.SetParent(this.get_transform());
			selectIconTrans.get_gameObject().SetActive(false);
		}
		selectIconTrans = null;
		selectItem = null;
		selectUUID = string.Empty;
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
		localInventory = sortSettings.CreateSortAry<AccessoryInfo, AccessorySortData>(target_ary);
	}

	public unsafe override void UpdateUI()
	{
		if (dispState != eDispState.Part)
		{
			if (localInventory == null)
			{
				InitLocalInventory();
			}
			SetLabelText((Enum)UI.LBL_SORT, sortSettings.GetSortLabel());
			SetToggle((Enum)UI.TGL_ICON_ASC, sortSettings.orderTypeAsc);
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
			SetActive((Enum)UI.LBL_NON_LIST, num <= 0);
			_003CUpdateUI_003Ec__AnonStorey46B _003CUpdateUI_003Ec__AnonStorey46B;
			SetDynamicList((Enum)UI.GRD_INVENTORY, (string)null, num, false, new Func<int, bool>((object)_003CUpdateUI_003Ec__AnonStorey46B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), null, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey46B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			base.UpdateUI();
			SetActive((Enum)UI.LIST_ROOT, true);
			SetActive((Enum)UI.ACCESSORY_ROOT, false);
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
			SetActive((Enum)UI.LIST_ROOT, dispState == eDispState.List);
			SetActive((Enum)UI.ACCESSORY_ROOT, dispState == eDispState.Part);
		}
	}

	private void ChangeMode(ePutMode mode)
	{
		putMode = mode;
		SetActive((Enum)UI.LBL_ON, putMode == ePutMode.On);
		SetActive((Enum)UI.LBL_OFF, putMode == ePutMode.Off);
		SetActive((Enum)UI.LBL_LIMIT, putMode == ePutMode.Limit);
	}

	private Transform CreateIcon(ulong _uuid)
	{
		Transform val = null;
		if (!iconDic.ContainsKey(_uuid))
		{
			int i = 0;
			for (int num = localInventory.Length; i < num; i++)
			{
				AccessorySortData accessorySortData = localInventory[i] as AccessorySortData;
				if (accessorySortData.GetUniqID() == _uuid)
				{
					val = AccessoryIcon.Create(accessorySortData.itemData.tableData.accessoryId, accessorySortData.itemData.tableData.rarity, accessorySortData.itemData.tableData.getType);
					break;
				}
			}
			if (val != null)
			{
				iconDic.Add(_uuid, val);
			}
		}
		else
		{
			val = iconDic[_uuid];
		}
		return val;
	}

	private void ResetIcon()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		foreach (KeyValuePair<ulong, Transform> item in iconDic)
		{
			if (!(item.Value == null))
			{
				item.Value.SetParent(this.get_transform());
				item.Value.get_gameObject().SetActive(false);
			}
		}
		iconDic.Clear();
	}

	private void SetIconParent(int part, Transform iconTrans)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		iconTrans.SetParent(GetCtrl(partParent[part]));
		iconTrans.set_localPosition(Vector3.get_zero());
		iconTrans.set_localRotation(Quaternion.get_identity());
		iconTrans.set_localScale(Vector3.get_one());
		iconTrans.get_gameObject().SetActive(true);
	}

	private void RemoveIconParent(int part)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(partParent[part]);
		if (ctrl.get_childCount() > 0)
		{
			Transform val = ctrl.GetChild(0);
			if (!(val == null))
			{
				val.get_gameObject().SetActive(false);
			}
		}
	}

	private void SetupIcon(int index)
	{
		if (tweenCtrl != null)
		{
			tweenCtrl.Reset();
			tweenCtrl.Play(true, null);
		}
		if (index == -1)
		{
			_SetupIconOff();
		}
		else
		{
			_SetupIconOn(index);
		}
		SetActive((Enum)UI.BTN_DECIDE, false);
	}

	private void _SetupIconOff()
	{
		for (int i = 0; i <= 9; i++)
		{
			SetActive((Enum)partButton[i], false);
		}
		EquipSetInfo currentLocalEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetCurrentLocalEquipSet();
		int j = 0;
		for (int count = currentLocalEquipSet.acc.ids.Count; j < count; j++)
		{
			int part = currentLocalEquipSet.acc.GetPart(j);
			if (part >= 0)
			{
				SetActive((Enum)partButton[part], true);
				SetIconParent(part, CreateIcon(currentLocalEquipSet.acc.GetId(j)));
			}
		}
		ChangeMode(ePutMode.Off);
	}

	private void _SetupIconOn(int index)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		AccessorySortData accessorySortData = localInventory[index] as AccessorySortData;
		selectItem = accessorySortData.itemData;
		selectUUID = accessorySortData.itemData.uniqueID.ToString();
		selectIconTrans = CreateIcon(selectItem.uniqueID);
		selectIconTrans.get_gameObject().SetActive(false);
		bool flag = false;
		EquipSetInfo currentLocalEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetCurrentLocalEquipSet();
		int i = 0;
		for (int count = currentLocalEquipSet.acc.ids.Count; i < count; i++)
		{
			string a = currentLocalEquipSet.acc.ids[i];
			if (a == selectUUID)
			{
				AccessoryTable.AccessoryInfoData info = selectItem.GetInfo((ACCESSORY_PART)currentLocalEquipSet.acc.GetPart(i));
				if (info != null)
				{
					selectModelTrans = playerLoader.GetAccessoryModel(ResourceName.GetPlayerAccessory(info.accessoryId));
					originParent = selectModelTrans.get_parent();
					originPos = selectModelTrans.get_localPosition();
					originScale = selectModelTrans.get_localScale();
					originRot = selectModelTrans.get_localRotation();
					SetIconParent(currentLocalEquipSet.acc.GetPart(i), selectIconTrans);
					selectIconTrans.get_gameObject().SetActive(true);
					flag = true;
					break;
				}
			}
		}
		if (!flag && currentLocalEquipSet.acc.ids.Count >= maxNum)
		{
			for (int j = 0; j <= 9; j++)
			{
				SetActive((Enum)partButton[j], false);
			}
			int k = 0;
			for (int count2 = currentLocalEquipSet.acc.ids.Count; k < count2; k++)
			{
				int part = currentLocalEquipSet.acc.GetPart(k);
				if (part >= 0)
				{
					SetActive((Enum)partButton[part], true);
					SetIconParent(part, CreateIcon(currentLocalEquipSet.acc.GetId(k)));
				}
			}
			ChangeMode(ePutMode.Limit);
		}
		else
		{
			for (int l = 0; l <= 9; l++)
			{
				SetActive((Enum)partButton[l], (selectItem.tableData.attachPlaceBit & (1 << l)) != 0);
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
		Finalize();
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
		selectUUID = string.Empty;
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
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		if (!isLoading && selectItem != null)
		{
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
					this.StartCoroutine(_Load(info.accessoryId, delegate(Transform t)
					{
						selectModelTrans = t;
						playerLoader.AddAccessoryModel(t);
						SetupModel(info);
					}));
				}
			}
		}
	}

	private void SetupModel(AccessoryTable.AccessoryInfoData info)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		if (!(selectModelTrans == null))
		{
			PlayerLoader.SetLightProbes(selectModelTrans, false);
			if (renderLayer != -1)
			{
				PlayerLoader.SetLayerWithChildren_SecondaryNoChange(selectModelTrans, renderLayer);
			}
			selectModelTrans.SetParent(playerLoader.GetNodeTrans(info.node));
			selectModelTrans.set_localPosition(info.offset);
			selectModelTrans.set_localRotation(info.rotation);
			selectModelTrans.set_localScale(info.scale);
			SetActive((Enum)UI.BTN_DECIDE, true);
		}
	}

	private IEnumerator _Load(uint id, Action<Transform> callback)
	{
		isLoading = true;
		LoadingQueue queue = new LoadingQueue(this);
		LoadObject lo = queue.Load(RESOURCE_CATEGORY.PLAYER_ACCESSORY, ResourceName.GetPlayerAccessory(id), false);
		if (queue.IsLoading())
		{
			yield return (object)queue.Wait();
		}
		if (lo == null)
		{
			Debug.LogWarning((object)("StatusAccessory::_Load() cant load [" + ResourceName.GetPlayerAccessory(id) + "]"));
		}
		else
		{
			Transform trans = lo.Realizes(null, -1);
			if (trans == null)
			{
				Debug.LogWarning((object)("StatusAccessory::_Load() cant realizes [" + ResourceName.GetPlayerAccessory(id) + "]"));
			}
			else
			{
				isLoading = false;
				callback?.Invoke(trans);
			}
		}
	}

	private void PutOff(ACCESSORY_PART _part)
	{
		uint num = 0u;
		EquipSetInfo currentLocalEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetCurrentLocalEquipSet();
		int i = 0;
		for (int count = currentLocalEquipSet.acc.ids.Count; i < count; i++)
		{
			int part = currentLocalEquipSet.acc.GetPart(i);
			if (part == (int)_part)
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
		}
		else
		{
			isRefresh = true;
			for (int k = 0; k <= 9; k++)
			{
				SetActive((Enum)partButton[k], (selectItem.tableData.attachPlaceBit & (1 << k)) != 0);
			}
			ChangeMode(ePutMode.On);
		}
	}
}
