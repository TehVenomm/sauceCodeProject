using System;

public abstract class PPInputBase : GameSection
{
	protected enum UI
	{
		IPT_PW,
		BTN_OK,
		STR_REMOVE_PASS
	}

	public override void UpdateUI()
	{
		SetInput((Enum)UI.IPT_PW, string.Empty, 4, (EventDelegate.Callback)OnInputChange);
	}

	private void OnInputChange()
	{
		SetButtonEnabled((Enum)UI.BTN_OK, GetInputValue((Enum)UI.IPT_PW).Length == 4);
	}
}
