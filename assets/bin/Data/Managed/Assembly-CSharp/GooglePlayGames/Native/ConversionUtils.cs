// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.ConversionUtils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.BasicApi;
using GooglePlayGames.Native.Cwrapper;
using System;
using UnityEngine;

#nullable disable
namespace GooglePlayGames.Native;

internal static class ConversionUtils
{
  internal static GooglePlayGames.BasicApi.ResponseStatus ConvertResponseStatus(
    CommonErrorStatus.ResponseStatus status)
  {
    switch (status)
    {
      case CommonErrorStatus.ResponseStatus.ERROR_TIMEOUT:
        return GooglePlayGames.BasicApi.ResponseStatus.Timeout;
      case CommonErrorStatus.ResponseStatus.ERROR_VERSION_UPDATE_REQUIRED:
        return GooglePlayGames.BasicApi.ResponseStatus.VersionUpdateRequired;
      case CommonErrorStatus.ResponseStatus.ERROR_NOT_AUTHORIZED:
        return GooglePlayGames.BasicApi.ResponseStatus.NotAuthorized;
      case CommonErrorStatus.ResponseStatus.ERROR_INTERNAL:
        return GooglePlayGames.BasicApi.ResponseStatus.InternalError;
      case CommonErrorStatus.ResponseStatus.ERROR_LICENSE_CHECK_FAILED:
        return GooglePlayGames.BasicApi.ResponseStatus.LicenseCheckFailed;
      case CommonErrorStatus.ResponseStatus.VALID:
        return GooglePlayGames.BasicApi.ResponseStatus.Success;
      case CommonErrorStatus.ResponseStatus.VALID_BUT_STALE:
        return GooglePlayGames.BasicApi.ResponseStatus.SuccessWithStale;
      default:
        throw new InvalidOperationException("Unknown status: " + (object) status);
    }
  }

  internal static CommonStatusCodes ConvertResponseStatusToCommonStatus(
    CommonErrorStatus.ResponseStatus status)
  {
    switch (status)
    {
      case CommonErrorStatus.ResponseStatus.ERROR_TIMEOUT:
        return CommonStatusCodes.Timeout;
      case CommonErrorStatus.ResponseStatus.ERROR_VERSION_UPDATE_REQUIRED:
        return CommonStatusCodes.ServiceVersionUpdateRequired;
      case CommonErrorStatus.ResponseStatus.ERROR_NOT_AUTHORIZED:
        return CommonStatusCodes.AuthApiAccessForbidden;
      case CommonErrorStatus.ResponseStatus.ERROR_INTERNAL:
        return CommonStatusCodes.InternalError;
      case CommonErrorStatus.ResponseStatus.ERROR_LICENSE_CHECK_FAILED:
        return CommonStatusCodes.LicenseCheckFailed;
      case CommonErrorStatus.ResponseStatus.VALID:
        return CommonStatusCodes.Success;
      case CommonErrorStatus.ResponseStatus.VALID_BUT_STALE:
        return CommonStatusCodes.SuccessCached;
      default:
        Debug.LogWarning((object) $"Unknown ResponseStatus: {(object) status}, defaulting to CommonStatusCodes.Error");
        return CommonStatusCodes.Error;
    }
  }

  internal static GooglePlayGames.BasicApi.UIStatus ConvertUIStatus(
    CommonErrorStatus.UIStatus status)
  {
    switch (status)
    {
      case CommonErrorStatus.UIStatus.ERROR_UI_BUSY:
        return GooglePlayGames.BasicApi.UIStatus.UiBusy;
      case CommonErrorStatus.UIStatus.ERROR_CANCELED:
        return GooglePlayGames.BasicApi.UIStatus.UserClosedUI;
      case CommonErrorStatus.UIStatus.ERROR_TIMEOUT:
        return GooglePlayGames.BasicApi.UIStatus.Timeout;
      case CommonErrorStatus.UIStatus.ERROR_VERSION_UPDATE_REQUIRED:
        return GooglePlayGames.BasicApi.UIStatus.VersionUpdateRequired;
      case CommonErrorStatus.UIStatus.ERROR_NOT_AUTHORIZED:
        return GooglePlayGames.BasicApi.UIStatus.NotAuthorized;
      case CommonErrorStatus.UIStatus.ERROR_INTERNAL:
        return GooglePlayGames.BasicApi.UIStatus.InternalError;
      case CommonErrorStatus.UIStatus.VALID:
        return GooglePlayGames.BasicApi.UIStatus.Valid;
      default:
        throw new InvalidOperationException("Unknown status: " + (object) status);
    }
  }

  internal static Types.DataSource AsDataSource(GooglePlayGames.BasicApi.DataSource source)
  {
    if (source == GooglePlayGames.BasicApi.DataSource.ReadCacheOrNetwork)
      return Types.DataSource.CACHE_OR_NETWORK;
    if (source == GooglePlayGames.BasicApi.DataSource.ReadNetworkOnly)
      return Types.DataSource.NETWORK_ONLY;
    throw new InvalidOperationException("Found unhandled DataSource: " + (object) source);
  }

  internal static Types.VideoCaptureMode ConvertVideoCaptureMode(GooglePlayGames.BasicApi.VideoCaptureMode captureMode)
  {
    switch (captureMode)
    {
      case GooglePlayGames.BasicApi.VideoCaptureMode.Unknown:
        return Types.VideoCaptureMode.UNKNOWN;
      case GooglePlayGames.BasicApi.VideoCaptureMode.File:
        return Types.VideoCaptureMode.FILE;
      case GooglePlayGames.BasicApi.VideoCaptureMode.Stream:
        return Types.VideoCaptureMode.STREAM;
      default:
        Debug.LogWarning((object) $"Unknown VideoCaptureMode: {(object) captureMode}, defaulting to Types.VideoCaptureMode.UNKNOWN.");
        return Types.VideoCaptureMode.UNKNOWN;
    }
  }

  internal static GooglePlayGames.BasicApi.VideoCaptureMode ConvertNativeVideoCaptureMode(
    Types.VideoCaptureMode nativeCaptureMode)
  {
    switch (nativeCaptureMode)
    {
      case Types.VideoCaptureMode.UNKNOWN:
        return GooglePlayGames.BasicApi.VideoCaptureMode.Unknown;
      case Types.VideoCaptureMode.FILE:
        return GooglePlayGames.BasicApi.VideoCaptureMode.File;
      case Types.VideoCaptureMode.STREAM:
        return GooglePlayGames.BasicApi.VideoCaptureMode.Stream;
      default:
        Debug.LogWarning((object) $"Unknown Types.VideoCaptureMode: {(object) nativeCaptureMode}, defaulting to VideoCaptureMode.Unknown.");
        return GooglePlayGames.BasicApi.VideoCaptureMode.Unknown;
    }
  }

  internal static GooglePlayGames.BasicApi.VideoQualityLevel ConvertNativeVideoQualityLevel(
    Types.VideoQualityLevel nativeQualityLevel)
  {
    switch (nativeQualityLevel)
    {
      case Types.VideoQualityLevel.UNKNOWN:
        return GooglePlayGames.BasicApi.VideoQualityLevel.Unknown;
      case Types.VideoQualityLevel.SD:
        return GooglePlayGames.BasicApi.VideoQualityLevel.SD;
      case Types.VideoQualityLevel.HD:
        return GooglePlayGames.BasicApi.VideoQualityLevel.HD;
      case Types.VideoQualityLevel.XHD:
        return GooglePlayGames.BasicApi.VideoQualityLevel.XHD;
      case Types.VideoQualityLevel.FULLHD:
        return GooglePlayGames.BasicApi.VideoQualityLevel.FullHD;
      default:
        Debug.LogWarning((object) $"Unknown Types.VideoQualityLevel: {(object) nativeQualityLevel}, defaulting to VideoQualityLevel.Unknown.");
        return GooglePlayGames.BasicApi.VideoQualityLevel.Unknown;
    }
  }

  internal static GooglePlayGames.BasicApi.VideoCaptureOverlayState ConvertNativeVideoCaptureOverlayState(
    Types.VideoCaptureOverlayState nativeOverlayState)
  {
    switch (nativeOverlayState)
    {
      case Types.VideoCaptureOverlayState.UNKNOWN:
        return GooglePlayGames.BasicApi.VideoCaptureOverlayState.Unknown;
      case Types.VideoCaptureOverlayState.SHOWN:
        return GooglePlayGames.BasicApi.VideoCaptureOverlayState.Shown;
      case Types.VideoCaptureOverlayState.STARTED:
        return GooglePlayGames.BasicApi.VideoCaptureOverlayState.Started;
      case Types.VideoCaptureOverlayState.STOPPED:
        return GooglePlayGames.BasicApi.VideoCaptureOverlayState.Stopped;
      case Types.VideoCaptureOverlayState.DISMISSED:
        return GooglePlayGames.BasicApi.VideoCaptureOverlayState.Dismissed;
      default:
        Debug.LogWarning((object) $"Unknown Types.VideoCaptureOverlayState: {(object) nativeOverlayState}, defaulting to VideoCaptureOverlayState.Unknown.");
        return GooglePlayGames.BasicApi.VideoCaptureOverlayState.Unknown;
    }
  }
}
