// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeConnectionResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.BasicApi.Nearby;
using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class NativeConnectionResponse : BaseReferenceHolder
{
  internal NativeConnectionResponse(IntPtr pointer)
    : base(pointer)
  {
  }

  internal string RemoteEndpointId()
  {
    return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_arg, out_size) => NearbyConnectionTypes.ConnectionResponse_GetRemoteEndpointId(this.SelfPtr(), out_arg, out_size)));
  }

  internal NearbyConnectionTypes.ConnectionResponse_ResponseCode ResponseCode()
  {
    return NearbyConnectionTypes.ConnectionResponse_GetStatus(this.SelfPtr());
  }

  internal byte[] Payload()
  {
    return PInvokeUtilities.OutParamsToArray<byte>((PInvokeUtilities.OutMethod<byte>) ((out_arg, out_size) => NearbyConnectionTypes.ConnectionResponse_GetPayload(this.SelfPtr(), out_arg, out_size)));
  }

  protected override void CallDispose(HandleRef selfPointer)
  {
    NearbyConnectionTypes.ConnectionResponse_Dispose(selfPointer);
  }

  internal ConnectionResponse AsResponse(long localClientId)
  {
    switch (this.ResponseCode())
    {
      case NearbyConnectionTypes.ConnectionResponse_ResponseCode.ERROR_ENDPOINT_NOT_CONNECTED:
        return ConnectionResponse.EndpointNotConnected(localClientId, this.RemoteEndpointId());
      case NearbyConnectionTypes.ConnectionResponse_ResponseCode.ERROR_ENDPOINT_ALREADY_CONNECTED:
        return ConnectionResponse.AlreadyConnected(localClientId, this.RemoteEndpointId());
      case NearbyConnectionTypes.ConnectionResponse_ResponseCode.ERROR_NETWORK_NOT_CONNECTED:
        return ConnectionResponse.NetworkNotConnected(localClientId, this.RemoteEndpointId());
      case NearbyConnectionTypes.ConnectionResponse_ResponseCode.ERROR_INTERNAL:
        return ConnectionResponse.InternalError(localClientId, this.RemoteEndpointId());
      case NearbyConnectionTypes.ConnectionResponse_ResponseCode.ACCEPTED:
        return ConnectionResponse.Accepted(localClientId, this.RemoteEndpointId(), this.Payload());
      case NearbyConnectionTypes.ConnectionResponse_ResponseCode.REJECTED:
        return ConnectionResponse.Rejected(localClientId, this.RemoteEndpointId());
      default:
        throw new InvalidOperationException("Found connection response of unknown type: " + (object) this.ResponseCode());
    }
  }

  internal static NativeConnectionResponse FromPointer(IntPtr pointer)
  {
    return pointer == IntPtr.Zero ? (NativeConnectionResponse) null : new NativeConnectionResponse(pointer);
  }
}
