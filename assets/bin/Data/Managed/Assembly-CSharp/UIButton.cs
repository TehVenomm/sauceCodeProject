// Decompiled with JetBrains decompiler
// Type: UIButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Interaction/Button")]
public class UIButton : UIButtonColor
{
  public static UIButton current;
  public bool dragHighlight;
  public string hoverSprite;
  public string pressedSprite;
  public string disabledSprite;
  public Sprite hoverSprite2D;
  public Sprite pressedSprite2D;
  public Sprite disabledSprite2D;
  public bool pixelSnap;
  public List<EventDelegate> onClick = new List<EventDelegate>();
  [NonSerialized]
  private UISprite mSprite;
  [NonSerialized]
  private UI2DSprite mSprite2D;
  [NonSerialized]
  private string mNormalSprite;
  [NonSerialized]
  private Sprite mNormalSprite2D;
  private bool isCheckNeedButtonEffect;
  private UIButtonEffect AutoAddButtonEffect;

  public override bool isEnabled
  {
    get
    {
      if (!((Behaviour) this).enabled)
        return false;
      Collider component1 = ((Component) this).gameObject.GetComponent<Collider>();
      if (Object.op_Implicit((Object) component1) && component1.enabled)
        return true;
      Collider2D component2 = ((Component) this).GetComponent<Collider2D>();
      return Object.op_Implicit((Object) component2) && ((Behaviour) component2).enabled;
    }
    set
    {
      if (this.isEnabled == value)
        return;
      Collider component1 = ((Component) this).gameObject.GetComponent<Collider>();
      if (Object.op_Inequality((Object) component1, (Object) null))
      {
        component1.enabled = value;
        foreach (UIButtonColor component2 in ((Component) this).GetComponents<UIButton>())
          component2.SetState(value ? UIButtonColor.State.Normal : UIButtonColor.State.Disabled, false);
      }
      else
      {
        Collider2D component3 = ((Component) this).GetComponent<Collider2D>();
        if (Object.op_Inequality((Object) component3, (Object) null))
        {
          ((Behaviour) component3).enabled = value;
          foreach (UIButtonColor component4 in ((Component) this).GetComponents<UIButton>())
            component4.SetState(value ? UIButtonColor.State.Normal : UIButtonColor.State.Disabled, false);
        }
        else
          ((Behaviour) this).enabled = value;
      }
    }
  }

  public string normalSprite
  {
    get
    {
      if (!this.mInitDone)
        this.OnInit();
      return this.mNormalSprite;
    }
    set
    {
      if (!this.mInitDone)
        this.OnInit();
      if (Object.op_Inequality((Object) this.mSprite, (Object) null) && !string.IsNullOrEmpty(this.mNormalSprite) && this.mNormalSprite == this.mSprite.spriteName)
      {
        this.mNormalSprite = value;
        this.SetSprite(value);
        NGUITools.SetDirty((Object) this.mSprite);
      }
      else
      {
        this.mNormalSprite = value;
        if (this.mState != UIButtonColor.State.Normal)
          return;
        this.SetSprite(value);
      }
    }
  }

  public Sprite normalSprite2D
  {
    get
    {
      if (!this.mInitDone)
        this.OnInit();
      return this.mNormalSprite2D;
    }
    set
    {
      if (!this.mInitDone)
        this.OnInit();
      if (Object.op_Inequality((Object) this.mSprite2D, (Object) null) && Object.op_Equality((Object) this.mNormalSprite2D, (Object) this.mSprite2D.sprite2D))
      {
        this.mNormalSprite2D = value;
        this.SetSprite(value);
        NGUITools.SetDirty((Object) this.mSprite);
      }
      else
      {
        this.mNormalSprite2D = value;
        if (this.mState != UIButtonColor.State.Normal)
          return;
        this.SetSprite(value);
      }
    }
  }

  protected override void OnInit()
  {
    base.OnInit();
    this.mSprite = this.mWidget as UISprite;
    this.mSprite2D = this.mWidget as UI2DSprite;
    if (Object.op_Inequality((Object) this.mSprite, (Object) null))
      this.mNormalSprite = this.mSprite.spriteName;
    if (!Object.op_Inequality((Object) this.mSprite2D, (Object) null))
      return;
    this.mNormalSprite2D = this.mSprite2D.sprite2D;
  }

  protected override void OnEnable()
  {
    if (this.isEnabled)
    {
      if (!this.mInitDone)
        return;
      this.OnHover(Object.op_Equality((Object) UICamera.hoveredObject, (Object) ((Component) this).gameObject));
    }
    else
      this.SetState(UIButtonColor.State.Disabled, true);
  }

  protected override void OnDragOver()
  {
    if (!this.isEnabled || !this.dragHighlight && !Object.op_Equality((Object) UICamera.currentTouch.pressed, (Object) ((Component) this).gameObject))
      return;
    base.OnDragOver();
  }

  protected override void OnDragOut()
  {
    if (!this.isEnabled || !this.dragHighlight && !Object.op_Equality((Object) UICamera.currentTouch.pressed, (Object) ((Component) this).gameObject))
      return;
    base.OnDragOut();
  }

  protected virtual void OnClick()
  {
    if (!Object.op_Equality((Object) UIButton.current, (Object) null) || !this.isEnabled || !TutorialMessage.IsActiveButton(((Component) this).gameObject))
      return;
    UIButton.current = this;
    EventDelegate.Execute(this.onClick);
    UIButton.current = (UIButton) null;
  }

  public override void SetState(UIButtonColor.State state, bool immediate)
  {
    base.SetState(state, immediate);
    if (Object.op_Inequality((Object) this.mSprite, (Object) null))
    {
      switch (state)
      {
        case UIButtonColor.State.Normal:
          this.SetSprite(this.mNormalSprite);
          break;
        case UIButtonColor.State.Hover:
          this.SetSprite(string.IsNullOrEmpty(this.hoverSprite) ? this.mNormalSprite : this.hoverSprite);
          break;
        case UIButtonColor.State.Pressed:
          this.SetSprite(this.pressedSprite);
          break;
        case UIButtonColor.State.Disabled:
          this.SetSprite(this.disabledSprite);
          break;
      }
    }
    else
    {
      if (!Object.op_Inequality((Object) this.mSprite2D, (Object) null))
        return;
      switch (state)
      {
        case UIButtonColor.State.Normal:
          this.SetSprite(this.mNormalSprite2D);
          break;
        case UIButtonColor.State.Hover:
          this.SetSprite(Object.op_Equality((Object) this.hoverSprite2D, (Object) null) ? this.mNormalSprite2D : this.hoverSprite2D);
          break;
        case UIButtonColor.State.Pressed:
          this.SetSprite(this.pressedSprite2D);
          break;
        case UIButtonColor.State.Disabled:
          this.SetSprite(this.disabledSprite2D);
          break;
      }
    }
  }

  protected void SetSprite(string sp)
  {
    if (!Object.op_Inequality((Object) this.mSprite, (Object) null) || string.IsNullOrEmpty(sp) || !(this.mSprite.spriteName != sp))
      return;
    this.mSprite.spriteName = sp;
    if (!this.pixelSnap)
      return;
    this.mSprite.MakePixelPerfect();
  }

  protected void SetSprite(Sprite sp)
  {
    if (!Object.op_Inequality((Object) sp, (Object) null) || !Object.op_Inequality((Object) this.mSprite2D, (Object) null) || !Object.op_Inequality((Object) this.mSprite2D.sprite2D, (Object) sp))
      return;
    this.mSprite2D.sprite2D = sp;
    if (!this.pixelSnap)
      return;
    this.mSprite2D.MakePixelPerfect();
  }

  protected override void OnPress(bool isPressed)
  {
    if (!this.isCheckNeedButtonEffect)
    {
      if (Object.op_Equality((Object) ((Component) this).GetComponent<UINoAuto>(), (Object) null) && Object.op_Equality((Object) null, (Object) ((Component) this).GetComponent<UIButtonEffect>()))
        this.AutoAddButtonEffect = ((Component) this).gameObject.AddComponent<UIButtonEffect>();
      this.isCheckNeedButtonEffect = true;
    }
    base.OnPress(isPressed);
  }

  public void RemoveAutoAddButtonEffect()
  {
    if (!this.isCheckNeedButtonEffect)
      return;
    if (Object.op_Inequality((Object) this.AutoAddButtonEffect, (Object) null))
    {
      Object.Destroy((Object) this.AutoAddButtonEffect);
      this.AutoAddButtonEffect = (UIButtonEffect) null;
    }
    this.isCheckNeedButtonEffect = false;
  }
}
