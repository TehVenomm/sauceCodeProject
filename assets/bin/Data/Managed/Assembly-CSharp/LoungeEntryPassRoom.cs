using Network;
using System;

public class LoungeEntryPassRoom : QuestEntryPassRoom
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

	private unsafe void OnQuery_LOUNGE()
	{
		GameSection.StayEvent();
		LoungeMatchingManager i = MonoBehaviourSingleton<LoungeMatchingManager>.I;
		string loungeNumber = string.Join(string.Empty, passCode);
		if (_003C_003Ef__am_0024cache0 == null)
		{
			_003C_003Ef__am_0024cache0 = new Action<bool, Error>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		i.SendApply(loungeNumber, _003C_003Ef__am_0024cache0);
	}
}
