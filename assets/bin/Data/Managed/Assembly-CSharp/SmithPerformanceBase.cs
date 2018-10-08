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
			MonoBehaviourSingleton<StatusStageManager>.I.SetUITextureActive(false);
		}
		object obj = resultData = GameSection.GetEventData();
		SetToggle(UI.TGL_DIRECTION, true);
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject lo_direction = loadingQueue.Load(RESOURCE_CATEGORY.UI, "SmithEquipDirection", false);
		int wait2 = 0;
		wait2++;
		NPCTable.NPCData npcData4 = Singleton<NPCTable>.I.GetNPCData(4);
		GameObject npcRoot4 = new GameObject("NPC");
		npcData4.LoadModel(npcRoot4, false, true, delegate
		{
			((_003CDoInitialize_003Ec__Iterator159)/*Error near IL_0093: stateMachine*/)._003Cwait_003E__2--;
		}, false);
		GameObject npcRoot3 = null;
		if (this is SmithAbilityChangePerformance || this is SmithAbilityItemPerformance)
		{
			wait2++;
			NPCTable.NPCData npcData3 = Singleton<NPCTable>.I.GetNPCData(3);
			npcRoot3 = new GameObject("NPC003");
			npcData3.LoadModel(npcRoot3, false, true, delegate
			{
				((_003CDoInitialize_003Ec__Iterator159)/*Error near IL_011c: stateMachine*/)._003Cwait_003E__2--;
			}, false);
		}
		int[] seIds2 = (int[])Enum.GetValues(typeof(SmithEquipDirector.AUDIO));
		int[] array = seIds2;
		foreach (int seId in array)
		{
			loadingQueue.CacheSE(seId, null);
		}
		seIds2 = (int[])Enum.GetValues(typeof(EquipResultBase.AUDIO));
		int[] array2 = seIds2;
		foreach (int seId2 in array2)
		{
			loadingQueue.CacheSE(seId2, null);
		}
		yield return (object)loadingQueue.Wait();
		while (wait2 > 0)
		{
			yield return (object)null;
		}
		UnityEngine.Object directionObject = lo_direction.loadedObject;
		Transform directionTransform = ResourceUtility.Realizes(directionObject, MonoBehaviourSingleton<StageManager>.I.stageObject, -1);
		director = directionTransform.GetComponent<SmithEquipDirector>();
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
			MonoBehaviourSingleton<StatusStageManager>.I.SetUITextureActive(true);
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
		SetToggle(UI.TGL_DIRECTION, false);
	}

	private IEnumerator DoEnd()
	{
		yield return (object)MonoBehaviourSingleton<TransitionManager>.I.Out(TransitionManager.TYPE.WHITE);
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusStageManager>.I.SetSmithCharacterActivateFlag(true);
		}
		EndDirectionUI();
		DispatchEvent("SKIP", null);
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
			MonoBehaviourSingleton<StatusStageManager>.I.SetUITextureActive(true);
		}
		base.OnDestroy();
	}
}
