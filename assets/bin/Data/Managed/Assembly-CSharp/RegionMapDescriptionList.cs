using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RegionMapDescriptionList : GameSection
{
	protected enum UI
	{
		SCR_ALL,
		TBL_ALL,
		LBL_MAP_NAME,
		LBL_MAP_NAME_D,
		LBL_BORDER_TITLE,
		BTN_TO_FIELD,
		LBL_NON_LIST,
		SPR_BG_FRAME,
		TEX_FIELD
	}

	private struct EnemyDataForDisplay
	{
		public EnemyTable.EnemyData data;

		public int level;

		public EnemyDataForDisplay(EnemyTable.EnemyData data, uint level)
		{
			this.data = data;
			this.level = (int)level;
		}
	}

	private struct DeliveryDataAndUId
	{
		public DeliveryTable.DeliveryData data;

		public string uId;

		public DeliveryDataAndUId(DeliveryTable.DeliveryData data, string uId)
		{
			this.data = data;
			this.uId = uId;
		}
	}

	private RegionMap.SpotEventData mapData;

	private List<DeliveryDataAndUId> deliveryDataAndUIdList = new List<DeliveryDataAndUId>();

	private List<QuestTable.QuestTableData> happenDataList;

	private List<EnemyDataForDisplay> enemyDataList;

	public override void Initialize()
	{
		object eventData = GameSection.GetEventData();
		if (eventData != null && eventData is RegionMap.SpotEventData)
		{
			mapData = (RegionMap.SpotEventData)eventData;
		}
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && FieldManager.IsValidInGame())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
		}
		InitDataLists();
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			Reposition();
		}
		UpdateTable();
		bool is_visible = MonoBehaviourSingleton<FieldManager>.I.currentMapID != mapData.mapId;
		SetActive((Enum)UI.BTN_TO_FIELD, is_visible);
		base.UpdateUI();
	}

	protected unsafe void UpdateTable()
	{
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(mapData.mapId);
		string mapName = fieldMapData.mapName;
		SetLabelText((Enum)UI.LBL_MAP_NAME, mapName);
		SetLabelText((Enum)UI.LBL_MAP_NAME_D, mapName);
		UITexture component = GetCtrl(UI.TEX_FIELD).GetComponent<UITexture>();
		ResourceLoad.LoadFieldIconTexture(component, fieldMapData);
		Dictionary<int, string> borderIndexTitleDic = new Dictionary<int, string>(3);
		int count = deliveryDataAndUIdList.Count;
		int count2 = enemyDataList.Count;
		int count3 = happenDataList.Count;
		int num = 0;
		if (count >= 1)
		{
			borderIndexTitleDic[0] = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 0u);
			num++;
		}
		int deliveryStartIndex = num;
		if (count3 >= 1)
		{
			borderIndexTitleDic[count + deliveryStartIndex] = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 1u);
			num++;
		}
		int happenStartIndex = count + num;
		if (count2 >= 1)
		{
			borderIndexTitleDic[happenStartIndex + count3] = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 2u);
			num++;
		}
		int enemyStartIndex = count + count3 + num;
		ClearTable();
		int num2 = count + count3 + count2 + num;
		SetActive((Enum)UI.LBL_NON_LIST, num2 <= 0);
		_003CUpdateTable_003Ec__AnonStorey486 _003CUpdateTable_003Ec__AnonStorey;
		SetTable(UI.TBL_ALL, string.Empty, num2, true, new Func<int, Transform, Transform>((object)_003CUpdateTable_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), new Action<int, Transform, bool>((object)_003CUpdateTable_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void InitDataLists()
	{
		enemyDataList = CreateEnemyDataList(mapData.mapId);
		deliveryDataAndUIdList = CreateDeliveryList(mapData.mapId);
		happenDataList = CreateHappneList(mapData.mapId);
	}

	private List<QuestTable.QuestTableData> CreateHappneList(uint mapId)
	{
		List<QuestTable.QuestTableData> list = new List<QuestTable.QuestTableData>();
		Dictionary<uint, uint> questIdEventIdDic = Singleton<QuestToFieldTable>.I.GetQuestIdEventIdDic(mapId);
		if (questIdEventIdDic == null || questIdEventIdDic.Count <= 0)
		{
			return list;
		}
		foreach (KeyValuePair<uint, uint> item in questIdEventIdDic)
		{
			QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(item.Key);
			if (questData != null && (item.Value < 1 || MonoBehaviourSingleton<QuestManager>.I.IsEventPlayableWith((int)item.Value, NetworkNative.getNativeVersionFromName())))
			{
				list.Add(questData);
			}
		}
		return list;
	}

	private unsafe List<DeliveryDataAndUId> CreateDeliveryList(uint mapId)
	{
		Delivery[] deliveryList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(false);
		List<DeliveryDataAndUId> list = new List<DeliveryDataAndUId>();
		if (deliveryList == null)
		{
			return null;
		}
		int i = 0;
		for (int num = deliveryList.Length; i < num; i++)
		{
			Delivery delivery = deliveryList[i];
			int dId = delivery.dId;
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)dId);
			_003CCreateDeliveryList_003Ec__AnonStorey487 _003CCreateDeliveryList_003Ec__AnonStorey;
			if (IsExistTargetEnemy(deliveryTableData, mapId) && (!deliveryTableData.IsEvent() || !MonoBehaviourSingleton<QuestManager>.I.bingoEventList.Any(new Func<Network.EventData, bool>((object)_003CCreateDeliveryList_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/))))
			{
				list.Add(new DeliveryDataAndUId(deliveryTableData, delivery.uId));
			}
		}
		return list;
	}

	private bool IsExistTargetEnemy(DeliveryTable.DeliveryData deliveryData, uint mapId)
	{
		if (deliveryData == null)
		{
			return false;
		}
		bool flag = false;
		List<uint> mapIdList = deliveryData.GetMapIdList();
		if (mapIdList != null)
		{
			flag = true;
			if (mapIdList.Contains(mapId))
			{
				return true;
			}
		}
		if (flag)
		{
			return false;
		}
		List<uint> enemyIdList = deliveryData.GetEnemyIdList();
		if (enemyIdList == null)
		{
			return false;
		}
		int i = 0;
		for (int count = enemyDataList.Count; i < count; i++)
		{
			List<uint> list = enemyIdList;
			EnemyDataForDisplay enemyDataForDisplay = enemyDataList[i];
			if (list.Contains(enemyDataForDisplay.data.id))
			{
				return true;
			}
		}
		return false;
	}

	private List<EnemyDataForDisplay> CreateEnemyDataList(uint mapId)
	{
		List<FieldMapTable.EnemyPopTableData> enemyPopList = Singleton<FieldMapTable>.I.GetEnemyPopList(mapId);
		List<EnemyDataForDisplay> list = new List<EnemyDataForDisplay>(enemyPopList.Count);
		Dictionary<uint, HashSet<uint>> dictionary = new Dictionary<uint, HashSet<uint>>(enemyPopList.Count);
		int i = 0;
		for (int count = enemyPopList.Count; i < count; i++)
		{
			if (enemyPopList[i].enemyPopType == ENEMY_POP_TYPE.NONE)
			{
				EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData(enemyPopList[i].enemyID);
				uint num = enemyPopList[i].enemyLv;
				if (num == 0)
				{
					int num2 = enemyData.level;
					num = (uint)num2;
				}
				if (dictionary.TryGetValue(enemyData.id, out HashSet<uint> value))
				{
					if (!value.Add(num))
					{
						continue;
					}
				}
				else
				{
					dictionary[enemyData.id] = new HashSet<uint>();
					dictionary[enemyData.id].Add(num);
				}
				list.Add(new EnemyDataForDisplay(enemyData, num));
			}
		}
		return list;
	}

	private void SetupDeliveryListItem(Transform t, DeliveryDataAndUId deliveryDataAndUId)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		RegionMapDescriptionDeliveryItem regionMapDescriptionDeliveryItem = t.GetComponent<RegionMapDescriptionDeliveryItem>();
		if (regionMapDescriptionDeliveryItem == null)
		{
			regionMapDescriptionDeliveryItem = t.get_gameObject().AddComponent<RegionMapDescriptionDeliveryItem>();
		}
		regionMapDescriptionDeliveryItem.InitUI();
		regionMapDescriptionDeliveryItem.Setup(t, deliveryDataAndUId.data);
		SetEvent(t, "SELECT_DELIVERY", deliveryDataAndUId);
	}

	public void OnQuery_SELECT_DELIVERY()
	{
		DeliveryDataAndUId dataAndUId = (DeliveryDataAndUId)GameSection.GetEventData();
		DeliveryTable.DeliveryData data = dataAndUId.data;
		int id = (int)dataAndUId.data.id;
		bool is_enough_material = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(id);
		if (!is_enough_material)
		{
			GameSection.SetEventData(new object[4]
			{
				id,
				null,
				false,
				mapData
			});
		}
		else
		{
			bool flag = FieldManager.IsValidInGame();
			bool flag2 = data.clearEventID != 0;
			if (flag)
			{
				if (data.IsInvalidClearIngame() || flag2)
				{
					GameSection.ChangeEvent("DELIVERY_ITEM_COMPLETE", null);
				}
				else
				{
					GameSection.StayEvent();
					MonoBehaviourSingleton<CoopManager>.I.coopStage.fieldRewardPool.SendFieldDrop(delegate(bool b)
					{
						if (b)
						{
							SendDeliveryComplete(data, dataAndUId.uId, is_enough_material);
						}
					});
				}
			}
			else
			{
				GameSection.StayEvent();
				SendDeliveryComplete(data, dataAndUId.uId, is_enough_material);
			}
		}
	}

	private unsafe void SendDeliveryComplete(DeliveryTable.DeliveryData deliveryData, string deliveryUniqueId, bool is_enough_material)
	{
		bool is_tutorial = !TutorialStep.HasFirstDeliveryCompleted();
		int delivery_id = (int)deliveryData.id;
		bool enable_clear_event = deliveryData.clearEventID != 0;
		_003CSendDeliveryComplete_003Ec__AnonStorey489 _003CSendDeliveryComplete_003Ec__AnonStorey;
		MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(deliveryUniqueId, enable_clear_event, new Action<bool, DeliveryRewardList>((object)_003CSendDeliveryComplete_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void SetupHappenListItem(Transform t, QuestTable.QuestTableData happenData)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		RegionMapDescriptionHappenItem regionMapDescriptionHappenItem = t.GetComponent<RegionMapDescriptionHappenItem>();
		if (regionMapDescriptionHappenItem == null)
		{
			regionMapDescriptionHappenItem = t.get_gameObject().AddComponent<RegionMapDescriptionHappenItem>();
		}
		regionMapDescriptionHappenItem.InitUI();
		regionMapDescriptionHappenItem.SetUp(happenData);
		SetEvent(t, "SELECT_HAPPEN", new object[4]
		{
			(int)happenData.questID,
			string.Empty,
			false,
			mapData
		});
	}

	private void SetupEnemyListItem(Transform t, EnemyDataForDisplay enemyData)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		RegionMapDescriptionEnemyItem regionMapDescriptionEnemyItem = t.GetComponent<RegionMapDescriptionEnemyItem>();
		if (regionMapDescriptionEnemyItem == null)
		{
			regionMapDescriptionEnemyItem = t.get_gameObject().AddComponent<RegionMapDescriptionEnemyItem>();
		}
		regionMapDescriptionEnemyItem.InitUI();
		regionMapDescriptionEnemyItem.SetUpEnemyOnly(enemyData.data, enemyData.level);
		SetEvent(t, "SELECT_HAPPEN", new object[2]
		{
			(int)enemyData.data.id,
			string.Empty
		});
	}

	private void ClearTable()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(UI.TBL_ALL);
		if (Object.op_Implicit(ctrl))
		{
			int i = 0;
			for (int childCount = ctrl.get_childCount(); i < childCount; i++)
			{
				Transform val = ctrl.GetChild(0);
				val.set_parent(null);
				Object.Destroy(val.get_gameObject());
			}
		}
	}

	public void OnQuery_TO_FIELD()
	{
		GameSection.StopEvent();
		EventData[] autoEvents = new EventData[2]
		{
			new EventData("TO_REGION_MAP", null),
			new EventData("TO_FIELD_OR_HOME", mapData)
		};
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	public void OnQuery_TO_REGION_MAP()
	{
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(mapData.mapId);
		GameSection.SetEventData(fieldMapData.regionId);
	}

	private void OnScreenRotate(bool isPortrait)
	{
		Reposition();
	}

	private void Reposition()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		UIScreenRotationHandler[] componentsInChildren = base._transform.GetComponentsInChildren<UIScreenRotationHandler>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].InvokeRotate();
		}
		GetCtrl(UI.SPR_BG_FRAME).GetComponent<UIRect>().UpdateAnchors();
		UpdateAnchors();
		if (GetCtrl(UI.SCR_ALL).get_gameObject().get_activeInHierarchy())
		{
			ScrollViewResetPosition((Enum)UI.SCR_ALL);
		}
	}

	public override void Exit()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && FieldManager.IsValidInGame())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
		base.Exit();
	}

	private void OnQuery_InGameQuestAcceptDeliveryItemComplete_YES()
	{
		BackHome();
	}

	private void BackHome()
	{
		if (FieldManager.IsValidInGame() && MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
		}
	}
}
