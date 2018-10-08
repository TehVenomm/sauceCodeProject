using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoader : ModelLoaderBase
{
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

	public delegate void OnCompleteLoad(Enemy enemy);

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

	public void StartLoad(int body_id, int anim_id, float scale, string base_effect, string base_effect_node, bool need_shadow, bool enable_light_probes, bool need_anim_event_res_cache, SHADER_TYPE shader_type, int layer = -1, string foundation_name = null, bool need_stamp_effect = false, bool will_stock = false, OnCompleteLoad callback = null)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
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
			this.StartCoroutine(DoLoad(body_id, anim_id, scale, base_effect, base_effect_node, need_shadow, enable_light_probes, need_anim_event_res_cache, shader_type, layer, foundation_name, need_stamp_effect, will_stock, callback));
		}
	}

	private IEnumerator DoLoad(int body_id, int anim_id, float scale, string base_effect, string base_effect_node, bool need_shadow, bool enable_light_probes, bool need_anim_event_res_cache, SHADER_TYPE shader_type, int layer, string foundation_name, bool need_stamp_effect, bool will_stock, OnCompleteLoad callback)
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
				enemy.packetReceiver.SetStopPacketUpdate(true);
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
		}, false);
		LoadObject lo_anim = load_queue.Load(RESOURCE_CATEGORY.ENEMY_ANIM, anim_name, new string[2]
		{
			anim_name + "Ctrl",
			anim_name + "Event"
		}, false);
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
		yield return (object)load_queue.Wait();
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
			Material mate = lo_mate.loadedObject as Material;
			if (mate != null)
			{
				renderersBody[0].set_sharedMaterial(mate);
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
			string node_name = base_effect_node;
			if (string.IsNullOrEmpty(node_name))
			{
				node_name = "Root";
			}
			Transform node = Utility.Find(body, node_name);
			Transform effect_transform = EffectManager.GetEffect(base_effect, node);
			if (effect_transform != null)
			{
				baseEffect = effect_transform;
				if (layer != -1)
				{
					Utility.SetLayerWithChildren(effect_transform, layer);
				}
			}
		}
		if (shader_type == SHADER_TYPE.LIGHTWEIGHT)
		{
			ShaderGlobal.ChangeWantLightweightShader(renderersBody);
		}
		if (is_boss)
		{
			materialParamsList = new List<MaterialParams>();
			Shader.PropertyToID("_RimPower");
			Shader.PropertyToID("_RimWidth");
			Shader.PropertyToID("_Vanish_flag");
			Shader.PropertyToID("_Vanish_rate");
			Utility.MaterialForEach(renderersBody, delegate(Material material)
			{
				if (material != null)
				{
					MaterialParams materialParams = new MaterialParams
					{
						material = material
					};
					if (materialParams.hasRimPower = material.HasProperty(((_003CDoLoad_003Ec__Iterator269)/*Error near IL_062e: stateMachine*/)._003CID_RIM_POWER_003E__16))
					{
						materialParams.defaultRimPower = material.GetFloat(((_003CDoLoad_003Ec__Iterator269)/*Error near IL_062e: stateMachine*/)._003CID_RIM_POWER_003E__16);
					}
					if (materialParams.hasRimWidth = material.HasProperty(((_003CDoLoad_003Ec__Iterator269)/*Error near IL_062e: stateMachine*/)._003CID_RIM_WIDTH_003E__17))
					{
						materialParams.defaultRimWidth = material.GetFloat(((_003CDoLoad_003Ec__Iterator269)/*Error near IL_062e: stateMachine*/)._003CID_RIM_WIDTH_003E__17);
					}
					materialParams.hasVanishFlag = material.HasProperty(((_003CDoLoad_003Ec__Iterator269)/*Error near IL_062e: stateMachine*/)._003CID_VANISH_FLAG_003E__18);
					materialParams.hasVanishRate = material.HasProperty(((_003CDoLoad_003Ec__Iterator269)/*Error near IL_062e: stateMachine*/)._003CID_VANISH_RATE_003E__19);
					((_003CDoLoad_003Ec__Iterator269)/*Error near IL_062e: stateMachine*/)._003C_003Ef__this.materialParamsList.Add(materialParams);
				}
			});
		}
		int l = 0;
		for (int k = renderersBody.Length; l < k; l++)
		{
			renderersBody[l].set_useLightProbes(enable_light_probes);
		}
		EnemyParam param = body.get_gameObject().GetComponent<EnemyParam>();
		body.get_gameObject().SetActive(false);
		if (need_anim_event_res_cache && animator != null && lo_anim.loadedObjects != null && lo_anim.loadedObjects[1] != null)
		{
			AnimEventData tmpAnimEventData = lo_anim.loadedObjects[1].obj as AnimEventData;
			if (tmpAnimEventData != null)
			{
				if (enemy == null)
				{
					load_queue.CacheAnimDataUseResource(tmpAnimEventData, null, null);
				}
				else
				{
					LoadingQueue loadingQueue = load_queue;
					AnimEventData animEventData = tmpAnimEventData;
					Enemy enemy2 = enemy;
					loadingQueue.CacheAnimDataUseResource(animEventData, ((Character)enemy2).EffectNameAnalyzer, enemy.continusAtkEventDataList);
				}
				PreSetAnimationEventDataParamToEnemy(tmpAnimEventData, enemy);
			}
		}
		AnimEventData.ResidentEffectData[] residentEffectList = null;
		if (this.animEventData != null)
		{
			residentEffectList = this.animEventData.residentEffectDataList;
			if (residentEffectList != null)
			{
				int numEffect3 = residentEffectList.Length;
				for (int ef3 = 0; ef3 < numEffect3; ef3++)
				{
					if (!string.IsNullOrEmpty(residentEffectList[ef3].effectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, residentEffectList[ef3].effectName);
					}
				}
			}
		}
		if (param != null)
		{
			if (enemy != null || need_stamp_effect)
			{
				StageObject.StampInfo[] stampInfos = param.stampInfos;
				foreach (StageObject.StampInfo info4 in stampInfos)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, info4.effectName);
				}
			}
			if (param.isHide)
			{
				FieldMapTable.GatherPointViewTableData viewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(param.gatherPointViewId);
				if (viewData != null)
				{
					if (!string.IsNullOrEmpty(viewData.targetEffectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, viewData.targetEffectName);
					}
					if (!string.IsNullOrEmpty(viewData.gatherEffectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, viewData.gatherEffectName);
					}
				}
			}
			SystemEffectSetting sysEffectSetting2 = param.residentEffectSetting;
			if (sysEffectSetting2 != null)
			{
				SystemEffectSetting.Data[] effectDataList = sysEffectSetting2.effectDataList;
				if (effectDataList != null)
				{
					int numEffect2 = effectDataList.Length;
					for (int ef2 = 0; ef2 < numEffect2; ef2++)
					{
						if (!string.IsNullOrEmpty(effectDataList[ef2].effectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectDataList[ef2].effectName);
						}
					}
				}
			}
		}
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
		}
		if (enemy != null)
		{
			if (param != null)
			{
				EnemyTable.EnemyData data = enemy.enemyTableData;
				AttackHitInfo[] attackHitInfos = param.attackHitInfos;
				foreach (AttackHitInfo temp_info in attackHitInfos)
				{
					AttackHitInfo info = temp_info;
					if (!string.IsNullOrEmpty(data.convertRegionKey))
					{
						string convert_name = info.name + "_" + data.convertRegionKey;
						AttackHitInfo[] convertAttackHitInfos = param.convertAttackHitInfos;
						foreach (AttackHitInfo convert_info in convertAttackHitInfos)
						{
							if (convert_info.name == convert_name)
							{
								info = convert_info;
								break;
							}
						}
					}
					if (info.hitSEID != 0)
					{
						load_queue.CacheSE(info.hitSEID, null);
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, info.hitEffectName);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, info.remainEffectName);
					load_queue.CacheBulletDataUseResource(info.bulletData, null);
					RestraintInfo restInfo = temp_info.restraintInfo;
					if (restInfo.enable && !string.IsNullOrEmpty(restInfo.effectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, restInfo.effectName);
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_flick");
						if (temp_info.toPlayer.reactionType != 0)
						{
							Log.Error(LOG.INGAME, "Can't use reactionType with RestraintInfo!! " + temp_info.name);
						}
					}
					GrabInfo grabInfo = temp_info.grabInfo;
					if (grabInfo != null && grabInfo.enable && temp_info.toPlayer.reactionType != 0)
					{
						Log.Error(LOG.INGAME, "Can't use reactionType with GrabInfo!! " + temp_info.name);
					}
					InkSplashInfo inkSplashInfo = temp_info.inkSplashInfo;
					if (inkSplashInfo != null && inkSplashInfo.duration > 0f)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_blind_01");
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_blind_02");
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_flick");
					}
				}
				AttackContinuationInfo[] attackContinuationInfos = param.attackContinuationInfos;
				foreach (AttackContinuationInfo temp_info2 in attackContinuationInfos)
				{
					AttackContinuationInfo info2 = temp_info2;
					if (!string.IsNullOrEmpty(data.convertRegionKey))
					{
						string convert_name2 = info2.name + "_" + data.convertRegionKey;
						AttackContinuationInfo[] convertAttackContinuationInfos = param.convertAttackContinuationInfos;
						foreach (AttackContinuationInfo convert_info2 in convertAttackContinuationInfos)
						{
							if (convert_info2.name == convert_name2)
							{
								info2 = convert_info2;
								break;
							}
						}
					}
					load_queue.CacheBulletDataUseResource(info2.bulletData, null);
				}
				Enemy.RegionInfo[] regionInfos = param.regionInfos;
				foreach (Enemy.RegionInfo temp_info3 in regionInfos)
				{
					Enemy.RegionInfo info3 = temp_info3;
					if (!string.IsNullOrEmpty(data.convertRegionKey))
					{
						string convert_name3 = info3.name + "_" + data.convertRegionKey;
						Enemy.RegionInfo[] convertRegionInfos = param.convertRegionInfos;
						foreach (Enemy.RegionInfo convert_info3 in convertRegionInfos)
						{
							if (convert_info3.name == convert_name3)
							{
								info3 = convert_info3;
								break;
							}
						}
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, info3.breakEffect.effectName);
				}
				if (Singleton<EnemyHitMaterialTable>.IsValid())
				{
					int j = 0;
					for (int len2 = param.regionInfos.Length; j < len2 + 1; j++)
					{
						string hit_material_name = (j >= len2) ? param.baseHitMaterialName : param.regionInfos[j].hitMaterialName;
						if (!string.IsNullOrEmpty(hit_material_name))
						{
							EnemyHitMaterialTable.MaterialData check_data = Singleton<EnemyHitMaterialTable>.I.GetData(hit_material_name);
							if (check_data != null)
							{
								load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, check_data.addEffectName);
								int[] typeSEIDs = check_data.typeSEIDs;
								foreach (int type_se_id in typeSEIDs)
								{
									if (type_se_id != 0)
									{
										load_queue.CacheSE(type_se_id, null);
									}
								}
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
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_fire_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_movedown_01");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_bindring_01");
			if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				InGameSettingsManager.LightRingParam lihgtRing = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.lightRingParam;
				if (lihgtRing.startSeId != 0)
				{
					load_queue.CacheSE(lihgtRing.startSeId, null);
				}
				if (lihgtRing.loopSeId != 0)
				{
					load_queue.CacheSE(lihgtRing.loopSeId, null);
				}
				if (lihgtRing.endSeId != 0)
				{
					load_queue.CacheSE(lihgtRing.endSeId, null);
				}
			}
			EffectPlayProcessor processor = body.get_gameObject().GetComponent<EffectPlayProcessor>();
			if (processor != null && processor.effectSettings != null)
			{
				enemy.effectPlayProcessor = processor;
				int i = 0;
				for (int len = processor.effectSettings.Length; i < len; i++)
				{
					if (!string.IsNullOrEmpty(processor.effectSettings[i].effectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, processor.effectSettings[i].effectName);
					}
				}
			}
			if (load_queue.IsLoading())
			{
				yield return (object)load_queue.Wait();
			}
		}
		body.get_gameObject().SetActive(true);
		if (residentEffectList != null)
		{
			int numEffect = residentEffectList.Length;
			for (int ef = 0; ef < numEffect; ef++)
			{
				AnimEventData.ResidentEffectData effectData = residentEffectList[ef];
				if (!string.IsNullOrEmpty(effectData.effectName) && !string.IsNullOrEmpty(effectData.linkNodeName))
				{
					Transform parentTrans = Utility.Find(body.get_transform(), effectData.linkNodeName);
					if (parentTrans == null)
					{
						parentTrans = body.get_transform();
					}
					Transform effectTrans = EffectManager.GetEffect(effectData.effectName, parentTrans);
					if (effectTrans != null)
					{
						if (layer != -1)
						{
							Utility.SetLayerWithChildren(effectTrans, layer);
						}
						Vector3 basisScale = effectTrans.get_localScale();
						effectTrans.set_localScale(basisScale * effectData.scale);
						effectTrans.set_localPosition(effectData.offsetPos);
						effectTrans.set_localRotation(Quaternion.Euler(effectData.offsetRot));
						ResidentEffectObject residentEffect = effectTrans.get_gameObject().AddComponent<ResidentEffectObject>();
						residentEffect.Initialize(effectData);
						if (enemy != null)
						{
							enemy.RegisterResidentEffect(residentEffect);
						}
					}
				}
			}
		}
		if (param != null)
		{
			SystemEffectSetting sysEffectSetting = param.residentEffectSetting;
			SysEffectCreate(enemy, layer, sysEffectSetting);
		}
		if (need_shadow && param != null && param.shadowSize > 0f)
		{
			shadow = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.CreateShadow(param.shadowSize, param.bodyRadius, bodyScale, true, _this, shader_type == SHADER_TYPE.LIGHTWEIGHT);
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
				enemy.packetReceiver.SetStopPacketUpdate(false);
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
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		if (sysEffectSetting != null)
		{
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
			if (effectDataList != null)
			{
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
					if (!string.IsNullOrEmpty(data.effectName) && !string.IsNullOrEmpty(data.linkNodeName))
					{
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
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		if (!(animEventData == null) && !(enemy == null))
		{
			AnimEventData.AnimData[] animations = animEventData.animations;
			if (animations != null)
			{
				int i = 0;
				for (int num = animations.Length; i < num; i++)
				{
					AnimEventData.EventData[] events = animations[i].events;
					if (events != null)
					{
						int j = 0;
						for (int num2 = events.Length; j < num2; j++)
						{
							AnimEventData.EventData eventData = events[j];
							if (eventData != null)
							{
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
								}
							}
						}
					}
				}
			}
		}
	}

	public void DeleteLoadedObjects()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
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
