using Network;
using System;

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
		SetActive((Enum)UI.STR_NON_SETTINGS, false);
		SetLabelText((Enum)UI.LBL_INPUT_PASS_1, passCode[0]);
		SetLabelText((Enum)UI.LBL_INPUT_PASS_2, passCode[1]);
		SetLabelText((Enum)UI.LBL_INPUT_PASS_3, passCode[2]);
		SetLabelText((Enum)UI.LBL_INPUT_PASS_4, passCode[3]);
		SetLabelText((Enum)UI.LBL_INPUT_PASS_5, passCode[4]);
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
		SendApply(0);
	}

	protected unsafe void SendApply(int questId = 0)
	{
		GameSection.SetEventData(new object[1]
		{
			true
		});
		GameSection.StayEvent();
		PartyManager i = MonoBehaviourSingleton<PartyManager>.I;
		string partyNumber = string.Join(string.Empty, passCode);
		if (_003C_003Ef__am_0024cache3 == null)
		{
			_003C_003Ef__am_0024cache3 = new Action<bool, Error>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		i.SendApply(partyNumber, _003C_003Ef__am_0024cache3, questId);
	}
}
