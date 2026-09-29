// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Video.VideoCapabilities
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace GooglePlayGames.BasicApi.Video;

public class VideoCapabilities
{
  private bool mIsCameraSupported;
  private bool mIsMicSupported;
  private bool mIsWriteStorageSupported;
  private bool[] mCaptureModesSupported;
  private bool[] mQualityLevelsSupported;

  internal VideoCapabilities(
    bool isCameraSupported,
    bool isMicSupported,
    bool isWriteStorageSupported,
    bool[] captureModesSupported,
    bool[] qualityLevelsSupported)
  {
    this.mIsCameraSupported = isCameraSupported;
    this.mIsMicSupported = isMicSupported;
    this.mIsWriteStorageSupported = isWriteStorageSupported;
    this.mCaptureModesSupported = captureModesSupported;
    this.mQualityLevelsSupported = qualityLevelsSupported;
  }

  public bool IsCameraSupported => this.mIsCameraSupported;

  public bool IsMicSupported => this.mIsMicSupported;

  public bool IsWriteStorageSupported => this.mIsWriteStorageSupported;

  public bool SupportsCaptureMode(VideoCaptureMode captureMode)
  {
    if (captureMode != VideoCaptureMode.Unknown)
      return this.mCaptureModesSupported[(int) captureMode];
    Logger.w("SupportsCaptureMode called with an unknown captureMode.");
    return false;
  }

  public bool SupportsQualityLevel(VideoQualityLevel qualityLevel)
  {
    if (qualityLevel != VideoQualityLevel.Unknown)
      return this.mQualityLevelsSupported[(int) qualityLevel];
    Logger.w("SupportsCaptureMode called with an unknown qualityLevel.");
    return false;
  }

  public override string ToString()
  {
    return $"[VideoCapabilities: mIsCameraSupported={this.mIsCameraSupported}, mIsMicSupported={this.mIsMicSupported}, mIsWriteStorageSupported={this.mIsWriteStorageSupported}, mCaptureModesSupported={string.Join(",", ((IEnumerable<bool>) this.mCaptureModesSupported).Select<bool, string>((Func<bool, string>) (p => p.ToString())).ToArray<string>())}, mQualityLevelsSupported={string.Join(",", ((IEnumerable<bool>) this.mQualityLevelsSupported).Select<bool, string>((Func<bool, string>) (p => p.ToString())).ToArray<string>())}]";
  }
}
