// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Nearby.NearbyConnectionConfiguration
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;
using System;

#nullable disable
namespace GooglePlayGames.BasicApi.Nearby;

public struct NearbyConnectionConfiguration(
  Action<InitializationStatus> callback,
  long localClientId)
{
  public const int MaxUnreliableMessagePayloadLength = 1168;
  public const int MaxReliableMessagePayloadLength = 4096 /*0x1000*/;
  private readonly Action<InitializationStatus> mInitializationCallback = Misc.CheckNotNull<Action<InitializationStatus>>(callback);
  private readonly long mLocalClientId = localClientId;

  public long LocalClientId => this.mLocalClientId;

  public Action<InitializationStatus> InitializationCallback => this.mInitializationCallback;
}
