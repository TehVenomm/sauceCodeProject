using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountChangeServerConflict : GameSection
{
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
		TEX_GOLD
	}

	private const int gemIIC = 1;

	private const int goldIIC = 2;

	private bool _accountChangeSuccess;

	private object[] event_data;

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		event_data = (GameSection.GetEventData() as object[]);
		List<GetAllAccountsModel.ServerAccountInfo> list = event_data[0] as List<GetAllAccountsModel.ServerAccountInfo>;
		Transform local = GetCtrl(UI.OBJ_LOCAL);
		SetLabelText(local, UI.LBL_NAME, list[0].userAccount.name);
		SetLabelText(local, UI.LBL_DATE, list[0].userAccount.lastLogin);
		SetLabelText(local, UI.LBL_HUNTER_ID, list[0].userAccount.code);
		SetLabelText(local, UI.LBL_LEVEL, $"Lv.{list[0].level}");
		SetLabelText(local, UI.LBL_GEM, list[0].crystal);
		SetLabelText(local, UI.LBL_GOLD, list[0].money);
		_ = event_data[1];
		Transform cloud = GetCtrl(UI.OBJ_CLOUD);
		SetLabelText(cloud, UI.LBL_NAME, list[1].userAccount.name);
		SetLabelText(cloud, UI.LBL_DATE, list[1].userAccount.lastLogin);
		SetLabelText(cloud, UI.LBL_HUNTER_ID, list[1].userAccount.code);
		SetLabelText(cloud, UI.LBL_LEVEL, $"Lv.{list[1].level}");
		SetLabelText(cloud, UI.LBL_GEM, list[1].crystal);
		SetLabelText(cloud, UI.LBL_GOLD, list[1].money);
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject lo_gem = loadingQueue.LoadItemIcon(ResourceName.GetItemIcon(1));
		LoadObject lo_gold = loadingQueue.LoadItemIcon(ResourceName.GetItemIcon(2));
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		SetTexture(local, UI.TEX_GEM, lo_gem.loadedObject as Texture);
		SetTexture(local, UI.TEX_GOLD, lo_gold.loadedObject as Texture);
		SetTexture(cloud, UI.TEX_GEM, lo_gem.loadedObject as Texture);
		SetTexture(cloud, UI.TEX_GOLD, lo_gold.loadedObject as Texture);
		base.Initialize();
	}

	private void OnQuery_ACCOUNT_1()
	{
		List<GetAllAccountsModel.ServerAccountInfo> list = event_data[0] as List<GetAllAccountsModel.ServerAccountInfo>;
		DoChangeServer(list[0]);
	}

	private void OnQuery_ACCOUNT_2()
	{
		List<GetAllAccountsModel.ServerAccountInfo> list = event_data[0] as List<GetAllAccountsModel.ServerAccountInfo>;
		DoChangeServer(list[1]);
	}

	private void DoChangeServer(GetAllAccountsModel.ServerAccountInfo serverAccountInfo)
	{
		ServerListTable.ServerData currentServer = event_data[1] as ServerListTable.ServerData;
		MonoBehaviourSingleton<AppMain>.I.email = serverAccountInfo.email;
		MonoBehaviourSingleton<AppMain>.I.fbId = serverAccountInfo.fbId;
		MonoBehaviourSingleton<AppMain>.I.uid = serverAccountInfo.uid;
		MonoBehaviourSingleton<AppMain>.I.currentServer = currentServer;
		GameSaveData.instance.SetCurrentServer(currentServer);
		MonoBehaviourSingleton<AppMain>.I.Reset();
		RefreshUI();
	}
}
