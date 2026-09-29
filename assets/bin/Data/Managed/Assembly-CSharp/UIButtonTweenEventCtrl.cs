// Decompiled with JetBrains decompiler
// Type: UIButtonTweenEventCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[AddComponentMenu("ProjectUI/UIButtonTweenEventCtrl")]
[RequireComponent(typeof (UIGameSceneEventSender))]
public class UIButtonTweenEventCtrl : UITweenCtrl
{
  public UITweener[] pushTweens;
  private bool isEnd;

  private void OnValidate()
  {
    if (this.tweens != null && this.tweens.Length != 0)
      Array.ForEach<UITweener>(this.tweens, (Action<UITweener>) (t =>
      {
        if (!Object.op_Inequality((Object) t, (Object) null))
          return;
        this._TweenReset(t);
        ((Behaviour) t).enabled = false;
      }));
    if (this.pushTweens == null || this.pushTweens.Length == 0)
      return;
    Array.ForEach<UITweener>(this.pushTweens, (Action<UITweener>) (t =>
    {
      if (!Object.op_Inequality((Object) t, (Object) null))
        return;
      this._TweenReset(t);
      ((Behaviour) t).enabled = false;
    }));
  }

  private void OnEnable() => this.OnValidate();

  private void Strat()
  {
    UIGameSceneEventSender sceneEventSender = ((Component) this).gameObject.GetComponent<UIGameSceneEventSender>();
    if (Object.op_Equality((Object) sceneEventSender, (Object) null))
      sceneEventSender = ((Component) this).gameObject.AddComponent<UIGameSceneEventSender>();
    if (!string.IsNullOrEmpty(sceneEventSender.eventName))
      return;
    sceneEventSender.eventName = "NONE";
  }

  public void PlayPush(bool isDown)
  {
    if (isDown)
    {
      this.isEnd = false;
      this._Reset(this.pushTweens);
      this.isPlaying = false;
      this._Play(this.pushTweens, isDown);
    }
    else
      this.End(this.pushTweens);
  }

  protected override void _TweenPlay(UITweener target, bool forward)
  {
    if (target.style != UITweener.Style.Once)
    {
      if (forward)
        target.Play(forward);
      else
        this._TweenReset(target);
    }
    else
      target.Play(forward);
  }

  protected void OnDragOut()
  {
    if (!this.isPlaying)
      return;
    this.End(this.pushTweens);
  }

  private void End(UITweener[] target_tweens)
  {
    if (target_tweens == null || target_tweens.Length == 0 || this.isEnd)
      return;
    this.isEnd = true;
    int index = 0;
    for (int length = target_tweens.Length; index < length; ++index)
    {
      if (!Object.op_Equality((Object) target_tweens[index], (Object) null))
      {
        UITweener.Style style = target_tweens[index].style;
        target_tweens[index].style = UITweener.Style.Once;
        target_tweens[index].tweenFactor = 1f;
        target_tweens[index].Sample(target_tweens[index].tweenFactor, true);
        target_tweens[index].PlayForward();
        target_tweens[index].style = style;
      }
    }
  }
}
