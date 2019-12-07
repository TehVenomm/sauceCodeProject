using Network;
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
		TEX_FIELD,
		OBJ_BACK
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

	private const float ANCHOR_LEFT = 0f;

	private const float ANCHOR_CENTER = 0.5f;

	private const float ANCHOR_RIGHT = 1f;

	private const float ANCHOR_BOT = 0f;

	private const float ANCHOR_TOP = 1f;

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
		SetActive(UI.BTN_TO_FIELD, is_visible);
		base.UpdateUI();
	}

	protected void UpdateTable()
	{
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(mapData.mapId);
		string mapName = fieldMapData.mapName;
		SetLabelText(UI.LBL_MAP_NAME, mapName);
		SetLabelText(UI.LBL_MAP_NAME_D, mapName);
		ResourceLoad.LoadFieldIconTexture(GetCtrl(UI.TEX_FIELD).GetComponent<UITexture>(), fieldMapData);
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
		SetActive(UI.LBL_NON_LIST, num2 <= 0);
		SetTable(UI.TBL_ALL, "", num2, reset: true, delegate(int i, Transform parent)
		{
			Transform transform = null;
			if (borderIndexTitleDic.ContainsKey(i))
			{
				return Realizes("RegionMapDescriptionBorderItem", parent);
			}
			if (i >= enemyStartIndex)
			{
				return Realizes("RegionMapDescriptionEnemyItem", parent);
			}
			if (i >= happenStartIndex)
			{
				return Realizes("RegionMapDescriptionHappenItem", parent);
			}
			return (i >= deliveryStartIndex) ? Realizes("RegionMapDescriptionDeliveryItem", parent) : transform;
		}, delegate(int i, Transform t, bool is_recycle)
		{
			string value = "";
			if (borderIndexTitleDic.TryGetValue(i, out value))
			{
				SetLabelText(t, UI.LBL_BORDER_TITLE, value);
				SetActive(t, is_visible: true);
			}
			else if (i >= enemyStartIndex && i - enemyStartIndex < enemyDataList.Count)
			{
				SetupEnemyListItem(t, enemyDataList[i - enemyStartIndex]);
				SetActive(t, is_visible: true);
			}
			else if (i >= happenStartIndex && i - happenStartIndex < happenDataList.Count)
			{
				SetupHappenListItem(t, happenDataList[i - happenStartIndex]);
				SetActive(t, is_visible: true);
			}
			else if (i >= deliveryStartIndex && i - deliveryStartIndex < deliveryDataAndUIdList.Count)
			{
				SetupDeliveryListItem(t, deliveryDataAndUIdList[i - deliveryStartIndex]);
				SetActive(t, is_visible: true);
			}
			else
			{
				SetActive(t, is_visible: true);
			}
		});
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

	private List<DeliveryDataAndUId> CreateDeliveryList(uint mapId)
	{
		Delivery[] deliveryList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(do_sort: false);
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
			if (IsExistTargetEnemy(deliveryTableData, mapId) && (!deliveryTableData.IsEvent() || !MonoBehaviourSingleton<QuestManager>.I.bingoEventList.Any((Network.EventData e) => e.eventId == deliveryTableData.eventID)))
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
			if (enemyIdList.Contains(enemyDataList[i].data.id))
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
			if (enemyPopList[i].enemyPopType != 0 && enemyPopList[i].enemyPopType != ENEMY_POP_TYPE.FIELD_BOSS)
			{
				continue;
			}
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData(enemyPopList[i].enemyID);
			uint num = enemyPopList[i].enemyLv;
			if (num == 0)
			{
				num = (uint)(int)enemyData.level;
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
		return list;
	}

	private void SetupDeliveryListItem(Transform t, DeliveryDataAndUId deliveryDataAndUId)
	{
		RegionMapDescriptionDeliveryItem regionMapDescriptionDeliveryItem = t.GetComponent<RegionMapDescriptionDeliveryItem>();
		if (regionMapDescriptionDeliveryItem == null)
		{
			regionMapDescriptionDeliveryItem = t.gameObject.AddComponent<RegionMapDescriptionDeliveryItem>();
		}
		regionMapDescriptionDeliveryItem.InitUI();
		regionMapDescriptionDeliveryItem.Setup(t, deliveryDataAndUId.data);
		SetEvent(t, "SELECT_DELIVERY", deliveryDataAndUId);
	}

	public void OnQuery_SELECT_DELIVERY()
	{
		DeliveryDataAndUId dataAndUId = (DeliveryDataAndUId)GameSection.GetEventData();
		DeliveryTable.DeliveryData data = dataAndUId.data;
		int deliveryId = (int)dataAndUId.data.id;
		bool is_enough_material = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(deliveryId);
		if (!is_enough_material)
		{
			GameSection.SetEventData(new object[4]
			{
				deliveryId,
				null,
				false,
				mapData
			});
			return;
		}
		bool num = FieldManager.IsValidInGame();
		_ = data.clearEventID;
		if (num)
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<CoopManager>.I.coopStage.fieldRewardPool.SendFieldDrop(delegate(bool b)
			{
				if (b)
				{
					SendDeliveryComplete(data, dataAndUId.uId, is_enough_material);
					if (Singleton<FieldMapTable>.I.GetDeliveryRelationPortalData((uint)deliveryId) != null)
					{
						MonoBehaviourSingleton<DeliveryManager>.I.CheckAnnouncePortalOpen();
					}
				}
			});
		}
		else
		{
			GameSection.StayEvent();
			SendDeliveryComplete(data, dataAndUId.uId, is_enough_material);
		}
	}

	private void SendDeliveryComplete(DeliveryTable.DeliveryData deliveryData, string deliveryUniqueId, bool is_enough_material)
	{
		bool is_tutorial = !TutorialStep.HasFirstDeliveryCompleted();
		int delivery_id = (int)deliveryData.id;
		bool enable_clear_event = deliveryData.clearEventID != 0;
		MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(deliveryUniqueId, enable_clear_event, delegate(bool is_success, DeliveryRewardList recv_reward)
		{
			if (is_success)
			{
				List<FieldMapTable.PortalTableData> deliveryRelationPortalData = Singleton<FieldMapTable>.I.GetDeliveryRelationPortalData((uint)delivery_id);
				for (int i = 0; i < deliveryRelationPortalData.Count; i++)
				{
					GameSaveData.instance.newReleasePortals.Add(deliveryRelationPortalData[i].portalID);
				}
				if (is_tutorial)
				{
					TutorialStep.isSendFirstRewardComplete = true;
				}
				if (!enable_clear_event)
				{
					MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
					GameSection.ChangeStayEvent("DELIVERY_REWARD", new object[4]
					{
						delivery_id,
						recv_reward,
						false,
						mapData
					});
				}
				else
				{
					GameSection.ChangeStayEvent("CLEAR_EVENT", new object[3]
					{
						(int)deliveryData.clearEventID,
						delivery_id,
						recv_reward
					});
					if (FieldManager.IsValidInGame())
					{
						is_success = false;
						List<int> list = new List<int>(MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtInGame);
						for (int j = 0; j < list.Count; j++)
						{
							int item = list[j];
							if (!MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtHomeScene.Contains(item))
							{
								MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtHomeScene.Add(item);
							}
						}
						EventData[] requestEventData = new EventData[2]
						{
							new EventData("STORY_DELIVERY_REWARD", new object[2]
							{
								delivery_id,
								recv_reward
							}),
							new EventData("PORTAL_RELEASE", GameSaveData.instance.newReleasePortals)
						};
						GameSaveData.instance.newReleasePortals = new List<uint>();
						MonoBehaviourSingleton<InGameProgress>.I.FieldReadStory((int)deliveryData.clearEventID, isSend: true, requestEventData);
						MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtInGame.Clear();
					}
				}
				deliveryDataAndUIdList = CreateDeliveryList(mapData.mapId);
			}
			GameSection.ResumeEvent(is_success);
		});
	}

	private void SetupHappenListItem(Transform t, QuestTable.QuestTableData happenData)
	{
		RegionMapDescriptionHappenItem regionMapDescriptionHappenItem = t.GetComponent<RegionMapDescriptionHappenItem>();
		if (regionMapDescriptionHappenItem == null)
		{
			regionMapDescriptionHappenItem = t.gameObject.AddComponent<RegionMapDescriptionHappenItem>();
		}
		regionMapDescriptionHappenItem.InitUI();
		regionMapDescriptionHappenItem.SetUp(happenData);
		SetEvent(t, "SELECT_HAPPEN", new object[4]
		{
			(int)happenData.questID,
			"",
			false,
			mapData
		});
	}

	private void SetupEnemyListItem(Transform t, EnemyDataForDisplay enemyData)
	{
		RegionMapDescriptionEnemyItem regionMapDescriptionEnemyItem = t.GetComponent<RegionMapDescriptionEnemyItem>();
		if (regionMapDescriptionEnemyItem == null)
		{
			regionMapDescriptionEnemyItem = t.gameObject.AddComponent<RegionMapDescriptionEnemyItem>();
		}
		regionMapDescriptionEnemyItem.InitUI();
		regionMapDescriptionEnemyItem.SetUpEnemyOnly(enemyData.data, enemyData.level);
		SetEvent(t, "SELECT_HAPPEN", new object[2]
		{
			(int)enemyData.data.id,
			""
		});
	}

	private void ClearTable()
	{
		Transform ctrl = GetCtrl(UI.TBL_ALL);
		if ((bool)ctrl)
		{
			int i = 0;
			for (int childCount = ctrl.childCount; i < childCount; i++)
			{
				Transform child = ctrl.GetChild(0);
				child.parent = null;
				Object.Destroy(child.gameObject);
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
		GameSection.SetEventData(Singleton<FieldMapTable>.I.GetFieldMapData(mapData.mapId).regionId);
	}

	private void OnScreenRotate(bool isPortrait)
	{
		Reposition();
	}

	private void Reposition()
	{
		UIScreenRotationHandler[] componentsInChildren = base._transform.GetComponentsInChildren<UIScreenRotationHandler>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].InvokeRotate();
		}
		if (SpecialDeviceManager.HasSpecialDeviceInfo)
		{
			DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
			if (specialDeviceInfo.NeedModifyRegionMapDescriptionList)
			{
				UIWidget component = GetCtrl(UI.OBJ_BACK).GetComponent<UIWidget>();
				UISprite component2 = GetCtrl(UI.BTN_TO_FIELD).GetComponent<UISprite>();
				if (SpecialDeviceManager.IsPortrait)
				{
					component.leftAnchor.Set(0f, specialDeviceInfo.RegionMapDescriptionListBACKAnchorPortrait.left);
					component.rightAnchor.Set(0.5f, specialDeviceInfo.RegionMapDescriptionListBACKAnchorPortrait.right);
					component.bottomAnchor.Set(0f, specialDeviceInfo.RegionMapDescriptionListBACKAnchorPortrait.bottom);
					component.topAnchor.Set(0f, specialDeviceInfo.RegionMapDescriptionListBACKAnchorPortrait.top);
					component.UpdateAnchors();
					component2.leftAnchor.Set(0.5f, specialDeviceInfo.RegionMapDescriptionListBTNTOFIELDAnchorPortrait.left);
					component2.rightAnchor.Set(0.5f, specialDeviceInfo.RegionMapDescriptionListBTNTOFIELDAnchorPortrait.right);
					component2.bottomAnchor.Set(0f, specialDeviceInfo.RegionMapDescriptionListBTNTOFIELDAnchorPortrait.bottom);
					component2.topAnchor.Set(0f, specialDeviceInfo.RegionMapDescriptionListBTNTOFIELDAnchorPortrait.top);
					component2.UpdateAnchors();
				}
				else
				{
					component.leftAnchor.Set(0f, specialDeviceInfo.RegionMapDescriptionListBACKAnchorLandscape.left);
					component.rightAnchor.Set(0.5f, specialDeviceInfo.RegionMapDescriptionListBACKAnchorLandscape.right);
					component.bottomAnchor.Set(0f, specialDeviceInfo.RegionMapDescriptionListBACKAnchorLandscape.bottom);
					component.topAnchor.Set(0f, specialDeviceInfo.RegionMapDescriptionListBACKAnchorLandscape.top);
					component.UpdateAnchors();
					component2.leftAnchor.Set(0.5f, specialDeviceInfo.RegionMapDescriptionListBTNTOFIELDAnchorLandscape.left);
					component2.rightAnchor.Set(0.5f, specialDeviceInfo.RegionMapDescriptionListBTNTOFIELDAnchorLandscape.right);
					component2.bottomAnchor.Set(0f, specialDeviceInfo.RegionMapDescriptionListBTNTOFIELDAnchorLandscape.bottom);
					component2.topAnchor.Set(0f, specialDeviceInfo.RegionMapDescriptionListBTNTOFIELDAnchorLandscape.top);
					component2.UpdateAnchors();
				}
			}
		}
		GetCtrl(UI.SPR_BG_FRAME).GetComponent<UIRect>().UpdateAnchors();
		UpdateAnchors();
		if (GetCtrl(UI.SCR_ALL).gameObject.activeInHierarchy)
		{
			ScrollViewResetPosition(UI.SCR_ALL);
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
