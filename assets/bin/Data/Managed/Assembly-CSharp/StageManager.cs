using App.Scripts.GoGame.Optimization;
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

	private bool isLoadingStageObject;

	private bool isLoadingSkyObject;

	private Coroutine loadEffectCoroutine;

	private Transform currentStageContainer;

	private List<Vector3> insideChipList = new List<Vector3>();

	public bool isLoading
	{
		get
		{
			if (!isLoadingStage)
			{
				return isLoadingBackgoundImage;
			}
			return true;
		}
	}

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

	private IEnumerator LoadStage(string id, string load_scene_name, StageTable.StageData data)
	{
		isLoadingStageObject = true;
		if (GoGameCacheManager.HasCacheObj(id))
		{
			load_scene_name += "_lightmap";
		}
		LoadingQueue loadingQueue = new LoadingQueue(this);
		EffectObject.wait = true;
		if (ResourceManager.internalMode)
		{
			load_scene_name = $"internal__STAGE_SCENE__{load_scene_name}";
		}
		else if (ResourceManager.isDownloadAssets)
		{
			loadingQueue.Load(RESOURCE_CATEGORY.STAGE_SCENE, load_scene_name, null, cache_package: true);
			yield return loadingQueue.Wait();
		}
		AsyncOperation ao = SceneManager.LoadSceneAsync(load_scene_name);
		while (!ao.isDone)
		{
			yield return null;
		}
		if (GoGameCacheManager.HasCacheObj(id))
		{
			currentStageContainer = GoGameCacheManager.RetrieveObj(id, base._transform);
			SceneSettingsManager componentInChildren = currentStageContainer.GetComponentInChildren<SceneSettingsManager>();
			if (componentInChildren != null)
			{
				componentInChildren.Self();
				componentInChildren.InitializeScene();
				stageObject = componentInChildren.transform;
			}
		}
		else
		{
			currentStageContainer = new GameObject(string.Format("{0}_{1}", "cache", load_scene_name)).transform;
			currentStageContainer.parent = base._transform;
			GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
			for (int i = 0; i < rootGameObjects.Length; i++)
			{
				rootGameObjects[i].transform.parent = currentStageContainer;
			}
			if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
			{
				stageObject = MonoBehaviourSingleton<SceneSettingsManager>.I.transform;
			}
		}
		if (!MonoBehaviourSingleton<GlobalSettingsManager>.I.stageCache.Contains(data.scene))
		{
			PackageObject packageObject = MonoBehaviourSingleton<ResourceManager>.I.cache.PopCachedPackage(RESOURCE_CATEGORY.STAGE_SCENE.ToAssetBundleName(load_scene_name));
			AssetBundle assetBundle = (packageObject != null) ? (packageObject.obj as AssetBundle) : null;
			if (assetBundle != null)
			{
				assetBundle.Unload(unloadAllLoadedObjects: false);
			}
		}
		bool flag = id.StartsWith("FI");
		if (stageObject != null && flag && (!MonoBehaviourSingleton<SceneSettingsManager>.IsValid() || !MonoBehaviourSingleton<SceneSettingsManager>.I.forceFogON))
		{
			ChangeLightShader(base._transform);
		}
		if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
		{
			MonoBehaviourSingleton<SceneSettingsManager>.I.attributeID = data.attributeID;
			SceneParameter component = MonoBehaviourSingleton<SceneSettingsManager>.I.GetComponent<SceneParameter>();
			if (component != null)
			{
				component.Apply();
			}
			if (flag && !MonoBehaviourSingleton<SceneSettingsManager>.I.forceFogON)
			{
				ShaderGlobal.fogColor = (MonoBehaviourSingleton<SceneSettingsManager>.I.fogColor = new Color(0f, 0f, 0f, 0f));
				ShaderGlobal.fogNear = (MonoBehaviourSingleton<SceneSettingsManager>.I.linearFogStart = 0f);
				ShaderGlobal.fogFar = (MonoBehaviourSingleton<SceneSettingsManager>.I.linearFogEnd = float.MaxValue);
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
		isLoadingStageObject = false;
	}

	private IEnumerator LoadSky(StageTable.StageData data)
	{
		isLoadingSkyObject = true;
		if (GoGameCacheManager.HasCacheObj(data.sky))
		{
			skyObject = GoGameCacheManager.RetrieveObj(data.sky, base._transform);
		}
		else
		{
			ResourceManager.enableCache = false;
			LoadingQueue load_queue = new LoadingQueue(this);
			LoadObject lo_sky = null;
			if (!string.IsNullOrEmpty(data.sky))
			{
				lo_sky = load_queue.Load(RESOURCE_CATEGORY.STAGE_SKY, data.sky);
			}
			ResourceManager.enableCache = true;
			while (load_queue.IsLoading())
			{
				yield return null;
			}
			if (lo_sky != null)
			{
				skyObject = ResourceUtility.Realizes(lo_sky.loadedObject, base._transform);
			}
		}
		isLoadingSkyObject = false;
	}

	private IEnumerator LoadEffect(StageTable.StageData data)
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		bool wait_load_root_effect = false;
		if (GoGameCacheManager.HasCacheObj(data.rootEffect))
		{
			rootEffect = GoGameCacheManager.RetrieveObj(data.rootEffect, base._transform);
		}
		else
		{
			wait_load_root_effect = true;
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.rootEffect);
		}
		bool wait_load_cameraLinkEffect = false;
		if (GoGameCacheManager.HasCacheObj(data.cameraLinkEffect))
		{
			cameraLinkEffect = GoGameCacheManager.RetrieveObj(data.cameraLinkEffect, base._transform);
		}
		else
		{
			wait_load_cameraLinkEffect = true;
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.cameraLinkEffect);
		}
		bool wait_load_cameraLinkEffectY0 = false;
		if (GoGameCacheManager.HasCacheObj(data.cameraLinkEffectY0))
		{
			cameraLinkEffectY0 = GoGameCacheManager.RetrieveObj(data.cameraLinkEffectY0, base._transform);
		}
		else
		{
			wait_load_cameraLinkEffectY0 = true;
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.cameraLinkEffectY0);
		}
		for (int i = 0; i < 8; i++)
		{
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.useEffects[i]);
		}
		while (load_queue.IsLoading())
		{
			yield return null;
		}
		EffectObject.wait = false;
		if (wait_load_cameraLinkEffect)
		{
			cameraLinkEffect = EffectManager.GetCameraLinkEffect(data.cameraLinkEffect, y0: false, base._transform);
		}
		if (wait_load_cameraLinkEffectY0)
		{
			cameraLinkEffectY0 = EffectManager.GetCameraLinkEffect(data.cameraLinkEffectY0, y0: true, base._transform);
		}
		if (wait_load_root_effect)
		{
			rootEffect = EffectManager.GetEffect(data.rootEffect, base._transform);
		}
		while (isLoadingStageObject)
		{
			yield return null;
		}
		if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
		{
			WeatherController weatherController = MonoBehaviourSingleton<SceneSettingsManager>.I.weatherController;
			if (cameraLinkEffect != null)
			{
				cameraLinkEffect.gameObject.SetActive(!weatherController.cameraLinkEffectEnable);
			}
			if (cameraLinkEffectY0 != null)
			{
				cameraLinkEffectY0.gameObject.SetActive(!weatherController.cameraLinkEffectY0Enable);
			}
			if (MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.fieldData != null)
			{
				MonoBehaviourSingleton<SceneSettingsManager>.I.ActivateObjectsByProgress(MonoBehaviourSingleton<FieldManager>.I.fieldData.mapFlag);
			}
		}
	}

	public bool LoadStage(string id)
	{
		if (isLoadingStage)
		{
			Log.Error(LOG.GAMESCENE, "can't load stage.");
			return false;
		}
		if (currentStageName == id)
		{
			return false;
		}
		StartCoroutine(LoadStageCoroutine(id));
		return true;
	}

	private IEnumerator LoadStageCoroutine(string id)
	{
		_ = Time.realtimeSinceStartup;
		isLoadingStage = true;
		insideColliderData = null;
		isValidInside = false;
		UnloadStage();
		_ = Time.realtimeSinceStartup;
		Input.gyro.enabled = false;
		currentStageName = id;
		StageTable.StageData data = null;
		if (!string.IsNullOrEmpty(id))
		{
			_ = Time.realtimeSinceStartup;
			if (!Singleton<StageTable>.IsValid())
			{
				yield break;
			}
			data = Singleton<StageTable>.I.GetData(id);
			if (data == null)
			{
				yield break;
			}
			_ = Time.realtimeSinceStartup;
			StartCoroutine(LoadStage(id, data.scene, data));
			StartCoroutine(LoadSky(data));
			loadEffectCoroutine = StartCoroutine(LoadEffect(data));
			while (isLoadingStageObject || isLoadingSkyObject)
			{
				yield return null;
			}
			_ = Time.realtimeSinceStartup;
		}
		else if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
		{
			ShaderGlobal.lightProbe = true;
		}
		ShaderGlobal.lightProbe = (LightmapSettings.lightProbes != null);
		currentStageData = data;
		isLoadingStage = false;
	}

	public void SetWeatherEffect(string effectName)
	{
		if (cameraLinkEffect != null)
		{
			Object.Destroy(cameraLinkEffect.gameObject);
			cameraLinkEffect = null;
		}
		cameraLinkEffect = EffectManager.GetCameraLinkEffect(effectName, y0: false, base._transform);
		if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
		{
			WeatherController weatherController = MonoBehaviourSingleton<SceneSettingsManager>.I.weatherController;
			if (cameraLinkEffect != null)
			{
				weatherController.cameraLinkEffectEnable = true;
				cameraLinkEffect.gameObject.SetActive(!weatherController.cameraLinkEffectEnable);
			}
		}
	}

	public void LoadBackgoundImage(int image_id)
	{
		if (isLoadingBackgoundImage)
		{
			Log.Error(LOG.GAMESCENE, "can't load stage.");
		}
		else if (backgroundImageID != image_id)
		{
			StartCoroutine(DoLoadBackgoundImage(image_id));
		}
	}

	private IEnumerator DoLoadBackgoundImage(int image_id)
	{
		isLoadingBackgoundImage = true;
		UnloadStage();
		LoadingQueue loadingQueue = new LoadingQueue(this);
		ResourceManager.enableCache = false;
		LoadObject lo_bg = loadingQueue.Load(RESOURCE_CATEGORY.STAGE_IMAGE, MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName().Contains("TutorialWeaponSelectTop") ? "BackgroundTutImage" : "BackgroundImage");
		LoadObject lo_tex = loadingQueue.Load(RESOURCE_CATEGORY.STAGE_IMAGE, ResourceName.GetBackgroundImage(image_id));
		ResourceManager.enableCache = true;
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		Transform transform = ResourceUtility.Realizes(lo_bg.loadedObject, base._transform, 0);
		transform.gameObject.GetComponent<MeshRenderer>().material.mainTexture = (lo_tex.loadedObject as Texture);
		transform.gameObject.AddComponent<FixedViewQuad>();
		backgroundImageID = image_id;
		backgroundImage = transform;
		isLoadingBackgoundImage = false;
	}

	public void UnloadStage()
	{
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			MonoBehaviourSingleton<EffectManager>.I.DeleteManagerChildrenEffects();
			MonoBehaviourSingleton<EffectManager>.I.ClearStocks();
		}
		if (AppMain.needClearMemory && MonoBehaviourSingleton<InstantiateManager>.IsValid())
		{
			MonoBehaviourSingleton<InstantiateManager>.I.ClearStocks();
		}
		if (loadEffectCoroutine != null)
		{
			StopCoroutine(loadEffectCoroutine);
			loadEffectCoroutine = null;
		}
		if (currentStageContainer != null)
		{
			bool num = stageObject == null;
			if (GoGameCacheManager.ShouldCacheStage(currentStageName))
			{
				GoGameCacheManager.CacheObj(currentStageName, currentStageContainer);
				if (stageObject != null)
				{
					stageObject.GetComponent<SceneSettingsManager>().Remove();
				}
			}
			else
			{
				Object.DestroyImmediate(currentStageContainer.gameObject);
			}
			currentStageContainer = null;
			stageObject = null;
			if (!num)
			{
				SceneManager.LoadScene("Empty");
				ShaderGlobal.Initialize();
				MonoBehaviourSingleton<GlobalSettingsManager>.I.ResetLightRot();
				MonoBehaviourSingleton<GlobalSettingsManager>.I.ResetAmbientColor();
				Input.gyro.enabled = true;
			}
		}
		if (skyObject != null)
		{
			if (GoGameCacheManager.ShouldCacheSky(currentStageData.sky))
			{
				GoGameCacheManager.CacheObj(currentStageData.sky, skyObject);
			}
			else
			{
				Object.Destroy(skyObject.gameObject);
			}
			skyObject = null;
		}
		if (rootEffect != null)
		{
			if (GoGameCacheManager.ShouldCacheEffect(currentStageData.rootEffect))
			{
				GoGameCacheManager.CacheObj(currentStageData.rootEffect, rootEffect);
			}
			else
			{
				Object.Destroy(rootEffect.gameObject);
			}
			rootEffect = null;
		}
		if (cameraLinkEffect != null)
		{
			if (GoGameCacheManager.ShouldCacheEffect(currentStageData.cameraLinkEffect))
			{
				GoGameCacheManager.CacheObj(currentStageData.cameraLinkEffect, cameraLinkEffect);
			}
			else
			{
				Object.Destroy(cameraLinkEffect.gameObject);
			}
			cameraLinkEffect = null;
		}
		if (cameraLinkEffectY0 != null)
		{
			if (GoGameCacheManager.ShouldCacheEffect(currentStageData.cameraLinkEffectY0))
			{
				GoGameCacheManager.CacheObj(currentStageData.cameraLinkEffectY0, cameraLinkEffectY0);
			}
			else
			{
				Object.Destroy(cameraLinkEffectY0.gameObject);
			}
			cameraLinkEffectY0 = null;
		}
		currentStageName = null;
		currentStageData = null;
		backgroundImageID = 0;
		if (backgroundImage != null)
		{
			Object.Destroy(backgroundImage.gameObject);
			backgroundImage = null;
		}
	}

	public static float GetHeight(Vector3 pos)
	{
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
		Vector3 position = i.terrainTransform.position;
		return terrain.terrainData.GetInterpolatedHeight((pos.x - position.x) * i.terrainDataSizeInvX, (pos.z - position.z) * i.terrainDataSizeInvZ);
	}

	public static Vector3 FitHeight(Vector3 pos)
	{
		pos.y = GetHeight(pos);
		return pos;
	}

	public static void ChangeLightShader(Transform root)
	{
		UIntKeyTable<Material> uIntKeyTable = new UIntKeyTable<Material>();
		List<Renderer> list = new List<Renderer>();
		root.GetComponentsInChildren(includeInactive: true, list);
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			Renderer renderer = list[i];
			Material[] sharedMaterials = renderer.sharedMaterials;
			int j = 0;
			for (int num = sharedMaterials.Length; j < num; j++)
			{
				Material material = sharedMaterials[j];
				if (!(material != null) || !(material.shader != null))
				{
					continue;
				}
				Material material2 = uIntKeyTable.Get((uint)material.GetInstanceID());
				if (material2 != null)
				{
					sharedMaterials[j] = material2;
					continue;
				}
				string name = material.shader.name;
				if (!name.EndsWith("__l"))
				{
					Shader shader = ResourceUtility.FindShader(name + "__l");
					if (shader != null)
					{
						material2 = new Material(material);
						material2.shader = shader;
						sharedMaterials[j] = material2;
						uIntKeyTable.Add((uint)material.GetInstanceID(), material2);
						continue;
					}
				}
				uIntKeyTable.Add((uint)material.GetInstanceID(), material);
			}
			renderer.sharedMaterials = sharedMaterials;
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
		if (insideColliderData == null)
		{
			return pos;
		}
		return new Vector3(Mathf.Clamp(pos.x, insideColliderData.minX, insideColliderData.maxX), pos.y, Mathf.Clamp(pos.z, insideColliderData.minZ, insideColliderData.maxZ));
	}

	public Vector3 GetRandomPosByInsideInfo(Vector3 center, float max_radius, float min_radius = 0f)
	{
		bool valid = false;
		return GetRandomPosByInsideInfo(center, max_radius, min_radius, ref valid);
	}

	public Vector3 GetRandomPosByInsideInfo(Vector3 center, float max_radius, float min_radius, ref bool valid)
	{
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
				Vector3 zero = Vector3.zero;
				zero.x = (float)(num3 + i) * insideColliderData.chipSize + insideColliderData.chipSize * 0.5f;
				zero.z = (float)(num4 + j) * insideColliderData.chipSize + insideColliderData.chipSize * 0.5f;
				if (!((zero - center).sqrMagnitude >= max_radius * max_radius) && !((zero - center).sqrMagnitude <= min_radius * min_radius) && CheckPosInside(zero))
				{
					insideChipList.Add(zero);
				}
			}
		}
		if (insideChipList.Count <= 0)
		{
			return center;
		}
		Vector3 result = insideChipList[(int)((float)insideChipList.Count * Random.value)];
		result.x += insideColliderData.chipSize * (Random.value - 0.5f);
		result.z += insideColliderData.chipSize * (Random.value - 0.5f);
		valid = true;
		return result;
	}
}
