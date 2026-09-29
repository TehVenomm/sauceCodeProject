// Decompiled with JetBrains decompiler
// Type: UIButtonOffset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Interaction/Button Offset")]
public class UIButtonOffset : MonoBehaviour
{
  public Transform tweenTarget;
  public Vector3 hover = Vector3.zero;
  public Vector3 pressed = new Vector3(2f, -2f);
  public float duration = 0.2f;
  [NonSerialized]
  private Vector3 mPos;
  [NonSerialized]
  private bool mStarted;
  [NonSerialized]
  private bool mPressed;

  private void Start()
  {
    if (this.mStarted)
      return;
    this.mStarted = true;
    if (Object.op_Equality((Object) this.tweenTarget, (Object) null))
      this.tweenTarget = ((Component) this).transform;
    this.mPos = this.tweenTarget.localPosition;
  }

  private void OnEnable()
  {
    if (!this.mStarted)
      return;
    this.OnHover(UICamera.IsHighlighted(((Component) this).gameObject));
  }

  private void OnDisable()
  {
    if (!this.mStarted || !Object.op_Inequality((Object) this.tweenTarget, (Object) null))
      return;
    TweenPosition component = ((Component) this.tweenTarget).GetComponent<TweenPosition>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.value = this.mPos;
    ((Behaviour) component).enabled = false;
  }

  private void OnPress(bool isPressed)
  {
    this.mPressed = isPressed;
    if (!((Behaviour) this).enabled)
      return;
    if (!this.mStarted)
      this.Start();
    TweenPosition.Begin(((Component) this.tweenTarget).gameObject, this.duration, isPressed ? Vector3.op_Addition(this.mPos, this.pressed) : (UICamera.IsHighlighted(((Component) this).gameObject) ? Vector3.op_Addition(this.mPos, this.hover) : this.mPos)).method = UITweener.Method.EaseInOut;
  }

  private void OnHover(bool isOver)
  {
    if (!((Behaviour) this).enabled)
      return;
    if (!this.mStarted)
      this.Start();
    TweenPosition.Begin(((Component) this.tweenTarget).gameObject, this.duration, isOver ? Vector3.op_Addition(this.mPos, this.hover) : this.mPos).method = UITweener.Method.EaseInOut;
  }

  private void OnDragOver()
  {
    if (!this.mPressed)
      return;
    TweenPosition.Begin(((Component) this.tweenTarget).gameObject, this.duration, Vector3.op_Addition(this.mPos, this.hover)).method = UITweener.Method.EaseInOut;
  }

  private void OnDragOut()
  {
    if (!this.mPressed)
      return;
    TweenPosition.Begin(((Component) this.tweenTarget).gameObject, this.duration, this.mPos).method = UITweener.Method.EaseInOut;
  }

  private void OnSelect(bool isSelected)
  {
    if (!((Behaviour) this).enabled || isSelected && UICamera.currentScheme != UICamera.ControlScheme.Controller)
      return;
    this.OnHover(isSelected);
  }
}
