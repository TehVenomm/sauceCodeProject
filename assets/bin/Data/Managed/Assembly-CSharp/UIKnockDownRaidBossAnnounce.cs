// Decompiled with JetBrains decompiler
// Type: UIKnockDownRaidBossAnnounce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIKnockDownRaidBossAnnounce : UIInGameSelfAnnounce
{
  private const int COUNT_WAIT = 3;
  private const int SE_KNOCK_DOWN = 40000067;
  private Coroutine m_coroutine;
  private AudioClip m_audioClip;
  private List<EventItemCounts> m_eventItemCountList = new List<EventItemCounts>();
  private string m_raidBossHp_Str = string.Empty;
  private bool isPlaying;

  private void Start() => this.StoreAudioClip();

  private void StoreAudioClip()
  {
    string se = ResourceName.GetSE(40000067);
    if (string.IsNullOrEmpty(se))
      return;
    ResourceLink component = ((Component) this).gameObject.GetComponent<ResourceLink>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    this.m_audioClip = component.Get<AudioClip>(se);
  }

  private void PlayAudioKnockDown()
  {
    if (Object.op_Equality((Object) this.m_audioClip, (Object) null))
      return;
    SoundManager.PlayOneshotJingle(this.m_audioClip, 40000067);
  }

  private bool IsAbleToPlay()
  {
    return !MonoBehaviourSingleton<UIManager>.I.IsTransitioning() && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing && !MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && (!MonoBehaviourSingleton<InGameProgress>.IsValid() || !MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop) && (!MonoBehaviourSingleton<InGameManager>.IsValid() || !MonoBehaviourSingleton<InGameManager>.I.isQuestHappen) && (!MonoBehaviourSingleton<InGameManager>.IsValid() || !MonoBehaviourSingleton<InGameManager>.I.isQuestFromGimmick) && (!MonoBehaviourSingleton<DeliveryManager>.IsValid() || !MonoBehaviourSingleton<DeliveryManager>.I.isNoticeNewDeliveryAtHomeScene) && (!MonoBehaviourSingleton<DeliveryManager>.IsValid() || MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableStoryDelivery() == 0U) && (!MonoBehaviourSingleton<DeliveryManager>.IsValid() || MonoBehaviourSingleton<DeliveryManager>.I.GetEventCleardDeliveryData() == null) && !MonoBehaviourSingleton<UIManager>.I.levelUp.IsWaitDelay() && !MonoBehaviourSingleton<UIManager>.I.levelUp.IsPlaying() && (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "HomeScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "HomeTop" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "LoungeScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "LoungeTop" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "ClanScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "ClanTop" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "InGameMain");
  }

  public void SetEventItemCountList(List<EventItemCounts> eventItemCounts)
  {
    this.m_eventItemCountList = eventItemCounts;
  }

  public void SetRaidBossHp(string raidBossHp_Str) => this.m_raidBossHp_Str = raidBossHp_Str;

  public bool IsKnockDownRaidBossByEventItemCountList()
  {
    if (this.m_eventItemCountList.IsNullOrEmpty<EventItemCounts>())
      return false;
    int index = 0;
    for (int count = this.m_eventItemCountList.Count; index < count; ++index)
    {
      if (this.m_eventItemCountList[index].eventType == 28)
      {
        long result1 = 0;
        long result2 = 0;
        if (long.TryParse(this.m_eventItemCountList[index].maxCount, out result1) && long.TryParse(this.m_eventItemCountList[index].count, out result2) && result2 >= result1)
          return true;
      }
    }
    return false;
  }

  public bool IsKnockDownRaidBossByRaidBossHp()
  {
    if (string.IsNullOrEmpty(this.m_raidBossHp_Str))
      return false;
    long result = 0;
    return long.TryParse(this.m_raidBossHp_Str, out result) && result <= 0L;
  }

  private void ClearKnockDownData()
  {
    this.m_eventItemCountList.Clear();
    this.m_raidBossHp_Str = string.Empty;
  }

  public void PlayKnockDown(bool isForcePlay = false, System.Action callback = null)
  {
    if (!TutorialStep.HasQuestSpecialUnlocked() || PlayerPrefs.GetInt("IS_SHOWED_RAID_BOSS_DIRECTION", 0) != 0 || MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGame())
      return;
    if (!isForcePlay && !this.IsAbleToPlay())
    {
      if (this.m_coroutine != null)
        return;
      this.m_coroutine = this.StartCoroutine(this.DelayPlay());
    }
    else
    {
      if (isForcePlay && this.m_coroutine != null)
      {
        this.StopCoroutine(this.m_coroutine);
        this.m_coroutine = (Coroutine) null;
      }
      this.Play(callback);
      this.PlayAudioKnockDown();
      PlayerPrefs.SetInt("IS_SHOWED_RAID_BOSS_DIRECTION", 1);
      this.ClearKnockDownData();
    }
  }

  private IEnumerator DelayPlay()
  {
    int waitCount = 0;
    while (!this.IsAbleToPlay() || waitCount < 3)
    {
      if (this.IsAbleToPlay())
        ++waitCount;
      else
        waitCount = 0;
      yield return (object) null;
    }
    this.Play();
    this.PlayAudioKnockDown();
    PlayerPrefs.SetInt("IS_SHOWED_RAID_BOSS_DIRECTION", 1);
    this.m_coroutine = (Coroutine) null;
    this.ClearKnockDownData();
  }

  public void ClearAnnounce()
  {
    if (this.m_coroutine == null)
      return;
    this.StopCoroutine(this.m_coroutine);
    this.m_coroutine = (Coroutine) null;
  }
}
