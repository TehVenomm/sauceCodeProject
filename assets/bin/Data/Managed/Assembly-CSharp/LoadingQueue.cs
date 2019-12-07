using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingQueue
{
	public delegate string EffectNameAnalyzer(string effect_name);

	private Queue<LoadObject> loadQueue = new Queue<LoadObject>();

	private MonoBehaviour monoBehaviour;

	public LoadingQueue(MonoBehaviour mono_behaviour)
	{
		monoBehaviour = mono_behaviour;
	}

	public LoadingQueue(MonoBehaviour mono_behaviour, bool enable_ref_count)
	{
		monoBehaviour = mono_behaviour;
		if (enable_ref_count)
		{
			ResourceLoad resourceLoad = ResourceLoad.GetResourceLoad(mono_behaviour, destroy_notify: true);
			if (resourceLoad.list == null)
			{
				resourceLoad.list = new BetterList<ResourceObject>();
			}
		}
	}

	public LoadObject Load(bool isEventAsset, RESOURCE_CATEGORY category, string resource_name, bool cache_package = false)
	{
		switch (category)
		{
		case RESOURCE_CATEGORY.DEGREE_FRAME:
		case RESOURCE_CATEGORY.EVENT_BG:
		case RESOURCE_CATEGORY.EVENT_ICON:
		case RESOURCE_CATEGORY.GACHA_BANNER:
		case RESOURCE_CATEGORY.HOME_BANNER_ADS:
		case RESOURCE_CATEGORY.HOME_BANNER_IMAGE:
		case RESOURCE_CATEGORY.HOME_GACHA_DECO_IMAGE:
		case RESOURCE_CATEGORY.LOGINBONUS_IMAGE:
		case RESOURCE_CATEGORY.SHOP_IMG:
		case RESOURCE_CATEGORY.TIPS_IMAGE:
			if (MonoBehaviourSingleton<ResourceManager>.I.event_manifest == null || !MonoBehaviourSingleton<ResourceManager>.I.event_manifest.GetAssetBundleHash(category.ToAssetBundleName(resource_name)).isValid)
			{
				Log.Error("Missing event asset: " + resource_name);
				resource_name = category.ToString();
			}
			break;
		}
		LoadObject loadObject = new LoadObject(isEventAsset, monoBehaviour, category, resource_name, cache_package);
		if (loadObject.isLoading)
		{
			loadQueue.Enqueue(loadObject);
		}
		return loadObject;
	}

	public LoadObject Load(RESOURCE_CATEGORY category, string resource_name, bool cache_package = false)
	{
		LoadObject loadObject = new LoadObject(monoBehaviour, category, resource_name, cache_package);
		if (loadObject.isLoading)
		{
			loadQueue.Enqueue(loadObject);
		}
		return loadObject;
	}

	public LoadObject LoadAssetBundleToCache(RESOURCE_CATEGORY category, string resource_name, bool cache_package = false)
	{
		LoadObject loadObject = new LoadObject(monoBehaviour, category, resource_name, cache_package, unload_bundle: true);
		if (loadObject.isLoading)
		{
			loadQueue.Enqueue(loadObject);
		}
		return loadObject;
	}

	public LoadAndInstantiateObject LoadAndInstantiate(RESOURCE_CATEGORY category, string resource_name)
	{
		LoadAndInstantiateObject loadAndInstantiateObject = new LoadAndInstantiateObject(monoBehaviour, category, resource_name);
		if (loadAndInstantiateObject.isLoading)
		{
			loadQueue.Enqueue(loadAndInstantiateObject);
		}
		return loadAndInstantiateObject;
	}

	public LoadObject Load(RESOURCE_CATEGORY category, string package_name, string[] resource_names, bool cache_package = false)
	{
		LoadObject loadObject = new LoadObject(monoBehaviour, category, package_name, resource_names, cache_package);
		if (loadObject.isLoading)
		{
			loadQueue.Enqueue(loadObject);
		}
		return loadObject;
	}

	public LoadObject LoadItemIcon(string icon_name)
	{
		byte hash = (byte)Utility.GetHash(icon_name);
		string package_name = RESOURCE_CATEGORY.ICON_ITEM.ToHash256String(hash);
		string[] resource_names = new string[1]
		{
			icon_name
		};
		LoadObject loadObject = new LoadObject(monoBehaviour, RESOURCE_CATEGORY.ICON_ITEM, package_name, resource_names);
		if (loadObject.isLoading)
		{
			loadQueue.Enqueue(loadObject);
		}
		return loadObject;
	}

	public LoadObject LoadSE(int se_id)
	{
		return Load(RESOURCE_CATEGORY.SOUND_SE, ResourceName.GetSEPackage(se_id), new string[1]
		{
			ResourceName.GetSE(se_id)
		}, cache_package: true);
	}

	public LoadObject QuickLoad(RESOURCE_CATEGORY category, string resource_name, bool cache_package = false)
	{
		LoadObject loadObject = new LoadObject();
		loadObject.QuickLoad(monoBehaviour, category, resource_name, cache_package);
		if (loadObject.isLoading)
		{
			loadQueue.Enqueue(loadObject);
		}
		return loadObject;
	}

	public LoadObject QuickLoad(RESOURCE_CATEGORY category, string package_name, string[] resource_names, bool cache_package = false)
	{
		LoadObject loadObject = new LoadObject();
		loadObject.QuickLoad(monoBehaviour, category, package_name, resource_names, cache_package);
		if (loadObject.isLoading)
		{
			loadQueue.Enqueue(loadObject);
		}
		return loadObject;
	}

	public void CacheSE(int se_id, List<LoadObject> los = null)
	{
		string sEPackage = ResourceName.GetSEPackage(se_id);
		string[] resource_names = new string[1]
		{
			ResourceName.GetSE(se_id)
		};
		LoadObject item = Load(RESOURCE_CATEGORY.SOUND_SE, sEPackage, resource_names);
		los?.Add(item);
	}

	public void CacheVoice(int voice_id, List<LoadObject> los = null)
	{
		string storyVoicePackageNameFromVoiceID = ResourceName.GetStoryVoicePackageNameFromVoiceID(voice_id);
		string[] resource_names = new string[1]
		{
			ResourceName.GetStoryVoiceName(voice_id)
		};
		LoadObject item = Load(RESOURCE_CATEGORY.SOUND_VOICE, storyVoicePackageNameFromVoiceID, resource_names);
		los?.Add(item);
	}

	public void CacheActionVoice(int voice_id, List<LoadObject> los = null)
	{
		string actionVoicePackageNameFromVoiceID = ResourceName.GetActionVoicePackageNameFromVoiceID(voice_id);
		string[] resource_names = new string[1]
		{
			ResourceName.GetActionVoiceName(voice_id)
		};
		LoadObject item = Load(RESOURCE_CATEGORY.SOUND_VOICE, actionVoicePackageNameFromVoiceID, resource_names);
		los?.Add(item);
	}

	public void CacheStage(RESOURCE_CATEGORY category, string name)
	{
		LoadStage(category, name, cache_package: true, check_cache: true);
	}

	public LoadObject LoadStage(RESOURCE_CATEGORY category, string name, bool cache_package, bool check_cache = false)
	{
		if (string.IsNullOrEmpty(name))
		{
			return null;
		}
		if (check_cache)
		{
			ResourceObject cachedResourceObject = MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedResourceObject(category, name);
			if (cachedResourceObject != null)
			{
				ResourceLoad.GetResourceLoad(monoBehaviour).SetReference(cachedResourceObject);
				return null;
			}
		}
		return Load(category, name, cache_package);
	}

	public void CacheEffect(RESOURCE_CATEGORY category, string name)
	{
		if (!MonoBehaviourSingleton<InGameManager>.IsValid() || !MonoBehaviourSingleton<InGameManager>.I.IsDisableEffectGraphicLow(name))
		{
			LoadEffect(category, name, check_cache: true);
		}
	}

	public LoadObject LoadEffect(RESOURCE_CATEGORY category, string name, bool check_cache = false)
	{
		if (string.IsNullOrEmpty(name))
		{
			return null;
		}
		name = ResourceName.AddAttributID(name);
		if (check_cache)
		{
			ResourceObject cachedResourceObject = MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedResourceObject(category, name);
			if (cachedResourceObject != null)
			{
				ResourceLoad.GetResourceLoad(monoBehaviour).SetReference(cachedResourceObject);
				return null;
			}
		}
		return Load(category, name);
	}

	public void CacheAnimDataUseResource(AnimEventData animEventData, EffectNameAnalyzer name_analyzer = null, List<AnimEventData.EventData> cntAtkDataList = null)
	{
		if (animEventData == null)
		{
			return;
		}
		animEventData.Initialize();
		AnimEventData.AnimData[] animations = animEventData.animations;
		int i = 0;
		for (int num = animations.Length; i < num; i++)
		{
			AnimEventData.EventData[] events = animations[i].events;
			int j = 0;
			for (int num2 = events.Length; j < num2; j++)
			{
				AnimEventData.EventData eventData = events[j];
				switch (eventData.id)
				{
				case AnimEventFormat.ID.EFFECT:
				case AnimEventFormat.ID.EFFECT_ONESHOT:
				case AnimEventFormat.ID.EFFECT_STATIC:
				case AnimEventFormat.ID.EFFECT_LOOP_CUSTOM:
				case AnimEventFormat.ID.CAMERA_EFFECT:
				case AnimEventFormat.ID.EFFECT_SCALE_DEPEND_VALUE:
				case AnimEventFormat.ID.EFFECT_ONESHOT_ON_RAIN_SHOT_POS:
				{
					string text2 = eventData.stringArgs[0];
					if (name_analyzer != null)
					{
						text2 = name_analyzer(text2);
					}
					if (!string.IsNullOrEmpty(text2))
					{
						CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text2);
					}
					break;
				}
				case AnimEventFormat.ID.EFFECT_DEPEND_SP_ATTACK_TYPE:
				{
					int k = 1;
					for (int num3 = eventData.stringArgs.Length; k < num3; k++)
					{
						string text3 = eventData.stringArgs[k];
						if (name_analyzer != null && !string.IsNullOrEmpty(text3))
						{
							text3 = name_analyzer(text3);
						}
						if (!string.IsNullOrEmpty(text3))
						{
							CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text3);
						}
					}
					break;
				}
				case AnimEventFormat.ID.SE_ONESHOT:
				case AnimEventFormat.ID.SE_LOOP_PLAY:
				case AnimEventFormat.ID.BUFF_START_SHIELD_REFLECT:
					CacheSE(eventData.intArgs[0]);
					break;
				case AnimEventFormat.ID.CONTINUS_ATTACK:
				{
					cntAtkDataList?.Add(eventData);
					string text4 = eventData.stringArgs[2];
					if (name_analyzer != null)
					{
						text4 = name_analyzer(text4);
					}
					if (!string.IsNullOrEmpty(text4))
					{
						CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text4);
					}
					break;
				}
				case AnimEventFormat.ID.WEAKPOINT_ON:
				{
					eventData.attackMode = Player.ATTACK_MODE.NONE;
					string text6 = (eventData.stringArgs.Length != 0) ? eventData.stringArgs[0] : "";
					if (!string.IsNullOrEmpty(text6))
					{
						if (Enum.IsDefined(typeof(EQUIPMENT_TYPE), text6))
						{
							EQUIPMENT_TYPE equipment_type2 = (EQUIPMENT_TYPE)Enum.Parse(typeof(EQUIPMENT_TYPE), text6);
							eventData.attackMode = Player.ConvertEquipmentTypeToAttackMode(equipment_type2);
						}
						else
						{
							Log.Error("Undefined EQUIPMENT_TYPE name:" + text6);
						}
					}
					Enemy.WEAK_STATE wEAK_STATE = (Enemy.WEAK_STATE)((eventData.intArgs.Length > 1) ? eventData.intArgs[1] : 0);
					if (!Enemy.IsWeakStateElementAttack(wEAK_STATE) && !Enemy.IsWeakStateSkillAttack(wEAK_STATE) && !Enemy.IsWeakStateHealAttack(wEAK_STATE) && !Enemy.IsWeakStateCannonAttack(wEAK_STATE))
					{
						break;
					}
					TargetMarker.EFFECT_TYPE eFFECT_TYPE = Enemy.WeakStateToEffectType(wEAK_STATE);
					if (eFFECT_TYPE == TargetMarker.EFFECT_TYPE.NONE)
					{
						break;
					}
					string text7 = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerSettings.effectNames[(int)eFFECT_TYPE];
					switch (wEAK_STATE)
					{
					case Enemy.WEAK_STATE.WEAK_ELEMENT_ATTACK:
					case Enemy.WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK:
					{
						int num5 = (eventData.intArgs.Length > 2) ? eventData.intArgs[2] : (-1);
						if (num5 >= 0)
						{
							text7 += num5.ToString();
						}
						break;
					}
					case Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK:
					{
						int num4 = (eventData.intArgs.Length > 2) ? eventData.intArgs[2] : (-1);
						if (eventData.attackMode != 0 && num4 >= 0)
						{
							text7 = string.Format(text7, (int)(eventData.attackMode - 1), num4);
						}
						break;
					}
					}
					CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text7);
					break;
				}
				case AnimEventFormat.ID.WEAKPOINT_ALL_ON:
				{
					eventData.attackMode = Player.ATTACK_MODE.NONE;
					string text = (eventData.stringArgs.Length != 0) ? eventData.stringArgs[0] : "";
					if (!string.IsNullOrEmpty(text))
					{
						if (!Enum.IsDefined(typeof(EQUIPMENT_TYPE), text))
						{
							Log.Error("Undefined EQUIPMENT_TYPE name:" + text);
							break;
						}
						EQUIPMENT_TYPE equipment_type = (EQUIPMENT_TYPE)Enum.Parse(typeof(EQUIPMENT_TYPE), text);
						eventData.attackMode = Player.ConvertEquipmentTypeToAttackMode(equipment_type);
					}
					break;
				}
				case AnimEventFormat.ID.GENERATE_AEGIS:
				{
					string text5 = eventData.stringArgs[0];
					if (!string.IsNullOrEmpty(text5))
					{
						CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text5);
					}
					if (eventData.intArgs.Length >= 3 && eventData.intArgs[2] > 0)
					{
						CacheSE(eventData.intArgs[2]);
					}
					if (eventData.intArgs.Length >= 4 && eventData.intArgs[3] > 0)
					{
						CacheSE(eventData.intArgs[3]);
					}
					if (eventData.intArgs.Length >= 5 && eventData.intArgs[4] > 0)
					{
						CacheSE(eventData.intArgs[4]);
					}
					break;
				}
				case AnimEventFormat.ID.SUMMON_ENEMY:
					if (eventData.stringArgs != null && eventData.stringArgs.Length >= 1 && !string.IsNullOrEmpty(eventData.stringArgs[0]))
					{
						CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, eventData.stringArgs[0]);
					}
					else
					{
						CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_summon_01");
					}
					break;
				}
			}
		}
	}

	public void CacheAnimDataUseResourceDependPlayer(Player player, AnimEventData animEventData)
	{
		if (player == null || animEventData == null)
		{
			return;
		}
		animEventData.Initialize();
		AnimEventData.AnimData[] animations = animEventData.animations;
		for (int i = 0; i < animations.Length; i++)
		{
			AnimEventData.EventData[] events = animations[i].events;
			foreach (AnimEventData.EventData eventData in events)
			{
				switch (eventData.id)
				{
				case AnimEventFormat.ID.EFFECT_DEPEND_WEAPON_ELEMENT:
				{
					string str = eventData.stringArgs[0];
					int currentWeaponElement2 = player.GetCurrentWeaponElement();
					if (currentWeaponElement2 < 6)
					{
						str += currentWeaponElement2.ToString();
						if (!string.IsNullOrEmpty(str))
						{
							CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, str);
						}
					}
					break;
				}
				case AnimEventFormat.ID.EFFECT_SWITCH_OBJECT_BY_CONDITION:
				case AnimEventFormat.ID.EFFECT_TILING:
				{
					bool num = eventData.intArgs != null && eventData.intArgs.Length > 3 && eventData.intArgs[3] == 1;
					string text = eventData.stringArgs[0];
					if (num)
					{
						int currentWeaponElement3 = player.GetCurrentWeaponElement();
						if (currentWeaponElement3 >= 6)
						{
							break;
						}
						text = $"{eventData.stringArgs[0]}{currentWeaponElement3:D2}";
					}
					if (!string.IsNullOrEmpty(text))
					{
						CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text);
					}
					break;
				}
				case AnimEventFormat.ID.SE_ONESHOT_DEPEND_WEAPON_ELEMENT:
				{
					int currentWeaponElement = player.GetCurrentWeaponElement();
					if (currentWeaponElement <= 6 && eventData.intArgs.Length > currentWeaponElement && eventData.intArgs[currentWeaponElement] != 0)
					{
						CacheSE(eventData.intArgs[currentWeaponElement]);
					}
					break;
				}
				}
			}
		}
	}

	public void CacheBulletDataUseResource(BulletData bulletData, Player player = null)
	{
		if (bulletData == null)
		{
			return;
		}
		BulletData.BulletBase data = bulletData.data;
		string effectName = data.GetEffectName(player);
		if (!effectName.IsNullOrWhiteSpace())
		{
			CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectName);
		}
		CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.landHiteffectName);
		if (data.endBullet != null)
		{
			CacheBulletDataUseResource(data.endBullet);
		}
		if (bulletData.dataDecoyTurretBit != null && !string.IsNullOrEmpty(bulletData.dataDecoyTurretBit.chargeEffectName))
		{
			CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, bulletData.dataDecoyTurretBit.chargeEffectName);
		}
		BulletData.BulletFunnel dataFunnel = bulletData.dataFunnel;
		if (dataFunnel != null && dataFunnel.bitBullet != null)
		{
			CacheBulletDataUseResource(dataFunnel.bitBullet);
		}
		BulletData.BulletMine dataMine = bulletData.dataMine;
		if (dataMine != null && dataMine.explodeBullet != null)
		{
			CacheBulletDataUseResource(dataMine.explodeBullet);
		}
		BulletData.BulletTracking dataTracking = bulletData.dataTracking;
		if (dataTracking != null && dataTracking.emissionBullet != null)
		{
			CacheBulletDataUseResource(dataTracking.emissionBullet);
		}
		BulletData.BulletUndead dataUndead = bulletData.dataUndead;
		if (dataUndead != null && dataUndead.closeBullet != null)
		{
			CacheBulletDataUseResource(dataUndead.closeBullet);
		}
		BulletData.BulletDig dataDig = bulletData.dataDig;
		if (dataDig != null && dataDig.flyOutBullet != null)
		{
			CacheBulletDataUseResource(dataDig.flyOutBullet);
		}
		BulletData.BulletActionMine dataActionMine = bulletData.dataActionMine;
		if (dataActionMine != null && dataActionMine.explodeBullet != null && dataActionMine.actionBullet != null)
		{
			CacheBulletDataUseResource(dataActionMine.explodeBullet);
			CacheBulletDataUseResource(dataActionMine.actionBullet);
			CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, dataActionMine.appearEffectName);
			CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, dataActionMine.actionEffectName1);
			CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, dataActionMine.actionEffectName2);
		}
		BulletData.BulletBarrier dataBarrier = bulletData.dataBarrier;
		if (dataBarrier != null && !string.IsNullOrEmpty(dataBarrier.effectNameInBarrier))
		{
			CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, dataBarrier.effectNameInBarrier);
		}
		if (bulletData.dataResurrectionHomingBullet != null)
		{
			CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_heal_04_03");
		}
		BulletData.BulletBreakable dataBreakable = bulletData.dataBreakable;
		if (dataBreakable != null && dataBreakable.emissionBulletOnBroken != null)
		{
			CacheBulletDataUseResource(dataBreakable.emissionBulletOnBroken);
		}
		if (bulletData.dataOracleSpearSp != null && player != null && player.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE))
		{
			string effectName2 = bulletData.dataOracleSpearSp.GetEffectName(player);
			if (!effectName2.IsNullOrWhiteSpace())
			{
				CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectName2);
			}
			if (bulletData.dataOracleSpearSp.chargedSEId > 0)
			{
				CacheSE(bulletData.dataOracleSpearSp.chargedSEId);
			}
		}
	}

	public void CacheItemIcon(int icon_id, List<LoadObject> los = null)
	{
		string itemIcon = ResourceName.GetItemIcon(icon_id);
		byte hash = (byte)Utility.GetHash(itemIcon);
		string package_name = RESOURCE_CATEGORY.ICON_ITEM.ToHash256String(hash);
		string[] resource_names = new string[1]
		{
			itemIcon
		};
		LoadObject item = Load(RESOURCE_CATEGORY.ICON_ITEM, package_name, resource_names);
		los?.Add(item);
	}

	public LoadObject LoadChatStamp(int stamp_id, bool cache_check = false)
	{
		return Load(RESOURCE_CATEGORY.UI_CHAT_STAMP, ResourceName.GetChatStamp(stamp_id), cache_check);
	}

	public LoadObject LoadSymbol(int symbol_id, bool cache_check = false)
	{
		return Load(RESOURCE_CATEGORY.UI_SYMBOL_MARK, ResourceName.GetSymbolImageName(symbol_id), cache_check);
	}

	public LoadObject LoadSymbolFrame(int symbol_id, bool cache_check = false)
	{
		return Load(RESOURCE_CATEGORY.UI_SYMBOL_MARK, ResourceName.GetSymbolFrameImageName(symbol_id), cache_check);
	}

	public bool IsLoading()
	{
		while (loadQueue.Count > 0)
		{
			if (loadQueue.Peek().isLoading)
			{
				return true;
			}
			loadQueue.Dequeue();
		}
		return false;
	}

	public bool IsStop()
	{
		if (loadQueue.Count <= 0)
		{
			return true;
		}
		return false;
	}

	public Coroutine Wait()
	{
		return monoBehaviour.StartCoroutine(DoWait());
	}

	private IEnumerator DoWait()
	{
		while (IsLoading())
		{
			yield return null;
		}
	}
}
