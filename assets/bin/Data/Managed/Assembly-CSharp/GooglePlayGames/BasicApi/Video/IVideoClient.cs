// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Video.IVideoClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace GooglePlayGames.BasicApi.Video;

public interface IVideoClient
{
  void GetCaptureCapabilities(Action<ResponseStatus, VideoCapabilities> callback);

  void ShowCaptureOverlay();

  void GetCaptureState(Action<ResponseStatus, VideoCaptureState> callback);

  void IsCaptureAvailable(VideoCaptureMode captureMode, Action<ResponseStatus, bool> callback);

  bool IsCaptureSupported();

  void RegisterCaptureOverlayStateChangedListener(CaptureOverlayStateListener listener);

  void UnregisterCaptureOverlayStateChangedListener();
}
