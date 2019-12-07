using BestHTTP.Authentication;
using BestHTTP.Caching;
using Org.BouncyCastle.Crypto.Tls;
using SocketEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace BestHTTP
{
	internal sealed class HTTPConnection : IDisposable
	{
		private enum RetryCauses
		{
			None,
			Reconnect,
			Authenticate
		}

		private TcpClient Client;

		private Stream Stream;

		private DateTime LastProcessTime;

		internal string ServerAddress
		{
			get;
			private set;
		}

		internal HTTPConnectionStates State
		{
			get;
			private set;
		}

		internal bool IsFree => State == HTTPConnectionStates.Free;

		internal HTTPRequest CurrentRequest
		{
			get;
			private set;
		}

		internal bool IsRemovable => DateTime.UtcNow - LastProcessTime > HTTPManager.MaxConnectionIdleTime;

		internal HTTPConnection(string serverAddress)
		{
			ServerAddress = serverAddress;
			State = HTTPConnectionStates.Initial;
			LastProcessTime = DateTime.UtcNow;
		}

		internal void Process(HTTPRequest request)
		{
			if (State == HTTPConnectionStates.Processing)
			{
				throw new Exception("Connection already processing a request!");
			}
			State = HTTPConnectionStates.Processing;
			CurrentRequest = request;
			ThreadPool.QueueUserWorkItem(ThreadFunc);
		}

		internal void Recycle()
		{
			State = HTTPConnectionStates.Free;
			CurrentRequest = null;
		}

		private void ThreadFunc(object param)
		{
			bool flag = false;
			bool flag2 = false;
			RetryCauses retryCauses = RetryCauses.None;
			object obj = null;
			try
			{
				if (!CurrentRequest.DisableCache)
				{
					Monitor.Enter(obj = HTTPCacheFileLock.Acquire(CurrentRequest.CurrentUri));
				}
				CurrentRequest.Processing = true;
				if (!TryLoadAllFromCache())
				{
					if (Client != null && !Client.IsConnected())
					{
						Close();
					}
					do
					{
						if (retryCauses == RetryCauses.Reconnect)
						{
							Close();
							Thread.Sleep(100);
						}
						retryCauses = RetryCauses.None;
						Connect();
						if (!CurrentRequest.DisableCache)
						{
							HTTPCacheService.SetHeaders(CurrentRequest);
						}
						bool num = CurrentRequest.SendOutTo(Stream);
						if (!num)
						{
							Close();
							if (!flag)
							{
								flag = true;
								retryCauses = RetryCauses.Reconnect;
							}
						}
						if (num)
						{
							if (!Receive() && !flag)
							{
								flag = true;
								retryCauses = RetryCauses.Reconnect;
							}
							if (CurrentRequest.Response != null)
							{
								switch (CurrentRequest.Response.StatusCode)
								{
								case 401:
								{
									string firstHeaderValue2 = CurrentRequest.Response.GetFirstHeaderValue("www-authenticate");
									if (!string.IsNullOrEmpty(firstHeaderValue2))
									{
										Digest orCreate = DigestStore.GetOrCreate(CurrentRequest.CurrentUri);
										orCreate.ParseChallange(firstHeaderValue2);
										if (CurrentRequest.Credentials != null && orCreate.IsUriProtected(CurrentRequest.CurrentUri) && (!CurrentRequest.HasHeader("Authorization") || orCreate.Stale))
										{
											retryCauses = RetryCauses.Authenticate;
										}
									}
									break;
								}
								case 301:
								case 302:
								case 307:
									if (CurrentRequest.RedirectCount < CurrentRequest.MaxRedirects)
									{
										CurrentRequest.RedirectCount++;
										string firstHeaderValue = CurrentRequest.Response.GetFirstHeaderValue("location");
										if (string.IsNullOrEmpty(firstHeaderValue))
										{
											throw new MissingFieldException($"Got redirect status({CurrentRequest.Response.StatusCode.ToString()}) without 'location' header!");
										}
										CurrentRequest.RedirectUri = GetRedirectUri(firstHeaderValue);
										CurrentRequest.Response = null;
										bool flag4 = CurrentRequest.IsRedirected = true;
										flag2 = flag4;
									}
									break;
								}
								TryStoreInCache();
								if (CurrentRequest.Response.HasHeaderWithValue("connection", "close") || CurrentRequest.UseAlternateSSL)
								{
									Close();
								}
							}
						}
					}
					while (retryCauses != 0);
				}
			}
			catch (Exception exception)
			{
				if (CurrentRequest.UseStreaming)
				{
					HTTPCacheService.DeleteEntity(CurrentRequest.CurrentUri);
				}
				CurrentRequest.Response = null;
				CurrentRequest.Exception = exception;
				Close();
			}
			finally
			{
				if (!CurrentRequest.DisableCache && obj != null)
				{
					Monitor.Exit(obj);
				}
				HTTPCacheService.SaveLibrary();
				CurrentRequest.Processing = false;
				if (CurrentRequest != null && CurrentRequest.Response != null && CurrentRequest.Response.IsUpgraded)
				{
					State = HTTPConnectionStates.Upgraded;
				}
				else
				{
					State = (flag2 ? HTTPConnectionStates.Redirected : ((Client == null) ? HTTPConnectionStates.Closed : HTTPConnectionStates.WaitForRecycle));
				}
				LastProcessTime = DateTime.UtcNow;
			}
		}

		private void Connect()
		{
			Uri currentUri = CurrentRequest.CurrentUri;
			if (Client == null)
			{
				Client = new TcpClient();
			}
			if (!Client.Connected)
			{
				Client.Connect(currentUri.Host, currentUri.Port);
			}
			if (Stream != null)
			{
				return;
			}
			if (HTTPProtocolFactory.IsSecureProtocol(CurrentRequest.Uri))
			{
				if (CurrentRequest.UseAlternateSSL)
				{
					TlsProtocolHandler tlsProtocolHandler = new TlsProtocolHandler(Client.GetStream());
					tlsProtocolHandler.Connect(new LegacyTlsClient(new AlwaysValidVerifyer()));
					Stream = tlsProtocolHandler.Stream;
					return;
				}
				SslStream sslStream = new SslStream(Client.GetStream(), leaveInnerStreamOpen: false, (object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors) => true);
				if (!sslStream.IsAuthenticated)
				{
					sslStream.AuthenticateAsClient(currentUri.Host);
				}
				Stream = sslStream;
			}
			else
			{
				Stream = Client.GetStream();
			}
		}

		private bool Receive()
		{
			CurrentRequest.Response = HTTPProtocolFactory.Get(HTTPProtocolFactory.GetProtocolFromUri(CurrentRequest.CurrentUri), CurrentRequest, Stream, CurrentRequest.UseStreaming, isFromCache: false);
			if (!CurrentRequest.Response.Receive())
			{
				CurrentRequest.Response = null;
				return false;
			}
			if (CurrentRequest.Response.StatusCode == 304)
			{
				int length;
				using (Stream stream = HTTPCacheService.GetBody(CurrentRequest.CurrentUri, out length))
				{
					if (!CurrentRequest.Response.HasHeader("content-length"))
					{
						CurrentRequest.Response.Headers.Add("content-length", new List<string>
						{
							length.ToString()
						});
					}
					CurrentRequest.Response.ReadRaw(stream, length);
				}
			}
			return true;
		}

		private bool TryLoadAllFromCache()
		{
			if (CurrentRequest.DisableCache)
			{
				return false;
			}
			try
			{
				if (HTTPCacheService.IsCachedEntityExpiresInTheFuture(CurrentRequest))
				{
					CurrentRequest.Response = HTTPCacheService.GetFullResponse(CurrentRequest);
					if (CurrentRequest.Response != null)
					{
						return true;
					}
				}
			}
			catch
			{
				HTTPCacheService.DeleteEntity(CurrentRequest.CurrentUri);
			}
			return false;
		}

		private void TryStoreInCache()
		{
			if (!CurrentRequest.UseStreaming && !CurrentRequest.DisableCache && CurrentRequest.Response != null && HTTPCacheService.IsCacheble(CurrentRequest.CurrentUri, CurrentRequest.MethodType, CurrentRequest.Response))
			{
				HTTPCacheService.Store(CurrentRequest.CurrentUri, CurrentRequest.MethodType, CurrentRequest.Response);
			}
		}

		private Uri GetRedirectUri(string location)
		{
			Uri uri = null;
			try
			{
				return new Uri(location);
			}
			catch (UriFormatException)
			{
				Uri uri2 = CurrentRequest.Uri;
				return new UriBuilder(uri2.Scheme, uri2.Host, uri2.Port, location).Uri;
			}
		}

		internal void HandleCallback()
		{
			if (State == HTTPConnectionStates.Upgraded)
			{
				if (CurrentRequest != null && CurrentRequest.Response != null && CurrentRequest.Response.IsUpgraded)
				{
					CurrentRequest.UpgradeCallback();
				}
				State = HTTPConnectionStates.WaitForProtocolShutdown;
			}
			else
			{
				CurrentRequest.CallCallback();
			}
		}

		private void Close()
		{
			if (Client != null)
			{
				try
				{
					Client.Close();
				}
				catch
				{
				}
				finally
				{
					Stream = null;
					Client = null;
				}
			}
		}

		public void Dispose()
		{
			Close();
		}
	}
}
