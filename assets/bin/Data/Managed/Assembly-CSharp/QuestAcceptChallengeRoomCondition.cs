using Network;
using System;
using UnityEngine;

public class QuestAcceptChallengeRoomCondition : QuestSearchRoomCondition
{
	public new enum UI
	{
		BTN_N,
		BTN_HN,
		BTN_R,
		BTN_HR,
		BTN_SR,
		BTN_HSR,
		BTN_SSR,
		TGL_ORDER,
		TGL_EVENT,
		TGL_STORY,
		POP_TARGET_ENEMY_TYPE,
		LBL_TARGET_ENEMY_TYPE,
		BTN_FIRE,
		BTN_WATER,
		BTN_THUNDER,
		BTN_SOIL,
		BTN_LIGHT,
		BTN_DARK,
		POP_TARGET_MIN_LEVEL,
		POP_TARGET_MAX_LEVEL,
		LBL_TARGET_MIN_LEVEL,
		LBL_TARGET_MAX_LEVEL,
		OBJ_SEARCH,
		OBJ_MY_SEARCH,
		POP_TARGET_LEVEL,
		LBL_TARGET_LEVEL
	}

	public class ChallengeSearchRequestParam : SearchRequestParam
	{
		public int enemyLevelIndex;

		public int enemyLevel;

		public ChallengeSearchRequestParam()
		{
			enemyLevelIndex = 0;
			enemyLevel = 0;
		}

		public override bool IsMatchLevel(QuestItemInfo item)
		{
			QuestTable.QuestTableData tableData = item.infoData.questData.tableData;
			if (enemyLevel == tableData.GetMainEnemyLv())
			{
				return true;
			}
			return false;
		}
	}

	private int maxLevel;

	private int maxLevelIndex;

	private Transform levelPopup;

	private ChallengeSearchRequestParam challengeRequest;

	public override void Initialize()
	{
		searchRequest = new ChallengeSearchRequestParam();
		challengeRequest = (searchRequest as ChallengeSearchRequestParam);
		base.Initialize();
		maxLevel = MonoBehaviourSingleton<UserInfoManager>.I.GetEnemyLevelFromUserLevel();
		bool flag = false;
		for (int i = 0; i < enemyLevelList.Count; i++)
		{
			if (enemyLevelList[i] == challengeRequest.enemyLevel)
			{
				challengeRequest.enemyLevelIndex = i;
			}
			if (enemyLevelList[i] == maxLevel)
			{
				maxLevelIndex = i;
				flag = true;
			}
		}
		if (!flag)
		{
			challengeRequest.enemyLevel = enemyLevelList[0];
			challengeRequest.enemyLevelIndex = 0;
			maxLevel = enemyLevelList[0];
			maxLevelIndex = 0;
		}
		UpdateEnemyLevel();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		UpdateEnemyLevel();
	}

	protected virtual ChallengeSearchRequestParam GetInitChallengeSearchParam()
	{
		ChallengeSearchRequestParam challengeSearchRequestParam = GameSection.GetEventData() as ChallengeSearchRequestParam;
		if (challengeSearchRequestParam == null)
		{
			challengeSearchRequestParam = new ChallengeSearchRequestParam();
		}
		return challengeSearchRequestParam;
	}

	protected override void LoadSearchRequestParam()
	{
	}

	protected override void CopySearchRequestParam()
	{
		ChallengeSearchRequestParam initChallengeSearchParam = GetInitChallengeSearchParam();
		challengeRequest.order = initChallengeSearchParam.order;
		challengeRequest.rarityBit = initChallengeSearchParam.rarityBit;
		challengeRequest.elementBit = initChallengeSearchParam.elementBit;
		challengeRequest.enemyLevelMin = initChallengeSearchParam.enemyLevelMin;
		challengeRequest.enemyLevelMax = initChallengeSearchParam.enemyLevelMax;
		challengeRequest.targetEnemySpeciesName = initChallengeSearchParam.targetEnemySpeciesName;
		challengeRequest.questTypeBit = initChallengeSearchParam.questTypeBit;
		challengeRequest.enemyLevel = initChallengeSearchParam.enemyLevel;
	}

	protected override void CreateEnemyLevelPopText()
	{
		int qUEST_ITEM_LEVEL_MAX = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.QUEST_ITEM_LEVEL_MAX;
		int num = qUEST_ITEM_LEVEL_MAX / 10 + 1;
		for (int i = 1; i < num; i++)
		{
			enemyLevelList.Add(10 * i);
		}
		for (int j = 0; j < enemyLevelList.Count; j++)
		{
			enemyLevelNames.Add(enemyLevelList[j].ToString());
		}
	}

	private void UpdateEnemyLevel()
	{
		int enemyLevelIndex = challengeRequest.enemyLevelIndex;
		SetLabelText((Enum)UI.LBL_TARGET_LEVEL, enemyLevelNames[enemyLevelIndex]);
	}

	private void OnQuery_TARGET_LEVEL()
	{
		ShowLevelPopup();
	}

	private void ShowLevelPopup()
	{
		if (levelPopup == null)
		{
			levelPopup = Realizes("ScrollablePopupList", GetCtrl(UI.POP_TARGET_LEVEL), false);
		}
		if (!(levelPopup == null))
		{
			bool[] array = new bool[enemyLevelNames.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (i <= maxLevelIndex);
			}
			int enemyLevelIndex = challengeRequest.enemyLevelIndex;
			UIScrollablePopupList.CreatePopup(levelPopup, GetCtrl(UI.POP_TARGET_LEVEL), 5, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, enemyLevelNames.ToArray(), array, enemyLevelIndex, delegate(int index)
			{
				challengeRequest.enemyLevelIndex = index;
				challengeRequest.enemyLevel = enemyLevelList[index];
				RefreshUI();
			});
		}
	}

	protected override void OnQuery_SEARCH()
	{
		FixBit();
		if (challengeRequest.rarityBit == 0)
		{
			GameSection.ChangeEvent("NOT_RARITY", null);
		}
		else if (challengeRequest.elementBit == 0)
		{
			GameSection.ChangeEvent("NOT_ELEMENT", null);
		}
		else
		{
			challengeRequest.order = 1;
			GameSection.SetEventData(challengeRequest);
			base.OnQuery_SEARCH();
		}
	}

	protected unsafe override void SendSearch()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<QuestManager>.I.SendGetChallengeList(challengeRequest, new Action<bool, Error>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), true);
	}

	protected override void OnQuery_SPECIES_SEARCH_REQUEST()
	{
		int num = (int)GameSection.GetEventData();
		challengeRequest.enemySpeciesIndex = 0;
		challengeRequest.targetEnemySpeciesName = null;
		if (Singleton<GachaSearchEnemyTable>.IsValid())
		{
			GachaSearchEnemyTable.GachaSearchEnemyData[] sortedGachaSearchEnemyData = Singleton<GachaSearchEnemyTable>.I.GetSortedGachaSearchEnemyData();
			for (int i = 0; i < sortedGachaSearchEnemyData.Length; i++)
			{
				if (num == sortedGachaSearchEnemyData[i].id)
				{
					challengeRequest.targetEnemySpeciesName = sortedGachaSearchEnemyData[i].name;
					break;
				}
			}
		}
		challengeRequest.order = 1;
		GameSection.SetEventData(challengeRequest);
		SendSearch();
	}
}
