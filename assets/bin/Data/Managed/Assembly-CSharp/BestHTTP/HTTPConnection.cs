// Decompiled with JetBrains decompiler
// Type: BestHTTP.HTTPConnection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using BestHTTP.Authentication;
using BestHTTP.Caching;
using Org.BouncyCastle.Crypto.Tls;
using SocketEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Threading;

#nullable disable
namespace BestHTTP;

internal sealed class HTTPConnection : IDisposable
{
  private TcpClient Client;
  private Stream Stream;
  private DateTime LastProcessTime;

  internal string ServerAddress { get; private set; }

  internal HTTPConnectionStates State { get; private set; }

  internal bool IsFree => this.State == HTTPConnectionStates.Free;

  internal HTTPRequest CurrentRequest { get; private set; }

  internal bool IsRemovable
  {
    get => DateTime.UtcNow - this.LastProcessTime > HTTPManager.MaxConnectionIdleTime;
  }

  internal HTTPConnection(string serverAddress)
  {
    this.ServerAddress = serverAddress;
    this.State = HTTPConnectionStates.Initial;
    this.LastProcessTime = DateTime.UtcNow;
  }

  internal void Process(HTTPRequest request)
  {
    this.State = this.State != HTTPConnectionStates.Processing ? HTTPConnectionStates.Processing : throw new Exception("Connection already processing a request!");
    this.CurrentRequest = request;
    ThreadPool.QueueUserWorkItem(new WaitCallback(this.ThreadFunc));
  }

  internal void Recycle()
  {
    this.State = HTTPConnectionStates.Free;
    this.CurrentRequest = (HTTPRequest) null;
  }

  private void ThreadFunc(object param)
  {
    bool flag1 = false;
    bool flag2 = false;
    HTTPConnection.RetryCauses retryCauses = HTTPConnection.RetryCauses.None;
    object obj = (object) null;
    try
    {
      if (!this.CurrentRequest.DisableCache)
        Monitor.Enter(obj = HTTPCacheFileLock.Acquire(this.CurrentRequest.CurrentUri));
      this.CurrentRequest.Processing = true;
      if (this.TryLoadAllFromCache())
        return;
      if (this.Client != null && !this.Client.IsConnected())
        this.Close();
      do
      {
        if (retryCauses == HTTPConnection.RetryCauses.Reconnect)
        {
          this.Close();
          Thread.Sleep(100);
        }
        retryCauses = HTTPConnection.RetryCauses.None;
        this.Connect();
        if (!this.CurrentRequest.DisableCache)
          HTTPCacheService.SetHeaders(this.CurrentRequest);
        int num = this.CurrentRequest.SendOutTo(this.Stream) ? 1 : 0;
        if (num == 0)
        {
          this.Close();
          if (!flag1)
          {
            flag1 = true;
            retryCauses = HTTPConnection.RetryCauses.Reconnect;
          }
        }
        if (num != 0)
        {
          if (!this.Receive() && !flag1)
          {
            flag1 = true;
            retryCauses = HTTPConnection.RetryCauses.Reconnect;
          }
          if (this.CurrentRequest.Response != null)
          {
            switch (this.CurrentRequest.Response.StatusCode)
            {
              case 301:
              case 302:
              case 307:
                if (this.CurrentRequest.RedirectCount < this.CurrentRequest.MaxRedirects)
                {
                  ++this.CurrentRequest.RedirectCount;
                  string firstHeaderValue = this.CurrentRequest.Response.GetFirstHeaderValue("location");
                  this.CurrentRequest.RedirectUri = !string.IsNullOrEmpty(firstHeaderValue) ? this.GetRedirectUri(firstHeaderValue) : throw new MissingFieldException($"Got redirect status({this.CurrentRequest.Response.StatusCode.ToString()}) without 'location' header!");
                  this.CurrentRequest.Response = (HTTPResponse) null;
                  flag2 = this.CurrentRequest.IsRedirected = true;
                  break;
                }
                break;
              case 401:
                string firstHeaderValue1 = this.CurrentRequest.Response.GetFirstHeaderValue("www-authenticate");
                if (!string.IsNullOrEmpty(firstHeaderValue1))
                {
                  Digest digest = DigestStore.GetOrCreate(this.CurrentRequest.CurrentUri);
                  digest.ParseChallange(firstHeaderValue1);
                  if (this.CurrentRequest.Credentials != null && digest.IsUriProtected(this.CurrentRequest.CurrentUri) && (!this.CurrentRequest.HasHeader("Authorization") || digest.Stale))
                  {
                    retryCauses = HTTPConnection.RetryCauses.Authenticate;
                    break;
                  }
                  break;
                }
                break;
            }
            this.TryStoreInCache();
            if (this.CurrentRequest.Response.HasHeaderWithValue("connection", "close") || this.CurrentRequest.UseAlternateSSL)
              this.Close();
          }
        }
      }
      while (retryCauses != HTTPConnection.RetryCauses.None);
    }
    catch (Exception ex)
    {
      if (this.CurrentRequest.UseStreaming)
        HTTPCacheService.DeleteEntity(this.CurrentRequest.CurrentUri);
      this.CurrentRequest.Response = (HTTPResponse) null;
      this.CurrentRequest.Exception = ex;
      this.Close();
    }
    finally
    {
      if (!this.CurrentRequest.DisableCache && obj != null)
        Monitor.Exit(obj);
      HTTPCacheService.SaveLibrary();
      this.CurrentRequest.Processing = false;
      this.State = this.CurrentRequest == null || this.CurrentRequest.Response == null || !this.CurrentRequest.Response.IsUpgraded ? (flag2 ? HTTPConnectionStates.Redirected : (this.Client == null ? HTTPConnectionStates.Closed : HTTPConnectionStates.WaitForRecycle)) : HTTPConnectionStates.Upgraded;
      this.LastProcessTime = DateTime.UtcNow;
    }
  }

  private void Connect()
  {
    Uri currentUri = this.CurrentRequest.CurrentUri;
    if (this.Client == null)
      this.Client = new TcpClient();
    if (!this.Client.Connected)
      this.Client.Connect(currentUri.Host, currentUri.Port);
    if (this.Stream != null)
      return;
    if (HTTPProtocolFactory.IsSecureProtocol(this.CurrentRequest.Uri))
    {
      if (this.CurrentRequest.UseAlternateSSL)
      {
        TlsProtocolHandler tlsProtocolHandler = new TlsProtocolHandler(this.Client.GetStream());
        tlsProtocolHandler.Connect((TlsClient) new LegacyTlsClient((ICertificateVerifyer) new AlwaysValidVerifyer()));
        this.Stream = tlsProtocolHandler.Stream;
      }
      else
      {
        SslStream sslStream = new SslStream(this.Client.GetStream(), false, (RemoteCertificateValidationCallback) ((sender, cert, chain, errors) => true));
        if (!sslStream.IsAuthenticated)
          sslStream.AuthenticateAsClient(currentUri.Host);
        this.Stream = (Stream) sslStream;
      }
    }
    else
      this.Stream = this.Client.GetStream();
  }

  private bool Receive()
  {
    this.CurrentRequest.Response = HTTPProtocolFactory.Get(HTTPProtocolFactory.GetProtocolFromUri(this.CurrentRequest.CurrentUri), this.CurrentRequest, this.Stream, this.CurrentRequest.UseStreaming, false);
    if (!this.CurrentRequest.Response.Receive())
    {
      this.CurrentRequest.Response = (HTTPResponse) null;
      return false;
    }
    if (this.CurrentRequest.Response.StatusCode == 304)
    {
      int length;
      using (Stream body = HTTPCacheService.GetBody(this.CurrentRequest.CurrentUri, out length))
      {
        if (!this.CurrentRequest.Response.HasHeader("content-length"))
          this.CurrentRequest.Response.Headers.Add("content-length", new List<string>()
          {
            length.ToString()
          });
        this.CurrentRequest.Response.ReadRaw(body, length);
      }
    }
    return true;
  }

  private bool TryLoadAllFromCache()
  {
    if (this.CurrentRequest.DisableCache)
      return false;
    try
    {
      if (HTTPCacheService.IsCachedEntityExpiresInTheFuture(this.CurrentRequest))
      {
        this.CurrentRequest.Response = HTTPCacheService.GetFullResponse(this.CurrentRequest);
        if (this.CurrentRequest.Response != null)
          return true;
      }
    }
    catch
    {
      HTTPCacheService.DeleteEntity(this.CurrentRequest.CurrentUri);
    }
    return false;
  }

  private void TryStoreInCache()
  {
    if (this.CurrentRequest.UseStreaming || this.CurrentRequest.DisableCache || this.CurrentRequest.Response == null || !HTTPCacheService.IsCacheble(this.CurrentRequest.CurrentUri, this.CurrentRequest.MethodType, this.CurrentRequest.Response))
      return;
    HTTPCacheService.Store(this.CurrentRequest.CurrentUri, this.CurrentRequest.MethodType, this.CurrentRequest.Response);
  }

  private Uri GetRedirectUri(string location)
  {
    try
    {
      return new Uri(location);
    }
    catch (UriFormatException ex)
    {
      Uri uri = this.CurrentRequest.Uri;
      return new UriBuilder(uri.Scheme, uri.Host, uri.Port, location).Uri;
    }
  }

  internal void HandleCallback()
  {
    if (this.State == HTTPConnectionStates.Upgraded)
    {
      if (this.CurrentRequest != null && this.CurrentRequest.Response != null && this.CurrentRequest.Response.IsUpgraded)
        this.CurrentRequest.UpgradeCallback();
      this.State = HTTPConnectionStates.WaitForProtocolShutdown;
    }
    else
      this.CurrentRequest.CallCallback();
  }

  private void Close()
  {
    if (this.Client == null)
      return;
    try
    {
      this.Client.Close();
    }
    catch
    {
    }
    finally
    {
      this.Stream = (Stream) null;
      this.Client = (TcpClient) null;
    }
  }

  public void Dispose() => this.Close();

  private enum RetryCauses
  {
    None,
    Reconnect,
    Authenticate,
  }
}
