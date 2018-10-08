using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class DataLoader
{
	private const int DL_MAX = 5;

	private DataCache cache;

	private List<DataLoadRequest> requestList = new List<DataLoadRequest>();

	private MultiThreadTaskRunner taskRunner;

	private int downloadCount;

	public DataLoader()
		: this()
	{
	}

	private void Awake()
	{
		taskRunner = new MultiThreadTaskRunner();
	}

	private void OnDestroy()
	{
		if (taskRunner != null)
		{
			taskRunner.DestroyThread();
			taskRunner = null;
		}
	}

	public bool IsLoading(string name)
	{
		return null != requestList.Find((DataLoadRequest o) => o.name == name);
	}

	public bool IsLoading()
	{
		return 0 < requestList.Count;
	}

	public void SetCache(DataCache cache)
	{
		this.cache = cache;
	}

	public void Request(DataLoadRequest req)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(Load(req, req.downloadOnly, false, false));
	}

	public void Request(List<DataLoadRequest> reqs)
	{
		taskRunner.CreateThread();
		requestList.AddRange(reqs);
		reqs.ForEach(delegate(DataLoadRequest o)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			this.StartCoroutine(Load(o, o.downloadOnly, true, false));
		});
	}

	public void RequestManifest(DataLoadRequest req)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(Load(req, req.downloadOnly, false, true));
	}

	private void RemoveQueue(DataLoadRequest req)
	{
		requestList.Remove(req);
		if (0 >= requestList.Count)
		{
			taskRunner.DestroyThread();
		}
	}

	private IEnumerator Load(DataLoadRequest req, bool downloadOnly, bool useQueue, bool forceDownload = false)
	{
		int i = 0;
		for (int len = req.depReqs.Count; i < len; i++)
		{
			while (!req.depReqs[i].isCompleted)
			{
				yield return (object)null;
			}
		}
		DataTableLoadError error = DataTableLoadError.None;
		byte[] bytes = null;
		if (!cache.IsCached(req) || forceDownload)
		{
			IEnumerator download = Download(req, delegate(byte[] b)
			{
				((_003CLoad_003Ec__Iterator234)/*Error near IL_00f7: stateMachine*/)._003Cbytes_003E__3 = b;
			}, delegate(DataTableLoadError e)
			{
				((_003CLoad_003Ec__Iterator234)/*Error near IL_0103: stateMachine*/)._003Cerror_003E__2 = e;
			});
			while (download.MoveNext())
			{
				yield return download.Current;
			}
			if (error != 0)
			{
				if (useQueue)
				{
					RemoveQueue(req);
				}
				if (req.enableLoadBinaryData)
				{
					req.enableLoadBinaryData = false;
					Request(req);
				}
				else
				{
					req.OnError(error);
				}
				yield break;
			}
		}
		if (downloadOnly)
		{
			bool wait = true;
			ThreadPoolWrapper.QueueUserWorkItem(delegate
			{
				try
				{
					if (((_003CLoad_003Ec__Iterator234)/*Error near IL_01cc: stateMachine*/)._003Cbytes_003E__3 != null)
					{
						((_003CLoad_003Ec__Iterator234)/*Error near IL_01cc: stateMachine*/)._003Cerror_003E__2 = ((_003CLoad_003Ec__Iterator234)/*Error near IL_01cc: stateMachine*/)._003C_003Ef__this.Save(((_003CLoad_003Ec__Iterator234)/*Error near IL_01cc: stateMachine*/).req, ((_003CLoad_003Ec__Iterator234)/*Error near IL_01cc: stateMachine*/)._003Cbytes_003E__3);
					}
				}
				catch (Exception)
				{
					((_003CLoad_003Ec__Iterator234)/*Error near IL_01cc: stateMachine*/)._003Cerror_003E__2 = DataTableLoadError.FileReadError;
				}
				finally
				{
					((_003CLoad_003Ec__Iterator234)/*Error near IL_01cc: stateMachine*/)._003Cwait_003E__6 = false;
				}
			});
			while (wait)
			{
				yield return (object)null;
			}
			if (error == DataTableLoadError.None)
			{
				req.OnComplete();
			}
			else
			{
				req.OnError(error);
			}
			if (useQueue)
			{
				RemoveQueue(req);
			}
		}
		else
		{
			Stopwatch sw = Stopwatch.StartNew();
			IEnumerator loading;
			if (req.enableLoadBinaryData)
			{
				loading = LoadCompressedBinary(req, bytes, delegate(DataTableLoadError e)
				{
					((_003CLoad_003Ec__Iterator234)/*Error near IL_027b: stateMachine*/)._003Cerror_003E__2 = e;
				}, useQueue);
				while (loading.MoveNext())
				{
					yield return loading.Current;
				}
				if (error != 0)
				{
					if (useQueue)
					{
						requestList.Remove(req);
					}
					req.enableLoadBinaryData = false;
					Request(req);
					yield break;
				}
			}
			else
			{
				loading = LoadCompressedTextWithSignature(req, bytes, delegate(DataTableLoadError e)
				{
					((_003CLoad_003Ec__Iterator234)/*Error near IL_0330: stateMachine*/)._003Cerror_003E__2 = e;
				}, useQueue);
			}
			while (loading.MoveNext())
			{
				yield return loading.Current;
			}
			sw.Stop();
			if (error != 0)
			{
				req.OnError(error);
			}
			else
			{
				req.OnComplete();
			}
			if (useQueue)
			{
				RemoveQueue(req);
			}
		}
	}

	private IEnumerator LoadCompressedTextWithSignature(DataLoadRequest req, byte[] bytes, Action<DataTableLoadError> onEnd, bool useQueue)
	{
		bool wait = true;
		DataTableLoadError error = DataTableLoadError.None;
		Action act = delegate
		{
			try
			{
				if (((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/).bytes != null)
				{
					((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/)._003Cerror_003E__1 = ((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/)._003C_003Ef__this.Save(((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/).req, ((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/).bytes);
				}
				else
				{
					((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/).bytes = ((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/)._003C_003Ef__this.cache.Load(((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/).req);
				}
				if (((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/).bytes != null)
				{
					if (((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/)._003C_003Ef__this.Verify(((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/).req, ((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/).bytes))
					{
						((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/)._003Cerror_003E__1 = ((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/)._003C_003Ef__this.ProcessCompressedText(((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/).req, ((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/).bytes);
					}
					else
					{
						((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/)._003Cerror_003E__1 = DataTableLoadError.VerifyError;
					}
				}
				else
				{
					((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/)._003Cerror_003E__1 = DataTableLoadError.FileReadError;
				}
			}
			catch (Exception)
			{
				((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/)._003Cerror_003E__1 = DataTableLoadError.FileReadError;
			}
			finally
			{
				((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0030: stateMachine*/)._003Cwait_003E__0 = false;
			}
		};
		if (useQueue)
		{
			taskRunner.Add(req.name, act);
		}
		else
		{
			ThreadPoolWrapper.QueueUserWorkItem(delegate
			{
				((_003CLoadCompressedTextWithSignature_003Ec__Iterator235)/*Error near IL_0072: stateMachine*/)._003Cact_003E__2();
			});
		}
		while (wait)
		{
			yield return (object)null;
		}
		onEnd(error);
	}

	private IEnumerator LoadCompressedBinary(DataLoadRequest req, byte[] bytes, Action<DataTableLoadError> onEnd, bool useQueue)
	{
		bool wait = true;
		DataTableLoadError error = DataTableLoadError.None;
		Action act = delegate
		{
			try
			{
				if (((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/).bytes != null)
				{
					((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/)._003Cerror_003E__1 = ((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/)._003C_003Ef__this.Save(((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/).req, ((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/).bytes);
				}
				else
				{
					((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/).bytes = ((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/)._003C_003Ef__this.cache.Load(((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/).req);
				}
				if (((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/).bytes != null)
				{
					if (((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/)._003C_003Ef__this.Verify(((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/).req, ((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/).bytes))
					{
						((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/)._003Cerror_003E__1 = ((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/)._003C_003Ef__this.ProcessCompressedBinary(((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/).req, ((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/).bytes);
					}
					else
					{
						((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/)._003Cerror_003E__1 = DataTableLoadError.VerifyError;
					}
				}
				else
				{
					((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/)._003Cerror_003E__1 = DataTableLoadError.FileReadError;
				}
			}
			catch (Exception)
			{
				((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/)._003Cerror_003E__1 = DataTableLoadError.FileReadError;
			}
			finally
			{
				((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0030: stateMachine*/)._003Cwait_003E__0 = false;
			}
		};
		if (useQueue)
		{
			taskRunner.Add(req.name, act);
		}
		else
		{
			ThreadPoolWrapper.QueueUserWorkItem(delegate
			{
				((_003CLoadCompressedBinary_003Ec__Iterator236)/*Error near IL_0072: stateMachine*/)._003Cact_003E__2();
			});
		}
		while (wait)
		{
			yield return (object)null;
		}
		onEnd(error);
	}

	private bool Verify(DataLoadRequest req, byte[] bytes)
	{
		bool flag = false;
		using (MemoryStream memoryStream = new MemoryStream(bytes))
		{
			int num = 256;
			byte[] array = new byte[num];
			memoryStream.Read(array, 0, num);
			try
			{
				flag = Cipher.verifyBytes(memoryStream, array);
			}
			catch (Exception ex)
			{
				flag = false;
				Log.Error(LOG.DATA_TABLE, "verify exception({0}): {1}", req.name, ex);
			}
			if (flag)
			{
				return flag;
			}
			MD5Hash mD5Hash = MD5Hash.Calc(bytes);
			return req.OnVerifyError(mD5Hash.ToString());
		}
	}

	private DataTableLoadError ProcessCompressedText(DataLoadRequest req, byte[] bytes)
	{
		DataTableLoadError result = DataTableLoadError.None;
		try
		{
			req.processCompressedTextData(bytes);
			return result;
		}
		catch (Exception ex)
		{
			Log.Error(LOG.DATA_TABLE, "DataLoadError({0}): {1}", req.name, ex);
			return DataTableLoadError.FileReadError;
		}
	}

	private DataTableLoadError ProcessCompressedBinary(DataLoadRequest req, byte[] bytes)
	{
		DataTableLoadError result = DataTableLoadError.None;
		try
		{
			req.processCompressedBinaryData(bytes);
			return result;
		}
		catch (Exception ex)
		{
			Log.Error(LOG.DATA_TABLE, "DataLoadError({0}): {1}", req.name, ex);
			return DataTableLoadError.FileReadError;
		}
	}

	private DataTableLoadError Save(DataLoadRequest req, byte[] bytes)
	{
		DataTableLoadError result = DataTableLoadError.None;
		try
		{
			cache.Save(req, bytes);
			return result;
		}
		catch (Exception ex)
		{
			Log.Error(LOG.DATA_TABLE, "SaveError({0}): {1}", req.name, ex);
			return DataTableLoadError.FileWriteError;
		}
	}

	private IEnumerator Download(DataLoadRequest req, Action<byte[]> onComplete, Action<DataTableLoadError> onError)
	{
		while (downloadCount >= 5)
		{
			yield return (object)null;
		}
		downloadCount++;
		string host = NetworkManager.TABLE_HOST + MonoBehaviourSingleton<ResourceManager>.I.tableIndex.ToString() + "/";
		WWW www = new WWW(host + req.path);
		float progress = 0f;
		float timeOut = 15f;
		while (!www.get_isDone())
		{
			yield return (object)null;
			timeOut -= Time.get_unscaledDeltaTime();
			if (www.get_progress() != progress)
			{
				progress = www.get_progress();
				timeOut = 15f;
			}
			if (timeOut < 0f)
			{
				onError(DataTableLoadError.DownloadTimeOut);
				www.Dispose();
				downloadCount--;
				yield break;
			}
		}
		if (!string.IsNullOrEmpty(www.get_error()))
		{
			if (www.get_error().Contains("404"))
			{
				onError(DataTableLoadError.AssetNotFoundError);
			}
			else
			{
				onError(DataTableLoadError.NetworkError);
			}
			www.Dispose();
			downloadCount--;
		}
		else
		{
			byte[] bytes = www.get_bytes();
			www.Dispose();
			downloadCount--;
			onComplete(bytes);
		}
	}

	public void ChangePriorityTop(string tableName)
	{
		taskRunner.ChangePriorityTop(tableName);
		DataLoadRequest dataLoadRequest = requestList.Find((DataLoadRequest o) => o.name == tableName);
		if (dataLoadRequest != null && dataLoadRequest.depReqs != null)
		{
			dataLoadRequest.depReqs.ForEach(delegate(DataLoadRequest o)
			{
				taskRunner.ChangePriorityTop(o.name);
			});
		}
	}
}
