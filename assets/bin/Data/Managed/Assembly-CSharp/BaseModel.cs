using Network;
using System;
using System.Collections.Generic;

public class BaseModel
{
	[Serializable]
	public class AssetVersionInfo
	{
		public string name = "";

		public int version;
	}

	public class RecommendUpdate
	{
		public bool flag;

		public string ver = "";
	}

	public int error;

	public string currentTime = "";

	public int tutorial = -1;

	public List<AssetVersionInfo> assetVersion;

	public int assetManifestVersion;

	public int assetIndex;

	public int tableIndex;

	public int tableManifestVersion;

	public bool usageLimitMode;

	public int infoError;

	public bool appClose;

	public string closedNotice = "";

	public bool openRefundForm;

	public XorInt vm = new XorInt(1);

	public List<RecommendUpdate> recommendUpdate;

	public List<BaseModelDiff> diff;

	public Error Error
	{
		get
		{
			return (Error)error;
		}
		set
		{
			error = (int)value;
		}
	}

	public void Apply()
	{
		_ = tutorial;
		_ = 0;
		if (assetManifestVersion > 0 && MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			MonoBehaviourSingleton<ResourceManager>.I.manifestVersion = assetManifestVersion;
		}
		if (assetIndex > 0 && MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			MonoBehaviourSingleton<ResourceManager>.I.assetIndex = assetIndex;
		}
		if (tableIndex > 0 && MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			MonoBehaviourSingleton<ResourceManager>.I.tableIndex = tableIndex;
		}
		if (tableManifestVersion > 0 && MonoBehaviourSingleton<DataTableManager>.IsValid())
		{
			MonoBehaviourSingleton<DataTableManager>.I.OnReceiveTableManifestVersion(tableManifestVersion);
		}
		if ((int)vm > 0 && MonoBehaviourSingleton<DataTableManager>.IsValid())
		{
			MonoBehaviourSingleton<DataTableManager>.I.OnReceiveVM(vm);
		}
		if (!string.IsNullOrEmpty(currentTime) && MonoBehaviourSingleton<TimeManager>.IsValid())
		{
			TimeManager.SetServerTime(currentTime);
		}
		if (MonoBehaviourSingleton<AccountManager>.IsValid())
		{
			MonoBehaviourSingleton<AccountManager>.I.appClose = appClose;
			MonoBehaviourSingleton<AccountManager>.I.usageLimitMode = false;
			MonoBehaviourSingleton<AccountManager>.I.closedNotice = closedNotice;
			MonoBehaviourSingleton<AccountManager>.I.openRefundForm = openRefundForm;
		}
	}
}
