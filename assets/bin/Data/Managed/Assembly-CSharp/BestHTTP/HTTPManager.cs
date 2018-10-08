using BestHTTP.Caching;
using BestHTTP.WebSocket;
using System;
using System.Collections.Generic;

namespace BestHTTP
{
	public static class HTTPManager
	{
		private static byte maxConnectionPerServer;

		private static Dictionary<string, List<HTTPConnection>> Connections;

		private static List<HTTPConnection> ActiveConnections;

		private static List<HTTPConnection> RecycledConnections;

		private static List<HTTPRequest> RequestQueue;

		private static bool IsCallingCallbacks;

		public static byte MaxConnectionPerServer
		{
			get
			{
				return maxConnectionPerServer;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("MaxConnectionPerServer must be greater than 0!");
				}
				maxConnectionPerServer = value;
			}
		}

		public static bool KeepAliveDefaultValue
		{
			get;
			set;
		}

		public static bool IsCachingDisabled
		{
			get;
			set;
		}

		public static TimeSpan MaxConnectionIdleTime
		{
			get;
			set;
		}

		internal static int MaxPathLength
		{
			get;
			set;
		}

		static HTTPManager()
		{
			Connections = new Dictionary<string, List<HTTPConnection>>();
			ActiveConnections = new List<HTTPConnection>();
			RecycledConnections = new List<HTTPConnection>();
			RequestQueue = new List<HTTPRequest>();
			MaxConnectionPerServer = 4;
			KeepAliveDefaultValue = true;
			MaxPathLength = 255;
			MaxConnectionIdleTime = TimeSpan.FromMinutes(2.0);
		}

		public static HTTPRequest SendRequest(string url, Action<HTTPRequest, HTTPResponse> callback)
		{
			return SendRequest(new HTTPRequest(new Uri(url), HTTPMethods.Get, callback));
		}

		public static HTTPRequest SendRequest(string url, HTTPMethods methodType, Action<HTTPRequest, HTTPResponse> callback)
		{
			return SendRequest(new HTTPRequest(new Uri(url), methodType, callback));
		}

		public static HTTPRequest SendRequest(string url, HTTPMethods methodType, bool isKeepAlive, Action<HTTPRequest, HTTPResponse> callback)
		{
			return SendRequest(new HTTPRequest(new Uri(url), methodType, isKeepAlive, callback));
		}

		public static HTTPRequest SendRequest(string url, HTTPMethods methodType, bool isKeepAlive, bool disableCache, Action<HTTPRequest, HTTPResponse> callback)
		{
			return SendRequest(new HTTPRequest(new Uri(url), methodType, isKeepAlive, disableCache, callback));
		}

		public static HTTPRequest SendRequest(HTTPRequest request)
		{
			HTTPUpdateDelegator.CheckInstance();
			if (IsCallingCallbacks)
			{
				RequestQueue.Add(request);
			}
			else
			{
				SendRequestImpl(request);
			}
			return request;
		}

		private static void SendRequestImpl(HTTPRequest request)
		{
			HTTPConnection conn = FindOrCreateFreeConnection(request.CurrentUri);
			if (conn != null)
			{
				if (ActiveConnections.Find((HTTPConnection c) => c == conn) == null)
				{
					ActiveConnections.Add(conn);
				}
				conn.Process(request);
			}
			else
			{
				RequestQueue.Add(request);
			}
		}

		private static HTTPConnection FindOrCreateFreeConnection(Uri uri)
		{
			HTTPConnection hTTPConnection = null;
			string text = new UriBuilder(uri.Scheme, uri.Host, uri.Port).Uri.ToString();
			if (Connections.TryGetValue(text, out List<HTTPConnection> value))
			{
				for (int i = 0; i < value.Count; i++)
				{
					if (hTTPConnection != null)
					{
						break;
					}
					if (value[i] != null && value[i].IsFree)
					{
						hTTPConnection = value[i];
					}
				}
			}
			else
			{
				Connections.Add(text, value = new List<HTTPConnection>(MaxConnectionPerServer));
			}
			if (hTTPConnection == null)
			{
				if (value.Count == MaxConnectionPerServer)
				{
					return null;
				}
				value.Add(hTTPConnection = new HTTPConnection(text));
			}
			return hTTPConnection;
		}

		private static void RecycleConnection(HTTPConnection conn)
		{
			conn.Recycle();
			RecycledConnections.Add(conn);
		}

		internal static void OnUpdate()
		{
			IsCallingCallbacks = true;
			try
			{
				for (int i = 0; i < ActiveConnections.Count; i++)
				{
					HTTPConnection hTTPConnection = ActiveConnections[i];
					switch (hTTPConnection.State)
					{
					case HTTPConnectionStates.Processing:
						if (hTTPConnection.CurrentRequest.UseStreaming && hTTPConnection.CurrentRequest.Response != null && hTTPConnection.CurrentRequest.Response.HasStreamedFragments())
						{
							hTTPConnection.HandleCallback();
						}
						break;
					case HTTPConnectionStates.Redirected:
						SendRequest(hTTPConnection.CurrentRequest);
						RecycleConnection(hTTPConnection);
						break;
					case HTTPConnectionStates.WaitForRecycle:
						hTTPConnection.CurrentRequest.FinishStreaming();
						hTTPConnection.HandleCallback();
						RecycleConnection(hTTPConnection);
						break;
					case HTTPConnectionStates.Upgraded:
						hTTPConnection.HandleCallback();
						break;
					case HTTPConnectionStates.WaitForProtocolShutdown:
					{
						WebSocketResponse webSocketResponse = hTTPConnection.CurrentRequest.Response as WebSocketResponse;
						webSocketResponse.HandleEvents();
						if (webSocketResponse.IsClosed)
						{
							hTTPConnection.HandleCallback();
							hTTPConnection.Dispose();
							RecycleConnection(hTTPConnection);
						}
						break;
					}
					case HTTPConnectionStates.Closed:
						hTTPConnection.CurrentRequest.FinishStreaming();
						hTTPConnection.HandleCallback();
						RecycleConnection(hTTPConnection);
						Connections[hTTPConnection.ServerAddress].Remove(hTTPConnection);
						break;
					case HTTPConnectionStates.Free:
						if (hTTPConnection.IsRemovable)
						{
							hTTPConnection.Dispose();
							Connections[hTTPConnection.ServerAddress].Remove(hTTPConnection);
						}
						break;
					}
				}
			}
			finally
			{
				IsCallingCallbacks = false;
			}
			if (RecycledConnections.Count > 0)
			{
				for (int j = 0; j < RecycledConnections.Count; j++)
				{
					if (RecycledConnections[j].IsFree)
					{
						ActiveConnections.Remove(RecycledConnections[j]);
					}
				}
				RecycledConnections.Clear();
			}
			if (RequestQueue.Count > 0)
			{
				HTTPRequest[] array = RequestQueue.ToArray();
				RequestQueue.Clear();
				for (int k = 0; k < array.Length; k++)
				{
					SendRequest(array[k]);
				}
			}
		}

		internal static void OnQuit()
		{
			HTTPCacheService.SaveLibrary();
			foreach (KeyValuePair<string, List<HTTPConnection>> connection in Connections)
			{
				foreach (HTTPConnection item in connection.Value)
				{
					item.Dispose();
				}
				connection.Value.Clear();
			}
			Connections.Clear();
		}
	}
}
