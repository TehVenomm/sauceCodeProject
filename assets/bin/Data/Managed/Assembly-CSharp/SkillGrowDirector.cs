using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGrowDirector : AnimationDirector
{
	[SerializeField]
	private int tmpid = 3;

	[SerializeField]
	private GameObject[] magiEffects;

	[SerializeField]
	private GameObject foundationEffect;

	[SerializeField]
	private GameObject growingEffect;

	[SerializeField]
	private GameObject completeEffect;

	[SerializeField]
	private GameObject npcEffect;

	[SerializeField]
	private Transform magiObjectParent;

	[SerializeField]
	private Transform magiSymbolParent;

	[SerializeField]
	private Transform magiEffectParent;

	[SerializeField]
	private Transform foundationEffectParent;

	[SerializeField]
	private Transform growingEffectParent;

	[SerializeField]
	private Transform completeEffectParent;

	[SerializeField]
	private Transform npcParent;

	[SerializeField]
	private Transform npcEffectParent;

	[SerializeField]
	private GameObject magiMaterialEffectPrefab;

	[SerializeField]
	private Camera magiCamera;

	[SerializeField]
	private MeshRenderer magiInnerQuadRenderer;

	[SerializeField]
	private Camera magiInnerCamera;

	[SerializeField]
	private Camera npcCamera;

	[SerializeField]
	private MeshRenderer magiInnerTexRenderer;

	[SerializeField]
	private Animator[] cameraAnimators;

	private SkillItemInfo[] materials;

	private BlurFilter mainCameraBlur;

	private BlurFilter directionCameraBlur;

	private RenderTexture magiInnerRenderTexture;

	private GameObject[] magiMaterialEffects;

	private Animator npcAnimator;

	private List<GameObject> effects = new List<GameObject>();

	private int origDownsample;

	private bool initialized;

	private List<Animator> playingAnimators = new List<Animator>();

	public bool skipped => skip;

	private void Start()
	{
		Init();
	}

	public void Init()
	{
		if (!initialized)
		{
			Play("MainAnim_Init");
			directionCameraBlur = useCamera.GetComponent<BlurFilter>();
			if (MonoBehaviourSingleton<AppMain>.IsValid())
			{
				mainCameraBlur = MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<BlurFilter>();
			}
			else
			{
				mainCameraBlur = directionCameraBlur;
				useCamera.gameObject.AddComponent<RenderTargetCacher>();
			}
			origDownsample = mainCameraBlur.downSample;
			mainCameraBlur.downSample = directionCameraBlur.downSample;
			base.enabled = false;
			rymFX.OnDisableDelegate = (rymFX.CallbackFunction)Delegate.Combine(rymFX.OnDisableDelegate, new rymFX.CallbackFunction(OnFxDisable));
			RenderTexture temporary = RenderTexture.GetTemporary(256, 256);
			magiInnerCamera.targetTexture = temporary;
			magiInnerQuadRenderer.material.mainTexture = temporary;
			magiInnerRenderTexture = temporary;
			initialized = true;
		}
	}

	private void OnFxDisable(rymFX fx)
	{
		if (skip)
		{
			fx.PlayRate = 1f;
		}
	}

	private void ResetPlayRate()
	{
		foreach (GameObject effect in effects)
		{
			if ((bool)effect)
			{
				effect.GetComponent<rymFX>().PlayRate = 1f;
			}
		}
	}

	protected override void OnDestroy()
	{
		rymFX.OnDisableDelegate = (rymFX.CallbackFunction)Delegate.Remove(rymFX.OnDisableDelegate, new rymFX.CallbackFunction(OnFxDisable));
		mainCameraBlur.downSample = origDownsample;
		RenderTexture.ReleaseTemporary(magiInnerRenderTexture);
		base.OnDestroy();
	}

	private IEnumerator TEst()
	{
		base.gameObject.AddComponent<ResourceManager>();
		SkillItemInfo skillItemInfo = new SkillItemInfo();
		skillItemInfo.tableData = new SkillItemTable.SkillItemData();
		skillItemInfo.tableData.type = SKILL_SLOT_TYPE.ATTACK;
		skillItemInfo.tableData.modelID = 1;
		List<SkillItemInfo> list = new List<SkillItemInfo>();
		list.Add(skillItemInfo);
		list.Add(skillItemInfo);
		list.Add(skillItemInfo);
		list.Add(skillItemInfo);
		list.Add(skillItemInfo);
		SetMaterials(list.ToArray());
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject lo = loadingQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemModel(tmpid));
		LoadObject lo_symbol = loadingQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetItemModel(80000001));
		LoadObject[] materialLoadObjects = new LoadObject[materials.Length];
		for (int i = 0; i < materials.Length; i++)
		{
			materialLoadObjects[i] = loadingQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemModel(UnityEngine.Random.Range(1, 5)));
		}
		LoadObject npcTableLoadObject = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "NPCTable");
		yield return loadingQueue.Wait();
		TextAsset textAsset = npcTableLoadObject.loadedObject as TextAsset;
		if (!Singleton<NPCTable>.IsValid())
		{
			Singleton<NPCTable>.Create();
			Singleton<NPCTable>.I.CreateTable(textAsset.text);
		}
		bool wait = true;
		Singleton<NPCTable>.I.GetNPCData(3).LoadModel(npcParent.gameObject, need_shadow: false, enable_light_probe: true, delegate
		{
			SetNPC(npcParent.gameObject);
			wait = false;
		}, useSpecialModel: false);
		GameObject[] array = new GameObject[materials.Length];
		for (int j = 0; j < materials.Length; j++)
		{
			Transform transform = ResourceUtility.Realizes(materialLoadObjects[j].loadedObject);
			array[j] = transform.gameObject;
		}
		Transform transform2 = ResourceUtility.Realizes(lo.loadedObject);
		Transform transform3 = ResourceUtility.Realizes(lo_symbol.loadedObject);
		SetMagiModel(transform2.gameObject, transform3.gameObject, array);
		while (wait)
		{
			yield return null;
		}
		yield return new WaitForSeconds(0.2f);
		StartDirection(delegate
		{
		});
	}

	public void SetNPC(GameObject npc)
	{
		Utility.Attach(npcParent, npc.transform);
		Utility.SetLayerWithChildren(npc.transform, npcParent.gameObject.layer);
		npcAnimator = npc.GetComponentInChildren<Animator>();
	}

	public void SetMagiModel(GameObject magi, GameObject symbol, GameObject[] materials)
	{
		Utility.Attach(magiObjectParent, magi.transform);
		Utility.SetLayerWithChildren(magi.transform, magiObjectParent.gameObject.layer);
		Utility.Attach(magiSymbolParent, symbol.transform);
		Utility.SetLayerWithChildren(symbol.transform, magiSymbolParent.gameObject.layer);
		CreateEffect(foundationEffect, foundationEffectParent, magiCamera);
		RenderTargetCacher component = mainCameraBlur.GetComponent<RenderTargetCacher>();
		Renderer[] componentsInChildren = magi.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Material material = componentsInChildren[i].material;
			material.SetTexture("_EnvTex", component.GetTexture());
			material.SetVector("_LightDir", new Vector4(-0.24f, -0.24f, -1.64f, 1f));
		}
		component.cacheAfter = true;
		magiMaterialEffects = new GameObject[materials.Length];
		for (int j = 0; j < materials.Length; j++)
		{
			GameObject gameObject = materials[j];
			gameObject.name = "MAGI";
			gameObject.AddComponent<GrowMagiMaterialRotator>().Setup(new Vector3(UnityEngine.Random.value * 360f, UnityEngine.Random.value * 360f, UnityEngine.Random.value * 360f), UnityEngine.Random.Range(180f, 540f));
			Transform transform = ResourceUtility.Realizes(magiMaterialEffectPrefab, magiEffectParent, magiEffectParent.gameObject.layer);
			Utility.Attach(transform.GetChild(0), gameObject.transform);
			Utility.SetLayerWithChildren(gameObject.transform, magiEffectParent.gameObject.layer);
			magiMaterialEffects[j] = transform.gameObject;
			componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Material material2 = componentsInChildren[i].material;
				material2.SetTexture("_EnvTex", component.GetTexture());
				material2.SetVector("_LightDir", new Vector4(13.07f, -12.7f, -0.6f, 1f));
			}
			transform.gameObject.SetActive(value: false);
		}
	}

	public void SetMaterials(SkillItemInfo[] materials)
	{
		this.materials = materials;
	}

	public void StartDirection(Action onEnd)
	{
		base.enabled = true;
		SetLinkCamera(mainCameraBlur != directionCameraBlur);
		mainCameraBlur.StartFilter();
		CreateEffect(completeEffect, completeEffectParent, magiCamera);
		CreateEffect(npcEffect, npcEffectParent, npcCamera);
		StartCreateMagiEffects();
		npcAnimator.Play("MAGI_GROW");
		npcAnimator.Update(0f);
		SoundManager.PlayOneShotSE(40000065);
		Animator[] array = cameraAnimators;
		foreach (Animator obj in array)
		{
			obj.Play("CameraAnim_Start", 0, 0f);
			obj.Update(0f);
		}
		Play("MainAnim_Start", delegate
		{
			ResetPlayRate();
			if (onEnd != null)
			{
				onEnd();
			}
		});
	}

	private void StartCreateMagiEffects()
	{
		float num = 250f;
		float num2 = 360f / (float)magiMaterialEffects.Length;
		GameObject[] array = magiMaterialEffects;
		foreach (GameObject obj in array)
		{
			obj.transform.localEulerAngles = new Vector3(0f, num, 0f);
			num += num2;
			obj.SetActive(value: true);
			Animator componentInChildren = obj.GetComponentInChildren<Animator>();
			componentInChildren.Play("MagiMaterialAnim_Init", 0, 0f);
			componentInChildren.Update(0f);
			playingAnimators.Add(componentInChildren);
		}
	}

	private IEnumerator CreateMagiEffects()
	{
		GameObject[] array = magiMaterialEffects;
		foreach (GameObject obj in array)
		{
			obj.SetActive(value: true);
			obj.GetComponentInChildren<Animation>().Play("MaterialAnim_1");
			for (int i = 0; i < 6; i++)
			{
				yield return null;
				if (skip)
				{
					break;
				}
			}
		}
	}

	private Transform CreateEffect(GameObject effect, Transform parent, Camera cam)
	{
		Transform transform = ResourceUtility.Realizes(effect, parent, parent.gameObject.layer);
		transform.GetComponent<rymFX>().Cameras = new Camera[1]
		{
			cam
		};
		effects.Add(transform.gameObject);
		return transform;
	}

	public override void Skip()
	{
		if (!skip)
		{
			foreach (GameObject effect in effects)
			{
				if ((bool)effect)
				{
					rymFX component = effect.GetComponent<rymFX>();
					float num = component.GetLoopLastFrame() - component.GetCurFrame();
					component.UpdateFx(num / 30f, null, force_loop: false);
				}
			}
			base.Skip();
		}
	}

	protected override void Update()
	{
		for (int i = 0; i < cameraAnimators.Length; i++)
		{
			cameraAnimators[i].speed = (skip ? 1000f : 1f);
		}
		for (int j = 0; j < playingAnimators.Count; j++)
		{
			playingAnimators[j].speed = (skip ? 1000f : 1f);
		}
		base.Update();
	}

	protected override void LateUpdate()
	{
		if (useCamera != null)
		{
			float x = cameraAnimators[0].transform.localScale.x;
			if (x > 0f)
			{
				float fieldOfView = Utility.HorizontalToVerticalFOV(x);
				useCamera.fieldOfView = fieldOfView;
				npcCamera.fieldOfView = fieldOfView;
			}
		}
		mainCameraBlur.blurStrength = directionCameraBlur.blurStrength;
		base.LateUpdate();
	}

	public override void Reset()
	{
		Play("MainAnim_Init");
		foreach (GameObject effect in effects)
		{
			if ((bool)effect)
			{
				effect.GetComponent<rymFX>().PlayRate = 1f;
				UnityEngine.Object.Destroy(effect);
			}
		}
		effects.Clear();
		mainCameraBlur.blurStrength = origDownsample;
		mainCameraBlur.StopFilter();
		mainCameraBlur.GetComponent<RenderTargetCacher>().cacheAfter = false;
		skip = false;
		base.Reset();
		base.enabled = false;
	}
}
