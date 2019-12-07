using System.Collections;
using UnityEngine;

public class ConfigServer : GameSection
{
	private enum UI
	{
		TGL_SERVER_1,
		TGL_SERVER_2,
		TGL_SERVER_3,
		TGL_SERVER_4,
		SERVER_NOTE,
		DSV_ROOT
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void Exit()
	{
		GameSaveData.Save();
		base.Exit();
	}

	public override void UpdateUI()
	{
		Transform ctrl = GetCtrl(UI.DSV_ROOT);
		for (int i = 0; i < Singleton<ServerListTable>.I.GetActiveServerList().Count; i++)
		{
			Transform child = ctrl.GetChild(i);
			child.gameObject.SetActive(value: true);
			ServerListTable.ServerData serverData = Singleton<ServerListTable>.I.GetActiveServerList()[i];
			SetToggle((UI)i, GameSaveData.instance.currentServer.id == serverData.id);
			Component[] componentsInChildren = child.GetComponentsInChildren(typeof(UILabel), includeInactive: true);
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				componentsInChildren[j].GetComponent<UILabel>().SetTextOnly(serverData.name);
			}
			if (!string.IsNullOrEmpty(serverData.note))
			{
				SetActive(child, UI.SERVER_NOTE, is_visible: true);
				SetLabelText(child, UI.SERVER_NOTE, serverData.note);
				SetSupportEncoding(child, UI.SERVER_NOTE, isEnable: true);
			}
		}
	}

	private void OnQuery_SEVER_1()
	{
		StartCoroutine(DoChangeServer(0));
	}

	private void OnQuery_SEVER_2()
	{
		StartCoroutine(DoChangeServer(1));
	}

	private void OnQuery_SEVER_3()
	{
		StartCoroutine(DoChangeServer(2));
	}

	private void OnQuery_SEVER_4()
	{
		StartCoroutine(DoChangeServer(3));
	}

	private IEnumerator DoChangeServer(int btnId)
	{
		if (GameSaveData.instance.currentServer.id != Singleton<ServerListTable>.I.GetActiveServerList()[btnId].id)
		{
			AccountManager.ResetAccount();
			GameSaveData.instance.SetCurrentServer(Singleton<ServerListTable>.I.GetActiveServerList()[btnId]);
			MonoBehaviourSingleton<AppMain>.I.Reset();
			RefreshUI();
		}
		else
		{
			GameSection.BackSection();
		}
		yield break;
	}
}
