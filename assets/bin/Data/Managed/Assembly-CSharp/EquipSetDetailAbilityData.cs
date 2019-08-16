using System;
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
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		SetFullScreenButton((Enum)UI.BTN_OK);
		SetFontStyle((Enum)UI.STR_TITLE, 2);
		SetFontStyle((Enum)UI.STR_NEED_POINT, 2);
		AbilityDataTable.AbilityData table = Singleton<AbilityDataTable>.I.GetAbilityData(ability.id, ability.ap);
		AbilityDataTable.AbilityData minimumAbilityData = Singleton<AbilityDataTable>.I.GetMinimumAbilityData(ability.id);
		if (ability.ap == -1)
		{
			SetLabelText((Enum)UI.LBL_DATA_NAME, ability.GetName());
			SetActive((Enum)UI.LBL_DESCRIPTION, minimumAbilityData != null);
			SetLabelText((Enum)UI.LBL_DESCRIPTION, minimumAbilityData.description);
		}
		else if (table != null)
		{
			SetLabelText((Enum)UI.LBL_DATA_NAME, table.name);
			SetActive((Enum)UI.LBL_DESCRIPTION, is_visible: true);
			SetLabelText((Enum)UI.LBL_DESCRIPTION, table.description);
		}
		else
		{
			SetLabelText((Enum)UI.LBL_DATA_NAME, base.sectionData.GetText("NON_DATA"));
			SetActive((Enum)UI.LBL_DESCRIPTION, minimumAbilityData != null);
			SetLabelText((Enum)UI.LBL_DESCRIPTION, minimumAbilityData.description);
		}
		AbilityDataTable.AbilityData[] data = Singleton<AbilityDataTable>.I.GetAbilityDataArray(ability.id);
		SetActive((Enum)UI.GRD_DATA_LIST, data != null);
		if (data == null)
		{
			return;
		}
		SetGrid(UI.GRD_DATA_LIST, "EquipSetDetailAbilityDataItem", data.Length, reset: false, delegate(int i, Transform t, bool is_recycle)
		{
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
			AbilityDataTable.AbilityData abilityData = data[i];
			string text = string.Format(((int)abilityData.needAP < 0) ? "{0}" : "+{0}", abilityData.needAP);
			SetLabelText(t, UI.LBL_ABILITY_DATA_NAME, abilityData.name);
			SetLabelText(t, UI.LBL_AP, text);
			bool flag = table != null && table.needAP == abilityData.needAP;
			SetActive(t, UI.SPR_ABILITY_ON, flag);
			Color color = (ability.ap != -1) ? Color.get_gray() : Color.get_white();
			if (flag)
			{
				centeringIndex = i;
				color = Color.get_white();
			}
			SetColor(t, UI.LBL_ABILITY_DATA_NAME, color);
			SetColor(t, UI.LBL_AP, color);
		});
		UIScrollView component = base.GetComponent<UIScrollView>((Enum)UI.SCR_DATA_LIST);
		if (component != null && component.get_enabled())
		{
			UIGrid component2 = base.GetComponent<UIGrid>((Enum)UI.GRD_DATA_LIST);
			Vector2 viewSize = component.panel.GetViewSize();
			float y = viewSize.y;
			float cellHeight = component2.cellHeight;
			int num = Mathf.RoundToInt(y / cellHeight);
			int num2 = num / 2;
			int num3 = -1;
			if (centeringIndex > num2)
			{
				num3 = centeringIndex - num2;
			}
			Transform val = component.get_transform().Find("_DRAG_SCROLL_");
			Vector3 position = component2.get_transform().get_position();
			component.ResetPosition();
			component.MoveRelative(new Vector3(0f, cellHeight * (float)num3));
			Vector3 position2 = val.get_transform().get_position();
			Vector3 position3 = component2.get_transform().get_position();
			Vector3 val2 = position3 - position;
			val.get_transform().set_position(position2 - val2);
		}
	}
}
