// Decompiled with JetBrains decompiler
// Type: DownloadEnemyDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class DownloadEnemyDialog : GameSection
{
  private bool remove = true;

  protected override void OnOpen() => base.OnOpen();

  private void OnQuery_YES() => this.remove = true;

  private void OnQuery_NO() => this.remove = false;

  protected override void OnClose()
  {
    if (!this.remove && MonoBehaviourSingleton<AssetPreDownloadManager>.IsValid() && MonoBehaviourSingleton<AssetPreDownloadManager>.I.IsNewDownloadData() && !MonoBehaviourSingleton<AssetPreDownloadManager>.I.IsFinishDownload())
      MonoBehaviourSingleton<GameSceneManager>.I.AddHighForceChangeScene("EnemyDownload", "DownloadEnemy");
    base.OnClose();
  }

  public override void UpdateUI()
  {
  }

  private enum UI
  {
    LBL_FILESIZE_NUM,
  }
}
