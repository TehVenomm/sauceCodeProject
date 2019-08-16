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
		SetToggle((Enum)UI.TGL_DIRECTION, value: true);
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject lo_direction = loadingQueue.Load(RESOURCE_CATEGORY.UI, "SmithEquipDirection");
		int wait = 0;
		wait++;
		int npcId = (!StatusManager.IsUnique()) ? 4 : 36;
		NPCTable.NPCData npcData4 = Singleton<NPCTable>.I.GetNPCData(npcId);
		GameObject npcRoot4 = new GameObject("NPC");
		npcData4.LoadModel(npcRoot4, need_shadow: false, enable_light_probe: true, delegate
		{
			wait--;
		}, useSpecialModel: false);
		GameObject npcRoot3 = null;
		if (this is SmithAbilityChangePerformance || this is SmithAbilityItemPerformance)
		{
			wait++;
			NPCTable.NPCData nPCData = Singleton<NPCTable>.I.GetNPCData(3);
			npcRoot3 = new GameObject("NPC003");
			nPCData.LoadModel(npcRoot3, need_shadow: false, enable_light_probe: true, delegate
			{
				wait--;
			}, useSpecialModel: false);
		}
		int[] seIds2 = (int[])Enum.GetValues(typeof(SmithEquipDirector.AUDIO));
		int[] array = seIds2;
		foreach (int se_id in array)
		{
			loadingQueue.CacheSE(se_id);
		}
		seIds2 = (int[])Enum.GetValues(typeof(EquipResultBase.AUDIO));
		int[] array2 = seIds2;
		foreach (int se_id2 in array2)
		{
			loadingQueue.CacheSE(se_id2);
		}
		yield return loadingQueue.Wait();
		while (wait > 0)
		{
			yield return null;
		}
		Object directionObject = lo_direction.loadedObject;
		Transform directionTransform = ResourceUtility.Realizes(directionObject, MonoBehaviourSingleton<StageManager>.I.stageObject);
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
		this.StartCoroutine(DoEnd());
	}

	protected void EndDirectionUI()
	{
		SetToggle((Enum)UI.TGL_DIRECTION, value: false);
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
		if (Object.op_Implicit(director))
		{
			director.Reset();
			Object.Destroy(director.get_gameObject());
		}
	}

	protected override void OnDestroy()
	{
		if (Object.op_Implicit(director))
		{
			director.Reset();
			Object.Destroy(director.get_gameObject());
		}
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusStageManager>.I.SetUITextureActive(active: true);
		}
		base.OnDestroy();
	}
}
