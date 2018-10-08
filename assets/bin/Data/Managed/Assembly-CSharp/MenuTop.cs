using System;
using UnityEngine;

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
		SetBadge(UI.INFO, GameSaveData.instance.IsShowNewsNotification() ? 1 : 0, SpriteAlignment.TopRight, -4, -4, false);
		SetBadge(UI.PRESENT_BTN, MonoBehaviourSingleton<PresentManager>.I.presentNum, SpriteAlignment.TopRight, -4, -4, false);
		SetBadge(UI.FRIEND, MonoBehaviourSingleton<FriendManager>.I.noReadMessageNum, SpriteAlignment.TopRight, -4, -4, false);
	}

	public void OnQuery_FRIEND()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "FriendScene")
		{
			GameSection.ChangeEvent("[BACK]", null);
		}
	}

	public void OnQuery_PROFILE()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "ProfileScene")
		{
			GameSection.ChangeEvent("[BACK]", null);
		}
	}

	private void OnQuery_PRESENT()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(0, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
		});
	}

	private void OnQuery_MUTUAL_FOLLOW()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<FriendManager>.I.SendGetFollowLink(delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
		});
	}

	private void OnQuery_GOWRAP_INFO()
	{
		GameSaveData.instance.dayShowNewsNotification = DateTime.UtcNow.AddSeconds(-10800.0).Day;
		UpdateUI();
		MonoBehaviourSingleton<GoWrapManager>.I.ShowMenu();
	}
}
