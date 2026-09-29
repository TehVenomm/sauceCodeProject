// Decompiled with JetBrains decompiler
// Type: AccountLinkRobConflict
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class AccountLinkRobConflict : GameSection
{
  private const int gemIIC = 1;
  private const int goldIIC = 2;
  private bool _accountChangeSuccess;
  private object[] event_data;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    Network.UserInfo userInfo = MonoBehaviourSingleton<UserInfoManager>.I.userInfo;
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    Transform local = this.GetCtrl((Enum) AccountLinkRobConflict.UI.OBJ_LOCAL);
    this.SetLabelText(local, (Enum) AccountLinkRobConflict.UI.LBL_NAME, userInfo.name);
    this.SetLabelText(local, (Enum) AccountLinkRobConflict.UI.LBL_DATE, userInfo.lastLogin);
    this.SetLabelText(local, (Enum) AccountLinkRobConflict.UI.LBL_HUNTER_ID, userInfo.code);
    this.SetLabelText(local, (Enum) AccountLinkRobConflict.UI.LBL_LEVEL, $"Lv.{userStatus.level.ToString()}");
    this.SetLabelText(local, (Enum) AccountLinkRobConflict.UI.LBL_GEM, userStatus.crystal.ToString());
    this.SetLabelText(local, (Enum) AccountLinkRobConflict.UI.LBL_GOLD, userStatus.money.ToString());
    Transform cloud = this.GetCtrl((Enum) AccountLinkRobConflict.UI.OBJ_CLOUD);
    this.event_data = GameSection.GetEventData() as object[];
    if (this.event_data[0] is LinkRobModel.ExistInfoParam existInfoParam)
    {
      this.SetLabelText(cloud, (Enum) AccountLinkRobConflict.UI.LBL_NAME, existInfoParam.name);
      this.SetLabelText(cloud, (Enum) AccountLinkRobConflict.UI.LBL_DATE, existInfoParam.lastLogin);
      this.SetLabelText(cloud, (Enum) AccountLinkRobConflict.UI.LBL_HUNTER_ID, existInfoParam.code);
      this.SetLabelText(cloud, (Enum) AccountLinkRobConflict.UI.LBL_LEVEL, $"Lv.{existInfoParam.level}");
      this.SetLabelText(cloud, (Enum) AccountLinkRobConflict.UI.LBL_GEM, (object) existInfoParam.crystal);
      this.SetLabelText(cloud, (Enum) AccountLinkRobConflict.UI.LBL_GOLD, (object) existInfoParam.money);
    }
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_gem = loadingQueue.LoadItemIcon(ResourceName.GetItemIcon(1));
    LoadObject lo_gold = loadingQueue.LoadItemIcon(ResourceName.GetItemIcon(2));
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    this.SetTexture(local, (Enum) AccountLinkRobConflict.UI.TEX_GEM, lo_gem.loadedObject as Texture);
    this.SetTexture(local, (Enum) AccountLinkRobConflict.UI.TEX_GOLD, lo_gold.loadedObject as Texture);
    this.SetTexture(cloud, (Enum) AccountLinkRobConflict.UI.TEX_GEM, lo_gem.loadedObject as Texture);
    this.SetTexture(cloud, (Enum) AccountLinkRobConflict.UI.TEX_GOLD, lo_gold.loadedObject as Texture);
    base.Initialize();
  }

  private void OnQuery_CURRENT()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<AccountManager>.I.SendRegistIgnoreCloudData((string) this.event_data[1], (Action<bool>) (success =>
    {
      if (success)
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.FACEBOOK_LOGIN);
      GameSection.ResumeEvent(success);
    }));
  }

  private void OnQuery_CLOUD()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<AccountManager>.I.SendLinkRobWithCloudData((string) this.event_data[1], (Action<bool>) (success =>
    {
      if (success)
      {
        MenuReset.needClearCache = true;
        MenuReset.needPredownload = true;
      }
      GameSection.ResumeEvent(success);
    }));
  }

  private void OnCloseDialog_AccountUseLocalData() => this._accountChangeSuccess = true;

  public void Update()
  {
    if (!this._accountChangeSuccess || !MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
      return;
    this._accountChangeSuccess = false;
    this.RequestEvent("CURRENT_DATA");
    GameSection.BackSection();
  }

  private enum UI
  {
    OBJ_LOCAL,
    OBJ_CLOUD,
    LBL_NAME,
    LBL_DATE,
    LBL_HUNTER_ID,
    LBL_LEVEL,
    LBL_GEM,
    LBL_GOLD,
    TEX_GEM,
    TEX_GOLD,
  }
}
