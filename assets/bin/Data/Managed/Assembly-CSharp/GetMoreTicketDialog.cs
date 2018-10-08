public class GetMoreTicketDialog : GameSection
{
	private enum UI
	{
		SPR_BTN_FB,
		SPR_BTN_Tweter
	}

	public override void Initialize()
	{
		GetCtrl(UI.SPR_BTN_FB).GetComponent<UIButton>().set_enabled(true);
		GetCtrl(UI.SPR_BTN_Tweter).GetComponent<UIButton>().set_enabled(true);
		base.Initialize();
	}

	private void OnQuery_FACEBOOK()
	{
		Native.OpenURL("https://www.facebook.com/DragonProject/");
	}

	private void OnQuery_TWITTER()
	{
		Native.OpenURL("https://twitter.com/dragonprojectgl");
	}

	private void OnQuery_OK()
	{
		GameSection.BackSection();
	}
}
