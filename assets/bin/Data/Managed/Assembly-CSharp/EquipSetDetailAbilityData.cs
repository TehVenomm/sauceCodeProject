using UnityEngine;

public class EquipSetDetailAbilityData : GameSection
{
	private enum UI
	{
		LBL_DATA_NAME,
		LBL_DESCRIPTION,
		BTN_OK,
		GRD_DATA_LIST,
		SCR_DATA_LIST,
		STR_TITLE,
		STR_NEED_POINT,
		LBL_ABILITY_DATA_NAME,
		LBL_AP,
		SPR_ABILITY_ON
	}

	private EquipItemAbility ability;

	private int centeringIndex = -1;

	public override void Initialize()
	{
		ability = (GameSection.GetEventData() as EquipItemAbility);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetFullScreenButton(UI.BTN_OK);
		SetFontStyle(UI.STR_TITLE, FontStyle.Italic);
		SetFontStyle(UI.STR_NEED_POINT, FontStyle.Italic);
		AbilityDataTable.AbilityData table = Singleton<AbilityDataTable>.I.GetAbilityData(ability.id, ability.ap);
		AbilityDataTable.AbilityData minimumAbilityData = Singleton<AbilityDataTable>.I.GetMinimumAbilityData(ability.id);
		if (ability.ap == -1)
		{
			SetLabelText(UI.LBL_DATA_NAME, ability.GetName());
			SetActive(UI.LBL_DESCRIPTION, minimumAbilityData != null);
			SetLabelText(UI.LBL_DESCRIPTION, minimumAbilityData.description);
		}
		else if (table != null)
		{
			SetLabelText(UI.LBL_DATA_NAME, table.name);
			SetActive(UI.LBL_DESCRIPTION, is_visible: true);
			SetLabelText(UI.LBL_DESCRIPTION, table.description);
		}
		else
		{
			SetLabelText(UI.LBL_DATA_NAME, base.sectionData.GetText("NON_DATA"));
			SetActive(UI.LBL_DESCRIPTION, minimumAbilityData != null);
			SetLabelText(UI.LBL_DESCRIPTION, minimumAbilityData.description);
		}
		AbilityDataTable.AbilityData[] data = Singleton<AbilityDataTable>.I.GetAbilityDataArray(ability.id);
		SetActive(UI.GRD_DATA_LIST, data != null);
		if (data == null)
		{
			return;
		}
		SetGrid(UI.GRD_DATA_LIST, "EquipSetDetailAbilityDataItem", data.Length, reset: false, delegate(int i, Transform t, bool is_recycle)
		{
			AbilityDataTable.AbilityData abilityData = data[i];
			string text = string.Format(((int)abilityData.needAP >= 0) ? "+{0}" : "{0}", abilityData.needAP);
			SetLabelText(t, UI.LBL_ABILITY_DATA_NAME, abilityData.name);
			SetLabelText(t, UI.LBL_AP, text);
			bool flag = table != null && table.needAP == abilityData.needAP;
			SetActive(t, UI.SPR_ABILITY_ON, flag);
			Color color = (ability.ap == -1) ? Color.white : Color.gray;
			if (flag)
			{
				centeringIndex = i;
				color = Color.white;
			}
			SetColor(t, UI.LBL_ABILITY_DATA_NAME, color);
			SetColor(t, UI.LBL_AP, color);
		});
		UIScrollView component = GetComponent<UIScrollView>(UI.SCR_DATA_LIST);
		if (component != null && component.enabled)
		{
			UIGrid component2 = GetComponent<UIGrid>(UI.GRD_DATA_LIST);
			float y = component.panel.GetViewSize().y;
			float cellHeight = component2.cellHeight;
			int num = Mathf.RoundToInt(y / cellHeight) / 2;
			int num2 = -1;
			if (centeringIndex > num)
			{
				num2 = centeringIndex - num;
			}
			Transform transform = component.transform.Find("_DRAG_SCROLL_");
			Vector3 position = component2.transform.position;
			component.ResetPosition();
			component.MoveRelative(new Vector3(0f, cellHeight * (float)num2));
			Vector3 position2 = transform.transform.position;
			Vector3 b = component2.transform.position - position;
			transform.transform.position = position2 - b;
		}
	}
}
