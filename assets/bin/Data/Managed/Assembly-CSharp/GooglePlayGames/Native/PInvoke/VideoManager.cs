// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.VideoManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class VideoManager
{
  private readonly GameServices mServices;

  internal VideoManager(GameServices services)
  {
    this.mServices = Misc.CheckNotNull<GameServices>(services);
  }

  internal int NumCaptureModes => 2;

  internal int NumQualityLevels => 4;

  internal void GetCaptureCapabilities(Action<GetCaptureCapabilitiesResponse> callback)
  {
    GooglePlayGames.Native.Cwrapper.VideoManager.VideoManager_GetCaptureCapabilities(this.mServices.AsHandle(), new GooglePlayGames.Native.Cwrapper.VideoManager.CaptureCapabilitiesCallback(VideoManager.InternalCaptureCapabilitiesCallback), Callbacks.ToIntPtr<GetCaptureCapabilitiesResponse>(callback, new Func<IntPtr, GetCaptureCapabilitiesResponse>(GetCaptureCapabilitiesResponse.FromPointer)));
  }

  [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.VideoManager.CaptureCapabilitiesCallback))]
  internal static void InternalCaptureCapabilitiesCallback(IntPtr response, IntPtr data)
  {
    Callbacks.PerformInternalCallback("VideoManager#CaptureCapabilitiesCallback", Callbacks.Type.Temporary, response, data);
  }

  internal void ShowCaptureOverlay()
  {
    GooglePlayGames.Native.Cwrapper.VideoManager.VideoManager_ShowCaptureOverlay(this.mServices.AsHandle());
  }

  internal void GetCaptureState(Action<GetCaptureStateResponse> callback)
  {
    GooglePlayGames.Native.Cwrapper.VideoManager.VideoManager_GetCaptureState(this.mServices.AsHandle(), new GooglePlayGames.Native.Cwrapper.VideoManager.CaptureStateCallback(VideoManager.InternalCaptureStateCallback), Callbacks.ToIntPtr<GetCaptureStateResponse>(callback, new Func<IntPtr, GetCaptureStateResponse>(GetCaptureStateResponse.FromPointer)));
  }

  [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.VideoManager.CaptureStateCallback))]
  internal static void InternalCaptureStateCallback(IntPtr response, IntPtr data)
  {
    Callbacks.PerformInternalCallback("VideoManager#CaptureStateCallback", Callbacks.Type.Temporary, response, data);
  }

  internal void IsCaptureAvailable(
    Types.VideoCaptureMode captureMode,
    Action<IsCaptureAvailableResponse> callback)
  {
    GooglePlayGames.Native.Cwrapper.VideoManager.VideoManager_IsCaptureAvailable(this.mServices.AsHandle(), captureMode, new GooglePlayGames.Native.Cwrapper.VideoManager.IsCaptureAvailableCallback(VideoManager.InternalIsCaptureAvailableCallback), Callbacks.ToIntPtr<IsCaptureAvailableResponse>(callback, new Func<IntPtr, IsCaptureAvailableResponse>(IsCaptureAvailableResponse.FromPointer)));
  }

  [MonoPInvokeCallback(typeof (GooglePlayGames.Native.Cwrapper.VideoManager.IsCaptureAvailableCallback))]
  internal static void InternalIsCaptureAvailableCallback(IntPtr response, IntPtr data)
  {
    Callbacks.PerformInternalCallback("VideoManager#IsCaptureAvailableCallback", Callbacks.Type.Temporary, response, data);
  }

  internal bool IsCaptureSupported()
  {
    return GooglePlayGames.Native.Cwrapper.VideoManager.VideoManager_IsCaptureSupported(this.mServices.AsHandle());
  }

  internal void RegisterCaptureOverlayStateChangedListener(CaptureOverlayStateListenerHelper helper)
  {
    GooglePlayGames.Native.Cwrapper.VideoManager.VideoManager_RegisterCaptureOverlayStateChangedListener(this.mServices.AsHandle(), helper.AsPointer());
  }

  internal void UnregisterCaptureOverlayStateChangedListener()
  {
    GooglePlayGames.Native.Cwrapper.VideoManager.VideoManager_UnregisterCaptureOverlayStateChangedListener(this.mServices.AsHandle());
  }
}
