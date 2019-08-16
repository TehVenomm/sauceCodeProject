using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			Log.Error(LOG.RESOURCE, this.get_name() + " now loading.");
		}
		else if (body != null)
		{
			Log.Error(LOG.RESOURCE, this.get_name() + " loaded.");
		}
		else
		{
			this.StartCoroutine(DoLoad(body_id, anim_id, scale, base_effect, base_effect_node, need_shadow, enable_light_probes, need_anim_event_res_cache, shader_type, layer, foundation_name, need_stamp_effect, will_stock, weather_effect, callback));
		}
	}

	private IEnumerator DoLoad(int body_id, int anim_id, float scale, string base_effect, string base_effect_node, bool need_shadow, bool enable_light_probes, bool need_anim_event_res_cache, SHADER_TYPE shader_type, int layer, string foundation_name, bool need_stamp_effect, bool will_stock, string weather_effect, OnCompleteLoad callback)
	{
		Enemy enemy = this.get_gameObject().GetComponent<Enemy>();
		if (enemy != null)
		{
			int id = enemy.id;
		}
		bodyID = body_id;
		bodyScale = scale;
		bool is_boss = false;
		if (enemy != null)
		{
			is_boss = enemy.isBoss;
			if (enemy.controller != null)
			{
				enemy.controller.set_enabled(false);
			}
			if (enemy.packetReceiver != null)
			{
				enemy.packetReceiver.SetStopPacketUpdate(is_stop: true);
			}
			enemy.OnLoadStart();
		}
		string body_name = ResourceName.GetEnemyBody(body_id);
		string mate_name = ResourceName.GetEnemyMaterial(body_id);
		string anim_name = ResourceName.GetEnemyAnim(anim_id);
		Transform _this = this.get_transform();
		isLoading = true;
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_body = load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.ENEMY_MODEL, body_name);
		LoadObject lo_mate = (mate_name == null) ? null : load_queue.Load(RESOURCE_CATEGORY.ENEMY_MATERIAL, body_name, new string[1]
		{
			mate_name
		});
		LoadObject lo_anim = load_queue.Load(RESOURCE_CATEGORY.ENEMY_ANIM, anim_name, new string[2]
		{
			anim_name + "Ctrl",
			anim_name + "Event"
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
		body = lo_body.Realizes(_this, (layer != -1) ? layer : 11);
		if (layer == -1)
		{
			this.get_gameObject().set_layer(10);
		}
		body.set_localPosition(Vector3.get_zero());
		body.set_localRotation(Quaternion.get_identity());
		renderersBody = body.get_gameObject().GetComponentsInChildren<Renderer>();
		if (lo_mate != null && lo_mate.loadedObject != null && renderersBody.Length == 1)
		{
			Material val = lo_mate.loadedObject as Material;
			if (val != null)
			{
				renderersBody[0].set_sharedMaterial(val);
			}
		}
		if (enemy != null)
		{
			enemy.body = body;
		}
		body.set_localScale(Vector3.Scale(body.get_localScale(), new Vector3(scale, scale, scale)));
		animator = body.get_gameObject().GetComponent<Animator>();
		if (animator != null && lo_anim.loadedObjects != null)
		{
			animator.set_runtimeAnimatorController(lo_anim.loadedObjects[0].obj);
			if (lo_anim.loadedObjects.Length >= 2 && lo_anim.loadedObjects[1] != null)
			{
				this.animEventData = (lo_anim.loadedObjects[1].obj as AnimEventData);
			}
			if (enemy != null)
			{
				body.get_gameObject().AddComponent<StageObjectProxy>().stageObject = enemy;
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
			LoadingQueue queue = new LoadingQueue(this);
			queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, weather_effect);
			if (queue.IsLoading())
			{
				yield return queue.Wait();
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
			renderersBody[i].set_useLightProbes(enable_light_probes);
		}
		EnemyParam param = body.get_gameObject().GetComponent<EnemyParam>();
		body.get_gameObject().SetActive(false);
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
					LoadingQueue loadingQueue = load_queue;
					AnimEventData animEventData2 = animEventData;
					Enemy enemy2 = enemy;
					loadingQueue.CacheAnimDataUseResource(animEventData2, ((Character)enemy2).EffectNameAnalyzer, enemy.continusAtkEventDataList);
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
			if (enemy != null || need_stamp_effect)
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
				foreach (AttackContinuationInfo attackContinuationInfo in attackContinuationInfos)
				{
					AttackContinuationInfo attackContinuationInfo2 = attackContinuationInfo;
					if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
					{
						string b2 = attackContinuationInfo2.name + "_" + enemyTableData.convertRegionKey;
						AttackContinuationInfo[] convertAttackContinuationInfos = param.convertAttackContinuationInfos;
						foreach (AttackContinuationInfo attackContinuationInfo3 in convertAttackContinuationInfos)
						{
							if (attackContinuationInfo3.name == b2)
							{
								attackContinuationInfo2 = attackContinuationInfo3;
								break;
							}
						}
					}
					load_queue.CacheBulletDataUseResource(attackContinuationInfo2.bulletData);
				}
				Enemy.RegionInfo[] regionInfos = param.regionInfos;
				foreach (Enemy.RegionInfo regionInfo in regionInfos)
				{
					Enemy.RegionInfo regionInfo2 = regionInfo;
					if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
					{
						string b3 = regionInfo2.name + "_" + enemyTableData.convertRegionKey;
						Enemy.RegionInfo[] convertRegionInfos = param.convertRegionInfos;
						foreach (Enemy.RegionInfo regionInfo3 in convertRegionInfos)
						{
							if (regionInfo3.name == b3)
							{
								regionInfo2 = regionInfo3;
								break;
							}
						}
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, regionInfo2.breakEffect.effectName);
				}
				if (Singleton<EnemyHitMaterialTable>.IsValid())
				{
					int num8 = 0;
					for (int num9 = param.regionInfos.Length; num8 < num9 + 1; num8++)
					{
						string text2 = (num8 >= num9) ? param.baseHitMaterialName : param.regionInfos[num8].hitMaterialName;
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
						foreach (int num11 in typeSEIDs)
						{
							if (num11 != 0)
							{
								load_queue.CacheSE(num11);
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
			EffectPlayProcessor processor = body.get_gameObject().GetComponent<EffectPlayProcessor>();
			if (processor != null && processor.effectSettings != null)
			{
				enemy.effectPlayProcessor = processor;
				int num12 = 0;
				for (int num13 = processor.effectSettings.Length; num12 < num13; num12++)
				{
					if (!string.IsNullOrEmpty(processor.effectSettings[num12].effectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, processor.effectSettings[num12].effectName);
					}
				}
			}
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
		}
		body.get_gameObject().SetActive(true);
		if (residentEffectList != null)
		{
			int num14 = residentEffectList.Length;
			for (int num15 = 0; num15 < num14; num15++)
			{
				AnimEventData.ResidentEffectData residentEffectData = residentEffectList[num15];
				if (string.IsNullOrEmpty(residentEffectData.effectName) || string.IsNullOrEmpty(residentEffectData.linkNodeName))
				{
					continue;
				}
				Transform val2 = Utility.Find(body.get_transform(), residentEffectData.linkNodeName);
				if (val2 == null)
				{
					val2 = body.get_transform();
				}
				Transform effect2 = EffectManager.GetEffect(residentEffectData.effectName, val2);
				if (effect2 != null)
				{
					if (layer != -1)
					{
						Utility.SetLayerWithChildren(effect2, layer);
					}
					Vector3 localScale = effect2.get_localScale();
					effect2.set_localScale(localScale * residentEffectData.scale);
					effect2.set_localPosition(residentEffectData.offsetPos);
					effect2.set_localRotation(Quaternion.Euler(residentEffectData.offsetRot));
					ResidentEffectObject residentEffectObject = effect2.get_gameObject().AddComponent<ResidentEffectObject>();
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
				Object.DestroyImmediate(param);
			}
			if (enemy.controller != null)
			{
				enemy.controller.set_enabled(true);
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
			foundation.SetParent(_this.get_parent(), true);
		}
		isLoading = false;
	}

	public void SysEffectCreate(Enemy enemy, int layer, SystemEffectSetting sysEffectSetting)
	{
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
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
			Transform val = Utility.Find(body.get_transform(), data.linkNodeName);
			if (val == null)
			{
				val = body.get_transform();
			}
			Transform effect = EffectManager.GetEffect(data.effectName, val);
			if (effect != null)
			{
				if (layer != -1)
				{
					Utility.SetLayerWithChildren(effect, layer);
				}
				Vector3 localScale = effect.get_localScale();
				effect.set_localScale(localScale * data.scale);
				effect.set_localPosition(data.offsetPos);
				effect.set_localRotation(Quaternion.Euler(data.offsetRot));
				ResidentEffectObject residentEffectObject = effect.get_gameObject().AddComponent<ResidentEffectObject>();
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
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
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
						if (eventData.floatArgs.Length > 0)
						{
							enemy.paralyzeEffectScale = eventData.floatArgs[0];
						}
						if (eventData.stringArgs.Length > 0)
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
						num3 = Mathf.FloorToInt((float)((int)enemy.enemyLevel / eventData.intArgs[2]));
					}
					this.StartCoroutine(MonoBehaviourSingleton<InGameManager>.I.InitializeEnemyPopForSummon(enemyId2, num3));
					break;
				}
				case AnimEventFormat.ID.SUMMON_ATTACK:
					if (!enemy.isSummonAttack)
					{
						int enemyId = eventData.intArgs[0];
						this.StartCoroutine(MonoBehaviourSingleton<InGameManager>.I.InitializeEnemyPopForSummonAttack(enemyId, enemy.enemyLevel));
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
			Object.DestroyImmediate(body.get_gameObject());
			body = null;
		}
		if (foundation != null)
		{
			Object.DestroyImmediate(foundation.get_gameObject());
			foundation = null;
		}
		animator = null;
		animEventData = null;
		renderersBody = null;
		isLoading = false;
	}

	public void ApplyGachaDisplayScaleToParentNode()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (body != null && body.get_parent() != null)
		{
			body.get_parent().set_localScale(new Vector3(displayGachaScale, displayGachaScale, displayGachaScale));
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
