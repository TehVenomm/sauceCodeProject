using gogame;
using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GoWrapManager : MonoBehaviourSingleton<GoWrapManager>, IGoWrapDelegate
{
	private void Start()
	{
		GoWrap.INSTANCE.setDelegate(this);
		GoWrap.INSTANCE.initGoWrap(base.name);
		base.gameObject.AddComponent<GoWrapComponent>();
		List<string> list = new List<string>();
		list.Add("unity");
		GoWrap.INSTANCE.setCustomUrlSchemes(list);
	}

	public void setDeviceToken(byte[] token)
	{
		GoWrap.INSTANCE.setIOSDeviceToken(token);
	}

	public void ShowMenu()
	{
		GoWrap.INSTANCE.showMenu();
	}

	public void setGUID(string guid)
	{
		GoWrap.INSTANCE.setGuid(guid);
	}

	public void trackTutorialStep(TRACK_TUTORIAL_STEP_BIT stepName, string category)
	{
		if (!GameSaveData.instance.IsPushedTrackTutorialBit(stepName))
		{
			string text = stepName.ToString();
			if (PlayerPrefs.GetInt("track_" + text, 0) == 0)
			{
				GoWrap.INSTANCE.trackEvent(text, category);
				GameSaveData.instance.SetPushedTrackTutorialBit(stepName);
			}
		}
	}

	public void trackTutorialStep(TRACK_TUTORIAL_STEP_BIT stepName, string category, Dictionary<string, object> values)
	{
		if (!GameSaveData.instance.IsPushedTrackTutorialBit(stepName))
		{
			string text = stepName.ToString();
			if (PlayerPrefs.GetInt("track_" + text, 0) == 0)
			{
				GoWrap.INSTANCE.trackEvent(text, category, values);
				GameSaveData.instance.SetPushedTrackTutorialBit(stepName);
			}
		}
	}

	public void SendStatusTracking(TRACK_TUTORIAL_STEP_BIT _stepName, string _category, Dictionary<string, object> dictionary = null, Action<bool> call_back = null)
	{
		Protocol.Force(delegate
		{
			Debug.LogWarning("SendStatusTracking Called!");
			AnalyticTrackingPointModel.RequestSendForm postData = new AnalyticTrackingPointModel.RequestSendForm
			{
				name = _stepName.ToString(),
				category = _category
			};
			Protocol.SendAsync(AnalyticTrackingPointModel.URL, postData, delegate(AnalyticTrackingPointModel ret)
			{
				bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
				if (call_back != null)
				{
					call_back(obj);
				}
				Debug.LogWarning("SendStatusTracking Bit Success!");
			});
		});
	}

	public void trackEvent(string name, string category)
	{
		GoWrap.INSTANCE.trackEvent(name, category);
	}

	public void trackEvent(string name, string category, Dictionary<string, object> values)
	{
		GoWrap.INSTANCE.trackEvent(name, category, values);
	}

	public void trackPurchase(string productId, string currencyCode, double price, string purchaseData, string signature)
	{
		GoWrap.INSTANCE.trackPurchase(productId, currencyCode, price, purchaseData, signature);
	}

	public void trackQuestStart(uint questID)
	{
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(questID);
		if (questData != null)
		{
			if (questData.eventId > 0)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("quest_id", questID);
				dictionary.Add("user_level", MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level);
				dictionary.Add("boss_level", questData.enemyLv[0]);
				dictionary.Add("boss_id", questData.enemyID[0]);
				GoWrap.INSTANCE.trackEvent("expedition_start", "Gameplay", dictionary);
			}
			else if (questData.questType == QUEST_TYPE.ORDER)
			{
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
				dictionary2.Add("user_level", MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level);
				dictionary2.Add("boss_level", questData.enemyLv[0]);
				dictionary2.Add("boss_id", questData.enemyID[0]);
				dictionary2.Add("is_meeting_room", LoungeMatchingManager.IsValidInLounge());
				GoWrap.INSTANCE.trackEvent("quest_start", "Gameplay", dictionary2);
			}
		}
	}

	public void trackQuestEnd(uint questID, bool isSuccess)
	{
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(questID);
		if (questData != null && questData.eventId == 0 && questData.questType == QUEST_TYPE.ORDER)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("user_level", MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level);
			dictionary.Add("boss_level", questData.enemyLv[0]);
			dictionary.Add("boss_id", questData.enemyID[0]);
			dictionary.Add("is_meeting_room", LoungeMatchingManager.IsValidInLounge());
			dictionary.Add("is_success", isSuccess);
			GoWrap.INSTANCE.trackEvent("quest_end", "Gameplay", dictionary);
		}
	}

	public void trackBattleDisconnect()
	{
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
		if (questData != null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("user_level", MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level);
			dictionary.Add("boss_level", questData.enemyLv[0]);
			dictionary.Add("boss_id", questData.enemyID[0]);
			GoWrap.INSTANCE.trackEvent("battle_disconnect", "Gameplay", dictionary);
		}
	}

	public void TrackMysteryGift(MysteryGift.MysteryGiftReward reward)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		switch (reward.type)
		{
		case 1:
			dictionary.Add("reward_type", "gem");
			break;
		case 2:
			dictionary.Add("reward_type", "gold");
			break;
		case 3:
			dictionary.Add("reward_type", "item");
			dictionary.Add("reward_item_id", reward.itemId);
			break;
		case 5:
			dictionary.Add("reward_type", "magi");
			dictionary.Add("reward_skill_id", reward.itemId);
			break;
		}
		dictionary.Add("reward_value", reward.num);
		GoWrap.INSTANCE.trackEvent("mystery_gift_open", "MysteryGift", dictionary);
	}

	public void didCompleteRewardedAd(string rewardId, int rewardQuantity)
	{
	}

	public void onMenuOpened()
	{
	}

	public void onMenuClosed()
	{
	}

	public void onCustomUrl(string url)
	{
		if (url.StartsWith("unity:"))
		{
			WebViewManager.ProcessGotoEvent(url.Replace("unity:", ""));
		}
	}

	public void onOffersAvailable()
	{
	}
}
