// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Nearby.ConnectionResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;

#nullable disable
namespace GooglePlayGames.BasicApi.Nearby;

public struct ConnectionResponse
{
  private static readonly byte[] EmptyPayload = new byte[0];
  private readonly long mLocalClientId;
  private readonly string mRemoteEndpointId;
  private readonly ConnectionResponse.Status mResponseStatus;
  private readonly byte[] mPayload;

  private ConnectionResponse(
    long localClientId,
    string remoteEndpointId,
    ConnectionResponse.Status code,
    byte[] payload)
  {
    this.mLocalClientId = localClientId;
    this.mRemoteEndpointId = Misc.CheckNotNull<string>(remoteEndpointId);
    this.mResponseStatus = code;
    this.mPayload = Misc.CheckNotNull<byte[]>(payload);
  }

  public long LocalClientId => this.mLocalClientId;

  public string RemoteEndpointId => this.mRemoteEndpointId;

  public ConnectionResponse.Status ResponseStatus => this.mResponseStatus;

  public byte[] Payload => this.mPayload;

  public static ConnectionResponse Rejected(long localClientId, string remoteEndpointId)
  {
    return new ConnectionResponse(localClientId, remoteEndpointId, ConnectionResponse.Status.Rejected, ConnectionResponse.EmptyPayload);
  }

  public static ConnectionResponse NetworkNotConnected(long localClientId, string remoteEndpointId)
  {
    return new ConnectionResponse(localClientId, remoteEndpointId, ConnectionResponse.Status.ErrorNetworkNotConnected, ConnectionResponse.EmptyPayload);
  }

  public static ConnectionResponse InternalError(long localClientId, string remoteEndpointId)
  {
    return new ConnectionResponse(localClientId, remoteEndpointId, ConnectionResponse.Status.ErrorInternal, ConnectionResponse.EmptyPayload);
  }

  public static ConnectionResponse EndpointNotConnected(long localClientId, string remoteEndpointId)
  {
    return new ConnectionResponse(localClientId, remoteEndpointId, ConnectionResponse.Status.ErrorEndpointNotConnected, ConnectionResponse.EmptyPayload);
  }

  public static ConnectionResponse Accepted(
    long localClientId,
    string remoteEndpointId,
    byte[] payload)
  {
    return new ConnectionResponse(localClientId, remoteEndpointId, ConnectionResponse.Status.Accepted, payload);
  }

  public static ConnectionResponse AlreadyConnected(long localClientId, string remoteEndpointId)
  {
    return new ConnectionResponse(localClientId, remoteEndpointId, ConnectionResponse.Status.ErrorAlreadyConnected, ConnectionResponse.EmptyPayload);
  }

  public enum Status
  {
    Accepted,
    Rejected,
    ErrorInternal,
    ErrorNetworkNotConnected,
    ErrorEndpointNotConnected,
    ErrorAlreadyConnected,
  }
}
