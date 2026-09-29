// Decompiled with JetBrains decompiler
// Type: UIButtonRotation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Interaction/Button Rotation")]
public class UIButtonRotation : MonoBehaviour
{
  public Transform tweenTarget;
  public Vector3 hover = Vector3.zero;
  public Vector3 pressed = Vector3.zero;
  public float duration = 0.2f;
  private Quaternion mRot;
  private bool mStarted;

  private void Start()
  {
    if (this.mStarted)
      return;
    this.mStarted = true;
    if (Object.op_Equality((Object) this.tweenTarget, (Object) null))
      this.tweenTarget = ((Component) this).transform;
    this.mRot = this.tweenTarget.localRotation;
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
    TweenRotation component = ((Component) this.tweenTarget).GetComponent<TweenRotation>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.value = this.mRot;
    ((Behaviour) component).enabled = false;
  }

  private void OnPress(bool isPressed)
  {
    if (!((Behaviour) this).enabled)
      return;
    if (!this.mStarted)
      this.Start();
    TweenRotation.Begin(((Component) this.tweenTarget).gameObject, this.duration, isPressed ? Quaternion.op_Multiply(this.mRot, Quaternion.Euler(this.pressed)) : (UICamera.IsHighlighted(((Component) this).gameObject) ? Quaternion.op_Multiply(this.mRot, Quaternion.Euler(this.hover)) : this.mRot)).method = UITweener.Method.EaseInOut;
  }

  private void OnHover(bool isOver)
  {
    if (!((Behaviour) this).enabled)
      return;
    if (!this.mStarted)
      this.Start();
    TweenRotation.Begin(((Component) this.tweenTarget).gameObject, this.duration, isOver ? Quaternion.op_Multiply(this.mRot, Quaternion.Euler(this.hover)) : this.mRot).method = UITweener.Method.EaseInOut;
  }

  private void OnSelect(bool isSelected)
  {
    if (!((Behaviour) this).enabled || isSelected && UICamera.currentScheme != UICamera.ControlScheme.Controller)
      return;
    this.OnHover(isSelected);
  }
}
