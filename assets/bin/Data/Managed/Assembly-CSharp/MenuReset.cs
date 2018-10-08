public class MenuReset : GameSection
{
	public static bool needClearCache;

	public static bool needPredownload;

	public override void Initialize()
	{
		MonoBehaviourSingleton<AppMain>.I.Reset(needClearCache, needPredownload);
		needClearCache = false;
		needPredownload = false;
	}
}
