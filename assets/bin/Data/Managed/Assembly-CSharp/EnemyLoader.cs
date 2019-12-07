using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyLoader : ModelLoaderBase
{
	public delegate void OnCompleteLoad(Enemy enemy);

	public class MaterialParams
	{
		public Material material;

		public bool hasRimPower;

		public bool hasRimWidth;

		public float defaultRimPower;

		public float defaultRimWidth;

		public bool hasVanishFlag;

		public bool hasVanishRate;
	}

	private static UIntKeyTable<Material> materialCaches;

	private float displayGachaScale = 1f;

	private bool kc;

	private AnimEventData.ResidentEffectData[] _residentEffectList;

	private int _layer;

	private EnemyParam _param;

	private Action _callback;

	private AnimEventData _tmpAnimEventData;

	private bool _need_stamp_effect;

	public int bodyID
	{
		get;
		private set;
	}

	public float bodyScale
	{
		get;
		private set;
	}

	public Transform body
	{
		get;
		private set;
	}

	protected Transform foundation
	{
		get;
		private set;
	}

	public Animator animator
	{
		get;
		private set;
	}

	public AnimEventData animEventData
	{
		get;
		private set;
	}

	public Transform shadow
	{
		get;
		private set;
	}

	public Renderer[] renderersBody
	{
		get;
		private set;
	}

	public Transform baseEffect
	{
		get;
		private set;
	}

	public float DisplayGachaScale
	{
		get
		{
			return displayGachaScale;
		}
		set
		{
			displayGachaScale = value;
		}
	}

	public List<MaterialParams> materialParamsList
	{
		get;
		private set;
	}

	public bool isLoading
	{
		get;
		private set;
	}

	public static void ClearPoolObjects()
	{
		if (materialCaches != null)
		{
			materialCaches.Clear();
			materialCaches = null;
		}
	}

	public override bool IsLoading()
	{
		return isLoading;
	}

	public override Animator GetAnimator()
	{
		return animator;
	}

	public override Transform GetHead()
	{
		throw new NotImplementedException();
	}

	public override void SetEnabled(bool is_enable)
	{
		throw new NotImplementedException();
	}

	public void StartLoad(int body_id, int anim_id, float scale, string base_effect, string base_effect_node, bool need_shadow, bool enable_light_probes, bool need_anim_event_res_cache, SHADER_TYPE shader_type, int layer = -1, string foundation_name = null, bool need_stamp_effect = false, bool will_stock = false, string weather_effect = "", OnCompleteLoad callback = null)
	{
		if (isLoading)
		{
			Log.Error(LOG.RESOURCE, base.name + " now loading.");
		}
		else if (body != null)
		{
			Log.Error(LOG.RESOURCE, base.name + " loaded.");
		}
		else
		{
			StartCoroutine(DoLoad(body_id, anim_id, scale, base_effect, base_effect_node, need_shadow, enable_light_probes, need_anim_event_res_cache, shader_type, layer, foundation_name, need_stamp_effect, will_stock, weather_effect, callback));
		}
	}

	private IEnumerator DoLoad(int body_id, int anim_id, float scale, string base_effect, string base_effect_node, bool need_shadow, bool enable_light_probes, bool need_anim_event_res_cache, SHADER_TYPE shader_type, int layer, string foundation_name, bool need_stamp_effect, bool will_stock, string weather_effect, OnCompleteLoad callback)
	{
		Enemy enemy = base.gameObject.GetComponent<Enemy>();
		if (enemy != null)
		{
			_ = enemy.id;
		}
		bodyID = body_id;
		bodyScale = scale;
		bool is_boss = false;
		if (enemy != null)
		{
			is_boss = enemy.isBoss;
			if (enemy.controller != null)
			{
				enemy.controller.enabled = false;
			}
			if (enemy.packetReceiver != null)
			{
				enemy.packetReceiver.SetStopPacketUpdate(is_stop: true);
			}
			enemy.OnLoadStart();
		}
		string enemyBody = ResourceName.GetEnemyBody(body_id);
		string enemyMaterial = ResourceName.GetEnemyMaterial(body_id);
		string enemyAnim = ResourceName.GetEnemyAnim(anim_id);
		Transform _this = base.transform;
		isLoading = true;
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_body = load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.ENEMY_MODEL, enemyBody);
		LoadObject lo_mate = (enemyMaterial != null) ? load_queue.Load(RESOURCE_CATEGORY.ENEMY_MATERIAL, enemyBody, new string[1]
		{
			enemyMaterial
		}) : null;
		LoadObject lo_anim = load_queue.Load(RESOURCE_CATEGORY.ENEMY_ANIM, enemyAnim, new string[2]
		{
			enemyAnim + "Ctrl",
			enemyAnim + "Event"
		});
		if (!string.IsNullOrEmpty(base_effect))
		{
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, base_effect);
		}
		LoadObject lo_foundation = null;
		if (!string.IsNullOrEmpty(foundation_name))
		{
			if (!MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.enableEnemyModelFoundationFromQuestStage)
			{
				foundation_name = "FST011";
			}
			lo_foundation = load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.FOUNDATION_MODEL, foundation_name);
		}
		yield return load_queue.Wait();
		body = lo_body.Realizes(_this, (layer == -1) ? 11 : layer);
		if (layer == -1)
		{
			base.gameObject.layer = 10;
		}
		body.localPosition = Vector3.zero;
		body.localRotation = Quaternion.identity;
		renderersBody = body.gameObject.GetComponentsInChildren<Renderer>();
		if (lo_mate != null && lo_mate.loadedObject != null && renderersBody.Length == 1)
		{
			Material material2 = lo_mate.loadedObject as Material;
			if (material2 != null)
			{
				renderersBody[0].sharedMaterial = material2;
			}
		}
		if (enemy != null)
		{
			enemy.body = body;
		}
		body.localScale = Vector3.Scale(body.localScale, new Vector3(scale, scale, scale));
		animator = body.gameObject.GetComponent<Animator>();
		if (animator != null && lo_anim.loadedObjects != null)
		{
			animator.runtimeAnimatorController = (RuntimeAnimatorController)lo_anim.loadedObjects[0].obj;
			if (lo_anim.loadedObjects.Length >= 2 && lo_anim.loadedObjects[1] != null)
			{
				this.animEventData = (lo_anim.loadedObjects[1].obj as AnimEventData);
			}
			if (enemy != null)
			{
				body.gameObject.AddComponent<StageObjectProxy>().stageObject = enemy;
				enemy.animEventData = this.animEventData;
			}
		}
		if (!string.IsNullOrEmpty(base_effect))
		{
			string text = base_effect_node;
			if (string.IsNullOrEmpty(text))
			{
				text = "Root";
			}
			Transform parent = Utility.Find(body, text);
			Transform effect = EffectManager.GetEffect(base_effect, parent);
			if (effect != null)
			{
				baseEffect = effect;
				if (layer != -1)
				{
					Utility.SetLayerWithChildren(effect, layer);
				}
			}
		}
		if (!string.IsNullOrEmpty(weather_effect) && MonoBehaviourSingleton<StageManager>.IsValid())
		{
			LoadingQueue loadingQueue = new LoadingQueue(this);
			loadingQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, weather_effect);
			if (loadingQueue.IsLoading())
			{
				yield return loadingQueue.Wait();
			}
			MonoBehaviourSingleton<StageManager>.I.SetWeatherEffect(weather_effect);
		}
		if (shader_type == SHADER_TYPE.LIGHTWEIGHT)
		{
			ShaderGlobal.ChangeWantLightweightShader(renderersBody);
		}
		if (is_boss)
		{
			materialParamsList = new List<MaterialParams>();
			int ID_RIM_POWER = Shader.PropertyToID("_RimPower");
			int ID_RIM_WIDTH = Shader.PropertyToID("_RimWidth");
			int ID_VANISH_FLAG = Shader.PropertyToID("_Vanish_flag");
			int ID_VANISH_RATE = Shader.PropertyToID("_Vanish_rate");
			Utility.MaterialForEach(renderersBody, delegate(Material material)
			{
				if (material != null)
				{
					MaterialParams materialParams = new MaterialParams
					{
						material = material
					};
					if (materialParams.hasRimPower = material.HasProperty(ID_RIM_POWER))
					{
						materialParams.defaultRimPower = material.GetFloat(ID_RIM_POWER);
					}
					if (materialParams.hasRimWidth = material.HasProperty(ID_RIM_WIDTH))
					{
						materialParams.defaultRimWidth = material.GetFloat(ID_RIM_WIDTH);
					}
					materialParams.hasVanishFlag = material.HasProperty(ID_VANISH_FLAG);
					materialParams.hasVanishRate = material.HasProperty(ID_VANISH_RATE);
					materialParamsList.Add(materialParams);
				}
			});
		}
		int i = 0;
		for (int num = renderersBody.Length; i < num; i++)
		{
			if (enable_light_probes)
			{
				renderersBody[i].lightProbeUsage = LightProbeUsage.UseProxyVolume;
			}
			else
			{
				renderersBody[i].lightProbeUsage = LightProbeUsage.Off;
			}
		}
		EnemyParam param = body.gameObject.GetComponent<EnemyParam>();
		body.gameObject.SetActive(value: false);
		if (need_anim_event_res_cache && animator != null && lo_anim.loadedObjects != null && lo_anim.loadedObjects[1] != null)
		{
			AnimEventData animEventData = lo_anim.loadedObjects[1].obj as AnimEventData;
			if (animEventData != null)
			{
				if (enemy == null)
				{
					load_queue.CacheAnimDataUseResource(animEventData);
				}
				else
				{
					load_queue.CacheAnimDataUseResource(animEventData, enemy.EffectNameAnalyzer, enemy.continusAtkEventDataList);
				}
				PreSetAnimationEventDataParamToEnemy(animEventData, enemy);
			}
		}
		AnimEventData.ResidentEffectData[] residentEffectList = null;
		if (this.animEventData != null)
		{
			residentEffectList = this.animEventData.residentEffectDataList;
			if (residentEffectList != null)
			{
				int num2 = residentEffectList.Length;
				for (int j = 0; j < num2; j++)
				{
					if (!string.IsNullOrEmpty(residentEffectList[j].effectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, residentEffectList[j].effectName);
					}
				}
			}
		}
		if (param != null)
		{
			if ((enemy != null) | need_stamp_effect)
			{
				StageObject.StampInfo[] stampInfos = param.stampInfos;
				foreach (StageObject.StampInfo stampInfo in stampInfos)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, stampInfo.effectName);
				}
			}
			if (param.isHide)
			{
				FieldMapTable.GatherPointViewTableData gatherPointViewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(param.gatherPointViewId);
				if (gatherPointViewData != null)
				{
					if (!string.IsNullOrEmpty(gatherPointViewData.targetEffectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, gatherPointViewData.targetEffectName);
					}
					if (!string.IsNullOrEmpty(gatherPointViewData.gatherEffectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, gatherPointViewData.gatherEffectName);
					}
				}
			}
			SystemEffectSetting residentEffectSetting = param.residentEffectSetting;
			if (residentEffectSetting != null)
			{
				SystemEffectSetting.Data[] effectDataList = residentEffectSetting.effectDataList;
				if (effectDataList != null)
				{
					int num3 = effectDataList.Length;
					for (int l = 0; l < num3; l++)
					{
						if (!string.IsNullOrEmpty(effectDataList[l].effectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectDataList[l].effectName);
						}
					}
				}
			}
		}
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
		}
		if (enemy != null)
		{
			if (param != null)
			{
				EnemyTable.EnemyData enemyTableData = enemy.enemyTableData;
				AttackHitInfo[] attackHitInfos = param.attackHitInfos;
				foreach (AttackHitInfo attackHitInfo in attackHitInfos)
				{
					AttackHitInfo attackHitInfo2 = attackHitInfo;
					if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
					{
						string b = attackHitInfo2.name + "_" + enemyTableData.convertRegionKey;
						AttackHitInfo[] convertAttackHitInfos = param.convertAttackHitInfos;
						foreach (AttackHitInfo attackHitInfo3 in convertAttackHitInfos)
						{
							if (attackHitInfo3.name == b)
							{
								attackHitInfo2 = attackHitInfo3;
								break;
							}
						}
					}
					if (attackHitInfo2.hitSEID != 0)
					{
						load_queue.CacheSE(attackHitInfo2.hitSEID);
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, attackHitInfo2.hitEffectName);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, attackHitInfo2.remainEffectName);
					load_queue.CacheBulletDataUseResource(attackHitInfo2.bulletData);
					RestraintInfo restraintInfo = attackHitInfo.restraintInfo;
					if (restraintInfo.enable && !string.IsNullOrEmpty(restraintInfo.effectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, restraintInfo.effectName);
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_flick");
						if (attackHitInfo.toPlayer.reactionType != 0)
						{
							Log.Error(LOG.INGAME, "Can't use reactionType with RestraintInfo!! " + attackHitInfo.name);
						}
					}
					GrabInfo grabInfo = attackHitInfo.grabInfo;
					if (grabInfo != null && grabInfo.enable && attackHitInfo.toPlayer.reactionType != 0)
					{
						Log.Error(LOG.INGAME, "Can't use reactionType with GrabInfo!! " + attackHitInfo.name);
					}
					InkSplashInfo inkSplashInfo = attackHitInfo.inkSplashInfo;
					if (inkSplashInfo != null && inkSplashInfo.duration > 0f)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_blind_01");
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_blind_02");
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_flick");
					}
					if (attackHitInfo.badStatus.stone > 0f)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_stone_01");
					}
				}
				AttackContinuationInfo[] attackContinuationInfos = param.attackContinuationInfos;
				for (int k = 0; k < attackContinuationInfos.Length; k++)
				{
					AttackContinuationInfo attackContinuationInfo = attackContinuationInfos[k];
					if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
					{
						string b2 = attackContinuationInfo.name + "_" + enemyTableData.convertRegionKey;
						AttackContinuationInfo[] convertAttackContinuationInfos = param.convertAttackContinuationInfos;
						foreach (AttackContinuationInfo attackContinuationInfo2 in convertAttackContinuationInfos)
						{
							if (attackContinuationInfo2.name == b2)
							{
								attackContinuationInfo = attackContinuationInfo2;
								break;
							}
						}
					}
					load_queue.CacheBulletDataUseResource(attackContinuationInfo.bulletData);
				}
				Enemy.RegionInfo[] regionInfos = param.regionInfos;
				for (int k = 0; k < regionInfos.Length; k++)
				{
					Enemy.RegionInfo regionInfo = regionInfos[k];
					if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
					{
						string b3 = regionInfo.name + "_" + enemyTableData.convertRegionKey;
						Enemy.RegionInfo[] convertRegionInfos = param.convertRegionInfos;
						foreach (Enemy.RegionInfo regionInfo2 in convertRegionInfos)
						{
							if (regionInfo2.name == b3)
							{
								regionInfo = regionInfo2;
								break;
							}
						}
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, regionInfo.breakEffect.effectName);
				}
				if (Singleton<EnemyHitMaterialTable>.IsValid())
				{
					int n = 0;
					for (int num4 = param.regionInfos.Length; n < num4 + 1; n++)
					{
						string text2 = (n >= num4) ? param.baseHitMaterialName : param.regionInfos[n].hitMaterialName;
						if (string.IsNullOrEmpty(text2))
						{
							continue;
						}
						EnemyHitMaterialTable.MaterialData data = Singleton<EnemyHitMaterialTable>.I.GetData(text2);
						if (data == null)
						{
							continue;
						}
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.addEffectName);
						int[] typeSEIDs = data.typeSEIDs;
						foreach (int num5 in typeSEIDs)
						{
							if (num5 != 0)
							{
								load_queue.CacheSE(num5);
							}
						}
					}
				}
			}
			if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.enemyParalyzeHitEffectName);
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.enemyPoisonHitEffectName);
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.enemyFreezeHitEffectName);
			}
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_shock_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_gravity_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_fire_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_movedown_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_bindring_01");
			if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				InGameSettingsManager.LightRingParam lightRingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.lightRingParam;
				if (lightRingParam.startSeId != 0)
				{
					load_queue.CacheSE(lightRingParam.startSeId);
				}
				if (lightRingParam.loopSeId != 0)
				{
					load_queue.CacheSE(lightRingParam.loopSeId);
				}
				if (lightRingParam.endSeId != 0)
				{
					load_queue.CacheSE(lightRingParam.endSeId);
				}
			}
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_erosion_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_acid_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_corruption_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.stigmataParam.effectName);
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.cyclonicThunderstormParam.effectName);
			EffectPlayProcessor component = body.gameObject.GetComponent<EffectPlayProcessor>();
			if (component != null && component.effectSettings != null)
			{
				enemy.effectPlayProcessor = component;
				int num6 = 0;
				for (int num7 = component.effectSettings.Length; num6 < num7; num6++)
				{
					if (!string.IsNullOrEmpty(component.effectSettings[num6].effectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, component.effectSettings[num6].effectName);
					}
				}
			}
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
		}
		body.gameObject.SetActive(value: true);
		if (residentEffectList != null)
		{
			int num8 = residentEffectList.Length;
			for (int num9 = 0; num9 < num8; num9++)
			{
				AnimEventData.ResidentEffectData residentEffectData = residentEffectList[num9];
				if (string.IsNullOrEmpty(residentEffectData.effectName) || string.IsNullOrEmpty(residentEffectData.linkNodeName))
				{
					continue;
				}
				Transform transform = Utility.Find(body.transform, residentEffectData.linkNodeName);
				if (transform == null)
				{
					transform = body.transform;
				}
				Transform effect2 = EffectManager.GetEffect(residentEffectData.effectName, transform);
				if (effect2 != null)
				{
					if (layer != -1)
					{
						Utility.SetLayerWithChildren(effect2, layer);
					}
					Vector3 localScale = effect2.localScale;
					effect2.localScale = localScale * residentEffectData.scale;
					effect2.localPosition = residentEffectData.offsetPos;
					effect2.localRotation = Quaternion.Euler(residentEffectData.offsetRot);
					ResidentEffectObject residentEffectObject = effect2.gameObject.AddComponent<ResidentEffectObject>();
					residentEffectObject.Initialize(residentEffectData);
					if (enemy != null)
					{
						enemy.RegisterResidentEffect(residentEffectObject);
					}
				}
			}
		}
		if (param != null)
		{
			SystemEffectSetting residentEffectSetting2 = param.residentEffectSetting;
			SysEffectCreate(enemy, layer, residentEffectSetting2);
		}
		if (need_shadow && param != null && param.shadowSize > 0f)
		{
			shadow = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.CreateShadow(param.shadowSize, param.bodyRadius, bodyScale, fixedY0: true, _this, shader_type == SHADER_TYPE.LIGHTWEIGHT);
		}
		if (enemy != null)
		{
			if (param != null)
			{
				param.SetParam(enemy);
				UnityEngine.Object.DestroyImmediate(param);
			}
			if (enemy.controller != null)
			{
				enemy.controller.enabled = true;
			}
			enemy.willStock = will_stock;
			enemy.OnLoadComplete();
			if (enemy.packetReceiver != null)
			{
				enemy.packetReceiver.SetStopPacketUpdate(is_stop: false);
			}
		}
		callback?.Invoke(enemy);
		if (lo_foundation != null)
		{
			foundation = lo_foundation.Realizes(_this, layer);
			foundation.SetParent(_this.parent, worldPositionStays: true);
		}
		isLoading = false;
	}

	public void StartLoad_GG_Optomize(int body_id, int anim_id, float scale, string base_effect, string base_effect_node, bool need_shadow, bool enable_light_probes, bool need_anim_event_res_cache, SHADER_TYPE shader_type, int layer = -1, string foundation_name = null, bool need_stamp_effect = false, bool will_stock = false, string weather_effect = "", OnCompleteLoad callback = null, Action effectCallBack = null, bool use_load_later = true)
	{
		if (isLoading)
		{
			Log.Error(LOG.RESOURCE, base.name + " now loading.");
		}
		else if (body != null)
		{
			Log.Error(LOG.RESOURCE, base.name + " loaded.");
		}
		else
		{
			StartCoroutine(DoGGOptimizationLoad(body_id, anim_id, scale, base_effect, base_effect_node, need_shadow, enable_light_probes, need_anim_event_res_cache, shader_type, layer, foundation_name, need_stamp_effect, will_stock, weather_effect, callback, effectCallBack, use_load_later));
		}
	}

	private IEnumerator DoGGOptimizationLoad(int body_id, int anim_id, float scale, string base_effect, string base_effect_node, bool need_shadow, bool enable_light_probes, bool need_anim_event_res_cache, SHADER_TYPE shader_type, int layer, string foundation_name, bool need_stamp_effect, bool will_stock, string weather_effect, OnCompleteLoad callback, Action effectCallBack, bool useLoadLater)
	{
		Enemy enemy = base.gameObject.GetComponent<Enemy>();
		if (enemy != null)
		{
			_ = enemy.id;
		}
		bodyID = body_id;
		bodyScale = scale;
		bool is_boss = false;
		if (enemy != null)
		{
			is_boss = enemy.isBoss;
			if (enemy.controller != null)
			{
				enemy.controller.enabled = false;
			}
			if (enemy.packetReceiver != null)
			{
				enemy.packetReceiver.SetStopPacketUpdate(is_stop: true);
			}
			enemy.OnLoadStart();
		}
		string enemyBody = ResourceName.GetEnemyBody(body_id);
		string enemyMaterial = ResourceName.GetEnemyMaterial(body_id);
		string enemyAnim = ResourceName.GetEnemyAnim(anim_id);
		Transform _this = base.transform;
		isLoading = true;
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_body = load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.ENEMY_MODEL, enemyBody);
		LoadObject lo_mate = (enemyMaterial != null) ? load_queue.Load(RESOURCE_CATEGORY.ENEMY_MATERIAL, enemyBody, new string[1]
		{
			enemyMaterial
		}) : null;
		LoadObject lo_anim = load_queue.Load(RESOURCE_CATEGORY.ENEMY_ANIM, enemyAnim, new string[2]
		{
			enemyAnim + "Ctrl",
			enemyAnim + "Event"
		});
		if (!string.IsNullOrEmpty(base_effect))
		{
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, base_effect);
		}
		LoadObject lo_foundation = null;
		if (!string.IsNullOrEmpty(foundation_name))
		{
			if (!MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.enableEnemyModelFoundationFromQuestStage)
			{
				foundation_name = "FST011";
			}
			lo_foundation = load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.FOUNDATION_MODEL, foundation_name);
		}
		yield return load_queue.Wait();
		bool lockE = useLoadLater;
		body = lo_body.Realizes(_this, (layer == -1) ? 11 : layer);
		if (layer == -1)
		{
			base.gameObject.layer = 10;
		}
		body.localPosition = Vector3.zero;
		body.localRotation = Quaternion.identity;
		renderersBody = body.gameObject.GetComponentsInChildren<Renderer>();
		if (lo_mate != null && lo_mate.loadedObject != null && renderersBody.Length == 1)
		{
			Material material2 = lo_mate.loadedObject as Material;
			if (material2 != null)
			{
				renderersBody[0].sharedMaterial = material2;
			}
		}
		if (enemy != null)
		{
			enemy.body = body;
		}
		body.localScale = Vector3.Scale(body.localScale, new Vector3(scale, scale, scale));
		animator = body.gameObject.GetComponent<Animator>();
		if (animator != null && lo_anim.loadedObjects != null)
		{
			animator.runtimeAnimatorController = (RuntimeAnimatorController)lo_anim.loadedObjects[0].obj;
			if (lo_anim.loadedObjects.Length >= 2 && lo_anim.loadedObjects[1] != null)
			{
				this.animEventData = (lo_anim.loadedObjects[1].obj as AnimEventData);
			}
			if (enemy != null)
			{
				body.gameObject.AddComponent<StageObjectProxy>().stageObject = enemy;
				enemy.animEventData = this.animEventData;
			}
		}
		if (!string.IsNullOrEmpty(base_effect))
		{
			string text = base_effect_node;
			if (string.IsNullOrEmpty(text))
			{
				text = "Root";
			}
			Transform parent = Utility.Find(body, text);
			Transform effect = EffectManager.GetEffect(base_effect, parent);
			if (effect != null)
			{
				baseEffect = effect;
				if (layer != -1)
				{
					Utility.SetLayerWithChildren(effect, layer);
				}
			}
		}
		if (!string.IsNullOrEmpty(weather_effect) && MonoBehaviourSingleton<StageManager>.IsValid())
		{
			LoadingQueue loadingQueue = new LoadingQueue(this);
			loadingQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, weather_effect);
			if (loadingQueue.IsLoading())
			{
				yield return loadingQueue.Wait();
			}
			MonoBehaviourSingleton<StageManager>.I.SetWeatherEffect(weather_effect);
		}
		if (shader_type == SHADER_TYPE.LIGHTWEIGHT)
		{
			ShaderGlobal.ChangeWantLightweightShader(renderersBody);
		}
		if (is_boss)
		{
			materialParamsList = new List<MaterialParams>();
			int ID_RIM_POWER = Shader.PropertyToID("_RimPower");
			int ID_RIM_WIDTH = Shader.PropertyToID("_RimWidth");
			int ID_VANISH_FLAG = Shader.PropertyToID("_Vanish_flag");
			int ID_VANISH_RATE = Shader.PropertyToID("_Vanish_rate");
			Utility.MaterialForEach(renderersBody, delegate(Material material)
			{
				if (material != null)
				{
					MaterialParams materialParams = new MaterialParams
					{
						material = material
					};
					if (materialParams.hasRimPower = material.HasProperty(ID_RIM_POWER))
					{
						materialParams.defaultRimPower = material.GetFloat(ID_RIM_POWER);
					}
					if (materialParams.hasRimWidth = material.HasProperty(ID_RIM_WIDTH))
					{
						materialParams.defaultRimWidth = material.GetFloat(ID_RIM_WIDTH);
					}
					materialParams.hasVanishFlag = material.HasProperty(ID_VANISH_FLAG);
					materialParams.hasVanishRate = material.HasProperty(ID_VANISH_RATE);
					materialParamsList.Add(materialParams);
				}
			});
		}
		int i = 0;
		for (int num = renderersBody.Length; i < num; i++)
		{
			if (enable_light_probes)
			{
				renderersBody[i].lightProbeUsage = LightProbeUsage.UseProxyVolume;
			}
			else
			{
				renderersBody[i].lightProbeUsage = LightProbeUsage.Off;
			}
		}
		EnemyParam param = body.gameObject.GetComponent<EnemyParam>();
		body.gameObject.SetActive(value: false);
		if (need_anim_event_res_cache && animator != null && lo_anim.loadedObjects != null && lo_anim.loadedObjects[1] != null)
		{
			AnimEventData animEventData = lo_anim.loadedObjects[1].obj as AnimEventData;
			if (animEventData != null)
			{
				if (!lockE)
				{
					if (enemy == null)
					{
						load_queue.CacheAnimDataUseResource(animEventData);
					}
					else
					{
						load_queue.CacheAnimDataUseResource(animEventData, enemy.EffectNameAnalyzer, enemy.continusAtkEventDataList);
					}
				}
				PreSetAnimationEventDataParamToEnemy(animEventData, enemy);
			}
		}
		AnimEventData.ResidentEffectData[] residentEffectList = null;
		if (this.animEventData != null)
		{
			residentEffectList = this.animEventData.residentEffectDataList;
			if (residentEffectList != null)
			{
				int num2 = residentEffectList.Length;
				for (int j = 0; j < num2; j++)
				{
					if (!string.IsNullOrEmpty(residentEffectList[j].effectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, residentEffectList[j].effectName);
					}
				}
			}
		}
		if (param != null && !lockE)
		{
			if ((enemy != null) | need_stamp_effect)
			{
				StageObject.StampInfo[] stampInfos = param.stampInfos;
				foreach (StageObject.StampInfo stampInfo in stampInfos)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, stampInfo.effectName);
				}
			}
			if (param.isHide)
			{
				FieldMapTable.GatherPointViewTableData gatherPointViewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(param.gatherPointViewId);
				if (gatherPointViewData != null)
				{
					if (!string.IsNullOrEmpty(gatherPointViewData.targetEffectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, gatherPointViewData.targetEffectName);
					}
					if (!string.IsNullOrEmpty(gatherPointViewData.gatherEffectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, gatherPointViewData.gatherEffectName);
					}
				}
			}
			SystemEffectSetting residentEffectSetting = param.residentEffectSetting;
			if (residentEffectSetting != null)
			{
				SystemEffectSetting.Data[] effectDataList = residentEffectSetting.effectDataList;
				if (effectDataList != null)
				{
					int num3 = effectDataList.Length;
					for (int l = 0; l < num3; l++)
					{
						if (!string.IsNullOrEmpty(effectDataList[l].effectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectDataList[l].effectName);
						}
					}
				}
			}
		}
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
		}
		if (enemy != null && !lockE)
		{
			if (param != null)
			{
				EnemyTable.EnemyData enemyTableData = enemy.enemyTableData;
				AttackHitInfo[] attackHitInfos = param.attackHitInfos;
				foreach (AttackHitInfo attackHitInfo in attackHitInfos)
				{
					AttackHitInfo attackHitInfo2 = attackHitInfo;
					if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
					{
						string b = attackHitInfo2.name + "_" + enemyTableData.convertRegionKey;
						AttackHitInfo[] convertAttackHitInfos = param.convertAttackHitInfos;
						foreach (AttackHitInfo attackHitInfo3 in convertAttackHitInfos)
						{
							if (attackHitInfo3.name == b)
							{
								attackHitInfo2 = attackHitInfo3;
								break;
							}
						}
					}
					if (attackHitInfo2.hitSEID != 0)
					{
						load_queue.CacheSE(attackHitInfo2.hitSEID);
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, attackHitInfo2.hitEffectName);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, attackHitInfo2.remainEffectName);
					load_queue.CacheBulletDataUseResource(attackHitInfo2.bulletData);
					RestraintInfo restraintInfo = attackHitInfo.restraintInfo;
					if (restraintInfo.enable && !string.IsNullOrEmpty(restraintInfo.effectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, restraintInfo.effectName);
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_flick");
						if (attackHitInfo.toPlayer.reactionType != 0)
						{
							Log.Error(LOG.INGAME, "Can't use reactionType with RestraintInfo!! " + attackHitInfo.name);
						}
					}
					GrabInfo grabInfo = attackHitInfo.grabInfo;
					if (grabInfo != null && grabInfo.enable && attackHitInfo.toPlayer.reactionType != 0)
					{
						Log.Error(LOG.INGAME, "Can't use reactionType with GrabInfo!! " + attackHitInfo.name);
					}
					InkSplashInfo inkSplashInfo = attackHitInfo.inkSplashInfo;
					if (inkSplashInfo != null && inkSplashInfo.duration > 0f)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_blind_01");
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_blind_02");
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_flick");
					}
					if (attackHitInfo.badStatus.stone > 0f)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_stone_01");
					}
				}
				AttackContinuationInfo[] attackContinuationInfos = param.attackContinuationInfos;
				for (int k = 0; k < attackContinuationInfos.Length; k++)
				{
					AttackContinuationInfo attackContinuationInfo = attackContinuationInfos[k];
					if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
					{
						string b2 = attackContinuationInfo.name + "_" + enemyTableData.convertRegionKey;
						AttackContinuationInfo[] convertAttackContinuationInfos = param.convertAttackContinuationInfos;
						foreach (AttackContinuationInfo attackContinuationInfo2 in convertAttackContinuationInfos)
						{
							if (attackContinuationInfo2.name == b2)
							{
								attackContinuationInfo = attackContinuationInfo2;
								break;
							}
						}
					}
					load_queue.CacheBulletDataUseResource(attackContinuationInfo.bulletData);
				}
				Enemy.RegionInfo[] regionInfos = param.regionInfos;
				for (int k = 0; k < regionInfos.Length; k++)
				{
					Enemy.RegionInfo regionInfo = regionInfos[k];
					if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
					{
						string b3 = regionInfo.name + "_" + enemyTableData.convertRegionKey;
						Enemy.RegionInfo[] convertRegionInfos = param.convertRegionInfos;
						foreach (Enemy.RegionInfo regionInfo2 in convertRegionInfos)
						{
							if (regionInfo2.name == b3)
							{
								regionInfo = regionInfo2;
								break;
							}
						}
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, regionInfo.breakEffect.effectName);
				}
				if (Singleton<EnemyHitMaterialTable>.IsValid())
				{
					int n = 0;
					for (int num4 = param.regionInfos.Length; n < num4 + 1; n++)
					{
						string text2 = (n >= num4) ? param.baseHitMaterialName : param.regionInfos[n].hitMaterialName;
						if (string.IsNullOrEmpty(text2))
						{
							continue;
						}
						EnemyHitMaterialTable.MaterialData data = Singleton<EnemyHitMaterialTable>.I.GetData(text2);
						if (data == null)
						{
							continue;
						}
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.addEffectName);
						int[] typeSEIDs = data.typeSEIDs;
						foreach (int num5 in typeSEIDs)
						{
							if (num5 != 0)
							{
								load_queue.CacheSE(num5);
							}
						}
					}
				}
			}
			if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.enemyParalyzeHitEffectName);
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.enemyPoisonHitEffectName);
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.enemyFreezeHitEffectName);
			}
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_shock_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_gravity_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_fire_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_movedown_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_bindring_01");
			if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				InGameSettingsManager.LightRingParam lightRingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.lightRingParam;
				if (lightRingParam.startSeId != 0)
				{
					load_queue.CacheSE(lightRingParam.startSeId);
				}
				if (lightRingParam.loopSeId != 0)
				{
					load_queue.CacheSE(lightRingParam.loopSeId);
				}
				if (lightRingParam.endSeId != 0)
				{
					load_queue.CacheSE(lightRingParam.endSeId);
				}
			}
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_erosion_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_acid_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_corruption_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.stigmataParam.effectName);
			EffectPlayProcessor component = body.gameObject.GetComponent<EffectPlayProcessor>();
			if (component != null && component.effectSettings != null)
			{
				enemy.effectPlayProcessor = component;
				int num6 = 0;
				for (int num7 = component.effectSettings.Length; num6 < num7; num6++)
				{
					if (!string.IsNullOrEmpty(component.effectSettings[num6].effectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, component.effectSettings[num6].effectName);
					}
				}
			}
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
		}
		body.gameObject.SetActive(value: true);
		if (residentEffectList != null)
		{
			int num8 = residentEffectList.Length;
			for (int num9 = 0; num9 < num8; num9++)
			{
				AnimEventData.ResidentEffectData residentEffectData = residentEffectList[num9];
				if (string.IsNullOrEmpty(residentEffectData.effectName) || string.IsNullOrEmpty(residentEffectData.linkNodeName))
				{
					continue;
				}
				Transform transform = Utility.Find(body.transform, residentEffectData.linkNodeName);
				if (transform == null)
				{
					transform = body.transform;
				}
				Transform effect2 = EffectManager.GetEffect(residentEffectData.effectName, transform);
				if (effect2 != null)
				{
					if (layer != -1)
					{
						Utility.SetLayerWithChildren(effect2, layer);
					}
					Vector3 localScale = effect2.localScale;
					effect2.localScale = localScale * residentEffectData.scale;
					effect2.localPosition = residentEffectData.offsetPos;
					effect2.localRotation = Quaternion.Euler(residentEffectData.offsetRot);
					ResidentEffectObject residentEffectObject = effect2.gameObject.AddComponent<ResidentEffectObject>();
					residentEffectObject.Initialize(residentEffectData);
					if (enemy != null)
					{
						enemy.RegisterResidentEffect(residentEffectObject);
					}
				}
			}
		}
		if (param != null)
		{
			SystemEffectSetting residentEffectSetting2 = param.residentEffectSetting;
			SysEffectCreate(enemy, layer, residentEffectSetting2);
		}
		if (need_shadow && param != null && param.shadowSize > 0f)
		{
			shadow = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.CreateShadow(param.shadowSize, param.bodyRadius, bodyScale, fixedY0: true, _this, shader_type == SHADER_TYPE.LIGHTWEIGHT);
		}
		if (lockE)
		{
			StartCoroutine(LoadEnemyEffectAfter(base.gameObject, lo_anim, param, need_anim_event_res_cache, need_stamp_effect, layer, delegate
			{
				effectCallBack();
			}));
		}
		if (enemy != null)
		{
			if (param != null)
			{
				param.SetParam(enemy);
			}
			if (enemy.controller != null)
			{
				enemy.controller.enabled = true;
			}
			enemy.willStock = will_stock;
			enemy.OnLoadComplete();
			if (enemy.packetReceiver != null)
			{
				enemy.packetReceiver.SetStopPacketUpdate(is_stop: false);
			}
		}
		callback?.Invoke(enemy);
		if (!lockE && effectCallBack != null)
		{
			effectCallBack();
		}
		if (lo_foundation != null)
		{
			foundation = lo_foundation.Realizes(_this, layer);
			foundation.SetParent(_this.parent, worldPositionStays: true);
		}
		isLoading = false;
	}

	public IEnumerator LoadEnemyEffectAfter(GameObject gameObject, LoadObject lo_anim, EnemyParam param, bool need_anim_event_res_cache, bool need_stamp_effect, int layer, Action callback)
	{
		if (!(gameObject.GetComponent<Enemy>() == null))
		{
			kc = true;
			_param = param;
			_callback = callback;
			if (need_anim_event_res_cache && animator != null && lo_anim.loadedObjects != null && lo_anim.loadedObjects[1] != null)
			{
				_tmpAnimEventData = (lo_anim.loadedObjects[1].obj as AnimEventData);
			}
			_layer = layer;
			_need_stamp_effect = need_stamp_effect;
		}
		yield break;
	}

	private IEnumerator LoadEnemyEffectAfterDisable(GameObject gameObject, EnemyParam param, bool need_stamp_effect, Action callback)
	{
		Enemy enemy = gameObject.GetComponent<Enemy>();
		if (enemy == null)
		{
			yield break;
		}
		LoadingQueue load_queue = new LoadingQueue(this);
		_ = enemy.id;
		_ = _layer;
		_ = _residentEffectList;
		if (_tmpAnimEventData != null)
		{
			AnimEventData tmpAnimEventData = _tmpAnimEventData;
			if (tmpAnimEventData != null)
			{
				if (enemy == null)
				{
					load_queue.CacheAnimDataUseResource(tmpAnimEventData);
				}
				else
				{
					load_queue.CacheAnimDataUseResource(tmpAnimEventData, enemy.EffectNameAnalyzer, enemy.continusAtkEventDataList);
				}
			}
		}
		if (param != null)
		{
			if ((enemy != null) | need_stamp_effect)
			{
				StageObject.StampInfo[] stampInfos = param.stampInfos;
				foreach (StageObject.StampInfo stampInfo in stampInfos)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, stampInfo.effectName);
				}
			}
			if (param.isHide)
			{
				FieldMapTable.GatherPointViewTableData gatherPointViewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(param.gatherPointViewId);
				if (gatherPointViewData != null)
				{
					if (!string.IsNullOrEmpty(gatherPointViewData.targetEffectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, gatherPointViewData.targetEffectName);
					}
					if (!string.IsNullOrEmpty(gatherPointViewData.gatherEffectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, gatherPointViewData.gatherEffectName);
					}
				}
			}
			SystemEffectSetting residentEffectSetting = param.residentEffectSetting;
			if (residentEffectSetting != null)
			{
				SystemEffectSetting.Data[] effectDataList = residentEffectSetting.effectDataList;
				if (effectDataList != null)
				{
					int num = effectDataList.Length;
					for (int j = 0; j < num; j++)
					{
						if (!string.IsNullOrEmpty(effectDataList[j].effectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectDataList[j].effectName);
						}
					}
				}
			}
		}
		while (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
		}
		if (enemy != null)
		{
			if (param != null)
			{
				EnemyTable.EnemyData enemyTableData = enemy.enemyTableData;
				AttackHitInfo[] attackHitInfos = param.attackHitInfos;
				foreach (AttackHitInfo attackHitInfo in attackHitInfos)
				{
					AttackHitInfo attackHitInfo2 = attackHitInfo;
					if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
					{
						string b = attackHitInfo2.name + "_" + enemyTableData.convertRegionKey;
						AttackHitInfo[] convertAttackHitInfos = param.convertAttackHitInfos;
						foreach (AttackHitInfo attackHitInfo3 in convertAttackHitInfos)
						{
							if (attackHitInfo3.name == b)
							{
								attackHitInfo2 = attackHitInfo3;
								break;
							}
						}
					}
					if (attackHitInfo2.hitSEID != 0)
					{
						load_queue.CacheSE(attackHitInfo2.hitSEID);
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, attackHitInfo2.hitEffectName);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, attackHitInfo2.remainEffectName);
					load_queue.CacheBulletDataUseResource(attackHitInfo2.bulletData);
					RestraintInfo restraintInfo = attackHitInfo.restraintInfo;
					if (restraintInfo.enable && !string.IsNullOrEmpty(restraintInfo.effectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, restraintInfo.effectName);
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_flick");
						if (attackHitInfo.toPlayer.reactionType != 0)
						{
							Log.Error(LOG.INGAME, "Can't use reactionType with RestraintInfo!! " + attackHitInfo.name);
						}
					}
					GrabInfo grabInfo = attackHitInfo.grabInfo;
					if (grabInfo != null && grabInfo.enable && attackHitInfo.toPlayer.reactionType != 0)
					{
						Log.Error(LOG.INGAME, "Can't use reactionType with GrabInfo!! " + attackHitInfo.name);
					}
					InkSplashInfo inkSplashInfo = attackHitInfo.inkSplashInfo;
					if (inkSplashInfo != null && inkSplashInfo.duration > 0f)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_blind_01");
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_blind_02");
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_flick");
					}
					if (attackHitInfo.badStatus.stone > 0f)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_stone_01");
					}
				}
				AttackContinuationInfo[] attackContinuationInfos = param.attackContinuationInfos;
				for (int i = 0; i < attackContinuationInfos.Length; i++)
				{
					AttackContinuationInfo attackContinuationInfo = attackContinuationInfos[i];
					if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
					{
						string b2 = attackContinuationInfo.name + "_" + enemyTableData.convertRegionKey;
						AttackContinuationInfo[] convertAttackContinuationInfos = param.convertAttackContinuationInfos;
						foreach (AttackContinuationInfo attackContinuationInfo2 in convertAttackContinuationInfos)
						{
							if (attackContinuationInfo2.name == b2)
							{
								attackContinuationInfo = attackContinuationInfo2;
								break;
							}
						}
					}
					load_queue.CacheBulletDataUseResource(attackContinuationInfo.bulletData);
				}
				Enemy.RegionInfo[] regionInfos = param.regionInfos;
				for (int i = 0; i < regionInfos.Length; i++)
				{
					Enemy.RegionInfo regionInfo = regionInfos[i];
					if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
					{
						string b3 = regionInfo.name + "_" + enemyTableData.convertRegionKey;
						Enemy.RegionInfo[] convertRegionInfos = param.convertRegionInfos;
						foreach (Enemy.RegionInfo regionInfo2 in convertRegionInfos)
						{
							if (regionInfo2.name == b3)
							{
								regionInfo = regionInfo2;
								break;
							}
						}
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, regionInfo.breakEffect.effectName);
				}
				if (Singleton<EnemyHitMaterialTable>.IsValid())
				{
					int l = 0;
					for (int num2 = param.regionInfos.Length; l < num2 + 1; l++)
					{
						string text = (l >= num2) ? param.baseHitMaterialName : param.regionInfos[l].hitMaterialName;
						if (string.IsNullOrEmpty(text))
						{
							continue;
						}
						EnemyHitMaterialTable.MaterialData data = Singleton<EnemyHitMaterialTable>.I.GetData(text);
						if (data == null)
						{
							continue;
						}
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.addEffectName);
						int[] typeSEIDs = data.typeSEIDs;
						foreach (int num3 in typeSEIDs)
						{
							if (num3 != 0)
							{
								load_queue.CacheSE(num3);
							}
						}
					}
				}
			}
			if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.enemyParalyzeHitEffectName);
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.enemyPoisonHitEffectName);
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.enemyFreezeHitEffectName);
			}
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_shock_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_gravity_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_fire_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_movedown_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_bindring_01");
			if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				InGameSettingsManager.LightRingParam lightRingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.lightRingParam;
				if (lightRingParam.startSeId != 0)
				{
					load_queue.CacheSE(lightRingParam.startSeId);
				}
				if (lightRingParam.loopSeId != 0)
				{
					load_queue.CacheSE(lightRingParam.loopSeId);
				}
				if (lightRingParam.endSeId != 0)
				{
					load_queue.CacheSE(lightRingParam.endSeId);
				}
			}
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_erosion_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_acid_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_corruption_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.stigmataParam.effectName);
			EffectPlayProcessor component = body.gameObject.GetComponent<EffectPlayProcessor>();
			if (component != null && component.effectSettings != null)
			{
				enemy.effectPlayProcessor = component;
				int m = 0;
				for (int num4 = component.effectSettings.Length; m < num4; m++)
				{
					if (!string.IsNullOrEmpty(component.effectSettings[m].effectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, component.effectSettings[m].effectName);
					}
				}
			}
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
		}
		if (enemy != null && param != null)
		{
			UnityEngine.Object.DestroyImmediate(param);
			param = null;
		}
		kc = false;
		callback?.Invoke();
		StopAllCoroutines();
	}

	private void OnDisable()
	{
	}

	private void OnEnable()
	{
		if (kc)
		{
			StartCoroutine(LoadEnemyEffectAfterDisable(base.gameObject, _param, _need_stamp_effect, _callback));
		}
	}

	public void SysEffectCreate(Enemy enemy, int layer, SystemEffectSetting sysEffectSetting)
	{
		if (!(sysEffectSetting != null))
		{
			return;
		}
		int[] array = sysEffectSetting.startGroupIds;
		bool flag = false;
		if (array == null || array.Length == 0)
		{
			flag = true;
			array = new int[1];
		}
		else if (array[0] < 0)
		{
			return;
		}
		SystemEffectSetting.Data[] effectDataList = sysEffectSetting.effectDataList;
		if (effectDataList == null)
		{
			return;
		}
		int num = effectDataList.Length;
		for (int i = 0; i < num; i++)
		{
			SystemEffectSetting.Data data = effectDataList[i];
			if (!flag)
			{
				bool flag2 = false;
				int j = 0;
				for (int num2 = array.Length; j < num2; j++)
				{
					if (array[j] == data.groupID)
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					continue;
				}
			}
			if (string.IsNullOrEmpty(data.effectName) || string.IsNullOrEmpty(data.linkNodeName))
			{
				continue;
			}
			Transform transform = Utility.Find(body.transform, data.linkNodeName);
			if (transform == null)
			{
				transform = body.transform;
			}
			Transform effect = EffectManager.GetEffect(data.effectName, transform);
			if (effect != null)
			{
				if (layer != -1)
				{
					Utility.SetLayerWithChildren(effect, layer);
				}
				Vector3 localScale = effect.localScale;
				effect.localScale = localScale * data.scale;
				effect.localPosition = data.offsetPos;
				effect.localRotation = Quaternion.Euler(data.offsetRot);
				ResidentEffectObject residentEffectObject = effect.gameObject.AddComponent<ResidentEffectObject>();
				residentEffectObject.Initialize(data);
				if (enemy != null)
				{
					enemy.RegisterResidentEffect(residentEffectObject);
				}
			}
		}
	}

	public void ResetRimParams()
	{
		if (materialParamsList != null)
		{
			int ID_RIM_POWER = Shader.PropertyToID("_RimPower");
			int ID_RIM_WIDTH = Shader.PropertyToID("_RimWidth");
			materialParamsList.ForEach(delegate(MaterialParams prm)
			{
				if (prm.hasRimPower)
				{
					prm.material.SetFloat(ID_RIM_POWER, prm.defaultRimPower);
				}
				if (prm.hasRimWidth)
				{
					prm.material.SetFloat(ID_RIM_WIDTH, prm.defaultRimWidth);
				}
			});
		}
	}

	private void PreSetAnimationEventDataParamToEnemy(AnimEventData animEventData, Enemy enemy)
	{
		if (animEventData == null || enemy == null)
		{
			return;
		}
		AnimEventData.AnimData[] animations = animEventData.animations;
		if (animations == null)
		{
			return;
		}
		int i = 0;
		for (int num = animations.Length; i < num; i++)
		{
			AnimEventData.EventData[] events = animations[i].events;
			if (events == null)
			{
				continue;
			}
			int j = 0;
			for (int num2 = events.Length; j < num2; j++)
			{
				AnimEventData.EventData eventData = events[j];
				if (eventData == null)
				{
					continue;
				}
				switch (eventData.id)
				{
				case AnimEventFormat.ID.MOVE_SIDEWAYS_LOOK_TARGET:
					if (eventData.floatArgs.Length >= 2)
					{
						enemy.moveAngle_deg = eventData.floatArgs[0];
						enemy.moveAngleSpeed_deg = eventData.floatArgs[1];
					}
					break;
				case AnimEventFormat.ID.MOVE_POINT_DATA:
					if (eventData.floatArgs.Length >= 2)
					{
						enemy.movePointPos = new Vector3(eventData.floatArgs[0], 0f, eventData.floatArgs[1]);
					}
					break;
				case AnimEventFormat.ID.MOVE_LOOKAT_DATA:
					if (eventData.floatArgs.Length >= 3)
					{
						enemy.moveLookAtPos = new Vector3(eventData.floatArgs[0], 0f, eventData.floatArgs[1]);
						enemy.moveLookAtAngle = eventData.floatArgs[2];
					}
					break;
				case AnimEventFormat.ID.EFFECT:
					if (animations[i].name == "paralyze")
					{
						if (eventData.floatArgs.Length != 0)
						{
							enemy.paralyzeEffectScale = eventData.floatArgs[0];
						}
						if (eventData.stringArgs.Length != 0)
						{
							enemy.paralyzeEffectName = eventData.stringArgs[0];
						}
					}
					break;
				case AnimEventFormat.ID.SUMMON_ENEMY:
				{
					int enemyId2 = eventData.intArgs[0];
					int num3 = eventData.intArgs[1];
					if (num3 == 0 && eventData.intArgs[2] > 0)
					{
						num3 = Mathf.FloorToInt((int)enemy.enemyLevel / eventData.intArgs[2]);
					}
					StartCoroutine(MonoBehaviourSingleton<InGameManager>.I.InitializeEnemyPopForSummon(enemyId2, num3));
					break;
				}
				case AnimEventFormat.ID.SUMMON_ATTACK:
					if (!enemy.isSummonAttack)
					{
						int enemyId = eventData.intArgs[0];
						StartCoroutine(MonoBehaviourSingleton<InGameManager>.I.InitializeEnemyPopForSummonAttack(enemyId, enemy.enemyLevel));
					}
					break;
				}
			}
		}
	}

	public void DeleteLoadedObjects()
	{
		if (body != null)
		{
			UnityEngine.Object.DestroyImmediate(body.gameObject);
			body = null;
		}
		if (foundation != null)
		{
			UnityEngine.Object.DestroyImmediate(foundation.gameObject);
			foundation = null;
		}
		animator = null;
		animEventData = null;
		renderersBody = null;
		isLoading = false;
	}

	public void ApplyGachaDisplayScaleToParentNode()
	{
		if (body != null && body.parent != null)
		{
			body.parent.localScale = new Vector3(displayGachaScale, displayGachaScale, displayGachaScale);
		}
	}

	public static string GetElementEffectName(ELEMENT_TYPE element_type)
	{
		int num = (int)element_type;
		if (!MonoBehaviourSingleton<GlobalSettingsManager>.IsValid() || !MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.enableEnemyModelEffectFromEnemyElement)
		{
			num = 0;
		}
		if (num < 0 || num >= MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.enemyModelElementEffects.Length)
		{
			num = 0;
		}
		return MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.enemyModelElementEffects[num];
	}

	public static void CacheUIElementEffect(LoadingQueue load_queue, ELEMENT_TYPE element_type)
	{
		if (element_type < ELEMENT_TYPE.MAX)
		{
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, GetElementEffectName(element_type));
		}
	}

	public static void CacheUIElementEffects(LoadingQueue load_queue)
	{
		if (MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
		{
			int i = 0;
			for (int num = 6; i < num; i++)
			{
				CacheUIElementEffect(load_queue, (ELEMENT_TYPE)i);
			}
		}
	}
}
