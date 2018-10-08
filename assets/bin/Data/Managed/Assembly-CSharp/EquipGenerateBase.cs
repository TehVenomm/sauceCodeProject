using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipGenerateBase : EquipMaterialBase
{
	protected new enum UI
	{
		BTN_DECISION,
		BTN_INACTIVE,
		LBL_NEXT_BTN,
		LBL_TO_SELECT,
		BTN_TO_SELECT,
		BTN_TO_SELECT_CENTER,
		OBJ_ADD_ABILITY,
		LBL_ADD_ABILITY,
		TEX_MODEL,
		TEX_DETAIL_BASE_MODEL,
		OBJ_DETAIL_ROOT,
		OBJ_DETAIL_BASE_ROOT,
		OBJ_ITEM_INFO_ROOT,
		OBJ_AIM_GROW,
		BTN_AIM_L,
		BTN_AIM_R,
		BTN_AIM_L_INACTIVE,
		BTN_AIM_R_INACTIVE,
		SPR_AIM_L,
		SPR_AIM_R,
		LBL_AIM_LV,
		OBJ_EVOLVE_ROOT,
		LBL_EVO_INDEX,
		LBL_EVO_INDEX_MAX,
		BTN_EVO_L,
		BTN_EVO_R,
		BTN_EVO_L_INACTIVE,
		BTN_EVO_R_INACTIVE,
		SPR_EVO_L,
		SPR_EVO_R,
		BTN_EVO_R2,
		BTN_EVO_L2,
		BTN_EVO_L2_INACTIVE,
		BTN_EVO_R2_INACTIVE,
		SPR_EVO_R2,
		SPR_EVO_L2,
		OBJ_ORDER_L2,
		OBJ_ORDER_R2,
		OBJ_ORDER_NORMAL_CENTER,
		OBJ_ORDER_ATTRIBUTE_CENTER,
		SPR_ORDER_ELEM_CENTER,
		OBJ_ORDER_NORMAL_R,
		OBJ_ORDER_ATTRIBUTE_R,
		SPR_ORDER_ELEM_R,
		OBJ_ORDER_NORMAL_L,
		OBJ_ORDER_ATTRIBUTE_L,
		SPR_ORDER_ELEM_L,
		OBJ_ORDER_CENTER_ANIM_ROOT,
		OBJ_ORDER_L_ANIM_ROOT,
		OBJ_ORDER_R_ANIM_ROOT,
		STR_INACTIVE,
		STR_INACTIVE_REFLECT,
		STR_DECISION,
		STR_DECISION_REFLECT,
		STR_TITLE_MATERIAL,
		STR_TITLE_MONEY,
		STR_TITLE_ATK,
		STR_TITLE_ELEM,
		STR_TITLE_DEF,
		STR_TITLE_ELEM_DEF,
		STR_TITLE_HP,
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
		BTN_SELL,
		BTN_GROW,
		OBJ_FAVORITE_ROOT,
		SPR_FAVORITE,
		SPR_UNFAVORITE,
		SPR_IS_EVOLVE,
		TWN_FAVORITE,
		TWN_UNFAVORITE,
		OBJ_ATK_ROOT,
		OBJ_DEF_ROOT,
		OBJ_ELEM_ROOT,
		SPR_TYPE_ICON,
		SPR_TYPE_ICON_BG,
		SPR_TYPE_ICON_RARITY,
		STR_TITLE_ITEM_INFO,
		STR_TITLE_STATUS,
		STR_TITLE_SKILL_SLOT,
		STR_TITLE_ABILITY,
		STR_TITLE_SELL,
		STR_TITLE_ELEMENT,
		TBL_ABILITY,
		STR_NON_ABILITY,
		LBL_ABILITY,
		LBL_ABILITY_NUM,
		BTN_EXCEED,
		SPR_COUNT_0_ON,
		SPR_COUNT_1_ON,
		SPR_COUNT_2_ON,
		SPR_COUNT_3_ON,
		STR_ONLY_EXCEED,
		LBL_AFTER_ATK,
		LBL_AFTER_DEF,
		LBL_AFTER_HP,
		LBL_AFTER_ELEM,
		LBL_AFTER_ELEM_DEF,
		GRD_NEED_MATERIAL,
		LBL_GOLD,
		LBL_CAPTION,
		BTN_GRAPH,
		BTN_LIST,
		SPR_SP_ATTACK_TYPE,
		SPR_ORDER_ACTIONTYPE_CENTER,
		SPR_ORDER_ACTIONTYPE_LEFT,
		SPR_ORDER_ACTIONTYPE_RIGHT,
		BTN_SHADOW_EVOLVE,
		OBJ_ABILITY,
		OBJ_FIXEDABILITY,
		LBL_FIXEDABILITY,
		LBL_FIXEDABILITY_NUM,
		OBJ_ABILITY_ITEM,
		LBL_ABILITY_ITEM,
		OBJ_WEAPON_ROOT,
		OBJ_ARMOR_ROOT,
		LinePartsR01
	}

	protected SkillItemTable.SkillItemData[] skillDataTable;

	protected AbilityDetailPopUp abilityDetailPopUp;

	protected List<Transform> touchAndReleaseButtons = new List<Transform>();

	public override void Initialize()
	{
		base.Initialize();
	}

	protected override string GetEquipItemName()
	{
		return GetEquipTableData().name;
	}

	protected unsafe override void EquipTableParam()
	{
		int exceed = 0;
		EquipItemInfo equipData = GetEquipData();
		if (equipData != null)
		{
			exceed = equipData.exceed;
		}
		EquipItemTable.EquipItemData table_data = GetEquipTableData();
		if (table_data != null)
		{
			EquipItemExceedParamTable.EquipItemExceedParamAll equipItemExceedParamAll = table_data.GetExceedParam((uint)exceed);
			if (equipItemExceedParamAll == null)
			{
				equipItemExceedParamAll = new EquipItemExceedParamTable.EquipItemExceedParamAll();
			}
			SetLabelText((Enum)UI.LBL_NAME, table_data.name);
			SetLabelText((Enum)UI.LBL_LV_NOW, "1");
			SetLabelText((Enum)UI.LBL_LV_MAX, table_data.maxLv.ToString());
			int num = (int)table_data.baseAtk + (int)equipItemExceedParamAll.atk;
			int elemAtk = equipItemExceedParamAll.GetElemAtk(table_data.atkElement);
			SetElementSprite((Enum)UI.SPR_ELEM, equipItemExceedParamAll.GetElemAtkType(table_data.atkElement));
			SetLabelText((Enum)UI.LBL_ATK, num.ToString());
			SetLabelText((Enum)UI.LBL_ELEM, elemAtk.ToString());
			int num2 = (int)table_data.baseDef + (int)equipItemExceedParamAll.def;
			SetLabelText((Enum)UI.LBL_DEF, num2.ToString());
			int elemDef = equipItemExceedParamAll.GetElemDef(table_data.defElement);
			SetDefElementSprite((Enum)UI.SPR_ELEM_DEF, equipItemExceedParamAll.GetElemDefType(table_data.defElement));
			SetLabelText((Enum)UI.LBL_ELEM_DEF, elemDef.ToString());
			int num3 = (int)table_data.baseHp + (int)equipItemExceedParamAll.hp;
			SetLabelText((Enum)UI.LBL_HP, num3.ToString());
			SetActive((Enum)UI.SPR_IS_EVOLVE, table_data.IsEvolve());
			SetEquipmentTypeIcon((Enum)UI.SPR_TYPE_ICON, (Enum)UI.SPR_TYPE_ICON_BG, (Enum)UI.SPR_TYPE_ICON_RARITY, table_data);
			SetLabelText((Enum)UI.LBL_SELL, table_data.sale.ToString());
			if (smithType != SmithType.EVOLVE)
			{
				SetSkillIconButton(UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButton", table_data, GetSkillSlotData(table_data, 0), null, 0);
				if (table_data.fixedAbility.Length > 0)
				{
					string allAbilityName = string.Empty;
					string allAp = string.Empty;
					string allAbilityDesc = string.Empty;
					_003CEquipTableParam_003Ec__AnonStorey459 _003CEquipTableParam_003Ec__AnonStorey;
					SetTable(UI.TBL_ABILITY, "ItemDetailEquipAbilityItem", table_data.fixedAbility.Length, false, new Action<int, Transform, bool>((object)_003CEquipTableParam_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
					SetActive((Enum)UI.STR_NON_ABILITY, false);
					PreCacheAbilityDetail(allAbilityName, allAp, allAbilityDesc);
				}
				else
				{
					SetActive((Enum)UI.STR_NON_ABILITY, true);
				}
			}
		}
	}

	protected override void OnQuery_SKILL_ICON_BUTTON()
	{
		GameSection.SetEventData(new object[2]
		{
			ItemDetailEquip.CURRENT_SECTION.SMITH_CREATE,
			GetEquipTableData()
		});
	}

	protected override void OnQuery_START()
	{
		SmithManager.ERR_SMITH_SEND eRR_SMITH_SEND = MonoBehaviourSingleton<SmithManager>.I.CheckCreateEquipItem(GetCreateEquiptableID());
		if (eRR_SMITH_SEND != 0)
		{
			GameSection.ChangeEvent(eRR_SMITH_SEND.ToString(), null);
		}
		else
		{
			isDialogEventYES = false;
			GameSection.SetEventData(new object[1]
			{
				GetEquipItemName()
			});
		}
	}

	protected unsafe override void Send()
	{
		SmithManager.ResultData result_data = new SmithManager.ResultData();
		GameSection.SetEventData(result_data);
		GameSection.StayEvent();
		_003CSend_003Ec__AnonStorey45A _003CSend_003Ec__AnonStorey45A;
		MonoBehaviourSingleton<SmithManager>.I.SendCreateEquipItem(GetCreateEquiptableID(), new Action<Error, EquipItemInfo>((object)_003CSend_003Ec__AnonStorey45A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	public void OnQuery_SmithCreateOverEquipItem_GO_ITEM_STORAGE()
	{
		string name = "TAB_" + 2;
		EventData[] autoEvents = new EventData[3]
		{
			new EventData("SECTION_BACK", null),
			new EventData("SELL", null),
			new EventData(name, null)
		};
		GameSection.StopEvent();
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	public void OnQuery_SmithCreateOverEquipItem_EXPAND_STORAGE()
	{
		DispatchEvent("EXPAND_STORAGE", null);
	}

	protected virtual void OnQuery_ABILITY_DATA_POPUP()
	{
		object[] array = GameSection.GetEventData() as object[];
		int num = (int)array[0];
		EquipItemTable.EquipItemData equipTableData = GetEquipTableData();
		EquipItem.Ability ability = equipTableData.fixedAbility[num];
		Transform targetTrans = array[1] as Transform;
		EquipItemAbility abilityDetailText = new EquipItemAbility((uint)ability.id, ability.pt);
		if (abilityDetailPopUp == null)
		{
			abilityDetailPopUp = CreateAndGetAbilityDetail((Enum)UI.OBJ_DETAIL_ROOT);
		}
		abilityDetailPopUp.ShowAbilityDetail(targetTrans);
		abilityDetailPopUp.SetAbilityDetailText(abilityDetailText);
		GameSection.StopEvent();
	}

	protected void OnQuery_RELEASE_ABILITY()
	{
		if (!(abilityDetailPopUp == null))
		{
			abilityDetailPopUp.Hide();
			GameSection.StopEvent();
		}
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		base.OnNotify(flags);
		if ((flags & NOTIFY_FLAG.PRETREAT_SCENE) != (NOTIFY_FLAG)0L)
		{
			NoEventReleaseTouchAndReleases(touchAndReleaseButtons);
			OnQuery_RELEASE_ABILITY();
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

	protected virtual uint GetCreateEquiptableID()
	{
		return 0u;
	}
}
