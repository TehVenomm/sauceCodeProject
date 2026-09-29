// Decompiled with JetBrains decompiler
// Type: DataLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

#nullable disable
public class DataLoader : MonoBehaviour
{
  private DataCache cache;
  private List<DataLoadRequest> requestList = new List<DataLoadRequest>();
  private MultiThreadTaskRunner taskRunner;
  private const int DL_MAX = 5;
  private int downloadCount;

  private void Awake() => this.taskRunner = new MultiThreadTaskRunner();

  private void OnDestroy()
  {
    if (this.taskRunner == null)
      return;
    this.taskRunner.DestroyThread();
    this.taskRunner = (MultiThreadTaskRunner) null;
  }

  public bool IsLoading(string name)
  {
    return this.requestList.Find((Predicate<DataLoadRequest>) (o => o.name == name)) != null;
  }

  public bool IsLoading() => 0 < this.requestList.Count;

  public void SetCache(DataCache cache) => this.cache = cache;

  public void Request(DataLoadRequest req)
  {
    this.StartCoroutine(this.Load(req, req.downloadOnly, false));
  }

  public void Request(List<DataLoadRequest> reqs)
  {
    this.taskRunner.CreateThread();
    this.requestList.AddRange((IEnumerable<DataLoadRequest>) reqs);
    reqs.ForEach((Action<DataLoadRequest>) (o => this.StartCoroutine(this.Load(o, o.downloadOnly, true))));
  }

  public void RequestManifest(DataLoadRequest req)
  {
    this.StartCoroutine(this.Load(req, req.downloadOnly, false, true));
  }

  private void RemoveQueue(DataLoadRequest req)
  {
    this.requestList.Remove(req);
    if (0 < this.requestList.Count)
      return;
    this.taskRunner.DestroyThread();
  }

  private IEnumerator Load(
    DataLoadRequest req,
    bool downloadOnly,
    bool useQueue,
    bool forceDownload = false)
  {
    int i = 0;
    for (int len = req.depReqs.Count; i < len; ++i)
    {
      while (!req.depReqs[i].isCompleted)
        yield return (object) null;
    }
    DataTableLoadError error = DataTableLoadError.None;
    byte[] bytes = (byte[]) null;
    if (!this.cache.IsCached(req) | forceDownload)
    {
      IEnumerator download = this.Download(req, (Action<byte[]>) (b => bytes = b), (Action<DataTableLoadError>) (e => error = e));
      while (download.MoveNext())
        yield return download.Current;
      if (error != DataTableLoadError.None)
      {
        if (useQueue)
          this.RemoveQueue(req);
        if (req.enableLoadBinaryData)
        {
          req.enableLoadBinaryData = false;
          this.Request(req);
          yield break;
        }
        req.OnError(error);
        yield break;
      }
      download = (IEnumerator) null;
    }
    if (downloadOnly)
    {
      bool wait = true;
      ThreadPoolWrapper.QueueUserWorkItem((WaitCallback) (obj =>
      {
        try
        {
          if (bytes == null)
            return;
          error = this.Save(req, bytes);
        }
        catch (Exception ex)
        {
          error = DataTableLoadError.FileReadError;
        }
        finally
        {
          wait = false;
        }
      }));
      while (wait)
        yield return (object) null;
      if (error == DataTableLoadError.None)
        req.OnComplete();
      else
        req.OnError(error);
      if (useQueue)
        this.RemoveQueue(req);
    }
    else
    {
      Stopwatch sw = Stopwatch.StartNew();
      IEnumerator loading;
      if (req.enableLoadBinaryData)
      {
        loading = this.LoadCompressedBinary(req, bytes, (Action<DataTableLoadError>) (e => error = e), useQueue);
        while (loading.MoveNext())
          yield return loading.Current;
        if (error != DataTableLoadError.None)
        {
          if (useQueue)
            this.requestList.Remove(req);
          req.enableLoadBinaryData = false;
          this.Request(req);
          yield break;
        }
      }
      else
        loading = this.LoadCompressedTextWithSignature(req, bytes, (Action<DataTableLoadError>) (e => error = e), useQueue);
      while (loading.MoveNext())
        yield return loading.Current;
      sw.Stop();
      if (error != DataTableLoadError.None)
        req.OnError(error);
      else
        req.OnComplete();
      if (useQueue)
        this.RemoveQueue(req);
    }
  }

  private IEnumerator LoadCompressedTextWithSignature(
    DataLoadRequest req,
    byte[] bytes,
    Action<DataTableLoadError> onEnd,
    bool useQueue)
  {
    bool wait = true;
    DataTableLoadError error = DataTableLoadError.None;
    System.Action act = (System.Action) (() =>
    {
      try
      {
        if (bytes != null)
          error = this.Save(req, bytes);
        else
          bytes = this.cache.Load(req);
        if (bytes != null)
        {
          if (this.Verify(req, bytes))
            error = this.ProcessCompressedText(req, bytes);
          else
            error = DataTableLoadError.VerifyError;
        }
        else
          error = DataTableLoadError.FileReadError;
      }
      catch (Exception ex)
      {
        error = DataTableLoadError.FileReadError;
      }
      finally
      {
        wait = false;
      }
    });
    if (useQueue)
      this.taskRunner.Add(req.name, act);
    else
      ThreadPoolWrapper.QueueUserWorkItem((WaitCallback) (obj => act()));
    while (wait)
      yield return (object) null;
    onEnd(error);
  }

  private IEnumerator LoadCompressedBinary(
    DataLoadRequest req,
    byte[] bytes,
    Action<DataTableLoadError> onEnd,
    bool useQueue)
  {
    bool wait = true;
    DataTableLoadError error = DataTableLoadError.None;
    System.Action act = (System.Action) (() =>
    {
      try
      {
        if (bytes != null)
          error = this.Save(req, bytes);
        else
          bytes = this.cache.Load(req);
        if (bytes != null)
        {
          if (this.Verify(req, bytes))
            error = this.ProcessCompressedBinary(req, bytes);
          else
            error = DataTableLoadError.VerifyError;
        }
        else
          error = DataTableLoadError.FileReadError;
      }
      catch (Exception ex)
      {
        error = DataTableLoadError.FileReadError;
      }
      finally
      {
        wait = false;
      }
    });
    if (useQueue)
      this.taskRunner.Add(req.name, act);
    else
      ThreadPoolWrapper.QueueUserWorkItem((WaitCallback) (obj => act()));
    while (wait)
      yield return (object) null;
    onEnd(error);
  }

  private bool Verify(DataLoadRequest req, byte[] bytes)
  {
    bool flag = false;
    using (MemoryStream signedDataStream = new MemoryStream(bytes))
    {
      int count = 256 /*0x0100*/;
      byte[] numArray = new byte[count];
      signedDataStream.Read(numArray, 0, count);
      try
      {
        flag = Cipher.verifyBytes((Stream) signedDataStream, numArray);
      }
      catch (Exception ex)
      {
        flag = false;
        Log.Error(LOG.DATA_TABLE, "verify exception({0}): {1}", (object) req.name, (object) ex);
      }
      if (!flag)
      {
        MD5Hash md5Hash = MD5Hash.Calc(bytes);
        flag = req.OnVerifyError(md5Hash.ToString());
      }
    }
    return flag;
  }

  private DataTableLoadError ProcessCompressedText(DataLoadRequest req, byte[] bytes)
  {
    DataTableLoadError dataTableLoadError = DataTableLoadError.None;
    try
    {
      req.processCompressedTextData(bytes);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.DATA_TABLE, "DataLoadError({0}): {1}", (object) req.name, (object) ex);
      dataTableLoadError = DataTableLoadError.FileReadError;
    }
    return dataTableLoadError;
  }

  private DataTableLoadError ProcessCompressedBinary(DataLoadRequest req, byte[] bytes)
  {
    DataTableLoadError dataTableLoadError = DataTableLoadError.None;
    try
    {
      req.processCompressedBinaryData(bytes);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.DATA_TABLE, "DataLoadError({0}): {1}", (object) req.name, (object) ex);
      dataTableLoadError = DataTableLoadError.FileReadError;
    }
    return dataTableLoadError;
  }

  private DataTableLoadError Save(DataLoadRequest req, byte[] bytes)
  {
    DataTableLoadError dataTableLoadError = DataTableLoadError.None;
    try
    {
      this.cache.Save(req, bytes);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.DATA_TABLE, "SaveError({0}): {1}", (object) req.name, (object) ex);
      dataTableLoadError = DataTableLoadError.FileWriteError;
    }
    return dataTableLoadError;
  }

  private IEnumerator Download(
    DataLoadRequest req,
    Action<byte[]> onComplete,
    Action<DataTableLoadError> onError)
  {
    while (this.downloadCount >= 5)
      yield return (object) null;
    ++this.downloadCount;
    UnityWebRequest www = new UnityWebRequest($"{NetworkManager.TABLE_HOST}{MonoBehaviourSingleton<ResourceManager>.I.tableIndex.ToString()}/" + req.path);
    www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
    www.SendWebRequest();
    float progress = 0.0f;
    float timeOut = 15f;
    while (!www.isDone)
    {
      yield return (object) null;
      timeOut -= Time.unscaledDeltaTime;
      if ((double) www.downloadProgress != (double) progress)
      {
        progress = www.downloadProgress;
        timeOut = 15f;
      }
      if ((double) timeOut < 0.0)
      {
        onError(DataTableLoadError.DownloadTimeOut);
        www.Dispose();
        --this.downloadCount;
        yield break;
      }
    }
    if (!string.IsNullOrEmpty(www.error))
    {
      if (www.error.Contains("404"))
        onError(DataTableLoadError.AssetNotFoundError);
      else
        onError(DataTableLoadError.NetworkError);
      www.Dispose();
      --this.downloadCount;
    }
    else
    {
      byte[] data = www.downloadHandler.data;
      www.Dispose();
      --this.downloadCount;
      onComplete(data);
    }
  }

  public void ChangePriorityTop(string tableName)
  {
    this.taskRunner.ChangePriorityTop(tableName);
    DataLoadRequest dataLoadRequest = this.requestList.Find((Predicate<DataLoadRequest>) (o => o.name == tableName));
    if (dataLoadRequest == null || dataLoadRequest.depReqs == null)
      return;
    dataLoadRequest.depReqs.ForEach((Action<DataLoadRequest>) (o => this.taskRunner.ChangePriorityTop(o.name)));
  }
}
