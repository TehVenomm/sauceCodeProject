using Network;
using System;
using UnityEngine;

public class CrashlyticsReporter
{
	private static bool isEnable;

	public unsafe static void EnableReport()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (!isEnable)
		{
			Application.RegisterLogCallback(new LogCallback((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			isEnable = true;
		}
	}

	private static void HandleLog(string log, string stack, LogType type)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Invalid comparison between Unknown and I4
		if ((int)type == 4)
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
			log = (log ?? string.Empty);
			stack = (stack ?? string.Empty);
			string report = log + "\n" + stack;
			CrashlyticsWrapper.ReportException(report);
		}
	}
}
