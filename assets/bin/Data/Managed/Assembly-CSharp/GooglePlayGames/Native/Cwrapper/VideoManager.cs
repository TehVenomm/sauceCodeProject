// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.VideoManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.Cwrapper;

internal static class VideoManager
{
  [DllImport("gpg")]
  internal static extern void VideoManager_GetCaptureCapabilities(
    HandleRef self,
    VideoManager.CaptureCapabilitiesCallback callback,
    IntPtr callback_arg);

  [DllImport("gpg")]
  internal static extern void VideoManager_ShowCaptureOverlay(HandleRef self);

  [DllImport("gpg")]
  internal static extern void VideoManager_GetCaptureState(
    HandleRef self,
    VideoManager.CaptureStateCallback callback,
    IntPtr callback_arg);

  [DllImport("gpg")]
  internal static extern void VideoManager_IsCaptureAvailable(
    HandleRef self,
    Types.VideoCaptureMode capture_mode,
    VideoManager.IsCaptureAvailableCallback callback,
    IntPtr callback_arg);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool VideoManager_IsCaptureSupported(HandleRef self);

  [DllImport("gpg")]
  internal static extern void VideoManager_RegisterCaptureOverlayStateChangedListener(
    HandleRef self,
    IntPtr helper);

  [DllImport("gpg")]
  internal static extern void VideoManager_UnregisterCaptureOverlayStateChangedListener(
    HandleRef self);

  [DllImport("gpg")]
  internal static extern void VideoManager_GetCaptureCapabilitiesResponse_Dispose(HandleRef self);

  [DllImport("gpg")]
  internal static extern CommonErrorStatus.ResponseStatus VideoManager_GetCaptureCapabilitiesResponse_GetStatus(
    HandleRef self);

  [DllImport("gpg")]
  internal static extern IntPtr VideoManager_GetCaptureCapabilitiesResponse_GetVideocapabilities(
    HandleRef self);

  [DllImport("gpg")]
  internal static extern void VideoManager_GetCaptureStateResponse_Dispose(HandleRef self);

  [DllImport("gpg")]
  internal static extern CommonErrorStatus.ResponseStatus VideoManager_GetCaptureStateResponse_GetStatus(
    HandleRef self);

  [DllImport("gpg")]
  internal static extern IntPtr VideoManager_GetCaptureStateResponse_GetVideocapturestate(
    HandleRef self);

  [DllImport("gpg")]
  internal static extern void VideoManager_IsCaptureAvailableResponse_Dispose(HandleRef self);

  [DllImport("gpg")]
  internal static extern CommonErrorStatus.ResponseStatus VideoManager_IsCaptureAvailableResponse_GetStatus(
    HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool VideoManager_IsCaptureAvailableResponse_GetIscaptureavailable(
    HandleRef self);

  internal delegate void CaptureCapabilitiesCallback(IntPtr arg0, IntPtr arg1);

  internal delegate void CaptureStateCallback(IntPtr arg0, IntPtr arg1);

  internal delegate void IsCaptureAvailableCallback(IntPtr arg0, IntPtr arg1);
}
