// Decompiled with JetBrains decompiler
// Type: EnemyLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

#nullable disable
public class EnemyLoader : ModelLoaderBase
{
  private static UIntKeyTable<Material> materialCaches;
  private float displayGachaScale = 1f;
  private bool kc;
  private AnimEventData.ResidentEffectData[] _residentEffectList;
  private int _layer;
  private EnemyParam _param;
  private System.Action _callback;
  private AnimEventData _tmpAnimEventData;
  private bool _need_stamp_effect;

  public static void ClearPoolObjects()
  {
    if (EnemyLoader.materialCaches == null)
      return;
    EnemyLoader.materialCaches.Clear();
    EnemyLoader.materialCaches = (UIntKeyTable<Material>) null;
  }

  public override bool IsLoading() => this.isLoading;

  public override Animator GetAnimator() => this.animator;

  public override Transform GetHead() => throw new NotImplementedException();

  public override void SetEnabled(bool is_enable) => throw new NotImplementedException();

  public int bodyID { get; private set; }

  public float bodyScale { get; private set; }

  public Transform body { get; private set; }

  protected Transform foundation { get; private set; }

  public Animator animator { get; private set; }

  public AnimEventData animEventData { get; private set; }

  public Transform shadow { get; private set; }

  public Renderer[] renderersBody { get; private set; }

  public Transform baseEffect { get; private set; }

  public float DisplayGachaScale
  {
    get => this.displayGachaScale;
    set => this.displayGachaScale = value;
  }

  public List<EnemyLoader.MaterialParams> materialParamsList { get; private set; }

  public bool isLoading { get; private set; }

  public void StartLoad(
    int body_id,
    int anim_id,
    float scale,
    string base_effect,
    string base_effect_node,
    bool need_shadow,
    bool enable_light_probes,
    bool need_anim_event_res_cache,
    SHADER_TYPE shader_type,
    int layer = -1,
    string foundation_name = null,
    bool need_stamp_effect = false,
    bool will_stock = false,
    string weather_effect = "",
    EnemyLoader.OnCompleteLoad callback = null)
  {
    if (this.isLoading)
      Log.Error(LOG.RESOURCE, ((Object) this).name + " now loading.");
    else if (Object.op_Inequality((Object) this.body, (Object) null))
      Log.Error(LOG.RESOURCE, ((Object) this).name + " loaded.");
    else
      this.StartCoroutine(this.DoLoad(body_id, anim_id, scale, base_effect, base_effect_node, need_shadow, enable_light_probes, need_anim_event_res_cache, shader_type, layer, foundation_name, need_stamp_effect, will_stock, weather_effect, callback));
  }

  private IEnumerator DoLoad(
    int body_id,
    int anim_id,
    float scale,
    string base_effect,
    string base_effect_node,
    bool need_shadow,
    bool enable_light_probes,
    bool need_anim_event_res_cache,
    SHADER_TYPE shader_type,
    int layer,
    string foundation_name,
    bool need_stamp_effect,
    bool will_stock,
    string weather_effect,
    EnemyLoader.OnCompleteLoad callback)
  {
    Enemy enemy = ((Component) this).gameObject.GetComponent<Enemy>();
    if (Object.op_Inequality((Object) enemy, (Object) null))
    {
      int id = enemy.id;
    }
    this.bodyID = body_id;
    this.bodyScale = scale;
    bool is_boss = false;
    if (Object.op_Inequality((Object) enemy, (Object) null))
    {
      is_boss = enemy.isBoss;
      if (Object.op_Inequality((Object) enemy.controller, (Object) null))
        ((Behaviour) enemy.controller).enabled = false;
      if (Object.op_Inequality((Object) enemy.packetReceiver, (Object) null))
        enemy.packetReceiver.SetStopPacketUpdate(true);
      enemy.OnLoadStart();
    }
    string enemyBody = ResourceName.GetEnemyBody(body_id);
    string enemyMaterial = ResourceName.GetEnemyMaterial(body_id);
    string enemyAnim = ResourceName.GetEnemyAnim(anim_id);
    Transform _this = ((Component) this).transform;
    this.isLoading = true;
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_body = (LoadObject) load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.ENEMY_MODEL, enemyBody);
    LoadObject loadObject;
    if (enemyMaterial == null)
      loadObject = (LoadObject) null;
    else
      loadObject = load_queue.Load(RESOURCE_CATEGORY.ENEMY_MATERIAL, enemyBody, new string[1]
      {
        enemyMaterial
      });
    LoadObject lo_mate = loadObject;
    LoadObject lo_anim = load_queue.Load(RESOURCE_CATEGORY.ENEMY_ANIM, enemyAnim, new string[2]
    {
      enemyAnim + "Ctrl",
      enemyAnim + "Event"
    });
    if (!string.IsNullOrEmpty(base_effect))
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, base_effect);
    LoadObject lo_foundation = (LoadObject) null;
    if (!string.IsNullOrEmpty(foundation_name))
    {
      if (!MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.enableEnemyModelFoundationFromQuestStage)
        foundation_name = "FST011";
      lo_foundation = (LoadObject) load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.FOUNDATION_MODEL, foundation_name);
    }
    yield return (object) load_queue.Wait();
    this.body = lo_body.Realizes(_this, layer == -1 ? 11 : layer);
    if (layer == -1)
      ((Component) this).gameObject.layer = 10;
    this.body.localPosition = Vector3.zero;
    this.body.localRotation = Quaternion.identity;
    this.renderersBody = ((Component) this.body).gameObject.GetComponentsInChildren<Renderer>();
    if (lo_mate != null && Object.op_Inequality(lo_mate.loadedObject, (Object) null) && this.renderersBody.Length == 1)
    {
      Material loadedObject = lo_mate.loadedObject as Material;
      if (Object.op_Inequality((Object) loadedObject, (Object) null))
        this.renderersBody[0].sharedMaterial = loadedObject;
    }
    if (Object.op_Inequality((Object) enemy, (Object) null))
      enemy.body = this.body;
    this.body.localScale = Vector3.Scale(this.body.localScale, new Vector3(scale, scale, scale));
    this.animator = ((Component) this.body).gameObject.GetComponent<Animator>();
    if (Object.op_Inequality((Object) this.animator, (Object) null) && lo_anim.loadedObjects != null)
    {
      this.animator.runtimeAnimatorController = (RuntimeAnimatorController) lo_anim.loadedObjects[0].obj;
      if (lo_anim.loadedObjects.Length >= 2 && lo_anim.loadedObjects[1] != null)
        this.animEventData = lo_anim.loadedObjects[1].obj as AnimEventData;
      if (Object.op_Inequality((Object) enemy, (Object) null))
      {
        ((Component) this.body).gameObject.AddComponent<StageObjectProxy>().stageObject = (StageObject) enemy;
        enemy.animEventData = this.animEventData;
      }
    }
    if (!string.IsNullOrEmpty(base_effect))
    {
      string name = base_effect_node;
      if (string.IsNullOrEmpty(name))
        name = "Root";
      Transform effect = EffectManager.GetEffect(base_effect, Utility.Find(this.body, name));
      if (Object.op_Inequality((Object) effect, (Object) null))
      {
        this.baseEffect = effect;
        if (layer != -1)
          Utility.SetLayerWithChildren(effect, layer);
      }
    }
    if (!string.IsNullOrEmpty(weather_effect) && MonoBehaviourSingleton<StageManager>.IsValid())
    {
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
      loadingQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, weather_effect);
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      MonoBehaviourSingleton<StageManager>.I.SetWeatherEffect(weather_effect);
    }
    if (shader_type == SHADER_TYPE.LIGHTWEIGHT)
      ShaderGlobal.ChangeWantLightweightShader(this.renderersBody);
    if (is_boss)
    {
      this.materialParamsList = new List<EnemyLoader.MaterialParams>();
      int ID_RIM_POWER = Shader.PropertyToID("_RimPower");
      int ID_RIM_WIDTH = Shader.PropertyToID("_RimWidth");
      int ID_VANISH_FLAG = Shader.PropertyToID("_Vanish_flag");
      int ID_VANISH_RATE = Shader.PropertyToID("_Vanish_rate");
      Utility.MaterialForEach(this.renderersBody, (Action<Material>) (material =>
      {
        if (!Object.op_Inequality((Object) material, (Object) null))
          return;
        EnemyLoader.MaterialParams materialParams = new EnemyLoader.MaterialParams();
        materialParams.material = material;
        if (materialParams.hasRimPower = material.HasProperty(ID_RIM_POWER))
          materialParams.defaultRimPower = material.GetFloat(ID_RIM_POWER);
        if (materialParams.hasRimWidth = material.HasProperty(ID_RIM_WIDTH))
          materialParams.defaultRimWidth = material.GetFloat(ID_RIM_WIDTH);
        materialParams.hasVanishFlag = material.HasProperty(ID_VANISH_FLAG);
        materialParams.hasVanishRate = material.HasProperty(ID_VANISH_RATE);
        this.materialParamsList.Add(materialParams);
      }));
    }
    int index1 = 0;
    for (int length = this.renderersBody.Length; index1 < length; ++index1)
      this.renderersBody[index1].lightProbeUsage = !enable_light_probes ? (LightProbeUsage) 0 : (LightProbeUsage) 2;
    EnemyParam param = ((Component) this.body).gameObject.GetComponent<EnemyParam>();
    ((Component) this.body).gameObject.SetActive(false);
    if (need_anim_event_res_cache && Object.op_Inequality((Object) this.animator, (Object) null) && lo_anim.loadedObjects != null && lo_anim.loadedObjects[1] != null)
    {
      AnimEventData animEventData = lo_anim.loadedObjects[1].obj as AnimEventData;
      if (Object.op_Inequality((Object) animEventData, (Object) null))
      {
        if (Object.op_Equality((Object) enemy, (Object) null))
          load_queue.CacheAnimDataUseResource(animEventData);
        else
          load_queue.CacheAnimDataUseResource(animEventData, new LoadingQueue.EffectNameAnalyzer(((Character) enemy).EffectNameAnalyzer), enemy.continusAtkEventDataList);
        this.PreSetAnimationEventDataParamToEnemy(animEventData, enemy);
      }
    }
    AnimEventData.ResidentEffectData[] residentEffectList = (AnimEventData.ResidentEffectData[]) null;
    if (Object.op_Inequality((Object) this.animEventData, (Object) null))
    {
      residentEffectList = this.animEventData.residentEffectDataList;
      if (residentEffectList != null)
      {
        int length = residentEffectList.Length;
        for (int index2 = 0; index2 < length; ++index2)
        {
          if (!string.IsNullOrEmpty(residentEffectList[index2].effectName))
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, residentEffectList[index2].effectName);
        }
      }
    }
    if (Object.op_Inequality((Object) param, (Object) null))
    {
      if (Object.op_Inequality((Object) enemy, (Object) null) | need_stamp_effect)
      {
        foreach (StageObject.StampInfo stampInfo in param.stampInfos)
          load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, stampInfo.effectName);
      }
      if (param.isHide)
      {
        FieldMapTable.GatherPointViewTableData gatherPointViewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(param.gatherPointViewId);
        if (gatherPointViewData != null)
        {
          if (!string.IsNullOrEmpty(gatherPointViewData.targetEffectName))
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, gatherPointViewData.targetEffectName);
          if (!string.IsNullOrEmpty(gatherPointViewData.gatherEffectName))
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, gatherPointViewData.gatherEffectName);
        }
      }
      SystemEffectSetting residentEffectSetting = param.residentEffectSetting;
      if (Object.op_Inequality((Object) residentEffectSetting, (Object) null))
      {
        SystemEffectSetting.Data[] effectDataList = residentEffectSetting.effectDataList;
        if (effectDataList != null)
        {
          int length = effectDataList.Length;
          for (int index3 = 0; index3 < length; ++index3)
          {
            if (!string.IsNullOrEmpty(effectDataList[index3].effectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectDataList[index3].effectName);
          }
        }
      }
    }
    if (load_queue.IsLoading())
      yield return (object) load_queue.Wait();
    if (Object.op_Inequality((Object) enemy, (Object) null))
    {
      if (Object.op_Inequality((Object) param, (Object) null))
      {
        EnemyTable.EnemyData enemyTableData = enemy.enemyTableData;
        foreach (AttackHitInfo attackHitInfo1 in param.attackHitInfos)
        {
          AttackHitInfo attackHitInfo2 = attackHitInfo1;
          if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
          {
            string str = $"{attackHitInfo2.name}_{enemyTableData.convertRegionKey}";
            foreach (AttackHitInfo convertAttackHitInfo in param.convertAttackHitInfos)
            {
              if (convertAttackHitInfo.name == str)
              {
                attackHitInfo2 = convertAttackHitInfo;
                break;
              }
            }
          }
          if (attackHitInfo2.hitSEID != 0)
            load_queue.CacheSE(attackHitInfo2.hitSEID);
          load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, attackHitInfo2.hitEffectName);
          load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, attackHitInfo2.remainEffectName);
          load_queue.CacheBulletDataUseResource(attackHitInfo2.bulletData);
          RestraintInfo restraintInfo = attackHitInfo1.restraintInfo;
          if (restraintInfo.enable && !string.IsNullOrEmpty(restraintInfo.effectName))
          {
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, restraintInfo.effectName);
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_flick");
            if (attackHitInfo1.toPlayer.reactionType != AttackHitInfo.ToPlayer.REACTION_TYPE.NONE)
              Log.Error(LOG.INGAME, "Can't use reactionType with RestraintInfo!! " + attackHitInfo1.name);
          }
          GrabInfo grabInfo = attackHitInfo1.grabInfo;
          if (grabInfo != null && grabInfo.enable && attackHitInfo1.toPlayer.reactionType != AttackHitInfo.ToPlayer.REACTION_TYPE.NONE)
            Log.Error(LOG.INGAME, "Can't use reactionType with GrabInfo!! " + attackHitInfo1.name);
          InkSplashInfo inkSplashInfo = attackHitInfo1.inkSplashInfo;
          if (inkSplashInfo != null && (double) inkSplashInfo.duration > 0.0)
          {
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_blind_01");
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_blind_02");
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_flick");
          }
          if ((double) attackHitInfo1.badStatus.stone > 0.0)
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_stone_01");
        }
        foreach (AttackContinuationInfo continuationInfo1 in param.attackContinuationInfos)
        {
          if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
          {
            string str = $"{continuationInfo1.name}_{enemyTableData.convertRegionKey}";
            foreach (AttackContinuationInfo continuationInfo2 in param.convertAttackContinuationInfos)
            {
              if (continuationInfo2.name == str)
              {
                continuationInfo1 = continuationInfo2;
                break;
              }
            }
          }
          load_queue.CacheBulletDataUseResource(continuationInfo1.bulletData);
        }
        foreach (Enemy.RegionInfo regionInfo in param.regionInfos)
        {
          if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
          {
            string str = $"{regionInfo.name}_{enemyTableData.convertRegionKey}";
            foreach (Enemy.RegionInfo convertRegionInfo in param.convertRegionInfos)
            {
              if (convertRegionInfo.name == str)
              {
                regionInfo = convertRegionInfo;
                break;
              }
            }
          }
          load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, regionInfo.breakEffect.effectName);
        }
        if (Singleton<EnemyHitMaterialTable>.IsValid())
        {
          int index4 = 0;
          for (int length = param.regionInfos.Length; index4 < length + 1; ++index4)
          {
            string name = index4 >= length ? param.baseHitMaterialName : param.regionInfos[index4].hitMaterialName;
            if (!string.IsNullOrEmpty(name))
            {
              EnemyHitMaterialTable.MaterialData data = Singleton<EnemyHitMaterialTable>.I.GetData(name);
              if (data != null)
              {
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.addEffectName);
                foreach (int typeSeiD in data.typeSEIDs)
                {
                  if (typeSeiD != 0)
                    load_queue.CacheSE(typeSeiD);
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
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_gravity_01");
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_fire_01");
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_movedown_01");
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_bindring_01");
      if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      {
        InGameSettingsManager.LightRingParam lightRingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.lightRingParam;
        if (lightRingParam.startSeId != 0)
          load_queue.CacheSE(lightRingParam.startSeId);
        if (lightRingParam.loopSeId != 0)
          load_queue.CacheSE(lightRingParam.loopSeId);
        if (lightRingParam.endSeId != 0)
          load_queue.CacheSE(lightRingParam.endSeId);
      }
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_erosion_01");
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_acid_01");
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_corruption_01");
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.stigmataParam.effectName);
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.cyclonicThunderstormParam.effectName);
      EffectPlayProcessor component = ((Component) this.body).gameObject.GetComponent<EffectPlayProcessor>();
      if (Object.op_Inequality((Object) component, (Object) null) && component.effectSettings != null)
      {
        enemy.effectPlayProcessor = component;
        int index5 = 0;
        for (int length = component.effectSettings.Length; index5 < length; ++index5)
        {
          if (!string.IsNullOrEmpty(component.effectSettings[index5].effectName))
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, component.effectSettings[index5].effectName);
        }
      }
      if (load_queue.IsLoading())
        yield return (object) load_queue.Wait();
    }
    ((Component) this.body).gameObject.SetActive(true);
    if (residentEffectList != null)
    {
      int length = residentEffectList.Length;
      for (int index6 = 0; index6 < length; ++index6)
      {
        AnimEventData.ResidentEffectData effectData = residentEffectList[index6];
        if (!string.IsNullOrEmpty(effectData.effectName) && !string.IsNullOrEmpty(effectData.linkNodeName))
        {
          Transform transform = Utility.Find(((Component) this.body).transform, effectData.linkNodeName);
          if (Object.op_Equality((Object) transform, (Object) null))
            transform = ((Component) this.body).transform;
          Transform effect = EffectManager.GetEffect(effectData.effectName, transform);
          if (Object.op_Inequality((Object) effect, (Object) null))
          {
            if (layer != -1)
              Utility.SetLayerWithChildren(effect, layer);
            Vector3 localScale = effect.localScale;
            effect.localScale = Vector3.op_Multiply(localScale, effectData.scale);
            effect.localPosition = effectData.offsetPos;
            effect.localRotation = Quaternion.Euler(effectData.offsetRot);
            ResidentEffectObject effectObj = ((Component) effect).gameObject.AddComponent<ResidentEffectObject>();
            effectObj.Initialize(effectData);
            if (Object.op_Inequality((Object) enemy, (Object) null))
              enemy.RegisterResidentEffect(effectObj);
          }
        }
      }
    }
    if (Object.op_Inequality((Object) param, (Object) null))
    {
      SystemEffectSetting residentEffectSetting = param.residentEffectSetting;
      this.SysEffectCreate(enemy, layer, residentEffectSetting);
    }
    if (need_shadow && Object.op_Inequality((Object) param, (Object) null) && (double) param.shadowSize > 0.0)
      this.shadow = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.CreateShadow(param.shadowSize, param.bodyRadius, this.bodyScale, true, _this, shader_type == SHADER_TYPE.LIGHTWEIGHT);
    if (Object.op_Inequality((Object) enemy, (Object) null))
    {
      if (Object.op_Inequality((Object) param, (Object) null))
      {
        param.SetParam(enemy);
        Object.DestroyImmediate((Object) param);
        param = (EnemyParam) null;
      }
      if (Object.op_Inequality((Object) enemy.controller, (Object) null))
        ((Behaviour) enemy.controller).enabled = true;
      enemy.willStock = will_stock;
      enemy.OnLoadComplete();
      if (Object.op_Inequality((Object) enemy.packetReceiver, (Object) null))
        enemy.packetReceiver.SetStopPacketUpdate(false);
    }
    if (callback != null)
      callback(enemy);
    if (lo_foundation != null)
    {
      this.foundation = lo_foundation.Realizes(_this, layer);
      this.foundation.SetParent(_this.parent, true);
    }
    this.isLoading = false;
  }

  public void StartLoad_GG_Optomize(
    int body_id,
    int anim_id,
    float scale,
    string base_effect,
    string base_effect_node,
    bool need_shadow,
    bool enable_light_probes,
    bool need_anim_event_res_cache,
    SHADER_TYPE shader_type,
    int layer = -1,
    string foundation_name = null,
    bool need_stamp_effect = false,
    bool will_stock = false,
    string weather_effect = "",
    EnemyLoader.OnCompleteLoad callback = null,
    System.Action effectCallBack = null,
    bool use_load_later = true)
  {
    if (this.isLoading)
      Log.Error(LOG.RESOURCE, ((Object) this).name + " now loading.");
    else if (Object.op_Inequality((Object) this.body, (Object) null))
      Log.Error(LOG.RESOURCE, ((Object) this).name + " loaded.");
    else
      this.StartCoroutine(this.DoGGOptimizationLoad(body_id, anim_id, scale, base_effect, base_effect_node, need_shadow, enable_light_probes, need_anim_event_res_cache, shader_type, layer, foundation_name, need_stamp_effect, will_stock, weather_effect, callback, effectCallBack, use_load_later));
  }

  private IEnumerator DoGGOptimizationLoad(
    int body_id,
    int anim_id,
    float scale,
    string base_effect,
    string base_effect_node,
    bool need_shadow,
    bool enable_light_probes,
    bool need_anim_event_res_cache,
    SHADER_TYPE shader_type,
    int layer,
    string foundation_name,
    bool need_stamp_effect,
    bool will_stock,
    string weather_effect,
    EnemyLoader.OnCompleteLoad callback,
    System.Action effectCallBack,
    bool useLoadLater)
  {
    Enemy enemy = ((Component) this).gameObject.GetComponent<Enemy>();
    if (Object.op_Inequality((Object) enemy, (Object) null))
    {
      int id = enemy.id;
    }
    this.bodyID = body_id;
    this.bodyScale = scale;
    bool is_boss = false;
    if (Object.op_Inequality((Object) enemy, (Object) null))
    {
      is_boss = enemy.isBoss;
      if (Object.op_Inequality((Object) enemy.controller, (Object) null))
        ((Behaviour) enemy.controller).enabled = false;
      if (Object.op_Inequality((Object) enemy.packetReceiver, (Object) null))
        enemy.packetReceiver.SetStopPacketUpdate(true);
      enemy.OnLoadStart();
    }
    string enemyBody = ResourceName.GetEnemyBody(body_id);
    string enemyMaterial = ResourceName.GetEnemyMaterial(body_id);
    string enemyAnim = ResourceName.GetEnemyAnim(anim_id);
    Transform _this = ((Component) this).transform;
    this.isLoading = true;
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_body = (LoadObject) load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.ENEMY_MODEL, enemyBody);
    LoadObject loadObject;
    if (enemyMaterial == null)
      loadObject = (LoadObject) null;
    else
      loadObject = load_queue.Load(RESOURCE_CATEGORY.ENEMY_MATERIAL, enemyBody, new string[1]
      {
        enemyMaterial
      });
    LoadObject lo_mate = loadObject;
    LoadObject lo_anim = load_queue.Load(RESOURCE_CATEGORY.ENEMY_ANIM, enemyAnim, new string[2]
    {
      enemyAnim + "Ctrl",
      enemyAnim + "Event"
    });
    if (!string.IsNullOrEmpty(base_effect))
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, base_effect);
    LoadObject lo_foundation = (LoadObject) null;
    if (!string.IsNullOrEmpty(foundation_name))
    {
      if (!MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.enableEnemyModelFoundationFromQuestStage)
        foundation_name = "FST011";
      lo_foundation = (LoadObject) load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.FOUNDATION_MODEL, foundation_name);
    }
    yield return (object) load_queue.Wait();
    bool lockE = useLoadLater;
    this.body = lo_body.Realizes(_this, layer == -1 ? 11 : layer);
    if (layer == -1)
      ((Component) this).gameObject.layer = 10;
    this.body.localPosition = Vector3.zero;
    this.body.localRotation = Quaternion.identity;
    this.renderersBody = ((Component) this.body).gameObject.GetComponentsInChildren<Renderer>();
    if (lo_mate != null && Object.op_Inequality(lo_mate.loadedObject, (Object) null) && this.renderersBody.Length == 1)
    {
      Material loadedObject = lo_mate.loadedObject as Material;
      if (Object.op_Inequality((Object) loadedObject, (Object) null))
        this.renderersBody[0].sharedMaterial = loadedObject;
    }
    if (Object.op_Inequality((Object) enemy, (Object) null))
      enemy.body = this.body;
    this.body.localScale = Vector3.Scale(this.body.localScale, new Vector3(scale, scale, scale));
    this.animator = ((Component) this.body).gameObject.GetComponent<Animator>();
    if (Object.op_Inequality((Object) this.animator, (Object) null) && lo_anim.loadedObjects != null)
    {
      this.animator.runtimeAnimatorController = (RuntimeAnimatorController) lo_anim.loadedObjects[0].obj;
      if (lo_anim.loadedObjects.Length >= 2 && lo_anim.loadedObjects[1] != null)
        this.animEventData = lo_anim.loadedObjects[1].obj as AnimEventData;
      if (Object.op_Inequality((Object) enemy, (Object) null))
      {
        ((Component) this.body).gameObject.AddComponent<StageObjectProxy>().stageObject = (StageObject) enemy;
        enemy.animEventData = this.animEventData;
      }
    }
    if (!string.IsNullOrEmpty(base_effect))
    {
      string name = base_effect_node;
      if (string.IsNullOrEmpty(name))
        name = "Root";
      Transform effect = EffectManager.GetEffect(base_effect, Utility.Find(this.body, name));
      if (Object.op_Inequality((Object) effect, (Object) null))
      {
        this.baseEffect = effect;
        if (layer != -1)
          Utility.SetLayerWithChildren(effect, layer);
      }
    }
    if (!string.IsNullOrEmpty(weather_effect) && MonoBehaviourSingleton<StageManager>.IsValid())
    {
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
      loadingQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, weather_effect);
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      MonoBehaviourSingleton<StageManager>.I.SetWeatherEffect(weather_effect);
    }
    if (shader_type == SHADER_TYPE.LIGHTWEIGHT)
      ShaderGlobal.ChangeWantLightweightShader(this.renderersBody);
    if (is_boss)
    {
      this.materialParamsList = new List<EnemyLoader.MaterialParams>();
      int ID_RIM_POWER = Shader.PropertyToID("_RimPower");
      int ID_RIM_WIDTH = Shader.PropertyToID("_RimWidth");
      int ID_VANISH_FLAG = Shader.PropertyToID("_Vanish_flag");
      int ID_VANISH_RATE = Shader.PropertyToID("_Vanish_rate");
      Utility.MaterialForEach(this.renderersBody, (Action<Material>) (material =>
      {
        if (!Object.op_Inequality((Object) material, (Object) null))
          return;
        EnemyLoader.MaterialParams materialParams = new EnemyLoader.MaterialParams();
        materialParams.material = material;
        if (materialParams.hasRimPower = material.HasProperty(ID_RIM_POWER))
          materialParams.defaultRimPower = material.GetFloat(ID_RIM_POWER);
        if (materialParams.hasRimWidth = material.HasProperty(ID_RIM_WIDTH))
          materialParams.defaultRimWidth = material.GetFloat(ID_RIM_WIDTH);
        materialParams.hasVanishFlag = material.HasProperty(ID_VANISH_FLAG);
        materialParams.hasVanishRate = material.HasProperty(ID_VANISH_RATE);
        this.materialParamsList.Add(materialParams);
      }));
    }
    int index1 = 0;
    for (int length = this.renderersBody.Length; index1 < length; ++index1)
      this.renderersBody[index1].lightProbeUsage = !enable_light_probes ? (LightProbeUsage) 0 : (LightProbeUsage) 2;
    EnemyParam param = ((Component) this.body).gameObject.GetComponent<EnemyParam>();
    ((Component) this.body).gameObject.SetActive(false);
    if (need_anim_event_res_cache && Object.op_Inequality((Object) this.animator, (Object) null) && lo_anim.loadedObjects != null && lo_anim.loadedObjects[1] != null)
    {
      AnimEventData animEventData = lo_anim.loadedObjects[1].obj as AnimEventData;
      if (Object.op_Inequality((Object) animEventData, (Object) null))
      {
        if (!lockE)
        {
          if (Object.op_Equality((Object) enemy, (Object) null))
            load_queue.CacheAnimDataUseResource(animEventData);
          else
            load_queue.CacheAnimDataUseResource(animEventData, new LoadingQueue.EffectNameAnalyzer(((Character) enemy).EffectNameAnalyzer), enemy.continusAtkEventDataList);
        }
        this.PreSetAnimationEventDataParamToEnemy(animEventData, enemy);
      }
    }
    AnimEventData.ResidentEffectData[] residentEffectList = (AnimEventData.ResidentEffectData[]) null;
    if (Object.op_Inequality((Object) this.animEventData, (Object) null))
    {
      residentEffectList = this.animEventData.residentEffectDataList;
      if (residentEffectList != null)
      {
        int length = residentEffectList.Length;
        for (int index2 = 0; index2 < length; ++index2)
        {
          if (!string.IsNullOrEmpty(residentEffectList[index2].effectName))
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, residentEffectList[index2].effectName);
        }
      }
    }
    if (Object.op_Inequality((Object) param, (Object) null) && !lockE)
    {
      if (Object.op_Inequality((Object) enemy, (Object) null) | need_stamp_effect)
      {
        foreach (StageObject.StampInfo stampInfo in param.stampInfos)
          load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, stampInfo.effectName);
      }
      if (param.isHide)
      {
        FieldMapTable.GatherPointViewTableData gatherPointViewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(param.gatherPointViewId);
        if (gatherPointViewData != null)
        {
          if (!string.IsNullOrEmpty(gatherPointViewData.targetEffectName))
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, gatherPointViewData.targetEffectName);
          if (!string.IsNullOrEmpty(gatherPointViewData.gatherEffectName))
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, gatherPointViewData.gatherEffectName);
        }
      }
      SystemEffectSetting residentEffectSetting = param.residentEffectSetting;
      if (Object.op_Inequality((Object) residentEffectSetting, (Object) null))
      {
        SystemEffectSetting.Data[] effectDataList = residentEffectSetting.effectDataList;
        if (effectDataList != null)
        {
          int length = effectDataList.Length;
          for (int index3 = 0; index3 < length; ++index3)
          {
            if (!string.IsNullOrEmpty(effectDataList[index3].effectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectDataList[index3].effectName);
          }
        }
      }
    }
    if (load_queue.IsLoading())
      yield return (object) load_queue.Wait();
    if (Object.op_Inequality((Object) enemy, (Object) null) && !lockE)
    {
      if (Object.op_Inequality((Object) param, (Object) null))
      {
        EnemyTable.EnemyData enemyTableData = enemy.enemyTableData;
        foreach (AttackHitInfo attackHitInfo1 in param.attackHitInfos)
        {
          AttackHitInfo attackHitInfo2 = attackHitInfo1;
          if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
          {
            string str = $"{attackHitInfo2.name}_{enemyTableData.convertRegionKey}";
            foreach (AttackHitInfo convertAttackHitInfo in param.convertAttackHitInfos)
            {
              if (convertAttackHitInfo.name == str)
              {
                attackHitInfo2 = convertAttackHitInfo;
                break;
              }
            }
          }
          if (attackHitInfo2.hitSEID != 0)
            load_queue.CacheSE(attackHitInfo2.hitSEID);
          load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, attackHitInfo2.hitEffectName);
          load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, attackHitInfo2.remainEffectName);
          load_queue.CacheBulletDataUseResource(attackHitInfo2.bulletData);
          RestraintInfo restraintInfo = attackHitInfo1.restraintInfo;
          if (restraintInfo.enable && !string.IsNullOrEmpty(restraintInfo.effectName))
          {
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, restraintInfo.effectName);
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_flick");
            if (attackHitInfo1.toPlayer.reactionType != AttackHitInfo.ToPlayer.REACTION_TYPE.NONE)
              Log.Error(LOG.INGAME, "Can't use reactionType with RestraintInfo!! " + attackHitInfo1.name);
          }
          GrabInfo grabInfo = attackHitInfo1.grabInfo;
          if (grabInfo != null && grabInfo.enable && attackHitInfo1.toPlayer.reactionType != AttackHitInfo.ToPlayer.REACTION_TYPE.NONE)
            Log.Error(LOG.INGAME, "Can't use reactionType with GrabInfo!! " + attackHitInfo1.name);
          InkSplashInfo inkSplashInfo = attackHitInfo1.inkSplashInfo;
          if (inkSplashInfo != null && (double) inkSplashInfo.duration > 0.0)
          {
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_blind_01");
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_blind_02");
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_flick");
          }
          if ((double) attackHitInfo1.badStatus.stone > 0.0)
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_stone_01");
        }
        foreach (AttackContinuationInfo continuationInfo1 in param.attackContinuationInfos)
        {
          if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
          {
            string str = $"{continuationInfo1.name}_{enemyTableData.convertRegionKey}";
            foreach (AttackContinuationInfo continuationInfo2 in param.convertAttackContinuationInfos)
            {
              if (continuationInfo2.name == str)
              {
                continuationInfo1 = continuationInfo2;
                break;
              }
            }
          }
          load_queue.CacheBulletDataUseResource(continuationInfo1.bulletData);
        }
        foreach (Enemy.RegionInfo regionInfo in param.regionInfos)
        {
          if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
          {
            string str = $"{regionInfo.name}_{enemyTableData.convertRegionKey}";
            foreach (Enemy.RegionInfo convertRegionInfo in param.convertRegionInfos)
            {
              if (convertRegionInfo.name == str)
              {
                regionInfo = convertRegionInfo;
                break;
              }
            }
          }
          load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, regionInfo.breakEffect.effectName);
        }
        if (Singleton<EnemyHitMaterialTable>.IsValid())
        {
          int index4 = 0;
          for (int length = param.regionInfos.Length; index4 < length + 1; ++index4)
          {
            string name = index4 >= length ? param.baseHitMaterialName : param.regionInfos[index4].hitMaterialName;
            if (!string.IsNullOrEmpty(name))
            {
              EnemyHitMaterialTable.MaterialData data = Singleton<EnemyHitMaterialTable>.I.GetData(name);
              if (data != null)
              {
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.addEffectName);
                foreach (int typeSeiD in data.typeSEIDs)
                {
                  if (typeSeiD != 0)
                    load_queue.CacheSE(typeSeiD);
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
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_gravity_01");
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_fire_01");
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_movedown_01");
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_bindring_01");
      if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      {
        InGameSettingsManager.LightRingParam lightRingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.lightRingParam;
        if (lightRingParam.startSeId != 0)
          load_queue.CacheSE(lightRingParam.startSeId);
        if (lightRingParam.loopSeId != 0)
          load_queue.CacheSE(lightRingParam.loopSeId);
        if (lightRingParam.endSeId != 0)
          load_queue.CacheSE(lightRingParam.endSeId);
      }
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_erosion_01");
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_acid_01");
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_corruption_01");
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.stigmataParam.effectName);
      EffectPlayProcessor component = ((Component) this.body).gameObject.GetComponent<EffectPlayProcessor>();
      if (Object.op_Inequality((Object) component, (Object) null) && component.effectSettings != null)
      {
        enemy.effectPlayProcessor = component;
        int index5 = 0;
        for (int length = component.effectSettings.Length; index5 < length; ++index5)
        {
          if (!string.IsNullOrEmpty(component.effectSettings[index5].effectName))
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, component.effectSettings[index5].effectName);
        }
      }
      if (load_queue.IsLoading())
        yield return (object) load_queue.Wait();
    }
    ((Component) this.body).gameObject.SetActive(true);
    if (residentEffectList != null)
    {
      int length = residentEffectList.Length;
      for (int index6 = 0; index6 < length; ++index6)
      {
        AnimEventData.ResidentEffectData effectData = residentEffectList[index6];
        if (!string.IsNullOrEmpty(effectData.effectName) && !string.IsNullOrEmpty(effectData.linkNodeName))
        {
          Transform transform = Utility.Find(((Component) this.body).transform, effectData.linkNodeName);
          if (Object.op_Equality((Object) transform, (Object) null))
            transform = ((Component) this.body).transform;
          Transform effect = EffectManager.GetEffect(effectData.effectName, transform);
          if (Object.op_Inequality((Object) effect, (Object) null))
          {
            if (layer != -1)
              Utility.SetLayerWithChildren(effect, layer);
            Vector3 localScale = effect.localScale;
            effect.localScale = Vector3.op_Multiply(localScale, effectData.scale);
            effect.localPosition = effectData.offsetPos;
            effect.localRotation = Quaternion.Euler(effectData.offsetRot);
            ResidentEffectObject effectObj = ((Component) effect).gameObject.AddComponent<ResidentEffectObject>();
            effectObj.Initialize(effectData);
            if (Object.op_Inequality((Object) enemy, (Object) null))
              enemy.RegisterResidentEffect(effectObj);
          }
        }
      }
    }
    if (Object.op_Inequality((Object) param, (Object) null))
    {
      SystemEffectSetting residentEffectSetting = param.residentEffectSetting;
      this.SysEffectCreate(enemy, layer, residentEffectSetting);
    }
    if (need_shadow && Object.op_Inequality((Object) param, (Object) null) && (double) param.shadowSize > 0.0)
      this.shadow = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.CreateShadow(param.shadowSize, param.bodyRadius, this.bodyScale, true, _this, shader_type == SHADER_TYPE.LIGHTWEIGHT);
    if (lockE)
      this.StartCoroutine(this.LoadEnemyEffectAfter(((Component) this).gameObject, lo_anim, param, need_anim_event_res_cache, need_stamp_effect, layer, (System.Action) (() => effectCallBack())));
    if (Object.op_Inequality((Object) enemy, (Object) null))
    {
      if (Object.op_Inequality((Object) param, (Object) null))
        param.SetParam(enemy);
      if (Object.op_Inequality((Object) enemy.controller, (Object) null))
        ((Behaviour) enemy.controller).enabled = true;
      enemy.willStock = will_stock;
      enemy.OnLoadComplete();
      if (Object.op_Inequality((Object) enemy.packetReceiver, (Object) null))
        enemy.packetReceiver.SetStopPacketUpdate(false);
    }
    if (callback != null)
      callback(enemy);
    if (!lockE && effectCallBack != null)
      effectCallBack();
    if (lo_foundation != null)
    {
      this.foundation = lo_foundation.Realizes(_this, layer);
      this.foundation.SetParent(_this.parent, true);
    }
    this.isLoading = false;
  }

  public IEnumerator LoadEnemyEffectAfter(
    GameObject gameObject,
    LoadObject lo_anim,
    EnemyParam param,
    bool need_anim_event_res_cache,
    bool need_stamp_effect,
    int layer,
    System.Action callback)
  {
    if (!Object.op_Equality((Object) gameObject.GetComponent<Enemy>(), (Object) null))
    {
      this.kc = true;
      this._param = param;
      this._callback = callback;
      if (need_anim_event_res_cache && Object.op_Inequality((Object) this.animator, (Object) null) && lo_anim.loadedObjects != null && lo_anim.loadedObjects[1] != null)
        this._tmpAnimEventData = lo_anim.loadedObjects[1].obj as AnimEventData;
      this._layer = layer;
      this._need_stamp_effect = need_stamp_effect;
      yield break;
    }
  }

  private IEnumerator LoadEnemyEffectAfterDisable(
    GameObject gameObject,
    EnemyParam param,
    bool need_stamp_effect,
    System.Action callback)
  {
    Enemy enemy = gameObject.GetComponent<Enemy>();
    if (!Object.op_Equality((Object) enemy, (Object) null))
    {
      LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
      int id = enemy.id;
      int layer = this._layer;
      AnimEventData.ResidentEffectData[] residentEffectList = this._residentEffectList;
      if (Object.op_Inequality((Object) this._tmpAnimEventData, (Object) null))
      {
        AnimEventData tmpAnimEventData = this._tmpAnimEventData;
        if (Object.op_Inequality((Object) tmpAnimEventData, (Object) null))
        {
          if (Object.op_Equality((Object) enemy, (Object) null))
            load_queue.CacheAnimDataUseResource(tmpAnimEventData);
          else
            load_queue.CacheAnimDataUseResource(tmpAnimEventData, new LoadingQueue.EffectNameAnalyzer(((Character) enemy).EffectNameAnalyzer), enemy.continusAtkEventDataList);
        }
      }
      if (Object.op_Inequality((Object) param, (Object) null))
      {
        if (Object.op_Inequality((Object) enemy, (Object) null) | need_stamp_effect)
        {
          foreach (StageObject.StampInfo stampInfo in param.stampInfos)
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, stampInfo.effectName);
        }
        if (param.isHide)
        {
          FieldMapTable.GatherPointViewTableData gatherPointViewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(param.gatherPointViewId);
          if (gatherPointViewData != null)
          {
            if (!string.IsNullOrEmpty(gatherPointViewData.targetEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, gatherPointViewData.targetEffectName);
            if (!string.IsNullOrEmpty(gatherPointViewData.gatherEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, gatherPointViewData.gatherEffectName);
          }
        }
        SystemEffectSetting residentEffectSetting = param.residentEffectSetting;
        if (Object.op_Inequality((Object) residentEffectSetting, (Object) null))
        {
          SystemEffectSetting.Data[] effectDataList = residentEffectSetting.effectDataList;
          if (effectDataList != null)
          {
            int length = effectDataList.Length;
            for (int index = 0; index < length; ++index)
            {
              if (!string.IsNullOrEmpty(effectDataList[index].effectName))
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectDataList[index].effectName);
            }
          }
        }
      }
      while (load_queue.IsLoading())
        yield return (object) load_queue.Wait();
      if (Object.op_Inequality((Object) enemy, (Object) null))
      {
        if (Object.op_Inequality((Object) param, (Object) null))
        {
          EnemyTable.EnemyData enemyTableData = enemy.enemyTableData;
          foreach (AttackHitInfo attackHitInfo1 in param.attackHitInfos)
          {
            AttackHitInfo attackHitInfo2 = attackHitInfo1;
            if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
            {
              string str = $"{attackHitInfo2.name}_{enemyTableData.convertRegionKey}";
              foreach (AttackHitInfo convertAttackHitInfo in param.convertAttackHitInfos)
              {
                if (convertAttackHitInfo.name == str)
                {
                  attackHitInfo2 = convertAttackHitInfo;
                  break;
                }
              }
            }
            if (attackHitInfo2.hitSEID != 0)
              load_queue.CacheSE(attackHitInfo2.hitSEID);
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, attackHitInfo2.hitEffectName);
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, attackHitInfo2.remainEffectName);
            load_queue.CacheBulletDataUseResource(attackHitInfo2.bulletData);
            RestraintInfo restraintInfo = attackHitInfo1.restraintInfo;
            if (restraintInfo.enable && !string.IsNullOrEmpty(restraintInfo.effectName))
            {
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, restraintInfo.effectName);
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_flick");
              if (attackHitInfo1.toPlayer.reactionType != AttackHitInfo.ToPlayer.REACTION_TYPE.NONE)
                Log.Error(LOG.INGAME, "Can't use reactionType with RestraintInfo!! " + attackHitInfo1.name);
            }
            GrabInfo grabInfo = attackHitInfo1.grabInfo;
            if (grabInfo != null && grabInfo.enable && attackHitInfo1.toPlayer.reactionType != AttackHitInfo.ToPlayer.REACTION_TYPE.NONE)
              Log.Error(LOG.INGAME, "Can't use reactionType with GrabInfo!! " + attackHitInfo1.name);
            InkSplashInfo inkSplashInfo = attackHitInfo1.inkSplashInfo;
            if (inkSplashInfo != null && (double) inkSplashInfo.duration > 0.0)
            {
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_blind_01");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_blind_02");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_flick");
            }
            if ((double) attackHitInfo1.badStatus.stone > 0.0)
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_stone_01");
          }
          foreach (AttackContinuationInfo continuationInfo1 in param.attackContinuationInfos)
          {
            if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
            {
              string str = $"{continuationInfo1.name}_{enemyTableData.convertRegionKey}";
              foreach (AttackContinuationInfo continuationInfo2 in param.convertAttackContinuationInfos)
              {
                if (continuationInfo2.name == str)
                {
                  continuationInfo1 = continuationInfo2;
                  break;
                }
              }
            }
            load_queue.CacheBulletDataUseResource(continuationInfo1.bulletData);
          }
          foreach (Enemy.RegionInfo regionInfo in param.regionInfos)
          {
            if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
            {
              string str = $"{regionInfo.name}_{enemyTableData.convertRegionKey}";
              foreach (Enemy.RegionInfo convertRegionInfo in param.convertRegionInfos)
              {
                if (convertRegionInfo.name == str)
                {
                  regionInfo = convertRegionInfo;
                  break;
                }
              }
            }
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, regionInfo.breakEffect.effectName);
          }
          if (Singleton<EnemyHitMaterialTable>.IsValid())
          {
            int index = 0;
            for (int length = param.regionInfos.Length; index < length + 1; ++index)
            {
              string name = index >= length ? param.baseHitMaterialName : param.regionInfos[index].hitMaterialName;
              if (!string.IsNullOrEmpty(name))
              {
                EnemyHitMaterialTable.MaterialData data = Singleton<EnemyHitMaterialTable>.I.GetData(name);
                if (data != null)
                {
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.addEffectName);
                  foreach (int typeSeiD in data.typeSEIDs)
                  {
                    if (typeSeiD != 0)
                      load_queue.CacheSE(typeSeiD);
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
        load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_gravity_01");
        load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_fire_01");
        load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_movedown_01");
        load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_bindring_01");
        if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
        {
          InGameSettingsManager.LightRingParam lightRingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.lightRingParam;
          if (lightRingParam.startSeId != 0)
            load_queue.CacheSE(lightRingParam.startSeId);
          if (lightRingParam.loopSeId != 0)
            load_queue.CacheSE(lightRingParam.loopSeId);
          if (lightRingParam.endSeId != 0)
            load_queue.CacheSE(lightRingParam.endSeId);
        }
        load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_erosion_01");
        load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_acid_01");
        load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enm_corruption_01");
        load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.stigmataParam.effectName);
        EffectPlayProcessor component = ((Component) this.body).gameObject.GetComponent<EffectPlayProcessor>();
        if (Object.op_Inequality((Object) component, (Object) null) && component.effectSettings != null)
        {
          enemy.effectPlayProcessor = component;
          int index = 0;
          for (int length = component.effectSettings.Length; index < length; ++index)
          {
            if (!string.IsNullOrEmpty(component.effectSettings[index].effectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, component.effectSettings[index].effectName);
          }
        }
        if (load_queue.IsLoading())
          yield return (object) load_queue.Wait();
      }
      if (Object.op_Inequality((Object) enemy, (Object) null) && Object.op_Inequality((Object) param, (Object) null))
      {
        Object.DestroyImmediate((Object) param);
        param = (EnemyParam) null;
      }
      this.kc = false;
      if (callback != null)
        callback();
      this.StopAllCoroutines();
    }
  }

  private void OnDisable()
  {
  }

  private void OnEnable()
  {
    if (!this.kc)
      return;
    this.StartCoroutine(this.LoadEnemyEffectAfterDisable(((Component) this).gameObject, this._param, this._need_stamp_effect, this._callback));
  }

  public void SysEffectCreate(Enemy enemy, int layer, SystemEffectSetting sysEffectSetting)
  {
    if (!Object.op_Inequality((Object) sysEffectSetting, (Object) null))
      return;
    int[] numArray = sysEffectSetting.startGroupIds;
    bool flag1 = false;
    if (numArray == null || numArray.Length == 0)
    {
      flag1 = true;
      numArray = new int[1];
    }
    else if (numArray[0] < 0)
      return;
    SystemEffectSetting.Data[] effectDataList = sysEffectSetting.effectDataList;
    if (effectDataList == null)
      return;
    int length1 = effectDataList.Length;
    for (int index1 = 0; index1 < length1; ++index1)
    {
      SystemEffectSetting.Data effectData = effectDataList[index1];
      if (!flag1)
      {
        bool flag2 = false;
        int index2 = 0;
        for (int length2 = numArray.Length; index2 < length2; ++index2)
        {
          if (numArray[index2] == effectData.groupID)
          {
            flag2 = true;
            break;
          }
        }
        if (!flag2)
          continue;
      }
      if (!string.IsNullOrEmpty(effectData.effectName) && !string.IsNullOrEmpty(effectData.linkNodeName))
      {
        Transform transform = Utility.Find(((Component) this.body).transform, effectData.linkNodeName);
        if (Object.op_Equality((Object) transform, (Object) null))
          transform = ((Component) this.body).transform;
        Transform effect = EffectManager.GetEffect(effectData.effectName, transform);
        if (Object.op_Inequality((Object) effect, (Object) null))
        {
          if (layer != -1)
            Utility.SetLayerWithChildren(effect, layer);
          Vector3 localScale = effect.localScale;
          effect.localScale = Vector3.op_Multiply(localScale, effectData.scale);
          effect.localPosition = effectData.offsetPos;
          effect.localRotation = Quaternion.Euler(effectData.offsetRot);
          ResidentEffectObject effectObj = ((Component) effect).gameObject.AddComponent<ResidentEffectObject>();
          effectObj.Initialize(effectData);
          if (Object.op_Inequality((Object) enemy, (Object) null))
            enemy.RegisterResidentEffect(effectObj);
        }
      }
    }
  }

  public void ResetRimParams()
  {
    if (this.materialParamsList == null)
      return;
    int ID_RIM_POWER = Shader.PropertyToID("_RimPower");
    int ID_RIM_WIDTH = Shader.PropertyToID("_RimWidth");
    this.materialParamsList.ForEach((Action<EnemyLoader.MaterialParams>) (prm =>
    {
      if (prm.hasRimPower)
        prm.material.SetFloat(ID_RIM_POWER, prm.defaultRimPower);
      if (!prm.hasRimWidth)
        return;
      prm.material.SetFloat(ID_RIM_WIDTH, prm.defaultRimWidth);
    }));
  }

  private void PreSetAnimationEventDataParamToEnemy(AnimEventData animEventData, Enemy enemy)
  {
    if (Object.op_Equality((Object) animEventData, (Object) null) || Object.op_Equality((Object) enemy, (Object) null))
      return;
    AnimEventData.AnimData[] animations = animEventData.animations;
    if (animations == null)
      return;
    int index1 = 0;
    for (int length1 = animations.Length; index1 < length1; ++index1)
    {
      AnimEventData.EventData[] events = animations[index1].events;
      if (events != null)
      {
        int index2 = 0;
        for (int length2 = events.Length; index2 < length2; ++index2)
        {
          AnimEventData.EventData eventData = events[index2];
          if (eventData != null)
          {
            switch (eventData.id)
            {
              case AnimEventFormat.ID.EFFECT:
                if (animations[index1].name == "paralyze")
                {
                  if (eventData.floatArgs.Length != 0)
                    enemy.paralyzeEffectScale = eventData.floatArgs[0];
                  if (eventData.stringArgs.Length != 0)
                  {
                    enemy.paralyzeEffectName = eventData.stringArgs[0];
                    continue;
                  }
                  continue;
                }
                continue;
              case AnimEventFormat.ID.MOVE_SIDEWAYS_LOOK_TARGET:
                if (eventData.floatArgs.Length >= 2)
                {
                  enemy.moveAngle_deg = eventData.floatArgs[0];
                  enemy.moveAngleSpeed_deg = eventData.floatArgs[1];
                  continue;
                }
                continue;
              case AnimEventFormat.ID.MOVE_POINT_DATA:
                if (eventData.floatArgs.Length >= 2)
                {
                  enemy.movePointPos = new Vector3(eventData.floatArgs[0], 0.0f, eventData.floatArgs[1]);
                  continue;
                }
                continue;
              case AnimEventFormat.ID.MOVE_LOOKAT_DATA:
                if (eventData.floatArgs.Length >= 3)
                {
                  enemy.moveLookAtPos = new Vector3(eventData.floatArgs[0], 0.0f, eventData.floatArgs[1]);
                  enemy.moveLookAtAngle = eventData.floatArgs[2];
                  continue;
                }
                continue;
              case AnimEventFormat.ID.SUMMON_ENEMY:
                int intArg1 = eventData.intArgs[0];
                int intArg2 = eventData.intArgs[1];
                if (intArg2 == 0 && eventData.intArgs[2] > 0)
                  intArg2 = Mathf.FloorToInt((float) ((int) enemy.enemyLevel / eventData.intArgs[2]));
                this.StartCoroutine(MonoBehaviourSingleton<InGameManager>.I.InitializeEnemyPopForSummon(intArg1, intArg2));
                continue;
              case AnimEventFormat.ID.SUMMON_ATTACK:
                if (!enemy.isSummonAttack)
                {
                  this.StartCoroutine(MonoBehaviourSingleton<InGameManager>.I.InitializeEnemyPopForSummonAttack(eventData.intArgs[0], (int) enemy.enemyLevel));
                  continue;
                }
                continue;
              default:
                continue;
            }
          }
        }
      }
    }
  }

  public void DeleteLoadedObjects()
  {
    if (Object.op_Inequality((Object) this.body, (Object) null))
    {
      Object.DestroyImmediate((Object) ((Component) this.body).gameObject);
      this.body = (Transform) null;
    }
    if (Object.op_Inequality((Object) this.foundation, (Object) null))
    {
      Object.DestroyImmediate((Object) ((Component) this.foundation).gameObject);
      this.foundation = (Transform) null;
    }
    this.animator = (Animator) null;
    this.animEventData = (AnimEventData) null;
    this.renderersBody = (Renderer[]) null;
    this.isLoading = false;
  }

  public void ApplyGachaDisplayScaleToParentNode()
  {
    if (!Object.op_Inequality((Object) this.body, (Object) null) || !Object.op_Inequality((Object) this.body.parent, (Object) null))
      return;
    this.body.parent.localScale = new Vector3(this.displayGachaScale, this.displayGachaScale, this.displayGachaScale);
  }

  public static string GetElementEffectName(ELEMENT_TYPE element_type)
  {
    int index = (int) element_type;
    if (!MonoBehaviourSingleton<GlobalSettingsManager>.IsValid() || !MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.enableEnemyModelEffectFromEnemyElement)
      index = 0;
    if (index < 0 || index >= MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.enemyModelElementEffects.Length)
      index = 0;
    return MonoBehaviourSingleton<GlobalSettingsManager>.I.uiModelRendering.enemyModelElementEffects[index];
  }

  public static void CacheUIElementEffect(LoadingQueue load_queue, ELEMENT_TYPE element_type)
  {
    if (element_type >= ELEMENT_TYPE.MAX)
      return;
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, EnemyLoader.GetElementEffectName(element_type));
  }

  public static void CacheUIElementEffects(LoadingQueue load_queue)
  {
    if (!MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
      return;
    int num = 0;
    for (int index = 6; num < index; ++num)
      EnemyLoader.CacheUIElementEffect(load_queue, (ELEMENT_TYPE) num);
  }

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
}
