// Decompiled with JetBrains decompiler
// Type: NativeReceiver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class NativeReceiver : MonoBehaviourSingleton<NativeReceiver>
{
  public Action<List<string>> m_OnAchievementSyncUnlock;

  public void setGCMRegistrationId(string RegistrationId)
  {
  }

  public void GCMRegistered(string RegistrationId)
  {
    Debug.Log((object) ("============ GCMRegistered ==== " + RegistrationId));
  }

  public void onAchievementUpdated(string json)
  {
    NativeReceiver.AchievementUpdated achievementUpdated = JSONSerializer.Deserialize<NativeReceiver.AchievementUpdated>(json);
    Debug.Log((object) achievementUpdated.achievementId);
    Debug.Log((object) achievementUpdated.statusCode);
    if (achievementUpdated.statusCode != 0 && achievementUpdated.statusCode != 3003)
      return;
    this.SendAchievementUnlock(achievementUpdated.achievementId);
  }

  public void onAchievementSyncUnlocked(string json)
  {
    NativeReceiver.AchievementSyncUnlocked achievementSyncUnlocked = JSONSerializer.Deserialize<NativeReceiver.AchievementSyncUnlocked>(json);
    if (this.m_OnAchievementSyncUnlock == null)
      return;
    this.m_OnAchievementSyncUnlock(achievementSyncUnlocked.achievementIds);
  }

  public void SendAchievementUnlock(string achievementId)
  {
  }

  public void CallFromJS(string message)
  {
    if (!MonoBehaviourSingleton<WebViewManager>.IsValid())
      return;
    MonoBehaviourSingleton<WebViewManager>.I.OnWebViewEvent(message);
  }

  public void notifyAnalytics(string data)
  {
    BootProcess.notifyFinishedAnalytics(JSONSerializer.Deserialize<NativeReceiver.AnalyticsNotificationResult>(data).data);
  }

  public void ProcessReceivedNotification(string strParam)
  {
    strParam.Split(',');
    if (!strParam[0].Equals((object) "gc"))
      return;
    if (strParam[1].Equals((object) "magi"))
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
      {
        new EventData("MAIN_MENU_SHOP", (object) null)
      });
    else
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
      {
        new EventData("MAIN_MENU_SHOP", (object) null),
        new EventData("MAGI_GACHA", (object) null)
      });
  }

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
}
