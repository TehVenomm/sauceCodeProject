// Decompiled with JetBrains decompiler
// Type: UIPlayAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using AnimationOrTween;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Play Animation")]
public class UIPlayAnimation : MonoBehaviour
{
  public static UIPlayAnimation current;
  public Animation target;
  public Animator animator;
  public string clipName;
  public AnimationOrTween.Trigger trigger;
  public AnimationOrTween.Direction playDirection = AnimationOrTween.Direction.Forward;
  public bool resetOnPlay;
  public bool clearSelection;
  public EnableCondition ifDisabledOnPlay;
  public DisableCondition disableWhenFinished;
  public List<EventDelegate> onFinished = new List<EventDelegate>();
  [HideInInspector]
  [SerializeField]
  private GameObject eventReceiver;
  [HideInInspector]
  [SerializeField]
  private string callWhenFinished;
  private bool mStarted;
  private bool mActivated;
  private bool dragHighlight;

  private bool dualState => this.trigger == AnimationOrTween.Trigger.OnPress || this.trigger == AnimationOrTween.Trigger.OnHover;

  private void Awake()
  {
    UIButton component = ((Component) this).GetComponent<UIButton>();
    if (Object.op_Inequality((Object) component, (Object) null))
      this.dragHighlight = component.dragHighlight;
    if (!Object.op_Inequality((Object) this.eventReceiver, (Object) null) || !EventDelegate.IsValid(this.onFinished))
      return;
    this.eventReceiver = (GameObject) null;
    this.callWhenFinished = (string) null;
  }

  private void Start()
  {
    this.mStarted = true;
    if (Object.op_Equality((Object) this.target, (Object) null) && Object.op_Equality((Object) this.animator, (Object) null))
      this.animator = ((Component) this).GetComponentInChildren<Animator>();
    if (Object.op_Inequality((Object) this.animator, (Object) null))
    {
      if (!((Behaviour) this.animator).enabled)
        return;
      ((Behaviour) this.animator).enabled = false;
    }
    else
    {
      if (Object.op_Equality((Object) this.target, (Object) null))
        this.target = ((Component) this).GetComponentInChildren<Animation>();
      if (!Object.op_Inequality((Object) this.target, (Object) null) || !((Behaviour) this.target).enabled)
        return;
      ((Behaviour) this.target).enabled = false;
    }
  }

  private void OnEnable()
  {
    if (this.mStarted)
      this.OnHover(UICamera.IsHighlighted(((Component) this).gameObject));
    if (UICamera.currentTouch != null)
    {
      if (this.trigger == AnimationOrTween.Trigger.OnPress || this.trigger == AnimationOrTween.Trigger.OnPressTrue)
        this.mActivated = Object.op_Equality((Object) UICamera.currentTouch.pressed, (Object) ((Component) this).gameObject);
      if (this.trigger == AnimationOrTween.Trigger.OnHover || this.trigger == AnimationOrTween.Trigger.OnHoverTrue)
        this.mActivated = Object.op_Equality((Object) UICamera.currentTouch.current, (Object) ((Component) this).gameObject);
    }
    UIToggle component = ((Component) this).GetComponent<UIToggle>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    EventDelegate.Add(component.onChange, new EventDelegate.Callback(this.OnToggle));
  }

  private void OnDisable()
  {
    UIToggle component = ((Component) this).GetComponent<UIToggle>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    EventDelegate.Remove(component.onChange, new EventDelegate.Callback(this.OnToggle));
  }

  private void OnHover(bool isOver)
  {
    if (!((Behaviour) this).enabled || this.trigger != AnimationOrTween.Trigger.OnHover && !(this.trigger == AnimationOrTween.Trigger.OnHoverTrue & isOver) && (this.trigger != AnimationOrTween.Trigger.OnHoverFalse || isOver))
      return;
    this.Play(isOver, this.dualState);
  }

  private void OnPress(bool isPressed)
  {
    if (!((Behaviour) this).enabled || UICamera.currentTouchID < -1 && UICamera.currentScheme != UICamera.ControlScheme.Controller || this.trigger != AnimationOrTween.Trigger.OnPress && !(this.trigger == AnimationOrTween.Trigger.OnPressTrue & isPressed) && (this.trigger != AnimationOrTween.Trigger.OnPressFalse || isPressed))
      return;
    this.Play(isPressed, this.dualState);
  }

  private void OnClick()
  {
    if (UICamera.currentTouchID < -1 && UICamera.currentScheme != UICamera.ControlScheme.Controller || !((Behaviour) this).enabled || this.trigger != AnimationOrTween.Trigger.OnClick)
      return;
    this.Play(true, false);
  }

  private void OnDoubleClick()
  {
    if (UICamera.currentTouchID < -1 && UICamera.currentScheme != UICamera.ControlScheme.Controller || !((Behaviour) this).enabled || this.trigger != AnimationOrTween.Trigger.OnDoubleClick)
      return;
    this.Play(true, false);
  }

  private void OnSelect(bool isSelected)
  {
    if (!((Behaviour) this).enabled || this.trigger != AnimationOrTween.Trigger.OnSelect && !(this.trigger == AnimationOrTween.Trigger.OnSelectTrue & isSelected) && (this.trigger != AnimationOrTween.Trigger.OnSelectFalse || isSelected))
      return;
    this.Play(isSelected, this.dualState);
  }

  private void OnToggle()
  {
    if (!((Behaviour) this).enabled || Object.op_Equality((Object) UIToggle.current, (Object) null) || this.trigger != AnimationOrTween.Trigger.OnActivate && (this.trigger != AnimationOrTween.Trigger.OnActivateTrue || !UIToggle.current.value) && (this.trigger != AnimationOrTween.Trigger.OnActivateFalse || UIToggle.current.value))
      return;
    this.Play(UIToggle.current.value, this.dualState);
  }

  private void OnDragOver()
  {
    if (!((Behaviour) this).enabled || !this.dualState)
      return;
    if (Object.op_Equality((Object) UICamera.currentTouch.dragged, (Object) ((Component) this).gameObject))
    {
      this.Play(true, true);
    }
    else
    {
      if (!this.dragHighlight || this.trigger != AnimationOrTween.Trigger.OnPress)
        return;
      this.Play(true, true);
    }
  }

  private void OnDragOut()
  {
    if (!((Behaviour) this).enabled || !this.dualState || !Object.op_Inequality((Object) UICamera.hoveredObject, (Object) ((Component) this).gameObject))
      return;
    this.Play(false, true);
  }

  private void OnDrop(GameObject go)
  {
    if (!((Behaviour) this).enabled || this.trigger != AnimationOrTween.Trigger.OnPress || !Object.op_Inequality((Object) UICamera.currentTouch.dragged, (Object) ((Component) this).gameObject))
      return;
    this.Play(false, true);
  }

  public void Play(bool forward) => this.Play(forward, true);

  public void Play(bool forward, bool onlyIfDifferent)
  {
    if (!Object.op_Implicit((Object) this.target) && !Object.op_Implicit((Object) this.animator))
      return;
    if (onlyIfDifferent)
    {
      if (this.mActivated == forward)
        return;
      this.mActivated = forward;
    }
    if (this.clearSelection && Object.op_Equality((Object) UICamera.selectedObject, (Object) ((Component) this).gameObject))
      UICamera.selectedObject = (GameObject) null;
    int num = -(int) this.playDirection;
    AnimationOrTween.Direction playDirection = forward ? this.playDirection : (AnimationOrTween.Direction) num;
    ActiveAnimation activeAnimation = Object.op_Implicit((Object) this.target) ? ActiveAnimation.Play(this.target, this.clipName, playDirection, this.ifDisabledOnPlay, this.disableWhenFinished) : ActiveAnimation.Play(this.animator, this.clipName, playDirection, this.ifDisabledOnPlay, this.disableWhenFinished);
    if (!Object.op_Inequality((Object) activeAnimation, (Object) null))
      return;
    if (this.resetOnPlay)
      activeAnimation.Reset();
    for (int index = 0; index < this.onFinished.Count; ++index)
      EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinished), true);
  }

  public void PlayForward() => this.Play(true);

  public void PlayReverse() => this.Play(false);

  private void OnFinished()
  {
    if (!Object.op_Equality((Object) UIPlayAnimation.current, (Object) null))
      return;
    UIPlayAnimation.current = this;
    EventDelegate.Execute(this.onFinished);
    if (Object.op_Inequality((Object) this.eventReceiver, (Object) null) && !string.IsNullOrEmpty(this.callWhenFinished))
      this.eventReceiver.SendMessage(this.callWhenFinished, (SendMessageOptions) 1);
    this.eventReceiver = (GameObject) null;
    UIPlayAnimation.current = (UIPlayAnimation) null;
  }
}
