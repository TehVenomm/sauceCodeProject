using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDetailEquip : SkillInfoBase
{
	protected enum UI
	{
		OBJ_DETAIL_ROOT,
		TEX_MODEL,
		OBJ_FRAME_BG,
		STR_LV,
		LBL_NAME,
		LBL_LV_NOW,
		LBL_LV_MAX,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_ELEM,
		LBL_ELEM_DEF,
		SPR_ELEM,
		SPR_ELEM_DEF,
		LBL_SELL,
		OBJ_SKILL_BUTTON_ROOT,
		SPR_IS_EVOLVE,
		OBJ_FAVORITE_ROOT,
		TWN_FAVORITE,
		TWN_UNFAVORITE,
		OBJ_ATK_ROOT,
		OBJ_DEF_ROOT,
		OBJ_ELEM_ROOT,
		STR_ONLY_VISUAL,
		SPR_TYPE_ICON,
		SPR_TYPE_ICON_BG,
		SPR_TYPE_ICON_RARITY,
		STR_TITLE_ITEM_INFO,
		STR_TITLE_STATUS,
		STR_TITLE_SKILL_SLOT,
		STR_TITLE_ABILITY,
		STR_TITLE_SELL,
		STR_TITLE_ATK,
		STR_TITLE_DEF,
		STR_TITLE_HP,
		STR_TITLE_ELEM_ATK,
		STR_TITLE_ELEM_DEF,
		TBL_ABILITY,
		STR_NON_ABILITY,
		OBJ_ABILITY,
		LBL_ABILITY,
		LBL_ABILITY_NUM,
		OBJ_FIXEDABILITY,
		LBL_FIXEDABILITY,
		LBL_FIXEDABILITY_NUM,
		OBJ_ABILITY_ITEM,
		LBL_ABILITY_ITEM,
		BTN_SELL,
		BTN_EXCEED,
		SPR_COUNT_0_ON,
		SPR_COUNT_1_ON,
		SPR_COUNT_2_ON,
		SPR_COUNT_3_ON,
		BTN_CHANGE,
		BTN_CREATE,
		BTN_GROW,
		BTN_ABILITY,
		BTN_GROW_OFF,
		BTN_ABILITY_OFF,
		STR_BTN_CHANGE,
		STR_BTN_CREATE,
		STR_BTN_GROW,
		STR_BTN_ABILITY,
		STR_BTN_CHANGE_D,
		STR_BTN_CREATE_D,
		STR_BTN_GROW_D,
		STR_BTN_ABILITY_D,
		OBJ_NEED_UPDATE_ABILITY,
		LBL_NEED_UPDATE_ABILITY,
		BTN_GRAPH,
		SPR_SP_ATTACK_TYPE,
		OBJ_EVOLVE_SELECT,
		LBL_EVOLVE_NORMAL,
		SPR_EVOLVE_ELEM,
		LBL_EVOLVE_ATTRIBUTE,
		OBJ_ARROW_BTN_ROOT
	}

	public class DetailEquipEventData
	{
		public object[] currentEventData
		{
			get;
			protected set;
		}

		public StatusEquip.LocalEquipSetData localEquipSetData
		{
			get;
			protected set;
		}

		public DetailEquipEventData(object[] currentEvnet, StatusEquip.LocalEquipSetData localEquip)
		{
			currentEventData = currentEvnet;
			localEquipSetData = localEquip;
		}
	}

	[Flags]
	public enum CURRENT_SECTION
	{
		NONE = 0x0,
		STATUS_TOP = 0x1,
		STATUS_SKILL_LIST = 0x2,
		STATUS_EQUIP = 0x4,
		STATUS_EQUIP_SKILL = 0x8,
		STATUS_AVATAR = 0x10,
		ITEM_STORAGE = 0x20,
		SMITH_CREATE = 0x40,
		SMITH_EVOLVE = 0x80,
		SMITH_GROW = 0x100,
		SMITH_SKILL_GROW = 0x200,
		SMITH_SKILL_MATERIAL = 0x400,
		QUEST_ROOM = 0x800,
		QUEST_RESULT = 0x1000,
		UI_PARTS = 0x2000,
		GACHA_RESULT = 0x4000,
		EQUIP_LIST = 0x8000,
		EQUIP_SET_DETAIL_STATUS = 0x10000,
		GACHA_EQUIP_PREVIEW = 0x20000,
		SHOP_TOP = 0x40000,
		SMITH_SELL = 0x80000
	}

	private AbilityDetailPopUp abilityDetailPopUp;

	private List<Transform> touchAndReleaseButtons = new List<Transform>();

	protected Transform detailBase;

	private object eventData;

	protected CURRENT_SECTION callSection;

	protected object detailItemData;

	private SkillSlotUIData[] equipAttachSkill;

	protected int sex = -1;

	protected int faceID = -1;

	protected StatusEquip.LocalEquipSetData localEquipSetData;

	protected object[] gameEventData;

	protected virtual bool IsShowFrameBG()
	{
		return false;
	}

	public override void Initialize()
	{
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		gameEventData = (GameSection.GetEventData() as object[]);
		callSection = (CURRENT_SECTION)(int)gameEventData[0];
		eventData = gameEventData[1];
		localEquipSetData = gameEventData.OfType<StatusEquip.LocalEquipSetData>().FirstOrDefault();
		switch (callSection)
		{
		case CURRENT_SECTION.STATUS_TOP:
		case CURRENT_SECTION.STATUS_EQUIP:
		case CURRENT_SECTION.STATUS_AVATAR:
		case CURRENT_SECTION.EQUIP_SET_DETAIL_STATUS:
		{
			EquipItemInfo equipItemInfo3 = eventData as EquipItemInfo;
			if (equipItemInfo3 != null)
			{
				detailItemData = equipItemInfo3;
				equipAttachSkill = GetSkillSlotData(detailItemData as EquipItemInfo);
			}
			break;
		}
		case CURRENT_SECTION.ITEM_STORAGE:
		case CURRENT_SECTION.SMITH_SELL:
		{
			SortCompareData sortCompareData = eventData as SortCompareData;
			if (sortCompareData != null)
			{
				EquipItemInfo equipItemInfo4 = (EquipItemInfo)(detailItemData = (sortCompareData.GetItemData() as EquipItemInfo));
				equipAttachSkill = GetSkillSlotData(equipItemInfo4);
				MonoBehaviourSingleton<StatusManager>.I.SetSelectEquipItem(equipItemInfo4);
			}
			break;
		}
		case CURRENT_SECTION.QUEST_ROOM:
		{
			EquipItemInfo equipItemInfo = eventData as EquipItemInfo;
			if (equipItemInfo != null)
			{
				detailItemData = equipItemInfo;
				equipAttachSkill = GetSkillSlotData(detailItemData as EquipItemInfo);
			}
			if (gameEventData.Length > 2)
			{
				sex = (int)gameEventData[2];
				faceID = (int)gameEventData[3];
			}
			break;
		}
		case CURRENT_SECTION.QUEST_RESULT:
		{
			EquipItemAndSkillData equipItemAndSkillData = eventData as EquipItemAndSkillData;
			if (equipItemAndSkillData != null)
			{
				detailItemData = equipItemAndSkillData.equipItemInfo;
				equipAttachSkill = equipItemAndSkillData.skillSlotUIData;
			}
			if (gameEventData.Length > 2)
			{
				sex = (int)gameEventData[2];
				faceID = (int)gameEventData[3];
			}
			break;
		}
		case CURRENT_SECTION.SMITH_EVOLVE:
		case CURRENT_SECTION.SMITH_GROW:
		{
			EquipItemInfo equipItemInfo2 = eventData as EquipItemInfo;
			if (equipItemInfo2 != null)
			{
				detailItemData = equipItemInfo2;
				equipAttachSkill = GetSkillSlotData(detailItemData as EquipItemInfo);
			}
			break;
		}
		case CURRENT_SECTION.SMITH_CREATE:
		case CURRENT_SECTION.GACHA_EQUIP_PREVIEW:
			detailItemData = eventData;
			equipAttachSkill = GetSkillSlotData(detailItemData as EquipItemTable.EquipItemData, 0);
			break;
		case CURRENT_SECTION.EQUIP_LIST:
			detailItemData = eventData;
			equipAttachSkill = GetSkillSlotData(detailItemData as EquipItemTable.EquipItemData, 0);
			for (int i = 0; i < equipAttachSkill.Length; i++)
			{
				equipAttachSkill[i].slotData.skill_id = 0u;
			}
			break;
		}
		if (detailItemData != null)
		{
			EquipItemInfo equipItemInfo5 = detailItemData as EquipItemInfo;
			if (equipItemInfo5 != null)
			{
				GameSaveData.instance.RemoveNewIconAndSave(ItemIcon.GetItemIconType(equipItemInfo5.tableData.type), equipItemInfo5.uniqueID);
			}
		}
		if (sex == -1)
		{
			sex = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
		}
		Transform ctrl = GetCtrl(UI.BTN_GRAPH);
		if (ctrl != null)
		{
			int num = -1;
			EquipItemInfo equipItemInfo6 = detailItemData as EquipItemInfo;
			if (equipItemInfo6 != null)
			{
				num = equipItemInfo6.tableData.damageDistanceId;
			}
			else
			{
				EquipItemTable.EquipItemData equipItemData = detailItemData as EquipItemTable.EquipItemData;
				if (equipItemData != null)
				{
					num = equipItemData.damageDistanceId;
				}
			}
			bool active = num >= 0;
			ctrl.get_gameObject().SetActive(active);
		}
		base.Initialize();
	}

	public unsafe override void UpdateUI()
	{
		SetActive((Enum)UI.OBJ_FRAME_BG, IsShowFrameBG());
		detailBase = SetPrefab(GetCtrl(UI.OBJ_DETAIL_ROOT), "ItemDetailEquipBase", true);
		SetFontStyle(detailBase, UI.STR_TITLE_ITEM_INFO, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_STATUS, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_SKILL_SLOT, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_ABILITY, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_SELL, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_ATK, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_ELEM_ATK, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_DEF, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_ELEM_DEF, 2);
		SetFontStyle(detailBase, UI.STR_TITLE_HP, 2);
		EquipItemInfo equip = detailItemData as EquipItemInfo;
		SetActive((Enum)UI.BTN_CHANGE, localEquipSetData != null && CanSmithSection(callSection));
		SetActive((Enum)UI.BTN_CREATE, CanSmithSection(callSection));
		SetActive((Enum)UI.BTN_GROW, CanSmithSection(callSection));
		SetActive((Enum)UI.BTN_GROW_OFF, CanSmithSection(callSection));
		SetActive((Enum)UI.BTN_ABILITY, CanSmithSection(callSection));
		SetActive((Enum)UI.BTN_ABILITY_OFF, CanSmithSection(callSection));
		SetActive((Enum)UI.BTN_SELL, IsEnableDispSellButton(callSection) && MonoBehaviourSingleton<ItemExchangeManager>.I.IsExchangeScene());
		if (equip != null)
		{
			int exceed = equip.exceed;
			SetActive((Enum)UI.BTN_EXCEED, equip.tableData.exceedID != 0);
			SetActive((Enum)UI.SPR_COUNT_0_ON, exceed > 0);
			SetActive((Enum)UI.SPR_COUNT_1_ON, exceed > 1);
			SetActive((Enum)UI.SPR_COUNT_2_ON, exceed > 2);
			SetActive((Enum)UI.SPR_COUNT_3_ON, exceed > 3);
			EquipParam(equip);
			SetSkillIconButton(detailBase, UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButton", equip.tableData, equipAttachSkill, "SKILL_ICON_BUTTON", 0);
			SetSprite((Enum)UI.SPR_SP_ATTACK_TYPE, (!equip.tableData.IsWeapon()) ? string.Empty : equip.tableData.spAttackType.GetBigFrameSpriteName());
			AbilityItemInfo abilityItem = equip.GetAbilityItem();
			bool flag = abilityItem != null;
			if ((equip.ability != null && equip.ability.Length > 0) || flag)
			{
				bool empty_ability = true;
				int validAbilityLength = equip.GetValidAbilityLength();
				string allAbilityName = string.Empty;
				string allAp = string.Empty;
				string allAbilityDesc = string.Empty;
				_003CUpdateUI_003Ec__AnonStorey3C5 _003CUpdateUI_003Ec__AnonStorey3C;
				SetTable(detailBase, UI.TBL_ABILITY, "ItemDetailEquipAbilityItem", equip.ability.Length + (flag ? 1 : 0), false, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey3C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				PreCacheAbilityDetail(allAbilityName, allAp, allAbilityDesc);
				if (empty_ability)
				{
					SetActive(detailBase, UI.STR_NON_ABILITY, 0 == validAbilityLength);
					SetActive((Enum)UI.BTN_ABILITY, false);
					SetActive((Enum)UI.BTN_ABILITY_OFF, CanSmithSection(callSection));
				}
				else
				{
					SetActive(detailBase, UI.STR_NON_ABILITY, false);
					SetActive((Enum)UI.BTN_ABILITY_OFF, false);
				}
				if (equip.tableData.IsShadow())
				{
					SetActive(detailBase, UI.STR_NON_ABILITY, false);
					SetActive((Enum)UI.BTN_ABILITY, CanSmithSection(callSection));
					SetActive((Enum)UI.BTN_ABILITY_OFF, CanSmithSection(callSection));
				}
			}
			else
			{
				SetActive(detailBase, UI.STR_NON_ABILITY, true);
				SetActive((Enum)UI.BTN_ABILITY, false);
				SetActive((Enum)UI.BTN_ABILITY_OFF, CanSmithSection(callSection));
			}
		}
		else
		{
			SetActive((Enum)UI.SPR_COUNT_0_ON, false);
			SetActive((Enum)UI.SPR_COUNT_1_ON, false);
			SetActive((Enum)UI.SPR_COUNT_2_ON, false);
			SetActive((Enum)UI.SPR_COUNT_3_ON, false);
			EquipItemTable.EquipItemData table = detailItemData as EquipItemTable.EquipItemData;
			SetActive((Enum)UI.BTN_EXCEED, table.exceedID != 0);
			EquipTableParam(table);
			EquipItemTable.EquipItemData equipItemData = detailItemData as EquipItemTable.EquipItemData;
			if (equipItemData.id == 81160110 || equipItemData.id == 82160110 || equipItemData.id == 83160110 || equipItemData.id == 84160110 || equipItemData.id == 21160111 || equipItemData.id == 22160111 || equipItemData.id == 23160111 || equipItemData.id == 24160111)
			{
				SkillSlotUIData[] skillSlotData = GetSkillSlotData(detailItemData as EquipItemTable.EquipItemData, 0);
				SetSkillIconButton(detailBase, UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButton", table, skillSlotData, "SKILL_ICON_BUTTON", 0);
			}
			else
			{
				SetSkillIconButton(detailBase, UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButton", table, equipAttachSkill, "SKILL_ICON_BUTTON", 0);
			}
			SetSprite((Enum)UI.SPR_SP_ATTACK_TYPE, (!table.IsWeapon()) ? string.Empty : table.spAttackType.GetBigFrameSpriteName());
			if (table.fixedAbility.Length > 0)
			{
				string allAbilityName2 = string.Empty;
				string allAp2 = string.Empty;
				string allAbilityDesc2 = string.Empty;
				_003CUpdateUI_003Ec__AnonStorey3C8 _003CUpdateUI_003Ec__AnonStorey3C2;
				SetTable(detailBase, UI.TBL_ABILITY, "ItemDetailEquipAbilityItem", table.fixedAbility.Length, false, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey3C2, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				PreCacheAbilityDetail(allAbilityName2, allAp2, allAbilityDesc2);
				SetActive(detailBase, UI.STR_NON_ABILITY, false);
			}
			else
			{
				SetActive(detailBase, UI.STR_NON_ABILITY, true);
			}
		}
	}

	private void EquipParam(EquipItemInfo item)
	{
		EquipItemTable.EquipItemData equipItemData = item?.tableData;
		if (item != null && equipItemData != null)
		{
			bool flag = item.tableData.IsVisual();
			SetLabelText(detailBase, UI.LBL_NAME, equipItemData.name);
			SetActive(detailBase, UI.STR_LV, !flag);
			SetActive(detailBase, UI.STR_ONLY_VISUAL, flag);
			SetLabelText(detailBase, UI.LBL_LV_NOW, item.level.ToString());
			SetLabelText(detailBase, UI.LBL_LV_MAX, equipItemData.maxLv.ToString());
			SetLabelText(detailBase, UI.LBL_ATK, item.atk.ToString());
			SetLabelText(detailBase, UI.LBL_ELEM, item.elemAtk.ToString());
			SetElementSprite(detailBase, UI.SPR_ELEM, item.GetElemAtkType());
			SetLabelText(detailBase, UI.LBL_DEF, item.def.ToString());
			int num = item.elemDef;
			if (equipItemData.isFormer)
			{
				num = Mathf.FloorToInt((float)num * 0.1f);
			}
			SetLabelText(detailBase, UI.LBL_ELEM_DEF, num.ToString());
			SetDefElementSprite(detailBase, UI.SPR_ELEM_DEF, item.GetElemDefType());
			SetLabelText(detailBase, UI.LBL_HP, item.hp.ToString());
			SetLabelText(detailBase, UI.LBL_SELL, item.sellPrice.ToString());
			SetActive(detailBase, UI.OBJ_FAVORITE_ROOT, (callSection & (CURRENT_SECTION.QUEST_RESULT | CURRENT_SECTION.EQUIP_LIST)) == CURRENT_SECTION.NONE);
			SetActive(detailBase, UI.SPR_IS_EVOLVE, item.tableData.IsEvolve());
			SetEquipmentTypeIcon(detailBase, UI.SPR_TYPE_ICON, UI.SPR_TYPE_ICON_BG, UI.SPR_TYPE_ICON_RARITY, item.tableData);
			SetRenderEquipModel((Enum)UI.TEX_MODEL, equipItemData.id, sex, faceID, 1f);
			ResetTween(detailBase, UI.TWN_FAVORITE, 0);
			ResetTween(detailBase, UI.TWN_UNFAVORITE, 0);
			SetActive(detailBase, UI.TWN_UNFAVORITE, !item.isFavorite);
			SetActive(detailBase, UI.TWN_FAVORITE, item.isFavorite);
			bool flag2 = !item.IsLevelMax() || !item.IsExceedMax() || item.tableData.IsEvolve() || item.tableData.IsShadow();
			SetActive((Enum)UI.BTN_GROW, flag2 && CanSmithSection(callSection));
			SetActive((Enum)UI.BTN_GROW_OFF, !flag2 && CanSmithSection(callSection));
		}
		else
		{
			NotDataEquipParam();
		}
	}

	protected virtual void EquipTableParam(EquipItemTable.EquipItemData table_data)
	{
		if (table_data != null)
		{
			bool flag = table_data.IsVisual();
			int num = table_data.baseAtk;
			int num2 = table_data.baseDef;
			int num3 = (table_data.baseElemAtk != 0) ? table_data.baseElemAtk : 0;
			int num4 = (table_data.baseElemDef != 0) ? table_data.baseElemDef : 0;
			int num5 = table_data.baseHp;
			SetLabelText(detailBase, UI.LBL_NAME, table_data.name);
			SetActive(detailBase, UI.STR_LV, !flag);
			SetActive(detailBase, UI.STR_ONLY_VISUAL, flag);
			SetLabelText(detailBase, UI.LBL_LV_NOW, "1");
			SetLabelText(detailBase, UI.LBL_LV_MAX, table_data.maxLv.ToString());
			SetLabelText(detailBase, UI.LBL_ATK, num.ToString());
			SetLabelText(detailBase, UI.LBL_ELEM, num3.ToString());
			SetElementSprite(detailBase, UI.SPR_ELEM, table_data.GetElemAtkType(null));
			SetLabelText(detailBase, UI.LBL_DEF, num2.ToString());
			SetLabelText(detailBase, UI.LBL_ELEM_DEF, num4.ToString());
			SetDefElementSprite(detailBase, UI.SPR_ELEM_DEF, table_data.GetElemDefType(null));
			SetLabelText(detailBase, UI.LBL_HP, num5.ToString());
			SetLabelText(detailBase, UI.LBL_SELL, table_data.sale.ToString());
			SetActive(detailBase, UI.OBJ_FAVORITE_ROOT, false);
			SetActive(detailBase, UI.SPR_IS_EVOLVE, table_data.IsEvolve());
			SetEquipmentTypeIcon(detailBase, UI.SPR_TYPE_ICON, UI.SPR_TYPE_ICON_BG, UI.SPR_TYPE_ICON_RARITY, table_data);
			SetRenderEquipModel((Enum)UI.TEX_MODEL, table_data.id, sex, faceID, 1f);
		}
		else
		{
			NotDataEquipParam();
		}
	}

	private void NotDataEquipParam()
	{
		Log.Error("ItemDetailEquip is Not Item Data");
		string text = "----";
		SetLabelText(detailBase, UI.LBL_NAME, text);
		SetLabelText(detailBase, UI.LBL_LV_NOW, text);
		SetLabelText(detailBase, UI.LBL_LV_MAX, text);
		SetLabelText(detailBase, UI.LBL_ATK, text);
		SetLabelText(detailBase, UI.LBL_DEF, text);
		SetLabelText(detailBase, UI.LBL_ELEM, text);
		SetActive(detailBase, UI.OBJ_ATK_ROOT, false);
		SetActive(detailBase, UI.OBJ_DEF_ROOT, false);
		SetActive(detailBase, UI.OBJ_ELEM_ROOT, false);
		SetActive(detailBase, UI.SPR_IS_EVOLVE, false);
	}

	protected void OnQuery_SWITCH_FAVORITE()
	{
		OnQueryFavorite(detailItemData as EquipItemInfo, delegate(EquipItemInfo item)
		{
			detailItemData = item;
			eventData = item;
			gameEventData[1] = item;
		});
	}

	protected unsafe void OnQueryFavorite(EquipItemInfo select_item, Action<EquipItemInfo> callback)
	{
		if (select_item != null)
		{
			GameSection.StayEvent();
			_003COnQueryFavorite_003Ec__AnonStorey3C9 _003COnQueryFavorite_003Ec__AnonStorey3C;
			MonoBehaviourSingleton<StatusManager>.I.SendInventoryEquipLock(select_item.uniqueID, new Action<bool, EquipItemInfo>((object)_003COnQueryFavorite_003Ec__AnonStorey3C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	protected virtual void OnQuery_ABILITY()
	{
		switch (callSection)
		{
		default:
			GameSection.StopEvent();
			break;
		case CURRENT_SECTION.STATUS_TOP:
		case CURRENT_SECTION.STATUS_SKILL_LIST:
		case CURRENT_SECTION.STATUS_EQUIP:
		case CURRENT_SECTION.STATUS_EQUIP_SKILL:
		case CURRENT_SECTION.ITEM_STORAGE:
		case CURRENT_SECTION.EQUIP_SET_DETAIL_STATUS:
		{
			EquipItemInfo equipItemInfo = detailItemData as EquipItemInfo;
			if (equipItemInfo == null || MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(equipItemInfo.uniqueID) == null)
			{
				GameSection.StopEvent();
			}
			else
			{
				SmithManager.SmithGrowData smithGrowData = MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>();
				smithGrowData.selectEquipData = equipItemInfo;
			}
			break;
		}
		}
	}

	protected void OnQuery_SELL()
	{
		EquipItemInfo equipItemInfo = detailItemData as EquipItemInfo;
		if (equipItemInfo == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			EquipItemSortData equipItemSortData = new EquipItemSortData();
			equipItemSortData.SetItem(equipItemInfo);
			if (!equipItemSortData.CanSale())
			{
				if (equipItemSortData.IsFavorite())
				{
					GameSection.ChangeEvent("NOT_SALE_FAVORITE", null);
				}
				else
				{
					GameSection.ChangeEvent("NOT_SALE_EQUIPPING", null);
				}
			}
			else if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP) && equipItemSortData.GetTableID() == 10000000)
			{
				GameSection.ChangeEvent("NOT_SELL_DEFAULT_WEAPON", null);
			}
			else
			{
				List<SortCompareData> list = new List<SortCompareData>();
				list.Add(equipItemSortData);
				GameSection.ChangeEvent("SELL", new object[2]
				{
					ItemStorageTop.TAB_MODE.EQUIP,
					list
				});
			}
		}
	}

	protected void OnQuery_EXCEED()
	{
		EquipItemTable.EquipItemData equipItemData = null;
		int num = 0;
		EquipItemInfo equipItemInfo = detailItemData as EquipItemInfo;
		if (equipItemInfo == null)
		{
			equipItemData = (detailItemData as EquipItemTable.EquipItemData);
		}
		else
		{
			equipItemData = equipItemInfo.tableData;
			num = equipItemInfo.exceed;
		}
		GameSection.SetEventData(new object[2]
		{
			equipItemData,
			num
		});
	}

	protected virtual void OnQuery_SKILL_ICON_BUTTON()
	{
		object obj = (callSection == CURRENT_SECTION.QUEST_RESULT) ? eventData : detailItemData;
		GameSection.SetEventData(new object[3]
		{
			callSection,
			obj,
			sex
		});
	}

	protected void OnQuery_CHANGE()
	{
		if (localEquipSetData == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			MonoBehaviourSingleton<StatusManager>.I.SetEquippingItem(localEquipSetData.equipSetInfo.item[localEquipSetData.index]);
			MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = StatusTop.GetInventoryType(localEquipSetData.equipSetInfo, localEquipSetData.index);
			if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA2))
			{
				List<EquipItemInfo> weaponInventory = MonoBehaviourSingleton<InventoryManager>.I.GetWeaponInventory();
				for (int i = 0; i < weaponInventory.Count; i++)
				{
					if (weaponInventory[i].tableData.rarity >= RARITY_TYPE.S)
					{
						MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = (InventoryManager.INVENTORY_TYPE)(UIBehaviour.GetEquipmentTypeIndex(weaponInventory[i].tableData.type) + 1);
						break;
					}
				}
			}
			DetailEquipEventData detailEquipEventData = new DetailEquipEventData(gameEventData, localEquipSetData);
			GameSection.SetEventData(detailEquipEventData);
		}
	}

	protected void OnQuery_GROW()
	{
		if (MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>() == null)
		{
			MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>();
		}
		EquipItemInfo equipItemInfo = detailItemData as EquipItemInfo;
		MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData = equipItemInfo;
		if (equipItemInfo.IsLevelMax() && equipItemInfo.tableData.IsEvolve())
		{
			GameSection.SetEventData(new object[2]
			{
				SmithEquipBase.SmithType.EVOLVE,
				(detailItemData as EquipItemInfo).tableData.type
			});
			GameSection.ChangeEvent("EVOLVE", null);
		}
		else
		{
			GameSection.SetEventData(new object[2]
			{
				SmithEquipBase.SmithType.GROW,
				(detailItemData as EquipItemInfo).tableData.type
			});
		}
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_SKILL_CHANGE) != (NOTIFY_FLAG)0L)
		{
			EquipItemInfo equipItemInfo = detailItemData as EquipItemInfo;
			if (equipItemInfo != null)
			{
				equipItemInfo = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(equipItemInfo.uniqueID);
				equipAttachSkill = GetSkillSlotData(equipItemInfo);
			}
		}
		if ((flags & NOTIFY_FLAG.PRETREAT_SCENE) != (NOTIFY_FLAG)0L)
		{
			NoEventReleaseTouchAndReleases(touchAndReleaseButtons);
			OnQuery_RELEASE_ABILITY();
		}
		base.OnNotify(flags);
	}

	private void OnQuery_CREATE()
	{
		GameSection.SetEventData((detailItemData as EquipItemInfo).tableData.type);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE | NOTIFY_FLAG.UPDATE_SKILL_CHANGE;
	}

	public static bool CanSmithSection(CURRENT_SECTION section)
	{
		switch (section)
		{
		case CURRENT_SECTION.STATUS_TOP:
		case CURRENT_SECTION.STATUS_SKILL_LIST:
		case CURRENT_SECTION.STATUS_EQUIP:
		case CURRENT_SECTION.STATUS_EQUIP_SKILL:
		case CURRENT_SECTION.ITEM_STORAGE:
		case CURRENT_SECTION.EQUIP_SET_DETAIL_STATUS:
			return true;
		default:
			return false;
		}
	}

	public static bool IsEnableDispSellButton(CURRENT_SECTION _section)
	{
		if (_section == CURRENT_SECTION.SMITH_SELL)
		{
			return false;
		}
		return true;
	}

	protected void OnQuery_DISTANCE_GRAPH()
	{
		int num = -1;
		EquipItemInfo equipItemInfo = detailItemData as EquipItemInfo;
		if (equipItemInfo != null)
		{
			num = equipItemInfo.tableData.damageDistanceId;
		}
		else
		{
			EquipItemTable.EquipItemData equipItemData = detailItemData as EquipItemTable.EquipItemData;
			if (equipItemData != null)
			{
				num = equipItemData.damageDistanceId;
			}
		}
		GameSection.SetEventData(new object[1]
		{
			num
		});
	}

	protected void OnQuery_RELEASE_ABILITY()
	{
		if (!(abilityDetailPopUp == null))
		{
			abilityDetailPopUp.Hide();
			GameSection.StopEvent();
		}
	}

	protected void OnQuery_ABILITY_DATA_POPUP()
	{
		object[] array = GameSection.GetEventData() as object[];
		int num = (int)array[0];
		Transform targetTrans = array[1] as Transform;
		EquipItemAbility equipItemAbility = null;
		if (detailItemData is EquipItemInfo)
		{
			EquipItemInfo equipItemInfo = detailItemData as EquipItemInfo;
			equipItemAbility = equipItemInfo.ability[num];
		}
		else if (detailItemData is EquipItemTable.EquipItemData)
		{
			EquipItemTable.EquipItemData equipItemData = detailItemData as EquipItemTable.EquipItemData;
			EquipItem.Ability ability = equipItemData.fixedAbility[num];
			equipItemAbility = new EquipItemAbility((uint)ability.id, ability.pt);
		}
		if (equipItemAbility != null)
		{
			if (abilityDetailPopUp == null)
			{
				abilityDetailPopUp = CreateAndGetAbilityDetail((Enum)UI.OBJ_DETAIL_ROOT);
			}
			abilityDetailPopUp.ShowAbilityDetail(targetTrans);
			abilityDetailPopUp.SetAbilityDetailText(equipItemAbility);
			GameSection.StopEvent();
		}
	}

	protected void OnQuery_ABILITY_ITEM_DATA_POPUP()
	{
		Transform targetTrans = GameSection.GetEventData() as Transform;
		if (detailItemData is EquipItemInfo)
		{
			if (abilityDetailPopUp == null)
			{
				abilityDetailPopUp = CreateAndGetAbilityDetail((Enum)UI.OBJ_DETAIL_ROOT);
			}
			abilityDetailPopUp.ShowAbilityDetail(targetTrans);
			AbilityItemInfo abilityItem = (detailItemData as EquipItemInfo).GetAbilityItem();
			abilityDetailPopUp.SetAbilityDetailText(abilityItem.GetName(), string.Empty, abilityItem.GetDescription());
		}
	}

	private void PreCacheAbilityDetail(string name, string ap, string desc)
	{
		if (abilityDetailPopUp == null)
		{
			abilityDetailPopUp = CreateAndGetAbilityDetail((Enum)UI.OBJ_DETAIL_ROOT);
		}
		abilityDetailPopUp.PreCacheAbilityDetail(name, ap, desc);
	}
}
