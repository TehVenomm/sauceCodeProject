using BestHTTP.Caching;
using BestHTTP.Decompression.Zlib;
using BestHTTP.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;

namespace BestHTTP
{
	public class HTTPResponse : IDisposable
	{
		internal const byte CR = 13;

		internal const byte LF = 10;

		internal const int BufferSize = 4096;

		protected string dataAsText;

		protected Texture2D texture;

		protected HTTPRequest baseRequest;

		protected Stream Stream;

		protected List<byte[]> streamedFragments;

		protected object SyncRoot = new object();

		protected byte[] fragmentBuffer;

		protected int fragmentBufferDataLength;

		protected Stream cacheStream;

		protected int allFragmentSize;

		public int VersionMajor
		{
			get;
			protected set;
		}

		public int VersionMinor
		{
			get;
			protected set;
		}

		public int StatusCode
		{
			get;
			protected set;
		}

		public string Message
		{
			get;
			protected set;
		}

		public bool IsStreamed
		{
			get;
			protected set;
		}

		public bool IsStreamingFinished
		{
			get;
			internal set;
		}

		public bool IsFromCache
		{
			get;
			protected set;
		}

		public Dictionary<string, List<string>> Headers
		{
			get;
			protected set;
		}

		public byte[] Data
		{
			get;
			internal set;
		}

		public bool IsUpgraded
		{
			get;
			protected set;
		}

		public string DataAsText
		{
			get
			{
				if (Data == null)
				{
					return string.Empty;
				}
				if (!string.IsNullOrEmpty(dataAsText))
				{
					return dataAsText;
				}
				return dataAsText = Encoding.UTF8.GetString(Data, 0, Data.Length);
			}
		}

		public Texture2D DataAsTexture2D
		{
			get
			{
				if (Data == null)
				{
					return null;
				}
				if (texture != null)
				{
					return texture;
				}
				texture = new Texture2D(0, 0);
				texture.LoadRawTextureData(Data);
				return texture;
			}
		}

		internal HTTPResponse(HTTPRequest request, Stream stream, bool isStreamed, bool isFromCache)
		{
			baseRequest = request;
			Stream = stream;
			IsStreamed = isStreamed;
			IsFromCache = isFromCache;
		}

		internal virtual bool Receive(int forceReadRawContentLength = -1)
		{
			string empty = string.Empty;
			try
			{
				empty = ReadTo(Stream, 32);
			}
			catch (Exception ex)
			{
				if (baseRequest.DisableRetry)
				{
					throw ex;
				}
				return false;
			}
			if (!baseRequest.DisableRetry && string.IsNullOrEmpty(empty))
			{
				return false;
			}
			string[] array = empty.Split('/', '.');
			VersionMajor = int.Parse(array[1]);
			VersionMinor = int.Parse(array[2]);
			string s = ReadTo(Stream, 32);
			int result;
			if (baseRequest.DisableRetry)
			{
				result = int.Parse(s);
			}
			else if (!int.TryParse(s, out result))
			{
				return false;
			}
			StatusCode = result;
			Message = ReadTo(Stream, 10);
			ReadHeaders(Stream);
			IsUpgraded = (StatusCode == 101 && HasHeaderWithValue("connection", "upgrade"));
			if (forceReadRawContentLength != -1)
			{
				IsFromCache = true;
				ReadRaw(Stream, forceReadRawContentLength);
				return true;
			}
			if ((StatusCode >= 100 && StatusCode < 200) || StatusCode == 204 || StatusCode == 304 || baseRequest.MethodType == HTTPMethods.Head)
			{
				return true;
			}
			if (HasHeaderWithValue("transfer-encoding", "chunked"))
			{
				ReadChunked(Stream);
			}
			else
			{
				List<string> headerValues = GetHeaderValues("content-length");
				List<string> headerValues2 = GetHeaderValues("content-range");
				if (headerValues != null && headerValues2 == null)
				{
					ReadRaw(Stream, int.Parse(headerValues[0]));
				}
				else if (headerValues2 != null)
				{
					HTTPRange range = GetRange();
					ReadRaw(Stream, range.LastBytePos - range.FirstBytePos + 1);
				}
			}
			return true;
		}

		protected void ReadHeaders(Stream stream)
		{
			string text = ReadTo(stream, 58, 10).Trim();
			while (text != string.Empty)
			{
				string value = ReadTo(stream, 10);
				AddHeader(text, value);
				text = ReadTo(stream, 58, 10);
			}
		}

		protected void AddHeader(string name, string value)
		{
			name = name.ToLower();
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

		public List<string> GetHeaderValues(string name)
		{
			name = name.ToLower();
			if (!Headers.TryGetValue(name, out List<string> value) || value.Count == 0)
			{
				return null;
			}
			return value;
		}

		public string GetFirstHeaderValue(string name)
		{
			name = name.ToLower();
			if (!Headers.TryGetValue(name, out List<string> value) || value.Count == 0)
			{
				return null;
			}
			return value[0];
		}

		public bool HasHeaderWithValue(string headerName, string value)
		{
			List<string> headerValues = GetHeaderValues(headerName);
			if (headerValues == null)
			{
				return false;
			}
			for (int i = 0; i < headerValues.Count; i++)
			{
				if (string.Compare(headerValues[i], value, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		public bool HasHeader(string headerName)
		{
			if (GetHeaderValues(headerName) == null)
			{
				return false;
			}
			return true;
		}

		public HTTPRange GetRange()
		{
			string[] array = (GetHeaderValues("content-range") ?? throw null)[0].Split(new char[3]
			{
				' ',
				'-',
				'/'
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array[1] == "*")
			{
				return new HTTPRange(int.Parse(array[2]));
			}
			return new HTTPRange(int.Parse(array[1]), int.Parse(array[2]), (array[3] != "*") ? int.Parse(array[3]) : (-1));
		}

		protected string ReadTo(Stream stream, byte blocker)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = stream.ReadByte();
				while (num != blocker && num != -1)
				{
					memoryStream.WriteByte((byte)num);
					num = stream.ReadByte();
				}
				return memoryStream.ToArray().AsciiToString().Trim();
			}
		}

		protected string ReadTo(Stream stream, byte blocker1, byte blocker2)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = stream.ReadByte();
				while (num != blocker1 && num != blocker2 && num != -1)
				{
					memoryStream.WriteByte((byte)num);
					num = stream.ReadByte();
				}
				return memoryStream.ToArray().AsciiToString().Trim();
			}
		}

		protected int ReadChunkLength(Stream stream)
		{
			return int.Parse(ReadTo(stream, 10).Split(';')[0], NumberStyles.AllowHexSpecifier);
		}

		protected void ReadChunked(Stream stream)
		{
			BeginReceiveStreamFragments();
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = ReadChunkLength(stream);
				byte[] array = new byte[num];
				int num2 = 0;
				while (num != 0)
				{
					if (array.Length < num)
					{
						Array.Resize(ref array, num);
					}
					int num3 = 0;
					WaitWhileHasFragments();
					do
					{
						num3 += stream.Read(array, num3, num - num3);
					}
					while (num3 < num);
					if (baseRequest.UseStreaming)
					{
						FeedStreamFragment(array, 0, num3);
					}
					else
					{
						memoryStream.Write(array, 0, num3);
					}
					ReadTo(stream, 10);
					num2 += num3;
					num = ReadChunkLength(stream);
				}
				if (baseRequest.UseStreaming)
				{
					FlushRemainingFragmentBuffer();
				}
				ReadHeaders(stream);
				if (!baseRequest.UseStreaming)
				{
					Data = DecodeStream(memoryStream);
				}
			}
		}

		internal void ReadRaw(Stream stream, int contentLength)
		{
			BeginReceiveStreamFragments();
			using (MemoryStream memoryStream = new MemoryStream((!baseRequest.UseStreaming) ? contentLength : 0))
			{
				byte[] array = new byte[Math.Min(baseRequest.StreamFragmentSize, 4096)];
				int num = 0;
				while (contentLength > 0)
				{
					num = 0;
					WaitWhileHasFragments();
					do
					{
						int num2 = stream.Read(array, num, Math.Min(contentLength, array.Length - num));
						num += num2;
						contentLength -= num2;
					}
					while (num < array.Length && contentLength > 0);
					if (baseRequest.UseStreaming)
					{
						FeedStreamFragment(array, 0, num);
					}
					else
					{
						memoryStream.Write(array, 0, num);
					}
				}
				if (baseRequest.UseStreaming)
				{
					FlushRemainingFragmentBuffer();
				}
				if (!baseRequest.UseStreaming)
				{
					Data = DecodeStream(memoryStream);
				}
			}
		}

		protected byte[] DecodeStream(Stream streamToDecode)
		{
			streamToDecode.Seek(0L, SeekOrigin.Begin);
			List<string> list = IsFromCache ? null : GetHeaderValues("content-encoding");
			Stream stream = null;
			if (list == null)
			{
				stream = streamToDecode;
			}
			else
			{
				switch (list[0])
				{
				case "identity":
					stream = streamToDecode;
					break;
				case "gzip":
					stream = new GZipStream(streamToDecode, CompressionMode.Decompress);
					break;
				case "deflate":
					stream = new DeflateStream(streamToDecode, CompressionMode.Decompress);
					break;
				default:
					throw new NotSupportedException("Not supported encoding found: " + list[0]);
				}
			}
			using (MemoryStream memoryStream = new MemoryStream((int)streamToDecode.Length))
			{
				byte[] array = new byte[1024];
				int num = 0;
				while ((num = stream.Read(array, 0, array.Length)) > 0)
				{
					memoryStream.Write(array, 0, num);
				}
				return memoryStream.ToArray();
			}
		}

		protected void BeginReceiveStreamFragments()
		{
			if (!baseRequest.DisableCache && baseRequest.UseStreaming && !IsFromCache && HTTPCacheService.IsCacheble(baseRequest.CurrentUri, baseRequest.MethodType, this))
			{
				cacheStream = HTTPCacheService.PrepareStreamed(baseRequest.CurrentUri, this);
			}
			allFragmentSize = 0;
		}

		protected void FeedStreamFragment(byte[] buffer, int pos, int length)
		{
			if (fragmentBuffer == null)
			{
				fragmentBuffer = new byte[baseRequest.StreamFragmentSize];
				fragmentBufferDataLength = 0;
			}
			if (fragmentBufferDataLength + length <= baseRequest.StreamFragmentSize)
			{
				Array.Copy(buffer, pos, fragmentBuffer, fragmentBufferDataLength, length);
				fragmentBufferDataLength += length;
				if (fragmentBufferDataLength == baseRequest.StreamFragmentSize)
				{
					AddStreamedFragment(fragmentBuffer);
					fragmentBuffer = null;
					fragmentBufferDataLength = 0;
				}
			}
			else
			{
				int num = baseRequest.StreamFragmentSize - fragmentBufferDataLength;
				FeedStreamFragment(buffer, pos, num);
				FeedStreamFragment(buffer, pos + num, length - num);
			}
		}

		protected void FlushRemainingFragmentBuffer()
		{
			if (fragmentBuffer != null)
			{
				Array.Resize(ref fragmentBuffer, fragmentBufferDataLength);
				AddStreamedFragment(fragmentBuffer);
				fragmentBuffer = null;
				fragmentBufferDataLength = 0;
			}
			if (cacheStream != null)
			{
				cacheStream.Close();
				cacheStream = null;
				HTTPCacheService.SetBodyLength(baseRequest.CurrentUri, allFragmentSize);
			}
		}

		protected void AddStreamedFragment(byte[] buffer)
		{
			lock (SyncRoot)
			{
				if (streamedFragments == null)
				{
					streamedFragments = new List<byte[]>();
				}
				streamedFragments.Add(buffer);
				if (cacheStream != null)
				{
					cacheStream.Write(buffer, 0, buffer.Length);
					allFragmentSize += buffer.Length;
				}
			}
		}

		protected void WaitWhileHasFragments()
		{
		}

		public List<byte[]> GetStreamedFragments()
		{
			lock (SyncRoot)
			{
				if (streamedFragments == null || streamedFragments.Count == 0)
				{
					return null;
				}
				List<byte[]> result = new List<byte[]>(streamedFragments);
				streamedFragments.Clear();
				return result;
			}
		}

		internal bool HasStreamedFragments()
		{
			lock (SyncRoot)
			{
				return streamedFragments != null && streamedFragments.Count > 0;
			}
		}

		internal void FinishStreaming()
		{
			IsStreamingFinished = true;
			Dispose();
		}

		public void Dispose()
		{
			if (cacheStream != null)
			{
				cacheStream.Close();
				cacheStream = null;
			}
		}
	}
}
