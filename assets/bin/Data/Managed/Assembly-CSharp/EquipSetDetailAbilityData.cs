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

	public unsafe override void UpdateUI()
	{
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Expected O, but got Unknown
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
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
			SetActive((Enum)UI.LBL_DESCRIPTION, true);
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
		if (data != null)
		{
			_003CUpdateUI_003Ec__AnonStorey308 _003CUpdateUI_003Ec__AnonStorey;
			SetGrid(UI.GRD_DATA_LIST, "EquipSetDetailAbilityDataItem", data.Length, false, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
				Transform val = component.get_transform().FindChild("_DRAG_SCROLL_");
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
}
