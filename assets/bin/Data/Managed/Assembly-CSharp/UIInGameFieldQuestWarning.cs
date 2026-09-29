// Decompiled with JetBrains decompiler
// Type: UIInGameFieldQuestWarning
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class UIInGameFieldQuestWarning : MonoBehaviourSingleton<UIInGameFieldQuestWarning>
{
  [SerializeField]
  protected UITweenCtrl tweenCtrl;
  [SerializeField]
  protected UIInGameFieldQuestWarning.EffectData[] effect;
  [SerializeField]
  protected UITweenCtrl rareBossTweenCtrl;
  [SerializeField]
  protected UITweenCtrl fieldEnemyBossTweenCtrl;
  [SerializeField]
  protected UITweenCtrl fieldEnemyRareTweenCtrl;
  [SerializeField]
  protected UIInGameFieldQuestWarning.EffectData[] fishingEffect;
  [SerializeField]
  protected UITweenCtrl fieldFishingTweenCtrl;
  [SerializeField]
  protected UITweenCtrl fieldFishingRareTweenCtrl;

  public void Load(LoadingQueue load_queue)
  {
    int index = 0;
    for (int length = this.effect.Length; index < length; ++index)
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, this.effect[index].effectName);
    foreach (int se_id in (int[]) Enum.GetValues(typeof (UIInGameFieldQuestWarning.AUDIO)))
      load_queue.CacheSE(se_id);
  }

  protected override void Awake()
  {
    base.Awake();
    ((Component) this).gameObject.SetActive(false);
  }

  protected override void OnDisable()
  {
    this.tweenCtrl.Skip();
    if (Object.op_Inequality((Object) this.rareBossTweenCtrl, (Object) null))
      this.rareBossTweenCtrl.Skip();
    base.OnDisable();
  }

  public void Play(ENEMY_TYPE type, int rareBossType = 0, bool isFieldBoss = false)
  {
    ((Component) this).gameObject.SetActive(true);
    TweenAlpha.Begin(((Component) this).gameObject, 0.0f, 1f);
    if (Object.op_Inequality((Object) this.fieldFishingTweenCtrl, (Object) null))
      ((Component) this.fieldFishingTweenCtrl).gameObject.SetActive(false);
    if (Object.op_Inequality((Object) this.fieldFishingRareTweenCtrl, (Object) null))
      ((Component) this.fieldFishingRareTweenCtrl).gameObject.SetActive(false);
    if (isFieldBoss)
    {
      if (Object.op_Inequality((Object) this.rareBossTweenCtrl, (Object) null))
        ((Component) this.rareBossTweenCtrl).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.tweenCtrl, (Object) null))
        ((Component) this.tweenCtrl).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.fieldEnemyRareTweenCtrl, (Object) null))
        ((Component) this.fieldEnemyRareTweenCtrl).gameObject.SetActive(false);
      ((Component) this.fieldEnemyBossTweenCtrl).gameObject.SetActive(true);
      this.fieldEnemyBossTweenCtrl.Reset();
      this.fieldEnemyBossTweenCtrl.Play();
      SoundManager.PlayOneshotJingle(40000031);
      int index = 0;
      for (int length = this.effect.Length; index < length; ++index)
        this.StartCoroutine(this.Direction(this.effect[index]));
    }
    else if (rareBossType > 0)
    {
      ((Component) this.tweenCtrl).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.fieldEnemyBossTweenCtrl, (Object) null))
        ((Component) this.fieldEnemyBossTweenCtrl).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.fieldEnemyRareTweenCtrl, (Object) null))
        ((Component) this.fieldEnemyRareTweenCtrl).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.rareBossTweenCtrl, (Object) null))
      {
        ((Component) this.rareBossTweenCtrl).gameObject.SetActive(true);
        this.rareBossTweenCtrl.Reset();
        this.rareBossTweenCtrl.Play();
      }
      SoundManager.PlayOneshotJingle(40000163);
    }
    else
    {
      if (Object.op_Inequality((Object) this.rareBossTweenCtrl, (Object) null))
        ((Component) this.rareBossTweenCtrl).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.fieldEnemyBossTweenCtrl, (Object) null))
        ((Component) this.fieldEnemyBossTweenCtrl).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) this.fieldEnemyRareTweenCtrl, (Object) null))
        ((Component) this.fieldEnemyRareTweenCtrl).gameObject.SetActive(false);
      ((Component) this.tweenCtrl).gameObject.SetActive(true);
      this.tweenCtrl.Reset();
      this.tweenCtrl.Play();
      SoundManager.PlayOneshotJingle(40000031);
      int index = 0;
      for (int length = this.effect.Length; index < length; ++index)
        this.StartCoroutine(this.Direction(this.effect[index]));
    }
    SoundManager.RequestBGM(12);
  }

  public void PlayRareFieldEnemy()
  {
    ((Component) this).gameObject.SetActive(true);
    TweenAlpha.Begin(((Component) this).gameObject, 0.0f, 1f);
    if (Object.op_Inequality((Object) this.rareBossTweenCtrl, (Object) null))
      ((Component) this.rareBossTweenCtrl).gameObject.SetActive(false);
    if (Object.op_Inequality((Object) this.fieldEnemyBossTweenCtrl, (Object) null))
      ((Component) this.fieldEnemyBossTweenCtrl).gameObject.SetActive(false);
    if (Object.op_Inequality((Object) this.fieldFishingTweenCtrl, (Object) null))
      ((Component) this.fieldFishingTweenCtrl).gameObject.SetActive(false);
    if (Object.op_Inequality((Object) this.fieldFishingRareTweenCtrl, (Object) null))
      ((Component) this.fieldFishingRareTweenCtrl).gameObject.SetActive(false);
    if (Object.op_Inequality((Object) this.tweenCtrl, (Object) null))
      ((Component) this.tweenCtrl).gameObject.SetActive(false);
    ((Component) this.fieldEnemyRareTweenCtrl).gameObject.SetActive(true);
    this.fieldEnemyRareTweenCtrl.Reset();
    this.fieldEnemyRareTweenCtrl.Play();
    SoundManager.PlayOneshotJingle(40000031);
    int index = 0;
    for (int length = this.effect.Length; index < length; ++index)
      this.StartCoroutine(this.Direction(this.effect[index]));
  }

  public void PlayFieldFishingEnemy(bool isRare)
  {
    ((Component) this).gameObject.SetActive(true);
    TweenAlpha.Begin(((Component) this).gameObject, 0.0f, 1f);
    if (Object.op_Inequality((Object) this.rareBossTweenCtrl, (Object) null))
      ((Component) this.rareBossTweenCtrl).gameObject.SetActive(false);
    if (Object.op_Inequality((Object) this.fieldEnemyBossTweenCtrl, (Object) null))
      ((Component) this.fieldEnemyBossTweenCtrl).gameObject.SetActive(false);
    if (Object.op_Inequality((Object) this.fieldEnemyRareTweenCtrl, (Object) null))
      ((Component) this.fieldEnemyRareTweenCtrl).gameObject.SetActive(false);
    if (Object.op_Inequality((Object) this.tweenCtrl, (Object) null))
      ((Component) this.tweenCtrl).gameObject.SetActive(false);
    if (isRare)
    {
      ((Component) this.fieldFishingTweenCtrl).gameObject.SetActive(false);
      ((Component) this.fieldFishingRareTweenCtrl).gameObject.SetActive(true);
      this.fieldFishingRareTweenCtrl.Reset();
      this.fieldFishingRareTweenCtrl.Play();
    }
    else
    {
      ((Component) this.fieldFishingRareTweenCtrl).gameObject.SetActive(false);
      ((Component) this.fieldFishingTweenCtrl).gameObject.SetActive(true);
      this.fieldFishingTweenCtrl.Reset();
      this.fieldFishingTweenCtrl.Play();
      int index = 0;
      for (int length = this.fishingEffect.Length; index < length; ++index)
        this.StartCoroutine(this.Direction(this.fishingEffect[index]));
    }
    SoundManager.PlayOneshotJingle(40000031);
  }

  private IEnumerator Direction(UIInGameFieldQuestWarning.EffectData data)
  {
    yield return (object) new WaitForSeconds(data.delayTime);
    EffectManager.GetUIEffect(data.effectName, data.link);
  }

  public void FadeOut(float delay, float duration, System.Action onComplete)
  {
    this.StartCoroutine(this.DoFadeOut(delay, duration, onComplete));
  }

  private IEnumerator DoFadeOut(float delay, float duration, System.Action onComplete)
  {
    yield return (object) new WaitForSeconds(delay);
    TweenAlpha.Begin(((Component) this).gameObject, duration, 0.0f);
    yield return (object) new WaitForSeconds(duration);
    if (onComplete != null)
      onComplete();
    if (Object.op_Inequality((Object) ((Component) this).gameObject, (Object) null))
      ((Component) this).gameObject.SetActive(false);
  }

  [Serializable]
  public class EffectData
  {
    public Transform link;
    public string effectName;
    public float delayTime;
  }

  public enum AUDIO
  {
    BOSS_WARNING = 40000031, // 0x02625A1F
    BOSS_WARNING_SR = 40000163, // 0x02625AA3
  }
}
