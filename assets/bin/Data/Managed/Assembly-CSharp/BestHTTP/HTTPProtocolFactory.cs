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
			string text = uri.Scheme.ToLowerInvariant();
			switch (text)
			{
			case "ws":
			case "wss":
				return SupportedProtocols.WebSocket;
			default:
				return SupportedProtocols.HTTP;
			}
		}

		public static bool IsSecureProtocol(Uri uri)
		{
			string text = uri.Scheme.ToLowerInvariant();
			switch (text)
			{
			case "https":
			case "wss":
				return true;
			default:
				return false;
			}
		}
	}
}
