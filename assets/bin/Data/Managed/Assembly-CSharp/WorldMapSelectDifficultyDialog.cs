using System;

public class WorldMapSelectDifficultyDialog : GameSection
{
	protected enum UI
	{
		BTN_HARD,
		BTN_HARD_GRAY,
		STR_NORMAL,
		STR_HARD,
		STR_HARD_LV
	}

	private bool isRegion;

	private bool isOpenedHard;

	private bool enableHardLevel;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		isRegion = (bool)array[0];
		isOpenedHard = (bool)array[1];
		enableHardLevel = ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 150);
		bool flag = enableHardLevel;
		if (flag && isRegion)
		{
			flag = isOpenedHard;
		}
		SetActive((Enum)UI.BTN_HARD, flag);
		SetActive((Enum)UI.BTN_HARD_GRAY, !flag);
		SetText();
		base.Initialize();
	}

	private void SetText()
	{
		SetLabelText((Enum)UI.STR_NORMAL, base.sectionData.GetText("NORMAL"));
		SetLabelText((Enum)UI.STR_HARD, base.sectionData.GetText("HARD"));
		string text = base.sectionData.GetText("HARD_LV");
		text = string.Format(text, 150);
		UILabel component = GetCtrl(UI.STR_HARD_LV).GetComponent<UILabel>();
		component.text = text;
		GetCtrl(UI.STR_HARD_LV).GetComponent<UILabel>().supportEncoding = true;
		SetLabelText((Enum)UI.STR_HARD_LV, text);
	}

	private void OnQuery_HARD()
	{
		GameSection.SetEventData(REGION_DIFFICULTY_TYPE.HARD);
	}

	private void OnQuery_NORMAL()
	{
		GameSection.SetEventData(REGION_DIFFICULTY_TYPE.NORMAL);
	}

	private void OnQuery_HARD_GRAY()
	{
		if (!isRegion || !enableHardLevel)
		{
			GameSection.SetEventData(new object[1]
			{
				150.ToString()
			});
		}
		else
		{
			GameSection.ChangeEvent("HARD_NOT_OPEN");
		}
	}
}
