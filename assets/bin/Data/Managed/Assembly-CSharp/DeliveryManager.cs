using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DeliveryManager : MonoBehaviourSingleton<DeliveryManager>
{
	private const string UNKNOWN_MAP = "????????";

	private List<Delivery> delivery;

	private bool checkNewDeliveryAtHomeScene;

	public List<int> noticeNewDeliveryAtHomeScene = new List<int>();

	public List<int> noticeNewDeliveryAtInGame = new List<int>();

	public float dailyUpdateRemainTime;

	public float weeklyUpdateRemainTime;

	private List<Coroutine> remainTimeCoroutine = new List<Coroutine>();

	private Coroutine m_coroutine;

	private int m_compDeliveryId;

	private Coroutine m_coroutinePortal;

	private List<EventNormalListData> eventNormalListData;

	public bool isUpdateEventListData;

	public List<EventListData> eventListData;

	private bool firstSetGetList = true;

	public bool initialized
	{
		get;
		private set;
	}

	public List<ClearStatusDelivery> clearStatusDelivery
	{
		get;
		private set;
	}

	public bool isNoticeNewDeliveryAtHomeScene => noticeNewDeliveryAtHomeScene.Count > 0;

	public bool isStoryEventEnd
	{
		get;
		set;
	}

	public List<int> releasedEventIds
	{
		get;
		private set;
	}

	public bool hasProgressDailyDelivery
	{
		get
		{
			if (MonoBehaviourSingleton<DeliveryManager>.I.delivery == null)
			{
				return false;
			}
			bool found = false;
			delivery.ForEach(delegate(Delivery data)
			{
				if (data.dId > 0 && data.type == 0 && !IsClearDelivery((uint)data.dId))
				{
					found = true;
				}
			});
			return found;
		}
	}

	public void AddReleasedRegion(int regionId)
	{
		if (releasedEventIds == null)
		{
			releasedEventIds = new List<int>();
		}
		releasedEventIds.Add(regionId);
	}

	public void UpdateDeliveryReaminTime(float daily, float weekly)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		foreach (Coroutine item3 in remainTimeCoroutine)
		{
			this.StopCoroutine(item3);
		}
		remainTimeCoroutine = new List<Coroutine>();
		if (!(daily < 0f) && !(weekly < 0f))
		{
			Coroutine item = this.StartCoroutine(CheckUpdateDeliveryItem(daily));
			Coroutine item2 = this.StartCoroutine(CheckUpdateDeliveryItem(weekly));
			remainTimeCoroutine.Add(item);
			remainTimeCoroutine.Add(item2);
			dailyUpdateRemainTime = daily;
			weeklyUpdateRemainTime = weekly;
		}
	}

	private unsafe IEnumerator CheckUpdateDeliveryItem(float remainTime)
	{
		yield return (object)new WaitForSeconds(remainTime);
		while (Protocol.isBusy)
		{
			yield return (object)null;
		}
		if (_003CCheckUpdateDeliveryItem_003Ec__Iterator22B._003C_003Ef__am_0024cache4 == null)
		{
			_003CCheckUpdateDeliveryItem_003Ec__Iterator22B._003C_003Ef__am_0024cache4 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		Protocol.Force(_003CCheckUpdateDeliveryItem_003Ec__Iterator22B._003C_003Ef__am_0024cache4);
	}

	public bool IsExistDelivery(DELIVERY_TYPE[] typeList)
	{
		foreach (Delivery item in delivery)
		{
			foreach (DELIVERY_TYPE dELIVERY_TYPE in typeList)
			{
				if (item.type == (int)dELIVERY_TYPE && item.dId > 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool IsExistNotClearDelivery(DELIVERY_CONDITION_TYPE[] conditionTypeList)
	{
		int i = 0;
		for (int count = delivery.Count; i < count; i++)
		{
			if (delivery[i].dId > 0)
			{
				DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)delivery[i].dId);
				if (deliveryTableData != null && deliveryTableData.needs != null && deliveryTableData.needs.Length > 0)
				{
					int j = 0;
					for (int num = deliveryTableData.needs.Length; j < num; j++)
					{
						DeliveryTable.DeliveryData.NeedData needData = deliveryTableData.needs[j];
						if (needData != null && conditionTypeList.Contains(needData.conditionType) && !IsClearDelivery(deliveryTableData.id))
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	public CLEAR_STATUS GetClearStatusDelivery(uint deliveryId)
	{
		CLEAR_STATUS result = CLEAR_STATUS.NEW;
		ClearStatusDelivery clearStatusDelivery = this.clearStatusDelivery.Find((ClearStatusDelivery data) => data.deliveryId == deliveryId);
		if (clearStatusDelivery != null)
		{
			result = (CLEAR_STATUS)clearStatusDelivery.deliveryStatus;
		}
		return result;
	}

	public bool IsClearDelivery(uint deliveryId)
	{
		CLEAR_STATUS clearStatusDelivery = GetClearStatusDelivery(deliveryId);
		CLEAR_STATUS cLEAR_STATUS = clearStatusDelivery;
		if (cLEAR_STATUS == CLEAR_STATUS.CLEAR || cLEAR_STATUS == CLEAR_STATUS.ALL_CLEAR)
		{
			return true;
		}
		return false;
	}

	public bool IsAppearDelivery(uint deliveryId)
	{
		if (deliveryId == 0)
		{
			return false;
		}
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData(deliveryId);
		if (deliveryTableData == null)
		{
			return false;
		}
		if (deliveryTableData.appearDeliveryId == 0)
		{
			return true;
		}
		return IsClearDelivery(deliveryTableData.appearDeliveryId);
	}

	public List<Delivery> GetEventDeliveryList(int event_id, bool do_sort = false)
	{
		List<Delivery> list = new List<Delivery>();
		MonoBehaviourSingleton<DeliveryManager>.I.delivery.ForEach(delegate(Delivery d)
		{
			if (d.dId > 0)
			{
				DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)d.dId);
				if (deliveryTableData.eventID == event_id && (deliveryTableData.type == DELIVERY_TYPE.EVENT || deliveryTableData.type == DELIVERY_TYPE.SUB_EVENT))
				{
					list.Add(d);
				}
			}
		});
		return list;
	}

	public Delivery[] GetDeliveryList(bool do_sort = true)
	{
		if (MonoBehaviourSingleton<DeliveryManager>.I.delivery == null)
		{
			return new Delivery[0];
		}
		List<Delivery> list = new List<Delivery>();
		MonoBehaviourSingleton<DeliveryManager>.I.delivery.ForEach(delegate(Delivery d)
		{
			if (d.dId > 0 && !list.Exists((Delivery x) => x.dId == d.dId))
			{
				list.Add(d);
			}
		});
		if (do_sort)
		{
			list.Sort(delegate(Delivery l, Delivery r)
			{
				DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)l.dId);
				DeliveryTable.DeliveryData deliveryTableData2 = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)r.dId);
				uint num = deliveryTableData?.id ?? 0;
				uint num2 = deliveryTableData2?.id ?? 0;
				int num3 = (num != 0) ? deliveryTableData.needs.Length : 0;
				int num4 = (num2 != 0) ? deliveryTableData2.needs.Length : 0;
				int num5 = (num != 0) ? 1 : 0;
				int num6 = (num2 != 0) ? 1 : 0;
				int i = 0;
				for (int num7 = num3; i < num7; i++)
				{
					MonoBehaviourSingleton<DeliveryManager>.I.GetProgressDelivery(l.dId, out int have, out int need, (uint)i);
					if (need > 0 && need > have)
					{
						num5 = 0;
						break;
					}
				}
				int j = 0;
				for (int num8 = num4; j < num8; j++)
				{
					MonoBehaviourSingleton<DeliveryManager>.I.GetProgressDelivery(r.dId, out int have2, out int need2, (uint)j);
					if (need2 > 0 && need2 > have2)
					{
						num6 = 0;
						break;
					}
				}
				int num9 = num6 - num5;
				if (num9 != 0)
				{
					return num9;
				}
				if (l.order != r.order)
				{
					return r.order - l.order;
				}
				int sortPriority = Singleton<DeliveryTable>.I.GetSortPriority(deliveryTableData.type);
				int sortPriority2 = Singleton<DeliveryTable>.I.GetSortPriority(deliveryTableData2.type);
				int num10 = sortPriority - sortPriority2;
				if (num10 == 0)
				{
					num10 = l.dId - r.dId;
				}
				return num10;
			});
		}
		return list.ToArray();
	}

	public List<DeliveryTable.DeliveryData> GetDeliveryTableDataList(bool do_sort = true)
	{
		List<DeliveryTable.DeliveryData> list = new List<DeliveryTable.DeliveryData>();
		Delivery[] deliveryList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(do_sort);
		int i = 0;
		for (int num = deliveryList.Length; i < num; i++)
		{
			Delivery delivery = deliveryList[i];
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)delivery.dId);
			if (deliveryTableData == null)
			{
				Log.Warning("DeliveryTable Not Found : dId " + delivery.dId);
			}
			else
			{
				list.Add(deliveryTableData);
			}
		}
		return list;
	}

	public bool IsCompletableDelivery(int delivery_id)
	{
		ClearStatusDelivery clearStatusDelivery = this.clearStatusDelivery.Find((ClearStatusDelivery data) => data.deliveryId == delivery_id);
		if (clearStatusDelivery == null)
		{
			return false;
		}
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)delivery_id);
		int i = 0;
		for (int num = deliveryTableData.needs.Length; i < num; i++)
		{
			if (deliveryTableData.needs[i].IsValid() && clearStatusDelivery.GetNeedCount((uint)i) < (uint)deliveryTableData.needs[i].needNum)
			{
				return false;
			}
		}
		return true;
	}

	public bool IsAllClearedEvent(int eventId)
	{
		int i = 0;
		for (int count = this.delivery.Count; i < count; i++)
		{
			Delivery delivery = this.delivery[i];
			if (delivery.dId != 0)
			{
				DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)delivery.dId);
				if (deliveryTableData.eventID == eventId)
				{
					return false;
				}
			}
		}
		return true;
	}

	public int GetCompletableDeliveryNum()
	{
		return CountCompletableDeliveryNum(null, null);
	}

	public int GetCompletableDeliveryNum(DELIVERY_TYPE[] delivery_type)
	{
		return CountCompletableDeliveryNum(null, delivery_type);
	}

	public unsafe int GetCompletableEventDeliveryNum()
	{
		if (_003C_003Ef__am_0024cache13 == null)
		{
			_003C_003Ef__am_0024cache13 = new Func<ClearStatusDelivery, DeliveryTable.DeliveryData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		return CountCompletableDeliveryNum(_003C_003Ef__am_0024cache13, null);
	}

	public unsafe int GetCompletableNormalDeliveryNum()
	{
		if (_003C_003Ef__am_0024cache14 == null)
		{
			_003C_003Ef__am_0024cache14 = new Func<ClearStatusDelivery, DeliveryTable.DeliveryData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		return CountCompletableDeliveryNum(_003C_003Ef__am_0024cache14, null);
	}

	public unsafe int GetCompletableEventDeliveryNum(int event_id)
	{
		_003CGetCompletableEventDeliveryNum_003Ec__AnonStorey57C _003CGetCompletableEventDeliveryNum_003Ec__AnonStorey57C;
		return CountCompletableDeliveryNum(new Func<ClearStatusDelivery, DeliveryTable.DeliveryData, bool>((object)_003CGetCompletableEventDeliveryNum_003Ec__AnonStorey57C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), null);
	}

	public unsafe int GetCompletableRegionDeliveryNum(int regionId, int groupId)
	{
		_003CGetCompletableRegionDeliveryNum_003Ec__AnonStorey57D _003CGetCompletableRegionDeliveryNum_003Ec__AnonStorey57D;
		return CountCompletableDeliveryNum(new Func<ClearStatusDelivery, DeliveryTable.DeliveryData, bool>((object)_003CGetCompletableRegionDeliveryNum_003Ec__AnonStorey57D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), new DELIVERY_TYPE[2]
		{
			DELIVERY_TYPE.STORY,
			DELIVERY_TYPE.ONCE
		});
	}

	private int CountCompletableDeliveryNum(Func<ClearStatusDelivery, DeliveryTable.DeliveryData, bool> condition = null, DELIVERY_TYPE[] delivery_type = null)
	{
		if (clearStatusDelivery == null || clearStatusDelivery.Count == 0)
		{
			return 0;
		}
		int num = 0;
		clearStatusDelivery.ForEach(delegate(ClearStatusDelivery data)
		{
			if (data != null && delivery.FindIndex((Delivery x) => x.dId == data.deliveryId) >= 0 && !IsLimit(data.deliveryId))
			{
				DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)data.deliveryId);
				if ((data.deliveryStatus < 3 || ((deliveryTableData.type == DELIVERY_TYPE.EVENT || deliveryTableData.type == DELIVERY_TYPE.SUB_EVENT) && delivery.Exists((Delivery x) => x.dId == data.deliveryId))) && deliveryTableData.subType != DELIVERY_SUB_TYPE.BINGO && deliveryTableData.subType != DELIVERY_SUB_TYPE.ROW_BINGO && deliveryTableData.subType != DELIVERY_SUB_TYPE.ALL_BINGO)
				{
					if (delivery_type != null)
					{
						bool flag = false;
						DELIVERY_TYPE[] array = delivery_type;
						foreach (DELIVERY_TYPE dELIVERY_TYPE in array)
						{
							if (deliveryTableData.type == dELIVERY_TYPE)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							return;
						}
					}
					if (deliveryTableData != null)
					{
						int j = 0;
						for (int num2 = deliveryTableData.needs.Length; j < num2; j++)
						{
							if (deliveryTableData.needs[j].IsValid() && data.GetNeedCount((uint)j) < (uint)deliveryTableData.needs[j].needNum)
							{
								return;
							}
						}
						if (condition == null || condition.Invoke(data, deliveryTableData))
						{
							num++;
						}
					}
				}
			}
		});
		return num;
	}

	public uint GetCompletableStoryDelivery()
	{
		if (clearStatusDelivery == null || clearStatusDelivery.Count == 0)
		{
			return 0u;
		}
		uint id = 0u;
		clearStatusDelivery.ForEach(delegate(ClearStatusDelivery data)
		{
			if (data != null && id == 0 && data.deliveryStatus < 3)
			{
				DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)data.deliveryId);
				if (deliveryTableData != null && deliveryTableData.type == DELIVERY_TYPE.STORY)
				{
					int i = 0;
					for (int num = deliveryTableData.needs.Length; i < num; i++)
					{
						if (deliveryTableData.needs[i].IsValid() && data.GetNeedCount((uint)i) < (uint)deliveryTableData.needs[i].needNum)
						{
							return;
						}
					}
					id = deliveryTableData.id;
				}
			}
		});
		return id;
	}

	public bool HasClearEventID(uint deliveryId)
	{
		if (deliveryId == 0)
		{
			return false;
		}
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData(deliveryId);
		if (deliveryTableData == null)
		{
			return false;
		}
		if (deliveryTableData.clearEventID == 0)
		{
			return false;
		}
		return true;
	}

	public void GetProgressDelivery(int delivery_id, out int have, out int need, uint idx = 0)
	{
		have = 0;
		need = 0;
		ClearStatusDelivery clearStatusDelivery = this.clearStatusDelivery.Find((ClearStatusDelivery data) => data.deliveryId == delivery_id);
		if (clearStatusDelivery != null)
		{
			have = clearStatusDelivery.GetNeedCount(idx);
		}
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)delivery_id);
		if (deliveryTableData != null)
		{
			need = (int)deliveryTableData.GetNeedItemNum(idx);
		}
	}

	public void GetAllProgressDelivery(int delivery_id, out int have, out int need)
	{
		have = 0;
		need = 0;
		ClearStatusDelivery clearStatusDelivery = this.clearStatusDelivery.Find((ClearStatusDelivery data) => data.deliveryId == delivery_id);
		if (clearStatusDelivery != null)
		{
			have = clearStatusDelivery.GetAllNeedCount();
		}
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)delivery_id);
		if (deliveryTableData != null)
		{
			need = (int)deliveryTableData.GetAllNeedItemNum();
		}
	}

	private bool IsLimit(int delivery_id)
	{
		string text = string.Empty;
		Delivery delivery = this.delivery.Find((Delivery data) => data.dId == delivery_id);
		if (delivery != null && (delivery.type == 11 || delivery.type == 12))
		{
			text = delivery.limit;
		}
		if (!string.IsNullOrEmpty(text))
		{
			DateTime now = TimeManager.GetNow();
			return DateTime.Parse(text).CompareTo(now) < 0;
		}
		return false;
	}

	public string GetLimitText(int delivery_id)
	{
		string text = string.Empty;
		GetAllProgressDelivery(delivery_id, out int have, out int need);
		if (need > 0 && have >= need)
		{
			return text;
		}
		Delivery delivery = this.delivery.Find((Delivery data) => data.dId == delivery_id);
		if (delivery != null)
		{
			text = delivery.limit;
		}
		if (string.IsNullOrEmpty(text))
		{
			return text;
		}
		DateTime now = TimeManager.GetNow();
		DateTime dateTime = DateTime.Parse(text);
		if (dateTime.CompareTo(now) < 0)
		{
			return StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 11u);
		}
		TimeSpan timeSpan = dateTime.Subtract(now);
		StringBuilder stringBuilder = new StringBuilder(string.Empty);
		if (timeSpan.Days > 0)
		{
			stringBuilder.Append(string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 9u), timeSpan.Days));
		}
		else if (timeSpan.Hours > 0)
		{
			stringBuilder.Append(string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 10u), timeSpan.Hours));
		}
		else
		{
			stringBuilder.Append(string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 10u), 1));
		}
		stringBuilder.Append(" " + StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 12u));
		return stringBuilder.ToString();
	}

	public string GetTargetItemName(int delivery_id, uint idx = 0)
	{
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)delivery_id);
		if (deliveryTableData != null)
		{
			return deliveryTableData.GetNeedItemName(idx);
		}
		return string.Empty;
	}

	public void GetDeliveryData(int delivery_id, out int have, out int need, out string item_name, out string limit_time)
	{
		GetProgressDelivery(delivery_id, out have, out need, 0u);
		limit_time = GetLimitText(delivery_id);
		item_name = GetTargetItemName(delivery_id, 0u);
	}

	public void GetDeliveryDataAllNeeds(int delivery_id, out int have, out int need, out string item_name, out string limit_time)
	{
		GetAllProgressDelivery(delivery_id, out have, out need);
		limit_time = GetLimitText(delivery_id);
		item_name = GetTargetItemName(delivery_id, 0u);
	}

	public void GetTargetEnemyData(int delivery_id, out uint jump_quest_id, out uint jump_map_id, out string map_name, out string enemy_name, out DIFFICULTY_TYPE? difficulty, out int[] targetPortalID)
	{
		jump_quest_id = 0u;
		jump_map_id = 0u;
		map_name = string.Empty;
		enemy_name = string.Empty;
		difficulty = null;
		targetPortalID = null;
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)delivery_id);
		if (deliveryTableData != null)
		{
			uint enemy_id = deliveryTableData.GetEnemyID(0u);
			uint mapID = deliveryTableData.GetMapID(0u);
			int num = (int)Singleton<FieldMapTable>.I.GetTargetEnemyPopMapID(enemy_id);
			if (num != 0)
			{
				if (deliveryTableData.jumpMapID < 0)
				{
					num = -1;
				}
				else
				{
					if (mapID != 0)
					{
						num = (int)mapID;
					}
					if (deliveryTableData.jumpMapID > 0)
					{
						num = deliveryTableData.jumpMapID;
					}
					FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)num);
					if (fieldMapData != null)
					{
						map_name = fieldMapData.mapName;
						jump_map_id = (uint)num;
					}
					else
					{
						num = -1;
					}
				}
				if (num == -1)
				{
					map_name = "????????";
					jump_map_id = 0u;
				}
			}
			else if (deliveryTableData.jumpMapID > 0)
			{
				FieldMapTable.FieldMapTableData fieldMapData2 = Singleton<FieldMapTable>.I.GetFieldMapData((uint)deliveryTableData.jumpMapID);
				if (fieldMapData2 != null)
				{
					map_name = fieldMapData2.mapName;
					targetPortalID = deliveryTableData.targetPortalID;
					if (targetPortalID != null)
					{
						bool flag = false;
						int i = 0;
						for (int num2 = targetPortalID.Length; i < num2; i++)
						{
							if (targetPortalID[i] != 0)
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							map_name = string.Format(StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 2u), map_name);
						}
					}
					jump_map_id = (uint)deliveryTableData.jumpMapID;
					enemy_name = StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 1u);
				}
				else
				{
					map_name = "????????";
					jump_map_id = 0u;
				}
			}
			else
			{
				bool is_find = false;
				string tmp_name = string.Empty;
				uint tmp_quest_id = 0u;
				DIFFICULTY_TYPE tmp_difficulty = DIFFICULTY_TYPE.LV1;
				Singleton<QuestTable>.I.AllQuestData(delegate(QuestTable.QuestTableData data)
				{
					if (!is_find)
					{
						for (int j = 0; j < data.seriesNum; j++)
						{
							if (data.enemyID[j] == (int)enemy_id)
							{
								is_find = true;
								tmp_name = data.questText;
								tmp_difficulty = data.difficulty;
								tmp_quest_id = data.questID;
							}
						}
					}
				});
				if (is_find)
				{
					map_name = tmp_name;
					difficulty = tmp_difficulty;
					jump_quest_id = tmp_quest_id;
				}
			}
			if (!string.IsNullOrEmpty(deliveryTableData.placeName))
			{
				map_name = deliveryTableData.placeName;
			}
			if (!string.IsNullOrEmpty(deliveryTableData.enemyName))
			{
				enemy_name = deliveryTableData.enemyName;
			}
			if (string.IsNullOrEmpty(enemy_name))
			{
				EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData(enemy_id);
				if (enemyData != null)
				{
					enemy_name = enemyData.name;
				}
				if (enemy_id == 0 && mapID != 0)
				{
					enemy_name = StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 0u);
				}
			}
		}
	}

	public int ProgressDelivery(int delivery_id, int need_index, int add_num)
	{
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)delivery_id);
		ClearStatusDelivery clearStatusDelivery = this.clearStatusDelivery.Find((ClearStatusDelivery data) => data.deliveryId == delivery_id);
		if (clearStatusDelivery == null)
		{
			clearStatusDelivery = new ClearStatusDelivery();
			clearStatusDelivery.deliveryId = delivery_id;
			if (deliveryTableData != null)
			{
				int i = 0;
				for (int num = deliveryTableData.needs.Length; i < num; i++)
				{
					clearStatusDelivery.needCount.Add(0);
				}
			}
			this.clearStatusDelivery.Add(clearStatusDelivery);
		}
		int num2 = clearStatusDelivery.needCount[need_index];
		List<int> needCount;
		List<int> list = needCount = clearStatusDelivery.needCount;
		int index;
		int index2 = index = need_index;
		index = needCount[index];
		list[index2] = index + add_num;
		if (clearStatusDelivery.needCount[need_index] > (int)deliveryTableData.needs[need_index].needNum)
		{
			clearStatusDelivery.needCount[need_index] = (int)deliveryTableData.needs[need_index].needNum;
			add_num = clearStatusDelivery.needCount[need_index] - num2;
		}
		if (IsClearTutorialDelivery(deliveryTableData, clearStatusDelivery) && UITutorialFieldHelper.IsValid())
		{
			UITutorialFieldHelper.I.OnCollectItem();
		}
		return add_num;
	}

	public bool IsClearTutorialDelivery(DeliveryTable.DeliveryData table, ClearStatusDelivery target_delivery_clear_status)
	{
		if (!TutorialStep.IsPlayingFirstBackHome() && !TutorialStep.IsPlayingFirstDelivery())
		{
			return false;
		}
		if (TutorialStep.IsPlayingFirstDelivery())
		{
			if (table == null || table.needs == null)
			{
				return false;
			}
			if (target_delivery_clear_status.needCount.Count < table.needs.Length)
			{
				return false;
			}
			int i = 0;
			for (int num = table.needs.Length; i < num; i++)
			{
				if (target_delivery_clear_status.needCount[i] < (int)table.needs[i].needNum)
				{
					return false;
				}
			}
		}
		return true;
	}

	public int[] GetRecvStoryDelivery()
	{
		List<int> list = new List<int>();
		delivery.ForEach(delegate(Delivery data)
		{
			if (data.type == 8)
			{
				list.Add(data.dId);
			}
		});
		return list.ToArray();
	}

	public void SetList()
	{
		if (firstSetGetList)
		{
			firstSetGetList = false;
			OnceDeliveryModel.Param param = MonoBehaviourSingleton<OnceManager>.I.result.delivery;
			delivery = param.delivery;
			clearStatusDelivery = param.clearStatusDelivery;
		}
	}

	public void SendDeliveryComplete(string uId, bool enable_clear_event, Action<bool, DeliveryRewardList> call_back)
	{
		DeliveryCompleteModel.RequestSendForm requestSendForm = new DeliveryCompleteModel.RequestSendForm();
		requestSendForm.uId = uId;
		if (enable_clear_event)
		{
			checkNewDeliveryAtHomeScene = true;
		}
		Protocol.Send(DeliveryCompleteModel.URL, requestSendForm, delegate(DeliveryCompleteModel ret)
		{
			checkNewDeliveryAtHomeScene = false;
			bool flag = false;
			DeliveryRewardList deliveryRewardList = null;
			switch (ret.Error)
			{
			case Error.None:
				flag = true;
				deliveryRewardList = ret.result.reward;
				if (ret.result.openRegionIds != null && ret.result.openRegionIds.Count > 0)
				{
					foreach (int openRegionId in ret.result.openRegionIds)
					{
						MonoBehaviourSingleton<WorldMapManager>.I.AddReleasedRegion(openRegionId);
					}
				}
				if (ret.result.openEventIds != null && ret.result.openEventIds.Count > 0)
				{
					foreach (int openEventId in ret.result.openEventIds)
					{
						AddReleasedRegion(openEventId);
					}
				}
				break;
			case Error.WRN_DELIVERY_OVER:
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_DELIVERY_OVER);
				break;
			}
			call_back.Invoke(flag, deliveryRewardList);
		}, string.Empty);
	}

	public void SendDeliveryUpdate(Action<bool> call_back)
	{
		Protocol.Send(DeliveryUpdateModel.URL, delegate(DeliveryUpdateModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendGetClearStatusList(List<DELIVERY_CONDITION_TYPE> condiditionTypeList, Action<bool, DeliveryGetClearStatusModel.Param> call_back)
	{
		DeliveryGetClearStatusModel.RequestSendForm requestSendForm = new DeliveryGetClearStatusModel.RequestSendForm();
		List<int> list = new List<int>(condiditionTypeList.Count);
		int i = 0;
		for (int count = condiditionTypeList.Count; i < count; i++)
		{
			list.Add((int)condiditionTypeList[i]);
		}
		requestSendForm.conditionTypes = list;
		Protocol.Send(DeliveryGetClearStatusModel.URL, requestSendForm, delegate(DeliveryGetClearStatusModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
			}
			call_back.Invoke(flag, ret.result);
		}, string.Empty);
	}

	public void UpdateClearStatuses(List<ClearStatusDelivery> clearStatusUpdateList)
	{
		if (clearStatusUpdateList != null && clearStatusUpdateList.Count > 0)
		{
			int i = 0;
			for (int count = clearStatusUpdateList.Count; i < count; i++)
			{
				ClearStatusDelivery clearStatusDelivery = clearStatusUpdateList[i];
				if (clearStatusDelivery != null && clearStatusDelivery.deliveryId > 0)
				{
					ClearStatusDelivery clearStatusDelivery2 = this.clearStatusDelivery.Find((ClearStatusDelivery status) => status.deliveryId == clearStatusUpdateList[i].deliveryId);
					if (clearStatusDelivery2 == null || clearStatusDelivery2.deliveryId <= 0)
					{
						this.clearStatusDelivery.Add(clearStatusDelivery);
						CheckCompletableClearStatus(clearStatusDelivery);
					}
					else
					{
						bool flag = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(clearStatusDelivery.deliveryId);
						clearStatusDelivery2.deliveryId = clearStatusDelivery.deliveryId;
						clearStatusDelivery2.deliveryStatus = clearStatusDelivery.deliveryStatus;
						clearStatusDelivery2.needCount = clearStatusDelivery.needCount;
						if (flag)
						{
							break;
						}
						CheckCompletableClearStatus(clearStatusDelivery2);
					}
				}
			}
		}
	}

	public void SendReadStoryRead(int scriptId, Action<bool, Error> call_back)
	{
		ReadStoryReadModel.RequestSendForm requestSendForm = new ReadStoryReadModel.RequestSendForm();
		requestSendForm.scriptNum = scriptId;
		Protocol.Send(ReadStoryReadModel.URL, requestSendForm, delegate(ReadStoryReadModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				flag = true;
			}
			call_back.Invoke(flag, ret.Error);
		}, string.Empty);
	}

	public void SendEventNormalList(Action<bool> call_back)
	{
		eventNormalListData = null;
		Protocol.Send(EventNormalListModel.URL, delegate(EventNormalListModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				eventNormalListData = ret.result;
			}
			call_back(obj);
		}, string.Empty);
	}

	public EventNormalListData GetEventNormalListData(int regionId)
	{
		if (eventNormalListData == null)
		{
			return null;
		}
		return eventNormalListData.Find((EventNormalListData x) => x.regionId == regionId);
	}

	public void SendEventList(Action<bool> call_back)
	{
		eventListData = null;
		Protocol.Send(EventListModel.URL, delegate(EventListModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				eventListData = ret.result;
				if (!eventListData.IsNullOrEmpty())
				{
					int i = 0;
					for (int count = eventListData.Count; i < count; i++)
					{
						eventListData[i].OnRecv();
						eventListData[i].SetupEnum();
					}
				}
			}
			isUpdateEventListData = true;
			call_back(obj);
		}, string.Empty);
	}

	public bool IsCarnivalEvent(int eventId)
	{
		if (!HasEventListData())
		{
			return false;
		}
		List<EventListData> list = MonoBehaviourSingleton<DeliveryManager>.I.eventListData;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].eventId == eventId && (list[i].place == 110 || list[i].place == 111))
			{
				return true;
			}
		}
		return false;
	}

	public bool HasEventListData()
	{
		return !eventListData.IsNullOrEmpty();
	}

	public EventListData GetEventListData(int eventId)
	{
		if (eventListData == null)
		{
			return null;
		}
		return eventListData.Find((EventListData x) => x.eventId == eventId);
	}

	public void SendDebugSetDeliveryCount(string uId, Action<bool> call_back, int cnt0 = 0, int cnt1 = 0, int cnt2 = 0, int cnt3 = 0, int cnt4 = 0)
	{
		DebugSetDeliveryCntModel.RequestSendForm requestSendForm = new DebugSetDeliveryCntModel.RequestSendForm();
		requestSendForm.uId = uId;
		requestSendForm.cnts.Add(cnt0);
		requestSendForm.cnts.Add(cnt1);
		requestSendForm.cnts.Add(cnt2);
		requestSendForm.cnts.Add(cnt3);
		requestSendForm.cnts.Add(cnt4);
		Protocol.Send(DebugSetDeliveryCntModel.URL, requestSendForm, delegate(DebugSetDeliveryCntModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendDebugSetDeliveryCountByDeliveryId(int deliveryId, int cnt0, int cnt1, int cnt2, int cnt3, int cnt4, Action<bool> call_back)
	{
		Delivery delivery = this.delivery.Find((Delivery d) => d.dId == deliveryId);
		if (delivery != null)
		{
			SendDebugSetDeliveryCount(delivery.uId, delegate(bool b)
			{
				call_back(b);
			}, cnt0, cnt1, cnt2, cnt3, cnt4);
		}
	}

	public void SendDebugGetDelivery(int did, Action<bool> call_back)
	{
		DebugGetDeliveryModel.RequestSendForm requestSendForm = new DebugGetDeliveryModel.RequestSendForm();
		requestSendForm.did = did;
		Protocol.Send(DebugGetDeliveryModel.URL, requestSendForm, delegate(DebugGetDeliveryModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	private void DirtyDelivery()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_DELIVERY_UPDATE);
	}

	private void DirtyClearDelivery()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_QUEST_CLEAR_STATUS);
	}

	public void OnDiff(BaseModelDiff.DiffDelivery diff)
	{
		bool normal_delivery_notice = false;
		bool daily_delivery_updated = false;
		bool weekly_delivery_updated = false;
		bool flag = false;
		if (Utility.IsExist(diff.add))
		{
			diff.add.ForEach(delegate(Delivery data)
			{
				delivery.Add(data);
				bool flag3 = false;
				DeliveryTable.DeliveryData deliveryTableData2 = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)data.dId);
				if (deliveryTableData2.type == DELIVERY_TYPE.STORY || deliveryTableData2.type == DELIVERY_TYPE.ONCE || deliveryTableData2.type == DELIVERY_TYPE.ETC || deliveryTableData2.type == DELIVERY_TYPE.DAILY || deliveryTableData2.type == DELIVERY_TYPE.WEEKLY)
				{
					flag3 = true;
					normal_delivery_notice = true;
				}
				if (deliveryTableData2.type == DELIVERY_TYPE.DAILY && data.dId > 0)
				{
					daily_delivery_updated = true;
				}
				if (deliveryTableData2.type == DELIVERY_TYPE.WEEKLY && data.dId > 0)
				{
					weekly_delivery_updated = true;
				}
				if (checkNewDeliveryAtHomeScene && data.dId != 0 && flag3)
				{
					noticeNewDeliveryAtHomeScene.Add(data.dId);
				}
				if (FieldManager.IsValidInGameNoQuest() && flag3)
				{
					noticeNewDeliveryAtInGame.Add(data.dId);
				}
			});
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			diff.update.ForEach(delegate(Delivery data)
			{
				Delivery delivery = this.delivery.Find((Delivery list_data) => list_data.uId == data.uId);
				delivery.uId = data.uId;
				delivery.dId = data.dId;
				delivery.type = data.type;
				delivery.limit = data.limit;
				delivery.order = data.order;
				bool flag2 = false;
				if (data.dId != 0)
				{
					DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)data.dId);
					if (deliveryTableData.type == DELIVERY_TYPE.STORY || deliveryTableData.type == DELIVERY_TYPE.ONCE || deliveryTableData.type == DELIVERY_TYPE.ETC || deliveryTableData.type == DELIVERY_TYPE.DAILY || deliveryTableData.type == DELIVERY_TYPE.WEEKLY)
					{
						flag2 = true;
						normal_delivery_notice = true;
					}
					if (deliveryTableData.type == DELIVERY_TYPE.DAILY && data.dId > 0)
					{
						daily_delivery_updated = true;
					}
					if (deliveryTableData.type == DELIVERY_TYPE.WEEKLY && data.dId > 0)
					{
						weekly_delivery_updated = true;
					}
					if (checkNewDeliveryAtHomeScene && data.dId != 0 && flag2)
					{
						noticeNewDeliveryAtHomeScene.Add(data.dId);
					}
					if (FieldManager.IsValidInGameNoQuest() && flag2)
					{
						noticeNewDeliveryAtInGame.Add(data.dId);
					}
				}
			});
			flag = true;
		}
		if (flag)
		{
			if (normal_delivery_notice && !GameSaveData.instance.IsRecommendedDeliveryCheck())
			{
				GameSaveData.instance.recommendedDeliveryCheck = 1;
				GameSaveData.Save();
			}
			if (daily_delivery_updated && !GameSaveData.instance.IsRecommendedDailyDeliveryCheck())
			{
				GameSaveData.instance.recommendedDailyDeliveryCheck = 1;
				GameSaveData.instance.recommendedDailyDeliveryCheckAtHome = 1;
				GameSaveData.Save();
			}
			if (weekly_delivery_updated && !GameSaveData.instance.IsRecommendedWeeklyDeliveryCheck())
			{
				GameSaveData.instance.recommendedWeeklyDeliveryCheck = 1;
				GameSaveData.Save();
			}
			DirtyDelivery();
		}
	}

	public void OnDiff(BaseModelDiff.DiffClearStatusDelivery diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.add))
		{
			diff.add.ForEach(delegate(ClearStatusDelivery data)
			{
				DeliveryManager deliveryManager2 = this;
				clearStatusDelivery.RemoveAll((ClearStatusDelivery find_data) => find_data.deliveryId == data.deliveryId);
				clearStatusDelivery.Add(data);
				CheckCompletableClearStatus(data);
			});
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			diff.update.ForEach(delegate(ClearStatusDelivery data)
			{
				DeliveryManager deliveryManager = this;
				ClearStatusDelivery clearStatusDelivery = this.clearStatusDelivery.Find((ClearStatusDelivery list_data) => list_data.deliveryId == data.deliveryId);
				clearStatusDelivery.deliveryId = data.deliveryId;
				clearStatusDelivery.deliveryStatus = data.deliveryStatus;
				clearStatusDelivery.needCount = data.needCount;
				CheckCompletableClearStatus(clearStatusDelivery);
			});
			flag = true;
		}
		if (flag)
		{
			DirtyClearDelivery();
		}
	}

	private void CheckCompletableClearStatus(ClearStatusDelivery data)
	{
		if (MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(data.deliveryId))
		{
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)data.deliveryId);
			if (!deliveryTableData.IsStoryDelivery() && deliveryTableData.GetConditionType(0u) != 0 && !IsDefeatFieldConditionType(deliveryTableData.GetConditionType(0u)) && !IsDeliveryArena(deliveryTableData) && MonoBehaviourSingleton<UIAnnounceBand>.IsValid())
			{
				string empty = string.Empty;
				empty = ((!IsDeliveryBingo(deliveryTableData)) ? StringTable.Get(STRING_CATEGORY.DELIVERY_COMPLETE, 1u) : StringTable.Get(STRING_CATEGORY.DELIVERY_COMPLETE, 2u));
				MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce(deliveryTableData.name, empty);
				SoundManager.PlayOneshotJingle(40000030, null, null);
			}
			CheckAnnounceHomeReturn(data.deliveryId);
		}
	}

	public Network.EventData GetEventCleardDeliveryData()
	{
		if (m_compDeliveryId == 0)
		{
			return null;
		}
		List<Network.EventData> list = new List<Network.EventData>(MonoBehaviourSingleton<QuestManager>.I.eventList);
		list.RemoveAll((Network.EventData e) => e.HasEndDate() && e.GetRest() < 0);
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)m_compDeliveryId);
		if (deliveryTableData == null)
		{
			return null;
		}
		int eventID = deliveryTableData.eventID;
		Network.EventData result = null;
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			if (list[i].eventId == eventID)
			{
				result = list[i];
				break;
			}
		}
		List<Network.EventData> list2 = new List<Network.EventData>(MonoBehaviourSingleton<QuestManager>.I.GetValidBingoDataListInSection());
		int j = 0;
		for (int count2 = list2.Count; j < count2; j++)
		{
			if (list2[j].eventId == eventID)
			{
				result = list2[j];
				break;
			}
		}
		return result;
	}

	public void DeleteCleardDeliveryId()
	{
		m_compDeliveryId = 0;
	}

	public void CheckAnnouncePortalOpen()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		if (m_coroutinePortal == null)
		{
			m_coroutinePortal = this.StartCoroutine(CheckPortalOpen());
		}
	}

	public void CheckAnnounceHomeReturn(int delivery_id)
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		if (m_compDeliveryId == 0)
		{
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)delivery_id);
			if (deliveryTableData.IsClearDialogInGame() && !IsDeliveryExplore(deliveryTableData) && !IsDeliveryRush(deliveryTableData) && !IsDeliveryWave(deliveryTableData))
			{
				if (m_coroutine != null)
				{
					this.StopCoroutine(m_coroutine);
				}
				if (deliveryTableData.IsEvent())
				{
					m_compDeliveryId = delivery_id;
				}
				m_coroutine = this.StartCoroutine(CheckRequestHomeReturn());
			}
		}
	}

	private bool IsDeliveryExplore(DeliveryTable.DeliveryData delivery)
	{
		List<Network.EventData> eventList = MonoBehaviourSingleton<QuestManager>.I.eventList;
		if (eventList == null)
		{
			return false;
		}
		int i = 0;
		for (int count = eventList.Count; i < count; i++)
		{
			if (eventList[i].eventId == delivery.eventID && eventList[i].eventType == 4)
			{
				return true;
			}
		}
		return false;
	}

	private bool IsDeliveryRush(DeliveryTable.DeliveryData delivery)
	{
		List<Network.EventData> eventList = MonoBehaviourSingleton<QuestManager>.I.eventList;
		if (eventList == null)
		{
			return false;
		}
		int i = 0;
		for (int count = eventList.Count; i < count; i++)
		{
			if (eventList[i].eventId == delivery.eventID && eventList[i].eventType == 12)
			{
				return true;
			}
		}
		return false;
	}

	private bool IsDeliveryWave(DeliveryTable.DeliveryData delivery)
	{
		List<Network.EventData> eventList = MonoBehaviourSingleton<QuestManager>.I.eventList;
		if (eventList == null)
		{
			return false;
		}
		int i = 0;
		for (int count = eventList.Count; i < count; i++)
		{
			if (eventList[i].eventId == delivery.eventID && eventList[i].eventType == 27)
			{
				return true;
			}
		}
		return false;
	}

	private bool IsDeliveryArena(DeliveryTable.DeliveryData delivery)
	{
		List<Network.EventData> eventList = MonoBehaviourSingleton<QuestManager>.I.eventList;
		if (eventList == null)
		{
			return false;
		}
		for (int i = 0; i < eventList.Count; i++)
		{
			if (eventList[i].eventId == delivery.eventID && eventList[i].eventType == 15)
			{
				return true;
			}
		}
		return false;
	}

	private static bool IsDeliveryBingo(DeliveryTable.DeliveryData delivery)
	{
		if (delivery.subType == DELIVERY_SUB_TYPE.BINGO)
		{
			return true;
		}
		if (delivery.subType == DELIVERY_SUB_TYPE.ROW_BINGO)
		{
			return true;
		}
		if (delivery.subType == DELIVERY_SUB_TYPE.ALL_BINGO)
		{
			return true;
		}
		return false;
	}

	public static bool IsDeliveryBingo(uint id)
	{
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData(id);
		if (deliveryTableData == null)
		{
			return false;
		}
		return IsDeliveryBingo(deliveryTableData);
	}

	private IEnumerator CheckRequestHomeReturn()
	{
		yield return (object)null;
		while (true)
		{
			if (IsDeleteCleardAnnounce())
			{
				m_coroutine = null;
				yield break;
			}
			if (IsDispCleardAnnounce())
			{
				break;
			}
			yield return (object)null;
		}
		MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameMain", this.get_gameObject(), "CLEARED_RETURN", null, null, true);
		m_coroutine = null;
	}

	private IEnumerator CheckPortalOpen()
	{
		yield return (object)null;
		while (true)
		{
			if (IsDeleteCleardAnnounce())
			{
				m_coroutinePortal = null;
				yield break;
			}
			if (IsDispCleardAnnounce())
			{
				break;
			}
			yield return (object)null;
		}
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			m_coroutinePortal = null;
		}
		else
		{
			List<PortalObject> portalObjList = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList;
			if (portalObjList == null)
			{
				m_coroutinePortal = null;
			}
			else
			{
				bool isNewPortalOpen = false;
				for (int i = 0; i < portalObjList.Count; i++)
				{
					FieldMapTable.PortalTableData portalData = portalObjList[i].portalData;
					if ((FieldManager.IsOpenPortalClearOrder(portalData) || FieldManager.IsOpenPortal(portalData)) && GameSaveData.instance.isNewReleasePortal(portalObjList[i].portalID))
					{
						PortalObject preObj = portalObjList[i];
						portalObjList[i] = PortalObject.Create(preObj.portalInfo, preObj._transform.get_parent());
						PortalUnlockEvent portalUnlockEvent = MonoBehaviourSingleton<InGameManager>.I.get_gameObject().AddComponent<PortalUnlockEvent>();
						portalUnlockEvent.AddPortal(portalObjList[i]);
						GameSaveData.instance.newReleasePortals.Remove(portalObjList[i].portalID);
						Object.Destroy(preObj.get_gameObject());
						isNewPortalOpen = true;
					}
				}
				if (isNewPortalOpen)
				{
					MonoBehaviourSingleton<FieldManager>.I.ResetPortalPointToIndex();
				}
				m_coroutinePortal = null;
			}
		}
	}

	private bool IsDeleteCleardAnnounce()
	{
		if (!TutorialStep.HasAllTutorialCompleted())
		{
			return true;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsCurrentSceneHomeOrLounge())
		{
			return true;
		}
		return false;
	}

	private bool IsDispCleardAnnounce()
	{
		if (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
		{
			return false;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			return false;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent())
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop)
		{
			return false;
		}
		if (!FieldManager.IsValidInGameNoBoss())
		{
			return false;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "InGameMain")
		{
			return true;
		}
		return false;
	}

	public static bool IsInvalidClearInGame(DELIVERY_TYPE type, DIFFICULTY_MODE fieldMode)
	{
		if (type == DELIVERY_TYPE.ONCE && fieldMode == DIFFICULTY_MODE.HARD)
		{
			return true;
		}
		return type != DELIVERY_TYPE.ONCE && type != DELIVERY_TYPE.SUB_EVENT;
	}

	public bool IsDefeatFieldConditionType(DELIVERY_CONDITION_TYPE conditionType)
	{
		List<DELIVERY_CONDITION_TYPE> list = new List<DELIVERY_CONDITION_TYPE>();
		list.Add(DELIVERY_CONDITION_TYPE.DEFEAT_FIELD_ENEMY_ID);
		List<DELIVERY_CONDITION_TYPE> list2 = list;
		return list2.Contains(conditionType);
	}
}
