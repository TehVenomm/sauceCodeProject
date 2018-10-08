using Network;
using System;
using System.Collections;
using UnityEngine;

public class GachaResultQuest : GachaResultBase
{
	private enum UI
	{
		TEX_MODEL,
		TEX_INNER_MODEL,
		LBL_NAME,
		LBL_CRYSTAL_NUM,
		TEX_TICKET,
		TEX_TICKET_HAVE,
		SPR_CRYSTAL,
		LBL_PRICE,
		OBJ_DIFFICULTY_ROOT,
		TEX_GUARANTEE_COUNT_DOWN,
		OBJ_RARITY_ROOT,
		OBJ_RARITY_D,
		OBJ_RARITY_C,
		OBJ_RARITY_B,
		OBJ_RARITY_A,
		OBJ_RARITY_S,
		OBJ_RARITY_SS,
		OBJ_RARITY_SSS,
		OBJ_RARITY_LIGHT,
		OBJ_RARITY_TEXT_ROOT,
		OBJ_SINGLE_ROOT,
		OBJ_MULTI_ROOT,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_DESCRIPTION,
		OBJ_ICONS_ROOT,
		OBJ_ICON_ROOT_0,
		OBJ_ICON_ROOT_1,
		OBJ_ICON_ROOT_2,
		OBJ_ICON_ROOT_3,
		OBJ_ICON_ROOT_4,
		OBJ_ICON_ROOT_5,
		OBJ_ICON_ROOT_6,
		OBJ_ICON_ROOT_7,
		OBJ_ICON_ROOT_8,
		OBJ_ICON_ROOT_9,
		OBJ_ICON_ROOT_10,
		LBL_ENEMY_LV_0,
		LBL_ENEMY_LV_1,
		LBL_ENEMY_LV_2,
		LBL_ENEMY_LV_3,
		LBL_ENEMY_LV_4,
		LBL_ENEMY_LV_5,
		LBL_ENEMY_LV_6,
		LBL_ENEMY_LV_7,
		LBL_ENEMY_LV_8,
		LBL_ENEMY_LV_9,
		LBL_ENEMY_LV_10,
		LBL_MAGI_NAME_0,
		LBL_MAGI_NAME_1,
		LBL_MAGI_NAME_2,
		LBL_MAGI_NAME_3,
		LBL_MAGI_NAME_4,
		LBL_MAGI_NAME_5,
		LBL_MAGI_NAME_6,
		LBL_MAGI_NAME_7,
		LBL_MAGI_NAME_8,
		LBL_MAGI_NAME_9,
		LBL_MAGI_NAME_10,
		BTN_GACHA,
		OBJ_GACHA_ENABLE_ROOT,
		OBJ_GACHA_DISABLE_ROOT,
		OBJ_BG_SINGLE,
		OBJ_BG_MULTI,
		SPR_LINE_TOP,
		SPR_LINE_BOTTOM,
		FOOTER_ROOT,
		FOOTER_GUARANTEE_ROOT,
		FOOTER_MULTI_RESULT_ROOT,
		OBJ_GUARANTEE,
		BG_MULTI,
		BTN_NEXT,
		BTN_BACK,
		BTN_BATTLE,
		BTN_EQUIP,
		GACHATICKETCOUNTERSRESULT,
		COUNTER_PROGRESSBAR_FOREGROUND,
		COUNTER_LBL,
		S_COUNTER,
		NUMBER_COUNTER_IMG,
		S_AVAILABLE
	}

	private UI[] iconRootAry = new UI[11]
	{
		UI.OBJ_ICON_ROOT_0,
		UI.OBJ_ICON_ROOT_1,
		UI.OBJ_ICON_ROOT_2,
		UI.OBJ_ICON_ROOT_3,
		UI.OBJ_ICON_ROOT_4,
		UI.OBJ_ICON_ROOT_5,
		UI.OBJ_ICON_ROOT_6,
		UI.OBJ_ICON_ROOT_7,
		UI.OBJ_ICON_ROOT_8,
		UI.OBJ_ICON_ROOT_9,
		UI.OBJ_ICON_ROOT_10
	};

	private UI[] iconLevelAry = new UI[11]
	{
		UI.LBL_ENEMY_LV_0,
		UI.LBL_ENEMY_LV_1,
		UI.LBL_ENEMY_LV_2,
		UI.LBL_ENEMY_LV_3,
		UI.LBL_ENEMY_LV_4,
		UI.LBL_ENEMY_LV_5,
		UI.LBL_ENEMY_LV_6,
		UI.LBL_ENEMY_LV_7,
		UI.LBL_ENEMY_LV_8,
		UI.LBL_ENEMY_LV_9,
		UI.LBL_ENEMY_LV_10
	};

	private UI[] rarityAnimRoot = new UI[7]
	{
		UI.OBJ_RARITY_D,
		UI.OBJ_RARITY_C,
		UI.OBJ_RARITY_B,
		UI.OBJ_RARITY_A,
		UI.OBJ_RARITY_S,
		UI.OBJ_RARITY_SS,
		UI.OBJ_RARITY_SSS
	};

	private bool isJumpToBattle;

	public override void Initialize()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		nextGuachaGuarantee = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().gachaGuaranteeCampaignInfo;
		MonoBehaviourSingleton<GachaManager>.I.SetSelectGachaGuarantee(nextGuachaGuarantee);
		LoadingQueue loadQueue = new LoadingQueue(this);
		if (MonoBehaviourSingleton<GachaManager>.I.IsMultiResult())
		{
			yield return (object)LoadMultiResultUI(loadQueue);
		}
		else
		{
			yield return (object)LoadNormalUI(loadQueue);
		}
		base.Initialize();
	}

	protected override IEnumerator LoadMultiResultUI(LoadingQueue loadQueue)
	{
		yield return (object)base.LoadMultiResultUI(loadQueue);
		GetCtrl(UI.BG_MULTI).GetComponent<UISprite>().height = 750;
	}

	private IEnumerator LoadNormalUI(LoadingQueue loadQueue)
	{
		if (nextGuachaGuarantee.IsValid())
		{
			footerRoot = GetCtrl(UI.FOOTER_GUARANTEE_ROOT);
		}
		else
		{
			footerRoot = GetCtrl(UI.FOOTER_ROOT);
		}
		yield return (object)LoadGachaButton(buttonName: CreateButtonName(), loadQueue: loadQueue, parent: FindCtrl(footerRoot, UI.BTN_GACHA));
		yield return (object)LoadGachaGuaranteeCounter(loadQueue, nextGuachaGuarantee, delegate(LoadObject lo_guarantee)
		{
			((_003CLoadNormalUI_003Ec__Iterator4F)/*Error near IL_00e8: stateMachine*/)._003C_003Ef__this.SetTexture(((_003CLoadNormalUI_003Ec__Iterator4F)/*Error near IL_00e8: stateMachine*/)._003C_003Ef__this.footerRoot, UI.TEX_GUARANTEE_COUNT_DOWN, lo_guarantee.loadedObject as Texture);
		});
	}

	public override void UpdateUI()
	{
		bool flag = MonoBehaviourSingleton<GachaManager>.I.selectGacha.num == 1;
		SetActive((Enum)UI.OBJ_SINGLE_ROOT, flag);
		SetActive((Enum)UI.OBJ_MULTI_ROOT, !flag);
		SetActive((Enum)UI.OBJ_BG_SINGLE, flag);
		SetActive((Enum)UI.OBJ_BG_MULTI, !flag);
		if (flag)
		{
			UpdateSingleGachaUI();
		}
		else
		{
			UpdateMultiGachaUI();
		}
		if (MonoBehaviourSingleton<GachaManager>.I.IsMultiResult())
		{
			UpdateMultiResultFooterUI();
		}
		else
		{
			UpdateSingleResultFooterUI();
		}
	}

	protected void UpdateSingleGachaUI()
	{
		string text = string.Empty;
		int star_num = 0;
		GachaResult.GachaReward gachaReward = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward[0];
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)gachaReward.itemId);
		if (questData != null)
		{
			text = questData.questText;
			star_num = (int)questData.difficulty;
		}
		SetLabelText((Enum)UI.LBL_NAME, text);
		RARITY_TYPE[] array = (RARITY_TYPE[])Enum.GetValues(typeof(RARITY_TYPE));
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			SetActive((Enum)rarityAnimRoot[i], questData.rarity == array[i]);
		}
		SetGachaQuestDifficulty(UI.OBJ_DIFFICULTY_ROOT, star_num);
		ResetTween((Enum)UI.OBJ_DIFFICULTY_ROOT, 0);
		ResetTween((Enum)rarityAnimRoot[(int)questData.rarity], 0);
		ResetTween((Enum)UI.OBJ_RARITY_TEXT_ROOT, 0);
		if (questData.rarity <= RARITY_TYPE.C)
		{
			ResetTween((Enum)UI.OBJ_RARITY_LIGHT, 0);
			PlayTween((Enum)UI.OBJ_RARITY_LIGHT, true, (EventDelegate.Callback)null, false, 0);
		}
		PlayTween((Enum)UI.OBJ_RARITY_TEXT_ROOT, true, (EventDelegate.Callback)null, false, 0);
		PlayTween((Enum)rarityAnimRoot[(int)questData.rarity], true, (EventDelegate.Callback)delegate
		{
			PlayTween((Enum)UI.OBJ_DIFFICULTY_ROOT, true, (EventDelegate.Callback)null, false, 0);
		}, false, 0);
		QuestGachaDirectorBase questGachaDirectorBase = AnimationDirector.I as QuestGachaDirectorBase;
		if (questGachaDirectorBase != null)
		{
			questGachaDirectorBase.PlayRarityAudio(questData.rarity, true);
			questGachaDirectorBase.PlayUIRarityEffect(questData.rarity, GetCtrl(UI.OBJ_RARITY_ROOT), GetCtrl(rarityAnimRoot[(int)questData.rarity]));
		}
	}

	protected void UpdateMultiGachaUI()
	{
		int index = 0;
		MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward.ForEach(delegate(GachaResult.GachaReward reward)
		{
			bool flag = false;
			int num = 0;
			QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem((uint)reward.itemId);
			if (questItem != null)
			{
				flag = GameSaveData.instance.IsNewItem(ITEM_ICON_TYPE.QUEST_ITEM, questItem.uniqueID);
				flag = IsNewItemQuestEnemySpecies(questItem);
				num = questItem.infoData.questData.tableData.GetMainEnemyLv();
			}
			bool is_new = flag;
			ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.QUEST_ITEM, (uint)reward.itemId, GetCtrl(iconRootAry[index]), -1, null, 0, is_new, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
			itemIcon.SetEnableCollider(false);
			string text = string.Empty;
			if (num > 0)
			{
				text = string.Format(StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 1u), num.ToString());
			}
			SetLabelText(GetCtrl(iconRootAry[index]), iconLevelAry[index], text);
			SetEvent(GetCtrl(iconRootAry[index]), "QUEST_DETAIL", index);
			index++;
		});
	}

	protected void UpdateSingleResultFooterUI()
	{
		SetActive((Enum)UI.FOOTER_MULTI_RESULT_ROOT, false);
		if (nextGuachaGuarantee.IsValid())
		{
			SetActive((Enum)UI.FOOTER_ROOT, false);
			SetActive((Enum)UI.FOOTER_GUARANTEE_ROOT, true);
			GetCtrl(UI.BG_MULTI).GetComponent<UISprite>().height = 750;
		}
		else
		{
			SetActive((Enum)UI.FOOTER_ROOT, true);
			SetActive((Enum)UI.FOOTER_GUARANTEE_ROOT, false);
			GetCtrl(UI.BG_MULTI).GetComponent<UISprite>().height = 740;
		}
		int num = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0)
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId);
			UITexture[] array = new UITexture[3]
			{
				FindCtrl(FindCtrl(footerRoot, UI.OBJ_GACHA_DISABLE_ROOT), UI.TEX_TICKET).GetComponent<UITexture>(),
				FindCtrl(FindCtrl(footerRoot, UI.OBJ_GACHA_ENABLE_ROOT), UI.TEX_TICKET).GetComponent<UITexture>(),
				FindCtrl(footerRoot, UI.TEX_TICKET_HAVE).GetComponent<UITexture>()
			};
			UITexture[] array2 = array;
			foreach (UITexture ui_tex in array2)
			{
				ResourceLoad.LoadItemIconTexture(ui_tex, itemData.iconID);
			}
			num = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == itemData.id, 1, false);
			SetActive(footerRoot, UI.GACHATICKETCOUNTERSRESULT, true);
			SetActive(footerRoot, UI.S_COUNTER, false);
			SetActive(footerRoot, UI.S_AVAILABLE, true);
			if (MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult() != null && MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().counter >= 0)
			{
				SetActive(footerRoot, UI.GACHATICKETCOUNTERSRESULT, false);
				SetActive(footerRoot, UI.S_COUNTER, false);
				SetActive(footerRoot, UI.S_AVAILABLE, true);
			}
			else if (MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult() != null && MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().counter > 0)
			{
				SetActive(footerRoot, UI.GACHATICKETCOUNTERSRESULT, true);
				SetActive(footerRoot, UI.S_COUNTER, true);
				SetActive(footerRoot, UI.S_AVAILABLE, false);
				SetActive(footerRoot, UI.NUMBER_COUNTER_IMG, true);
				FindCtrl(footerRoot, UI.NUMBER_COUNTER_IMG).GetComponent<UISprite>().spriteName = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().counter.ToString();
				FindCtrl(FindCtrl(footerRoot, UI.GACHATICKETCOUNTERSRESULT), UI.COUNTER_PROGRESSBAR_FOREGROUND).GetComponent<UISprite>().fillAmount = (float)(10 - MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().counter) / 10f;
				SetLabelText(footerRoot, UI.COUNTER_LBL, MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().counter);
			}
			else
			{
				SetActive(footerRoot, UI.GACHATICKETCOUNTERSRESULT, true);
				SetActive(footerRoot, UI.S_COUNTER, false);
				SetActive(footerRoot, UI.S_AVAILABLE, true);
			}
		}
		SetActive(footerRoot, UI.SPR_CRYSTAL, MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId == 0);
		SetActive(footerRoot, UI.TEX_TICKET_HAVE, MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0);
		SetLabelText(footerRoot, UI.LBL_CRYSTAL_NUM, num.ToString());
		bool gachaButtonActive = IsEnableEntry();
		SetGachaButtonActive(gachaButtonActive);
		SetEventDetailImageButton();
	}

	protected void UpdateMultiResultFooterUI()
	{
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<GachaManager>.I.IsExistNextGachaResult())
		{
			SetActive(footerRoot, UI.BTN_NEXT, true);
			SetActive(footerRoot, UI.BTN_BACK, false);
			SetActive(footerRoot, UI.BTN_BATTLE, false);
			SetActive(footerRoot, UI.OBJ_GUARANTEE, true);
			SetActive(footerRoot, UI.SPR_LINE_BOTTOM, false);
			GetCtrl(UI.OBJ_ICONS_ROOT).set_localPosition(new Vector3(0f, 0f, 0f));
		}
		else
		{
			SetActive(footerRoot, UI.BTN_NEXT, false);
			SetActive(footerRoot, UI.BTN_BACK, true);
			SetActive(footerRoot, UI.BTN_BATTLE, true);
			SetActive(footerRoot, UI.OBJ_GUARANTEE, false);
			SetActive(footerRoot, UI.SPR_LINE_BOTTOM, true);
			GetCtrl(UI.OBJ_ICONS_ROOT).set_localPosition(new Vector3(0f, -90f, 0f));
		}
		bool gachaButtonActive = IsEnableEntry();
		SetGachaButtonActive(gachaButtonActive);
		SetEventDetailImageButton();
	}

	private void SetEventDetailImageButton()
	{
		if (isExistDetailButton)
		{
			Transform val = FindCtrl(footerRoot, UI.TEX_GUARANTEE_COUNT_DOWN);
			if (!nextGuachaGuarantee.IsValid() || !nextGuachaGuarantee.IsItemConfirmed())
			{
				val.GetComponent<UIButton>().set_enabled(false);
			}
			else
			{
				REWARD_TYPE type = (REWARD_TYPE)nextGuachaGuarantee.type;
				if (type != REWARD_TYPE.SKILL_ITEM)
				{
					val.GetComponent<UIButton>().set_enabled(false);
				}
				else
				{
					val.GetComponent<UIButton>().set_enabled(true);
					SetEvent(val, "SKILL_DETAIL", new object[2]
					{
						ItemDetailEquip.CURRENT_SECTION.SHOP_TOP,
						Singleton<SkillItemTable>.I.GetSkillItemData((uint)nextGuachaGuarantee.itemId)
					});
				}
			}
		}
	}

	private bool IsNewItemQuestEnemySpecies(QuestItemInfo questItem)
	{
		bool result = true;
		if (questItem == null)
		{
			return result;
		}
		QuestInfoData infoData = questItem.infoData;
		if (infoData == null)
		{
			return result;
		}
		QuestInfoData.Quest questData = infoData.questData;
		if (questData == null)
		{
			return result;
		}
		QuestTable.QuestTableData tableData = questData.tableData;
		if (tableData == null)
		{
			return result;
		}
		ClearStatusQuestEnemySpecies clearStatusQuestEnemySpecies = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestEnemySpecies(tableData.questID);
		if (clearStatusQuestEnemySpecies == null)
		{
			return result;
		}
		if (clearStatusQuestEnemySpecies.questStatus != 1)
		{
			result = false;
		}
		return result;
	}

	private void OnQuery_QUEST_DETAIL()
	{
		int num = (int)GameSection.GetEventData();
		int count = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward.Count;
		if (num < 0 || num >= count)
		{
			GameSection.StopEvent();
		}
		else
		{
			QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem((uint)MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward[num].itemId);
			if (questItem == null)
			{
				GameSection.StopEvent();
			}
			else
			{
				QuestSortData questSortData = new QuestSortData();
				questSortData.SetItem(questItem);
				GameSection.SetEventData(questSortData);
			}
		}
	}

	private void OnQuery_GachaResultToBattleConfirm_YES()
	{
		isJumpToBattle = true;
	}

	private void OnQuery_GachaResultToBattleConfirm_NO()
	{
		isJumpToBattle = false;
	}

	private void OnCloseDialog_GachaResultToBattleConfirm()
	{
		if (isJumpToBattle)
		{
			EventData[] array = null;
			bool flag = MonoBehaviourSingleton<GachaManager>.I.selectGacha.num == 1;
			string name = (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge()) ? "MAIN_MENU_HOME" : "MAIN_MENU_LOUNGE";
			array = ((!flag) ? new EventData[3]
			{
				new EventData(name, null),
				new EventData("GACHA_QUEST_COUNTER", null),
				new EventData("TO_GACHA_QUEST_COUNTER", null)
			} : new EventData[3]
			{
				new EventData(name, null),
				new EventData("GACHA_QUEST_COUNTER", null),
				new EventData("TO_GACHA_QUEST_COUNTER", null)
			});
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array);
			isJumpToBattle = false;
		}
	}

	protected override void OnDestroy()
	{
		if (!AppMain.isApplicationQuit)
		{
			_OnDestroy();
			if (!isRetry && AnimationDirector.I != null)
			{
				AnimationDirector.I.Reset();
			}
		}
	}

	protected void _OnDestroy()
	{
		base.OnDestroy();
	}

	protected void SetGachaQuestDifficulty(Enum _enum, int star_num)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(_enum);
		int i = 0;
		for (int childCount = ctrl.get_childCount(); i < childCount; i++)
		{
			Transform val = ctrl.GetChild(i);
			val.get_gameObject().SetActive(i <= star_num);
		}
		ctrl.GetComponent<UIGrid>().Reposition();
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_USER_STATUS) != (NOTIFY_FLAG)0L)
		{
			CheckUpdateCrystalNum();
			if (!isRetry)
			{
				SetLabelText(footerRoot, UI.LBL_CRYSTAL_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal.ToString());
			}
		}
		base.OnNotify(flags);
	}

	private void OnQuery_RESET()
	{
		TEST_RESET(RARITY_TYPE.D, DIFFICULTY_TYPE.LV10);
	}

	private void OnQuery_D()
	{
		TEST_RESET(RARITY_TYPE.D, DIFFICULTY_TYPE.LV10);
		TEST_PLAY(RARITY_TYPE.D, DIFFICULTY_TYPE.LV10);
	}

	private void OnQuery_C()
	{
		TEST_RESET(RARITY_TYPE.C, DIFFICULTY_TYPE.LV10);
		TEST_PLAY(RARITY_TYPE.C, DIFFICULTY_TYPE.LV10);
	}

	private void OnQuery_B()
	{
		TEST_RESET(RARITY_TYPE.B, DIFFICULTY_TYPE.LV10);
		TEST_PLAY(RARITY_TYPE.B, DIFFICULTY_TYPE.LV10);
	}

	private void OnQuery_A()
	{
		TEST_RESET(RARITY_TYPE.A, DIFFICULTY_TYPE.LV10);
		TEST_PLAY(RARITY_TYPE.A, DIFFICULTY_TYPE.LV10);
	}

	private void OnQuery_S()
	{
		TEST_RESET(RARITY_TYPE.S, DIFFICULTY_TYPE.LV10);
		TEST_PLAY(RARITY_TYPE.S, DIFFICULTY_TYPE.LV10);
	}

	private void OnQuery_SS()
	{
		TEST_RESET(RARITY_TYPE.SS, DIFFICULTY_TYPE.LV10);
		TEST_PLAY(RARITY_TYPE.SS, DIFFICULTY_TYPE.LV10);
	}

	private void OnQuery_SSS()
	{
		TEST_RESET(RARITY_TYPE.SSS, DIFFICULTY_TYPE.LV10);
		TEST_PLAY(RARITY_TYPE.SSS, DIFFICULTY_TYPE.LV10);
	}

	private void TEST_RESET(RARITY_TYPE rarity, DIFFICULTY_TYPE difficulty)
	{
		RARITY_TYPE[] array = (RARITY_TYPE[])Enum.GetValues(typeof(RARITY_TYPE));
		int star_num = (int)(difficulty + 1);
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			SetActive((Enum)rarityAnimRoot[i], rarity == array[i]);
		}
		SetGachaQuestDifficulty(UI.OBJ_DIFFICULTY_ROOT, star_num);
		ResetTween((Enum)UI.OBJ_DIFFICULTY_ROOT, 0);
		ResetTween((Enum)rarityAnimRoot[(int)rarity], 0);
		ResetTween((Enum)UI.OBJ_RARITY_TEXT_ROOT, 0);
		if (rarity <= RARITY_TYPE.C)
		{
			ResetTween((Enum)UI.OBJ_RARITY_LIGHT, 0);
		}
	}

	private void TEST_PLAY(RARITY_TYPE rarity, DIFFICULTY_TYPE difficulty)
	{
		if (rarity <= RARITY_TYPE.C)
		{
			PlayTween((Enum)UI.OBJ_RARITY_LIGHT, true, (EventDelegate.Callback)null, false, 0);
		}
		PlayTween((Enum)UI.OBJ_RARITY_TEXT_ROOT, true, (EventDelegate.Callback)null, false, 0);
		PlayTween((Enum)rarityAnimRoot[(int)rarity], true, (EventDelegate.Callback)delegate
		{
			PlayTween((Enum)UI.OBJ_DIFFICULTY_ROOT, true, (EventDelegate.Callback)null, false, 0);
		}, false, 0);
		if (AnimationDirector.I is QuestGachaDirectorBase)
		{
			(AnimationDirector.I as QuestGachaDirectorBase).PlayUIRarityEffect(rarity, GetCtrl(UI.OBJ_RARITY_ROOT), GetCtrl(rarityAnimRoot[(int)rarity]));
		}
	}
}
