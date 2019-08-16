using Network;
using System;
using System.Collections;
using System.Collections.Generic;
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
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		currentGachaGuarantee = MonoBehaviourSingleton<GachaManager>.I.selectGachaGuarantee;
		nextGachaGuarantee = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().gachaGuaranteeCampaignInfo;
		MonoBehaviourSingleton<GachaManager>.I.SetSelectGachaGuarantee(nextGachaGuarantee);
		LoadingQueue loadQueue = new LoadingQueue(this);
		if (MonoBehaviourSingleton<GachaManager>.I.IsResultBonus() && !MonoBehaviourSingleton<GachaManager>.I.IsExistNextGachaResult())
		{
			yield return LoadFeverResultUI(loadQueue);
		}
		else if (MonoBehaviourSingleton<GachaManager>.I.IsMultiResult())
		{
			yield return LoadMultiResultUI(loadQueue);
		}
		else
		{
			yield return LoadNormalUI(loadQueue);
		}
		base.Initialize();
	}

	protected IEnumerator LoadFeverResultUI(LoadingQueue loadQueue)
	{
		SetActive((Enum)UI.FOOTER_ROOT, is_visible: false);
		if (MonoBehaviourSingleton<GachaManager>.I.enableFeverDirector)
		{
			SetActive((Enum)UI.FOOTER_GUARANTEE_ROOT, is_visible: true);
			SetActive((Enum)UI.FOOTER_MULTI_RESULT_ROOT, is_visible: false);
			footerRoot = GetCtrl(UI.FOOTER_GUARANTEE_ROOT);
			yield return LoadGachaButton(buttonName: CreateButtonName(), loadQueue: loadQueue, parent: FindCtrl(footerRoot, UI.BTN_GACHA));
			yield return LoadGachaGuaranteeCounter(loadQueue, nextGachaGuarantee, delegate(LoadObject lo_guarantee)
			{
				SetTexture(footerRoot, UI.TEX_GUARANTEE_COUNT_DOWN, lo_guarantee.loadedObject as Texture);
			});
		}
		else
		{
			SetActive((Enum)UI.FOOTER_GUARANTEE_ROOT, is_visible: false);
			SetActive((Enum)UI.FOOTER_MULTI_RESULT_ROOT, is_visible: true);
			footerRoot = GetCtrl(UI.FOOTER_MULTI_RESULT_ROOT);
			yield return LoadFeverGachaGuaranteeCounter(loadQueue, currentGachaGuarantee, delegate(LoadObject lo_guarantee)
			{
				SetTexture(footerRoot, UI.TEX_GUARANTEE_COUNT_DOWN, lo_guarantee.loadedObject as Texture);
			});
			yield return LoadGachaButton(loadQueue, FindCtrl(footerRoot, UI.BTN_NEXT), "BTN_GACHA_FEVER_NEXT");
		}
	}

	protected IEnumerator LoadFeverGachaGuaranteeCounter(LoadingQueue loadQueue, GachaGuaranteeCampaignInfo guarantee, Action<LoadObject> callback)
	{
		string imgName = string.Empty;
		if (guarantee.IsValid())
		{
			imgName = guarantee.GetTitleImageName() + "_FEVER";
		}
		if (imgName != string.Empty)
		{
			LoadObject loadObject = loadQueue.Load(RESOURCE_CATEGORY.GACHA_GUARANTEE_COUNTER, imgName);
			if (loadQueue.IsLoading())
			{
				yield return loadQueue.Wait();
			}
			callback(loadObject);
		}
	}

	protected override IEnumerator LoadMultiResultUI(LoadingQueue loadQueue)
	{
		yield return base.LoadMultiResultUI(loadQueue);
		GetCtrl(UI.BG_MULTI).GetComponent<UISprite>().height = 750;
	}

	private IEnumerator LoadNormalUI(LoadingQueue loadQueue)
	{
		if (nextGachaGuarantee.IsValid())
		{
			footerRoot = GetCtrl(UI.FOOTER_GUARANTEE_ROOT);
		}
		else
		{
			footerRoot = GetCtrl(UI.FOOTER_ROOT);
		}
		yield return LoadGachaButton(buttonName: CreateButtonName(), loadQueue: loadQueue, parent: FindCtrl(footerRoot, UI.BTN_GACHA));
		yield return LoadGachaGuaranteeCounter(loadQueue, nextGachaGuarantee, delegate(LoadObject lo_guarantee)
		{
			SetTexture(footerRoot, UI.TEX_GUARANTEE_COUNT_DOWN, lo_guarantee.loadedObject as Texture);
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
		if (!MonoBehaviourSingleton<GachaManager>.I.IsExistNextGachaResult() && MonoBehaviourSingleton<GachaManager>.I.IsResultBonus())
		{
			UpdateFeverResultFooterUI();
		}
		else if (MonoBehaviourSingleton<GachaManager>.I.IsMultiResult())
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
			PlayTween((Enum)UI.OBJ_RARITY_LIGHT, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
		}
		PlayTween((Enum)UI.OBJ_RARITY_TEXT_ROOT, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
		PlayTween((Enum)rarityAnimRoot[(int)questData.rarity], forward: true, (EventDelegate.Callback)delegate
		{
			PlayTween((Enum)UI.OBJ_DIFFICULTY_ROOT, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
		}, is_input_block: false, 0);
		QuestGachaDirectorBase questGachaDirectorBase = AnimationDirector.I as QuestGachaDirectorBase;
		if (questGachaDirectorBase != null)
		{
			questGachaDirectorBase.PlayRarityAudio(questData.rarity, ignore_skip_check: true);
			questGachaDirectorBase.PlayUIRarityEffect(questData.rarity, GetCtrl(UI.OBJ_RARITY_ROOT), GetCtrl(rarityAnimRoot[(int)questData.rarity]));
		}
	}

	protected void UpdateMultiGachaUI()
	{
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		List<GachaResult.GachaReward> list = (!MonoBehaviourSingleton<GachaManager>.I.enableFeverDirector) ? MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward : MonoBehaviourSingleton<GachaManager>.I.gachaResultBonus.reward;
		int index = 0;
		list.ForEach(delegate(GachaResult.GachaReward reward)
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
			REWARD_TYPE rewardType = REWARD_TYPE.QUEST_ITEM;
			uint itemId = (uint)reward.itemId;
			Transform ctrl = GetCtrl(iconRootAry[index]);
			bool is_new = flag;
			ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(rewardType, itemId, ctrl, -1, null, 0, is_new);
			itemIcon.SetEnableCollider(is_enable: false);
			string text = string.Empty;
			if (num > 0)
			{
				text = string.Format(StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 1u), num.ToString());
			}
			SetLabelText(GetCtrl(iconRootAry[index]), iconLevelAry[index], text);
			SetEvent(GetCtrl(iconRootAry[index]), "QUEST_DETAIL", index);
			index++;
		});
		for (int i = index; i < iconRootAry.Length; i++)
		{
			SetActive((Enum)iconRootAry[i], is_visible: false);
		}
		Vector3[] array = null;
		switch (list.Count)
		{
		case 2:
			array = (Vector3[])new Vector3[2]
			{
				new Vector3(-90f, -160f),
				new Vector3(90f, -160f)
			};
			break;
		case 4:
			array = (Vector3[])new Vector3[4]
			{
				new Vector3(-90f, -160f),
				new Vector3(90f, -160f),
				new Vector3(-90f, -296f),
				new Vector3(90f, -296f)
			};
			break;
		case 7:
			array = (Vector3[])new Vector3[7]
			{
				new Vector3(-90f, -92f),
				new Vector3(90f, -92f),
				new Vector3(-90f, -228f),
				new Vector3(90f, -228f),
				new Vector3(-133f, -364f),
				new Vector3(0f, -364f),
				new Vector3(133f, -364f)
			};
			break;
		case 9:
			array = (Vector3[])new Vector3[9]
			{
				new Vector3(-133f, -92f),
				new Vector3(0f, -92f),
				new Vector3(133f, -92f),
				new Vector3(-133f, -228f),
				new Vector3(0f, -228f),
				new Vector3(133f, -228f),
				new Vector3(-133f, -364f),
				new Vector3(0f, -364f),
				new Vector3(133f, -364f)
			};
			break;
		}
		if (array != null)
		{
			for (int j = 0; j < list.Count; j++)
			{
				GetCtrl(iconRootAry[j]).set_localPosition(array[j]);
			}
		}
	}

	protected void UpdateSingleResultFooterUI()
	{
		SetActive((Enum)UI.FOOTER_MULTI_RESULT_ROOT, is_visible: false);
		if (nextGachaGuarantee.IsValid())
		{
			SetActive((Enum)UI.FOOTER_ROOT, is_visible: false);
			SetActive((Enum)UI.FOOTER_GUARANTEE_ROOT, is_visible: true);
			GetCtrl(UI.BG_MULTI).GetComponent<UISprite>().height = 750;
		}
		else
		{
			SetActive((Enum)UI.FOOTER_ROOT, is_visible: true);
			SetActive((Enum)UI.FOOTER_GUARANTEE_ROOT, is_visible: false);
			GetCtrl(UI.BG_MULTI).GetComponent<UISprite>().height = 740;
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_WIN))
		{
			SetActive(footerRoot, UI.BTN_BACK, is_visible: false);
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
			num = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == itemData.id, 1);
			SetActive(footerRoot, UI.GACHATICKETCOUNTERSRESULT, is_visible: true);
			SetActive(footerRoot, UI.S_COUNTER, is_visible: false);
			SetActive(footerRoot, UI.S_AVAILABLE, is_visible: true);
			if (MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult() != null && MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().counter >= 0)
			{
				SetActive(footerRoot, UI.GACHATICKETCOUNTERSRESULT, is_visible: false);
				SetActive(footerRoot, UI.S_COUNTER, is_visible: false);
				SetActive(footerRoot, UI.S_AVAILABLE, is_visible: true);
			}
			else if (MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult() != null && MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().counter > 0)
			{
				SetActive(footerRoot, UI.GACHATICKETCOUNTERSRESULT, is_visible: true);
				SetActive(footerRoot, UI.S_COUNTER, is_visible: true);
				SetActive(footerRoot, UI.S_AVAILABLE, is_visible: false);
				SetActive(footerRoot, UI.NUMBER_COUNTER_IMG, is_visible: true);
				FindCtrl(footerRoot, UI.NUMBER_COUNTER_IMG).GetComponent<UISprite>().spriteName = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().counter.ToString();
				FindCtrl(FindCtrl(footerRoot, UI.GACHATICKETCOUNTERSRESULT), UI.COUNTER_PROGRESSBAR_FOREGROUND).GetComponent<UISprite>().fillAmount = (float)(10 - MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().counter) / 10f;
				SetLabelText(footerRoot, UI.COUNTER_LBL, MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().counter);
			}
			else
			{
				SetActive(footerRoot, UI.GACHATICKETCOUNTERSRESULT, is_visible: true);
				SetActive(footerRoot, UI.S_COUNTER, is_visible: false);
				SetActive(footerRoot, UI.S_AVAILABLE, is_visible: true);
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
			SetActive(footerRoot, UI.BTN_NEXT, is_visible: true);
			SetActive(footerRoot, UI.BTN_BACK, is_visible: false);
			SetActive(footerRoot, UI.BTN_BATTLE, is_visible: false);
			SetActive(footerRoot, UI.OBJ_GUARANTEE, is_visible: true);
			SetActive(footerRoot, UI.SPR_LINE_BOTTOM, is_visible: false);
			GetCtrl(UI.OBJ_ICONS_ROOT).set_localPosition(new Vector3(0f, 0f, 0f));
		}
		else
		{
			SetActive(footerRoot, UI.BTN_NEXT, is_visible: false);
			SetActive(footerRoot, UI.BTN_BACK, is_visible: true);
			SetActive(footerRoot, UI.BTN_BATTLE, is_visible: true);
			SetActive(footerRoot, UI.OBJ_GUARANTEE, is_visible: false);
			SetActive(footerRoot, UI.SPR_LINE_BOTTOM, is_visible: true);
			GetCtrl(UI.OBJ_ICONS_ROOT).set_localPosition(new Vector3(0f, -90f, 0f));
		}
		bool gachaButtonActive = IsEnableEntry();
		SetGachaButtonActive(gachaButtonActive);
		SetEventDetailImageButton();
	}

	protected void UpdateFeverResultFooterUI()
	{
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<GachaManager>.I.enableFeverDirector)
		{
			SetActive(footerRoot, UI.BTN_BACK, is_visible: true);
			SetActive(footerRoot, UI.BTN_BATTLE, is_visible: true);
			SetActive(footerRoot, UI.OBJ_GUARANTEE, is_visible: true);
			SetLabelText(footerRoot, UI.LBL_CRYSTAL_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal.ToString());
		}
		else
		{
			SetActive(footerRoot, UI.BTN_BACK, is_visible: false);
			SetActive(footerRoot, UI.BTN_BATTLE, is_visible: false);
			SetActive(footerRoot, UI.OBJ_GUARANTEE, is_visible: true);
			SetActive(footerRoot, UI.SPR_LINE_BOTTOM, is_visible: false);
			GetCtrl(UI.BG_MULTI).GetComponent<UISprite>().height = 830;
			FindCtrl(footerRoot, UI.BTN_NEXT).set_localPosition(new Vector3(5f, -258f));
		}
		bool gachaButtonActive = IsEnableEntry();
		SetGachaButtonActive(gachaButtonActive);
		SetEventDetailImageButton();
	}

	private void SetEventDetailImageButton()
	{
		Transform val = FindCtrl(footerRoot, UI.TEX_GUARANTEE_COUNT_DOWN);
		if (val == null)
		{
			return;
		}
		if (isExistDetailButton)
		{
			if (!nextGachaGuarantee.IsValid() || !nextGachaGuarantee.IsItemConfirmed())
			{
				if (!nextGachaGuarantee.link.IsNullOrWhiteSpace())
				{
					val.GetComponent<UIButton>().set_enabled(true);
					SetEvent(val, "GUARANTEE_GACHA_DETAIL_WEB", nextGachaGuarantee.link);
				}
				else
				{
					val.GetComponent<UIButton>().set_enabled(false);
				}
				return;
			}
			switch (nextGachaGuarantee.type)
			{
			default:
				val.GetComponent<UIButton>().set_enabled(false);
				break;
			case 5:
				val.GetComponent<UIButton>().set_enabled(true);
				SetEvent(val, "SKILL_DETAIL", new object[2]
				{
					ItemDetailEquip.CURRENT_SECTION.SHOP_TOP,
					Singleton<SkillItemTable>.I.GetSkillItemData((uint)nextGachaGuarantee.itemId)
				});
				break;
			case 14:
			{
				val.GetComponent<UIButton>().set_enabled(true);
				AccessorySortData accessorySortData = new AccessorySortData();
				AccessoryInfo accessoryInfo = new AccessoryInfo();
				accessoryInfo.SetValue((uint)nextGachaGuarantee.itemId);
				accessorySortData.SetItem(accessoryInfo);
				SetEvent(val, "ACCESSORY_SELECT", new object[2]
				{
					ItemDetailEquip.CURRENT_SECTION.SHOP_TOP,
					accessorySortData
				});
				break;
			}
			}
		}
		else
		{
			val.GetComponent<UIButton>().set_enabled(false);
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
		List<GachaResult.GachaReward> list = (!MonoBehaviourSingleton<GachaManager>.I.enableFeverDirector) ? MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward : MonoBehaviourSingleton<GachaManager>.I.gachaResultBonus.reward;
		int count = list.Count;
		if (num < 0 || num >= count)
		{
			GameSection.StopEvent();
			return;
		}
		QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem((uint)list[num].itemId);
		if (questItem == null)
		{
			GameSection.StopEvent();
			return;
		}
		QuestSortData questSortData = new QuestSortData();
		questSortData.SetItem(questItem);
		GameSection.SetEventData(questSortData);
	}

	private void OnQuery_BATTLE()
	{
		isJumpToBattle = true;
		OnCloseDialog_GachaResultToBattleConfirm();
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
			string goingHomeEvent = GameSection.GetGoingHomeEvent();
			array = ((!flag) ? new EventData[3]
			{
				new EventData(goingHomeEvent, null),
				new EventData("GACHA_QUEST_COUNTER", null),
				new EventData("TO_GACHA_QUEST_COUNTER", null)
			} : new EventData[3]
			{
				new EventData(goingHomeEvent, null),
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
		Transform ctrl = GetCtrl(_enum);
		int i = 0;
		for (int childCount = ctrl.get_childCount(); i < childCount; i++)
		{
			Transform child = ctrl.GetChild(i);
			child.get_gameObject().SetActive(i <= star_num);
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
			PlayTween((Enum)UI.OBJ_RARITY_LIGHT, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
		}
		PlayTween((Enum)UI.OBJ_RARITY_TEXT_ROOT, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
		PlayTween((Enum)rarityAnimRoot[(int)rarity], forward: true, (EventDelegate.Callback)delegate
		{
			PlayTween((Enum)UI.OBJ_DIFFICULTY_ROOT, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
		}, is_input_block: false, 0);
		if (AnimationDirector.I is QuestGachaDirectorBase)
		{
			(AnimationDirector.I as QuestGachaDirectorBase).PlayUIRarityEffect(rarity, GetCtrl(UI.OBJ_RARITY_ROOT), GetCtrl(rarityAnimRoot[(int)rarity]));
		}
	}
}
