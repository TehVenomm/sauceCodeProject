// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Achievement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace GooglePlayGames.BasicApi;

public class Achievement
{
  private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
  private string mId = string.Empty;
  private bool mIsIncremental;
  private bool mIsRevealed;
  private bool mIsUnlocked;
  private int mCurrentSteps;
  private int mTotalSteps;
  private string mDescription = string.Empty;
  private string mName = string.Empty;
  private long mLastModifiedTime;
  private ulong mPoints;
  private string mRevealedImageUrl;
  private string mUnlockedImageUrl;

  public override string ToString()
  {
    return $"[Achievement] id={this.mId}, name={this.mName}, desc={this.mDescription}, type={(this.mIsIncremental ? (object) "INCREMENTAL" : (object) "STANDARD")}, revealed={this.mIsRevealed}, unlocked={this.mIsUnlocked}, steps={this.mCurrentSteps}/{this.mTotalSteps}";
  }

  public bool IsIncremental
  {
    get => this.mIsIncremental;
    set => this.mIsIncremental = value;
  }

  public int CurrentSteps
  {
    get => this.mCurrentSteps;
    set => this.mCurrentSteps = value;
  }

  public int TotalSteps
  {
    get => this.mTotalSteps;
    set => this.mTotalSteps = value;
  }

  public bool IsUnlocked
  {
    get => this.mIsUnlocked;
    set => this.mIsUnlocked = value;
  }

  public bool IsRevealed
  {
    get => this.mIsRevealed;
    set => this.mIsRevealed = value;
  }

  public string Id
  {
    get => this.mId;
    set => this.mId = value;
  }

  public string Description
  {
    get => this.mDescription;
    set => this.mDescription = value;
  }

  public string Name
  {
    get => this.mName;
    set => this.mName = value;
  }

  public DateTime LastModifiedTime
  {
    get => Achievement.UnixEpoch.AddMilliseconds((double) this.mLastModifiedTime);
    set => this.mLastModifiedTime = (long) (value - Achievement.UnixEpoch).TotalMilliseconds;
  }

  public ulong Points
  {
    get => this.mPoints;
    set => this.mPoints = value;
  }

  public string RevealedImageUrl
  {
    get => this.mRevealedImageUrl;
    set => this.mRevealedImageUrl = value;
  }

  public string UnlockedImageUrl
  {
    get => this.mUnlockedImageUrl;
    set => this.mUnlockedImageUrl = value;
  }
}
