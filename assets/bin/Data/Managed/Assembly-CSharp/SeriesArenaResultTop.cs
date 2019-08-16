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
				UITexture component = GetCtrl(UI.TEX_RANK_PRE).GetComponent<UITexture>();
				ResourceLoad.LoadWithSetUITexture(component, RESOURCE_CATEGORY.SERIES_ARENA_RANK_ICON, ResourceName.GetSeriesArenaRankIconName(preRank));
				UITexture component2 = GetCtrl(UI.TEX_RANK_NEW).GetComponent<UITexture>();
				ResourceLoad.LoadWithSetUITexture(component2, RESOURCE_CATEGORY.SERIES_ARENA_RANK_ICON, ResourceName.GetSeriesArenaRankIconName(newRank));
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
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		allPointEvents = new PointEventCurrentData();
		allPointEvents.pointRankingData = new PointEventCurrentData.PointResultData();
		SetFullScreenButton((Enum)UI.BTN_SKIP_FULL_SCREEN);
		SetActive((Enum)UI.BTN_NEXT_ONLY, is_visible: false);
		SetActive((Enum)UI.BTN_NEXT_ALL, is_visible: false);
		SetActive((Enum)UI.OBJ_TIME, is_visible: false);
		SetActive((Enum)UI.OBJ_CLEAR_EFFECT_ROOT, is_visible: false);
		SetActive((Enum)UI.OBJ_CLEAR_EFFECT, is_visible: false);
		SetActive((Enum)UI.OBJ_RANK_UP_ROOT, is_visible: false);
		SetActive((Enum)UI.OBJ_CONGRATULATIONS_ROOT, is_visible: false);
		if (!isVictory)
		{
			Transform ctrl = GetCtrl(UI.OBJ_MONEY);
			Transform obj = ctrl;
			Vector3 localPosition = ctrl.get_localPosition();
			float x = localPosition.x;
			Vector3 localPosition2 = ctrl.get_localPosition();
			obj.set_localPosition(new Vector3(x, 0f, localPosition2.z));
		}
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
		SetLabelText((Enum)UI.LBL_QUEST_NAME, questData.questText);
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
		SetLabelText((Enum)UI.LBL_EXP, num.ToString("N0"));
		SetLabelText((Enum)UI.LBL_REWARD_GOLD, num2.ToString("N0"));
		SetLabelText((Enum)UI.LBL_TIME, MonoBehaviourSingleton<InGameRecorder>.I.arenaRemainTimeToString);
		SetGrid(UI.GRD_DROP_ITEM, null, dropItemIconData.Length, reset: true, delegate(int i, Transform o, bool is_recycle)
		{
			ITEM_ICON_TYPE iTEM_ICON_TYPE = ITEM_ICON_TYPE.NONE;
			RARITY_TYPE? rARITY_TYPE = null;
			ELEMENT_TYPE eLEMENT_TYPE = ELEMENT_TYPE.MAX;
			EQUIPMENT_TYPE? eQUIPMENT_TYPE = null;
			int num3 = -1;
			int num4 = -1;
			if (i < dropItemIconData.Length && dropItemIconData[i] != null)
			{
				iTEM_ICON_TYPE = dropItemIconData[i].GetIconType();
				num3 = dropItemIconData[i].GetIconID();
				rARITY_TYPE = dropItemIconData[i].GetRarity();
				eLEMENT_TYPE = dropItemIconData[i].GetIconElement();
				eQUIPMENT_TYPE = dropItemIconData[i].GetIconMagiEnableType();
				num4 = dropItemIconData[i].GetNum();
				if (num4 == 1)
				{
					num4 = -1;
				}
			}
			bool flag5 = false;
			switch (iTEM_ICON_TYPE)
			{
			case ITEM_ICON_TYPE.ITEM:
			case ITEM_ICON_TYPE.QUEST_ITEM:
			{
				ulong uniqID = dropItemIconData[i].GetUniqID();
				if (uniqID != 0)
				{
					flag5 = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iTEM_ICON_TYPE, dropItemIconData[i].GetUniqID());
				}
				break;
			}
			default:
				flag5 = true;
				break;
			case ITEM_ICON_TYPE.NONE:
				break;
			}
			int num5 = 0;
			int num6 = 0;
			if (iTEM_ICON_TYPE == ITEM_ICON_TYPE.ITEM)
			{
				ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(dropItemIconData[i].GetTableID());
				num5 = itemData.enemyIconID;
				num6 = itemData.enemyIconID2;
			}
			ItemIcon itemIcon = null;
			if (dropItemIconData[i].GetIconType() == ITEM_ICON_TYPE.QUEST_ITEM)
			{
				itemIcon = ItemIcon.Create(new ItemIcon.ItemIconCreateParam
				{
					icon_type = dropItemIconData[i].GetIconType(),
					icon_id = dropItemIconData[i].GetIconID(),
					rarity = dropItemIconData[i].GetRarity(),
					parent = o,
					element = dropItemIconData[i].GetIconElement(),
					magi_enable_equip_type = dropItemIconData[i].GetIconMagiEnableType(),
					num = dropItemIconData[i].GetNum(),
					enemy_icon_id = num5,
					enemy_icon_id2 = num6,
					questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST
				});
			}
			else
			{
				ITEM_ICON_TYPE icon_type = iTEM_ICON_TYPE;
				int icon_id = num3;
				RARITY_TYPE? rarity = rARITY_TYPE;
				ELEMENT_TYPE element = eLEMENT_TYPE;
				EQUIPMENT_TYPE? magi_enable_icon_type = eQUIPMENT_TYPE;
				int num7 = num4;
				string event_name = "DROP";
				bool is_new = flag5;
				int toggle_group = -1;
				bool is_select = false;
				string icon_under_text = null;
				bool is_equipping = false;
				int enemy_icon_id = num5;
				int enemy_icon_id2 = num6;
				GET_TYPE getType = dropItemIconData[i].GetGetType();
				itemIcon = ItemIcon.Create(icon_type, icon_id, rarity, o, element, magi_enable_icon_type, num7, event_name, i, is_new, toggle_group, is_select, icon_under_text, is_equipping, enemy_icon_id, enemy_icon_id2, disable_rarity_text: false, getType);
			}
			itemIcon.SetRewardBG(is_visible: true);
			itemIcon.SetRewardCategoryInfo(dropItemIconData[i].GetCategory());
			SetMaterialInfo(itemIcon.transform, dropItemIconData[i].GetMaterialType(), dropItemIconData[i].GetTableID(), GetCtrl(UI.PNL_MATERIAL_INFO));
			Transform val = SetPrefab(o, "QuestResultDropIconOpener");
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
				SetVisibleWidgetOneShotEffect(GetCtrl(UI.OBJ_SCROLL_VIEW), t, ui_effect_name);
			});
		});
		SetLabelText((Enum)UI.LBL_CLEAR_TIME, InGameProgress.GetSeriesArenaTimeWithMilliSecToString(0f));
		SetActive((Enum)UI.SPR_BESTSCORE, is_visible: false);
		if (isVictory)
		{
			SetLabelText((Enum)UI.LBL_BEFORE_TIME, InGameProgress.GetSeriesArenaTimeWithMilliSecToString((float)MonoBehaviourSingleton<QuestManager>.I.compData.seriesArena.prevClearTime * 0.001f));
		}
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
					UITexture component3 = FindCtrl(t, UI.TEX_NORMAL_POINT_SHOP_ICON).GetComponent<UITexture>();
					ResourceLoad.LoadPointIconImageTexture(component3, (uint)pointShopResultData.pointShopId);
				}
				SetActive(t, UI.OBJ_EVENT_POINT_SHOP_ROOT, pointShopResultData.isEvent);
				if (pointShopResultData.isEvent)
				{
					SetLabelText(t, UI.LBL_EVENT_GET_POINT_SHOP, string.Format("+" + StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopResultData.getPoint));
					SetLabelText(t, UI.LBL_EVENT_TOTAL_POINT_SHOP, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopResultData.totalPoint));
					UITexture component4 = FindCtrl(t, UI.TEX_EVENT_POINT_SHOP_ICON).GetComponent<UITexture>();
					ResourceLoad.LoadPointIconImageTexture(component4, (uint)pointShopResultData.pointShopId);
				}
			});
		}
		if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
		{
			UIVirtualScreen componentInChildren = this.GetComponentInChildren<UIVirtualScreen>();
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
			SetActive((Enum)array2[j], flag2);
			if (!flag2)
			{
				continue;
			}
			SetLabelText((Enum)array3[j], array[j].tableData.missionText);
			bool flag3 = missionNewClear != null && missionNewClear[j] > 0;
			bool flag4 = array[j].state >= CLEAR_STATUS.CLEAR || flag3;
			if (flag3)
			{
				isValidMissionNewClearAnim = true;
			}
			if (missionPointData != null)
			{
				SetActive((Enum)array6[j], is_visible: true);
				SetActive((Enum)array7[j], is_visible: false);
				SetActive((Enum)array4[j], is_visible: false);
				SetActive((Enum)array5[j], is_visible: false);
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
			SetActive((Enum)array6[j], is_visible: false);
			SetActive((Enum)array7[j], is_visible: true);
			if (flag3)
			{
				SetActive((Enum)array4[j], is_visible: true);
				SetActive((Enum)array5[j], is_visible: false);
			}
			else if (flag4)
			{
				SetActive((Enum)array4[j], is_visible: false);
				SetActive((Enum)array5[j], is_visible: true);
			}
			else
			{
				SetActive((Enum)array4[j], is_visible: false);
				SetActive((Enum)array5[j], is_visible: false);
			}
		}
		this.StartCoroutine(PlayAnimation());
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
		PlayTween((Enum)UI.OBJ_TITLE, forward: true, (EventDelegate.Callback)delegate
		{
			isTitleEnd = true;
		}, is_input_block: false, 0);
		yield return (object)new WaitWhile((Func<bool>)(() => !isTitleEnd && !isSkip));
		animState = RESULT_ANIM_STATE.DROP;
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
		PlayTween((Enum)UI.OBJ_EXP, forward: true, (EventDelegate.Callback)null, is_input_block: true, 0);
		if (isVictory)
		{
			animState = RESULT_ANIM_STATE.MISSION;
			bool isMissionEnd = false;
			if (!isValidMissionNewClearAnim)
			{
				PlayTween((Enum)UI.OBJ_MISSION_ROOT, forward: true, (EventDelegate.Callback)delegate
				{
					OpenMissionClearRewardDialog(delegate
					{
						isMissionEnd = true;
					});
				}, is_input_block: true, 0);
			}
			else
			{
				PlayTween((Enum)UI.OBJ_MISSION_ROOT, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
				PlayTween((Enum)UI.OBJ_MISSION_NEW_CLEAR_ROOT, forward: true, (EventDelegate.Callback)delegate
				{
					OpenMissionClearRewardDialog(delegate
					{
						isMissionEnd = true;
					});
				}, is_input_block: false, 0);
			}
			yield return (object)new WaitUntil((Func<bool>)(() => isMissionEnd));
		}
		bool isMoneyEnd = false;
		PlayTween((Enum)UI.OBJ_MONEY, forward: true, (EventDelegate.Callback)delegate
		{
			isMoneyEnd = true;
		}, is_input_block: false, 0);
		yield return (object)new WaitWhile((Func<bool>)(() => !isMoneyEnd && !isSkip));
		if (isVictory)
		{
			animState = RESULT_ANIM_STATE.TREASURE;
			SetActive((Enum)UI.OBJ_TREASURE_ROOT, is_visible: true);
			bool isTreasureEnd = false;
			PlayTween((Enum)UI.OBJ_TREASURE_ROOT, forward: true, (EventDelegate.Callback)delegate
			{
				isTreasureEnd = true;
			}, is_input_block: false, 0);
			yield return (object)new WaitWhile((Func<bool>)(() => !isTreasureEnd && !isSkip));
			int dropIndex = 0;
			float dropAnimTime = 0f;
			yield return (object)new WaitWhile((Func<bool>)delegate
			{
				dropAnimTime += Time.get_deltaTime();
				if (dropAnimTime > 0.4f || isSkip)
				{
					dropAnimTime = 0f;
					VisibleItemIcon(dropIndex, isSkip);
					if (dropIndex >= 5 && dropIndex % 5 == 0)
					{
						SetScroll((Enum)UI.OBJ_SCROLL_VIEW, animScrollValue);
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
			yield return (object)new WaitUntil((Func<bool>)(() => isNext));
			isSkip = false;
			SetActive((Enum)UI.OBJ_TREASURE_ROOT, is_visible: false);
			InvisibleEndButton();
			if (preRank != newRank)
			{
				SetActive((Enum)UI.OBJ_RANK_UP_ROOT, is_visible: true);
				ResetTween((Enum)UI.OBJ_RANK_UP, 0);
				animState = RESULT_ANIM_STATE.CLEAR_EFFECT;
				ParticleSystem particle = GetCtrl(UI.OBJ_PARTICLE).GetComponent<ParticleSystem>();
				particle.GetComponent<ParticleSystemRenderer>().get_sharedMaterial().set_renderQueue(4000);
				yield return null;
				PlayAudio(AUDIO.ARRIVAL);
				bool isRankUpEnd = false;
				PlayTween((Enum)UI.OBJ_RANK_UP, forward: true, (EventDelegate.Callback)delegate
				{
					isRankUpEnd = true;
				}, is_input_block: true, 0);
				yield return (object)new WaitWhile((Func<bool>)(() => !isRankUpEnd && !isSkip));
				float waitTime = 2.5f;
				while (waitTime > 0f && !isSkip)
				{
					yield return null;
					waitTime -= Time.get_deltaTime();
				}
				SetActive((Enum)UI.OBJ_RANK_UP_ROOT, is_visible: false);
			}
			SetActive((Enum)UI.OBJ_TIME, is_visible: true);
			animState = RESULT_ANIM_STATE.CLEAR_TIME_COUNT_UP;
			bool isTimeEnd = false;
			this.StartCoroutine(PlayCountUpClearTimeAnim(MonoBehaviourSingleton<InGameRecorder>.I.arenaElapsedTime, delegate
			{
				isTimeEnd = true;
			}));
			yield return (object)new WaitWhile((Func<bool>)(() => !isTimeEnd && !isSkip));
			if (IsBreakRecord())
			{
				animState = RESULT_ANIM_STATE.BEST_SCORE;
				PlayAudio(AUDIO.ARRIVAL);
				SetActive((Enum)UI.SPR_BESTSCORE, is_visible: true);
				bool isBestScoreEnd = false;
				PlayTween((Enum)UI.SPR_BESTSCORE, forward: true, (EventDelegate.Callback)delegate
				{
					isBestScoreEnd = true;
				}, is_input_block: true, 0);
				yield return (object)new WaitWhile((Func<bool>)(() => !isBestScoreEnd && !isSkip));
			}
		}
		animState = RESULT_ANIM_STATE.EVENT;
		OpenAllEventRewardDialog(delegate
		{
			animState = RESULT_ANIM_STATE.IDLE;
		});
		yield return (object)new WaitWhile((Func<bool>)(() => animState != 0 && !isSkip));
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
			int before = Mathf.FloorToInt(currentShowTime2);
			float addingTime = Mathf.Max((targetTime - currentShowTime2) * CountDownCube(Time.get_deltaTime() * 4f), 1f);
			currentShowTime2 += addingTime;
			currentShowTime2 = Mathf.Min(currentShowTime2, targetTime);
			if (before < Mathf.FloorToInt(currentShowTime2))
			{
				SoundManager.PlayOneShotUISE(40000012);
			}
			double roundTime = Math.Round(currentShowTime2, 2, MidpointRounding.AwayFromZero);
			SetLabelText((Enum)UI.LBL_CLEAR_TIME, InGameProgress.GetSeriesArenaTimeWithMilliSecToString((float)roundTime));
		}
		callBack?.Invoke();
	}

	private float CountDownCube(float currentValue)
	{
		return currentValue * (2f - currentValue);
	}

	private void InvisibleEndButton()
	{
		SetActive((Enum)UI.BTN_NEXT_ONLY, is_visible: false);
		SetActive((Enum)UI.BTN_NEXT_ALL, is_visible: false);
		SetActive((Enum)UI.BTN_SKIP_FULL_SCREEN, is_visible: true);
	}

	protected override void VisibleEndButton()
	{
		bool flag = animState == RESULT_ANIM_STATE.TREASURE_END;
		bool flag2 = animState == RESULT_ANIM_STATE.END;
		SetActive((Enum)UI.BTN_NEXT_ONLY, flag);
		SetActive((Enum)UI.BTN_NEXT_ALL, flag2);
		SetActive((Enum)UI.BTN_SKIP_FULL_SCREEN, !flag && !flag2);
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
			this.StartCoroutine(ExecEndDialogEvent(GetSceneName(), delegate
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
		Debug.LogFormat("OnCloseDialog_QuestResultMissionClearRewardDialog", new object[0]);
		if (missionClearRewardCallback != null)
		{
			this.StartCoroutine(OnCloseMissionClearCoroutine());
		}
	}

	private IEnumerator OnCloseMissionClearCoroutine()
	{
		Debug.LogFormat("OnCloseMissionClearCoroutine", new object[0]);
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
			SkipTween((Enum)UI.OBJ_TITLE, forward: true, 0);
			SkipTween((Enum)UI.OBJ_POINT_SHOP_RESULT_ROOT, forward: true, 0);
			SkipTween((Enum)UI.OBJ_EXP, forward: true, 0);
			SkipTween((Enum)UI.OBJ_MONEY, forward: true, 0);
			if (isVictory)
			{
				SkipTween((Enum)UI.OBJ_MISSION_ROOT, forward: true, 0);
				SkipTween((Enum)UI.OBJ_MISSION_NEW_CLEAR_ROOT, forward: true, 0);
				SkipTween((Enum)UI.OBJ_TREASURE_ROOT, forward: true, 0);
			}
			break;
		case RESULT_ANIM_STATE.REMAIN_TIME:
			SkipTween((Enum)UI.OBJ_REMAIN_TIME, forward: true, 0);
			break;
		case RESULT_ANIM_STATE.BEST_SCORE:
			SkipTween((Enum)UI.SPR_BESTSCORE, forward: true, 0);
			break;
		case RESULT_ANIM_STATE.CLEAR_EFFECT:
			SkipTween((Enum)UI.OBJ_RANK_UP, forward: true, 0);
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
