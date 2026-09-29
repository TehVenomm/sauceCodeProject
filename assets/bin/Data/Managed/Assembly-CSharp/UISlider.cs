// Decompiled with JetBrains decompiler
// Type: UISlider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/NGUI Slider")]
public class UISlider : UIProgressBar
{
  [HideInInspector]
  [SerializeField]
  private Transform foreground;
  [HideInInspector]
  [SerializeField]
  private float rawValue = 1f;
  [HideInInspector]
  [SerializeField]
  private UISlider.Direction direction = UISlider.Direction.Upgraded;
  [HideInInspector]
  [SerializeField]
  protected bool mInverted;

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
  public float sliderValue
  {
    get => this.value;
    set => this.value = value;
  }

  [Obsolete("Use 'fillDirection' instead")]
  public bool inverted
  {
    get => this.isInverted;
    set
    {
    }
  }

  protected override void Upgrade()
  {
    if (this.direction == UISlider.Direction.Upgraded)
      return;
    this.mValue = this.rawValue;
    if (Object.op_Inequality((Object) this.foreground, (Object) null))
      this.mFG = ((Component) this.foreground).GetComponent<UIWidget>();
    if (this.direction == UISlider.Direction.Horizontal)
      this.mFill = this.mInverted ? UIProgressBar.FillDirection.RightToLeft : UIProgressBar.FillDirection.LeftToRight;
    else
      this.mFill = this.mInverted ? UIProgressBar.FillDirection.TopToBottom : UIProgressBar.FillDirection.BottomToTop;
    this.direction = UISlider.Direction.Upgraded;
  }

  protected override void OnStart()
  {
    UIEventListener uiEventListener1 = UIEventListener.Get(!Object.op_Inequality((Object) this.mBG, (Object) null) || !Object.op_Inequality((Object) ((Component) this.mBG).GetComponent<Collider>(), (Object) null) && !Object.op_Inequality((Object) ((Component) this.mBG).GetComponent<Collider2D>(), (Object) null) ? ((Component) this).gameObject : ((Component) this.mBG).gameObject);
    uiEventListener1.onPress += new UIEventListener.BoolDelegate(this.OnPressBackground);
    uiEventListener1.onDrag += new UIEventListener.VectorDelegate(this.OnDragBackground);
    if (!Object.op_Inequality((Object) this.thumb, (Object) null) || !Object.op_Inequality((Object) ((Component) this.thumb).GetComponent<Collider>(), (Object) null) && !Object.op_Inequality((Object) ((Component) this.thumb).GetComponent<Collider2D>(), (Object) null) || !Object.op_Equality((Object) this.mFG, (Object) null) && !Object.op_Inequality((Object) this.thumb, (Object) this.mFG.cachedTransform))
      return;
    UIEventListener uiEventListener2 = UIEventListener.Get(((Component) this.thumb).gameObject);
    uiEventListener2.onPress += new UIEventListener.BoolDelegate(this.OnPressForeground);
    uiEventListener2.onDrag += new UIEventListener.VectorDelegate(this.OnDragForeground);
  }

  protected void OnPressBackground(GameObject go, bool isPressed)
  {
    if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
      return;
    this.mCam = UICamera.currentCamera;
    this.value = this.ScreenToValue(UICamera.lastEventPosition);
    if (isPressed || this.onDragFinished == null)
      return;
    this.onDragFinished();
  }

  protected void OnDragBackground(GameObject go, Vector2 delta)
  {
    if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
      return;
    this.mCam = UICamera.currentCamera;
    this.value = this.ScreenToValue(UICamera.lastEventPosition);
  }

  protected void OnPressForeground(GameObject go, bool isPressed)
  {
    if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
      return;
    this.mCam = UICamera.currentCamera;
    if (isPressed)
    {
      this.mOffset = Object.op_Equality((Object) this.mFG, (Object) null) ? 0.0f : this.value - this.ScreenToValue(UICamera.lastEventPosition);
    }
    else
    {
      if (this.onDragFinished == null)
        return;
      this.onDragFinished();
    }
  }

  protected void OnDragForeground(GameObject go, Vector2 delta)
  {
    if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
      return;
    this.mCam = UICamera.currentCamera;
    this.value = this.mOffset + this.ScreenToValue(UICamera.lastEventPosition);
  }

  public override void OnPan(Vector2 delta)
  {
    if (!((Behaviour) this).enabled || !this.isColliderEnabled)
      return;
    base.OnPan(delta);
  }

  private enum Direction
  {
    Horizontal,
    Vertical,
    Upgraded,
  }
}
