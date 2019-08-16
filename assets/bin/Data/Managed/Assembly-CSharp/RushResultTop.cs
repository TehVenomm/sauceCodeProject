using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushResultTop : QuestResultTop
{
	private enum UI
	{
		LBL_QUEST_NAME,
		LBL_PLAYER_LV,
		LBL_PLAYER_LVUP,
		SPR_LEVELUP,
		LBL_LVUP_NUM,
		OBJ_GET_EXP_ROOT,
		OBJ_MISSION_ROOT,
		OBJ_MISSION_NEW_CLEAR_ROOT,
		OBJ_TREASURE_ROOT,
		STR_TITLE_EXP,
		STR_TITLE_MISSION,
		STR_TITLE_REWARD,
		OBJ_EXP_REWARD_FRAME,
		LBL_EXP,
		SPR_GAUGE_UPPER,
		PBR_EXP,
		OBJ_RESULT_EXP_GAUGE_CTRL,
		OBJ_MISSION_INFO_FRAME,
		OBJ_MISSION_01,
		OBJ_MISSION_02,
		OBJ_MISSION_03,
		LBL_MISSION_NAME_01,
		LBL_MISSION_NAME_02,
		LBL_MISSION_NAME_03,
		SPR_CROWN_01,
		SPR_CROWN_02,
		SPR_CROWN_03,
		SPR_CLEARED_CROWN_01,
		SPR_CLEARED_CROWN_02,
		SPR_CLEARED_CROWN_03,
		STR_EMPTY_MISSION,
		GET_ITEM,
		GET_ITEM_2,
		OBJ_QUEST_REWARD_FRAME,
		LBL_REWARD_GOLD,
		TBL_ITEM,
		OBJ_SCROLL_VIEW,
		OBJ_SCROLL_VIEW_2,
		GRD_DROP_ITEM,
		GRD_DROP_ITEM_2,
		BTN_NEXT,
		BTN_SKIP_FULL_SCREEN,
		BTN_SKIP_IN_SCROLL,
		BTN_SKIP_IN_SCROLL_2,
		PNL_MATERIAL_INFO,
		PNL_MATERIAL_INFO_2,
		OBJ_TREASURE_ROOT_NON_MISSION,
		OBJ_POINT_SHOP_RESULT_ROOT,
		OBJ_NORMAL_POINT_SHOP_ROOT,
		OBJ_EVENT_POINT_SHOP_ROOT,
		LBL_NORMAL_GET_POINT_SHOP,
		LBL_NORMAL_TOTAL_POINT_SHOP,
		TEX_NORMAL_POINT_SHOP_ICON,
		LBL_EVENT_GET_POINT_SHOP,
		LBL_EVENT_TOTAL_POINT_SHOP,
		TEX_EVENT_POINT_SHOP_ICON,
		LBL_GUILD_REQUEST_GET_POINT,
		OBJ_TITLE,
		OBJ_WAVE,
		LBL_WAVE,
		OBJ_TIME,
		LBL_TIME,
		OBJ_MONEY,
		OBJ_COIN,
		OBJ_ARRIVAL_EFFECT_ROOT,
		OBJ_ARRIVAL_EFFECT,
		OBJ_ARRIVAL_BONUS,
		GRD_ARRIVAL_ITEM_ICON,
		STR_REWARD_TITLE,
		SPR_WAVE_01,
		SPR_WAVE_10,
		SPR_WAVE_100,
		SPR_WAVE_1000,
		TBL_DROP_ITEM,
		LBL_DROP_ITEM_WAVE,
		STR_TITLE_WAVE,
		STR_TITLE_TIME,
		LBL_EXPLORE_GET_POINT,
		LBL_EXPLORE_TOTAL_POINT,
		SPR_TITLE,
		OBJ_EXP,
		OBJ_REMAIN_TIME,
		OBJ_CLEAR_TIME,
		STR_CLEAR_TIME_NAME,
		LBL_CLEAR_TIME,
		OBJ_BEFORE_TIME,
		SPR_BEFORE_TIME_NAME,
		LBL_BEFORE_TIME,
		SPR_BESTSCORE,
		OBJ_CLEAR_EFFECT_ROOT,
		OBJ_CLEAR_EFFECT,
		OBJ_RANK_UP_ROOT,
		OBJ_RANK_UP,
		TEX_RANK_PRE,
		TEX_RANK_NEW,
		OBJ_PARTICLE,
		OBJ_CONGRATULATIONS_ROOT,
		OBJ_CONGRATULATIONS,
		OBJ_CONGRATULATIONS_PARTICLE,
		LBL_BOSS_NAME,
		TBL_GUILD_REQUEST_RESULT,
		OBJ_BONUS_POINT_SHOP,
		TXT_BONUS_POINT_ICON,
		LBL_BONUS_POINT_NUM,
		TEX_MISSION_COIN_01,
		TEX_MISSION_COIN_02,
		TEX_MISSION_COIN_03,
		SPR_CROWN01_OFF,
		SPR_CROWN02_OFF,
		SPR_CROWN03_OFF,
		SHADOW,
		BTN_NEXT_ALL,
		BTN_END_HUNT_CENTER,
		BTN_END_HUNT_LEFT,
		BTN_REPEAT_HUNT,
		LBL_BTN_REPEAT_HUNT,
		LBL_WAIT_FOR_HOST
	}

	private enum AUDIO
	{
		COUNTUP = 40000012,
		MISSION = 40000013,
		DROPITEM_BREAK = 40000014,
		DROPITEM = 40000015,
		MONEY = 40000016,
		LEVELUP = 40000017,
		ADVENT = 40000026,
		MONEY_WH = 40000027,
		ACHIEVEMENT = 40000028,
		GET_REWARD = 40000155,
		RESULT = 40000049,
		TITLE_LOGO = 40000227,
		CATEGORY = 40000228,
		POINTUP = 40000229,
		POINTREWARD = 40000230,
		ARRIVAL = 40000269,
		ARRIVAL_WAVE = 40000270
	}

	private new enum RESULT_ANIM_STATE
	{
		IDLE,
		TITLE,
		WAVE,
		TIME,
		ARRIVAL,
		ARRIVAL_NEXT,
		TREASURE,
		ITEM_ICON,
		EVENT,
		END
	}

	private ResultReward[] resultRewards;

	private PointEventCurrentData.Reward[] firstRewards;

	private bool is_skip;

	private const string ARRIVAL_EFFECT = "RushArrival";

	private const string ARRIVAL_EFFECT_NAME_BASE = "RushArrival_Wave_Txt_";

	private const float DROP_ICON_HEIGHT = 100f;

	private const float WAVE_LABEL_HEIGHT = 36f;

	private Transform material_info_t;

	private new RESULT_ANIM_STATE animState;

	private Vector3 preScrollViewPosition;

	public override void Initialize()
	{
		SetActive((Enum)UI.OBJ_ARRIVAL_EFFECT_ROOT, is_visible: false);
		SetActive((Enum)UI.OBJ_ARRIVAL_BONUS, is_visible: false);
		material_info_t = CreateMaterialInfo((Enum)UI.PNL_MATERIAL_INFO);
		base.Initialize();
	}

	protected override void InitReward()
	{
		List<ResultReward> list = new List<ResultReward>();
		dropItemNum = 0;
		dropLineNum = 0;
		eventRewardTitles = new List<string>();
		followReward = new QuestCompleteReward();
		List<PointEventCurrentData.Reward> list2 = new List<PointEventCurrentData.Reward>();
		if (MonoBehaviourSingleton<InGameManager>.I.rushRewards.Count > 0)
		{
			isVictory = true;
			foreach (QuestCompleteRewardList rushReward in MonoBehaviourSingleton<InGameManager>.I.rushRewards)
			{
				ResultReward resultReward = new ResultReward();
				DevideRewardDropAndEvent(resultReward, rushReward.drop);
				QuestCompleteReward breakPartsReward = rushReward.breakPartsReward;
				QuestCompleteReward breakReward = rushReward.breakReward;
				QuestCompleteReward order = rushReward.order;
				followReward.Add(rushReward.followReward);
				foreach (QuestCompleteReward.Item item in rushReward.first.item)
				{
					PointEventCurrentData.Reward reward = new PointEventCurrentData.Reward();
					reward.type = 3;
					reward.itemId = item.itemId;
					reward.num = item.num;
					list2.Add(reward);
				}
				foreach (QuestCompleteReward.QuestItem item2 in rushReward.first.questItem)
				{
					PointEventCurrentData.Reward reward2 = new PointEventCurrentData.Reward();
					reward2.type = 6;
					reward2.itemId = item2.questId;
					reward2.num = item2.num;
					list2.Add(reward2);
				}
				foreach (QuestCompleteReward.EquipItem item3 in rushReward.first.equipItem)
				{
					PointEventCurrentData.Reward reward3 = new PointEventCurrentData.Reward();
					reward3.type = 4;
					reward3.itemId = item3.equipItemId;
					reward3.num = item3.num;
					list2.Add(reward3);
				}
				foreach (QuestCompleteReward.AccessoryItem item4 in rushReward.first.accessoryItem)
				{
					PointEventCurrentData.Reward reward4 = new PointEventCurrentData.Reward();
					reward4.type = 14;
					reward4.itemId = item4.accessoryId;
					reward4.num = item4.num;
					list2.Add(reward4);
				}
				int money = rushReward.first.money;
				if (money > 0)
				{
					PointEventCurrentData.Reward reward5 = new PointEventCurrentData.Reward();
					reward5.type = 2;
					reward5.num = money;
					list2.Add(reward5);
				}
				int crystal = rushReward.first.crystal;
				if (crystal > 0)
				{
					PointEventCurrentData.Reward reward6 = new PointEventCurrentData.Reward();
					reward6.type = 1;
					reward6.num = crystal;
					list2.Add(reward6);
				}
				List<SortCompareData> list3 = new List<SortCompareData>();
				int start_ary_index = 0;
				start_ary_index = ResultUtility.SetDropData(list3, start_ary_index, order.item);
				start_ary_index = ResultUtility.SetDropData(list3, start_ary_index, order.equipItem);
				start_ary_index = ResultUtility.SetDropData(list3, start_ary_index, order.skillItem);
				start_ary_index = ResultUtility.SetDropData(list3, start_ary_index, order.accessoryItem);
				start_ary_index = ResultUtility.SetDropData(list3, start_ary_index, resultReward.dropReward.item);
				start_ary_index = ResultUtility.SetDropData(list3, start_ary_index, resultReward.dropReward.equipItem);
				start_ary_index = ResultUtility.SetDropData(list3, start_ary_index, resultReward.dropReward.skillItem);
				start_ary_index = ResultUtility.SetDropData(list3, start_ary_index, resultReward.dropReward.questItem);
				start_ary_index = ResultUtility.SetDropData(list3, start_ary_index, resultReward.dropReward.accessoryItem);
				start_ary_index = ResultUtility.SetDropData(list3, start_ary_index, breakReward.item);
				start_ary_index = ResultUtility.SetDropData(list3, start_ary_index, breakReward.equipItem);
				start_ary_index = ResultUtility.SetDropData(list3, start_ary_index, breakReward.skillItem);
				start_ary_index = ResultUtility.SetDropData(list3, start_ary_index, breakReward.accessoryItem);
				start_ary_index = ResultUtility.SetDropData(list3, start_ary_index, breakPartsReward.item, REWARD_CATEGORY.BREAK);
				start_ary_index = ResultUtility.SetDropData(list3, start_ary_index, breakPartsReward.equipItem, REWARD_CATEGORY.BREAK);
				start_ary_index = ResultUtility.SetDropData(list3, start_ary_index, breakPartsReward.skillItem, REWARD_CATEGORY.BREAK);
				start_ary_index = ResultUtility.SetDropData(list3, start_ary_index, breakPartsReward.accessoryItem, REWARD_CATEGORY.BREAK);
				list3.Sort((SortCompareData l, SortCompareData r) => r.GetSortValueQuestResult() - l.GetSortValueQuestResult());
				resultReward.dropItemIconData = list3.ToArray();
				dropItemNum += resultReward.dropItemIconData.Length;
				list.Add(resultReward);
			}
		}
		pointShopResultData = (MonoBehaviourSingleton<InGameManager>.I.rushPointShops ?? new List<PointShopResultData>());
		resultRewards = list.ToArray();
		firstRewards = list2.ToArray();
		dropLineNum = (dropItemNum - 1) / 5 + 1;
	}

	public override void UpdateUI()
	{
		SetFullScreenButton((Enum)UI.BTN_SKIP_FULL_SCREEN);
		SetHeight((Enum)UI.BTN_SKIP_IN_SCROLL, dropLineNum * 100);
		SetActive((Enum)UI.BTN_NEXT, is_visible: false);
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
		SetLabelText((Enum)UI.LBL_QUEST_NAME, questData.questText);
		string text = string.Format(StringTable.Get(STRING_CATEGORY.RUSH_WAVE, 10004400u), MonoBehaviourSingleton<InGameManager>.I.GetCurrentWaveNum());
		SetLabelText((Enum)UI.LBL_WAVE, text);
		SetLabelText((Enum)UI.LBL_TIME, MonoBehaviourSingleton<InGameRecorder>.I.rushRemainTimeToString);
		SetActive((Enum)UI.GET_ITEM, is_visible: true);
		int num = 0;
		if (isVictory)
		{
			List<QuestCompleteRewardList> rushRewards = MonoBehaviourSingleton<InGameManager>.I.rushRewards;
			SetTable(GetCtrl(UI.OBJ_TREASURE_ROOT), UI.TBL_DROP_ITEM, "RushWaveDropItem", resultRewards.Length, reset: true, delegate(int i, Transform t, bool is_recycle)
			{
				t.set_name("wave" + MonoBehaviourSingleton<InGameManager>.I.GetWaveNum(i));
				SetDropItemIcon(resultRewards[i].dropItemIconData, t, MonoBehaviourSingleton<InGameManager>.I.GetWaveNum(i));
			});
			for (int j = 0; j < rushRewards.Count; j++)
			{
				QuestCompleteRewardList questCompleteRewardList = rushRewards[j];
				QuestCompleteReward dropReward = resultRewards[j].dropReward;
				QuestCompleteReward breakReward = questCompleteRewardList.breakReward;
				QuestCompleteReward order = questCompleteRewardList.order;
				num = num + dropReward.money + breakReward.money + order.money;
			}
			if (firstRewards.Length > 0)
			{
				SetActive((Enum)UI.OBJ_ARRIVAL_EFFECT_ROOT, is_visible: true);
				int index = MonoBehaviourSingleton<InGameManager>.I.GetRushIndex() - ((MonoBehaviourSingleton<InGameRecorder>.I.progressEndType != InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY) ? 1 : 0);
				int waveNum = MonoBehaviourSingleton<InGameManager>.I.GetWaveNum(index);
				UISprite component = GetCtrl(UI.SPR_WAVE_01).GetComponent<UISprite>();
				UISprite component2 = GetCtrl(UI.SPR_WAVE_10).GetComponent<UISprite>();
				UISprite component3 = GetCtrl(UI.SPR_WAVE_100).GetComponent<UISprite>();
				UISprite component4 = GetCtrl(UI.SPR_WAVE_1000).GetComponent<UISprite>();
				string text2 = waveNum.ToString("D4");
				component.spriteName = "RushArrival_Wave_Txt_" + text2[3];
				component2.spriteName = "RushArrival_Wave_Txt_" + text2[2];
				component3.spriteName = ((waveNum < 100) ? string.Empty : ("RushArrival_Wave_Txt_" + text2[1]));
				component4.spriteName = ((waveNum < 1000) ? string.Empty : ("RushArrival_Wave_Txt_" + text2[0]));
				SetActive((Enum)UI.OBJ_ARRIVAL_EFFECT_ROOT, is_visible: false);
				SetActive((Enum)UI.OBJ_ARRIVAL_BONUS, is_visible: true);
				SetGrid(UI.GRD_ARRIVAL_ITEM_ICON, "ItemIconReward", firstRewards.Length, reset: true, delegate(int i, Transform t, bool is_recycle)
				{
					PointEventCurrentData.Reward reward = firstRewards[i];
					ItemIcon.CreateRewardItemIcon((REWARD_TYPE)reward.type, (uint)reward.itemId, t, reward.num);
					t.Find("itemNum").GetComponent<UILabel>().text = "×" + firstRewards[i].num;
				});
				SetActive((Enum)UI.OBJ_ARRIVAL_BONUS, is_visible: false);
			}
		}
		SetLabelText((Enum)UI.LBL_REWARD_GOLD, num.ToString("N0"));
		bool flag = pointShopResultData.Count > 0;
		SetActive((Enum)UI.OBJ_POINT_SHOP_RESULT_ROOT, flag);
		if (flag)
		{
			SetGrid(UI.OBJ_POINT_SHOP_RESULT_ROOT, "QuestResultPointShop", pointShopResultData.Count, reset: true, delegate(int i, Transform t, bool b)
			{
				ResetTween(t);
				PointShopResultData pointShopResultData = base.pointShopResultData[i];
				SetActive(t, UI.OBJ_NORMAL_POINT_SHOP_ROOT, !pointShopResultData.isEvent);
				if (!pointShopResultData.isEvent)
				{
					SetLabelText(t, UI.LBL_NORMAL_GET_POINT_SHOP, string.Format("+" + StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopResultData.getPoint));
					SetLabelText(t, UI.LBL_NORMAL_TOTAL_POINT_SHOP, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopResultData.totalPoint));
					UITexture component6 = FindCtrl(t, UI.TEX_NORMAL_POINT_SHOP_ICON).GetComponent<UITexture>();
					ResourceLoad.LoadPointIconImageTexture(component6, (uint)pointShopResultData.pointShopId);
				}
				SetActive(t, UI.OBJ_EVENT_POINT_SHOP_ROOT, pointShopResultData.isEvent);
				if (pointShopResultData.isEvent)
				{
					SetLabelText(t, UI.LBL_EVENT_GET_POINT_SHOP, string.Format("+" + StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopResultData.getPoint));
					SetLabelText(t, UI.LBL_EVENT_TOTAL_POINT_SHOP, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopResultData.totalPoint));
					UITexture component7 = FindCtrl(t, UI.TEX_EVENT_POINT_SHOP_ICON).GetComponent<UITexture>();
					ResourceLoad.LoadPointIconImageTexture(component7, (uint)pointShopResultData.pointShopId);
				}
			});
		}
		if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
		{
			UIVirtualScreen componentInChildren = this.GetComponentInChildren<UIVirtualScreen>();
			UIWidget component5 = GetCtrl(UI.SHADOW).GetComponent<UIWidget>();
			if (componentInChildren != null && component5 != null)
			{
				component5.width = (int)componentInChildren.ScreenWidthFull;
				component5.height = (int)componentInChildren.ScreenHeightFull;
			}
		}
		this.StartCoroutine(PlayAnimation());
	}

	private ItemIcon CreateItemIcon(SortCompareData dropItem, Transform o, string event_name, int i)
	{
		ITEM_ICON_TYPE iTEM_ICON_TYPE = ITEM_ICON_TYPE.NONE;
		RARITY_TYPE? rARITY_TYPE = null;
		ELEMENT_TYPE eLEMENT_TYPE = ELEMENT_TYPE.MAX;
		EQUIPMENT_TYPE? eQUIPMENT_TYPE = null;
		int num = -1;
		int num2 = -1;
		if (dropItem != null)
		{
			iTEM_ICON_TYPE = dropItem.GetIconType();
			num = dropItem.GetIconID();
			rARITY_TYPE = dropItem.GetRarity();
			eLEMENT_TYPE = dropItem.GetIconElement();
			eQUIPMENT_TYPE = dropItem.GetIconMagiEnableType();
			num2 = dropItem.GetNum();
			if (num2 == 1)
			{
				num2 = -1;
			}
		}
		bool flag = false;
		switch (iTEM_ICON_TYPE)
		{
		case ITEM_ICON_TYPE.ITEM:
		case ITEM_ICON_TYPE.QUEST_ITEM:
		{
			ulong uniqID = dropItem.GetUniqID();
			if (uniqID != 0)
			{
				flag = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iTEM_ICON_TYPE, dropItem.GetUniqID());
			}
			break;
		}
		default:
			flag = true;
			break;
		case ITEM_ICON_TYPE.NONE:
			break;
		}
		int num3 = 0;
		int num4 = 0;
		if (iTEM_ICON_TYPE == ITEM_ICON_TYPE.ITEM)
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(dropItem.GetTableID());
			num3 = itemData.enemyIconID;
			num4 = itemData.enemyIconID2;
		}
		ItemIcon itemIcon = null;
		if (dropItem.GetIconType() == ITEM_ICON_TYPE.QUEST_ITEM)
		{
			ItemIcon.ItemIconCreateParam itemIconCreateParam = new ItemIcon.ItemIconCreateParam();
			itemIconCreateParam.icon_type = dropItem.GetIconType();
			itemIconCreateParam.icon_id = dropItem.GetIconID();
			itemIconCreateParam.rarity = dropItem.GetRarity();
			itemIconCreateParam.parent = o;
			itemIconCreateParam.element = dropItem.GetIconElement();
			itemIconCreateParam.magi_enable_equip_type = dropItem.GetIconMagiEnableType();
			itemIconCreateParam.num = dropItem.GetNum();
			itemIconCreateParam.enemy_icon_id = num3;
			itemIconCreateParam.enemy_icon_id2 = num4;
			itemIconCreateParam.questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST;
			itemIcon = ItemIcon.Create(itemIconCreateParam);
		}
		else
		{
			ITEM_ICON_TYPE icon_type = iTEM_ICON_TYPE;
			int icon_id = num;
			RARITY_TYPE? rarity = rARITY_TYPE;
			ELEMENT_TYPE element = eLEMENT_TYPE;
			EQUIPMENT_TYPE? magi_enable_icon_type = eQUIPMENT_TYPE;
			int num5 = num2;
			bool is_new = flag;
			int toggle_group = -1;
			bool is_select = false;
			string icon_under_text = null;
			bool is_equipping = false;
			int enemy_icon_id = num3;
			int enemy_icon_id2 = num4;
			GET_TYPE getType = dropItem.GetGetType();
			itemIcon = ItemIcon.Create(icon_type, icon_id, rarity, o, element, magi_enable_icon_type, num5, event_name, i, is_new, toggle_group, is_select, icon_under_text, is_equipping, enemy_icon_id, enemy_icon_id2, disable_rarity_text: false, getType);
		}
		itemIcon.SetRewardBG(is_visible: true);
		itemIcon.SetRewardCategoryInfo(dropItem.GetCategory());
		Transform ctrl = GetCtrl(UI.PNL_MATERIAL_INFO);
		MaterialInfoButton.Set(itemIcon.transform, material_info_t, dropItem.GetMaterialType(), dropItem.GetTableID(), base.sectionData.sectionName, ctrl);
		return itemIcon;
	}

	private void SetDropItemIcon(SortCompareData[] dropItemList, Transform t_grid, int wave)
	{
		if (dropItemList != null)
		{
			string text = string.Format(StringTable.Get(STRING_CATEGORY.RUSH_WAVE, 10004400u), wave);
			SetLabelText(t_grid, UI.LBL_DROP_ITEM_WAVE, text);
			QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)MonoBehaviourSingleton<InGameManager>.I.GetRushQuestId(wave));
			string text2 = "Lv" + questData.GetMainEnemyLv().ToString() + Singleton<EnemyTable>.I.GetEnemyName((uint)questData.GetMainEnemyID());
			SetLabelText(t_grid, UI.LBL_BOSS_NAME, text2);
			SetGrid(t_grid, UI.GRD_DROP_ITEM, null, dropItemList.Length, reset: true, delegate(int i, Transform o, bool is_recycle)
			{
				ItemIcon icon = null;
				if (i < dropItemList.Length)
				{
					icon = CreateItemIcon(dropItemList[i], o, "DROP", i);
				}
				Transform val = SetPrefab(o, "QuestResultDropIconOpener");
				QuestResultDropIconOpener.Info info2 = new QuestResultDropIconOpener.Info
				{
					IsRare = ResultUtility.IsRare(dropItemList[i]),
					IsBroken = ResultUtility.IsBreakReward(dropItemList[i])
				};
				val.GetComponent<QuestResultDropIconOpener>().Initialized(icon, info2, delegate(Transform t, QuestResultDropIconOpener.Info info, bool is_skip)
				{
					string ui_effect_name = "ef_ui_dropitem_silver_01";
					if (info.IsBroken)
					{
						ui_effect_name = "ef_ui_dropitem_red_01";
					}
					else if (info.IsRare)
					{
						ui_effect_name = "ef_ui_dropitem_gold_01";
					}
					SetVisibleWidgetOneShotEffect(GetCtrl(UI.OBJ_SCROLL_VIEW), t, ui_effect_name);
				});
			});
		}
	}

	protected override void Update()
	{
	}

	private IEnumerator PlayAnimation()
	{
		is_skip = false;
		animState = RESULT_ANIM_STATE.TITLE;
		PlayAudio(AUDIO.ADVENT);
		PlayTween((Enum)UI.OBJ_TITLE, forward: true, (EventDelegate.Callback)delegate
		{
			animState = RESULT_ANIM_STATE.IDLE;
		}, is_input_block: false, 0);
		while (animState != 0 && !is_skip)
		{
			yield return null;
		}
		animState = RESULT_ANIM_STATE.WAVE;
		PlayAudio(AUDIO.ACHIEVEMENT);
		PlayTween((Enum)UI.OBJ_WAVE, forward: true, (EventDelegate.Callback)delegate
		{
			animState = RESULT_ANIM_STATE.IDLE;
		}, is_input_block: false, 0);
		while (animState != 0 && !is_skip)
		{
			yield return null;
		}
		animState = RESULT_ANIM_STATE.TIME;
		PlayAudio(AUDIO.ACHIEVEMENT);
		PlayTween((Enum)UI.OBJ_TIME, forward: true, (EventDelegate.Callback)delegate
		{
			animState = RESULT_ANIM_STATE.IDLE;
		}, is_input_block: false, 0);
		while (animState != 0 && !is_skip)
		{
			yield return null;
		}
		if (firstRewards.Length > 0)
		{
			animState = RESULT_ANIM_STATE.ARRIVAL;
			SetActive((Enum)UI.OBJ_ARRIVAL_EFFECT_ROOT, is_visible: true);
			SetActive((Enum)UI.OBJ_ARRIVAL_BONUS, is_visible: true);
			PlayAudio(AUDIO.ARRIVAL);
			PlayTween((Enum)UI.OBJ_ARRIVAL_EFFECT, forward: true, (EventDelegate.Callback)null, is_input_block: false, 1);
			PlayTween((Enum)UI.OBJ_ARRIVAL_EFFECT, forward: true, (EventDelegate.Callback)delegate
			{
				PlayAudio(AUDIO.ARRIVAL_WAVE);
			}, is_input_block: false, 2);
			PlayTween((Enum)UI.OBJ_ARRIVAL_EFFECT, forward: true, (EventDelegate.Callback)delegate
			{
				PlayAudio(AUDIO.ARRIVAL_WAVE);
			}, is_input_block: false, 3);
			PlayTween((Enum)UI.OBJ_ARRIVAL_BONUS, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
			SetActive((Enum)UI.BTN_NEXT, is_visible: true);
			is_skip = false;
			while (!is_skip)
			{
				yield return null;
			}
			animState = RESULT_ANIM_STATE.ARRIVAL_NEXT;
			PlayTween((Enum)UI.OBJ_ARRIVAL_EFFECT, forward: true, (EventDelegate.Callback)delegate
			{
				animState = RESULT_ANIM_STATE.IDLE;
			}, is_input_block: false, 4);
			while (animState != 0 && !is_skip)
			{
				yield return null;
			}
			is_skip = false;
			SetActive((Enum)UI.OBJ_ARRIVAL_EFFECT_ROOT, is_visible: false);
			SetActive((Enum)UI.OBJ_ARRIVAL_BONUS, is_visible: false);
		}
		animState = RESULT_ANIM_STATE.TREASURE;
		PlayAudio(AUDIO.ACHIEVEMENT);
		if (pointShopResultData.Count > 0)
		{
			IEnumerator enumerator = GetCtrl(UI.OBJ_POINT_SHOP_RESULT_ROOT).get_transform().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform t = enumerator.Current;
					PlayTween(t);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
		PlayTween((Enum)UI.OBJ_MONEY, forward: true, (EventDelegate.Callback)null, is_input_block: true, 0);
		PlayTween((Enum)UI.OBJ_TREASURE_ROOT, forward: true, (EventDelegate.Callback)delegate
		{
			animState = RESULT_ANIM_STATE.IDLE;
		}, is_input_block: false, 0);
		while (animState != 0 && !is_skip)
		{
			yield return null;
		}
		is_skip = false;
		animState = RESULT_ANIM_STATE.ITEM_ICON;
		this.StartCoroutine(PlayItemAnimation(delegate
		{
			animState = RESULT_ANIM_STATE.IDLE;
		}));
		while (animState != 0 && !is_skip)
		{
			yield return null;
		}
		animState = RESULT_ANIM_STATE.EVENT;
		OpenAllEventRewardDialog(delegate
		{
			OpenMutualFollowBonusDialog(delegate
			{
				animState = RESULT_ANIM_STATE.IDLE;
			});
		});
		while (animState != 0 && !is_skip)
		{
			yield return null;
		}
		is_skip = true;
		animState = RESULT_ANIM_STATE.END;
		VisibleEndButton();
	}

	protected override void VisibleEndButton()
	{
		SetActive((Enum)UI.BTN_NEXT, is_visible: true);
		SetActive((Enum)UI.BTN_SKIP_FULL_SCREEN, is_visible: false);
		SetActive((Enum)UI.BTN_SKIP_IN_SCROLL, is_visible: false);
	}

	private IEnumerator PlayItemAnimation(Action callback)
	{
		for (int wave = 0; wave < resultRewards.Length; wave++)
		{
			if (wave == 0)
			{
				Transform ctrl = GetCtrl(UI.OBJ_SCROLL_VIEW);
				ctrl.GetComponent<UIScrollView>().ResetPosition();
				preScrollViewPosition = ctrl.get_localPosition();
			}
			else
			{
				animTimer = 0f;
				while (animTimer < 0.4f && !is_skip)
				{
					animTimer += Time.get_deltaTime();
					yield return null;
				}
				int preWaveIconRowNum = Mathf.CeilToInt((float)resultRewards[wave - 1].dropItemIconData.Length / 5f);
				float scroll_delta = 36f + 100f * (float)preWaveIconRowNum;
				Scroll(scroll_delta);
			}
			for (int i = 0; i < resultRewards[wave].dropItemIconData.Length; i++)
			{
				animTimer = 0f;
				while (animTimer < 0.4f && !is_skip)
				{
					animTimer += Time.get_deltaTime();
					yield return null;
				}
				VisibleItemIcon(wave, i, is_skip);
			}
		}
		callback();
	}

	private void Scroll(float delta)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		Vector3 pos = preScrollViewPosition + Vector3.get_up() * delta;
		SpringPanel.Begin(GetCtrl(UI.OBJ_SCROLL_VIEW).get_gameObject(), pos, 8f);
		preScrollViewPosition = pos;
	}

	private void VisibleItemIcon(int wave, int index, bool is_skip = false)
	{
		Transform child = GetChild((Enum)UI.TBL_DROP_ITEM, wave);
		Transform child2 = GetChild(child, UI.GRD_DROP_ITEM, index);
		QuestResultDropIconOpener componentInChildren = child2.get_gameObject().GetComponentInChildren<QuestResultDropIconOpener>();
		if (!(componentInChildren == null))
		{
			PlayAudio(AUDIO.DROPITEM);
			componentInChildren.StartEffect(is_skip);
		}
	}

	private void DevideRewardDropAndEvent(ResultReward resultReward, QuestCompleteReward reward)
	{
		resultReward.dropReward = new QuestCompleteReward();
		resultReward.eventReward = new QuestCompleteReward();
		List<string> list = new List<string>();
		resultReward.dropReward.exp = reward.exp;
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < reward.eventPrice.Count; i++)
		{
			num += reward.eventPrice[i].gold;
			num2 += reward.eventPrice[i].gold;
			resultReward.eventReward.eventPrice.Add(reward.eventPrice[i]);
			list.Add(reward.eventPrice[i].rewardTitle);
		}
		resultReward.dropReward.money = Mathf.Max(0, reward.money - num);
		resultReward.dropReward.crystal = Mathf.Max(0, reward.crystal - num2);
		for (int j = 0; j < reward.item.Count; j++)
		{
			if (string.IsNullOrEmpty(reward.item[j].rewardTitle))
			{
				resultReward.dropReward.item.Add(reward.item[j]);
				continue;
			}
			resultReward.eventReward.item.Add(reward.item[j]);
			list.Add(reward.item[j].rewardTitle);
		}
		for (int k = 0; k < reward.skillItem.Count; k++)
		{
			if (string.IsNullOrEmpty(reward.skillItem[k].rewardTitle))
			{
				resultReward.dropReward.skillItem.Add(reward.skillItem[k]);
				continue;
			}
			resultReward.eventReward.skillItem.Add(reward.skillItem[k]);
			list.Add(reward.skillItem[k].rewardTitle);
		}
		for (int l = 0; l < reward.equipItem.Count; l++)
		{
			if (string.IsNullOrEmpty(reward.equipItem[l].rewardTitle))
			{
				resultReward.dropReward.equipItem.Add(reward.equipItem[l]);
				continue;
			}
			resultReward.eventReward.equipItem.Add(reward.equipItem[l]);
			list.Add(reward.equipItem[l].rewardTitle);
		}
		for (int m = 0; m < reward.questItem.Count; m++)
		{
			if (string.IsNullOrEmpty(reward.questItem[m].rewardTitle))
			{
				resultReward.dropReward.questItem.Add(reward.questItem[m]);
				continue;
			}
			resultReward.eventReward.questItem.Add(reward.questItem[m]);
			list.Add(reward.questItem[m].rewardTitle);
		}
		for (int n = 0; n < reward.accessoryItem.Count; n++)
		{
			if (string.IsNullOrEmpty(reward.accessoryItem[n].rewardTitle))
			{
				resultReward.dropReward.accessoryItem.Add(reward.accessoryItem[n]);
				continue;
			}
			resultReward.eventReward.accessoryItem.Add(reward.accessoryItem[n]);
			list.Add(reward.accessoryItem[n].rewardTitle);
		}
		for (int num3 = 0; num3 < list.Count; num3++)
		{
			if (!eventRewardTitles.Contains(list[num3]))
			{
				eventRewardTitles.Add(list[num3]);
			}
		}
	}

	private void OnQuery_SKIP()
	{
		switch (animState)
		{
		case RESULT_ANIM_STATE.TITLE:
		case RESULT_ANIM_STATE.WAVE:
		case RESULT_ANIM_STATE.TIME:
		case RESULT_ANIM_STATE.ARRIVAL:
			SkipTween((Enum)UI.OBJ_TITLE, forward: true, 0);
			SkipTween((Enum)UI.OBJ_WAVE, forward: true, 0);
			SkipTween((Enum)UI.OBJ_TIME, forward: true, 0);
			SkipTween((Enum)UI.OBJ_ARRIVAL_EFFECT, forward: true, 1);
			SkipTween((Enum)UI.OBJ_ARRIVAL_EFFECT, forward: true, 2);
			SkipTween((Enum)UI.OBJ_ARRIVAL_EFFECT, forward: true, 3);
			break;
		case RESULT_ANIM_STATE.ARRIVAL_NEXT:
			SkipTween((Enum)UI.OBJ_ARRIVAL_EFFECT, forward: true, 4);
			SkipTween((Enum)UI.OBJ_ARRIVAL_BONUS, forward: true, 0);
			break;
		case RESULT_ANIM_STATE.TREASURE:
			SkipTween((Enum)UI.OBJ_POINT_SHOP_RESULT_ROOT, forward: true, 0);
			SkipTween((Enum)UI.OBJ_MONEY, forward: true, 0);
			SkipTween((Enum)UI.OBJ_TREASURE_ROOT, forward: true, 0);
			break;
		}
		is_skip = true;
		GameSection.StopEvent();
	}

	private void OnQuery_NEXT()
	{
		if (animState != 0 && animState != RESULT_ANIM_STATE.END)
		{
			OnQuery_SKIP();
		}
	}

	private void PlayAudio(AUDIO type)
	{
		SoundManager.PlayOneShotUISE((int)type);
	}

	private new void OpenAllEventRewardDialog(Action endCallback)
	{
		eventRewardIndex = 0;
		eventRewardList = new List<QuestCompleteReward>();
		for (int i = 0; i < eventRewardTitles.Count; i++)
		{
			QuestCompleteReward item = new QuestCompleteReward();
			eventRewardList.Add(item);
		}
		ResultReward[] array = resultRewards;
		foreach (ResultReward resultReward in array)
		{
			QuestCompleteReward eventReward = resultReward.eventReward;
			for (int k = 0; k < eventReward.eventPrice.Count; k++)
			{
				for (int l = 0; l < eventRewardTitles.Count; l++)
				{
					if (eventRewardTitles[l] == eventReward.eventPrice[k].rewardTitle)
					{
						eventRewardList[l].eventPrice.Add(eventReward.eventPrice[k]);
					}
				}
			}
			for (int m = 0; m < eventReward.item.Count; m++)
			{
				for (int n = 0; n < eventRewardTitles.Count; n++)
				{
					if (eventRewardTitles[n] == eventReward.item[m].rewardTitle)
					{
						eventRewardList[n].item.Add(eventReward.item[m]);
					}
				}
			}
			for (int num = 0; num < eventReward.skillItem.Count; num++)
			{
				for (int num2 = 0; num2 < eventRewardTitles.Count; num2++)
				{
					if (eventRewardTitles[num2] == eventReward.skillItem[num].rewardTitle)
					{
						eventRewardList[num2].skillItem.Add(eventReward.skillItem[num]);
					}
				}
			}
			for (int num3 = 0; num3 < eventReward.equipItem.Count; num3++)
			{
				for (int num4 = 0; num4 < eventRewardTitles.Count; num4++)
				{
					if (eventRewardTitles[num4] == eventReward.equipItem[num3].rewardTitle)
					{
						eventRewardList[num4].equipItem.Add(eventReward.equipItem[num3]);
					}
				}
			}
			for (int num5 = 0; num5 < eventReward.questItem.Count; num5++)
			{
				for (int num6 = 0; num6 < eventRewardTitles.Count; num6++)
				{
					if (eventRewardTitles[num6] == eventReward.questItem[num5].rewardTitle)
					{
						eventRewardList[num6].questItem.Add(eventReward.questItem[num5]);
					}
				}
			}
			for (int num7 = 0; num7 < eventReward.accessoryItem.Count; num7++)
			{
				for (int num8 = 0; num8 < eventRewardTitles.Count; num8++)
				{
					if (eventRewardTitles[num8] == eventReward.accessoryItem[num7].rewardTitle)
					{
						eventRewardList[num8].accessoryItem.Add(eventReward.accessoryItem[num7]);
					}
				}
			}
		}
		if (eventRewardList.Count == 0)
		{
			endCallback?.Invoke();
			return;
		}
		OpenEventRewardDialog(eventRewardList[eventRewardIndex], eventRewardTitles[eventRewardIndex], endCallback);
		eventRewardIndex++;
	}

	private void OpenMutualFollowBonusDialog(Action end_callback)
	{
		if (base.followReward != null)
		{
			QuestCompleteReward followReward = base.followReward;
			base.followReward = null;
			List<SortCompareData> tmp = new List<SortCompareData>();
			int start_ary_index = 0;
			int gold = followReward.money;
			int crystal = followReward.crystal;
			int exp = followReward.exp;
			start_ary_index = ResultUtility.SetDropData(tmp, start_ary_index, followReward.item);
			start_ary_index = ResultUtility.SetDropData(tmp, start_ary_index, followReward.equipItem);
			start_ary_index = ResultUtility.SetDropData(tmp, start_ary_index, followReward.skillItem);
			start_ary_index = ResultUtility.SetDropData(tmp, start_ary_index, followReward.questItem);
			if (ResultUtility.SetDropData(tmp, start_ary_index, followReward.accessoryItem) == 0 && crystal == 0 && gold == 0 && exp == 0)
			{
				end_callback?.Invoke();
				return;
			}
			followBonusCallback = end_callback;
			if (!QuestResultTop.IsExecuteNowSceneEvent(GetSceneName()))
			{
				this.StartCoroutine(ExecEndDialogEvent(GetSceneName(), delegate
				{
					DispatchEvent("MUTUAL_FOLLOW_BONUS", new object[3]
					{
						tmp,
						gold,
						crystal
					});
				}));
			}
			else
			{
				DispatchEvent("MUTUAL_FOLLOW_BONUS", new object[3]
				{
					tmp,
					gold,
					crystal
				});
			}
		}
		else
		{
			end_callback?.Invoke();
		}
	}

	protected override string GetSceneName()
	{
		return "RushResultTop";
	}
}
