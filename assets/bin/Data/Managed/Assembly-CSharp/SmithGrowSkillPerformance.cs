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
		object[] array = GameSection.GetEventData() as object[];
		resultData = (array[0] as SmithManager.ResultData);
		isGreat = (bool)array[1];
		materials = (array[2] as SkillItemInfo[]);
		isExceed = (bool)array[3];
		SetToggle((Enum)UI.TGL_DIRECTION, value: true);
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		SkillItemInfo skillItemInfo = resultData.itemData as SkillItemInfo;
		magiLoader = new GameObject("magimodel").AddComponent<ItemLoader>();
		int wait = 1;
		magiLoader.LoadSkillItem(skillItemInfo.tableID, magiLoader.get_transform(), magiLoader.get_gameObject().get_layer(), delegate
		{
			magiLoader.nodeMain.get_gameObject().SetActive(false);
			wait--;
		});
		wait++;
		magiSymbolLoader = new GameObject("magisymbol").AddComponent<ItemLoader>();
		magiSymbolLoader.LoadSkillItemSymbol(skillItemInfo.tableID, magiSymbolLoader.get_transform(), magiSymbolLoader.get_gameObject().get_layer(), delegate
		{
			magiSymbolLoader.nodeMain.get_gameObject().SetActive(false);
			wait--;
		});
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject lo_direction = loadingQueue.Load(RESOURCE_CATEGORY.UI, "GrowSkillDirection");
		LoadObject[] materialLoadObjects = new LoadObject[materials.Length];
		for (int i = 0; i < materials.Length; i++)
		{
			SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(materials[i].tableID);
			materialLoadObjects[i] = loadingQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemModel(skillItemData.modelID));
		}
		wait++;
		NPCTable.NPCData npcData = Singleton<NPCTable>.I.GetNPCData(3);
		GameObject npcRoot = new GameObject("NPC");
		npcData.LoadModel(npcRoot, need_shadow: false, enable_light_probe: true, delegate
		{
			wait--;
		}, useSpecialModel: false);
		CacheAudio(loadingQueue);
		yield return loadingQueue.Wait();
		while (wait > 0)
		{
			yield return null;
		}
		Object directionObject = lo_direction.loadedObject;
		Transform directionTransform = ResourceUtility.Realizes(directionObject, MonoBehaviourSingleton<StageManager>.I.stageObject);
		GameObject[] materialObjects = (GameObject[])new GameObject[materials.Length];
		for (int j = 0; j < materials.Length; j++)
		{
			SkillItemTable.SkillItemData skillItemData2 = Singleton<SkillItemTable>.I.GetSkillItemData(materials[j].tableID);
			Transform val = ResourceUtility.Realizes(materialLoadObjects[j].loadedObject);
			PlayerLoader.SetEquipColor(val, skillItemData2.modelColor.ToColor());
			materialObjects[j] = val.get_gameObject();
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

	protected override void OnOpen()
	{
		director.StartDirection(OnEndDirection);
		base.OnOpen();
	}

	public override void UpdateUI()
	{
		SkillItemInfo skillItemInfo = resultData.itemData as SkillItemInfo;
		SetActive((Enum)UI.OBJ_GREAT, shouldShowGreatEffect);
		if (shouldShowGreatEffect)
		{
			PlayTween((Enum)UI.OBJ_GREAT, forward: true, (EventDelegate.Callback)delegate
			{
				DispatchEvent("SKIP");
			}, is_input_block: false, 0);
			SoundManager.PlayOneShotSE(40000066);
		}
		SetLabelText((Enum)UI.LBL_GET_EXP, (skillItemInfo.exp - resultData.beforeExp).ToString());
	}

	public override void Exit()
	{
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
		SetToggle((Enum)UI.TGL_DIRECTION, value: false);
		RefreshUI();
		if (!shouldShowGreatEffect)
		{
			DispatchEvent("SKIP");
		}
	}
}
