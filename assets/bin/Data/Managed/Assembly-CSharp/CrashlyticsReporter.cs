using Network;
using UnityEngine;

public class CrashlyticsReporter
{
	private static bool isEnable;

	public static void EnableReport()
	{
		if (!isEnable)
		{
			Application.logMessageReceived += HandleLog;
			isEnable = true;
		}
	}

	private static void HandleLog(string log, string stack, LogType type)
	{
		if (type == LogType.Exception)
		{
			ReportException(log, stack);
		}
	}

	public static void SetUserInfo(UserInfo userInfo)
	{
		if (isEnable)
		{
			CrashlyticsWrapper.SetUserId(userInfo.id);
			CrashlyticsWrapper.SetUserName(userInfo.name);
		}
	}

	public static void SetAssetIndex(int index)
	{
		if (isEnable)
		{
			CrashlyticsWrapper.SetInt("AssetIndex", index);
		}
	}

	public static void SetManifestVersion(int ver)
	{
		if (isEnable)
		{
			CrashlyticsWrapper.SetInt("ManifestVersion", ver);
		}
	}

	public static void SetAPIRequest(string url)
	{
		if (isEnable)
		{
			CrashlyticsWrapper.SetString("APIRequestURL", url);
		}
	}

	public static void SetAPIRequestStatus(bool requesting)
	{
		if (isEnable)
		{
			CrashlyticsWrapper.SetBool("APIRequesting", requesting);
		}
	}

	public static void SetSceneInfo(string sceneName, string sectionName)
	{
		if (isEnable)
		{
			CrashlyticsWrapper.SetString("SceneName", sceneName);
			CrashlyticsWrapper.SetString("SectionName", sectionName);
		}
	}

	public static void SetSceneStatus(bool isChanging)
	{
		if (isEnable)
		{
			CrashlyticsWrapper.SetBool("SceneChaning", isChanging);
		}
	}

	public static void SetLoadingBundle(string bundleName)
	{
		if (isEnable)
		{
			CrashlyticsWrapper.SetString("LoadingBundle", bundleName);
		}
	}

	private static void ReportException(string log, string stack)
	{
		if (isEnable)
		{
			log = (log ?? "");
			stack = (stack ?? "");
			CrashlyticsWrapper.ReportException(log + "\n" + stack);
		}
	}
}
