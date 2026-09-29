// Decompiled with JetBrains decompiler
// Type: SlimeAnimBase`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SlimeAnimBase<T> where T : new()
{
  protected AnimationCurve animCurve;
  protected AnimationCurve blendCurve;
  protected float playTime;
  protected float nowTime;
  protected T blendParam;
  protected float blendEndTime;
  protected bool isBlend;
  private System.Action callback;

  public bool isPlaying { get; protected set; }

  public virtual void InitAnim(
    AnimationCurve curve,
    float time,
    bool is_blend,
    T now_param,
    System.Action cb)
  {
    this.animCurve = curve;
    this.playTime = time;
    this.callback = cb;
    this.nowTime = 0.0f;
    this.isBlend = is_blend;
    this.blendParam = now_param;
    this.isPlaying = true;
  }

  public T Update()
  {
    T obj = this.UpdateAnim();
    this.updatePlayTime();
    return obj;
  }

  public virtual T UpdateAnim() => new T();

  public void SetBlendParam(AnimationCurve blend_curve, float end_time)
  {
    this.blendCurve = blend_curve;
    this.blendEndTime = end_time;
  }

  private void updatePlayTime()
  {
    this.nowTime += Time.deltaTime;
    if ((double) this.playTime > (double) this.nowTime)
      return;
    this.AnimFinish();
  }

  protected void AnimFinish()
  {
    if (!this.isPlaying)
      return;
    this.isPlaying = false;
    if (this.callback == null)
      return;
    this.callback();
  }

  public void Terminate() => this.isPlaying = false;
}
