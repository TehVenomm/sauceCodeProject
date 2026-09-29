// Decompiled with JetBrains decompiler
// Type: DownloadEnemyProcess
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class DownloadEnemyProcess : GameSection
{
  private bool isActive;

  private void OnQuery_CANCEL()
  {
    Debug.Log((object) "Stop Download");
    EnemyPredownloadManager.Stop();
  }

  public override void UpdateUI()
  {
    if (MonoBehaviourSingleton<EnemyPredownloadManager>.I.CurrentState == EnemyPredownloadManager.DownloadState.CheckingFile)
      this.SetLabelText((Enum) DownloadEnemyProcess.UI.LBL_DOWNLOAD_NUM, $"{(object) MonoBehaviourSingleton<EnemyPredownloadManager>.I.checkedCount}/{(object) MonoBehaviourSingleton<EnemyPredownloadManager>.I.totalPackage}");
    else
      this.SetLabelText((Enum) DownloadEnemyProcess.UI.LBL_DOWNLOAD_NUM, $"{(object) MonoBehaviourSingleton<EnemyPredownloadManager>.I.loadedCount}/{(object) MonoBehaviourSingleton<EnemyPredownloadManager>.I.totalCount}");
    this.SetSliderValue((Enum) DownloadEnemyProcess.UI.PROCESS_SL, this.ProcessValue());
  }

  private float ProcessValue()
  {
    return MonoBehaviourSingleton<EnemyPredownloadManager>.I.totalCount <= 0 ? 1f : Mathf.Clamp((float) MonoBehaviourSingleton<EnemyPredownloadManager>.I.loadedCount / (float) MonoBehaviourSingleton<EnemyPredownloadManager>.I.totalCount, 0.0f, 1f);
  }

  protected override void OnOpen()
  {
    base.OnOpen();
    if (!this.isActive)
    {
      MonoBehaviourSingleton<EnemyPredownloadManager>.I.AddListenerCheck(new EnemyPredownloadManager.OnCheckDownload(this.UpdateChecking), new EnemyPredownloadManager.OnCheckDownloadFinish(this.StopChecking));
      MonoBehaviourSingleton<EnemyPredownloadManager>.I.AddListenerDownload(new EnemyPredownloadManager.OnDownLoadPackage(this.UpdateDownload), new EnemyPredownloadManager.OnStopDownloadPackage(this.StopDownload));
    }
    this.isActive = true;
  }

  protected override void OnClose()
  {
    if (this.isActive && MonoBehaviourSingleton<EnemyPredownloadManager>.IsValid())
    {
      MonoBehaviourSingleton<EnemyPredownloadManager>.I.RemoveListenerCheck(new EnemyPredownloadManager.OnCheckDownload(this.UpdateChecking), new EnemyPredownloadManager.OnCheckDownloadFinish(this.StopChecking));
      MonoBehaviourSingleton<EnemyPredownloadManager>.I.RemoveListenerDownload(new EnemyPredownloadManager.OnDownLoadPackage(this.UpdateDownload), new EnemyPredownloadManager.OnStopDownloadPackage(this.StopDownload));
    }
    this.isActive = false;
    base.OnClose();
  }

  protected override void OnDestroy()
  {
    if (this.isActive && MonoBehaviourSingleton<EnemyPredownloadManager>.IsValid())
    {
      MonoBehaviourSingleton<EnemyPredownloadManager>.I.RemoveListenerCheck(new EnemyPredownloadManager.OnCheckDownload(this.UpdateChecking), new EnemyPredownloadManager.OnCheckDownloadFinish(this.StopChecking));
      MonoBehaviourSingleton<EnemyPredownloadManager>.I.RemoveListenerDownload(new EnemyPredownloadManager.OnDownLoadPackage(this.UpdateDownload), new EnemyPredownloadManager.OnStopDownloadPackage(this.StopDownload));
    }
    this.isActive = false;
    base.OnDestroy();
  }

  private void UpdateChecking() => this.UpdateUI();

  private void UpdateDownload() => this.UpdateUI();

  private void StopChecking(bool avai)
  {
  }

  private void StopDownload(bool succeed)
  {
    MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(this.sectionData.sectionName);
    GameSection.BackSection();
  }

  private enum UI
  {
    LBL_DOWNLOAD_NUM,
    PROCESS_SL,
  }
}
