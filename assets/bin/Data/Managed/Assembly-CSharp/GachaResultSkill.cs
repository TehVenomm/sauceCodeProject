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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		nextGuachaGuarantee = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().gachaGuaranteeCampaignInfo;
		MonoBehaviourSingleton<GachaManager>.I.SetSelectGachaGuarantee(nextGuachaGuarantee);
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

	private IEnumerator LoadNormalUI(LoadingQueue loadQueue)
	{
		SetActive((Enum)UI.FOOTER_MULTI_RESULT_ROOT, false);
		if (nextGuachaGuarantee.IsValid())
		{
			footerRoot = GetCtrl(UI.FOOTER_GUARANTEE_ROOT);
			SetActive((Enum)UI.FOOTER_ROOT, false);
			SetActive((Enum)UI.FOOTER_GUARANTEE_ROOT, true);
		}
		else
		{
			footerRoot = GetCtrl(UI.FOOTER_ROOT);
			SetActive((Enum)UI.FOOTER_ROOT, true);
			SetActive((Enum)UI.FOOTER_GUARANTEE_ROOT, false);
		}
		yield return (object)LoadGachaButton(buttonName: CreateButtonName(), loadQueue: loadQueue, parent: FindCtrl(footerRoot, UI.BTN_GACHA));
		yield return (object)LoadGachaGuaranteeCounter(loadQueue, nextGuachaGuarantee, delegate(LoadObject lo_guarantee)
		{
			((_003CLoadNormalUI_003Ec__Iterator5B)/*Error near IL_0147: stateMachine*/)._003C_003Ef__this.SetTexture(((_003CLoadNormalUI_003Ec__Iterator5B)/*Error near IL_0147: stateMachine*/)._003C_003Ef__this.footerRoot, UI.TEX_GUARANTEE_COUNT_DOWN, lo_guarantee.loadedObject as Texture);
		});
	}

	public override void UpdateUI()
	{
		bool flag = MonoBehaviourSingleton<GachaManager>.I.selectGacha.num == 1;
		SetActive((Enum)UI.OBJ_SINGLE_ROOT, flag);
		SetActive((Enum)UI.OBJ_MULTI_ROOT, !flag);
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
			SetActive((Enum)UI.OBJ_SINGLE_ROOT, false);
		}
		SetLabelText((Enum)UI.LBL_NAME, skillItemData.name);
		SetLabelText((Enum)UI.LBL_ATK, skillItemData.baseAtk.ToString());
		SetLabelText((Enum)UI.LBL_DEF, skillItemData.baseDef.ToString());
		SetLabelText((Enum)UI.LBL_HP, skillItemData.baseHp.ToString());
		SetLabelText((Enum)UI.LBL_DESCRIPTION, skillItemData.GetExplanationText(1));
		SetRenderSkillItemModel((Enum)UI.TEX_MODEL, skillItemData.id, true, false);
		SetRenderSkillItemSymbolModel((Enum)UI.TEX_INNER_MODEL, skillItemData.id, true);
		RARITY_TYPE[] array = (RARITY_TYPE[])Enum.GetValues(typeof(RARITY_TYPE));
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			SetActive((Enum)rarityAnimRoot[i], skillItemData.rarity == array[i]);
		}
		ResetTween((Enum)rarityAnimRoot[(int)skillItemData.rarity], 0);
		ResetTween((Enum)UI.OBJ_RARITY_TEXT_ROOT, 0);
		if (skillItemData.rarity <= RARITY_TYPE.C)
		{
			ResetTween((Enum)UI.OBJ_RARITY_LIGHT, 0);
			PlayTween((Enum)UI.OBJ_RARITY_LIGHT, true, (EventDelegate.Callback)null, false, 0);
		}
		PlayTween((Enum)rarityAnimRoot[(int)skillItemData.rarity], true, (EventDelegate.Callback)null, false, 0);
		PlayTween((Enum)UI.OBJ_RARITY_TEXT_ROOT, true, (EventDelegate.Callback)null, false, 0);
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
			bool flag = false;
			Transform ctrl = GetCtrl(iconRootAry[index]);
			SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)reward.itemId);
			if (skillItemData == null)
			{
				SetActive(ctrl, false);
			}
			else
			{
				SetActive(ctrl, true);
				bool is_new = flag;
				ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.SKILL_ITEM, (uint)reward.itemId, ctrl, -1, null, 0, is_new, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
				itemIcon.SetEnableCollider(false);
				Transform ctrl2 = GetCtrl(magiNameAry[index]);
				SetLabelText(ctrl2, skillItemData.name);
				SetEvent(GetCtrl(iconRootAry[index]), "SKILL_DETAIL", index);
				index++;
			}
		});
	}

	public void UpdateSingleResultFooterUI()
	{
		if (nextGuachaGuarantee.IsValid())
		{
			SetActive((Enum)UI.FOOTER_ROOT, false);
			SetActive((Enum)UI.FOOTER_GUARANTEE_ROOT, true);
		}
		else
		{
			SetActive((Enum)UI.FOOTER_ROOT, true);
			SetActive((Enum)UI.FOOTER_GUARANTEE_ROOT, false);
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
			UITexture[] array2 = array;
			foreach (UITexture ui_tex in array2)
			{
				ResourceLoad.LoadItemIconTexture(ui_tex, itemData.iconID);
			}
			num = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == itemData.id, 1, false);
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
			SetActive(footerRoot, UI.BTN_EQUIP, false);
			SetActive(footerRoot, UI.OBJ_GUARANTEE, true);
			SetActive(footerRoot, UI.SPR_LINE_BOTTOM, false);
			GetCtrl(UI.OBJ_ICONS_ROOT).set_localPosition(new Vector3(0f, 0f, 0f));
		}
		else
		{
			SetActive(footerRoot, UI.BTN_NEXT, false);
			SetActive(footerRoot, UI.BTN_BACK, true);
			SetActive(footerRoot, UI.BTN_EQUIP, true);
			SetActive(footerRoot, UI.OBJ_GUARANTEE, false);
			SetActive(footerRoot, UI.SPR_LINE_BOTTOM, true);
			GetCtrl(UI.OBJ_ICONS_ROOT).set_localPosition(new Vector3(0f, -50f, 0f));
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
					SetEvent(val, "GUARANTEE_SKILL_DETAIL", null);
				}
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
		}
		else
		{
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
	}

	private void OnQuery_GUARANTEE_SKILL_DETAIL()
	{
		uint itemId = (uint)nextGuachaGuarantee.itemId;
		SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(itemId);
		if (skillItemData == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(new object[2]
			{
				ItemDetailEquip.CURRENT_SECTION.SHOP_TOP,
				Singleton<SkillItemTable>.I.GetSkillItemData((uint)nextGuachaGuarantee.itemId)
			});
		}
	}

	protected override void OnDestroy()
	{
		_OnDestroy();
		if (!AppMain.isApplicationQuit && !isRetry && AnimationDirector.I != null)
		{
			AnimationDirector.I.Reset();
			AnimationDirector.I.SetLinkCamera(false);
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
				SetLabelText((Enum)UI.LBL_CRYSTAL_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal.ToString());
			}
		}
		base.OnNotify(flags);
	}
}
