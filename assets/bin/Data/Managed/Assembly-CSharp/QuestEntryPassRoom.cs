using Network;

public class QuestEntryPassRoom : GameSection
{
	protected enum UI
	{
		LBL_INPUT_PASS_1,
		LBL_INPUT_PASS_2,
		LBL_INPUT_PASS_3,
		LBL_INPUT_PASS_4,
		LBL_INPUT_PASS_5,
		STR_NON_SETTINGS
	}

	private const int PASS_CODE_MAX_DIGIT = 5;

	protected const string PASS_CODE_RESET_NUM = "-";

	protected int digit = 5;

	protected string[] passCode = new string[5]
	{
		"-",
		"-",
		"-",
		"-",
		"-"
	};

	protected int passCodeIndex;

	public override string overrideBackKeyEvent => "CLOSE";

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetActive(UI.STR_NON_SETTINGS, is_visible: false);
		SetLabelText(UI.LBL_INPUT_PASS_1, passCode[0]);
		SetLabelText(UI.LBL_INPUT_PASS_2, passCode[1]);
		SetLabelText(UI.LBL_INPUT_PASS_3, passCode[2]);
		SetLabelText(UI.LBL_INPUT_PASS_4, passCode[3]);
		SetLabelText(UI.LBL_INPUT_PASS_5, passCode[4]);
	}

	protected override void OnOpen()
	{
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.Clear();
		}
	}

	protected virtual void OnQuery_0()
	{
		InputNumber(0);
	}

	private void OnQuery_1()
	{
		InputNumber(1);
	}

	private void OnQuery_2()
	{
		InputNumber(2);
	}

	private void OnQuery_3()
	{
		InputNumber(3);
	}

	private void OnQuery_4()
	{
		InputNumber(4);
	}

	private void OnQuery_5()
	{
		InputNumber(5);
	}

	private void OnQuery_6()
	{
		InputNumber(6);
	}

	private void OnQuery_7()
	{
		InputNumber(7);
	}

	private void OnQuery_8()
	{
		InputNumber(8);
	}

	private void OnQuery_9()
	{
		InputNumber(9);
	}

	private void OnQuery_CLEAR()
	{
		passCodeIndex = 0;
		for (int i = 0; i < digit; i++)
		{
			passCode[i] = GetResetString();
		}
		RefreshUI();
	}

	protected virtual string GetResetString()
	{
		return "-";
	}

	private void InputNumber(int num)
	{
		if (passCodeIndex != digit)
		{
			passCode[passCodeIndex++] = num.ToString();
			RefreshUI();
		}
	}

	protected virtual void OnQuery_ROOM()
	{
		SendApply();
	}

	protected void SendApply(int questId = 0)
	{
		GameSection.SetEventData(new object[1]
		{
			true
		});
		GameSection.StayEvent();
		MonoBehaviourSingleton<PartyManager>.I.SendApply(string.Join("", passCode), delegate(bool is_apply, Error ret_code)
		{
			if (is_apply && !MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog(MonoBehaviourSingleton<PartyManager>.I.GetQuestId()))
			{
				Protocol.Force(delegate
				{
					MonoBehaviourSingleton<PartyManager>.I.SendLeave(delegate
					{
					});
				});
			}
			else if ((uint)(ret_code - 32009) <= 1u)
			{
				GameSection.ChangeStayEvent("NOT_FOUND_PARTY");
				GameSection.ResumeEvent(is_resume: true);
			}
			else
			{
				GameSection.ResumeEvent(is_apply);
			}
		}, questId);
	}
}
