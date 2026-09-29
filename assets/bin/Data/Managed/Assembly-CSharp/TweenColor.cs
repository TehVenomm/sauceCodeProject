// Decompiled with JetBrains decompiler
// Type: TweenColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Tween/Tween Color")]
public class TweenColor : UITweener
{
  public Color from = Color.white;
  public Color to = Color.white;
  private bool mCached;
  private UIWidget mWidget;
  private Material mMat;
  private Light mLight;
  private SpriteRenderer mSr;

  private void Cache()
  {
    this.mCached = true;
    this.mWidget = ((Component) this).GetComponent<UIWidget>();
    if (Object.op_Inequality((Object) this.mWidget, (Object) null))
      return;
    this.mSr = ((Component) this).GetComponent<SpriteRenderer>();
    if (Object.op_Inequality((Object) this.mSr, (Object) null))
      return;
    Renderer component = ((Component) this).GetComponent<Renderer>();
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      this.mMat = component.material;
    }
    else
    {
      this.mLight = ((Component) this).GetComponent<Light>();
      if (!Object.op_Equality((Object) this.mLight, (Object) null))
        return;
      this.mWidget = ((Component) this).GetComponentInChildren<UIWidget>();
    }
  }

  [Obsolete("Use 'value' instead")]
  public Color color
  {
    get => this.value;
    set => this.value = value;
  }

  public Color value
  {
    get
    {
      if (!this.mCached)
        this.Cache();
      if (Object.op_Inequality((Object) this.mWidget, (Object) null))
        return this.mWidget.color;
      if (Object.op_Inequality((Object) this.mMat, (Object) null))
        return this.mMat.color;
      if (Object.op_Inequality((Object) this.mSr, (Object) null))
        return this.mSr.color;
      return Object.op_Inequality((Object) this.mLight, (Object) null) ? this.mLight.color : Color.black;
    }
    set
    {
      if (!this.mCached)
        this.Cache();
      if (Object.op_Inequality((Object) this.mWidget, (Object) null))
        this.mWidget.color = value;
      else if (Object.op_Inequality((Object) this.mMat, (Object) null))
        this.mMat.color = value;
      else if (Object.op_Inequality((Object) this.mSr, (Object) null))
      {
        this.mSr.color = value;
      }
      else
      {
        if (!Object.op_Inequality((Object) this.mLight, (Object) null))
          return;
        this.mLight.color = value;
        ((Behaviour) this.mLight).enabled = (double) value.r + (double) value.g + (double) value.b > 0.0099999997764825821;
      }
    }
  }

  protected override void OnUpdate(float factor, bool isFinished)
  {
    this.value = Color.Lerp(this.from, this.to, factor);
  }

  public static TweenColor Begin(GameObject go, float duration, Color color)
  {
    TweenColor tweenColor = UITweener.Begin<TweenColor>(go, duration);
    tweenColor.from = tweenColor.value;
    tweenColor.to = color;
    if ((double) duration <= 0.0)
    {
      tweenColor.Sample(1f, true);
      ((Behaviour) tweenColor).enabled = false;
    }
    return tweenColor;
  }

  [ContextMenu("Set 'From' to current value")]
  public override void SetStartToCurrentValue() => this.from = this.value;

  [ContextMenu("Set 'To' to current value")]
  public override void SetEndToCurrentValue() => this.to = this.value;

  [ContextMenu("Assume value of 'From'")]
  private void SetCurrentValueToStart() => this.value = this.from;

  [ContextMenu("Assume value of 'To'")]
  private void SetCurrentValueToEnd() => this.value = this.to;
}
