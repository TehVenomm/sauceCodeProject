using System;
using UnityEngine;

public class QuestAcceptOrderCounterCondition : QuestSearchRoomCondition
{
	public override void Initialize()
	{
		base.Initialize();
		SetActive((Enum)UI.OBJ_SEARCH, false);
		SetActive((Enum)UI.OBJ_MY_SEARCH, true);
	}

	protected override void CopySearchRequestParam()
	{
	}

	protected override void LoadSearchRequestParam()
	{
		SetSearchRequestFromPrefs();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
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
			SaveSettingsMyGachaSearch();
			searchRequest.order = 1;
			GameSection.SetEventData(searchRequest);
		}
	}

	public void SaveSettingsMyGachaSearch()
	{
		PlayerPrefs.SetInt("MY_GACHA_SEARCH_RAIRTY_KEY", searchRequest.rarityBit);
		PlayerPrefs.SetInt("MY_GACHA_SEARCH_ELEMENT_KEY", searchRequest.elementBit);
		PlayerPrefs.SetInt("MY_GACHA_SEARCH_LEVEL_MIN_KEY", searchRequest.enemyLevelMin);
		PlayerPrefs.SetInt("MY_GACHA_SEARCH_LEVEL_MAX_KEY", searchRequest.enemyLevelMax);
		if (!string.IsNullOrEmpty(searchRequest.targetEnemySpeciesName))
		{
			PlayerPrefs.SetString("MY_GACHA_SEARCH_SPECIES_KEY", searchRequest.targetEnemySpeciesName);
		}
		PlayerPrefs.Save();
	}

	public void SetSearchRequestFromPrefs()
	{
		searchRequest = new SearchRequestParam();
		searchRequest.rarityBit = PlayerPrefs.GetInt("MY_GACHA_SEARCH_RAIRTY_KEY", 8388607);
		searchRequest.elementBit = PlayerPrefs.GetInt("MY_GACHA_SEARCH_ELEMENT_KEY", 8388607);
		searchRequest.enemyLevelMin = PlayerPrefs.GetInt("MY_GACHA_SEARCH_LEVEL_MIN_KEY", 1);
		int num = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.PARTY_SEARCH_QUEST_LEVEL_MAX;
		if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.PARTY_SEARCH_QUEST_EXTRA_LEVEL_MAX > 0)
		{
			num = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.PARTY_SEARCH_QUEST_EXTRA_LEVEL_MAX;
		}
		searchRequest.enemyLevelMax = PlayerPrefs.GetInt("MY_GACHA_SEARCH_LEVEL_MAX_KEY", num);
		searchRequest.targetEnemySpeciesName = PlayerPrefs.GetString("MY_GACHA_SEARCH_SPECIES_KEY", (string)null);
	}
}
