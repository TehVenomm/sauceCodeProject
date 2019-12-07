using Network;
using System.Collections;
using UnityEngine;

public class FriendArenaRankingInfoScore : GameSection
{
	private enum UI
	{
		OBJ_GROUP_A,
		OBJ_GROUP_B,
		OBJ_GROUP_C,
		OBJ_GROUP_D,
		OBJ_GROUP_E,
		OBJ_TOTAL,
		LBL_SCORE,
		LBL_TIME_DEFAULT
	}

	private static readonly UI[] GROUPSCORES = new UI[5]
	{
		UI.OBJ_GROUP_A,
		UI.OBJ_GROUP_B,
		UI.OBJ_GROUP_C,
		UI.OBJ_GROUP_D,
		UI.OBJ_GROUP_E
	};

	private ArenaUserRecordModel.Param record;

	private Network.EventData eventData;

	private int userId;

	public override void Initialize()
	{
		object[] array = (object[])GameSection.GetEventData();
		eventData = (array[0] as Network.EventData);
		userId = (int)array[1];
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		bool isFinishGetRecord = false;
		MonoBehaviourSingleton<QuestManager>.I.SendGetArenaUserRecord(userId, eventData.eventId, delegate(bool b, ArenaUserRecordModel.Param result)
		{
			isFinishGetRecord = true;
			record = result;
		});
		while (!isFinishGetRecord)
		{
			yield return null;
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		UpdateScores();
		base.UpdateUI();
	}

	private void UpdateScores()
	{
		int i = 0;
		for (int count = record.clearMilliSecList.Count; i < count; i++)
		{
			Transform ctrl = GetCtrl(GROUPSCORES[i]);
			string text = QuestUtility.CreateTimeStringByMilliSec(record.clearMilliSecList[i]);
			bool flag = QuestUtility.IsDefaultArenaTime(record.clearMilliSecList[i]);
			SetActive(ctrl, UI.LBL_SCORE, !flag);
			SetActive(ctrl, UI.LBL_TIME_DEFAULT, flag);
			if (flag)
			{
				SetLabelText(ctrl, UI.LBL_TIME_DEFAULT, text);
			}
			else
			{
				SetLabelText(ctrl, UI.LBL_SCORE, text);
			}
		}
		Transform ctrl2 = GetCtrl(UI.OBJ_TOTAL);
		string text2 = QuestUtility.CreateTimeStringByMilliSec(record.totalMilliSec);
		SetLabelText(ctrl2, UI.LBL_SCORE, text2);
	}
}
