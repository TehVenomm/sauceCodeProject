// Decompiled with JetBrains decompiler
// Type: UIButtonScale
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Interaction/Button Scale")]
public class UIButtonScale : MonoBehaviour
{
  public Transform tweenTarget;
  public Vector3 hover = new Vector3(1.1f, 1.1f, 1.1f);
  public Vector3 pressed = new Vector3(1.05f, 1.05f, 1.05f);
  public float duration = 0.2f;
  private Vector3 mScale;
  private bool mStarted;

  private void Start()
  {
    if (this.mStarted)
      return;
    this.mStarted = true;
    if (Object.op_Equality((Object) this.tweenTarget, (Object) null))
      this.tweenTarget = ((Component) this).transform;
    this.mScale = this.tweenTarget.localScale;
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
    TweenScale component = ((Component) this.tweenTarget).GetComponent<TweenScale>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.value = this.mScale;
    ((Behaviour) component).enabled = false;
  }

  private void OnPress(bool isPressed)
  {
    if (!((Behaviour) this).enabled)
      return;
    if (!this.mStarted)
      this.Start();
    TweenScale.Begin(((Component) this.tweenTarget).gameObject, this.duration, isPressed ? Vector3.Scale(this.mScale, this.pressed) : (UICamera.IsHighlighted(((Component) this).gameObject) ? Vector3.Scale(this.mScale, this.hover) : this.mScale)).method = UITweener.Method.EaseInOut;
  }

  private void OnHover(bool isOver)
  {
    if (!((Behaviour) this).enabled)
      return;
    if (!this.mStarted)
      this.Start();
    TweenScale.Begin(((Component) this.tweenTarget).gameObject, this.duration, isOver ? Vector3.Scale(this.mScale, this.hover) : this.mScale).method = UITweener.Method.EaseInOut;
  }

  private void OnSelect(bool isSelected)
  {
    if (!((Behaviour) this).enabled || isSelected && UICamera.currentScheme != UICamera.ControlScheme.Controller)
      return;
    this.OnHover(isSelected);
  }
}
