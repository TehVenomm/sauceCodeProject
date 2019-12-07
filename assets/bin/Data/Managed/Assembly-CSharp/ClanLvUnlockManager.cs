using Network;
using System.Collections.Generic;
using UnityEngine;

public class ClanLvUnlockManager : MonoBehaviour
{
	public enum ClanUnlockLv
	{
		STAGE_LV1 = 1,
		STAGE_LV2 = 10,
		STAGE_LV3 = 0xF,
		QUEST_BOARD = 2,
		NOTICE_BOARD = 5
	}

	public static readonly string CLAN_STAGE_LV1_NAME = "HP007D_01";

	public static readonly string CLAN_STAGE_LV2_NAME = "HP007D_02";

	public static readonly string CLAN_STAGE_LV3_NAME = "HP007D_03";

	private UserClanData m_clanData;

	private void Start()
	{
		m_clanData = MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData;
		UnLockObject();
	}

	private void UnLockObject()
	{
		LockItem(ClanUnlockLv.QUEST_BOARD, GameObject.Find("ClanQuestBoard"), GameObject.Find("ClanQuestBoardA"));
		LockItem(ClanUnlockLv.NOTICE_BOARD, GameObject.Find("HP007_board01"), GameObject.Find("HP007_board02"), GameObject.Find("HP007_board03"), GameObject.Find("NoticeBoard"), GameObject.Find("NoticeBoardA"));
	}

	private void LockItem(ClanUnlockLv item, params GameObject[] objs)
	{
		if (m_clanData == null)
		{
			Debug.LogError("m_clanData is null");
		}
		else
		{
			if (m_clanData.level >= (int)item)
			{
				return;
			}
			foreach (GameObject gameObject in objs)
			{
				if (!(gameObject == null))
				{
					gameObject.SetActive(value: false);
				}
			}
		}
	}

	public static string CallGetLoadStageName(int lv)
	{
		if (lv == 0)
		{
			lv = 1;
		}
		string result = string.Empty;
		List<ClanUnlockLv> list = new List<ClanUnlockLv>();
		list.Add(ClanUnlockLv.STAGE_LV1);
		list.Add(ClanUnlockLv.STAGE_LV2);
		list.Add(ClanUnlockLv.STAGE_LV3);
		list.RemoveAll((ClanUnlockLv s) => (int)s > lv);
		list.Sort((ClanUnlockLv a, ClanUnlockLv b) => b - a);
		switch (list[0])
		{
		case ClanUnlockLv.STAGE_LV1:
			result = CLAN_STAGE_LV1_NAME;
			break;
		case ClanUnlockLv.STAGE_LV2:
			result = CLAN_STAGE_LV2_NAME;
			break;
		case ClanUnlockLv.STAGE_LV3:
			result = CLAN_STAGE_LV3_NAME;
			break;
		}
		return result;
	}
}
