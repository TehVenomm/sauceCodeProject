// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.VideoCapabilities
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.Cwrapper;

internal static class VideoCapabilities
{
  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool VideoCapabilities_IsCameraSupported(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool VideoCapabilities_IsMicSupported(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool VideoCapabilities_IsWriteStorageSupported(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool VideoCapabilities_SupportsCaptureMode(
    HandleRef self,
    Types.VideoCaptureMode capture_mode);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool VideoCapabilities_SupportsQualityLevel(
    HandleRef self,
    Types.VideoQualityLevel quality_level);

  [DllImport("gpg")]
  internal static extern void VideoCapabilities_Dispose(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool VideoCapabilities_Valid(HandleRef self);
}
