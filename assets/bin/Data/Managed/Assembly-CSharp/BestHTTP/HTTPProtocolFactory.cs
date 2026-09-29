// Decompiled with JetBrains decompiler
// Type: BestHTTP.HTTPProtocolFactory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using BestHTTP.WebSocket;
using System;
using System.IO;

#nullable disable
namespace BestHTTP;

internal static class HTTPProtocolFactory
{
  public static HTTPResponse Get(
    SupportedProtocols protocol,
    HTTPRequest request,
    Stream stream,
    bool isStreamed,
    bool isFromCache)
  {
    return protocol == SupportedProtocols.WebSocket ? (HTTPResponse) new WebSocketResponse(request, stream, isStreamed, isFromCache) : new HTTPResponse(request, stream, isStreamed, isFromCache);
  }

  public static SupportedProtocols GetProtocolFromUri(Uri uri)
  {
    string lowerInvariant = uri.Scheme.ToLowerInvariant();
    return lowerInvariant == "ws" || lowerInvariant == "wss" ? SupportedProtocols.WebSocket : SupportedProtocols.HTTP;
  }

  public static bool IsSecureProtocol(Uri uri)
  {
    string lowerInvariant = uri.Scheme.ToLowerInvariant();
    return lowerInvariant == "https" || lowerInvariant == "wss";
  }
}
