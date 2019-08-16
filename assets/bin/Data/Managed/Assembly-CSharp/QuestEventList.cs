using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class QuestEventList : GameSection
{
	protected enum UI
	{
		OBJ_QUEST_LIST_ROOT,
		OBJ_CARNIVAL_LIST_ROOT,
		SCR_EVENT_QUEST,
		GRD_EVENT_QUEST,
		OBJ_EVENT_ROOT,
		OBJ_TWO_TAB,
		OBJ_LEFT_ON,
		OBJ_RIGHT_ON,
		SPR_EVENT_NEW,
		SPR_PRESENT_NEW,
		SPR_CARNIVAL_NEW,
		OBJ_THREE_TAB,
		SPR_THREE_EVENT_NEW,
		SPR_THREE_PRESENT_NEW,
		OBJ_THREE_LEFT_ON,
		OBJ_THREE_CENTER_ON,
		OBJ_THREE_RIGHT_ON,
		STR_EVENT_NON_LIST,
		BTN_EVENT,
		OBJ_FRAME,
		SPR_BG_FRAME,
		TEX_EVENT_BANNER,
		LBL_RIGHT_VALUE,
		SPR_BANNER_DISABLE,
		SPR_ICON_EXISTPRESENT,
		SPR_ICON_NEW,
		SPR_ICON_NEWDELIVERY,
		SPR_ICON_NEWSTORY,
		SPR_ICON_ADDDIFFICULT,
		SPR_ICON_ADDDIFFICULT_RUSH,
		SPR_ICON_CLEARED,
		SPR_ICON_EVENTREWARD,
		SPR_ICON_RESULTANNOUNCEMENT,
		SPR_ICON_BOSSAPPEAR,
		LBL_TITLE,
		LBL_STATE,
		TEX_CARNIVAL_BG,
		TBL_CARNIVAL_QUEST,
		OBJ_LEFT_SMALL_BOX,
		OBJ_RIGHT_SMALL_BOX,
		SCR_CARNIVAL_QUEST,
		LBL_CURRENT_POINT,
		LBL_CURRENT_RANK,
		LBL_NEXT_POINT,
		LBL_CARNIVAL_STATUS,
		OBJ_CARNIVAL_STATUS,
		SPR_CARNIVAL_END,
		OBJ_RANK_ROOT,
		SPR_RANK_0,
		SPR_RANK_1,
		SPR_RANK_2,
		SPR_RANK_3,
		SPR_RANK_4,
		SPR_RANK_5
	}

	private enum eDispTab
	{
		Event,
		Present,
		Carnival,
		Max
	}

	private enum CARNIVAL_STATUS
	{
		NONE,
		DOING,
		TOTALING,
		END
	}

	private readonly int kBannerId_Explore = 10000003;

	private readonly int kBannerId_Rush = 10000004;

	private readonly int kBannerId_Wave = 10000005;

	private readonly int kRedRestTime = 345600;

	private readonly string kBackHomeEvent = "SELECT_BACKHOME";

	private readonly string kToAreaEvent = "TO_AREA_EVENT";

	private readonly float UPDATE_INTERVAL_SEC = 30f;

	private readonly float kItemLeftPosX = -113f;

	private readonly float kItemRightPosX = 113f;

	private readonly float kItemNormalHeight = 110f;

	private readonly float kItemBBoxHeight = 278f;

	private readonly float kItemSBoxHeight = 140f;

	private List<EventListData> eventList;

	private Dictionary<int, LoadObject> bannerTable;

	protected bool isInGame;

	protected bool firstUpdate = true;

	protected bool[] hasNewEvent = new bool[3];

	private eDispTab dispTab;

	protected UI eventNewSprite;

	protected UI presentNewSprite;

	protected UI eventOn;

	protected UI presentOn;

	private QuestCarnivalPointModel.Param currentCarnivalData;

	private CARNIVAL_STATUS carnivalStatus;

	private readonly string[] SPR_RANKING_NUMBER = new string[10]
	{
		"RankingNumber_0",
		"RankingNumber_1",
		"RankingNumber_2",
		"RankingNumber_3",
		"RankingNumber_4",
		"RankingNumber_5",
		"RankingNumber_6",
		"RankingNumber_7",
		"RankingNumber_8",
		"RankingNumber_9"
	};

	protected UI[] rankNumbers = new UI[6]
	{
		UI.SPR_RANK_0,
		UI.SPR_RANK_1,
		UI.SPR_RANK_2,
		UI.SPR_RANK_3,
		UI.SPR_RANK_4,
		UI.SPR_RANK_5
	};

	private Coroutine checkUpdate;

	private bool isCarnival;

	public override void Initialize()
	{
		this.StartCoroutine("DoInitialize");
	}

	private IEnumerator DoInitialize()
	{
		bool is_recv_delivery = false;
		MonoBehaviourSingleton<QuestManager>.I.SendGetEventList(delegate
		{
			is_recv_delivery = true;
		});
		while (!is_recv_delivery)
		{
			yield return null;
		}
		is_recv_delivery = false;
		MonoBehaviourSingleton<DeliveryManager>.I.SendEventList(delegate
		{
			is_recv_delivery = true;
		});
		while (!is_recv_delivery)
		{
			yield return null;
		}
		UpdateEventListData();
		isCarnival = IsCarnival();
		UpdateTabUISettings();
		LoadingQueue loadingQueue = new LoadingQueue(this);
		bannerTable = new Dictionary<int, LoadObject>(eventList.Count);
		for (int i = 0; i < eventList.Count; i++)
		{
			EventListData e = eventList[i];
			int bannerId = GetBannerId(e);
			if (!bannerTable.ContainsKey(bannerId))
			{
				LoadObject value = loadingQueue.Load(isEventAsset: true, RESOURCE_CATEGORY.EVENT_ICON, GetBannerName(e, bannerId));
				bannerTable.Add(bannerId, value);
			}
		}
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		if (isCarnival)
		{
			yield return this.StartCoroutine(GetCurrentCarnivalStatus());
			int CARNIVAL_BG_ID = 10017100;
			if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.carnivalEventId > 0)
			{
				CARNIVAL_BG_ID = MonoBehaviourSingleton<QuestManager>.I.carnivalEventId;
			}
			string resourceName = ResourceName.GetEventBG(CARNIVAL_BG_ID);
			LoadObject eventBG = loadingQueue.Load(isEventAsset: true, RESOURCE_CATEGORY.EVENT_BG, resourceName);
			if (loadingQueue.IsLoading())
			{
				yield return loadingQueue.Wait();
			}
			SetTexture(texture: eventBG.loadedObject as Texture2D, texture_enum: UI.TEX_CARNIVAL_BG);
			SetCarnivalPointInfo();
		}
		base.Initialize();
		ChangeTab();
	}

	private void SetCarnivalPointInfo()
	{
		SetActive((Enum)UI.OBJ_CARNIVAL_STATUS, currentCarnivalData != null);
		if (currentCarnivalData == null)
		{
			return;
		}
		QuestCarnivalPointModel.Param param = currentCarnivalData;
		carnivalStatus = (CARNIVAL_STATUS)param.status;
		bool flag = false;
		string empty = string.Empty;
		string empty2 = string.Empty;
		string text = string.Empty;
		string text2 = string.Empty;
		bool flag2 = param.rank > 0;
		if (param.status == 1)
		{
			flag = flag2;
			if (flag2)
			{
				SetSpriteRank(param.rank);
			}
			else
			{
				text = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 42u);
			}
			empty = param.point.ToString("N0") + " pt";
			empty2 = param.pointForNextClass.ToString("N0") + " pt";
			SetActive((Enum)UI.SPR_CARNIVAL_END, is_visible: false);
		}
		else if (param.status == 2)
		{
			flag = false;
			empty = param.point.ToString("N0") + " pt";
			empty2 = "-------";
			text = "-------";
			text2 = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 38u);
			SetActive((Enum)UI.SPR_CARNIVAL_END, is_visible: true);
		}
		else
		{
			flag = flag2;
			if (flag2)
			{
				SetSpriteRank(param.rank);
			}
			else
			{
				text = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 42u);
			}
			empty = param.point.ToString("N0") + " pt";
			empty2 = "-------";
			text2 = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 39u);
			SetActive((Enum)UI.SPR_CARNIVAL_END, is_visible: true);
		}
		SetActive((Enum)UI.LBL_CURRENT_RANK, !flag);
		SetActive((Enum)UI.OBJ_RANK_ROOT, flag);
		SetLabelText((Enum)UI.LBL_CURRENT_POINT, empty);
		SetLabelText((Enum)UI.LBL_NEXT_POINT, empty2);
		SetLabelText((Enum)UI.LBL_CURRENT_RANK, text);
		SetLabelText((Enum)UI.LBL_CARNIVAL_STATUS, text2);
	}

	private void SetSpriteRank(int value)
	{
		value = Mathf.Clamp(value, 1, 999999);
		value = Mathf.Min(999999, value);
		string text = value.ToString();
		int length = text.Length;
		for (int i = 0; i < 6; i++)
		{
			int num = length - 1;
			if (i > num)
			{
				SetActive((Enum)rankNumbers[i], is_visible: false);
				continue;
			}
			SetActive((Enum)rankNumbers[i], is_visible: true);
			int num2 = int.Parse(text[i].ToString());
			SetSprite((Enum)rankNumbers[num - i], SPR_RANKING_NUMBER[num2]);
		}
	}

	private IEnumerator GetCurrentCarnivalStatus()
	{
		bool isRequest = true;
		Protocol.Send<QuestCarnivalPointModel.RequestSendForm, QuestCarnivalPointModel>(postData: new QuestCarnivalPointModel.RequestSendForm
		{
			eid = MonoBehaviourSingleton<QuestManager>.I.carnivalEventId
		}, url: QuestCarnivalPointModel.URL, callBack: (Action<QuestCarnivalPointModel>)delegate(QuestCarnivalPointModel result)
		{
			isRequest = false;
			currentCarnivalData = result.result;
		}, getParam: string.Empty);
		while (isRequest)
		{
			yield return null;
		}
	}

	private void UpdateTabUISettings()
	{
		eventNewSprite = UI.SPR_EVENT_NEW;
		presentNewSprite = UI.SPR_PRESENT_NEW;
		eventOn = UI.OBJ_LEFT_ON;
		presentOn = UI.OBJ_RIGHT_ON;
		if (isCarnival)
		{
			eventNewSprite = UI.SPR_THREE_EVENT_NEW;
			presentNewSprite = UI.SPR_THREE_PRESENT_NEW;
			eventOn = UI.OBJ_THREE_LEFT_ON;
			presentOn = UI.OBJ_THREE_CENTER_ON;
		}
	}

	private void UpdateEventListData()
	{
		if (MonoBehaviourSingleton<DeliveryManager>.I.isUpdateEventListData)
		{
			eventList = null;
			eventList = new List<EventListData>(MonoBehaviourSingleton<DeliveryManager>.I.eventListData);
			eventList.Sort(delegate(EventListData a, EventListData b)
			{
				if (a.place == b.place)
				{
					int num = (a.enableEvent && MonoBehaviourSingleton<DeliveryManager>.I.IsAllClearedEvent(a.eventId)) ? 1 : 0;
					int num2 = (b.enableEvent && MonoBehaviourSingleton<DeliveryManager>.I.IsAllClearedEvent(b.eventId)) ? 1 : 0;
					if (num != num2)
					{
						return num - num2;
					}
					return a.orderNo - b.orderNo;
				}
				return a.place - b.place;
			});
			MonoBehaviourSingleton<DeliveryManager>.I.isUpdateEventListData = false;
		}
	}

	private void OnApplicationPause(bool pause)
	{
		if (!pause)
		{
			RefreshUI();
		}
	}

	public override void UpdateUI()
	{
		UpdateEventListData();
		UpdateEventList();
		UpdateCheck();
	}

	protected void UpdateEventList()
	{
		bool is_visible = IsActive(UI.OBJ_QUEST_LIST_ROOT);
		bool is_visible2 = IsActive(UI.OBJ_CARNIVAL_LIST_ROOT);
		SetActive((Enum)UI.OBJ_QUEST_LIST_ROOT, is_visible: true);
		SetActive((Enum)UI.OBJ_CARNIVAL_LIST_ROOT, is_visible: true);
		_UpdateEvent();
		_UpdatePresent();
		if (isCarnival)
		{
			_UpdateCarnival();
		}
		if (firstUpdate)
		{
			firstUpdate = false;
			if (isCarnival && carnivalStatus == CARNIVAL_STATUS.DOING)
			{
				dispTab = eDispTab.Carnival;
			}
			else if (hasNewEvent[1])
			{
				dispTab = eDispTab.Present;
			}
			ChangeTab();
			DispEventNonListLabel();
		}
		else
		{
			SetActive((Enum)UI.OBJ_QUEST_LIST_ROOT, is_visible);
			SetActive((Enum)UI.OBJ_CARNIVAL_LIST_ROOT, is_visible2);
		}
	}

	private void _UpdateEvent()
	{
		if (IsActive(UI.OBJ_QUEST_LIST_ROOT))
		{
			hasNewEvent[0] = false;
			EVENT_DISPLAY_PLACE lastPlace = EVENT_DISPLAY_PLACE.NONE;
			float offsetY = 0f;
			SetSimpleContent(UI.OBJ_EVENT_ROOT, string.Empty, eventList.Count, reset: false, delegate(int i)
			{
				EventListData eventListData3 = eventList[i];
				return eventListData3.placeEnum != EVENT_DISPLAY_PLACE.PRESENT && eventListData3.placeEnum != EVENT_DISPLAY_PLACE.NONE;
			}, delegate(int i, Transform parent)
			{
				EventListData eventListData2 = eventList[i];
				switch (eventListData2.placeEnum)
				{
				case EVENT_DISPLAY_PLACE.NONE:
				case EVENT_DISPLAY_PLACE.PRESENT:
				case EVENT_DISPLAY_PLACE.CARNIVAL_NORMAL:
				case EVENT_DISPLAY_PLACE.CARNIVAL_SMALL_BOX:
				case EVENT_DISPLAY_PLACE.SERIES_ARENA:
					return null;
				case EVENT_DISPLAY_PLACE.LEFT_EVENT:
				case EVENT_DISPLAY_PLACE.RIGHT_EVENT:
					return Realizes("QuestEventListItemBBox", parent);
				case EVENT_DISPLAY_PLACE.LEFT_CONTENTS:
				case EVENT_DISPLAY_PLACE.RIGHT_CONTENTS:
					return Realizes("QuestEventListItemSBox", parent);
				default:
					return Realizes("QuestEventListItemNormal", parent);
				}
			}, delegate(int i, Transform t, bool is_recycle)
			{
				//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
				//IL_0102: Unknown result type (might be due to invalid IL or missing references)
				//IL_023c: Unknown result type (might be due to invalid IL or missing references)
				if (!(t == null))
				{
					EventListData eventListData = eventList[i];
					_SetupItem(eDispTab.Event, t, eventListData);
					if (lastPlace == EVENT_DISPLAY_PLACE.LEFT_EVENT && eventListData.placeEnum != EVENT_DISPLAY_PLACE.RIGHT_EVENT)
					{
						if (offsetY == 0f)
						{
							offsetY -= kItemBBoxHeight * 0.5f;
						}
						else
						{
							offsetY -= kItemBBoxHeight;
						}
					}
					else if (lastPlace == EVENT_DISPLAY_PLACE.LEFT_CONTENTS && eventListData.placeEnum != EVENT_DISPLAY_PLACE.RIGHT_CONTENTS)
					{
						if (offsetY == 0f)
						{
							offsetY -= kItemSBoxHeight * 0.5f;
						}
						else
						{
							offsetY -= kItemSBoxHeight;
						}
					}
					Vector3 localPosition = t.get_localPosition();
					if (eventListData.placeEnum == EVENT_DISPLAY_PLACE.LEFT_EVENT || eventListData.placeEnum == EVENT_DISPLAY_PLACE.LEFT_CONTENTS)
					{
						localPosition.x = kItemLeftPosX;
					}
					else if (eventListData.placeEnum == EVENT_DISPLAY_PLACE.RIGHT_EVENT || eventListData.placeEnum == EVENT_DISPLAY_PLACE.RIGHT_CONTENTS)
					{
						localPosition.x = kItemRightPosX;
					}
					else
					{
						localPosition.x = 0f;
					}
					float num;
					float num2;
					switch (eventListData.placeEnum)
					{
					case EVENT_DISPLAY_PLACE.LEFT_EVENT:
					case EVENT_DISPLAY_PLACE.RIGHT_EVENT:
						num = kItemBBoxHeight;
						num2 = kItemBBoxHeight * 0.5f;
						break;
					case EVENT_DISPLAY_PLACE.LEFT_CONTENTS:
					case EVENT_DISPLAY_PLACE.RIGHT_CONTENTS:
						num = kItemSBoxHeight;
						num2 = kItemSBoxHeight * 0.5f;
						break;
					default:
						num = kItemNormalHeight;
						num2 = kItemNormalHeight * 0.5f;
						break;
					}
					if (offsetY == 0f)
					{
						localPosition.y = 0f;
					}
					else
					{
						localPosition.y = offsetY - num2;
					}
					t.set_localPosition(localPosition);
					switch (eventListData.placeEnum)
					{
					default:
						if (offsetY == 0f)
						{
							offsetY -= num2;
						}
						else
						{
							offsetY -= num;
						}
						break;
					case EVENT_DISPLAY_PLACE.LEFT_EVENT:
					case EVENT_DISPLAY_PLACE.LEFT_CONTENTS:
						break;
					}
					lastPlace = eventListData.placeEnum;
				}
			});
			SetActive((Enum)eventNewSprite, hasNewEvent[0]);
		}
	}

	private void _UpdatePresent()
	{
		hasNewEvent[1] = false;
		SetDynamicList((Enum)UI.GRD_EVENT_QUEST, "QuestEventListItemNormal", eventList.Count, reset: false, (Func<int, bool>)delegate(int i)
		{
			EventListData eventListData = eventList[i];
			return eventListData.placeEnum == EVENT_DISPLAY_PLACE.PRESENT;
		}, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
		{
			if (!(t == null))
			{
				_SetupItem(eDispTab.Present, t, eventList[i]);
			}
		});
		SetActive((Enum)presentNewSprite, hasNewEvent[1]);
	}

	private void _UpdateCarnival()
	{
		hasNewEvent[2] = false;
		Transform ctrl = GetCtrl(UI.TBL_CARNIVAL_QUEST);
		if (Object.op_Implicit(ctrl))
		{
			int j = 0;
			for (int childCount = ctrl.get_childCount(); j < childCount; j++)
			{
				Transform child = ctrl.GetChild(0);
				child.set_parent(null);
				Object.Destroy(child.get_gameObject());
			}
		}
		List<EventListData> carnivalEvents = new List<EventListData>();
		for (int k = 0; k < eventList.Count; k++)
		{
			if (IsCarnivalEvent(eventList[k]))
			{
				carnivalEvents.Add(eventList[k]);
			}
		}
		int itemNum = carnivalEvents.Count;
		itemNum++;
		itemNum++;
		bool existSmallBoxEmpty = false;
		bool isLeftSmallBox = true;
		Transform targetPairBox = null;
		if (carnivalStatus != CARNIVAL_STATUS.DOING)
		{
			itemNum = 1;
		}
		SetTable(UI.TBL_CARNIVAL_QUEST, string.Empty, itemNum, reset: false, delegate(int i, Transform parent)
		{
			Transform result = null;
			if (i == 0)
			{
				result = Realizes("CarnivalRankingCheckItem", parent);
			}
			else if (i == itemNum - 1)
			{
				result = Realizes("NormalQuestItem", parent);
			}
			else
			{
				switch (carnivalEvents[i - 1].placeEnum)
				{
				case EVENT_DISPLAY_PLACE.CARNIVAL_NORMAL:
					result = Realizes("QuestEventListItemNormal", parent);
					break;
				case EVENT_DISPLAY_PLACE.CARNIVAL_SMALL_BOX:
					result = (existSmallBoxEmpty ? targetPairBox : (targetPairBox = Realizes("QuestEventListItemPairSmallBox", parent)));
					existSmallBoxEmpty = !existSmallBoxEmpty;
					break;
				}
			}
			return result;
		}, delegate(int i, Transform t, bool is_recycle)
		{
			if (!(t == null))
			{
				SetActive(t, is_visible: true);
				if (i == 0)
				{
					InitRankingCheckButton(t);
				}
				else if (i == itemNum - 1)
				{
					InitGachaQuestButton(t);
				}
				else
				{
					EventListData eventListData = carnivalEvents[i - 1];
					switch (eventListData.placeEnum)
					{
					case EVENT_DISPLAY_PLACE.CARNIVAL_NORMAL:
						_SetupItem(eDispTab.Carnival, t, eventListData);
						break;
					case EVENT_DISPLAY_PLACE.CARNIVAL_SMALL_BOX:
					{
						Transform val = null;
						if (isLeftSmallBox)
						{
							SetActive(t, UI.OBJ_RIGHT_SMALL_BOX, is_visible: false);
							val = FindCtrl(t, UI.OBJ_LEFT_SMALL_BOX);
						}
						else
						{
							SetActive(t, UI.OBJ_RIGHT_SMALL_BOX, is_visible: true);
							val = FindCtrl(t, UI.OBJ_RIGHT_SMALL_BOX);
						}
						_SetupItem(eDispTab.Carnival, val, eventListData);
						isLeftSmallBox = !isLeftSmallBox;
						break;
					}
					}
				}
			}
		});
		UIScrollView component = base.GetComponent<UIScrollView>((Enum)UI.SCR_CARNIVAL_QUEST);
		component.set_enabled(true);
		component.ResetPosition();
		RepositionCarnivalTable();
		SetActive((Enum)UI.SPR_CARNIVAL_NEW, hasNewEvent[2]);
	}

	private void RepositionCarnivalTable()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		UITable component = base.GetComponent<UITable>((Enum)UI.TBL_CARNIVAL_QUEST);
		if (Object.op_Implicit(component))
		{
			component.Reposition();
			List<Transform> childList = component.GetChildList();
			int i = 0;
			for (int count = childList.Count; i < count; i++)
			{
				Vector3 localPosition = childList[i].get_localPosition();
				localPosition.x = 0f;
				childList[i].set_localPosition(localPosition);
			}
		}
	}

	private void InitRankingCheckButton(Transform parent)
	{
		SetEvent(parent, "TO_CARNIVAL_RANKING", null);
	}

	private void InitGachaQuestButton(Transform parent)
	{
		SetEvent(parent, "TO_GACHA_QUEST_COUNTER", null);
	}

	private void _SetupItem(eDispTab tab, Transform t, EventListData e)
	{
		int bannerId = GetBannerId(e);
		if (bannerTable.TryGetValue(bannerId, out LoadObject value))
		{
			Texture2D val = value.loadedObject as Texture2D;
			if (val != null)
			{
				Transform t2 = FindCtrl(t, UI.TEX_EVENT_BANNER);
				SetTexture(t2, val);
				SetActive(t2, is_visible: true);
			}
		}
		if (!e.enableEvent && e.eventTypeEnum != EVENT_TYPE.ARENA)
		{
			SetEvent(t, "SELECT_DISABLE", e);
		}
		else if (e.linkName.Equals(kToAreaEvent))
		{
			SetEvent(t, kToAreaEvent, e);
		}
		else
		{
			switch (e.eventTypeEnum)
			{
			default:
				SetEvent(t, ChangeEventTypeToSelectEventName(e.eventTypeEnum), ChoiceEventData(e));
				break;
			case EVENT_TYPE.EXPLORE:
			case EVENT_TYPE.RUSH:
			case EVENT_TYPE.WAVE:
			case EVENT_TYPE.TRIAL:
				SetEvent(t, (!isInGame) ? ChangeEventTypeToSelectEventName(e.eventTypeEnum) : kBackHomeEvent, ChoiceEventData(e));
				break;
			case EVENT_TYPE.ARENA:
				SetArenaItem(t, e);
				break;
			case EVENT_TYPE.SERIES_ARENA_POINT_CLEAR:
				SetSeriesArenaItem(t, e);
				break;
			}
		}
		if (e.leftBadgeEnum == BADGE_1_CATEGORY.NEW_EVENT)
		{
			hasNewEvent[(int)tab] = true;
		}
		SetActive(t, UI.SPR_ICON_NEW, e.leftBadgeEnum == BADGE_1_CATEGORY.NEW_EVENT);
		SetActive(t, UI.SPR_ICON_NEWDELIVERY, e.leftBadgeEnum == BADGE_1_CATEGORY.NEW_DELIVERY);
		SetActive(t, UI.SPR_ICON_NEWSTORY, e.leftBadgeEnum == BADGE_1_CATEGORY.NEW_STORY);
		SetActive(t, UI.SPR_ICON_CLEARED, e.leftBadgeEnum == BADGE_1_CATEGORY.CLEARED);
		if (e.eventTypeEnum == EVENT_TYPE.EXPLORE || e.eventTypeEnum == EVENT_TYPE.WAVE)
		{
			if (IsDispTitle(e))
			{
				SetActive(t, UI.LBL_TITLE, is_visible: true);
				SetLabelText(t, UI.LBL_TITLE, e.name);
			}
			else
			{
				SetActive(t, UI.LBL_TITLE, is_visible: false);
			}
			SetActive(t, UI.SPR_ICON_ADDDIFFICULT, e.leftBadgeEnum == BADGE_1_CATEGORY.NEW_DIFFICULT_DELIVERY);
			SetActive(t, UI.SPR_ICON_ADDDIFFICULT_RUSH, is_visible: false);
		}
		else if (e.eventTypeEnum == EVENT_TYPE.RUSH)
		{
			if (IsDispTitle(e))
			{
				SetActive(t, UI.LBL_TITLE, is_visible: true);
				SetLabelText(t, UI.LBL_TITLE, e.name);
			}
			else
			{
				SetActive(t, UI.LBL_TITLE, is_visible: false);
			}
			SetActive(t, UI.SPR_ICON_ADDDIFFICULT, is_visible: false);
			SetActive(t, UI.SPR_ICON_ADDDIFFICULT_RUSH, e.leftBadgeEnum == BADGE_1_CATEGORY.NEW_DIFFICULT_DELIVERY);
		}
		else
		{
			SetActive(t, UI.SPR_ICON_ADDDIFFICULT, e.leftBadgeEnum == BADGE_1_CATEGORY.NEW_DIFFICULT_DELIVERY);
		}
		SetActive(t, UI.SPR_ICON_EVENTREWARD, e.rightBadgeEnum == BADGE_2_CATEGORY.EVENT_REWARD);
		SetActive(t, UI.SPR_ICON_RESULTANNOUNCEMENT, e.rightBadgeEnum == BADGE_2_CATEGORY.RESULT_ANNOUNCEMENT);
		SetActive(t, UI.SPR_ICON_BOSSAPPEAR, e.rightBadgeEnum == BADGE_2_CATEGORY.BOSS_APPEAR);
		UIWigetCrossFade component = t.GetComponent<UIWigetCrossFade>();
		if (component != null)
		{
			if (e.leftBadgeEnum != 0 && (e.rightBadgeEnum == BADGE_2_CATEGORY.EVENT_REWARD || e.rightBadgeEnum == BADGE_2_CATEGORY.RESULT_ANNOUNCEMENT))
			{
				component.Play();
			}
			else
			{
				component.Reset();
			}
		}
		if (e.linkName.Equals(kToAreaEvent))
		{
			SetActive(t, UI.LBL_STATE, is_visible: false);
			SetActive(t, UI.SPR_BANNER_DISABLE, is_visible: false);
			SetActive(t, UI.LBL_RIGHT_VALUE, is_visible: false);
			SetActive(t, UI.SPR_ICON_EXISTPRESENT, is_visible: false);
			return;
		}
		if (e.HasEndDate() && e.GetRest() < 0)
		{
			if (e.rightBadgeEnum == BADGE_2_CATEGORY.NOW_AGGREGATING)
			{
				SetLabelText(t, UI.LBL_STATE, StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 38u));
			}
			else
			{
				SetLabelText(t, UI.LBL_STATE, StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 39u));
			}
			SetActive(t, UI.LBL_STATE, is_visible: true);
			SetActive(t, UI.SPR_BANNER_DISABLE, is_visible: true);
			SetActive(t, UI.LBL_RIGHT_VALUE, is_visible: false);
			SetActive(t, UI.SPR_ICON_EXISTPRESENT, is_visible: false);
			return;
		}
		SetActive(t, UI.SPR_BANNER_DISABLE, is_visible: false);
		StringBuilder stringBuilder = new StringBuilder();
		if (e.rightBadgeEnum == BADGE_2_CATEGORY.DELIVERY_NUM)
		{
			stringBuilder.Append("[000000]");
			stringBuilder.Append(e.rightValue);
			stringBuilder.Append(" / [-]");
		}
		else if (e.leftBadgeEnum == BADGE_1_CATEGORY.CLEARED)
		{
			stringBuilder.Append("[000000]");
			stringBuilder.Append(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 43u));
			stringBuilder.Append(" / [-]");
		}
		stringBuilder.Append((e.GetRest() >= kRedRestTime) ? "[000000]" : "[FF0000]");
		stringBuilder.Append(UIUtility.TimeFormatWithUnit(e.GetRest()));
		stringBuilder.Append(" " + StringTable.Get(STRING_CATEGORY.TIME, 4u));
		stringBuilder.Append("[-]");
		SetLabelText(t, UI.LBL_RIGHT_VALUE, stringBuilder.ToString());
		SetSupportEncoding(t, UI.LBL_RIGHT_VALUE, isEnable: true);
		SetActive(t, UI.LBL_RIGHT_VALUE, e.GetRest() > 0);
		if (e.eventTypeEnum == EVENT_TYPE.SERIES_ARENA_POINT_CLEAR)
		{
			SetActive(t, UI.SPR_ICON_EXISTPRESENT, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableSeriesArenaEventDeliveryNum() > 0);
		}
		else
		{
			SetActive(t, UI.SPR_ICON_EXISTPRESENT, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum(e.eventId) > 0);
		}
	}

	private void SetArenaItem(Transform t, EventListData e)
	{
		if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 50)
		{
			SetEvent(t, "SELECT_ARENA", ChoiceEventData(e));
			SetLabelText(t, UI.LBL_TITLE, e.name);
			SetActive(t, UI.LBL_TITLE, is_visible: true);
		}
		else
		{
			SetEvent(t, "SELECT_DISABLE_ARENA", ChoiceEventData(e));
			SetActive(t, UI.LBL_TITLE, is_visible: false);
		}
		if (isInGame)
		{
			SetEvent(t, kBackHomeEvent, e);
		}
	}

	private void OnQuery_SELECT_SERIES_ARENA()
	{
		_CheckEvent();
	}

	private void SetSeriesArenaItem(Transform t, EventListData e)
	{
		SetActive(t, UI.LBL_TITLE, is_visible: false);
		if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 100)
		{
			SetEvent(t, "SELECT_SERIES_ARENA", ChoiceEventData(e));
		}
		else
		{
			SetEvent(t, "SELECT_NOT_LEVEL_SERIES_ARENA", ChoiceEventData(e));
		}
		if (isInGame)
		{
			SetEvent(t, kBackHomeEvent, e);
		}
	}

	protected void UpdateCheck()
	{
		if (checkUpdate != null)
		{
			this.StopCoroutine(checkUpdate);
		}
		List<EventListData> list = eventList.FindAll((EventListData x) => x.HasEndDate());
		if (list == null || list.Count == 0)
		{
			float updateTime = 86400f;
			checkUpdate = this.StartCoroutine(DoCheck(updateTime));
		}
		else
		{
			int num = list.Min((EventListData data) => data.GetRest());
			float updateTime = (!((float)num < UPDATE_INTERVAL_SEC)) ? ((float)num) : UPDATE_INTERVAL_SEC;
			checkUpdate = this.StartCoroutine(DoCheck(updateTime));
		}
	}

	private IEnumerator DoCheck(float updateTime)
	{
		yield return (object)new WaitForSeconds(updateTime);
		RefreshUI();
	}

	private void ChangeTab()
	{
		bool flag = dispTab == eDispTab.Carnival;
		SetActive((Enum)UI.OBJ_QUEST_LIST_ROOT, !flag);
		SetActive((Enum)UI.OBJ_CARNIVAL_LIST_ROOT, flag);
		SetActive((Enum)UI.OBJ_TWO_TAB, !isCarnival);
		SetActive((Enum)UI.OBJ_THREE_TAB, isCarnival);
		SetActive((Enum)eventOn, dispTab == eDispTab.Event);
		SetActive((Enum)presentOn, dispTab == eDispTab.Present);
		if (isCarnival)
		{
			SetActive((Enum)UI.OBJ_THREE_RIGHT_ON, dispTab == eDispTab.Carnival);
		}
		SetActive((Enum)UI.OBJ_EVENT_ROOT, dispTab == eDispTab.Event);
		SetActive((Enum)UI.GRD_EVENT_QUEST, dispTab == eDispTab.Present);
		DispEventNonListLabel();
		UIScrollView component = GetCtrl(UI.SCR_EVENT_QUEST).GetComponent<UIScrollView>();
		if (component != null)
		{
			component.set_enabled(true);
			component.ResetPosition();
		}
	}

	protected virtual bool IsCarnival()
	{
		return MonoBehaviourSingleton<QuestManager>.I.carnivalEventId > 0;
	}

	private void DispEventNonListLabel()
	{
		Transform val = (dispTab != 0) ? GetCtrl(UI.GRD_EVENT_QUEST) : GetCtrl(UI.OBJ_EVENT_ROOT);
		SetActive((Enum)UI.STR_EVENT_NON_LIST, (!(val != null) || val.get_childCount() <= 0) ? true : false);
	}

	private bool IsLoadEBI2(EventListData e)
	{
		if (e.placeEnum == EVENT_DISPLAY_PLACE.LEFT_EVENT || e.placeEnum == EVENT_DISPLAY_PLACE.RIGHT_EVENT || e.placeEnum == EVENT_DISPLAY_PLACE.LEFT_CONTENTS || e.placeEnum == EVENT_DISPLAY_PLACE.RIGHT_CONTENTS || e.placeEnum == EVENT_DISPLAY_PLACE.CARNIVAL_SMALL_BOX)
		{
			return true;
		}
		return false;
	}

	private bool IsDispTitle(EventListData e)
	{
		if (IsCarnivalEvent(e))
		{
			return false;
		}
		if (e.placeEnum == EVENT_DISPLAY_PLACE.LEFT_CONTENTS || e.placeEnum == EVENT_DISPLAY_PLACE.RIGHT_CONTENTS)
		{
			return true;
		}
		return false;
	}

	private bool IsCarnivalEvent(EventListData e)
	{
		if (e.placeEnum == EVENT_DISPLAY_PLACE.CARNIVAL_SMALL_BOX || e.placeEnum == EVENT_DISPLAY_PLACE.CARNIVAL_NORMAL)
		{
			return true;
		}
		return false;
	}

	private int GetBannerId(EventListData e)
	{
		switch (e.eventTypeEnum)
		{
		case EVENT_TYPE.ARENA:
			if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 50)
			{
				return e.bannerId;
			}
			return 10012201;
		case EVENT_TYPE.EXPLORE:
			if (IsLoadEBI2(e) && !IsCarnivalEvent(e))
			{
				return kBannerId_Explore;
			}
			break;
		case EVENT_TYPE.RUSH:
			if (IsLoadEBI2(e) && !IsCarnivalEvent(e))
			{
				return kBannerId_Rush;
			}
			break;
		case EVENT_TYPE.WAVE:
			if (IsLoadEBI2(e) && !IsCarnivalEvent(e))
			{
				return kBannerId_Wave;
			}
			break;
		}
		return e.bannerId;
	}

	private string GetBannerName(EventListData e, int bannerId)
	{
		bool flag = IsLoadEBI2(e);
		if (!e.enableEvent && e.eventTypeEnum != EVENT_TYPE.ARENA)
		{
			return (!flag) ? ResourceName.GetEventBanner(bannerId, "_close") : ResourceName.GetEventBannerVer2(bannerId, "_close");
		}
		return (!flag) ? ResourceName.GetEventBanner(bannerId) : ResourceName.GetEventBannerVer2(bannerId);
	}

	private Network.EventData ChoiceEventData(EventListData eld)
	{
		if (eld == null)
		{
			return eld;
		}
		if (!MonoBehaviourSingleton<QuestManager>.IsValid())
		{
			return eld;
		}
		Network.EventData eventData = MonoBehaviourSingleton<QuestManager>.I._GetEventData(eld.eventId);
		if (eventData == null)
		{
			return eld;
		}
		return eventData;
	}

	private void OnCloseDialog_QuestEventEndedDialog()
	{
		RefreshUI();
	}

	private void OnQuery_TAB_EVENT()
	{
		if (dispTab != 0)
		{
			dispTab = eDispTab.Event;
			ChangeTab();
		}
	}

	private void OnQuery_TAB_PRESENT()
	{
		if (dispTab != eDispTab.Present)
		{
			dispTab = eDispTab.Present;
			ChangeTab();
		}
	}

	private void OnQuery_TAB_CARNIVAL()
	{
		if (dispTab != eDispTab.Carnival)
		{
			dispTab = eDispTab.Carnival;
			ChangeTab();
		}
	}

	private void OnQuery_SELECT()
	{
		_CheckEvent();
	}

	private void OnQuery_SELECT_DISABLE()
	{
		EventListData ev = GameSection.GetEventData() as EventListData;
		Network.EventData eventData = (from e in MonoBehaviourSingleton<QuestManager>.I.eventList
		where ev.preEventId == e.eventId
		select e).First();
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)ev.preDeliveryId);
		MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, string.Format(StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 5u), ev.name, eventData.name, deliveryTableData.name)), delegate
		{
		});
	}

	private void OnQuery_InGameQuestBackHome_YES()
	{
		string goingHomeEvent = GameSection.GetGoingHomeEvent();
		EventData[] autoEvents = new EventData[3]
		{
			new EventData(goingHomeEvent, null),
			new EventData("EVENT_COUNTER", null),
			new EventData("TAB_EVENT", null)
		};
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	private void OnQuery_TO_AREA_EVENT()
	{
		EventData[] autoEvents;
		if (isInGame)
		{
			autoEvents = new EventData[3]
			{
				new EventData("[BACK]", null),
				new EventData("QUEST_WINDOW", null),
				new EventData("SELECT_NORMAL", null)
			};
		}
		else
		{
			string goingHomeEvent = GameSection.GetGoingHomeEvent();
			autoEvents = new EventData[3]
			{
				new EventData(goingHomeEvent, null),
				new EventData("QUEST_COUNTER", null),
				new EventData("SELECT_NORMAL", null)
			};
		}
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	private void OnQuery_SELECT_EXPLORE()
	{
		_CheckEvent();
	}

	private void OnQuery_SELECT_RUSH()
	{
		_CheckEvent();
	}

	private void OnQuery_SELECT_WAVE()
	{
		_CheckEvent();
	}

	private void OnQuery_SELECT_TRIAL()
	{
		_CheckEvent();
	}

	private void OnQuery_SELECT_ARENA()
	{
		_CheckEvent();
	}

	private void OnQuery_CARNIVAL_HELP()
	{
		GameSection.SetEventData(WebViewManager.Carnival);
	}

	private void OnQuery_TO_CARNIVAL_RANKING()
	{
		GameSection.SetEventData(currentCarnivalData.rankingURL);
	}

	private void OnQuery_CARNIVAL_INFO()
	{
		if (currentCarnivalData != null)
		{
			string arg = MonoBehaviourSingleton<QuestManager>.I.carnivalEventId.ToString();
			if (!string.IsNullOrEmpty(currentCarnivalData.linkName))
			{
				arg = currentCarnivalData.linkName;
			}
			string eventData = string.Format(WebViewManager.NewsWithLinkParamFormat, arg);
			GameSection.SetEventData(eventData);
		}
	}

	private void _CheckEvent()
	{
		Network.EventData ev = GameSection.GetEventData() as Network.EventData;
		if (ev == null)
		{
			return;
		}
		if (ev.HasEndDate() && ev.GetRest() < 0)
		{
			if (ev.eventTypeEnum == EVENT_TYPE.ARENA || ev.eventTypeEnum == EVENT_TYPE.SERIES_ARENA_POINT_CLEAR)
			{
				GameSection.SetEventData(null);
			}
			else
			{
				GameSection.ChangeEvent("SELECT_ENDED");
			}
			return;
		}
		Version nativeVersionFromName = NetworkNative.getNativeVersionFromName();
		if (!ev.IsPlayableWith(nativeVersionFromName))
		{
			string event_data = string.Format(base.sectionData.GetText("REQUIRE_HIGHER_VERSION"), ev.minVersion);
			GameSection.ChangeEvent("SELECT_VERSION", event_data);
		}
		else if (!ev.readPrologueStory)
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<QuestManager>.I.SendQuestReadEventStory(ev.eventId, delegate(bool success, Error error)
			{
				if (success)
				{
					if (ev.prologueStoryId > 0)
					{
						GameSceneTables.EventData eventData = base.sectionData.GetEventData("STORY");
						if (eventData != null)
						{
							string goingHomeEvent = GameSection.GetGoingHomeEvent();
							EventData[] array = null;
							array = ((ev.eventTypeEnum != EVENT_TYPE.ARENA) ? new EventData[4]
							{
								new EventData(goingHomeEvent, null),
								new EventData("EVENT_COUNTER", null),
								(dispTab != 0) ? new EventData("TAB_PRESENT", null) : new EventData("TAB_EVENT", null),
								new EventData(ChangeEventTypeToSelectEventName(ev.eventTypeEnum), ev.eventId)
							} : new EventData[4]
							{
								new EventData(goingHomeEvent, null),
								new EventData("EVENT_COUNTER", null),
								(dispTab != 0) ? new EventData("TAB_PRESENT", null) : new EventData("TAB_EVENT", null),
								new EventData("SELECT_ARENA", ev)
							});
							GameSection.ChangeStayEvent("STORY", new object[4]
							{
								ev.prologueStoryId,
								string.Empty,
								string.Empty,
								array
							});
						}
					}
					ev.readPrologueStory = true;
				}
				GameSection.ResumeEvent(is_resume: true);
			});
		}
	}

	public override EventData CheckAutoEvent(string event_name, object event_data)
	{
		if ((event_name == "SELECT" || event_name == "SELECT_EXPLORE" || event_name == "SELECT_RUSH" || event_name == "SELECT_WAVE" || event_name == "SELECT_TRIAL") && event_data is int)
		{
			if (eventList != null)
			{
				int event_id = (int)event_data;
				EventListData eventListData = eventList.Find((EventListData e) => e.eventId == event_id);
				if (eventListData != null && eventListData.enableEvent)
				{
					EVENT_TYPE eventType = (EVENT_TYPE)eventListData.eventType;
					event_name = ChangeEventTypeToSelectEventName(eventType);
					return new EventData(event_name, eventListData);
				}
			}
			return new EventData("NONE", null);
		}
		return base.CheckAutoEvent(event_name, event_data);
	}

	private string ChangeEventTypeToSelectEventName(EVENT_TYPE eventType)
	{
		switch (eventType)
		{
		case EVENT_TYPE.SERIES_ARENA_POINT_CLEAR:
			return "SELECT_SERIES_ARENA";
		case EVENT_TYPE.EXPLORE:
			return "SELECT_EXPLORE";
		case EVENT_TYPE.RUSH:
			return "SELECT_RUSH";
		case EVENT_TYPE.WAVE:
			return "SELECT_WAVE";
		case EVENT_TYPE.TRIAL:
			return "SELECT_TRIAL";
		default:
			return "SELECT";
		}
	}
}
