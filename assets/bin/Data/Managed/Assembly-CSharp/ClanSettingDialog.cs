public class ClanSettingDialog : ClanSettings
{
	private void OnQuery_CHANGE()
	{
		ClanEditClanModel.RequestSendForm requestSendForm = new ClanEditClanModel.RequestSendForm();
		requestSendForm.name = createRequest.clanName;
		requestSendForm.iId = createRequest.stampId;
		requestSendForm.jt = (createRequest.isLock ? 1 : 0);
		requestSendForm.lbl = (int)createRequest.label;
		requestSendForm.cmt = createRequest.comment;
		requestSendForm.tag = createRequest.clanTag;
		GameSection.StayEvent();
		MonoBehaviourSingleton<ClanMatchingManager>.I.RequestEdit(requestSendForm, delegate
		{
			GameSection.ResumeEvent(is_resume: true);
		});
	}
}
