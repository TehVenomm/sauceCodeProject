// Decompiled with JetBrains decompiler
// Type: AndroidPermissionCallback
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class AndroidPermissionCallback : AndroidJavaProxy
{
  private event Action<string> OnPermissionGrantedAction;

  private event Action<string> OnPermissionDeniedAction;

  public AndroidPermissionCallback(
    Action<string> onGrantedCallback,
    Action<string> onDeniedCallback)
    : base("com.unity3d.plugin.UnityAndroidPermissions$IPermissionRequestResult")
  {
    if (onGrantedCallback != null)
      this.OnPermissionGrantedAction += onGrantedCallback;
    if (onDeniedCallback == null)
      return;
    this.OnPermissionDeniedAction += onDeniedCallback;
  }

  public virtual void OnPermissionGranted(string permissionName)
  {
    if (this.OnPermissionGrantedAction == null)
      return;
    this.OnPermissionGrantedAction(permissionName);
  }

  public virtual void OnPermissionDenied(string permissionName)
  {
    if (this.OnPermissionDeniedAction == null)
      return;
    this.OnPermissionDeniedAction(permissionName);
  }
}
