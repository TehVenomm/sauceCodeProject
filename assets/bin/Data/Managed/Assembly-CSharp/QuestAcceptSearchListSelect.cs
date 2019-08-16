using Network;

public class QuestAcceptSearchListSelect : QuestSearchListSelect
{
	private void OnCloseDialog_QuestAcceptSearchRoomCondition()
	{
		CloseSearchRoomCondition();
	}

	public void OnQuery_INVITED_ROOM()
	{
		string inviteValue = MonoBehaviourSingleton<PartyManager>.I.InviteValue;
		if (!string.IsNullOrEmpty(inviteValue))
		{
			string[] array = inviteValue.Split('_');
			GameSection.SetEventData(new object[1]
			{
				false
			});
			GameSection.StayEvent();
			MonoBehaviourSingleton<PartyManager>.I.SendApply(array[0], delegate(bool is_success, Error ret_code)
			{
				if (is_success && !MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog(MonoBehaviourSingleton<PartyManager>.I.GetQuestId()))
				{
					Protocol.Force(delegate
					{
						MonoBehaviourSingleton<PartyManager>.I.SendLeave(delegate
						{
						});
					});
				}
				else
				{
					GameSection.ResumeEvent(is_success);
				}
			});
		}
	}

	public void OnQuery_JOIN_ROOM()
	{
		string text = (string)GameSection.GetEventData();
		if (string.IsNullOrEmpty(text))
		{
			GameSection.StopEvent();
		}
		GameSection.SetEventData(new object[1]
		{
			false
		});
		GameSection.StayEvent();
		MonoBehaviourSingleton<PartyManager>.I.SendEntry(text, isLoungeBoard: true, delegate(bool is_success)
		{
			if (is_success && !MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog(MonoBehaviourSingleton<PartyManager>.I.GetQuestId()))
			{
				Protocol.Force(delegate
				{
					MonoBehaviourSingleton<PartyManager>.I.SendLeave(delegate
					{
					});
				});
			}
			else
			{
				GameSection.ResumeEvent(is_success);
			}
		});
	}
}
