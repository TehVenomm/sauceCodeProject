// Decompiled with JetBrains decompiler
// Type: Com.Google.Android.Gms.Games.Stats.Stats_LoadPlayerStatsResultObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Com.Google.Android.Gms.Common.Api;
using Google.Developers;
using System;

#nullable disable
namespace Com.Google.Android.Gms.Games.Stats;

public class Stats_LoadPlayerStatsResultObject(IntPtr ptr) : 
  JavaObjWrapper(ptr),
  Stats_LoadPlayerStatsResult,
  Result
{
  private const string CLASS_NAME = "com/google/android/gms/games/stats/Stats$LoadPlayerStatsResult";

  public PlayerStats getPlayerStats()
  {
    return (PlayerStats) new PlayerStatsObject(this.InvokeCall<IntPtr>(nameof (getPlayerStats), "()Lcom/google/android/gms/games/stats/PlayerStats;"));
  }

  public Status getStatus()
  {
    return new Status(this.InvokeCall<IntPtr>(nameof (getStatus), "()Lcom/google/android/gms/common/api/Status;"));
  }
}
