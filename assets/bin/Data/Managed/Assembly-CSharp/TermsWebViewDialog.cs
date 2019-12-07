using System;

public class TermsWebViewDialog : WebViewDialog
{
	private enum UI
	{
		SPR_CHECK,
		SPR_CHECK_OFF,
		BTN_DECIDE,
		BTN_DECIDE_OFF,
		STR_CONFIRM_MESSAGE,
		STR_AGE_MESSAGE
	}

	private bool isTermsAgreement;

	public override void Initialize()
	{
		base.Initialize();
		string arg = (MonoBehaviourSingleton<AccountManager>.I.termsUpdateDay != null) ? DateTime.Parse(MonoBehaviourSingleton<AccountManager>.I.termsUpdateDay).ToString("yyyymmdd") : " ";
		string text = string.Format(base.sectionData.GetText("STR_CONFIRM"), arg);
		SetLabelText(UI.STR_CONFIRM_MESSAGE, text);
		SetLabelText(UI.STR_AGE_MESSAGE, base.sectionData.GetText("STR_AGE"));
	}

	private void OnQuery_DECIDE()
	{
		MonoBehaviourSingleton<AccountManager>.I.termsCheck = false;
		EventData[] autoEvents = new EventData[1]
		{
			new EventData("PUSH_START")
		};
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	public override void UpdateUI()
	{
		SetActive(UI.SPR_CHECK, isTermsAgreement);
		SetActive(UI.SPR_CHECK_OFF, !isTermsAgreement);
		SetActive(UI.BTN_DECIDE, isTermsAgreement);
		SetActive(UI.BTN_DECIDE_OFF, !isTermsAgreement);
	}

	private void OnQuery_TERMS()
	{
		isTermsAgreement = !isTermsAgreement;
		RefreshUI();
	}
}
