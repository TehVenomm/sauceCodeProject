using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GuildInformationStep2 : GameSection
{
	public enum UI
	{
		POP_TARGET_LOCK,
		LBL_TARGET_LOCK,
		POP_TARGET_MIN_LEVEL,
		LBL_TARGET_MIN_LEVEL,
		SPR_MAP,
		STR_MAP_ADDITION,
		BTN_INFO,
		SPR_TYPE_INFO,
		STR_TYPE_INFOR,
		BTN_NEXT,
		BTN_NEXT_UPDATE
	}

	private GuildManager.CreateGuildRequestParam mCreateRequest;

	private List<string> lockNames;

	protected int lockIndex;

	private Transform lockPopup;

	private List<string> levelNames;

	protected int levelIndex;

	private Transform levelPopup;

	protected int mapIndex;

	private List<string> spriteMapNames;

	private List<string> additioonMapInfos;

	public override void Initialize()
	{
		base.Initialize();
		if (MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanId != -1)
		{
			MonoBehaviourSingleton<GuildManager>.I.CreateAddedGuildRequestParam(MonoBehaviourSingleton<GuildManager>.I.guildChangeData);
		}
		mCreateRequest = MonoBehaviourSingleton<GuildManager>.I.GetCreateGuildRequestParam();
		InitUIData();
	}

	private void InitUIData()
	{
		lockNames = new List<string>();
		lockNames.Add("PUBLIC");
		lockNames.Add("PRIVATE");
		lockNames.Add("CLOSED");
		lockIndex = (int)mCreateRequest.GuildType;
		levelNames = new List<string>();
		levelNames.Add("15");
		levelNames.Add("30");
		levelNames.Add("80");
		levelNames.Add("150");
		levelNames.Add("250");
		levelIndex = 0;
		spriteMapNames = new List<string>();
		spriteMapNames.Add("temp_map1");
		spriteMapNames.Add("temp_map2");
		additioonMapInfos = new List<string>();
		additioonMapInfos.Add("+5% Fire Damage");
		additioonMapInfos.Add("+5% Water Damage");
		mapIndex = mCreateRequest.GuildMapID;
		UpdateMap();
		bool flag = MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanId != -1;
		SetActive((Enum)UI.BTN_NEXT, !flag);
		SetActive((Enum)UI.BTN_NEXT_UPDATE, flag);
		SetActive((Enum)UI.SPR_TYPE_INFO, false);
		SetTouchAndRelease((Enum)UI.BTN_INFO, "TYPE_INFO_SHOW", "TYPE_INFO_HIDE", (object)null);
		SetActive((Enum)UI.SPR_TYPE_INFO, false);
		SetSupportEncoding(UI.STR_TYPE_INFOR, true);
		mCreateRequest.SetGuildType(GuildManager.GUILD_TYPE.PUBLIC);
		mCreateRequest.SetGuildMinLevel(int.Parse(levelNames[0]));
	}

	public override void UpdateUI()
	{
		UpdateMinLevel();
		UpdateLock();
	}

	private void UpdateMinLevel()
	{
		int index = levelIndex;
		SetLabelText((Enum)UI.LBL_TARGET_MIN_LEVEL, levelNames[index]);
	}

	private void UpdateLock()
	{
		int index = lockIndex;
		SetLabelText((Enum)UI.LBL_TARGET_LOCK, lockNames[index]);
	}

	private void UpdateMap()
	{
		SetSprite((Enum)UI.SPR_MAP, spriteMapNames[mapIndex]);
		SetLabelText((Enum)UI.STR_MAP_ADDITION, additioonMapInfos[mapIndex]);
	}

	private void OnQuery_MAP_NEXT()
	{
		mapIndex = (mapIndex + 1) % 2;
		UpdateMap();
	}

	private void OnQuery_MAP_BACK()
	{
		mapIndex = (mapIndex - 1 + 2) % 2;
		UpdateMap();
	}

	private void OnQuery_TARGET_LOCK()
	{
		if (lockPopup == null)
		{
			lockPopup = Realizes("ScrollablePopupList", GetCtrl(UI.POP_TARGET_LOCK), false);
		}
		if (!(lockPopup == null))
		{
			bool[] array = new bool[lockNames.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = true;
			}
			int select_index = lockIndex;
			UIScrollablePopupList.CreatePopup(lockPopup, GetCtrl(UI.POP_TARGET_LOCK), 3, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, lockNames.ToArray(), array, select_index, delegate(int index)
			{
				lockIndex = index;
				mCreateRequest.SetGuildType((GuildManager.GUILD_TYPE)lockIndex);
				RefreshUI();
			});
		}
	}

	private void OnQuery_TARGET_LEVEL()
	{
		if (levelPopup == null)
		{
			levelPopup = Realizes("ScrollablePopupList", GetCtrl(UI.POP_TARGET_MIN_LEVEL), false);
		}
		if (!(levelPopup == null))
		{
			bool[] array = new bool[levelNames.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = true;
			}
			int select_index = levelIndex;
			UIScrollablePopupList.CreatePopup(levelPopup, GetCtrl(UI.POP_TARGET_MIN_LEVEL), levelNames.Count, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, levelNames.ToArray(), array, select_index, delegate(int index)
			{
				levelIndex = index;
				mCreateRequest.SetGuildMinLevel(int.Parse(levelNames[levelIndex]));
				RefreshUI();
			});
		}
	}

	private void OnQuery_SETTING_UPDATE()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendChangeSetting(mCreateRequest, delegate(bool isSuccess, Error error)
		{
			if (isSuccess)
			{
				MonoBehaviourSingleton<GuildManager>.I.GetClanStat(delegate
				{
					GameSection.ResumeEvent(true, null);
				});
			}
			else
			{
				GameSection.ResumeEvent(true, null);
			}
		});
	}

	private void OnQuery_CLOSE()
	{
		MonoBehaviourSingleton<GuildManager>.I.ClearCreateGuildRequestParam();
		if (MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanId != -1)
		{
			GameSection.ChangeEvent("CLOSE_UPDATE", null);
		}
	}

	private void OnQuery_TYPE_INFO_SHOW()
	{
		SetActive((Enum)UI.SPR_TYPE_INFO, true);
	}

	private void OnQuery_TYPE_INFO_HIDE()
	{
		SetActive((Enum)UI.SPR_TYPE_INFO, false);
	}
}
