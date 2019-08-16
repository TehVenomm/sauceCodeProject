using System;

public class QuestAcceptRoomSettingsLevel : QuestAcceptEntryPassRoom
{
	protected new enum UI
	{
		LBL_INPUT_PASS_1,
		LBL_INPUT_PASS_2,
		LBL_INPUT_PASS_3,
		LBL_INPUT_PASS_4,
		LBL_INPUT_PASS_5,
		STR_NON_SETTINGS
	}

	private const int LEVEL_MAX_DIGIT = 3;

	private UI[] lblAry = new UI[5]
	{
		UI.LBL_INPUT_PASS_1,
		UI.LBL_INPUT_PASS_2,
		UI.LBL_INPUT_PASS_3,
		UI.LBL_INPUT_PASS_4,
		UI.LBL_INPUT_PASS_5
	};

	public override void Initialize()
	{
		int i = 0;
		for (int num = passCode.Length; i < num; i++)
		{
			passCode[i] = string.Empty;
		}
		if (MonoBehaviourSingleton<PartyManager>.I.partySetting != null && MonoBehaviourSingleton<PartyManager>.I.partySetting.level > 0)
		{
			string text = MonoBehaviourSingleton<PartyManager>.I.partySetting.level.ToString();
			int length = text.Length;
			if (length > 0)
			{
				int j = 0;
				for (int num2 = length; j < num2; j++)
				{
					passCode[j] = text[j].ToString();
				}
				passCodeIndex = length;
			}
		}
		digit = 3;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.STR_NON_SETTINGS, is_visible: false);
		int i = 0;
		for (int num = passCode.Length; i < num; i++)
		{
			SetLabelText((Enum)lblAry[i], string.Empty);
		}
		string text = string.Join(string.Empty, passCode);
		if (text.Length == 0)
		{
			SetActive((Enum)UI.STR_NON_SETTINGS, is_visible: true);
		}
		else
		{
			SetLabelText((Enum)UI.LBL_INPUT_PASS_3, text);
		}
	}

	protected override void OnOpen()
	{
	}

	protected override string GetResetString()
	{
		return string.Empty;
	}

	protected override void OnQuery_0()
	{
		if (passCodeIndex == 0)
		{
			GameSection.StopEvent();
		}
		else
		{
			base.OnQuery_0();
		}
	}

	protected override void OnQuery_ROOM()
	{
		int result = 0;
		string text = string.Join(string.Empty, passCode);
		text = text.Replace("-", string.Empty);
		int.TryParse(text, out result);
		MonoBehaviourSingleton<PartyManager>.I.partySetting.level = result;
		GameSection.ChangeEvent("[BACK]");
	}

	private void OnQuery_SECTION_BACK()
	{
		MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(null);
	}
}
