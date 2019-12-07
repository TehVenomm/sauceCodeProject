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
		SetToggle(UI.TGL_DIRECTION, value: true);
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		SkillItemInfo skillItemInfo = resultData.itemData as SkillItemInfo;
		magiLoader = new GameObject("magimodel").AddComponent<ItemLoader>();
		int wait = 1;
		magiLoader.LoadSkillItem(skillItemInfo.tableID, magiLoader.transform, magiLoader.gameObject.layer, delegate
		{
			magiLoader.nodeMain.gameObject.SetActive(value: false);
			wait--;
		});
		wait++;
		magiSymbolLoader = new GameObject("magisymbol").AddComponent<ItemLoader>();
		magiSymbolLoader.LoadSkillItemSymbol(skillItemInfo.tableID, magiSymbolLoader.transform, magiSymbolLoader.gameObject.layer, delegate
		{
			magiSymbolLoader.nodeMain.gameObject.SetActive(value: false);
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
		NPCTable.NPCData nPCData = Singleton<NPCTable>.I.GetNPCData(3);
		GameObject npcRoot = new GameObject("NPC");
		nPCData.LoadModel(npcRoot, need_shadow: false, enable_light_probe: true, delegate
		{
			wait--;
		}, useSpecialModel: false);
		CacheAudio(loadingQueue);
		yield return loadingQueue.Wait();
		while (wait > 0)
		{
			yield return null;
		}
		Transform transform = ResourceUtility.Realizes(lo_direction.loadedObject, MonoBehaviourSingleton<StageManager>.I.stageObject);
		GameObject[] array = new GameObject[materials.Length];
		for (int j = 0; j < materials.Length; j++)
		{
			SkillItemTable.SkillItemData skillItemData2 = Singleton<SkillItemTable>.I.GetSkillItemData(materials[j].tableID);
			Transform transform2 = ResourceUtility.Realizes(materialLoadObjects[j].loadedObject);
			PlayerLoader.SetEquipColor(transform2, skillItemData2.modelColor.ToColor());
			array[j] = transform2.gameObject;
		}
		magiLoader.nodeMain.gameObject.SetActive(value: true);
		magiSymbolLoader.nodeMain.gameObject.SetActive(value: true);
		SkillGrowDirector component = transform.GetComponent<SkillGrowDirector>();
		component.Init();
		component.SetNPC(npcRoot);
		component.SetMagiModel(magiLoader.gameObject, magiSymbolLoader.gameObject, array);
		component.SetMaterials(materials);
		director = component;
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
			PlayTween(UI.OBJ_GREAT, forward: true, delegate
			{
				DispatchEvent("SKIP");
			}, is_input_block: false);
			SoundManager.PlayOneShotSE(40000066);
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
		SetToggle(UI.TGL_DIRECTION, value: false);
		RefreshUI();
		if (!shouldShowGreatEffect)
		{
			DispatchEvent("SKIP");
		}
	}
}
