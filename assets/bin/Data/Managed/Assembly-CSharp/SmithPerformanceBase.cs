using System;
using System.Collections;
using UnityEngine;

public class SmithPerformanceBase : GameSection
{
	private enum UI
	{
		TGL_DIRECTION
	}

	private object resultData;

	protected SmithEquipDirector director;

	public override void Initialize()
	{
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusStageManager>.I.SetUITextureActive(active: false);
		}
		object obj = resultData = GameSection.GetEventData();
		SetToggle(UI.TGL_DIRECTION, value: true);
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject lo_direction = loadingQueue.Load(RESOURCE_CATEGORY.UI, "SmithEquipDirection");
		int wait = 0;
		wait++;
		int npc_id = StatusManager.IsUnique() ? 36 : 4;
		NPCTable.NPCData nPCData = Singleton<NPCTable>.I.GetNPCData(npc_id);
		GameObject npcRoot4 = new GameObject("NPC");
		nPCData.LoadModel(npcRoot4, need_shadow: false, enable_light_probe: true, delegate
		{
			wait--;
		}, useSpecialModel: false);
		GameObject npcRoot3 = null;
		if (this is SmithAbilityChangePerformance || this is SmithAbilityItemPerformance)
		{
			wait++;
			NPCTable.NPCData nPCData2 = Singleton<NPCTable>.I.GetNPCData(3);
			npcRoot3 = new GameObject("NPC003");
			nPCData2.LoadModel(npcRoot3, need_shadow: false, enable_light_probe: true, delegate
			{
				wait--;
			}, useSpecialModel: false);
		}
		int[] array = (int[])Enum.GetValues(typeof(SmithEquipDirector.AUDIO));
		foreach (int se_id in array)
		{
			loadingQueue.CacheSE(se_id);
		}
		array = (int[])Enum.GetValues(typeof(EquipResultBase.AUDIO));
		foreach (int se_id2 in array)
		{
			loadingQueue.CacheSE(se_id2);
		}
		yield return loadingQueue.Wait();
		while (wait > 0)
		{
			yield return null;
		}
		Transform transform = ResourceUtility.Realizes(lo_direction.loadedObject, MonoBehaviourSingleton<StageManager>.I.stageObject);
		director = transform.GetComponent<SmithEquipDirector>();
		director.SetNPC004(npcRoot4);
		director.SetNPC003(npcRoot3);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
	}

	public override void Exit()
	{
		base.Exit();
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusStageManager>.I.SetUITextureActive(active: true);
		}
	}

	protected void OnQuery_SKIP()
	{
		if (director.isPlaying)
		{
			director.Skip();
		}
		GameSection.SetEventData(resultData);
	}

	protected virtual void OnEndDirection()
	{
		StartCoroutine(DoEnd());
	}

	protected void EndDirectionUI()
	{
		SetToggle(UI.TGL_DIRECTION, value: false);
	}

	private IEnumerator DoEnd()
	{
		yield return MonoBehaviourSingleton<TransitionManager>.I.Out(TransitionManager.TYPE.WHITE);
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusStageManager>.I.SetEnableSmithCharacterActivate(active: true);
		}
		EndDirectionUI();
		DispatchEvent("SKIP");
		if ((bool)director)
		{
			director.Reset();
			UnityEngine.Object.Destroy(director.gameObject);
		}
	}

	protected override void OnDestroy()
	{
		if ((bool)director)
		{
			director.Reset();
			UnityEngine.Object.Destroy(director.gameObject);
		}
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusStageManager>.I.SetUITextureActive(active: true);
		}
		base.OnDestroy();
	}
}
