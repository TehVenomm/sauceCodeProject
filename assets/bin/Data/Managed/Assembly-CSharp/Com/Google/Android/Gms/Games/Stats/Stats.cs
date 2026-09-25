// Decompiled with JetBrains decompiler
// Type: Com.Google.Android.Gms.Games.Stats.Stats
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Com.Google.Android.Gms.Common.Api;

#nullable disable
namespace Com.Google.Android.Gms.Games.Stats;

public interface Stats
{
  PendingResult<Stats_LoadPlayerStatsResultObject> loadPlayerStats(
    GoogleApiClient arg_GoogleApiClient_1,
    bool arg_bool_2);
}
