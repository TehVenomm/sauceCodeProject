using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuildRequestCounter : GameSection
{
	private enum UI
	{
		GRD_REQUEST_HOUND,
		LBL_REQUEST_NON_LIST,
		BTN_COMPLETE_ALL,
		BTN_COMPLETE_ALL_DISABLE,
		SPR_BG,
		LBL_HOUND_REMAIN_TIME,
		BTN_HOUND_START,
		SPR_HOUND_START,
		BTN_EMPLOY,
		OBJ_QUEST_ROOT,
		SPR_QUEST_INFO_BASE,
		OBJ_ENEMY,
		SPR_MONSTER_ICON,
		LBL_QUEST_NAME,
		LBL_QUEST_NUM,
		OBJ_COMPLETE_ICON,
		OBJ_TIMEUP_ICON,
		PBR_GAUGE,
		SPR_GAUGE,
		SPR_GAUGE_BG,
		LBL_QUEST_REMAIN_TIME,
		LBL_BONUS_REMAIN_TIME,
		LBL_QUEST_CURRENT_POINT,
		BTN_COMPLETE,
		BTN_CANCEL,
		BTN_CONFIRM
	}

	private class GuildRequestPrefab
	{
		public GuildRequestItem item;

		private TimeSpan beforeHoundRemainTime;

		private TimeSpan beforeQuestRemainTime;

		public Transform prefab;

		public GuildRequestPrefab(GuildRequestItem item, Transform prefab)
		{
			this.item = item;
			this.prefab = prefab;
			SetBeforeTime();
		}

		public void SetBeforeTime()
		{
			beforeHoundRemainTime = item.GetHoundRemainTime();
			beforeQuestRemainTime = item.GetQuestRemainTime();
		}

		public bool IsHoundTimeupNow()
		{
			if (beforeHoundRemainTime.TotalSeconds > 0.0 && item.GetHoundRemainTime().TotalSeconds <= 0.0)
			{
				return true;
			}
			return false;
		}

		public bool IsQuestEndNow()
		{
			if (beforeQuestRemainTime.TotalSeconds > 0.0 && item.GetQuestRemainTime().TotalSeconds <= 0.0)
			{
				return true;
			}
			return false;
		}
	}

	private const float UPDATE_INTARVAL = 0.2f;

	private QuestInfoData selectedQuestInfoData;

	private int selectedQuestNum;

	private List<GuildRequestPrefab> prefabCache = new List<GuildRequestPrefab>();

	private float timer;

	public override string overrideBackKeyEvent => "CLOSE";

	public override void Initialize()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		selectedQuestInfoData = (GameSection.GetEventData() as QuestInfoData);
		if (selectedQuestInfoData != null)
		{
			if (IsFromShadow())
			{
				selectedQuestNum = MonoBehaviourSingleton<PartyManager>.I.challengeInfo.num;
			}
			else
			{
				selectedQuestNum = selectedQuestInfoData.questData.num;
			}
		}
		this.StartCoroutine(DoInitialize());
	}

	private unsafe IEnumerator DoInitialize()
	{
		bool wait2 = true;
		MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestList(delegate
		{
			((_003CDoInitialize_003Ec__Iterator86)/*Error near IL_0031: stateMachine*/)._003Cwait_003E__0 = false;
		});
		while (wait2)
		{
			yield return (object)null;
		}
		wait2 = true;
		SendGetChallengeInfo(new Action((object)/*Error near IL_0072: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), null);
		while (wait2)
		{
			yield return (object)null;
		}
		base.Initialize();
	}

	private void Update()
	{
		if (base.state == STATE.OPEN)
		{
			UpdateTimers();
		}
	}

	private void UpdateTimers()
	{
		if (timer < 0.2f)
		{
			timer += Time.get_deltaTime();
		}
		if (!(timer < 0.2f))
		{
			timer = 0f;
			for (int i = 0; i < prefabCache.Count; i++)
			{
				GuildRequestPrefab guildRequestPrefab = prefabCache[i];
				UpdateHoundRemainTime(guildRequestPrefab.item, guildRequestPrefab.prefab);
				UpdateQuestTimer(guildRequestPrefab.item, guildRequestPrefab.prefab);
				UpdateBonusRemainTime(guildRequestPrefab.item, guildRequestPrefab.prefab);
				if (guildRequestPrefab.IsHoundTimeupNow() || guildRequestPrefab.IsQuestEndNow())
				{
					RefreshUI();
				}
				guildRequestPrefab.SetBeforeTime();
			}
		}
	}

	public unsafe override void UpdateUI()
	{
		int count = MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList.Count;
		MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList.Sort(delegate(GuildRequestItem a, GuildRequestItem b)
		{
			if (a.crystalNum != b.crystalNum)
			{
				return a.crystalNum - b.crystalNum;
			}
			if (a.questId > 0 && b.questId <= 0)
			{
				return -1;
			}
			if (a.questId <= 0 && b.questId > 0)
			{
				return 1;
			}
			if (a.GetHoundRemainTime().TotalSeconds > 0.0 && b.GetHoundRemainTime().TotalSeconds <= 0.0)
			{
				return -1;
			}
			if (a.GetHoundRemainTime().TotalSeconds <= 0.0 && b.GetHoundRemainTime().TotalSeconds > 0.0)
			{
				return 1;
			}
			return a.slotNo - b.slotNo;
		});
		ShowNonRequestList(count > 0);
		prefabCache.Clear();
		bool isExistEmployButton = false;
		_003CUpdateUI_003Ec__AnonStorey35A _003CUpdateUI_003Ec__AnonStorey35A;
		SetGrid(UI.GRD_REQUEST_HOUND, "GuildRequestItem", count, false, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey35A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		InitCompleteAllButton(MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList);
		base.UpdateUI();
	}

	private bool IsOpenFromGachaQuest()
	{
		return selectedQuestInfoData != null;
	}

	private unsafe bool IsFromShadow()
	{
		List<GameSectionHistory.HistoryData> historyList = MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList();
		List<GameSectionHistory.HistoryData> source = historyList;
		if (_003C_003Ef__am_0024cache5 == null)
		{
			_003C_003Ef__am_0024cache5 = new Func<GameSectionHistory.HistoryData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		return source.Any(_003C_003Ef__am_0024cache5);
	}

	private void InitButtonColor(GuildRequestItem item, int index, Transform parent, bool recycle)
	{
		if (item.crystalNum == 0)
		{
			SetSprite(parent, UI.SPR_BG, "GuildRequestPlateB");
			SetSprite(parent, UI.SPR_QUEST_INFO_BASE, "GuildRequestQuestPlateB");
		}
		else
		{
			SetSprite(parent, UI.SPR_BG, "GuildRequestPlateP");
			SetSprite(parent, UI.SPR_QUEST_INFO_BASE, "GuildRequestQuestPlateP");
		}
	}

	private void InitTimeupButton(GuildRequestItem item, int index, Transform parent, bool recycle)
	{
		SetActive(parent, UI.BTN_EMPLOY, false);
		SetActive(parent, UI.BTN_HOUND_START, false);
		SetActive(parent, UI.OBJ_QUEST_ROOT, true);
		SetActive(parent, UI.PBR_GAUGE, true);
		SetActive(parent, UI.LBL_QUEST_REMAIN_TIME, false);
		SetActive(parent, UI.LBL_BONUS_REMAIN_TIME, false);
		SetActive(parent, UI.LBL_QUEST_CURRENT_POINT, true);
		SetActive(parent, UI.BTN_COMPLETE, false);
		SetActive(parent, UI.BTN_CANCEL, false);
		SetActive(parent, UI.BTN_CONFIRM, true);
		SetActive(parent, UI.OBJ_COMPLETE_ICON, false);
		SetActive(parent, UI.OBJ_TIMEUP_ICON, true);
		SetEvent(FindCtrl(parent, UI.BTN_CONFIRM), "CONTINUE", item);
		InitQuestButton(item, index, parent);
		SetTimeupColor(item, parent);
		UpdateQuestTimer(item, parent);
	}

	private void InitInactiveButton(GuildRequestItem item, int index, Transform parent, bool recycle)
	{
		SetActive(parent, UI.BTN_EMPLOY, false);
		SetActive(parent, UI.BTN_HOUND_START, false);
		SetActive(parent, UI.OBJ_QUEST_ROOT, false);
	}

	private void InitEmployButton(GuildRequestItem item, int index, Transform parent, bool recycle)
	{
		SetActive(parent, UI.BTN_EMPLOY, true);
		SetActive(parent, UI.BTN_HOUND_START, false);
		SetActive(parent, UI.OBJ_QUEST_ROOT, false);
		SetEvent(FindCtrl(parent, UI.BTN_EMPLOY), "EMPLOY", item);
	}

	private void InitSortieingButton(GuildRequestItem item, int index, Transform parent, bool recycle)
	{
		SetActive(parent, UI.BTN_EMPLOY, false);
		SetActive(parent, UI.BTN_HOUND_START, false);
		SetActive(parent, UI.OBJ_QUEST_ROOT, true);
		SetActive(parent, UI.PBR_GAUGE, true);
		SetActive(parent, UI.LBL_QUEST_REMAIN_TIME, true);
		SetActive(parent, UI.LBL_BONUS_REMAIN_TIME, false);
		SetActive(parent, UI.LBL_QUEST_CURRENT_POINT, true);
		SetActive(parent, UI.BTN_COMPLETE, false);
		SetActive(parent, UI.BTN_CANCEL, true);
		SetActive(parent, UI.BTN_CONFIRM, false);
		SetActive(parent, UI.OBJ_COMPLETE_ICON, false);
		SetActive(parent, UI.OBJ_TIMEUP_ICON, false);
		SetEvent(FindCtrl(parent, UI.BTN_CANCEL), "CANCEL", item);
		SetDefaultColor(item, parent);
		InitQuestButton(item, index, parent);
		UpdateQuestTimer(item, parent);
	}

	private void InitCompleteButton(GuildRequestItem item, int index, Transform parent, bool recycle)
	{
		SetActive(parent, UI.BTN_EMPLOY, false);
		SetActive(parent, UI.BTN_HOUND_START, false);
		SetActive(parent, UI.OBJ_QUEST_ROOT, true);
		SetActive(parent, UI.PBR_GAUGE, false);
		SetActive(parent, UI.LBL_QUEST_REMAIN_TIME, false);
		SetActive(parent, UI.LBL_BONUS_REMAIN_TIME, true);
		SetActive(parent, UI.LBL_QUEST_CURRENT_POINT, false);
		SetActive(parent, UI.BTN_COMPLETE, true);
		SetActive(parent, UI.BTN_CANCEL, false);
		SetActive(parent, UI.BTN_CONFIRM, false);
		SetActive(parent, UI.OBJ_COMPLETE_ICON, true);
		SetActive(parent, UI.OBJ_TIMEUP_ICON, false);
		SetEvent(FindCtrl(parent, UI.BTN_COMPLETE), "COMPLETE", item);
		UpdateBonusRemainTime(item, parent);
		SetDefaultColor(item, parent);
		InitQuestButton(item, index, parent);
		UpdateQuestTimer(item, parent);
	}

	private unsafe void InitCompleteAllButton(List<GuildRequestItem> guildRequestItemList)
	{
		if (_003C_003Ef__am_0024cache6 == null)
		{
			_003C_003Ef__am_0024cache6 = new Func<GuildRequestItem, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		bool flag = guildRequestItemList.Any(_003C_003Ef__am_0024cache6);
		SetActive((Enum)UI.BTN_COMPLETE_ALL, flag);
		SetActive((Enum)UI.BTN_COMPLETE_ALL_DISABLE, !flag);
	}

	private void InitHoundStartButton(GuildRequestItem item, int index, Transform parent, bool recycle)
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		SetActive(parent, UI.BTN_EMPLOY, false);
		SetActive(parent, UI.BTN_HOUND_START, true);
		SetActive(parent, UI.OBJ_QUEST_ROOT, false);
		Transform val = FindCtrl(parent, UI.BTN_HOUND_START);
		UIButton component = val.GetComponent<UIButton>();
		if (IsOpenFromGachaQuest() && selectedQuestNum == 0)
		{
			component.isEnabled = false;
			SetColor(parent, UI.SPR_HOUND_START, new Color(0.5f, 0.5f, 0.5f));
		}
		else
		{
			component.isEnabled = true;
			SetColor(parent, UI.SPR_HOUND_START, new Color(1f, 1f, 1f));
		}
		if (IsOpenFromGachaQuest())
		{
			SetEvent(val, "SORTIE", item);
		}
		else
		{
			SetEvent(val, "SELECT", item);
		}
	}

	private void InitQuestButton(GuildRequestItem item, int index, Transform parent)
	{
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)item.questId);
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questData.GetMainEnemyID());
		ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, questData.rarity, FindCtrl(parent, UI.OBJ_ENEMY), enemyData.element, null, -1, null, 0, false, -1, false, null, false, 0, 0, false, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
		itemIcon.SetEnableCollider(false);
		SetLabelText(parent, UI.LBL_QUEST_NAME, questData.questText);
	}

	private void SetDefaultColor(GuildRequestItem item, Transform parent)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		SetColor(parent, UI.SPR_QUEST_INFO_BASE, new Color(1f, 1f, 1f));
		SetColor(parent, UI.LBL_QUEST_NAME, new Color(1f, 1f, 1f));
		SetColor(parent, UI.LBL_QUEST_CURRENT_POINT, new Color(1f, 1f, 1f));
		SetColor(parent, UI.SPR_GAUGE, new Color(1f, 1f, 1f));
		SetColor(parent, UI.SPR_GAUGE_BG, new Color(1f, 1f, 1f));
		SetColor(parent, UI.LBL_QUEST_REMAIN_TIME, new Color(1f, 1f, 1f));
	}

	private void SetTimeupColor(GuildRequestItem item, Transform parent)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		SetColor(parent, UI.SPR_QUEST_INFO_BASE, new Color(0.5f, 0.5f, 0.5f));
		SetColor(parent, UI.LBL_QUEST_NAME, new Color(0.5f, 0.5f, 0.5f));
		SetColor(parent, UI.LBL_QUEST_CURRENT_POINT, new Color(0.5f, 0.5f, 0.5f));
		SetColor(parent, UI.SPR_GAUGE, new Color(0.5f, 0.5f, 0.5f));
		SetColor(parent, UI.SPR_GAUGE_BG, new Color(0.5f, 0.5f, 0.5f));
		SetColor(parent, UI.LBL_QUEST_REMAIN_TIME, new Color(0.5f, 0.5f, 0.5f));
	}

	private void UpdateQuestTimer(GuildRequestItem item, Transform parent)
	{
		SetQuestRemainTime(item, parent);
		SetQuestPoint(item, parent);
	}

	private void SetQuestRemainTime(GuildRequestItem item, Transform parent)
	{
		double totalSeconds = item.GetQuestRemainTime().TotalSeconds;
		if (totalSeconds < 0.0)
		{
			SetActive(parent, UI.LBL_QUEST_REMAIN_TIME, false);
		}
		else
		{
			string format = StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 11u);
			string text = string.Format(format, UIUtility.TimeFormat((int)totalSeconds, true));
			SetLabelText(parent, UI.LBL_QUEST_REMAIN_TIME, text);
		}
	}

	private void SetQuestPoint(GuildRequestItem item, Transform parent)
	{
		double totalSeconds = item.GetQuestRemainTime().TotalSeconds;
		if (totalSeconds < 0.0)
		{
			SetProgressValue(parent, UI.PBR_GAUGE, 1f);
		}
		else
		{
			QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)item.questId);
			TimeSpan needTime = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedTime(questData.rarity);
			float value = (float)((needTime.TotalSeconds - totalSeconds) / needTime.TotalSeconds);
			SetProgressValue(parent, UI.PBR_GAUGE, value);
			int needPoint = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedPoint(questData.rarity);
			int questRemainPoint = item.GetQuestRemainPoint();
			int num = needPoint - questRemainPoint;
			SetLabelText(parent, UI.LBL_QUEST_CURRENT_POINT, num + "/" + needPoint + "pt");
		}
	}

	private void UpdateHoundRemainTime(GuildRequestItem item, Transform parent)
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		double totalSeconds = item.GetHoundRemainTime().TotalSeconds;
		string empty = string.Empty;
		Transform val = FindCtrl(parent, UI.LBL_HOUND_REMAIN_TIME);
		UILabel component = val.GetComponent<UILabel>();
		if (item.crystalNum > 0)
		{
			string format = StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 15u);
			string arg = StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, (uint)(16 + item.slotNo - 1));
			if (totalSeconds < 0.0)
			{
				empty = string.Format(format, arg, UIUtility.TimeFormat(0, true));
				SetLabelText(val, empty);
				SetColor(val, Color.get_yellow());
				component.effectStyle = UILabel.Effect.None;
			}
			else
			{
				empty = string.Format(format, arg, UIUtility.TimeFormat((int)totalSeconds, true));
				SetLabelText(val, empty);
				SetColor(val, Color.get_yellow());
				component.effectStyle = UILabel.Effect.None;
			}
		}
		else
		{
			empty = StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 12u);
			SetLabelText(val, empty);
			SetColor(val, Color.get_white());
			component.effectStyle = UILabel.Effect.Outline8;
			component.effectColor = Color.get_black();
		}
	}

	private void UpdateBonusRemainTime(GuildRequestItem item, Transform parent)
	{
		string format = StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 14u);
		string bonusRemainTimeWithFormat = item.GetBonusRemainTimeWithFormat();
		string text = string.Format(format, bonusRemainTimeWithFormat);
		SetLabelText(parent, UI.LBL_BONUS_REMAIN_TIME, text);
	}

	private void ShowNonRequestList(bool isShow)
	{
		if (isShow && MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData != null && MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList.Count == 0)
		{
			SetActive((Enum)UI.LBL_REQUEST_NON_LIST, true);
			SetLabelText((Enum)UI.LBL_REQUEST_NON_LIST, StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 100u));
		}
		else
		{
			SetActive((Enum)UI.LBL_REQUEST_NON_LIST, false);
		}
	}

	protected unsafe void SendGetChallengeInfo(Action onFinish, Action<bool> cb)
	{
		_003CSendGetChallengeInfo_003Ec__AnonStorey35B _003CSendGetChallengeInfo_003Ec__AnonStorey35B;
		MonoBehaviourSingleton<PartyManager>.I.SendGetChallengeInfo(new Action<bool, Error>((object)_003CSendGetChallengeInfo_003Ec__AnonStorey35B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void OnQuery_SELECT()
	{
		MonoBehaviourSingleton<GuildRequestManager>.I.SetSelectedItem(GameSection.GetEventData() as GuildRequestItem);
	}

	private void OnQuery_EMPLOY()
	{
		MonoBehaviourSingleton<GuildRequestManager>.I.SetSelectedItem(GameSection.GetEventData() as GuildRequestItem);
		GuildRequestItem selectedItem = MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem();
		string eventData = string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 1u), selectedItem.crystalNum);
		GameSection.SetEventData(eventData);
	}

	private void OnQuery_GuildRequestEmploy_YES()
	{
		GuildRequestItem selectedItem = MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem();
		if (GameSection.CheckCrystal(selectedItem.crystalNum, 0, true))
		{
			string eventData = StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 2u);
			GameSection.SetEventData(eventData);
			GameSection.StayEvent();
			MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestExtend(delegate(bool isSuccess)
			{
				GameSection.ResumeEvent(isSuccess, null);
			});
		}
	}

	private void OnQuery_CANCEL()
	{
		MonoBehaviourSingleton<GuildRequestManager>.I.SetSelectedItem(GameSection.GetEventData() as GuildRequestItem);
		GuildRequestItem selectedItem = MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem();
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)selectedItem.questId);
		int needPoint = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedPoint(questData.rarity);
		int questRemainPoint = selectedItem.GetQuestRemainPoint();
		int num = needPoint - questRemainPoint;
		string eventData = string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 3u), num + "/" + needPoint, selectedItem.GetQuestRemainTimeWithFormat());
		GameSection.SetEventData(eventData);
	}

	private unsafe void OnQuery_GuildRequestCancel_YES()
	{
		GuildRequestItem selectedItem = MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem();
		uint selectedQuestId = (uint)selectedItem.questId;
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestRetire(delegate
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Expected O, but got Unknown
			_003COnQuery_GuildRequestCancel_YES_003Ec__AnonStorey35C._003COnQuery_GuildRequestCancel_YES_003Ec__AnonStorey35D _003COnQuery_GuildRequestCancel_YES_003Ec__AnonStorey35D;
			SendGetChallengeInfo(new Action((object)_003COnQuery_GuildRequestCancel_YES_003Ec__AnonStorey35D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), null);
		});
	}

	private void UpdateSelectedQuestNum(int i, uint selectedQuestId)
	{
		if (IsOpenFromGachaQuest())
		{
			if (IsFromShadow())
			{
				selectedQuestNum = MonoBehaviourSingleton<PartyManager>.I.challengeInfo.num;
			}
			else if (selectedQuestId == selectedQuestInfoData.questData.tableData.questID)
			{
				selectedQuestNum += i;
			}
		}
	}

	private void OnQuery_COMPLETE()
	{
		MonoBehaviourSingleton<GuildRequestManager>.I.SetSelectedItem(GameSection.GetEventData() as GuildRequestItem);
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestComplete(delegate(GuildRequestCompleteModel.Param questCompleteData)
		{
			if (!MonoBehaviourSingleton<QuestManager>.I.needRequestOrderQuestList)
			{
				GameSection.ResumeEvent(questCompleteData != null, null);
				GameSection.SetEventData(questCompleteData);
			}
			else
			{
				MonoBehaviourSingleton<QuestManager>.I.SendGetQuestList(delegate
				{
					GameSection.ResumeEvent(questCompleteData != null, null);
					GameSection.SetEventData(questCompleteData);
				});
			}
		});
	}

	private void OnQuery_COMPLETE_ALL()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestCompleteAll(delegate(GuildRequestCompleteModel.Param questCompleteData)
		{
			MonoBehaviourSingleton<GuildRequestManager>.I.isCompleteMulti = true;
			if (!MonoBehaviourSingleton<QuestManager>.I.needRequestOrderQuestList)
			{
				GameSection.ResumeEvent(questCompleteData != null, null);
				GameSection.SetEventData(questCompleteData);
			}
			else
			{
				MonoBehaviourSingleton<QuestManager>.I.SendGetQuestList(delegate
				{
					GameSection.ResumeEvent(questCompleteData != null, null);
					GameSection.SetEventData(questCompleteData);
				});
			}
		});
	}

	private void OnQuery_CONTINUE()
	{
		MonoBehaviourSingleton<GuildRequestManager>.I.SetSelectedItem(GameSection.GetEventData() as GuildRequestItem);
		GuildRequestItem selectedItem = MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem();
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)selectedItem.questId);
		int needPoint = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedPoint(questData.rarity);
		int questRemainPoint = selectedItem.GetQuestRemainPoint();
		int num = needPoint - questRemainPoint;
		string eventData = string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 4u), selectedItem.crystalNum, num + "/" + needPoint, selectedItem.GetQuestRemainTimeWithFormat());
		GameSection.SetEventData(eventData);
	}

	private void OnQuery_DETAIL()
	{
		GameSection.SetEventData(WebViewManager.GuildRequest);
	}

	private void OnQuery_SORTIE()
	{
		MonoBehaviourSingleton<GuildRequestManager>.I.SetSelectedItem(GameSection.GetEventData() as GuildRequestItem);
		GuildRequestItem selectedItem = MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem();
		QuestInfoData questInfoData = selectedQuestInfoData;
		string arg = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedPoint(questInfoData.questData.tableData.rarity).ToString();
		string needTimeWithFormat = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedTimeWithFormat(questInfoData.questData.tableData.rarity);
		string houndRemainTimeWithFormat = selectedItem.GetHoundRemainTimeWithFormat();
		TimeSpan needTime = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedTime(questInfoData.questData.tableData.rarity);
		TimeSpan houndRemainTime = selectedItem.GetHoundRemainTime();
		string eventData = (!(0.0 < houndRemainTime.TotalSeconds) || !(houndRemainTime < needTime)) ? string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 0u), arg, needTimeWithFormat) : string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 5u), arg, needTimeWithFormat, houndRemainTimeWithFormat);
		GameSection.SetEventData(eventData);
	}

	protected unsafe virtual void OnQuery_GuildRequestCounterSortieMessage_YES()
	{
		QuestInfoData questInfoData = selectedQuestInfoData;
		bool flag = IsFromShadow();
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestStart(questInfoData, !flag, delegate
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Expected O, but got Unknown
			GuildRequestCounter guildRequestCounter = this;
			_003COnQuery_GuildRequestCounterSortieMessage_YES_003Ec__AnonStorey360 _003COnQuery_GuildRequestCounterSortieMessage_YES_003Ec__AnonStorey;
			SendGetChallengeInfo(new Action((object)_003COnQuery_GuildRequestCounterSortieMessage_YES_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), null);
		});
	}

	protected virtual void OnQuery_CLOSE()
	{
		if (IsOpenFromGachaQuest())
		{
			GameSection.ChangeEvent("BACK_TO_QUEST_SELECT", null);
		}
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_EQUIP_CHANGE) != (NOTIFY_FLAG)0L)
		{
			SetDirty(UI.GRD_REQUEST_HOUND);
		}
		base.OnNotify(flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_EQUIP_CHANGE;
	}
}
