// Decompiled with JetBrains decompiler
// Type: UIFieldBossInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class UIFieldBossInfo : MonoBehaviourSingleton<UIFieldBossInfo>
{
  [SerializeField]
  protected UILabel timeLabel;
  private bool active;
  private bool doingFadeOut;
  private bool doingFadeIn;

  private void Start()
  {
    if (this.active)
      return;
    this.SetActive(false);
  }

  private void LateUpdate()
  {
    this.timeLabel.text = MonoBehaviourSingleton<InGameProgress>.I.GetRemainTime();
    if (MonoBehaviourSingleton<InGameProgress>.IsValid() && (double) MonoBehaviourSingleton<InGameProgress>.I.remaindTime <= 0.0)
      this.FadeOut(0.0f, 1f, (System.Action) null);
    if (!MonoBehaviourSingleton<CoopManager>.IsValid() || MonoBehaviourSingleton<CoopManager>.I.coopStage.GetIsInFieldEnemyBossBattle())
      return;
    this.FadeOut(0.0f, 1f, (System.Action) null);
  }

  public void SetActive(bool isActive)
  {
    this.active = isActive;
    ((Component) this).gameObject.SetActive(isActive);
  }

  public void FadeIn(float delay, float duration, System.Action onComplete) => this.SetActive(true);

  public void FadeOut(float delay, float duration, System.Action onComplete)
  {
    this.SetActive(false);
  }

  private IEnumerator DoFade(
    float delay,
    float duration,
    float start,
    float end,
    System.Action onComplete)
  {
    TweenAlpha.Begin(((Component) this).gameObject, 0.0f, start);
    yield return (object) new WaitForSeconds(delay);
    TweenAlpha.Begin(((Component) this).gameObject, duration, end);
    yield return (object) new WaitForSeconds(duration);
    if (onComplete != null)
      onComplete();
  }
}
