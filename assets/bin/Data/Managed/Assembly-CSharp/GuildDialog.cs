using Network;
using System;

public class GuildDialog : GameSection
{
	public unsafe void OnQuery_INVITED_LOUNGE()
	{
		string inviteValue = MonoBehaviourSingleton<LoungeMatchingManager>.I.InviteValue;
		if (!string.IsNullOrEmpty(inviteValue))
		{
			string[] array = inviteValue.Split('_');
			GameSection.StayEvent();
			LoungeMatchingManager i = MonoBehaviourSingleton<LoungeMatchingManager>.I;
			string loungeNumber = array[0];
			if (_003C_003Ef__am_0024cache0 == null)
			{
				_003C_003Ef__am_0024cache0 = new Action<bool, Error>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			i.SendApply(loungeNumber, _003C_003Ef__am_0024cache0);
		}
	}

	public void OnQuery_FRIEND_INVITED_LOUNGE()
	{
		string id = (string)GameSection.GetEventData();
		GameSection.StayEvent();
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendEntry(id, delegate(bool isSuccess)
		{
			GameSection.ResumeEvent(isSuccess, null, false);
		});
	}

	private void OnQuery_HOW_TO()
	{
		GameSection.SetEventData(WebViewManager.lounge);
	}

	private void OnQuery_CREATE()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal < 15)
		{
			GameSection.ChangeEvent("DONT_HAVE_GEM", null);
		}
	}

	private void OnQuery_HINT()
	{
		GameSection.SetEventData(WebViewManager.GuildHint);
	}
}
