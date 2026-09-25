// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Nearby.EndpointDetails
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;

#nullable disable
namespace GooglePlayGames.BasicApi.Nearby;

public struct EndpointDetails(string endpointId, string deviceId, string name, string serviceId)
{
  private readonly string mEndpointId = Misc.CheckNotNull<string>(endpointId);
  private readonly string mDeviceId = Misc.CheckNotNull<string>(deviceId);
  private readonly string mName = Misc.CheckNotNull<string>(name);
  private readonly string mServiceId = Misc.CheckNotNull<string>(serviceId);

  public string EndpointId => this.mEndpointId;

  public string DeviceId => this.mDeviceId;

  public string Name => this.mName;

  public string ServiceId => this.mServiceId;
}
