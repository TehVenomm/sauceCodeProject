using UnityEngine;

public class EquipSortBase : SortBase
{
	protected enum UI
	{
		LBL_FAVORITE,
		BTN_FAVORITE,
		OBJ_FRAME,
		LBL_ONLY_EQUIP,
		BTN_ONLY_EQUIP,
		BTN_N,
		BTN_HN,
		BTN_R,
		BTN_HR,
		BTN_SR,
		BTN_HSR,
		BTN_SSR,
		OBJ_TYPE_EQUIP,
		BTN_ONE_HAND_SWORD,
		BTN_TWO_HAND_SWORD,
		BTN_SPEAR,
		BTN_PAIR_SWORD,
		BTN_ARROW,
		BTN_ARMOR,
		BTN_HELM,
		BTN_ARM,
		BTN_LEG,
		OBJ_TYPE_SKILL,
		BTN_ATTACK,
		BTN_SUPPORT,
		BTN_HEAL,
		BTN_PASSIVE,
		BTN_GROW,
		SPR_ATTACK_GRAY,
		SPR_SUPPORT_GRAY,
		SPR_HEAL_GRAY,
		SPR_PASSIVE_GRAY,
		SPR_GROW_GRAY,
		BTN_FIRE,
		BTN_WATER,
		BTN_THUNDER,
		BTN_SOIL,
		BTN_LIGHT,
		BTN_DARK,
		BTN_NO_ELEMENT,
		BTN_NUM,
		BTN_GET,
		BTN_RARITY,
		BTN_LEVEL,
		BTN_ATK,
		BTN_DEF,
		BTN_SELL,
		BTN_SOCKET,
		BTN_PRICE,
		BTN_HP,
		BTN_ELEMENT,
		BTN_ELEM_ATK,
		BTN_ELEM_DEF,
		BTN_SKILL_TYPE,
		BTN_ASC,
		BTN_DESC,
		OBJ_HEIGHT_ANCHOR,
		GRD_REQUIREMENT,
		BTN_EQUIP_PAY,
		BTN_EQUIP_NO_PAY,
		OBJ_TYPE,
		OBJ_SORT_ROOT,
		SPR_SORT_UNDER_LINE,
		Frame
	}

	private UI[] rarityButton = new UI[7]
	{
		UI.BTN_N,
		UI.BTN_HN,
		UI.BTN_R,
		UI.BTN_HR,
		UI.BTN_SR,
		UI.BTN_HSR,
		UI.BTN_SSR
	};

	private UI?[] typeButton = new UI?[9]
	{
		UI.BTN_ONE_HAND_SWORD,
		UI.BTN_TWO_HAND_SWORD,
		UI.BTN_SPEAR,
		UI.BTN_PAIR_SWORD,
		UI.BTN_ARROW,
		UI.BTN_ARMOR,
		UI.BTN_HELM,
		UI.BTN_ARM,
		UI.BTN_LEG
	};

	private UI?[] typeSkillButton = new UI?[11]
	{
		UI.BTN_ATTACK,
		UI.BTN_SUPPORT,
		UI.BTN_HEAL,
		null,
		null,
		null,
		null,
		UI.BTN_PASSIVE,
		null,
		null,
		UI.BTN_GROW
	};

	private UI?[] typeSkillGrayButton = new UI?[11]
	{
		UI.SPR_ATTACK_GRAY,
		UI.SPR_SUPPORT_GRAY,
		UI.SPR_HEAL_GRAY,
		null,
		null,
		null,
		null,
		UI.SPR_PASSIVE_GRAY,
		null,
		null,
		UI.SPR_GROW_GRAY
	};

	private UI?[] elementButton = new UI?[7]
	{
		UI.BTN_FIRE,
		UI.BTN_WATER,
		UI.BTN_THUNDER,
		UI.BTN_SOIL,
		UI.BTN_LIGHT,
		UI.BTN_DARK,
		UI.BTN_NO_ELEMENT
	};

	private UI?[] requirementButton = new UI?[17]
	{
		null,
		UI.BTN_NUM,
		UI.BTN_GET,
		UI.BTN_RARITY,
		UI.BTN_LEVEL,
		UI.BTN_ATK,
		UI.BTN_DEF,
		UI.BTN_SELL,
		UI.BTN_SOCKET,
		UI.BTN_PRICE,
		null,
		null,
		UI.BTN_HP,
		UI.BTN_ELEMENT,
		UI.BTN_ELEM_ATK,
		UI.BTN_ELEM_DEF,
		UI.BTN_SKILL_TYPE
	};

	private UI[] ascButton = new UI[2]
	{
		UI.BTN_ASC,
		UI.BTN_DESC
	};

	private UI[] equipChangeSortBaseFilterButton = new UI[2]
	{
		UI.BTN_EQUIP_PAY,
		UI.BTN_EQUIP_NO_PAY
	};

	protected int visible_type_flag_skill = 1159;

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetActive(UI.OBJ_TYPE, is_visible: false);
		if (sortOrder.dialogType == DIALOG_TYPE.STORAGE_SKILL || sortOrder.dialogType == DIALOG_TYPE.SKILL)
		{
			SetActive(UI.OBJ_TYPE_SKILL, is_visible: true);
			SetActive(UI.OBJ_TYPE_EQUIP, is_visible: false);
			UIWidget component = GetComponent<UIWidget>(UI.OBJ_FRAME);
			if (component != null)
			{
				component.height = 820;
				component.transform.localPosition = new Vector3(0f, 410f, 0f);
				component.UpdateAnchors();
			}
		}
		else
		{
			SetActive(UI.OBJ_TYPE_SKILL, is_visible: false);
			SetActive(UI.OBJ_TYPE_EQUIP, is_visible: true);
			if (sortOrder.dialogType == DIALOG_TYPE.WEAPON || sortOrder.dialogType == DIALOG_TYPE.ARMOR || sortOrder.dialogType == DIALOG_TYPE.TYPE_FILTERABLE_WEAPON || sortOrder.dialogType == DIALOG_TYPE.TYPE_FILTERABLE_ARMOR)
			{
				int num = 0;
				if (sortOrder.dialogType == DIALOG_TYPE.TYPE_FILTERABLE_WEAPON || sortOrder.dialogType == DIALOG_TYPE.TYPE_FILTERABLE_ARMOR)
				{
					int i = 0;
					for (int num2 = equipChangeSortBaseFilterButton.Length; i < num2; i++)
					{
						bool value = (sortOrder.equipFilter & (1 << i)) != 0;
						SetEvent(equipChangeSortBaseFilterButton[i], "EQUIP_FILTER", i);
						SetToggle(GetCtrl(equipChangeSortBaseFilterButton[i]).parent, value);
					}
					num = 95;
					SetActive(UI.OBJ_TYPE, is_visible: true);
				}
				int num3 = num / 2;
				UIWidget component2 = GetComponent<UIWidget>(UI.Frame);
				if (component2 != null)
				{
					component2.height = 703 + num3;
					component2.transform.localPosition = new Vector3(0f, 349f + (float)num3, 0f);
					component2.UpdateAnchors();
				}
			}
			else
			{
				UIWidget component3 = GetComponent<UIWidget>(UI.OBJ_FRAME);
				if (component3 != null)
				{
					component3.height = 770;
					component3.transform.localPosition = new Vector3(0f, 385f, 0f);
					component3.UpdateAnchors();
				}
			}
		}
		int j = 0;
		for (int num4 = rarityButton.Length; j < num4; j++)
		{
			bool value2 = (sortOrder.rarity & (1 << j)) != 0;
			SetEvent(rarityButton[j], "RARITY", j);
			SetToggle(GetCtrl(rarityButton[j]).parent, value2);
		}
		UI?[] array = typeButton;
		UI?[] array2 = null;
		UI?[] array3 = elementButton;
		string event_name = "TYPE";
		int num5;
		int num6;
		switch (sortOrder.dialogType)
		{
		case DIALOG_TYPE.STORAGE_EQUIP:
			num5 = 511;
			num6 = 8604;
			break;
		case DIALOG_TYPE.WEAPON:
		case DIALOG_TYPE.TYPE_FILTERABLE_WEAPON:
			num5 = 31;
			num6 = 25020;
			array = null;
			break;
		default:
			num5 = 0;
			num6 = 41436;
			array = null;
			break;
		case DIALOG_TYPE.SKILL:
		case DIALOG_TYPE.STORAGE_SKILL:
			num5 = ((!(MonoBehaviourSingleton<GameSceneManager>.I.GetPrevSectionNameFromHistory() == "SmithGrowSkillSelectMaterial")) ? 135 : visible_type_flag_skill);
			num6 = 69884;
			event_name = "SKILL_TYPE";
			if (sortOrder.dialogType == DIALOG_TYPE.STORAGE_SKILL)
			{
				array = typeSkillButton;
				array2 = typeSkillGrayButton;
			}
			else
			{
				array = null;
				array2 = null;
			}
			break;
		}
		if (array != null)
		{
			int k = 0;
			for (int num7 = array.Length; k < num7; k++)
			{
				if (!array[k].HasValue)
				{
					continue;
				}
				if ((num5 & (1 << k)) != 0)
				{
					bool value3 = (sortOrder.type & (1 << k)) != 0;
					SetEvent(array[k], event_name, k);
					SetToggle(GetCtrl(array[k]).parent, value3);
				}
				else if (MonoBehaviourSingleton<GameSceneManager>.I.GetPrevSectionNameFromHistory() == "SmithGrowSkillSelectMaterial")
				{
					if (array2 != null && k < array2.Length && array2[k].HasValue)
					{
						SetActive(array[k], is_visible: false);
						SetActive(array2[k], is_visible: true);
					}
				}
				else
				{
					SetActive(array[k], is_visible: false);
					SetActive(array2[k], is_visible: false);
				}
			}
		}
		if (array3 != null)
		{
			int l = 0;
			for (int num8 = array3.Length; l < num8; l++)
			{
				bool value4 = (sortOrder.element & (1 << l)) != 0;
				SetEvent(array3[l], "ELEMENT", l);
				SetToggle(GetCtrl(array3[l]).parent, value4);
			}
		}
		UI? uI = null;
		int m = 0;
		for (int num9 = requirementButton.Length; m < num9; m++)
		{
			if (requirementButton[m].HasValue)
			{
				int num10 = 1 << m;
				if ((num10 & num6) != 0)
				{
					bool value5 = sortOrder.requirement == (SORT_REQUIREMENT)num10;
					SetEvent(requirementButton[m], "REQUIREMENT", num10);
					SetToggle(requirementButton[m], value5);
					uI = requirementButton[m];
				}
				else
				{
					SetActive(requirementButton[m], is_visible: false);
				}
			}
		}
		if (uI.HasValue)
		{
			GetComponent<UIGrid>(UI.GRD_REQUIREMENT).Reposition();
			GetCtrl(UI.OBJ_HEIGHT_ANCHOR).position = GetCtrl(uI).position;
		}
		int n = 0;
		for (int num11 = ascButton.Length; n < num11; n++)
		{
			bool value6 = false;
			if ((n == 0 && sortOrder.orderTypeAsc) || (n == 1 && !sortOrder.orderTypeAsc))
			{
				value6 = true;
			}
			SetEvent(ascButton[n], "ORDER_TYPE", n);
			SetToggle(ascButton[n], value6);
		}
	}

	protected void OnQuery_RARITY()
	{
		OnQueryEvent_Rarity(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(rarityButton[_index]).parent, _is_enable);
	}

	protected void OnQuery_TYPE()
	{
		OnQueryEvent_Type(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(typeButton[_index]).parent, _is_enable);
	}

	protected void OnQuery_SKILL_TYPE()
	{
		OnQueryEvent_Type(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(typeSkillButton[_index]).parent, _is_enable);
	}

	protected void OnQuery_ELEMENT()
	{
		OnQueryEvent_Element(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(elementButton[_index]).parent, _is_enable);
	}

	protected void OnQuery_EQUIP_FILTER()
	{
		OnQueryEvent_EquipFilter(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(equipChangeSortBaseFilterButton[_index]).parent, _is_enable);
	}
}
