// Decompiled with JetBrains decompiler
// Type: UITweener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public abstract class UITweener : MonoBehaviour
{
  public static UITweener current;
  [HideInInspector]
  public UITweener.Method method;
  [HideInInspector]
  public UITweener.Style style;
  [HideInInspector]
  public AnimationCurve animationCurve = new AnimationCurve(new Keyframe[2]
  {
    new Keyframe(0.0f, 0.0f, 0.0f, 1f),
    new Keyframe(1f, 1f, 1f, 0.0f)
  });
  [HideInInspector]
  public bool ignoreTimeScale = true;
  [HideInInspector]
  public float delay;
  [HideInInspector]
  public float duration = 1f;
  [HideInInspector]
  public bool steeperCurves;
  [HideInInspector]
  public int tweenGroup;
  [HideInInspector]
  public List<EventDelegate> onFinished = new List<EventDelegate>();
  [HideInInspector]
  public GameObject eventReceiver;
  [HideInInspector]
  public string callWhenFinished;
  private bool mStarted;
  private float mStartTime;
  private float mDuration;
  private float mAmountPerDelta = 1000f;
  private float mFactor;
  private List<EventDelegate> mTemp;

  public float amountPerDelta
  {
    get
    {
      if ((double) this.mDuration != (double) this.duration)
      {
        this.mDuration = this.duration;
        this.mAmountPerDelta = Mathf.Abs((double) this.duration > 0.0 ? 1f / this.duration : 1000f) * Mathf.Sign(this.mAmountPerDelta);
      }
      return this.mAmountPerDelta;
    }
  }

  public float tweenFactor
  {
    get => this.mFactor;
    set => this.mFactor = Mathf.Clamp01(value);
  }

  public AnimationOrTween.Direction direction
  {
    get => (double) this.amountPerDelta >= 0.0 ? AnimationOrTween.Direction.Forward : AnimationOrTween.Direction.Reverse;
  }

  private void Reset()
  {
    if (this.mStarted)
      return;
    this.SetStartToCurrentValue();
    this.SetEndToCurrentValue();
  }

  protected virtual void Start() => this.Update();

  private void Update()
  {
    bool flag = false;
    float num1 = this.ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime;
    float num2 = this.ignoreTimeScale ? RealTime.time : Time.time;
    if (!this.mStarted)
    {
      flag = true;
      this.mStarted = true;
      this.mStartTime = num2 + this.delay;
    }
    if ((double) num2 < (double) this.mStartTime)
      return;
    this.mFactor += this.amountPerDelta * num1;
    if (this.style == UITweener.Style.Loop)
    {
      if ((double) this.mFactor > 1.0)
        this.mFactor -= Mathf.Floor(this.mFactor);
    }
    else if (this.style == UITweener.Style.PingPong)
    {
      if ((double) this.mFactor > 1.0)
      {
        this.mFactor = (float) (1.0 - ((double) this.mFactor - (double) Mathf.Floor(this.mFactor)));
        this.mAmountPerDelta = -this.mAmountPerDelta;
      }
      else if ((double) this.mFactor < 0.0)
      {
        this.mFactor = -this.mFactor;
        this.mFactor -= Mathf.Floor(this.mFactor);
        this.mAmountPerDelta = -this.mAmountPerDelta;
      }
    }
    if (this.style == UITweener.Style.Once && ((double) this.duration == 0.0 || (double) this.mFactor > 1.0 || (double) this.mFactor < 0.0))
    {
      this.mFactor = Mathf.Clamp01(this.mFactor);
      if (flag)
      {
        this.Sample(this.mFactor, false);
        if ((double) this.duration != 0.0)
          return;
        ((Behaviour) this).enabled = false;
      }
      else
      {
        this.Sample(this.mFactor, true);
        ((Behaviour) this).enabled = false;
        if (!Object.op_Equality((Object) UITweener.current, (Object) null))
          return;
        UITweener current = UITweener.current;
        UITweener.current = this;
        if (this.onFinished != null)
        {
          this.mTemp = this.onFinished;
          this.onFinished = new List<EventDelegate>();
          EventDelegate.Execute(this.mTemp);
          for (int index = 0; index < this.mTemp.Count; ++index)
          {
            EventDelegate ev = this.mTemp[index];
            if (ev != null && !ev.oneShot)
              EventDelegate.Add(this.onFinished, ev, ev.oneShot);
          }
          this.mTemp = (List<EventDelegate>) null;
        }
        if (Object.op_Inequality((Object) this.eventReceiver, (Object) null) && !string.IsNullOrEmpty(this.callWhenFinished))
          this.eventReceiver.SendMessage(this.callWhenFinished, (object) this, (SendMessageOptions) 1);
        UITweener.current = current;
      }
    }
    else
      this.Sample(this.mFactor, false);
  }

  public void SetOnFinished(EventDelegate.Callback del) => EventDelegate.Set(this.onFinished, del);

  public void SetOnFinished(EventDelegate del) => EventDelegate.Set(this.onFinished, del);

  public void AddOnFinished(EventDelegate.Callback del) => EventDelegate.Add(this.onFinished, del);

  public void AddOnFinished(EventDelegate del) => EventDelegate.Add(this.onFinished, del);

  public void RemoveOnFinished(EventDelegate del)
  {
    if (this.onFinished != null)
      this.onFinished.Remove(del);
    if (this.mTemp == null)
      return;
    this.mTemp.Remove(del);
  }

  private void OnDisable() => this.mStarted = false;

  public void Sample(float factor, bool isFinished)
  {
    float val = Mathf.Clamp01(factor);
    if (this.method == UITweener.Method.EaseIn)
    {
      val = 1f - Mathf.Sin((float) (1.5707963705062866 * (1.0 - (double) val)));
      if (this.steeperCurves)
        val *= val;
    }
    else if (this.method == UITweener.Method.EaseOut)
    {
      val = Mathf.Sin(1.57079637f * val);
      if (this.steeperCurves)
      {
        float num = 1f - val;
        val = (float) (1.0 - (double) num * (double) num);
      }
    }
    else if (this.method == UITweener.Method.EaseInOut)
    {
      val -= Mathf.Sin(val * 6.28318548f) / 6.28318548f;
      if (this.steeperCurves)
      {
        float num1 = (float) ((double) val * 2.0 - 1.0);
        double num2 = (double) Mathf.Sign(num1);
        float num3 = 1f - Mathf.Abs(num1);
        double num4 = 1.0 - (double) num3 * (double) num3;
        val = (float) (num2 * num4 * 0.5 + 0.5);
      }
    }
    else if (this.method == UITweener.Method.BounceIn)
      val = this.BounceLogic(val);
    else if (this.method == UITweener.Method.BounceOut)
      val = 1f - this.BounceLogic(1f - val);
    this.OnUpdate(this.animationCurve != null ? this.animationCurve.Evaluate(val) : val, isFinished);
  }

  private float BounceLogic(float val)
  {
    val = (double) val >= 0.36363598704338074 ? ((double) val >= 0.72727197408676147 ? ((double) val >= 0.909089982509613 ? (float) (121.0 / 16.0 * (double) (val -= 0.9545454f) * (double) val + 63.0 / 64.0) : (float) (121.0 / 16.0 * (double) (val -= 0.818181f) * (double) val + 15.0 / 16.0)) : (float) (121.0 / 16.0 * (double) (val -= 0.545454f) * (double) val + 0.75)) : 7.5685f * val * val;
    return val;
  }

  [Obsolete("Use PlayForward() instead")]
  public void Play() => this.Play(true);

  public void PlayForward() => this.Play(true);

  public void PlayReverse() => this.Play(false);

  public void Play(bool forward)
  {
    this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
    if (!forward)
      this.mAmountPerDelta = -this.mAmountPerDelta;
    ((Behaviour) this).enabled = true;
    this.Update();
  }

  public void ResetToBeginning()
  {
    this.mStarted = false;
    this.mFactor = (double) this.amountPerDelta < 0.0 ? 1f : 0.0f;
    this.Sample(this.mFactor, false);
  }

  public void Toggle()
  {
    this.mAmountPerDelta = (double) this.mFactor <= 0.0 ? Mathf.Abs(this.amountPerDelta) : -this.amountPerDelta;
    ((Behaviour) this).enabled = true;
  }

  protected abstract void OnUpdate(float factor, bool isFinished);

  public static T Begin<T>(GameObject go, float duration, bool overrideAnimationCurve = true) where T : UITweener
  {
    T obj = go.GetComponent<T>();
    if (Object.op_Inequality((Object) (object) obj, (Object) null) && obj.tweenGroup != 0)
    {
      obj = default (T);
      T[] components = go.GetComponents<T>();
      int index = 0;
      for (int length = components.Length; index < length; ++index)
      {
        obj = components[index];
        if (!Object.op_Inequality((Object) (object) obj, (Object) null) || obj.tweenGroup != 0)
          obj = default (T);
        else
          break;
      }
    }
    if (Object.op_Equality((Object) (object) obj, (Object) null))
    {
      obj = go.AddComponent<T>();
      if (Object.op_Equality((Object) (object) obj, (Object) null))
      {
        Debug.LogError((object) $"Unable to add {(object) typeof (T)} to {NGUITools.GetHierarchy(go)}", (Object) go);
        return default (T);
      }
    }
    obj.mStarted = false;
    obj.duration = duration;
    obj.mFactor = 0.0f;
    obj.mAmountPerDelta = Mathf.Abs(obj.amountPerDelta);
    obj.style = UITweener.Style.Once;
    if (overrideAnimationCurve)
      obj.animationCurve = new AnimationCurve(new Keyframe[2]
      {
        new Keyframe(0.0f, 0.0f, 0.0f, 1f),
        new Keyframe(1f, 1f, 1f, 0.0f)
      });
    obj.eventReceiver = (GameObject) null;
    obj.callWhenFinished = (string) null;
    ((Behaviour) (object) obj).enabled = true;
    return obj;
  }

  public virtual void SetStartToCurrentValue()
  {
  }

  public virtual void SetEndToCurrentValue()
  {
  }

  public enum Method
  {
    Linear,
    EaseIn,
    EaseOut,
    EaseInOut,
    BounceIn,
    BounceOut,
  }

  public enum Style
  {
    Once,
    Loop,
    PingPong,
  }
}
