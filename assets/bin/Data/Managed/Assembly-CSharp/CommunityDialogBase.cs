using UnityEngine;

public class CommunityDialogBase : GameSection
{
	private enum UI
	{
		SPR_FRAME,
		BTN_CLAN_ON,
		BTN_CLAN_OFF,
		BTN_LOUNGE,
		BTN_LOUNGE_OFF,
		BTN_EXIT,
		SPR_NEW_ICON
	}

	public override void Initialize()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "HomeScene")
		{
			SetActive(UI.BTN_EXIT, is_visible: false);
			UISprite component = GetComponent<UISprite>(UI.SPR_FRAME);
			int height = component.height;
			component.height = height - 90;
			Vector3 localPosition = component.transform.localPosition;
			component.transform.localPosition = new Vector3(localPosition.x, localPosition.y - 45f, localPosition.x);
		}
		base.Initialize();
	}

	public override void StartSection()
	{
	}

	public override void UpdateUI()
	{
		bool flag = MonoBehaviourSingleton<UserInfoManager>.I.clanDisplayType != 0 && (int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 15;
		SetActive(UI.BTN_CLAN_ON, flag);
		SetActive(UI.BTN_CLAN_OFF, !flag);
		if (flag && MonoBehaviourSingleton<UserInfoManager>.I.clanDisplayType == DISPLAY_TYPE.NEW)
		{
			SetActive(UI.SPR_NEW_ICON, is_visible: true);
		}
		else
		{
			SetActive(UI.SPR_NEW_ICON, is_visible: false);
		}
		MonoBehaviourSingleton<UserInfoManager>.I.IsRegisteredClan();
		bool flag2 = (int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 15;
		SetActive(UI.BTN_LOUNGE, flag2);
		SetActive(UI.BTN_LOUNGE_OFF, !flag2);
	}

	private void OnQuery_CLAN()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.IsRegisteredClan())
		{
			GameSection.ChangeEvent("REGIESTERED_CLAN");
			GameSection.StayEvent();
			MonoBehaviourSingleton<ClanMatchingManager>.I.RequestDetail("0", delegate
			{
				GameSection.ResumeEvent(is_resume: true);
			});
		}
	}

	private void OnQuery_CLAN_ERROR()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.clanDisplayType == DISPLAY_TYPE.NONE)
		{
			GameSection.SetEventData(StringTable.Get(STRING_CATEGORY.CLAN_ERROR, 0u));
		}
		else
		{
			GameSection.SetEventData(string.Format(StringTable.Get(STRING_CATEGORY.CLAN_ERROR, 1u), 15.ToString()));
		}
	}

	private void OnQuery_LOUNGE()
	{
		if (MonoBehaviourSingleton<LoungeManager>.IsValid())
		{
			GameSection.ChangeEvent("LOUNGE_SETTING");
		}
	}

	private void OnQuery_LOUNGE_ERROR()
	{
		GameSection.SetEventData(new string[2]
		{
			15.ToString(),
			"OK"
		});
	}

	private void OnQuery_EXIT()
	{
		EventData[] autoEvents = new EventData[1]
		{
			new EventData("MAIN_MENU_HOME", null)
		};
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}
}
