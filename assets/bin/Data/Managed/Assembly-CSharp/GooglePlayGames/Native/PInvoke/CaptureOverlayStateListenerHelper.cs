// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.CaptureOverlayStateListenerHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class CaptureOverlayStateListenerHelper : BaseReferenceHolder
{
  internal CaptureOverlayStateListenerHelper(IntPtr selfPointer)
    : base(selfPointer)
  {
  }

  protected override void CallDispose(HandleRef selfPointer)
  {
    GooglePlayGames.Native.Cwrapper.CaptureOverlayStateListenerHelper.CaptureOverlayStateListenerHelper_Dispose(selfPointer);
  }

  internal CaptureOverlayStateListenerHelper SetOnCaptureOverlayStateChangedCallback(
    Action<Types.VideoCaptureOverlayState> callback)
  {
    GooglePlayGames.Native.Cwrapper.CaptureOverlayStateListenerHelper.CaptureOverlayStateListenerHelper_SetOnCaptureOverlayStateChangedCallback(this.SelfPtr(), new GooglePlayGames.Native.Cwrapper.CaptureOverlayStateListenerHelper.OnCaptureOverlayStateChangedCallback(CaptureOverlayStateListenerHelper.InternalOnCaptureOverlayStateChangedCallback), Callbacks.ToIntPtr((Delegate) callback));
    return this;
  }

  [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.CaptureOverlayStateListenerHelper.OnCaptureOverlayStateChangedCallback))]
  internal static void InternalOnCaptureOverlayStateChangedCallback(
    Types.VideoCaptureOverlayState response,
    IntPtr data)
  {
    Action<Types.VideoCaptureOverlayState> permanentCallback = Callbacks.IntPtrToPermanentCallback<Action<Types.VideoCaptureOverlayState>>(data);
    try
    {
      permanentCallback(response);
    }
    catch (Exception ex)
    {
      Logger.e("Error encountered executing InternalOnCaptureOverlayStateChangedCallback. Smothering to avoid passing exception into Native: " + (object) ex);
    }
  }

  internal static CaptureOverlayStateListenerHelper Create()
  {
    return new CaptureOverlayStateListenerHelper(GooglePlayGames.Native.Cwrapper.CaptureOverlayStateListenerHelper.CaptureOverlayStateListenerHelper_Construct());
  }
}
