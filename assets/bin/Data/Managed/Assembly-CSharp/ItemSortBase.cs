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
		if (sortOrder.dialogType == DIALOG_TYPE.MATERIAL)
		{
			SetActive(UI.MATERIAL_ROOT, is_visible: true);
			SetActive(UI.RARITY_ROOT, is_visible: false);
			SetActive(UI.EQUIP_FILTER_ROOT, is_visible: false);
			SetActive(UI.ELEMENT_ROOT, is_visible: true);
			int i = 0;
			for (int num = materialButton.Length; i < num; i++)
			{
				bool value = (sortOrder.type & (1 << i)) != 0;
				SetEvent(materialButton[i], "MATERIAL", i);
				SetToggle(GetCtrl(materialButton[i]).parent, value);
			}
			int j = 0;
			for (int num2 = elementButton.Length; j < num2; j++)
			{
				bool value2 = (sortOrder.element & (1 << j)) != 0;
				SetEvent(elementButton[j], "ELEMENT", j);
				SetToggle(GetCtrl(elementButton[j]).parent, value2);
			}
			GameObject.Find("ItemSortFrame").gameObject.GetComponent<UIWidget>().height = 633;
			GetCtrl(UI.ELEMENT_ROOT).localPosition = new Vector3(0f, -153f, 0f);
			GameObject.Find("sort").gameObject.transform.localPosition = new Vector3(0f, -322f, 0f);
		}
		else if (sortOrder.dialogType == DIALOG_TYPE.SMITH_CREATE_WEAPON || sortOrder.dialogType == DIALOG_TYPE.SMITH_CREATE_ARMOR)
		{
			SetActive(UI.MATERIAL_ROOT, is_visible: false);
			SetActive(UI.EQUIP_FILTER_ROOT, is_visible: true);
			SetActive(UI.RARITY_ROOT, is_visible: true);
			SetActive(UI.ELEMENT_ROOT, is_visible: true);
			int k = 0;
			for (int num3 = rarityButton.Length; k < num3; k++)
			{
				bool value3 = (sortOrder.rarity & (1 << k)) != 0;
				SetEvent(rarityButton[k], "RARITY", k);
				SetToggle(GetCtrl(rarityButton[k]).parent, value3);
			}
			int l = 0;
			for (int num4 = equipFilterButton.Length; l < num4; l++)
			{
				bool value4 = (sortOrder.equipFilter & (1 << l)) != 0;
				SetEvent(equipFilterButton[l], "EQUIPFILTER", l);
				SetToggle(GetCtrl(equipFilterButton[l]).parent, value4);
			}
			int m = 0;
			for (int num5 = elementButton.Length; m < num5; m++)
			{
				bool value5 = (sortOrder.element & (1 << m)) != 0;
				SetEvent(elementButton[m], "ELEMENT", m);
				SetToggle(GetCtrl(elementButton[m]).parent, value5);
			}
			UIWidget component = GameObject.Find("ItemSortFrame").gameObject.GetComponent<UIWidget>();
			Transform transform = component.transform;
			Vector3 vector2 = transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 40f, transform.localPosition.z);
			component.height = 750;
			Transform transform2 = GameObject.Find("sort").gameObject.transform;
			Vector3 vector4 = transform2.localPosition = new Vector3(transform2.localPosition.x, transform2.localPosition.y - 240f, transform2.localPosition.z);
		}
		else if (sortOrder.dialogType == DIALOG_TYPE.ABILITY_ITEM)
		{
			SetActive(UI.MATERIAL_ROOT, is_visible: false);
			SetActive(UI.EQUIP_FILTER_ROOT, is_visible: false);
			SetActive(UI.RARITY_ROOT, is_visible: true);
			SetActive(UI.ELEMENT_ROOT, is_visible: true);
			int n = 0;
			for (int num6 = rarityButton.Length; n < num6; n++)
			{
				bool value6 = (sortOrder.rarity & (1 << n)) != 0;
				SetEvent(rarityButton[n], "RARITY", n);
				SetToggle(GetCtrl(rarityButton[n]).parent, value6);
			}
			int num7 = 0;
			for (int num8 = elementButton.Length; num7 < num8; num7++)
			{
				bool value7 = (sortOrder.element & (1 << num7)) != 0;
				SetEvent(elementButton[num7], "ELEMENT", num7);
				SetToggle(GetCtrl(elementButton[num7]).parent, value7);
			}
			GameObject.Find("ItemSortFrame").gameObject.GetComponent<UIWidget>().height = 583;
			GetCtrl(UI.ELEMENT_ROOT).localPosition = new Vector3(0f, -98f, 0f);
			GameObject.Find("sort").gameObject.transform.localPosition = new Vector3(0f, -264f, 0f);
		}
		else
		{
			SetActive(UI.MATERIAL_ROOT, is_visible: false);
			SetActive(UI.EQUIP_FILTER_ROOT, is_visible: false);
			SetActive(UI.RARITY_ROOT, is_visible: true);
			SetActive(UI.ELEMENT_ROOT, is_visible: false);
			int num9 = 0;
			for (int num10 = rarityButton.Length; num9 < num10; num9++)
			{
				bool value8 = (sortOrder.rarity & (1 << num9)) != 0;
				SetEvent(rarityButton[num9], "RARITY", num9);
				SetToggle(GetCtrl(rarityButton[num9]).parent, value8);
			}
		}
		int num11;
		switch (sortOrder.dialogType)
		{
		default:
			num11 = 138;
			break;
		case DIALOG_TYPE.STORAGE_EQUIP:
		case DIALOG_TYPE.STORAGE_SKILL:
			num11 = 8604;
			break;
		case DIALOG_TYPE.SMITH_CREATE_WEAPON:
			num11 = 8488;
			break;
		case DIALOG_TYPE.SMITH_CREATE_ARMOR:
			num11 = 8520;
			break;
		case DIALOG_TYPE.SMITH_CREATE_PICKUP_WEAPON:
			num11 = 8489;
			break;
		case DIALOG_TYPE.SMITH_CREATE_PICKUP_ARMOR:
			num11 = 8521;
			break;
		}
		UI? uI = null;
		int num12 = 0;
		for (int num13 = requirementButton.Length; num12 < num13; num12++)
		{
			if (requirementButton[num12].HasValue)
			{
				int num14 = 1 << num12;
				if ((num14 & num11) != 0)
				{
					bool value9 = sortOrder.requirement == (SORT_REQUIREMENT)num14;
					SetEvent(requirementButton[num12], "REQUIREMENT", num14);
					SetToggle(requirementButton[num12], value9);
					uI = requirementButton[num12];
				}
				else
				{
					SetActive(requirementButton[num12], is_visible: false);
				}
			}
		}
		if (uI.HasValue)
		{
			GetComponent<UIGrid>(UI.GRD_REQUIREMENT).Reposition();
			GetCtrl(UI.OBJ_HEIGHT_ANCHOR).position = GetCtrl(uI).position;
		}
		int num15 = 0;
		for (int num16 = ascButton.Length; num15 < num16; num15++)
		{
			bool value10 = false;
			if ((num15 == 0 && sortOrder.orderTypeAsc) || (num15 == 1 && !sortOrder.orderTypeAsc))
			{
				value10 = true;
			}
			SetEvent(ascButton[num15], "ORDER_TYPE", num15);
			SetToggle(ascButton[num15], value10);
		}
	}

	private void OnQuery_RARITY()
	{
		OnQueryEvent_Rarity(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(rarityButton[_index]).parent, _is_enable);
	}

	private void OnQuery_MATERIAL()
	{
		OnQueryEvent_Type(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(materialButton[_index]).parent, _is_enable);
	}

	private void OnQuery_EQUIPFILTER()
	{
		OnQueryEvent_EquipFilter(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(equipFilterButton[_index]).parent, _is_enable);
	}

	private void OnQuery_ELEMENT()
	{
		OnQueryEvent_Element(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(elementButton[_index]).parent, _is_enable);
	}
}
