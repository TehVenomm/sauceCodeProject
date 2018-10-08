using System;
using UnityEngine;

public class GuildRequestResult : QuestResultTop
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
		START_BONUS_TIME = 40000268
	}

	private UI dropItemSCR = UI.OBJ_SCROLL_VIEW;

	private GuildRequestCompleteModel.Param guildRequestCompleteData;

	public override void Initialize()
	{
		guildRequestCompleteData = (GameSection.GetEventData() as GuildRequestCompleteModel.Param);
		uint beforeQuestId = MonoBehaviourSingleton<GuildRequestManager>.I.GetBeforeQuestId();
		MonoBehaviourSingleton<QuestManager>.I.SetCompleteDataFromGuildRequest(beforeQuestId, guildRequestCompleteData);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.BTN_NEXT, false);
		SetActive((Enum)UI.OBJ_BONUS_POINT_SHOP, false);
		SetGrid(UI.GRD_DROP_ITEM, null, dropItemIconData.Length, true, delegate(int i, Transform o, bool is_recycle)
		{
			//IL_0287: Unknown result type (might be due to invalid IL or missing references)
			//IL_028c: Unknown result type (might be due to invalid IL or missing references)
			ITEM_ICON_TYPE iTEM_ICON_TYPE = ITEM_ICON_TYPE.NONE;
			RARITY_TYPE? rarity = null;
			ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
			EQUIPMENT_TYPE? magi_enable_icon_type = null;
			int icon_id = -1;
			int num = -1;
			if (i < dropItemIconData.Length && dropItemIconData[i] != null)
			{
				iTEM_ICON_TYPE = dropItemIconData[i].GetIconType();
				icon_id = dropItemIconData[i].GetIconID();
				rarity = dropItemIconData[i].GetRarity();
				element = dropItemIconData[i].GetIconElement();
				magi_enable_icon_type = dropItemIconData[i].GetIconMagiEnableType();
				num = dropItemIconData[i].GetNum();
				if (num == 1)
				{
					num = -1;
				}
			}
			bool is_new = false;
			switch (iTEM_ICON_TYPE)
			{
			case ITEM_ICON_TYPE.ITEM:
			case ITEM_ICON_TYPE.QUEST_ITEM:
			{
				ulong uniqID = dropItemIconData[i].GetUniqID();
				if (uniqID != 0L)
				{
					is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iTEM_ICON_TYPE, dropItemIconData[i].GetUniqID());
				}
				break;
			}
			default:
				is_new = true;
				break;
			case ITEM_ICON_TYPE.NONE:
				break;
			}
			int enemy_icon_id = 0;
			if (iTEM_ICON_TYPE == ITEM_ICON_TYPE.ITEM)
			{
				ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(dropItemIconData[i].GetTableID());
				enemy_icon_id = itemData.enemyIconID;
			}
			ItemIcon itemIcon = null;
			if (dropItemIconData[i].GetIconType() == ITEM_ICON_TYPE.QUEST_ITEM)
			{
				itemIcon = ItemIcon.Create(new ItemIcon.ItemIconCreateParam
				{
					icon_type = dropItemIconData[i].GetIconType(),
					icon_id = dropItemIconData[i].GetIconID(),
					rarity = new RARITY_TYPE?(dropItemIconData[i].GetRarity()),
					parent = o,
					element = dropItemIconData[i].GetIconElement(),
					magi_enable_equip_type = dropItemIconData[i].GetIconMagiEnableType(),
					num = dropItemIconData[i].GetNum(),
					enemy_icon_id = enemy_icon_id,
					questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST
				});
			}
			else
			{
				GET_TYPE getType = dropItemIconData[i].GetGetType();
				itemIcon = ItemIcon.Create(iTEM_ICON_TYPE, icon_id, rarity, o, element, magi_enable_icon_type, num, "DROP", i, is_new, -1, false, null, false, enemy_icon_id, 0, false, getType);
			}
			itemIcon.SetRewardBG(true);
			itemIcon.SetRewardCategoryInfo(dropItemIconData[i].GetCategory());
			SetMaterialInfo(itemIcon.transform, dropItemIconData[i].GetMaterialType(), dropItemIconData[i].GetTableID(), GetCtrl(UI.PNL_MATERIAL_INFO));
			itemIcon.transform.FindChild("MaterialInfo").get_gameObject().SetActive(false);
			Transform val = SetPrefab(o, "QuestResultDropIconOpener", true);
			QuestResultDropIconOpener.Info info2 = new QuestResultDropIconOpener.Info
			{
				IsRare = ResultUtility.IsRare(dropItemIconData[i]),
				IsBroken = ResultUtility.IsBreakReward(dropItemIconData[i])
			};
			val.GetComponent<QuestResultDropIconOpener>().Initialized(itemIcon, info2, delegate(Transform t, QuestResultDropIconOpener.Info info, bool is_skip)
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
				SetVisibleWidgetOneShotEffect(GetCtrl(dropItemSCR), t, ui_effect_name);
			});
		});
		base.GetComponent<UITable>((Enum)UI.TBL_GUILD_REQUEST_RESULT).Reposition();
		TreasureStart();
	}

	protected override void TreasureStart()
	{
		animState = RESULT_ANIM_STATE.TREASURE;
		startDropDirection = true;
	}

	protected override void AnimationEnd()
	{
		animState = RESULT_ANIM_STATE.END;
		animationEnd = true;
		if (guildRequestCompleteData.bonusPointShop != null && guildRequestCompleteData.bonusPointShop.Count > 0)
		{
			SetLabelText((Enum)UI.LBL_BONUS_POINT_NUM, guildRequestCompleteData.bonusPointShop[0].getPoint.ToString());
			SetActive((Enum)UI.OBJ_BONUS_POINT_SHOP, true);
			base.GetComponent<UITable>((Enum)UI.TBL_GUILD_REQUEST_RESULT).Reposition();
			SetScroll((Enum)dropItemSCR, animScrollValue);
		}
		OpenPointEvent(delegate
		{
			VisibleEndButton();
		});
	}

	protected override void VisibleEndButton()
	{
		SetActive((Enum)UI.BTN_NEXT, animationEnd);
		SetActive((Enum)UI.BTN_SKIP_FULL_SCREEN, false);
		SetActive((Enum)UI.BTN_SKIP_IN_SCROLL, false);
		SetActive((Enum)UI.BTN_SKIP_IN_SCROLL_2, false);
	}

	protected override string GetSceneName()
	{
		return "GuildRequestResult";
	}
}
