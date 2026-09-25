// Decompiled with JetBrains decompiler
// Type: ActiveAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using AnimationOrTween;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Internal/Active Animation")]
public class ActiveAnimation : MonoBehaviour
{
  public static ActiveAnimation current;
  public List<EventDelegate> onFinished = new List<EventDelegate>();
  [HideInInspector]
  public GameObject eventReceiver;
  [HideInInspector]
  public string callWhenFinished;
  private Animation mAnim;
  private AnimationOrTween.Direction mLastDirection;
  private AnimationOrTween.Direction mDisableDirection;
  private bool mNotify;
  private Animator mAnimator;
  private string mClip = "";

  private float playbackTime
  {
    get
    {
      AnimatorStateInfo animatorStateInfo = this.mAnimator.GetCurrentAnimatorStateInfo(0);
      return Mathf.Clamp01(((AnimatorStateInfo) ref animatorStateInfo).normalizedTime);
    }
  }

  public bool isPlaying
  {
    get
    {
      if (Object.op_Equality((Object) this.mAnim, (Object) null))
      {
        if (!Object.op_Inequality((Object) this.mAnimator, (Object) null))
          return false;
        if (this.mLastDirection == AnimationOrTween.Direction.Reverse)
        {
          if ((double) this.playbackTime == 0.0)
            return false;
        }
        else if ((double) this.playbackTime == 1.0)
          return false;
        return true;
      }
      foreach (AnimationState animationState in this.mAnim)
      {
        if (this.mAnim.IsPlaying(animationState.name))
        {
          if (this.mLastDirection == AnimationOrTween.Direction.Forward)
          {
            if ((double) animationState.time < (double) animationState.length)
              return true;
          }
          else if (this.mLastDirection != AnimationOrTween.Direction.Reverse || (double) animationState.time > 0.0)
            return true;
        }
      }
      return false;
    }
  }

  public void Finish()
  {
    if (Object.op_Inequality((Object) this.mAnim, (Object) null))
    {
      foreach (AnimationState animationState in this.mAnim)
      {
        if (this.mLastDirection == AnimationOrTween.Direction.Forward)
          animationState.time = animationState.length;
        else if (this.mLastDirection == AnimationOrTween.Direction.Reverse)
          animationState.time = 0.0f;
      }
      this.mAnim.Sample();
    }
    else
    {
      if (!Object.op_Inequality((Object) this.mAnimator, (Object) null))
        return;
      this.mAnimator.Play(this.mClip, 0, this.mLastDirection == AnimationOrTween.Direction.Forward ? 1f : 0.0f);
    }
  }

  public void Reset()
  {
    if (Object.op_Inequality((Object) this.mAnim, (Object) null))
    {
      foreach (AnimationState animationState in this.mAnim)
      {
        if (this.mLastDirection == AnimationOrTween.Direction.Reverse)
          animationState.time = animationState.length;
        else if (this.mLastDirection == AnimationOrTween.Direction.Forward)
          animationState.time = 0.0f;
      }
    }
    else
    {
      if (!Object.op_Inequality((Object) this.mAnimator, (Object) null))
        return;
      this.mAnimator.Play(this.mClip, 0, this.mLastDirection == AnimationOrTween.Direction.Reverse ? 1f : 0.0f);
    }
  }

  private void Start()
  {
    if (!Object.op_Inequality((Object) this.eventReceiver, (Object) null) || !EventDelegate.IsValid(this.onFinished))
      return;
    this.eventReceiver = (GameObject) null;
    this.callWhenFinished = (string) null;
  }

  private void Update()
  {
    float deltaTime = RealTime.deltaTime;
    if ((double) deltaTime == 0.0)
      return;
    if (Object.op_Inequality((Object) this.mAnimator, (Object) null))
    {
      this.mAnimator.Update(this.mLastDirection == AnimationOrTween.Direction.Reverse ? -deltaTime : deltaTime);
      if (this.isPlaying)
        return;
      ((Behaviour) this.mAnimator).enabled = false;
      ((Behaviour) this).enabled = false;
    }
    else if (Object.op_Inequality((Object) this.mAnim, (Object) null))
    {
      bool flag = false;
      foreach (AnimationState animationState in this.mAnim)
      {
        if (this.mAnim.IsPlaying(animationState.name))
        {
          float num = animationState.speed * deltaTime;
          animationState.time += num;
          if ((double) num < 0.0)
          {
            if ((double) animationState.time > 0.0)
              flag = true;
            else
              animationState.time = 0.0f;
          }
          else if ((double) animationState.time < (double) animationState.length)
            flag = true;
          else
            animationState.time = animationState.length;
        }
      }
      this.mAnim.Sample();
      if (flag)
        return;
      ((Behaviour) this).enabled = false;
    }
    else
    {
      ((Behaviour) this).enabled = false;
      return;
    }
    if (!this.mNotify)
      return;
    this.mNotify = false;
    if (Object.op_Equality((Object) ActiveAnimation.current, (Object) null))
    {
      ActiveAnimation.current = this;
      EventDelegate.Execute(this.onFinished);
      if (Object.op_Inequality((Object) this.eventReceiver, (Object) null) && !string.IsNullOrEmpty(this.callWhenFinished))
        this.eventReceiver.SendMessage(this.callWhenFinished, (SendMessageOptions) 1);
      ActiveAnimation.current = (ActiveAnimation) null;
    }
    if (this.mDisableDirection == AnimationOrTween.Direction.Toggle || this.mLastDirection != this.mDisableDirection)
      return;
    NGUITools.SetActive(((Component) this).gameObject, false);
  }

  private void Play(string clipName, AnimationOrTween.Direction playDirection)
  {
    if (playDirection == AnimationOrTween.Direction.Toggle)
      playDirection = this.mLastDirection != AnimationOrTween.Direction.Forward ? AnimationOrTween.Direction.Forward : AnimationOrTween.Direction.Reverse;
    if (Object.op_Inequality((Object) this.mAnim, (Object) null))
    {
      ((Behaviour) this).enabled = true;
      ((Behaviour) this.mAnim).enabled = false;
      if (string.IsNullOrEmpty(clipName))
      {
        if (!this.mAnim.isPlaying)
          this.mAnim.Play();
      }
      else if (!this.mAnim.IsPlaying(clipName))
        this.mAnim.Play(clipName);
      foreach (AnimationState animationState in this.mAnim)
      {
        if (string.IsNullOrEmpty(clipName) || animationState.name == clipName)
        {
          float num = Mathf.Abs(animationState.speed);
          animationState.speed = num * (float) playDirection;
          if (playDirection == AnimationOrTween.Direction.Reverse && (double) animationState.time == 0.0)
            animationState.time = animationState.length;
          else if (playDirection == AnimationOrTween.Direction.Forward && (double) animationState.time == (double) animationState.length)
            animationState.time = 0.0f;
        }
      }
      this.mLastDirection = playDirection;
      this.mNotify = true;
      this.mAnim.Sample();
    }
    else
    {
      if (!Object.op_Inequality((Object) this.mAnimator, (Object) null))
        return;
      if (((Behaviour) this).enabled && this.isPlaying && this.mClip == clipName)
      {
        this.mLastDirection = playDirection;
      }
      else
      {
        ((Behaviour) this).enabled = true;
        this.mNotify = true;
        this.mLastDirection = playDirection;
        this.mClip = clipName;
        this.mAnimator.Play(this.mClip, 0, playDirection == AnimationOrTween.Direction.Forward ? 0.0f : 1f);
      }
    }
  }

  public static ActiveAnimation Play(
    Animation anim,
    string clipName,
    AnimationOrTween.Direction playDirection,
    EnableCondition enableBeforePlay,
    DisableCondition disableCondition)
  {
    if (!NGUITools.GetActive(((Component) anim).gameObject))
    {
      if (enableBeforePlay != EnableCondition.EnableThenPlay)
        return (ActiveAnimation) null;
      NGUITools.SetActive(((Component) anim).gameObject, true);
      UIPanel[] componentsInChildren = ((Component) anim).gameObject.GetComponentsInChildren<UIPanel>();
      int index = 0;
      for (int length = componentsInChildren.Length; index < length; ++index)
        componentsInChildren[index].Refresh();
    }
    ActiveAnimation activeAnimation = ((Component) anim).GetComponent<ActiveAnimation>();
    if (Object.op_Equality((Object) activeAnimation, (Object) null))
      activeAnimation = ((Component) anim).gameObject.AddComponent<ActiveAnimation>();
    activeAnimation.mAnim = anim;
    activeAnimation.mDisableDirection = (AnimationOrTween.Direction) disableCondition;
    activeAnimation.onFinished.Clear();
    activeAnimation.Play(clipName, playDirection);
    if (Object.op_Inequality((Object) activeAnimation.mAnim, (Object) null))
      activeAnimation.mAnim.Sample();
    else if (Object.op_Inequality((Object) activeAnimation.mAnimator, (Object) null))
      activeAnimation.mAnimator.Update(0.0f);
    return activeAnimation;
  }

  public static ActiveAnimation Play(Animation anim, string clipName, AnimationOrTween.Direction playDirection)
  {
    return ActiveAnimation.Play(anim, clipName, playDirection, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
  }

  public static ActiveAnimation Play(Animation anim, AnimationOrTween.Direction playDirection)
  {
    return ActiveAnimation.Play(anim, (string) null, playDirection, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
  }

  public static ActiveAnimation Play(
    Animator anim,
    string clipName,
    AnimationOrTween.Direction playDirection,
    EnableCondition enableBeforePlay,
    DisableCondition disableCondition)
  {
    if (enableBeforePlay != EnableCondition.IgnoreDisabledState && !NGUITools.GetActive(((Component) anim).gameObject))
    {
      if (enableBeforePlay != EnableCondition.EnableThenPlay)
        return (ActiveAnimation) null;
      NGUITools.SetActive(((Component) anim).gameObject, true);
      UIPanel[] componentsInChildren = ((Component) anim).gameObject.GetComponentsInChildren<UIPanel>();
      int index = 0;
      for (int length = componentsInChildren.Length; index < length; ++index)
        componentsInChildren[index].Refresh();
    }
    ActiveAnimation activeAnimation = ((Component) anim).GetComponent<ActiveAnimation>();
    if (Object.op_Equality((Object) activeAnimation, (Object) null))
      activeAnimation = ((Component) anim).gameObject.AddComponent<ActiveAnimation>();
    activeAnimation.mAnimator = anim;
    activeAnimation.mDisableDirection = (AnimationOrTween.Direction) disableCondition;
    activeAnimation.onFinished.Clear();
    activeAnimation.Play(clipName, playDirection);
    if (Object.op_Inequality((Object) activeAnimation.mAnim, (Object) null))
      activeAnimation.mAnim.Sample();
    else if (Object.op_Inequality((Object) activeAnimation.mAnimator, (Object) null))
      activeAnimation.mAnimator.Update(0.0f);
    return activeAnimation;
  }
}
