using System;

public class QuestResultHostLeft : GameSection
{
	private void OnQuery_LEAVE_HUNT()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<ChatManager>.I.SwitchRoomChatConnectionToCoopConnection();
		Action<bool> call_back = delegate
		{
			GameSection.ResumeEvent(is_resume: true);
		};
		MonoBehaviourSingleton<CoopApp>.I.LeaveWithParty(call_back, toHome: true);
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
		}
	}
}
