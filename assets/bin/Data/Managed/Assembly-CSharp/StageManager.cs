using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviourSingleton<StageManager>
{
	private bool isLoadingStage;

	private bool isLoadingBackgoundImage;

	private Transform terrainTransform;

	private float terrainDataSizeInvX;

	private float terrainDataSizeInvZ;

	private List<Vector3> insideChipList = new List<Vector3>();

	public bool isLoading => isLoadingStage || isLoadingBackgoundImage;

	public Terrain terrain
	{
		get;
		private set;
	}

	public string currentStageName
	{
		get;
		private set;
	}

	public StageTable.StageData currentStageData
	{
		get;
		private set;
	}

	public Transform stageObject
	{
		get;
		private set;
	}

	public Transform skyObject
	{
		get;
		private set;
	}

	public Transform rootEffect
	{
		get;
		private set;
	}

	public Transform cameraLinkEffect
	{
		get;
		private set;
	}

	public Transform cameraLinkEffectY0
	{
		get;
		private set;
	}

	public int backgroundImageID
	{
		get;
		private set;
	}

	public Transform backgroundImage
	{
		get;
		private set;
	}

	public bool isValidInside
	{
		get;
		private set;
	}

	public SceneSettingsManager.InsideColliderData insideColliderData
	{
		get;
		private set;
	}

	public bool LoadStage(string id)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		if (isLoadingStage)
		{
			Log.Error(LOG.GAMESCENE, "can't load stage.");
			return false;
		}
		if (currentStageName == id)
		{
			return false;
		}
		this.StartCoroutine(LoadStageCoroutine(id));
		return true;
	}

	private IEnumerator LoadStageCoroutine(string id)
	{
		isLoadingStage = true;
		insideColliderData = null;
		isValidInside = false;
		UnloadStage();
		Input.get_gyro().set_enabled(false);
		currentStageName = id;
		StageTable.StageData data = null;
		if (!string.IsNullOrEmpty(id))
		{
			if (!Singleton<StageTable>.IsValid())
			{
				yield break;
			}
			data = Singleton<StageTable>.I.GetData(id);
			if (data == null)
			{
				yield break;
			}
			LoadingQueue load_queue = new LoadingQueue(this);
			AssetBundle asset_bundle = null;
			EffectObject.wait = true;
			string load_scene_name = data.scene;
			if (ResourceManager.internalMode)
			{
				load_scene_name = $"internal__STAGE_SCENE__{load_scene_name}";
			}
			else if (ResourceManager.isDownloadAssets)
			{
				load_queue.Load(RESOURCE_CATEGORY.STAGE_SCENE, data.scene, null, true);
				yield return (object)load_queue.Wait();
				PackageObject package = MonoBehaviourSingleton<ResourceManager>.I.cache.PopCachedPackage(RESOURCE_CATEGORY.STAGE_SCENE.ToAssetBundleName(data.scene));
				asset_bundle = ((package == null) ? null : (package.obj as AssetBundle));
			}
			AsyncOperation ao = SceneManager.LoadSceneAsync(load_scene_name);
			ResourceManager.enableCache = false;
			LoadObject lo_sky = null;
			if (!string.IsNullOrEmpty(data.sky))
			{
				lo_sky = load_queue.Load(RESOURCE_CATEGORY.STAGE_SKY, data.sky, false);
			}
			ResourceManager.enableCache = true;
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.cameraLinkEffect);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.cameraLinkEffectY0);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.rootEffect);
			for (int i = 0; i < 8; i++)
			{
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.useEffects[i]);
			}
			if (load_queue.IsLoading())
			{
				yield return (object)load_queue.Wait();
			}
			while (!ao.get_isDone())
			{
				yield return (object)null;
			}
			EffectObject.wait = false;
			if (asset_bundle != null)
			{
				asset_bundle.Unload(false);
			}
			if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
			{
				stageObject = MonoBehaviourSingleton<SceneSettingsManager>.I.get_transform();
				stageObject.set_parent(base._transform);
			}
			if (lo_sky != null)
			{
				skyObject = ResourceUtility.Realizes(lo_sky.loadedObject, base._transform, -1);
			}
			bool is_field_stage = id.StartsWith("FI");
			if (stageObject != null && is_field_stage && (!MonoBehaviourSingleton<SceneSettingsManager>.IsValid() || !MonoBehaviourSingleton<SceneSettingsManager>.I.forceFogON))
			{
				ChangeLightShader(base._transform);
			}
			cameraLinkEffect = EffectManager.GetCameraLinkEffect(data.cameraLinkEffect, false, base._transform);
			cameraLinkEffectY0 = EffectManager.GetCameraLinkEffect(data.cameraLinkEffectY0, true, base._transform);
			rootEffect = EffectManager.GetEffect(data.rootEffect, base._transform);
			if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
			{
				MonoBehaviourSingleton<SceneSettingsManager>.I.attributeID = data.attributeID;
				SceneParameter sp = MonoBehaviourSingleton<SceneSettingsManager>.I.GetComponent<SceneParameter>();
				if (sp != null)
				{
					sp.Apply();
				}
				if (is_field_stage && !MonoBehaviourSingleton<SceneSettingsManager>.I.forceFogON)
				{
					ShaderGlobal.fogColor = (MonoBehaviourSingleton<SceneSettingsManager>.I.fogColor = new Color(0f, 0f, 0f, 0f));
					ShaderGlobal.fogNear = (MonoBehaviourSingleton<SceneSettingsManager>.I.linearFogStart = 0f);
					ShaderGlobal.fogFar = (MonoBehaviourSingleton<SceneSettingsManager>.I.linearFogEnd = 3.40282347E+38f);
					ShaderGlobal.fogNearLimit = (MonoBehaviourSingleton<SceneSettingsManager>.I.limitFogStart = 0f);
					ShaderGlobal.fogFarLimit = (MonoBehaviourSingleton<SceneSettingsManager>.I.limitFogEnd = 1f);
				}
				if (MonoBehaviourSingleton<SceneSettingsManager>.I.saveInsideCollider && MonoBehaviourSingleton<SceneSettingsManager>.I.insideColliderData != null && (MonoBehaviourSingleton<SceneSettingsManager>.I.insideColliderData.minX != 0 || MonoBehaviourSingleton<SceneSettingsManager>.I.insideColliderData.maxX != 0 || MonoBehaviourSingleton<SceneSettingsManager>.I.insideColliderData.minZ != 0 || MonoBehaviourSingleton<SceneSettingsManager>.I.insideColliderData.maxZ != 0))
				{
					isValidInside = true;
				}
				if (isValidInside)
				{
					insideColliderData = MonoBehaviourSingleton<SceneSettingsManager>.I.insideColliderData;
				}
			}
		}
		else if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
		{
			ShaderGlobal.lightProbe = true;
		}
		ShaderGlobal.lightProbe = (LightmapSettings.get_lightProbes() != null);
		currentStageData = data;
		if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
		{
			WeatherController weatherController = MonoBehaviourSingleton<SceneSettingsManager>.I.weatherController;
			if (cameraLinkEffect != null)
			{
				cameraLinkEffect.get_gameObject().SetActive(!weatherController.cameraLinkEffectEnable);
			}
			if (cameraLinkEffectY0 != null)
			{
				cameraLinkEffectY0.get_gameObject().SetActive(!weatherController.cameraLinkEffectY0Enable);
			}
			if (MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.fieldData != null)
			{
				MonoBehaviourSingleton<SceneSettingsManager>.I.ActivateObjectsByProgress(MonoBehaviourSingleton<FieldManager>.I.fieldData.mapFlag);
			}
		}
		isLoadingStage = false;
	}

	public void LoadBackgoundImage(int image_id)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		if (isLoadingBackgoundImage)
		{
			Log.Error(LOG.GAMESCENE, "can't load stage.");
		}
		else if (backgroundImageID != image_id)
		{
			this.StartCoroutine(DoLoadBackgoundImage(image_id));
		}
	}

	private IEnumerator DoLoadBackgoundImage(int image_id)
	{
		isLoadingBackgoundImage = true;
		UnloadStage();
		LoadingQueue load_queue = new LoadingQueue(this);
		ResourceManager.enableCache = false;
		LoadObject lo_bg = load_queue.Load(RESOURCE_CATEGORY.STAGE_IMAGE, "BackgroundImage", false);
		LoadObject lo_tex = load_queue.Load(RESOURCE_CATEGORY.STAGE_IMAGE, ResourceName.GetBackgroundImage(image_id), false);
		ResourceManager.enableCache = true;
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
		}
		Transform bg = ResourceUtility.Realizes(lo_bg.loadedObject, base._transform, 0);
		bg.get_gameObject().GetComponent<MeshRenderer>().get_material()
			.set_mainTexture(lo_tex.loadedObject as Texture);
		bg.get_gameObject().AddComponent<FixedViewQuad>();
		backgroundImageID = image_id;
		backgroundImage = bg;
		isLoadingBackgoundImage = false;
	}

	public void UnloadStage()
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			MonoBehaviourSingleton<EffectManager>.I.DeleteManagerChildrenEffects();
		}
		if (MonoBehaviourSingleton<InstantiateManager>.IsValid())
		{
			MonoBehaviourSingleton<InstantiateManager>.I.ClearStocks();
		}
		if (stageObject != null)
		{
			stageObject.set_parent(null);
			SceneManager.LoadScene("Empty");
			stageObject = null;
			ShaderGlobal.Initialize();
			MonoBehaviourSingleton<GlobalSettingsManager>.I.ResetLightRot();
			MonoBehaviourSingleton<GlobalSettingsManager>.I.ResetAmbientColor();
			Input.get_gyro().set_enabled(true);
		}
		if (skyObject != null)
		{
			Object.Destroy(skyObject.get_gameObject());
			skyObject = null;
		}
		if (rootEffect != null)
		{
			Object.Destroy(rootEffect.get_gameObject());
			rootEffect = null;
		}
		if (cameraLinkEffect != null)
		{
			Object.Destroy(cameraLinkEffect.get_gameObject());
			cameraLinkEffect = null;
		}
		if (cameraLinkEffectY0 != null)
		{
			Object.Destroy(cameraLinkEffectY0.get_gameObject());
			cameraLinkEffectY0 = null;
		}
		currentStageName = null;
		currentStageData = null;
		backgroundImageID = 0;
		if (backgroundImage != null)
		{
			Object.Destroy(backgroundImage.get_gameObject());
			backgroundImage = null;
		}
	}

	public static float GetHeight(Vector3 pos)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<StageManager>.IsValid())
		{
			return 0f;
		}
		StageManager i = MonoBehaviourSingleton<StageManager>.I;
		Terrain terrain = i.terrain;
		if (terrain == null)
		{
			return 0f;
		}
		Vector3 position = i.terrainTransform.get_position();
		return terrain.get_terrainData().GetInterpolatedHeight((pos.x - position.x) * i.terrainDataSizeInvX, (pos.z - position.z) * i.terrainDataSizeInvZ);
	}

	public static Vector3 FitHeight(Vector3 pos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		pos.y = GetHeight(pos);
		return pos;
	}

	public static void ChangeLightShader(Transform root)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		UIntKeyTable<Material> uIntKeyTable = new UIntKeyTable<Material>();
		List<Renderer> list = new List<Renderer>();
		root.GetComponentsInChildren<Renderer>(true, list);
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			Renderer val = list[i];
			Material[] sharedMaterials = val.get_sharedMaterials();
			int j = 0;
			for (int num = sharedMaterials.Length; j < num; j++)
			{
				Material val2 = sharedMaterials[j];
				if (val2 != null && val2.get_shader() != null)
				{
					Material val3 = uIntKeyTable.Get((uint)val2.GetInstanceID());
					if (val3 != null)
					{
						sharedMaterials[j] = val3;
					}
					else
					{
						string name = val2.get_shader().get_name();
						if (!name.EndsWith("__l"))
						{
							Shader val4 = ResourceUtility.FindShader(name + "__l");
							if (val4 != null)
							{
								val3 = new Material(val2);
								val3.set_shader(val4);
								sharedMaterials[j] = val3;
								uIntKeyTable.Add((uint)val2.GetInstanceID(), val3);
								continue;
							}
						}
						uIntKeyTable.Add((uint)val2.GetInstanceID(), val2);
					}
				}
			}
			val.set_sharedMaterials(sharedMaterials);
		}
		uIntKeyTable.Clear();
		list.Clear();
	}

	public bool CheckInsideFlags(int index_x, int index_z)
	{
		if (!isValidInside)
		{
			return false;
		}
		int num = insideColliderData.maxZ - insideColliderData.minZ + 1;
		int num2 = index_x * num + index_z;
		int num3 = num2 / 32;
		if (num3 >= insideColliderData.insideFlags.Count)
		{
			return false;
		}
		return (insideColliderData.insideFlags[num3] & (1 << num2 % 32)) != 0;
	}

	public bool CheckPosInside(Vector3 check_pos)
	{
		if (!isValidInside)
		{
			return false;
		}
		float num = (float)insideColliderData.minX * insideColliderData.chipSize;
		float num2 = (float)insideColliderData.maxX * insideColliderData.chipSize;
		if (check_pos.x < num || check_pos.x >= num2)
		{
			return false;
		}
		float num3 = (float)insideColliderData.minZ * insideColliderData.chipSize;
		float num4 = (float)insideColliderData.maxZ * insideColliderData.chipSize;
		if (check_pos.z < num3 || check_pos.z >= num4)
		{
			return false;
		}
		int index_x = (int)((check_pos.x - num) / insideColliderData.chipSize);
		int index_z = (int)((check_pos.z - num3) / insideColliderData.chipSize);
		return CheckInsideFlags(index_x, index_z);
	}

	public Vector3 ClampInside(Vector3 pos)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		if (insideColliderData == null)
		{
			return pos;
		}
		return new Vector3(Mathf.Clamp(pos.x, (float)insideColliderData.minX, (float)insideColliderData.maxX), pos.y, Mathf.Clamp(pos.z, (float)insideColliderData.minZ, (float)insideColliderData.maxZ));
	}

	public Vector3 GetRandomPosByInsideInfo(Vector3 center, float max_radius, float min_radius = 0f)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		bool valid = false;
		return GetRandomPosByInsideInfo(center, max_radius, min_radius, ref valid);
	}

	public Vector3 GetRandomPosByInsideInfo(Vector3 center, float max_radius, float min_radius, ref bool valid)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		valid = false;
		if (!isValidInside)
		{
			return center;
		}
		if (max_radius < 0f || min_radius < 0f)
		{
			return center;
		}
		if (min_radius > max_radius)
		{
			return center;
		}
		float num = insideColliderData.chipSize * 0.5f * 1.42f;
		if (max_radius - min_radius < num)
		{
			num = max_radius - num;
			if (num < 0f)
			{
				num = 0f;
			}
		}
		insideChipList.Clear();
		int num2 = Mathf.CeilToInt(max_radius * 2f / insideColliderData.chipSize) + 1;
		int num3 = Mathf.FloorToInt(center.x - max_radius);
		int num4 = Mathf.FloorToInt(center.z - max_radius);
		for (int i = 0; i < num2; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				Vector3 zero = Vector3.get_zero();
				zero.x = (float)(num3 + i) * insideColliderData.chipSize + insideColliderData.chipSize * 0.5f;
				zero.z = (float)(num4 + j) * insideColliderData.chipSize + insideColliderData.chipSize * 0.5f;
				Vector3 val = zero - center;
				if (!(val.get_sqrMagnitude() >= max_radius * max_radius))
				{
					Vector3 val2 = zero - center;
					if (!(val2.get_sqrMagnitude() <= min_radius * min_radius) && CheckPosInside(zero))
					{
						insideChipList.Add(zero);
					}
				}
			}
		}
		if (insideChipList.Count <= 0)
		{
			return center;
		}
		Vector3 result = insideChipList[(int)((float)insideChipList.Count * Random.get_value())];
		result.x += insideColliderData.chipSize * (Random.get_value() - 0.5f);
		result.z += insideColliderData.chipSize * (Random.get_value() - 0.5f);
		valid = true;
		return result;
	}
}
