// Decompiled with JetBrains decompiler
// Type: UIToggle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using AnimationOrTween;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Toggle")]
public class UIToggle : UIWidgetContainer
{
  public static BetterList<UIToggle> list = new BetterList<UIToggle>();
  public static UIToggle current;
  public int group;
  public UIWidget activeSprite;
  public Animation activeAnimation;
  public Animator animator;
  public bool startsActive;
  public bool instantTween;
  public bool optionCanBeNone;
  public List<EventDelegate> onChange = new List<EventDelegate>();
  public UIToggle.Validate validator;
  [HideInInspector]
  [SerializeField]
  private UISprite checkSprite;
  [HideInInspector]
  [SerializeField]
  private Animation checkAnimation;
  [HideInInspector]
  [SerializeField]
  private GameObject eventReceiver;
  [HideInInspector]
  [SerializeField]
  private string functionName = "OnActivate";
  [HideInInspector]
  [SerializeField]
  private bool startsChecked;
  private bool mIsActive = true;
  private bool mStarted;

  public bool value
  {
    get => !this.mStarted ? this.startsActive : this.mIsActive;
    set
    {
      if (!this.mStarted)
      {
        this.startsActive = value;
      }
      else
      {
        if (!(this.group == 0 | value) && !this.optionCanBeNone && this.mStarted)
          return;
        this.Set(value);
      }
    }
  }

  public bool isColliderEnabled
  {
    get
    {
      Collider component1 = ((Component) this).GetComponent<Collider>();
      if (Object.op_Inequality((Object) component1, (Object) null))
        return component1.enabled;
      Collider2D component2 = ((Component) this).GetComponent<Collider2D>();
      return Object.op_Inequality((Object) component2, (Object) null) && ((Behaviour) component2).enabled;
    }
  }

  [Obsolete("Use 'value' instead")]
  public bool isChecked
  {
    get => this.value;
    set => this.value = value;
  }

  public static UIToggle GetActiveToggle(int group)
  {
    for (int i = 0; i < UIToggle.list.size; ++i)
    {
      UIToggle activeToggle = UIToggle.list[i];
      if (Object.op_Inequality((Object) activeToggle, (Object) null) && activeToggle.group == group && activeToggle.mIsActive)
        return activeToggle;
    }
    return (UIToggle) null;
  }

  private void OnEnable() => UIToggle.list.Add(this);

  private void OnDisable() => UIToggle.list.Remove(this);

  private void Start()
  {
    if (this.startsChecked)
    {
      this.startsChecked = false;
      this.startsActive = true;
    }
    if (!Application.isPlaying)
    {
      if (Object.op_Inequality((Object) this.checkSprite, (Object) null) && Object.op_Equality((Object) this.activeSprite, (Object) null))
      {
        this.activeSprite = (UIWidget) this.checkSprite;
        this.checkSprite = (UISprite) null;
      }
      if (Object.op_Inequality((Object) this.checkAnimation, (Object) null) && Object.op_Equality((Object) this.activeAnimation, (Object) null))
      {
        this.activeAnimation = this.checkAnimation;
        this.checkAnimation = (Animation) null;
      }
      if (Application.isPlaying && Object.op_Inequality((Object) this.activeSprite, (Object) null))
        this.activeSprite.alpha = this.startsActive ? 1f : 0.0f;
      if (!EventDelegate.IsValid(this.onChange))
        return;
      this.eventReceiver = (GameObject) null;
      this.functionName = (string) null;
    }
    else
    {
      this.mIsActive = !this.startsActive;
      this.mStarted = true;
      bool instantTween = this.instantTween;
      this.instantTween = true;
      this.Set(this.startsActive);
      this.instantTween = instantTween;
    }
  }

  private void OnClick()
  {
    if (!((Behaviour) this).enabled || !this.isColliderEnabled || UICamera.currentTouchID == -2)
      return;
    this.value = !this.value;
  }

  public void Set(bool state)
  {
    if (this.validator != null && !this.validator(state))
      return;
    if (!this.mStarted)
    {
      this.mIsActive = state;
      this.startsActive = state;
      if (!Object.op_Inequality((Object) this.activeSprite, (Object) null))
        return;
      this.activeSprite.alpha = state ? 1f : 0.0f;
    }
    else
    {
      if (this.mIsActive == state)
        return;
      if (this.group != 0 & state)
      {
        int i = 0;
        int size = UIToggle.list.size;
        while (i < size)
        {
          UIToggle uiToggle = UIToggle.list[i];
          if (Object.op_Inequality((Object) uiToggle, (Object) this) && uiToggle.group == this.group)
            uiToggle.Set(false);
          if (UIToggle.list.size != size)
          {
            size = UIToggle.list.size;
            i = 0;
          }
          else
            ++i;
        }
      }
      this.mIsActive = state;
      if (Object.op_Inequality((Object) this.activeSprite, (Object) null))
      {
        if (this.instantTween || !NGUITools.GetActive((Behaviour) this))
          this.activeSprite.alpha = this.mIsActive ? 1f : 0.0f;
        else
          TweenAlpha.Begin(((Component) this.activeSprite).gameObject, 0.15f, this.mIsActive ? 1f : 0.0f);
      }
      if (Object.op_Equality((Object) UIToggle.current, (Object) null))
      {
        UIToggle current = UIToggle.current;
        UIToggle.current = this;
        if (EventDelegate.IsValid(this.onChange))
          EventDelegate.Execute(this.onChange);
        else if (Object.op_Inequality((Object) this.eventReceiver, (Object) null) && !string.IsNullOrEmpty(this.functionName))
          this.eventReceiver.SendMessage(this.functionName, (object) this.mIsActive, (SendMessageOptions) 1);
        UIToggle.current = current;
      }
      if (Object.op_Inequality((Object) this.animator, (Object) null))
      {
        ActiveAnimation activeAnimation = ActiveAnimation.Play(this.animator, (string) null, state ? AnimationOrTween.Direction.Forward : AnimationOrTween.Direction.Reverse, EnableCondition.IgnoreDisabledState, DisableCondition.DoNotDisable);
        if (!Object.op_Inequality((Object) activeAnimation, (Object) null) || !this.instantTween && NGUITools.GetActive((Behaviour) this))
          return;
        activeAnimation.Finish();
      }
      else
      {
        if (!Object.op_Inequality((Object) this.activeAnimation, (Object) null))
          return;
        ActiveAnimation activeAnimation = ActiveAnimation.Play(this.activeAnimation, (string) null, state ? AnimationOrTween.Direction.Forward : AnimationOrTween.Direction.Reverse, EnableCondition.IgnoreDisabledState, DisableCondition.DoNotDisable);
        if (!Object.op_Inequality((Object) activeAnimation, (Object) null) || !this.instantTween && NGUITools.GetActive((Behaviour) this))
          return;
        activeAnimation.Finish();
      }
    }
  }

  public delegate bool Validate(bool choice);
}
