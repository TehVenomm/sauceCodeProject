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

	public unsafe void Init()
	{
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
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
				useCamera.get_gameObject().AddComponent<RenderTargetCacher>();
			}
			origDownsample = mainCameraBlur.downSample;
			mainCameraBlur.downSample = directionCameraBlur.downSample;
			this.set_enabled(false);
			rymFX.OnDisableDelegate = Delegate.Combine((Delegate)rymFX.OnDisableDelegate, (Delegate)new CallbackFunction((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			RenderTexture temporary = RenderTexture.GetTemporary(256, 256);
			magiInnerCamera.set_targetTexture(temporary);
			magiInnerQuadRenderer.get_material().set_mainTexture(temporary);
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
			if (Object.op_Implicit(effect))
			{
				effect.GetComponent<rymFX>().PlayRate = 1f;
			}
		}
	}

	protected unsafe override void OnDestroy()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		rymFX.OnDisableDelegate = Delegate.Remove((Delegate)rymFX.OnDisableDelegate, (Delegate)new CallbackFunction((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		mainCameraBlur.downSample = origDownsample;
		RenderTexture.ReleaseTemporary(magiInnerRenderTexture);
		base.OnDestroy();
	}

	private IEnumerator TEst()
	{
		this.get_gameObject().AddComponent<ResourceManager>();
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
		LoadObject lo = loadingQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemModel(tmpid));
		LoadObject lo_symbol = loadingQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetItemModel(80000001));
		LoadObject[] materialLoadObjects = new LoadObject[materials.Length];
		for (int i = 0; i < materials.Length; i++)
		{
			materialLoadObjects[i] = loadingQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemModel(Random.Range(1, 5)));
		}
		LoadObject npcTableLoadObject = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "NPCTable");
		yield return loadingQueue.Wait();
		TextAsset tableCSV = npcTableLoadObject.loadedObject as TextAsset;
		if (!Singleton<NPCTable>.IsValid())
		{
			Singleton<NPCTable>.Create();
			Singleton<NPCTable>.I.CreateTable(tableCSV.get_text());
		}
		bool wait = true;
		NPCTable.NPCData npcData = Singleton<NPCTable>.I.GetNPCData(3);
		npcData.LoadModel(npcParent.get_gameObject(), need_shadow: false, enable_light_probe: true, delegate
		{
			SetNPC(npcParent.get_gameObject());
			wait = false;
		}, useSpecialModel: false);
		GameObject[] materialObjects = (GameObject[])new GameObject[materials.Length];
		for (int j = 0; j < materials.Length; j++)
		{
			Transform val = ResourceUtility.Realizes(materialLoadObjects[j].loadedObject);
			materialObjects[j] = val.get_gameObject();
		}
		Transform magi = ResourceUtility.Realizes(lo.loadedObject);
		Transform magiSymbol = ResourceUtility.Realizes(lo_symbol.loadedObject);
		SetMagiModel(magi.get_gameObject(), magiSymbol.get_gameObject(), materialObjects);
		while (wait)
		{
			yield return null;
		}
		yield return (object)new WaitForSeconds(0.2f);
		StartDirection(delegate
		{
		});
	}

	public void SetNPC(GameObject npc)
	{
		Utility.Attach(npcParent, npc.get_transform());
		Utility.SetLayerWithChildren(npc.get_transform(), npcParent.get_gameObject().get_layer());
		npcAnimator = npc.GetComponentInChildren<Animator>();
	}

	public void SetMagiModel(GameObject magi, GameObject symbol, GameObject[] materials)
	{
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		Utility.Attach(magiObjectParent, magi.get_transform());
		Utility.SetLayerWithChildren(magi.get_transform(), magiObjectParent.get_gameObject().get_layer());
		Utility.Attach(magiSymbolParent, symbol.get_transform());
		Utility.SetLayerWithChildren(symbol.get_transform(), magiSymbolParent.get_gameObject().get_layer());
		CreateEffect(foundationEffect, foundationEffectParent, magiCamera);
		RenderTargetCacher component = mainCameraBlur.GetComponent<RenderTargetCacher>();
		Renderer[] componentsInChildren = magi.GetComponentsInChildren<Renderer>();
		Renderer[] array = componentsInChildren;
		foreach (Renderer val in array)
		{
			Material material = val.get_material();
			material.SetTexture("_EnvTex", component.GetTexture());
			material.SetVector("_LightDir", new Vector4(-0.24f, -0.24f, -1.64f, 1f));
		}
		component.cacheAfter = true;
		magiMaterialEffects = (GameObject[])new GameObject[materials.Length];
		for (int j = 0; j < materials.Length; j++)
		{
			GameObject val2 = materials[j];
			val2.set_name("MAGI");
			GrowMagiMaterialRotator growMagiMaterialRotator = val2.AddComponent<GrowMagiMaterialRotator>();
			growMagiMaterialRotator.Setup(new Vector3(Random.get_value() * 360f, Random.get_value() * 360f, Random.get_value() * 360f), Random.Range(180f, 540f));
			Transform val3 = ResourceUtility.Realizes(magiMaterialEffectPrefab, magiEffectParent, magiEffectParent.get_gameObject().get_layer());
			Utility.Attach(val3.GetChild(0), val2.get_transform());
			Utility.SetLayerWithChildren(val2.get_transform(), magiEffectParent.get_gameObject().get_layer());
			magiMaterialEffects[j] = val3.get_gameObject();
			Renderer[] componentsInChildren2 = val2.GetComponentsInChildren<Renderer>();
			Renderer[] array2 = componentsInChildren2;
			foreach (Renderer val4 in array2)
			{
				Material material2 = val4.get_material();
				material2.SetTexture("_EnvTex", component.GetTexture());
				material2.SetVector("_LightDir", new Vector4(13.07f, -12.7f, -0.6f, 1f));
			}
			val3.get_gameObject().SetActive(false);
		}
	}

	public void SetMaterials(SkillItemInfo[] materials)
	{
		this.materials = materials;
	}

	public void StartDirection(Action onEnd)
	{
		this.set_enabled(true);
		SetLinkCamera(mainCameraBlur != directionCameraBlur);
		mainCameraBlur.StartFilter();
		CreateEffect(completeEffect, completeEffectParent, magiCamera);
		CreateEffect(npcEffect, npcEffectParent, npcCamera);
		StartCreateMagiEffects();
		npcAnimator.Play("MAGI_GROW");
		npcAnimator.Update(0f);
		SoundManager.PlayOneShotSE(40000065);
		Animator[] array = cameraAnimators;
		foreach (Animator val in array)
		{
			val.Play("CameraAnim_Start", 0, 0f);
			val.Update(0f);
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
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		float num = 250f;
		float num2 = 360f / (float)magiMaterialEffects.Length;
		GameObject[] array = magiMaterialEffects;
		foreach (GameObject val in array)
		{
			val.get_transform().set_localEulerAngles(new Vector3(0f, num, 0f));
			num += num2;
			val.SetActive(true);
			Animator componentInChildren = val.GetComponentInChildren<Animator>();
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
		Transform val = ResourceUtility.Realizes(effect, parent, parent.get_gameObject().get_layer());
		rymFX component = val.GetComponent<rymFX>();
		component.Cameras = (Camera[])new Camera[1]
		{
			cam
		};
		effects.Add(val.get_gameObject());
		return val;
	}

	public override void Skip()
	{
		if (!skip)
		{
			foreach (GameObject effect in effects)
			{
				if (Object.op_Implicit(effect))
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
			cameraAnimators[i].set_speed((!skip) ? 1f : 1000f);
		}
		for (int j = 0; j < playingAnimators.Count; j++)
		{
			playingAnimators[j].set_speed((!skip) ? 1f : 1000f);
		}
		base.Update();
	}

	protected override void LateUpdate()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (useCamera != null)
		{
			Animator val = cameraAnimators[0];
			Vector3 localScale = val.get_transform().get_localScale();
			float x = localScale.x;
			if (x > 0f)
			{
				float fieldOfView = Utility.HorizontalToVerticalFOV(x);
				useCamera.set_fieldOfView(fieldOfView);
				npcCamera.set_fieldOfView(fieldOfView);
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
			if (Object.op_Implicit(effect))
			{
				effect.GetComponent<rymFX>().PlayRate = 1f;
				Object.Destroy(effect);
			}
		}
		effects.Clear();
		mainCameraBlur.blurStrength = origDownsample;
		mainCameraBlur.StopFilter();
		RenderTargetCacher component = mainCameraBlur.GetComponent<RenderTargetCacher>();
		component.cacheAfter = false;
		skip = false;
		base.Reset();
		this.set_enabled(false);
	}
}
