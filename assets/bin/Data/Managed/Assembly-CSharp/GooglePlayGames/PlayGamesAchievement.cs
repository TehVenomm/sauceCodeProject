// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.PlayGamesAchievement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.BasicApi;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms;

#nullable disable
namespace GooglePlayGames;

internal class PlayGamesAchievement : IAchievement, IAchievementDescription
{
  private readonly GooglePlayGames.ReportProgress mProgressCallback;
  private string mId = string.Empty;
  private bool mIsIncremental;
  private int mCurrentSteps;
  private int mTotalSteps;
  private double mPercentComplete;
  private bool mCompleted;
  private bool mHidden;
  private DateTime mLastModifiedTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
  private string mTitle = string.Empty;
  private string mRevealedImageUrl = string.Empty;
  private string mUnlockedImageUrl = string.Empty;
  private UnityWebRequest mImageFetcher;
  private Texture2D mImage;
  private string mDescription = string.Empty;
  private ulong mPoints;

  internal PlayGamesAchievement()
    : this(new GooglePlayGames.ReportProgress(PlayGamesPlatform.Instance.ReportProgress))
  {
  }

  internal PlayGamesAchievement(GooglePlayGames.ReportProgress progressCallback)
  {
    this.mProgressCallback = progressCallback;
  }

  internal PlayGamesAchievement(Achievement ach)
    : this()
  {
    this.mId = ach.Id;
    this.mIsIncremental = ach.IsIncremental;
    this.mCurrentSteps = ach.CurrentSteps;
    this.mTotalSteps = ach.TotalSteps;
    this.mPercentComplete = !ach.IsIncremental ? (ach.IsUnlocked ? 100.0 : 0.0) : (ach.TotalSteps <= 0 ? 0.0 : (double) ach.CurrentSteps / (double) ach.TotalSteps * 100.0);
    this.mCompleted = ach.IsUnlocked;
    this.mHidden = !ach.IsRevealed;
    this.mLastModifiedTime = ach.LastModifiedTime;
    this.mTitle = ach.Name;
    this.mDescription = ach.Description;
    this.mPoints = ach.Points;
    this.mRevealedImageUrl = ach.RevealedImageUrl;
    this.mUnlockedImageUrl = ach.UnlockedImageUrl;
  }

  public void ReportProgress(Action<bool> callback)
  {
    this.mProgressCallback(this.mId, this.mPercentComplete, callback);
  }

  private Texture2D LoadImage()
  {
    if (this.hidden)
      return (Texture2D) null;
    string str = this.completed ? this.mUnlockedImageUrl : this.mRevealedImageUrl;
    if (!string.IsNullOrEmpty(str))
    {
      if (this.mImageFetcher == null || this.mImageFetcher.url != str)
      {
        this.mImageFetcher = UnityWebRequestTexture.GetTexture(str);
        this.mImageFetcher.SendWebRequest();
        this.mImage = (Texture2D) null;
      }
      if (Object.op_Inequality((Object) this.mImage, (Object) null))
        return this.mImage;
      if (this.mImageFetcher.isDone)
      {
        this.mImage = DownloadHandlerTexture.GetContent(this.mImageFetcher);
        return this.mImage;
      }
    }
    return (Texture2D) null;
  }

  public string id
  {
    get => this.mId;
    set => this.mId = value;
  }

  public bool isIncremental => this.mIsIncremental;

  public int currentSteps => this.mCurrentSteps;

  public int totalSteps => this.mTotalSteps;

  public double percentCompleted
  {
    get => this.mPercentComplete;
    set => this.mPercentComplete = value;
  }

  public bool completed => this.mCompleted;

  public bool hidden => this.mHidden;

  public DateTime lastReportedDate => this.mLastModifiedTime;

  public string title => this.mTitle;

  public Texture2D image => this.LoadImage();

  public string achievedDescription => this.mDescription;

  public string unachievedDescription => this.mDescription;

  public int points => (int) this.mPoints;
}
