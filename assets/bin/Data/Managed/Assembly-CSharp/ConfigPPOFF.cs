using Network;
using System;

public class ConfigPPOFF : PPInputBase
{
	private void OnQuery_OK()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<UserInfoManager>.I.SendResetParentalPassword(GetInputValue((Enum)UI.IPT_PW), delegate(Error ret)
		{
			if (ret != 0)
			{
				GameSection.ChangeStayEvent("ERROR", new object[1]
				{
					(int)ret
				});
			}
			GameSection.ResumeEvent(true, null, false);
		});
	}
}
