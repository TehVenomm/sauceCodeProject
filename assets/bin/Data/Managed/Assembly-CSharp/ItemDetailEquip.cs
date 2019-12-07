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
		gameEventData = (GameSection.GetEventData() as object[]);
		callSection = (CURRENT_SECTION)gameEventData[0];
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
		{
			detailItemData = eventData;
			equipAttachSkill = GetSkillSlotData(detailItemData as EquipItemTable.EquipItemData, 0);
			for (int i = 0; i < equipAttachSkill.Length; i++)
			{
				equipAttachSkill[i].slotData.skill_id = 0u;
			}
			break;
		}
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
			ctrl.gameObject.SetActive(active);
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetActive(UI.OBJ_FRAME_BG, IsShowFrameBG());
		detailBase = SetPrefab(GetCtrl(UI.OBJ_DETAIL_ROOT), "ItemDetailEquipBase");
		SetFontStyle(detailBase, UI.STR_TITLE_ITEM_INFO, FontStyle.Italic);
		SetFontStyle(detailBase, UI.STR_TITLE_STATUS, FontStyle.Italic);
		SetFontStyle(detailBase, UI.STR_TITLE_SKILL_SLOT, FontStyle.Italic);
		SetFontStyle(detailBase, UI.STR_TITLE_ABILITY, FontStyle.Italic);
		SetFontStyle(detailBase, UI.STR_TITLE_SELL, FontStyle.Italic);
		SetFontStyle(detailBase, UI.STR_TITLE_ATK, FontStyle.Italic);
		SetFontStyle(detailBase, UI.STR_TITLE_ELEM_ATK, FontStyle.Italic);
		SetFontStyle(detailBase, UI.STR_TITLE_DEF, FontStyle.Italic);
		SetFontStyle(detailBase, UI.STR_TITLE_ELEM_DEF, FontStyle.Italic);
		SetFontStyle(detailBase, UI.STR_TITLE_HP, FontStyle.Italic);
		EquipItemInfo equip = detailItemData as EquipItemInfo;
		SetActive(UI.BTN_CHANGE, localEquipSetData != null && CanSmithSection(callSection));
		SetActive(UI.BTN_CREATE, CanSmithSection(callSection));
		SetActive(UI.BTN_GROW, CanSmithSection(callSection));
		SetActive(UI.BTN_GROW_OFF, CanSmithSection(callSection));
		SetActive(UI.BTN_ABILITY, CanSmithSection(callSection));
		SetActive(UI.BTN_ABILITY_OFF, CanSmithSection(callSection));
		SetActive(UI.BTN_SELL, IsEnableDispSellButton(callSection) && MonoBehaviourSingleton<ItemExchangeManager>.I.IsExchangeScene());
		if (equip != null)
		{
			int exceed = equip.exceed;
			SetActive(UI.BTN_EXCEED, equip.tableData.exceedID != 0);
			SetActive(UI.SPR_COUNT_0_ON, exceed > 0);
			SetActive(UI.SPR_COUNT_1_ON, exceed > 1);
			SetActive(UI.SPR_COUNT_2_ON, exceed > 2);
			SetActive(UI.SPR_COUNT_3_ON, exceed > 3);
			EquipParam(equip);
			SetSkillIconButton(detailBase, UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButton", equip.tableData, equipAttachSkill);
			SetSprite(UI.SPR_SP_ATTACK_TYPE, equip.tableData.IsWeapon() ? equip.tableData.spAttackType.GetBigFrameSpriteName() : "");
			AbilityItemInfo abilityItem = equip.GetAbilityItem();
			bool flag = abilityItem != null;
			if ((equip.ability != null && equip.ability.Length != 0) | flag)
			{
				bool empty_ability = true;
				int validAbilityLength = equip.GetValidAbilityLength();
				string allAbilityName2 = "";
				string allAp2 = "";
				string allAbilityDesc2 = "";
				SetTable(detailBase, UI.TBL_ABILITY, "ItemDetailEquipAbilityItem", equip.ability.Length + (flag ? 1 : 0), reset: false, delegate(int i, Transform t, bool is_recycle)
				{
					if (i < equip.ability.Length)
					{
						EquipItemAbility equipItemAbility2 = equip.ability[i];
						if (equipItemAbility2.id == 0)
						{
							SetActive(t, is_visible: false);
						}
						else
						{
							SetActive(t, is_visible: true);
							if (equipItemAbility2.IsNeedUpdate())
							{
								SetActive(t, UI.OBJ_ABILITY, is_visible: false);
								SetActive(t, UI.OBJ_FIXEDABILITY, is_visible: false);
								SetActive(t, UI.OBJ_NEED_UPDATE_ABILITY, is_visible: true);
								SetLabelText(t, UI.LBL_NEED_UPDATE_ABILITY, StringTable.Get(STRING_CATEGORY.ABILITY, 0u));
								SetButtonEnabled(t, is_enabled: false);
							}
							else if (!equipItemAbility2.IsActiveAbility())
							{
								SetActive(t, UI.OBJ_ABILITY, is_visible: false);
								SetActive(t, UI.OBJ_FIXEDABILITY, is_visible: false);
								SetActive(t, UI.OBJ_NEED_UPDATE_ABILITY, is_visible: true);
								SetLabelText(t, UI.LBL_NEED_UPDATE_ABILITY, StringTable.Get(STRING_CATEGORY.ABILITY, 1u));
								SetButtonEnabled(t, is_enabled: false);
							}
							else if (equip.IsFixedAbility(i))
							{
								SetActive(t, UI.OBJ_ABILITY, is_visible: false);
								SetActive(t, UI.OBJ_FIXEDABILITY, is_visible: true);
								SetActive(t, UI.OBJ_NEED_UPDATE_ABILITY, is_visible: false);
								SetLabelText(t, UI.LBL_FIXEDABILITY, Utility.TrimText(equipItemAbility2.GetName(), FindCtrl(t, UI.LBL_FIXEDABILITY).GetComponent<UILabel>()));
								SetLabelText(t, UI.LBL_FIXEDABILITY_NUM, equipItemAbility2.GetAP());
							}
							else
							{
								empty_ability = false;
								SetActive(t, UI.OBJ_NEED_UPDATE_ABILITY, is_visible: false);
								SetLabelText(t, UI.LBL_ABILITY, Utility.TrimText(equipItemAbility2.GetName(), FindCtrl(t, UI.LBL_ABILITY).GetComponent<UILabel>()));
								SetLabelText(t, UI.LBL_ABILITY_NUM, equipItemAbility2.GetAP());
							}
							SetAbilityItemEvent(t, i, touchAndReleaseButtons);
							allAbilityName2 += equipItemAbility2.GetName();
							allAp2 += equipItemAbility2.GetAP();
							allAbilityDesc2 += equipItemAbility2.GetDescription();
						}
					}
					else
					{
						SetActive(t, UI.OBJ_ABILITY, is_visible: false);
						SetActive(t, UI.OBJ_ABILITY_ITEM, is_visible: true);
						SetLabelText(t, UI.LBL_ABILITY_ITEM, abilityItem.GetName());
						SetTouchAndRelease(t.GetComponentInChildren<UIButton>().transform, "ABILITY_ITEM_DATA_POPUP", "RELEASE_ABILITY", t);
						allAbilityName2 += abilityItem.GetName();
						allAbilityDesc2 += abilityItem.GetDescription();
					}
				});
				PreCacheAbilityDetail(allAbilityName2, allAp2, allAbilityDesc2);
				if (empty_ability)
				{
					SetActive(detailBase, UI.STR_NON_ABILITY, validAbilityLength == 0);
					SetActive(UI.BTN_ABILITY, is_visible: false);
					SetActive(UI.BTN_ABILITY_OFF, CanSmithSection(callSection));
				}
				else
				{
					SetActive(detailBase, UI.STR_NON_ABILITY, is_visible: false);
					SetActive(UI.BTN_ABILITY_OFF, is_visible: false);
				}
				if (equip.tableData.IsShadow())
				{
					SetActive(detailBase, UI.STR_NON_ABILITY, is_visible: false);
					SetActive(UI.BTN_ABILITY, CanSmithSection(callSection));
					SetActive(UI.BTN_ABILITY_OFF, CanSmithSection(callSection));
				}
			}
			else
			{
				SetActive(detailBase, UI.STR_NON_ABILITY, is_visible: true);
				SetActive(UI.BTN_ABILITY, is_visible: false);
				SetActive(UI.BTN_ABILITY_OFF, CanSmithSection(callSection));
			}
		}
		else
		{
			SetActive(UI.SPR_COUNT_0_ON, is_visible: false);
			SetActive(UI.SPR_COUNT_1_ON, is_visible: false);
			SetActive(UI.SPR_COUNT_2_ON, is_visible: false);
			SetActive(UI.SPR_COUNT_3_ON, is_visible: false);
			EquipItemTable.EquipItemData table = detailItemData as EquipItemTable.EquipItemData;
			SetActive(UI.BTN_EXCEED, table.exceedID != 0);
			EquipTableParam(table);
			EquipItemTable.EquipItemData equipItemData = detailItemData as EquipItemTable.EquipItemData;
			if (equipItemData.id == 81160110 || equipItemData.id == 82160110 || equipItemData.id == 83160110 || equipItemData.id == 84160110 || equipItemData.id == 21160111 || equipItemData.id == 22160111 || equipItemData.id == 23160111 || equipItemData.id == 24160111)
			{
				SkillSlotUIData[] skillSlotData = GetSkillSlotData(detailItemData as EquipItemTable.EquipItemData, 0);
				SetSkillIconButton(detailBase, UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButton", table, skillSlotData);
			}
			else
			{
				SetSkillIconButton(detailBase, UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButton", table, equipAttachSkill);
			}
			SetSprite(UI.SPR_SP_ATTACK_TYPE, table.IsWeapon() ? table.spAttackType.GetBigFrameSpriteName() : "");
			if (table.fixedAbility.Length != 0)
			{
				string allAbilityName = "";
				string allAp = "";
				string allAbilityDesc = "";
				SetTable(detailBase, UI.TBL_ABILITY, "ItemDetailEquipAbilityItem", table.fixedAbility.Length, reset: false, delegate(int i, Transform t, bool is_recycle)
				{
					EquipItemAbility equipItemAbility = new EquipItemAbility((uint)table.fixedAbility[i].id, table.fixedAbility[i].pt);
					SetActive(t, is_visible: true);
					SetActive(t, UI.OBJ_ABILITY, is_visible: false);
					SetActive(t, UI.OBJ_FIXEDABILITY, is_visible: true);
					SetLabelText(t, UI.LBL_FIXEDABILITY, Utility.TrimText(equipItemAbility.GetName(), FindCtrl(t, UI.LBL_FIXEDABILITY).GetComponent<UILabel>()));
					SetLabelText(t, UI.LBL_FIXEDABILITY_NUM, equipItemAbility.GetAP());
					SetAbilityItemEvent(t, i, touchAndReleaseButtons);
					allAbilityName += equipItemAbility.GetName();
					allAp += equipItemAbility.GetAP();
					allAbilityDesc += equipItemAbility.GetDescription();
				});
				PreCacheAbilityDetail(allAbilityName, allAp, allAbilityDesc);
				SetActive(detailBase, UI.STR_NON_ABILITY, is_visible: false);
			}
			else
			{
				SetActive(detailBase, UI.STR_NON_ABILITY, is_visible: true);
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
			SetActive(detailBase, UI.OBJ_FAVORITE_ROOT, (callSection & (CURRENT_SECTION.QUEST_RESULT | CURRENT_SECTION.EQUIP_LIST)) == 0);
			SetActive(detailBase, UI.SPR_IS_EVOLVE, item.tableData.IsEvolve());
			SetEquipmentTypeIcon(detailBase, UI.SPR_TYPE_ICON, UI.SPR_TYPE_ICON_BG, UI.SPR_TYPE_ICON_RARITY, item.tableData);
			SetRenderEquipModel(UI.TEX_MODEL, equipItemData.id, sex, faceID);
			ResetTween(detailBase, UI.TWN_FAVORITE);
			ResetTween(detailBase, UI.TWN_UNFAVORITE);
			SetActive(detailBase, UI.TWN_UNFAVORITE, !item.isFavorite);
			SetActive(detailBase, UI.TWN_FAVORITE, item.isFavorite);
			bool flag2 = !item.IsLevelMax() || !item.IsExceedMax() || item.tableData.IsEvolve() || item.tableData.IsShadow();
			SetActive(UI.BTN_GROW, flag2 && CanSmithSection(callSection));
			SetActive(UI.BTN_GROW_OFF, !flag2 && CanSmithSection(callSection));
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
			SetElementSprite(detailBase, UI.SPR_ELEM, table_data.GetElemAtkType());
			SetLabelText(detailBase, UI.LBL_DEF, num2.ToString());
			SetLabelText(detailBase, UI.LBL_ELEM_DEF, num4.ToString());
			SetDefElementSprite(detailBase, UI.SPR_ELEM_DEF, table_data.GetElemDefType());
			SetLabelText(detailBase, UI.LBL_HP, num5.ToString());
			SetLabelText(detailBase, UI.LBL_SELL, table_data.sale.ToString());
			SetActive(detailBase, UI.OBJ_FAVORITE_ROOT, is_visible: false);
			SetActive(detailBase, UI.SPR_IS_EVOLVE, table_data.IsEvolve());
			SetEquipmentTypeIcon(detailBase, UI.SPR_TYPE_ICON, UI.SPR_TYPE_ICON_BG, UI.SPR_TYPE_ICON_RARITY, table_data);
			SetRenderEquipModel(UI.TEX_MODEL, table_data.id, sex, faceID);
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
		SetActive(detailBase, UI.OBJ_ATK_ROOT, is_visible: false);
		SetActive(detailBase, UI.OBJ_DEF_ROOT, is_visible: false);
		SetActive(detailBase, UI.OBJ_ELEM_ROOT, is_visible: false);
		SetActive(detailBase, UI.SPR_IS_EVOLVE, is_visible: false);
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

	protected void OnQueryFavorite(EquipItemInfo select_item, Action<EquipItemInfo> callback)
	{
		if (select_item != null)
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<StatusManager>.I.SendInventoryEquipLock(select_item.uniqueID, delegate(bool is_success, EquipItemInfo recv_equip_item)
			{
				if (is_success)
				{
					callback(recv_equip_item);
					if (recv_equip_item.isFavorite)
					{
						SetActive(detailBase, UI.TWN_UNFAVORITE, is_visible: false);
						SetActive(detailBase, UI.TWN_FAVORITE, is_visible: true);
						ResetTween(detailBase, UI.TWN_FAVORITE);
						PlayTween(detailBase, UI.TWN_FAVORITE, forward: true, delegate
						{
							GameSection.ChangeStayEvent("FAVORITE");
							GameSection.ResumeEvent(is_success);
						});
					}
					else
					{
						SetActive(detailBase, UI.TWN_FAVORITE, is_visible: false);
						SetActive(detailBase, UI.TWN_UNFAVORITE, is_visible: true);
						ResetTween(detailBase, UI.TWN_UNFAVORITE);
						PlayTween(detailBase, UI.TWN_UNFAVORITE, forward: true, delegate
						{
							GameSection.ChangeStayEvent("RELEASE_FAVORITE");
							GameSection.ResumeEvent(is_success);
						});
					}
				}
				else
				{
					GameSection.ResumeEvent(is_success);
				}
			});
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
				MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>().selectEquipData = equipItemInfo;
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
			return;
		}
		EquipItemSortData equipItemSortData = new EquipItemSortData();
		equipItemSortData.SetItem(equipItemInfo);
		if (!equipItemSortData.CanSale())
		{
			if (equipItemSortData.IsFavorite())
			{
				GameSection.ChangeEvent("NOT_SALE_FAVORITE");
			}
			else if (equipItemSortData.IsHomeEquipping())
			{
				GameSection.ChangeEvent("NOT_SALE_EQUIPPING");
			}
			else
			{
				GameSection.ChangeEvent("NOT_SALE_UNIQUE_EQUIPPING");
			}
		}
		else if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP) && equipItemSortData.GetTableID() == 10000000)
		{
			GameSection.ChangeEvent("NOT_SELL_DEFAULT_WEAPON");
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
		object obj = (callSection != CURRENT_SECTION.QUEST_RESULT) ? detailItemData : eventData;
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
			return;
		}
		if (StatusManager.IsUnique())
		{
			GameSection.ChangeEvent("UNIQUE_CHANGE");
		}
		MonoBehaviourSingleton<StatusManager>.I.SetEquippingItem(localEquipSetData.equipSetInfo.item[localEquipSetData.index]);
		MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = StatusTop.GetInventoryType(localEquipSetData.equipSetInfo, localEquipSetData.index);
		if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SHADOW_QUEST_WIN))
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
		GameSection.SetEventData(new DetailEquipEventData(gameEventData, localEquipSetData));
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
			GameSection.ChangeEvent("EVOLVE");
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
		object[] obj = GameSection.GetEventData() as object[];
		int num = (int)obj[0];
		Transform targetTrans = obj[1] as Transform;
		EquipItemAbility equipItemAbility = null;
		if (detailItemData is EquipItemInfo)
		{
			equipItemAbility = (detailItemData as EquipItemInfo).ability[num];
		}
		else if (detailItemData is EquipItemTable.EquipItemData)
		{
			EquipItem.Ability ability = (detailItemData as EquipItemTable.EquipItemData).fixedAbility[num];
			equipItemAbility = new EquipItemAbility((uint)ability.id, ability.pt);
		}
		if (equipItemAbility != null)
		{
			if (abilityDetailPopUp == null)
			{
				abilityDetailPopUp = CreateAndGetAbilityDetail(UI.OBJ_DETAIL_ROOT);
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
				abilityDetailPopUp = CreateAndGetAbilityDetail(UI.OBJ_DETAIL_ROOT);
			}
			abilityDetailPopUp.ShowAbilityDetail(targetTrans);
			AbilityItemInfo abilityItem = (detailItemData as EquipItemInfo).GetAbilityItem();
			abilityDetailPopUp.SetAbilityDetailText(abilityItem.GetName(), "", abilityItem.GetDescription());
		}
	}

	private void PreCacheAbilityDetail(string name, string ap, string desc)
	{
		if (abilityDetailPopUp == null)
		{
			abilityDetailPopUp = CreateAndGetAbilityDetail(UI.OBJ_DETAIL_ROOT);
		}
		abilityDetailPopUp.PreCacheAbilityDetail(name, ap, desc);
	}
}
