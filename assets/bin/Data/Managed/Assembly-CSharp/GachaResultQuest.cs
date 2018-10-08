using Network;
using System;
using System.Collections;
using UnityEngine;

public class GachaResultQuest : GachaResultBase
{
	private enum UI
	{
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
		BTN_GACHA,
		OBJ_GACHA_ENABLE_ROOT,
		OBJ_GACHA_DISABLE_ROOT,
		OBJ_BG_SINGLE,
		OBJ_BG_MULTI,
		FOOTER_ROOT,
		FOOTER_GUARANTEE_ROOT,
		BG_MULTI,
		FOOTER_ROOT_OFFER,
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
		LoadingQueue loadQueue = new LoadingQueue(this);
		nextGuachaGuarantee = MonoBehaviourSingleton<GachaManager>.I.gachaResult.gachaGuaranteeCampaignInfo;
		MonoBehaviourSingleton<GachaManager>.I.SetSelectGachaGuarantee(nextGuachaGuarantee);
		if (nextGuachaGuarantee.IsValid())
		{
			footerRoot = GetCtrl(UI.FOOTER_GUARANTEE_ROOT);
			SetActive((Enum)UI.FOOTER_ROOT, false);
			SetActive((Enum)UI.FOOTER_GUARANTEE_ROOT, true);
			GetCtrl(UI.BG_MULTI).GetComponent<UISprite>().height = 750;
		}
		else
		{
			footerRoot = GetCtrl(UI.FOOTER_ROOT);
			SetActive((Enum)UI.FOOTER_ROOT, true);
			SetActive((Enum)UI.FOOTER_GUARANTEE_ROOT, false);
			GetCtrl(UI.BG_MULTI).GetComponent<UISprite>().height = 740;
		}
		Debug.Log((object)("counter: " + MonoBehaviourSingleton<GachaManager>.I.gachaResult.counter));
		Debug.Log((object)("oncePurchasedState: " + MonoBehaviourSingleton<GachaManager>.I.gachaResult.oncePurchasedState));
		if (MonoBehaviourSingleton<GachaManager>.I.gachaResult.oncePurchasedState)
		{
			SetActive((Enum)UI.FOOTER_ROOT, false);
			SetActive((Enum)UI.FOOTER_GUARANTEE_ROOT, false);
			SetActive((Enum)UI.FOOTER_ROOT_OFFER, true);
		}
		string empty = string.Empty;
		string buttonName = (MonoBehaviourSingleton<GachaManager>.I.gachaResult.counter != 0 || MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId == 0) ? CreateButtonName() : "BTN_GACHA_TICKET1_Skaku_RESULT";
		LoadObject lo_button = loadQueue.Load(RESOURCE_CATEGORY.GACHA_BUTTON, buttonName, false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		buttonObj = (Object.Instantiate(lo_button.loadedObject) as GameObject);
		buttonObj.get_transform().set_parent(FindCtrl(footerRoot, UI.BTN_GACHA));
		buttonObj.get_transform().set_name(UI.BTN_GACHA.ToString());
		buttonObj.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
		buttonObj.get_transform().set_localPosition(new Vector3(0f, 0f, 0f));
		if (nextGuachaGuarantee.IsValid())
		{
			bool isGuaranteeLoaded = false;
			yield return (object)LoadGachaGuaranteeCounter(delegate(LoadObject lo_guarantee)
			{
				((_003CDoInitialize_003Ec__Iterator45)/*Error near IL_0361: stateMachine*/)._003CisGuaranteeLoaded_003E__3 = true;
				((_003CDoInitialize_003Ec__Iterator45)/*Error near IL_0361: stateMachine*/)._003C_003Ef__this.SetTexture(((_003CDoInitialize_003Ec__Iterator45)/*Error near IL_0361: stateMachine*/)._003C_003Ef__this.footerRoot, UI.TEX_GUARANTEE_COUNT_DOWN, lo_guarantee.loadedObject as Texture);
			});
			if (!isGuaranteeLoaded)
			{
				yield return (object)loadQueue.Wait();
			}
			Transform guaranteeCountDown = FindCtrl(footerRoot, UI.TEX_GUARANTEE_COUNT_DOWN);
			if (!nextGuachaGuarantee.IsItemConfirmed())
			{
				guaranteeCountDown.GetComponent<UIButton>().set_enabled(false);
			}
			else
			{
				REWARD_TYPE type = (REWARD_TYPE)nextGuachaGuarantee.type;
				if (type != REWARD_TYPE.SKILL_ITEM)
				{
					guaranteeCountDown.GetComponent<UIButton>().set_enabled(false);
				}
				else
				{
					guaranteeCountDown.GetComponent<UIButton>().set_enabled(true);
					SetEvent(guaranteeCountDown, "SKILL_DETAIL", new object[2]
					{
						ItemDetailEquip.CURRENT_SECTION.SHOP_TOP,
						Singleton<SkillItemTable>.I.GetSkillItemData((uint)nextGuachaGuarantee.itemId)
					});
				}
			}
		}
		base.Initialize();
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
			string text = string.Empty;
			int star_num = 0;
			GachaResult.GachaReward gachaReward = MonoBehaviourSingleton<GachaManager>.I.gachaResult.reward[0];
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
		else
		{
			int index = 0;
			MonoBehaviourSingleton<GachaManager>.I.gachaResult.reward.ForEach(delegate(GachaResult.GachaReward reward)
			{
				bool flag2 = false;
				int num3 = 0;
				QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem((uint)reward.itemId);
				if (questItem != null)
				{
					flag2 = GameSaveData.instance.IsNewItem(ITEM_ICON_TYPE.QUEST_ITEM, questItem.uniqueID);
					flag2 = IsNewItemQuestEnemySpecies(questItem);
					num3 = questItem.infoData.questData.tableData.GetMainEnemyLv();
				}
				bool is_new = flag2;
				ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.QUEST_ITEM, (uint)reward.itemId, GetCtrl(iconRootAry[index]), -1, null, 0, is_new, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
				itemIcon.SetEnableCollider(false);
				string text2 = string.Empty;
				if (num3 > 0)
				{
					text2 = string.Format(StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 1u), num3.ToString());
				}
				SetLabelText(GetCtrl(iconRootAry[index]), iconLevelAry[index], text2);
				SetEvent(GetCtrl(iconRootAry[index]), "QUEST_DETAIL", index);
				index++;
			});
		}
		int num2 = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0)
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId);
			UITexture[] array2 = new UITexture[3]
			{
				FindCtrl(FindCtrl(footerRoot, UI.OBJ_GACHA_DISABLE_ROOT), UI.TEX_TICKET).GetComponent<UITexture>(),
				FindCtrl(FindCtrl(footerRoot, UI.OBJ_GACHA_ENABLE_ROOT), UI.TEX_TICKET).GetComponent<UITexture>(),
				FindCtrl(footerRoot, UI.TEX_TICKET_HAVE).GetComponent<UITexture>()
			};
			UITexture[] array3 = array2;
			foreach (UITexture ui_tex in array3)
			{
				ResourceLoad.LoadItemIconTexture(ui_tex, itemData.iconID);
			}
			num2 = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == itemData.id, 1, false);
			if (MonoBehaviourSingleton<GachaManager>.I.gachaResult.counter >= 0)
			{
				SetActive(footerRoot, UI.GACHATICKETCOUNTERSRESULT, false);
				SetActive(footerRoot, UI.S_COUNTER, false);
				SetActive(footerRoot, UI.S_AVAILABLE, true);
			}
			else if (MonoBehaviourSingleton<GachaManager>.I.gachaResult.counter > 0)
			{
				SetActive(footerRoot, UI.GACHATICKETCOUNTERSRESULT, true);
				SetActive(footerRoot, UI.S_COUNTER, true);
				SetActive(footerRoot, UI.S_AVAILABLE, false);
				SetActive(footerRoot, UI.NUMBER_COUNTER_IMG, true);
				FindCtrl(footerRoot, UI.NUMBER_COUNTER_IMG).GetComponent<UISprite>().spriteName = MonoBehaviourSingleton<GachaManager>.I.gachaResult.counter.ToString();
				FindCtrl(FindCtrl(footerRoot, UI.GACHATICKETCOUNTERSRESULT), UI.COUNTER_PROGRESSBAR_FOREGROUND).GetComponent<UISprite>().fillAmount = (float)(10 - MonoBehaviourSingleton<GachaManager>.I.gachaResult.counter) / 10f;
				SetLabelText(footerRoot, UI.COUNTER_LBL, MonoBehaviourSingleton<GachaManager>.I.gachaResult.counter);
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
		SetLabelText(footerRoot, UI.LBL_CRYSTAL_NUM, num2.ToString());
		if (MonoBehaviourSingleton<GachaManager>.I.gachaResult.gachaGuaranteeCampaignInfo == null)
		{
			SetGachaButtonActive(!MonoBehaviourSingleton<GachaManager>.I.IsSelectTutorialGacha() && MonoBehaviourSingleton<GachaManager>.I.gachaResult.remainCount != 0);
		}
		else
		{
			SetGachaButtonActive(!MonoBehaviourSingleton<GachaManager>.I.IsSelectTutorialGacha());
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
		int count = MonoBehaviourSingleton<GachaManager>.I.gachaResult.reward.Count;
		if (num < 0 || num >= count)
		{
			GameSection.StopEvent();
		}
		else
		{
			QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem((uint)MonoBehaviourSingleton<GachaManager>.I.gachaResult.reward[num].itemId);
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

	protected override void SetGachaButtonActive(bool enableRetry)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Expected O, but got Unknown
		Transform root = FindCtrl(buttonObj.get_transform(), (!enableRetry) ? UI.OBJ_GACHA_DISABLE_ROOT : UI.OBJ_GACHA_ENABLE_ROOT);
		SetActive(buttonObj.get_transform(), UI.OBJ_GACHA_ENABLE_ROOT, enableRetry);
		SetActive(buttonObj.get_transform(), UI.OBJ_GACHA_DISABLE_ROOT, !enableRetry);
		int num = (MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId <= 0) ? GetCrystalNum() : MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum;
		SetLabelText(root, UI.LBL_PRICE, num.ToString());
		if (!enableRetry)
		{
			SetButtonEnabled(buttonObj.get_transform(), false);
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
