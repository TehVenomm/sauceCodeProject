using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestSearchRoomCondition : QuestSearchRoomConditionBase
{
	public enum UI
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
		BTN_NO_ELEMENT,
		POP_TARGET_MIN_LEVEL,
		POP_TARGET_MAX_LEVEL,
		LBL_TARGET_MIN_LEVEL,
		LBL_TARGET_MAX_LEVEL,
		OBJ_SEARCH,
		OBJ_MY_SEARCH
	}

	[Flags]
	private enum FILTER
	{
		NORMAL = 0x1,
		ORDER = 0x4,
		GATE = 0x10,
		ALL = 0x15
	}

	public class SearchRequestParam
	{
		public int order;

		public int rarityBit;

		public int elementBit;

		public int enemyMinLevelIndex;

		public int enemyLevelMin;

		public int enemyMaxLevelIndex;

		public int enemyLevelMax;

		public int enemySpeciesIndex;

		public string targetEnemySpeciesName;

		public int questTypeBit;

		public SearchRequestParam()
		{
			order = 0;
			rarityBit = 8388607;
			elementBit = 8388607;
			enemyMinLevelIndex = 0;
			enemyMaxLevelIndex = 0;
			enemySpeciesIndex = 0;
			questTypeBit = 21;
		}

		public int GetEnemySpeciesId(string name)
		{
			return Singleton<GachaSearchEnemyTable>.I.GetEnemySpeciesId(name);
		}

		public bool IsMatchRarity(QuestItemInfo item)
		{
			for (int i = 0; i < rarityButton.Length; i++)
			{
				int num = i;
				if ((rarityBit & (1 << num)) > 0 && item.infoData.questData.tableData.rarity == (RARITY_TYPE)num)
				{
					return true;
				}
			}
			return false;
		}

		public bool IsMatchElement(QuestItemInfo item)
		{
			QuestTable.QuestTableData tableData = item.infoData.questData.tableData;
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)tableData.GetMainEnemyID());
			for (int i = 0; i < elementButton.Length; i++)
			{
				int num = i;
				if ((elementBit & (1 << num)) > 0 && enemyData.element == (ELEMENT_TYPE)num)
				{
					return true;
				}
			}
			return false;
		}

		public virtual bool IsMatchLevel(QuestItemInfo item)
		{
			QuestTable.QuestTableData tableData = item.infoData.questData.tableData;
			if (enemyLevelMin <= tableData.GetMainEnemyLv() && tableData.GetMainEnemyLv() <= enemyLevelMax)
			{
				return true;
			}
			return false;
		}

		public bool IsMatchEnemySpecies(QuestItemInfo item)
		{
			int enemySpeciesId = GetEnemySpeciesId(targetEnemySpeciesName);
			if (enemySpeciesId > 0)
			{
				EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)item.infoData.questData.tableData.GetMainEnemyID());
				if (enemyData.enemySpecies == enemySpeciesId)
				{
					return true;
				}
				return false;
			}
			return true;
		}
	}

	protected static readonly UI[] rarityButton = new UI[7]
	{
		UI.BTN_N,
		UI.BTN_HN,
		UI.BTN_R,
		UI.BTN_HR,
		UI.BTN_SR,
		UI.BTN_HSR,
		UI.BTN_SSR
	};

	protected static readonly UI[] elementButton = new UI[7]
	{
		UI.BTN_FIRE,
		UI.BTN_WATER,
		UI.BTN_THUNDER,
		UI.BTN_SOIL,
		UI.BTN_LIGHT,
		UI.BTN_DARK,
		UI.BTN_NO_ELEMENT
	};

	protected SearchRequestParam searchRequest = new SearchRequestParam();

	protected GachaSearchEnemyTable.GachaSearchEnemyData[] sortedAllSpeciesData;

	protected GachaSearchEnemyTable.GachaSearchEnemyData[] sortedTargetSpeciesData;

	protected List<string> enemyLevelNames;

	protected List<int> enemyLevelList;

	protected List<string> enemySpeciesNames;

	private Transform speciesPopup;

	private Transform minLevelPopup;

	private Transform maxLevelPopup;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "GachaSearchEnemyTable";
		}
	}

	public override void Initialize()
	{
		LoadSearchRequestParam();
		CopySearchRequestParam();
		SetupRarityButtons();
		SetupElementButtons();
		sortedAllSpeciesData = (sortedTargetSpeciesData = Singleton<GachaSearchEnemyTable>.I.GetSortedGachaSearchEnemyData());
		CreateEnemySpeciesPopText();
		for (int i = 0; i < enemySpeciesNames.Count; i++)
		{
			if (enemySpeciesNames[i] == searchRequest.targetEnemySpeciesName)
			{
				searchRequest.enemySpeciesIndex = i;
				break;
			}
		}
		enemyLevelNames = new List<string>();
		enemyLevelList = new List<int>();
		CreateEnemyLevelPopText();
		bool flag = false;
		for (int j = 0; j < enemyLevelList.Count; j++)
		{
			if (enemyLevelList[j] == searchRequest.enemyLevelMin)
			{
				searchRequest.enemyMinLevelIndex = j;
			}
			if (enemyLevelList[j] == searchRequest.enemyLevelMax)
			{
				flag = true;
				searchRequest.enemyMaxLevelIndex = j;
			}
		}
		if (!flag)
		{
			searchRequest.enemyLevelMax = enemyLevelList[enemyLevelList.Count - 1];
			searchRequest.enemyMaxLevelIndex = enemyLevelList.Count - 1;
		}
		SetActive((Enum)UI.OBJ_SEARCH, true);
		SetActive((Enum)UI.OBJ_MY_SEARCH, false);
		GameSection.SetEventData(false);
		base.Initialize();
	}

	protected override void LoadSearchRequestParam()
	{
		if (MonoBehaviourSingleton<PartyManager>.I.searchRequestTemp == null)
		{
			MonoBehaviourSingleton<PartyManager>.I.SetSearchRequestFromPrefs();
		}
	}

	protected override void CopySearchRequestParam()
	{
		SearchRequestParam searchRequestParam = MonoBehaviourSingleton<PartyManager>.I.searchRequest;
		searchRequest.order = searchRequestParam.order;
		searchRequest.rarityBit = searchRequestParam.rarityBit;
		searchRequest.elementBit = searchRequestParam.elementBit;
		searchRequest.enemyLevelMin = searchRequestParam.enemyLevelMin;
		searchRequest.enemyLevelMax = searchRequestParam.enemyLevelMax;
		searchRequest.enemyMinLevelIndex = searchRequestParam.enemyMinLevelIndex;
		searchRequest.enemyMaxLevelIndex = searchRequestParam.enemyMaxLevelIndex;
		searchRequest.targetEnemySpeciesName = searchRequestParam.targetEnemySpeciesName;
		searchRequest.questTypeBit = searchRequestParam.questTypeBit;
	}

	protected void SetupRarityButtons()
	{
		for (int i = 0; i < rarityButton.Length; i++)
		{
			SetEvent((Enum)rarityButton[i], "RARITY", i);
		}
	}

	protected void SetupElementButtons()
	{
		for (int i = 0; i < elementButton.Length; i++)
		{
			SetEvent((Enum)elementButton[i], "ELEMENT", i);
		}
	}

	protected void CreateEnemySpeciesPopText()
	{
		FilterSpeciesPopupOnRarity(null);
		FilterSpeciesPopupOnElement(sortedTargetSpeciesData);
		enemySpeciesNames = Singleton<GachaSearchEnemyTable>.I.GetGachaSearchEnemyNames(sortedTargetSpeciesData);
		enemySpeciesNames.Insert(0, base.sectionData.GetText("NO_CONDITION"));
	}

	protected virtual void CreateEnemyLevelPopText()
	{
		int pARTY_SEARCH_QUEST_LEVEL_MAX = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.PARTY_SEARCH_QUEST_LEVEL_MAX;
		enemyLevelList = new List<int>();
		int num = pARTY_SEARCH_QUEST_LEVEL_MAX / 10 + 1;
		for (int i = 0; i < num; i++)
		{
			if (i == 0)
			{
				enemyLevelList.Add(1);
			}
			else
			{
				enemyLevelList.Add(10 * i);
			}
		}
		for (int j = 0; j < enemyLevelList.Count; j++)
		{
			enemyLevelNames.Add(enemyLevelList[j].ToString());
		}
	}

	public override void UpdateUI()
	{
		UpdateRarityToggles();
		UpdateElementToggles();
		UpdateEnemyMinLevel();
		UpdateEnemyMaxLevel();
		UpdateEnemySpecies();
	}

	private void UpdateRarityToggles()
	{
		Array values = Enum.GetValues(typeof(RARITY_TYPE));
		foreach (int item in values)
		{
			SetToggle(GetRairtyToggleTransform(item), (searchRequest.rarityBit & (1 << item)) != 0);
		}
	}

	private Transform GetRairtyToggleTransform(int rarity)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		return GetCtrl(rarityButton[rarity]).get_parent();
	}

	private void UpdateElementToggles()
	{
		Array values = Enum.GetValues(typeof(ELEMENT_TYPE));
		foreach (int item in values)
		{
			Transform elementToggleTransform = GetElementToggleTransform(item);
			if (elementToggleTransform != null)
			{
				SetToggle(elementToggleTransform, (searchRequest.elementBit & (1 << item)) != 0);
			}
		}
	}

	private Transform GetElementToggleTransform(int elementIndex)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		if (elementIndex >= elementButton.Length || elementIndex < 0)
		{
			return null;
		}
		return GetCtrl(elementButton[elementIndex]).get_parent();
	}

	private void UpdateEnemyMinLevel()
	{
		int enemyMinLevelIndex = searchRequest.enemyMinLevelIndex;
		SetLabelText((Enum)UI.LBL_TARGET_MIN_LEVEL, enemyLevelNames[enemyMinLevelIndex]);
	}

	private void UpdateEnemyMaxLevel()
	{
		int enemyMaxLevelIndex = searchRequest.enemyMaxLevelIndex;
		SetLabelText((Enum)UI.LBL_TARGET_MAX_LEVEL, enemyLevelNames[enemyMaxLevelIndex]);
	}

	private void UpdateEnemySpecies()
	{
		FilterSpeciesPopupOnRarity(null);
		FilterSpeciesPopupOnElement(sortedTargetSpeciesData);
		UpdateSelectingEnemySpecies();
		int enemySpeciesIndex = searchRequest.enemySpeciesIndex;
		SetLabelText((Enum)UI.LBL_TARGET_ENEMY_TYPE, enemySpeciesNames[enemySpeciesIndex]);
	}

	private void FilterSpeciesPopupOnRarity(GachaSearchEnemyTable.GachaSearchEnemyData[] targetData = null)
	{
		if (targetData == null)
		{
			targetData = sortedAllSpeciesData;
		}
		sortedTargetSpeciesData = Singleton<GachaSearchEnemyTable>.I.GetEnemyDataOnRairtyFlag(targetData, searchRequest.rarityBit);
		enemySpeciesNames = Singleton<GachaSearchEnemyTable>.I.GetGachaSearchEnemyNames(sortedTargetSpeciesData);
		enemySpeciesNames.Insert(0, base.sectionData.GetText("NO_CONDITION"));
	}

	private void FilterSpeciesPopupOnElement(GachaSearchEnemyTable.GachaSearchEnemyData[] targetData = null)
	{
		if (targetData == null)
		{
			targetData = sortedAllSpeciesData;
		}
		sortedTargetSpeciesData = Singleton<GachaSearchEnemyTable>.I.GetEnemyDataOnElementFlag(targetData, searchRequest.elementBit);
		enemySpeciesNames = Singleton<GachaSearchEnemyTable>.I.GetGachaSearchEnemyNames(sortedTargetSpeciesData);
		enemySpeciesNames.Insert(0, base.sectionData.GetText("NO_CONDITION"));
	}

	private void UpdateSelectingEnemySpecies()
	{
		if (!string.IsNullOrEmpty(searchRequest.targetEnemySpeciesName))
		{
			bool flag = false;
			for (int i = 0; i < enemySpeciesNames.Count; i++)
			{
				if (enemySpeciesNames[i] == searchRequest.targetEnemySpeciesName)
				{
					searchRequest.enemySpeciesIndex = i;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				searchRequest.enemySpeciesIndex = 0;
				searchRequest.targetEnemySpeciesName = null;
			}
		}
	}

	private void OnQuery_RARITY()
	{
		int num = (int)GameSection.GetEventData();
		searchRequest.rarityBit ^= 1 << num;
		RefreshUI();
	}

	private void OnQuery_ELEMENT()
	{
		int num = (int)GameSection.GetEventData();
		searchRequest.elementBit ^= 1 << num;
		RefreshUI();
	}

	private void OnQuery_TARGET_ENEMY_TYPE()
	{
		ShowEnemySpeciesPopup();
	}

	private void ShowEnemySpeciesPopup()
	{
		if (speciesPopup == null)
		{
			speciesPopup = Realizes("ScrollablePopupList", GetCtrl(UI.POP_TARGET_ENEMY_TYPE), false);
		}
		if (!(speciesPopup == null))
		{
			bool[] array = new bool[enemySpeciesNames.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = true;
			}
			int select_index = 0;
			for (int j = 0; j < enemySpeciesNames.Count; j++)
			{
				if (enemySpeciesNames[j] == searchRequest.targetEnemySpeciesName)
				{
					searchRequest.enemySpeciesIndex = j;
					select_index = searchRequest.enemySpeciesIndex;
					break;
				}
			}
			UIScrollablePopupList.CreatePopup(speciesPopup, GetCtrl(UI.POP_TARGET_ENEMY_TYPE), 8, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, enemySpeciesNames.ToArray(), array, select_index, delegate(int index)
			{
				searchRequest.enemySpeciesIndex = index;
				searchRequest.targetEnemySpeciesName = enemySpeciesNames[index];
				RefreshUI();
			});
		}
	}

	private void OnQuery_TARGET_MIN_LEVEL()
	{
		ShowMinLevelPopup();
	}

	private void ShowMinLevelPopup()
	{
		if (minLevelPopup == null)
		{
			minLevelPopup = Realizes("ScrollablePopupList", GetCtrl(UI.POP_TARGET_MIN_LEVEL), false);
		}
		if (!(minLevelPopup == null))
		{
			bool[] array = new bool[enemyLevelNames.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (i <= searchRequest.enemyMaxLevelIndex);
			}
			int enemyMinLevelIndex = searchRequest.enemyMinLevelIndex;
			UIScrollablePopupList.CreatePopup(minLevelPopup, GetCtrl(UI.POP_TARGET_MIN_LEVEL), 6, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, enemyLevelNames.ToArray(), array, enemyMinLevelIndex, delegate(int index)
			{
				searchRequest.enemyMinLevelIndex = index;
				searchRequest.enemyLevelMin = enemyLevelList[index];
				RefreshUI();
			});
		}
	}

	private void OnQuery_TARGET_MAX_LEVEL()
	{
		ShowMaxLevelPopup();
	}

	private void ShowMaxLevelPopup()
	{
		if (maxLevelPopup == null)
		{
			maxLevelPopup = Realizes("ScrollablePopupList", GetCtrl(UI.POP_TARGET_MAX_LEVEL), false);
		}
		if (!(maxLevelPopup == null))
		{
			bool[] array = new bool[enemyLevelNames.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (i >= searchRequest.enemyMinLevelIndex);
			}
			int enemyMaxLevelIndex = searchRequest.enemyMaxLevelIndex;
			UIScrollablePopupList.CreatePopup(maxLevelPopup, GetCtrl(UI.POP_TARGET_MAX_LEVEL), 6, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, enemyLevelNames.ToArray(), array, enemyMaxLevelIndex, delegate(int index)
			{
				searchRequest.enemyMaxLevelIndex = index;
				searchRequest.enemyLevelMax = enemyLevelList[index];
				RefreshUI();
			});
		}
	}

	protected override void OnQuery_SEARCH()
	{
		FixBit();
		if (searchRequest.rarityBit == 0)
		{
			GameSection.ChangeEvent("NOT_RARITY", null);
		}
		else if (searchRequest.elementBit == 0)
		{
			GameSection.ChangeEvent("NOT_ELEMENT", null);
		}
		else
		{
			base.OnQuery_SEARCH();
		}
	}

	protected override void SetCondition()
	{
		searchRequest.order = 1;
		MonoBehaviourSingleton<PartyManager>.I.SetSearchRequest(searchRequest);
	}

	protected override void SendSearch()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<PartyManager>.I.SendSearch(delegate(bool is_success, Error err)
		{
			if (!is_success && err == Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST)
			{
				OnNotFoundQuest();
			}
			GameSection.ResumeEvent(true, null);
		}, true);
	}

	protected override void OnQuery_MATCHING()
	{
		FixBit();
		if (searchRequest.rarityBit == 0)
		{
			GameSection.ChangeEvent("NOT_RARITY", null);
		}
		else if (searchRequest.elementBit == 0)
		{
			GameSection.ChangeEvent("NOT_ELEMENT", null);
		}
		else
		{
			base.OnQuery_MATCHING();
		}
	}

	protected override void SendRandomMatching()
	{
		GameSection.SetEventData(new object[1]
		{
			false
		});
		GameSection.StayEvent();
		MonoBehaviourSingleton<PartyManager>.I.SendSearchRandomMatching(delegate(bool is_success, Error err)
		{
			if (!is_success)
			{
				OnNotFoundMatchingParty();
			}
			GameSection.ResumeEvent(true, null);
		});
	}

	protected void FixBit()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		for (int i = 0; i < rarityButton.Length; i++)
		{
			int num2 = i;
			if ((searchRequest.rarityBit & (1 << num2)) != 0 && GetCtrl(rarityButton[i]).get_gameObject().get_activeInHierarchy())
			{
				num |= 1 << (num2 & 0x1F);
			}
		}
		searchRequest.rarityBit = num;
		int num3 = 0;
		for (int j = 0; j < elementButton.Length; j++)
		{
			int num4 = j;
			if ((searchRequest.elementBit & (1 << num4)) != 0 && GetCtrl(elementButton[j]).get_gameObject().get_activeInHierarchy())
			{
				num3 |= 1 << (num4 & 0x1F);
			}
		}
		searchRequest.elementBit = num3;
	}

	protected virtual void OnQuery_SPECIES_SEARCH_REQUEST()
	{
		int num = (int)GameSection.GetEventData();
		SearchRequestParam searchRequestParam = new SearchRequestParam();
		searchRequestParam.enemySpeciesIndex = 0;
		searchRequestParam.targetEnemySpeciesName = null;
		if (Singleton<GachaSearchEnemyTable>.IsValid())
		{
			GachaSearchEnemyTable.GachaSearchEnemyData[] sortedGachaSearchEnemyData = Singleton<GachaSearchEnemyTable>.I.GetSortedGachaSearchEnemyData();
			for (int i = 0; i < sortedGachaSearchEnemyData.Length; i++)
			{
				if (num == sortedGachaSearchEnemyData[i].id)
				{
					searchRequestParam.targetEnemySpeciesName = sortedGachaSearchEnemyData[i].name;
					break;
				}
			}
		}
		searchRequestParam.order = 1;
		MonoBehaviourSingleton<PartyManager>.I.SetSearchRequestTemp(searchRequestParam);
		GameSection.StayEvent();
		MonoBehaviourSingleton<PartyManager>.I.SendSearch(delegate(bool is_success, Error err)
		{
			if (!is_success && err == Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST)
			{
				OnNotFoundQuest();
			}
			GameSection.ResumeEvent(true, null);
		}, false);
	}
}
