using System.Collections;

public class MenuReset : GameSection
{
	public static bool needClearCache;

	public static bool needPredownload;

	public override void Initialize()
	{
		if (needClearCache && needPredownload)
		{
			this.StartCoroutine(ResetProc());
			base.Initialize();
		}
		else
		{
			Reset();
		}
	}

	private IEnumerator ResetProc()
	{
		GameSceneGlobalSettings.forceIgnoreMainUI = true;
		yield return ResourceSizeInfo.Init();
		yield return ResourceSizeInfo.OpenConfirmDialog(ResourceSizeInfo.GetOpeningAssetSizeMB(isTutorial: false));
		GameSceneGlobalSettings.forceIgnoreMainUI = false;
		Reset();
	}

	private void Reset()
	{
		MonoBehaviourSingleton<AppMain>.I.Reset(needClearCache, needPredownload);
		needClearCache = false;
		needPredownload = false;
	}
}
