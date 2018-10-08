using System;
using System.Collections;
using UnityEngine;

public class SmithGrowSkillPerformance : GameSection
{
	private enum UI
	{
		STR_RESULT,
		TGL_DIRECTION,
		LBL_GET_EXP,
		OBJ_GREAT
	}

	public enum AUDIO
	{
		NORMAL_EFFECT = 40000065,
		GREAT_EFFECT
	}

	private SmithManager.ResultData resultData;

	private bool isGreat;

	private bool isExceed;

	private SkillItemInfo[] materials;

	private bool shouldShowGreatEffect;

	private SkillGrowDirector director;

	private ItemLoader magiLoader;

	private ItemLoader magiSymbolLoader;

	public override void Initialize()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		object[] array = GameSection.GetEventData() as object[];
		resultData = (array[0] as SmithManager.ResultData);
		isGreat = (bool)array[1];
		materials = (array[2] as SkillItemInfo[]);
		isExceed = (bool)array[3];
		SetToggle((Enum)UI.TGL_DIRECTION, true);
		this.StartCoroutine(DoInitialize());
	}

	private unsafe IEnumerator DoInitialize()
	{
		SkillItemInfo skillItemInfo = resultData.itemData as SkillItemInfo;
		magiLoader = new GameObject("magimodel").AddComponent<ItemLoader>();
		int wait3 = 1;
		magiLoader.LoadSkillItem(skillItemInfo.tableID, magiLoader.get_transform(), magiLoader.get_gameObject().get_layer(), new Action((object)/*Error near IL_009c: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		wait3++;
		magiSymbolLoader = new GameObject("magisymbol").AddComponent<ItemLoader>();
		magiSymbolLoader.LoadSkillItemSymbol(skillItemInfo.tableID, magiSymbolLoader.get_transform(), magiSymbolLoader.get_gameObject().get_layer(), new Action((object)/*Error near IL_0110: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject lo_direction = loadingQueue.Load(RESOURCE_CATEGORY.UI, "GrowSkillDirection", false);
		LoadObject[] materialLoadObjects = new LoadObject[materials.Length];
		for (int j = 0; j < materials.Length; j++)
		{
			SkillItemTable.SkillItemData data = Singleton<SkillItemTable>.I.GetSkillItemData(materials[j].tableID);
			materialLoadObjects[j] = loadingQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemModel(data.modelID), false);
		}
		wait3++;
		NPCTable.NPCData npcData = Singleton<NPCTable>.I.GetNPCData(3);
		GameObject npcRoot = new GameObject("NPC");
		npcData.LoadModel(npcRoot, false, true, delegate
		{
			((_003CDoInitialize_003Ec__Iterator159)/*Error near IL_0224: stateMachine*/)._003Cwait_003E__1--;
		}, false);
		CacheAudio(loadingQueue);
		yield return (object)loadingQueue.Wait();
		while (wait3 > 0)
		{
			yield return (object)null;
		}
		Object directionObject = lo_direction.loadedObject;
		Transform directionTransform = ResourceUtility.Realizes(directionObject, MonoBehaviourSingleton<StageManager>.I.stageObject, -1);
		GameObject[] materialObjects = (GameObject[])new GameObject[materials.Length];
		for (int i = 0; i < materials.Length; i++)
		{
			SkillItemTable.SkillItemData data2 = Singleton<SkillItemTable>.I.GetSkillItemData(materials[i].tableID);
			Transform item = ResourceUtility.Realizes(materialLoadObjects[i].loadedObject, -1);
			PlayerLoader.SetEquipColor(item, data2.modelColor.ToColor());
			materialObjects[i] = item.get_gameObject();
		}
		magiLoader.nodeMain.get_gameObject().SetActive(true);
		magiSymbolLoader.nodeMain.get_gameObject().SetActive(true);
		SkillGrowDirector d = directionTransform.GetComponent<SkillGrowDirector>();
		d.Init();
		d.SetNPC(npcRoot);
		d.SetMagiModel(magiLoader.get_gameObject(), magiSymbolLoader.get_gameObject(), materialObjects);
		d.SetMaterials(materials);
		director = d;
		base.Initialize();
	}

	protected unsafe override void OnOpen()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		director.StartDirection(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		base.OnOpen();
	}

	public override void UpdateUI()
	{
		SkillItemInfo skillItemInfo = resultData.itemData as SkillItemInfo;
		SetActive((Enum)UI.OBJ_GREAT, shouldShowGreatEffect);
		if (shouldShowGreatEffect)
		{
			PlayTween((Enum)UI.OBJ_GREAT, true, (EventDelegate.Callback)delegate
			{
				DispatchEvent("SKIP", null);
			}, false, 0);
			SoundManager.PlayOneShotSE(40000066, null, null);
		}
		SetLabelText((Enum)UI.LBL_GET_EXP, (skillItemInfo.exp - resultData.beforeExp).ToString());
	}

	public override void Exit()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit(director))
		{
			director.Reset();
			Object.Destroy(director.get_gameObject());
		}
		if (Object.op_Implicit(magiLoader))
		{
			Object.Destroy(magiLoader.get_gameObject());
		}
		base.Exit();
	}

	private void OnQuery_SKIP()
	{
		SkillGrowDirector skillGrowDirector = director;
		if (skillGrowDirector.isPlaying)
		{
			skillGrowDirector.Skip();
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(new object[3]
			{
				resultData,
				isGreat,
				isExceed
			});
		}
	}

	private void OnEndDirection()
	{
		if (isGreat)
		{
			shouldShowGreatEffect = true;
		}
		SetToggle((Enum)UI.TGL_DIRECTION, false);
		RefreshUI();
		if (!shouldShowGreatEffect)
		{
			DispatchEvent("SKIP", null);
		}
	}
}
