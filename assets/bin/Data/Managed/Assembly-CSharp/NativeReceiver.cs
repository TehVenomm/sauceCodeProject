using System;
using System.Collections.Generic;
using UnityEngine;

public class NativeReceiver : MonoBehaviourSingleton<NativeReceiver>
{
	private class AchievementUpdated
	{
		public string achievementId;

		public int statusCode;
	}

	private class AchievementSyncUnlocked
	{
		public List<string> achievementIds;
	}

	private class AnalyticsNotificationResult
	{
		public string data;
	}

	public Action<List<string>> m_OnAchievementSyncUnlock;

	public void setGCMRegistrationId(string RegistrationId)
	{
	}

	public void GCMRegistered(string RegistrationId)
	{
		Debug.Log("============ GCMRegistered ==== " + RegistrationId);
	}

	public void onAchievementUpdated(string json)
	{
		AchievementUpdated achievementUpdated = JSONSerializer.Deserialize<AchievementUpdated>(json);
		Debug.Log(achievementUpdated.achievementId);
		Debug.Log(achievementUpdated.statusCode);
		if (achievementUpdated.statusCode == 0 || achievementUpdated.statusCode == 3003)
		{
			SendAchievementUnlock(achievementUpdated.achievementId);
		}
	}

	public void onAchievementSyncUnlocked(string json)
	{
		AchievementSyncUnlocked achievementSyncUnlocked = JSONSerializer.Deserialize<AchievementSyncUnlocked>(json);
		if (m_OnAchievementSyncUnlock != null)
		{
			m_OnAchievementSyncUnlock(achievementSyncUnlocked.achievementIds);
		}
	}

	public void SendAchievementUnlock(string achievementId)
	{
	}

	public void CallFromJS(string message)
	{
		if (MonoBehaviourSingleton<WebViewManager>.IsValid())
		{
			MonoBehaviourSingleton<WebViewManager>.I.OnWebViewEvent(message);
		}
	}

	public void notifyAnalytics(string data)
	{
		AnalyticsNotificationResult analyticsNotificationResult = JSONSerializer.Deserialize<AnalyticsNotificationResult>(data);
		BootProcess.notifyFinishedAnalytics(analyticsNotificationResult.data);
	}

	public void ProcessReceivedNotification(string strParam)
	{
		string[] array = strParam.Split(',');
		if (strParam[0].Equals("gc"))
		{
			EventData[] array2 = null;
			if (strParam[1].Equals("magi"))
			{
				array2 = new EventData[1]
				{
					new EventData("MAIN_MENU_SHOP", null)
				};
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
			}
			else
			{
				array2 = new EventData[2]
				{
					new EventData("MAIN_MENU_SHOP", null),
					new EventData("MAGI_GACHA", null)
				};
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
			}
		}
	}
}
