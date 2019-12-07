using System.Collections;
using UnityEngine;

public class HomeSceneBase : GameSection
{
	public override void Initialize()
	{
		UILabel.OutlineLimit = false;
		StartCoroutine(DoInitialize());
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
		yield return SetEnemyDownloadCallback();
	}

	public override void Exit()
	{
		MonoBehaviourSingleton<StatusManager>.I.InitStatusEquipData();
		base.Exit();
	}

	private IEnumerator SetEnemyDownloadCallback()
	{
		while (!MonoBehaviourSingleton<EnemyPredownloadManager>.IsValid())
		{
			yield return null;
		}
		if (!MonoBehaviourSingleton<EnemyPredownloadManager>.I.enabled)
		{
			MonoBehaviourSingleton<EnemyPredownloadManager>.I.enabled = true;
			yield return new WaitForSeconds(0.5f);
		}
		if (MonoBehaviourSingleton<EnemyPredownloadManager>.I.IsAvaiDownload())
		{
			OnFinishCheckDownloadEnemyData(avaiDownload: true);
		}
		else
		{
			MonoBehaviourSingleton<EnemyPredownloadManager>.I.AddListenerCheck(OnUpdateCheckDownloadEnemyData, OnFinishCheckDownloadEnemyData);
		}
	}

	protected override void OnClose()
	{
		MonoBehaviourSingleton<EnemyPredownloadManager>.I.RemoveListenerCheck(OnUpdateCheckDownloadEnemyData, OnFinishCheckDownloadEnemyData);
		base.OnClose();
	}

	private void OnFinishCheckDownloadEnemyData(bool avaiDownload)
	{
		if (avaiDownload)
		{
			Debug.Log("ENABLE DOWNLOAD ENEMY DATA");
			MonoBehaviourSingleton<GameSceneManager>.I.AddHighForceChangeScene("Home", "DownloadEnemy");
		}
	}

	private void OnUpdateCheckDownloadEnemyData()
	{
	}
}
