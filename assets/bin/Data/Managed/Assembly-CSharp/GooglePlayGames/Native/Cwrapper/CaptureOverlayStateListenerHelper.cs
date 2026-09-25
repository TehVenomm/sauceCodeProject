// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.CaptureOverlayStateListenerHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.Cwrapper;

internal static class CaptureOverlayStateListenerHelper
{
  [DllImport("gpg")]
  internal static extern void CaptureOverlayStateListenerHelper_SetOnCaptureOverlayStateChangedCallback(
    HandleRef self,
    CaptureOverlayStateListenerHelper.OnCaptureOverlayStateChangedCallback callback,
    IntPtr callback_arg);

  [DllImport("gpg")]
  internal static extern IntPtr CaptureOverlayStateListenerHelper_Construct();

  [DllImport("gpg")]
  internal static extern void CaptureOverlayStateListenerHelper_Dispose(HandleRef self);

  internal delegate void OnCaptureOverlayStateChangedCallback(
    Types.VideoCaptureOverlayState arg0,
    IntPtr arg1);
}
