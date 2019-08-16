using System;
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
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "HomeScene")
		{
			SetActive((Enum)UI.BTN_EXIT, is_visible: false);
			UISprite component = base.GetComponent<UISprite>((Enum)UI.SPR_FRAME);
			int height = component.height;
			component.height = height - 90;
			Vector3 localPosition = component.get_transform().get_localPosition();
			component.get_transform().set_localPosition(new Vector3(localPosition.x, localPosition.y - 45f, localPosition.x));
		}
		base.Initialize();
	}

	public override void StartSection()
	{
	}

	public override void UpdateUI()
	{
		bool flag = MonoBehaviourSingleton<UserInfoManager>.I.clanDisplayType != 0 && (int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 15;
		SetActive((Enum)UI.BTN_CLAN_ON, flag);
		SetActive((Enum)UI.BTN_CLAN_OFF, !flag);
		if (flag && MonoBehaviourSingleton<UserInfoManager>.I.clanDisplayType == DISPLAY_TYPE.NEW)
		{
			SetActive((Enum)UI.SPR_NEW_ICON, is_visible: true);
		}
		else
		{
			SetActive((Enum)UI.SPR_NEW_ICON, is_visible: false);
		}
		string text = (!MonoBehaviourSingleton<UserInfoManager>.I.IsRegisteredClan()) ? "旅団" : "旅団情報";
		bool flag2 = (int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 15;
		SetActive((Enum)UI.BTN_LOUNGE, flag2);
		SetActive((Enum)UI.BTN_LOUNGE_OFF, !flag2);
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
			return;
		}
		string format = StringTable.Get(STRING_CATEGORY.CLAN_ERROR, 1u);
		string eventData = string.Format(format, 15.ToString());
		GameSection.SetEventData(eventData);
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
