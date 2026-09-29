// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.GetCaptureCapabilitiesResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class GetCaptureCapabilitiesResponse : BaseReferenceHolder
{
  internal GetCaptureCapabilitiesResponse(IntPtr selfPointer)
    : base(selfPointer)
  {
  }

  protected override void CallDispose(HandleRef selfPointer)
  {
    GooglePlayGames.Native.Cwrapper.VideoManager.VideoManager_GetCaptureCapabilitiesResponse_Dispose(this.SelfPtr());
  }

  internal CommonErrorStatus.ResponseStatus GetStatus()
  {
    return GooglePlayGames.Native.Cwrapper.VideoManager.VideoManager_GetCaptureCapabilitiesResponse_GetStatus(this.SelfPtr());
  }

  internal bool RequestSucceeded()
  {
    return this.GetStatus() > ~CommonErrorStatus.ResponseStatus.ERROR_LICENSE_CHECK_FAILED;
  }

  internal NativeVideoCapabilities GetData()
  {
    return NativeVideoCapabilities.FromPointer(GooglePlayGames.Native.Cwrapper.VideoManager.VideoManager_GetCaptureCapabilitiesResponse_GetVideocapabilities(this.SelfPtr()));
  }

  internal static GetCaptureCapabilitiesResponse FromPointer(IntPtr pointer)
  {
    return pointer.Equals((object) IntPtr.Zero) ? (GetCaptureCapabilitiesResponse) null : new GetCaptureCapabilitiesResponse(pointer);
  }
}
