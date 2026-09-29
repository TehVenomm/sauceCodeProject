// Decompiled with JetBrains decompiler
// Type: BestHTTP.HTTPManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using BestHTTP.Caching;
using BestHTTP.WebSocket;
using System;
using System.Collections.Generic;

#nullable disable
namespace BestHTTP;

public static class HTTPManager
{
  private static byte maxConnectionPerServer;
  private static Dictionary<string, List<HTTPConnection>> Connections = new Dictionary<string, List<HTTPConnection>>();
  private static List<HTTPConnection> ActiveConnections = new List<HTTPConnection>();
  private static List<HTTPConnection> RecycledConnections = new List<HTTPConnection>();
  private static List<HTTPRequest> RequestQueue = new List<HTTPRequest>();
  private static bool IsCallingCallbacks;

  static HTTPManager()
  {
    HTTPManager.MaxConnectionPerServer = (byte) 4;
    HTTPManager.KeepAliveDefaultValue = true;
    HTTPManager.MaxPathLength = (int) byte.MaxValue;
    HTTPManager.MaxConnectionIdleTime = TimeSpan.FromMinutes(2.0);
  }

  public static byte MaxConnectionPerServer
  {
    get => HTTPManager.maxConnectionPerServer;
    set
    {
      HTTPManager.maxConnectionPerServer = value > (byte) 0 ? value : throw new ArgumentOutOfRangeException("MaxConnectionPerServer must be greater than 0!");
    }
  }

  public static bool KeepAliveDefaultValue { get; set; }

  public static bool IsCachingDisabled { get; set; }

  public static TimeSpan MaxConnectionIdleTime { get; set; }

  internal static int MaxPathLength { get; set; }

  public static HTTPRequest SendRequest(string url, Action<HTTPRequest, HTTPResponse> callback)
  {
    return HTTPManager.SendRequest(new HTTPRequest(new Uri(url), HTTPMethods.Get, callback));
  }

  public static HTTPRequest SendRequest(
    string url,
    HTTPMethods methodType,
    Action<HTTPRequest, HTTPResponse> callback)
  {
    return HTTPManager.SendRequest(new HTTPRequest(new Uri(url), methodType, callback));
  }

  public static HTTPRequest SendRequest(
    string url,
    HTTPMethods methodType,
    bool isKeepAlive,
    Action<HTTPRequest, HTTPResponse> callback)
  {
    return HTTPManager.SendRequest(new HTTPRequest(new Uri(url), methodType, isKeepAlive, callback));
  }

  public static HTTPRequest SendRequest(
    string url,
    HTTPMethods methodType,
    bool isKeepAlive,
    bool disableCache,
    Action<HTTPRequest, HTTPResponse> callback)
  {
    return HTTPManager.SendRequest(new HTTPRequest(new Uri(url), methodType, isKeepAlive, disableCache, callback));
  }

  public static HTTPRequest SendRequest(HTTPRequest request)
  {
    HTTPUpdateDelegator.CheckInstance();
    if (HTTPManager.IsCallingCallbacks)
      HTTPManager.RequestQueue.Add(request);
    else
      HTTPManager.SendRequestImpl(request);
    return request;
  }

  private static void SendRequestImpl(HTTPRequest request)
  {
    HTTPConnection conn = HTTPManager.FindOrCreateFreeConnection(request.CurrentUri);
    if (conn != null)
    {
      if (HTTPManager.ActiveConnections.Find((Predicate<HTTPConnection>) (c => c == conn)) == null)
        HTTPManager.ActiveConnections.Add(conn);
      conn.Process(request);
    }
    else
      HTTPManager.RequestQueue.Add(request);
  }

  private static HTTPConnection FindOrCreateFreeConnection(Uri uri)
  {
    HTTPConnection createFreeConnection = (HTTPConnection) null;
    string str = new UriBuilder(uri.Scheme, uri.Host, uri.Port).Uri.ToString();
    List<HTTPConnection> httpConnectionList;
    if (HTTPManager.Connections.TryGetValue(str, out httpConnectionList))
    {
      for (int index = 0; index < httpConnectionList.Count && createFreeConnection == null; ++index)
      {
        if (httpConnectionList[index] != null && httpConnectionList[index].IsFree)
          createFreeConnection = httpConnectionList[index];
      }
    }
    else
      HTTPManager.Connections.Add(str, httpConnectionList = new List<HTTPConnection>((int) HTTPManager.MaxConnectionPerServer));
    if (createFreeConnection == null)
    {
      if (httpConnectionList.Count == (int) HTTPManager.MaxConnectionPerServer)
        return (HTTPConnection) null;
      httpConnectionList.Add(createFreeConnection = new HTTPConnection(str));
    }
    return createFreeConnection;
  }

  private static void RecycleConnection(HTTPConnection conn)
  {
    conn.Recycle();
    HTTPManager.RecycledConnections.Add(conn);
  }

  internal static void OnUpdate()
  {
    HTTPManager.IsCallingCallbacks = true;
    try
    {
      for (int index = 0; index < HTTPManager.ActiveConnections.Count; ++index)
      {
        HTTPConnection activeConnection = HTTPManager.ActiveConnections[index];
        switch (activeConnection.State)
        {
          case HTTPConnectionStates.Processing:
            if (activeConnection.CurrentRequest.UseStreaming && activeConnection.CurrentRequest.Response != null && activeConnection.CurrentRequest.Response.HasStreamedFragments())
            {
              activeConnection.HandleCallback();
              break;
            }
            break;
          case HTTPConnectionStates.Redirected:
            HTTPManager.SendRequest(activeConnection.CurrentRequest);
            HTTPManager.RecycleConnection(activeConnection);
            break;
          case HTTPConnectionStates.Upgraded:
            activeConnection.HandleCallback();
            break;
          case HTTPConnectionStates.WaitForProtocolShutdown:
            WebSocketResponse response = activeConnection.CurrentRequest.Response as WebSocketResponse;
            response.HandleEvents();
            if (response.IsClosed)
            {
              activeConnection.HandleCallback();
              activeConnection.Dispose();
              HTTPManager.RecycleConnection(activeConnection);
              break;
            }
            break;
          case HTTPConnectionStates.WaitForRecycle:
            activeConnection.CurrentRequest.FinishStreaming();
            activeConnection.HandleCallback();
            HTTPManager.RecycleConnection(activeConnection);
            break;
          case HTTPConnectionStates.Free:
            if (activeConnection.IsRemovable)
            {
              activeConnection.Dispose();
              HTTPManager.Connections[activeConnection.ServerAddress].Remove(activeConnection);
              break;
            }
            break;
          case HTTPConnectionStates.Closed:
            activeConnection.CurrentRequest.FinishStreaming();
            activeConnection.HandleCallback();
            HTTPManager.RecycleConnection(activeConnection);
            HTTPManager.Connections[activeConnection.ServerAddress].Remove(activeConnection);
            break;
        }
      }
    }
    finally
    {
      HTTPManager.IsCallingCallbacks = false;
    }
    if (HTTPManager.RecycledConnections.Count > 0)
    {
      for (int index = 0; index < HTTPManager.RecycledConnections.Count; ++index)
      {
        if (HTTPManager.RecycledConnections[index].IsFree)
          HTTPManager.ActiveConnections.Remove(HTTPManager.RecycledConnections[index]);
      }
      HTTPManager.RecycledConnections.Clear();
    }
    if (HTTPManager.RequestQueue.Count <= 0)
      return;
    HTTPRequest[] array = HTTPManager.RequestQueue.ToArray();
    HTTPManager.RequestQueue.Clear();
    for (int index = 0; index < array.Length; ++index)
      HTTPManager.SendRequest(array[index]);
  }

  internal static void OnQuit()
  {
    HTTPCacheService.SaveLibrary();
    foreach (KeyValuePair<string, List<HTTPConnection>> connection in HTTPManager.Connections)
    {
      foreach (HTTPConnection httpConnection in connection.Value)
        httpConnection.Dispose();
      connection.Value.Clear();
    }
    HTTPManager.Connections.Clear();
  }
}
