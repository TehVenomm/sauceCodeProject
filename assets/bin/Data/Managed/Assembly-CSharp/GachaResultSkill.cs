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
		LBL_PRICE,
		TEX_TICKET,
		TEX_TICKET_HAVE,
		SPR_CRYSTAL,
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
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_DESCRIPTION,
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
		FOOTER_ROOT,
		FOOTER_GUARANTEE_ROOT
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
		nextGuachaGuarantee = MonoBehaviourSingleton<GachaManager>.I.gachaResult.gachaGuaranteeCampaignInfo;
		MonoBehaviourSingleton<GachaManager>.I.SetSelectGachaGuarantee(nextGuachaGuarantee);
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
		string buttonName = CreateButtonName();
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
				((_003CDoInitialize_003Ec__Iterator46)/*Error near IL_0249: stateMachine*/)._003CisGuaranteeLoaded_003E__3 = true;
				((_003CDoInitialize_003Ec__Iterator46)/*Error near IL_0249: stateMachine*/)._003C_003Ef__this.SetTexture(((_003CDoInitialize_003Ec__Iterator46)/*Error near IL_0249: stateMachine*/)._003C_003Ef__this.footerRoot, UI.TEX_GUARANTEE_COUNT_DOWN, lo_guarantee.loadedObject as Texture);
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
					SetEvent(guaranteeCountDown, "SKILL_DETAIL", null);
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
		if (flag)
		{
			GachaResult.GachaReward gachaReward = MonoBehaviourSingleton<GachaManager>.I.gachaResult.reward[0];
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
		else
		{
			int index = 0;
			MonoBehaviourSingleton<GachaManager>.I.gachaResult.reward.ForEach(delegate(GachaResult.GachaReward reward)
			{
				bool flag2 = false;
				Transform ctrl = GetCtrl(iconRootAry[index]);
				SkillItemTable.SkillItemData skillItemData2 = Singleton<SkillItemTable>.I.GetSkillItemData((uint)reward.itemId);
				if (skillItemData2 == null)
				{
					SetActive(ctrl, false);
				}
				else
				{
					SetActive(ctrl, true);
					bool is_new = flag2;
					ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.SKILL_ITEM, (uint)reward.itemId, ctrl, -1, null, 0, is_new, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
					itemIcon.SetEnableCollider(false);
					Transform ctrl2 = GetCtrl(magiNameAry[index]);
					SetLabelText(ctrl2, skillItemData2.name);
					SetEvent(GetCtrl(iconRootAry[index]), "SKILL_DETAIL", index);
					index++;
				}
			});
		}
		int num2 = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0)
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId);
			UITexture[] array2 = new UITexture[3]
			{
				FindCtrl(GetCtrl(UI.OBJ_GACHA_DISABLE_ROOT), UI.TEX_TICKET).GetComponent<UITexture>(),
				FindCtrl(GetCtrl(UI.OBJ_GACHA_ENABLE_ROOT), UI.TEX_TICKET).GetComponent<UITexture>(),
				GetCtrl(UI.TEX_TICKET_HAVE).GetComponent<UITexture>()
			};
			UITexture[] array3 = array2;
			foreach (UITexture ui_tex in array3)
			{
				ResourceLoad.LoadItemIconTexture(ui_tex, itemData.iconID);
			}
			num2 = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == itemData.id, 1, false);
		}
		SetActive(footerRoot, UI.SPR_CRYSTAL, MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId == 0);
		SetActive(footerRoot, UI.TEX_TICKET_HAVE, MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0);
		SetLabelText(footerRoot, UI.LBL_CRYSTAL_NUM, num2.ToString());
		SetGachaButtonActive(!MonoBehaviourSingleton<GachaManager>.I.IsSelectTutorialGacha() && MonoBehaviourSingleton<GachaManager>.I.gachaResult.remainCount != 0);
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
		uint itemId;
		if (GameSection.GetEventData() is int)
		{
			int num = (int)GameSection.GetEventData();
			int count = MonoBehaviourSingleton<GachaManager>.I.gachaResult.reward.Count;
			if (num < 0 || num >= count)
			{
				GameSection.StopEvent();
				return;
			}
			itemId = (uint)MonoBehaviourSingleton<GachaManager>.I.gachaResult.reward[num].itemId;
		}
		else
		{
			itemId = (uint)nextGuachaGuarantee.itemId;
		}
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
}
