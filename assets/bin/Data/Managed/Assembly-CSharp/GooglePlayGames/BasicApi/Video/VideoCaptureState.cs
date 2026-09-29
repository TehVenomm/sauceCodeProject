// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Video.VideoCaptureState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace GooglePlayGames.BasicApi.Video;

public class VideoCaptureState
{
  private bool mIsCapturing;
  private VideoCaptureMode mCaptureMode;
  private VideoQualityLevel mQualityLevel;
  private bool mIsOverlayVisible;
  private bool mIsPaused;

  internal VideoCaptureState(
    bool isCapturing,
    VideoCaptureMode captureMode,
    VideoQualityLevel qualityLevel,
    bool isOverlayVisible,
    bool isPaused)
  {
    this.mIsCapturing = isCapturing;
    this.mCaptureMode = captureMode;
    this.mQualityLevel = qualityLevel;
    this.mIsOverlayVisible = isOverlayVisible;
    this.mIsPaused = isPaused;
  }

  public bool IsCapturing => this.mIsCapturing;

  public VideoCaptureMode CaptureMode => this.mCaptureMode;

  public VideoQualityLevel QualityLevel => this.mQualityLevel;

  public bool IsOverlayVisible => this.mIsOverlayVisible;

  public bool IsPaused => this.mIsPaused;

  public override string ToString()
  {
    return $"[VideoCaptureState: mIsCapturing={this.mIsCapturing}, mCaptureMode={this.mCaptureMode.ToString()}, mQualityLevel={this.mQualityLevel.ToString()}, mIsOverlayVisible={this.mIsOverlayVisible}, mIsPaused={this.mIsPaused}]";
  }
}
