// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Nearby.ConnectionRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;

#nullable disable
namespace GooglePlayGames.BasicApi.Nearby;

public struct ConnectionRequest
{
  private readonly EndpointDetails mRemoteEndpoint;
  private readonly byte[] mPayload;

  public ConnectionRequest(
    string remoteEndpointId,
    string remoteDeviceId,
    string remoteEndpointName,
    string serviceId,
    byte[] payload)
  {
    Logger.d("Constructing ConnectionRequest");
    this.mRemoteEndpoint = new EndpointDetails(remoteEndpointId, remoteDeviceId, remoteEndpointName, serviceId);
    this.mPayload = Misc.CheckNotNull<byte[]>(payload);
  }

  public EndpointDetails RemoteEndpoint => this.mRemoteEndpoint;

  public byte[] Payload => this.mPayload;
}
