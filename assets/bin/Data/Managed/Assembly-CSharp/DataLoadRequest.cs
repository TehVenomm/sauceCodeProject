using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class DataLoadRequest
{
	private string directory;

	private bool enableLoadBinary = true;

	public Action<byte[]> processCompressedTextData;

	public Action<byte[]> processCompressedBinaryData;

	public List<DataLoadRequest> depReqs = new List<DataLoadRequest>();

	public string name
	{
		get;
		private set;
	}

	public IDataTableRequestHash hash
	{
		get;
		private set;
	}

	public IDataTableRequestHash hashBinary
	{
		get;
		private set;
	}

	public string filename => GetName().ToLower() + GoGameResourceManager.GetDefaultAssetBundleExtension();

	public string path => directory + "/" + filename + "?v=" + GetHash().ToString();

	public bool downloadOnly
	{
		get;
		private set;
	}

	public bool enableLoadBinaryData
	{
		get
		{
			return processCompressedBinaryData != null && enableLoadBinary;
		}
		set
		{
			enableLoadBinary = value;
		}
	}

	public float progress
	{
		get;
		set;
	}

	public DataTableLoadError error
	{
		get;
		private set;
	}

	public bool isCompleted
	{
		get;
		private set;
	}

	public event Action onComplete
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			this.onComplete = Delegate.Combine((Delegate)this.onComplete, (Delegate)value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			this.onComplete = Delegate.Remove((Delegate)this.onComplete, (Delegate)value);
		}
	}

	public event Action<DataTableLoadError> onError;

	public event Func<string, bool> onVerifyError
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.onVerifyError = Delegate.Combine((Delegate)this.onVerifyError, (Delegate)value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.onVerifyError = Delegate.Remove((Delegate)this.onVerifyError, (Delegate)value);
		}
	}

	public DataLoadRequest(string name, IDataTableRequestHash hash, string directory, bool downloadOnly)
	{
		this.name = name;
		this.hash = hash;
		this.directory = directory;
		this.downloadOnly = downloadOnly;
	}

	public string GetName()
	{
		if (enableLoadBinaryData)
		{
			return name + "_b";
		}
		return name;
	}

	public IDataTableRequestHash GetHash()
	{
		if (enableLoadBinaryData)
		{
			return hashBinary;
		}
		return hash;
	}

	public void Reset()
	{
		progress = 0f;
		enableLoadBinary = true;
		error = DataTableLoadError.None;
		isCompleted = false;
	}

	public void DependsOn(DataLoadRequest depReq)
	{
		depReqs.Add(depReq);
	}

	public void OnComplete()
	{
		isCompleted = true;
		if (this.onComplete != null)
		{
			this.onComplete.Invoke();
		}
	}

	public void OnError(DataTableLoadError error)
	{
		this.error = error;
		this.onError(error);
	}

	public bool OnVerifyError(string hash)
	{
		return this.onVerifyError.Invoke(hash);
	}

	public void SetupLoadBinary(DataTableManifest manifest, Action<byte[]> processBinary)
	{
		enableLoadBinary = true;
		processCompressedBinaryData = processBinary;
		hashBinary = manifest.GetTableHash(GetName());
		if (hashBinary == null)
		{
			enableLoadBinary = false;
		}
	}
}
