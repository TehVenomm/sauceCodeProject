using UnityEngine;

public class DownloadEnemyProcess : GameSection
{
	private enum UI
	{
		LBL_DOWNLOAD_NUM,
		PROCESS_SL
	}

	private bool isActive;

	private void OnQuery_CANCEL()
	{
		Debug.Log("Stop Download");
		EnemyPredownloadManager.Stop();
	}

	public override void UpdateUI()
	{
		if (MonoBehaviourSingleton<EnemyPredownloadManager>.I.CurrentState == EnemyPredownloadManager.DownloadState.CheckingFile)
		{
			SetLabelText(UI.LBL_DOWNLOAD_NUM, MonoBehaviourSingleton<EnemyPredownloadManager>.I.checkedCount + "/" + MonoBehaviourSingleton<EnemyPredownloadManager>.I.totalPackage);
		}
		else
		{
			SetLabelText(UI.LBL_DOWNLOAD_NUM, MonoBehaviourSingleton<EnemyPredownloadManager>.I.loadedCount + "/" + MonoBehaviourSingleton<EnemyPredownloadManager>.I.totalCount);
		}
		SetSliderValue(UI.PROCESS_SL, ProcessValue());
	}

	private float ProcessValue()
	{
		if (MonoBehaviourSingleton<EnemyPredownloadManager>.I.totalCount <= 0)
		{
			return 1f;
		}
		return Mathf.Clamp((float)MonoBehaviourSingleton<EnemyPredownloadManager>.I.loadedCount / (float)MonoBehaviourSingleton<EnemyPredownloadManager>.I.totalCount, 0f, 1f);
	}

	protected override void OnOpen()
	{
		base.OnOpen();
		if (!isActive)
		{
			MonoBehaviourSingleton<EnemyPredownloadManager>.I.AddListenerCheck(UpdateChecking, StopChecking);
			MonoBehaviourSingleton<EnemyPredownloadManager>.I.AddListenerDownload(UpdateDownload, StopDownload);
		}
		isActive = true;
	}

	protected override void OnClose()
	{
		if (isActive && MonoBehaviourSingleton<EnemyPredownloadManager>.IsValid())
		{
			MonoBehaviourSingleton<EnemyPredownloadManager>.I.RemoveListenerCheck(UpdateChecking, StopChecking);
			MonoBehaviourSingleton<EnemyPredownloadManager>.I.RemoveListenerDownload(UpdateDownload, StopDownload);
		}
		isActive = false;
		base.OnClose();
	}

	protected override void OnDestroy()
	{
		if (isActive && MonoBehaviourSingleton<EnemyPredownloadManager>.IsValid())
		{
			MonoBehaviourSingleton<EnemyPredownloadManager>.I.RemoveListenerCheck(UpdateChecking, StopChecking);
			MonoBehaviourSingleton<EnemyPredownloadManager>.I.RemoveListenerDownload(UpdateDownload, StopDownload);
		}
		isActive = false;
		base.OnDestroy();
	}

	private void UpdateChecking()
	{
		UpdateUI();
	}

	private void UpdateDownload()
	{
		UpdateUI();
	}

	private void StopChecking(bool avai)
	{
	}

	private void StopDownload(bool succeed)
	{
		MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(base.sectionData.sectionName);
		GameSection.BackSection();
	}
}
