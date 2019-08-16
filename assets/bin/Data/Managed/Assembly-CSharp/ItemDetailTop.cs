using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDetailTop : GameSection
{
	private enum UI
	{
		OBJ_DETAIL_ROOT,
		BTN_DETAIL_SELL,
		STR_BTN_SELL,
		STR_BTN_SELL_D,
		BTN_SEARCH_TP,
		OBJ_ICON_ROOT,
		LBL_NAME,
		LBL_SELL,
		LBL_HAVE_NUM,
		SPR_NEED,
		LBL_NEED_NUM,
		GRD_HOWTO,
		STR_NOT_HOWTO,
		SCR_HOWTO,
		STR_TITLE,
		STR_SELL,
		STR_NEED,
		STR_HAVE,
		OBJ_ICON,
		LBL_QUEST_TYPE,
		LBL_QUEST_NUM,
		LBL_QUEST_NAME,
		OBJ_MISSION_INFO_ROOT,
		OBJ_TOP_CROWN1,
		SPR_CROWN_1,
		OBJ_TOP_CROWN2,
		SPR_CROWN_2,
		OBJ_TOP_CROWN3,
		SPR_CROWN_3,
		OBJ_ENEMY,
		SPR_MONSTER_ICON,
		SPR_ELEMENT_ROOT,
		SPR_ELEMENT,
		SPR_WEAK_ELEMENT,
		STR_NON_WEAK_ELEMENT,
		OBJ_FIELD_ICON,
		TEX_FIELD,
		TEX_FIELD_SUB,
		TWN_DIFFICULT_STAR,
		OBJ_DIFFICULT_STAR_1,
		OBJ_DIFFICULT_STAR_2,
		OBJ_DIFFICULT_STAR_3,
		OBJ_DIFFICULT_STAR_4,
		OBJ_DIFFICULT_STAR_5,
		OBJ_DIFFICULT_STAR_6,
		OBJ_DIFFICULT_STAR_7,
		OBJ_DIFFICULT_STAR_8,
		OBJ_DIFFICULT_STAR_9,
		OBJ_DIFFICULT_STAR_10,
		LBL_ORDER_NUM,
		LBL_FIELD_NAME,
		LBL_FIELD_ENEMY_NAME,
		OBJ_SCROLL_BAR,
		OBJ_CHALLENGE_ON,
		OBJ_CHALLENGE_OFF,
		LBL_CHALLENGE_ON_MESSAGE,
		LBL_CHALLENGE_OFF_MESSAGE,
		TEX_NPC,
		SPR_EMBLEM_LAYER_1,
		SPR_EMBLEM_LAYER_2,
		SPR_EMBLEM_LAYER_3
	}

	private class CandidateEnemyInfo
	{
		public uint EnemyId;

		public bool IsHappenEnemy;
	}

	public class ItemDestination
	{
		public enum TYPE
		{
			Unknown,
			AdmissionQuest,
			DeniedQuest,
			HappenField,
			HappenFieldUnknown,
			DropField,
			DropFieldUnknown,
			GachaQuest,
			ChallengeQuest,
			PointShop,
			GuildRequest,
			TradingPostQuest
		}

		public TYPE type;

		public QuestTable.QuestTableData quest;

		public FieldMapTable.FieldMapTableData field;

		public ItemToFieldTable.ItemDetailToFieldData recommend_field;

		public uint enemy_id;

		public int enemy_species;

		public PointShopData pointShopData;
	}

	public class PointShopData
	{
		public PointShop shop;

		public PointShopItem item;

		public PointShopData(PointShop shop, PointShopItem item)
		{
			this.shop = shop;
			this.item = item;
		}
	}

	private SortCompareData data;

	private Transform detailBase;

	private UI[] difficult = new UI[10]
	{
		UI.OBJ_DIFFICULT_STAR_1,
		UI.OBJ_DIFFICULT_STAR_2,
		UI.OBJ_DIFFICULT_STAR_3,
		UI.OBJ_DIFFICULT_STAR_4,
		UI.OBJ_DIFFICULT_STAR_5,
		UI.OBJ_DIFFICULT_STAR_6,
		UI.OBJ_DIFFICULT_STAR_7,
		UI.OBJ_DIFFICULT_STAR_8,
		UI.OBJ_DIFFICULT_STAR_9,
		UI.OBJ_DIFFICULT_STAR_10
	};

	private UI[] mission = new UI[6]
	{
		UI.OBJ_TOP_CROWN1,
		UI.SPR_CROWN_1,
		UI.OBJ_TOP_CROWN2,
		UI.SPR_CROWN_2,
		UI.OBJ_TOP_CROWN3,
		UI.SPR_CROWN_3
	};

	private const int RECOMMEND_ITEM_MAX = 3;

	private List<ItemDestination> m_ItemDestinations = new List<ItemDestination>();

	private int searchEnemySpecies;

	private QuestTable.QuestTableData jumpQuest;

	private FieldMapTable.FieldMapTableData jumpField;

	private EventData[] jump_event_list_datas;

	private List<PointShop> pointShopList = new List<PointShop>();

	private PointShopData selectedPointShopdata;

	private ItemDestination selectedPointShopDestination;

	private int itemId = -1;

	private bool backSection;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "ItemToQuestTable";
			yield return "QuestToFieldTable";
			yield return "ItemToFieldTable";
			yield return "EquipItemExceedTable";
			yield return "FieldMapTable";
			yield return "QuestTable";
		}
	}

	public override void Initialize()
	{
		data = (GameSection.GetEventData() as SortCompareData);
		Protocol.Force(GetItemData);
	}

	private void GetItemData()
	{
		ItemToQuestTableModel.RequestForm requestForm = new ItemToQuestTableModel.RequestForm();
		itemId = (int)data.GetTableID();
		requestForm.itemId = itemId.ToString();
		Protocol.Send("ajax/datatable/itemtoquest", requestForm, delegate(ItemToQuestTableModel res)
		{
			if (res.Error == Error.None)
			{
				Singleton<ItemToQuestTable>.I.AddTableFromAPI(data.GetTableID(), res.result.questIds);
				if (data is ItemSortData)
				{
					GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.ITEM, data.GetUniqID());
				}
				else if (data is AbilityItemSortData)
				{
					GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.ABILITY_ITEM, data.GetUniqID());
				}
				else if (data is AccessorySortData)
				{
					GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.ACCESSORY, data.GetUniqID());
				}
				this.StartCoroutine(SendGetInfos());
			}
		}, string.Empty);
	}

	private IEnumerator SendGetInfos()
	{
		bool isFinishChallengeInfo = false;
		MonoBehaviourSingleton<PartyManager>.I.SendGetChallengeInfo(delegate
		{
			isFinishChallengeInfo = true;
		});
		if (!isFinishChallengeInfo)
		{
			yield return null;
		}
		bool isFinishSendPointShop = false;
		MonoBehaviourSingleton<UserInfoManager>.I.PointShopManager.SendGetPointShops(delegate(bool isSuccess, List<PointShop> resultList)
		{
			if (isSuccess)
			{
				pointShopList = resultList;
				isFinishSendPointShop = true;
			}
		});
		if (!isFinishSendPointShop)
		{
			yield return null;
		}
		bool isRecvQuest = false;
		QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam sendParam = new QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam
		{
			enemyLevel = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.QUEST_ITEM_LEVEL_MAX
		};
		MonoBehaviourSingleton<QuestManager>.I.SendGetChallengeList(sendParam, delegate
		{
			isRecvQuest = true;
		}, isSave: false);
		while (!isRecvQuest)
		{
			yield return null;
		}
		CreateDestinationList();
		base.Initialize();
	}

	private void CreateDestinationList()
	{
		if (data == null)
		{
			return;
		}
		uint tableID = data.GetTableID();
		m_ItemDestinations.Clear();
		int num = 0;
		ItemToQuestTable.RecommendQuestData recommendQuest = Singleton<ItemToQuestTable>.I.GetRecommendQuest(tableID);
		if (recommendQuest != null)
		{
			QuestTable.QuestTableData[] recommendData = recommendQuest.recommendData;
			foreach (QuestTable.QuestTableData questTableData in recommendData)
			{
				if (questTableData != null)
				{
					m_ItemDestinations.Add(new ItemDestination
					{
						type = ItemDestination.TYPE.AdmissionQuest,
						quest = questTableData
					});
				}
			}
		}
		num = 3;
		if (num > 0)
		{
			QuestTable.QuestTableData[] happenQuestTableFromItemID = Singleton<ItemToQuestTable>.I.GetHappenQuestTableFromItemID(tableID);
			if (happenQuestTableFromItemID != null)
			{
				List<ItemDestination> list = new List<ItemDestination>();
				List<ItemDestination> list2 = new List<ItemDestination>();
				QuestTable.QuestTableData[] array = happenQuestTableFromItemID;
				foreach (QuestTable.QuestTableData questTableData2 in array)
				{
					FieldMapTable.FieldMapTableData[] fieldMapTableFromQuestIdWithClosedField = Singleton<QuestToFieldTable>.I.GetFieldMapTableFromQuestIdWithClosedField(questTableData2.questID);
					if (fieldMapTableFromQuestIdWithClosedField == null)
					{
						continue;
					}
					FieldMapTable.FieldMapTableData[] array2 = fieldMapTableFromQuestIdWithClosedField;
					foreach (FieldMapTable.FieldMapTableData fieldMapTableData in array2)
					{
						if (Singleton<ItemToFieldTable>.I.IsOpenMap(fieldMapTableData))
						{
							list2.Add(new ItemDestination
							{
								type = ItemDestination.TYPE.HappenField,
								quest = questTableData2,
								field = fieldMapTableData
							});
						}
						else
						{
							list.Add(new ItemDestination
							{
								type = ItemDestination.TYPE.HappenFieldUnknown,
								quest = questTableData2,
								field = fieldMapTableData
							});
						}
					}
				}
				list2.AddRange(list);
				int num2 = Mathf.Min(list2.Count, num);
				for (int l = 0; l < num2; l++)
				{
					m_ItemDestinations.Add(list2[l]);
				}
			}
		}
		if (m_ItemDestinations.Count < 1)
		{
			num = 3;
			QuestTable.QuestTableData[] distinctQuestFromItemID = Singleton<ItemToQuestTable>.I.GetDistinctQuestFromItemID(tableID, QUEST_TYPE.ORDER);
			if (distinctQuestFromItemID != null)
			{
				int num3 = Mathf.Min(num, distinctQuestFromItemID.Length);
				for (int m = 0; m < num3; m++)
				{
					QuestTable.QuestTableData questTableData3 = distinctQuestFromItemID[m];
					if (questTableData3 != null)
					{
						m_ItemDestinations.Add(new ItemDestination
						{
							type = ItemDestination.TYPE.DeniedQuest,
							quest = questTableData3
						});
					}
				}
			}
		}
		num = 3;
		int num4 = 0;
		if (num > 0)
		{
			ItemToFieldTable.RecommendFieldData recommendField = Singleton<ItemToFieldTable>.I.GetRecommendField(tableID, num, isExcludeNotPlayable: true);
			if (recommendField != null && recommendField.dropFieldData != null)
			{
				ItemToFieldTable.ItemDetailToFieldData[] dropFieldData = recommendField.dropFieldData;
				foreach (ItemToFieldTable.ItemDetailToFieldData itemDetailToFieldData in dropFieldData)
				{
					m_ItemDestinations.Add(new ItemDestination
					{
						type = ItemDestination.TYPE.DropField,
						recommend_field = itemDetailToFieldData,
						field = itemDetailToFieldData.mapData
					});
					num4++;
				}
			}
		}
		num = ((num4 <= 0) ? 3 : 0);
		if (num > 0)
		{
			ItemToFieldTable.CandidateField[] candidateField = Singleton<ItemToFieldTable>.I.GetCandidateField(tableID, num, isExcludeNotPlayable: true);
			if (candidateField != null)
			{
				ItemToFieldTable.CandidateField[] array3 = candidateField;
				foreach (ItemToFieldTable.CandidateField candidateField2 in array3)
				{
					m_ItemDestinations.Add(new ItemDestination
					{
						type = ItemDestination.TYPE.DropFieldUnknown,
						enemy_id = candidateField2.enemyId,
						field = candidateField2.mapData
					});
				}
			}
		}
		AddPointShopIfNeed();
		if (!IsItemBossMaterial(tableID))
		{
			return;
		}
		int num6 = 0;
		int count = m_ItemDestinations.Count;
		EnemyTable.EnemyData enemyData;
		while (true)
		{
			if (num6 >= count)
			{
				return;
			}
			ItemDestination itemDestination = m_ItemDestinations[num6];
			if (itemDestination != null)
			{
				enemyData = SearchEnemyData(itemDestination.quest);
				if (enemyData != null)
				{
					break;
				}
			}
			num6++;
		}
		AddChallengeQuestIfNeed(enemyData);
		m_ItemDestinations.Insert(0, new ItemDestination
		{
			type = ItemDestination.TYPE.GachaQuest,
			enemy_species = enemyData.enemySpecies
		});
	}

	private int GetQuestNum(QuestTable.QuestTableData q)
	{
		if (q.questType != QUEST_TYPE.ORDER)
		{
			return -1;
		}
		int num = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(q.questID)?.infoData.questData.num ?? 0;
		int num2 = 0;
		if (MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
		{
			num2 = (from g in MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList
			where g.questId == (int)q.questID
			select g).Count();
		}
		int num3 = num - num2;
		return Mathf.Max(num3, 0);
	}

	private void AddChallengeQuestIfNeed(EnemyTable.EnemyData enemyData)
	{
		List<QuestData> challengeList = MonoBehaviourSingleton<QuestManager>.I.challengeList;
		int num = 0;
		int count = challengeList.Count;
		while (true)
		{
			if (num < count)
			{
				EnemyTable.EnemyData enemyData2 = SearchEnemyData(Singleton<QuestTable>.I.GetQuestData((uint)challengeList[num].questId));
				if (enemyData2.enemySpecies == enemyData.enemySpecies)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		m_ItemDestinations.Insert(0, new ItemDestination
		{
			type = ItemDestination.TYPE.ChallengeQuest,
			enemy_species = enemyData.enemySpecies
		});
	}

	private void AddPointShopIfNeed()
	{
		int num = ItemTable.ChangeItemIdToSkillItemIdIfNeed(itemId);
		int i = 0;
		for (int count = pointShopList.Count; i < count; i++)
		{
			PointShop pointShop = pointShopList[i];
			List<PointShopItem> items = pointShop.items;
			int j = 0;
			for (int count2 = items.Count; j < count2; j++)
			{
				PointShopItem pointShopItem = items[j];
				if (pointShopItem.itemId == num && pointShopItem.isBuyable)
				{
					m_ItemDestinations.Insert(0, new ItemDestination
					{
						type = ItemDestination.TYPE.PointShop,
						pointShopData = new PointShopData(pointShop, pointShopItem)
					});
				}
			}
		}
	}

	private bool IsItemBossMaterial(uint itemId)
	{
		if (!Singleton<ItemTable>.IsValid())
		{
			return false;
		}
		ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(itemId);
		if (itemData == null)
		{
			return false;
		}
		if (itemData.enemyIconID == 0)
		{
			return false;
		}
		return true;
	}

	private EnemyTable.EnemyData SearchEnemyData(QuestTable.QuestTableData questTableData)
	{
		if (questTableData == null)
		{
			return null;
		}
		int i = 0;
		for (int num = questTableData.enemyID.Length; i < num; i++)
		{
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questTableData.enemyID[i]);
			if (enemyData != null)
			{
				return enemyData;
			}
		}
		return null;
	}

	public override void UpdateUI()
	{
		string key = "TEXT_BTN_SELL";
		SetLabelText((Enum)UI.STR_BTN_SELL, base.sectionData.GetText(key));
		SetLabelText((Enum)UI.STR_BTN_SELL_D, base.sectionData.GetText(key));
		detailBase = SetPrefab(GetCtrl(UI.OBJ_DETAIL_ROOT), "ItemDetailBase");
		SetActive((Enum)UI.OBJ_SCROLL_BAR, is_visible: true);
		if (detailBase != null)
		{
			SetFontStyle(detailBase, UI.STR_TITLE, 2);
			SetFontStyle(detailBase, UI.STR_SELL, 2);
			SetFontStyle(detailBase, UI.STR_NEED, 2);
			SetFontStyle(detailBase, UI.STR_HAVE, 2);
			SetActive(detailBase, UI.STR_SELL, data.CanSale());
			SetDepth(detailBase, UI.SCR_HOWTO, base.baseDepth + 1);
			SetLabelText(detailBase, UI.LBL_NAME, data.GetName());
			SetLabelText(detailBase, UI.LBL_HAVE_NUM, data.GetNum().ToString());
			SetLabelText(detailBase, UI.LBL_SELL, data.GetSalePrice().ToString());
			int num = 0;
			int num2 = 0;
			ItemTable.ItemData itemData = null;
			if (data is ItemSortData)
			{
				itemData = (data.GetItemData() as ItemInfo).tableData;
			}
			else if (data is AbilityItemSortData)
			{
				itemData = (data.GetItemData() as AbilityItemInfo).GetItemTableData();
			}
			num = itemData.enemyIconID;
			num2 = itemData.enemyIconID2;
			ITEM_ICON_TYPE iconType = data.GetIconType();
			int iconID = data.GetIconID();
			RARITY_TYPE? rarity = data.GetRarity();
			Transform parent = FindCtrl(detailBase, UI.OBJ_ICON_ROOT);
			ELEMENT_TYPE iconElement = data.GetIconElement();
			EQUIPMENT_TYPE? iconMagiEnableType = data.GetIconMagiEnableType();
			int num3 = -1;
			string event_name = null;
			int event_data = 0;
			bool is_new = false;
			int toggle_group = -1;
			bool is_select = false;
			string icon_under_text = null;
			bool is_equipping = false;
			int enemy_icon_id = num;
			int enemy_icon_id2 = num2;
			GET_TYPE getType = data.GetGetType();
			ItemIcon itemIcon = ItemIcon.Create(iconType, iconID, rarity, parent, iconElement, iconMagiEnableType, num3, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, enemy_icon_id, enemy_icon_id2, disable_rarity_text: false, getType);
			itemIcon.SetEnableCollider(is_enable: false);
			int count = m_ItemDestinations.Count;
			Transform ctrl = GetCtrl(UI.GRD_HOWTO);
			if (Object.op_Implicit(ctrl))
			{
				int i = 0;
				for (int childCount = ctrl.get_childCount(); i < childCount; i++)
				{
					Transform child = ctrl.GetChild(0);
					child.set_parent(null);
					Object.Destroy(child.get_gameObject());
				}
			}
			SetTable(detailBase, UI.GRD_HOWTO, "QuestListItem", count, reset: true, CreateListItem, UpdateListItem);
			bool is_visible = m_ItemDestinations.Count < 1;
			SetActive(detailBase, UI.STR_NOT_HOWTO, is_visible);
			string empty = string.Empty;
			int id = 2;
			RARITY_TYPE rarity2 = RARITY_TYPE.A;
			if (itemData.type == ITEM_TYPE.LAPIS)
			{
				rarity2 = itemData.rarity;
				id = ((!Singleton<EquipItemExceedTable>.I.IsFreeLapis(rarity2, itemData.id, itemData.eventId)) ? 1 : 0);
				if (Singleton<LimitedEquipItemExceedTable>.I.IsLimitedLapis(itemData.id))
				{
					id = 8;
				}
			}
			empty = string.Format(StringTable.Get(STRING_CATEGORY.ITEM_DETAIL, (uint)id), rarity2.ToString());
			SetLabelText(detailBase, UI.STR_NOT_HOWTO, empty);
			SetActive(detailBase, UI.SPR_NEED, GetNeedNum().HasValue);
			if (GetNeedNum().HasValue)
			{
				UIBehaviour.SetMaterialNumText(FindCtrl(detailBase, UI.LBL_HAVE_NUM), FindCtrl(detailBase, UI.LBL_NEED_NUM), data.GetNum(), GetNeedNum().Value);
			}
		}
		SetActive((Enum)UI.BTN_DETAIL_SELL, CanSell() && MonoBehaviourSingleton<ItemExchangeManager>.I.IsExchangeScene());
		SetActive((Enum)UI.BTN_SEARCH_TP, is_visible: false);
	}

	private Transform CreateListItem(int index, Transform t)
	{
		if (index >= m_ItemDestinations.Count)
		{
			return null;
		}
		if (m_ItemDestinations[index].type == ItemDestination.TYPE.AdmissionQuest && m_ItemDestinations[index].quest.questType == QUEST_TYPE.ORDER)
		{
			return Realizes("QuestListOrderItem", t);
		}
		if (m_ItemDestinations[index].type == ItemDestination.TYPE.AdmissionQuest && m_ItemDestinations[index].quest.questType == QUEST_TYPE.SERIES_ARENA)
		{
			return Realizes("QuestListSeriesArena", t);
		}
		if (m_ItemDestinations[index].type == ItemDestination.TYPE.AdmissionQuest && m_ItemDestinations[index].quest.questType == QUEST_TYPE.WAVE && m_ItemDestinations[index].quest.questType == QUEST_TYPE.WAVE_STRATEGY && m_ItemDestinations[index].quest.questType == QUEST_TYPE.SERIES && m_ItemDestinations[index].quest.questType == QUEST_TYPE.EVENT)
		{
			return Realizes("QuestListFieldItem", t);
		}
		if (m_ItemDestinations[index].type == ItemDestination.TYPE.DeniedQuest)
		{
			return Realizes("QuestListOrderItem", t);
		}
		if (m_ItemDestinations[index].type == ItemDestination.TYPE.GachaQuest)
		{
			return Realizes("QuestListGachaQuestItem", t);
		}
		if (m_ItemDestinations[index].type == ItemDestination.TYPE.ChallengeQuest)
		{
			return Realizes("QuestListChallengeGotoItem", t);
		}
		if (m_ItemDestinations[index].type == ItemDestination.TYPE.PointShop)
		{
			return Realizes("ItemDetailPointShopItem", t);
		}
		if (m_ItemDestinations[index].type == ItemDestination.TYPE.GuildRequest)
		{
			return Realizes("QuestListGuildRequestItem", t);
		}
		if (m_ItemDestinations[index].type == ItemDestination.TYPE.TradingPostQuest)
		{
			return Realizes("QuestListSearchTradingPostItem", t);
		}
		return Realizes("QuestListFieldItem", t);
	}

	private void UpdateListItem(int i, Transform t, bool is_recycle)
	{
		SetActive(t, UI.TEX_FIELD_SUB, is_visible: false);
		if (i >= m_ItemDestinations.Count)
		{
			return;
		}
		ItemDestination itemDestination = m_ItemDestinations[i];
		switch (itemDestination.type)
		{
		case ItemDestination.TYPE.Unknown:
			break;
		case ItemDestination.TYPE.DropField:
		{
			ItemToFieldTable.ItemDetailToFieldData recommend_field = itemDestination.recommend_field;
			ItemToFieldTable.ItemDetailToFieldEnemyData itemDetailToFieldEnemyData = recommend_field as ItemToFieldTable.ItemDetailToFieldEnemyData;
			ItemToFieldTable.ItemDetailToFieldPointData itemDetailToFieldPointData = recommend_field as ItemToFieldTable.ItemDetailToFieldPointData;
			if (itemDetailToFieldEnemyData == null && itemDetailToFieldPointData == null)
			{
				SetActive(t, is_visible: false);
				break;
			}
			SetActive(t, is_visible: true);
			QuestListFieldItem questListFieldItem = t.GetComponent<QuestListFieldItem>();
			if (questListFieldItem == null)
			{
				questListFieldItem = t.get_gameObject().AddComponent<QuestListFieldItem>();
			}
			questListFieldItem.InitUI();
			if (itemDetailToFieldEnemyData != null)
			{
				EnemyTable.EnemyData enemyData6 = Singleton<EnemyTable>.I.GetEnemyData(itemDetailToFieldEnemyData.enemyID[0]);
				questListFieldItem.SetUpFieldEnemy(enemyData6, recommend_field);
			}
			else if (itemDetailToFieldPointData != null)
			{
				string text5 = base.sectionData.GetText("STR_GATHER_FIELD_NAME");
				string field_name = string.Format(text5, recommend_field.mapData.mapName);
				questListFieldItem.SetUpGather(field_name, itemDetailToFieldPointData);
			}
			SetEvent(t, "JUMP_FIELD", i);
			break;
		}
		case ItemDestination.TYPE.DropFieldUnknown:
		{
			SetActive(t, UI.OBJ_FIELD_ICON, is_visible: true);
			uint enemy_id = itemDestination.enemy_id;
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData(enemy_id);
			if (enemyData != null)
			{
				SetActive(t, UI.OBJ_ENEMY, is_visible: true);
				SetLabelText(t, UI.LBL_FIELD_ENEMY_NAME, enemyData.name);
				int iconId = enemyData.iconId;
				ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, iconId, null, FindCtrl(t, UI.OBJ_ENEMY), enemyData.element);
				itemIcon.SetEnableCollider(is_enable: false);
				SetActive(t, UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
				SetElementSprite(t, UI.SPR_ELEMENT, (int)enemyData.element);
				SetElementSprite(t, UI.SPR_WEAK_ELEMENT, (int)enemyData.weakElement);
				SetActive(t, UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
				if (itemDestination.field != null)
				{
					FieldMapTable.FieldMapTableData field = itemDestination.field;
					SetLabelText(t, UI.LBL_FIELD_NAME, field.mapName);
					UITexture component = FindCtrl(t, UI.TEX_FIELD).GetComponent<UITexture>();
					ResourceLoad.LoadFieldIconTexture(component, field);
				}
			}
			SetEvent(t, "UNKNOWN_RECOMMEND", 0);
			break;
		}
		case ItemDestination.TYPE.HappenField:
		{
			int mainEnemyID2 = itemDestination.quest.GetMainEnemyID();
			FieldMapTable.FieldMapTableData field3 = itemDestination.field;
			EnemyTable.EnemyData enemyData3 = Singleton<EnemyTable>.I.GetEnemyData((uint)mainEnemyID2);
			SetActive(t, UI.OBJ_ENEMY, is_visible: true);
			string text3 = base.sectionData.GetText("HAPPEN_BOSS");
			string text4 = $"{text3}{enemyData3.name}";
			SetLabelText(t, UI.LBL_FIELD_ENEMY_NAME, text4);
			SetLabelText(t, UI.LBL_FIELD_NAME, field3.mapName);
			if (enemyData3 != null)
			{
				int iconId3 = enemyData3.iconId;
				ItemIcon itemIcon3 = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, iconId3, null, FindCtrl(t, UI.OBJ_ENEMY), enemyData3.element);
				itemIcon3.SetEnableCollider(is_enable: false);
			}
			UITexture component3 = FindCtrl(t, UI.TEX_FIELD).GetComponent<UITexture>();
			ResourceLoad.LoadFieldIconTexture(component3, field3);
			SetEvent(t, "JUMP_FIELD", i);
			break;
		}
		case ItemDestination.TYPE.HappenFieldUnknown:
		{
			SetActive(t, UI.OBJ_FIELD_ICON, is_visible: true);
			int mainEnemyID = itemDestination.quest.GetMainEnemyID();
			EnemyTable.EnemyData enemyData2 = Singleton<EnemyTable>.I.GetEnemyData((uint)mainEnemyID);
			if (enemyData2 != null)
			{
				SetActive(t, UI.OBJ_ENEMY, is_visible: true);
				string text = base.sectionData.GetText("HAPPEN_BOSS");
				string text2 = $"{text}{enemyData2.name}";
				SetLabelText(t, UI.LBL_FIELD_ENEMY_NAME, text2);
				int iconId2 = enemyData2.iconId;
				ItemIcon itemIcon2 = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, iconId2, null, FindCtrl(t, UI.OBJ_ENEMY), enemyData2.element);
				itemIcon2.SetEnableCollider(is_enable: false);
				SetActive(t, UI.SPR_ELEMENT_ROOT, enemyData2.element != ELEMENT_TYPE.MAX);
				SetElementSprite(t, UI.SPR_ELEMENT, (int)enemyData2.element);
				SetElementSprite(t, UI.SPR_WEAK_ELEMENT, (int)enemyData2.weakElement);
				SetActive(t, UI.STR_NON_WEAK_ELEMENT, enemyData2.weakElement == ELEMENT_TYPE.MAX);
				FieldMapTable.FieldMapTableData field2 = itemDestination.field;
				SetLabelText(t, UI.LBL_FIELD_NAME, field2.mapName);
				UITexture component2 = FindCtrl(t, UI.TEX_FIELD).GetComponent<UITexture>();
				ResourceLoad.LoadFieldIconTexture(component2, field2);
			}
			SetEvent(t, "UNKNOWN_RECOMMEND", 0);
			break;
		}
		case ItemDestination.TYPE.AdmissionQuest:
			if (itemDestination.quest.questType == QUEST_TYPE.ORDER)
			{
				UpdateJumpQuestButton(i, t, itemDestination);
			}
			else if (itemDestination.quest.questType == QUEST_TYPE.SERIES_ARENA)
			{
				UpdateJumpSeriesArenaButton(i, t, itemDestination);
			}
			else
			{
				UpdateJumpEventButton(i, t, itemDestination);
			}
			break;
		case ItemDestination.TYPE.DeniedQuest:
		{
			QuestTable.QuestTableData quest = itemDestination.quest;
			if (quest == null)
			{
				SetActive(t, is_visible: false);
				break;
			}
			SetActive(t, is_visible: true);
			SetLabelText(t, UI.LBL_ORDER_NUM, 0.ToString());
			SetActive(t, UI.OBJ_ICON, is_visible: false);
			SetLabelText(t, UI.LBL_QUEST_TYPE, quest.questType.ToString());
			SetLabelText(t, UI.LBL_QUEST_NUM, string.Empty);
			uint mainEnemyID3 = (uint)quest.GetMainEnemyID();
			EnemyTable.EnemyData enemyData4 = Singleton<EnemyTable>.I.GetEnemyData(mainEnemyID3);
			if (enemyData4 != null)
			{
				SetLabelText(t, UI.LBL_QUEST_NAME, enemyData4.name);
			}
			else
			{
				SetLabelText(t, UI.LBL_QUEST_NAME, quest.questText);
			}
			int j = 0;
			for (int num = 3; j < num; j++)
			{
				int num2 = j * 2;
				SetActive(t, mission[num2], is_visible: false);
				SetActive(t, mission[num2 + 1], is_visible: false);
			}
			int k = 0;
			for (int num3 = difficult.Length; k < num3; k++)
			{
				SetActive(t, difficult[k], is_visible: false);
			}
			EnemyTable.EnemyData enemyData5 = Singleton<EnemyTable>.I.GetEnemyData((uint)quest.GetMainEnemyID());
			if (enemyData5 != null)
			{
				SetActive(t, UI.OBJ_ENEMY, is_visible: true);
				int iconId4 = enemyData5.iconId;
				RARITY_TYPE? rarity = (quest.questType != QUEST_TYPE.ORDER) ? null : new RARITY_TYPE?(quest.rarity);
				ItemIcon itemIcon4 = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, iconId4, rarity, FindCtrl(t, UI.OBJ_ENEMY), enemyData5.element);
				itemIcon4.SetEnableCollider(is_enable: false);
				SetActive(t, UI.SPR_ELEMENT_ROOT, enemyData5.element != ELEMENT_TYPE.MAX);
				SetElementSprite(t, UI.SPR_ELEMENT, (int)enemyData5.element);
				SetElementSprite(t, UI.SPR_WEAK_ELEMENT, (int)enemyData5.weakElement);
				SetActive(t, UI.STR_NON_WEAK_ELEMENT, enemyData5.weakElement == ELEMENT_TYPE.MAX);
			}
			else
			{
				SetActive(t, UI.OBJ_ENEMY, is_visible: false);
				SetElementSprite(t, UI.SPR_WEAK_ELEMENT, 6);
				SetActive(t, UI.STR_NON_WEAK_ELEMENT, is_visible: true);
			}
			SetEvent(t, "ORDER_NOT_HAVE", i);
			break;
		}
		case ItemDestination.TYPE.GachaQuest:
			SetEvent(t, "JUMP_GACHA_QUEST", i);
			break;
		case ItemDestination.TYPE.ChallengeQuest:
			UpdateGridListItemChallenge(i, t);
			break;
		case ItemDestination.TYPE.PointShop:
			UpdateListItemPointShop(i, t, m_ItemDestinations[i].pointShopData);
			break;
		case ItemDestination.TYPE.GuildRequest:
			if (MonoBehaviourSingleton<GuildManager>.I.guildStatData.emblem != null && MonoBehaviourSingleton<GuildManager>.I.guildStatData.emblem.Length >= 3)
			{
				SetSprite((Enum)UI.SPR_EMBLEM_LAYER_1, GuildItemManager.I.GetItemSprite(MonoBehaviourSingleton<GuildManager>.I.guildStatData.emblem[0]));
				SetSprite((Enum)UI.SPR_EMBLEM_LAYER_2, GuildItemManager.I.GetItemSprite(MonoBehaviourSingleton<GuildManager>.I.guildStatData.emblem[1]));
				SetSprite((Enum)UI.SPR_EMBLEM_LAYER_3, GuildItemManager.I.GetItemSprite(MonoBehaviourSingleton<GuildManager>.I.guildStatData.emblem[2]));
			}
			else
			{
				SetSprite((Enum)UI.SPR_EMBLEM_LAYER_1, string.Empty);
				SetSprite((Enum)UI.SPR_EMBLEM_LAYER_2, string.Empty);
				SetSprite((Enum)UI.SPR_EMBLEM_LAYER_3, string.Empty);
			}
			SetEvent(t, "OPEN_SEND_DIALOG", null);
			break;
		case ItemDestination.TYPE.TradingPostQuest:
			SetEvent(t, "SEARCH_IN_TP", i);
			break;
		}
	}

	private void UpdateJumpQuestButton(int i, Transform t, ItemDestination destination)
	{
		QuestTable.QuestTableData table = destination.quest;
		if (table == null)
		{
			SetActive(t, is_visible: false);
			return;
		}
		SetActive(t, is_visible: true);
		if (table.questType == QUEST_TYPE.ORDER)
		{
			int questNum = GetQuestNum(table);
			SetLabelText(t, UI.LBL_ORDER_NUM, questNum.ToString());
		}
		SetActive(t, UI.OBJ_ICON, is_visible: false);
		SetLabelText(t, UI.LBL_QUEST_TYPE, table.questType.ToString());
		SetLabelText(t, UI.LBL_QUEST_NUM, string.Empty);
		SetLabelText(t, UI.LBL_QUEST_NAME, table.questText);
		ClearStatusQuest clearStatusQuest = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((ClearStatusQuest _status) => _status.questId == table.questID);
		if (clearStatusQuest == null || table.missionID == null || table.missionID.Length == 0)
		{
			int j = 0;
			for (int num = 3; j < num; j++)
			{
				int num2 = j * 2;
				SetActive(t, mission[num2], is_visible: false);
				SetActive(t, mission[num2 + 1], is_visible: false);
			}
		}
		else
		{
			int k = 0;
			for (int num3 = 3; k < num3; k++)
			{
				int num4 = k * 2;
				SetActive(t, mission[num4], k < table.missionID.Length);
				SetActive(t, mission[num4 + 1], clearStatusQuest.missionStatus[k] >= 3);
			}
		}
		int l = 0;
		for (int num5 = difficult.Length; l < num5; l++)
		{
			SetActive(t, difficult[l], l <= (int)table.difficulty);
		}
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)table.GetMainEnemyID());
		if (enemyData != null)
		{
			SetActive(t, UI.OBJ_ENEMY, is_visible: true);
			int iconId = enemyData.iconId;
			RARITY_TYPE? rarity = (table.questType != QUEST_TYPE.ORDER) ? null : new RARITY_TYPE?(table.rarity);
			ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, iconId, rarity, FindCtrl(t, UI.OBJ_ENEMY), enemyData.element);
			itemIcon.SetEnableCollider(is_enable: false);
			SetActive(t, UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
			SetElementSprite(t, UI.SPR_ELEMENT, (int)enemyData.element);
			SetElementSprite(t, UI.SPR_WEAK_ELEMENT, (int)enemyData.weakElement);
			SetActive(t, UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
		}
		else
		{
			SetActive(t, UI.OBJ_ENEMY, is_visible: false);
			SetElementSprite(t, UI.SPR_WEAK_ELEMENT, 6);
			SetActive(t, UI.STR_NON_WEAK_ELEMENT, is_visible: true);
		}
		ResetTween(t, UI.TWN_DIFFICULT_STAR);
		PlayTween(t, UI.TWN_DIFFICULT_STAR, forward: true, null, is_input_block: false);
		SetEvent(t, "JUMP_QUEST", i);
	}

	private void UpdateJumpSeriesArenaButton(int i, Transform t, ItemDestination destination)
	{
		ItemDetailSeriesArena component = t.GetComponent<ItemDetailSeriesArena>();
		component.SetUpItem(t);
		SetEvent(t, "SERIES_ARENA", i);
	}

	private void UpdateJumpEventButton(int i, Transform t, ItemDestination destination)
	{
		int mainEnemyID = destination.quest.GetMainEnemyID();
		DeliveryTable.DeliveryData deliveryData = Singleton<DeliveryTable>.I.GetDeliveryTableDataFromQuestId(destination.quest.questID);
		if (deliveryData == null)
		{
			SetActive(t, is_visible: false);
			return;
		}
		Network.EventData eventData = (from e in MonoBehaviourSingleton<QuestManager>.I.eventList
		where e.eventId == deliveryData.eventID
		select e).FirstOrDefault();
		if (eventData == null)
		{
			SetActive(t, is_visible: false);
			return;
		}
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)mainEnemyID);
		SetActive(t, UI.OBJ_ENEMY, is_visible: true);
		SetLabelText(t, UI.LBL_FIELD_ENEMY_NAME, enemyData.name);
		string text = $"{eventData.name} / {deliveryData.name}";
		SetLabelText(t, UI.LBL_FIELD_NAME, text);
		if (enemyData != null)
		{
			int iconId = enemyData.iconId;
			ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, iconId, null, FindCtrl(t, UI.OBJ_ENEMY), enemyData.element);
			itemIcon.SetEnableCollider(is_enable: false);
		}
		UITexture component = FindCtrl(t, UI.TEX_FIELD).GetComponent<UITexture>();
		SetEvent(t, "JUMP_EVENT", i);
	}

	private void UpdateGridListItemChallenge(int i, Transform t)
	{
		if (!MonoBehaviourSingleton<PartyManager>.IsValid() || MonoBehaviourSingleton<PartyManager>.I.challengeInfo == null)
		{
			SetActive(t, is_visible: false);
			return;
		}
		SetActive(t, is_visible: true);
		SetEvent(t, "JUMP_CHALLENGE_QUEST", i);
		SetActive(t, UI.OBJ_CHALLENGE_ON, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.IsSatisfy());
		SetActive(t, UI.OBJ_CHALLENGE_OFF, !MonoBehaviourSingleton<PartyManager>.I.challengeInfo.IsSatisfy());
		SetLabelText(t, UI.LBL_CHALLENGE_ON_MESSAGE, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.message);
		SetLabelText(t, UI.LBL_CHALLENGE_OFF_MESSAGE, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.message);
		SetSupportEncoding(UI.LBL_CHALLENGE_ON_MESSAGE, isEnable: true);
		SetSupportEncoding(UI.LBL_CHALLENGE_OFF_MESSAGE, isEnable: true);
	}

	private void UpdateListItemPointShop(int i, Transform t, PointShopData pointShopData)
	{
		SetEvent(t, "SELLECT_POINT_SHOP", i);
		SetPointShopIcon(t);
		ItemDetailPointShopItem component = t.GetComponent<ItemDetailPointShopItem>();
		component.SetUpItemDetailItem(pointShopData.item, pointShopData.shop, (uint)pointShopData.shop.pointShopId, pointShopData.item.needPoint <= pointShopData.shop.userPoint);
	}

	protected virtual void SetPointShopIcon(Transform t)
	{
		NPCTable.NPCData nPCData = Singleton<NPCTable>.I.GetNPCData(1);
		SetNPCIcon(t, UI.TEX_NPC, nPCData.npcModelID);
	}

	private void OnBuy(PointShopItem item, int num)
	{
		string boughtMessage = PointShopManager.GetBoughtMessage(item, num);
		GameSection.SetEventData(boughtMessage);
		GameSection.StayEvent();
		MonoBehaviourSingleton<UserInfoManager>.I.PointShopManager.SendPointShopBuy(item, selectedPointShopdata.shop, num, delegate(bool isSuccess)
		{
			if (isSuccess && !selectedPointShopdata.item.isBuyable)
			{
				m_ItemDestinations.Remove(selectedPointShopDestination);
			}
			RefreshUI();
			GameSection.ResumeEvent(isSuccess);
		});
	}

	protected void OnQuery_JUMP_QUEST()
	{
		int num = (int)GameSection.GetEventData();
		if (m_ItemDestinations == null || m_ItemDestinations.Count <= num)
		{
			return;
		}
		ItemDestination itemDestination = m_ItemDestinations[num];
		if (itemDestination.type != ItemDestination.TYPE.AdmissionQuest)
		{
			return;
		}
		QuestTable.QuestTableData quest = itemDestination.quest;
		if (quest == null)
		{
			return;
		}
		if (quest.questType == QUEST_TYPE.ORDER)
		{
			QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(quest.questID);
			if (questItem == null || questItem.infoData == null || questItem.infoData.questData.num == 0)
			{
				GameSection.ChangeEvent("ORDER_NOT_HAVE");
				return;
			}
		}
		jumpQuest = quest;
		GameSection.ChangeEvent("JUMP_QUEST_CONFIRM");
	}

	protected void OnQuery_ItemDetailJumpQuestConfirm_NO()
	{
		jumpQuest = null;
	}

	protected void OnCloseDialog_ItemDetailJumpQuestConfirm()
	{
		QuestTable.QuestTableData questTableData = jumpQuest;
		if (questTableData != null)
		{
			EventData[] array = null;
			QUEST_TYPE questType = questTableData.questType;
			if (questType == QUEST_TYPE.ORDER)
			{
				string goingHomeEvent = GameSection.GetGoingHomeEvent();
				array = new EventData[4]
				{
					new EventData(goingHomeEvent, null),
					new EventData("GACHA_QUEST_COUNTER", null),
					new EventData("TO_GACHA_QUEST_COUNTER", null),
					new EventData("SELECT_ORDER_FROM_ITEM_DETAIL", questTableData.questID)
				};
				GameSaveData.instance.lastQusetID = (int)questTableData.questID;
				GameSaveData.Save();
				MonoBehaviourSingleton<QuestManager>.I.StartHowToGetAutoEvent(questTableData.questID);
				GameSection.StopEvent();
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array);
			}
			else
			{
				GameSection.StopEvent();
			}
		}
	}

	protected void OnQuery_JUMP_GACHA_QUEST()
	{
		int num = (int)GameSection.GetEventData();
		if (m_ItemDestinations != null && m_ItemDestinations.Count > num)
		{
			ItemDestination itemDestination = m_ItemDestinations[num];
			if (itemDestination != null && itemDestination.type == ItemDestination.TYPE.GachaQuest)
			{
				searchEnemySpecies = itemDestination.enemy_species;
				GameSection.ChangeEvent("JUMP_GACHA_QUEST_CONFIRM");
			}
		}
	}

	protected void OnQuery_ItemDetailJumpGachaQuestConfirm_NO()
	{
		searchEnemySpecies = 0;
	}

	protected void OnCloseDialog_ItemDetailJumpGachaQuestConfirm()
	{
		if (searchEnemySpecies != 0)
		{
			string goingHomeEvent = GameSection.GetGoingHomeEvent();
			EventData[] autoEvents = new EventData[4]
			{
				new EventData(goingHomeEvent, null),
				new EventData("GACHA_QUEST_COUNTER", null),
				new EventData("CONDITION", null),
				new EventData("SPECIES_SEARCH_REQUEST", searchEnemySpecies)
			};
			GameSection.StopEvent();
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
		}
	}

	protected void OnQuery_JUMP_EVENT()
	{
		int num = (int)GameSection.GetEventData();
		if (num < m_ItemDestinations.Count)
		{
			ItemDestination destination = m_ItemDestinations[num];
			List<Network.EventData> list = new List<Network.EventData>(MonoBehaviourSingleton<QuestManager>.I.eventList);
			Network.EventData eventData = list.Find((Network.EventData e) => e.eventId == destination.quest.eventId);
			DeliveryTable.DeliveryData deliveryTableDataFromQuestId = Singleton<DeliveryTable>.I.GetDeliveryTableDataFromQuestId(destination.quest.questID);
			string goingHomeEvent = GameSection.GetGoingHomeEvent();
			jump_event_list_datas = new EventData[3]
			{
				new EventData(goingHomeEvent, null),
				new EventData("EVENT_COUNTER", null),
				new EventData("SELECT", destination.quest.eventId)
			};
			GameSection.ChangeEvent("JUMP_EVENT_NORMAL_CONFIRM");
		}
	}

	protected void OnQuery_SERIES_ARENA()
	{
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentSeriesArenaId(0);
		ToSeriesArena();
	}

	protected void OnQuery_JUMP_FIELD()
	{
		jump_event_list_datas = null;
		int num = (int)GameSection.GetEventData();
		if (num >= m_ItemDestinations.Count)
		{
			return;
		}
		ItemDestination itemDestination = m_ItemDestinations[num];
		if (itemDestination.type == ItemDestination.TYPE.DropField || itemDestination.type == ItemDestination.TYPE.HappenField)
		{
			FieldMapTable.FieldMapTableData table = itemDestination.field;
			if (table == null)
			{
				return;
			}
			if (table.IsEventData)
			{
				List<Network.EventData> list = new List<Network.EventData>(MonoBehaviourSingleton<QuestManager>.I.eventList);
				Network.EventData eventData = list.Find((Network.EventData e) => e.eventId == table.eventId);
				string goingHomeEvent = GameSection.GetGoingHomeEvent();
				if (MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(table.mapID) && eventData.readPrologueStory)
				{
					if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(table.jumpPortalID, check_dst_quest: false))
					{
						GameSection.StopEvent();
						return;
					}
					jumpField = table;
					GameSection.ChangeEvent("JUMP_FIELD_CONFIRM");
				}
				else
				{
					jump_event_list_datas = new EventData[3]
					{
						new EventData(goingHomeEvent, null),
						new EventData("EVENT_COUNTER", null),
						new EventData("SELECT", table.eventId)
					};
					GameSection.ChangeEvent("JUMP_EVENT_CONFIRM");
				}
				return;
			}
			if (MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(table.mapID))
			{
				if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(table.jumpPortalID, check_dst_quest: false))
				{
					GameSection.StopEvent();
					return;
				}
				jumpField = table;
				GameSection.ChangeEvent("JUMP_FIELD_CONFIRM");
				return;
			}
		}
		jumpField = null;
		GameSection.ChangeEvent("CAN_NOT_JUMP_FIELD");
	}

	protected void OnQuery_ItemDetailJumpFieldConfirm_YES()
	{
		GameSection.StayEvent();
		CoopApp.EnterField(jumpField.jumpPortalID, 0u, delegate(bool is_matching, bool is_connect, bool is_regist)
		{
			if (!is_connect)
			{
				GameSection.ChangeStayEvent("COOP_SERVER_INVALID");
				GameSection.ResumeEvent(is_resume: true);
			}
			else
			{
				jumpField = null;
				GameSection.ChangeStayEvent("TO_FIELD");
				GameSection.ResumeEvent(is_regist);
			}
		});
	}

	protected void OnQuery_ItemDetailJumpFieldConfirm_NO()
	{
		jumpField = null;
	}

	protected void OnQuery_ItemDetailJumpEventConfirm_YES()
	{
		if (jump_event_list_datas != null)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(jump_event_list_datas);
		}
	}

	protected void OnQuery_ItemDetailJumpEventConfirm_NO()
	{
		jump_event_list_datas = null;
	}

	protected void OnQuery_ItemDetailJumpEventNormalConfirm_YES()
	{
		if (jump_event_list_datas != null)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(jump_event_list_datas);
		}
	}

	protected void OnQuery_ItemDetailJumpEventNormalConfirm_NO()
	{
		jump_event_list_datas = null;
	}

	protected void OnQuery_JUMP_CHALLENGE_QUEST()
	{
		int num = (int)GameSection.GetEventData();
		if (m_ItemDestinations == null || m_ItemDestinations.Count <= num)
		{
			return;
		}
		ItemDestination itemDestination = m_ItemDestinations[num];
		if (itemDestination != null && itemDestination.type == ItemDestination.TYPE.ChallengeQuest && MonoBehaviourSingleton<PartyManager>.IsValid())
		{
			if (!MonoBehaviourSingleton<PartyManager>.I.challengeInfo.IsSatisfy())
			{
				GameSection.ChangeEvent("NO_SATISFY");
				return;
			}
			if (MonoBehaviourSingleton<PartyManager>.I.challengeInfo.num == 0)
			{
				GameSection.ChangeEvent("NUM_ZERO");
				return;
			}
			searchEnemySpecies = itemDestination.enemy_species;
			GameSection.ChangeEvent("JUMP_CHALLENGE_QUEST_CONFIRM");
		}
	}

	protected void OnQuery_ItemDetailJumpChallengeQuestConfirm_NO()
	{
		searchEnemySpecies = 0;
	}

	protected void OnCloseDialog_ItemDetailJumpChallengeQuestConfirm()
	{
		if (searchEnemySpecies != 0)
		{
			string goingHomeEvent = GameSection.GetGoingHomeEvent();
			EventData[] autoEvents = new EventData[4]
			{
				new EventData(goingHomeEvent, null),
				new EventData("CHALLENGE_COUNTER", null),
				new EventData("CONDITION", null),
				new EventData("SPECIES_SEARCH_REQUEST", searchEnemySpecies)
			};
			GameSection.StopEvent();
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
		}
	}

	private void OnQuery_SELLECT_POINT_SHOP()
	{
		int index = (int)GameSection.GetEventData();
		selectedPointShopDestination = m_ItemDestinations[index];
		PointShopData pointShopData = selectedPointShopdata = selectedPointShopDestination.pointShopData;
		object eventData = new object[3]
		{
			selectedPointShopdata.item,
			selectedPointShopdata.shop,
			new Action<PointShopItem, int>(OnBuy)
		};
		GameSection.SetEventData(eventData);
		if (selectedPointShopdata.shop.userPoint < selectedPointShopdata.item.needPoint)
		{
			GameSection.ChangeEvent("SHORTAGE_POINT");
		}
	}

	private void OnQuery_SELL()
	{
		if (!CanSell())
		{
			GameSection.ChangeEvent("NOT_SELL");
		}
		GameSection.SetEventData(data);
	}

	private bool CanSell()
	{
		if (data == null || !data.CanSale())
		{
			return false;
		}
		return true;
	}

	protected virtual int? GetNeedNum()
	{
		return null;
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_ITEM_INVENTORY) != (NOTIFY_FLAG)0L)
		{
			int haveingItemNum = MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum(data.GetTableID());
			if (data.GetNum() != haveingItemNum)
			{
				ItemInfo itemInfo = new ItemInfo();
				itemInfo.uniqueID = data.GetUniqID();
				itemInfo.tableID = data.GetTableID();
				itemInfo.tableData = Singleton<ItemTable>.I.GetItemData(itemInfo.tableID);
				itemInfo.num = haveingItemNum;
				data = new ItemSortData();
				data.SetItem(itemInfo);
			}
		}
		base.OnNotify(flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_ITEM_INVENTORY;
	}

	private void OnCloseDialog_GuildDonateSendDialog()
	{
		string s = GameSection.GetEventData() as string;
		try
		{
			int num = int.Parse(s);
			if (num > 0)
			{
				this.StartCoroutine(CRSendDonateRequest((int)data.GetTableID(), data.GetName(), string.Empty, num));
			}
		}
		catch
		{
		}
	}

	private IEnumerator CRSendDonateRequest(int itemID, string itemName, string request, int numRequest)
	{
		yield return (object)new WaitUntil((Func<bool>)(() => !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible()));
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendDonateRequest(itemID, itemName, request, numRequest, delegate(bool success)
		{
			GameSection.ResumeEvent(success);
			if (success)
			{
				backSection = true;
			}
		});
	}

	private void Update()
	{
		if (backSection && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			backSection = false;
			GameSection.BackSection();
		}
	}
}
