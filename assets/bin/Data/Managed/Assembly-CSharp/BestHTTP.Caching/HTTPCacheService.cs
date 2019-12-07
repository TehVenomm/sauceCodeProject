using BestHTTP.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

namespace BestHTTP.Caching
{
	public static class HTTPCacheService
	{
		private const int LibraryVersion = 1;

		private static Dictionary<Uri, HTTPCacheFileInfo> library;

		private static bool InClearThread;

		private static bool InMaintainenceThread;

		private static Dictionary<Uri, HTTPCacheFileInfo> Library
		{
			get
			{
				LoadLibrary();
				return library;
			}
		}

		internal static string CacheFolder
		{
			get;
			set;
		}

		private static string LibraryPath
		{
			get;
			set;
		}

		private static string GetFileNameFromUri(Uri uri)
		{
			return Convert.ToBase64String(uri.ToString().GetASCIIBytes()).Replace('/', '-');
		}

		private static Uri GetUriFromFileName(string fileName)
		{
			return new Uri(Convert.FromBase64String(fileName.Replace('-', '/')).AsciiToString());
		}

		private static void CheckSetup()
		{
			try
			{
				SetupCacheFolder();
				LoadLibrary();
			}
			catch
			{
			}
		}

		internal static void SetupCacheFolder()
		{
			if (string.IsNullOrEmpty(CacheFolder))
			{
				CacheFolder = Path.Combine(Application.persistentDataPath, "HTTPCache");
				if (!Directory.Exists(CacheFolder))
				{
					Directory.CreateDirectory(CacheFolder);
				}
				LibraryPath = Path.Combine(Application.persistentDataPath, "Library");
			}
		}

		internal static bool HasEntity(Uri uri)
		{
			lock (Library)
			{
				return Library.ContainsKey(uri);
			}
		}

		internal static void DeleteEntity(Uri uri)
		{
			object obj = HTTPCacheFileLock.Acquire(uri);
			if (Monitor.TryEnter(obj, TimeSpan.FromSeconds(0.5)))
			{
				try
				{
					lock (Library)
					{
						HTTPCacheFileInfo value;
						bool num = Library.TryGetValue(uri, out value);
						if (num)
						{
							value.Delete();
						}
						if (num)
						{
							Library.Remove(uri);
						}
					}
				}
				finally
				{
					Monitor.Exit(obj);
				}
			}
		}

		internal static bool IsCachedEntityExpiresInTheFuture(HTTPRequest request)
		{
			lock (Library)
			{
				if (Library.TryGetValue(request.CurrentUri, out HTTPCacheFileInfo value))
				{
					return value.WillExpireInTheFuture();
				}
			}
			return false;
		}

		internal static void SetHeaders(HTTPRequest request)
		{
			lock (Library)
			{
				if (Library.TryGetValue(request.CurrentUri, out HTTPCacheFileInfo value))
				{
					value.SetUpRevalidationHeaders(request);
				}
			}
		}

		internal static Stream GetBody(Uri uri, out int length)
		{
			length = 0;
			lock (Library)
			{
				if (Library.TryGetValue(uri, out HTTPCacheFileInfo value))
				{
					return value.GetBodyStream(out length);
				}
			}
			return null;
		}

		internal static HTTPResponse GetFullResponse(HTTPRequest request)
		{
			lock (Library)
			{
				if (Library.TryGetValue(request.CurrentUri, out HTTPCacheFileInfo value))
				{
					return value.ReadResponseTo(request);
				}
			}
			return null;
		}

		internal static bool IsCacheble(Uri uri, HTTPMethods method, HTTPResponse response)
		{
			if (method != 0)
			{
				return false;
			}
			if (response == null)
			{
				return false;
			}
			if (response.StatusCode == 304)
			{
				return false;
			}
			if (response.StatusCode < 200 || response.StatusCode >= 400)
			{
				return false;
			}
			List<string> headerValues = response.GetHeaderValues("cache-control");
			if (headerValues != null && headerValues[0].ToLower().Contains("no-store"))
			{
				return false;
			}
			List<string> headerValues2 = response.GetHeaderValues("pragma");
			if (headerValues2 != null && headerValues2[0].ToLower().Contains("no-cache"))
			{
				return false;
			}
			if (response.GetHeaderValues("content-range") != null)
			{
				return false;
			}
			return true;
		}

		internal static void Store(Uri uri, HTTPMethods method, HTTPResponse response)
		{
			if (response != null && response.Data != null && response.Data.Length != 0)
			{
				lock (Library)
				{
					if (!Library.TryGetValue(uri, out HTTPCacheFileInfo value))
					{
						Library.Add(uri, value = new HTTPCacheFileInfo(uri));
					}
					try
					{
						value.Store(response);
					}
					catch (Exception ex)
					{
						DeleteEntity(uri);
						throw ex;
					}
				}
			}
		}

		internal static Stream PrepareStreamed(Uri uri, HTTPResponse response)
		{
			lock (Library)
			{
				if (!Library.TryGetValue(uri, out HTTPCacheFileInfo value))
				{
					Library.Add(uri, value = new HTTPCacheFileInfo(uri));
				}
				try
				{
					return value.GetSaveStream(response);
				}
				catch (Exception ex)
				{
					DeleteEntity(uri);
					throw ex;
				}
			}
		}

		public static void BeginClear()
		{
			if (!InClearThread)
			{
				InClearThread = true;
				SetupCacheFolder();
				ThreadPool.QueueUserWorkItem(delegate
				{
					try
					{
						string[] files = Directory.GetFiles(CacheFolder);
						for (int i = 0; i < files.Length; i++)
						{
							try
							{
								DeleteEntity(GetUriFromFileName(Path.GetFileName(files[i])));
							}
							catch
							{
							}
						}
					}
					finally
					{
						SaveLibrary();
						InClearThread = false;
					}
				});
			}
		}

		public static void BeginMaintainence(HTTPCacheMaintananceParams maintananceParams)
		{
			if (maintananceParams == null)
			{
				throw new ArgumentNullException("maintananceParams == null");
			}
			if (!InMaintainenceThread)
			{
				InMaintainenceThread = true;
				SetupCacheFolder();
				ThreadPool.QueueUserWorkItem(delegate(object maintananceParam)
				{
					HTTPCacheMaintananceParams hTTPCacheMaintananceParams = maintananceParam as HTTPCacheMaintananceParams;
					try
					{
						lock (Library)
						{
							DateTime t = DateTime.UtcNow - hTTPCacheMaintananceParams.DeleteOlder;
							foreach (KeyValuePair<Uri, HTTPCacheFileInfo> item in Library)
							{
								if (item.Value.LastAccess < t)
								{
									DeleteEntity(item.Key);
								}
							}
							ulong num = GetCacheSize();
							if (num > hTTPCacheMaintananceParams.MaxCacheSize)
							{
								List<HTTPCacheFileInfo> list = new List<HTTPCacheFileInfo>(library.Count);
								foreach (KeyValuePair<Uri, HTTPCacheFileInfo> item2 in library)
								{
									list.Add(item2.Value);
								}
								list.Sort();
								int num2 = 0;
								while (num >= hTTPCacheMaintananceParams.MaxCacheSize && num2 < list.Count)
								{
									try
									{
										HTTPCacheFileInfo hTTPCacheFileInfo = list[num2];
										ulong num3 = (ulong)hTTPCacheFileInfo.BodyLength;
										DeleteEntity(hTTPCacheFileInfo.Uri);
										num -= num3;
									}
									catch
									{
									}
									finally
									{
										num2++;
									}
								}
							}
						}
					}
					finally
					{
						SaveLibrary();
						InMaintainenceThread = false;
					}
				}, maintananceParams);
			}
		}

		public static int GetCacheEntityCount()
		{
			CheckSetup();
			lock (Library)
			{
				return Library.Count;
			}
		}

		public static ulong GetCacheSize()
		{
			CheckSetup();
			ulong num = 0uL;
			lock (Library)
			{
				foreach (KeyValuePair<Uri, HTTPCacheFileInfo> item in Library)
				{
					if (item.Value.BodyLength > 0)
					{
						num = (ulong)((long)num + (long)item.Value.BodyLength);
					}
				}
				return num;
			}
		}

		private static void LoadLibrary()
		{
			if (library == null)
			{
				library = new Dictionary<Uri, HTTPCacheFileInfo>();
				if (!File.Exists(LibraryPath))
				{
					DeleteUnusedFiles();
				}
				else
				{
					try
					{
						lock (Library)
						{
							using (FileStream input = new FileStream(LibraryPath, FileMode.Open))
							{
								using (BinaryReader binaryReader = new BinaryReader(input))
								{
									int version = binaryReader.ReadInt32();
									int num = binaryReader.ReadInt32();
									for (int i = 0; i < num; i++)
									{
										Uri uri = new Uri(binaryReader.ReadString());
										if (File.Exists(Path.Combine(CacheFolder, GetFileNameFromUri(uri))))
										{
											Library.Add(uri, new HTTPCacheFileInfo(uri, binaryReader, version));
										}
									}
								}
							}
						}
						DeleteUnusedFiles();
					}
					catch
					{
					}
				}
			}
		}

		internal static void SaveLibrary()
		{
			try
			{
				lock (Library)
				{
					using (FileStream output = new FileStream(LibraryPath, FileMode.Create))
					{
						using (BinaryWriter binaryWriter = new BinaryWriter(output))
						{
							binaryWriter.Write(1);
							binaryWriter.Write(Library.Count);
							foreach (KeyValuePair<Uri, HTTPCacheFileInfo> item in Library)
							{
								binaryWriter.Write(item.Key.ToString());
								item.Value.SaveTo(binaryWriter);
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		internal static void SetBodyLength(Uri uri, int bodyLength)
		{
			lock (Library)
			{
				if (Library.TryGetValue(uri, out HTTPCacheFileInfo value))
				{
					value.BodyLength = bodyLength;
				}
				else
				{
					Library.Add(uri, value = new HTTPCacheFileInfo(uri, DateTime.UtcNow, bodyLength));
				}
			}
		}

		private static void DeleteUnusedFiles()
		{
			CheckSetup();
			string[] files = Directory.GetFiles(CacheFolder);
			for (int i = 0; i < files.Length; i++)
			{
				try
				{
					Uri uriFromFileName = GetUriFromFileName(Path.GetFileName(files[i]));
					lock (Library)
					{
						if (!Library.ContainsKey(uriFromFileName))
						{
							DeleteEntity(uriFromFileName);
						}
					}
				}
				catch
				{
				}
			}
		}
	}
}
