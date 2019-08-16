using System;

public class SmithSelectEquipSort : SortBase
{
	protected enum UI
	{
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

	private UI?[] requirementButton = new UI?[16]
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
		null,
		null
	};

	private UI[] ascButton = new UI[2]
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
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		int num;
		switch (sortOrder.dialogType)
		{
		case DIALOG_TYPE.WEAPON:
			num = 25020;
			break;
		default:
			num = 41436;
			break;
		}
		UI? uI = null;
		int i = 0;
		for (int num2 = requirementButton.Length; i < num2; i++)
		{
			UI? uI2 = requirementButton[i];
			if (uI2.HasValue)
			{
				int num3 = 1 << i;
				if ((num3 & num) != 0)
				{
					bool value = sortOrder.requirement == (SORT_REQUIREMENT)num3;
					SetEvent((Enum)(object)requirementButton[i], "REQUIREMENT", num3);
					SetToggle((Enum)(object)requirementButton[i], value);
					uI = requirementButton[i];
				}
				else
				{
					SetActive((Enum)(object)requirementButton[i], is_visible: false);
				}
			}
		}
		if (uI.HasValue)
		{
			base.GetComponent<UIGrid>((Enum)UI.GRD_REQUIREMENT).Reposition();
			GetCtrl(UI.OBJ_HEIGHT_ANCHOR).set_position(GetCtrl((Enum)(object)uI).get_position());
		}
		int j = 0;
		for (int num4 = ascButton.Length; j < num4; j++)
		{
			bool value2 = false;
			if ((j == 0 && sortOrder.orderTypeAsc) || (j == 1 && !sortOrder.orderTypeAsc))
			{
				value2 = true;
			}
			SetEvent((Enum)ascButton[j], "ORDER_TYPE", j);
			SetToggle((Enum)ascButton[j], value2);
		}
	}
}
