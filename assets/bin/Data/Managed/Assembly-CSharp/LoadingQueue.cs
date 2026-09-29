// Decompiled with JetBrains decompiler
// Type: LoadingQueue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class LoadingQueue
{
  private Queue<LoadObject> loadQueue = new Queue<LoadObject>();
  private MonoBehaviour monoBehaviour;

  public LoadingQueue(MonoBehaviour mono_behaviour) => this.monoBehaviour = mono_behaviour;

  public LoadingQueue(MonoBehaviour mono_behaviour, bool enable_ref_count)
  {
    this.monoBehaviour = mono_behaviour;
    if (!enable_ref_count)
      return;
    ResourceLoad resourceLoad = ResourceLoad.GetResourceLoad(mono_behaviour, true);
    if (resourceLoad.list != null)
      return;
    resourceLoad.list = new BetterList<ResourceObject>();
  }

  public LoadObject Load(
    bool isEventAsset,
    RESOURCE_CATEGORY category,
    string resource_name,
    bool cache_package = false)
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
        if (!Object.op_Equality((Object) MonoBehaviourSingleton<ResourceManager>.I.event_manifest, (Object) null))
        {
          Hash128 assetBundleHash = MonoBehaviourSingleton<ResourceManager>.I.event_manifest.GetAssetBundleHash(category.ToAssetBundleName(resource_name));
          if (((Hash128) ref assetBundleHash).isValid)
            break;
        }
        Log.Error("Missing event asset: " + resource_name);
        resource_name = category.ToString();
        break;
    }
    LoadObject loadObject = new LoadObject(isEventAsset, this.monoBehaviour, category, resource_name, cache_package);
    if (loadObject.isLoading)
      this.loadQueue.Enqueue(loadObject);
    return loadObject;
  }

  public LoadObject Load(RESOURCE_CATEGORY category, string resource_name, bool cache_package = false)
  {
    LoadObject loadObject = new LoadObject(this.monoBehaviour, category, resource_name, cache_package);
    if (loadObject.isLoading)
      this.loadQueue.Enqueue(loadObject);
    return loadObject;
  }

  public LoadObject LoadAssetBundleToCache(
    RESOURCE_CATEGORY category,
    string resource_name,
    bool cache_package = false)
  {
    LoadObject cache = new LoadObject(this.monoBehaviour, category, resource_name, cache_package, true);
    if (cache.isLoading)
      this.loadQueue.Enqueue(cache);
    return cache;
  }

  public LoadAndInstantiateObject LoadAndInstantiate(
    RESOURCE_CATEGORY category,
    string resource_name)
  {
    LoadAndInstantiateObject instantiateObject = new LoadAndInstantiateObject(this.monoBehaviour, category, resource_name);
    if (instantiateObject.isLoading)
      this.loadQueue.Enqueue((LoadObject) instantiateObject);
    return instantiateObject;
  }

  public LoadObject Load(
    RESOURCE_CATEGORY category,
    string package_name,
    string[] resource_names,
    bool cache_package = false)
  {
    LoadObject loadObject = new LoadObject(this.monoBehaviour, category, package_name, resource_names, cache_package);
    if (loadObject.isLoading)
      this.loadQueue.Enqueue(loadObject);
    return loadObject;
  }

  public LoadObject LoadItemIcon(string icon_name)
  {
    LoadObject loadObject = new LoadObject(this.monoBehaviour, RESOURCE_CATEGORY.ICON_ITEM, RESOURCE_CATEGORY.ICON_ITEM.ToHash256String((byte) Utility.GetHash(icon_name)), new string[1]
    {
      icon_name
    });
    if (loadObject.isLoading)
      this.loadQueue.Enqueue(loadObject);
    return loadObject;
  }

  public LoadObject LoadSE(int se_id)
  {
    return this.Load(RESOURCE_CATEGORY.SOUND_SE, ResourceName.GetSEPackage(se_id), new string[1]
    {
      ResourceName.GetSE(se_id)
    }, true);
  }

  public LoadObject QuickLoad(RESOURCE_CATEGORY category, string resource_name, bool cache_package = false)
  {
    LoadObject loadObject = new LoadObject();
    loadObject.QuickLoad(this.monoBehaviour, category, resource_name, cache_package);
    if (loadObject.isLoading)
      this.loadQueue.Enqueue(loadObject);
    return loadObject;
  }

  public LoadObject QuickLoad(
    RESOURCE_CATEGORY category,
    string package_name,
    string[] resource_names,
    bool cache_package = false)
  {
    LoadObject loadObject = new LoadObject();
    loadObject.QuickLoad(this.monoBehaviour, category, package_name, resource_names, cache_package);
    if (loadObject.isLoading)
      this.loadQueue.Enqueue(loadObject);
    return loadObject;
  }

  public void CacheSE(int se_id, List<LoadObject> los = null)
  {
    LoadObject loadObject = this.Load(RESOURCE_CATEGORY.SOUND_SE, ResourceName.GetSEPackage(se_id), new string[1]
    {
      ResourceName.GetSE(se_id)
    });
    los?.Add(loadObject);
  }

  public void CacheVoice(int voice_id, List<LoadObject> los = null)
  {
    LoadObject loadObject = this.Load(RESOURCE_CATEGORY.SOUND_VOICE, ResourceName.GetStoryVoicePackageNameFromVoiceID(voice_id), new string[1]
    {
      ResourceName.GetStoryVoiceName(voice_id)
    });
    los?.Add(loadObject);
  }

  public void CacheActionVoice(int voice_id, List<LoadObject> los = null)
  {
    LoadObject loadObject = this.Load(RESOURCE_CATEGORY.SOUND_VOICE, ResourceName.GetActionVoicePackageNameFromVoiceID(voice_id), new string[1]
    {
      ResourceName.GetActionVoiceName(voice_id)
    });
    los?.Add(loadObject);
  }

  public void CacheStage(RESOURCE_CATEGORY category, string name)
  {
    this.LoadStage(category, name, true, true);
  }

  public LoadObject LoadStage(
    RESOURCE_CATEGORY category,
    string name,
    bool cache_package,
    bool check_cache = false)
  {
    if (string.IsNullOrEmpty(name))
      return (LoadObject) null;
    if (check_cache)
    {
      ResourceObject cachedResourceObject = MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedResourceObject(category, name);
      if (cachedResourceObject != null)
      {
        ResourceLoad.GetResourceLoad(this.monoBehaviour).SetReference(cachedResourceObject);
        return (LoadObject) null;
      }
    }
    return this.Load(category, name, cache_package);
  }

  public void CacheEffect(RESOURCE_CATEGORY category, string name)
  {
    if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.IsDisableEffectGraphicLow(name))
      return;
    this.LoadEffect(category, name, true);
  }

  public LoadObject LoadEffect(RESOURCE_CATEGORY category, string name, bool check_cache = false)
  {
    if (string.IsNullOrEmpty(name))
      return (LoadObject) null;
    name = ResourceName.AddAttributID(name);
    if (check_cache)
    {
      ResourceObject cachedResourceObject = MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedResourceObject(category, name);
      if (cachedResourceObject != null)
      {
        ResourceLoad.GetResourceLoad(this.monoBehaviour).SetReference(cachedResourceObject);
        return (LoadObject) null;
      }
    }
    return this.Load(category, name);
  }

  public void CacheAnimDataUseResource(
    AnimEventData animEventData,
    LoadingQueue.EffectNameAnalyzer name_analyzer = null,
    List<AnimEventData.EventData> cntAtkDataList = null)
  {
    if (Object.op_Equality((Object) animEventData, (Object) null))
      return;
    animEventData.Initialize();
    AnimEventData.AnimData[] animations = animEventData.animations;
    int index1 = 0;
    for (int length1 = animations.Length; index1 < length1; ++index1)
    {
      AnimEventData.EventData[] events = animations[index1].events;
      int index2 = 0;
      for (int length2 = events.Length; index2 < length2; ++index2)
      {
        AnimEventData.EventData eventData = events[index2];
        switch (eventData.id)
        {
          case AnimEventFormat.ID.EFFECT:
          case AnimEventFormat.ID.EFFECT_ONESHOT:
          case AnimEventFormat.ID.EFFECT_STATIC:
          case AnimEventFormat.ID.EFFECT_LOOP_CUSTOM:
          case AnimEventFormat.ID.CAMERA_EFFECT:
          case AnimEventFormat.ID.EFFECT_SCALE_DEPEND_VALUE:
          case AnimEventFormat.ID.EFFECT_ONESHOT_ON_RAIN_SHOT_POS:
            string str1 = eventData.stringArgs[0];
            if (name_analyzer != null)
              str1 = name_analyzer(str1);
            if (!string.IsNullOrEmpty(str1))
            {
              this.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, str1);
              break;
            }
            break;
          case AnimEventFormat.ID.SE_ONESHOT:
          case AnimEventFormat.ID.SE_LOOP_PLAY:
          case AnimEventFormat.ID.BUFF_START_SHIELD_REFLECT:
            this.CacheSE(eventData.intArgs[0]);
            break;
          case AnimEventFormat.ID.WEAKPOINT_ON:
            eventData.attackMode = Player.ATTACK_MODE.NONE;
            string stringArg1 = eventData.stringArgs.Length != 0 ? eventData.stringArgs[0] : "";
            if (!string.IsNullOrEmpty(stringArg1))
            {
              if (Enum.IsDefined(typeof (EQUIPMENT_TYPE), (object) stringArg1))
              {
                EQUIPMENT_TYPE equipment_type = (EQUIPMENT_TYPE) Enum.Parse(typeof (EQUIPMENT_TYPE), stringArg1);
                eventData.attackMode = Player.ConvertEquipmentTypeToAttackMode(equipment_type);
              }
              else
                Log.Error("Undefined EQUIPMENT_TYPE name:" + stringArg1);
            }
            Enemy.WEAK_STATE weakState = eventData.intArgs.Length > 1 ? (Enemy.WEAK_STATE) eventData.intArgs[1] : Enemy.WEAK_STATE.NONE;
            if (Enemy.IsWeakStateElementAttack(weakState) || Enemy.IsWeakStateSkillAttack(weakState) || Enemy.IsWeakStateHealAttack(weakState) || Enemy.IsWeakStateCannonAttack(weakState))
            {
              TargetMarker.EFFECT_TYPE effectType = Enemy.WeakStateToEffectType(weakState);
              if (effectType != TargetMarker.EFFECT_TYPE.NONE)
              {
                string str2 = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerSettings.effectNames[(int) effectType];
                switch (weakState)
                {
                  case Enemy.WEAK_STATE.WEAK_ELEMENT_ATTACK:
                  case Enemy.WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK:
                    int num1 = eventData.intArgs.Length > 2 ? eventData.intArgs[2] : -1;
                    if (num1 >= 0)
                    {
                      str2 += num1.ToString();
                      break;
                    }
                    break;
                  case Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK:
                    int num2 = eventData.intArgs.Length > 2 ? eventData.intArgs[2] : -1;
                    if (eventData.attackMode != Player.ATTACK_MODE.NONE && num2 >= 0)
                    {
                      str2 = string.Format(str2, (object) (int) (eventData.attackMode - 1), (object) num2);
                      break;
                    }
                    break;
                }
                this.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, str2);
                break;
              }
              break;
            }
            break;
          case AnimEventFormat.ID.WEAKPOINT_ALL_ON:
            eventData.attackMode = Player.ATTACK_MODE.NONE;
            string stringArg2 = eventData.stringArgs.Length != 0 ? eventData.stringArgs[0] : "";
            if (!string.IsNullOrEmpty(stringArg2))
            {
              if (!Enum.IsDefined(typeof (EQUIPMENT_TYPE), (object) stringArg2))
              {
                Log.Error("Undefined EQUIPMENT_TYPE name:" + stringArg2);
                break;
              }
              EQUIPMENT_TYPE equipment_type = (EQUIPMENT_TYPE) Enum.Parse(typeof (EQUIPMENT_TYPE), stringArg2);
              eventData.attackMode = Player.ConvertEquipmentTypeToAttackMode(equipment_type);
              break;
            }
            break;
          case AnimEventFormat.ID.CONTINUS_ATTACK:
            cntAtkDataList?.Add(eventData);
            string str3 = eventData.stringArgs[2];
            if (name_analyzer != null)
              str3 = name_analyzer(str3);
            if (!string.IsNullOrEmpty(str3))
            {
              this.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, str3);
              break;
            }
            break;
          case AnimEventFormat.ID.EFFECT_DEPEND_SP_ATTACK_TYPE:
            int index3 = 1;
            for (int length3 = eventData.stringArgs.Length; index3 < length3; ++index3)
            {
              string str4 = eventData.stringArgs[index3];
              if (name_analyzer != null && !string.IsNullOrEmpty(str4))
                str4 = name_analyzer(str4);
              if (!string.IsNullOrEmpty(str4))
                this.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, str4);
            }
            break;
          case AnimEventFormat.ID.GENERATE_AEGIS:
            string stringArg3 = eventData.stringArgs[0];
            if (!string.IsNullOrEmpty(stringArg3))
              this.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, stringArg3);
            if (eventData.intArgs.Length >= 3 && eventData.intArgs[2] > 0)
              this.CacheSE(eventData.intArgs[2]);
            if (eventData.intArgs.Length >= 4 && eventData.intArgs[3] > 0)
              this.CacheSE(eventData.intArgs[3]);
            if (eventData.intArgs.Length >= 5 && eventData.intArgs[4] > 0)
            {
              this.CacheSE(eventData.intArgs[4]);
              break;
            }
            break;
          case AnimEventFormat.ID.SUMMON_ENEMY:
            if (eventData.stringArgs != null && eventData.stringArgs.Length >= 1 && !string.IsNullOrEmpty(eventData.stringArgs[0]))
            {
              this.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, eventData.stringArgs[0]);
              break;
            }
            this.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_summon_01");
            break;
        }
      }
    }
  }

  public void CacheAnimDataUseResourceDependPlayer(Player player, AnimEventData animEventData)
  {
    if (Object.op_Equality((Object) player, (Object) null) || Object.op_Equality((Object) animEventData, (Object) null))
      return;
    animEventData.Initialize();
    foreach (AnimEventData.AnimData animation in animEventData.animations)
    {
      foreach (AnimEventData.EventData eventData in animation.events)
      {
        switch (eventData.id)
        {
          case AnimEventFormat.ID.EFFECT_DEPEND_WEAPON_ELEMENT:
            string stringArg = eventData.stringArgs[0];
            int currentWeaponElement1 = player.GetCurrentWeaponElement();
            if (currentWeaponElement1 < 6)
            {
              string name = stringArg + currentWeaponElement1.ToString();
              if (!string.IsNullOrEmpty(name))
              {
                this.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name);
                break;
              }
              break;
            }
            break;
          case AnimEventFormat.ID.SE_ONESHOT_DEPEND_WEAPON_ELEMENT:
            int currentWeaponElement2 = player.GetCurrentWeaponElement();
            if (currentWeaponElement2 <= 6 && eventData.intArgs.Length > currentWeaponElement2 && eventData.intArgs[currentWeaponElement2] != 0)
            {
              this.CacheSE(eventData.intArgs[currentWeaponElement2]);
              break;
            }
            break;
          case AnimEventFormat.ID.EFFECT_SWITCH_OBJECT_BY_CONDITION:
          case AnimEventFormat.ID.EFFECT_TILING:
            int num = eventData.intArgs == null || eventData.intArgs.Length <= 3 ? 0 : (eventData.intArgs[3] == 1 ? 1 : 0);
            string name1 = eventData.stringArgs[0];
            if (num != 0)
            {
              int currentWeaponElement3 = player.GetCurrentWeaponElement();
              if (currentWeaponElement3 < 6)
                name1 = $"{eventData.stringArgs[0]}{currentWeaponElement3:D2}";
              else
                break;
            }
            if (!string.IsNullOrEmpty(name1))
            {
              this.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name1);
              break;
            }
            break;
        }
      }
    }
  }

  public void CacheBulletDataUseResource(BulletData bulletData, Player player = null)
  {
    if (Object.op_Equality((Object) bulletData, (Object) null))
      return;
    BulletData.BulletBase data = bulletData.data;
    string effectName1 = data.GetEffectName(player);
    if (!effectName1.IsNullOrWhiteSpace())
      this.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectName1);
    this.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.landHiteffectName);
    if (Object.op_Inequality((Object) data.endBullet, (Object) null))
      this.CacheBulletDataUseResource(data.endBullet);
    if (bulletData.dataDecoyTurretBit != null && !string.IsNullOrEmpty(bulletData.dataDecoyTurretBit.chargeEffectName))
      this.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, bulletData.dataDecoyTurretBit.chargeEffectName);
    BulletData.BulletFunnel dataFunnel = bulletData.dataFunnel;
    if (dataFunnel != null && Object.op_Inequality((Object) dataFunnel.bitBullet, (Object) null))
      this.CacheBulletDataUseResource(dataFunnel.bitBullet);
    BulletData.BulletMine dataMine = bulletData.dataMine;
    if (dataMine != null && Object.op_Inequality((Object) dataMine.explodeBullet, (Object) null))
      this.CacheBulletDataUseResource(dataMine.explodeBullet);
    BulletData.BulletTracking dataTracking = bulletData.dataTracking;
    if (dataTracking != null && Object.op_Inequality((Object) dataTracking.emissionBullet, (Object) null))
      this.CacheBulletDataUseResource(dataTracking.emissionBullet);
    BulletData.BulletUndead dataUndead = bulletData.dataUndead;
    if (dataUndead != null && Object.op_Inequality((Object) dataUndead.closeBullet, (Object) null))
      this.CacheBulletDataUseResource(dataUndead.closeBullet);
    BulletData.BulletDig dataDig = bulletData.dataDig;
    if (dataDig != null && Object.op_Inequality((Object) dataDig.flyOutBullet, (Object) null))
      this.CacheBulletDataUseResource(dataDig.flyOutBullet);
    BulletData.BulletActionMine dataActionMine = bulletData.dataActionMine;
    if (dataActionMine != null && Object.op_Inequality((Object) dataActionMine.explodeBullet, (Object) null) && Object.op_Inequality((Object) dataActionMine.actionBullet, (Object) null))
    {
      this.CacheBulletDataUseResource(dataActionMine.explodeBullet);
      this.CacheBulletDataUseResource(dataActionMine.actionBullet);
      this.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, dataActionMine.appearEffectName);
      this.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, dataActionMine.actionEffectName1);
      this.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, dataActionMine.actionEffectName2);
    }
    BulletData.BulletBarrier dataBarrier = bulletData.dataBarrier;
    if (dataBarrier != null && !string.IsNullOrEmpty(dataBarrier.effectNameInBarrier))
      this.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, dataBarrier.effectNameInBarrier);
    if (bulletData.dataResurrectionHomingBullet != null)
      this.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_heal_04_03");
    BulletData.BulletBreakable dataBreakable = bulletData.dataBreakable;
    if (dataBreakable != null && Object.op_Inequality((Object) dataBreakable.emissionBulletOnBroken, (Object) null))
      this.CacheBulletDataUseResource(dataBreakable.emissionBulletOnBroken);
    if (bulletData.dataOracleSpearSp == null || !Object.op_Inequality((Object) player, (Object) null) || !player.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE))
      return;
    string effectName2 = bulletData.dataOracleSpearSp.GetEffectName(player);
    if (!effectName2.IsNullOrWhiteSpace())
      this.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectName2);
    if (bulletData.dataOracleSpearSp.chargedSEId <= 0)
      return;
    this.CacheSE(bulletData.dataOracleSpearSp.chargedSEId);
  }

  public void CacheItemIcon(int icon_id, List<LoadObject> los = null)
  {
    string itemIcon = ResourceName.GetItemIcon(icon_id);
    LoadObject loadObject = this.Load(RESOURCE_CATEGORY.ICON_ITEM, RESOURCE_CATEGORY.ICON_ITEM.ToHash256String((byte) Utility.GetHash(itemIcon)), new string[1]
    {
      itemIcon
    });
    los?.Add(loadObject);
  }

  public LoadObject LoadChatStamp(int stamp_id, bool cache_check = false)
  {
    return this.Load(RESOURCE_CATEGORY.UI_CHAT_STAMP, ResourceName.GetChatStamp(stamp_id), cache_check);
  }

  public LoadObject LoadSymbol(int symbol_id, bool cache_check = false)
  {
    return this.Load(RESOURCE_CATEGORY.UI_SYMBOL_MARK, ResourceName.GetSymbolImageName(symbol_id), cache_check);
  }

  public LoadObject LoadSymbolFrame(int symbol_id, bool cache_check = false)
  {
    return this.Load(RESOURCE_CATEGORY.UI_SYMBOL_MARK, ResourceName.GetSymbolFrameImageName(symbol_id), cache_check);
  }

  public bool IsLoading()
  {
    while (this.loadQueue.Count > 0)
    {
      if (this.loadQueue.Peek().isLoading)
        return true;
      this.loadQueue.Dequeue();
    }
    return false;
  }

  public bool IsStop() => this.loadQueue.Count <= 0;

  public Coroutine Wait() => this.monoBehaviour.StartCoroutine(this.DoWait());

  private IEnumerator DoWait()
  {
    while (this.IsLoading())
      yield return (object) null;
  }

  public delegate string EffectNameAnalyzer(string effect_name);
}
