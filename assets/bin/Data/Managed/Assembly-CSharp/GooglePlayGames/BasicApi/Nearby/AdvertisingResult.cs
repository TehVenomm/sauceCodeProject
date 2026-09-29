// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Nearby.AdvertisingResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;

#nullable disable
namespace GooglePlayGames.BasicApi.Nearby;

public struct AdvertisingResult(ResponseStatus status, string localEndpointName)
{
  private readonly ResponseStatus mStatus = status;
  private readonly string mLocalEndpointName = Misc.CheckNotNull<string>(localEndpointName);

  public bool Succeeded => this.mStatus == ResponseStatus.Success;

  public ResponseStatus Status => this.mStatus;

  public string LocalEndpointName => this.mLocalEndpointName;
}
