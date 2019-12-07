using BestHTTP.Authentication;
using BestHTTP.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace BestHTTP
{
	public sealed class HTTPRequest
	{
		internal static readonly byte[] EOL = new byte[2]
		{
			13,
			10
		};

		public Action<HTTPRequest, HTTPResponse> OnUpgraded;

		private bool isKeepAlive;

		private bool disableCache;

		private int streamFragmentSize;

		private bool useStreaming;

		private Action<HTTPRequest, HTTPResponse> callback;

		public Uri Uri
		{
			get;
			private set;
		}

		public HTTPMethods MethodType
		{
			get;
			private set;
		}

		public byte[] RawData
		{
			get;
			set;
		}

		public bool IsKeepAlive
		{
			get
			{
				return isKeepAlive;
			}
			set
			{
				if (isKeepAlive)
				{
					throw new NotSupportedException("Changing the IsKeepAlive property while processing the request is not supported.");
				}
				isKeepAlive = value;
			}
		}

		public bool DisableCache
		{
			get
			{
				return disableCache;
			}
			set
			{
				if (Processing)
				{
					throw new NotSupportedException("Changing the DisableCache property while processing the request is not supported.");
				}
				disableCache = value;
			}
		}

		public bool UseStreaming
		{
			get
			{
				return useStreaming;
			}
			set
			{
				if (Processing)
				{
					throw new NotSupportedException("Changing the UseStreaming property while processing the request is not supported.");
				}
				useStreaming = value;
			}
		}

		public int StreamFragmentSize
		{
			get
			{
				return streamFragmentSize;
			}
			set
			{
				if (Processing)
				{
					throw new NotSupportedException("Changing the StreamFragmentSize property while processing the request is not supported.");
				}
				if (value < 1)
				{
					throw new ArgumentException("StreamFragmentSize must be at least 1.");
				}
				streamFragmentSize = value;
			}
		}

		public Action<HTTPRequest, HTTPResponse> Callback
		{
			get
			{
				return callback;
			}
			set
			{
				if (Processing)
				{
					throw new NotSupportedException("Changing the StreamFragmentSize property while processing the request is not supported.");
				}
				callback = value;
			}
		}

		public bool DisableRetry
		{
			get;
			set;
		}

		public bool IsRedirected
		{
			get;
			internal set;
		}

		public Uri RedirectUri
		{
			get;
			internal set;
		}

		public Uri CurrentUri
		{
			get
			{
				if (!IsRedirected)
				{
					return Uri;
				}
				return RedirectUri;
			}
		}

		public HTTPResponse Response
		{
			get;
			internal set;
		}

		public Exception Exception
		{
			get;
			internal set;
		}

		public object Tag
		{
			get;
			set;
		}

		public Credentials Credentials
		{
			get;
			set;
		}

		public int MaxRedirects
		{
			get;
			set;
		}

		public bool UseAlternateSSL
		{
			get;
			set;
		}

		internal bool Processing
		{
			get;
			set;
		}

		internal int RedirectCount
		{
			get;
			set;
		}

		private Dictionary<string, List<string>> Headers
		{
			get;
			set;
		}

		private WWWForm FieldsImpl
		{
			get;
			set;
		}

		public HTTPRequest(Uri uri)
			: this(uri, HTTPMethods.Get, HTTPManager.KeepAliveDefaultValue, HTTPManager.IsCachingDisabled, null)
		{
		}

		public HTTPRequest(Uri uri, Action<HTTPRequest, HTTPResponse> callback)
			: this(uri, HTTPMethods.Get, HTTPManager.KeepAliveDefaultValue, HTTPManager.IsCachingDisabled, callback)
		{
		}

		public HTTPRequest(Uri uri, bool isKeepAlive, Action<HTTPRequest, HTTPResponse> callback)
			: this(uri, HTTPMethods.Get, isKeepAlive, HTTPManager.IsCachingDisabled, callback)
		{
		}

		public HTTPRequest(Uri uri, bool isKeepAlive, bool disableCache, Action<HTTPRequest, HTTPResponse> callback)
			: this(uri, HTTPMethods.Get, isKeepAlive, disableCache, callback)
		{
		}

		public HTTPRequest(Uri uri, HTTPMethods methodType, Action<HTTPRequest, HTTPResponse> callback)
			: this(uri, methodType, HTTPManager.KeepAliveDefaultValue, HTTPManager.IsCachingDisabled, callback)
		{
		}

		public HTTPRequest(Uri uri, HTTPMethods methodType, bool isKeepAlive, Action<HTTPRequest, HTTPResponse> callback)
			: this(uri, methodType, isKeepAlive, HTTPManager.IsCachingDisabled, callback)
		{
		}

		public HTTPRequest(Uri uri, HTTPMethods methodType, bool isKeepAlive, bool disableCache, Action<HTTPRequest, HTTPResponse> callback)
		{
			Uri = uri;
			MethodType = methodType;
			IsKeepAlive = isKeepAlive;
			DisableCache = disableCache;
			Callback = callback;
			StreamFragmentSize = 4096;
			DisableRetry = (methodType == HTTPMethods.Post);
			MaxRedirects = int.MaxValue;
			RedirectCount = 0;
		}

		public void AddField(string fieldName, string value)
		{
			if (FieldsImpl == null)
			{
				FieldsImpl = new WWWForm();
			}
			FieldsImpl.AddField(fieldName, value);
		}

		public void AddBinaryData(string fieldName, byte[] contents)
		{
			if (FieldsImpl == null)
			{
				FieldsImpl = new WWWForm();
			}
			FieldsImpl.AddBinaryData(fieldName, contents);
		}

		public void SetFields(WWWForm wwwForm)
		{
			FieldsImpl = wwwForm;
		}

		public void AddHeader(string name, string value)
		{
			if (Headers == null)
			{
				Headers = new Dictionary<string, List<string>>();
			}
			if (!Headers.TryGetValue(name, out List<string> value2))
			{
				Headers.Add(name, value2 = new List<string>(1));
			}
			value2.Add(value);
		}

		public void SetHeader(string name, string value)
		{
			if (Headers == null)
			{
				Headers = new Dictionary<string, List<string>>();
			}
			if (!Headers.TryGetValue(name, out List<string> value2))
			{
				Headers.Add(name, value2 = new List<string>(1));
			}
			value2.Clear();
			value2.Add(value);
		}

		public bool HasHeader(string name)
		{
			if (Headers != null)
			{
				return Headers.ContainsKey(name);
			}
			return false;
		}

		public string GetFirstHeaderValue(string name)
		{
			if (Headers == null)
			{
				return null;
			}
			List<string> value = null;
			if (Headers.TryGetValue(name, out value) && value.Count > 0)
			{
				return value[0];
			}
			return null;
		}

		public void SetRangeHeader(int firstBytePos)
		{
			SetHeader("Range", $"bytes={firstBytePos}-");
		}

		public void SetRangeHeader(int firstBytePos, int lastBytePos)
		{
			SetHeader("Range", $"bytes={firstBytePos}-{lastBytePos}");
		}

		private void SendHeaders(BinaryWriter stream)
		{
			SetHeader("Host", CurrentUri.Host);
			if (IsRedirected && !HasHeader("Referer"))
			{
				AddHeader("Referer", Uri.ToString());
			}
			if (!HasHeader("Accept-Encoding"))
			{
				AddHeader("Accept-Encoding", "gzip, deflate, identity");
			}
			if (!HasHeader("Connection"))
			{
				AddHeader("Connection", IsKeepAlive ? "Keep-Alive, TE" : "Close, TE");
			}
			if (!HasHeader("TE"))
			{
				AddHeader("TE", "chunked, identity");
			}
			byte[] entityBody = GetEntityBody();
			int num = (entityBody != null) ? entityBody.Length : 0;
			if (RawData == null)
			{
				byte[] array = (FieldsImpl != null) ? FieldsImpl.data : null;
				if (array != null && array.Length != 0 && !HasHeader("Content-Type"))
				{
					AddHeader("Content-Type", "application/x-www-form-urlencoded");
				}
			}
			if (!HasHeader("Content-Length") && num != 0)
			{
				AddHeader("Content-Length", num.ToString());
			}
			if (Credentials != null)
			{
				switch (Credentials.Type)
				{
				case AuthenticationTypes.Basic:
					SetHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(Credentials.UserName + ":" + Credentials.Password)));
					break;
				case AuthenticationTypes.Unknown:
				case AuthenticationTypes.Digest:
				{
					Digest digest = DigestStore.Get(CurrentUri);
					if (digest != null)
					{
						string value = digest.GenerateResponseHeader(this);
						if (!string.IsNullOrEmpty(value))
						{
							SetHeader("Authorization", value);
						}
					}
					break;
				}
				}
			}
			foreach (KeyValuePair<string, List<string>> header in Headers)
			{
				byte[] aSCIIBytes = (header.Key + ": ").GetASCIIBytes();
				for (int i = 0; i < header.Value.Count; i++)
				{
					stream.Write(aSCIIBytes);
					stream.Write(header.Value[i].GetASCIIBytes());
					stream.Write(EOL);
				}
			}
		}

		public string DumpHeaders()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter stream = new BinaryWriter(memoryStream))
				{
					SendHeaders(stream);
					return memoryStream.ToArray().AsciiToString();
				}
			}
		}

		internal byte[] GetEntityBody()
		{
			if (RawData != null)
			{
				return RawData;
			}
			if (FieldsImpl == null)
			{
				return null;
			}
			return FieldsImpl.data;
		}

		internal bool SendOutTo(Stream stream)
		{
			bool result = false;
			try
			{
				BinaryWriter binaryWriter = new BinaryWriter(stream);
				binaryWriter.Write($"{MethodType.ToString().ToUpper()} {CurrentUri.PathAndQuery} HTTP/1.1".GetASCIIBytes());
				binaryWriter.Write(EOL);
				SendHeaders(binaryWriter);
				binaryWriter.Write(EOL);
				byte[] array = (RawData != null) ? RawData : ((FieldsImpl != null) ? FieldsImpl.data : null);
				if (array != null && array.Length != 0)
				{
					binaryWriter.Write(array, 0, array.Length);
				}
				result = true;
				return result;
			}
			catch
			{
				return result;
			}
		}

		internal void UpgradeCallback()
		{
			if (Response != null && Response.IsUpgraded)
			{
				try
				{
					if (OnUpgraded != null)
					{
						OnUpgraded(this, Response);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError($"{ex.Message}: {ex.StackTrace}");
				}
			}
		}

		internal void CallCallback()
		{
			try
			{
				if (Callback != null)
				{
					Callback(this, Response);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError($"{ex.Message}: {ex.StackTrace}");
			}
		}

		internal void FinishStreaming()
		{
			if (Response != null && UseStreaming)
			{
				Response.FinishStreaming();
			}
		}

		public void Send()
		{
			HTTPManager.SendRequest(this);
		}
	}
}
