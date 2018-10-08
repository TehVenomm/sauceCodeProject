using System;
using UnityEngine;

public class QuestResultHostLeft : GameSection
{
	private void OnQuery_LEAVE_HUNT()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<ChatManager>.I.SwitchRoomChatConnectionToCoopConnection();
		Action<bool> call_back = delegate
		{
			GameSection.ResumeEvent(true, null);
		};
		MonoBehaviourSingleton<CoopApp>.I.LeaveWithParty(call_back, true, false);
		if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.mainChat != (UnityEngine.Object)null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
		}
	}
}
