using System.Collections;

public class HomeSceneBase : GameSection
{
	public override void Initialize()
	{
		UILabel.OutlineLimit = false;
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		bool wait = true;
		MonoBehaviourSingleton<OnceManager>.I.SendGetOnce(delegate
		{
			wait = false;
		});
		while (wait)
		{
			yield return null;
		}
		MonoBehaviourSingleton<QuestManager>.I.SetClearStatus();
		MonoBehaviourSingleton<DeliveryManager>.I.SetList();
		MonoBehaviourSingleton<WorldMapManager>.I.SetWorldMapTraveledList();
		MonoBehaviourSingleton<BlackListManager>.I.SetAllList();
		MonoBehaviourSingleton<AchievementManager>.I.SetAchievement();
		MonoBehaviourSingleton<GuildRequestManager>.I.SetList();
		MonoBehaviourSingleton<WorldMapManager>.I.SetReleasedRegion();
		MonoBehaviourSingleton<NativeGameService>.I.FixAchievement();
		base.Initialize();
	}

	public override void Exit()
	{
		MonoBehaviourSingleton<StatusManager>.I.InitStatusEquipData();
		base.Exit();
	}
}
