using System.Collections;
using UnityEngine;

public class EnemyDownloadTop : GameSection
{
	private enum UI
	{
		TEX_BG,
		Container,
		LBL_DOWNLOAD_NUM,
		LBL_CHECKING,
		LBL_DOWNLOAD,
		PROCESS_SL
	}

	private bool isActive;

	private bool remove;

	private GameObject titleObjectRoot;

	private GameObject secondCamRoot;

	private Camera secondCam;

	private void OnQuery_CANCEL()
	{
		Debug.Log("Stop Download");
		if (MonoBehaviourSingleton<EnemyPredownloadManager>.IsValid() && MonoBehaviourSingleton<EnemyPredownloadManager>.I.CurrentState == EnemyPredownloadManager.DownloadState.Downloading)
		{
			EnemyPredownloadManager.Stop();
		}
		remove = false;
	}

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	public override void UpdateUI()
	{
		if (MonoBehaviourSingleton<EnemyPredownloadManager>.I.CurrentState == EnemyPredownloadManager.DownloadState.CheckingFile)
		{
			SetActive(UI.LBL_CHECKING, is_visible: true);
			SetActive(UI.LBL_DOWNLOAD, is_visible: false);
			SetLabelText(UI.LBL_DOWNLOAD_NUM, MonoBehaviourSingleton<EnemyPredownloadManager>.I.checkedCount + "/" + MonoBehaviourSingleton<EnemyPredownloadManager>.I.totalPackage);
			SetSliderValue(UI.PROCESS_SL, ProcessValue(MonoBehaviourSingleton<EnemyPredownloadManager>.I.checkedCount, MonoBehaviourSingleton<EnemyPredownloadManager>.I.totalPackage));
		}
		else if (MonoBehaviourSingleton<EnemyPredownloadManager>.I.CurrentState == EnemyPredownloadManager.DownloadState.Downloading || MonoBehaviourSingleton<EnemyPredownloadManager>.I.CurrentState == EnemyPredownloadManager.DownloadState.FinishDownload)
		{
			SetActive(UI.LBL_CHECKING, is_visible: false);
			SetActive(UI.LBL_DOWNLOAD, is_visible: true);
			SetLabelText(UI.LBL_DOWNLOAD_NUM, MonoBehaviourSingleton<EnemyPredownloadManager>.I.loadedCount + "/" + MonoBehaviourSingleton<EnemyPredownloadManager>.I.totalCount);
			SetSliderValue(UI.PROCESS_SL, ProcessValue(MonoBehaviourSingleton<EnemyPredownloadManager>.I.loadedCount, MonoBehaviourSingleton<EnemyPredownloadManager>.I.totalCount));
		}
	}

	private float ProcessValue(int a, int b)
	{
		if (b <= 0)
		{
			return 1f;
		}
		return Mathf.Clamp((float)a / (float)b, 0f, 1f);
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
		if (titleObjectRoot != null)
		{
			Object.Destroy(titleObjectRoot);
			titleObjectRoot = null;
		}
		if (secondCamRoot != null)
		{
			if (secondCam != null)
			{
				Object.Destroy(secondCam);
			}
			Object.Destroy(secondCamRoot);
			secondCamRoot = null;
		}
		isActive = false;
		if (!remove && MonoBehaviourSingleton<EnemyPredownloadManager>.IsValid() && MonoBehaviourSingleton<EnemyPredownloadManager>.I.IsAvaiDownload())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.AddHighForceChangeScene("Home", "DownloadEnemy");
		}
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

	private IEnumerator DoInitialize()
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject loadedTitleObj = load_queue.Load(RESOURCE_CATEGORY.CUTSCENE, "Title");
		while (load_queue.IsLoading())
		{
			yield return null;
		}
		titleObjectRoot = (ResourceUtility.Instantiate(loadedTitleObj.loadedObject) as GameObject);
		titleObjectRoot.transform.parent = MonoBehaviourSingleton<AppMain>.I.transform;
		secondCamRoot = new GameObject();
		secondCamRoot.transform.parent = MonoBehaviourSingleton<AppMain>.I.transform;
		secondCamRoot.transform.position = new Vector3(0f, 0f, -30f);
		secondCam = secondCamRoot.AddComponent<Camera>();
		secondCam.clearFlags = CameraClearFlags.Color;
		secondCam.backgroundColor = new Color(0f, 0f, 0f, 5f);
		secondCam.orthographic = true;
		secondCam.orthographicSize = 4f;
		secondCam.nearClipPlane = 0.3f;
		secondCam.farClipPlane = 50f;
		secondCam.depth = -1f;
		base.Initialize();
		if (MonoBehaviourSingleton<EnemyPredownloadManager>.I.CurrentState == EnemyPredownloadManager.DownloadState.ReadyDownload)
		{
			MonoBehaviourSingleton<EnemyPredownloadManager>.I.ProceedDownload();
		}
	}

	private void SetActiveUI(bool enable)
	{
		GetCtrl(UI.Container).gameObject.SetActive(enable);
	}

	private void UpdateDownload()
	{
		UpdateUI();
	}

	private void StopDownload(bool succeed)
	{
		if (succeed)
		{
			remove = true;
		}
		else
		{
			remove = false;
		}
		MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(base.sectionData.sectionName);
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Home", "");
	}

	private void UpdateChecking()
	{
		UpdateUI();
	}

	private void StopChecking(bool avai)
	{
		if (avai && MonoBehaviourSingleton<EnemyPredownloadManager>.I.CurrentState == EnemyPredownloadManager.DownloadState.ReadyDownload)
		{
			MonoBehaviourSingleton<EnemyPredownloadManager>.I.ProceedDownload();
		}
	}
}
