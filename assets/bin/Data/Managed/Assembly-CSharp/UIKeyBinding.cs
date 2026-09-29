// Decompiled with JetBrains decompiler
// Type: UIKeyBinding
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Interaction/Key Binding")]
public class UIKeyBinding : MonoBehaviour
{
  private static List<UIKeyBinding> mList = new List<UIKeyBinding>();
  public KeyCode keyCode;
  public UIKeyBinding.Modifier modifier;
  public UIKeyBinding.Action action;
  [NonSerialized]
  private bool mIgnoreUp;
  [NonSerialized]
  private bool mIsInput;
  [NonSerialized]
  private bool mPress;

  public static bool IsBound(KeyCode key)
  {
    int index = 0;
    for (int count = UIKeyBinding.mList.Count; index < count; ++index)
    {
      UIKeyBinding m = UIKeyBinding.mList[index];
      if (Object.op_Inequality((Object) m, (Object) null) && m.keyCode == key)
        return true;
    }
    return false;
  }

  protected virtual void OnEnable() => UIKeyBinding.mList.Add(this);

  protected virtual void OnDisable() => UIKeyBinding.mList.Remove(this);

  protected virtual void Start()
  {
    UIInput component = ((Component) this).GetComponent<UIInput>();
    this.mIsInput = Object.op_Inequality((Object) component, (Object) null);
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    EventDelegate.Add(component.onSubmit, new EventDelegate.Callback(this.OnSubmit));
  }

  protected virtual void OnSubmit()
  {
    if (UICamera.currentKey != this.keyCode || !this.IsModifierActive())
      return;
    this.mIgnoreUp = true;
  }

  protected virtual bool IsModifierActive()
  {
    if (this.modifier == UIKeyBinding.Modifier.Any)
      return true;
    if (this.modifier == UIKeyBinding.Modifier.Alt)
    {
      if (UICamera.GetKey((KeyCode) 308) || UICamera.GetKey((KeyCode) 307))
        return true;
    }
    else if (this.modifier == UIKeyBinding.Modifier.Control)
    {
      if (UICamera.GetKey((KeyCode) 306) || UICamera.GetKey((KeyCode) 305))
        return true;
    }
    else if (this.modifier == UIKeyBinding.Modifier.Shift)
    {
      if (UICamera.GetKey((KeyCode) 304) || UICamera.GetKey((KeyCode) 303))
        return true;
    }
    else if (this.modifier == UIKeyBinding.Modifier.None && !UICamera.GetKey((KeyCode) 308) && !UICamera.GetKey((KeyCode) 307) && !UICamera.GetKey((KeyCode) 306) && !UICamera.GetKey((KeyCode) 305) && !UICamera.GetKey((KeyCode) 304))
      return !UICamera.GetKey((KeyCode) 303);
    return false;
  }

  protected virtual void Update()
  {
    if (UICamera.inputHasFocus || this.keyCode == null || !this.IsModifierActive())
      return;
    bool flag1 = UICamera.GetKeyDown(this.keyCode);
    bool flag2 = UICamera.GetKeyUp(this.keyCode);
    if (flag1)
      this.mPress = true;
    if (this.action == UIKeyBinding.Action.PressAndClick || this.action == UIKeyBinding.Action.All)
    {
      if (flag1)
      {
        UICamera.currentKey = this.keyCode;
        this.OnBindingPress(true);
      }
      if (this.mPress & flag2)
      {
        UICamera.currentKey = this.keyCode;
        this.OnBindingPress(false);
        this.OnBindingClick();
      }
    }
    if ((this.action == UIKeyBinding.Action.Select || this.action == UIKeyBinding.Action.All) && flag2)
    {
      if (this.mIsInput)
      {
        if (!this.mIgnoreUp && !UICamera.inputHasFocus && this.mPress)
          UICamera.selectedObject = ((Component) this).gameObject;
        this.mIgnoreUp = false;
      }
      else if (this.mPress)
        UICamera.hoveredObject = ((Component) this).gameObject;
    }
    if (!flag2)
      return;
    this.mPress = false;
  }

  protected virtual void OnBindingPress(bool pressed)
  {
    UICamera.Notify(((Component) this).gameObject, "OnPress", (object) pressed);
  }

  protected virtual void OnBindingClick()
  {
    UICamera.Notify(((Component) this).gameObject, "OnClick", (object) null);
  }

  public enum Action
  {
    PressAndClick,
    Select,
    All,
  }

  public enum Modifier
  {
    Any,
    Shift,
    Control,
    Alt,
    None,
  }
}
