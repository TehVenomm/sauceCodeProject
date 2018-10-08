using BestHTTP.WebSocket.Frames;
using System;

namespace BestHTTP.WebSocket
{
	public sealed class WebSocket
	{
		public Action<WebSocket> OnOpen;

		public Action<WebSocket, string> OnMessage;

		public Action<WebSocket, byte[]> OnBinary;

		public Action<WebSocket, byte[]> OnPong;

		public Action<WebSocket, ushort, string> OnClosed;

		public Action<WebSocket, Exception> OnError;

		public Action<WebSocket, WebSocketFrameReader> OnIncompleteFrame;

		private bool requestSent;

		private WebSocketResponse webSocket;

		public HTTPRequest InternalRequest
		{
			get;
			private set;
		}

		public bool IsOpen => webSocket != null && !webSocket.IsClosed;

		public bool StartPingThread
		{
			get;
			set;
		}

		public int PingFrequency
		{
			get;
			set;
		}

		public WebSocket(Uri uri)
			: this(uri, string.Empty, string.Empty)
		{
		}

		public WebSocket(Uri uri, string protocol)
			: this(uri, protocol, string.Empty)
		{
		}

		public unsafe WebSocket(Uri uri, string origin, string protocol = "")
		{
			PingFrequency = 1000;
			if (uri.Port == -1)
			{
				uri = new Uri(uri.Scheme + "://" + uri.Host + ":" + ((!uri.Scheme.Equals("wss", StringComparison.OrdinalIgnoreCase)) ? "80" : "443") + uri.PathAndQuery);
			}
			InternalRequest = new HTTPRequest(uri, new Action<HTTPRequest, HTTPResponse>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			InternalRequest.SetHeader("Host", uri.Host + ":" + uri.Port);
			InternalRequest.SetHeader("Upgrade", "websocket");
			InternalRequest.SetHeader("Connection", "keep-alive, Upgrade");
			InternalRequest.SetHeader("Sec-WebSocket-Key", GetSecKey(new object[4]
			{
				this,
				InternalRequest,
				uri,
				new object()
			}));
			if (!string.IsNullOrEmpty(origin))
			{
				InternalRequest.SetHeader("Origin", origin);
			}
			InternalRequest.SetHeader("Sec-WebSocket-Version", "13");
			if (!string.IsNullOrEmpty(protocol))
			{
				InternalRequest.SetHeader("Sec-WebSocket-Protocol", protocol);
			}
			InternalRequest.SetHeader("Cache-Control", "no-cache");
			InternalRequest.SetHeader("Pragma", "no-cache");
			InternalRequest.OnUpgraded = new Action<HTTPRequest, HTTPResponse>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}

		public void Open()
		{
			if (!requestSent && InternalRequest != null)
			{
				InternalRequest.Send();
				requestSent = true;
			}
		}

		public void Send(string message)
		{
			if (IsOpen)
			{
				webSocket.Send(message);
			}
		}

		public void Send(byte[] buffer)
		{
			if (IsOpen)
			{
				webSocket.Send(buffer);
			}
		}

		public void Send(IWebSocketFrameWriter frame)
		{
			if (IsOpen)
			{
				webSocket.Send(frame);
			}
		}

		public void Close()
		{
			if (IsOpen)
			{
				webSocket.Close();
			}
		}

		public void Close(ushort code, string message)
		{
			if (IsOpen)
			{
				webSocket.Close(code, message);
			}
		}

		private string GetSecKey(object[] from)
		{
			byte[] array = new byte[16];
			int num = 0;
			for (int i = 0; i < from.Length; i++)
			{
				byte[] bytes = BitConverter.GetBytes(from[i].GetHashCode());
				for (int j = 0; j < bytes.Length; j++)
				{
					if (num >= array.Length)
					{
						break;
					}
					array[num++] = bytes[j];
				}
			}
			return Convert.ToBase64String(array);
		}
	}
}
