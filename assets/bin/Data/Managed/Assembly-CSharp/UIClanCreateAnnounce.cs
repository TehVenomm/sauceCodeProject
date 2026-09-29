// Decompiled with JetBrains decompiler
// Type: UIClanCreateAnnounce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIClanCreateAnnounce : UIInGameSelfAnnounce
{
  [SerializeField]
  private UISprite[] announceSpr;
  private const int COUNT_WAIT = 3;
  private const int SE_CLAN_CREATE = 40000011;
  private Coroutine m_coroutine;
  private AudioClip m_audioClip;
  private List<EventItemCounts> m_eventItemCountList = new List<EventItemCounts>();
  private string m_raidBossHp_Str = string.Empty;
  private bool isPlaying;

  private void Start() => this.StoreAudioClip();

  private void StoreAudioClip()
  {
    string se = ResourceName.GetSE(40000011);
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
    SoundManager.PlayOneshotJingle(this.m_audioClip, 40000011);
  }

  private bool IsAbleToPlay()
  {
    return !MonoBehaviourSingleton<UIManager>.I.IsTransitioning() && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing && !MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && (!MonoBehaviourSingleton<InGameProgress>.IsValid() || !MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop) && (!MonoBehaviourSingleton<InGameManager>.IsValid() || !MonoBehaviourSingleton<InGameManager>.I.isQuestHappen) && (!MonoBehaviourSingleton<InGameManager>.IsValid() || !MonoBehaviourSingleton<InGameManager>.I.isQuestFromGimmick) && (!MonoBehaviourSingleton<DeliveryManager>.IsValid() || !MonoBehaviourSingleton<DeliveryManager>.I.isNoticeNewDeliveryAtHomeScene) && (!MonoBehaviourSingleton<DeliveryManager>.IsValid() || MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableStoryDelivery() == 0U) && (!MonoBehaviourSingleton<DeliveryManager>.IsValid() || MonoBehaviourSingleton<DeliveryManager>.I.GetEventCleardDeliveryData() == null) && !MonoBehaviourSingleton<UIManager>.I.levelUp.IsWaitDelay() && !MonoBehaviourSingleton<UIManager>.I.levelUp.IsPlaying() && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "ClanScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "ClanTop";
  }

  public void SetEventItemCountList(List<EventItemCounts> eventItemCounts)
  {
    this.m_eventItemCountList = eventItemCounts;
  }

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

  public void Play(bool isForcePlay = false, System.Action callback = null, UIClanCreateAnnounce.eType type = UIClanCreateAnnounce.eType.Create)
  {
    if (!TutorialStep.HasQuestSpecialUnlocked())
      return;
    this.SetTypeSprite(type);
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
    this.Play((System.Action) null);
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

  private void SetTypeSprite(UIClanCreateAnnounce.eType type)
  {
    string str;
    switch (type)
    {
      case UIClanCreateAnnounce.eType.Create:
        str = "ClanFormationTxt";
        break;
      case UIClanCreateAnnounce.eType.LevelUp:
        str = "ClanLevelupTxt";
        break;
      default:
        return;
    }
    int index = 0;
    for (int length = this.announceSpr.Length; index < length; ++index)
    {
      this.announceSpr[index].spriteName = str;
      this.announceSpr[index].SetDirty();
    }
  }

  public enum eType
  {
    Create,
    LevelUp,
  }
}
