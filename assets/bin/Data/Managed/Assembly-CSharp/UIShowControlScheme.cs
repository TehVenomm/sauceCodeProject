// Decompiled with JetBrains decompiler
// Type: UIShowControlScheme
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIShowControlScheme : MonoBehaviour
{
  public GameObject target;
  public bool mouse;
  public bool touch;
  public bool controller = true;

  private void OnEnable()
  {
    UICamera.onSchemeChange += new UICamera.OnSchemeChange(this.OnScheme);
    this.OnScheme();
  }

  private void OnDisable() => UICamera.onSchemeChange -= new UICamera.OnSchemeChange(this.OnScheme);

  private void OnScheme()
  {
    if (!Object.op_Inequality((Object) this.target, (Object) null))
      return;
    switch (UICamera.currentScheme)
    {
      case UICamera.ControlScheme.Mouse:
        this.target.SetActive(this.mouse);
        break;
      case UICamera.ControlScheme.Touch:
        this.target.SetActive(this.touch);
        break;
      case UICamera.ControlScheme.Controller:
        this.target.SetActive(this.controller);
        break;
    }
  }
}
