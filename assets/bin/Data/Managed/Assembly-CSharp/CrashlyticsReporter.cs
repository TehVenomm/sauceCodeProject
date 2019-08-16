using Network;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CrashlyticsReporter
{
	private static bool isEnable;

	[CompilerGenerated]
	private static LogCallback _003C_003Ef__mg_0024cache0;

	public unsafe static void EnableReport()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		if (!isEnable)
		{
			if (_003C_003Ef__mg_0024cache0 == null)
			{
				_003C_003Ef__mg_0024cache0 = new LogCallback((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			Application.RegisterLogCallback(_003C_003Ef__mg_0024cache0);
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
