using System;
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
		GRD_REQUIREMENT
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

	protected int visible_type_flag_skill = 1159;

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Expected O, but got Unknown
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Expected O, but got Unknown
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Expected O, but got Unknown
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		if (sortOrder.dialogType == DIALOG_TYPE.STORAGE_SKILL || sortOrder.dialogType == DIALOG_TYPE.SKILL)
		{
			SetActive((Enum)UI.OBJ_TYPE_SKILL, true);
			SetActive((Enum)UI.OBJ_TYPE_EQUIP, false);
			UIWidget component = base.GetComponent<UIWidget>((Enum)UI.OBJ_FRAME);
			if (component != null)
			{
				component.height = 812;
				component.get_transform().set_localPosition(new Vector3(0f, 408f, 0f));
				component.UpdateAnchors();
			}
		}
		else
		{
			SetActive((Enum)UI.OBJ_TYPE_SKILL, false);
			SetActive((Enum)UI.OBJ_TYPE_EQUIP, true);
			UIWidget component2 = base.GetComponent<UIWidget>((Enum)UI.OBJ_FRAME);
			if (component2 != null)
			{
				component2.height = 752;
				component2.get_transform().set_localPosition(new Vector3(0f, 383f, 0f));
				component2.UpdateAnchors();
			}
		}
		int i = 0;
		for (int num = rarityButton.Length; i < num; i++)
		{
			bool value = (sortOrder.rarity & (1 << i)) != 0;
			SetEvent((Enum)rarityButton[i], "RARITY", i);
			SetToggle(GetCtrl(rarityButton[i]).get_parent(), value);
		}
		UI?[] array = typeButton;
		UI?[] array2 = null;
		UI?[] array3 = elementButton;
		string event_name = "TYPE";
		int num2;
		int num3;
		switch (sortOrder.dialogType)
		{
		case DIALOG_TYPE.STORAGE_EQUIP:
			num2 = 511;
			num3 = 8604;
			break;
		case DIALOG_TYPE.WEAPON:
			num2 = 31;
			num3 = 25020;
			array = null;
			break;
		default:
			num2 = 0;
			num3 = 41436;
			array = null;
			break;
		case DIALOG_TYPE.SKILL:
		case DIALOG_TYPE.STORAGE_SKILL:
			num2 = ((!(MonoBehaviourSingleton<GameSceneManager>.I.GetPrevSectionNameFromHistory() == "SmithGrowSkillSelectMaterial")) ? 135 : visible_type_flag_skill);
			num3 = 69884;
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
			int j = 0;
			for (int num4 = array.Length; j < num4; j++)
			{
				UI? nullable = array[j];
				if (nullable.HasValue)
				{
					if ((num2 & (1 << j)) != 0)
					{
						bool value2 = (sortOrder.type & (1 << j)) != 0;
						SetEvent((Enum)array[j], event_name, j);
						SetToggle(GetCtrl(array[j]).get_parent(), value2);
					}
					else if (MonoBehaviourSingleton<GameSceneManager>.I.GetPrevSectionNameFromHistory() == "SmithGrowSkillSelectMaterial")
					{
						if (array2 != null && j < array2.Length)
						{
							UI? nullable2 = array2[j];
							if (nullable2.HasValue)
							{
								SetActive((Enum)array[j], false);
								SetActive((Enum)array2[j], true);
							}
						}
					}
					else
					{
						SetActive((Enum)array[j], false);
						SetActive((Enum)array2[j], false);
					}
				}
			}
		}
		if (array3 != null)
		{
			int k = 0;
			for (int num5 = array3.Length; k < num5; k++)
			{
				bool value3 = (sortOrder.element & (1 << k)) != 0;
				SetEvent((Enum)array3[k], "ELEMENT", k);
				SetToggle(GetCtrl(array3[k]).get_parent(), value3);
			}
		}
		UI? nullable3 = null;
		int l = 0;
		for (int num6 = requirementButton.Length; l < num6; l++)
		{
			UI? nullable4 = requirementButton[l];
			if (nullable4.HasValue)
			{
				int num7 = 1 << l;
				if ((num7 & num3) != 0)
				{
					bool value4 = sortOrder.requirement == (SORT_REQUIREMENT)num7;
					SetEvent((Enum)requirementButton[l], "REQUIREMENT", num7);
					SetToggle((Enum)requirementButton[l], value4);
					nullable3 = requirementButton[l];
				}
				else
				{
					SetActive((Enum)requirementButton[l], false);
				}
			}
		}
		if (nullable3.HasValue)
		{
			base.GetComponent<UIGrid>((Enum)UI.GRD_REQUIREMENT).Reposition();
			GetCtrl(UI.OBJ_HEIGHT_ANCHOR).set_position(GetCtrl(nullable3).get_position());
		}
		int m = 0;
		for (int num8 = ascButton.Length; m < num8; m++)
		{
			bool value5 = false;
			if ((m == 0 && sortOrder.orderTypeAsc) || (m == 1 && !sortOrder.orderTypeAsc))
			{
				value5 = true;
			}
			SetEvent((Enum)ascButton[m], "ORDER_TYPE", m);
			SetToggle((Enum)ascButton[m], value5);
		}
	}

	protected void OnQuery_RARITY()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		OnQueryEvent_Rarity(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(rarityButton[_index]).get_parent(), _is_enable);
	}

	protected void OnQuery_TYPE()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		OnQueryEvent_Type(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(typeButton[_index]).get_parent(), _is_enable);
	}

	protected void OnQuery_SKILL_TYPE()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		OnQueryEvent_Type(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(typeSkillButton[_index]).get_parent(), _is_enable);
	}

	protected void OnQuery_ELEMENT()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		OnQueryEvent_Element(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(elementButton[_index]).get_parent(), _is_enable);
	}
}
