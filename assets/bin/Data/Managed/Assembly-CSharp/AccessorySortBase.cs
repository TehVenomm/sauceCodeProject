using UnityEngine;

public class AccessorySortBase : SortBase
{
	private enum UI
	{
		BTN_N,
		BTN_HN,
		BTN_R,
		BTN_HR,
		BTN_SR,
		BTN_HSR,
		BTN_SSR,
		BTN_GET,
		BTN_RARITY,
		BTN_SELL,
		BTN_ASC,
		BTN_DESC
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

	private static readonly UI?[] requirementButton = new UI?[4]
	{
		null,
		null,
		UI.BTN_GET,
		UI.BTN_RARITY
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
		int i = 0;
		for (int num = rarityButton.Length; i < num; i++)
		{
			bool value = (sortOrder.rarity & (1 << i)) != 0;
			SetEvent(rarityButton[i], "RARITY", i);
			Transform ctrl = GetCtrl(rarityButton[i]);
			if (ctrl != null)
			{
				SetToggle(ctrl.parent, value);
			}
		}
		int j = 0;
		for (int num2 = requirementButton.Length; j < num2; j++)
		{
			if (requirementButton[j].HasValue)
			{
				int num3 = 1 << j;
				bool value2 = sortOrder.requirement == (SORT_REQUIREMENT)num3;
				SetEvent(requirementButton[j], "REQUIREMENT", num3);
				SetToggle(requirementButton[j], value2);
			}
		}
		int k = 0;
		for (int num4 = ascButton.Length; k < num4; k++)
		{
			bool value3 = (k == 0 && sortOrder.orderTypeAsc) || (k == 1 && !sortOrder.orderTypeAsc);
			SetEvent(ascButton[k], "ORDER_TYPE", k);
			SetToggle(ascButton[k], value3);
		}
	}

	private void OnQuery_RARITY()
	{
		OnQueryEvent_Rarity(out int _index, out bool _is_enable);
		SetToggle(GetCtrl(rarityButton[_index]).parent, _is_enable);
	}
}
