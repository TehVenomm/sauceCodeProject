using System;

public class MenuTop : GameSection
{
	private enum UI
	{
		PRESENT_BTN,
		PRESENT_LBL,
		INFO,
		TITLE,
		FRIEND,
		HELP,
		CONFIG,
		STORAGE,
		SPR_BADGE
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_PRESENT_NUM | NOTIFY_FLAG.UPDATE_USER_INFO | NOTIFY_FLAG.UPDATE_USER_STATUS;
	}

	public override void UpdateUI()
	{
		SetBadge((Enum)UI.INFO, GameSaveData.instance.IsShowNewsNotification() ? 1 : 0, 3, -4, -4, is_scale_normalize: false);
		SetBadge((Enum)UI.PRESENT_BTN, MonoBehaviourSingleton<PresentManager>.I.presentNum, 3, -4, -4, is_scale_normalize: false);
		SetBadge((Enum)UI.FRIEND, MonoBehaviourSingleton<FriendManager>.I.noReadMessageNum, 3, -4, -4, is_scale_normalize: false);
	}

	public void OnQuery_FRIEND()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "FriendScene")
		{
			GameSection.ChangeEvent("[BACK]");
		}
	}

	public void OnQuery_PROFILE()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "ProfileScene")
		{
			GameSection.ChangeEvent("[BACK]");
		}
	}

	private void OnQuery_PRESENT()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(0, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	private void OnQuery_MUTUAL_FOLLOW()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<FriendManager>.I.SendGetFollowLink(delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	private void OnQuery_GOWRAP_INFO()
	{
		GameSaveData.instance.dayShowNewsNotification = DateTime.UtcNow.AddSeconds(-10800.0).Day;
		UpdateUI();
		MonoBehaviourSingleton<GoWrapManager>.I.ShowMenu();
	}
}
