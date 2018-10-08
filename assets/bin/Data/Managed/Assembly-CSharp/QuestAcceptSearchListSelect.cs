using Network;
using System;

public class QuestAcceptSearchListSelect : QuestSearchListSelect
{
	private void OnCloseDialog_QuestAcceptSearchRoomCondition()
	{
		CloseSearchRoomCondition();
	}

	public unsafe void OnQuery_INVITED_ROOM()
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
			PartyManager i = MonoBehaviourSingleton<PartyManager>.I;
			string partyNumber = array[0];
			if (_003C_003Ef__am_0024cache0 == null)
			{
				_003C_003Ef__am_0024cache0 = new Action<bool, Error>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			i.SendApply(partyNumber, _003C_003Ef__am_0024cache0, 0);
		}
	}

	public unsafe void OnQuery_JOIN_ROOM()
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
		MonoBehaviourSingleton<PartyManager>.I.SendEntry(text, true, delegate(bool is_success)
		{
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			if (is_success && !MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog(MonoBehaviourSingleton<PartyManager>.I.GetQuestId(), true))
			{
				if (_003C_003Ef__am_0024cache3 == null)
				{
					_003C_003Ef__am_0024cache3 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				Protocol.Force(_003C_003Ef__am_0024cache3);
			}
			else
			{
				GameSection.ResumeEvent(is_success, null, false);
			}
		});
	}
}
