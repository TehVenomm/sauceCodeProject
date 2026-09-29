// Decompiled with JetBrains decompiler
// Type: GoWrapManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using gogame;
using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GoWrapManager : MonoBehaviourSingleton<GoWrapManager>, IGoWrapDelegate
{
  private void Start()
  {
    GoWrap.INSTANCE.setDelegate((IGoWrapDelegate) this);
    GoWrap.INSTANCE.initGoWrap(((Object) this).name);
    ((Component) this).gameObject.AddComponent<GoWrapComponent>();
    GoWrap.INSTANCE.setCustomUrlSchemes(new List<string>()
    {
      "unity"
    });
  }

  public void setDeviceToken(byte[] token) => GoWrap.INSTANCE.setIOSDeviceToken(token);

  public void ShowMenu() => GoWrap.INSTANCE.showMenu();

  public void setGUID(string guid) => GoWrap.INSTANCE.setGuid(guid);

  public void trackTutorialStep(TRACK_TUTORIAL_STEP_BIT stepName, string category)
  {
    if (GameSaveData.instance.IsPushedTrackTutorialBit(stepName))
      return;
    string name = stepName.ToString();
    if (PlayerPrefs.GetInt("track_" + name, 0) != 0)
      return;
    GoWrap.INSTANCE.trackEvent(name, category);
    GameSaveData.instance.SetPushedTrackTutorialBit(stepName);
  }

  public void trackTutorialStep(
    TRACK_TUTORIAL_STEP_BIT stepName,
    string category,
    Dictionary<string, object> values)
  {
    if (GameSaveData.instance.IsPushedTrackTutorialBit(stepName))
      return;
    string name = stepName.ToString();
    if (PlayerPrefs.GetInt("track_" + name, 0) != 0)
      return;
    GoWrap.INSTANCE.trackEvent(name, category, values);
    GameSaveData.instance.SetPushedTrackTutorialBit(stepName);
  }

  public void SendStatusTracking(
    TRACK_TUTORIAL_STEP_BIT _stepName,
    string _category,
    Dictionary<string, object> dictionary = null,
    Action<bool> call_back = null)
  {
    Protocol.Force((System.Action) (() =>
    {
      Debug.LogWarning((object) "SendStatusTracking Called!");
      Protocol.SendAsync<AnalyticTrackingPointModel.RequestSendForm, AnalyticTrackingPointModel>(AnalyticTrackingPointModel.URL, new AnalyticTrackingPointModel.RequestSendForm()
      {
        name = _stepName.ToString(),
        category = _category
      }, (Action<AnalyticTrackingPointModel>) (ret =>
      {
        bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
        if (call_back != null)
          call_back(flag);
        Debug.LogWarning((object) "SendStatusTracking Bit Success!");
      }));
    }));
  }

  public void trackEvent(string name, string category)
  {
    GoWrap.INSTANCE.trackEvent(name, category);
  }

  public void trackEvent(string name, string category, Dictionary<string, object> values)
  {
    GoWrap.INSTANCE.trackEvent(name, category, values);
  }

  public void trackPurchase(
    string productId,
    string currencyCode,
    double price,
    string purchaseData,
    string signature)
  {
    GoWrap.INSTANCE.trackPurchase(productId, currencyCode, price, purchaseData, signature);
  }

  public void trackQuestStart(uint questID)
  {
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(questID);
    if (questData == null)
      return;
    if (questData.eventId > 0)
    {
      GoWrap.INSTANCE.trackEvent("expedition_start", "Gameplay", new Dictionary<string, object>()
      {
        {
          "quest_id",
          (object) questID
        },
        {
          "user_level",
          (object) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level
        },
        {
          "boss_level",
          (object) questData.enemyLv[0]
        },
        {
          "boss_id",
          (object) questData.enemyID[0]
        }
      });
    }
    else
    {
      if (questData.questType != QUEST_TYPE.ORDER)
        return;
      GoWrap.INSTANCE.trackEvent("quest_start", "Gameplay", new Dictionary<string, object>()
      {
        {
          "user_level",
          (object) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level
        },
        {
          "boss_level",
          (object) questData.enemyLv[0]
        },
        {
          "boss_id",
          (object) questData.enemyID[0]
        },
        {
          "is_meeting_room",
          (object) LoungeMatchingManager.IsValidInLounge()
        }
      });
    }
  }

  public void trackQuestEnd(uint questID, bool isSuccess)
  {
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(questID);
    if (questData == null || questData.eventId != 0 || questData.questType != QUEST_TYPE.ORDER)
      return;
    GoWrap.INSTANCE.trackEvent("quest_end", "Gameplay", new Dictionary<string, object>()
    {
      {
        "user_level",
        (object) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level
      },
      {
        "boss_level",
        (object) questData.enemyLv[0]
      },
      {
        "boss_id",
        (object) questData.enemyID[0]
      },
      {
        "is_meeting_room",
        (object) LoungeMatchingManager.IsValidInLounge()
      },
      {
        "is_success",
        (object) isSuccess
      }
    });
  }

  public void trackBattleDisconnect()
  {
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
    if (questData == null)
      return;
    GoWrap.INSTANCE.trackEvent("battle_disconnect", "Gameplay", new Dictionary<string, object>()
    {
      {
        "user_level",
        (object) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level
      },
      {
        "boss_level",
        (object) questData.enemyLv[0]
      },
      {
        "boss_id",
        (object) questData.enemyID[0]
      }
    });
  }

  public void TrackMysteryGift(MysteryGift.MysteryGiftReward reward)
  {
    Dictionary<string, object> values = new Dictionary<string, object>();
    switch (reward.type)
    {
      case 1:
        values.Add("reward_type", (object) "gem");
        break;
      case 2:
        values.Add("reward_type", (object) "gold");
        break;
      case 3:
        values.Add("reward_type", (object) "item");
        values.Add("reward_item_id", (object) reward.itemId);
        break;
      case 5:
        values.Add("reward_type", (object) "magi");
        values.Add("reward_skill_id", (object) reward.itemId);
        break;
    }
    values.Add("reward_value", (object) reward.num);
    GoWrap.INSTANCE.trackEvent("mystery_gift_open", "MysteryGift", values);
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
    if (!url.StartsWith("unity:"))
      return;
    WebViewManager.ProcessGotoEvent(url.Replace("unity:", ""));
  }

  public void onOffersAvailable()
  {
  }
}
