// Decompiled with JetBrains decompiler
// Type: UILevelUpAnnounce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UILevelUpAnnounce : UIInGameSelfAnnounce
{
  [SerializeField]
  private UILabel m_upStatusLevel;
  [SerializeField]
  private List<UILabel> m_upParamList;
  [SerializeField]
  private List<UILabel> m_upTitleList;
  private const int WAIT_COUNT = 3;
  private const int LEVELUP_SE = 40000017;
  private string m_lvupTextFormat = string.Empty;
  private string m_paramTextFormat = string.Empty;
  private string m_titleTextHp = string.Empty;
  private string m_titleTextAtk = string.Empty;
  private string m_titleTextDef = string.Empty;
  private bool m_isReceiveRequest = true;
  private int m_oldHp;
  private int m_oldAtk;
  private int m_oldDef;
  private int m_oldLevel;
  private Coroutine m_coroutine;
  private AudioClip m_AudioClip;
  private bool m_isPlaying;

  public void GetNowStatus()
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || this.m_coroutine != null)
      return;
    this.m_oldAtk = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.atk;
    this.m_oldDef = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.def;
    this.m_oldHp = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.hp;
    this.m_oldLevel = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
  }

  public bool IsLevelUp()
  {
    return this.m_oldLevel < (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
  }

  public void Lock() => this.m_isReceiveRequest = false;

  public void Unlock() => this.m_isReceiveRequest = true;

  public bool IsLocked() => !this.m_isReceiveRequest;

  public bool IsWaitDelay() => this.m_coroutine != null;

  public bool IsPlaying() => this.m_isPlaying;

  public void PlayLevelUp()
  {
    this.SetNewParameter();
    this.PlayLevelUpInner();
  }

  public void PlayLevelUpForce(System.Action callback = null)
  {
    this.SetNewParameter();
    this.PlayLevelUpInner(true, callback);
  }

  public void SkipAnim() => this.Skip();

  private void StoreAudioClip()
  {
    string se = ResourceName.GetSE(40000017);
    if (string.IsNullOrEmpty(se))
      return;
    ResourceLink component = ((Component) this).gameObject.GetComponent<ResourceLink>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    this.m_AudioClip = component.Get<AudioClip>(se);
  }

  private void PlayAudioLevelUp()
  {
    if (!Object.op_Inequality((Object) this.m_AudioClip, (Object) null))
      return;
    SoundManager.PlayOneshotJingle(this.m_AudioClip, 40000017);
  }

  private void Start() => this.StoreAudioClip();

  private void SetNewParameter()
  {
    int level = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
    int hp = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.hp;
    int atk = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.atk;
    int def = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.def;
    if (string.IsNullOrEmpty(this.m_lvupTextFormat))
    {
      this.m_lvupTextFormat = this.m_upStatusLevel.text;
      this.m_paramTextFormat = this.m_upParamList[0].text;
      this.m_titleTextHp = this.m_upTitleList[0].text;
      this.m_titleTextAtk = this.m_upTitleList[1].text;
      this.m_titleTextDef = this.m_upTitleList[2].text;
    }
    int index = 0;
    this.m_upStatusLevel.text = string.Format(this.m_lvupTextFormat, (object) this.m_oldLevel, (object) level);
    if (hp - this.m_oldHp > 0)
    {
      this.m_upParamList[index].text = string.Format(this.m_paramTextFormat, (object) (hp - this.m_oldHp));
      this.m_upTitleList[index].text = this.m_titleTextHp;
      ++index;
    }
    if (atk - this.m_oldAtk > 0)
    {
      this.m_upParamList[index].text = string.Format(this.m_paramTextFormat, (object) (atk - this.m_oldAtk));
      this.m_upTitleList[index].text = this.m_titleTextAtk;
      ++index;
    }
    if (def - this.m_oldDef > 0)
    {
      this.m_upParamList[index].text = string.Format(this.m_paramTextFormat, (object) (def - this.m_oldDef));
      this.m_upTitleList[index].text = this.m_titleTextDef;
      ++index;
    }
    for (; index < 3; ++index)
    {
      this.m_upParamList[index].text = "";
      this.m_upTitleList[index].text = "";
    }
  }

  private bool IsPlay()
  {
    return !MonoBehaviourSingleton<UIManager>.I.IsTransitioning() && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing && !MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && this.m_isReceiveRequest && (!MonoBehaviourSingleton<InGameProgress>.IsValid() || !MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop) && (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "HomeScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "HomeTop" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "LoungeScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "LoungeTop" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "ClanScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "ClanTop" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "InGameMain");
  }

  private void PlayLevelUpInner(bool forcePlay = false, System.Action callback = null)
  {
    if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.IsRush())
      return;
    if (!forcePlay && !this.IsPlay())
    {
      if (this.m_coroutine != null)
        return;
      this.m_coroutine = this.StartCoroutine("DelayPlay");
    }
    else
    {
      if (forcePlay && this.m_coroutine != null)
      {
        this.StopCoroutine(this.m_coroutine);
        this.m_coroutine = (Coroutine) null;
      }
      this.Play(callback);
      this.PlayAudioLevelUp();
      this.GetNowStatus();
    }
  }

  private IEnumerator DelayPlay()
  {
    int waitCount = 0;
    while (!this.IsPlay() || waitCount < 3)
    {
      if (this.IsPlay())
        ++waitCount;
      else
        waitCount = 0;
      yield return (object) null;
    }
    this.m_isPlaying = true;
    this.Play((System.Action) (() => this.m_isPlaying = false));
    this.PlayAudioLevelUp();
    this.m_coroutine = (Coroutine) null;
    this.GetNowStatus();
  }

  private enum PARAM_POS
  {
    HP,
    ATK,
    DEF,
    END_POS,
  }
}
