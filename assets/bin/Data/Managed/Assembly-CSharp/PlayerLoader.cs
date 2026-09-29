// Decompiled with JetBrains decompiler
// Type: PlayerLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using App.Scripts.GoGame.Optimization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

#nullable disable
public class PlayerLoader : ModelLoaderBase
{
  public static readonly Bounds BOUNDS = new Bounds(Vector3.zero, new Vector3(2f, 2f, 2f));
  public HashSet<string> playerLoaderLoadedAttackInfoNames;
  public List<Transform> accessory = new List<Transform>();
  public const string BASE_ANIM_TABLE_KEY = "BASE";
  public const int kHighResoTextureType_None = 0;
  public const int kHighResoTextureType_Main = 1;
  public const int kHighResoTextureType_MainMask = 2;
  public const int kHighResoTextureType_Right = 4;
  public const int kHighResoTextureType_RightMask = 8;
  public const int kHighResoTextureType_Left = 16 /*0x10*/;
  public const int kHighResoTextureType_LeftMask = 32 /*0x20*/;
  private bool autoEyeBlink;
  protected PlayerLoader.FACE_ID faceID;
  protected bool validFaceChange;
  protected float eyeBlinkTime;
  private ResourceObject[] voiceAudioClips;
  protected int[] voiceAudioClipIds;
  private static readonly int[] ATK_VOICE_S = new int[3]
  {
    1,
    94,
    90
  };
  private static readonly int[] ATK_VOICE_M = new int[5]
  {
    2,
    3,
    95,
    97,
    98
  };
  private static readonly int[] ATK_VOICE_L = new int[3]
  {
    4,
    5,
    96 /*0x60*/
  };
  private static readonly int[] DAMAGE_VOICES = new int[2]
  {
    6,
    7
  };
  private static readonly int[] DEATH_VOICES = new int[2]
  {
    9,
    10
  };
  private static readonly int[] HAPPY_VOICES = new int[2]
  {
    14,
    15
  };

  public override bool IsLoading() => this.isLoading;

  public override Animator GetAnimator() => this.animator;

  public override Transform GetHead() => this.socketHead;

  public override void SetEnabled(bool is_enable)
  {
    if (Object.op_Inequality((Object) this.animator, (Object) null))
      ((Behaviour) this.animator).enabled = is_enable;
    if (Object.op_Inequality((Object) this.shadow, (Object) null))
      ((Component) this.shadow).gameObject.SetActive(is_enable);
    ModelLoaderBase.SetEnabled(this.renderersWep, is_enable);
    ModelLoaderBase.SetEnabled(this.renderersFace, is_enable);
    ModelLoaderBase.SetEnabled(this.renderersHair, is_enable);
    ModelLoaderBase.SetEnabled(this.renderersBody, is_enable);
    ModelLoaderBase.SetEnabled(this.renderersHead, is_enable);
    ModelLoaderBase.SetEnabled(this.renderersArm, is_enable);
    ModelLoaderBase.SetEnabled(this.renderersLeg, is_enable);
    ModelLoaderBase.SetEnabled(this.renderersAccessory, is_enable);
  }

  public PlayerLoadInfo loadInfo { get; protected set; }

  public Transform wepR { get; protected set; }

  public Transform wepL { get; protected set; }

  public Transform face { get; protected set; }

  public Transform hair { get; protected set; }

  public Transform body { get; protected set; }

  public Transform head { get; protected set; }

  public Transform arm { get; protected set; }

  public Transform leg { get; protected set; }

  public string weaponCacheName { get; protected set; }

  public string faceCacheName { get; protected set; }

  public string hairCacheName { get; protected set; }

  public string bodyCacheName { get; protected set; }

  public string headCacheName { get; protected set; }

  public string armCacheName { get; protected set; }

  public string legCacheName { get; protected set; }

  public Animator animator { get; protected set; }

  public Transform shadow { get; protected set; }

  public Transform hairPhysics { get; protected set; }

  public Renderer[] renderersWep { get; protected set; }

  public Renderer[] renderersFace { get; protected set; }

  public Renderer[] renderersHair { get; protected set; }

  public Renderer[] renderersBody { get; protected set; }

  public Renderer[] renderersHead { get; protected set; }

  public Renderer[] renderersArm { get; protected set; }

  public Renderer[] renderersLeg { get; protected set; }

  public Renderer[] renderersAccessory { get; protected set; }

  public Transform socketRoot { get; protected set; }

  public Transform socketWepL { get; protected set; }

  public Transform socketWepR { get; protected set; }

  public Transform socketHead { get; protected set; }

  public Transform socketFootL { get; protected set; }

  public Transform socketFootR { get; protected set; }

  public Transform socketHandL { get; protected set; }

  public Transform socketHandR { get; protected set; }

  public Transform socketForearmL { get; protected set; }

  public Transform socketForearmR { get; protected set; }

  public StringKeyTable<LoadObject> animObjectTable { get; protected set; }

  public virtual bool eyeBlink
  {
    get => this.autoEyeBlink;
    set
    {
      if (this.autoEyeBlink == value)
        return;
      this.eyeBlinkTime = !value ? 0.0f : Random.Range(3f, 6f);
      this.autoEyeBlink = value;
      this.ChangeFace(PlayerLoader.FACE_ID.NORMAL);
    }
  }

  public List<DynamicBone> dynamicBones { get; private set; }

  public List<DynamicBone> dynamicBones_Body { get; private set; }

  public virtual int GetVoiceId(ACTION_VOICE_ID voice_type)
  {
    return this.loadInfo == null ? 0 : this.loadInfo.actionVoiceBaseID + this.RandomizeAttackVoice((int) voice_type);
  }

  public virtual int GetVoiceId(ACTION_VOICE_EX_ID voice_type)
  {
    return this.loadInfo == null ? 0 : this.loadInfo.actionVoiceBaseID + this.RandomizeAttackVoice((int) voice_type);
  }

  protected virtual void UpdateVoiceAudioClipIds()
  {
    if (this.voiceAudioClips != null && this.voiceAudioClips.Length != 0)
    {
      this.voiceAudioClipIds = new int[this.voiceAudioClips.Length];
      int index = 0;
      for (int length = this.voiceAudioClips.Length; index < length; ++index)
      {
        int result = 0;
        ResourceObject voiceAudioClip = this.voiceAudioClips[index];
        if (voiceAudioClip != null && Object.op_Inequality(voiceAudioClip.obj, (Object) null) && voiceAudioClip.obj.name != null)
          int.TryParse(voiceAudioClip.obj.name.Substring(voiceAudioClip.obj.name.Length - 4), out result);
        this.voiceAudioClipIds[index] = result;
      }
    }
    else
      this.voiceAudioClipIds = (int[]) null;
  }

  public virtual AudioClip GetVoiceAudioClip(int voice_id)
  {
    if (this.voiceAudioClips != null)
    {
      int num = this.RandomizeAttackVoice(voice_id);
      if (num < 1)
        return (AudioClip) null;
      if (this.voiceAudioClipIds != null && this.voiceAudioClipIds.Length != 0)
      {
        int index = 0;
        for (int length = this.voiceAudioClipIds.Length; index < length; ++index)
        {
          if (this.voiceAudioClipIds[index] == num)
            return this.voiceAudioClips[index].obj as AudioClip;
        }
      }
    }
    return (AudioClip) null;
  }

  protected virtual int RandomizeAttackVoice(int voice_id)
  {
    switch (voice_id)
    {
      case 1:
        return this.ChooseRandom(PlayerLoader.ATK_VOICE_S, 0.2f);
      case 2:
      case 3:
        return this.ChooseRandom(PlayerLoader.ATK_VOICE_M);
      case 4:
      case 5:
        return this.ChooseRandom(PlayerLoader.ATK_VOICE_L);
      case 6:
        return this.ChooseRandom(PlayerLoader.DAMAGE_VOICES);
      case 9:
        return this.ChooseRandom(PlayerLoader.DEATH_VOICES);
      case 14:
        return this.ChooseRandom(PlayerLoader.HAPPY_VOICES);
      default:
        return voice_id;
    }
  }

  private int ChooseRandom(int[] items, float rejectRate = 0.0f)
  {
    if (items == null || (double) Random.Range(0.0f, 1f) < (double) rejectRate)
      return 0;
    int index = Random.Range(0, items.Length);
    return items[index];
  }

  public bool isLoading { get; protected set; }

  protected virtual void LoadAttackInfoResource(Player player, LoadingQueue loadQueue)
  {
    AttackInfo[] attackInfos = player.GetAttackInfos();
    if (attackInfos == null)
      return;
    int length = attackInfos.Length;
    for (int index1 = 0; index1 < length; ++index1)
    {
      if (attackInfos[index1] != null)
      {
        loadQueue.CacheBulletDataUseResource(attackInfos[index1].bulletData, player);
        if (attackInfos[index1] is AttackHitInfo attackHitInfo)
        {
          if (attackHitInfo.hitSEID != 0)
            loadQueue.CacheSE(attackHitInfo.hitSEID);
          loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, attackHitInfo.hitEffectName);
          loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, attackHitInfo.remainEffectName);
          if (!string.IsNullOrEmpty(attackHitInfo.toEnemy.hitTypeName))
          {
            EnemyHitTypeTable.TypeData data = Singleton<EnemyHitTypeTable>.I.GetData(attackHitInfo.toEnemy.hitTypeName, FieldManager.IsValidInGameNoQuest());
            if (data != null)
            {
              loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.baseEffectName);
              for (int index2 = 0; index2 < data.elementEffectNames.Length; ++index2)
              {
                if (!string.IsNullOrEmpty(data.elementEffectNames[index2]))
                  loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.elementEffectNames[index2]);
              }
            }
          }
        }
      }
    }
  }

  public virtual void StartLoad(
    PlayerLoadInfo player_load_info,
    int layer,
    int anim_id,
    bool need_anim_event,
    bool need_foot_stamp,
    bool need_shadow,
    bool enable_light_probes,
    bool need_action_voice,
    bool need_high_reso_tex,
    bool need_res_ref_count,
    bool need_dev_frame_instantiate,
    SHADER_TYPE shader_type,
    PlayerLoader.OnCompleteLoad callback,
    bool enable_eye_blick = true,
    int use_hair_overlay = -1)
  {
    if (this.isLoading)
      Log.Error(LOG.RESOURCE, ((Object) this).name + " now loading.");
    else
      this.StartCoroutine(this.DoLoad(player_load_info, layer, anim_id, need_anim_event, need_foot_stamp, need_shadow, enable_light_probes, need_action_voice, need_high_reso_tex, need_res_ref_count, need_dev_frame_instantiate, shader_type, callback, enable_eye_blick, use_hair_overlay));
  }

  public virtual void StartLoad_GG_Optimize(
    PlayerLoadInfo player_load_info,
    int layer,
    int anim_id,
    bool need_anim_event,
    bool need_foot_stamp,
    bool need_shadow,
    bool enable_light_probes,
    bool need_action_voice,
    bool need_high_reso_tex,
    bool need_res_ref_count,
    bool need_dev_frame_instantiate,
    SHADER_TYPE shader_type,
    PlayerLoader.OnCompleteLoad callback,
    bool enable_eye_blick = true,
    int use_hair_overlay = -1)
  {
    if (this.isLoading)
      Log.Error(LOG.RESOURCE, ((Object) this).name + " now loading.");
    else if (MonoBehaviourSingleton<GoGameResourceManager>.IsValid())
      this.StartCoroutine(this.DoLoad_GG_Optimize_Self(player_load_info, layer, anim_id, need_anim_event, need_foot_stamp, need_shadow, enable_light_probes, need_action_voice, need_high_reso_tex, need_res_ref_count, need_dev_frame_instantiate, shader_type, callback, enable_eye_blick, use_hair_overlay));
    else
      this.StartCoroutine(this.DoLoad_GG_Optimize(player_load_info, layer, anim_id, need_anim_event, need_foot_stamp, need_shadow, enable_light_probes, need_action_voice, need_high_reso_tex, need_res_ref_count, need_dev_frame_instantiate, shader_type, callback, enable_eye_blick, use_hair_overlay));
  }

  private static void SerializePlayerLoadInfo(PlayerLoadInfo info)
  {
  }

  private LoadObject GoGameQuickLoad(
    LoadingQueue loadQueue,
    bool needDevFrameInstantiate,
    RESOURCE_CATEGORY resourceCategory,
    string resourceName)
  {
    return needDevFrameInstantiate ? (resourceName == null ? (LoadObject) null : (LoadObject) loadQueue.LoadAndInstantiate(resourceCategory, resourceName)) : (resourceName == null ? (LoadObject) null : loadQueue.Load(resourceCategory, resourceName));
  }

  protected virtual IEnumerator DoLoad(
    PlayerLoadInfo info,
    int layer,
    int anim_id,
    bool need_anim_event,
    bool need_foot_stamp,
    bool need_shadow,
    bool enable_light_probes,
    bool need_action_voice,
    bool need_high_reso_tex,
    bool need_res_ref_count,
    bool need_dev_frame_instantiate,
    SHADER_TYPE shader_type,
    PlayerLoader.OnCompleteLoad callback,
    bool enable_eye_blick,
    int use_hair_overlay)
  {
    if (info == null)
      Log.Error(LOG.RESOURCE, "PlayerLoader:info=null");
    PlayerLoader.SerializePlayerLoadInfo(info);
    this.animObjectTable = new StringKeyTable<LoadObject>();
    Player player = ((Component) this).gameObject.GetComponent<Player>();
    bool enableBone = this._EnableDynamicBone(shader_type, player is Self);
    EquipModelTable.Data data1 = Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.ARMOR, info.bodyModelID);
    EquipModelTable.Data data2 = data1.needHelm ? Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.HELM, info.headModelID) : (EquipModelTable.Data) null;
    EquipModelTable.Data arm_model_data = data1.needArm ? Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.ARM, info.armModelID) : (EquipModelTable.Data) null;
    EquipModelTable.Data leg_model_data = data1.needLeg ? Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.LEG, info.legModelID) : (EquipModelTable.Data) null;
    int id1 = data2 != null ? data2.GetHairModelID(info.hairModelID) : data1.GetHairModelID(info.hairModelID);
    if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 0)
    {
      need_high_reso_tex = false;
      use_hair_overlay = -1;
    }
    int high_reso_tex_flags = 0;
    if (need_high_reso_tex && MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
      high_reso_tex_flags = (int) MonoBehaviourSingleton<GlobalSettingsManager>.I.equipModelHQTable.GetWeaponFlag(info.weaponModelID);
    bool is_self = false;
    if (Object.op_Inequality((Object) player, (Object) null))
    {
      int id2 = player.id;
      is_self = player is Self;
    }
    this.DeleteLoadedObjects();
    this.loadInfo = info;
    if (anim_id < 0)
      anim_id = anim_id != -1 || info.weaponModelID == -1 ? -anim_id + info.weaponModelID / 1000 : info.weaponModelID / 1000;
    bool flag = data2 != null ? data2.needFace : data1.needFace;
    int num1 = data2 != null ? data2.hairMode : data1.hairMode;
    string face_name = info.faceModelID > -1 & flag ? ResourceName.GetPlayerFace(info.faceModelID) : (string) null;
    string hair_name = id1 <= -1 || num1 == 0 ? (string) null : ResourceName.GetPlayerHead(id1);
    string body_name = info.bodyModelID > -1 ? ResourceName.GetPlayerBody(info.bodyModelID) : (string) null;
    string head_name = info.headModelID <= -1 || data2 == null ? (string) null : ResourceName.GetPlayerHead(info.headModelID);
    string arm_name = info.armModelID <= -1 || arm_model_data == null ? (string) null : ResourceName.GetPlayerArm(info.armModelID);
    string leg_name = info.legModelID <= -1 || leg_model_data == null ? (string) null : ResourceName.GetPlayerLeg(info.legModelID);
    string wepn_name = info.weaponModelID > -1 ? ResourceName.GetPlayerWeapon(info.weaponModelID) : (string) null;
    if (body_name != null)
    {
      Transform _this = ((Component) this).transform;
      if (Object.op_Inequality((Object) player, (Object) null))
      {
        if (Object.op_Inequality((Object) player.controller, (Object) null))
          ((Behaviour) player.controller).enabled = false;
        if (Object.op_Inequality((Object) player.packetReceiver, (Object) null))
          player.packetReceiver.SetStopPacketUpdate(true);
        player.OnLoadStart();
      }
      this.isLoading = true;
      LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this, need_res_ref_count);
      LoadObject lo_face = (LoadObject) null;
      LoadObject lo_hair = (LoadObject) null;
      LoadObject lo_body = (LoadObject) null;
      LoadObject lo_head = (LoadObject) null;
      LoadObject lo_arm = (LoadObject) null;
      LoadObject lo_leg = (LoadObject) null;
      LoadObject lo_wepn = (LoadObject) null;
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_FACE, face_name))
        lo_face = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_FACE, face_name);
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name))
        lo_hair = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_HEAD, hair_name);
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_BDY, body_name))
        lo_body = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_BDY, body_name);
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_HEAD, head_name))
        lo_head = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_HEAD, head_name);
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ARM, arm_name))
        lo_arm = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_ARM, arm_name);
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_LEG, leg_name))
        lo_leg = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_LEG, leg_name);
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name))
        lo_wepn = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name);
      List<LoadObject> lo_accessories = new List<LoadObject>();
      if (!info.accUIDs.IsNullOrEmpty<uint>())
      {
        int index = 0;
        for (int count = info.accUIDs.Count; index < count; ++index)
        {
          string playerAccessory = ResourceName.GetPlayerAccessory(Singleton<AccessoryTable>.I.GetInfoData(info.accUIDs[index]).accessoryId);
          lo_accessories.Add(need_dev_frame_instantiate ? (LoadObject) load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_ACCESSORY, playerAccessory) : load_queue.Load(RESOURCE_CATEGORY.PLAYER_ACCESSORY, playerAccessory));
        }
      }
      LoadObject lo_voices = (LoadObject) null;
      LoadObject lo_hr_wep_tex = (LoadObject) null;
      LoadObject lo_hr_hed_tex = (LoadObject) null;
      LoadObject lo_hr_bdy_tex = (LoadObject) null;
      LoadObject lo_hr_arm_tex = (LoadObject) null;
      LoadObject lo_hr_leg_tex = (LoadObject) null;
      string anim_name = anim_id > -1 ? ResourceName.GetPlayerAnim(anim_id) : (string) null;
      if (load_queue.IsLoading())
        yield return (object) load_queue.Wait();
      if (info.isNeedToCache)
      {
        if (lo_face != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_FACE, face_name, lo_face.loadedObject);
        if (lo_hair != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name, lo_hair.loadedObject);
        if (lo_body != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_BDY, body_name, lo_body.loadedObject);
        if (lo_head != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_HEAD, head_name, lo_head.loadedObject);
        if (lo_arm != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_ARM, arm_name, lo_arm.loadedObject);
        if (lo_leg != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_LEG, leg_name, lo_leg.loadedObject);
        if (lo_wepn != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name, lo_wepn.loadedObject);
      }
      LoadObject loadObject1;
      if (anim_name == null)
      {
        loadObject1 = (LoadObject) null;
      }
      else
      {
        LoadingQueue loadingQueue = load_queue;
        string package_name = anim_name;
        string[] resource_names;
        if (!need_anim_event)
          resource_names = new string[1]
          {
            anim_name + "Ctrl"
          };
        else
          resource_names = new string[2]
          {
            anim_name + "Ctrl",
            anim_name + "Event"
          };
        loadObject1 = loadingQueue.Load(RESOURCE_CATEGORY.PLAYER_ANIM, package_name, resource_names);
      }
      LoadObject lo_anim = loadObject1;
      if (lo_anim != null)
        this.animObjectTable.Add("BASE", lo_anim);
      if (Object.op_Inequality((Object) player, (Object) null) && anim_id > -1)
      {
        List<string> stringList = new List<string>();
        int num2 = 3;
        for (int index1 = 0; index1 < num2; ++index1)
        {
          for (int index2 = 0; index2 < 2; ++index2)
          {
            SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + index1);
            if (skillParam != null)
            {
              string fromAnimFormatName = Character.GetCtrlNameFromAnimFormatName(index2 == 0 ? skillParam.tableData.castStateName : skillParam.tableData.actStateName);
              if (!string.IsNullOrEmpty(fromAnimFormatName) && stringList.IndexOf(fromAnimFormatName) < 0)
              {
                stringList.Add(fromAnimFormatName);
                string playerSubAnim = ResourceName.GetPlayerSubAnim(anim_id, fromAnimFormatName);
                LoadObject loadObject2;
                if (playerSubAnim == null)
                {
                  loadObject2 = (LoadObject) null;
                }
                else
                {
                  LoadingQueue loadingQueue = load_queue;
                  string package_name = playerSubAnim;
                  string[] resource_names;
                  if (!need_anim_event)
                    resource_names = new string[1]
                    {
                      playerSubAnim + "Ctrl"
                    };
                  else
                    resource_names = new string[2]
                    {
                      playerSubAnim + "Ctrl",
                      playerSubAnim + "Event"
                    };
                  loadObject2 = loadingQueue.Load(RESOURCE_CATEGORY.PLAYER_ANIM_SKILL, package_name, resource_names);
                }
                LoadObject loadObject3 = loadObject2;
                if (loadObject3 != null)
                  this.animObjectTable.Add(fromAnimFormatName, loadObject3);
              }
            }
          }
        }
      }
      if (Object.op_Inequality((Object) player, (Object) null) && info.weaponEvolveId > 0U)
      {
        string ctrlName;
        string animName;
        ResourceName.GetPlayerEvolveAnim(info.weaponEvolveId, out ctrlName, out animName);
        LoadObject loadObject4 = load_queue.Load(RESOURCE_CATEGORY.PLAYER_ANIM_EVOLVE, animName, new string[2]
        {
          animName + "Ctrl",
          animName + "Event"
        });
        if (loadObject4 != null)
          this.animObjectTable.Add(ctrlName, loadObject4);
      }
      if (need_action_voice && info.actionVoiceBaseID > -1)
      {
        int[] values = (int[]) Enum.GetValues(typeof (ACTION_VOICE_ID));
        int length = values.Length;
        string[] resource_names = new string[length];
        for (int index = 0; index < length; ++index)
          resource_names[index] = ResourceName.GetActionVoiceName(info.actionVoiceBaseID + values[index]);
        lo_voices = load_queue.Load(RESOURCE_CATEGORY.SOUND_VOICE, ResourceName.GetActionVoicePackageNameFromVoiceID(info.actionVoiceBaseID), resource_names);
      }
      if (need_high_reso_tex)
      {
        if (high_reso_tex_flags != 0 && wepn_name != null)
          lo_hr_wep_tex = PlayerLoader.LoadHighResoTexs(load_queue, wepn_name, high_reso_tex_flags);
        if (head_name != null)
          lo_hr_hed_tex = PlayerLoader.LoadHighResoTexs(load_queue, head_name, 1);
        if (body_name != null)
          lo_hr_bdy_tex = PlayerLoader.LoadHighResoTexs(load_queue, body_name, 1);
        if (arm_name != null)
          lo_hr_arm_tex = PlayerLoader.LoadHighResoTexs(load_queue, arm_name, 1);
        if (leg_name != null)
          lo_hr_leg_tex = PlayerLoader.LoadHighResoTexs(load_queue, leg_name, 1);
      }
      LoadObject loHairOverlay = (LoadObject) null;
      if (use_hair_overlay != -1)
        loHairOverlay = PlayerLoader.LoadHairOverlayTexs(load_queue, info, use_hair_overlay);
      yield return (object) load_queue.Wait();
      List<string> needAtkInfoNames = new List<string>();
      StringKeyTable<LoadObject> animEventBulletLoadObjTable = new StringKeyTable<LoadObject>();
      int skill_len;
      int i;
      if (Object.op_Inequality((Object) player, (Object) null))
      {
        if (need_anim_event)
          this.animObjectTable.ForEach((Action<LoadObject>) (load_object =>
          {
            load_queue.CacheAnimDataUseResource(load_object.loadedObjects[1].obj as AnimEventData, (LoadingQueue.EffectNameAnalyzer) (effect_name => effect_name[0] != '@' ? effect_name : (string) null));
            load_queue.CacheAnimDataUseResourceDependPlayer(player, load_object.loadedObjects[1].obj as AnimEventData);
            AnimEventData animEventData = load_object.loadedObjects[1].obj as AnimEventData;
            if (!Object.op_Inequality((Object) animEventData, (Object) null) || ((IList<AnimEventData.AnimData>) animEventData.animations).IsNullOrEmpty<AnimEventData.AnimData>())
              return;
            foreach (AnimEventData.AnimData animation in animEventData.animations)
            {
              foreach (AnimEventData.EventData eventData in animation.events)
              {
                AnimEventFormat.ID id3 = eventData.id;
                if (id3 <= AnimEventFormat.ID.SHOT_ZONE)
                {
                  if (id3 != AnimEventFormat.ID.SHOT_PRESENT)
                  {
                    if (id3 != AnimEventFormat.ID.SHOT_ZONE)
                      goto label_23;
                  }
                  else
                  {
                    int index3 = 0;
                    for (int length1 = eventData.stringArgs.Length; index3 < length1; ++index3)
                    {
                      string[] self = eventData.stringArgs[index3].Split(':');
                      if (!((IList<string>) self).IsNullOrEmpty<string>())
                      {
                        int index4 = 0;
                        for (int length2 = self.Length; index4 < length2; ++index4)
                        {
                          string str = self[index4];
                          if (!string.IsNullOrEmpty(str))
                          {
                            LoadObject loadObject5 = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, str);
                            if (loadObject5 != null && animEventBulletLoadObjTable.Get(str) == null)
                              animEventBulletLoadObjTable.Add(str, loadObject5);
                          }
                        }
                      }
                    }
                    goto label_23;
                  }
                }
                else if (id3 != AnimEventFormat.ID.SHOT_DECOY && id3 != AnimEventFormat.ID.LOAD_BULLET)
                  goto label_23;
                for (int index = 0; index < eventData.stringArgs.Length; ++index)
                {
                  string stringArg = eventData.stringArgs[index];
                  if (!string.IsNullOrEmpty(stringArg))
                  {
                    LoadObject loadObject6 = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, stringArg);
                    if (loadObject6 != null && animEventBulletLoadObjTable.Get(stringArg) == null)
                      animEventBulletLoadObjTable.Add(stringArg, loadObject6);
                  }
                }
label_23:
                if (id3 <= AnimEventFormat.ID.SHOT_NODE_LINK)
                {
                  if (id3 <= AnimEventFormat.ID.NWAY_LASER_ATTACK)
                  {
                    if (id3 != AnimEventFormat.ID.SHOT_ARROW && (uint) (id3 - 70) > 5U && id3 != AnimEventFormat.ID.NWAY_LASER_ATTACK)
                      continue;
                  }
                  else if (id3 <= AnimEventFormat.ID.GENERATE_TRACKING)
                  {
                    if (id3 != AnimEventFormat.ID.EXATK_COLLIDER_START && id3 != AnimEventFormat.ID.GENERATE_TRACKING)
                      continue;
                  }
                  else if (id3 != AnimEventFormat.ID.PLAYER_FUNNEL_ATTACK && id3 != AnimEventFormat.ID.SHOT_NODE_LINK)
                    continue;
                }
                else if (id3 <= AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE_MULTI)
                {
                  if (id3 <= AnimEventFormat.ID.PAIR_SWORDS_SHOT_LASER)
                  {
                    if (id3 != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE && (uint) (id3 - 199) > 1U)
                      continue;
                  }
                  else if (id3 != AnimEventFormat.ID.SHOT_HEALING_HOMING && id3 != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE_MULTI)
                    continue;
                }
                else if (id3 <= AnimEventFormat.ID.BUFF_START_SHIELD_REFLECT)
                {
                  if (id3 != AnimEventFormat.ID.SHOT_RESURRECTION_HOMING && id3 != AnimEventFormat.ID.BUFF_START_SHIELD_REFLECT)
                    continue;
                }
                else if (id3 != AnimEventFormat.ID.SHOT_ORACLE_SPEAR_SP && id3 != AnimEventFormat.ID.SHOT_ORACLE_PAIR_SWORDS_RUSH)
                  continue;
                string stringArg1 = eventData.stringArgs[0];
                if (!string.IsNullOrEmpty(stringArg1))
                {
                  foreach (string str in ResourceName.GetNamesNeededLoadAtkInfoFromAnimEvent())
                  {
                    if (stringArg1.StartsWith(str))
                    {
                      needAtkInfoNames.Add(stringArg1);
                      break;
                    }
                  }
                }
              }
            }
          }));
        this.AddSkillAttackInfoName(player, ref needAtkInfoNames);
        this.AddFieldGimmickAttackInfoName(ref needAtkInfoNames);
        List<LoadObject> atkInfo = new List<LoadObject>();
        List<LoadObject> atkInfoLoaded = new List<LoadObject>();
        Dictionary<string, LoadObject> atkInfoDict = new Dictionary<string, LoadObject>();
        if (this.playerLoaderLoadedAttackInfoNames == null)
          this.playerLoaderLoadedAttackInfoNames = new HashSet<string>();
        for (int index = 0; index < needAtkInfoNames.Count; ++index)
        {
          if (!this.playerLoaderLoadedAttackInfoNames.Contains(needAtkInfoNames[index]))
          {
            if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[index]))
            {
              LoadObject loadObject7 = load_queue.Load(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[index]);
              atkInfo.Add(loadObject7);
              atkInfoLoaded.Add(loadObject7);
              atkInfoDict.Add(needAtkInfoNames[index], loadObject7);
            }
            else
            {
              Object playerResourceCache = MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[index]);
              LoadObject loadObject8 = new LoadObject();
              loadObject8.loadedObject = playerResourceCache;
              atkInfoLoaded.Add(loadObject8);
              atkInfoDict.Add(needAtkInfoNames[index], loadObject8);
            }
            this.playerLoaderLoadedAttackInfoNames.Add(needAtkInfoNames[index]);
          }
        }
        if (load_queue.IsLoading())
          yield return (object) load_queue.Wait();
        if (info.isNeedToCache)
        {
          foreach (KeyValuePair<string, LoadObject> keyValuePair in atkInfoDict)
          {
            string key = keyValuePair.Key;
            LoadObject loadObject9 = keyValuePair.Value;
            if (atkInfo.Contains(loadObject9))
              MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, key, loadObject9.loadedObject);
          }
        }
        if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
        {
          Transform _settingTransform = MonoBehaviourSingleton<InGameSettingsManager>.I._transform;
          List<AttackInfo> hitInfos = new List<AttackInfo>();
          for (i = 0; i < atkInfoLoaded.Count; ++i)
          {
            SplitPlayerAttackInfo attackInfo = ((Component) LoadObject.RealizesWithGameObject((GameObject) atkInfoLoaded[i].loadedObject, _settingTransform)).gameObject.GetComponent<SplitPlayerAttackInfo>();
            hitInfos.Add((AttackInfo) attackInfo.attackHitInfo);
            hitInfos.Add((AttackInfo) attackInfo.attackContinuationInfo);
            if (!string.IsNullOrEmpty(attackInfo.attackHitInfo.nextBulletInfoName))
              yield return (object) this.StartCoroutine(this.LoadNextBulletInfo(load_queue, attackInfo.attackHitInfo.nextBulletInfoName, hitInfos, info.isNeedToCache));
            if (!string.IsNullOrEmpty(attackInfo.attackContinuationInfo.nextBulletInfoName))
              yield return (object) this.StartCoroutine(this.LoadNextBulletInfo(load_queue, attackInfo.attackContinuationInfo.nextBulletInfoName, hitInfos, info.isNeedToCache));
            attackInfo = (SplitPlayerAttackInfo) null;
          }
          InGameSettingsManager.Player player1 = MonoBehaviourSingleton<InGameSettingsManager>.I.player;
          if (player1.attackInfosAll == null)
            player1.attackInfosAll = new AttackInfo[0];
          AttackInfo[] mergedArray = Utility.CreateMergedArray<AttackInfo>(player1.attackInfosAll, hitInfos.ToArray());
          player1.attackInfosAll = Utility.DistinctArray<AttackInfo>(mergedArray);
          player.AddAttackInfos(hitInfos.ToArray());
          _settingTransform = (Transform) null;
          hitInfos = (List<AttackInfo>) null;
        }
        this.LoadAttackInfoResource(player, load_queue);
        ELEMENT_TYPE nowWeaponElement = player.GetNowWeaponElement();
        switch (anim_id)
        {
          case 0:
            load_queue.CacheSE(10000042);
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_sword_01_01");
            switch (player.spAttackType)
            {
              case SP_ATTACK_TYPE.HEAT:
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl05_attack_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_sword_01_04");
                break;
              case SP_ATTACK_TYPE.SOUL:
                if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
                {
                  InGameSettingsManager.Player.OneHandSwordActionInfo ohsActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo;
                  load_queue.CacheSE(ohsActionInfo.Soul_BoostSeId);
                  load_queue.CacheSE(ohsActionInfo.Soul_SnatchHitSeId);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsActionInfo.Soul_SnatchHitEffect);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsActionInfo.Soul_SnatchHitRemainEffect);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsActionInfo.Soul_SnatchHitEffectOnBoostMode);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_btl_soul_energy_01");
                  if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) ohsActionInfo.Soul_BoostElementHitEffect.Length)
                  {
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsActionInfo.Soul_BoostElementHitEffect[(int) nowWeaponElement]);
                    break;
                  }
                  break;
                }
                break;
              case SP_ATTACK_TYPE.BURST:
                InGameSettingsManager.Player.BurstOneHandSwordActionInfo burstOhsInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.burstOHSInfo;
                if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) burstOhsInfo.BoostElementHitEffect.Length)
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstOhsInfo.BoostElementHitEffect[(int) nowWeaponElement]);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
                break;
              case SP_ATTACK_TYPE.ORACLE:
                InGameSettingsManager.Player.OracleOneHandSwordActionInfo oracleOhsInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.oracleOHSInfo;
                for (int index = 0; index < oracleOhsInfo.dragonEffects.Length; ++index)
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleOhsInfo.dragonEffects[index].GetEffectName(nowWeaponElement));
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_sword_dragon_veil");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_sword_dragon_veil_re");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, $"ef_btl_wsk4_sword_02_{(int) nowWeaponElement:D2}");
                break;
            }
            break;
          case 1:
            if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
            {
              InGameSettingsManager.Player.TwoHandSwordActionInfo handSwordActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo;
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, handSwordActionInfo.nameChargeExpandEffect);
              if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
              {
                load_queue.CacheSE(handSwordActionInfo.soulBoostSeId);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, handSwordActionInfo.soulIaiChargeMaxEffect);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_btl_soul_energy_01");
              }
              if (player.spAttackType == SP_ATTACK_TYPE.BURST)
              {
                InGameSettingsManager.Player.BurstTwoHandSwordActionInfo burstThsInfo = handSwordActionInfo.burstTHSInfo;
                if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) burstThsInfo.HitEffect_SingleShot.Length)
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstThsInfo.HitEffect_SingleShot[(int) nowWeaponElement]);
                if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) burstThsInfo.HitEffect_FullBurst.Length)
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstThsInfo.HitEffect_FullBurst[(int) nowWeaponElement]);
              }
              if (player.spAttackType == SP_ATTACK_TYPE.ORACLE)
              {
                InGameSettingsManager.Player.OracleTwoHandSwordActionInfo oracleThsInfo = handSwordActionInfo.oracleTHSInfo;
                load_queue.CacheSE(oracleThsInfo.normalVernierSeId);
                load_queue.CacheSE(oracleThsInfo.maxVernierSeId);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleThsInfo.normalVernierEffectName);
                if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) oracleThsInfo.maxVernierEffectNames.Length)
                {
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleThsInfo.maxVernierEffectNames[(int) nowWeaponElement]);
                  break;
                }
                break;
              }
              break;
            }
            break;
          case 2:
            switch (player.spAttackType)
            {
              case SP_ATTACK_TYPE.NONE:
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_loop_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_loop_02");
                break;
              case SP_ATTACK_TYPE.HEAT:
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_e_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_spear_01_03");
                break;
              case SP_ATTACK_TYPE.SOUL:
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_btl_soul_energy_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_spear_02_02");
                break;
              case SP_ATTACK_TYPE.BURST:
                if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
                {
                  InGameSettingsManager.Player.BurstSpearActionInfo burstSpearInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.burstSpearInfo;
                  if (burstSpearInfo.spinSeId > 0)
                    load_queue.CacheSE(burstSpearInfo.spinSeId);
                  if (burstSpearInfo.spinMaxSpeedSeId > 0)
                    load_queue.CacheSE(burstSpearInfo.spinMaxSpeedSeId);
                  string name1 = string.Empty;
                  if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) burstSpearInfo.spinEffectNames.Length)
                    name1 = burstSpearInfo.spinEffectNames[(int) nowWeaponElement];
                  string name2 = string.Empty;
                  if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) burstSpearInfo.spinElementHitEffectNames.Length)
                    name2 = burstSpearInfo.spinElementHitEffectNames[(int) nowWeaponElement];
                  string name3 = string.Empty;
                  if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) burstSpearInfo.spinThrowGroundEffectNames.Length)
                    name3 = burstSpearInfo.spinThrowGroundEffectNames[(int) nowWeaponElement];
                  if (!string.IsNullOrEmpty(name1))
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name1);
                  if (!string.IsNullOrEmpty(name2))
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name2);
                  if (!string.IsNullOrEmpty(name3))
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name3);
                  if (!string.IsNullOrEmpty(burstSpearInfo.throwGroundEffectName))
                  {
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstSpearInfo.throwGroundEffectName);
                    break;
                  }
                  break;
                }
                break;
              case SP_ATTACK_TYPE.ORACLE:
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_spear_aura");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_spear_guard");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_spear_stock");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_sword_01_01");
                load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.oracle.gutsSE);
                load_queue.CacheSE(10000042);
                break;
            }
            if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid() && player.spAttackType != SP_ATTACK_TYPE.BURST)
            {
              InGameSettingsManager.Player.SpearActionInfo spearActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo;
              string name = spearActionInfo.jumpHugeHitEffectName;
              if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) spearActionInfo.jumpHugeElementHitEffectNames.Length)
                name = spearActionInfo.jumpHugeElementHitEffectNames[(int) nowWeaponElement];
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name);
              break;
            }
            break;
          case 4:
            if (player.spAttackType == SP_ATTACK_TYPE.NONE)
            {
              if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
                load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.wildDanceChargeMaxSeId);
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
              break;
            }
            if (player.spAttackType == SP_ATTACK_TYPE.HEAT)
            {
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_02");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_03");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_04");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_05");
              break;
            }
            if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
            {
              if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
              {
                InGameSettingsManager.Player.PairSwordsActionInfo swordsActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo;
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, swordsActionInfo.Soul_EffectForWaitingLaser);
                string soulEffectForBullet = swordsActionInfo.Soul_EffectForBullet;
                if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) swordsActionInfo.Soul_EffectsForBullet.Length)
                  soulEffectForBullet = swordsActionInfo.Soul_EffectsForBullet[(int) nowWeaponElement];
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, soulEffectForBullet);
                if (!((IList<int>) swordsActionInfo.Soul_SeIds).IsNullOrEmpty<int>())
                {
                  for (int index = 0; index < swordsActionInfo.Soul_SeIds.Length; ++index)
                  {
                    if (swordsActionInfo.Soul_SeIds[index] >= 0)
                      load_queue.CacheSE(swordsActionInfo.Soul_SeIds[index]);
                  }
                  break;
                }
                break;
              }
              break;
            }
            if (player.spAttackType == SP_ATTACK_TYPE.BURST)
            {
              load_queue.CacheSE(10000051);
              load_queue.CacheSE(10000042);
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_twinsword_01_00");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
              InGameSettingsManager.Player.PairSwordsActionInfo swordsActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo;
              if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) swordsActionInfo.Burst_CombineHitEffect.Length)
              {
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, swordsActionInfo.Burst_CombineHitEffect[(int) nowWeaponElement]);
                break;
              }
              break;
            }
            if (player.spAttackType == SP_ATTACK_TYPE.ORACLE)
            {
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_twinsword_01");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, $"ef_btl_wsk4_twinsword_01_{(int) nowWeaponElement:D2}");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_twinsword_03");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_twinsword_04");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, $"ef_btl_wsk4_twinsword_05_{(int) nowWeaponElement:D2}");
              break;
            }
            break;
          case 5:
            if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
            {
              InGameSettingsManager.Player.SpecialActionInfo specialActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo;
              InGameSettingsManager.TargetMarkerSettings targetMarkerSettings = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerSettings;
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowChargeAimEffectName);
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowAimLesserCursorEffectName);
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.bestDistanceEffect);
              if (player.spAttackType == SP_ATTACK_TYPE.NONE)
              {
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[6]);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[5]);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowBleedEffectName);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowBleedDamageEffectName);
                switch (nowWeaponElement)
                {
                  case ELEMENT_TYPE.FIRE:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowFireBurstEffectName);
                    break;
                  case ELEMENT_TYPE.WATER:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowWaterBurstEffectName);
                    break;
                  case ELEMENT_TYPE.THUNDER:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowThunderBurstEffectName);
                    break;
                  case ELEMENT_TYPE.SOIL:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowSoilBurstEffectName);
                    break;
                  case ELEMENT_TYPE.LIGHT:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowLightrBurstEffectName);
                    break;
                  case ELEMENT_TYPE.DARK:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowDarkBurstEffectName);
                    break;
                  default:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowBurstEffectName);
                    break;
                }
              }
              else
              {
                if (player.spAttackType == SP_ATTACK_TYPE.HEAT)
                {
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[22]);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[21]);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_bow_01_02");
                  break;
                }
                if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
                {
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[24]);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_bow_lock_02");
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
                  load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulLockMaxSeId);
                  load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulLockSeId);
                  load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.soulBoostSeId);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
                  break;
                }
                if (player.spAttackType == SP_ATTACK_TYPE.BURST)
                {
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[27]);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[26]);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.GetBombArrowEffectName(nowWeaponElement));
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.arrowRainShotAimLesserCursorEffectName);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.GetBombEffectName(nowWeaponElement));
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.boostArrowChargeMaxEffectName);
                  load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.burstBoostModeSEId);
                  List<int> bombArrowSeIdList = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombArrowSEIdList;
                  for (int index = 0; index < bombArrowSeIdList.Count; ++index)
                    load_queue.CacheSE(bombArrowSeIdList[index]);
                  List<int> bombSeIdList = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombSEIdList;
                  for (int index = 0; index < bombSeIdList.Count; ++index)
                    load_queue.CacheSE(bombSeIdList[index]);
                  break;
                }
                break;
              }
            }
            else
              break;
            break;
        }
        EvolveController.Load(load_queue, info.weaponEvolveId);
        skill_len = 3;
        LoadObject[] bullet_load = new LoadObject[skill_len];
        for (int index5 = 0; index5 < skill_len; ++index5)
        {
          SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + index5);
          if (skillParam == null)
          {
            bullet_load[index5] = (LoadObject) null;
          }
          else
          {
            SkillItemTable.SkillItemData tableData = skillParam.tableData;
            if (!string.IsNullOrEmpty(tableData.startEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.startEffectName);
            if (tableData.startSEID != 0)
              load_queue.CacheSE(tableData.startSEID);
            if (!string.IsNullOrEmpty(tableData.actLocalEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.actLocalEffectName);
            if (!string.IsNullOrEmpty(tableData.actOneshotEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.actOneshotEffectName);
            if (tableData.actSEID != 0)
              load_queue.CacheSE(tableData.actSEID);
            if (!string.IsNullOrEmpty(tableData.enchantEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.enchantEffectName);
            if (!string.IsNullOrEmpty(tableData.hitEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.hitEffectName);
            if ((double) (float) tableData.skillRange > 0.0 && !string.IsNullOrEmpty(MonoBehaviourSingleton<InGameSettingsManager>.I.player.skillRangeEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.skillRangeEffectName);
            if (tableData.hitSEID != 0)
              load_queue.CacheSE(tableData.hitSEID);
            if (is_self)
              load_queue.CacheItemIcon(tableData.iconID);
            if (tableData.healType == HEAL_TYPE.RESURRECTION_ALL)
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_heal_04_03");
            if (!((IList<int>) tableData.buffTableIds).IsNullOrEmpty<int>())
            {
              for (int index6 = 0; index6 < tableData.buffTableIds.Length; ++index6)
              {
                BuffTable.BuffData data3 = Singleton<BuffTable>.I.GetData((uint) tableData.buffTableIds[index6]);
                if (BuffParam.IsHitAbsorbType(data3.type))
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_drain_01_01");
                else if (data3.type == BuffParam.BUFFTYPE.AUTO_REVIVE)
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_heal_04_03");
              }
            }
            foreach (string name in tableData.supportEffectName)
            {
              if (!string.IsNullOrEmpty(name))
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name);
            }
            foreach (BuffParam.BUFFTYPE bufftype in tableData.supportType)
            {
              switch (bufftype)
              {
                case BuffParam.BUFFTYPE.SKILL_CHARGE_ABOVE:
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_btl_sk_magi_move_01_01");
                  break;
                case BuffParam.BUFFTYPE.SUBSTITUTE:
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_magi_shikigami_01_02");
                  break;
              }
            }
            if (tableData.isTeleportation)
            {
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_warp_02_01");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_warp_02_02");
            }
            bullet_load[index5] = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, tableData.bulletName);
          }
        }
        if (is_self)
          load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_darkness_02");
        EffectPlayProcessor effectPlayProcessor = player.effectPlayProcessor;
        if (Object.op_Inequality((Object) effectPlayProcessor, (Object) null) && effectPlayProcessor.effectSettings != null)
        {
          int index = 0;
          for (int length = effectPlayProcessor.effectSettings.Length; index < length; ++index)
          {
            if (!string.IsNullOrEmpty(effectPlayProcessor.effectSettings[index].effectName))
            {
              string name = effectPlayProcessor.effectSettings[index].name;
              if (name.StartsWith("BUFF_"))
              {
                string str = name.Substring(name.Length - "_PLC00".Length);
                if (str.Contains("_PLC") && str != "_PLC" + (this.loadInfo.weaponModelID / 1000).ToString("D2"))
                  continue;
              }
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor.effectSettings[index].effectName);
            }
          }
        }
        if (load_queue.IsLoading())
          yield return (object) load_queue.Wait();
        for (int index = 0; index < skill_len; ++index)
        {
          SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + index);
          if (skillParam != null)
          {
            skillParam.bullet = bullet_load[index].loadedObject as BulletData;
            load_queue.CacheBulletDataUseResource(skillParam.bullet, player);
          }
        }
        if (animEventBulletLoadObjTable != null)
          animEventBulletLoadObjTable.ForEachKeyAndValue((Action<string, LoadObject>) ((key, item) =>
          {
            if (item == null || !Object.op_Inequality(item.loadedObject, (Object) null))
              return;
            BulletData loadedObject = item.loadedObject as BulletData;
            if (!Object.op_Inequality((Object) loadedObject, (Object) null) || !Object.op_Equality((Object) player.cachedBulletDataTable.Get(key), (Object) null))
              return;
            player.cachedBulletDataTable.Add(key, loadedObject);
            load_queue.CacheBulletDataUseResource(loadedObject, player);
          }));
        animEventBulletLoadObjTable.Clear();
        animEventBulletLoadObjTable = (StringKeyTable<LoadObject>) null;
        if (load_queue.IsLoading())
          yield return (object) load_queue.Wait();
        atkInfo = (List<LoadObject>) null;
        atkInfoLoaded = (List<LoadObject>) null;
        atkInfoDict = (Dictionary<string, LoadObject>) null;
        bullet_load = (LoadObject[]) null;
      }
      if (lo_arm != null)
      {
        if (Object.op_Inequality(lo_arm.loadedObject, (Object) null))
        {
          GameObject loadedObject = lo_arm.loadedObject as GameObject;
          EffectPlayProcessor component = Object.op_Inequality((Object) loadedObject, (Object) null) ? loadedObject.GetComponent<EffectPlayProcessor>() : (EffectPlayProcessor) null;
          if (Object.op_Inequality((Object) component, (Object) null) && component.effectSettings != null)
          {
            int index = 0;
            for (int length = component.effectSettings.Length; index < length; ++index)
            {
              if (!string.IsNullOrEmpty(component.effectSettings[index].effectName))
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, component.effectSettings[index].effectName);
            }
          }
        }
      }
      else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ARM, arm_name))
      {
        GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ARM, arm_name);
        EffectPlayProcessor component = Object.op_Inequality((Object) playerResourceCache, (Object) null) ? playerResourceCache.GetComponent<EffectPlayProcessor>() : (EffectPlayProcessor) null;
        if (Object.op_Inequality((Object) component, (Object) null) && component.effectSettings != null)
        {
          int index = 0;
          for (int length = component.effectSettings.Length; index < length; ++index)
          {
            if (!string.IsNullOrEmpty(component.effectSettings[index].effectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, component.effectSettings[index].effectName);
          }
        }
      }
      if (lo_leg != null)
      {
        if (Object.op_Inequality(lo_leg.loadedObject, (Object) null))
        {
          GameObject loadedObject = lo_leg.loadedObject as GameObject;
          EffectPlayProcessor component = Object.op_Inequality((Object) loadedObject, (Object) null) ? loadedObject.GetComponent<EffectPlayProcessor>() : (EffectPlayProcessor) null;
          if (Object.op_Inequality((Object) component, (Object) null) && component.effectSettings != null)
          {
            int index = 0;
            for (int length = component.effectSettings.Length; index < length; ++index)
            {
              if (!string.IsNullOrEmpty(component.effectSettings[index].effectName))
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, component.effectSettings[index].effectName);
            }
          }
        }
      }
      else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_LEG, leg_name))
      {
        GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_LEG, leg_name);
        EffectPlayProcessor component = Object.op_Inequality((Object) playerResourceCache, (Object) null) ? playerResourceCache.GetComponent<EffectPlayProcessor>() : (EffectPlayProcessor) null;
        if (Object.op_Inequality((Object) component, (Object) null) && component.effectSettings != null)
        {
          int index = 0;
          for (int length = component.effectSettings.Length; index < length; ++index)
          {
            if (!string.IsNullOrEmpty(component.effectSettings[index].effectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, component.effectSettings[index].effectName);
          }
        }
      }
      if (load_queue.IsLoading())
        yield return (object) load_queue.Wait();
      bool wait = false;
      bool div_frame_realizes = false;
      int skin_color = info.skinColor;
      if (lo_body != null)
      {
        if (!div_frame_realizes)
        {
          this.body = lo_body.Realizes(_this);
          this.renderersBody = ((Component) this.body).gameObject.GetComponentsInChildren<Renderer>();
          ModelLoaderBase.SetEnabled(this.renderersBody, false);
          this.SetDynamicBones_Body(this.body, enableBone);
        }
        else
        {
          wait = true;
          InstantiateManager.Request((Object) this, lo_body.loadedObject, (Action<InstantiateManager.InstantiateData>) (data =>
          {
            this.body = ((GameObject) data.instantiatedObject).transform;
            this.body.SetParent(_this, false);
            this.renderersBody = ((Component) this.body).GetComponentsInChildren<Renderer>();
            this.SetDynamicBones_Body(this.body, enableBone);
            PlayerLoader.SetRenderersEnabled(this.renderersBody, false);
            wait = false;
          }));
          while (wait)
            yield return (object) null;
        }
      }
      else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_BDY, body_name))
      {
        GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_BDY, body_name);
        if (Object.op_Inequality((Object) playerResourceCache, (Object) null))
        {
          if (!div_frame_realizes)
          {
            this.body = LoadObject.RealizesWithGameObject(playerResourceCache, _this);
            this.renderersBody = ((Component) this.body).gameObject.GetComponentsInChildren<Renderer>();
            ModelLoaderBase.SetEnabled(this.renderersBody, false);
            this.SetDynamicBones_Body(this.body, enableBone);
          }
          else
          {
            wait = true;
            InstantiateManager.Request((Object) this, (Object) playerResourceCache, (Action<InstantiateManager.InstantiateData>) (data =>
            {
              this.body = ((GameObject) data.instantiatedObject).transform;
              this.body.SetParent(_this, false);
              this.renderersBody = ((Component) this.body).GetComponentsInChildren<Renderer>();
              this.SetDynamicBones_Body(this.body, enableBone);
              PlayerLoader.SetRenderersEnabled(this.renderersBody, false);
              wait = false;
            }));
            while (wait)
              yield return (object) null;
          }
        }
      }
      if (!Object.op_Equality((Object) this.body, (Object) null))
      {
        yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, this.body, shader_type));
        if (this.renderersBody != null)
        {
          if (this.renderersBody.Length != 0)
          {
            SkinnedMeshRenderer skinnedMeshRenderer = this.renderersBody[0] as SkinnedMeshRenderer;
            if (Object.op_Inequality((Object) skinnedMeshRenderer, (Object) null))
              skinnedMeshRenderer.localBounds = PlayerLoader.BOUNDS;
          }
          PlayerLoader.SetSkinAndEquipColor(this.renderersBody, skin_color, info.bodyColor, 0.0f);
          PlayerLoader.ApplyEquipHighResoTexs(lo_hr_bdy_tex, this.renderersBody);
          this.animator = ((Component) this.body).GetComponentInChildren<Animator>();
          if (Object.op_Inequality((Object) player, (Object) null))
            player.body = this.body;
          this.socketRoot = Utility.Find(this.body, "Root");
          this.socketHead = Utility.Find(this.body, "Head");
          this.socketWepL = Utility.Find(this.body, "L_Wep");
          this.socketWepR = Utility.Find(this.body, "R_Wep");
          this.socketFootL = Utility.Find(this.body, "L_Foot");
          this.socketFootR = Utility.Find(this.body, "R_Foot");
          this.socketHandL = Utility.Find(this.body, "L_Hand");
          this.socketHandR = Utility.Find(this.body, "R_Hand");
          this.socketForearmL = Utility.Find(this.body, "L_Forearm");
          this.socketForearmR = Utility.Find(this.body, "R_Forearm");
          if (need_foot_stamp)
          {
            if (Object.op_Inequality((Object) this.socketFootL, (Object) null) && Object.op_Equality((Object) ((Component) this.socketFootL).GetComponent<StampNode>(), (Object) null))
            {
              StampNode stampNode = ((Component) this.socketFootL).gameObject.AddComponent<StampNode>();
              stampNode.offset = new Vector3(-0.08f, 0.01f, 0.0f);
              stampNode.autoBaseY = 0.1f;
            }
            if (Object.op_Inequality((Object) this.socketFootR, (Object) null) && Object.op_Equality((Object) ((Component) this.socketFootR).GetComponent<StampNode>(), (Object) null))
            {
              StampNode stampNode = ((Component) this.socketFootR).gameObject.AddComponent<StampNode>();
              stampNode.offset = new Vector3(-0.08f, 0.01f, 0.0f);
              stampNode.autoBaseY = 0.1f;
            }
            CharacterStampCtrl characterStampCtrl = ((Component) this.body).GetComponent<CharacterStampCtrl>();
            if (Object.op_Equality((Object) characterStampCtrl, (Object) null))
              characterStampCtrl = ((Component) this.body).gameObject.AddComponent<CharacterStampCtrl>();
            characterStampCtrl.Init(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos, (Character) player);
            int index = 0;
            for (int length = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos.Length; index < length; ++index)
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos[index].effectName);
            if (load_queue.IsLoading())
              yield return (object) load_queue.Wait();
          }
          if (lo_face != null)
          {
            if (!div_frame_realizes)
            {
              this.face = lo_face.Realizes(this.socketHead);
              this.renderersFace = ((Component) this.face).gameObject.GetComponentsInChildren<Renderer>();
              ModelLoaderBase.SetEnabled(this.renderersFace, false);
            }
            else
            {
              wait = true;
              InstantiateManager.Request((Object) this, lo_face.loadedObject, (Action<InstantiateManager.InstantiateData>) (data =>
              {
                this.face = ((GameObject) data.instantiatedObject).transform;
                this.face.SetParent(this.socketHead, false);
                this.renderersFace = ((Component) this.face).GetComponentsInChildren<Renderer>();
                PlayerLoader.SetRenderersEnabled(this.renderersFace, false);
                wait = false;
              }));
              while (wait)
                yield return (object) null;
              if (this.renderersFace == null)
                yield break;
            }
            PlayerLoader.SetSkinColor(this.renderersFace, skin_color);
            this.validFaceChange = this.renderersFace != null && this.renderersFace.Length != 0 && this.renderersFace[0].material.HasProperty("_Face_shift");
            this.eyeBlink = enable_eye_blick;
          }
          else
          {
            GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_FACE, face_name);
            if (Object.op_Inequality((Object) playerResourceCache, (Object) null))
            {
              if (!div_frame_realizes)
              {
                this.face = LoadObject.RealizesWithGameObject(playerResourceCache, this.socketHead);
                this.renderersFace = ((Component) this.face).gameObject.GetComponentsInChildren<Renderer>();
                ModelLoaderBase.SetEnabled(this.renderersFace, false);
              }
              else
              {
                wait = true;
                InstantiateManager.Request((Object) this, (Object) playerResourceCache, (Action<InstantiateManager.InstantiateData>) (data =>
                {
                  this.face = ((GameObject) data.instantiatedObject).transform;
                  this.face.SetParent(this.socketHead, false);
                  this.renderersFace = ((Component) this.face).GetComponentsInChildren<Renderer>();
                  PlayerLoader.SetRenderersEnabled(this.renderersFace, false);
                  wait = false;
                }));
                while (wait)
                  yield return (object) null;
                if (this.renderersFace == null)
                  yield break;
              }
              PlayerLoader.SetSkinColor(this.renderersFace, skin_color);
              this.validFaceChange = this.renderersFace != null && this.renderersFace.Length != 0 && this.renderersFace[0].material.HasProperty("_Face_shift");
              this.eyeBlink = enable_eye_blick;
            }
          }
          if (lo_hair != null)
          {
            if (!div_frame_realizes)
            {
              this.hair = lo_hair.Realizes(this.socketHead);
              this.renderersHair = ((Component) this.hair).GetComponentsInChildren<Renderer>();
              ModelLoaderBase.SetEnabled(this.renderersHair, false);
              this.SetDynamicBones(this.body, this.hair, enableBone);
            }
            else
            {
              wait = true;
              InstantiateManager.Request((Object) this, lo_hair.loadedObject, (Action<InstantiateManager.InstantiateData>) (data =>
              {
                this.hair = ((GameObject) data.instantiatedObject).transform;
                this.hair.SetParent(this.socketHead, false);
                this.renderersHair = ((Component) this.hair).GetComponentsInChildren<Renderer>();
                this.SetDynamicBones(this.body, this.hair, enableBone);
                PlayerLoader.SetRenderersEnabled(this.renderersHair, false);
                wait = false;
              }));
              while (wait)
                yield return (object) null;
              if (this.renderersHair == null)
                yield break;
            }
            PlayerLoader.SetSkinAndEquipColor(this.renderersHair, skin_color, info.hairColor, 0.0f);
            if (loHairOverlay != null)
              PlayerLoader.ApplyHairOverlay(loHairOverlay, this.renderersHair);
          }
          else
          {
            GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name);
            if (Object.op_Inequality((Object) playerResourceCache, (Object) null))
            {
              if (!div_frame_realizes)
              {
                this.hair = LoadObject.RealizesWithGameObject(playerResourceCache, this.socketHead);
                this.renderersHair = ((Component) this.hair).GetComponentsInChildren<Renderer>();
                ModelLoaderBase.SetEnabled(this.renderersHair, false);
                this.SetDynamicBones(this.body, this.hair, enableBone);
              }
              else
              {
                wait = true;
                InstantiateManager.Request((Object) this, (Object) playerResourceCache, (Action<InstantiateManager.InstantiateData>) (data =>
                {
                  this.hair = ((GameObject) data.instantiatedObject).transform;
                  this.hair.SetParent(this.socketHead, false);
                  this.renderersHair = ((Component) this.hair).GetComponentsInChildren<Renderer>();
                  this.SetDynamicBones(this.body, this.hair, enableBone);
                  PlayerLoader.SetRenderersEnabled(this.renderersHair, false);
                  wait = false;
                }));
                while (wait)
                  yield return (object) null;
                if (this.renderersHair == null)
                  yield break;
              }
              PlayerLoader.SetSkinAndEquipColor(this.renderersHair, skin_color, info.hairColor, 0.0f);
              if (loHairOverlay != null)
                PlayerLoader.ApplyHairOverlay(loHairOverlay, this.renderersHair);
            }
          }
          if (lo_head != null)
          {
            if (!div_frame_realizes)
            {
              this.head = lo_head.Realizes(this.socketHead);
              if (Object.op_Inequality((Object) this.head, (Object) null))
              {
                this.renderersHead = ((Component) this.head).GetComponentsInChildren<Renderer>();
                ModelLoaderBase.SetEnabled(this.renderersHead, false);
              }
            }
            else
            {
              wait = true;
              InstantiateManager.Request((Object) this, lo_head.loadedObject, (Action<InstantiateManager.InstantiateData>) (data =>
              {
                this.head = ((GameObject) data.instantiatedObject).transform;
                this.head.SetParent(this.socketHead, false);
                this.renderersHead = ((Component) this.head).GetComponentsInChildren<Renderer>();
                PlayerLoader.SetRenderersEnabled(this.renderersHead, false);
                wait = false;
              }));
              while (wait)
                yield return (object) null;
              if (this.renderersHead == null)
                yield break;
            }
            if (Object.op_Inequality((Object) this.head, (Object) null))
              yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, this.head, shader_type));
            PlayerLoader.SetEquipColor(this.renderersHead, info.headColor);
            PlayerLoader.ApplyEquipHighResoTexs(lo_hr_hed_tex, this.renderersHead);
          }
          else
          {
            GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_HEAD, head_name);
            if (Object.op_Inequality((Object) playerResourceCache, (Object) null))
            {
              if (!div_frame_realizes)
              {
                this.head = LoadObject.RealizesWithGameObject(playerResourceCache, this.socketHead);
                if (Object.op_Inequality((Object) this.head, (Object) null))
                {
                  this.renderersHead = ((Component) this.head).GetComponentsInChildren<Renderer>();
                  ModelLoaderBase.SetEnabled(this.renderersHead, false);
                }
              }
              else
              {
                wait = true;
                InstantiateManager.Request((Object) this, (Object) playerResourceCache, (Action<InstantiateManager.InstantiateData>) (data =>
                {
                  this.head = ((GameObject) data.instantiatedObject).transform;
                  this.head.SetParent(this.socketHead, false);
                  this.renderersHead = ((Component) this.head).GetComponentsInChildren<Renderer>();
                  PlayerLoader.SetRenderersEnabled(this.renderersHead, false);
                  wait = false;
                }));
                while (wait)
                  yield return (object) null;
                if (this.renderersHead == null)
                  yield break;
              }
              if (Object.op_Inequality((Object) this.head, (Object) null))
                yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, this.head, shader_type));
              PlayerLoader.SetEquipColor(this.renderersHead, info.headColor);
              PlayerLoader.ApplyEquipHighResoTexs(lo_hr_hed_tex, this.renderersHead);
            }
          }
          if (lo_arm != null)
          {
            if (!div_frame_realizes)
            {
              this.arm = this.AddSkin(lo_arm);
              if (Object.op_Inequality((Object) this.arm, (Object) null))
              {
                this.renderersArm = ((Component) this.arm).GetComponentsInChildren<Renderer>();
                ModelLoaderBase.SetEnabled(this.renderersArm, false);
              }
            }
            else
            {
              wait = true;
              InstantiateManager.Request((Object) this, lo_arm.loadedObject, (Action<InstantiateManager.InstantiateData>) (data =>
              {
                this.arm = ((GameObject) data.instantiatedObject).transform;
                this.arm = this.AddSkin(this.arm);
                this.renderersArm = ((Component) this.arm).GetComponentsInChildren<Renderer>();
                PlayerLoader.SetRenderersEnabled(this.renderersArm, false);
                wait = false;
              }));
              while (wait)
                yield return (object) null;
              if (this.renderersArm == null)
                yield break;
            }
            if (Object.op_Inequality((Object) this.arm, (Object) null))
            {
              PlayerLoader.SetSkinAndEquipColor(this.renderersArm, skin_color, info.armColor, arm_model_data.GetZBias());
              PlayerLoader.ApplyEquipHighResoTexs(lo_hr_arm_tex, this.renderersArm);
              this.InvisibleBodyTriangles((int) arm_model_data.bodyDraw);
            }
          }
          else
          {
            GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ARM, arm_name);
            if (Object.op_Implicit((Object) playerResourceCache))
            {
              if (!div_frame_realizes)
              {
                this.arm = this.AddSkinFromCache(playerResourceCache);
                if (Object.op_Inequality((Object) this.arm, (Object) null))
                {
                  this.renderersArm = ((Component) this.arm).GetComponentsInChildren<Renderer>();
                  ModelLoaderBase.SetEnabled(this.renderersArm, false);
                }
              }
              else
              {
                wait = true;
                InstantiateManager.Request((Object) this, (Object) playerResourceCache, (Action<InstantiateManager.InstantiateData>) (data =>
                {
                  this.arm = ((GameObject) data.instantiatedObject).transform;
                  this.arm = this.AddSkin(this.arm);
                  this.renderersArm = ((Component) this.arm).GetComponentsInChildren<Renderer>();
                  PlayerLoader.SetRenderersEnabled(this.renderersArm, false);
                  wait = false;
                }));
                while (wait)
                  yield return (object) null;
                if (this.renderersArm == null)
                  yield break;
              }
              if (Object.op_Inequality((Object) this.arm, (Object) null))
              {
                PlayerLoader.SetSkinAndEquipColor(this.renderersArm, skin_color, info.armColor, arm_model_data.GetZBias());
                PlayerLoader.ApplyEquipHighResoTexs(lo_hr_arm_tex, this.renderersArm);
                this.InvisibleBodyTriangles((int) arm_model_data.bodyDraw);
              }
            }
          }
          if (lo_leg != null)
          {
            if (!div_frame_realizes)
            {
              this.leg = this.AddSkin(lo_leg);
              if (Object.op_Inequality((Object) this.leg, (Object) null))
              {
                this.renderersLeg = ((Component) this.leg).GetComponentsInChildren<Renderer>();
                ModelLoaderBase.SetEnabled(this.renderersLeg, false);
              }
            }
            else
            {
              wait = true;
              InstantiateManager.Request((Object) this, lo_leg.loadedObject, (Action<InstantiateManager.InstantiateData>) (data =>
              {
                this.leg = ((GameObject) data.instantiatedObject).transform;
                this.leg = this.AddSkin(this.leg);
                this.renderersLeg = ((Component) this.leg).GetComponentsInChildren<Renderer>();
                PlayerLoader.SetRenderersEnabled(this.renderersLeg, false);
                wait = false;
              }));
              while (wait)
                yield return (object) null;
              if (this.renderersLeg == null)
                yield break;
            }
            if (Object.op_Inequality((Object) this.leg, (Object) null))
            {
              PlayerLoader.SetSkinAndEquipColor(this.renderersLeg, skin_color, info.legColor, leg_model_data.GetZBias());
              PlayerLoader.ApplyEquipHighResoTexs(lo_hr_leg_tex, this.renderersLeg);
            }
          }
          else
          {
            GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_LEG, leg_name);
            if (Object.op_Implicit((Object) playerResourceCache))
            {
              if (!div_frame_realizes)
              {
                this.leg = this.AddSkinFromCache(playerResourceCache);
                if (Object.op_Inequality((Object) this.leg, (Object) null))
                {
                  this.renderersLeg = ((Component) this.leg).GetComponentsInChildren<Renderer>();
                  ModelLoaderBase.SetEnabled(this.renderersLeg, false);
                }
              }
              else
              {
                wait = true;
                InstantiateManager.Request((Object) this, (Object) playerResourceCache, (Action<InstantiateManager.InstantiateData>) (data =>
                {
                  this.leg = ((GameObject) data.instantiatedObject).transform;
                  this.leg = this.AddSkin(this.leg);
                  this.renderersLeg = ((Component) this.leg).GetComponentsInChildren<Renderer>();
                  PlayerLoader.SetRenderersEnabled(this.renderersLeg, false);
                  wait = false;
                }));
                while (wait)
                  yield return (object) null;
                if (this.renderersLeg == null)
                  yield break;
              }
              if (Object.op_Inequality((Object) this.leg, (Object) null))
              {
                PlayerLoader.SetSkinAndEquipColor(this.renderersLeg, skin_color, info.legColor, leg_model_data.GetZBias());
                PlayerLoader.ApplyEquipHighResoTexs(lo_hr_leg_tex, this.renderersLeg);
              }
            }
          }
          bool isSoulArrowOutGameEffect = false;
          if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && info.equipType == 5U && info.weaponSpAttackType == 2U)
            isSoulArrowOutGameEffect = true;
          if (lo_wepn != null)
          {
            Transform weapon = (Transform) null;
            if (!div_frame_realizes)
            {
              weapon = lo_wepn.Realizes();
              if (Object.op_Inequality((Object) weapon, (Object) null))
              {
                this.renderersWep = ((Component) weapon).gameObject.GetComponentsInChildren<Renderer>();
                ModelLoaderBase.SetEnabled(this.renderersWep, false);
              }
            }
            else
            {
              wait = true;
              InstantiateManager.Request((Object) this, lo_wepn.loadedObject, (Action<InstantiateManager.InstantiateData>) (data =>
              {
                weapon = ((GameObject) data.instantiatedObject).transform;
                this.renderersWep = ((Component) weapon).GetComponentsInChildren<Renderer>();
                PlayerLoader.SetRenderersEnabled(this.renderersWep, false);
                wait = false;
              }));
              while (wait)
                yield return (object) null;
              if (this.renderersWep == null)
                yield break;
            }
            if (Object.op_Inequality((Object) weapon, (Object) null))
            {
              if (isSoulArrowOutGameEffect)
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_bow_01_01");
              yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, weapon, shader_type));
            }
            if (this.renderersBody == null)
              yield break;
            if (Object.op_Inequality((Object) weapon, (Object) null))
              this.InitWeaponLinkBuffEffect(player, weapon);
            PlayerLoader.SetWeaponShader(this.renderersWep, info.weaponColor0, info.weaponColor1, info.weaponColor2, info.weaponEffectID, info.weaponEffectParam, info.weaponEffectColor);
            Material mate0 = (Material) null;
            Material mate1 = (Material) null;
            if (this.renderersWep != null)
            {
              int index = 0;
              for (int length = this.renderersWep.Length; index < length; ++index)
              {
                if (((Object) this.renderersWep[index]).name.EndsWith("_L"))
                {
                  mate1 = this.renderersWep[index].material;
                  this.wepL = ((Component) this.renderersWep[index]).transform;
                  if (this.loadInfo.equipType == 0U && this.loadInfo.weaponSpAttackType == 2U)
                    Utility.Attach(this.socketHandL, this.wepL);
                  else
                    Utility.Attach(this.socketWepL, this.wepL);
                }
                else
                {
                  mate0 = this.renderersWep[index].material;
                  this.wepR = ((Component) this.renderersWep[index]).transform;
                  Utility.Attach(this.socketWepR, this.wepR);
                }
              }
            }
            if (Object.op_Inequality((Object) weapon, (Object) null))
              Object.DestroyImmediate((Object) ((Component) weapon).gameObject);
            if (lo_hr_wep_tex != null)
              PlayerLoader.ApplyWeaponHighResoTexs(lo_hr_wep_tex, high_reso_tex_flags, mate0, mate1);
          }
          else
          {
            GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name);
            if (Object.op_Implicit((Object) playerResourceCache))
            {
              Transform weapon = (Transform) null;
              if (!div_frame_realizes)
              {
                weapon = LoadObject.RealizesWithGameObject(playerResourceCache);
                if (Object.op_Inequality((Object) weapon, (Object) null))
                {
                  this.renderersWep = ((Component) weapon).gameObject.GetComponentsInChildren<Renderer>();
                  ModelLoaderBase.SetEnabled(this.renderersWep, false);
                }
              }
              else
              {
                wait = true;
                InstantiateManager.Request((Object) this, (Object) playerResourceCache, (Action<InstantiateManager.InstantiateData>) (data =>
                {
                  weapon = ((GameObject) data.instantiatedObject).transform;
                  this.renderersWep = ((Component) weapon).GetComponentsInChildren<Renderer>();
                  PlayerLoader.SetRenderersEnabled(this.renderersWep, false);
                  wait = false;
                }));
                while (wait)
                  yield return (object) null;
                if (this.renderersWep == null)
                  yield break;
              }
              if (Object.op_Inequality((Object) weapon, (Object) null))
              {
                if (isSoulArrowOutGameEffect)
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_bow_01_01");
                yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, weapon, shader_type));
              }
              if (this.renderersBody == null)
                yield break;
              if (Object.op_Inequality((Object) weapon, (Object) null))
                this.InitWeaponLinkBuffEffect(player, weapon);
              PlayerLoader.SetWeaponShader(this.renderersWep, info.weaponColor0, info.weaponColor1, info.weaponColor2, info.weaponEffectID, info.weaponEffectParam, info.weaponEffectColor);
              Material mate0 = (Material) null;
              Material mate1 = (Material) null;
              if (this.renderersWep != null)
              {
                int index = 0;
                for (int length = this.renderersWep.Length; index < length; ++index)
                {
                  if (((Object) this.renderersWep[index]).name.EndsWith("_L"))
                  {
                    mate1 = this.renderersWep[index].material;
                    this.wepL = ((Component) this.renderersWep[index]).transform;
                    if (this.loadInfo.equipType == 0U && this.loadInfo.weaponSpAttackType == 2U)
                      Utility.Attach(this.socketHandL, this.wepL);
                    else
                      Utility.Attach(this.socketWepL, this.wepL);
                  }
                  else
                  {
                    mate0 = this.renderersWep[index].material;
                    this.wepR = ((Component) this.renderersWep[index]).transform;
                    Utility.Attach(this.socketWepR, this.wepR);
                  }
                }
              }
              if (Object.op_Inequality((Object) weapon, (Object) null))
                Object.DestroyImmediate((Object) ((Component) weapon).gameObject);
              if (lo_hr_wep_tex != null)
                PlayerLoader.ApplyWeaponHighResoTexs(lo_hr_wep_tex, high_reso_tex_flags, mate0, mate1);
            }
          }
          if (Object.op_Inequality((Object) this.animator, (Object) null) && lo_anim != null)
          {
            RuntimeAnimatorController animatorController = lo_anim.loadedObjects[0].obj as RuntimeAnimatorController;
            if (Object.op_Inequality((Object) animatorController, (Object) null))
            {
              this.animator.runtimeAnimatorController = animatorController;
              if (Object.op_Inequality((Object) player, (Object) null))
              {
                ((Component) this.animator).gameObject.AddComponent<StageObjectProxy>().stageObject = (StageObject) player;
                if (need_anim_event)
                  player.animEventData = lo_anim.loadedObjects[1].obj as AnimEventData;
              }
              this.animator.updateMode = (AnimatorUpdateMode) 1;
              if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.isPlaySpAttackTypeMotion)
              {
                SP_ATTACK_TYPE weaponSpAttackType = (SP_ATTACK_TYPE) info.weaponSpAttackType;
                if (weaponSpAttackType != SP_ATTACK_TYPE.NONE)
                {
                  string str = weaponSpAttackType.ToString();
                  int parameterCount = this.animator.parameterCount;
                  for (int index = 0; index < parameterCount; ++index)
                  {
                    if (this.animator.GetParameter(index).name == str)
                    {
                      this.animator.SetTrigger(str);
                      if (MonoBehaviourSingleton<EffectManager>.IsValid() & isSoulArrowOutGameEffect)
                      {
                        EffectManager.GetEffect("ef_btl_wsk2_bow_01_01", this.socketWepR);
                        break;
                      }
                      break;
                    }
                  }
                }
              }
            }
          }
          if (lo_voices != null)
          {
            this.voiceAudioClips = lo_voices.loadedObjects;
            this.UpdateVoiceAudioClipIds();
          }
          if (!lo_accessories.IsNullOrEmpty<LoadObject>())
          {
            List<Renderer> accRendererList = new List<Renderer>();
            skill_len = 0;
            for (i = lo_accessories.Count; skill_len < i; ++skill_len)
            {
              LoadObject loadObject10 = lo_accessories[skill_len];
              Transform accTrans = (Transform) null;
              if (!div_frame_realizes)
              {
                accTrans = loadObject10.Realizes();
              }
              else
              {
                wait = true;
                InstantiateManager.Request((Object) this, loadObject10.loadedObject, (Action<InstantiateManager.InstantiateData>) (data =>
                {
                  accTrans = ((GameObject) data.instantiatedObject).transform;
                  wait = false;
                }));
                while (wait)
                  yield return (object) null;
              }
              if (Object.op_Inequality((Object) accTrans, (Object) null))
              {
                AccessoryTable.AccessoryInfoData infoData = Singleton<AccessoryTable>.I.GetInfoData(info.accUIDs[skill_len]);
                accTrans.SetParent(this.GetNodeTrans(infoData.node));
                accTrans.localPosition = infoData.offset;
                accTrans.localRotation = infoData.rotation;
                accTrans.localScale = infoData.scale;
                this.accessory.Add(accTrans);
                accRendererList.AddRange((IEnumerable<Renderer>) ((Component) accTrans).GetComponentsInChildren<Renderer>());
              }
            }
            if (!this.accessory.IsNullOrEmpty<Transform>())
            {
              i = 0;
              for (skill_len = this.accessory.Count; i < skill_len; ++i)
              {
                Transform equipItemRoot = this.accessory[i];
                yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, equipItemRoot, shader_type));
              }
            }
            this.renderersAccessory = accRendererList.ToArray();
            ModelLoaderBase.SetEnabled(this.renderersAccessory, false);
            accRendererList = (List<Renderer>) null;
          }
          switch (shader_type)
          {
            case SHADER_TYPE.LIGHTWEIGHT:
              ShaderGlobal.ChangeWantLightweightShader(this.renderersWep);
              ShaderGlobal.ChangeWantLightweightShader(this.renderersFace);
              ShaderGlobal.ChangeWantLightweightShader(this.renderersHair);
              ShaderGlobal.ChangeWantLightweightShader(this.renderersBody);
              ShaderGlobal.ChangeWantLightweightShader(this.renderersHead);
              ShaderGlobal.ChangeWantLightweightShader(this.renderersArm);
              ShaderGlobal.ChangeWantLightweightShader(this.renderersLeg);
              ShaderGlobal.ChangeWantLightweightShader(this.renderersAccessory);
              break;
            case SHADER_TYPE.UI:
              ShaderGlobal.ChangeWantUIShader(this.renderersWep);
              ShaderGlobal.ChangeWantUIShader(this.renderersFace);
              ShaderGlobal.ChangeWantUIShader(this.renderersHair);
              ShaderGlobal.ChangeWantUIShader(this.renderersBody);
              ShaderGlobal.ChangeWantUIShader(this.renderersHead);
              ShaderGlobal.ChangeWantUIShader(this.renderersArm);
              ShaderGlobal.ChangeWantUIShader(this.renderersLeg);
              ShaderGlobal.ChangeWantUIShader(this.renderersAccessory);
              break;
          }
          this.SetLightProbes(enable_light_probes);
          if (layer != -1)
            PlayerLoader.SetLayerWithChildren_SecondaryNoChange(_this, layer);
          PlayerLoader.SetRenderersEnabled(this.renderersWep, true);
          PlayerLoader.SetRenderersEnabled(this.renderersFace, true);
          PlayerLoader.SetRenderersEnabled(this.renderersHair, true);
          PlayerLoader.SetRenderersEnabled(this.renderersBody, true);
          PlayerLoader.SetRenderersEnabled(this.renderersHead, true);
          PlayerLoader.SetRenderersEnabled(this.renderersArm, true);
          PlayerLoader.SetRenderersEnabled(this.renderersLeg, true);
          PlayerLoader.SetRenderersEnabled(this.renderersAccessory, true);
          if (need_shadow && Object.op_Equality((Object) this.shadow, (Object) null))
            this.shadow = PlayerLoader.CreateShadow(_this, is_lightweight: shader_type == SHADER_TYPE.LIGHTWEIGHT);
          if (Object.op_Inequality((Object) player, (Object) null))
          {
            if (Object.op_Inequality((Object) player.controller, (Object) null))
              ((Behaviour) player.controller).enabled = true;
            player.OnLoadComplete();
            if (Object.op_Inequality((Object) player.packetReceiver, (Object) null))
              player.packetReceiver.SetStopPacketUpdate(false);
          }
          if (Object.op_Inequality((Object) player, (Object) null) & is_self && MonoBehaviourSingleton<AudioListenerManager>.IsValid())
            MonoBehaviourSingleton<AudioListenerManager>.I.SetTargetObject((StageObject) player);
          if (callback != null)
            callback((object) player);
          this.ResetDynamicBones(this.dynamicBones);
          this.ResetDynamicBones(this.dynamicBones_Body);
          if (is_self)
          {
            ResourceLoad component = ((Component) player).gameObject.GetComponent<ResourceLoad>();
            if (Object.op_Inequality((Object) component, (Object) null) && component.list != null)
            {
              List<string> stringList = new List<string>();
              int index = 0;
              for (int size = component.list.size; index < size; ++index)
                stringList.Add(component.list.buffer[index].name);
              stringList.Distinct<string>();
              MonoBehaviourSingleton<ResourceManager>.I.cache.AddIgnoreCategorySpecifiedReleaseList(stringList);
            }
          }
          this.isLoading = false;
        }
      }
    }
  }

  protected virtual IEnumerator DoLoad_GG_Optimize(
    PlayerLoadInfo info,
    int layer,
    int anim_id,
    bool need_anim_event,
    bool need_foot_stamp,
    bool need_shadow,
    bool enable_light_probes,
    bool need_action_voice,
    bool need_high_reso_tex,
    bool need_res_ref_count,
    bool need_dev_frame_instantiate,
    SHADER_TYPE shader_type,
    PlayerLoader.OnCompleteLoad callback,
    bool enable_eye_blick,
    int use_hair_overlay)
  {
    bool gg_op = true;
    if (info == null)
      Log.Error(LOG.RESOURCE, "PlayerLoader:info=null");
    PlayerLoader.SerializePlayerLoadInfo(info);
    this.animObjectTable = new StringKeyTable<LoadObject>();
    Player player = ((Component) this).gameObject.GetComponent<Player>();
    bool enableBone = this._EnableDynamicBone(shader_type, player is Self);
    EquipModelTable.Data data1 = Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.ARMOR, info.bodyModelID);
    EquipModelTable.Data data2 = data1.needHelm ? Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.HELM, info.headModelID) : (EquipModelTable.Data) null;
    EquipModelTable.Data arm_model_data = data1.needArm ? Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.ARM, info.armModelID) : (EquipModelTable.Data) null;
    EquipModelTable.Data leg_model_data = data1.needLeg ? Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.LEG, info.legModelID) : (EquipModelTable.Data) null;
    int id1 = data2 != null ? data2.GetHairModelID(info.hairModelID) : data1.GetHairModelID(info.hairModelID);
    if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 0)
    {
      need_high_reso_tex = false;
      use_hair_overlay = -1;
    }
    int high_reso_tex_flags = 0;
    if (need_high_reso_tex && MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
      high_reso_tex_flags = (int) MonoBehaviourSingleton<GlobalSettingsManager>.I.equipModelHQTable.GetWeaponFlag(info.weaponModelID);
    bool is_self = false;
    if (Object.op_Inequality((Object) player, (Object) null))
    {
      int id2 = player.id;
      is_self = player is Self;
    }
    this.DeleteLoadedObjects();
    this.loadInfo = info;
    if (anim_id < 0)
      anim_id = anim_id != -1 || info.weaponModelID == -1 ? -anim_id + info.weaponModelID / 1000 : info.weaponModelID / 1000;
    bool flag = data2 != null ? data2.needFace : data1.needFace;
    int num1 = data2 != null ? data2.hairMode : data1.hairMode;
    string face_name = info.faceModelID > -1 & flag ? ResourceName.GetPlayerFace(info.faceModelID) : (string) null;
    string hair_name = id1 <= -1 || num1 == 0 ? (string) null : ResourceName.GetPlayerHead(id1);
    string body_name = info.bodyModelID > -1 ? ResourceName.GetPlayerBody(info.bodyModelID) : (string) null;
    string head_name = info.headModelID <= -1 || data2 == null ? (string) null : ResourceName.GetPlayerHead(info.headModelID);
    string arm_name = info.armModelID <= -1 || arm_model_data == null ? (string) null : ResourceName.GetPlayerArm(info.armModelID);
    string leg_name = info.legModelID <= -1 || leg_model_data == null ? (string) null : ResourceName.GetPlayerLeg(info.legModelID);
    string wepn_name = info.weaponModelID > -1 ? ResourceName.GetPlayerWeapon(info.weaponModelID) : (string) null;
    if (body_name != null)
    {
      Transform _this = ((Component) this).transform;
      if (Object.op_Inequality((Object) player, (Object) null))
      {
        if (Object.op_Inequality((Object) player.controller, (Object) null))
          ((Behaviour) player.controller).enabled = false;
        if (Object.op_Inequality((Object) player.packetReceiver, (Object) null))
          player.packetReceiver.SetStopPacketUpdate(true);
        player.OnLoadStart();
      }
      this.isLoading = true;
      LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this, need_res_ref_count);
      LoadObject lo_face = (LoadObject) null;
      LoadObject lo_hair = (LoadObject) null;
      LoadObject lo_body = (LoadObject) null;
      LoadObject lo_head = (LoadObject) null;
      LoadObject lo_arm = (LoadObject) null;
      LoadObject lo_leg = (LoadObject) null;
      LoadObject lo_wepn = (LoadObject) null;
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_FACE, face_name))
        lo_face = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_FACE, face_name);
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name))
        lo_hair = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_HEAD, hair_name);
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_BDY, body_name))
        lo_body = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_BDY, body_name);
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_HEAD, head_name))
        lo_head = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_HEAD, head_name);
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ARM, arm_name))
        lo_arm = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_ARM, arm_name);
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_LEG, leg_name))
        lo_leg = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_LEG, leg_name);
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name))
        lo_wepn = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name);
      List<LoadObject> lo_accessories = new List<LoadObject>();
      if (!info.accUIDs.IsNullOrEmpty<uint>())
      {
        int index = 0;
        for (int count = info.accUIDs.Count; index < count; ++index)
        {
          string playerAccessory = ResourceName.GetPlayerAccessory(Singleton<AccessoryTable>.I.GetInfoData(info.accUIDs[index]).accessoryId);
          lo_accessories.Add(need_dev_frame_instantiate ? (LoadObject) load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_ACCESSORY, playerAccessory) : load_queue.Load(RESOURCE_CATEGORY.PLAYER_ACCESSORY, playerAccessory));
        }
      }
      LoadObject lo_voices = (LoadObject) null;
      LoadObject lo_hr_wep_tex = (LoadObject) null;
      LoadObject lo_hr_hed_tex = (LoadObject) null;
      LoadObject lo_hr_bdy_tex = (LoadObject) null;
      LoadObject lo_hr_arm_tex = (LoadObject) null;
      LoadObject lo_hr_leg_tex = (LoadObject) null;
      string anim_name = anim_id > -1 ? ResourceName.GetPlayerAnim(anim_id) : (string) null;
      if (load_queue.IsLoading())
        yield return (object) load_queue.Wait();
      if (info.isNeedToCache)
      {
        if (lo_face != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_FACE, face_name, lo_face.loadedObject);
        if (lo_hair != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name, lo_hair.loadedObject);
        if (lo_body != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_BDY, body_name, lo_body.loadedObject);
        if (lo_head != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_HEAD, head_name, lo_head.loadedObject);
        if (lo_arm != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_ARM, arm_name, lo_arm.loadedObject);
        if (lo_leg != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_LEG, leg_name, lo_leg.loadedObject);
        if (lo_wepn != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name, lo_wepn.loadedObject);
      }
      LoadObject loadObject1;
      if (anim_name == null)
      {
        loadObject1 = (LoadObject) null;
      }
      else
      {
        LoadingQueue loadingQueue = load_queue;
        string package_name = anim_name;
        string[] resource_names;
        if (!need_anim_event)
          resource_names = new string[1]
          {
            anim_name + "Ctrl"
          };
        else
          resource_names = new string[2]
          {
            anim_name + "Ctrl",
            anim_name + "Event"
          };
        loadObject1 = loadingQueue.Load(RESOURCE_CATEGORY.PLAYER_ANIM, package_name, resource_names);
      }
      LoadObject lo_anim = loadObject1;
      if (lo_anim != null)
        this.animObjectTable.Add("BASE", lo_anim);
      if (Object.op_Inequality((Object) player, (Object) null) && anim_id > -1)
      {
        List<string> stringList = new List<string>();
        int num2 = 3;
        for (int index1 = 0; index1 < num2; ++index1)
        {
          for (int index2 = 0; index2 < 2; ++index2)
          {
            SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + index1);
            if (skillParam != null)
            {
              string fromAnimFormatName = Character.GetCtrlNameFromAnimFormatName(index2 == 0 ? skillParam.tableData.castStateName : skillParam.tableData.actStateName);
              if (!string.IsNullOrEmpty(fromAnimFormatName) && stringList.IndexOf(fromAnimFormatName) < 0)
              {
                stringList.Add(fromAnimFormatName);
                string playerSubAnim = ResourceName.GetPlayerSubAnim(anim_id, fromAnimFormatName);
                LoadObject loadObject2;
                if (playerSubAnim == null)
                {
                  loadObject2 = (LoadObject) null;
                }
                else
                {
                  LoadingQueue loadingQueue = load_queue;
                  string package_name = playerSubAnim;
                  string[] resource_names;
                  if (!need_anim_event)
                    resource_names = new string[1]
                    {
                      playerSubAnim + "Ctrl"
                    };
                  else
                    resource_names = new string[2]
                    {
                      playerSubAnim + "Ctrl",
                      playerSubAnim + "Event"
                    };
                  loadObject2 = loadingQueue.Load(RESOURCE_CATEGORY.PLAYER_ANIM_SKILL, package_name, resource_names);
                }
                LoadObject loadObject3 = loadObject2;
                if (loadObject3 != null)
                  this.animObjectTable.Add(fromAnimFormatName, loadObject3);
              }
            }
          }
        }
      }
      if (Object.op_Inequality((Object) player, (Object) null) && info.weaponEvolveId > 0U)
      {
        string ctrlName;
        string animName;
        ResourceName.GetPlayerEvolveAnim(info.weaponEvolveId, out ctrlName, out animName);
        LoadObject loadObject4 = load_queue.Load(RESOURCE_CATEGORY.PLAYER_ANIM_EVOLVE, animName, new string[2]
        {
          animName + "Ctrl",
          animName + "Event"
        });
        if (loadObject4 != null)
          this.animObjectTable.Add(ctrlName, loadObject4);
      }
      if (need_action_voice && info.actionVoiceBaseID > -1)
      {
        int[] values = (int[]) Enum.GetValues(typeof (ACTION_VOICE_ID));
        int length = values.Length;
        string[] resource_names = new string[length];
        for (int index = 0; index < length; ++index)
          resource_names[index] = ResourceName.GetActionVoiceName(info.actionVoiceBaseID + values[index]);
        lo_voices = load_queue.Load(RESOURCE_CATEGORY.SOUND_VOICE, ResourceName.GetActionVoicePackageNameFromVoiceID(info.actionVoiceBaseID), resource_names);
      }
      if (need_high_reso_tex)
      {
        if (high_reso_tex_flags != 0 && wepn_name != null)
          lo_hr_wep_tex = PlayerLoader.LoadHighResoTexs(load_queue, wepn_name, high_reso_tex_flags);
        if (head_name != null)
          lo_hr_hed_tex = PlayerLoader.LoadHighResoTexs(load_queue, head_name, 1);
        if (body_name != null)
          lo_hr_bdy_tex = PlayerLoader.LoadHighResoTexs(load_queue, body_name, 1);
        if (arm_name != null)
          lo_hr_arm_tex = PlayerLoader.LoadHighResoTexs(load_queue, arm_name, 1);
        if (leg_name != null)
          lo_hr_leg_tex = PlayerLoader.LoadHighResoTexs(load_queue, leg_name, 1);
      }
      LoadObject loHairOverlay = (LoadObject) null;
      if (use_hair_overlay != -1)
        loHairOverlay = PlayerLoader.LoadHairOverlayTexs(load_queue, info, use_hair_overlay);
      yield return (object) load_queue.Wait();
      List<string> needAtkInfoNames = new List<string>();
      StringKeyTable<LoadObject> animEventBulletLoadObjTable = new StringKeyTable<LoadObject>();
      int skill_len;
      int i;
      if (Object.op_Inequality((Object) player, (Object) null))
      {
        if (need_anim_event)
          this.animObjectTable.ForEach((Action<LoadObject>) (load_object =>
          {
            if (!gg_op)
              load_queue.CacheAnimDataUseResource(load_object.loadedObjects[1].obj as AnimEventData, (LoadingQueue.EffectNameAnalyzer) (effect_name => effect_name[0] != '@' ? effect_name : (string) null));
            load_queue.CacheAnimDataUseResourceDependPlayer(player, load_object.loadedObjects[1].obj as AnimEventData);
            AnimEventData animEventData = load_object.loadedObjects[1].obj as AnimEventData;
            if (!Object.op_Inequality((Object) animEventData, (Object) null) || ((IList<AnimEventData.AnimData>) animEventData.animations).IsNullOrEmpty<AnimEventData.AnimData>())
              return;
            foreach (AnimEventData.AnimData animation in animEventData.animations)
            {
              foreach (AnimEventData.EventData eventData in animation.events)
              {
                AnimEventFormat.ID id3 = eventData.id;
                if (id3 <= AnimEventFormat.ID.SHOT_ZONE)
                {
                  if (id3 != AnimEventFormat.ID.SHOT_PRESENT)
                  {
                    if (id3 != AnimEventFormat.ID.SHOT_ZONE)
                      goto label_25;
                  }
                  else
                  {
                    int index3 = 0;
                    for (int length1 = eventData.stringArgs.Length; index3 < length1; ++index3)
                    {
                      string[] self = eventData.stringArgs[index3].Split(':');
                      if (!((IList<string>) self).IsNullOrEmpty<string>())
                      {
                        int index4 = 0;
                        for (int length2 = self.Length; index4 < length2; ++index4)
                        {
                          string str = self[index4];
                          if (!string.IsNullOrEmpty(str))
                          {
                            LoadObject loadObject5 = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, str);
                            if (loadObject5 != null && animEventBulletLoadObjTable.Get(str) == null)
                              animEventBulletLoadObjTable.Add(str, loadObject5);
                          }
                        }
                      }
                    }
                    goto label_25;
                  }
                }
                else if (id3 != AnimEventFormat.ID.SHOT_DECOY && id3 != AnimEventFormat.ID.LOAD_BULLET)
                  goto label_25;
                for (int index = 0; index < eventData.stringArgs.Length; ++index)
                {
                  string stringArg = eventData.stringArgs[index];
                  if (!string.IsNullOrEmpty(stringArg))
                  {
                    LoadObject loadObject6 = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, stringArg);
                    if (loadObject6 != null && animEventBulletLoadObjTable.Get(stringArg) == null)
                      animEventBulletLoadObjTable.Add(stringArg, loadObject6);
                  }
                }
label_25:
                if (id3 <= AnimEventFormat.ID.SHOT_NODE_LINK)
                {
                  if (id3 <= AnimEventFormat.ID.NWAY_LASER_ATTACK)
                  {
                    if (id3 != AnimEventFormat.ID.SHOT_ARROW && (uint) (id3 - 70) > 5U && id3 != AnimEventFormat.ID.NWAY_LASER_ATTACK)
                      continue;
                  }
                  else if (id3 <= AnimEventFormat.ID.GENERATE_TRACKING)
                  {
                    if (id3 != AnimEventFormat.ID.EXATK_COLLIDER_START && id3 != AnimEventFormat.ID.GENERATE_TRACKING)
                      continue;
                  }
                  else if (id3 != AnimEventFormat.ID.PLAYER_FUNNEL_ATTACK && id3 != AnimEventFormat.ID.SHOT_NODE_LINK)
                    continue;
                }
                else if (id3 <= AnimEventFormat.ID.SHOT_HEALING_HOMING)
                {
                  if (id3 != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE && (uint) (id3 - 199) > 1U && id3 != AnimEventFormat.ID.SHOT_HEALING_HOMING)
                    continue;
                }
                else if (id3 <= AnimEventFormat.ID.SHOT_RESURRECTION_HOMING)
                {
                  if (id3 != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE_MULTI && id3 != AnimEventFormat.ID.SHOT_RESURRECTION_HOMING)
                    continue;
                }
                else if (id3 != AnimEventFormat.ID.BUFF_START_SHIELD_REFLECT && id3 != AnimEventFormat.ID.SHOT_ORACLE_SPEAR_SP)
                  continue;
                string stringArg1 = eventData.stringArgs[0];
                if (!string.IsNullOrEmpty(stringArg1))
                {
                  foreach (string str in ResourceName.GetNamesNeededLoadAtkInfoFromAnimEvent())
                  {
                    if (stringArg1.StartsWith(str))
                    {
                      needAtkInfoNames.Add(stringArg1);
                      break;
                    }
                  }
                }
              }
            }
          }));
        this.AddSkillAttackInfoName(player, ref needAtkInfoNames);
        this.AddFieldGimmickAttackInfoName(ref needAtkInfoNames);
        List<LoadObject> atkInfo = new List<LoadObject>();
        List<LoadObject> atkInfoLoaded = new List<LoadObject>();
        Dictionary<string, LoadObject> atkInfoDict = new Dictionary<string, LoadObject>();
        if (this.playerLoaderLoadedAttackInfoNames == null)
          this.playerLoaderLoadedAttackInfoNames = new HashSet<string>();
        for (int index = 0; index < needAtkInfoNames.Count; ++index)
        {
          if (!this.playerLoaderLoadedAttackInfoNames.Contains(needAtkInfoNames[index]))
          {
            if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[index]))
            {
              LoadObject loadObject7 = load_queue.Load(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[index]);
              atkInfo.Add(loadObject7);
              atkInfoLoaded.Add(loadObject7);
              atkInfoDict.Add(needAtkInfoNames[index], loadObject7);
            }
            else
            {
              Object playerResourceCache = MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[index]);
              LoadObject loadObject8 = new LoadObject();
              loadObject8.loadedObject = playerResourceCache;
              atkInfoLoaded.Add(loadObject8);
              atkInfoDict.Add(needAtkInfoNames[index], loadObject8);
            }
            this.playerLoaderLoadedAttackInfoNames.Add(needAtkInfoNames[index]);
          }
        }
        if (load_queue.IsLoading())
          yield return (object) load_queue.Wait();
        if (info.isNeedToCache)
        {
          foreach (KeyValuePair<string, LoadObject> keyValuePair in atkInfoDict)
          {
            string key = keyValuePair.Key;
            LoadObject loadObject9 = keyValuePair.Value;
            if (atkInfo.Contains(loadObject9))
              MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, key, loadObject9.loadedObject);
          }
        }
        if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
        {
          Transform _settingTransform = MonoBehaviourSingleton<InGameSettingsManager>.I._transform;
          List<AttackInfo> hitInfos = new List<AttackInfo>();
          for (i = 0; i < atkInfoLoaded.Count; ++i)
          {
            SplitPlayerAttackInfo attackInfo = ((Component) LoadObject.RealizesWithGameObject((GameObject) atkInfoLoaded[i].loadedObject, _settingTransform)).gameObject.GetComponent<SplitPlayerAttackInfo>();
            hitInfos.Add((AttackInfo) attackInfo.attackHitInfo);
            hitInfos.Add((AttackInfo) attackInfo.attackContinuationInfo);
            if (!string.IsNullOrEmpty(attackInfo.attackHitInfo.nextBulletInfoName))
              yield return (object) this.StartCoroutine(this.LoadNextBulletInfo(load_queue, attackInfo.attackHitInfo.nextBulletInfoName, hitInfos, info.isNeedToCache));
            if (!string.IsNullOrEmpty(attackInfo.attackContinuationInfo.nextBulletInfoName))
              yield return (object) this.StartCoroutine(this.LoadNextBulletInfo(load_queue, attackInfo.attackContinuationInfo.nextBulletInfoName, hitInfos, info.isNeedToCache));
            attackInfo = (SplitPlayerAttackInfo) null;
          }
          InGameSettingsManager.Player player1 = MonoBehaviourSingleton<InGameSettingsManager>.I.player;
          if (player1.attackInfosAll == null)
            player1.attackInfosAll = new AttackInfo[0];
          AttackInfo[] mergedArray = Utility.CreateMergedArray<AttackInfo>(player1.attackInfosAll, hitInfos.ToArray());
          player1.attackInfosAll = Utility.DistinctArray<AttackInfo>(mergedArray);
          player.AddAttackInfos(hitInfos.ToArray());
          _settingTransform = (Transform) null;
          hitInfos = (List<AttackInfo>) null;
        }
        this.LoadAttackInfoResource(player, load_queue);
        if (load_queue.IsLoading())
          yield return (object) load_queue.Wait();
        ELEMENT_TYPE nowWeaponElement = player.GetNowWeaponElement();
        switch (anim_id)
        {
          case 0:
            load_queue.CacheSE(10000042);
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_sword_01_01");
            switch (player.spAttackType)
            {
              case SP_ATTACK_TYPE.HEAT:
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl05_attack_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_sword_01_04");
                break;
              case SP_ATTACK_TYPE.SOUL:
                if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
                {
                  InGameSettingsManager.Player.OneHandSwordActionInfo ohsActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo;
                  load_queue.CacheSE(ohsActionInfo.Soul_BoostSeId);
                  load_queue.CacheSE(ohsActionInfo.Soul_SnatchHitSeId);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsActionInfo.Soul_SnatchHitEffect);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsActionInfo.Soul_SnatchHitRemainEffect);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsActionInfo.Soul_SnatchHitEffectOnBoostMode);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_btl_soul_energy_01");
                  if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) ohsActionInfo.Soul_BoostElementHitEffect.Length)
                  {
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsActionInfo.Soul_BoostElementHitEffect[(int) nowWeaponElement]);
                    break;
                  }
                  break;
                }
                break;
              case SP_ATTACK_TYPE.BURST:
                InGameSettingsManager.Player.BurstOneHandSwordActionInfo burstOhsInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.burstOHSInfo;
                if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) burstOhsInfo.BoostElementHitEffect.Length)
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstOhsInfo.BoostElementHitEffect[(int) nowWeaponElement]);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
                break;
              case SP_ATTACK_TYPE.ORACLE:
                InGameSettingsManager.Player.OracleOneHandSwordActionInfo oracleOhsInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.oracleOHSInfo;
                for (int index = 0; index < oracleOhsInfo.dragonEffects.Length; ++index)
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleOhsInfo.dragonEffects[index].GetEffectName(nowWeaponElement));
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_sword_dragon_veil");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_sword_dragon_veil_re");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, $"ef_btl_wsk4_sword_02_{(int) nowWeaponElement:D2}");
                break;
            }
            break;
          case 1:
            if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
            {
              InGameSettingsManager.Player.TwoHandSwordActionInfo handSwordActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo;
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, handSwordActionInfo.nameChargeExpandEffect);
              if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
              {
                load_queue.CacheSE(handSwordActionInfo.soulBoostSeId);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, handSwordActionInfo.soulIaiChargeMaxEffect);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_btl_soul_energy_01");
              }
              if (player.spAttackType == SP_ATTACK_TYPE.BURST)
              {
                InGameSettingsManager.Player.BurstTwoHandSwordActionInfo burstThsInfo = handSwordActionInfo.burstTHSInfo;
                if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) burstThsInfo.HitEffect_SingleShot.Length)
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstThsInfo.HitEffect_SingleShot[(int) nowWeaponElement]);
                if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) burstThsInfo.HitEffect_FullBurst.Length)
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstThsInfo.HitEffect_FullBurst[(int) nowWeaponElement]);
              }
              if (player.spAttackType == SP_ATTACK_TYPE.ORACLE)
              {
                InGameSettingsManager.Player.OracleTwoHandSwordActionInfo oracleThsInfo = handSwordActionInfo.oracleTHSInfo;
                load_queue.CacheSE(oracleThsInfo.normalVernierSeId);
                load_queue.CacheSE(oracleThsInfo.maxVernierSeId);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleThsInfo.normalVernierEffectName);
                if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) oracleThsInfo.maxVernierEffectNames.Length)
                {
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleThsInfo.maxVernierEffectNames[(int) nowWeaponElement]);
                  break;
                }
                break;
              }
              break;
            }
            break;
          case 2:
            switch (player.spAttackType)
            {
              case SP_ATTACK_TYPE.NONE:
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_loop_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_loop_02");
                break;
              case SP_ATTACK_TYPE.HEAT:
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_e_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_spear_01_03");
                break;
              case SP_ATTACK_TYPE.SOUL:
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_btl_soul_energy_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_spear_02_02");
                break;
              case SP_ATTACK_TYPE.BURST:
                if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
                {
                  InGameSettingsManager.Player.BurstSpearActionInfo burstSpearInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.burstSpearInfo;
                  if (burstSpearInfo.spinSeId > 0)
                    load_queue.CacheSE(burstSpearInfo.spinSeId);
                  if (burstSpearInfo.spinMaxSpeedSeId > 0)
                    load_queue.CacheSE(burstSpearInfo.spinMaxSpeedSeId);
                  string name1 = string.Empty;
                  if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) burstSpearInfo.spinEffectNames.Length)
                    name1 = burstSpearInfo.spinEffectNames[(int) nowWeaponElement];
                  string name2 = string.Empty;
                  if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) burstSpearInfo.spinElementHitEffectNames.Length)
                    name2 = burstSpearInfo.spinElementHitEffectNames[(int) nowWeaponElement];
                  string name3 = string.Empty;
                  if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) burstSpearInfo.spinThrowGroundEffectNames.Length)
                    name3 = burstSpearInfo.spinThrowGroundEffectNames[(int) nowWeaponElement];
                  if (!string.IsNullOrEmpty(name1))
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name1);
                  if (!string.IsNullOrEmpty(name2))
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name2);
                  if (!string.IsNullOrEmpty(name3))
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name3);
                  if (!string.IsNullOrEmpty(burstSpearInfo.throwGroundEffectName))
                  {
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstSpearInfo.throwGroundEffectName);
                    break;
                  }
                  break;
                }
                break;
              case SP_ATTACK_TYPE.ORACLE:
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_spear_aura");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_spear_guard");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_spear_stock");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_sword_01_01");
                load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.oracle.gutsSE);
                load_queue.CacheSE(10000042);
                break;
            }
            if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid() && player.spAttackType != SP_ATTACK_TYPE.BURST)
            {
              InGameSettingsManager.Player.SpearActionInfo spearActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo;
              string name = spearActionInfo.jumpHugeHitEffectName;
              if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) spearActionInfo.jumpHugeElementHitEffectNames.Length)
                name = spearActionInfo.jumpHugeElementHitEffectNames[(int) nowWeaponElement];
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name);
              break;
            }
            break;
          case 4:
            if (player.spAttackType == SP_ATTACK_TYPE.NONE)
            {
              if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
                load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.wildDanceChargeMaxSeId);
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
              break;
            }
            if (player.spAttackType == SP_ATTACK_TYPE.HEAT)
            {
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_02");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_03");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_04");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_05");
              break;
            }
            if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
            {
              if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
              {
                InGameSettingsManager.Player.PairSwordsActionInfo swordsActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo;
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, swordsActionInfo.Soul_EffectForWaitingLaser);
                string soulEffectForBullet = swordsActionInfo.Soul_EffectForBullet;
                if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) swordsActionInfo.Soul_EffectsForBullet.Length)
                  soulEffectForBullet = swordsActionInfo.Soul_EffectsForBullet[(int) nowWeaponElement];
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, soulEffectForBullet);
                if (!((IList<int>) swordsActionInfo.Soul_SeIds).IsNullOrEmpty<int>())
                {
                  for (int index = 0; index < swordsActionInfo.Soul_SeIds.Length; ++index)
                  {
                    if (swordsActionInfo.Soul_SeIds[index] >= 0)
                      load_queue.CacheSE(swordsActionInfo.Soul_SeIds[index]);
                  }
                  break;
                }
                break;
              }
              break;
            }
            if (player.spAttackType == SP_ATTACK_TYPE.BURST)
            {
              load_queue.CacheSE(10000051);
              load_queue.CacheSE(10000042);
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_twinsword_01_00");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
              InGameSettingsManager.Player.PairSwordsActionInfo swordsActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo;
              if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) swordsActionInfo.Burst_CombineHitEffect.Length)
              {
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, swordsActionInfo.Burst_CombineHitEffect[(int) nowWeaponElement]);
                break;
              }
              break;
            }
            if (player.spAttackType == SP_ATTACK_TYPE.ORACLE)
            {
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_twinsword_01");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, $"ef_btl_wsk4_twinsword_01_{(int) nowWeaponElement:D2}");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_twinsword_03");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_twinsword_04");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, $"ef_btl_wsk4_twinsword_05_{(int) nowWeaponElement:D2}");
              break;
            }
            break;
          case 5:
            if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
            {
              InGameSettingsManager.Player.SpecialActionInfo specialActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo;
              InGameSettingsManager.TargetMarkerSettings targetMarkerSettings = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerSettings;
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowChargeAimEffectName);
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowAimLesserCursorEffectName);
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.bestDistanceEffect);
              if (player.spAttackType == SP_ATTACK_TYPE.NONE)
              {
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[6]);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[5]);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowBleedEffectName);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowBleedDamageEffectName);
                switch (nowWeaponElement)
                {
                  case ELEMENT_TYPE.FIRE:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowFireBurstEffectName);
                    break;
                  case ELEMENT_TYPE.WATER:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowWaterBurstEffectName);
                    break;
                  case ELEMENT_TYPE.THUNDER:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowThunderBurstEffectName);
                    break;
                  case ELEMENT_TYPE.SOIL:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowSoilBurstEffectName);
                    break;
                  case ELEMENT_TYPE.LIGHT:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowLightrBurstEffectName);
                    break;
                  case ELEMENT_TYPE.DARK:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowDarkBurstEffectName);
                    break;
                  default:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowBurstEffectName);
                    break;
                }
              }
              else
              {
                if (player.spAttackType == SP_ATTACK_TYPE.HEAT)
                {
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[22]);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[21]);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_bow_01_02");
                  break;
                }
                if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
                {
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[24]);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_bow_lock_02");
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
                  load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulLockMaxSeId);
                  load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulLockSeId);
                  load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.soulBoostSeId);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
                  break;
                }
                if (player.spAttackType == SP_ATTACK_TYPE.BURST)
                {
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[27]);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[26]);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.GetBombArrowEffectName(nowWeaponElement));
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.arrowRainShotAimLesserCursorEffectName);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.GetBombEffectName(nowWeaponElement));
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.boostArrowChargeMaxEffectName);
                  load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.burstBoostModeSEId);
                  List<int> bombArrowSeIdList = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombArrowSEIdList;
                  for (int index = 0; index < bombArrowSeIdList.Count; ++index)
                    load_queue.CacheSE(bombArrowSeIdList[index]);
                  List<int> bombSeIdList = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombSEIdList;
                  for (int index = 0; index < bombSeIdList.Count; ++index)
                    load_queue.CacheSE(bombSeIdList[index]);
                  break;
                }
                break;
              }
            }
            else
              break;
            break;
        }
        EvolveController.Load(load_queue, info.weaponEvolveId);
        skill_len = 3;
        LoadObject[] bullet_load = new LoadObject[skill_len];
        for (int index5 = 0; index5 < skill_len; ++index5)
        {
          SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + index5);
          if (skillParam == null)
          {
            bullet_load[index5] = (LoadObject) null;
          }
          else
          {
            SkillItemTable.SkillItemData tableData = skillParam.tableData;
            if (!string.IsNullOrEmpty(tableData.startEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.startEffectName);
            if (tableData.startSEID != 0)
              load_queue.CacheSE(tableData.startSEID);
            if (!string.IsNullOrEmpty(tableData.actLocalEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.actLocalEffectName);
            if (!string.IsNullOrEmpty(tableData.actOneshotEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.actOneshotEffectName);
            if (tableData.actSEID != 0)
              load_queue.CacheSE(tableData.actSEID);
            if (!string.IsNullOrEmpty(tableData.enchantEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.enchantEffectName);
            if (!string.IsNullOrEmpty(tableData.hitEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.hitEffectName);
            if ((double) (float) tableData.skillRange > 0.0 && !string.IsNullOrEmpty(MonoBehaviourSingleton<InGameSettingsManager>.I.player.skillRangeEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.skillRangeEffectName);
            if (tableData.hitSEID != 0)
              load_queue.CacheSE(tableData.hitSEID);
            if (is_self)
              load_queue.CacheItemIcon(tableData.iconID);
            if (tableData.healType == HEAL_TYPE.RESURRECTION_ALL)
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_heal_04_03");
            if (!((IList<int>) tableData.buffTableIds).IsNullOrEmpty<int>())
            {
              for (int index6 = 0; index6 < tableData.buffTableIds.Length; ++index6)
              {
                BuffTable.BuffData data3 = Singleton<BuffTable>.I.GetData((uint) tableData.buffTableIds[index6]);
                if (BuffParam.IsHitAbsorbType(data3.type))
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_drain_01_01");
                else if (data3.type == BuffParam.BUFFTYPE.AUTO_REVIVE)
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_heal_04_03");
              }
            }
            foreach (string name in tableData.supportEffectName)
            {
              if (!string.IsNullOrEmpty(name))
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name);
            }
            foreach (BuffParam.BUFFTYPE bufftype in tableData.supportType)
            {
              switch (bufftype)
              {
                case BuffParam.BUFFTYPE.SKILL_CHARGE_ABOVE:
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_btl_sk_magi_move_01_01");
                  break;
                case BuffParam.BUFFTYPE.SUBSTITUTE:
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_magi_shikigami_01_02");
                  break;
              }
            }
            if (tableData.isTeleportation)
            {
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_warp_02_01");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_warp_02_02");
            }
            bullet_load[index5] = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, tableData.bulletName);
          }
        }
        if (is_self)
          load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_darkness_02");
        EffectPlayProcessor effectPlayProcessor = player.effectPlayProcessor;
        if (Object.op_Inequality((Object) effectPlayProcessor, (Object) null) && effectPlayProcessor.effectSettings != null)
        {
          int index = 0;
          for (int length = effectPlayProcessor.effectSettings.Length; index < length; ++index)
          {
            if (!string.IsNullOrEmpty(effectPlayProcessor.effectSettings[index].effectName))
            {
              string name = effectPlayProcessor.effectSettings[index].name;
              if (name.StartsWith("BUFF_"))
              {
                string str = name.Substring(name.Length - "_PLC00".Length);
                if (str.Contains("_PLC") && str != "_PLC" + (this.loadInfo.weaponModelID / 1000).ToString("D2"))
                  continue;
              }
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor.effectSettings[index].effectName);
            }
          }
        }
        if (load_queue.IsLoading())
          yield return (object) load_queue.Wait();
        for (int index = 0; index < skill_len; ++index)
        {
          SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + index);
          if (skillParam != null)
          {
            skillParam.bullet = bullet_load[index].loadedObject as BulletData;
            load_queue.CacheBulletDataUseResource(skillParam.bullet, player);
          }
        }
        if (animEventBulletLoadObjTable != null)
          animEventBulletLoadObjTable.ForEachKeyAndValue((Action<string, LoadObject>) ((key, item) =>
          {
            if (item == null || !Object.op_Inequality(item.loadedObject, (Object) null))
              return;
            BulletData loadedObject = item.loadedObject as BulletData;
            if (!Object.op_Inequality((Object) loadedObject, (Object) null) || !Object.op_Equality((Object) player.cachedBulletDataTable.Get(key), (Object) null))
              return;
            player.cachedBulletDataTable.Add(key, loadedObject);
            load_queue.CacheBulletDataUseResource(loadedObject, player);
          }));
        animEventBulletLoadObjTable.Clear();
        animEventBulletLoadObjTable = (StringKeyTable<LoadObject>) null;
        if (load_queue.IsLoading())
          yield return (object) load_queue.Wait();
        atkInfo = (List<LoadObject>) null;
        atkInfoLoaded = (List<LoadObject>) null;
        atkInfoDict = (Dictionary<string, LoadObject>) null;
        bullet_load = (LoadObject[]) null;
      }
      if (lo_arm != null)
      {
        if (Object.op_Inequality(lo_arm.loadedObject, (Object) null))
        {
          GameObject loadedObject = lo_arm.loadedObject as GameObject;
          EffectPlayProcessor component = Object.op_Inequality((Object) loadedObject, (Object) null) ? loadedObject.GetComponent<EffectPlayProcessor>() : (EffectPlayProcessor) null;
          if (Object.op_Inequality((Object) component, (Object) null) && component.effectSettings != null)
          {
            int index = 0;
            for (int length = component.effectSettings.Length; index < length; ++index)
            {
              if (!string.IsNullOrEmpty(component.effectSettings[index].effectName))
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, component.effectSettings[index].effectName);
            }
          }
        }
      }
      else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ARM, arm_name))
      {
        GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ARM, arm_name);
        EffectPlayProcessor component = Object.op_Inequality((Object) playerResourceCache, (Object) null) ? playerResourceCache.GetComponent<EffectPlayProcessor>() : (EffectPlayProcessor) null;
        if (Object.op_Inequality((Object) component, (Object) null) && component.effectSettings != null)
        {
          int index = 0;
          for (int length = component.effectSettings.Length; index < length; ++index)
          {
            if (!string.IsNullOrEmpty(component.effectSettings[index].effectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, component.effectSettings[index].effectName);
          }
        }
      }
      if (lo_leg != null)
      {
        if (Object.op_Inequality(lo_leg.loadedObject, (Object) null))
        {
          GameObject loadedObject = lo_leg.loadedObject as GameObject;
          EffectPlayProcessor component = Object.op_Inequality((Object) loadedObject, (Object) null) ? loadedObject.GetComponent<EffectPlayProcessor>() : (EffectPlayProcessor) null;
          if (Object.op_Inequality((Object) component, (Object) null) && component.effectSettings != null)
          {
            int index = 0;
            for (int length = component.effectSettings.Length; index < length; ++index)
            {
              if (!string.IsNullOrEmpty(component.effectSettings[index].effectName))
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, component.effectSettings[index].effectName);
            }
          }
        }
      }
      else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_LEG, leg_name))
      {
        GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_LEG, leg_name);
        EffectPlayProcessor component = Object.op_Inequality((Object) playerResourceCache, (Object) null) ? playerResourceCache.GetComponent<EffectPlayProcessor>() : (EffectPlayProcessor) null;
        if (Object.op_Inequality((Object) component, (Object) null) && component.effectSettings != null)
        {
          int index = 0;
          for (int length = component.effectSettings.Length; index < length; ++index)
          {
            if (!string.IsNullOrEmpty(component.effectSettings[index].effectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, component.effectSettings[index].effectName);
          }
        }
      }
      if (load_queue.IsLoading())
        yield return (object) load_queue.Wait();
      bool wait = false;
      bool div_frame_realizes = false;
      int skin_color = info.skinColor;
      if (lo_body != null)
      {
        if (!div_frame_realizes)
        {
          this.body = lo_body.Realizes(_this);
          if (Object.op_Equality((Object) this.body, (Object) null))
            yield break;
          this.renderersBody = ((Component) this.body).gameObject.GetComponentsInChildren<Renderer>();
          ModelLoaderBase.SetEnabled(this.renderersBody, false);
          this.SetDynamicBones_Body(this.body, enableBone);
        }
        else
        {
          wait = true;
          InstantiateManager.Request((Object) this, lo_body.loadedObject, (Action<InstantiateManager.InstantiateData>) (data =>
          {
            this.body = ((GameObject) data.instantiatedObject).transform;
            this.body.SetParent(_this, false);
            this.renderersBody = ((Component) this.body).GetComponentsInChildren<Renderer>();
            this.SetDynamicBones_Body(this.body, enableBone);
            PlayerLoader.SetRenderersEnabled(this.renderersBody, false);
            wait = false;
          }));
          while (wait)
            yield return (object) null;
        }
      }
      else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_BDY, body_name))
      {
        GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_BDY, body_name);
        if (Object.op_Inequality((Object) playerResourceCache, (Object) null))
        {
          if (!div_frame_realizes)
          {
            this.body = LoadObject.RealizesWithGameObject(playerResourceCache, _this);
            this.renderersBody = ((Component) this.body).gameObject.GetComponentsInChildren<Renderer>();
            ModelLoaderBase.SetEnabled(this.renderersBody, false);
            this.SetDynamicBones_Body(this.body, enableBone);
          }
          else
          {
            wait = true;
            InstantiateManager.Request((Object) this, (Object) playerResourceCache, (Action<InstantiateManager.InstantiateData>) (data =>
            {
              this.body = ((GameObject) data.instantiatedObject).transform;
              this.body.SetParent(_this, false);
              this.renderersBody = ((Component) this.body).GetComponentsInChildren<Renderer>();
              this.SetDynamicBones_Body(this.body, enableBone);
              PlayerLoader.SetRenderersEnabled(this.renderersBody, false);
              wait = false;
            }));
            while (wait)
              yield return (object) null;
          }
        }
      }
      if (!Object.op_Equality((Object) this.body, (Object) null))
      {
        yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, this.body, shader_type));
        if (this.renderersBody != null)
        {
          if (this.renderersBody.Length != 0)
          {
            SkinnedMeshRenderer skinnedMeshRenderer = this.renderersBody[0] as SkinnedMeshRenderer;
            if (Object.op_Inequality((Object) skinnedMeshRenderer, (Object) null))
              skinnedMeshRenderer.localBounds = PlayerLoader.BOUNDS;
          }
          PlayerLoader.SetSkinAndEquipColor(this.renderersBody, skin_color, info.bodyColor, 0.0f);
          PlayerLoader.ApplyEquipHighResoTexs(lo_hr_bdy_tex, this.renderersBody);
          this.animator = ((Component) this.body).GetComponentInChildren<Animator>();
          if (Object.op_Inequality((Object) player, (Object) null))
            player.body = this.body;
          this.socketRoot = Utility.Find(this.body, "Root");
          this.socketHead = Utility.Find(this.body, "Head");
          this.socketWepL = Utility.Find(this.body, "L_Wep");
          this.socketWepR = Utility.Find(this.body, "R_Wep");
          this.socketFootL = Utility.Find(this.body, "L_Foot");
          this.socketFootR = Utility.Find(this.body, "R_Foot");
          this.socketHandL = Utility.Find(this.body, "L_Hand");
          this.socketForearmL = Utility.Find(this.body, "L_Forearm");
          if (need_foot_stamp)
          {
            if (Object.op_Inequality((Object) this.socketFootL, (Object) null) && Object.op_Equality((Object) ((Component) this.socketFootL).GetComponent<StampNode>(), (Object) null))
            {
              StampNode stampNode = ((Component) this.socketFootL).gameObject.AddComponent<StampNode>();
              stampNode.offset = new Vector3(-0.08f, 0.01f, 0.0f);
              stampNode.autoBaseY = 0.1f;
            }
            if (Object.op_Inequality((Object) this.socketFootR, (Object) null) && Object.op_Equality((Object) ((Component) this.socketFootR).GetComponent<StampNode>(), (Object) null))
            {
              StampNode stampNode = ((Component) this.socketFootR).gameObject.AddComponent<StampNode>();
              stampNode.offset = new Vector3(-0.08f, 0.01f, 0.0f);
              stampNode.autoBaseY = 0.1f;
            }
            CharacterStampCtrl characterStampCtrl = ((Component) this.body).GetComponent<CharacterStampCtrl>();
            if (Object.op_Equality((Object) characterStampCtrl, (Object) null))
              characterStampCtrl = ((Component) this.body).gameObject.AddComponent<CharacterStampCtrl>();
            characterStampCtrl.Init(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos, (Character) player);
            int index = 0;
            for (int length = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos.Length; index < length; ++index)
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos[index].effectName);
            if (load_queue.IsLoading())
              yield return (object) load_queue.Wait();
          }
          bool isLoading_Face = true;
          bool isLoading_Hair = true;
          bool isLoading_Head = true;
          bool isLoading_Arm = true;
          bool isLoading_Foot = true;
          bool isLoading_Weapn = true;
          bool kqLoading_Face = false;
          bool kqLoading_Hair = false;
          bool kqLoading_Head = false;
          bool kqLoading_Arm = false;
          bool kqLoading_Foot = false;
          bool kqLoading_Weapn = false;
          bool isSoulArrowOutGameEffect = false;
          if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && info.equipType == 5U && info.weaponSpAttackType == 2U)
            isSoulArrowOutGameEffect = true;
          this.StartCoroutine(this.DoLoadFace(lo_face, face_name, skin_color, enable_eye_blick, div_frame_realizes, (Action<bool>) (kq =>
          {
            isLoading_Face = false;
            kqLoading_Face = kq;
          })));
          this.StartCoroutine(this.DoLoadHair(lo_hair, loHairOverlay, hair_name, info, enableBone, skin_color, div_frame_realizes, (Action<bool>) (kq =>
          {
            isLoading_Hair = false;
            kqLoading_Hair = kq;
          })));
          this.StartCoroutine(this.DoLoadHead(load_queue, lo_head, lo_hr_hed_tex, head_name, info, shader_type, div_frame_realizes, (Action<bool>) (kq =>
          {
            isLoading_Head = false;
            kqLoading_Head = kq;
          })));
          this.StartCoroutine(this.DoLoadArm(lo_arm, lo_hr_arm_tex, arm_name, skin_color, info, arm_model_data, div_frame_realizes, (Action<bool>) (kq =>
          {
            isLoading_Arm = false;
            kqLoading_Arm = kq;
          })));
          this.StartCoroutine(this.DoLoadFoot(lo_leg, lo_hr_leg_tex, leg_name, skin_color, info, leg_model_data, div_frame_realizes, (Action<bool>) (kq =>
          {
            isLoading_Foot = false;
            kqLoading_Foot = kq;
          })));
          this.StartCoroutine(this.DoLoadWeapon(load_queue, lo_wepn, lo_hr_wep_tex, wepn_name, isSoulArrowOutGameEffect, high_reso_tex_flags, player, info, shader_type, div_frame_realizes, (Action<bool>) (kq =>
          {
            isLoading_Weapn = false;
            kqLoading_Weapn = kq;
          })));
          while (isLoading_Face | isLoading_Hair | isLoading_Head | isLoading_Arm | isLoading_Foot | isLoading_Weapn)
            yield return (object) null;
          if (kqLoading_Face && kqLoading_Hair && kqLoading_Head && kqLoading_Arm && kqLoading_Foot && kqLoading_Weapn)
          {
            if (Object.op_Inequality((Object) this.animator, (Object) null) && lo_anim != null)
            {
              RuntimeAnimatorController animatorController = lo_anim.loadedObjects[0].obj as RuntimeAnimatorController;
              if (Object.op_Inequality((Object) animatorController, (Object) null))
              {
                this.animator.runtimeAnimatorController = animatorController;
                if (Object.op_Inequality((Object) player, (Object) null))
                {
                  ((Component) this.animator).gameObject.AddComponent<StageObjectProxy>().stageObject = (StageObject) player;
                  if (need_anim_event)
                    player.animEventData = lo_anim.loadedObjects[1].obj as AnimEventData;
                }
                this.animator.updateMode = (AnimatorUpdateMode) 1;
                if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.isPlaySpAttackTypeMotion)
                {
                  SP_ATTACK_TYPE weaponSpAttackType = (SP_ATTACK_TYPE) info.weaponSpAttackType;
                  if (weaponSpAttackType != SP_ATTACK_TYPE.NONE)
                  {
                    string str = weaponSpAttackType.ToString();
                    int parameterCount = this.animator.parameterCount;
                    for (int index = 0; index < parameterCount; ++index)
                    {
                      if (this.animator.GetParameter(index).name == str)
                      {
                        this.animator.SetTrigger(str);
                        if (MonoBehaviourSingleton<EffectManager>.IsValid() & isSoulArrowOutGameEffect)
                        {
                          EffectManager.GetEffect("ef_btl_wsk2_bow_01_01", this.socketWepR);
                          break;
                        }
                        break;
                      }
                    }
                  }
                }
              }
            }
            if (lo_voices != null)
            {
              this.voiceAudioClips = lo_voices.loadedObjects;
              this.UpdateVoiceAudioClipIds();
            }
            if (!lo_accessories.IsNullOrEmpty<LoadObject>())
            {
              List<Renderer> accRendererList = new List<Renderer>();
              skill_len = 0;
              for (i = lo_accessories.Count; skill_len < i; ++skill_len)
              {
                LoadObject loadObject10 = lo_accessories[skill_len];
                Transform accTrans = (Transform) null;
                if (!div_frame_realizes)
                {
                  accTrans = loadObject10.Realizes();
                }
                else
                {
                  wait = true;
                  InstantiateManager.Request((Object) this, loadObject10.loadedObject, (Action<InstantiateManager.InstantiateData>) (data =>
                  {
                    accTrans = ((GameObject) data.instantiatedObject).transform;
                    wait = false;
                  }));
                  while (wait)
                    yield return (object) null;
                }
                if (Object.op_Inequality((Object) accTrans, (Object) null))
                {
                  AccessoryTable.AccessoryInfoData infoData = Singleton<AccessoryTable>.I.GetInfoData(info.accUIDs[skill_len]);
                  accTrans.SetParent(this.GetNodeTrans(infoData.node));
                  accTrans.localPosition = infoData.offset;
                  accTrans.localRotation = infoData.rotation;
                  accTrans.localScale = infoData.scale;
                  this.accessory.Add(accTrans);
                  accRendererList.AddRange((IEnumerable<Renderer>) ((Component) accTrans).GetComponentsInChildren<Renderer>());
                }
              }
              if (!this.accessory.IsNullOrEmpty<Transform>())
              {
                i = 0;
                for (skill_len = this.accessory.Count; i < skill_len; ++i)
                {
                  Transform equipItemRoot = this.accessory[i];
                  yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, equipItemRoot, shader_type));
                }
              }
              this.renderersAccessory = accRendererList.ToArray();
              ModelLoaderBase.SetEnabled(this.renderersAccessory, false);
              accRendererList = (List<Renderer>) null;
            }
            switch (shader_type)
            {
              case SHADER_TYPE.LIGHTWEIGHT:
                ShaderGlobal.ChangeWantLightweightShader(this.renderersWep);
                ShaderGlobal.ChangeWantLightweightShader(this.renderersFace);
                ShaderGlobal.ChangeWantLightweightShader(this.renderersHair);
                ShaderGlobal.ChangeWantLightweightShader(this.renderersBody);
                ShaderGlobal.ChangeWantLightweightShader(this.renderersHead);
                ShaderGlobal.ChangeWantLightweightShader(this.renderersArm);
                ShaderGlobal.ChangeWantLightweightShader(this.renderersLeg);
                ShaderGlobal.ChangeWantLightweightShader(this.renderersAccessory);
                break;
              case SHADER_TYPE.UI:
                ShaderGlobal.ChangeWantUIShader(this.renderersWep);
                ShaderGlobal.ChangeWantUIShader(this.renderersFace);
                ShaderGlobal.ChangeWantUIShader(this.renderersHair);
                ShaderGlobal.ChangeWantUIShader(this.renderersBody);
                ShaderGlobal.ChangeWantUIShader(this.renderersHead);
                ShaderGlobal.ChangeWantUIShader(this.renderersArm);
                ShaderGlobal.ChangeWantUIShader(this.renderersLeg);
                ShaderGlobal.ChangeWantUIShader(this.renderersAccessory);
                break;
            }
            this.SetLightProbes(enable_light_probes);
            if (layer != -1)
              PlayerLoader.SetLayerWithChildren_SecondaryNoChange(_this, layer);
            PlayerLoader.SetRenderersEnabled(this.renderersWep, true);
            PlayerLoader.SetRenderersEnabled(this.renderersFace, true);
            PlayerLoader.SetRenderersEnabled(this.renderersHair, true);
            PlayerLoader.SetRenderersEnabled(this.renderersBody, true);
            PlayerLoader.SetRenderersEnabled(this.renderersHead, true);
            PlayerLoader.SetRenderersEnabled(this.renderersArm, true);
            PlayerLoader.SetRenderersEnabled(this.renderersLeg, true);
            PlayerLoader.SetRenderersEnabled(this.renderersAccessory, true);
            if (need_shadow && Object.op_Equality((Object) this.shadow, (Object) null))
              this.shadow = PlayerLoader.CreateShadow(_this, is_lightweight: shader_type == SHADER_TYPE.LIGHTWEIGHT);
            if (Object.op_Inequality((Object) player, (Object) null))
            {
              if (Object.op_Inequality((Object) player.controller, (Object) null))
                ((Behaviour) player.controller).enabled = true;
              player.OnLoadComplete();
              if (Object.op_Inequality((Object) player.packetReceiver, (Object) null))
                player.packetReceiver.SetStopPacketUpdate(false);
            }
            if (Object.op_Inequality((Object) player, (Object) null) & is_self && MonoBehaviourSingleton<AudioListenerManager>.IsValid())
              MonoBehaviourSingleton<AudioListenerManager>.I.SetTargetObject((StageObject) player);
            if (callback != null)
              callback((object) player);
            this.ResetDynamicBones(this.dynamicBones);
            this.ResetDynamicBones(this.dynamicBones_Body);
            if (is_self)
            {
              ResourceLoad component = ((Component) player).gameObject.GetComponent<ResourceLoad>();
              if (Object.op_Inequality((Object) component, (Object) null) && component.list != null)
              {
                List<string> stringList = new List<string>();
                int index = 0;
                for (int size = component.list.size; index < size; ++index)
                  stringList.Add(component.list.buffer[index].name);
                stringList.Distinct<string>();
                MonoBehaviourSingleton<ResourceManager>.I.cache.AddIgnoreCategorySpecifiedReleaseList(stringList);
              }
            }
            this.isLoading = false;
            if (gg_op)
              this.DoLoadLater(need_anim_event);
          }
        }
      }
    }
  }

  protected virtual IEnumerator DoLoad_GG_Optimize_Self(
    PlayerLoadInfo info,
    int layer,
    int anim_id,
    bool need_anim_event,
    bool need_foot_stamp,
    bool need_shadow,
    bool enable_light_probes,
    bool need_action_voice,
    bool need_high_reso_tex,
    bool need_res_ref_count,
    bool need_dev_frame_instantiate,
    SHADER_TYPE shader_type,
    PlayerLoader.OnCompleteLoad callback,
    bool enable_eye_blick,
    int use_hair_overlay)
  {
    bool gg_op = true;
    if (info == null)
      Log.Error(LOG.RESOURCE, "PlayerLoader:info=null");
    PlayerLoader.SerializePlayerLoadInfo(info);
    this.animObjectTable = new StringKeyTable<LoadObject>();
    Player player = ((Component) this).gameObject.GetComponent<Player>();
    bool enableBone = this._EnableDynamicBone(shader_type, player is Self);
    EquipModelTable.Data data1 = Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.ARMOR, info.bodyModelID);
    EquipModelTable.Data data2 = data1.needHelm ? Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.HELM, info.headModelID) : (EquipModelTable.Data) null;
    EquipModelTable.Data arm_model_data = data1.needArm ? Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.ARM, info.armModelID) : (EquipModelTable.Data) null;
    EquipModelTable.Data leg_model_data = data1.needLeg ? Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.LEG, info.legModelID) : (EquipModelTable.Data) null;
    int id1 = data2 != null ? data2.GetHairModelID(info.hairModelID) : data1.GetHairModelID(info.hairModelID);
    if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 0)
    {
      need_high_reso_tex = false;
      use_hair_overlay = -1;
    }
    int high_reso_tex_flags = 0;
    if (need_high_reso_tex && MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
      high_reso_tex_flags = (int) MonoBehaviourSingleton<GlobalSettingsManager>.I.equipModelHQTable.GetWeaponFlag(info.weaponModelID);
    bool is_self = false;
    if (Object.op_Inequality((Object) player, (Object) null))
    {
      int id2 = player.id;
      is_self = player is Self;
    }
    this.DeleteLoadedObjects();
    this.loadInfo = info;
    if (anim_id < 0)
      anim_id = anim_id != -1 || info.weaponModelID == -1 ? -anim_id + info.weaponModelID / 1000 : info.weaponModelID / 1000;
    bool flag = data2 != null ? data2.needFace : data1.needFace;
    int num1 = data2 != null ? data2.hairMode : data1.hairMode;
    string face_name = info.faceModelID > -1 & flag ? ResourceName.GetPlayerFace(info.faceModelID) : (string) null;
    string hair_name = id1 <= -1 || num1 == 0 ? (string) null : ResourceName.GetPlayerHead(id1);
    string body_name = info.bodyModelID > -1 ? ResourceName.GetPlayerBody(info.bodyModelID) : (string) null;
    string head_name = info.headModelID <= -1 || data2 == null ? (string) null : ResourceName.GetPlayerHead(info.headModelID);
    string arm_name = info.armModelID <= -1 || arm_model_data == null ? (string) null : ResourceName.GetPlayerArm(info.armModelID);
    string leg_name = info.legModelID <= -1 || leg_model_data == null ? (string) null : ResourceName.GetPlayerLeg(info.legModelID);
    string wepn_name = info.weaponModelID > -1 ? ResourceName.GetPlayerWeapon(info.weaponModelID) : (string) null;
    if (body_name != null)
    {
      Transform _this = ((Component) this).transform;
      if (Object.op_Inequality((Object) player, (Object) null))
      {
        if (Object.op_Inequality((Object) player.controller, (Object) null))
          ((Behaviour) player.controller).enabled = false;
        if (Object.op_Inequality((Object) player.packetReceiver, (Object) null))
          player.packetReceiver.SetStopPacketUpdate(true);
        player.OnLoadStart();
      }
      if (is_self)
        MonoBehaviourSingleton<GoGameCacheManager>.I.ClearCacheSelfPlayerModelNotUse(new GoGameCacheManager.PlayerModelData()
        {
          faceName = face_name,
          hairName = hair_name,
          bodyName = body_name,
          headName = head_name,
          armName = arm_name,
          legName = leg_name,
          wepnName = wepn_name
        });
      this.isLoading = true;
      LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this, need_res_ref_count);
      LoadObject lo_face = (LoadObject) null;
      LoadObject lo_hair = (LoadObject) null;
      LoadObject lo_body = (LoadObject) null;
      LoadObject lo_head = (LoadObject) null;
      LoadObject lo_arm = (LoadObject) null;
      LoadObject lo_leg = (LoadObject) null;
      LoadObject lo_wepn = (LoadObject) null;
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_FACE, face_name))
        lo_face = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_FACE, face_name);
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name))
        lo_hair = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_HEAD, hair_name);
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_BDY, body_name))
        lo_body = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_BDY, body_name);
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_HEAD, head_name))
        lo_head = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_HEAD, head_name);
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ARM, arm_name))
        lo_arm = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_ARM, arm_name);
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_LEG, leg_name))
        lo_leg = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_LEG, leg_name);
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name))
        lo_wepn = this.GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name);
      List<LoadObject> lo_accessories = new List<LoadObject>();
      if (!info.accUIDs.IsNullOrEmpty<uint>())
      {
        int index = 0;
        for (int count = info.accUIDs.Count; index < count; ++index)
        {
          string playerAccessory = ResourceName.GetPlayerAccessory(Singleton<AccessoryTable>.I.GetInfoData(info.accUIDs[index]).accessoryId);
          lo_accessories.Add(need_dev_frame_instantiate ? (LoadObject) load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_ACCESSORY, playerAccessory) : load_queue.Load(RESOURCE_CATEGORY.PLAYER_ACCESSORY, playerAccessory));
        }
      }
      LoadObject lo_voices = (LoadObject) null;
      LoadObject lo_hr_wep_tex = (LoadObject) null;
      LoadObject lo_hr_hed_tex = (LoadObject) null;
      LoadObject lo_hr_bdy_tex = (LoadObject) null;
      LoadObject lo_hr_arm_tex = (LoadObject) null;
      LoadObject lo_hr_leg_tex = (LoadObject) null;
      string anim_name = anim_id > -1 ? ResourceName.GetPlayerAnim(anim_id) : (string) null;
      if (load_queue.IsLoading())
        yield return (object) load_queue.Wait();
      if (is_self)
      {
        if (lo_face != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_FACE, face_name, lo_face.loadedObject);
        if (lo_hair != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name, lo_hair.loadedObject);
        if (lo_body != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_BDY, body_name, lo_body.loadedObject);
        if (lo_head != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_HEAD, head_name, lo_head.loadedObject);
        if (lo_arm != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_ARM, arm_name, lo_arm.loadedObject);
        if (lo_leg != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_LEG, leg_name, lo_leg.loadedObject);
        if (lo_wepn != null)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name, lo_wepn.loadedObject);
      }
      if (is_self)
      {
        this.faceCacheName = face_name;
        this.hairCacheName = hair_name;
        this.bodyCacheName = body_name;
        this.headCacheName = head_name;
        this.armCacheName = arm_name;
        this.legCacheName = leg_name;
        this.weaponCacheName = wepn_name;
      }
      LoadObject loadObject1;
      if (anim_name == null)
      {
        loadObject1 = (LoadObject) null;
      }
      else
      {
        LoadingQueue loadingQueue = load_queue;
        string package_name = anim_name;
        string[] resource_names;
        if (!need_anim_event)
          resource_names = new string[1]
          {
            anim_name + "Ctrl"
          };
        else
          resource_names = new string[2]
          {
            anim_name + "Ctrl",
            anim_name + "Event"
          };
        loadObject1 = loadingQueue.Load(RESOURCE_CATEGORY.PLAYER_ANIM, package_name, resource_names);
      }
      LoadObject lo_anim = loadObject1;
      if (lo_anim != null)
        this.animObjectTable.Add("BASE", lo_anim);
      if (Object.op_Inequality((Object) player, (Object) null) && anim_id > -1)
      {
        List<string> stringList = new List<string>();
        int num2 = 3;
        for (int index1 = 0; index1 < num2; ++index1)
        {
          for (int index2 = 0; index2 < 2; ++index2)
          {
            SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + index1);
            if (skillParam != null)
            {
              string fromAnimFormatName = Character.GetCtrlNameFromAnimFormatName(index2 == 0 ? skillParam.tableData.castStateName : skillParam.tableData.actStateName);
              if (!string.IsNullOrEmpty(fromAnimFormatName) && stringList.IndexOf(fromAnimFormatName) < 0)
              {
                stringList.Add(fromAnimFormatName);
                string playerSubAnim = ResourceName.GetPlayerSubAnim(anim_id, fromAnimFormatName);
                LoadObject loadObject2;
                if (playerSubAnim == null)
                {
                  loadObject2 = (LoadObject) null;
                }
                else
                {
                  LoadingQueue loadingQueue = load_queue;
                  string package_name = playerSubAnim;
                  string[] resource_names;
                  if (!need_anim_event)
                    resource_names = new string[1]
                    {
                      playerSubAnim + "Ctrl"
                    };
                  else
                    resource_names = new string[2]
                    {
                      playerSubAnim + "Ctrl",
                      playerSubAnim + "Event"
                    };
                  loadObject2 = loadingQueue.Load(RESOURCE_CATEGORY.PLAYER_ANIM_SKILL, package_name, resource_names);
                }
                LoadObject loadObject3 = loadObject2;
                if (loadObject3 != null)
                  this.animObjectTable.Add(fromAnimFormatName, loadObject3);
              }
            }
          }
        }
      }
      if (Object.op_Inequality((Object) player, (Object) null) && info.weaponEvolveId > 0U)
      {
        string ctrlName;
        string animName;
        ResourceName.GetPlayerEvolveAnim(info.weaponEvolveId, out ctrlName, out animName);
        LoadObject loadObject4 = load_queue.Load(RESOURCE_CATEGORY.PLAYER_ANIM_EVOLVE, animName, new string[2]
        {
          animName + "Ctrl",
          animName + "Event"
        });
        if (loadObject4 != null)
          this.animObjectTable.Add(ctrlName, loadObject4);
      }
      if (need_action_voice && info.actionVoiceBaseID > -1)
      {
        int[] values = (int[]) Enum.GetValues(typeof (ACTION_VOICE_ID));
        int length = values.Length;
        string[] resource_names = new string[length];
        for (int index = 0; index < length; ++index)
          resource_names[index] = ResourceName.GetActionVoiceName(info.actionVoiceBaseID + values[index]);
        lo_voices = load_queue.Load(RESOURCE_CATEGORY.SOUND_VOICE, ResourceName.GetActionVoicePackageNameFromVoiceID(info.actionVoiceBaseID), resource_names);
      }
      if (need_high_reso_tex)
      {
        if (high_reso_tex_flags != 0 && wepn_name != null)
          lo_hr_wep_tex = PlayerLoader.LoadHighResoTexs(load_queue, wepn_name, high_reso_tex_flags);
        if (head_name != null)
          lo_hr_hed_tex = PlayerLoader.LoadHighResoTexs(load_queue, head_name, 1);
        if (body_name != null)
          lo_hr_bdy_tex = PlayerLoader.LoadHighResoTexs(load_queue, body_name, 1);
        if (arm_name != null)
          lo_hr_arm_tex = PlayerLoader.LoadHighResoTexs(load_queue, arm_name, 1);
        if (leg_name != null)
          lo_hr_leg_tex = PlayerLoader.LoadHighResoTexs(load_queue, leg_name, 1);
      }
      LoadObject loHairOverlay = (LoadObject) null;
      if (use_hair_overlay != -1)
        loHairOverlay = PlayerLoader.LoadHairOverlayTexs(load_queue, info, use_hair_overlay);
      yield return (object) load_queue.Wait();
      List<string> needAtkInfoNames = new List<string>();
      StringKeyTable<LoadObject> animEventBulletLoadObjTable = new StringKeyTable<LoadObject>();
      int skill_len;
      int i;
      if (Object.op_Inequality((Object) player, (Object) null))
      {
        if (need_anim_event)
          this.animObjectTable.ForEach((Action<LoadObject>) (load_object =>
          {
            if (!gg_op)
              load_queue.CacheAnimDataUseResource(load_object.loadedObjects[1].obj as AnimEventData, (LoadingQueue.EffectNameAnalyzer) (effect_name => effect_name[0] != '@' ? effect_name : (string) null));
            load_queue.CacheAnimDataUseResourceDependPlayer(player, load_object.loadedObjects[1].obj as AnimEventData);
            AnimEventData animEventData = load_object.loadedObjects[1].obj as AnimEventData;
            if (!Object.op_Inequality((Object) animEventData, (Object) null) || ((IList<AnimEventData.AnimData>) animEventData.animations).IsNullOrEmpty<AnimEventData.AnimData>())
              return;
            foreach (AnimEventData.AnimData animation in animEventData.animations)
            {
              foreach (AnimEventData.EventData eventData in animation.events)
              {
                AnimEventFormat.ID id3 = eventData.id;
                if (id3 <= AnimEventFormat.ID.SHOT_ZONE)
                {
                  if (id3 != AnimEventFormat.ID.SHOT_PRESENT)
                  {
                    if (id3 != AnimEventFormat.ID.SHOT_ZONE)
                      goto label_25;
                  }
                  else
                  {
                    int index3 = 0;
                    for (int length1 = eventData.stringArgs.Length; index3 < length1; ++index3)
                    {
                      string[] self = eventData.stringArgs[index3].Split(':');
                      if (!((IList<string>) self).IsNullOrEmpty<string>())
                      {
                        int index4 = 0;
                        for (int length2 = self.Length; index4 < length2; ++index4)
                        {
                          string str = self[index4];
                          if (!string.IsNullOrEmpty(str))
                          {
                            LoadObject loadObject5 = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, str);
                            if (loadObject5 != null && animEventBulletLoadObjTable.Get(str) == null)
                              animEventBulletLoadObjTable.Add(str, loadObject5);
                          }
                        }
                      }
                    }
                    goto label_25;
                  }
                }
                else if (id3 != AnimEventFormat.ID.SHOT_DECOY && id3 != AnimEventFormat.ID.LOAD_BULLET)
                  goto label_25;
                for (int index = 0; index < eventData.stringArgs.Length; ++index)
                {
                  string stringArg = eventData.stringArgs[index];
                  if (!string.IsNullOrEmpty(stringArg))
                  {
                    LoadObject loadObject6 = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, stringArg);
                    if (loadObject6 != null && animEventBulletLoadObjTable.Get(stringArg) == null)
                      animEventBulletLoadObjTable.Add(stringArg, loadObject6);
                  }
                }
label_25:
                if (id3 <= AnimEventFormat.ID.SHOT_NODE_LINK)
                {
                  if (id3 <= AnimEventFormat.ID.NWAY_LASER_ATTACK)
                  {
                    if (id3 != AnimEventFormat.ID.SHOT_ARROW && (uint) (id3 - 70) > 5U && id3 != AnimEventFormat.ID.NWAY_LASER_ATTACK)
                      continue;
                  }
                  else if (id3 <= AnimEventFormat.ID.GENERATE_TRACKING)
                  {
                    if (id3 != AnimEventFormat.ID.EXATK_COLLIDER_START && id3 != AnimEventFormat.ID.GENERATE_TRACKING)
                      continue;
                  }
                  else if (id3 != AnimEventFormat.ID.PLAYER_FUNNEL_ATTACK && id3 != AnimEventFormat.ID.SHOT_NODE_LINK)
                    continue;
                }
                else if (id3 <= AnimEventFormat.ID.SHOT_HEALING_HOMING)
                {
                  if (id3 != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE && (uint) (id3 - 199) > 1U && id3 != AnimEventFormat.ID.SHOT_HEALING_HOMING)
                    continue;
                }
                else if (id3 <= AnimEventFormat.ID.SHOT_RESURRECTION_HOMING)
                {
                  if (id3 != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE_MULTI && id3 != AnimEventFormat.ID.SHOT_RESURRECTION_HOMING)
                    continue;
                }
                else if (id3 != AnimEventFormat.ID.BUFF_START_SHIELD_REFLECT && id3 != AnimEventFormat.ID.SHOT_ORACLE_SPEAR_SP)
                  continue;
                string stringArg1 = eventData.stringArgs[0];
                if (!string.IsNullOrEmpty(stringArg1))
                {
                  foreach (string str in ResourceName.GetNamesNeededLoadAtkInfoFromAnimEvent())
                  {
                    if (stringArg1.StartsWith(str))
                    {
                      needAtkInfoNames.Add(stringArg1);
                      break;
                    }
                  }
                }
              }
            }
          }));
        this.AddSkillAttackInfoName(player, ref needAtkInfoNames);
        this.AddFieldGimmickAttackInfoName(ref needAtkInfoNames);
        List<LoadObject> atkInfo = new List<LoadObject>();
        List<LoadObject> atkInfoLoaded = new List<LoadObject>();
        Dictionary<string, LoadObject> atkInfoDict = new Dictionary<string, LoadObject>();
        if (this.playerLoaderLoadedAttackInfoNames == null)
          this.playerLoaderLoadedAttackInfoNames = new HashSet<string>();
        for (int index = 0; index < needAtkInfoNames.Count; ++index)
        {
          if (!this.playerLoaderLoadedAttackInfoNames.Contains(needAtkInfoNames[index]))
          {
            if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[index]))
            {
              LoadObject loadObject7 = load_queue.Load(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[index]);
              atkInfo.Add(loadObject7);
              atkInfoLoaded.Add(loadObject7);
              atkInfoDict.Add(needAtkInfoNames[index], loadObject7);
            }
            else
            {
              Object playerResourceCache = MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[index]);
              LoadObject loadObject8 = new LoadObject();
              loadObject8.loadedObject = playerResourceCache;
              atkInfoLoaded.Add(loadObject8);
              atkInfoDict.Add(needAtkInfoNames[index], loadObject8);
            }
            this.playerLoaderLoadedAttackInfoNames.Add(needAtkInfoNames[index]);
          }
        }
        if (load_queue.IsLoading())
          yield return (object) load_queue.Wait();
        if (info.isNeedToCache)
        {
          foreach (KeyValuePair<string, LoadObject> keyValuePair in atkInfoDict)
          {
            string key = keyValuePair.Key;
            LoadObject loadObject9 = keyValuePair.Value;
            if (atkInfo.Contains(loadObject9))
              MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, key, loadObject9.loadedObject);
          }
        }
        if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
        {
          Transform _settingTransform = MonoBehaviourSingleton<InGameSettingsManager>.I._transform;
          List<AttackInfo> hitInfos = new List<AttackInfo>();
          for (i = 0; i < atkInfoLoaded.Count; ++i)
          {
            SplitPlayerAttackInfo attackInfo = ((Component) LoadObject.RealizesWithGameObject((GameObject) atkInfoLoaded[i].loadedObject, _settingTransform)).gameObject.GetComponent<SplitPlayerAttackInfo>();
            hitInfos.Add((AttackInfo) attackInfo.attackHitInfo);
            hitInfos.Add((AttackInfo) attackInfo.attackContinuationInfo);
            if (!string.IsNullOrEmpty(attackInfo.attackHitInfo.nextBulletInfoName))
              yield return (object) this.StartCoroutine(this.LoadNextBulletInfo(load_queue, attackInfo.attackHitInfo.nextBulletInfoName, hitInfos, info.isNeedToCache));
            if (!string.IsNullOrEmpty(attackInfo.attackContinuationInfo.nextBulletInfoName))
              yield return (object) this.StartCoroutine(this.LoadNextBulletInfo(load_queue, attackInfo.attackContinuationInfo.nextBulletInfoName, hitInfos, info.isNeedToCache));
            attackInfo = (SplitPlayerAttackInfo) null;
          }
          InGameSettingsManager.Player player1 = MonoBehaviourSingleton<InGameSettingsManager>.I.player;
          if (player1.attackInfosAll == null)
            player1.attackInfosAll = new AttackInfo[0];
          AttackInfo[] mergedArray = Utility.CreateMergedArray<AttackInfo>(player1.attackInfosAll, hitInfos.ToArray());
          player1.attackInfosAll = Utility.DistinctArray<AttackInfo>(mergedArray);
          player.AddAttackInfos(hitInfos.ToArray());
          _settingTransform = (Transform) null;
          hitInfos = (List<AttackInfo>) null;
        }
        this.LoadAttackInfoResource(player, load_queue);
        if (load_queue.IsLoading())
          yield return (object) load_queue.Wait();
        ELEMENT_TYPE nowWeaponElement = player.GetNowWeaponElement();
        switch (anim_id)
        {
          case 0:
            load_queue.CacheSE(10000042);
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_sword_01_01");
            switch (player.spAttackType)
            {
              case SP_ATTACK_TYPE.HEAT:
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl05_attack_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_sword_01_04");
                break;
              case SP_ATTACK_TYPE.SOUL:
                if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
                {
                  InGameSettingsManager.Player.OneHandSwordActionInfo ohsActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo;
                  load_queue.CacheSE(ohsActionInfo.Soul_BoostSeId);
                  load_queue.CacheSE(ohsActionInfo.Soul_SnatchHitSeId);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsActionInfo.Soul_SnatchHitEffect);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsActionInfo.Soul_SnatchHitRemainEffect);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsActionInfo.Soul_SnatchHitEffectOnBoostMode);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_btl_soul_energy_01");
                  if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) ohsActionInfo.Soul_BoostElementHitEffect.Length)
                  {
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsActionInfo.Soul_BoostElementHitEffect[(int) nowWeaponElement]);
                    break;
                  }
                  break;
                }
                break;
              case SP_ATTACK_TYPE.BURST:
                InGameSettingsManager.Player.BurstOneHandSwordActionInfo burstOhsInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.burstOHSInfo;
                if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) burstOhsInfo.BoostElementHitEffect.Length)
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstOhsInfo.BoostElementHitEffect[(int) nowWeaponElement]);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
                break;
              case SP_ATTACK_TYPE.ORACLE:
                InGameSettingsManager.Player.OracleOneHandSwordActionInfo oracleOhsInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.oracleOHSInfo;
                for (int index = 0; index < oracleOhsInfo.dragonEffects.Length; ++index)
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleOhsInfo.dragonEffects[index].GetEffectName(nowWeaponElement));
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_sword_dragon_veil");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_sword_dragon_veil_re");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, $"ef_btl_wsk4_sword_02_{(int) nowWeaponElement:D2}");
                break;
            }
            break;
          case 1:
            if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
            {
              InGameSettingsManager.Player.TwoHandSwordActionInfo handSwordActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo;
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, handSwordActionInfo.nameChargeExpandEffect);
              if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
              {
                load_queue.CacheSE(handSwordActionInfo.soulBoostSeId);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, handSwordActionInfo.soulIaiChargeMaxEffect);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_btl_soul_energy_01");
              }
              if (player.spAttackType == SP_ATTACK_TYPE.BURST)
              {
                InGameSettingsManager.Player.BurstTwoHandSwordActionInfo burstThsInfo = handSwordActionInfo.burstTHSInfo;
                if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) burstThsInfo.HitEffect_SingleShot.Length)
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstThsInfo.HitEffect_SingleShot[(int) nowWeaponElement]);
                if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) burstThsInfo.HitEffect_FullBurst.Length)
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstThsInfo.HitEffect_FullBurst[(int) nowWeaponElement]);
              }
              if (player.spAttackType == SP_ATTACK_TYPE.ORACLE)
              {
                InGameSettingsManager.Player.OracleTwoHandSwordActionInfo oracleThsInfo = handSwordActionInfo.oracleTHSInfo;
                load_queue.CacheSE(oracleThsInfo.normalVernierSeId);
                load_queue.CacheSE(oracleThsInfo.maxVernierSeId);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleThsInfo.normalVernierEffectName);
                if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) oracleThsInfo.maxVernierEffectNames.Length)
                {
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleThsInfo.maxVernierEffectNames[(int) nowWeaponElement]);
                  break;
                }
                break;
              }
              break;
            }
            break;
          case 2:
            switch (player.spAttackType)
            {
              case SP_ATTACK_TYPE.NONE:
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_loop_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_loop_02");
                break;
              case SP_ATTACK_TYPE.HEAT:
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_e_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_spear_01_03");
                break;
              case SP_ATTACK_TYPE.SOUL:
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_btl_soul_energy_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_spear_02_02");
                break;
              case SP_ATTACK_TYPE.BURST:
                if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
                {
                  InGameSettingsManager.Player.BurstSpearActionInfo burstSpearInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.burstSpearInfo;
                  if (burstSpearInfo.spinSeId > 0)
                    load_queue.CacheSE(burstSpearInfo.spinSeId);
                  if (burstSpearInfo.spinMaxSpeedSeId > 0)
                    load_queue.CacheSE(burstSpearInfo.spinMaxSpeedSeId);
                  string name1 = string.Empty;
                  if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) burstSpearInfo.spinEffectNames.Length)
                    name1 = burstSpearInfo.spinEffectNames[(int) nowWeaponElement];
                  string name2 = string.Empty;
                  if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) burstSpearInfo.spinElementHitEffectNames.Length)
                    name2 = burstSpearInfo.spinElementHitEffectNames[(int) nowWeaponElement];
                  string name3 = string.Empty;
                  if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) burstSpearInfo.spinThrowGroundEffectNames.Length)
                    name3 = burstSpearInfo.spinThrowGroundEffectNames[(int) nowWeaponElement];
                  if (!string.IsNullOrEmpty(name1))
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name1);
                  if (!string.IsNullOrEmpty(name2))
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name2);
                  if (!string.IsNullOrEmpty(name3))
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name3);
                  if (!string.IsNullOrEmpty(burstSpearInfo.throwGroundEffectName))
                  {
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstSpearInfo.throwGroundEffectName);
                    break;
                  }
                  break;
                }
                break;
              case SP_ATTACK_TYPE.ORACLE:
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_spear_aura");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_spear_guard");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_spear_stock");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_sword_01_01");
                load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.oracle.gutsSE);
                load_queue.CacheSE(10000042);
                break;
            }
            if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid() && player.spAttackType != SP_ATTACK_TYPE.BURST)
            {
              InGameSettingsManager.Player.SpearActionInfo spearActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo;
              string name = spearActionInfo.jumpHugeHitEffectName;
              if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) spearActionInfo.jumpHugeElementHitEffectNames.Length)
                name = spearActionInfo.jumpHugeElementHitEffectNames[(int) nowWeaponElement];
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name);
              break;
            }
            break;
          case 4:
            if (player.spAttackType == SP_ATTACK_TYPE.NONE)
            {
              if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
                load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.wildDanceChargeMaxSeId);
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
              break;
            }
            if (player.spAttackType == SP_ATTACK_TYPE.HEAT)
            {
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_02");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_03");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_04");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_05");
              break;
            }
            if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
            {
              if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
              {
                InGameSettingsManager.Player.PairSwordsActionInfo swordsActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo;
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, swordsActionInfo.Soul_EffectForWaitingLaser);
                string soulEffectForBullet = swordsActionInfo.Soul_EffectForBullet;
                if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) swordsActionInfo.Soul_EffectsForBullet.Length)
                  soulEffectForBullet = swordsActionInfo.Soul_EffectsForBullet[(int) nowWeaponElement];
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, soulEffectForBullet);
                if (!((IList<int>) swordsActionInfo.Soul_SeIds).IsNullOrEmpty<int>())
                {
                  for (int index = 0; index < swordsActionInfo.Soul_SeIds.Length; ++index)
                  {
                    if (swordsActionInfo.Soul_SeIds[index] >= 0)
                      load_queue.CacheSE(swordsActionInfo.Soul_SeIds[index]);
                  }
                  break;
                }
                break;
              }
              break;
            }
            if (player.spAttackType == SP_ATTACK_TYPE.BURST)
            {
              load_queue.CacheSE(10000051);
              load_queue.CacheSE(10000042);
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_twinsword_01_00");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
              InGameSettingsManager.Player.PairSwordsActionInfo swordsActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo;
              if (nowWeaponElement <= ELEMENT_TYPE.DARK && nowWeaponElement < (ELEMENT_TYPE) swordsActionInfo.Burst_CombineHitEffect.Length)
              {
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, swordsActionInfo.Burst_CombineHitEffect[(int) nowWeaponElement]);
                break;
              }
              break;
            }
            if (player.spAttackType == SP_ATTACK_TYPE.ORACLE)
            {
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_twinsword_01");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, $"ef_btl_wsk4_twinsword_01_{(int) nowWeaponElement:D2}");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_twinsword_03");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_twinsword_04");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, $"ef_btl_wsk4_twinsword_05_{(int) nowWeaponElement:D2}");
              break;
            }
            break;
          case 5:
            if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
            {
              InGameSettingsManager.Player.SpecialActionInfo specialActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo;
              InGameSettingsManager.TargetMarkerSettings targetMarkerSettings = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerSettings;
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowChargeAimEffectName);
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowAimLesserCursorEffectName);
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.bestDistanceEffect);
              if (player.spAttackType == SP_ATTACK_TYPE.NONE)
              {
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[6]);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[5]);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowBleedEffectName);
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowBleedDamageEffectName);
                switch (nowWeaponElement)
                {
                  case ELEMENT_TYPE.FIRE:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowFireBurstEffectName);
                    break;
                  case ELEMENT_TYPE.WATER:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowWaterBurstEffectName);
                    break;
                  case ELEMENT_TYPE.THUNDER:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowThunderBurstEffectName);
                    break;
                  case ELEMENT_TYPE.SOIL:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowSoilBurstEffectName);
                    break;
                  case ELEMENT_TYPE.LIGHT:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowLightrBurstEffectName);
                    break;
                  case ELEMENT_TYPE.DARK:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowDarkBurstEffectName);
                    break;
                  default:
                    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, specialActionInfo.arrowBurstEffectName);
                    break;
                }
              }
              else
              {
                if (player.spAttackType == SP_ATTACK_TYPE.HEAT)
                {
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[22]);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[21]);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_bow_01_02");
                  break;
                }
                if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
                {
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[24]);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_bow_lock_02");
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
                  load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulLockMaxSeId);
                  load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulLockSeId);
                  load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.soulBoostSeId);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
                  break;
                }
                if (player.spAttackType == SP_ATTACK_TYPE.BURST)
                {
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[27]);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[26]);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.GetBombArrowEffectName(nowWeaponElement));
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.arrowRainShotAimLesserCursorEffectName);
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.GetBombEffectName(nowWeaponElement));
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.boostArrowChargeMaxEffectName);
                  load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.burstBoostModeSEId);
                  List<int> bombArrowSeIdList = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombArrowSEIdList;
                  for (int index = 0; index < bombArrowSeIdList.Count; ++index)
                    load_queue.CacheSE(bombArrowSeIdList[index]);
                  List<int> bombSeIdList = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombSEIdList;
                  for (int index = 0; index < bombSeIdList.Count; ++index)
                    load_queue.CacheSE(bombSeIdList[index]);
                  break;
                }
                break;
              }
            }
            else
              break;
            break;
        }
        EvolveController.Load(load_queue, info.weaponEvolveId);
        skill_len = 3;
        LoadObject[] bullet_load = new LoadObject[skill_len];
        for (int index5 = 0; index5 < skill_len; ++index5)
        {
          SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + index5);
          if (skillParam == null)
          {
            bullet_load[index5] = (LoadObject) null;
          }
          else
          {
            SkillItemTable.SkillItemData tableData = skillParam.tableData;
            if (!string.IsNullOrEmpty(tableData.startEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.startEffectName);
            if (tableData.startSEID != 0)
              load_queue.CacheSE(tableData.startSEID);
            if (!string.IsNullOrEmpty(tableData.actLocalEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.actLocalEffectName);
            if (!string.IsNullOrEmpty(tableData.actOneshotEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.actOneshotEffectName);
            if (tableData.actSEID != 0)
              load_queue.CacheSE(tableData.actSEID);
            if (!string.IsNullOrEmpty(tableData.enchantEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.enchantEffectName);
            if (!string.IsNullOrEmpty(tableData.hitEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.hitEffectName);
            if ((double) (float) tableData.skillRange > 0.0 && !string.IsNullOrEmpty(MonoBehaviourSingleton<InGameSettingsManager>.I.player.skillRangeEffectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.skillRangeEffectName);
            if (tableData.hitSEID != 0)
              load_queue.CacheSE(tableData.hitSEID);
            if (is_self)
              load_queue.CacheItemIcon(tableData.iconID);
            if (tableData.healType == HEAL_TYPE.RESURRECTION_ALL)
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_heal_04_03");
            if (!((IList<int>) tableData.buffTableIds).IsNullOrEmpty<int>())
            {
              for (int index6 = 0; index6 < tableData.buffTableIds.Length; ++index6)
              {
                BuffTable.BuffData data3 = Singleton<BuffTable>.I.GetData((uint) tableData.buffTableIds[index6]);
                if (BuffParam.IsHitAbsorbType(data3.type))
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_drain_01_01");
                else if (data3.type == BuffParam.BUFFTYPE.AUTO_REVIVE)
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_heal_04_03");
              }
            }
            foreach (string name in tableData.supportEffectName)
            {
              if (!string.IsNullOrEmpty(name))
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name);
            }
            foreach (BuffParam.BUFFTYPE bufftype in tableData.supportType)
            {
              switch (bufftype)
              {
                case BuffParam.BUFFTYPE.SKILL_CHARGE_ABOVE:
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_btl_sk_magi_move_01_01");
                  break;
                case BuffParam.BUFFTYPE.SUBSTITUTE:
                  load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_magi_shikigami_01_02");
                  break;
              }
            }
            if (tableData.isTeleportation)
            {
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_warp_02_01");
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_warp_02_02");
            }
            bullet_load[index5] = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, tableData.bulletName);
          }
        }
        if (is_self)
          load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_darkness_02");
        EffectPlayProcessor effectPlayProcessor = player.effectPlayProcessor;
        if (Object.op_Inequality((Object) effectPlayProcessor, (Object) null) && effectPlayProcessor.effectSettings != null)
        {
          int index = 0;
          for (int length = effectPlayProcessor.effectSettings.Length; index < length; ++index)
          {
            if (!string.IsNullOrEmpty(effectPlayProcessor.effectSettings[index].effectName))
            {
              string name = effectPlayProcessor.effectSettings[index].name;
              if (name.StartsWith("BUFF_"))
              {
                string str = name.Substring(name.Length - "_PLC00".Length);
                if (str.Contains("_PLC") && str != "_PLC" + (this.loadInfo.weaponModelID / 1000).ToString("D2"))
                  continue;
              }
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor.effectSettings[index].effectName);
            }
          }
        }
        if (load_queue.IsLoading())
          yield return (object) load_queue.Wait();
        for (int index = 0; index < skill_len; ++index)
        {
          SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + index);
          if (skillParam != null)
          {
            skillParam.bullet = bullet_load[index].loadedObject as BulletData;
            load_queue.CacheBulletDataUseResource(skillParam.bullet, player);
          }
        }
        if (animEventBulletLoadObjTable != null)
          animEventBulletLoadObjTable.ForEachKeyAndValue((Action<string, LoadObject>) ((key, item) =>
          {
            if (item == null || !Object.op_Inequality(item.loadedObject, (Object) null))
              return;
            BulletData loadedObject = item.loadedObject as BulletData;
            if (!Object.op_Inequality((Object) loadedObject, (Object) null) || !Object.op_Equality((Object) player.cachedBulletDataTable.Get(key), (Object) null))
              return;
            player.cachedBulletDataTable.Add(key, loadedObject);
            load_queue.CacheBulletDataUseResource(loadedObject, player);
          }));
        animEventBulletLoadObjTable.Clear();
        animEventBulletLoadObjTable = (StringKeyTable<LoadObject>) null;
        if (load_queue.IsLoading())
          yield return (object) load_queue.Wait();
        atkInfo = (List<LoadObject>) null;
        atkInfoLoaded = (List<LoadObject>) null;
        atkInfoDict = (Dictionary<string, LoadObject>) null;
        bullet_load = (LoadObject[]) null;
      }
      if (lo_arm != null)
      {
        if (Object.op_Inequality(lo_arm.loadedObject, (Object) null))
        {
          GameObject loadedObject = lo_arm.loadedObject as GameObject;
          EffectPlayProcessor component = Object.op_Inequality((Object) loadedObject, (Object) null) ? loadedObject.GetComponent<EffectPlayProcessor>() : (EffectPlayProcessor) null;
          if (Object.op_Inequality((Object) component, (Object) null) && component.effectSettings != null)
          {
            int index = 0;
            for (int length = component.effectSettings.Length; index < length; ++index)
            {
              if (!string.IsNullOrEmpty(component.effectSettings[index].effectName))
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, component.effectSettings[index].effectName);
            }
          }
        }
      }
      else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ARM, arm_name))
      {
        GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ARM, arm_name);
        EffectPlayProcessor component = Object.op_Inequality((Object) playerResourceCache, (Object) null) ? playerResourceCache.GetComponent<EffectPlayProcessor>() : (EffectPlayProcessor) null;
        if (Object.op_Inequality((Object) component, (Object) null) && component.effectSettings != null)
        {
          int index = 0;
          for (int length = component.effectSettings.Length; index < length; ++index)
          {
            if (!string.IsNullOrEmpty(component.effectSettings[index].effectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, component.effectSettings[index].effectName);
          }
        }
      }
      if (lo_leg != null)
      {
        if (Object.op_Inequality(lo_leg.loadedObject, (Object) null))
        {
          GameObject loadedObject = lo_leg.loadedObject as GameObject;
          EffectPlayProcessor component = Object.op_Inequality((Object) loadedObject, (Object) null) ? loadedObject.GetComponent<EffectPlayProcessor>() : (EffectPlayProcessor) null;
          if (Object.op_Inequality((Object) component, (Object) null) && component.effectSettings != null)
          {
            int index = 0;
            for (int length = component.effectSettings.Length; index < length; ++index)
            {
              if (!string.IsNullOrEmpty(component.effectSettings[index].effectName))
                load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, component.effectSettings[index].effectName);
            }
          }
        }
      }
      else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_LEG, leg_name))
      {
        GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_LEG, leg_name);
        EffectPlayProcessor component = Object.op_Inequality((Object) playerResourceCache, (Object) null) ? playerResourceCache.GetComponent<EffectPlayProcessor>() : (EffectPlayProcessor) null;
        if (Object.op_Inequality((Object) component, (Object) null) && component.effectSettings != null)
        {
          int index = 0;
          for (int length = component.effectSettings.Length; index < length; ++index)
          {
            if (!string.IsNullOrEmpty(component.effectSettings[index].effectName))
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, component.effectSettings[index].effectName);
          }
        }
      }
      if (load_queue.IsLoading())
        yield return (object) load_queue.Wait();
      bool wait = false;
      bool div_frame_realizes = false;
      int skin_color = info.skinColor;
      if (lo_body != null)
      {
        if (!div_frame_realizes)
        {
          this.body = lo_body.Realizes(_this);
          if (Object.op_Equality((Object) this.body, (Object) null))
            yield break;
          this.renderersBody = ((Component) this.body).gameObject.GetComponentsInChildren<Renderer>();
          ModelLoaderBase.SetEnabled(this.renderersBody, false);
          this.SetDynamicBones_Body(this.body, enableBone);
        }
        else
        {
          wait = true;
          InstantiateManager.Request((Object) this, lo_body.loadedObject, (Action<InstantiateManager.InstantiateData>) (data =>
          {
            this.body = ((GameObject) data.instantiatedObject).transform;
            this.body.SetParent(_this, false);
            this.renderersBody = ((Component) this.body).GetComponentsInChildren<Renderer>();
            this.SetDynamicBones_Body(this.body, enableBone);
            PlayerLoader.SetRenderersEnabled(this.renderersBody, false);
            wait = false;
          }));
          while (wait)
            yield return (object) null;
        }
      }
      else if (GoGameCacheManager.HasCacheObj(body_name))
      {
        Transform transform = GoGameCacheManager.RetrieveObj(body_name);
        if (Object.op_Inequality((Object) transform, (Object) null))
        {
          this.body = transform;
          this.body.SetParent(_this, false);
          ((Component) this.body).transform.localPosition = Vector3.zero;
          ((Component) this.body).transform.localRotation = Quaternion.identity;
          this.renderersBody = ((Component) this.body).gameObject.GetComponentsInChildren<Renderer>();
          ModelLoaderBase.SetEnabled(this.renderersBody, false);
          this.SetDynamicBones_Body(this.body, enableBone);
        }
      }
      else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_BDY, body_name))
      {
        GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_BDY, body_name);
        if (Object.op_Inequality((Object) playerResourceCache, (Object) null))
        {
          if (!div_frame_realizes)
          {
            this.body = LoadObject.RealizesWithGameObject(playerResourceCache, _this);
            this.renderersBody = ((Component) this.body).gameObject.GetComponentsInChildren<Renderer>();
            ModelLoaderBase.SetEnabled(this.renderersBody, false);
            this.SetDynamicBones_Body(this.body, enableBone);
          }
          else
          {
            wait = true;
            InstantiateManager.Request((Object) this, (Object) playerResourceCache, (Action<InstantiateManager.InstantiateData>) (data =>
            {
              this.body = ((GameObject) data.instantiatedObject).transform;
              this.body.SetParent(_this, false);
              this.renderersBody = ((Component) this.body).GetComponentsInChildren<Renderer>();
              this.SetDynamicBones_Body(this.body, enableBone);
              PlayerLoader.SetRenderersEnabled(this.renderersBody, false);
              wait = false;
            }));
            while (wait)
              yield return (object) null;
          }
        }
      }
      if (!Object.op_Equality((Object) this.body, (Object) null))
      {
        yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, this.body, shader_type));
        if (this.renderersBody != null)
        {
          if (this.renderersBody.Length != 0)
          {
            SkinnedMeshRenderer skinnedMeshRenderer = this.renderersBody[0] as SkinnedMeshRenderer;
            if (Object.op_Inequality((Object) skinnedMeshRenderer, (Object) null))
              skinnedMeshRenderer.localBounds = PlayerLoader.BOUNDS;
          }
          PlayerLoader.SetSkinAndEquipColor(this.renderersBody, skin_color, info.bodyColor, 0.0f);
          PlayerLoader.ApplyEquipHighResoTexs(lo_hr_bdy_tex, this.renderersBody);
          if (Object.op_Inequality((Object) player, (Object) null))
            player.body = this.body;
          this.socketRoot = Utility.Find(this.body, "Root");
          this.socketHead = Utility.Find(this.body, "Head");
          this.socketWepL = Utility.Find(this.body, "L_Wep");
          this.socketWepR = Utility.Find(this.body, "R_Wep");
          this.socketFootL = Utility.Find(this.body, "L_Foot");
          this.socketFootR = Utility.Find(this.body, "R_Foot");
          this.socketHandL = Utility.Find(this.body, "L_Hand");
          this.socketForearmL = Utility.Find(this.body, "L_Forearm");
          if (need_foot_stamp)
          {
            if (Object.op_Inequality((Object) this.socketFootL, (Object) null) && Object.op_Equality((Object) ((Component) this.socketFootL).GetComponent<StampNode>(), (Object) null))
            {
              StampNode stampNode = ((Component) this.socketFootL).gameObject.AddComponent<StampNode>();
              stampNode.offset = new Vector3(-0.08f, 0.01f, 0.0f);
              stampNode.autoBaseY = 0.1f;
            }
            if (Object.op_Inequality((Object) this.socketFootR, (Object) null) && Object.op_Equality((Object) ((Component) this.socketFootR).GetComponent<StampNode>(), (Object) null))
            {
              StampNode stampNode = ((Component) this.socketFootR).gameObject.AddComponent<StampNode>();
              stampNode.offset = new Vector3(-0.08f, 0.01f, 0.0f);
              stampNode.autoBaseY = 0.1f;
            }
            CharacterStampCtrl characterStampCtrl = ((Component) this.body).GetComponent<CharacterStampCtrl>();
            if (Object.op_Equality((Object) characterStampCtrl, (Object) null))
              characterStampCtrl = ((Component) this.body).gameObject.AddComponent<CharacterStampCtrl>();
            characterStampCtrl.Init(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos, (Character) player);
            int index = 0;
            for (int length = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos.Length; index < length; ++index)
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos[index].effectName);
            if (load_queue.IsLoading())
              yield return (object) load_queue.Wait();
          }
          bool isLoading_Face = true;
          bool isLoading_Hair = true;
          bool isLoading_Head = true;
          bool isLoading_Arm = true;
          bool isLoading_Foot = true;
          bool isLoading_Weapn = true;
          bool kqLoading_Face = false;
          bool kqLoading_Hair = false;
          bool kqLoading_Head = false;
          bool kqLoading_Arm = false;
          bool kqLoading_Foot = false;
          bool kqLoading_Weapn = false;
          bool isSoulArrowOutGameEffect = false;
          if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && info.equipType == 5U && info.weaponSpAttackType == 2U)
            isSoulArrowOutGameEffect = true;
          this.StartCoroutine(this.DoLoadFace(lo_face, face_name, skin_color, enable_eye_blick, div_frame_realizes, (Action<bool>) (kq =>
          {
            isLoading_Face = false;
            kqLoading_Face = kq;
          })));
          this.StartCoroutine(this.DoLoadHair(lo_hair, loHairOverlay, hair_name, info, enableBone, skin_color, div_frame_realizes, (Action<bool>) (kq =>
          {
            isLoading_Hair = false;
            kqLoading_Hair = kq;
          })));
          this.StartCoroutine(this.DoLoadHead(load_queue, lo_head, lo_hr_hed_tex, head_name, info, shader_type, div_frame_realizes, (Action<bool>) (kq =>
          {
            isLoading_Head = false;
            kqLoading_Head = kq;
          })));
          this.StartCoroutine(this.DoLoadArm(lo_arm, lo_hr_arm_tex, arm_name, skin_color, info, arm_model_data, div_frame_realizes, (Action<bool>) (kq =>
          {
            isLoading_Arm = false;
            kqLoading_Arm = kq;
          })));
          this.StartCoroutine(this.DoLoadFoot(lo_leg, lo_hr_leg_tex, leg_name, skin_color, info, leg_model_data, div_frame_realizes, (Action<bool>) (kq =>
          {
            isLoading_Foot = false;
            kqLoading_Foot = kq;
          })));
          this.StartCoroutine(this.DoLoadWeapon(load_queue, lo_wepn, lo_hr_wep_tex, wepn_name, isSoulArrowOutGameEffect, high_reso_tex_flags, player, info, shader_type, div_frame_realizes, (Action<bool>) (kq =>
          {
            isLoading_Weapn = false;
            kqLoading_Weapn = kq;
          })));
          while (isLoading_Face | isLoading_Hair | isLoading_Head | isLoading_Arm | isLoading_Foot | isLoading_Weapn)
            yield return (object) null;
          if (kqLoading_Face && kqLoading_Hair && kqLoading_Head && kqLoading_Arm && kqLoading_Foot && kqLoading_Weapn)
          {
            this.animator = ((Component) this.body).GetComponentInChildren<Animator>();
            if (Object.op_Inequality((Object) this.animator, (Object) null) && lo_anim != null)
            {
              RuntimeAnimatorController animatorController = lo_anim.loadedObjects[0].obj as RuntimeAnimatorController;
              if (Object.op_Inequality((Object) animatorController, (Object) null))
              {
                this.animator.runtimeAnimatorController = animatorController;
                if (Object.op_Inequality((Object) player, (Object) null))
                {
                  StageObjectProxy stageObjectProxy = ((Component) this.animator).gameObject.GetComponent<StageObjectProxy>();
                  if (Object.op_Equality((Object) stageObjectProxy, (Object) null))
                    stageObjectProxy = ((Component) this.animator).gameObject.AddComponent<StageObjectProxy>();
                  stageObjectProxy.stageObject = (StageObject) player;
                  if (need_anim_event)
                    player.animEventData = lo_anim.loadedObjects[1].obj as AnimEventData;
                }
                this.animator.updateMode = (AnimatorUpdateMode) 1;
                if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.isPlaySpAttackTypeMotion)
                {
                  SP_ATTACK_TYPE weaponSpAttackType = (SP_ATTACK_TYPE) info.weaponSpAttackType;
                  if (weaponSpAttackType != SP_ATTACK_TYPE.NONE)
                  {
                    string str = weaponSpAttackType.ToString();
                    int parameterCount = this.animator.parameterCount;
                    for (int index = 0; index < parameterCount; ++index)
                    {
                      if (this.animator.GetParameter(index).name == str)
                      {
                        this.animator.SetTrigger(str);
                        if (MonoBehaviourSingleton<EffectManager>.IsValid() & isSoulArrowOutGameEffect)
                        {
                          EffectManager.GetEffect("ef_btl_wsk2_bow_01_01", this.socketWepR);
                          break;
                        }
                        break;
                      }
                    }
                  }
                }
              }
            }
            if (lo_voices != null)
            {
              this.voiceAudioClips = lo_voices.loadedObjects;
              this.UpdateVoiceAudioClipIds();
            }
            if (!lo_accessories.IsNullOrEmpty<LoadObject>())
            {
              List<Renderer> accRendererList = new List<Renderer>();
              skill_len = 0;
              for (i = lo_accessories.Count; skill_len < i; ++skill_len)
              {
                LoadObject loadObject10 = lo_accessories[skill_len];
                Transform accTrans = (Transform) null;
                if (!div_frame_realizes)
                {
                  accTrans = loadObject10.Realizes();
                }
                else
                {
                  wait = true;
                  InstantiateManager.Request((Object) this, loadObject10.loadedObject, (Action<InstantiateManager.InstantiateData>) (data =>
                  {
                    accTrans = ((GameObject) data.instantiatedObject).transform;
                    wait = false;
                  }));
                  while (wait)
                    yield return (object) null;
                }
                if (Object.op_Inequality((Object) accTrans, (Object) null))
                {
                  AccessoryTable.AccessoryInfoData infoData = Singleton<AccessoryTable>.I.GetInfoData(info.accUIDs[skill_len]);
                  accTrans.SetParent(this.GetNodeTrans(infoData.node));
                  accTrans.localPosition = infoData.offset;
                  accTrans.localRotation = infoData.rotation;
                  accTrans.localScale = infoData.scale;
                  this.accessory.Add(accTrans);
                  accRendererList.AddRange((IEnumerable<Renderer>) ((Component) accTrans).GetComponentsInChildren<Renderer>());
                }
              }
              if (!this.accessory.IsNullOrEmpty<Transform>())
              {
                i = 0;
                for (skill_len = this.accessory.Count; i < skill_len; ++i)
                {
                  Transform equipItemRoot = this.accessory[i];
                  yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, equipItemRoot, shader_type));
                }
              }
              this.renderersAccessory = accRendererList.ToArray();
              ModelLoaderBase.SetEnabled(this.renderersAccessory, false);
              accRendererList = (List<Renderer>) null;
            }
            switch (shader_type)
            {
              case SHADER_TYPE.LIGHTWEIGHT:
                ShaderGlobal.ChangeWantLightweightShader(this.renderersWep);
                ShaderGlobal.ChangeWantLightweightShader(this.renderersFace);
                ShaderGlobal.ChangeWantLightweightShader(this.renderersHair);
                ShaderGlobal.ChangeWantLightweightShader(this.renderersBody);
                ShaderGlobal.ChangeWantLightweightShader(this.renderersHead);
                ShaderGlobal.ChangeWantLightweightShader(this.renderersArm);
                ShaderGlobal.ChangeWantLightweightShader(this.renderersLeg);
                ShaderGlobal.ChangeWantLightweightShader(this.renderersAccessory);
                break;
              case SHADER_TYPE.UI:
                ShaderGlobal.ChangeWantUIShader(this.renderersWep);
                ShaderGlobal.ChangeWantUIShader(this.renderersFace);
                ShaderGlobal.ChangeWantUIShader(this.renderersHair);
                ShaderGlobal.ChangeWantUIShader(this.renderersBody);
                ShaderGlobal.ChangeWantUIShader(this.renderersHead);
                ShaderGlobal.ChangeWantUIShader(this.renderersArm);
                ShaderGlobal.ChangeWantUIShader(this.renderersLeg);
                ShaderGlobal.ChangeWantUIShader(this.renderersAccessory);
                break;
            }
            this.SetLightProbes(enable_light_probes);
            if (layer != -1)
              PlayerLoader.SetLayerWithChildren_SecondaryNoChange(_this, layer);
            PlayerLoader.SetRenderersEnabled(this.renderersWep, true);
            PlayerLoader.SetRenderersEnabled(this.renderersFace, true);
            PlayerLoader.SetRenderersEnabled(this.renderersHair, true);
            PlayerLoader.SetRenderersEnabled(this.renderersBody, true);
            PlayerLoader.SetRenderersEnabled(this.renderersHead, true);
            PlayerLoader.SetRenderersEnabled(this.renderersArm, true);
            PlayerLoader.SetRenderersEnabled(this.renderersLeg, true);
            PlayerLoader.SetRenderersEnabled(this.renderersAccessory, true);
            if (need_shadow && Object.op_Equality((Object) this.shadow, (Object) null))
              this.shadow = PlayerLoader.CreateShadow(_this, is_lightweight: shader_type == SHADER_TYPE.LIGHTWEIGHT);
            if (Object.op_Inequality((Object) player, (Object) null))
            {
              if (Object.op_Inequality((Object) player.controller, (Object) null))
                ((Behaviour) player.controller).enabled = true;
              player.OnLoadComplete();
              if (Object.op_Inequality((Object) player.packetReceiver, (Object) null))
                player.packetReceiver.SetStopPacketUpdate(false);
            }
            if (Object.op_Inequality((Object) player, (Object) null) & is_self && MonoBehaviourSingleton<AudioListenerManager>.IsValid())
              MonoBehaviourSingleton<AudioListenerManager>.I.SetTargetObject((StageObject) player);
            if (callback != null)
              callback((object) player);
            this.ResetDynamicBones(this.dynamicBones);
            this.ResetDynamicBones(this.dynamicBones_Body);
            if (is_self)
            {
              ResourceLoad component = ((Component) player).gameObject.GetComponent<ResourceLoad>();
              if (Object.op_Inequality((Object) component, (Object) null) && component.list != null)
              {
                List<string> stringList = new List<string>();
                int index = 0;
                for (int size = component.list.size; index < size; ++index)
                  stringList.Add(component.list.buffer[index].name);
                stringList.Distinct<string>();
                MonoBehaviourSingleton<ResourceManager>.I.cache.AddIgnoreCategorySpecifiedReleaseList(stringList);
              }
            }
            this.isLoading = false;
            if (is_self)
              MonoBehaviourSingleton<GoGameCacheManager>.I.SaveCacheSelfPlayerModel(player as Self);
            if (gg_op)
              this.DoLoadLater(need_anim_event);
          }
        }
      }
    }
  }

  protected virtual IEnumerator DoReLoad_GG_Optimize()
  {
    yield return (object) null;
  }

  protected void DoLoadLater(bool need_anim_event)
  {
    if (!Object.op_Inequality((Object) ((Component) this).gameObject.GetComponent<Player>(), (Object) null))
      return;
    if (!MonoBehaviourSingleton<EffectSubLoader>.IsValid())
      EffectSubLoader.CreateInstance();
    if (!need_anim_event)
      return;
    this.animObjectTable.ForEach((Action<LoadObject>) (load_object => MonoBehaviourSingleton<EffectSubLoader>.I.CacheAnimDataUseResource(load_object.loadedObjects[1].obj as AnimEventData, (LoadingQueue.EffectNameAnalyzer) (effect_name => effect_name[0] != '@' ? effect_name : (string) null))));
    MonoBehaviourSingleton<EffectSubLoader>.I.StartLoad();
  }

  protected IEnumerator DoLoadFace(
    LoadObject lo_face,
    string face_name,
    int skin_color,
    bool enable_eye_blick,
    bool div_frame_realizes,
    Action<bool> callback)
  {
    bool wait = false;
    if (lo_face != null)
    {
      if (!div_frame_realizes)
      {
        this.face = lo_face.Realizes(this.socketHead);
        if (Object.op_Equality((Object) this.face, (Object) null))
        {
          callback(false);
          yield break;
        }
        this.renderersFace = ((Component) this.face).gameObject.GetComponentsInChildren<Renderer>();
        ModelLoaderBase.SetEnabled(this.renderersFace, false);
      }
      else
      {
        wait = true;
        InstantiateManager.Request((Object) this, lo_face.loadedObject, (Action<InstantiateManager.InstantiateData>) (data =>
        {
          this.face = ((GameObject) data.instantiatedObject).transform;
          this.face.SetParent(this.socketHead, false);
          this.renderersFace = ((Component) this.face).GetComponentsInChildren<Renderer>();
          PlayerLoader.SetRenderersEnabled(this.renderersFace, false);
          wait = false;
        }));
        while (wait)
          yield return (object) null;
        if (this.renderersFace == null)
        {
          callback(false);
          yield break;
        }
      }
      PlayerLoader.SetSkinColor(this.renderersFace, skin_color);
      this.validFaceChange = this.renderersFace != null && this.renderersFace.Length != 0 && this.renderersFace[0].material.HasProperty("_Face_shift");
      this.eyeBlink = enable_eye_blick;
    }
    else if (GoGameCacheManager.HasCacheObj(face_name))
    {
      this.face = GoGameCacheManager.RetrieveObj(face_name, this.socketHead);
      this.face.SetParent(this.socketHead, false);
      ((Component) this.face).transform.localPosition = Vector3.zero;
      ((Component) this.face).transform.localRotation = Quaternion.identity;
      this.renderersFace = ((Component) this.face).gameObject.GetComponentsInChildren<Renderer>();
      ModelLoaderBase.SetEnabled(this.renderersFace, false);
      PlayerLoader.SetSkinColor(this.renderersFace, skin_color);
      this.validFaceChange = this.renderersFace != null && this.renderersFace.Length != 0 && this.renderersFace[0].material.HasProperty("_Face_shift");
      this.eyeBlink = enable_eye_blick;
    }
    else
    {
      GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_FACE, face_name);
      if (Object.op_Inequality((Object) playerResourceCache, (Object) null))
      {
        if (!div_frame_realizes)
        {
          this.face = LoadObject.RealizesWithGameObject(playerResourceCache, this.socketHead);
          this.renderersFace = ((Component) this.face).gameObject.GetComponentsInChildren<Renderer>();
          ModelLoaderBase.SetEnabled(this.renderersFace, false);
        }
        else
        {
          wait = true;
          InstantiateManager.Request((Object) this, (Object) playerResourceCache, (Action<InstantiateManager.InstantiateData>) (data =>
          {
            this.face = ((GameObject) data.instantiatedObject).transform;
            this.face.SetParent(this.socketHead, false);
            this.renderersFace = ((Component) this.face).GetComponentsInChildren<Renderer>();
            PlayerLoader.SetRenderersEnabled(this.renderersFace, false);
            wait = false;
          }));
          while (wait)
            yield return (object) null;
          if (this.renderersFace == null)
          {
            callback(false);
            yield break;
          }
        }
        PlayerLoader.SetSkinColor(this.renderersFace, skin_color);
        this.validFaceChange = this.renderersFace != null && this.renderersFace.Length != 0 && this.renderersFace[0].material.HasProperty("_Face_shift");
        this.eyeBlink = enable_eye_blick;
      }
    }
    callback(true);
  }

  protected IEnumerator DoLoadHair(
    LoadObject lo_hair,
    LoadObject loHairOverlay,
    string hair_name,
    PlayerLoadInfo info,
    bool enableBone,
    int skin_color,
    bool div_frame_realizes,
    Action<bool> callback)
  {
    bool wait = false;
    if (lo_hair != null)
    {
      if (!div_frame_realizes)
      {
        this.hair = lo_hair.Realizes(this.socketHead);
        if (Object.op_Equality((Object) this.hair, (Object) null))
        {
          callback(false);
          yield break;
        }
        this.renderersHair = ((Component) this.hair).GetComponentsInChildren<Renderer>();
        ModelLoaderBase.SetEnabled(this.renderersHair, false);
        this.SetDynamicBones(this.body, this.hair, enableBone);
      }
      else
      {
        wait = true;
        InstantiateManager.Request((Object) this, lo_hair.loadedObject, (Action<InstantiateManager.InstantiateData>) (data =>
        {
          this.hair = ((GameObject) data.instantiatedObject).transform;
          this.hair.SetParent(this.socketHead, false);
          this.renderersHair = ((Component) this.hair).GetComponentsInChildren<Renderer>();
          this.SetDynamicBones(this.body, this.hair, enableBone);
          PlayerLoader.SetRenderersEnabled(this.renderersHair, false);
          wait = false;
        }));
        while (wait)
          yield return (object) null;
        if (this.renderersHair == null)
        {
          callback(false);
          yield break;
        }
      }
      PlayerLoader.SetSkinAndEquipColor(this.renderersHair, skin_color, info.hairColor, 0.0f);
      if (loHairOverlay != null)
        PlayerLoader.ApplyHairOverlay(loHairOverlay, this.renderersHair);
    }
    else if (GoGameCacheManager.HasCacheObj(hair_name))
    {
      this.hair = GoGameCacheManager.RetrieveObj(hair_name, this.socketHead);
      this.hair.SetParent(this.socketHead, false);
      ((Component) this.hair).transform.localPosition = Vector3.zero;
      ((Component) this.hair).transform.localRotation = Quaternion.identity;
      this.renderersHair = ((Component) this.hair).GetComponentsInChildren<Renderer>();
      ModelLoaderBase.SetEnabled(this.renderersHair, false);
      this.SetDynamicBones(this.body, this.hair, enableBone);
      PlayerLoader.SetSkinAndEquipColor(this.renderersHair, skin_color, info.hairColor, 0.0f);
      if (loHairOverlay != null)
        PlayerLoader.ApplyHairOverlay(loHairOverlay, this.renderersHair);
    }
    else
    {
      GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name);
      if (Object.op_Inequality((Object) playerResourceCache, (Object) null))
      {
        if (!div_frame_realizes)
        {
          this.hair = LoadObject.RealizesWithGameObject(playerResourceCache, this.socketHead);
          this.renderersHair = ((Component) this.hair).GetComponentsInChildren<Renderer>();
          ModelLoaderBase.SetEnabled(this.renderersHair, false);
          this.SetDynamicBones(this.body, this.hair, enableBone);
        }
        else
        {
          wait = true;
          InstantiateManager.Request((Object) this, (Object) playerResourceCache, (Action<InstantiateManager.InstantiateData>) (data =>
          {
            this.hair = ((GameObject) data.instantiatedObject).transform;
            this.hair.SetParent(this.socketHead, false);
            this.renderersHair = ((Component) this.hair).GetComponentsInChildren<Renderer>();
            this.SetDynamicBones(this.body, this.hair, enableBone);
            PlayerLoader.SetRenderersEnabled(this.renderersHair, false);
            wait = false;
          }));
          while (wait)
            yield return (object) null;
          if (this.renderersHair == null)
          {
            callback(false);
            yield break;
          }
        }
        PlayerLoader.SetSkinAndEquipColor(this.renderersHair, skin_color, info.hairColor, 0.0f);
        if (loHairOverlay != null)
          PlayerLoader.ApplyHairOverlay(loHairOverlay, this.renderersHair);
      }
    }
    callback(true);
  }

  protected IEnumerator DoLoadHead(
    LoadingQueue load_queue,
    LoadObject lo_head,
    LoadObject lo_hr_hed_tex,
    string head_name,
    PlayerLoadInfo info,
    SHADER_TYPE shader_type,
    bool div_frame_realizes,
    Action<bool> callback)
  {
    bool wait = false;
    if (lo_head != null)
    {
      if (!div_frame_realizes)
      {
        this.head = lo_head.Realizes(this.socketHead);
        if (Object.op_Inequality((Object) this.head, (Object) null))
        {
          this.renderersHead = ((Component) this.head).GetComponentsInChildren<Renderer>();
          ModelLoaderBase.SetEnabled(this.renderersHead, false);
        }
      }
      else
      {
        wait = true;
        InstantiateManager.Request((Object) this, lo_head.loadedObject, (Action<InstantiateManager.InstantiateData>) (data =>
        {
          this.head = ((GameObject) data.instantiatedObject).transform;
          this.head.SetParent(this.socketHead, false);
          this.renderersHead = ((Component) this.head).GetComponentsInChildren<Renderer>();
          PlayerLoader.SetRenderersEnabled(this.renderersHead, false);
          wait = false;
        }));
        while (wait)
          yield return (object) null;
        if (this.renderersHead == null)
        {
          callback(false);
          yield break;
        }
      }
      if (Object.op_Inequality((Object) this.head, (Object) null))
        yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, this.head, shader_type));
      PlayerLoader.SetEquipColor(this.renderersHead, info.headColor);
      PlayerLoader.ApplyEquipHighResoTexs(lo_hr_hed_tex, this.renderersHead);
    }
    else if (GoGameCacheManager.HasCacheObj(head_name))
    {
      this.head = GoGameCacheManager.RetrieveObj(head_name, this.socketHead);
      ((Component) this.head).transform.localPosition = Vector3.zero;
      ((Component) this.head).transform.localRotation = Quaternion.identity;
      this.renderersHead = ((Component) this.head).GetComponentsInChildren<Renderer>();
      if (div_frame_realizes)
        ModelLoaderBase.SetEnabled(this.renderersHead, false);
      else
        PlayerLoader.SetRenderersEnabled(this.renderersHead, false);
      if (Object.op_Inequality((Object) this.head, (Object) null))
        yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, this.head, shader_type));
      PlayerLoader.SetEquipColor(this.renderersHead, info.headColor);
      PlayerLoader.ApplyEquipHighResoTexs(lo_hr_hed_tex, this.renderersHead);
    }
    else
    {
      GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_HEAD, head_name);
      if (Object.op_Inequality((Object) playerResourceCache, (Object) null))
      {
        if (!div_frame_realizes)
        {
          this.head = LoadObject.RealizesWithGameObject(playerResourceCache, this.socketHead);
          if (Object.op_Inequality((Object) this.head, (Object) null))
          {
            this.renderersHead = ((Component) this.head).GetComponentsInChildren<Renderer>();
            ModelLoaderBase.SetEnabled(this.renderersHead, false);
          }
        }
        else
        {
          wait = true;
          InstantiateManager.Request((Object) this, (Object) playerResourceCache, (Action<InstantiateManager.InstantiateData>) (data =>
          {
            this.head = ((GameObject) data.instantiatedObject).transform;
            this.head.SetParent(this.socketHead, false);
            this.renderersHead = ((Component) this.head).GetComponentsInChildren<Renderer>();
            PlayerLoader.SetRenderersEnabled(this.renderersHead, false);
            wait = false;
          }));
          while (wait)
            yield return (object) null;
          if (this.renderersHead == null)
          {
            callback(false);
            yield break;
          }
        }
        if (Object.op_Inequality((Object) this.head, (Object) null))
          yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, this.head, shader_type));
        PlayerLoader.SetEquipColor(this.renderersHead, info.headColor);
        PlayerLoader.ApplyEquipHighResoTexs(lo_hr_hed_tex, this.renderersHead);
      }
    }
    callback(true);
  }

  protected IEnumerator DoLoadArm(
    LoadObject lo_arm,
    LoadObject lo_hr_arm_tex,
    string arm_name,
    int skin_color,
    PlayerLoadInfo info,
    EquipModelTable.Data arm_model_data,
    bool div_frame_realizes,
    Action<bool> callback)
  {
    bool wait = false;
    if (lo_arm != null)
    {
      if (!div_frame_realizes)
      {
        this.arm = this.AddSkin(lo_arm);
        if (Object.op_Inequality((Object) this.arm, (Object) null))
        {
          this.renderersArm = ((Component) this.arm).GetComponentsInChildren<Renderer>();
          ModelLoaderBase.SetEnabled(this.renderersArm, false);
        }
      }
      else
      {
        wait = true;
        InstantiateManager.Request((Object) this, lo_arm.loadedObject, (Action<InstantiateManager.InstantiateData>) (data =>
        {
          this.arm = ((GameObject) data.instantiatedObject).transform;
          this.arm = this.AddSkin(this.arm);
          this.renderersArm = ((Component) this.arm).GetComponentsInChildren<Renderer>();
          PlayerLoader.SetRenderersEnabled(this.renderersArm, false);
          wait = false;
        }));
        while (wait)
          yield return (object) null;
        if (this.renderersArm == null)
        {
          callback(false);
          yield break;
        }
      }
      if (Object.op_Inequality((Object) this.arm, (Object) null))
      {
        PlayerLoader.SetSkinAndEquipColor(this.renderersArm, skin_color, info.armColor, arm_model_data.GetZBias());
        PlayerLoader.ApplyEquipHighResoTexs(lo_hr_arm_tex, this.renderersArm);
        this.InvisibleBodyTriangles((int) arm_model_data.bodyDraw);
      }
    }
    else if (GoGameCacheManager.HasCacheObj(arm_name))
    {
      this.arm = GoGameCacheManager.RetrieveObj(arm_name, ((Component) this).transform);
      if (Object.op_Inequality((Object) this.arm, (Object) null))
      {
        this.arm = this.AddSkin(this.arm);
        this.renderersArm = ((Component) this.arm).GetComponentsInChildren<Renderer>();
        PlayerLoader.SetRenderersEnabled(this.renderersArm, false);
        PlayerLoader.SetSkinAndEquipColor(this.renderersArm, skin_color, info.armColor, arm_model_data.GetZBias());
        PlayerLoader.ApplyEquipHighResoTexs(lo_hr_arm_tex, this.renderersArm);
        ((Component) this.arm).transform.localPosition = Vector3.zero;
        ((Component) this.arm).transform.localRotation = Quaternion.identity;
        this.InvisibleBodyTriangles((int) arm_model_data.bodyDraw);
      }
    }
    else
    {
      GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ARM, arm_name);
      if (Object.op_Implicit((Object) playerResourceCache))
      {
        if (!div_frame_realizes)
        {
          this.arm = this.AddSkinFromCache(playerResourceCache);
          if (Object.op_Inequality((Object) this.arm, (Object) null))
          {
            this.renderersArm = ((Component) this.arm).GetComponentsInChildren<Renderer>();
            ModelLoaderBase.SetEnabled(this.renderersArm, false);
          }
        }
        else
        {
          wait = true;
          InstantiateManager.Request((Object) this, (Object) playerResourceCache, (Action<InstantiateManager.InstantiateData>) (data =>
          {
            this.arm = ((GameObject) data.instantiatedObject).transform;
            this.arm = this.AddSkin(this.arm);
            this.renderersArm = ((Component) this.arm).GetComponentsInChildren<Renderer>();
            PlayerLoader.SetRenderersEnabled(this.renderersArm, false);
            wait = false;
          }));
          while (wait)
            yield return (object) null;
          if (this.renderersArm == null)
          {
            callback(false);
            yield break;
          }
        }
        if (Object.op_Inequality((Object) this.arm, (Object) null))
        {
          PlayerLoader.SetSkinAndEquipColor(this.renderersArm, skin_color, info.armColor, arm_model_data.GetZBias());
          PlayerLoader.ApplyEquipHighResoTexs(lo_hr_arm_tex, this.renderersArm);
          this.InvisibleBodyTriangles((int) arm_model_data.bodyDraw);
        }
      }
    }
    callback(true);
  }

  protected IEnumerator DoLoadFoot(
    LoadObject lo_leg,
    LoadObject lo_hr_leg_tex,
    string leg_name,
    int skin_color,
    PlayerLoadInfo info,
    EquipModelTable.Data leg_model_data,
    bool div_frame_realizes,
    Action<bool> callback)
  {
    bool wait = false;
    if (lo_leg != null)
    {
      if (!div_frame_realizes)
      {
        this.leg = this.AddSkin(lo_leg);
        if (Object.op_Inequality((Object) this.leg, (Object) null))
        {
          this.renderersLeg = ((Component) this.leg).GetComponentsInChildren<Renderer>();
          ModelLoaderBase.SetEnabled(this.renderersLeg, false);
        }
      }
      else
      {
        wait = true;
        InstantiateManager.Request((Object) this, lo_leg.loadedObject, (Action<InstantiateManager.InstantiateData>) (data =>
        {
          this.leg = ((GameObject) data.instantiatedObject).transform;
          this.leg = this.AddSkin(this.leg);
          this.renderersLeg = ((Component) this.leg).GetComponentsInChildren<Renderer>();
          PlayerLoader.SetRenderersEnabled(this.renderersLeg, false);
          wait = false;
        }));
        while (wait)
          yield return (object) null;
        if (this.renderersLeg == null)
        {
          callback(false);
          yield break;
        }
      }
      if (Object.op_Inequality((Object) this.leg, (Object) null))
      {
        PlayerLoader.SetSkinAndEquipColor(this.renderersLeg, skin_color, info.legColor, leg_model_data.GetZBias());
        PlayerLoader.ApplyEquipHighResoTexs(lo_hr_leg_tex, this.renderersLeg);
      }
    }
    else if (GoGameCacheManager.HasCacheObj(leg_name))
    {
      this.leg = GoGameCacheManager.RetrieveObj(leg_name, ((Component) this).transform);
      this.leg = this.AddSkin(this.leg);
      if (Object.op_Inequality((Object) this.leg, (Object) null))
      {
        ((Component) this.leg).transform.localPosition = Vector3.zero;
        ((Component) this.leg).transform.localRotation = Quaternion.identity;
        this.renderersLeg = ((Component) this.leg).GetComponentsInChildren<Renderer>();
        PlayerLoader.SetRenderersEnabled(this.renderersLeg, false);
        PlayerLoader.SetSkinAndEquipColor(this.renderersLeg, skin_color, info.legColor, leg_model_data.GetZBias());
        PlayerLoader.ApplyEquipHighResoTexs(lo_hr_leg_tex, this.renderersLeg);
      }
    }
    else
    {
      GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_LEG, leg_name);
      if (Object.op_Implicit((Object) playerResourceCache))
      {
        if (!div_frame_realizes)
        {
          this.leg = this.AddSkinFromCache(playerResourceCache);
          if (Object.op_Inequality((Object) this.leg, (Object) null))
          {
            this.renderersLeg = ((Component) this.leg).GetComponentsInChildren<Renderer>();
            ModelLoaderBase.SetEnabled(this.renderersLeg, false);
          }
        }
        else
        {
          wait = true;
          InstantiateManager.Request((Object) this, (Object) playerResourceCache, (Action<InstantiateManager.InstantiateData>) (data =>
          {
            this.leg = ((GameObject) data.instantiatedObject).transform;
            this.leg = this.AddSkin(this.leg);
            this.renderersLeg = ((Component) this.leg).GetComponentsInChildren<Renderer>();
            PlayerLoader.SetRenderersEnabled(this.renderersLeg, false);
            wait = false;
          }));
          while (wait)
            yield return (object) null;
          if (this.renderersLeg == null)
          {
            callback(false);
            yield break;
          }
        }
        if (Object.op_Inequality((Object) this.leg, (Object) null))
        {
          PlayerLoader.SetSkinAndEquipColor(this.renderersLeg, skin_color, info.legColor, leg_model_data.GetZBias());
          PlayerLoader.ApplyEquipHighResoTexs(lo_hr_leg_tex, this.renderersLeg);
        }
      }
    }
    callback(true);
  }

  protected IEnumerator DoLoadWeapon(
    LoadingQueue load_queue,
    LoadObject lo_wepn,
    LoadObject lo_hr_wep_tex,
    string wepn_name,
    bool isSoulArrowOutGameEffect,
    int high_reso_tex_flags,
    Player player,
    PlayerLoadInfo info,
    SHADER_TYPE shader_type,
    bool div_frame_realizes,
    Action<bool> callback)
  {
    bool wait = false;
    if (lo_wepn != null)
    {
      Transform weapon = (Transform) null;
      if (!div_frame_realizes)
      {
        weapon = lo_wepn.Realizes();
        if (Object.op_Inequality((Object) weapon, (Object) null))
        {
          this.renderersWep = ((Component) weapon).gameObject.GetComponentsInChildren<Renderer>();
          ModelLoaderBase.SetEnabled(this.renderersWep, false);
        }
      }
      else
      {
        wait = true;
        InstantiateManager.Request((Object) this, lo_wepn.loadedObject, (Action<InstantiateManager.InstantiateData>) (data =>
        {
          weapon = ((GameObject) data.instantiatedObject).transform;
          this.renderersWep = ((Component) weapon).GetComponentsInChildren<Renderer>();
          PlayerLoader.SetRenderersEnabled(this.renderersWep, false);
          wait = false;
        }));
        while (wait)
          yield return (object) null;
        if (this.renderersWep == null)
        {
          callback(false);
          yield break;
        }
      }
      if (Object.op_Inequality((Object) weapon, (Object) null))
      {
        if (isSoulArrowOutGameEffect)
          load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_bow_01_01");
        yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, weapon, shader_type));
      }
      if (this.renderersBody == null)
      {
        callback(false);
        yield break;
      }
      if (Object.op_Inequality((Object) weapon, (Object) null))
        this.InitWeaponLinkBuffEffect(player, weapon);
      PlayerLoader.SetWeaponShader(this.renderersWep, info.weaponColor0, info.weaponColor1, info.weaponColor2, info.weaponEffectID, info.weaponEffectParam, info.weaponEffectColor);
      Material mate0 = (Material) null;
      Material mate1 = (Material) null;
      if (this.renderersWep != null)
      {
        int index = 0;
        for (int length = this.renderersWep.Length; index < length; ++index)
        {
          if (((Object) this.renderersWep[index]).name.EndsWith("_L"))
          {
            mate1 = this.renderersWep[index].material;
            this.wepL = ((Component) this.renderersWep[index]).transform;
            if (this.loadInfo.equipType == 0U && this.loadInfo.weaponSpAttackType == 2U)
              Utility.Attach(this.socketHandL, this.wepL);
            else
              Utility.Attach(this.socketWepL, this.wepL);
          }
          else
          {
            mate0 = this.renderersWep[index].material;
            this.wepR = ((Component) this.renderersWep[index]).transform;
            Utility.Attach(this.socketWepR, this.wepR);
          }
        }
      }
      if (Object.op_Inequality((Object) weapon, (Object) null))
        Object.DestroyImmediate((Object) ((Component) weapon).gameObject);
      if (lo_hr_wep_tex != null)
        PlayerLoader.ApplyWeaponHighResoTexs(lo_hr_wep_tex, high_reso_tex_flags, mate0, mate1);
    }
    else
    {
      GameObject playerResourceCache = (GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name);
      if (Object.op_Implicit((Object) playerResourceCache))
      {
        Transform weapon = (Transform) null;
        if (!div_frame_realizes)
        {
          weapon = LoadObject.RealizesWithGameObject(playerResourceCache);
          if (Object.op_Inequality((Object) weapon, (Object) null))
          {
            this.renderersWep = ((Component) weapon).gameObject.GetComponentsInChildren<Renderer>();
            ModelLoaderBase.SetEnabled(this.renderersWep, false);
          }
        }
        else
        {
          wait = true;
          InstantiateManager.Request((Object) this, (Object) playerResourceCache, (Action<InstantiateManager.InstantiateData>) (data =>
          {
            weapon = ((GameObject) data.instantiatedObject).transform;
            this.renderersWep = ((Component) weapon).GetComponentsInChildren<Renderer>();
            PlayerLoader.SetRenderersEnabled(this.renderersWep, false);
            wait = false;
          }));
          while (wait)
            yield return (object) null;
          if (this.renderersWep == null)
          {
            callback(false);
            yield break;
          }
        }
        if (Object.op_Inequality((Object) weapon, (Object) null))
        {
          if (isSoulArrowOutGameEffect)
            load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_bow_01_01");
          yield return (object) this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, weapon, shader_type));
        }
        if (this.renderersBody == null)
        {
          callback(false);
          yield break;
        }
        if (Object.op_Inequality((Object) weapon, (Object) null))
          this.InitWeaponLinkBuffEffect(player, weapon);
        PlayerLoader.SetWeaponShader(this.renderersWep, info.weaponColor0, info.weaponColor1, info.weaponColor2, info.weaponEffectID, info.weaponEffectParam, info.weaponEffectColor);
        Material mate0 = (Material) null;
        Material mate1 = (Material) null;
        if (this.renderersWep != null)
        {
          int index = 0;
          for (int length = this.renderersWep.Length; index < length; ++index)
          {
            if (((Object) this.renderersWep[index]).name.EndsWith("_L"))
            {
              mate1 = this.renderersWep[index].material;
              this.wepL = ((Component) this.renderersWep[index]).transform;
              if (this.loadInfo.equipType == 0U && this.loadInfo.weaponSpAttackType == 2U)
                Utility.Attach(this.socketHandL, this.wepL);
              else
                Utility.Attach(this.socketWepL, this.wepL);
            }
            else
            {
              mate0 = this.renderersWep[index].material;
              this.wepR = ((Component) this.renderersWep[index]).transform;
              Utility.Attach(this.socketWepR, this.wepR);
            }
          }
        }
        if (Object.op_Inequality((Object) weapon, (Object) null))
          Object.DestroyImmediate((Object) ((Component) weapon).gameObject);
        if (lo_hr_wep_tex != null)
          PlayerLoader.ApplyWeaponHighResoTexs(lo_hr_wep_tex, high_reso_tex_flags, mate0, mate1);
      }
    }
    callback(true);
  }

  private void AddSkillAttackInfoName(Player player, ref List<string> needAtkInfoNames)
  {
    int num = 3;
    for (int index1 = 0; index1 < num; ++index1)
    {
      SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + index1);
      if (skillParam != null)
      {
        SkillItemTable.SkillItemData tableData = skillParam.tableData;
        string[] attackInfoNames = tableData.attackInfoNames;
        for (int index2 = 0; index2 < attackInfoNames.Length; ++index2)
        {
          if (!string.IsNullOrEmpty(attackInfoNames[index2]))
            needAtkInfoNames.Add(attackInfoNames[index2]);
        }
        if ((int) tableData.healHp > 0 && (double) (float) tableData.skillRange > 0.0)
          needAtkInfoNames.Add("sk_heal_atk");
      }
    }
    if (player.shieldReflectInfo != null && !string.IsNullOrEmpty(player.shieldReflectInfo.attackInfoName))
      needAtkInfoNames.Add(player.shieldReflectInfo.attackInfoName);
    needAtkInfoNames.Add("sk_heal_atk_zone");
  }

  private void AddFieldGimmickAttackInfoName(ref List<string> needAtkInfoNames)
  {
    if (!MonoBehaviourSingleton<FieldManager>.IsValid() || !Singleton<FieldMapTable>.IsValid())
      return;
    List<FieldMapTable.FieldGimmickPointTableData> pointListByMapId = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
    if (pointListByMapId == null)
      return;
    for (int index = 0; index < pointListByMapId.Count; ++index)
    {
      switch (pointListByMapId[index].gimmickType)
      {
        case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON:
          foreach (string cannonAttackInfoName in ResourceName.GetCannonAttackInfoNames())
            needAtkInfoNames.Add(cannonAttackInfoName);
          break;
        case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.BOMBROCK:
          needAtkInfoNames.Add("bombrock");
          break;
        case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_HEAVY:
          needAtkInfoNames.Add("cannonball_heavy");
          break;
        case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_RAPID:
          needAtkInfoNames.Add("cannonball_rapid");
          break;
        case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_SPECIAL:
          needAtkInfoNames.Add(ResourceName.CANNONBALL_SPECIAL_ATTACK_INFO_NAME);
          break;
        case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_FIELD:
          needAtkInfoNames.Add(FieldGimmickCannonField.GetAttackInfoName(pointListByMapId[index].value2));
          break;
        case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_TURRET:
          needAtkInfoNames.AddRange((IEnumerable<string>) FieldCarriableTurretGimmickObject.GetAttackInfoNames(pointListByMapId[index].value2));
          break;
        case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_BOMB:
          needAtkInfoNames.Add(FieldCarriableBombGimmickObject.GetAttackInfoName(pointListByMapId[index].value2));
          break;
      }
    }
  }

  private IEnumerator LoadNextBulletInfo(
    LoadingQueue loadQueue,
    string infoName,
    List<AttackInfo> hitInfos,
    bool isNeedToCache)
  {
    if (!this.playerLoaderLoadedAttackInfoNames.Contains(infoName))
    {
      GameObject gameObject;
      if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, infoName))
      {
        LoadObject loadObj = loadQueue.Load(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, infoName);
        this.playerLoaderLoadedAttackInfoNames.Add(infoName);
        if (loadQueue.IsLoading())
          yield return (object) loadQueue.Wait();
        if (isNeedToCache)
          MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, infoName, loadObj.loadedObject);
        gameObject = ((Component) loadObj.Realizes(MonoBehaviourSingleton<InGameSettingsManager>.I._transform)).gameObject;
        loadObj = (LoadObject) null;
      }
      else
        gameObject = ((Component) LoadObject.RealizesWithGameObject((GameObject) MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, infoName), MonoBehaviourSingleton<InGameSettingsManager>.I._transform)).gameObject;
      SplitPlayerAttackInfo component = gameObject.GetComponent<SplitPlayerAttackInfo>();
      hitInfos.Add((AttackInfo) component.attackHitInfo);
      if (!string.IsNullOrEmpty(component.attackHitInfo.nextBulletInfoName))
        yield return (object) this.StartCoroutine(this.LoadNextBulletInfo(loadQueue, component.attackHitInfo.nextBulletInfoName, hitInfos, isNeedToCache));
    }
  }

  private void InitWeaponLinkBuffEffect(Player player, Transform weaponTrans)
  {
    if (Object.op_Equality((Object) player, (Object) null))
      return;
    EffectPlayProcessor componentInChildren = ((Component) weaponTrans).GetComponentInChildren<EffectPlayProcessor>();
    if (Object.op_Equality((Object) componentInChildren, (Object) null))
      return;
    EffectPlayProcessor.EffectSetting[] effectSettings = componentInChildren.effectSettings;
    if (effectSettings == null || effectSettings.Length == 0)
      return;
    int length = effectSettings.Length;
    for (int index = 0; index < length; ++index)
    {
      if (effectSettings[index].name.StartsWith("BUFF_LOOP_"))
        player.RegisterWeaponLinkEffect(effectSettings[index]);
    }
  }

  public void DeleteLoadedObjects()
  {
    if (Object.op_Inequality((Object) this.wepR, (Object) null))
      Object.DestroyImmediate((Object) ((Component) this.wepR).gameObject);
    if (Object.op_Inequality((Object) this.wepL, (Object) null))
      Object.DestroyImmediate((Object) ((Component) this.wepL).gameObject);
    if (Object.op_Inequality((Object) this.body, (Object) null))
      Object.DestroyImmediate((Object) ((Component) this.body).gameObject);
    if (Object.op_Inequality((Object) this.shadow, (Object) null))
      Object.DestroyImmediate((Object) ((Component) this.shadow).gameObject);
    this.loadInfo = (PlayerLoadInfo) null;
    this.wepR = (Transform) null;
    this.wepL = (Transform) null;
    this.body = (Transform) null;
    this.face = (Transform) null;
    this.hair = (Transform) null;
    this.head = (Transform) null;
    this.arm = (Transform) null;
    this.leg = (Transform) null;
    this.accessory.Clear();
    this.shadow = (Transform) null;
    this.animator = (Animator) null;
    this.hairPhysics = (Transform) null;
    this.renderersWep = (Renderer[]) null;
    this.renderersFace = (Renderer[]) null;
    this.renderersHair = (Renderer[]) null;
    this.renderersBody = (Renderer[]) null;
    this.renderersHead = (Renderer[]) null;
    this.renderersArm = (Renderer[]) null;
    this.renderersLeg = (Renderer[]) null;
    this.renderersAccessory = (Renderer[]) null;
    this.socketRoot = (Transform) null;
    this.socketHead = (Transform) null;
    this.socketWepL = (Transform) null;
    this.socketWepR = (Transform) null;
    this.socketFootL = (Transform) null;
    this.socketFootR = (Transform) null;
    this.socketHandL = (Transform) null;
    this.socketHandR = (Transform) null;
    this.socketForearmL = (Transform) null;
    this.socketForearmR = (Transform) null;
    this.validFaceChange = false;
    this.eyeBlink = false;
    this.isLoading = false;
  }

  private void Update()
  {
    if (!this.eyeBlink)
      return;
    this.UpdateEyeBlink();
  }

  private void UpdateEyeBlink()
  {
    if (!this.eyeBlink)
      return;
    this.eyeBlinkTime -= Time.deltaTime;
    if ((double) this.eyeBlinkTime > 0.0)
      return;
    if (this.faceID != PlayerLoader.FACE_ID.CLOSE_EYE)
    {
      this.ChangeFace(PlayerLoader.FACE_ID.CLOSE_EYE);
      this.eyeBlinkTime = Random.Range(0.1f, 0.3f);
    }
    else
    {
      this.ChangeFace(PlayerLoader.FACE_ID.NORMAL);
      this.eyeBlinkTime = Random.Range(3f, 6f);
    }
  }

  private bool _EnableDynamicBone(SHADER_TYPE shaderType, bool isSelf)
  {
    if (shaderType == SHADER_TYPE.LIGHTWEIGHT && !isSelf)
      return false;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
    {
      switch (MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.dynamicBoneType)
      {
        case DYNAMICBONE_TYPE.DISABLE:
          return false;
        case DYNAMICBONE_TYPE.DISABLE_LOW:
          if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 0)
            return false;
          break;
      }
    }
    return true;
  }

  public void SetDynamicBones_Body(Transform body, bool isEnable)
  {
    if (isEnable)
      return;
    if (this.dynamicBones_Body == null)
      this.dynamicBones_Body = new List<DynamicBone>();
    List<DynamicBone> dynamicBonesBody = this.dynamicBones_Body;
    dynamicBonesBody.Clear();
    ((Component) body).GetComponentsInChildren<DynamicBone>(true, dynamicBonesBody);
    if (dynamicBonesBody.Count <= 0)
      return;
    int index = 0;
    for (int count = dynamicBonesBody.Count; index < count; ++index)
    {
      Object.DestroyImmediate((Object) dynamicBonesBody[index]);
      dynamicBonesBody[index] = (DynamicBone) null;
    }
    dynamicBonesBody.Clear();
  }

  public void SetDynamicBones(Transform body, Transform hair, bool isEnable)
  {
    if (this.dynamicBones == null)
      this.dynamicBones = new List<DynamicBone>();
    List<DynamicBone> dynamicBones = this.dynamicBones;
    dynamicBones.Clear();
    ((Component) hair).GetComponentsInChildren<DynamicBone>(true, dynamicBones);
    if (dynamicBones.Count <= 0)
      return;
    if (isEnable)
    {
      Transform transform1 = Utility.Find(body, "Neck");
      DynamicBoneCollider dynamicBoneCollider1 = ((Component) transform1).gameObject.GetComponent<DynamicBoneCollider>();
      if (Object.op_Equality((Object) dynamicBoneCollider1, (Object) null))
        dynamicBoneCollider1 = ((Component) transform1).gameObject.AddComponent<DynamicBoneCollider>();
      dynamicBoneCollider1.m_Radius = 0.1f;
      dynamicBoneCollider1.m_Height = 0.39f;
      dynamicBoneCollider1.m_Direction = DynamicBoneCollider.Direction.Z;
      dynamicBoneCollider1.m_Center = new Vector3(0.06f, 0.02f, 0.0f);
      Transform transform2 = Utility.Find(transform1, "Head");
      DynamicBoneCollider dynamicBoneCollider2 = ((Component) transform2).gameObject.GetComponent<DynamicBoneCollider>();
      if (Object.op_Equality((Object) dynamicBoneCollider2, (Object) null))
        dynamicBoneCollider2 = ((Component) transform2).gameObject.AddComponent<DynamicBoneCollider>();
      dynamicBoneCollider2.m_Radius = 0.1f;
      dynamicBoneCollider2.m_Height = 0.3f;
      dynamicBoneCollider2.m_Direction = DynamicBoneCollider.Direction.Y;
      dynamicBoneCollider2.m_Center = new Vector3(0.0f, -0.01f, 0.0f);
      int index = 0;
      for (int count = dynamicBones.Count; index < count; ++index)
      {
        dynamicBones[index].m_Colliders.Add(dynamicBoneCollider1);
        dynamicBones[index].m_Colliders.Add(dynamicBoneCollider2);
      }
    }
    else
    {
      int index = 0;
      for (int count = dynamicBones.Count; index < count; ++index)
      {
        Object.DestroyImmediate((Object) dynamicBones[index]);
        dynamicBones[index] = (DynamicBone) null;
      }
    }
  }

  public void ResetDynamicBones(List<DynamicBone> list)
  {
    if (list == null)
      return;
    int index = 0;
    for (int count = list.Count; index < count; ++index)
    {
      DynamicBone dynamicBone = list[index];
      if (Object.op_Inequality((Object) dynamicBone, (Object) null) && ((Behaviour) dynamicBone).enabled)
      {
        ((Behaviour) dynamicBone).enabled = false;
        ((Behaviour) dynamicBone).enabled = true;
      }
    }
  }

  private Transform AddSkin(LoadObject lo)
  {
    return PlayerLoader.AddSkin(lo, this.renderersBody[0] as SkinnedMeshRenderer);
  }

  private Transform AddSkin(Transform base_model)
  {
    return PlayerLoader.AddSkin(base_model, this.renderersBody[0] as SkinnedMeshRenderer);
  }

  private Transform AddSkinFromCache(GameObject gameObj)
  {
    return Object.op_Equality((Object) gameObj, (Object) null) ? (Transform) null : PlayerLoader.AddSkin(LoadObject.RealizesWithGameObject(gameObj), this.renderersBody[0] as SkinnedMeshRenderer);
  }

  public static Transform AddSkin(LoadObject lo, SkinnedMeshRenderer body_skin_renderer, int layer = -1)
  {
    if (Object.op_Equality(lo.loadedObject, (Object) null))
      return (Transform) null;
    Transform base_model = lo.Realizes();
    lo.loadedObject = (Object) null;
    SkinnedMeshRenderer body_skin_renderer1 = body_skin_renderer;
    int layer1 = layer;
    return PlayerLoader.AddSkin(base_model, body_skin_renderer1, layer1);
  }

  public static Transform AddSkin(
    Transform base_model,
    SkinnedMeshRenderer body_skin_renderer,
    int layer = -1)
  {
    SkinnedMeshRenderer skinnedMeshRenderer1 = body_skin_renderer;
    SkinnedMeshRenderer componentInChildren = ((Component) base_model).GetComponentInChildren<SkinnedMeshRenderer>();
    Transform transform = (Transform) null;
    if (Object.op_Inequality((Object) skinnedMeshRenderer1, (Object) null) && Object.op_Inequality((Object) componentInChildren, (Object) null))
    {
      GameObject gameObject = new GameObject(((Object) componentInChildren).name);
      transform = gameObject.transform;
      ((Component) transform).transform.parent = ((Component) skinnedMeshRenderer1).transform.parent;
      SkinnedMeshRenderer skinnedMeshRenderer2 = gameObject.GetComponent<SkinnedMeshRenderer>();
      if (Object.op_Equality((Object) skinnedMeshRenderer2, (Object) null))
        skinnedMeshRenderer2 = gameObject.AddComponent<SkinnedMeshRenderer>();
      skinnedMeshRenderer2.sharedMesh = componentInChildren.sharedMesh;
      ((Renderer) skinnedMeshRenderer2).sharedMaterials = ((Renderer) componentInChildren).sharedMaterials;
      skinnedMeshRenderer2.quality = componentInChildren.quality;
      skinnedMeshRenderer2.localBounds = PlayerLoader.BOUNDS;
      Transform rootBone = skinnedMeshRenderer1.rootBone;
      if (Object.op_Inequality((Object) componentInChildren.rootBone, (Object) null))
      {
        skinnedMeshRenderer2.rootBone = Utility.Find(rootBone, ((Object) componentInChildren.rootBone).name);
        EffectPlayProcessor component = ((Component) base_model).GetComponent<EffectPlayProcessor>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          EffectPlayProcessor effectPlayProcessor = ((Component) skinnedMeshRenderer2.rootBone).gameObject.GetComponent<EffectPlayProcessor>();
          if (Object.op_Equality((Object) effectPlayProcessor, (Object) null))
            effectPlayProcessor = ((Component) skinnedMeshRenderer2.rootBone).gameObject.AddComponent<EffectPlayProcessor>();
          effectPlayProcessor.effectSettings = component.effectSettings;
          List<Transform> transformList = effectPlayProcessor.PlayEffect("InitRoop");
          if (transformList != null)
          {
            for (int index = 0; index < transformList.Count; ++index)
              Utility.SetLayerWithChildren(transformList[index], ((Component) skinnedMeshRenderer2.rootBone).gameObject.layer);
          }
        }
      }
      Transform[] bones = componentInChildren.bones;
      Transform[] transformArray = new Transform[componentInChildren.bones.Length];
      int index1 = 0;
      for (int length = bones.Length; index1 < length; ++index1)
        transformArray[index1] = Utility.Find(rootBone, ((Object) bones[index1]).name);
      skinnedMeshRenderer2.bones = transformArray;
    }
    Object.DestroyImmediate((Object) ((Component) base_model).gameObject);
    base_model = (Transform) null;
    if (layer != -1)
      Utility.SetLayerWithChildren(transform, layer);
    return transform;
  }

  public void InvisibleBodyTriangles(int level)
  {
    PlayerLoader.InvisibleBodyTriangles(level, this.renderersBody[0] as SkinnedMeshRenderer);
  }

  public static void InvisibleBodyTriangles(int level, SkinnedMeshRenderer body_skin_renderer)
  {
    if (level == 0)
      return;
    SkinnedMeshRenderer skinnedMeshRenderer = body_skin_renderer;
    if (Object.op_Equality((Object) skinnedMeshRenderer, (Object) null))
      return;
    Color32[] colors32 = skinnedMeshRenderer.sharedMesh.colors32;
    if (colors32.Length == 0)
      return;
    Mesh mesh = ResourceUtility.Instantiate<Mesh>(skinnedMeshRenderer.sharedMesh);
    int[] triangles = mesh.triangles;
    byte num1 = 0;
    if (level >= 2)
      num1 = (byte) 128 /*0x80*/;
    byte num2 = (byte) ((uint) num1 + 4U);
    int index = 0;
    for (int length = triangles.Length; index < length; index += 3)
    {
      if ((int) colors32[triangles[index]].g <= (int) num2 && (int) colors32[triangles[index + 1]].g <= (int) num2 && (int) colors32[triangles[index + 2]].g <= (int) num2)
        triangles[index + 2] = triangles[index + 1] = triangles[index];
    }
    mesh.triangles = triangles;
    skinnedMeshRenderer.sharedMesh = mesh;
  }

  public virtual void ChangeFace(PlayerLoader.FACE_ID id)
  {
    if (this.faceID == id)
      return;
    this.faceID = id;
    if (!this.validFaceChange)
      return;
    this.renderersFace[0].material.SetFloat("_Face_shift", (float) id);
  }

  public void SetLightProbes(bool enable_light_probes)
  {
    PlayerLoader.SetLightProbes(this.renderersWep, enable_light_probes);
    PlayerLoader.SetLightProbes(this.renderersFace, enable_light_probes);
    PlayerLoader.SetLightProbes(this.renderersHair, enable_light_probes);
    PlayerLoader.SetLightProbes(this.renderersBody, enable_light_probes);
    PlayerLoader.SetLightProbes(this.renderersHead, enable_light_probes);
    PlayerLoader.SetLightProbes(this.renderersArm, enable_light_probes);
    PlayerLoader.SetLightProbes(this.renderersLeg, enable_light_probes);
    PlayerLoader.SetLightProbes(this.renderersAccessory, enable_light_probes);
  }

  public static void SetRenderersEnabled(Renderer[] renderers, bool is_enabled)
  {
    if (((IList<Renderer>) renderers).IsNullOrEmpty<Renderer>())
      return;
    int index = 0;
    for (int length = renderers.Length; index < length; ++index)
    {
      if (Object.op_Inequality((Object) renderers[index], (Object) null))
        renderers[index].enabled = is_enabled;
    }
  }

  public static void SetLightProbes(Transform t, bool enable_light_probes)
  {
    PlayerLoader.SetLightProbes(((Component) t).GetComponentsInChildren<Renderer>(), enable_light_probes);
  }

  public static void SetLightProbes(Renderer[] renderers, bool enable_light_probes)
  {
    if (renderers == null)
      return;
    int index = 0;
    for (int length = renderers.Length; index < length; ++index)
      renderers[index].lightProbeUsage = !enable_light_probes ? (LightProbeUsage) 0 : (LightProbeUsage) 2;
  }

  public static void SetLayerWithChildren_SecondaryNoChange(Transform transform, int layer)
  {
    ((Component) transform).gameObject.layer = layer;
    foreach (Transform transform1 in transform)
      PlayerLoader.SetLayerWithChildren_SecondaryNoChange(transform1, layer);
  }

  public static Vector4 ApplySkinColorCoef(Color skin_color)
  {
    if (!MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
      return Color.op_Implicit(skin_color);
    float skinColorCoef = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.skinColorCoef;
    Vector4 vector4;
    vector4.x = skin_color.r * skinColorCoef;
    vector4.y = skin_color.g * skinColorCoef;
    vector4.z = skin_color.b * skinColorCoef;
    vector4.w = skin_color.a;
    return vector4;
  }

  public static void SetSkinColor(Transform t, int color)
  {
    PlayerLoader.SetSkinColor(t, NGUIMath.IntToColor(color));
  }

  public static void SetSkinColor(Transform t, Color color)
  {
    PlayerLoader.SetSkinColor(((Component) t).GetComponentsInChildren<Renderer>(), color);
  }

  public static void SetSkinColor(Renderer[] renderers, int color)
  {
    PlayerLoader.SetSkinColor(renderers, NGUIMath.IntToColor(color));
  }

  public static void SetSkinColor(Renderer[] renderers, Color color)
  {
    int ID_CHANGE_SKIN_COLOR = Shader.PropertyToID("_Change_Skin_Color");
    Vector4 _color = PlayerLoader.ApplySkinColorCoef(color);
    Utility.MaterialForEach(renderers, (Action<Material>) (mtrl => mtrl.SetVector(ID_CHANGE_SKIN_COLOR, _color)));
  }

  public static void SetEquipColor(Transform t, int color)
  {
    PlayerLoader.SetEquipColor(t, NGUIMath.IntToColor(color));
  }

  public static void SetEquipColor(Transform t, Color color)
  {
    PlayerLoader.SetEquipColor(((Component) t).GetComponentsInChildren<Renderer>(), color);
  }

  public static void SetEquipColor(Renderer[] renderers, int color)
  {
    PlayerLoader.SetEquipColor(renderers, NGUIMath.IntToColor(color));
  }

  public static void SetEquipColor(Renderer[] renderers, Color color)
  {
    int ID_CHANGE_EQUIP_COLOR = Shader.PropertyToID("_Change_Equip_Color");
    Utility.MaterialForEach(renderers, (Action<Material>) (mtrl => mtrl.SetColor(ID_CHANGE_EQUIP_COLOR, color)));
  }

  public static void SetEquipColor3(Renderer[] renderers, int color0, int color1, int color2)
  {
    PlayerLoader.SetEquipColor3(renderers, NGUIMath.IntToColor(color0), NGUIMath.IntToColor(color1), NGUIMath.IntToColor(color2));
  }

  public static void SetEquipColor3(
    Renderer[] renderers,
    Color color0,
    Color color1,
    Color color2)
  {
    int ID_CHANGE_EQUIP_COLOR0 = Shader.PropertyToID("_Change_Equip_Color");
    int ID_CHANGE_EQUIP_COLOR1 = Shader.PropertyToID("_Change_Equip1_Color");
    int ID_CHANGE_EQUIP_COLOR2 = Shader.PropertyToID("_Change_Equip2_Color");
    Utility.MaterialForEach(renderers, (Action<Material>) (mtrl =>
    {
      mtrl.SetColor(ID_CHANGE_EQUIP_COLOR0, color0);
      mtrl.SetColor(ID_CHANGE_EQUIP_COLOR1, color1);
      mtrl.SetColor(ID_CHANGE_EQUIP_COLOR2, color2);
    }));
  }

  public static void SetWeaponShader(
    Renderer[] renderers,
    int color0,
    int color1,
    int color2,
    int effectID,
    float effectParam,
    int effectColor)
  {
    PlayerLoader.SetWeaponShader(renderers, NGUIMath.IntToColor(color0), NGUIMath.IntToColor(color1), NGUIMath.IntToColor(color2), effectID, effectParam, NGUIMath.IntToColor(effectColor));
  }

  public static void SetWeaponShader(
    Renderer[] renderers,
    Color color0,
    Color color1,
    Color color2,
    int effectID,
    float effectParam,
    Color effectColor)
  {
    int ID_CHANGE_EQUIP_COLOR0 = Shader.PropertyToID("_Change_Equip_Color");
    int ID_CHANGE_EQUIP_COLOR1 = Shader.PropertyToID("_Change_Equip1_Color");
    int ID_CHANGE_EQUIP_COLOR2 = Shader.PropertyToID("_Change_Equip2_Color");
    int ID_CHANGE_ATTRIBUTE_COLOR = Shader.PropertyToID("_Change_Attribute_Color");
    int ID_ATTRIBUTE_PITCH = Shader.PropertyToID("_attribute_Pitch");
    Utility.MaterialForEach(renderers, (Action<Material>) (mtrl =>
    {
      Shader shader = ResourceUtility.FindShader($"{((Object) mtrl.shader).name}_{effectID}");
      if (Object.op_Inequality((Object) shader, (Object) null) && Object.op_Inequality((Object) mtrl.shader, (Object) shader))
        mtrl.shader = shader;
      mtrl.SetColor(ID_CHANGE_EQUIP_COLOR0, color0);
      mtrl.SetColor(ID_CHANGE_EQUIP_COLOR1, color1);
      mtrl.SetColor(ID_CHANGE_EQUIP_COLOR2, color2);
      if (mtrl.HasProperty(ID_CHANGE_ATTRIBUTE_COLOR))
        mtrl.SetColor(ID_CHANGE_ATTRIBUTE_COLOR, effectColor);
      if (!mtrl.HasProperty(ID_ATTRIBUTE_PITCH))
        return;
      mtrl.SetFloat(ID_ATTRIBUTE_PITCH, effectParam);
    }));
  }

  public static void SetSkinAndEquipColor(
    Transform t,
    int skin_color,
    int equip_color,
    float z_bias)
  {
    PlayerLoader.SetSkinAndEquipColor(t, NGUIMath.IntToColor(skin_color), NGUIMath.IntToColor(equip_color), z_bias);
  }

  public static void SetSkinAndEquipColor(
    Transform t,
    Color skin_color,
    Color equip_color,
    float z_bias)
  {
    PlayerLoader.SetSkinAndEquipColor(((Component) t).GetComponentsInChildren<Renderer>(), skin_color, equip_color, z_bias);
  }

  public static void SetSkinAndEquipColor(
    Renderer[] renderers,
    int skin_color,
    int equip_color,
    float z_bias)
  {
    PlayerLoader.SetSkinAndEquipColor(renderers, NGUIMath.IntToColor(skin_color), NGUIMath.IntToColor(equip_color), z_bias);
  }

  public static void SetSkinAndEquipColor(
    Renderer[] renderers,
    Color skin_color,
    Color equip_color,
    float z_bias)
  {
    int ID_CHANGE_SKIN_COLOR = Shader.PropertyToID("_Change_Skin_Color");
    int ID_CHANGE_EQUIP_COLOR = Shader.PropertyToID("_Change_Equip_Color");
    int ID_ZBIAS = Shader.PropertyToID("_ZBias");
    Vector4 _skin_color = PlayerLoader.ApplySkinColorCoef(skin_color);
    Utility.MaterialForEach(renderers, (Action<Material>) (mtrl =>
    {
      mtrl.SetVector(ID_CHANGE_SKIN_COLOR, _skin_color);
      mtrl.SetColor(ID_CHANGE_EQUIP_COLOR, equip_color);
      mtrl.SetFloat(ID_ZBIAS, z_bias);
    }));
  }

  public static Transform CreateShadow(
    Transform parent = null,
    bool fixedY0 = true,
    int layer = -1,
    bool is_lightweight = false)
  {
    Transform shadow = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.CreateShadow(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.shadowSize, MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.height * 0.5f, 1f, fixedY0, parent, is_lightweight);
    if (Object.op_Equality((Object) shadow, (Object) null))
      return (Transform) null;
    if (layer != -1)
      ((Component) shadow).gameObject.layer = layer;
    return shadow;
  }

  public static string[] GetHighResoTexNames(string name, int flags)
  {
    List<string> stringList = new List<string>();
    int num = 1;
    while (flags != 0)
    {
      if ((flags & num) != 0)
      {
        switch (num)
        {
          case 1:
            stringList.Add(name);
            break;
          case 4:
            stringList.Add(name + "_R");
            break;
          case 16 /*0x10*/:
            stringList.Add(name + "_L");
            break;
        }
        flags &= ~num;
      }
      num <<= 1;
    }
    return stringList.ToArray();
  }

  public static LoadObject LoadHighResoTexs(LoadingQueue load_queue, string name, int flags)
  {
    if (string.IsNullOrEmpty(name) || flags == 0)
      return (LoadObject) null;
    string[] highResoTexNames = PlayerLoader.GetHighResoTexNames(name, flags);
    return load_queue.Load(RESOURCE_CATEGORY.PLAYER_HIGH_RESO_TEX, name, highResoTexNames);
  }

  public static void ApplyWeaponHighResoTexs(
    LoadObject lo_high_reso_texs,
    int flags,
    Material mate0,
    Material mate1)
  {
    if (lo_high_reso_texs == null || flags == 0 || Object.op_Equality((Object) mate0, (Object) null) && Object.op_Equality((Object) mate1, (Object) null))
      return;
    int num1 = 1;
    int num2 = 0;
    int id = Shader.PropertyToID("_MainTex");
    Shader.PropertyToID("_MaskTex");
    while (flags != 0)
    {
      if ((flags & num1) != 0)
      {
        flags &= ~num1;
        int num3 = id;
        bool flag1 = false;
        bool flag2 = false;
        switch (num1)
        {
          case 1:
            flag1 = true;
            flag2 = true;
            break;
          case 2:
          case 8:
          case 32 /*0x20*/:
            continue;
          case 4:
            flag1 = true;
            break;
          case 16 /*0x10*/:
            flag2 = true;
            break;
        }
        Texture texture = lo_high_reso_texs.loadedObjects[num2++].obj as Texture;
        if (Object.op_Inequality((Object) texture, (Object) null))
        {
          if (flag1 && Object.op_Inequality((Object) mate0, (Object) null))
            mate0.SetTexture(num3, texture);
          if (flag2 && Object.op_Inequality((Object) mate1, (Object) null))
            mate1.SetTexture(num3, texture);
        }
      }
      num1 <<= 1;
    }
  }

  public static void ApplyEquipHighResoTexs(LoadObject lo, Renderer[] renderers)
  {
    if (lo == null || lo.loadedObjects == null || lo.loadedObjects.Length == 0 || renderers == null)
      return;
    Texture texture = lo.loadedObjects[0].obj as Texture;
    if (Object.op_Equality((Object) texture, (Object) null))
      return;
    int index = 0;
    if (index >= renderers.Length)
      return;
    renderers[index].material.SetTexture(Shader.PropertyToID("_MainTex"), texture);
  }

  public static LoadObject LoadHairOverlayTexs(
    LoadingQueue load_queue,
    PlayerLoadInfo info,
    int hairColorId)
  {
    string package_name = "HED" + info.hairModelID.ToString().Insert(2, "_");
    return load_queue.Load(RESOURCE_CATEGORY.HAIR_OVERLAY, package_name, new string[2]
    {
      "base",
      hairColorId.ToString()
    });
  }

  public static void ApplyHairOverlay(LoadObject lo, Renderer[] renderers)
  {
    if (lo == null || lo.loadedObjects == null || lo.loadedObjects.Length != 2 || renderers == null)
      return;
    Shader shader = ResourceUtility.FindShader("mobile/Custom/Character/character_matcap_overlay");
    if (Object.op_Equality((Object) shader, (Object) null))
      return;
    Texture texture1 = lo.loadedObjects[0].obj as Texture;
    if (Object.op_Equality((Object) texture1, (Object) null))
      return;
    Texture texture2 = lo.loadedObjects[1].obj as Texture;
    if (Object.op_Equality((Object) texture2, (Object) null))
      return;
    int id1 = Shader.PropertyToID("_MainTex");
    int id2 = Shader.PropertyToID("_MatCap");
    int index = 0;
    if (index >= renderers.Length)
      return;
    renderers[index].material.shader = shader;
    renderers[index].material.SetTexture(id1, texture1);
    renderers[index].material.SetTexture(id2, texture2);
  }

  public static bool IsLoading(PlayerLoader[] loaders)
  {
    if (loaders != null)
    {
      int index = 0;
      for (int length = loaders.Length; index < length; ++index)
      {
        if (Object.op_Inequality((Object) loaders[index], (Object) null) && loaders[index].isLoading)
          return true;
      }
    }
    return false;
  }

  public static void DestroyModels(PlayerLoader[] loaders)
  {
    int index = 0;
    for (int length = loaders.Length; index < length; ++index)
    {
      if (Object.op_Inequality((Object) loaders[index], (Object) null))
      {
        Object.Destroy((Object) ((Component) loaders[index]).gameObject);
        loaders[index] = (PlayerLoader) null;
      }
    }
  }

  public Transform GetNodeTrans(string node)
  {
    return Object.op_Equality((Object) this.body, (Object) null) ? (Transform) null : Utility.Find(this.body, node);
  }

  public void CombineBurstPairSword(bool isCombine, Vector3 pos, Quaternion rot)
  {
    if (Object.op_Equality((Object) this.wepL, (Object) null))
      return;
    if (isCombine)
    {
      if (Object.op_Equality((Object) this.socketWepR, (Object) null))
        return;
      this.wepL.SetParent(this.socketWepR);
    }
    else
    {
      if (Object.op_Equality((Object) this.socketWepL, (Object) null))
        return;
      this.wepL.SetParent(this.socketWepL);
    }
    this.wepL.localPosition = pos;
    this.wepL.localRotation = rot;
  }

  public void AddAccessoryModel(Transform modelTrans) => this.accessory.Add(modelTrans);

  public Transform GetAccessoryModel(string name)
  {
    if (this.accessory.IsNullOrEmpty<Transform>())
      return (Transform) null;
    for (int index = 0; index < this.accessory.Count; ++index)
    {
      Transform accessoryModel = this.accessory[index];
      if (!Object.op_Equality((Object) accessoryModel, (Object) null) && ((Object) accessoryModel).name == name)
        return accessoryModel;
    }
    return (Transform) null;
  }

  public void DeleteAccessoryModel(string name)
  {
    if (this.accessory.IsNullOrEmpty<Transform>())
      return;
    for (int index = 0; index < this.accessory.Count; ++index)
    {
      Transform transform = this.accessory[index];
      if (!Object.op_Equality((Object) transform, (Object) null) && ((Object) transform).name == name)
      {
        Object.Destroy((Object) ((Component) transform).gameObject);
        this.accessory.RemoveAt(index);
        break;
      }
    }
    List<Renderer> rendererList = new List<Renderer>();
    for (int index = 0; index < this.accessory.Count; ++index)
    {
      if (!Object.op_Equality((Object) this.accessory[index], (Object) null))
      {
        Renderer[] componentsInChildren = ((Component) this.accessory[index]).GetComponentsInChildren<Renderer>();
        if (!((IList<Renderer>) componentsInChildren).IsNullOrEmpty<Renderer>())
          rendererList.AddRange((IEnumerable<Renderer>) componentsInChildren);
      }
    }
    this.renderersAccessory = (Renderer[]) null;
    this.renderersAccessory = rendererList.ToArray();
  }

  public virtual string GetWinMotionState()
  {
    string winMotionState = "win";
    if (this.loadInfo.equipType == 5U && this.loadInfo.weaponSpAttackType == 3U || this.loadInfo.equipType == 1U && this.loadInfo.weaponSpAttackType == 4U || this.loadInfo.equipType == 4U && this.loadInfo.weaponSpAttackType == 4U)
      winMotionState = "win_02";
    return winMotionState;
  }

  public virtual string GetWinLoopMotionState()
  {
    string winLoopMotionState = "win_loop";
    if (this.loadInfo.equipType == 5U && this.loadInfo.weaponSpAttackType == 3U || this.loadInfo.equipType == 1U && this.loadInfo.weaponSpAttackType == 4U || this.loadInfo.equipType == 4U && this.loadInfo.weaponSpAttackType == 4U)
      winLoopMotionState = "win_loop_02";
    return winLoopMotionState;
  }

  public enum FACE_ID
  {
    NORMAL,
    CLOSE_EYE,
  }

  public delegate void OnCompleteLoad(object player);
}
