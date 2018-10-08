using System;

public class GuildInfoMoreDialog : GameSection
{
	private enum UI
	{
		ProvisionalLabel
	}

	private string desc;

	public override string overrideBackKeyEvent => "CLOSE";

	public override void Initialize()
	{
		desc = (GameSection.GetEventData() as string);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetLabelText((Enum)UI.ProvisionalLabel, desc);
		UpdateAnchors();
	}

	private void OnQuery_CLOSE()
	{
		GameSection.BackSection();
	}
}
