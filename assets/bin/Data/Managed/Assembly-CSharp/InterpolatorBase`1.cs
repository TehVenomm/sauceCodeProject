// Decompiled with JetBrains decompiler
// Type: InterpolatorBase`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public abstract class InterpolatorBase<T> : Interpolator
{
  public bool play = true;
  public Interpolator.LOOP loopType;
  public float time;
  public T beginValue;
  public T endValue;
  public AnimationCurve easeCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1f, 1f);
  public T addValue;
  public AnimationCurve addCurve;
  protected T nowValue;
  protected bool calcAngle;
  protected float nowTime;
  protected bool turn;
  protected bool init;

  public void Set(
    float _time,
    T begin_value,
    T end_value,
    AnimationCurve ease_curve = null,
    T add_value = null,
    AnimationCurve add_curve = null)
  {
    this.time = _time;
    this.beginValue = begin_value;
    this.endValue = end_value;
    this.easeCurve = ease_curve == null ? Curves.easeInOut : ease_curve;
    this.addValue = add_value;
    this.addCurve = add_curve;
    this.init = true;
  }

  public void Set(
    float _time,
    T end_value,
    AnimationCurve ease_curve = null,
    T add_value = null,
    AnimationCurve add_curve = null)
  {
    this.Set(_time, this.Get(), end_value, ease_curve, add_value, add_curve);
  }

  public void Set(T value) => this.Set(0.0f, value, value);

  public void Play()
  {
    if ((double) this.time == 0.0)
    {
      this.Stop();
    }
    else
    {
      this.play = true;
      this.turn = false;
      this.nowTime = 0.0f;
    }
  }

  public void Stop() => this.play = false;

  public bool IsPlaying()
  {
    if (this.init)
      return true;
    return this.play && (double) this.time > 0.0;
  }

  public void Update(float dt)
  {
    this.init = false;
    if (!this.IsPlaying())
    {
      this.nowValue = this.endValue;
    }
    else
    {
      switch (this.loopType)
      {
        case Interpolator.LOOP.NONE:
          this.nowTime += dt;
          if ((double) this.nowTime >= (double) this.time)
          {
            this.nowValue = this.endValue;
            this.Stop();
            return;
          }
          break;
        case Interpolator.LOOP.REPETE:
          this.nowTime += dt;
          if ((double) this.nowTime >= (double) this.time)
          {
            this.nowTime %= this.time;
            break;
          }
          break;
        case Interpolator.LOOP.PINGPONG:
          if (!this.turn)
          {
            this.nowTime += dt;
            if ((double) this.nowTime >= (double) this.time)
            {
              this.nowTime = this.time - this.nowTime % this.time;
              this.turn = true;
              break;
            }
            break;
          }
          this.nowTime -= dt;
          if ((double) this.nowTime <= 0.0)
          {
            this.nowTime = -this.nowTime % this.time;
            this.turn = false;
            break;
          }
          break;
      }
      float t = this.nowTime / this.time;
      float r = this.easeCurve == null ? t : this.easeCurve.Evaluate(t);
      this.Calc(t, r);
    }
  }

  public T Update()
  {
    this.Update(Time.deltaTime);
    return this.Get();
  }

  public T Get() => this.nowValue;

  protected virtual void Calc(float t, float r)
  {
  }
}
