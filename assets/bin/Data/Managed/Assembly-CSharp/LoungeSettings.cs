using System;

public class LoungeSettings : GameSection
{
	private enum UI
	{
		BTN_SETTING,
		SPR_SETTING_GRAY,
		LBL_ROOM_ID
	}

	public override void Initialize()
	{
		string loungeNumber = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetLoungeNumber();
		SetLabelText((Enum)UI.LBL_ROOM_ID, loungeNumber);
		int ownerUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetOwnerUserId();
		bool flag = ownerUserId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		SetActive((Enum)UI.BTN_SETTING, flag);
		SetActive((Enum)UI.SPR_SETTING_GRAY, !flag);
		base.Initialize();
	}

	private void OnQuery_MEMBER()
	{
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData == null)
		{
			GameSection.ChangeEvent("ERROR", null);
		}
		else
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo(delegate(bool isSuccess)
			{
				GameSection.ResumeEvent(isSuccess, null, false);
			}, false);
		}
	}

	private void OnQuery_EXIT()
	{
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData != null)
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<LoungeMatchingManager>.I.SendLeave(delegate(bool isSuccess)
			{
				GameSection.ResumeEvent(isSuccess, null, false);
			});
		}
	}

	private void OnQuery_INVITE()
	{
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData == null)
		{
			GameSection.ChangeEvent("ERROR", null);
		}
	}

	private void OnQuery_SETTING()
	{
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData == null)
		{
			GameSection.ChangeEvent("ERROR", null);
		}
	}

	private void OnQuery_HOW_TO()
	{
		GameSection.SetEventData(WebViewManager.lounge);
	}
}
