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
		SetLabelText(UI.Title, base.sectionData.GetText("STR_TITLE"));
		SetLabelText(UI.Title_D, base.sectionData.GetText("STR_TITLE"));
	}
}
