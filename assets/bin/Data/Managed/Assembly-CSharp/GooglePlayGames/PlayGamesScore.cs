// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.PlayGamesScore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine.SocialPlatforms;

#nullable disable
namespace GooglePlayGames;

public class PlayGamesScore : IScore
{
  private string mLbId;
  private long mValue;
  private ulong mRank;
  private string mPlayerId = string.Empty;
  private string mMetadata = string.Empty;
  private DateTime mDate = new DateTime(1970, 1, 1, 0, 0, 0);

  internal PlayGamesScore(
    DateTime date,
    string leaderboardId,
    ulong rank,
    string playerId,
    ulong value,
    string metadata)
  {
    this.mDate = date;
    this.mLbId = this.leaderboardID;
    this.mRank = rank;
    this.mPlayerId = playerId;
    this.mValue = (long) value;
    this.mMetadata = metadata;
  }

  public void ReportScore(Action<bool> callback)
  {
    PlayGamesPlatform.Instance.ReportScore(this.mValue, this.mLbId, this.mMetadata, callback);
  }

  public string leaderboardID
  {
    get => this.mLbId;
    set => this.mLbId = value;
  }

  public long value
  {
    get => this.mValue;
    set => this.mValue = value;
  }

  public DateTime date => this.mDate;

  public string formattedValue => this.mValue.ToString();

  public string userID => this.mPlayerId;

  public int rank => (int) this.mRank;

  public string metaData => this.mMetadata;
}
