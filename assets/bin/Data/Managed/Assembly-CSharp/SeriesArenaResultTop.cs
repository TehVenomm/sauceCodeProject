using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeriesArenaResultTop : QuestResultTop
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
		BTN_NEXT_ONLY,
		BTN_NEXT_ALL,
		BTN_NEXT,
		BTN_RETRY,
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
		SHADOW
	}

	private enum AUDIO
	{
		ACHIEVEMENT = 40000028,
		ADVENT = 40000026,
		ARRIVAL = 40000269,
		CATEGORY = 40000228,
		COUNTUP = 40000012,
		POINTREWARD = 40000230
	}

	private new enum RESULT_ANIM_STATE
	{
		IDLE,
		TITLE,
		DROP,
		MISSION,
		MISSION_REWARD,
		TREASURE,
		TREASURE_END,
		REMAIN_TIME,
		CLEAR_TIME_COUNT_UP,
		CLEAR_EFFECT,
		BEST_SCORE,
		EVENT,
		END
	}

	private const float COUNT_ANIM_SPEED = 4f;

	private bool isSkip;

	private bool isNext;

	private ResultReward[] resultRewards;

	private PointEventCurrentData allPointEvents;

	private ARENA_RANK preRank;

	private ARENA_RANK newRank;

	private new RESULT_ANIM_STATE animState;

	public override void Initialize()
	{
		base.Initialize();
		if (MonoBehaviourSingleton<QuestManager>.I.compData != null)
		{
			preRank = (ARENA_RANK)MonoBehaviourSingleton<QuestManager>.I.compData.seriesArena.beforeRank;
			newRank = (ARENA_RANK)MonoBehaviourSingleton<QuestManager>.I.compData.seriesArena.afterRank;
			if (preRank != newRank)
			{
				ResourceLoad.LoadWithSetUITexture(GetCtrl(UI.TEX_RANK_PRE).GetComponent<UITexture>(), RESOURCE_CATEGORY.SERIES_ARENA_RANK_ICON, ResourceName.GetSeriesArenaRankIconName(preRank));
				ResourceLoad.LoadWithSetUITexture(GetCtrl(UI.TEX_RANK_NEW).GetComponent<UITexture>(), RESOURCE_CATEGORY.SERIES_ARENA_RANK_ICON, ResourceName.GetSeriesArenaRankIconName(newRank));
			}
		}
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
		}
	}

	protected override void InitReward()
	{
		base.InitReward();
		if (eventRewardTitles == null)
		{
			eventRewardTitles = new List<string>();
		}
		if (eventReward == null)
		{
			eventReward = new QuestCompleteReward();
		}
		if (pointShopResultData == null)
		{
			pointShopResultData = new List<PointShopResultData>();
		}
	}

	public override void UpdateUI()
	{
		allPointEvents = new PointEventCurrentData();
		allPointEvents.pointRankingData = new PointEventCurrentData.PointResultData();
		SetFullScreenButton(UI.BTN_SKIP_FULL_SCREEN);
		SetActive(UI.BTN_NEXT_ONLY, is_visible: false);
		SetActive(UI.BTN_NEXT_ALL, is_visible: false);
		SetActive(UI.OBJ_TIME, is_visible: false);
		SetActive(UI.OBJ_CLEAR_EFFECT_ROOT, is_visible: false);
		SetActive(UI.OBJ_CLEAR_EFFECT, is_visible: false);
		SetActive(UI.OBJ_RANK_UP_ROOT, is_visible: false);
		SetActive(UI.OBJ_CONGRATULATIONS_ROOT, is_visible: false);
		if (!isVictory)
		{
			Transform ctrl = GetCtrl(UI.OBJ_MONEY);
			ctrl.localPosition = new Vector3(ctrl.localPosition.x, 0f, ctrl.localPosition.z);
		}
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
		SetLabelText(UI.LBL_QUEST_NAME, questData.questText);
		int num = 0;
		int num2 = 0;
		if (isVictory)
		{
			QuestCompleteRewardList reward = MonoBehaviourSingleton<QuestManager>.I.compData.reward;
			QuestCompleteReward breakReward = reward.breakReward;
			QuestCompleteReward order = reward.order;
			num2 = dropReward.money + breakReward.money + order.money;
			num = dropReward.exp + breakReward.exp + order.exp;
		}
		SetLabelText(UI.LBL_EXP, num.ToString("N0"));
		SetLabelText(UI.LBL_REWARD_GOLD, num2.ToString("N0"));
		SetLabelText(UI.LBL_TIME, MonoBehaviourSingleton<InGameRecorder>.I.arenaRemainTimeToString);
		SetGrid(UI.GRD_DROP_ITEM, null, dropItemIconData.Length, reset: true, delegate(int i, Transform o, bool is_recycle)
		{
			ITEM_ICON_TYPE iTEM_ICON_TYPE = ITEM_ICON_TYPE.NONE;
			RARITY_TYPE? rarity = null;
			ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
			EQUIPMENT_TYPE? magi_enable_icon_type = null;
			int icon_id = -1;
			int num3 = -1;
			if (i < dropItemIconData.Length && dropItemIconData[i] != null)
			{
				iTEM_ICON_TYPE = dropItemIconData[i].GetIconType();
				icon_id = dropItemIconData[i].GetIconID();
				rarity = dropItemIconData[i].GetRarity();
				element = dropItemIconData[i].GetIconElement();
				magi_enable_icon_type = dropItemIconData[i].GetIconMagiEnableType();
				num3 = dropItemIconData[i].GetNum();
				if (num3 == 1)
				{
					num3 = -1;
				}
			}
			bool is_new = false;
			switch (iTEM_ICON_TYPE)
			{
			case ITEM_ICON_TYPE.ITEM:
			case ITEM_ICON_TYPE.QUEST_ITEM:
				if (dropItemIconData[i].GetUniqID() != 0L)
				{
					is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iTEM_ICON_TYPE, dropItemIconData[i].GetUniqID());
				}
				break;
			default:
				is_new = true;
				break;
			case ITEM_ICON_TYPE.NONE:
				break;
			}
			int enemy_icon_id = 0;
			int enemy_icon_id2 = 0;
			if (iTEM_ICON_TYPE == ITEM_ICON_TYPE.ITEM)
			{
				ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(dropItemIconData[i].GetTableID());
				enemy_icon_id = itemData.enemyIconID;
				enemy_icon_id2 = itemData.enemyIconID2;
			}
			ItemIcon itemIcon = null;
			itemIcon = ((dropItemIconData[i].GetIconType() != ITEM_ICON_TYPE.QUEST_ITEM) ? ItemIcon.Create(iTEM_ICON_TYPE, icon_id, rarity, o, element, magi_enable_icon_type, num3, "DROP", i, is_new, -1, is_select: false, null, is_equipping: false, enemy_icon_id, enemy_icon_id2, disable_rarity_text: false, dropItemIconData[i].GetGetType()) : ItemIcon.Create(new ItemIcon.ItemIconCreateParam
			{
				icon_type = dropItemIconData[i].GetIconType(),
				icon_id = dropItemIconData[i].GetIconID(),
				rarity = dropItemIconData[i].GetRarity(),
				parent = o,
				element = dropItemIconData[i].GetIconElement(),
				magi_enable_equip_type = dropItemIconData[i].GetIconMagiEnableType(),
				num = dropItemIconData[i].GetNum(),
				enemy_icon_id = enemy_icon_id,
				enemy_icon_id2 = enemy_icon_id2,
				questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST
			}));
			itemIcon.SetRewardBG(is_visible: true);
			itemIcon.SetRewardCategoryInfo(dropItemIconData[i].GetCategory());
			SetMaterialInfo(itemIcon.transform, dropItemIconData[i].GetMaterialType(), dropItemIconData[i].GetTableID(), GetCtrl(UI.PNL_MATERIAL_INFO));
			Transform transform = SetPrefab(o, "QuestResultDropIconOpener");
			QuestResultDropIconOpener.Info info2 = new QuestResultDropIconOpener.Info
			{
				IsRare = ResultUtility.IsRare(dropItemIconData[i]),
				IsBroken = ResultUtility.IsBreakReward(dropItemIconData[i])
			};
			transform.GetComponent<QuestResultDropIconOpener>().Initialized(itemIcon, info2, delegate(Transform t, QuestResultDropIconOpener.Info info, bool is_skip)
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
		SetLabelText(UI.LBL_CLEAR_TIME, InGameProgress.GetSeriesArenaTimeWithMilliSecToString(0f));
		SetActive(UI.SPR_BESTSCORE, is_visible: false);
		if (isVictory)
		{
			SetLabelText(UI.LBL_BEFORE_TIME, InGameProgress.GetSeriesArenaTimeWithMilliSecToString((float)MonoBehaviourSingleton<QuestManager>.I.compData.seriesArena.prevClearTime * 0.001f));
		}
		bool flag = pointShopResultData.Count > 0;
		SetActive(UI.OBJ_POINT_SHOP_RESULT_ROOT, flag);
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
					ResourceLoad.LoadPointIconImageTexture(FindCtrl(t, UI.TEX_NORMAL_POINT_SHOP_ICON).GetComponent<UITexture>(), (uint)pointShopResultData.pointShopId);
				}
				SetActive(t, UI.OBJ_EVENT_POINT_SHOP_ROOT, pointShopResultData.isEvent);
				if (pointShopResultData.isEvent)
				{
					SetLabelText(t, UI.LBL_EVENT_GET_POINT_SHOP, string.Format("+" + StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopResultData.getPoint));
					SetLabelText(t, UI.LBL_EVENT_TOTAL_POINT_SHOP, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopResultData.totalPoint));
					ResourceLoad.LoadPointIconImageTexture(FindCtrl(t, UI.TEX_EVENT_POINT_SHOP_ICON).GetComponent<UITexture>(), (uint)pointShopResultData.pointShopId);
				}
			});
		}
		if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
		{
			UIVirtualScreen componentInChildren = GetComponentInChildren<UIVirtualScreen>();
			UIWidget component = GetCtrl(UI.SHADOW).GetComponent<UIWidget>();
			if (componentInChildren != null && component != null)
			{
				component.width = (int)componentInChildren.ScreenWidthFull;
				component.height = (int)componentInChildren.ScreenHeightFull;
			}
		}
		QuestInfoData.Mission[] array = null;
		if (MonoBehaviourSingleton<QuestManager>.I.missionNewClearFlag != null)
		{
			missionNewClear = MonoBehaviourSingleton<QuestManager>.I.missionNewClearFlag.ToArray();
		}
		UI[] array2 = new UI[3]
		{
			UI.OBJ_MISSION_01,
			UI.OBJ_MISSION_02,
			UI.OBJ_MISSION_03
		};
		UI[] array3 = new UI[3]
		{
			UI.LBL_MISSION_NAME_01,
			UI.LBL_MISSION_NAME_02,
			UI.LBL_MISSION_NAME_03
		};
		UI[] array4 = new UI[3]
		{
			UI.SPR_CROWN_01,
			UI.SPR_CROWN_02,
			UI.SPR_CROWN_03
		};
		UI[] array5 = new UI[3]
		{
			UI.SPR_CLEARED_CROWN_01,
			UI.SPR_CLEARED_CROWN_02,
			UI.SPR_CLEARED_CROWN_03
		};
		UI[] array6 = new UI[3]
		{
			UI.TEX_MISSION_COIN_01,
			UI.TEX_MISSION_COIN_02,
			UI.TEX_MISSION_COIN_03
		};
		UI[] array7 = new UI[3]
		{
			UI.SPR_CROWN01_OFF,
			UI.SPR_CROWN02_OFF,
			UI.SPR_CROWN03_OFF
		};
		array = QuestInfoData.CreateMissionData(Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID));
		for (int j = 0; j < 3; j++)
		{
			bool flag2 = array[j] != null;
			SetActive(array2[j], flag2);
			if (!flag2)
			{
				continue;
			}
			SetLabelText(array3[j], array[j].tableData.missionText);
			bool flag3 = missionNewClear != null && missionNewClear[j] > 0;
			bool flag4 = (array[j].state >= CLEAR_STATUS.CLEAR) | flag3;
			if (flag3)
			{
				isValidMissionNewClearAnim = true;
			}
			if (missionPointData != null)
			{
				SetActive(array6[j], is_visible: true);
				SetActive(array7[j], is_visible: false);
				SetActive(array4[j], is_visible: false);
				SetActive(array5[j], is_visible: false);
				UITexture component2 = GetCtrl(array6[j]).GetComponent<UITexture>();
				if (flag3)
				{
					ResourceLoad.LoadPointIconImageTexture(component2, (uint)missionPointData.pointShopId);
				}
				else
				{
					ResourceLoad.LoadGrayPointIconImageTexture(component2, (uint)missionPointData.pointShopId);
				}
				continue;
			}
			SetActive(array6[j], is_visible: false);
			SetActive(array7[j], is_visible: true);
			if (flag3)
			{
				SetActive(array4[j], is_visible: true);
				SetActive(array5[j], is_visible: false);
			}
			else if (flag4)
			{
				SetActive(array4[j], is_visible: false);
				SetActive(array5[j], is_visible: true);
			}
			else
			{
				SetActive(array4[j], is_visible: false);
				SetActive(array5[j], is_visible: false);
			}
		}
		StartCoroutine(PlayAnimation());
	}

	private void PlayAudio(AUDIO type)
	{
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			SoundManager.PlayOneShotUISE((int)type);
		}
	}

	private IEnumerator PlayAnimation()
	{
		isSkip = false;
		animState = RESULT_ANIM_STATE.TITLE;
		PlayAudio(AUDIO.ADVENT);
		bool isTitleEnd = false;
		PlayTween(UI.OBJ_TITLE, forward: true, delegate
		{
			isTitleEnd = true;
		}, is_input_block: false);
		yield return new WaitWhile(() => !isTitleEnd && !isSkip);
		animState = RESULT_ANIM_STATE.DROP;
		PlayAudio(AUDIO.ACHIEVEMENT);
		if (pointShopResultData.Count > 0)
		{
			foreach (Transform item in GetCtrl(UI.OBJ_POINT_SHOP_RESULT_ROOT).transform)
			{
				PlayTween(item);
			}
		}
		PlayTween(UI.OBJ_EXP);
		if (isVictory)
		{
			animState = RESULT_ANIM_STATE.MISSION;
			bool isMissionEnd = false;
			if (!isValidMissionNewClearAnim)
			{
				PlayTween(UI.OBJ_MISSION_ROOT, forward: true, delegate
				{
					OpenMissionClearRewardDialog(delegate
					{
						isMissionEnd = true;
					});
				});
			}
			else
			{
				PlayTween(UI.OBJ_MISSION_ROOT, forward: true, null, is_input_block: false);
				PlayTween(UI.OBJ_MISSION_NEW_CLEAR_ROOT, forward: true, delegate
				{
					OpenMissionClearRewardDialog(delegate
					{
						isMissionEnd = true;
					});
				}, is_input_block: false);
			}
			yield return new WaitUntil(() => isMissionEnd);
		}
		bool isMoneyEnd = false;
		PlayTween(UI.OBJ_MONEY, forward: true, delegate
		{
			isMoneyEnd = true;
		}, is_input_block: false);
		yield return new WaitWhile(() => !isMoneyEnd && !isSkip);
		if (isVictory)
		{
			animState = RESULT_ANIM_STATE.TREASURE;
			SetActive(UI.OBJ_TREASURE_ROOT, is_visible: true);
			bool isTreasureEnd = false;
			PlayTween(UI.OBJ_TREASURE_ROOT, forward: true, delegate
			{
				isTreasureEnd = true;
			}, is_input_block: false);
			yield return new WaitWhile(() => !isTreasureEnd && !isSkip);
			int dropIndex = 0;
			float dropAnimTime = 0f;
			yield return new WaitWhile(delegate
			{
				dropAnimTime += Time.deltaTime;
				if (dropAnimTime > 0.4f || isSkip)
				{
					dropAnimTime = 0f;
					VisibleItemIcon(dropIndex, isSkip);
					if (dropIndex >= 5 && dropIndex % 5 == 0)
					{
						SetScroll(UI.OBJ_SCROLL_VIEW, animScrollValue);
					}
					dropIndex++;
					if (dropIndex >= dropItemNum)
					{
						return false;
					}
				}
				return true;
			});
			animState = RESULT_ANIM_STATE.TREASURE_END;
			isNext = false;
			VisibleEndButton();
			yield return new WaitUntil(() => isNext);
			isSkip = false;
			SetActive(UI.OBJ_TREASURE_ROOT, is_visible: false);
			InvisibleEndButton();
			if (preRank != newRank)
			{
				SetActive(UI.OBJ_RANK_UP_ROOT, is_visible: true);
				ResetTween(UI.OBJ_RANK_UP);
				animState = RESULT_ANIM_STATE.CLEAR_EFFECT;
				GetCtrl(UI.OBJ_PARTICLE).GetComponent<ParticleSystem>().GetComponent<ParticleSystemRenderer>().sharedMaterial.renderQueue = 4000;
				yield return null;
				PlayAudio(AUDIO.ARRIVAL);
				bool isRankUpEnd = false;
				PlayTween(UI.OBJ_RANK_UP, forward: true, delegate
				{
					isRankUpEnd = true;
				});
				yield return new WaitWhile(() => !isRankUpEnd && !isSkip);
				float waitTime = 2.5f;
				while (waitTime > 0f && !isSkip)
				{
					yield return null;
					waitTime -= Time.deltaTime;
				}
				SetActive(UI.OBJ_RANK_UP_ROOT, is_visible: false);
			}
			SetActive(UI.OBJ_TIME, is_visible: true);
			animState = RESULT_ANIM_STATE.CLEAR_TIME_COUNT_UP;
			bool isTimeEnd = false;
			StartCoroutine(PlayCountUpClearTimeAnim(MonoBehaviourSingleton<InGameRecorder>.I.arenaElapsedTime, delegate
			{
				isTimeEnd = true;
			}));
			yield return new WaitWhile(() => !isTimeEnd && !isSkip);
			if (IsBreakRecord())
			{
				animState = RESULT_ANIM_STATE.BEST_SCORE;
				PlayAudio(AUDIO.ARRIVAL);
				SetActive(UI.SPR_BESTSCORE, is_visible: true);
				bool isBestScoreEnd = false;
				PlayTween(UI.SPR_BESTSCORE, forward: true, delegate
				{
					isBestScoreEnd = true;
				});
				yield return new WaitWhile(() => !isBestScoreEnd && !isSkip);
			}
		}
		animState = RESULT_ANIM_STATE.EVENT;
		OpenAllEventRewardDialog(delegate
		{
			animState = RESULT_ANIM_STATE.IDLE;
		});
		yield return new WaitWhile(() => animState != 0 && !isSkip);
		animState = RESULT_ANIM_STATE.END;
		VisibleEndButton();
	}

	private IEnumerator PlayCountUpClearTimeAnim(float targetTime, Action callBack)
	{
		float currentShowTime2 = 0f;
		while (currentShowTime2 < targetTime)
		{
			yield return null;
			if (isSkip)
			{
				currentShowTime2 = targetTime;
			}
			int num = Mathf.FloorToInt(currentShowTime2);
			float num2 = Mathf.Max((targetTime - currentShowTime2) * CountDownCube(Time.deltaTime * 4f), 1f);
			currentShowTime2 += num2;
			currentShowTime2 = Mathf.Min(currentShowTime2, targetTime);
			if (num < Mathf.FloorToInt(currentShowTime2))
			{
				SoundManager.PlayOneShotUISE(40000012);
			}
			double num3 = Math.Round(currentShowTime2, 2, MidpointRounding.AwayFromZero);
			SetLabelText(UI.LBL_CLEAR_TIME, InGameProgress.GetSeriesArenaTimeWithMilliSecToString((float)num3));
		}
		callBack?.Invoke();
	}

	private float CountDownCube(float currentValue)
	{
		return currentValue * (2f - currentValue);
	}

	private void InvisibleEndButton()
	{
		SetActive(UI.BTN_NEXT_ONLY, is_visible: false);
		SetActive(UI.BTN_NEXT_ALL, is_visible: false);
		SetActive(UI.BTN_SKIP_FULL_SCREEN, is_visible: true);
	}

	protected override void VisibleEndButton()
	{
		bool flag = animState == RESULT_ANIM_STATE.TREASURE_END;
		bool flag2 = animState == RESULT_ANIM_STATE.END;
		SetActive(UI.BTN_NEXT_ONLY, flag);
		SetActive(UI.BTN_NEXT_ALL, flag2);
		SetActive(UI.BTN_SKIP_FULL_SCREEN, !flag && !flag2);
	}

	private bool IsBreakRecord()
	{
		if (!MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			return false;
		}
		if (!MonoBehaviourSingleton<QuestManager>.IsValid())
		{
			return false;
		}
		int num = Mathf.FloorToInt(MonoBehaviourSingleton<InGameRecorder>.I.arenaElapsedTime * 1000f);
		int prevClearTime = MonoBehaviourSingleton<QuestManager>.I.compData.seriesArena.prevClearTime;
		if (num < prevClearTime)
		{
			return true;
		}
		return false;
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

	private void OpenMissionClearRewardDialog(Action end_callback)
	{
		bool flag = animState == RESULT_ANIM_STATE.MISSION || animState == RESULT_ANIM_STATE.END;
		if (!base.isNeedOpenMissionClearDialog || !flag || isOpenedMissionClearDialog)
		{
			end_callback?.Invoke();
			return;
		}
		QuestCompleteReward questCompleteReward = null;
		PointShopResultData missionPoint = null;
		bool isCompleteReward = false;
		if (missionPointData != null || missionReward != null)
		{
			if (missionReward != null)
			{
				questCompleteReward = missionReward;
				missionReward = null;
			}
			if (missionPointData != null)
			{
				missionPoint = missionPointData;
				missionPointData = null;
			}
		}
		else
		{
			if (missionCompleteReward == null)
			{
				isOpenedMissionClearDialog = true;
				if (animState < RESULT_ANIM_STATE.END)
				{
					animState = RESULT_ANIM_STATE.MISSION_REWARD;
				}
				end_callback?.Invoke();
				return;
			}
			questCompleteReward = missionCompleteReward;
			missionCompleteReward = null;
			isCompleteReward = true;
			if (animState < RESULT_ANIM_STATE.END)
			{
				animState = RESULT_ANIM_STATE.MISSION_REWARD;
			}
		}
		List<SortCompareData> tmp = new List<SortCompareData>();
		int num = 0;
		int gold = questCompleteReward?.money ?? 0;
		int crystal = questCompleteReward?.crystal ?? 0;
		if (questCompleteReward != null)
		{
			num = ResultUtility.SetDropData(tmp, num, questCompleteReward.item);
			num = ResultUtility.SetDropData(tmp, num, questCompleteReward.equipItem);
			num = ResultUtility.SetDropData(tmp, num, questCompleteReward.skillItem);
			num = ResultUtility.SetDropData(tmp, num, questCompleteReward.accessoryItem);
		}
		if (num == 0 && crystal == 0 && missionPoint == null)
		{
			end_callback?.Invoke();
			return;
		}
		missionClearRewardCallback = end_callback;
		if (missionPoint == null)
		{
			missionPoint = new PointShopResultData();
		}
		if (!QuestResultTop.IsExecuteNowSceneEvent(GetSceneName()))
		{
			StartCoroutine(ExecEndDialogEvent(GetSceneName(), delegate
			{
				DispatchEvent("MISSION_CLEAR_REWARD", new object[5]
				{
					tmp,
					gold,
					crystal,
					isCompleteReward,
					missionPoint
				});
			}));
		}
		else
		{
			DispatchEvent("MISSION_CLEAR_REWARD", new object[5]
			{
				tmp,
				gold,
				crystal,
				isCompleteReward,
				missionPoint
			});
		}
	}

	private void OnCloseDialog_QuestResultMissionClearRewardDialog()
	{
		Debug.LogFormat("OnCloseDialog_QuestResultMissionClearRewardDialog");
		if (missionClearRewardCallback != null)
		{
			StartCoroutine(OnCloseMissionClearCoroutine());
		}
	}

	private IEnumerator OnCloseMissionClearCoroutine()
	{
		Debug.LogFormat("OnCloseMissionClearCoroutine");
		if (!QuestResultTop.IsExecuteNowSceneEvent(GetSceneName()))
		{
			yield return null;
		}
		OpenMissionClearRewardDialog(missionClearRewardCallback);
	}

	private void OnQuery_SKIP()
	{
		switch (animState)
		{
		case RESULT_ANIM_STATE.TITLE:
		case RESULT_ANIM_STATE.DROP:
		case RESULT_ANIM_STATE.MISSION:
		case RESULT_ANIM_STATE.TREASURE:
			SkipTween(UI.OBJ_TITLE);
			SkipTween(UI.OBJ_POINT_SHOP_RESULT_ROOT);
			SkipTween(UI.OBJ_EXP);
			SkipTween(UI.OBJ_MONEY);
			if (isVictory)
			{
				SkipTween(UI.OBJ_MISSION_ROOT);
				SkipTween(UI.OBJ_MISSION_NEW_CLEAR_ROOT);
				SkipTween(UI.OBJ_TREASURE_ROOT);
			}
			break;
		case RESULT_ANIM_STATE.REMAIN_TIME:
			SkipTween(UI.OBJ_REMAIN_TIME);
			break;
		case RESULT_ANIM_STATE.BEST_SCORE:
			SkipTween(UI.SPR_BESTSCORE);
			break;
		case RESULT_ANIM_STATE.CLEAR_EFFECT:
			SkipTween(UI.OBJ_RANK_UP);
			break;
		}
		isSkip = true;
		GameSection.StopEvent();
	}

	private void OnQuery_NEXT()
	{
		if (animState == RESULT_ANIM_STATE.END)
		{
			ToSeriesArena();
		}
		else if (animState == RESULT_ANIM_STATE.TREASURE_END)
		{
			isNext = true;
		}
		else if (animState != 0)
		{
			OnQuery_SKIP();
		}
	}

	private void OnQuery_RETRY()
	{
		if (animState == RESULT_ANIM_STATE.END)
		{
			if (MonoBehaviourSingleton<CoopManager>.IsValid())
			{
				MonoBehaviourSingleton<CoopManager>.I.Clear();
			}
			if (MonoBehaviourSingleton<InGameManager>.IsValid())
			{
				MonoBehaviourSingleton<InGameManager>.I.isRetry = true;
			}
			MonoBehaviourSingleton<GameSceneManager>.I.ReloadScene();
		}
		else if (animState != 0 && animState != RESULT_ANIM_STATE.TREASURE_END)
		{
			OnQuery_SKIP();
		}
	}

	protected override string GetSceneName()
	{
		return "SeriesArenaResultTop";
	}
}
