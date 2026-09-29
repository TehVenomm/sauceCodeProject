// Decompiled with JetBrains decompiler
// Type: UIToggleButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class UIToggleButton : MonoBehaviour
{
  public UIButton activeButton;
  public UIButton inactiveButton;
  public bool isActive;
  public Action<bool> onChanged;

  public void Initialize()
  {
    if (Object.op_Equality((Object) this.activeButton, (Object) null) || Object.op_Equality((Object) this.inactiveButton, (Object) null))
      return;
    EventDelegate eventDelegate = new EventDelegate(new EventDelegate.Callback(this.OnChange));
    ((Component) this.activeButton).gameObject.SetActive(this.isActive);
    this.activeButton.onClick.Clear();
    this.activeButton.onClick.Add(eventDelegate);
    ((Component) this.inactiveButton).gameObject.SetActive(!this.isActive);
    this.inactiveButton.onClick.Clear();
    this.inactiveButton.onClick.Add(eventDelegate);
  }

  public void Change()
  {
    this.isActive = !this.isActive;
    if (Object.op_Inequality((Object) this.activeButton, (Object) null))
      ((Component) this.activeButton).gameObject.SetActive(this.isActive);
    if (!Object.op_Inequality((Object) this.inactiveButton, (Object) null))
      return;
    ((Component) this.inactiveButton).gameObject.SetActive(!this.isActive);
  }

  public void OnChange()
  {
    this.Change();
    if (this.onChanged != null)
      this.onChanged(this.isActive);
    SoundManager.PlaySystemSE(SoundID.UISE.CLICK);
  }
}
