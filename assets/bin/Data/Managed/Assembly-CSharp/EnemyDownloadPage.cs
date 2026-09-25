// Decompiled with JetBrains decompiler
// Type: EnemyDownloadPage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class EnemyDownloadPage : GameSection
{
  private bool isActive;
  private bool remove;
  private GameObject titleObjectRoot;
  private GameObject secondCamRoot;
  private Camera secondCam;

  private void OnQuery_CANCEL()
  {
    if (MonoBehaviourSingleton<AssetPreDownloadManager>.IsValid())
    {
      if (MonoBehaviourSingleton<AssetPreDownloadManager>.I.CurrentState == AssetPreDownloadManager.DownloadState.Downloading)
        MonoBehaviourSingleton<AssetPreDownloadManager>.I.Stop();
      MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(this.sectionData.sectionName);
      GameSection.ChangeEvent("[BACK]");
      if (MonoBehaviourSingleton<AssetPreDownloadManager>.I.BackScene == "ClanScene")
        this.OnQuery_MAIN_MENU_CLAN();
      else if (MonoBehaviourSingleton<AssetPreDownloadManager>.I.BackScene == "Lounge")
        this.OnQuery_MAIN_MENU_LOUNGE();
    }
    this.remove = false;
  }

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  public override void UpdateUI()
  {
    if (MonoBehaviourSingleton<AssetPreDownloadManager>.I.CurrentState == AssetPreDownloadManager.DownloadState.Checking)
    {
      this.SetActive((Enum) EnemyDownloadPage.UI.LBL_CHECKING, true);
      this.SetActive((Enum) EnemyDownloadPage.UI.LBL_DOWNLOAD, false);
      this.SetActive((Enum) EnemyDownloadPage.UI.LBL_DOWNLOAD_NUM, true);
      this.SetActive((Enum) EnemyDownloadPage.UI.PROCESS_SL, true);
      this.SetLabelText((Enum) EnemyDownloadPage.UI.LBL_DOWNLOAD_NUM, $"{(object) MonoBehaviourSingleton<AssetPreDownloadManager>.I.checkedCount}/{(object) MonoBehaviourSingleton<AssetPreDownloadManager>.I.totalCheckCount}");
      this.SetSliderValue((Enum) EnemyDownloadPage.UI.PROCESS_SL, this.ProcessValue(MonoBehaviourSingleton<AssetPreDownloadManager>.I.checkedCount, MonoBehaviourSingleton<AssetPreDownloadManager>.I.totalCheckCount));
    }
    else if (MonoBehaviourSingleton<AssetPreDownloadManager>.I.CurrentState == AssetPreDownloadManager.DownloadState.Downloading || MonoBehaviourSingleton<AssetPreDownloadManager>.I.CurrentState == AssetPreDownloadManager.DownloadState.End)
    {
      this.SetActive((Enum) EnemyDownloadPage.UI.LBL_CHECKING, false);
      this.SetActive((Enum) EnemyDownloadPage.UI.LBL_DOWNLOAD, true);
      this.SetActive((Enum) EnemyDownloadPage.UI.LBL_DOWNLOAD_NUM, true);
      this.SetActive((Enum) EnemyDownloadPage.UI.PROCESS_SL, true);
      this.SetLabelText((Enum) EnemyDownloadPage.UI.LBL_DOWNLOAD_NUM, $"{(object) MonoBehaviourSingleton<AssetPreDownloadManager>.I.loadedCount}/{(object) MonoBehaviourSingleton<AssetPreDownloadManager>.I.totalCount}");
      this.SetSliderValue((Enum) EnemyDownloadPage.UI.PROCESS_SL, this.ProcessValue(MonoBehaviourSingleton<AssetPreDownloadManager>.I.loadedCount, MonoBehaviourSingleton<AssetPreDownloadManager>.I.totalCount));
    }
    else
    {
      this.SetActive((Enum) EnemyDownloadPage.UI.LBL_CHECKING, true);
      this.SetActive((Enum) EnemyDownloadPage.UI.LBL_DOWNLOAD, false);
      this.SetActive((Enum) EnemyDownloadPage.UI.LBL_DOWNLOAD_NUM, false);
      this.SetActive((Enum) EnemyDownloadPage.UI.PROCESS_SL, false);
    }
    if (MonoBehaviourSingleton<AssetPreDownloadManager>.I.CurrentState != AssetPreDownloadManager.DownloadState.End)
      return;
    this.OnFinishDownload();
  }

  private float ProcessValue(int a, int b)
  {
    return b <= 0 ? 1f : Mathf.Clamp((float) a / (float) b, 0.0f, 1f);
  }

  protected override void OnOpen()
  {
    base.OnOpen();
    if (this.isActive)
      return;
    MonoBehaviourSingleton<AssetPreDownloadManager>.I.AddListenerCheck(new AssetPreDownloadManager.OnUpdateCheck(this.UpdateChecking), new AssetPreDownloadManager.OnFinishCheck(this.StopChecking));
    MonoBehaviourSingleton<AssetPreDownloadManager>.I.AddListenerDownload(new AssetPreDownloadManager.OnUpdateDownload(this.UpdateDownload), new AssetPreDownloadManager.OnStopDownload(this.OnStopDownload), new AssetPreDownloadManager.OnFinishDownload(this.OnFinishDownload));
    this.isActive = true;
  }

  protected override void OnClose()
  {
    if (this.isActive && MonoBehaviourSingleton<AssetPreDownloadManager>.IsValid())
    {
      MonoBehaviourSingleton<AssetPreDownloadManager>.I.RemoveListenerCheck(new AssetPreDownloadManager.OnUpdateCheck(this.UpdateChecking), new AssetPreDownloadManager.OnFinishCheck(this.StopChecking));
      MonoBehaviourSingleton<AssetPreDownloadManager>.I.RemoveListenerDownload(new AssetPreDownloadManager.OnUpdateDownload(this.UpdateDownload), new AssetPreDownloadManager.OnStopDownload(this.OnStopDownload), new AssetPreDownloadManager.OnFinishDownload(this.OnFinishDownload));
    }
    if (Object.op_Inequality((Object) this.titleObjectRoot, (Object) null))
    {
      Object.Destroy((Object) this.titleObjectRoot);
      this.titleObjectRoot = (GameObject) null;
    }
    if (Object.op_Inequality((Object) this.secondCamRoot, (Object) null))
    {
      if (Object.op_Inequality((Object) this.secondCam, (Object) null))
        Object.Destroy((Object) this.secondCam);
      Object.Destroy((Object) this.secondCamRoot);
      this.secondCamRoot = (GameObject) null;
    }
    this.isActive = false;
    if (!this.remove && MonoBehaviourSingleton<AssetPreDownloadManager>.IsValid() && MonoBehaviourSingleton<AssetPreDownloadManager>.I.IsNewDownloadData() && !MonoBehaviourSingleton<AssetPreDownloadManager>.I.IsFinishDownload())
      MonoBehaviourSingleton<GameSceneManager>.I.AddHighForceChangeScene("EnemyDownload", "DownloadEnemy");
    base.OnClose();
  }

  protected override void OnDestroy()
  {
    if (this.isActive && MonoBehaviourSingleton<AssetPreDownloadManager>.IsValid())
    {
      MonoBehaviourSingleton<AssetPreDownloadManager>.I.RemoveListenerCheck(new AssetPreDownloadManager.OnUpdateCheck(this.UpdateChecking), new AssetPreDownloadManager.OnFinishCheck(this.StopChecking));
      MonoBehaviourSingleton<AssetPreDownloadManager>.I.RemoveListenerDownload(new AssetPreDownloadManager.OnUpdateDownload(this.UpdateDownload), new AssetPreDownloadManager.OnStopDownload(this.OnStopDownload), new AssetPreDownloadManager.OnFinishDownload(this.OnFinishDownload));
    }
    this.isActive = false;
    base.OnDestroy();
  }

  private IEnumerator DoInitialize()
  {
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    LoadObject loadedTitleObj = load_queue.Load(RESOURCE_CATEGORY.CUTSCENE, "Title");
    while (load_queue.IsLoading())
      yield return (object) null;
    this.titleObjectRoot = ResourceUtility.Instantiate<Object>(loadedTitleObj.loadedObject) as GameObject;
    this.titleObjectRoot.transform.parent = ((Component) MonoBehaviourSingleton<AppMain>.I).transform;
    this.secondCamRoot = new GameObject();
    this.secondCamRoot.transform.parent = ((Component) MonoBehaviourSingleton<AppMain>.I).transform;
    this.secondCamRoot.transform.position = new Vector3(0.0f, 0.0f, -30f);
    this.secondCam = this.secondCamRoot.AddComponent<Camera>();
    this.secondCam.clearFlags = (CameraClearFlags) 2;
    this.secondCam.backgroundColor = new Color(0.0f, 0.0f, 0.0f, 5f);
    this.secondCam.orthographic = true;
    this.secondCam.orthographicSize = 4f;
    this.secondCam.nearClipPlane = 0.3f;
    this.secondCam.farClipPlane = 50f;
    this.secondCam.depth = -1f;
    base.Initialize();
    if (!this.isActive)
    {
      MonoBehaviourSingleton<AssetPreDownloadManager>.I.AddListenerCheck(new AssetPreDownloadManager.OnUpdateCheck(this.UpdateChecking), new AssetPreDownloadManager.OnFinishCheck(this.StopChecking));
      MonoBehaviourSingleton<AssetPreDownloadManager>.I.AddListenerDownload(new AssetPreDownloadManager.OnUpdateDownload(this.UpdateDownload), new AssetPreDownloadManager.OnStopDownload(this.OnStopDownload), new AssetPreDownloadManager.OnFinishDownload(this.OnFinishDownload));
      this.isActive = true;
    }
    if (MonoBehaviourSingleton<AssetPreDownloadManager>.I.IsReadyDownload())
      MonoBehaviourSingleton<AssetPreDownloadManager>.I.Download();
  }

  private void SetActiveUI(bool enable)
  {
    ((Component) this.GetCtrl((Enum) EnemyDownloadPage.UI.Container)).gameObject.SetActive(enable);
  }

  private void UpdateDownload() => this.UpdateUI();

  private void StopDownload(bool succeed)
  {
    this.remove = succeed;
    MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(this.sectionData.sectionName);
    GameSection.ChangeEvent("[BACK]");
    if (MonoBehaviourSingleton<AssetPreDownloadManager>.I.BackScene == "ClanScene")
      this.OnQuery_MAIN_MENU_CLAN();
    else if (MonoBehaviourSingleton<AssetPreDownloadManager>.I.BackScene == "Lounge")
      this.OnQuery_MAIN_MENU_LOUNGE();
    else
      this.OnQuery_MAIN_MENU_HOME();
  }

  private void UpdateChecking() => this.UpdateUI();

  private void StopChecking(bool avai)
  {
    if (avai && MonoBehaviourSingleton<AssetPreDownloadManager>.I.CurrentState == AssetPreDownloadManager.DownloadState.Ready)
    {
      MonoBehaviourSingleton<AssetPreDownloadManager>.I.Download();
    }
    else
    {
      if (avai)
        return;
      this.StopDownload(true);
    }
  }

  private void OnStopDownload() => this.StopDownload(false);

  private void OnFinishDownload() => this.StopDownload(true);

  private enum UI
  {
    TEX_BG,
    Container,
    LBL_DOWNLOAD_NUM,
    LBL_CHECKING,
    LBL_DOWNLOAD,
    PROCESS_SL,
  }
}
