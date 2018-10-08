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
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_00ae: Expected O, but got Unknown
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
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
				useCamera.get_gameObject().AddComponent<RenderTargetCacher>();
			}
			origDownsample = mainCameraBlur.downSample;
			mainCameraBlur.downSample = directionCameraBlur.downSample;
			this.set_enabled(false);
			rymFX.OnDisableDelegate = Delegate.Combine((Delegate)rymFX.OnDisableDelegate, (Delegate)new CallbackFunction((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			RenderTexture val = RenderTexture.GetTemporary(256, 256);
			magiInnerCamera.set_targetTexture(val);
			magiInnerQuadRenderer.get_material().set_mainTexture(val);
			magiInnerRenderTexture = val;
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
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		//IL_0011: Expected O, but got Unknown
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		rymFX.OnDisableDelegate = Delegate.Remove((Delegate)rymFX.OnDisableDelegate, (Delegate)new CallbackFunction((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		mainCameraBlur.downSample = origDownsample;
		RenderTexture.ReleaseTemporary(magiInnerRenderTexture);
		base.OnDestroy();
	}

	private unsafe IEnumerator TEst()
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
		LoadObject lo = loadingQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemModel(tmpid), false);
		LoadObject lo_symbol = loadingQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetItemModel(80000001), false);
		LoadObject[] materialLoadObjects = new LoadObject[materials.Length];
		for (int j = 0; j < materials.Length; j++)
		{
			materialLoadObjects[j] = loadingQueue.Load(RESOURCE_CATEGORY.ITEM_MODEL, ResourceName.GetSkillItemModel(Random.Range(1, 5)), false);
		}
		LoadObject npcTableLoadObject = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "NPCTable", false);
		yield return (object)loadingQueue.Wait();
		TextAsset tableCSV = npcTableLoadObject.loadedObject as TextAsset;
		if (!Singleton<NPCTable>.IsValid())
		{
			Singleton<NPCTable>.Create();
			Singleton<NPCTable>.I.CreateTable(tableCSV.get_text());
		}
		bool wait = true;
		NPCTable.NPCData npcData = Singleton<NPCTable>.I.GetNPCData(3);
		npcData.LoadModel(npcParent.get_gameObject(), false, true, delegate
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			((_003CTEst_003Ec__Iterator15D)/*Error near IL_0251: stateMachine*/)._003C_003Ef__this.SetNPC(((_003CTEst_003Ec__Iterator15D)/*Error near IL_0251: stateMachine*/)._003C_003Ef__this.npcParent.get_gameObject());
			((_003CTEst_003Ec__Iterator15D)/*Error near IL_0251: stateMachine*/)._003Cwait_003E__9 = false;
		}, false);
		GameObject[] materialObjects = (GameObject[])new GameObject[materials.Length];
		for (int i = 0; i < materials.Length; i++)
		{
			Transform item = ResourceUtility.Realizes(materialLoadObjects[i].loadedObject, -1);
			materialObjects[i] = item.get_gameObject();
		}
		Transform magi = ResourceUtility.Realizes(lo.loadedObject, -1);
		Transform magiSymbol = ResourceUtility.Realizes(lo_symbol.loadedObject, -1);
		SetMagiModel(magi.get_gameObject(), magiSymbol.get_gameObject(), materialObjects);
		while (wait)
		{
			yield return (object)null;
		}
		yield return (object)new WaitForSeconds(0.2f);
		if (_003CTEst_003Ec__Iterator15D._003C_003Ef__am_0024cache13 == null)
		{
			_003CTEst_003Ec__Iterator15D._003C_003Ef__am_0024cache13 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		StartDirection(_003CTEst_003Ec__Iterator15D._003C_003Ef__am_0024cache13);
	}

	public void SetNPC(GameObject npc)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		Utility.Attach(npcParent, npc.get_transform());
		Utility.SetLayerWithChildren(npc.get_transform(), npcParent.get_gameObject().get_layer());
		npcAnimator = npc.GetComponentInChildren<Animator>();
	}

	public void SetMagiModel(GameObject magi, GameObject symbol, GameObject[] materials)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Expected O, but got Unknown
		//IL_0188: Expected O, but got Unknown
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Expected O, but got Unknown
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Expected O, but got Unknown
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Expected O, but got Unknown
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
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
			Material val2 = val.get_material();
			val2.SetTexture("_EnvTex", component.GetTexture());
			val2.SetVector("_LightDir", new Vector4(-0.24f, -0.24f, -1.64f, 1f));
		}
		component.cacheAfter = true;
		magiMaterialEffects = (GameObject[])new GameObject[materials.Length];
		for (int j = 0; j < materials.Length; j++)
		{
			GameObject val3 = materials[j];
			val3.set_name("MAGI");
			GrowMagiMaterialRotator growMagiMaterialRotator = val3.AddComponent<GrowMagiMaterialRotator>();
			growMagiMaterialRotator.Setup(new Vector3(Random.get_value() * 360f, Random.get_value() * 360f, Random.get_value() * 360f), Random.Range(180f, 540f));
			Transform val4 = ResourceUtility.Realizes(magiMaterialEffectPrefab, magiEffectParent, magiEffectParent.get_gameObject().get_layer());
			Utility.Attach(val4.GetChild(0), val3.get_transform());
			Utility.SetLayerWithChildren(val3.get_transform(), magiEffectParent.get_gameObject().get_layer());
			magiMaterialEffects[j] = val4.get_gameObject();
			Renderer[] componentsInChildren2 = val3.GetComponentsInChildren<Renderer>();
			Renderer[] array2 = componentsInChildren2;
			foreach (Renderer val5 in array2)
			{
				Material val6 = val5.get_material();
				val6.SetTexture("_EnvTex", component.GetTexture());
				val6.SetVector("_LightDir", new Vector4(13.07f, -12.7f, -0.6f, 1f));
			}
			val4.get_gameObject().SetActive(false);
		}
	}

	public void SetMaterials(SkillItemInfo[] materials)
	{
		this.materials = materials;
	}

	public unsafe void StartDirection(Action onEnd)
	{
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
		this.set_enabled(true);
		SetLinkCamera(mainCameraBlur != directionCameraBlur);
		mainCameraBlur.StartFilter();
		CreateEffect(completeEffect, completeEffectParent, magiCamera);
		CreateEffect(npcEffect, npcEffectParent, npcCamera);
		StartCreateMagiEffects();
		npcAnimator.Play("MAGI_GROW");
		npcAnimator.Update(0f);
		SoundManager.PlayOneShotSE(40000065, null, null);
		Animator[] array = cameraAnimators;
		foreach (Animator val in array)
		{
			val.Play("CameraAnim_Start", 0, 0f);
			val.Update(0f);
		}
		_003CStartDirection_003Ec__AnonStorey45D _003CStartDirection_003Ec__AnonStorey45D;
		Play("MainAnim_Start", new Action((object)_003CStartDirection_003Ec__AnonStorey45D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), 0f);
	}

	private void StartCreateMagiEffects()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
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
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
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
		Play("MainAnim_Init", null, 0f);
		foreach (GameObject effect in effects)
		{
			if (Object.op_Implicit(effect))
			{
				effect.GetComponent<rymFX>().PlayRate = 1f;
				Object.Destroy(effect);
			}
		}
		effects.Clear();
		mainCameraBlur.blurStrength = (float)origDownsample;
		mainCameraBlur.StopFilter();
		RenderTargetCacher component = mainCameraBlur.GetComponent<RenderTargetCacher>();
		component.cacheAfter = false;
		skip = false;
		base.Reset();
		this.set_enabled(false);
	}
}
