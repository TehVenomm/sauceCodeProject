using Network;
using System;

public class ConfigPPON : GameSection
{
	private enum UI
	{
		IPT_PW,
		IPT_PW_CONFIRM,
		BTN_OK
	}

	public override void UpdateUI()
	{
		SetInput((Enum)UI.IPT_PW, string.Empty, 4, (EventDelegate.Callback)OnInputChange);
		SetInput((Enum)UI.IPT_PW_CONFIRM, string.Empty, 4, (EventDelegate.Callback)OnInputChange);
	}

	private void OnInputChange()
	{
		SetButtonEnabled((Enum)UI.BTN_OK, GetInputValue((Enum)UI.IPT_PW).Length == 4 && GetInputValue((Enum)UI.IPT_PW_CONFIRM).Length == 4);
	}

	private void OnQuery_OK()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<UserInfoManager>.I.SendParentalPassword(GetInputValue((Enum)UI.IPT_PW), GetInputValue((Enum)UI.IPT_PW_CONFIRM), delegate(Error ret)
		{
			if (ret != 0)
			{
				GameSection.ChangeStayEvent("ERROR", new object[1]
				{
					(int)ret
				});
			}
			GameSection.ResumeEvent(true, null);
		});
	}
}
