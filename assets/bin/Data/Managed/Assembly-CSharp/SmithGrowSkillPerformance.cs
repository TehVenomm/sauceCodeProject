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
		object[] array = GameSection.GetEventData() as object[];
		resultData = (array[0] as SmithManager.ResultData);
		isGreat = (bool)array[1];
		materials = (array[2] as SkillItemInfo[]);
		isExceed = (bool)array[3];
		SetToggle(UI.TGL_DIRECTION, true);
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		SkillItemInfo skillItemInfo = resultData.itemData as SkillItemInfo;
		magiLoader = new GameObject("magimodel").AddComponent<ItemLoader>();
		int wait3 = 1;
		magiLoader.LoadSkillItem(skillItemInfo.tableID, magiLoader.transform, magiLoader.gameObject.layer, delegate
		{
			((_003CDoInitialize_003Ec__Iterator15B)/*Error near IL_009c: stateMachine*/)._003C_003Ef__this.magiLoader.nodeMain.gameObject.SetActive(false);
			((_003CDoInitialize_003Ec__Iterator15B)/*Error near IL_009c: stateMachine*/)._003Cwait_003E__1--;
		});
		wait3++;
		magiSymbolLoader = new GameObject("magisymbol").AddComponent<ItemLoader>();
		magiSymbolLoader.LoadSkillItemSymbol(skillItemInfo.tableID, magiSymbolLoader.transform, magiSymbolLoader.gameObject.layer, delegate
		{
			((_003CDoInitialize_003Ec__Iterator15B)/*Error near IL_0110: stateMachine*/)._003C_003Ef__this.magiSymbolLoader.nodeMain.gameObject.SetActive(false);
			((_003CDoInitialize_003Ec__Iterator15B)/*Error near IL_0110: stateMachine*/)._003Cwait_003E__1--;
		});
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
			((_003CDoInitialize_003Ec__Iterator15B)/*Error near IL_0224: stateMachine*/)._003Cwait_003E__1--;
		}, false);
		CacheAudio(loadingQueue);
		yield return (object)loadingQueue.Wait();
		while (wait3 > 0)
		{
			yield return (object)null;
		}
		Object directionObject = lo_direction.loadedObject;
		Transform directionTransform = ResourceUtility.Realizes(directionObject, MonoBehaviourSingleton<StageManager>.I.stageObject, -1);
		GameObject[] materialObjects = new GameObject[materials.Length];
		for (int i = 0; i < materials.Length; i++)
		{
			SkillItemTable.SkillItemData data2 = Singleton<SkillItemTable>.I.GetSkillItemData(materials[i].tableID);
			Transform item = ResourceUtility.Realizes(materialLoadObjects[i].loadedObject, -1);
			PlayerLoader.SetEquipColor(item, data2.modelColor.ToColor());
			materialObjects[i] = item.gameObject;
		}
		magiLoader.nodeMain.gameObject.SetActive(true);
		magiSymbolLoader.nodeMain.gameObject.SetActive(true);
		SkillGrowDirector d = directionTransform.GetComponent<SkillGrowDirector>();
		d.Init();
		d.SetNPC(npcRoot);
		d.SetMagiModel(magiLoader.gameObject, magiSymbolLoader.gameObject, materialObjects);
		d.SetMaterials(materials);
		director = d;
		base.Initialize();
	}

	protected override void OnOpen()
	{
		director.StartDirection(OnEndDirection);
		base.OnOpen();
	}

	public override void UpdateUI()
	{
		SkillItemInfo skillItemInfo = resultData.itemData as SkillItemInfo;
		SetActive(UI.OBJ_GREAT, shouldShowGreatEffect);
		if (shouldShowGreatEffect)
		{
			PlayTween(UI.OBJ_GREAT, true, delegate
			{
				DispatchEvent("SKIP", null);
			}, false, 0);
			SoundManager.PlayOneShotSE(40000066, null, null);
		}
		SetLabelText(UI.LBL_GET_EXP, (skillItemInfo.exp - resultData.beforeExp).ToString());
	}

	public override void Exit()
	{
		if ((bool)director)
		{
			director.Reset();
			Object.Destroy(director.gameObject);
		}
		if ((bool)magiLoader)
		{
			Object.Destroy(magiLoader.gameObject);
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
		SetToggle(UI.TGL_DIRECTION, false);
		RefreshUI();
		if (!shouldShowGreatEffect)
		{
			DispatchEvent("SKIP", null);
		}
	}
}
