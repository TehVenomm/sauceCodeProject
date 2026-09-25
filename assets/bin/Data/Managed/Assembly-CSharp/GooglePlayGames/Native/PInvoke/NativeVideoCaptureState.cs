// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeVideoCaptureState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class NativeVideoCaptureState : BaseReferenceHolder
{
  internal NativeVideoCaptureState(IntPtr selfPtr)
    : base(selfPtr)
  {
  }

  protected override void CallDispose(HandleRef selfPointer)
  {
    VideoCaptureState.VideoCaptureState_Dispose(selfPointer);
  }

  internal bool IsCapturing() => VideoCaptureState.VideoCaptureState_IsCapturing(this.SelfPtr());

  internal Types.VideoCaptureMode CaptureMode()
  {
    return VideoCaptureState.VideoCaptureState_CaptureMode(this.SelfPtr());
  }

  internal Types.VideoQualityLevel QualityLevel()
  {
    return VideoCaptureState.VideoCaptureState_QualityLevel(this.SelfPtr());
  }

  internal bool IsOverlayVisible()
  {
    return VideoCaptureState.VideoCaptureState_IsOverlayVisible(this.SelfPtr());
  }

  internal bool IsPaused() => VideoCaptureState.VideoCaptureState_IsPaused(this.SelfPtr());

  internal static NativeVideoCaptureState FromPointer(IntPtr pointer)
  {
    return pointer.Equals((object) IntPtr.Zero) ? (NativeVideoCaptureState) null : new NativeVideoCaptureState(pointer);
  }
}
