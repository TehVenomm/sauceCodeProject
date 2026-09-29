// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.LeaderboardScoreData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine.SocialPlatforms;

#nullable disable
namespace GooglePlayGames.BasicApi;

public class LeaderboardScoreData
{
  private string mId;
  private ResponseStatus mStatus;
  private ulong mApproxCount;
  private string mTitle;
  private IScore mPlayerScore;
  private ScorePageToken mPrevPage;
  private ScorePageToken mNextPage;
  private List<PlayGamesScore> mScores = new List<PlayGamesScore>();

  internal LeaderboardScoreData(string leaderboardId) => this.mId = leaderboardId;

  internal LeaderboardScoreData(string leaderboardId, ResponseStatus status)
  {
    this.mId = leaderboardId;
    this.mStatus = status;
  }

  public bool Valid
  {
    get
    {
      return this.mStatus == ResponseStatus.Success || this.mStatus == ResponseStatus.SuccessWithStale;
    }
  }

  public ResponseStatus Status
  {
    get => this.mStatus;
    internal set => this.mStatus = value;
  }

  public ulong ApproximateCount
  {
    get => this.mApproxCount;
    internal set => this.mApproxCount = value;
  }

  public string Title
  {
    get => this.mTitle;
    internal set => this.mTitle = value;
  }

  public string Id
  {
    get => this.mId;
    internal set => this.mId = value;
  }

  public IScore PlayerScore
  {
    get => this.mPlayerScore;
    internal set => this.mPlayerScore = value;
  }

  public IScore[] Scores => (IScore[]) this.mScores.ToArray();

  internal int AddScore(PlayGamesScore score)
  {
    this.mScores.Add(score);
    return this.mScores.Count;
  }

  public ScorePageToken PrevPageToken
  {
    get => this.mPrevPage;
    internal set => this.mPrevPage = value;
  }

  public ScorePageToken NextPageToken
  {
    get => this.mNextPage;
    internal set => this.mNextPage = value;
  }

  public override string ToString()
  {
    return $"[LeaderboardScoreData: mId={this.mId},  mStatus={this.mStatus}, mApproxCount={this.mApproxCount}, mTitle={this.mTitle}]";
  }
}
