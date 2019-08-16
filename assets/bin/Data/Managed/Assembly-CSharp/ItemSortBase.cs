using System;
using UnityEngine;

public class ItemSortBase : SortBase
{
	private enum UI
	{
		MATERIAL_ROOT,
		RARITY_ROOT,
		EQUIP_FILTER_ROOT,
		ELEMENT_ROOT,
		BTN_N,
		BTN_HN,
		BTN_R,
		BTN_HR,
		BTN_SR,
		BTN_HSR,
		BTN_SSR,
		BTN_COMMON,
		BTN_UNIQUE,
		BTN_LITHOGRAPH,
		BTN_EQUIP,
		BTN_METAL,
		BTN_EQUIP_PAY,
		BTN_EQUIP_NO_PAY,
		BTN_EQUIP_CREATABLE,
		BTN_EQUIP_NO_CREATABLE,
		BTN_EQUIP_OBTAINED,
		BTN_EQUIP_NO_OBTAINED,
		BTN_FIRE,
		BTN_WATER,
		BTN_THUNDER,
		BTN_SOIL,
		BTN_LIGHT,
		BTN_DARK,
		BTN_NO_ELEMENT,
		BTN_ID,
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
		BTN_ASC,
		BTN_DESC,
		OBJ_HEIGHT_ANCHOR,
		GRD_REQUIREMENT
	}

	private static readonly UI[] rarityButton = new UI[7]
	{
		UI.BTN_N,
		UI.BTN_HN,
		UI.BTN_R,
		UI.BTN_HR,
		UI.BTN_SR,
		UI.BTN_HSR,
		UI.BTN_SSR
	};

	private static readonly UI[] materialButton = new UI[5]
	{
		UI.BTN_COMMON,
		UI.BTN_UNIQUE,
		UI.BTN_LITHOGRAPH,
		UI.BTN_EQUIP,
		UI.BTN_METAL
	};

	private static readonly UI[] equipFilterButton = new UI[6]
	{
		UI.BTN_EQUIP_PAY,
		UI.BTN_EQUIP_NO_PAY,
		UI.BTN_EQUIP_CREATABLE,
		UI.BTN_EQUIP_NO_CREATABLE,
		UI.BTN_EQUIP_OBTAINED,
		UI.BTN_EQUIP_NO_OBTAINED
	};

	private static readonly UI?[] requirementButton = new UI?[14]
	{
		UI.BTN_ID,
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
		null,
		UI.BTN_ELEMENT
	};

	private static readonly UI[] elementButton = new UI[7]
	{
		UI.BTN_FIRE,
		UI.BTN_WATER,
		UI.BTN_THUNDER,
		UI.BTN_SOIL,
		UI.BTN_LIGHT,
		UI.BTN_DARK,
		UI.BTN_NO_ELEMENT
	};

	private static readonly UI[] ascButton = new UI[2]
	{
		UI.BTN_ASC,
		UI.BTN_DESC
	};

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		if (sortOrder.dialogType == DIALOG_TYPE.MATERIAL)
		{
			SetActive((Enum)UI.MATERIAL_ROOT, is_visible: true);
			SetActive((Enum)UI.RARITY_ROOT, is_visible: false);
			SetActive((Enum)UI.EQUIP_FILTER_ROOT, is_visible: false);
			SetActive((Enum)UI.ELEMENT_ROOT, is_visible: true);
			int i = 0;
			for (int num = materialButton.Length; i < num; i++)
			{
				bool value = (sortOrder.type & (1 << i)) != 0;
				SetEvent((Enum)materialButton[i], "MATERIAL", i);
				SetToggle(GetCtrl(materialButton[i]).get_parent(), value);
			}
			int j = 0;
			for (int num2 = elementButton.Length; j < num2; j++)
			{
				bool value2 = (sortOrder.element & (1 << j)) != 0;
				SetEvent((Enum)elementButton[j], "ELEMENT", j);
				SetToggle(GetCtrl(elementButton[j]).get_parent(), value2);
			}
			UIWidget component = GameObject.Find("ItemSortFrame").get_gameObject().GetComponent<UIWidget>();
			component.height = 633;
			GetCtrl(UI.ELEMENT_ROOT).set_localPosition(new Vector3(0f, -153f, 0f));
			Transform transform = GameObject.Find("sort").get_gameObject().get_transform();
			transform.set_localPosition(new Vector3(0f, -322f, 0f));
		}
		else if (sortOrder.dialogType == DIALOG_TYPE.SMITH_CREATE_WEAPON || sortOrder.dialogType == DIALOG_TYPE.SMITH_CREATE_ARMOR)
		{
			SetActive((Enum)UI.MATERIAL_ROOT, is_visible: false);
			SetActive((Enum)UI.EQUIP_FILTER_ROOT, is_visible: true);
			SetActive((Enum)UI.RARITY_ROOT, is_visible: true);
			SetActive((Enum)UI.ELEMENT_ROOT, is_visible: true);
			int k = 0;
			for (int num3 = rarityButton.Length; k < num3; k++)
			{
				bool value3 = (sortOrder.rarity & (1 << k)) != 0;
				SetEvent((Enum)rarityButton[k], "RARITY", k);
				SetToggle(GetCtrl(rarityButton[k]).get_parent(), value3);
			}
			int l = 0;
			for (int num4 = equipFilterButton.Length; l < num4; l++)
			{
				bool value4 = (sortOrder.equipFilter & (1 << l)) != 0;
				SetEvent((Enum)equipFilterButton[l], "EQUIPFILTER", l);
				SetToggle(GetCtrl(equipFilterButton[l]).get_parent(), value4);
			}
			int m = 0;
			for (int num5 = elementButton.Length; m < num5; m++)
			{
				bool value5 = (sortOrder.element & (1 << m)) != 0;
				SetEvent((Enum)elementButton[m], "ELEMENT", m);
				SetToggle(GetCtrl(elementButton[m]).get_parent(), value5);
			}
			UIWidget component2 = GameObject.Find("ItemSortFrame").get_gameObject().GetComponent<UIWidget>();
			Transform transform2 = component2.get_transform();
			Vector3 localPosition = transform2.get_localPosition();
			float x = localPosition.x;
			Vector3 localPosition2 = transform2.get_localPosition();
			float num6 = localPosition2.y + 40f;
			Vector3 localPosition3 = transform2.get_localPosition();
			Vector3 localPosition4 = default(Vector3);
			localPosition4._002Ector(x, num6, localPosition3.z);
			transform2.set_localPosition(localPosition4);
			component2.height = 750;
			Transform transform3 = GameObject.Find("sort").get_gameObject().get_transform();
			Vector3 localPosition5 = transform3.get_localPosition();
			float x2 = localPosition5.x;
			Vector3 localPosition6 = transform3.get_localPosition();
			float num7 = localPosition6.y - 240f;
			Vector3 localPosition7 = transform3.get_localPosition();
			Vector3 localPosition8 = default(Vector3);
			localPosition8._002Ector(x2, num7, localPosition7.z);
			transform3.set_localPosition(localPosition8);
		}
		else if (sortOrder.dialogType == DIALOG_TYPE.ABILITY_ITEM)
		{
			SetActive((Enum)UI.MATERIAL_ROOT, is_visible: false);
			SetActive((Enum)UI.EQUIP_FILTER_ROOT, is_visible: false);
			SetActive((Enum)UI.RARITY_ROOT, is_visible: true);
			SetActive((Enum)UI.ELEMENT_ROOT, is_visible: true);
			int n = 0;
			for (int num8 = rarityButton.Length; n < num8; n++)
			{
				bool value6 = (sortOrder.rarity & (1 << n)) != 0;
				SetEvent((Enum)rarityButton[n], "RARITY", n);
				SetToggle(GetCtrl(rarityButton[n]).get_parent(), value6);
			}
			int num9 = 0;
			for (int num10 = elementButton.Length; num9 < num10; num9++)
			{
				bool value7 = (sortOrder.element & (1 << num9)) != 0;
				SetEvent((Enum)elementButton[num9], "ELEMENT", num9);
				SetToggle(GetCtrl(elementButton[num9]).get_parent(), value7);
			}
			UIWidget component3 = GameObject.Find("ItemSortFrame").get_gameObject().GetComponent<UIWidget>();
			component3.height = 583;
			GetCtrl(UI.ELEMENT_ROOT).set_localPosition(new Vector3(0f, -98f, 0f));
			Transform transform4 = GameObject.Find("sort").get_gameObject().get_transform();
			transform4.set_localPosition(new Vector3(0f, -264f, 0f));
		}
		else
		{
			SetActive((Enum)UI.MATERIAL_ROOT, is_visible: false);
			SetActive((Enum)UI.EQUIP_FILTER_ROOT, is_visible: false);
			SetActive((Enum)UI.RARITY_ROOT, is_visible: true);
			SetActive((Enum)UI.ELEMENT_ROOT, is_visible: false);
			int num11 = 0;
			for (int num12 = rarityButton.Length; num11 < num12; num11++)
			{
				bool value8 = (sortOrder.rarity & (1 << num11)) != 0;
				SetEvent((Enum)rarityButton[num11], "RARITY", num11);
				SetToggle(GetCtrl(rarityButton[num11]).get_parent(), value8);
			}
		}
		int num13;
		switch (sortOrder.dialogType)
		{
		default:
			num13 = 138;
			break;
		case DIALOG_TYPE.STORAGE_EQUIP:
		case DIALOG_TYPE.STORAGE_SKILL:
			num13 = 8604;
			break;
		case DIALOG_TYPE.SMITH_CREATE_WEAPON:
			num13 = 8488;
			break;
		case DIALOG_TYPE.SMITH_CREATE_ARMOR:
			num13 = 8520;
			break;
		case DIALOG_TYPE.SMITH_CREATE_PICKUP_WEAPON:
			num13 = 8489;
			break;
		case DIALOG_TYPE.SMITH_CREATE_PICKUP_ARMOR:
			num13 = 8521;
			break;
		}
		UI? uI = null;
		int num14 = 0;
		for (int num15 = requirementButton.Length; num14 < num15; num14++)
		{
			UI? uI2 = requirementButton[num14];
			if (uI2.HasValue)
			{
				int num16 = 1 << num14;
				if ((num16 & num13) != 0)
				{
					bool value9 = sortOrder.requirement == (SORT_REQUIREMENT)num16;
					SetEvent((Enum)(object)requirementButton[num14], "REQUIREMENT", num16);
					SetToggle((Enum)(object)requirementButton[num14], value9);
					uI = requirementButton[num14];
				}
				else
				{
					SetActive((Enum)(object)requirementButton[num14], is_visible: false);
				}
			}
		}
		if (uI.HasValue)
		{
			base.GetComponent<UIGrid>((Enum)UI.GRD_REQUIREMENT).Reposition();
			GetCtrl(UI.OBJ_HEIGHT_ANCHOR).set_position(GetCtrl((Enum)(object)uI).get_position());
		}
		int num17 = 0;
		for (int num18 = ascButton.Length; num17 < num18; num17++)
		{
			bool value10 = false;
			if ((num17 == 0 && sortOrder.orderTypeAsc) || (num17 == 1 && !sortOrder.orderTypeAsc))
			{
				value10 = true;
			}
			SetEvent((Enum)ascButton[num17], "ORDER_TYPE", num17);
			SetToggle((Enum)ascButton[num17], value10);
		}
	}

	private void OnQuery_RARITY()
	{
		OnQueryEvent_Rarity(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(rarityButton[_index]).get_parent(), _is_enable);
	}

	private void OnQuery_MATERIAL()
	{
		OnQueryEvent_Type(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(materialButton[_index]).get_parent(), _is_enable);
	}

	private void OnQuery_EQUIPFILTER()
	{
		OnQueryEvent_EquipFilter(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(equipFilterButton[_index]).get_parent(), _is_enable);
	}

	private void OnQuery_ELEMENT()
	{
		OnQueryEvent_Element(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(elementButton[_index]).get_parent(), _is_enable);
	}
}
