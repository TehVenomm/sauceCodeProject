// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.NativeVideoClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.BasicApi.Video;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;
using System;

#nullable disable
namespace GooglePlayGames.Native;

internal class NativeVideoClient : IVideoClient
{
  private readonly GooglePlayGames.Native.PInvoke.VideoManager mManager;

  internal NativeVideoClient(GooglePlayGames.Native.PInvoke.VideoManager manager)
  {
    this.mManager = Misc.CheckNotNull<GooglePlayGames.Native.PInvoke.VideoManager>(manager);
  }

  public void GetCaptureCapabilities(Action<GooglePlayGames.BasicApi.ResponseStatus, GooglePlayGames.BasicApi.Video.VideoCapabilities> callback)
  {
    Misc.CheckNotNull<Action<GooglePlayGames.BasicApi.ResponseStatus, GooglePlayGames.BasicApi.Video.VideoCapabilities>>(callback);
    callback = CallbackUtils.ToOnGameThread<GooglePlayGames.BasicApi.ResponseStatus, GooglePlayGames.BasicApi.Video.VideoCapabilities>(callback);
    this.mManager.GetCaptureCapabilities((Action<GetCaptureCapabilitiesResponse>) (response =>
    {
      GooglePlayGames.BasicApi.ResponseStatus responseStatus = ConversionUtils.ConvertResponseStatus(response.GetStatus());
      if (!response.RequestSucceeded())
        callback(responseStatus, (GooglePlayGames.BasicApi.Video.VideoCapabilities) null);
      else
        callback(responseStatus, this.FromNativeVideoCapabilities(response.GetData()));
    }));
  }

  private GooglePlayGames.BasicApi.Video.VideoCapabilities FromNativeVideoCapabilities(
    NativeVideoCapabilities capabilities)
  {
    bool[] captureModesSupported = new bool[this.mManager.NumCaptureModes];
    captureModesSupported[0] = capabilities.SupportsCaptureMode(Types.VideoCaptureMode.FILE);
    captureModesSupported[1] = capabilities.SupportsCaptureMode(Types.VideoCaptureMode.STREAM);
    bool[] qualityLevelsSupported = new bool[this.mManager.NumQualityLevels];
    qualityLevelsSupported[0] = capabilities.SupportsQualityLevel(Types.VideoQualityLevel.SD);
    qualityLevelsSupported[1] = capabilities.SupportsQualityLevel(Types.VideoQualityLevel.HD);
    qualityLevelsSupported[2] = capabilities.SupportsQualityLevel(Types.VideoQualityLevel.XHD);
    qualityLevelsSupported[3] = capabilities.SupportsQualityLevel(Types.VideoQualityLevel.FULLHD);
    return new GooglePlayGames.BasicApi.Video.VideoCapabilities(capabilities.IsCameraSupported(), capabilities.IsMicSupported(), capabilities.IsWriteStorageSupported(), captureModesSupported, qualityLevelsSupported);
  }

  public void ShowCaptureOverlay() => this.mManager.ShowCaptureOverlay();

  public void GetCaptureState(Action<GooglePlayGames.BasicApi.ResponseStatus, GooglePlayGames.BasicApi.Video.VideoCaptureState> callback)
  {
    Misc.CheckNotNull<Action<GooglePlayGames.BasicApi.ResponseStatus, GooglePlayGames.BasicApi.Video.VideoCaptureState>>(callback);
    callback = CallbackUtils.ToOnGameThread<GooglePlayGames.BasicApi.ResponseStatus, GooglePlayGames.BasicApi.Video.VideoCaptureState>(callback);
    this.mManager.GetCaptureState((Action<GetCaptureStateResponse>) (response =>
    {
      GooglePlayGames.BasicApi.ResponseStatus responseStatus = ConversionUtils.ConvertResponseStatus(response.GetStatus());
      if (!response.RequestSucceeded())
        callback(responseStatus, (GooglePlayGames.BasicApi.Video.VideoCaptureState) null);
      else
        callback(responseStatus, this.FromNativeVideoCaptureState(response.GetData()));
    }));
  }

  private GooglePlayGames.BasicApi.Video.VideoCaptureState FromNativeVideoCaptureState(
    NativeVideoCaptureState captureState)
  {
    return new GooglePlayGames.BasicApi.Video.VideoCaptureState(captureState.IsCapturing(), ConversionUtils.ConvertNativeVideoCaptureMode(captureState.CaptureMode()), ConversionUtils.ConvertNativeVideoQualityLevel(captureState.QualityLevel()), captureState.IsOverlayVisible(), captureState.IsPaused());
  }

  public void IsCaptureAvailable(
    GooglePlayGames.BasicApi.VideoCaptureMode captureMode,
    Action<GooglePlayGames.BasicApi.ResponseStatus, bool> callback)
  {
    Misc.CheckNotNull<Action<GooglePlayGames.BasicApi.ResponseStatus, bool>>(callback);
    callback = CallbackUtils.ToOnGameThread<GooglePlayGames.BasicApi.ResponseStatus, bool>(callback);
    this.mManager.IsCaptureAvailable(ConversionUtils.ConvertVideoCaptureMode(captureMode), (Action<IsCaptureAvailableResponse>) (response =>
    {
      GooglePlayGames.BasicApi.ResponseStatus responseStatus = ConversionUtils.ConvertResponseStatus(response.GetStatus());
      if (!response.RequestSucceeded())
        callback(responseStatus, false);
      else
        callback(responseStatus, response.IsCaptureAvailable());
    }));
  }

  public bool IsCaptureSupported() => this.mManager.IsCaptureSupported();

  public void RegisterCaptureOverlayStateChangedListener(CaptureOverlayStateListener listener)
  {
    Misc.CheckNotNull<CaptureOverlayStateListener>(listener);
    this.mManager.RegisterCaptureOverlayStateChangedListener(GooglePlayGames.Native.PInvoke.CaptureOverlayStateListenerHelper.Create().SetOnCaptureOverlayStateChangedCallback((Action<Types.VideoCaptureOverlayState>) (response => listener.OnCaptureOverlayStateChanged(ConversionUtils.ConvertNativeVideoCaptureOverlayState(response)))));
  }

  public void UnregisterCaptureOverlayStateChangedListener()
  {
    this.mManager.UnregisterCaptureOverlayStateChangedListener();
  }
}
