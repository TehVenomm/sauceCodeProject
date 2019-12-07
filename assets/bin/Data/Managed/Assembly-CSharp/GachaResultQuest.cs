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
		StartCoroutine(DoInitialize());
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
		SetActive(UI.FOOTER_ROOT, is_visible: false);
		if (MonoBehaviourSingleton<GachaManager>.I.enableFeverDirector)
		{
			SetActive(UI.FOOTER_GUARANTEE_ROOT, is_visible: true);
			SetActive(UI.FOOTER_MULTI_RESULT_ROOT, is_visible: false);
			footerRoot = GetCtrl(UI.FOOTER_GUARANTEE_ROOT);
			string buttonName = CreateButtonName();
			yield return LoadGachaButton(loadQueue, FindCtrl(footerRoot, UI.BTN_GACHA), buttonName);
			yield return LoadGachaGuaranteeCounter(loadQueue, nextGachaGuarantee, delegate(LoadObject lo_guarantee)
			{
				SetTexture(footerRoot, UI.TEX_GUARANTEE_COUNT_DOWN, lo_guarantee.loadedObject as Texture);
			});
		}
		else
		{
			SetActive(UI.FOOTER_GUARANTEE_ROOT, is_visible: false);
			SetActive(UI.FOOTER_MULTI_RESULT_ROOT, is_visible: true);
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
		string text = "";
		if (guarantee.IsValid())
		{
			text = guarantee.GetTitleImageName() + "_FEVER";
		}
		if (text != "")
		{
			LoadObject loadObject = loadQueue.Load(RESOURCE_CATEGORY.GACHA_GUARANTEE_COUNTER, text);
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
		string buttonName = CreateButtonName();
		yield return LoadGachaButton(loadQueue, FindCtrl(footerRoot, UI.BTN_GACHA), buttonName);
		yield return LoadGachaGuaranteeCounter(loadQueue, nextGachaGuarantee, delegate(LoadObject lo_guarantee)
		{
			SetTexture(footerRoot, UI.TEX_GUARANTEE_COUNT_DOWN, lo_guarantee.loadedObject as Texture);
		});
	}

	public override void UpdateUI()
	{
		bool flag = MonoBehaviourSingleton<GachaManager>.I.selectGacha.num == 1;
		SetActive(UI.OBJ_SINGLE_ROOT, flag);
		SetActive(UI.OBJ_MULTI_ROOT, !flag);
		SetActive(UI.OBJ_BG_SINGLE, flag);
		SetActive(UI.OBJ_BG_MULTI, !flag);
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
		SetLabelText(UI.LBL_NAME, text);
		RARITY_TYPE[] array = (RARITY_TYPE[])Enum.GetValues(typeof(RARITY_TYPE));
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			SetActive(rarityAnimRoot[i], questData.rarity == array[i]);
		}
		SetGachaQuestDifficulty(UI.OBJ_DIFFICULTY_ROOT, star_num);
		ResetTween(UI.OBJ_DIFFICULTY_ROOT);
		ResetTween(rarityAnimRoot[(int)questData.rarity]);
		ResetTween(UI.OBJ_RARITY_TEXT_ROOT);
		if (questData.rarity <= RARITY_TYPE.C)
		{
			ResetTween(UI.OBJ_RARITY_LIGHT);
			PlayTween(UI.OBJ_RARITY_LIGHT, forward: true, null, is_input_block: false);
		}
		PlayTween(UI.OBJ_RARITY_TEXT_ROOT, forward: true, null, is_input_block: false);
		PlayTween(rarityAnimRoot[(int)questData.rarity], forward: true, delegate
		{
			PlayTween(UI.OBJ_DIFFICULTY_ROOT, forward: true, null, is_input_block: false);
		}, is_input_block: false);
		QuestGachaDirectorBase questGachaDirectorBase = AnimationDirector.I as QuestGachaDirectorBase;
		if (questGachaDirectorBase != null)
		{
			questGachaDirectorBase.PlayRarityAudio(questData.rarity, ignore_skip_check: true);
			questGachaDirectorBase.PlayUIRarityEffect(questData.rarity, GetCtrl(UI.OBJ_RARITY_ROOT), GetCtrl(rarityAnimRoot[(int)questData.rarity]));
		}
	}

	protected void UpdateMultiGachaUI()
	{
		List<GachaResult.GachaReward> list = (!MonoBehaviourSingleton<GachaManager>.I.enableFeverDirector) ? MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward : MonoBehaviourSingleton<GachaManager>.I.gachaResultBonus.reward;
		int index = 0;
		list.ForEach(delegate(GachaResult.GachaReward reward)
		{
			bool is_new = false;
			int num = 0;
			QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem((uint)reward.itemId);
			if (questItem != null)
			{
				is_new = GameSaveData.instance.IsNewItem(ITEM_ICON_TYPE.QUEST_ITEM, questItem.uniqueID);
				is_new = IsNewItemQuestEnemySpecies(questItem);
				num = questItem.infoData.questData.tableData.GetMainEnemyLv();
			}
			ItemIcon.CreateRewardItemIcon(REWARD_TYPE.QUEST_ITEM, (uint)reward.itemId, GetCtrl(iconRootAry[index]), -1, null, 0, is_new).SetEnableCollider(is_enable: false);
			string text = string.Empty;
			if (num > 0)
			{
				text = string.Format(StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 1u), num.ToString());
			}
			SetLabelText(GetCtrl(iconRootAry[index]), iconLevelAry[index], text);
			SetEvent(GetCtrl(iconRootAry[index]), "QUEST_DETAIL", index);
			int num2 = ++index;
		});
		for (int i = index; i < iconRootAry.Length; i++)
		{
			SetActive(iconRootAry[i], is_visible: false);
		}
		Vector3[] array = null;
		switch (list.Count)
		{
		case 2:
			array = new Vector3[2]
			{
				new Vector3(-90f, -160f),
				new Vector3(90f, -160f)
			};
			break;
		case 4:
			array = new Vector3[4]
			{
				new Vector3(-90f, -160f),
				new Vector3(90f, -160f),
				new Vector3(-90f, -296f),
				new Vector3(90f, -296f)
			};
			break;
		case 7:
			array = new Vector3[7]
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
			array = new Vector3[9]
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
				GetCtrl(iconRootAry[j]).localPosition = array[j];
			}
		}
	}

	protected void UpdateSingleResultFooterUI()
	{
		SetActive(UI.FOOTER_MULTI_RESULT_ROOT, is_visible: false);
		if (nextGachaGuarantee.IsValid())
		{
			SetActive(UI.FOOTER_ROOT, is_visible: false);
			SetActive(UI.FOOTER_GUARANTEE_ROOT, is_visible: true);
			GetCtrl(UI.BG_MULTI).GetComponent<UISprite>().height = 750;
		}
		else
		{
			SetActive(UI.FOOTER_ROOT, is_visible: true);
			SetActive(UI.FOOTER_GUARANTEE_ROOT, is_visible: false);
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
			for (int i = 0; i < array.Length; i++)
			{
				ResourceLoad.LoadItemIconTexture(array[i], itemData.iconID);
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
		if (MonoBehaviourSingleton<GachaManager>.I.IsExistNextGachaResult())
		{
			SetActive(footerRoot, UI.BTN_NEXT, is_visible: true);
			SetActive(footerRoot, UI.BTN_BACK, is_visible: false);
			SetActive(footerRoot, UI.BTN_BATTLE, is_visible: false);
			SetActive(footerRoot, UI.OBJ_GUARANTEE, is_visible: true);
			SetActive(footerRoot, UI.SPR_LINE_BOTTOM, is_visible: false);
			GetCtrl(UI.OBJ_ICONS_ROOT).localPosition = new Vector3(0f, 0f, 0f);
		}
		else
		{
			SetActive(footerRoot, UI.BTN_NEXT, is_visible: false);
			SetActive(footerRoot, UI.BTN_BACK, is_visible: true);
			SetActive(footerRoot, UI.BTN_BATTLE, is_visible: true);
			SetActive(footerRoot, UI.OBJ_GUARANTEE, is_visible: false);
			SetActive(footerRoot, UI.SPR_LINE_BOTTOM, is_visible: true);
			GetCtrl(UI.OBJ_ICONS_ROOT).localPosition = new Vector3(0f, -90f, 0f);
		}
		bool gachaButtonActive = IsEnableEntry();
		SetGachaButtonActive(gachaButtonActive);
		SetEventDetailImageButton();
	}

	protected void UpdateFeverResultFooterUI()
	{
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
			FindCtrl(footerRoot, UI.BTN_NEXT).localPosition = new Vector3(5f, -258f);
		}
		bool gachaButtonActive = IsEnableEntry();
		SetGachaButtonActive(gachaButtonActive);
		SetEventDetailImageButton();
	}

	private void SetEventDetailImageButton()
	{
		Transform transform = FindCtrl(footerRoot, UI.TEX_GUARANTEE_COUNT_DOWN);
		if (transform == null)
		{
			return;
		}
		if (isExistDetailButton)
		{
			if (!nextGachaGuarantee.IsValid() || !nextGachaGuarantee.IsItemConfirmed())
			{
				if (!nextGachaGuarantee.link.IsNullOrWhiteSpace())
				{
					transform.GetComponent<UIButton>().enabled = true;
					SetEvent(transform, "GUARANTEE_GACHA_DETAIL_WEB", nextGachaGuarantee.link);
				}
				else
				{
					transform.GetComponent<UIButton>().enabled = false;
				}
				return;
			}
			switch (nextGachaGuarantee.type)
			{
			default:
				transform.GetComponent<UIButton>().enabled = false;
				break;
			case 5:
				transform.GetComponent<UIButton>().enabled = true;
				SetEvent(transform, "SKILL_DETAIL", new object[2]
				{
					ItemDetailEquip.CURRENT_SECTION.SHOP_TOP,
					Singleton<SkillItemTable>.I.GetSkillItemData((uint)nextGachaGuarantee.itemId)
				});
				break;
			case 14:
			{
				transform.GetComponent<UIButton>().enabled = true;
				AccessorySortData accessorySortData = new AccessorySortData();
				AccessoryInfo accessoryInfo = new AccessoryInfo();
				accessoryInfo.SetValue((uint)nextGachaGuarantee.itemId);
				accessorySortData.SetItem(accessoryInfo);
				SetEvent(transform, "ACCESSORY_SELECT", new object[2]
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
			transform.GetComponent<UIButton>().enabled = false;
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
			bool num = MonoBehaviourSingleton<GachaManager>.I.selectGacha.num == 1;
			string goingHomeEvent = GameSection.GetGoingHomeEvent();
			array = ((!num) ? new EventData[3]
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
		for (int childCount = ctrl.childCount; i < childCount; i++)
		{
			ctrl.GetChild(i).gameObject.SetActive(i <= star_num);
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
			SetActive(rarityAnimRoot[i], rarity == array[i]);
		}
		SetGachaQuestDifficulty(UI.OBJ_DIFFICULTY_ROOT, star_num);
		ResetTween(UI.OBJ_DIFFICULTY_ROOT);
		ResetTween(rarityAnimRoot[(int)rarity]);
		ResetTween(UI.OBJ_RARITY_TEXT_ROOT);
		if (rarity <= RARITY_TYPE.C)
		{
			ResetTween(UI.OBJ_RARITY_LIGHT);
		}
	}

	private void TEST_PLAY(RARITY_TYPE rarity, DIFFICULTY_TYPE difficulty)
	{
		if (rarity <= RARITY_TYPE.C)
		{
			PlayTween(UI.OBJ_RARITY_LIGHT, forward: true, null, is_input_block: false);
		}
		PlayTween(UI.OBJ_RARITY_TEXT_ROOT, forward: true, null, is_input_block: false);
		PlayTween(rarityAnimRoot[(int)rarity], forward: true, delegate
		{
			PlayTween(UI.OBJ_DIFFICULTY_ROOT, forward: true, null, is_input_block: false);
		}, is_input_block: false);
		if (AnimationDirector.I is QuestGachaDirectorBase)
		{
			(AnimationDirector.I as QuestGachaDirectorBase).PlayUIRarityEffect(rarity, GetCtrl(UI.OBJ_RARITY_ROOT), GetCtrl(rarityAnimRoot[(int)rarity]));
		}
	}
}
