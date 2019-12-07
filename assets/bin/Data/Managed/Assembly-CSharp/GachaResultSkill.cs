using Network;
using System;
using System.Collections;
using UnityEngine;

public class GachaResultSkill : GachaResultBase
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

	private UI[] magiNameAry = new UI[11]
	{
		UI.LBL_MAGI_NAME_0,
		UI.LBL_MAGI_NAME_1,
		UI.LBL_MAGI_NAME_2,
		UI.LBL_MAGI_NAME_3,
		UI.LBL_MAGI_NAME_4,
		UI.LBL_MAGI_NAME_5,
		UI.LBL_MAGI_NAME_6,
		UI.LBL_MAGI_NAME_7,
		UI.LBL_MAGI_NAME_8,
		UI.LBL_MAGI_NAME_9,
		UI.LBL_MAGI_NAME_10
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

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		currentGachaGuarantee = MonoBehaviourSingleton<GachaManager>.I.selectGachaGuarantee;
		nextGachaGuarantee = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().gachaGuaranteeCampaignInfo;
		MonoBehaviourSingleton<GachaManager>.I.SetSelectGachaGuarantee(nextGachaGuarantee);
		if (MonoBehaviourSingleton<GachaManager>.I.IsMultiResult())
		{
			yield return LoadMultiResultUI(loadQueue);
		}
		else
		{
			yield return LoadNormalUI(loadQueue);
		}
		base.Initialize();
	}

	private IEnumerator LoadNormalUI(LoadingQueue loadQueue)
	{
		SetActive(UI.FOOTER_MULTI_RESULT_ROOT, is_visible: false);
		if (nextGachaGuarantee.IsValid())
		{
			footerRoot = GetCtrl(UI.FOOTER_GUARANTEE_ROOT);
			SetActive(UI.FOOTER_ROOT, is_visible: false);
			SetActive(UI.FOOTER_GUARANTEE_ROOT, is_visible: true);
		}
		else
		{
			footerRoot = GetCtrl(UI.FOOTER_ROOT);
			SetActive(UI.FOOTER_ROOT, is_visible: true);
			SetActive(UI.FOOTER_GUARANTEE_ROOT, is_visible: false);
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
		GachaResult.GachaReward gachaReward = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward[0];
		SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)gachaReward.itemId);
		if (skillItemData == null)
		{
			SetActive(UI.OBJ_SINGLE_ROOT, is_visible: false);
		}
		SetLabelText(UI.LBL_NAME, skillItemData.name);
		SetLabelText(UI.LBL_ATK, skillItemData.baseAtk.ToString());
		SetLabelText(UI.LBL_DEF, skillItemData.baseDef.ToString());
		SetLabelText(UI.LBL_HP, skillItemData.baseHp.ToString());
		SetLabelText(UI.LBL_DESCRIPTION, skillItemData.GetExplanationText());
		SetRenderSkillItemModel(UI.TEX_MODEL, skillItemData.id);
		SetRenderSkillItemSymbolModel(UI.TEX_INNER_MODEL, skillItemData.id);
		RARITY_TYPE[] array = (RARITY_TYPE[])Enum.GetValues(typeof(RARITY_TYPE));
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			SetActive(rarityAnimRoot[i], skillItemData.rarity == array[i]);
		}
		ResetTween(rarityAnimRoot[(int)skillItemData.rarity]);
		ResetTween(UI.OBJ_RARITY_TEXT_ROOT);
		if (skillItemData.rarity <= RARITY_TYPE.C)
		{
			ResetTween(UI.OBJ_RARITY_LIGHT);
			PlayTween(UI.OBJ_RARITY_LIGHT, forward: true, null, is_input_block: false);
		}
		PlayTween(rarityAnimRoot[(int)skillItemData.rarity], forward: true, null, is_input_block: false);
		PlayTween(UI.OBJ_RARITY_TEXT_ROOT, forward: true, null, is_input_block: false);
		if (AnimationDirector.I is SkillGachaDirector)
		{
			(AnimationDirector.I as SkillGachaDirector).PlayUIRarityEffect(skillItemData.rarity, GetCtrl(UI.OBJ_RARITY_ROOT), GetCtrl(rarityAnimRoot[(int)skillItemData.rarity]));
		}
	}

	protected void UpdateMultiGachaUI()
	{
		int index = 0;
		MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward.ForEach(delegate(GachaResult.GachaReward reward)
		{
			bool is_new = false;
			Transform ctrl = GetCtrl(iconRootAry[index]);
			SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)reward.itemId);
			if (skillItemData == null)
			{
				SetActive(ctrl, is_visible: false);
			}
			else
			{
				SetActive(ctrl, is_visible: true);
				ItemIcon.CreateRewardItemIcon(REWARD_TYPE.SKILL_ITEM, (uint)reward.itemId, ctrl, -1, null, 0, is_new).SetEnableCollider(is_enable: false);
				Transform ctrl2 = GetCtrl(magiNameAry[index]);
				SetLabelText(ctrl2, skillItemData.name);
				SetEvent(GetCtrl(iconRootAry[index]), "SKILL_DETAIL", index);
				int num = ++index;
			}
		});
	}

	public void UpdateSingleResultFooterUI()
	{
		if (nextGachaGuarantee.IsValid())
		{
			SetActive(UI.FOOTER_ROOT, is_visible: false);
			SetActive(UI.FOOTER_GUARANTEE_ROOT, is_visible: true);
		}
		else
		{
			SetActive(UI.FOOTER_ROOT, is_visible: true);
			SetActive(UI.FOOTER_GUARANTEE_ROOT, is_visible: false);
		}
		int num = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0)
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId);
			UITexture[] array = new UITexture[3]
			{
				FindCtrl(GetCtrl(UI.OBJ_GACHA_DISABLE_ROOT), UI.TEX_TICKET).GetComponent<UITexture>(),
				FindCtrl(GetCtrl(UI.OBJ_GACHA_ENABLE_ROOT), UI.TEX_TICKET).GetComponent<UITexture>(),
				GetCtrl(UI.TEX_TICKET_HAVE).GetComponent<UITexture>()
			};
			for (int i = 0; i < array.Length; i++)
			{
				ResourceLoad.LoadItemIconTexture(array[i], itemData.iconID);
			}
			num = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == itemData.id, 1);
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
			SetActive(footerRoot, UI.BTN_EQUIP, is_visible: false);
			SetActive(footerRoot, UI.OBJ_GUARANTEE, is_visible: true);
			SetActive(footerRoot, UI.SPR_LINE_BOTTOM, is_visible: false);
			GetCtrl(UI.OBJ_ICONS_ROOT).localPosition = new Vector3(0f, 0f, 0f);
		}
		else
		{
			SetActive(footerRoot, UI.BTN_NEXT, is_visible: false);
			SetActive(footerRoot, UI.BTN_BACK, is_visible: true);
			SetActive(footerRoot, UI.BTN_EQUIP, is_visible: true);
			SetActive(footerRoot, UI.OBJ_GUARANTEE, is_visible: false);
			SetActive(footerRoot, UI.SPR_LINE_BOTTOM, is_visible: true);
			GetCtrl(UI.OBJ_ICONS_ROOT).localPosition = new Vector3(0f, -50f, 0f);
		}
		bool gachaButtonActive = IsEnableEntry();
		SetGachaButtonActive(gachaButtonActive);
		SetEventDetailImageButton();
	}

	private void SetEventDetailImageButton()
	{
		if (!isExistDetailButton)
		{
			return;
		}
		Transform transform = FindCtrl(footerRoot, UI.TEX_GUARANTEE_COUNT_DOWN);
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
			SetEvent(transform, "GUARANTEE_SKILL_DETAIL", null);
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

	private void OnQuery_SECTION_BACK()
	{
		if (AnimationDirector.I != null)
		{
			AnimationDirector.I.Reset();
		}
	}

	private void OnQuery_EQUIP()
	{
		EventData[] autoEvents = new EventData[2]
		{
			new EventData("MAIN_MENU_STUDIO", null),
			new EventData("SKILL_LIST", null)
		};
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	private void OnQuery_SKILL_DETAIL()
	{
		int num = (int)GameSection.GetEventData();
		int count = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward.Count;
		if (num < 0 || num >= count)
		{
			GameSection.StopEvent();
			return;
		}
		uint itemId = (uint)MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().reward[num].itemId;
		SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(itemId);
		if (skillItemData == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(new object[2]
			{
				ItemDetailEquip.CURRENT_SECTION.GACHA_RESULT,
				skillItemData
			});
		}
	}

	private void OnQuery_GUARANTEE_SKILL_DETAIL()
	{
		uint itemId = (uint)nextGachaGuarantee.itemId;
		if (Singleton<SkillItemTable>.I.GetSkillItemData(itemId) == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(new object[2]
			{
				ItemDetailEquip.CURRENT_SECTION.SHOP_TOP,
				Singleton<SkillItemTable>.I.GetSkillItemData((uint)nextGachaGuarantee.itemId)
			});
		}
	}

	protected override void OnDestroy()
	{
		_OnDestroy();
		if (!AppMain.isApplicationQuit && !isRetry && AnimationDirector.I != null)
		{
			AnimationDirector.I.Reset();
			AnimationDirector.I.SetLinkCamera(is_link: false);
		}
	}

	protected void _OnDestroy()
	{
		base.OnDestroy();
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_USER_STATUS) != (NOTIFY_FLAG)0L)
		{
			CheckUpdateCrystalNum();
			if (!isRetry)
			{
				SetLabelText(UI.LBL_CRYSTAL_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal.ToString());
			}
		}
		base.OnNotify(flags);
	}
}
