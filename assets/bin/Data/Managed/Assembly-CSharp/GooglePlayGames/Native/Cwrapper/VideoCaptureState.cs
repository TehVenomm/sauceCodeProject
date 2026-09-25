// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.VideoCaptureState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.Cwrapper;

internal static class VideoCaptureState
{
  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool VideoCaptureState_IsCapturing(HandleRef self);

  [DllImport("gpg")]
  internal static extern Types.VideoCaptureMode VideoCaptureState_CaptureMode(HandleRef self);

  [DllImport("gpg")]
  internal static extern Types.VideoQualityLevel VideoCaptureState_QualityLevel(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool VideoCaptureState_IsOverlayVisible(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool VideoCaptureState_IsPaused(HandleRef self);

  [DllImport("gpg")]
  internal static extern void VideoCaptureState_Dispose(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool VideoCaptureState_Valid(HandleRef self);
}
