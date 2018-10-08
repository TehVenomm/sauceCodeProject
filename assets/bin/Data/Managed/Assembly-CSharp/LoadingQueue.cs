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
			ResourceLoad resourceLoad = ResourceLoad.GetResourceLoad(mono_behaviour, true);
			if (resourceLoad.list == null)
			{
				resourceLoad.list = new BetterList<ResourceObject>();
			}
		}
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
		LoadObject loadObject = new LoadObject(monoBehaviour, RESOURCE_CATEGORY.ICON_ITEM, package_name, resource_names, false);
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
		}, true);
	}

	public void CacheSE(int se_id, List<LoadObject> los = null)
	{
		string sEPackage = ResourceName.GetSEPackage(se_id);
		string[] resource_names = new string[1]
		{
			ResourceName.GetSE(se_id)
		};
		LoadObject item = Load(RESOURCE_CATEGORY.SOUND_SE, sEPackage, resource_names, false);
		los?.Add(item);
	}

	public void CacheVoice(int voice_id, List<LoadObject> los = null)
	{
		string storyVoicePackageNameFromVoiceID = ResourceName.GetStoryVoicePackageNameFromVoiceID(voice_id);
		string[] resource_names = new string[1]
		{
			ResourceName.GetStoryVoiceName(voice_id)
		};
		LoadObject item = Load(RESOURCE_CATEGORY.SOUND_VOICE, storyVoicePackageNameFromVoiceID, resource_names, false);
		los?.Add(item);
	}

	public void CacheActionVoice(int voice_id, List<LoadObject> los = null)
	{
		string actionVoicePackageNameFromVoiceID = ResourceName.GetActionVoicePackageNameFromVoiceID(voice_id);
		string[] resource_names = new string[1]
		{
			ResourceName.GetActionVoiceName(voice_id)
		};
		LoadObject item = Load(RESOURCE_CATEGORY.SOUND_VOICE, actionVoicePackageNameFromVoiceID, resource_names, false);
		los?.Add(item);
	}

	public void CacheEffect(RESOURCE_CATEGORY category, string name)
	{
		if (!MonoBehaviourSingleton<InGameManager>.IsValid() || !MonoBehaviourSingleton<InGameManager>.I.IsDisableEffectGraphicLow(name))
		{
			LoadEffect(category, name, true);
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
				ResourceLoad resourceLoad = ResourceLoad.GetResourceLoad(monoBehaviour, false);
				resourceLoad.SetReference(cachedResourceObject);
				return null;
			}
		}
		return Load(category, name, false);
	}

	public void CacheAnimDataUseResource(AnimEventData animEventData, EffectNameAnalyzer name_analyzer = null, List<AnimEventData.EventData> cntAtkDataList = null)
	{
		if (!(animEventData == null))
		{
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
					{
						string text4 = eventData.stringArgs[0];
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
					case AnimEventFormat.ID.EFFECT_DEPEND_SP_ATTACK_TYPE:
					{
						int k = 1;
						for (int num4 = eventData.stringArgs.Length; k < num4; k++)
						{
							string text7 = eventData.stringArgs[k];
							if (name_analyzer != null && !string.IsNullOrEmpty(text7))
							{
								text7 = name_analyzer(text7);
							}
							if (!string.IsNullOrEmpty(text7))
							{
								CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text7);
							}
						}
						break;
					}
					case AnimEventFormat.ID.SE_ONESHOT:
					case AnimEventFormat.ID.SE_LOOP_PLAY:
						CacheSE(eventData.intArgs[0], null);
						break;
					case AnimEventFormat.ID.CONTINUS_ATTACK:
					{
						cntAtkDataList?.Add(eventData);
						string text5 = eventData.stringArgs[2];
						if (name_analyzer != null)
						{
							text5 = name_analyzer(text5);
						}
						if (!string.IsNullOrEmpty(text5))
						{
							CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text5);
						}
						break;
					}
					case AnimEventFormat.ID.WEAKPOINT_ON:
					{
						Enemy.WEAK_STATE wEAK_STATE = (Enemy.WEAK_STATE)((eventData.intArgs.Length > 1) ? eventData.intArgs[1] : 0);
						if (Enemy.IsWeakStateElementAttack(wEAK_STATE) || Enemy.IsWeakStateSkillAttack(wEAK_STATE) || Enemy.IsWeakStateHealAttack(wEAK_STATE) || Enemy.IsWeakStateCannonAttack(wEAK_STATE))
						{
							TargetMarker.EFFECT_TYPE eFFECT_TYPE = Enemy.WeakStateToEffectType(wEAK_STATE);
							if (eFFECT_TYPE != TargetMarker.EFFECT_TYPE.NONE)
							{
								string text2 = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerSettings.effectNames[(int)eFFECT_TYPE];
								Enemy.WEAK_STATE wEAK_STATE2 = wEAK_STATE;
								if (wEAK_STATE2 == Enemy.WEAK_STATE.WEAK_ELEMENT_ATTACK || wEAK_STATE2 == Enemy.WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK)
								{
									int num3 = (eventData.intArgs.Length <= 2) ? (-1) : eventData.intArgs[2];
									if (num3 >= 0)
									{
										text2 += num3.ToString();
									}
								}
								CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text2);
							}
						}
						eventData.attackMode = Player.ATTACK_MODE.NONE;
						string text3 = (eventData.stringArgs.Length <= 0) ? string.Empty : eventData.stringArgs[0];
						if (!string.IsNullOrEmpty(text3))
						{
							if (!Enum.IsDefined(typeof(EQUIPMENT_TYPE), text3))
							{
								Log.Error("Undefined EQUIPMENT_TYPE name:" + text3);
							}
							else
							{
								EQUIPMENT_TYPE equipment_type = (EQUIPMENT_TYPE)(int)Enum.Parse(typeof(EQUIPMENT_TYPE), text3);
								eventData.attackMode = Player.ConvertEquipmentTypeToAttackMode(equipment_type);
							}
						}
						break;
					}
					case AnimEventFormat.ID.WEAKPOINT_ALL_ON:
					{
						eventData.attackMode = Player.ATTACK_MODE.NONE;
						string text6 = (eventData.stringArgs.Length <= 0) ? string.Empty : eventData.stringArgs[0];
						if (!string.IsNullOrEmpty(text6))
						{
							if (!Enum.IsDefined(typeof(EQUIPMENT_TYPE), text6))
							{
								Log.Error("Undefined EQUIPMENT_TYPE name:" + text6);
							}
							else
							{
								EQUIPMENT_TYPE equipment_type2 = (EQUIPMENT_TYPE)(int)Enum.Parse(typeof(EQUIPMENT_TYPE), text6);
								eventData.attackMode = Player.ConvertEquipmentTypeToAttackMode(equipment_type2);
							}
						}
						break;
					}
					case AnimEventFormat.ID.GENERATE_AEGIS:
					{
						string text = eventData.stringArgs[0];
						if (!string.IsNullOrEmpty(text))
						{
							CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text);
						}
						if (eventData.intArgs.Length >= 3 && eventData.intArgs[2] > 0)
						{
							CacheSE(eventData.intArgs[2], null);
						}
						if (eventData.intArgs.Length >= 4 && eventData.intArgs[3] > 0)
						{
							CacheSE(eventData.intArgs[3], null);
						}
						if (eventData.intArgs.Length >= 5 && eventData.intArgs[4] > 0)
						{
							CacheSE(eventData.intArgs[4], null);
						}
						break;
					}
					}
				}
			}
		}
	}

	public void CacheAnimDataUseResourceDependPlayer(Player player, AnimEventData animEventData)
	{
		if (!(player == null) && !(animEventData == null))
		{
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
					case AnimEventFormat.ID.SE_ONESHOT_DEPEND_WEAPON_ELEMENT:
					{
						int currentWeaponElement = player.GetCurrentWeaponElement();
						if (currentWeaponElement <= 6 && eventData.intArgs.Length > currentWeaponElement && eventData.intArgs[currentWeaponElement] != 0)
						{
							CacheSE(eventData.intArgs[currentWeaponElement], null);
						}
						break;
					}
					}
				}
			}
		}
	}

	public void CacheBulletDataUseResource(BulletData bulletData, Player player = null)
	{
		if (!(bulletData == null))
		{
			BulletData.BulletBase data = bulletData.data;
			string effectName = data.GetEffectName(player);
			if (!effectName.IsNullOrWhiteSpace())
			{
				CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectName);
			}
			CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.landHiteffectName);
			if (data.endBullet != null)
			{
				CacheBulletDataUseResource(data.endBullet, null);
			}
			BulletData.BulletFunnel dataFunnel = bulletData.dataFunnel;
			if (dataFunnel != null && dataFunnel.bitBullet != null)
			{
				CacheBulletDataUseResource(dataFunnel.bitBullet, null);
			}
			BulletData.BulletMine dataMine = bulletData.dataMine;
			if (dataMine != null && dataMine.explodeBullet != null)
			{
				CacheBulletDataUseResource(dataMine.explodeBullet, null);
			}
			BulletData.BulletTracking dataTracking = bulletData.dataTracking;
			if (dataTracking != null && dataTracking.emissionBullet != null)
			{
				CacheBulletDataUseResource(dataTracking.emissionBullet, null);
			}
			BulletData.BulletUndead dataUndead = bulletData.dataUndead;
			if (dataUndead != null && dataUndead.closeBullet != null)
			{
				CacheBulletDataUseResource(dataUndead.closeBullet, null);
			}
			BulletData.BulletDig dataDig = bulletData.dataDig;
			if (dataDig != null && dataDig.flyOutBullet != null)
			{
				CacheBulletDataUseResource(dataDig.flyOutBullet, null);
			}
			BulletData.BulletActionMine dataActionMine = bulletData.dataActionMine;
			if (dataActionMine != null && dataActionMine.explodeBullet != null && dataActionMine.actionBullet != null)
			{
				CacheBulletDataUseResource(dataActionMine.explodeBullet, null);
				CacheBulletDataUseResource(dataActionMine.actionBullet, null);
				CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, dataActionMine.appearEffectName);
				CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, dataActionMine.actionEffectName1);
				CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, dataActionMine.actionEffectName2);
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
		LoadObject item = Load(RESOURCE_CATEGORY.ICON_ITEM, package_name, resource_names, false);
		los?.Add(item);
	}

	public LoadObject LoadChatStamp(int stamp_id, bool cache_check = false)
	{
		return Load(RESOURCE_CATEGORY.UI_CHAT_STAMP, ResourceName.GetChatStamp(stamp_id), cache_check);
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

	public Coroutine Wait()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		return monoBehaviour.StartCoroutine(DoWait());
	}

	private IEnumerator DoWait()
	{
		while (IsLoading())
		{
			yield return (object)null;
		}
	}
}
