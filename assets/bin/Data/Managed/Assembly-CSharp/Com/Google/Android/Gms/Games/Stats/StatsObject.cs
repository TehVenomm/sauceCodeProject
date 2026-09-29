// Decompiled with JetBrains decompiler
// Type: Com.Google.Android.Gms.Games.Stats.StatsObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Com.Google.Android.Gms.Common.Api;
using Google.Developers;
using System;

#nullable disable
namespace Com.Google.Android.Gms.Games.Stats;

public class StatsObject(IntPtr ptr) : JavaObjWrapper(ptr), Com.Google.Android.Gms.Games.Stats.Stats
{
  private const string CLASS_NAME = "com/google/android/gms/games/stats/Stats";

  public PendingResult<Stats_LoadPlayerStatsResultObject> loadPlayerStats(
    GoogleApiClient arg_GoogleApiClient_1,
    bool arg_bool_2)
  {
    return new PendingResult<Stats_LoadPlayerStatsResultObject>(this.InvokeCall<IntPtr>(nameof (loadPlayerStats), "(Lcom/google/android/gms/common/api/GoogleApiClient;Z)Lcom/google/android/gms/common/api/PendingResult;", (object) arg_GoogleApiClient_1, (object) arg_bool_2));
  }
}
