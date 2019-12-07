public class DownloadEnemyDialog : GameSection
{
	private enum UI
	{
		LBL_FILESIZE_NUM
	}

	private bool remove = true;

	private void OnQuery_YES()
	{
	}

	private void OnQuery_NO()
	{
		remove = false;
	}

	protected override void OnClose()
	{
		base.OnClose();
		if (!remove && MonoBehaviourSingleton<EnemyPredownloadManager>.IsValid() && MonoBehaviourSingleton<EnemyPredownloadManager>.I.IsAvaiDownload())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.AddHighForceChangeScene("Home", "DownloadEnemy");
		}
	}

	public override void UpdateUI()
	{
	}
}
