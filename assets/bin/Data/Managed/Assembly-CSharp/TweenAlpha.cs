// Decompiled with JetBrains decompiler
// Type: TweenAlpha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Tween/Tween Alpha")]
public class TweenAlpha : UITweener
{
  [Range(0.0f, 1f)]
  public float from = 1f;
  [Range(0.0f, 1f)]
  public float to = 1f;
  private bool mCached;
  private UIRect mRect;
  private Material mMat;
  private SpriteRenderer mSr;

  [Obsolete("Use 'value' instead")]
  public float alpha
  {
    get => this.value;
    set => this.value = value;
  }

  private void Cache()
  {
    this.mCached = true;
    this.mRect = ((Component) this).GetComponent<UIRect>();
    this.mSr = ((Component) this).GetComponent<SpriteRenderer>();
    if (!Object.op_Equality((Object) this.mRect, (Object) null) || !Object.op_Equality((Object) this.mSr, (Object) null))
      return;
    Renderer component = ((Component) this).GetComponent<Renderer>();
    if (Object.op_Inequality((Object) component, (Object) null))
      this.mMat = component.material;
    if (!Object.op_Equality((Object) this.mMat, (Object) null))
      return;
    this.mRect = ((Component) this).GetComponentInChildren<UIRect>();
  }

  public float value
  {
    get
    {
      if (!this.mCached)
        this.Cache();
      if (Object.op_Inequality((Object) this.mRect, (Object) null))
        return this.mRect.alpha;
      if (Object.op_Inequality((Object) this.mSr, (Object) null))
        return this.mSr.color.a;
      return !Object.op_Inequality((Object) this.mMat, (Object) null) ? 1f : this.mMat.color.a;
    }
    set
    {
      if (!this.mCached)
        this.Cache();
      if (Object.op_Inequality((Object) this.mRect, (Object) null))
        this.mRect.alpha = value;
      else if (Object.op_Inequality((Object) this.mSr, (Object) null))
      {
        Color color = this.mSr.color;
        color.a = value;
        this.mSr.color = color;
      }
      else
      {
        if (!Object.op_Inequality((Object) this.mMat, (Object) null))
          return;
        Color color = this.mMat.color;
        color.a = value;
        this.mMat.color = color;
      }
    }
  }

  protected override void OnUpdate(float factor, bool isFinished)
  {
    this.value = Mathf.Lerp(this.from, this.to, factor);
  }

  public static TweenAlpha Begin(GameObject go, float duration, float alpha)
  {
    TweenAlpha tweenAlpha = UITweener.Begin<TweenAlpha>(go, duration);
    tweenAlpha.from = tweenAlpha.value;
    tweenAlpha.to = alpha;
    if ((double) duration <= 0.0)
    {
      tweenAlpha.Sample(1f, true);
      ((Behaviour) tweenAlpha).enabled = false;
    }
    return tweenAlpha;
  }

  public override void SetStartToCurrentValue() => this.from = this.value;

  public override void SetEndToCurrentValue() => this.to = this.value;
}
