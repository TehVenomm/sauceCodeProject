// Decompiled with JetBrains decompiler
// Type: HomeSceneBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class HomeSceneBase : GameSection
{
  private bool AssetPredownloadActive;

  public override void Initialize()
  {
    UILabel.OutlineLimit = false;
    this.AssetPredownloadActive = false;
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    bool wait = true;
    MonoBehaviourSingleton<OnceManager>.I.SendGetOnce((Action<bool>) (b => wait = false));
    while (wait)
      yield return (object) null;
    MonoBehaviourSingleton<QuestManager>.I.SetClearStatus();
    MonoBehaviourSingleton<DeliveryManager>.I.SetList();
    MonoBehaviourSingleton<WorldMapManager>.I.SetWorldMapTraveledList();
    MonoBehaviourSingleton<BlackListManager>.I.SetAllList();
    MonoBehaviourSingleton<AchievementManager>.I.SetAchievement();
    MonoBehaviourSingleton<GuildRequestManager>.I.SetList();
    MonoBehaviourSingleton<WorldMapManager>.I.SetReleasedRegion();
    MonoBehaviourSingleton<NativeGameService>.I.FixAchievement();
    MonoBehaviourSingleton<HelpshiftManager>.I.OnStart(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.code, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.advancedUserMail, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
    base.Initialize();
    yield return (object) this.SetAssetPredownloadCallBack();
  }

  public override void Exit()
  {
    MonoBehaviourSingleton<StatusManager>.I.InitStatusEquipData();
    base.Exit();
  }

  protected override void OnClose()
  {
    if (this.AssetPredownloadActive)
    {
      MonoBehaviourSingleton<AssetPreDownloadManager>.I.RemoveListenerCheck(new AssetPreDownloadManager.OnUpdateCheck(this.OnUpdateCheckDownloadEnemyData), new AssetPreDownloadManager.OnFinishCheck(this.OnFinishCheck));
      this.AssetPredownloadActive = false;
    }
    base.OnClose();
  }

  protected override void OnOpen()
  {
    if (!this.AssetPredownloadActive)
      this.StartCoroutine(this.SetAssetPredownloadCallBack());
    base.OnOpen();
  }

  private void OnUpdateCheckDownloadEnemyData()
  {
  }

  private IEnumerator SetAssetPredownloadCallBack()
  {
    if (!this.AssetPredownloadActive)
    {
      this.AssetPredownloadActive = true;
      while (!MonoBehaviourSingleton<AssetPreDownloadManager>.IsValid())
        yield return (object) null;
      if (!((Behaviour) MonoBehaviourSingleton<AssetPreDownloadManager>.I).enabled)
      {
        ((Behaviour) MonoBehaviourSingleton<AssetPreDownloadManager>.I).enabled = true;
        yield return (object) new WaitForSeconds(0.5f);
      }
      while (!MonoBehaviourSingleton<AssetPreDownloadManager>.I.isAvailable)
        yield return (object) null;
      if (!MonoBehaviourSingleton<AssetPreDownloadManager>.I.IsFinishDownload())
        MonoBehaviourSingleton<AssetPreDownloadManager>.I.AddListenerCheck(new AssetPreDownloadManager.OnUpdateCheck(this.OnUpdateCheckDownloadEnemyData), new AssetPreDownloadManager.OnFinishCheck(this.OnFinishCheck));
      if (MonoBehaviourSingleton<AssetPreDownloadManager>.I.IsWaitCheckVersion())
      {
        GameSceneManager.LockChangeScene(true);
        while (MonoBehaviourSingleton<AssetPreDownloadManager>.I.IsWaitCheckVersion())
          yield return (object) null;
        GameSceneManager.LockChangeScene(false);
      }
      if (!MonoBehaviourSingleton<AssetPreDownloadManager>.I.IsFinishDownload())
      {
        if (MonoBehaviourSingleton<AssetPreDownloadManager>.I.IsNewDownloadData())
          this.SetForceDownload(true);
      }
      else
        this.OnFinishCheck(false);
    }
  }

  private void OnFinishCheck(bool avai)
  {
    if (avai)
    {
      this.SetForceDownload(true);
    }
    else
    {
      MonoBehaviourSingleton<GameSceneManager>.I.RemoveForceChangeSceneAll();
      this.AssetPredownloadActive = false;
    }
  }

  private void SetForceDownload(bool enable)
  {
    if (enable)
    {
      MonoBehaviourSingleton<GameSceneManager>.I.RemoveForceChangeSceneAll();
      MonoBehaviourSingleton<GameSceneManager>.I.AddHighForceChangeScene("EnemyDownload", "DownloadEnemy");
    }
    else
      MonoBehaviourSingleton<GameSceneManager>.I.RemoveForceChangeScene("EnemyDownload", "DownloadEnemy");
  }
}
