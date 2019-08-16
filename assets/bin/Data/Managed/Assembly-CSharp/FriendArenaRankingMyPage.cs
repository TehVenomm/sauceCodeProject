using Network;
using System;
using System.Collections;
using UnityEngine;

public class FriendArenaRankingMyPage : GameSection
{
	private enum UI
	{
		LBL_ARENA_NAME,
		LBL_END_DATE,
		OBJ_GROUP_A,
		OBJ_GROUP_B,
		OBJ_GROUP_C,
		OBJ_GROUP_D,
		OBJ_GROUP_E,
		OBJ_TOTAL,
		OBJ_SCORE,
		OBJ_NO_SCORE,
		OBJ_MY_RANK,
		LBL_GROUP_TIME,
		LBL_TIME_DEFAULT,
		LBL_MY_RANK,
		OBJ_NOT_EXIST,
		SPR_RANK_NUM_0,
		SPR_RANK_NUM_1,
		SPR_RANK_NUM_2,
		SPR_RANK_NUM_3,
		SPR_RANK_NUM_4,
		SPR_RANK_NUM_5,
		SPR_RANK_NUM_6,
		SPR_RANK,
		SPR_OUT_OF_RANK,
		LBL_NO_TOTAL
	}

	private Network.EventData eventData;

	private bool isExistArena = true;

	private static readonly UI[] Groups = new UI[5]
	{
		UI.OBJ_GROUP_A,
		UI.OBJ_GROUP_B,
		UI.OBJ_GROUP_C,
		UI.OBJ_GROUP_D,
		UI.OBJ_GROUP_E
	};

	private bool IsFinishRecieveDelivery;

	private ArenaUserRecordModel.Param record;

	private int userRank = -1;

	private readonly string[] RankingNumbers = new string[10]
	{
		"RankingNumber_0",
		"RankingNumber_1",
		"RankingNumber_2",
		"RankingNumber_3",
		"RankingNumber_4",
		"RankingNumber_5",
		"RankingNumber_6",
		"RankingNumber_7",
		"RankingNumber_8",
		"RankingNumber_9"
	};

	private readonly UI[] RankingNumUIs = new UI[7]
	{
		UI.SPR_RANK_NUM_0,
		UI.SPR_RANK_NUM_1,
		UI.SPR_RANK_NUM_2,
		UI.SPR_RANK_NUM_3,
		UI.SPR_RANK_NUM_4,
		UI.SPR_RANK_NUM_5,
		UI.SPR_RANK_NUM_6
	};

	public override void Initialize()
	{
		eventData = (GameSection.GetEventData() as Network.EventData);
		IsFinishRecieveDelivery = true;
		if (eventData == null)
		{
			isExistArena = false;
			base.Initialize();
		}
		else if (IsRankingJoin())
		{
			this.StartCoroutine(SendGetMyRcord());
		}
		else
		{
			base.Initialize();
		}
	}

	private IEnumerator SendGetMyRcord()
	{
		while (!IsFinishRecieveDelivery)
		{
			yield return null;
		}
		bool isFinishGetRecord = false;
		MonoBehaviourSingleton<QuestManager>.I.SendGetArenaUserRecord(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, eventData.eventId, delegate(bool b, ArenaUserRecordModel.Param result)
		{
			isFinishGetRecord = true;
			record = result;
			userRank = record.userRank;
		});
		while (!isFinishGetRecord)
		{
			yield return null;
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		UpdateTitle();
		UpdateRecord();
		base.UpdateUI();
	}

	private void UpdateTitle()
	{
		if (!isExistArena)
		{
			SetLabelText((Enum)UI.LBL_ARENA_NAME, string.Empty);
			SetLabelText((Enum)UI.LBL_END_DATE, string.Empty);
		}
		else
		{
			SetLabelText((Enum)UI.LBL_ARENA_NAME, eventData.name);
			string endDateString = QuestUtility.GetEndDateString(eventData);
			SetLabelText((Enum)UI.LBL_END_DATE, endDateString);
		}
	}

	private void UpdateRecord()
	{
		SetActive((Enum)UI.OBJ_NO_SCORE, !IsRankingJoin());
		SetActive((Enum)UI.OBJ_SCORE, IsRankingJoin());
		SetActive((Enum)UI.OBJ_MY_RANK, IsRankingJoin());
		SetActive((Enum)UI.OBJ_NOT_EXIST, is_visible: false);
		if (!isExistArena)
		{
			SetActive((Enum)UI.OBJ_NO_SCORE, is_visible: false);
			SetActive((Enum)UI.OBJ_SCORE, is_visible: false);
			SetActive((Enum)UI.OBJ_MY_RANK, is_visible: false);
			SetActive((Enum)UI.OBJ_NOT_EXIST, is_visible: true);
			return;
		}
		if (!IsRankingJoin())
		{
			SetLabelText((Enum)UI.LBL_NO_TOTAL, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 29u), ARENA_RANK.S.ToString()));
			return;
		}
		UpdateRank();
		int i = 0;
		for (int count = record.clearMilliSecList.Count; i < count; i++)
		{
			Transform ctrl = GetCtrl(Groups[i]);
			bool flag = QuestUtility.IsDefaultArenaTime(record.clearMilliSecList[i]);
			string text = QuestUtility.CreateTimeStringByMilliSec(record.clearMilliSecList[i]);
			SetActive(ctrl, UI.LBL_GROUP_TIME, !flag);
			SetActive(ctrl, UI.LBL_TIME_DEFAULT, flag);
			if (flag)
			{
				SetLabelText(ctrl, UI.LBL_TIME_DEFAULT, text);
			}
			else
			{
				SetLabelText(ctrl, UI.LBL_GROUP_TIME, text);
			}
		}
		Transform ctrl2 = GetCtrl(UI.OBJ_TOTAL);
		string text2 = QuestUtility.CreateTimeStringByMilliSec(record.totalMilliSec);
		SetLabelText(ctrl2, UI.LBL_GROUP_TIME, text2);
	}

	private void UpdateRank()
	{
		int num = record.userRank;
		string text = num.ToString();
		for (int i = 0; i < RankingNumUIs.Length; i++)
		{
			SetActive((Enum)RankingNumUIs[i], is_visible: false);
		}
		if (num <= 0)
		{
			SetActive((Enum)UI.SPR_RANK, is_visible: false);
			SetActive((Enum)UI.SPR_OUT_OF_RANK, is_visible: true);
			return;
		}
		SetActive((Enum)UI.SPR_RANK, is_visible: true);
		SetActive((Enum)UI.SPR_OUT_OF_RANK, is_visible: false);
		int num2 = (RankingNumUIs.Length - text.Length) / 2;
		for (int j = 0; j < text.Length; j++)
		{
			int num3 = int.Parse(text[j].ToString());
			if (j >= RankingNumUIs.Length)
			{
				break;
			}
			int num4 = j + num2;
			SetSprite(GetCtrl(RankingNumUIs[num4]), RankingNumbers[num3]);
			SetActive((Enum)RankingNumUIs[num4], is_visible: true);
		}
	}

	private void OnQuery_FOLLOWER()
	{
		GameSection.SetEventData(new object[2]
		{
			eventData,
			userRank
		});
	}

	private void OnQuery_WORLD()
	{
		GameSection.SetEventData(new object[2]
		{
			eventData,
			userRank
		});
	}

	private void OnQuery_LAST()
	{
		GameSection.SetEventData(new object[2]
		{
			eventData,
			userRank
		});
	}

	private void OnQuery_LEGEND()
	{
		GameSection.SetEventData(new object[2]
		{
			eventData,
			userRank
		});
	}

	private bool IsRankingJoin()
	{
		return MonoBehaviourSingleton<UserInfoManager>.I.isJoinedArenaRanking;
	}
}
