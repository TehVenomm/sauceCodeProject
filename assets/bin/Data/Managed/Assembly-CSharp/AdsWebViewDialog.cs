using System;

public class AdsWebViewDialog : WebViewDialog
{
	private enum UI
	{
		Title,
		Title_D
	}

	public override void Initialize()
	{
		base.Initialize();
		SetLabelText((Enum)UI.Title, base.sectionData.GetText("STR_TITLE"));
		SetLabelText((Enum)UI.Title_D, base.sectionData.GetText("STR_TITLE"));
	}
}
