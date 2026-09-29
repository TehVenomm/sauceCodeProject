// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.PlayerStats
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace GooglePlayGames.BasicApi;

public class PlayerStats
{
  private static float UNSET_VALUE = -1f;

  public bool Valid { get; set; }

  public int NumberOfPurchases { get; set; }

  public float AvgSessonLength { get; set; }

  public int DaysSinceLastPlayed { get; set; }

  public int NumberOfSessions { get; set; }

  public float SessPercentile { get; set; }

  public float SpendPercentile { get; set; }

  public float SpendProbability { get; set; }

  public float ChurnProbability { get; set; }

  public float HighSpenderProbability { get; set; }

  public float TotalSpendNext28Days { get; set; }

  public PlayerStats() => this.Valid = false;

  public bool HasNumberOfPurchases() => this.NumberOfPurchases != (int) PlayerStats.UNSET_VALUE;

  public bool HasAvgSessonLength()
  {
    return (double) this.AvgSessonLength != (double) PlayerStats.UNSET_VALUE;
  }

  public bool HasDaysSinceLastPlayed() => this.DaysSinceLastPlayed != (int) PlayerStats.UNSET_VALUE;

  public bool HasNumberOfSessions() => this.NumberOfSessions != (int) PlayerStats.UNSET_VALUE;

  public bool HasSessPercentile()
  {
    return (double) this.SessPercentile != (double) PlayerStats.UNSET_VALUE;
  }

  public bool HasSpendPercentile()
  {
    return (double) this.SpendPercentile != (double) PlayerStats.UNSET_VALUE;
  }

  public bool HasChurnProbability()
  {
    return (double) this.ChurnProbability != (double) PlayerStats.UNSET_VALUE;
  }

  public bool HasHighSpenderProbability()
  {
    return (double) this.HighSpenderProbability != (double) PlayerStats.UNSET_VALUE;
  }

  public bool HasTotalSpendNext28Days()
  {
    return (double) this.TotalSpendNext28Days != (double) PlayerStats.UNSET_VALUE;
  }
}
