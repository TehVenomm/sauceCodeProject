// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeVideoCapabilities
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class NativeVideoCapabilities : BaseReferenceHolder
{
  internal NativeVideoCapabilities(IntPtr selfPtr)
    : base(selfPtr)
  {
  }

  protected override void CallDispose(HandleRef selfPointer)
  {
    VideoCapabilities.VideoCapabilities_Dispose(selfPointer);
  }

  internal bool IsCameraSupported()
  {
    return VideoCapabilities.VideoCapabilities_IsCameraSupported(this.SelfPtr());
  }

  internal bool IsMicSupported()
  {
    return VideoCapabilities.VideoCapabilities_IsMicSupported(this.SelfPtr());
  }

  internal bool IsWriteStorageSupported()
  {
    return VideoCapabilities.VideoCapabilities_IsWriteStorageSupported(this.SelfPtr());
  }

  internal bool SupportsCaptureMode(Types.VideoCaptureMode captureMode)
  {
    return VideoCapabilities.VideoCapabilities_SupportsCaptureMode(this.SelfPtr(), captureMode);
  }

  internal bool SupportsQualityLevel(Types.VideoQualityLevel qualityLevel)
  {
    return VideoCapabilities.VideoCapabilities_SupportsQualityLevel(this.SelfPtr(), qualityLevel);
  }

  internal static NativeVideoCapabilities FromPointer(IntPtr pointer)
  {
    return pointer.Equals((object) IntPtr.Zero) ? (NativeVideoCapabilities) null : new NativeVideoCapabilities(pointer);
  }
}
