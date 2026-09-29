// Decompiled with JetBrains decompiler
// Type: AccountChangeServerConflict
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AccountChangeServerConflict : GameSection
{
  private const int gemIIC = 1;
  private const int goldIIC = 2;
  private bool _accountChangeSuccess;
  private object[] event_data;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    this.event_data = GameSection.GetEventData() as object[];
    List<GetAllAccountsModel.ServerAccountInfo> serverAccountInfoList = this.event_data[0] as List<GetAllAccountsModel.ServerAccountInfo>;
    Transform local = this.GetCtrl((Enum) AccountChangeServerConflict.UI.OBJ_LOCAL);
    this.SetLabelText(local, (Enum) AccountChangeServerConflict.UI.LBL_NAME, serverAccountInfoList[0].userAccount.name);
    this.SetLabelText(local, (Enum) AccountChangeServerConflict.UI.LBL_DATE, serverAccountInfoList[0].userAccount.lastLogin);
    this.SetLabelText(local, (Enum) AccountChangeServerConflict.UI.LBL_HUNTER_ID, serverAccountInfoList[0].userAccount.code);
    this.SetLabelText(local, (Enum) AccountChangeServerConflict.UI.LBL_LEVEL, $"Lv.{serverAccountInfoList[0].level}");
    this.SetLabelText(local, (Enum) AccountChangeServerConflict.UI.LBL_GEM, (object) serverAccountInfoList[0].crystal);
    this.SetLabelText(local, (Enum) AccountChangeServerConflict.UI.LBL_GOLD, (object) serverAccountInfoList[0].money);
    object obj = this.event_data[1];
    Transform cloud = this.GetCtrl((Enum) AccountChangeServerConflict.UI.OBJ_CLOUD);
    this.SetLabelText(cloud, (Enum) AccountChangeServerConflict.UI.LBL_NAME, serverAccountInfoList[1].userAccount.name);
    this.SetLabelText(cloud, (Enum) AccountChangeServerConflict.UI.LBL_DATE, serverAccountInfoList[1].userAccount.lastLogin);
    this.SetLabelText(cloud, (Enum) AccountChangeServerConflict.UI.LBL_HUNTER_ID, serverAccountInfoList[1].userAccount.code);
    this.SetLabelText(cloud, (Enum) AccountChangeServerConflict.UI.LBL_LEVEL, $"Lv.{serverAccountInfoList[1].level}");
    this.SetLabelText(cloud, (Enum) AccountChangeServerConflict.UI.LBL_GEM, (object) serverAccountInfoList[1].crystal);
    this.SetLabelText(cloud, (Enum) AccountChangeServerConflict.UI.LBL_GOLD, (object) serverAccountInfoList[1].money);
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_gem = loadingQueue.LoadItemIcon(ResourceName.GetItemIcon(1));
    LoadObject lo_gold = loadingQueue.LoadItemIcon(ResourceName.GetItemIcon(2));
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    this.SetTexture(local, (Enum) AccountChangeServerConflict.UI.TEX_GEM, lo_gem.loadedObject as Texture);
    this.SetTexture(local, (Enum) AccountChangeServerConflict.UI.TEX_GOLD, lo_gold.loadedObject as Texture);
    this.SetTexture(cloud, (Enum) AccountChangeServerConflict.UI.TEX_GEM, lo_gem.loadedObject as Texture);
    this.SetTexture(cloud, (Enum) AccountChangeServerConflict.UI.TEX_GOLD, lo_gold.loadedObject as Texture);
    base.Initialize();
  }

  private void OnQuery_ACCOUNT_1()
  {
    this.DoChangeServer((this.event_data[0] as List<GetAllAccountsModel.ServerAccountInfo>)[0]);
  }

  private void OnQuery_ACCOUNT_2()
  {
    this.DoChangeServer((this.event_data[0] as List<GetAllAccountsModel.ServerAccountInfo>)[1]);
  }

  private void DoChangeServer(
    GetAllAccountsModel.ServerAccountInfo serverAccountInfo)
  {
    ServerListTable.ServerData server = this.event_data[1] as ServerListTable.ServerData;
    MonoBehaviourSingleton<AppMain>.I.email = serverAccountInfo.email;
    MonoBehaviourSingleton<AppMain>.I.fbId = serverAccountInfo.fbId;
    MonoBehaviourSingleton<AppMain>.I.uid = serverAccountInfo.uid;
    MonoBehaviourSingleton<AppMain>.I.currentServer = server;
    GameSaveData.instance.SetCurrentServer(server);
    MonoBehaviourSingleton<AppMain>.I.Reset();
    this.RefreshUI();
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
