// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.ScorePageToken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace GooglePlayGames.BasicApi;

public class ScorePageToken
{
  private string mId;
  private object mInternalObject;
  private LeaderboardCollection mCollection;
  private LeaderboardTimeSpan mTimespan;

  internal ScorePageToken(
    object internalObject,
    string id,
    LeaderboardCollection collection,
    LeaderboardTimeSpan timespan)
  {
    this.mInternalObject = internalObject;
    this.mId = id;
    this.mCollection = collection;
    this.mTimespan = timespan;
  }

  public LeaderboardCollection Collection => this.mCollection;

  public LeaderboardTimeSpan TimeSpan => this.mTimespan;

  public string LeaderboardId => this.mId;

  internal object InternalObject => this.mInternalObject;
}
