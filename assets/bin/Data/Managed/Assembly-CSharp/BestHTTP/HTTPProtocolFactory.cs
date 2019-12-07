using BestHTTP.WebSocket;
using System;
using System.IO;

namespace BestHTTP
{
	internal static class HTTPProtocolFactory
	{
		public static HTTPResponse Get(SupportedProtocols protocol, HTTPRequest request, Stream stream, bool isStreamed, bool isFromCache)
		{
			if (protocol == SupportedProtocols.WebSocket)
			{
				return new WebSocketResponse(request, stream, isStreamed, isFromCache);
			}
			return new HTTPResponse(request, stream, isStreamed, isFromCache);
		}

		public static SupportedProtocols GetProtocolFromUri(Uri uri)
		{
			string a = uri.Scheme.ToLowerInvariant();
			if (a == "ws" || a == "wss")
			{
				return SupportedProtocols.WebSocket;
			}
			return SupportedProtocols.HTTP;
		}

		public static bool IsSecureProtocol(Uri uri)
		{
			string a = uri.Scheme.ToLowerInvariant();
			if (a == "https" || a == "wss")
			{
				return true;
			}
			return false;
		}
	}
}
