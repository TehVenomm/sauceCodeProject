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
			Play("MainAnim_Init", null, 0f);
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
		SkillItemInfo s = new SkillItemInfo();
		s.tableData = new SkillItemTable.SkillItemData();
		s.tableData.type = SKILL_SLOT_TYPE.ATTACK;
		s.tableData.modelID = 1;
		SetMaterials(new List<SkillItemInfo>
		{
			s,
			s,
			s,
			s,
			s
		}.ToArray());
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject lo = loadingQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemModel(tmpid), false);
		LoadObject lo_symbol = loadingQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetItemModel(80000001), false);
		LoadObject[] materialLoadObjects = new LoadObject[materials.Length];
		for (int j = 0; j < materials.Length; j++)
		{
			materialLoadObjects[j] = loadingQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemModel(UnityEngine.Random.Range(1, 5)), false);
		}
		LoadObject npcTableLoadObject = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "NPCTable", false);
		yield return (object)loadingQueue.Wait();
		TextAsset tableCSV = npcTableLoadObject.loadedObject as TextAsset;
		if (!Singleton<NPCTable>.IsValid())
		{
			Singleton<NPCTable>.Create();
			Singleton<NPCTable>.I.CreateTable(tableCSV.text);
		}
		bool wait = true;
		NPCTable.NPCData npcData = Singleton<NPCTable>.I.GetNPCData(3);
		npcData.LoadModel(npcParent.gameObject, false, true, delegate
		{
			((_003CTEst_003Ec__Iterator156)/*Error near IL_0251: stateMachine*/)._003C_003Ef__this.SetNPC(((_003CTEst_003Ec__Iterator156)/*Error near IL_0251: stateMachine*/)._003C_003Ef__this.npcParent.gameObject);
			((_003CTEst_003Ec__Iterator156)/*Error near IL_0251: stateMachine*/)._003Cwait_003E__9 = false;
		}, false);
		GameObject[] materialObjects = new GameObject[materials.Length];
		for (int i = 0; i < materials.Length; i++)
		{
			Transform item = ResourceUtility.Realizes(materialLoadObjects[i].loadedObject, -1);
			materialObjects[i] = item.gameObject;
		}
		Transform magi = ResourceUtility.Realizes(lo.loadedObject, -1);
		Transform magiSymbol = ResourceUtility.Realizes(lo_symbol.loadedObject, -1);
		SetMagiModel(magi.gameObject, magiSymbol.gameObject, materialObjects);
		while (wait)
		{
			yield return (object)null;
		}
		yield return (object)new WaitForSeconds(0.2f);
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
		Renderer[] array = componentsInChildren;
		foreach (Renderer renderer in array)
		{
			Material material = renderer.material;
			material.SetTexture("_EnvTex", component.GetTexture());
			material.SetVector("_LightDir", new Vector4(-0.24f, -0.24f, -1.64f, 1f));
		}
		component.cacheAfter = true;
		magiMaterialEffects = new GameObject[materials.Length];
		for (int j = 0; j < materials.Length; j++)
		{
			GameObject gameObject = materials[j];
			gameObject.name = "MAGI";
			GrowMagiMaterialRotator growMagiMaterialRotator = gameObject.AddComponent<GrowMagiMaterialRotator>();
			growMagiMaterialRotator.Setup(new Vector3(UnityEngine.Random.value * 360f, UnityEngine.Random.value * 360f, UnityEngine.Random.value * 360f), UnityEngine.Random.Range(180f, 540f));
			Transform transform = ResourceUtility.Realizes(magiMaterialEffectPrefab, magiEffectParent, magiEffectParent.gameObject.layer);
			Utility.Attach(transform.GetChild(0), gameObject.transform);
			Utility.SetLayerWithChildren(gameObject.transform, magiEffectParent.gameObject.layer);
			magiMaterialEffects[j] = transform.gameObject;
			Renderer[] componentsInChildren2 = gameObject.GetComponentsInChildren<Renderer>();
			Renderer[] array2 = componentsInChildren2;
			foreach (Renderer renderer2 in array2)
			{
				Material material2 = renderer2.material;
				material2.SetTexture("_EnvTex", component.GetTexture());
				material2.SetVector("_LightDir", new Vector4(13.07f, -12.7f, -0.6f, 1f));
			}
			transform.gameObject.SetActive(false);
		}
	}

	public void SetMaterials(SkillItemInfo[] materials)
	{
		this.materials = materials;
	}

	public void StartDirection(Action onEnd)
	{
		base.enabled = true;
		SetLinkCamera((UnityEngine.Object)mainCameraBlur != (UnityEngine.Object)directionCameraBlur);
		mainCameraBlur.StartFilter();
		CreateEffect(completeEffect, completeEffectParent, magiCamera);
		CreateEffect(npcEffect, npcEffectParent, npcCamera);
		StartCreateMagiEffects();
		npcAnimator.Play("MAGI_GROW");
		npcAnimator.Update(0f);
		SoundManager.PlayOneShotSE(40000065, null, null);
		Animator[] array = cameraAnimators;
		foreach (Animator animator in array)
		{
			animator.Play("CameraAnim_Start", 0, 0f);
			animator.Update(0f);
		}
		Play("MainAnim_Start", delegate
		{
			ResetPlayRate();
			if (onEnd != null)
			{
				onEnd();
			}
		}, 0f);
	}

	private void StartCreateMagiEffects()
	{
		float num = 250f;
		float num2 = 360f / (float)magiMaterialEffects.Length;
		GameObject[] array = magiMaterialEffects;
		foreach (GameObject gameObject in array)
		{
			gameObject.transform.localEulerAngles = new Vector3(0f, num, 0f);
			num += num2;
			gameObject.SetActive(true);
			Animator componentInChildren = gameObject.GetComponentInChildren<Animator>();
			componentInChildren.Play("MagiMaterialAnim_Init", 0, 0f);
			componentInChildren.Update(0f);
			playingAnimators.Add(componentInChildren);
		}
	}

	private IEnumerator CreateMagiEffects()
	{
		GameObject[] array = magiMaterialEffects;
		foreach (GameObject j in array)
		{
			j.SetActive(true);
			j.GetComponentInChildren<Animation>().Play("MaterialAnim_1");
			for (int i = 0; i < 6; i++)
			{
				yield return (object)null;
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
		rymFX component = transform.GetComponent<rymFX>();
		component.Cameras = new Camera[1]
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
					component.UpdateFx(num / 30f, null, false);
				}
			}
			base.Skip();
		}
	}

	protected override void Update()
	{
		for (int i = 0; i < cameraAnimators.Length; i++)
		{
			cameraAnimators[i].speed = ((!skip) ? 1f : 1000f);
		}
		for (int j = 0; j < playingAnimators.Count; j++)
		{
			playingAnimators[j].speed = ((!skip) ? 1f : 1000f);
		}
		base.Update();
	}

	protected override void LateUpdate()
	{
		if ((UnityEngine.Object)useCamera != (UnityEngine.Object)null)
		{
			Animator animator = cameraAnimators[0];
			Vector3 localScale = animator.transform.localScale;
			float x = localScale.x;
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
		Play("MainAnim_Init", null, 0f);
		foreach (GameObject effect in effects)
		{
			if ((bool)effect)
			{
				effect.GetComponent<rymFX>().PlayRate = 1f;
				UnityEngine.Object.Destroy(effect);
			}
		}
		effects.Clear();
		mainCameraBlur.blurStrength = (float)origDownsample;
		mainCameraBlur.StopFilter();
		RenderTargetCacher component = mainCameraBlur.GetComponent<RenderTargetCacher>();
		component.cacheAfter = false;
		skip = false;
		base.Reset();
		base.enabled = false;
	}
}
