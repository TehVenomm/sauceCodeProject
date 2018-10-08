using System.Collections;
using UnityEngine;

public class JackpotWinDialog : GameSection
{
	private enum UI
	{
		TEX_NPCMODEL_PAMELA,
		TEX_NPCMODEL_DRAGON
	}

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		UpdateNPC();
		yield return (object)null;
		base.Initialize();
	}

	private void UpdateNPC()
	{
		SetRenderNPCModel(UI.TEX_NPCMODEL_PAMELA, 0, new Vector3(0.4f, -1.3f, 6.87f), new Vector3(0f, -157.64f, 0f), MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCFOV, delegate
		{
		});
		SetRenderNPCModel(UI.TEX_NPCMODEL_DRAGON, 6, new Vector3(-0.1f, -2.18f, 12f), new Vector3(0f, 169f, 0f), MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCFOV, delegate(NPCLoader loader)
		{
			loader.GetAnimator().Play("EVENT_IDLE_NO_BALLOON");
		});
	}

	public void OnQuery_SHARE()
	{
	}
}
