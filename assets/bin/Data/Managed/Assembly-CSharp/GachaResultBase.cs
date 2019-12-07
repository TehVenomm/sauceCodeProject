using Network;
using System;
using System.Collections;
using UnityEngine;

public abstract class GachaResultBase : GameSection
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
		BTN_EQUIP
	}

	protected bool isRetry;

	protected int userCrystalNum;

	protected GameObject buttonObj;

	protected Transform footerRoot;

	protected GachaGuaranteeCampaignInfo currentGachaGuarantee;

	protected GachaGuaranteeCampaignInfo nextGachaGuarantee;

	protected bool isExistDetailButton;

	public override void Initialize()
	{
		if (nextGachaGuarantee != null && nextGachaGuarantee.IsValid())
		{
			MonoBehaviourSingleton<GachaManager>.I.selectGacha.SetCrystalNum(nextGachaGuarantee.crystalNum);
		}
		userCrystalNum = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		base.Initialize();
	}

	protected virtual IEnumerator LoadMultiResultUI(LoadingQueue loadQueue)
	{
		SetActive(UI.FOOTER_ROOT, is_visible: false);
		SetActive(UI.FOOTER_GUARANTEE_ROOT, is_visible: false);
		SetActive(UI.FOOTER_MULTI_RESULT_ROOT, is_visible: true);
		footerRoot = GetCtrl(UI.FOOTER_MULTI_RESULT_ROOT);
		string buttonName = CreateButtonName();
		yield return LoadGachaButton(loadQueue, FindCtrl(footerRoot, UI.BTN_NEXT), buttonName);
		SetEvent(FindCtrl(footerRoot, UI.BTN_NEXT).GetChild(0), "NEXT_PERFORMANCE", -1);
		yield return LoadGachaGuaranteeCounter(loadQueue, nextGachaGuarantee, delegate(LoadObject lo_guarantee)
		{
			SetTexture(footerRoot, UI.TEX_GUARANTEE_COUNT_DOWN, lo_guarantee.loadedObject as Texture);
		});
	}

	protected string CreateButtonName()
	{
		return MonoBehaviourSingleton<GachaManager>.I.CreateButtonBaseName(MonoBehaviourSingleton<GachaManager>.I.selectGacha, nextGachaGuarantee, resultScene: true) + "_RESULT";
	}

	protected IEnumerator LoadGachaButton(LoadingQueue loadQueue, Transform parent, string buttonName)
	{
		LoadObject lo_button = loadQueue.Load(RESOURCE_CATEGORY.GACHA_BUTTON, buttonName);
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		buttonObj = (UnityEngine.Object.Instantiate(lo_button.loadedObject) as GameObject);
		buttonObj.transform.parent = parent;
		buttonObj.transform.name = parent.name;
		buttonObj.transform.localScale = new Vector3(1f, 1f, 1f);
		buttonObj.transform.localPosition = new Vector3(0f, 0f, 0f);
	}

	protected IEnumerator LoadGachaGuaranteeCounter(LoadingQueue loadQueue, GachaGuaranteeCampaignInfo guarantee, Action<LoadObject> callback)
	{
		string text = "";
		if (guarantee.IsValid())
		{
			text = guarantee.GetTitleImageName();
		}
		GachaResult currentGachaResult = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult();
		if (currentGachaResult.detailButtonImg == null)
		{
			currentGachaResult.detailButtonImg = "";
		}
		if (currentGachaResult.detailButtonImg != "")
		{
			text = currentGachaResult.detailButtonImg;
		}
		if (text != "")
		{
			isExistDetailButton = true;
			LoadObject loadObject = loadQueue.Load(RESOURCE_CATEGORY.GACHA_GUARANTEE_COUNTER, text);
			if (loadQueue.IsLoading())
			{
				yield return loadQueue.Wait();
			}
			callback(loadObject);
		}
	}

	protected override void OnQuery_GachaConfirm_YES()
	{
		isRetry = true;
		base.OnQuery_GachaConfirm_YES();
	}

	protected void CheckUpdateCrystalNum()
	{
		int crystal = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		if (userCrystalNum < crystal)
		{
			isRetry = false;
		}
		userCrystalNum = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
	}

	protected void OnQuery_GACHA()
	{
		if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.IsEnd)
		{
			GameSection.ChangeEvent("END");
			SetGachaButtonActive(enableRetry: false);
			return;
		}
		string text = "";
		if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0)
		{
			int ticketId = MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId;
			int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == ticketId, 1);
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)ticketId);
			text = itemData.name + " " + MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum + StringTable.Get(STRING_CATEGORY.COMMON, 4000u) + "\n";
			if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum > itemNum)
			{
				object[] event_data = new object[2]
				{
					itemData.name,
					(MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum - itemNum).ToString() + StringTable.Get(STRING_CATEGORY.COMMON, 4000u)
				};
				GameSection.ChangeEvent("NOT_ENOUGH_GACHA_TICKET", event_data);
				return;
			}
		}
		else
		{
			text = StringTable.Get(STRING_CATEGORY.COMMON, 100u) + " " + GetCrystalNum() + StringTable.Get(STRING_CATEGORY.COMMON, 3000u);
		}
		GameSection.SetEventData(new object[1]
		{
			text
		});
	}

	public void OnQuery_NEXT_PERFORMANCE()
	{
		if (MonoBehaviourSingleton<GachaManager>.I.IsExistNextGachaResult())
		{
			if (MonoBehaviourSingleton<GachaManager>.I.GetNextGachaResult().reward[0].rewardType == 6)
			{
				GameSection.ChangeEvent("NEXT_PERFORMANCE_QUEST");
			}
			else
			{
				GameSection.ChangeEvent("NEXT_PERFORMANCE_SKILL");
			}
			MonoBehaviourSingleton<GachaManager>.I.IncrementGachaIndex();
		}
	}

	public void OnQuery_FEVER_PERFORMANCE()
	{
		if (MonoBehaviourSingleton<GachaManager>.I.IsResultBonus())
		{
			if (MonoBehaviourSingleton<GachaManager>.I.gachaResultBonus.reward[0].rewardType == 6)
			{
				GameSection.ChangeEvent("NEXT_PERFORMANCE_QUEST");
			}
			else
			{
				GameSection.ChangeEvent("NEXT_PERFORMANCE_SKILL");
			}
			MonoBehaviourSingleton<GachaManager>.I.SetNextFever();
		}
	}

	public int GetCrystalNum()
	{
		if (nextGachaGuarantee == null || !nextGachaGuarantee.IsValid())
		{
			return MonoBehaviourSingleton<GachaManager>.I.selectGacha.crystalNum;
		}
		if (!nextGachaGuarantee.hasFreeGachaReward)
		{
			return MonoBehaviourSingleton<GachaManager>.I.selectGacha.crystalNum;
		}
		return 0;
	}

	protected bool IsEnableEntry()
	{
		if (MonoBehaviourSingleton<GachaManager>.I.IsExistNextGachaResult())
		{
			return true;
		}
		if (MonoBehaviourSingleton<GachaManager>.I.IsSelectTutorialGacha())
		{
			return false;
		}
		if (MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().remainCount == 0)
		{
			return false;
		}
		return true;
	}

	protected void SetGachaButtonActive(bool enableRetry)
	{
		Transform root = FindCtrl(buttonObj.transform, enableRetry ? UI.OBJ_GACHA_ENABLE_ROOT : UI.OBJ_GACHA_DISABLE_ROOT);
		SetActive(buttonObj.transform, UI.OBJ_GACHA_ENABLE_ROOT, enableRetry);
		SetActive(buttonObj.transform, UI.OBJ_GACHA_DISABLE_ROOT, !enableRetry);
		SetLabelText(text: ((MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0) ? MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum : GetCrystalNum()).ToString(), root: root, label_enum: UI.LBL_PRICE);
		if (!enableRetry)
		{
			SetButtonEnabled(buttonObj.transform, is_enabled: false);
		}
	}
}
