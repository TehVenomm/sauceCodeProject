using App.Scripts.GoGame.Optimization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerLoader : ModelLoaderBase
{
	public enum FACE_ID
	{
		NORMAL,
		CLOSE_EYE
	}

	public delegate void OnCompleteLoad(object player);

	public static readonly Bounds BOUNDS = new Bounds(Vector3.zero, new Vector3(2f, 2f, 2f));

	public HashSet<string> playerLoaderLoadedAttackInfoNames;

	public List<Transform> accessory = new List<Transform>();

	public const string BASE_ANIM_TABLE_KEY = "BASE";

	public const int kHighResoTextureType_None = 0;

	public const int kHighResoTextureType_Main = 1;

	public const int kHighResoTextureType_MainMask = 2;

	public const int kHighResoTextureType_Right = 4;

	public const int kHighResoTextureType_RightMask = 8;

	public const int kHighResoTextureType_Left = 16;

	public const int kHighResoTextureType_LeftMask = 32;

	private bool autoEyeBlink;

	protected FACE_ID faceID;

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
		96
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

	public PlayerLoadInfo loadInfo
	{
		get;
		protected set;
	}

	public Transform wepR
	{
		get;
		protected set;
	}

	public Transform wepL
	{
		get;
		protected set;
	}

	public Transform face
	{
		get;
		protected set;
	}

	public Transform hair
	{
		get;
		protected set;
	}

	public Transform body
	{
		get;
		protected set;
	}

	public Transform head
	{
		get;
		protected set;
	}

	public Transform arm
	{
		get;
		protected set;
	}

	public Transform leg
	{
		get;
		protected set;
	}

	public string weaponCacheName
	{
		get;
		protected set;
	}

	public string faceCacheName
	{
		get;
		protected set;
	}

	public string hairCacheName
	{
		get;
		protected set;
	}

	public string bodyCacheName
	{
		get;
		protected set;
	}

	public string headCacheName
	{
		get;
		protected set;
	}

	public string armCacheName
	{
		get;
		protected set;
	}

	public string legCacheName
	{
		get;
		protected set;
	}

	public Animator animator
	{
		get;
		protected set;
	}

	public Transform shadow
	{
		get;
		protected set;
	}

	public Transform hairPhysics
	{
		get;
		protected set;
	}

	public Renderer[] renderersWep
	{
		get;
		protected set;
	}

	public Renderer[] renderersFace
	{
		get;
		protected set;
	}

	public Renderer[] renderersHair
	{
		get;
		protected set;
	}

	public Renderer[] renderersBody
	{
		get;
		protected set;
	}

	public Renderer[] renderersHead
	{
		get;
		protected set;
	}

	public Renderer[] renderersArm
	{
		get;
		protected set;
	}

	public Renderer[] renderersLeg
	{
		get;
		protected set;
	}

	public Renderer[] renderersAccessory
	{
		get;
		protected set;
	}

	public Transform socketRoot
	{
		get;
		protected set;
	}

	public Transform socketWepL
	{
		get;
		protected set;
	}

	public Transform socketWepR
	{
		get;
		protected set;
	}

	public Transform socketHead
	{
		get;
		protected set;
	}

	public Transform socketFootL
	{
		get;
		protected set;
	}

	public Transform socketFootR
	{
		get;
		protected set;
	}

	public Transform socketHandL
	{
		get;
		protected set;
	}

	public Transform socketHandR
	{
		get;
		protected set;
	}

	public Transform socketForearmL
	{
		get;
		protected set;
	}

	public Transform socketForearmR
	{
		get;
		protected set;
	}

	public StringKeyTable<LoadObject> animObjectTable
	{
		get;
		protected set;
	}

	public virtual bool eyeBlink
	{
		get
		{
			return autoEyeBlink;
		}
		set
		{
			if (autoEyeBlink != value)
			{
				if (value)
				{
					eyeBlinkTime = UnityEngine.Random.Range(3f, 6f);
				}
				else
				{
					eyeBlinkTime = 0f;
				}
				autoEyeBlink = value;
				ChangeFace(FACE_ID.NORMAL);
			}
		}
	}

	public List<DynamicBone> dynamicBones
	{
		get;
		private set;
	}

	public List<DynamicBone> dynamicBones_Body
	{
		get;
		private set;
	}

	public bool isLoading
	{
		get;
		protected set;
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
		return socketHead;
	}

	public override void SetEnabled(bool is_enable)
	{
		if (animator != null)
		{
			animator.enabled = is_enable;
		}
		if (shadow != null)
		{
			shadow.gameObject.SetActive(is_enable);
		}
		ModelLoaderBase.SetEnabled(renderersWep, is_enable);
		ModelLoaderBase.SetEnabled(renderersFace, is_enable);
		ModelLoaderBase.SetEnabled(renderersHair, is_enable);
		ModelLoaderBase.SetEnabled(renderersBody, is_enable);
		ModelLoaderBase.SetEnabled(renderersHead, is_enable);
		ModelLoaderBase.SetEnabled(renderersArm, is_enable);
		ModelLoaderBase.SetEnabled(renderersLeg, is_enable);
		ModelLoaderBase.SetEnabled(renderersAccessory, is_enable);
	}

	public virtual int GetVoiceId(ACTION_VOICE_ID voice_type)
	{
		if (loadInfo == null)
		{
			return 0;
		}
		int num = RandomizeAttackVoice((int)voice_type);
		return loadInfo.actionVoiceBaseID + num;
	}

	public virtual int GetVoiceId(ACTION_VOICE_EX_ID voice_type)
	{
		if (loadInfo == null)
		{
			return 0;
		}
		int num = RandomizeAttackVoice((int)voice_type);
		return loadInfo.actionVoiceBaseID + num;
	}

	protected virtual void UpdateVoiceAudioClipIds()
	{
		if (voiceAudioClips != null && voiceAudioClips.Length != 0)
		{
			voiceAudioClipIds = new int[voiceAudioClips.Length];
			int i = 0;
			for (int num = voiceAudioClips.Length; i < num; i++)
			{
				int result = 0;
				ResourceObject resourceObject = voiceAudioClips[i];
				if (resourceObject != null && resourceObject.obj != null && resourceObject.obj.name != null)
				{
					int.TryParse(resourceObject.obj.name.Substring(resourceObject.obj.name.Length - 4), out result);
				}
				voiceAudioClipIds[i] = result;
			}
		}
		else
		{
			voiceAudioClipIds = null;
		}
	}

	public virtual AudioClip GetVoiceAudioClip(int voice_id)
	{
		if (voiceAudioClips != null)
		{
			int num = RandomizeAttackVoice(voice_id);
			if (num < 1)
			{
				return null;
			}
			if (voiceAudioClipIds != null && voiceAudioClipIds.Length != 0)
			{
				int i = 0;
				for (int num2 = voiceAudioClipIds.Length; i < num2; i++)
				{
					if (voiceAudioClipIds[i] == num)
					{
						return voiceAudioClips[i].obj as AudioClip;
					}
				}
			}
		}
		return null;
	}

	protected virtual int RandomizeAttackVoice(int voice_id)
	{
		switch (voice_id)
		{
		case 1:
			return ChooseRandom(ATK_VOICE_S, 0.2f);
		case 2:
		case 3:
			return ChooseRandom(ATK_VOICE_M);
		case 4:
		case 5:
			return ChooseRandom(ATK_VOICE_L);
		case 14:
			return ChooseRandom(HAPPY_VOICES);
		case 6:
			return ChooseRandom(DAMAGE_VOICES);
		case 9:
			return ChooseRandom(DEATH_VOICES);
		default:
			return voice_id;
		}
	}

	private int ChooseRandom(int[] items, float rejectRate = 0f)
	{
		if (items == null)
		{
			return 0;
		}
		if (UnityEngine.Random.Range(0f, 1f) < rejectRate)
		{
			return 0;
		}
		int num = UnityEngine.Random.Range(0, items.Length);
		return items[num];
	}

	protected virtual void LoadAttackInfoResource(Player player, LoadingQueue loadQueue)
	{
		AttackInfo[] attackInfos = player.GetAttackInfos();
		if (attackInfos == null)
		{
			return;
		}
		int num = attackInfos.Length;
		for (int i = 0; i < num; i++)
		{
			if (attackInfos[i] == null)
			{
				continue;
			}
			loadQueue.CacheBulletDataUseResource(attackInfos[i].bulletData, player);
			AttackHitInfo attackHitInfo = attackInfos[i] as AttackHitInfo;
			if (attackHitInfo == null)
			{
				continue;
			}
			if (attackHitInfo.hitSEID != 0)
			{
				loadQueue.CacheSE(attackHitInfo.hitSEID);
			}
			loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, attackHitInfo.hitEffectName);
			loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, attackHitInfo.remainEffectName);
			if (string.IsNullOrEmpty(attackHitInfo.toEnemy.hitTypeName))
			{
				continue;
			}
			EnemyHitTypeTable.TypeData data = Singleton<EnemyHitTypeTable>.I.GetData(attackHitInfo.toEnemy.hitTypeName, FieldManager.IsValidInGameNoQuest());
			if (data == null)
			{
				continue;
			}
			loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.baseEffectName);
			for (int j = 0; j < data.elementEffectNames.Length; j++)
			{
				if (!string.IsNullOrEmpty(data.elementEffectNames[j]))
				{
					loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.elementEffectNames[j]);
				}
			}
		}
	}

	public virtual void StartLoad(PlayerLoadInfo player_load_info, int layer, int anim_id, bool need_anim_event, bool need_foot_stamp, bool need_shadow, bool enable_light_probes, bool need_action_voice, bool need_high_reso_tex, bool need_res_ref_count, bool need_dev_frame_instantiate, SHADER_TYPE shader_type, OnCompleteLoad callback, bool enable_eye_blick = true, int use_hair_overlay = -1)
	{
		if (isLoading)
		{
			Log.Error(LOG.RESOURCE, base.name + " now loading.");
		}
		else
		{
			StartCoroutine(DoLoad(player_load_info, layer, anim_id, need_anim_event, need_foot_stamp, need_shadow, enable_light_probes, need_action_voice, need_high_reso_tex, need_res_ref_count, need_dev_frame_instantiate, shader_type, callback, enable_eye_blick, use_hair_overlay));
		}
	}

	public virtual void StartLoad_GG_Optimize(PlayerLoadInfo player_load_info, int layer, int anim_id, bool need_anim_event, bool need_foot_stamp, bool need_shadow, bool enable_light_probes, bool need_action_voice, bool need_high_reso_tex, bool need_res_ref_count, bool need_dev_frame_instantiate, SHADER_TYPE shader_type, OnCompleteLoad callback, bool enable_eye_blick = true, int use_hair_overlay = -1)
	{
		if (isLoading)
		{
			Log.Error(LOG.RESOURCE, base.name + " now loading.");
		}
		else if (MonoBehaviourSingleton<GoGameResourceManager>.IsValid())
		{
			StartCoroutine(DoLoad_GG_Optimize_Self(player_load_info, layer, anim_id, need_anim_event, need_foot_stamp, need_shadow, enable_light_probes, need_action_voice, need_high_reso_tex, need_res_ref_count, need_dev_frame_instantiate, shader_type, callback, enable_eye_blick, use_hair_overlay));
		}
		else
		{
			StartCoroutine(DoLoad_GG_Optimize(player_load_info, layer, anim_id, need_anim_event, need_foot_stamp, need_shadow, enable_light_probes, need_action_voice, need_high_reso_tex, need_res_ref_count, need_dev_frame_instantiate, shader_type, callback, enable_eye_blick, use_hair_overlay));
		}
	}

	private static void SerializePlayerLoadInfo(PlayerLoadInfo info)
	{
	}

	private LoadObject GoGameQuickLoad(LoadingQueue loadQueue, bool needDevFrameInstantiate, RESOURCE_CATEGORY resourceCategory, string resourceName)
	{
		if (needDevFrameInstantiate)
		{
			if (resourceName == null)
			{
				return null;
			}
			return loadQueue.LoadAndInstantiate(resourceCategory, resourceName);
		}
		if (resourceName == null)
		{
			return null;
		}
		return loadQueue.Load(resourceCategory, resourceName);
	}

	protected virtual IEnumerator DoLoad(PlayerLoadInfo info, int layer, int anim_id, bool need_anim_event, bool need_foot_stamp, bool need_shadow, bool enable_light_probes, bool need_action_voice, bool need_high_reso_tex, bool need_res_ref_count, bool need_dev_frame_instantiate, SHADER_TYPE shader_type, OnCompleteLoad callback, bool enable_eye_blick, int use_hair_overlay)
	{
		if (info == null)
		{
			Log.Error(LOG.RESOURCE, "PlayerLoader:info=null");
		}
		SerializePlayerLoadInfo(info);
		animObjectTable = new StringKeyTable<LoadObject>();
		Player player = base.gameObject.GetComponent<Player>();
		bool enableBone = _EnableDynamicBone(shader_type, player is Self);
		EquipModelTable.Data data2 = Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.ARMOR, info.bodyModelID);
		EquipModelTable.Data data3 = data2.needHelm ? Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.HELM, info.headModelID) : null;
		EquipModelTable.Data arm_model_data = data2.needArm ? Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.ARM, info.armModelID) : null;
		EquipModelTable.Data leg_model_data = data2.needLeg ? Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.LEG, info.legModelID) : null;
		int num = data3?.GetHairModelID(info.hairModelID) ?? data2.GetHairModelID(info.hairModelID);
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 0)
		{
			need_high_reso_tex = false;
			use_hair_overlay = -1;
		}
		int high_reso_tex_flags = 0;
		if (need_high_reso_tex && MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
		{
			EquipModelHQTable equipModelHQTable = MonoBehaviourSingleton<GlobalSettingsManager>.I.equipModelHQTable;
			high_reso_tex_flags = equipModelHQTable.GetWeaponFlag(info.weaponModelID);
		}
		bool is_self = false;
		if (player != null)
		{
			_ = player.id;
			is_self = (player is Self);
		}
		DeleteLoadedObjects();
		loadInfo = info;
		if (anim_id < 0)
		{
			anim_id = ((anim_id != -1 || info.weaponModelID == -1) ? (-anim_id + info.weaponModelID / 1000) : (info.weaponModelID / 1000));
		}
		bool flag = data3?.needFace ?? data2.needFace;
		int num2 = data3?.hairMode ?? data2.hairMode;
		string face_name = (info.faceModelID > -1 && flag) ? ResourceName.GetPlayerFace(info.faceModelID) : null;
		string hair_name = (num > -1 && num2 != 0) ? ResourceName.GetPlayerHead(num) : null;
		string body_name = (info.bodyModelID > -1) ? ResourceName.GetPlayerBody(info.bodyModelID) : null;
		string head_name = (info.headModelID > -1 && data3 != null) ? ResourceName.GetPlayerHead(info.headModelID) : null;
		string arm_name = (info.armModelID > -1 && arm_model_data != null) ? ResourceName.GetPlayerArm(info.armModelID) : null;
		string leg_name = (info.legModelID > -1 && leg_model_data != null) ? ResourceName.GetPlayerLeg(info.legModelID) : null;
		string wepn_name = (info.weaponModelID > -1) ? ResourceName.GetPlayerWeapon(info.weaponModelID) : null;
		if (body_name == null)
		{
			yield break;
		}
		Transform _this = base.transform;
		if (player != null)
		{
			if (player.controller != null)
			{
				player.controller.enabled = false;
			}
			if (player.packetReceiver != null)
			{
				player.packetReceiver.SetStopPacketUpdate(is_stop: true);
			}
			player.OnLoadStart();
		}
		isLoading = true;
		LoadingQueue load_queue = new LoadingQueue(this, need_res_ref_count);
		LoadObject lo_face = null;
		LoadObject lo_hair = null;
		LoadObject lo_body = null;
		LoadObject lo_head = null;
		LoadObject lo_arm = null;
		LoadObject lo_leg = null;
		LoadObject lo_wepn = null;
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_FACE, face_name))
		{
			lo_face = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_FACE, face_name);
		}
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name))
		{
			lo_hair = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_HEAD, hair_name);
		}
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_BDY, body_name))
		{
			lo_body = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_BDY, body_name);
		}
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_HEAD, head_name))
		{
			lo_head = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_HEAD, head_name);
		}
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ARM, arm_name))
		{
			lo_arm = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_ARM, arm_name);
		}
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_LEG, leg_name))
		{
			lo_leg = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_LEG, leg_name);
		}
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name))
		{
			lo_wepn = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name);
		}
		List<LoadObject> lo_accessories = new List<LoadObject>();
		if (!info.accUIDs.IsNullOrEmpty())
		{
			int l = 0;
			for (int count = info.accUIDs.Count; l < count; l++)
			{
				string playerAccessory = ResourceName.GetPlayerAccessory(Singleton<AccessoryTable>.I.GetInfoData(info.accUIDs[l]).accessoryId);
				lo_accessories.Add(need_dev_frame_instantiate ? load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_ACCESSORY, playerAccessory) : load_queue.Load(RESOURCE_CATEGORY.PLAYER_ACCESSORY, playerAccessory));
			}
		}
		LoadObject lo_voices = null;
		LoadObject lo_hr_wep_tex = null;
		LoadObject lo_hr_hed_tex = null;
		LoadObject lo_hr_bdy_tex = null;
		LoadObject lo_hr_arm_tex = null;
		LoadObject lo_hr_leg_tex = null;
		string anim_name = (anim_id > -1) ? ResourceName.GetPlayerAnim(anim_id) : null;
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
		}
		if (info.isNeedToCache)
		{
			if (lo_face != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_FACE, face_name, lo_face.loadedObject);
			}
			if (lo_hair != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name, lo_hair.loadedObject);
			}
			if (lo_body != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_BDY, body_name, lo_body.loadedObject);
			}
			if (lo_head != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_HEAD, head_name, lo_head.loadedObject);
			}
			if (lo_arm != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_ARM, arm_name, lo_arm.loadedObject);
			}
			if (lo_leg != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_LEG, leg_name, lo_leg.loadedObject);
			}
			if (lo_wepn != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name, lo_wepn.loadedObject);
			}
		}
		LoadObject lo_anim = (anim_name != null) ? load_queue.Load(RESOURCE_CATEGORY.PLAYER_ANIM, anim_name, (!need_anim_event) ? new string[1]
		{
			anim_name + "Ctrl"
		} : new string[2]
		{
			anim_name + "Ctrl",
			anim_name + "Event"
		}) : null;
		if (lo_anim != null)
		{
			animObjectTable.Add("BASE", lo_anim);
		}
		if (player != null && anim_id > -1)
		{
			List<string> list = new List<string>();
			int num3 = 3;
			for (int m = 0; m < num3; m++)
			{
				for (int n = 0; n < 2; n++)
				{
					SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + m);
					if (skillParam == null)
					{
						continue;
					}
					string ctrlNameFromAnimFormatName = Character.GetCtrlNameFromAnimFormatName((n == 0) ? skillParam.tableData.castStateName : skillParam.tableData.actStateName);
					if (!string.IsNullOrEmpty(ctrlNameFromAnimFormatName) && list.IndexOf(ctrlNameFromAnimFormatName) < 0)
					{
						list.Add(ctrlNameFromAnimFormatName);
						string playerSubAnim = ResourceName.GetPlayerSubAnim(anim_id, ctrlNameFromAnimFormatName);
						LoadObject loadObject = (playerSubAnim != null) ? load_queue.Load(RESOURCE_CATEGORY.PLAYER_ANIM_SKILL, playerSubAnim, (!need_anim_event) ? new string[1]
						{
							playerSubAnim + "Ctrl"
						} : new string[2]
						{
							playerSubAnim + "Ctrl",
							playerSubAnim + "Event"
						}) : null;
						if (loadObject != null)
						{
							animObjectTable.Add(ctrlNameFromAnimFormatName, loadObject);
						}
					}
				}
			}
		}
		if (player != null && info.weaponEvolveId != 0)
		{
			ResourceName.GetPlayerEvolveAnim(info.weaponEvolveId, out string ctrlName, out string animName);
			LoadObject loadObject2 = load_queue.Load(RESOURCE_CATEGORY.PLAYER_ANIM_EVOLVE, animName, new string[2]
			{
				animName + "Ctrl",
				animName + "Event"
			});
			if (loadObject2 != null)
			{
				animObjectTable.Add(ctrlName, loadObject2);
			}
		}
		if (need_action_voice && info.actionVoiceBaseID > -1)
		{
			int[] array = (int[])Enum.GetValues(typeof(ACTION_VOICE_ID));
			int num4 = array.Length;
			string[] array2 = new string[num4];
			for (int num5 = 0; num5 < num4; num5++)
			{
				array2[num5] = ResourceName.GetActionVoiceName(info.actionVoiceBaseID + array[num5]);
			}
			lo_voices = load_queue.Load(RESOURCE_CATEGORY.SOUND_VOICE, ResourceName.GetActionVoicePackageNameFromVoiceID(info.actionVoiceBaseID), array2);
		}
		if (need_high_reso_tex)
		{
			if (high_reso_tex_flags != 0 && wepn_name != null)
			{
				lo_hr_wep_tex = LoadHighResoTexs(load_queue, wepn_name, high_reso_tex_flags);
			}
			if (head_name != null)
			{
				lo_hr_hed_tex = LoadHighResoTexs(load_queue, head_name, 1);
			}
			if (body_name != null)
			{
				lo_hr_bdy_tex = LoadHighResoTexs(load_queue, body_name, 1);
			}
			if (arm_name != null)
			{
				lo_hr_arm_tex = LoadHighResoTexs(load_queue, arm_name, 1);
			}
			if (leg_name != null)
			{
				lo_hr_leg_tex = LoadHighResoTexs(load_queue, leg_name, 1);
			}
		}
		LoadObject loHairOverlay = null;
		if (use_hair_overlay != -1)
		{
			loHairOverlay = LoadHairOverlayTexs(load_queue, info, use_hair_overlay);
		}
		yield return load_queue.Wait();
		List<string> needAtkInfoNames = new List<string>();
		StringKeyTable<LoadObject> animEventBulletLoadObjTable = new StringKeyTable<LoadObject>();
		if (player != null)
		{
			if (need_anim_event)
			{
				animObjectTable.ForEach(delegate(LoadObject load_object)
				{
					load_queue.CacheAnimDataUseResource(load_object.loadedObjects[1].obj as AnimEventData, (string effect_name) => (effect_name[0] != '@') ? effect_name : null);
					load_queue.CacheAnimDataUseResourceDependPlayer(player, load_object.loadedObjects[1].obj as AnimEventData);
					AnimEventData animEventData = load_object.loadedObjects[1].obj as AnimEventData;
					if (animEventData != null && !animEventData.animations.IsNullOrEmpty())
					{
						AnimEventData.AnimData[] animations = animEventData.animations;
						for (int num33 = 0; num33 < animations.Length; num33++)
						{
							AnimEventData.EventData[] events = animations[num33].events;
							foreach (AnimEventData.EventData eventData in events)
							{
								AnimEventFormat.ID id = eventData.id;
								switch (id)
								{
								case AnimEventFormat.ID.SHOT_PRESENT:
								{
									int num36 = 0;
									for (int num37 = eventData.stringArgs.Length; num36 < num37; num36++)
									{
										string[] array4 = eventData.stringArgs[num36].Split(':');
										if (!array4.IsNullOrEmpty())
										{
											int num38 = 0;
											for (int num39 = array4.Length; num38 < num39; num38++)
											{
												string text8 = array4[num38];
												if (!string.IsNullOrEmpty(text8))
												{
													LoadObject loadObject7 = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, text8);
													if (loadObject7 != null && animEventBulletLoadObjTable.Get(text8) == null)
													{
														animEventBulletLoadObjTable.Add(text8, loadObject7);
													}
												}
											}
										}
									}
									break;
								}
								case AnimEventFormat.ID.SHOT_ZONE:
								case AnimEventFormat.ID.SHOT_DECOY:
								case AnimEventFormat.ID.LOAD_BULLET:
								{
									for (int num35 = 0; num35 < eventData.stringArgs.Length; num35++)
									{
										string text7 = eventData.stringArgs[num35];
										if (!string.IsNullOrEmpty(text7))
										{
											LoadObject loadObject6 = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, text7);
											if (loadObject6 != null && animEventBulletLoadObjTable.Get(text7) == null)
											{
												animEventBulletLoadObjTable.Add(text7, loadObject6);
											}
										}
									}
									break;
								}
								}
								switch (id)
								{
								case AnimEventFormat.ID.SHOT_ARROW:
								case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE:
								case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_START:
								case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_END:
								case AnimEventFormat.ID.SHOT_GENERIC:
								case AnimEventFormat.ID.SHOT_TARGET:
								case AnimEventFormat.ID.SHOT_POINT:
								case AnimEventFormat.ID.NWAY_LASER_ATTACK:
								case AnimEventFormat.ID.EXATK_COLLIDER_START:
								case AnimEventFormat.ID.GENERATE_TRACKING:
								case AnimEventFormat.ID.PLAYER_FUNNEL_ATTACK:
								case AnimEventFormat.ID.SHOT_NODE_LINK:
								case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE:
								case AnimEventFormat.ID.PAIR_SWORDS_SHOT_BULLET:
								case AnimEventFormat.ID.PAIR_SWORDS_SHOT_LASER:
								case AnimEventFormat.ID.SHOT_HEALING_HOMING:
								case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE_MULTI:
								case AnimEventFormat.ID.SHOT_RESURRECTION_HOMING:
								case AnimEventFormat.ID.BUFF_START_SHIELD_REFLECT:
								case AnimEventFormat.ID.SHOT_ORACLE_SPEAR_SP:
								case AnimEventFormat.ID.SHOT_ORACLE_PAIR_SWORDS_RUSH:
								{
									string text9 = eventData.stringArgs[0];
									if (!string.IsNullOrEmpty(text9))
									{
										string[] namesNeededLoadAtkInfoFromAnimEvent = ResourceName.GetNamesNeededLoadAtkInfoFromAnimEvent();
										for (int num40 = 0; num40 < namesNeededLoadAtkInfoFromAnimEvent.Length; num40++)
										{
											if (text9.StartsWith(namesNeededLoadAtkInfoFromAnimEvent[num40]))
											{
												needAtkInfoNames.Add(text9);
												break;
											}
										}
									}
									break;
								}
								}
							}
						}
					}
				});
			}
			AddSkillAttackInfoName(player, ref needAtkInfoNames);
			AddFieldGimmickAttackInfoName(ref needAtkInfoNames);
			List<LoadObject> atkInfo = new List<LoadObject>();
			List<LoadObject> atkInfoLoaded = new List<LoadObject>();
			Dictionary<string, LoadObject> atkInfoDict = new Dictionary<string, LoadObject>();
			if (playerLoaderLoadedAttackInfoNames == null)
			{
				playerLoaderLoadedAttackInfoNames = new HashSet<string>();
			}
			for (int num6 = 0; num6 < needAtkInfoNames.Count; num6++)
			{
				if (!playerLoaderLoadedAttackInfoNames.Contains(needAtkInfoNames[num6]))
				{
					if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[num6]))
					{
						LoadObject loadObject3 = load_queue.Load(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[num6]);
						atkInfo.Add(loadObject3);
						atkInfoLoaded.Add(loadObject3);
						atkInfoDict.Add(needAtkInfoNames[num6], loadObject3);
					}
					else
					{
						UnityEngine.Object selfPlayerResourceCache = MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[num6]);
						LoadObject loadObject4 = new LoadObject();
						loadObject4.loadedObject = selfPlayerResourceCache;
						atkInfoLoaded.Add(loadObject4);
						atkInfoDict.Add(needAtkInfoNames[num6], loadObject4);
					}
					playerLoaderLoadedAttackInfoNames.Add(needAtkInfoNames[num6]);
				}
			}
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
			if (info.isNeedToCache)
			{
				foreach (KeyValuePair<string, LoadObject> item in atkInfoDict)
				{
					string key2 = item.Key;
					LoadObject value = item.Value;
					if (atkInfo.Contains(value))
					{
						MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, key2, value.loadedObject);
					}
				}
			}
			if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				Transform _settingTransform = MonoBehaviourSingleton<InGameSettingsManager>.I._transform;
				List<AttackInfo> hitInfos = new List<AttackInfo>();
				for (int k = 0; k < atkInfoLoaded.Count; k++)
				{
					GameObject gameObject = LoadObject.RealizesWithGameObject((GameObject)atkInfoLoaded[k].loadedObject, _settingTransform).gameObject;
					SplitPlayerAttackInfo attackInfo = gameObject.GetComponent<SplitPlayerAttackInfo>();
					hitInfos.Add(attackInfo.attackHitInfo);
					hitInfos.Add(attackInfo.attackContinuationInfo);
					if (!string.IsNullOrEmpty(attackInfo.attackHitInfo.nextBulletInfoName))
					{
						yield return StartCoroutine(LoadNextBulletInfo(load_queue, attackInfo.attackHitInfo.nextBulletInfoName, hitInfos, info.isNeedToCache));
					}
					if (!string.IsNullOrEmpty(attackInfo.attackContinuationInfo.nextBulletInfoName))
					{
						yield return StartCoroutine(LoadNextBulletInfo(load_queue, attackInfo.attackContinuationInfo.nextBulletInfoName, hitInfos, info.isNeedToCache));
					}
				}
				InGameSettingsManager.Player player2 = MonoBehaviourSingleton<InGameSettingsManager>.I.player;
				if (player2.attackInfosAll == null)
				{
					player2.attackInfosAll = new AttackInfo[0];
				}
				AttackInfo[] array3 = Utility.CreateMergedArray(player2.attackInfosAll, hitInfos.ToArray());
				player2.attackInfosAll = Utility.DistinctArray(array3);
				player.AddAttackInfos(hitInfos.ToArray());
			}
			LoadAttackInfoResource(player, load_queue);
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
						if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < ohsActionInfo.Soul_BoostElementHitEffect.Length)
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsActionInfo.Soul_BoostElementHitEffect[(int)nowWeaponElement]);
						}
					}
					break;
				case SP_ATTACK_TYPE.BURST:
				{
					InGameSettingsManager.Player.BurstOneHandSwordActionInfo burstOHSInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.burstOHSInfo;
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < burstOHSInfo.BoostElementHitEffect.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstOHSInfo.BoostElementHitEffect[(int)nowWeaponElement]);
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
					break;
				}
				case SP_ATTACK_TYPE.ORACLE:
				{
					InGameSettingsManager.Player.OracleOneHandSwordActionInfo oracleOHSInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.oracleOHSInfo;
					for (int num9 = 0; num9 < oracleOHSInfo.dragonEffects.Length; num9++)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleOHSInfo.dragonEffects[num9].GetEffectName(nowWeaponElement));
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_sword_dragon_veil");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_sword_dragon_veil_re");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, $"ef_btl_wsk4_sword_02_{(int)nowWeaponElement:D2}");
					break;
				}
				}
				break;
			case 1:
			{
				if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
				{
					break;
				}
				InGameSettingsManager.Player.TwoHandSwordActionInfo twoHandSwordActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo;
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, twoHandSwordActionInfo.nameChargeExpandEffect);
				if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
				{
					load_queue.CacheSE(twoHandSwordActionInfo.soulBoostSeId);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, twoHandSwordActionInfo.soulIaiChargeMaxEffect);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_btl_soul_energy_01");
				}
				if (player.spAttackType == SP_ATTACK_TYPE.BURST)
				{
					InGameSettingsManager.Player.BurstTwoHandSwordActionInfo burstTHSInfo = twoHandSwordActionInfo.burstTHSInfo;
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < burstTHSInfo.HitEffect_SingleShot.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstTHSInfo.HitEffect_SingleShot[(int)nowWeaponElement]);
					}
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < burstTHSInfo.HitEffect_FullBurst.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstTHSInfo.HitEffect_FullBurst[(int)nowWeaponElement]);
					}
				}
				if (player.spAttackType == SP_ATTACK_TYPE.ORACLE)
				{
					InGameSettingsManager.Player.OracleTwoHandSwordActionInfo oracleTHSInfo = twoHandSwordActionInfo.oracleTHSInfo;
					load_queue.CacheSE(oracleTHSInfo.normalVernierSeId);
					load_queue.CacheSE(oracleTHSInfo.maxVernierSeId);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleTHSInfo.normalVernierEffectName);
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < oracleTHSInfo.maxVernierEffectNames.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleTHSInfo.maxVernierEffectNames[(int)nowWeaponElement]);
					}
				}
				break;
			}
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
						{
							load_queue.CacheSE(burstSpearInfo.spinSeId);
						}
						if (burstSpearInfo.spinMaxSpeedSeId > 0)
						{
							load_queue.CacheSE(burstSpearInfo.spinMaxSpeedSeId);
						}
						string text = string.Empty;
						if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < burstSpearInfo.spinEffectNames.Length)
						{
							text = burstSpearInfo.spinEffectNames[(int)nowWeaponElement];
						}
						string text2 = string.Empty;
						if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < burstSpearInfo.spinElementHitEffectNames.Length)
						{
							text2 = burstSpearInfo.spinElementHitEffectNames[(int)nowWeaponElement];
						}
						string text3 = string.Empty;
						if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < burstSpearInfo.spinThrowGroundEffectNames.Length)
						{
							text3 = burstSpearInfo.spinThrowGroundEffectNames[(int)nowWeaponElement];
						}
						if (!string.IsNullOrEmpty(text))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text);
						}
						if (!string.IsNullOrEmpty(text2))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text2);
						}
						if (!string.IsNullOrEmpty(text3))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text3);
						}
						if (!string.IsNullOrEmpty(burstSpearInfo.throwGroundEffectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstSpearInfo.throwGroundEffectName);
						}
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
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < spearActionInfo.jumpHugeElementHitEffectNames.Length)
					{
						name = spearActionInfo.jumpHugeElementHitEffectNames[(int)nowWeaponElement];
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name);
				}
				break;
			case 4:
				if (player.spAttackType == SP_ATTACK_TYPE.NONE)
				{
					if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
					{
						load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.wildDanceChargeMaxSeId);
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
				}
				else if (player.spAttackType == SP_ATTACK_TYPE.HEAT)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_02");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_03");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_04");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_05");
				}
				else if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
				{
					if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
					{
						break;
					}
					InGameSettingsManager.Player.PairSwordsActionInfo pairSwordsActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo;
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, pairSwordsActionInfo.Soul_EffectForWaitingLaser);
					string name2 = pairSwordsActionInfo.Soul_EffectForBullet;
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < pairSwordsActionInfo.Soul_EffectsForBullet.Length)
					{
						name2 = pairSwordsActionInfo.Soul_EffectsForBullet[(int)nowWeaponElement];
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name2);
					if (pairSwordsActionInfo.Soul_SeIds.IsNullOrEmpty())
					{
						break;
					}
					for (int num10 = 0; num10 < pairSwordsActionInfo.Soul_SeIds.Length; num10++)
					{
						if (pairSwordsActionInfo.Soul_SeIds[num10] >= 0)
						{
							load_queue.CacheSE(pairSwordsActionInfo.Soul_SeIds[num10]);
						}
					}
				}
				else if (player.spAttackType == SP_ATTACK_TYPE.BURST)
				{
					load_queue.CacheSE(10000051);
					load_queue.CacheSE(10000042);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_twinsword_01_00");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
					InGameSettingsManager.Player.PairSwordsActionInfo pairSwordsActionInfo2 = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo;
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < pairSwordsActionInfo2.Burst_CombineHitEffect.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, pairSwordsActionInfo2.Burst_CombineHitEffect[(int)nowWeaponElement]);
					}
				}
				else if (player.spAttackType == SP_ATTACK_TYPE.ORACLE)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_twinsword_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, $"ef_btl_wsk4_twinsword_01_{(int)nowWeaponElement:D2}");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_twinsword_03");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_twinsword_04");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, $"ef_btl_wsk4_twinsword_05_{(int)nowWeaponElement:D2}");
				}
				break;
			case 5:
			{
				if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
				{
					break;
				}
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
				else if (player.spAttackType == SP_ATTACK_TYPE.HEAT)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[22]);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[21]);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_bow_01_02");
				}
				else if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[24]);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_bow_lock_02");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
					load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulLockMaxSeId);
					load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulLockSeId);
					load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.soulBoostSeId);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
				}
				else if (player.spAttackType == SP_ATTACK_TYPE.BURST)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[27]);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[26]);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.GetBombArrowEffectName(nowWeaponElement));
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.arrowRainShotAimLesserCursorEffectName);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.GetBombEffectName(nowWeaponElement));
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.boostArrowChargeMaxEffectName);
					load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.burstBoostModeSEId);
					List<int> bombArrowSEIdList = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombArrowSEIdList;
					for (int num7 = 0; num7 < bombArrowSEIdList.Count; num7++)
					{
						load_queue.CacheSE(bombArrowSEIdList[num7]);
					}
					List<int> bombSEIdList = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombSEIdList;
					for (int num8 = 0; num8 < bombSEIdList.Count; num8++)
					{
						load_queue.CacheSE(bombSEIdList[num8]);
					}
				}
				break;
			}
			}
			EvolveController.Load(load_queue, info.weaponEvolveId);
			int skill_len3 = 3;
			LoadObject[] bullet_load = new LoadObject[skill_len3];
			for (int num11 = 0; num11 < skill_len3; num11++)
			{
				SkillInfo.SkillParam skillParam2 = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + num11);
				if (skillParam2 == null)
				{
					bullet_load[num11] = null;
					continue;
				}
				SkillItemTable.SkillItemData tableData = skillParam2.tableData;
				if (!string.IsNullOrEmpty(tableData.startEffectName))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.startEffectName);
				}
				if (tableData.startSEID != 0)
				{
					load_queue.CacheSE(tableData.startSEID);
				}
				if (!string.IsNullOrEmpty(tableData.actLocalEffectName))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.actLocalEffectName);
				}
				if (!string.IsNullOrEmpty(tableData.actOneshotEffectName))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.actOneshotEffectName);
				}
				if (tableData.actSEID != 0)
				{
					load_queue.CacheSE(tableData.actSEID);
				}
				if (!string.IsNullOrEmpty(tableData.enchantEffectName))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.enchantEffectName);
				}
				if (!string.IsNullOrEmpty(tableData.hitEffectName))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.hitEffectName);
				}
				if ((float)tableData.skillRange > 0f && !string.IsNullOrEmpty(MonoBehaviourSingleton<InGameSettingsManager>.I.player.skillRangeEffectName))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.skillRangeEffectName);
				}
				if (tableData.hitSEID != 0)
				{
					load_queue.CacheSE(tableData.hitSEID);
				}
				if (is_self)
				{
					load_queue.CacheItemIcon(tableData.iconID);
				}
				if (tableData.healType == HEAL_TYPE.RESURRECTION_ALL)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_heal_04_03");
				}
				if (!tableData.buffTableIds.IsNullOrEmpty())
				{
					for (int num12 = 0; num12 < tableData.buffTableIds.Length; num12++)
					{
						BuffTable.BuffData data4 = Singleton<BuffTable>.I.GetData((uint)tableData.buffTableIds[num12]);
						if (BuffParam.IsHitAbsorbType(data4.type))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_drain_01_01");
						}
						else if (data4.type == BuffParam.BUFFTYPE.AUTO_REVIVE)
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_heal_04_03");
						}
					}
				}
				string[] supportEffectName = tableData.supportEffectName;
				foreach (string text4 in supportEffectName)
				{
					if (!string.IsNullOrEmpty(text4))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text4);
					}
				}
				BuffParam.BUFFTYPE[] supportType = tableData.supportType;
				for (int num13 = 0; num13 < supportType.Length; num13++)
				{
					switch (supportType[num13])
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
				bullet_load[num11] = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, tableData.bulletName);
			}
			if (is_self)
			{
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_darkness_02");
			}
			EffectPlayProcessor effectPlayProcessor = player.effectPlayProcessor;
			if (effectPlayProcessor != null && effectPlayProcessor.effectSettings != null)
			{
				int num14 = 0;
				for (int num15 = effectPlayProcessor.effectSettings.Length; num14 < num15; num14++)
				{
					if (string.IsNullOrEmpty(effectPlayProcessor.effectSettings[num14].effectName))
					{
						continue;
					}
					string name3 = effectPlayProcessor.effectSettings[num14].name;
					if (name3.StartsWith("BUFF_"))
					{
						string text5 = name3.Substring(name3.Length - "_PLC00".Length);
						if (text5.Contains("_PLC") && text5 != "_PLC" + (loadInfo.weaponModelID / 1000).ToString("D2"))
						{
							continue;
						}
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor.effectSettings[num14].effectName);
				}
			}
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
			for (int num16 = 0; num16 < skill_len3; num16++)
			{
				SkillInfo.SkillParam skillParam3 = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + num16);
				if (skillParam3 != null)
				{
					skillParam3.bullet = (bullet_load[num16].loadedObject as BulletData);
					load_queue.CacheBulletDataUseResource(skillParam3.bullet, player);
				}
			}
			if (animEventBulletLoadObjTable != null)
			{
				animEventBulletLoadObjTable.ForEachKeyAndValue(delegate(string key, LoadObject item)
				{
					if (item != null && item.loadedObject != null)
					{
						BulletData bulletData = item.loadedObject as BulletData;
						if (bulletData != null && player.cachedBulletDataTable.Get(key) == null)
						{
							player.cachedBulletDataTable.Add(key, bulletData);
							load_queue.CacheBulletDataUseResource(bulletData, player);
						}
					}
				});
			}
			animEventBulletLoadObjTable.Clear();
			animEventBulletLoadObjTable = null;
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
		}
		if (lo_arm != null)
		{
			if (lo_arm.loadedObject != null)
			{
				GameObject gameObject2 = lo_arm.loadedObject as GameObject;
				EffectPlayProcessor effectPlayProcessor2 = (gameObject2 != null) ? gameObject2.GetComponent<EffectPlayProcessor>() : null;
				if (effectPlayProcessor2 != null && effectPlayProcessor2.effectSettings != null)
				{
					int num17 = 0;
					for (int num18 = effectPlayProcessor2.effectSettings.Length; num17 < num18; num17++)
					{
						if (!string.IsNullOrEmpty(effectPlayProcessor2.effectSettings[num17].effectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor2.effectSettings[num17].effectName);
						}
					}
				}
			}
		}
		else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ARM, arm_name))
		{
			GameObject gameObject3 = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ARM, arm_name);
			EffectPlayProcessor effectPlayProcessor3 = (gameObject3 != null) ? gameObject3.GetComponent<EffectPlayProcessor>() : null;
			if (effectPlayProcessor3 != null && effectPlayProcessor3.effectSettings != null)
			{
				int num19 = 0;
				for (int num20 = effectPlayProcessor3.effectSettings.Length; num19 < num20; num19++)
				{
					if (!string.IsNullOrEmpty(effectPlayProcessor3.effectSettings[num19].effectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor3.effectSettings[num19].effectName);
					}
				}
			}
		}
		if (lo_leg != null)
		{
			if (lo_leg.loadedObject != null)
			{
				GameObject gameObject4 = lo_leg.loadedObject as GameObject;
				EffectPlayProcessor effectPlayProcessor4 = (gameObject4 != null) ? gameObject4.GetComponent<EffectPlayProcessor>() : null;
				if (effectPlayProcessor4 != null && effectPlayProcessor4.effectSettings != null)
				{
					int num21 = 0;
					for (int num22 = effectPlayProcessor4.effectSettings.Length; num21 < num22; num21++)
					{
						if (!string.IsNullOrEmpty(effectPlayProcessor4.effectSettings[num21].effectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor4.effectSettings[num21].effectName);
						}
					}
				}
			}
		}
		else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_LEG, leg_name))
		{
			GameObject gameObject5 = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_LEG, leg_name);
			EffectPlayProcessor effectPlayProcessor5 = (gameObject5 != null) ? gameObject5.GetComponent<EffectPlayProcessor>() : null;
			if (effectPlayProcessor5 != null && effectPlayProcessor5.effectSettings != null)
			{
				int num23 = 0;
				for (int num24 = effectPlayProcessor5.effectSettings.Length; num23 < num24; num23++)
				{
					if (!string.IsNullOrEmpty(effectPlayProcessor5.effectSettings[num23].effectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor5.effectSettings[num23].effectName);
					}
				}
			}
		}
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
		}
		bool wait = false;
		bool div_frame_realizes = false;
		int skin_color = info.skinColor;
		if (lo_body != null)
		{
			if (!div_frame_realizes)
			{
				body = lo_body.Realizes(_this);
				renderersBody = body.gameObject.GetComponentsInChildren<Renderer>();
				ModelLoaderBase.SetEnabled(renderersBody, is_enable: false);
				SetDynamicBones_Body(body, enableBone);
			}
			else
			{
				wait = true;
				InstantiateManager.Request(this, lo_body.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					body = ((GameObject)data.instantiatedObject).transform;
					body.SetParent(_this, worldPositionStays: false);
					renderersBody = body.GetComponentsInChildren<Renderer>();
					SetDynamicBones_Body(body, enableBone);
					SetRenderersEnabled(renderersBody, is_enabled: false);
					wait = false;
				});
				while (wait)
				{
					yield return null;
				}
			}
		}
		else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_BDY, body_name))
		{
			GameObject gameObject6 = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_BDY, body_name);
			if (gameObject6 != null)
			{
				if (!div_frame_realizes)
				{
					body = LoadObject.RealizesWithGameObject(gameObject6, _this);
					renderersBody = body.gameObject.GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersBody, is_enable: false);
					SetDynamicBones_Body(body, enableBone);
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, gameObject6, delegate(InstantiateManager.InstantiateData data)
					{
						body = ((GameObject)data.instantiatedObject).transform;
						body.SetParent(_this, worldPositionStays: false);
						renderersBody = body.GetComponentsInChildren<Renderer>();
						SetDynamicBones_Body(body, enableBone);
						SetRenderersEnabled(renderersBody, is_enabled: false);
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
				}
			}
		}
		if (body == null)
		{
			yield break;
		}
		yield return StartCoroutine(ItemLoader.InitRoopEffect(load_queue, body, shader_type));
		if (renderersBody == null)
		{
			yield break;
		}
		if (renderersBody.Length != 0)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = renderersBody[0] as SkinnedMeshRenderer;
			if (skinnedMeshRenderer != null)
			{
				skinnedMeshRenderer.localBounds = BOUNDS;
			}
		}
		SetSkinAndEquipColor(renderersBody, skin_color, info.bodyColor, 0f);
		ApplyEquipHighResoTexs(lo_hr_bdy_tex, renderersBody);
		animator = body.GetComponentInChildren<Animator>();
		if (player != null)
		{
			player.body = body;
		}
		socketRoot = Utility.Find(body, "Root");
		socketHead = Utility.Find(body, "Head");
		socketWepL = Utility.Find(body, "L_Wep");
		socketWepR = Utility.Find(body, "R_Wep");
		socketFootL = Utility.Find(body, "L_Foot");
		socketFootR = Utility.Find(body, "R_Foot");
		socketHandL = Utility.Find(body, "L_Hand");
		socketHandR = Utility.Find(body, "R_Hand");
		socketForearmL = Utility.Find(body, "L_Forearm");
		socketForearmR = Utility.Find(body, "R_Forearm");
		if (need_foot_stamp)
		{
			if (socketFootL != null && socketFootL.GetComponent<StampNode>() == null)
			{
				StampNode stampNode = socketFootL.gameObject.AddComponent<StampNode>();
				stampNode.offset = new Vector3(-0.08f, 0.01f, 0f);
				stampNode.autoBaseY = 0.1f;
			}
			if (socketFootR != null && socketFootR.GetComponent<StampNode>() == null)
			{
				StampNode stampNode2 = socketFootR.gameObject.AddComponent<StampNode>();
				stampNode2.offset = new Vector3(-0.08f, 0.01f, 0f);
				stampNode2.autoBaseY = 0.1f;
			}
			CharacterStampCtrl characterStampCtrl = body.GetComponent<CharacterStampCtrl>();
			if (characterStampCtrl == null)
			{
				characterStampCtrl = body.gameObject.AddComponent<CharacterStampCtrl>();
			}
			characterStampCtrl.Init(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos, player);
			int num25 = 0;
			for (int num26 = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos.Length; num25 < num26; num25++)
			{
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos[num25].effectName);
			}
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
		}
		if (lo_face != null)
		{
			if (!div_frame_realizes)
			{
				face = lo_face.Realizes(socketHead);
				renderersFace = face.gameObject.GetComponentsInChildren<Renderer>();
				ModelLoaderBase.SetEnabled(renderersFace, is_enable: false);
			}
			else
			{
				wait = true;
				InstantiateManager.Request(this, lo_face.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					face = ((GameObject)data.instantiatedObject).transform;
					face.SetParent(socketHead, worldPositionStays: false);
					renderersFace = face.GetComponentsInChildren<Renderer>();
					SetRenderersEnabled(renderersFace, is_enabled: false);
					wait = false;
				});
				while (wait)
				{
					yield return null;
				}
				if (renderersFace == null)
				{
					yield break;
				}
			}
			SetSkinColor(renderersFace, skin_color);
			validFaceChange = (renderersFace != null && renderersFace.Length != 0 && renderersFace[0].material.HasProperty("_Face_shift"));
			eyeBlink = enable_eye_blick;
		}
		else
		{
			GameObject gameObject7 = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_FACE, face_name);
			if (gameObject7 != null)
			{
				if (!div_frame_realizes)
				{
					face = LoadObject.RealizesWithGameObject(gameObject7, socketHead);
					renderersFace = face.gameObject.GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersFace, is_enable: false);
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, gameObject7, delegate(InstantiateManager.InstantiateData data)
					{
						face = ((GameObject)data.instantiatedObject).transform;
						face.SetParent(socketHead, worldPositionStays: false);
						renderersFace = face.GetComponentsInChildren<Renderer>();
						SetRenderersEnabled(renderersFace, is_enabled: false);
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
					if (renderersFace == null)
					{
						yield break;
					}
				}
				SetSkinColor(renderersFace, skin_color);
				validFaceChange = (renderersFace != null && renderersFace.Length != 0 && renderersFace[0].material.HasProperty("_Face_shift"));
				eyeBlink = enable_eye_blick;
			}
		}
		if (lo_hair != null)
		{
			if (!div_frame_realizes)
			{
				hair = lo_hair.Realizes(socketHead);
				renderersHair = hair.GetComponentsInChildren<Renderer>();
				ModelLoaderBase.SetEnabled(renderersHair, is_enable: false);
				SetDynamicBones(body, hair, enableBone);
			}
			else
			{
				wait = true;
				InstantiateManager.Request(this, lo_hair.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					hair = ((GameObject)data.instantiatedObject).transform;
					hair.SetParent(socketHead, worldPositionStays: false);
					renderersHair = hair.GetComponentsInChildren<Renderer>();
					SetDynamicBones(body, hair, enableBone);
					SetRenderersEnabled(renderersHair, is_enabled: false);
					wait = false;
				});
				while (wait)
				{
					yield return null;
				}
				if (renderersHair == null)
				{
					yield break;
				}
			}
			SetSkinAndEquipColor(renderersHair, skin_color, info.hairColor, 0f);
			if (loHairOverlay != null)
			{
				ApplyHairOverlay(loHairOverlay, renderersHair);
			}
		}
		else
		{
			GameObject gameObject8 = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name);
			if (gameObject8 != null)
			{
				if (!div_frame_realizes)
				{
					hair = LoadObject.RealizesWithGameObject(gameObject8, socketHead);
					renderersHair = hair.GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersHair, is_enable: false);
					SetDynamicBones(body, hair, enableBone);
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, gameObject8, delegate(InstantiateManager.InstantiateData data)
					{
						hair = ((GameObject)data.instantiatedObject).transform;
						hair.SetParent(socketHead, worldPositionStays: false);
						renderersHair = hair.GetComponentsInChildren<Renderer>();
						SetDynamicBones(body, hair, enableBone);
						SetRenderersEnabled(renderersHair, is_enabled: false);
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
					if (renderersHair == null)
					{
						yield break;
					}
				}
				SetSkinAndEquipColor(renderersHair, skin_color, info.hairColor, 0f);
				if (loHairOverlay != null)
				{
					ApplyHairOverlay(loHairOverlay, renderersHair);
				}
			}
		}
		if (lo_head != null)
		{
			if (!div_frame_realizes)
			{
				head = lo_head.Realizes(socketHead);
				if (head != null)
				{
					renderersHead = head.GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersHead, is_enable: false);
				}
			}
			else
			{
				wait = true;
				InstantiateManager.Request(this, lo_head.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					head = ((GameObject)data.instantiatedObject).transform;
					head.SetParent(socketHead, worldPositionStays: false);
					renderersHead = head.GetComponentsInChildren<Renderer>();
					SetRenderersEnabled(renderersHead, is_enabled: false);
					wait = false;
				});
				while (wait)
				{
					yield return null;
				}
				if (renderersHead == null)
				{
					yield break;
				}
			}
			if (head != null)
			{
				yield return StartCoroutine(ItemLoader.InitRoopEffect(load_queue, head, shader_type));
			}
			SetEquipColor(renderersHead, info.headColor);
			ApplyEquipHighResoTexs(lo_hr_hed_tex, renderersHead);
		}
		else
		{
			GameObject gameObject9 = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_HEAD, head_name);
			if (gameObject9 != null)
			{
				if (!div_frame_realizes)
				{
					head = LoadObject.RealizesWithGameObject(gameObject9, socketHead);
					if (head != null)
					{
						renderersHead = head.GetComponentsInChildren<Renderer>();
						ModelLoaderBase.SetEnabled(renderersHead, is_enable: false);
					}
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, gameObject9, delegate(InstantiateManager.InstantiateData data)
					{
						head = ((GameObject)data.instantiatedObject).transform;
						head.SetParent(socketHead, worldPositionStays: false);
						renderersHead = head.GetComponentsInChildren<Renderer>();
						SetRenderersEnabled(renderersHead, is_enabled: false);
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
					if (renderersHead == null)
					{
						yield break;
					}
				}
				if (head != null)
				{
					yield return StartCoroutine(ItemLoader.InitRoopEffect(load_queue, head, shader_type));
				}
				SetEquipColor(renderersHead, info.headColor);
				ApplyEquipHighResoTexs(lo_hr_hed_tex, renderersHead);
			}
		}
		if (lo_arm != null)
		{
			if (!div_frame_realizes)
			{
				arm = AddSkin(lo_arm);
				if (arm != null)
				{
					renderersArm = arm.GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersArm, is_enable: false);
				}
			}
			else
			{
				wait = true;
				InstantiateManager.Request(this, lo_arm.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					arm = ((GameObject)data.instantiatedObject).transform;
					arm = AddSkin(arm);
					renderersArm = arm.GetComponentsInChildren<Renderer>();
					SetRenderersEnabled(renderersArm, is_enabled: false);
					wait = false;
				});
				while (wait)
				{
					yield return null;
				}
				if (renderersArm == null)
				{
					yield break;
				}
			}
			if (arm != null)
			{
				SetSkinAndEquipColor(renderersArm, skin_color, info.armColor, arm_model_data.GetZBias());
				ApplyEquipHighResoTexs(lo_hr_arm_tex, renderersArm);
				InvisibleBodyTriangles(arm_model_data.bodyDraw);
			}
		}
		else
		{
			GameObject gameObject10 = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ARM, arm_name);
			if ((bool)gameObject10)
			{
				if (!div_frame_realizes)
				{
					arm = AddSkinFromCache(gameObject10);
					if (arm != null)
					{
						renderersArm = arm.GetComponentsInChildren<Renderer>();
						ModelLoaderBase.SetEnabled(renderersArm, is_enable: false);
					}
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, gameObject10, delegate(InstantiateManager.InstantiateData data)
					{
						arm = ((GameObject)data.instantiatedObject).transform;
						arm = AddSkin(arm);
						renderersArm = arm.GetComponentsInChildren<Renderer>();
						SetRenderersEnabled(renderersArm, is_enabled: false);
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
					if (renderersArm == null)
					{
						yield break;
					}
				}
				if (arm != null)
				{
					SetSkinAndEquipColor(renderersArm, skin_color, info.armColor, arm_model_data.GetZBias());
					ApplyEquipHighResoTexs(lo_hr_arm_tex, renderersArm);
					InvisibleBodyTriangles(arm_model_data.bodyDraw);
				}
			}
		}
		if (lo_leg != null)
		{
			if (!div_frame_realizes)
			{
				leg = AddSkin(lo_leg);
				if (leg != null)
				{
					renderersLeg = leg.GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersLeg, is_enable: false);
				}
			}
			else
			{
				wait = true;
				InstantiateManager.Request(this, lo_leg.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					leg = ((GameObject)data.instantiatedObject).transform;
					leg = AddSkin(leg);
					renderersLeg = leg.GetComponentsInChildren<Renderer>();
					SetRenderersEnabled(renderersLeg, is_enabled: false);
					wait = false;
				});
				while (wait)
				{
					yield return null;
				}
				if (renderersLeg == null)
				{
					yield break;
				}
			}
			if (leg != null)
			{
				SetSkinAndEquipColor(renderersLeg, skin_color, info.legColor, leg_model_data.GetZBias());
				ApplyEquipHighResoTexs(lo_hr_leg_tex, renderersLeg);
			}
		}
		else
		{
			GameObject gameObject11 = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_LEG, leg_name);
			if ((bool)gameObject11)
			{
				if (!div_frame_realizes)
				{
					leg = AddSkinFromCache(gameObject11);
					if (leg != null)
					{
						renderersLeg = leg.GetComponentsInChildren<Renderer>();
						ModelLoaderBase.SetEnabled(renderersLeg, is_enable: false);
					}
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, gameObject11, delegate(InstantiateManager.InstantiateData data)
					{
						leg = ((GameObject)data.instantiatedObject).transform;
						leg = AddSkin(leg);
						renderersLeg = leg.GetComponentsInChildren<Renderer>();
						SetRenderersEnabled(renderersLeg, is_enabled: false);
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
					if (renderersLeg == null)
					{
						yield break;
					}
				}
				if (leg != null)
				{
					SetSkinAndEquipColor(renderersLeg, skin_color, info.legColor, leg_model_data.GetZBias());
					ApplyEquipHighResoTexs(lo_hr_leg_tex, renderersLeg);
				}
			}
		}
		bool isSoulArrowOutGameEffect = false;
		if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && info.equipType == 5 && info.weaponSpAttackType == 2)
		{
			isSoulArrowOutGameEffect = true;
		}
		if (lo_wepn != null)
		{
			Transform weapon2 = null;
			if (!div_frame_realizes)
			{
				weapon2 = lo_wepn.Realizes();
				if (weapon2 != null)
				{
					renderersWep = weapon2.gameObject.GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersWep, is_enable: false);
				}
			}
			else
			{
				wait = true;
				InstantiateManager.Request(this, lo_wepn.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					weapon2 = ((GameObject)data.instantiatedObject).transform;
					renderersWep = weapon2.GetComponentsInChildren<Renderer>();
					SetRenderersEnabled(renderersWep, is_enabled: false);
					wait = false;
				});
				while (wait)
				{
					yield return null;
				}
				if (renderersWep == null)
				{
					yield break;
				}
			}
			if (weapon2 != null)
			{
				if (isSoulArrowOutGameEffect)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_bow_01_01");
				}
				yield return StartCoroutine(ItemLoader.InitRoopEffect(load_queue, weapon2, shader_type));
			}
			if (renderersBody == null)
			{
				yield break;
			}
			if (weapon2 != null)
			{
				InitWeaponLinkBuffEffect(player, weapon2);
			}
			SetWeaponShader(renderersWep, info.weaponColor0, info.weaponColor1, info.weaponColor2, info.weaponEffectID, info.weaponEffectParam, info.weaponEffectColor);
			Material mate = null;
			Material mate2 = null;
			if (renderersWep != null)
			{
				int num27 = 0;
				for (int num28 = renderersWep.Length; num27 < num28; num27++)
				{
					if (renderersWep[num27].name.EndsWith("_L"))
					{
						mate2 = renderersWep[num27].material;
						wepL = renderersWep[num27].transform;
						if (loadInfo.equipType == 0 && loadInfo.weaponSpAttackType == 2)
						{
							Utility.Attach(socketHandL, wepL);
						}
						else
						{
							Utility.Attach(socketWepL, wepL);
						}
					}
					else
					{
						mate = renderersWep[num27].material;
						wepR = renderersWep[num27].transform;
						Utility.Attach(socketWepR, wepR);
					}
				}
			}
			if (weapon2 != null)
			{
				UnityEngine.Object.DestroyImmediate(weapon2.gameObject);
			}
			if (lo_hr_wep_tex != null)
			{
				ApplyWeaponHighResoTexs(lo_hr_wep_tex, high_reso_tex_flags, mate, mate2);
			}
		}
		else
		{
			GameObject gameObject12 = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name);
			if ((bool)gameObject12)
			{
				Transform weapon = null;
				if (!div_frame_realizes)
				{
					weapon = LoadObject.RealizesWithGameObject(gameObject12);
					if (weapon != null)
					{
						renderersWep = weapon.gameObject.GetComponentsInChildren<Renderer>();
						ModelLoaderBase.SetEnabled(renderersWep, is_enable: false);
					}
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, gameObject12, delegate(InstantiateManager.InstantiateData data)
					{
						weapon = ((GameObject)data.instantiatedObject).transform;
						renderersWep = weapon.GetComponentsInChildren<Renderer>();
						SetRenderersEnabled(renderersWep, is_enabled: false);
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
					if (renderersWep == null)
					{
						yield break;
					}
				}
				if (weapon != null)
				{
					if (isSoulArrowOutGameEffect)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_bow_01_01");
					}
					yield return StartCoroutine(ItemLoader.InitRoopEffect(load_queue, weapon, shader_type));
				}
				if (renderersBody == null)
				{
					yield break;
				}
				if (weapon != null)
				{
					InitWeaponLinkBuffEffect(player, weapon);
				}
				SetWeaponShader(renderersWep, info.weaponColor0, info.weaponColor1, info.weaponColor2, info.weaponEffectID, info.weaponEffectParam, info.weaponEffectColor);
				Material mate3 = null;
				Material mate4 = null;
				if (renderersWep != null)
				{
					int num29 = 0;
					for (int num30 = renderersWep.Length; num29 < num30; num29++)
					{
						if (renderersWep[num29].name.EndsWith("_L"))
						{
							mate4 = renderersWep[num29].material;
							wepL = renderersWep[num29].transform;
							if (loadInfo.equipType == 0 && loadInfo.weaponSpAttackType == 2)
							{
								Utility.Attach(socketHandL, wepL);
							}
							else
							{
								Utility.Attach(socketWepL, wepL);
							}
						}
						else
						{
							mate3 = renderersWep[num29].material;
							wepR = renderersWep[num29].transform;
							Utility.Attach(socketWepR, wepR);
						}
					}
				}
				if (weapon != null)
				{
					UnityEngine.Object.DestroyImmediate(weapon.gameObject);
				}
				if (lo_hr_wep_tex != null)
				{
					ApplyWeaponHighResoTexs(lo_hr_wep_tex, high_reso_tex_flags, mate3, mate4);
				}
			}
		}
		if (animator != null && lo_anim != null)
		{
			RuntimeAnimatorController runtimeAnimatorController = lo_anim.loadedObjects[0].obj as RuntimeAnimatorController;
			if (runtimeAnimatorController != null)
			{
				animator.runtimeAnimatorController = runtimeAnimatorController;
				if (player != null)
				{
					animator.gameObject.AddComponent<StageObjectProxy>().stageObject = player;
					if (need_anim_event)
					{
						player.animEventData = (lo_anim.loadedObjects[1].obj as AnimEventData);
					}
				}
				animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
				if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.isPlaySpAttackTypeMotion)
				{
					SP_ATTACK_TYPE weaponSpAttackType = (SP_ATTACK_TYPE)info.weaponSpAttackType;
					if (weaponSpAttackType != 0)
					{
						string text6 = weaponSpAttackType.ToString();
						int parameterCount = animator.parameterCount;
						for (int num31 = 0; num31 < parameterCount; num31++)
						{
							if (animator.GetParameter(num31).name == text6)
							{
								animator.SetTrigger(text6);
								if (MonoBehaviourSingleton<EffectManager>.IsValid() && isSoulArrowOutGameEffect)
								{
									EffectManager.GetEffect("ef_btl_wsk2_bow_01_01", socketWepR);
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
			voiceAudioClips = lo_voices.loadedObjects;
			UpdateVoiceAudioClipIds();
		}
		if (!lo_accessories.IsNullOrEmpty())
		{
			List<Renderer> accRendererList = new List<Renderer>();
			int skill_len3 = 0;
			int k = lo_accessories.Count;
			while (skill_len3 < k)
			{
				LoadObject loadObject5 = lo_accessories[skill_len3];
				Transform accTrans = null;
				if (!div_frame_realizes)
				{
					accTrans = loadObject5.Realizes();
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, loadObject5.loadedObject, delegate(InstantiateManager.InstantiateData data)
					{
						accTrans = ((GameObject)data.instantiatedObject).transform;
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
				}
				if (accTrans != null)
				{
					AccessoryTable.AccessoryInfoData infoData = Singleton<AccessoryTable>.I.GetInfoData(info.accUIDs[skill_len3]);
					accTrans.SetParent(GetNodeTrans(infoData.node));
					accTrans.localPosition = infoData.offset;
					accTrans.localRotation = infoData.rotation;
					accTrans.localScale = infoData.scale;
					accessory.Add(accTrans);
					accRendererList.AddRange(accTrans.GetComponentsInChildren<Renderer>());
				}
				int num13 = skill_len3 + 1;
				skill_len3 = num13;
			}
			if (!accessory.IsNullOrEmpty())
			{
				k = 0;
				skill_len3 = accessory.Count;
				while (k < skill_len3)
				{
					Transform equipItemRoot = accessory[k];
					yield return StartCoroutine(ItemLoader.InitRoopEffect(load_queue, equipItemRoot, shader_type));
					int num13 = k + 1;
					k = num13;
				}
			}
			renderersAccessory = accRendererList.ToArray();
			ModelLoaderBase.SetEnabled(renderersAccessory, is_enable: false);
		}
		switch (shader_type)
		{
		case SHADER_TYPE.LIGHTWEIGHT:
			ShaderGlobal.ChangeWantLightweightShader(renderersWep);
			ShaderGlobal.ChangeWantLightweightShader(renderersFace);
			ShaderGlobal.ChangeWantLightweightShader(renderersHair);
			ShaderGlobal.ChangeWantLightweightShader(renderersBody);
			ShaderGlobal.ChangeWantLightweightShader(renderersHead);
			ShaderGlobal.ChangeWantLightweightShader(renderersArm);
			ShaderGlobal.ChangeWantLightweightShader(renderersLeg);
			ShaderGlobal.ChangeWantLightweightShader(renderersAccessory);
			break;
		case SHADER_TYPE.UI:
			ShaderGlobal.ChangeWantUIShader(renderersWep);
			ShaderGlobal.ChangeWantUIShader(renderersFace);
			ShaderGlobal.ChangeWantUIShader(renderersHair);
			ShaderGlobal.ChangeWantUIShader(renderersBody);
			ShaderGlobal.ChangeWantUIShader(renderersHead);
			ShaderGlobal.ChangeWantUIShader(renderersArm);
			ShaderGlobal.ChangeWantUIShader(renderersLeg);
			ShaderGlobal.ChangeWantUIShader(renderersAccessory);
			break;
		}
		SetLightProbes(enable_light_probes);
		if (layer != -1)
		{
			SetLayerWithChildren_SecondaryNoChange(_this, layer);
		}
		SetRenderersEnabled(renderersWep, is_enabled: true);
		SetRenderersEnabled(renderersFace, is_enabled: true);
		SetRenderersEnabled(renderersHair, is_enabled: true);
		SetRenderersEnabled(renderersBody, is_enabled: true);
		SetRenderersEnabled(renderersHead, is_enabled: true);
		SetRenderersEnabled(renderersArm, is_enabled: true);
		SetRenderersEnabled(renderersLeg, is_enabled: true);
		SetRenderersEnabled(renderersAccessory, is_enabled: true);
		if (need_shadow && shadow == null)
		{
			shadow = CreateShadow(_this, fixedY0: true, -1, shader_type == SHADER_TYPE.LIGHTWEIGHT);
		}
		if (player != null)
		{
			if (player.controller != null)
			{
				player.controller.enabled = true;
			}
			player.OnLoadComplete();
			if (player.packetReceiver != null)
			{
				player.packetReceiver.SetStopPacketUpdate(is_stop: false);
			}
		}
		if (player != null && is_self && MonoBehaviourSingleton<AudioListenerManager>.IsValid())
		{
			MonoBehaviourSingleton<AudioListenerManager>.I.SetTargetObject(player);
		}
		callback?.Invoke(player);
		ResetDynamicBones(dynamicBones);
		ResetDynamicBones(dynamicBones_Body);
		if (is_self)
		{
			ResourceLoad component = player.gameObject.GetComponent<ResourceLoad>();
			if (component != null && component.list != null)
			{
				List<string> list2 = new List<string>();
				int num32 = 0;
				for (int size = component.list.size; num32 < size; num32++)
				{
					list2.Add(component.list.buffer[num32].name);
				}
				list2.Distinct();
				MonoBehaviourSingleton<ResourceManager>.I.cache.AddIgnoreCategorySpecifiedReleaseList(list2);
			}
		}
		isLoading = false;
	}

	protected virtual IEnumerator DoLoad_GG_Optimize(PlayerLoadInfo info, int layer, int anim_id, bool need_anim_event, bool need_foot_stamp, bool need_shadow, bool enable_light_probes, bool need_action_voice, bool need_high_reso_tex, bool need_res_ref_count, bool need_dev_frame_instantiate, SHADER_TYPE shader_type, OnCompleteLoad callback, bool enable_eye_blick, int use_hair_overlay)
	{
		bool gg_op = true;
		if (info == null)
		{
			Log.Error(LOG.RESOURCE, "PlayerLoader:info=null");
		}
		SerializePlayerLoadInfo(info);
		animObjectTable = new StringKeyTable<LoadObject>();
		Player player = base.gameObject.GetComponent<Player>();
		bool enableBone = _EnableDynamicBone(shader_type, player is Self);
		EquipModelTable.Data data2 = Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.ARMOR, info.bodyModelID);
		EquipModelTable.Data data3 = data2.needHelm ? Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.HELM, info.headModelID) : null;
		EquipModelTable.Data arm_model_data = data2.needArm ? Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.ARM, info.armModelID) : null;
		EquipModelTable.Data leg_model_data = data2.needLeg ? Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.LEG, info.legModelID) : null;
		int num = data3?.GetHairModelID(info.hairModelID) ?? data2.GetHairModelID(info.hairModelID);
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 0)
		{
			need_high_reso_tex = false;
			use_hair_overlay = -1;
		}
		int high_reso_tex_flags = 0;
		if (need_high_reso_tex && MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
		{
			EquipModelHQTable equipModelHQTable = MonoBehaviourSingleton<GlobalSettingsManager>.I.equipModelHQTable;
			high_reso_tex_flags = equipModelHQTable.GetWeaponFlag(info.weaponModelID);
		}
		bool is_self = false;
		if (player != null)
		{
			_ = player.id;
			is_self = (player is Self);
		}
		DeleteLoadedObjects();
		loadInfo = info;
		if (anim_id < 0)
		{
			anim_id = ((anim_id != -1 || info.weaponModelID == -1) ? (-anim_id + info.weaponModelID / 1000) : (info.weaponModelID / 1000));
		}
		bool flag = data3?.needFace ?? data2.needFace;
		int num2 = data3?.hairMode ?? data2.hairMode;
		string face_name = (info.faceModelID > -1 && flag) ? ResourceName.GetPlayerFace(info.faceModelID) : null;
		string hair_name = (num > -1 && num2 != 0) ? ResourceName.GetPlayerHead(num) : null;
		string body_name = (info.bodyModelID > -1) ? ResourceName.GetPlayerBody(info.bodyModelID) : null;
		string head_name = (info.headModelID > -1 && data3 != null) ? ResourceName.GetPlayerHead(info.headModelID) : null;
		string arm_name = (info.armModelID > -1 && arm_model_data != null) ? ResourceName.GetPlayerArm(info.armModelID) : null;
		string leg_name = (info.legModelID > -1 && leg_model_data != null) ? ResourceName.GetPlayerLeg(info.legModelID) : null;
		string wepn_name = (info.weaponModelID > -1) ? ResourceName.GetPlayerWeapon(info.weaponModelID) : null;
		if (body_name == null)
		{
			yield break;
		}
		Transform _this = base.transform;
		if (player != null)
		{
			if (player.controller != null)
			{
				player.controller.enabled = false;
			}
			if (player.packetReceiver != null)
			{
				player.packetReceiver.SetStopPacketUpdate(is_stop: true);
			}
			player.OnLoadStart();
		}
		isLoading = true;
		LoadingQueue load_queue = new LoadingQueue(this, need_res_ref_count);
		LoadObject lo_face = null;
		LoadObject lo_hair = null;
		LoadObject lo_body = null;
		LoadObject lo_head = null;
		LoadObject lo_arm = null;
		LoadObject lo_leg = null;
		LoadObject lo_wepn = null;
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_FACE, face_name))
		{
			lo_face = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_FACE, face_name);
		}
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name))
		{
			lo_hair = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_HEAD, hair_name);
		}
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_BDY, body_name))
		{
			lo_body = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_BDY, body_name);
		}
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_HEAD, head_name))
		{
			lo_head = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_HEAD, head_name);
		}
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ARM, arm_name))
		{
			lo_arm = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_ARM, arm_name);
		}
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_LEG, leg_name))
		{
			lo_leg = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_LEG, leg_name);
		}
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name))
		{
			lo_wepn = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name);
		}
		List<LoadObject> lo_accessories = new List<LoadObject>();
		if (!info.accUIDs.IsNullOrEmpty())
		{
			int l = 0;
			for (int count = info.accUIDs.Count; l < count; l++)
			{
				string playerAccessory = ResourceName.GetPlayerAccessory(Singleton<AccessoryTable>.I.GetInfoData(info.accUIDs[l]).accessoryId);
				lo_accessories.Add(need_dev_frame_instantiate ? load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_ACCESSORY, playerAccessory) : load_queue.Load(RESOURCE_CATEGORY.PLAYER_ACCESSORY, playerAccessory));
			}
		}
		LoadObject lo_voices = null;
		LoadObject lo_hr_wep_tex = null;
		LoadObject lo_hr_hed_tex = null;
		LoadObject lo_hr_bdy_tex = null;
		LoadObject lo_hr_arm_tex = null;
		LoadObject lo_hr_leg_tex = null;
		string anim_name = (anim_id > -1) ? ResourceName.GetPlayerAnim(anim_id) : null;
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
		}
		if (info.isNeedToCache)
		{
			if (lo_face != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_FACE, face_name, lo_face.loadedObject);
			}
			if (lo_hair != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name, lo_hair.loadedObject);
			}
			if (lo_body != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_BDY, body_name, lo_body.loadedObject);
			}
			if (lo_head != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_HEAD, head_name, lo_head.loadedObject);
			}
			if (lo_arm != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_ARM, arm_name, lo_arm.loadedObject);
			}
			if (lo_leg != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_LEG, leg_name, lo_leg.loadedObject);
			}
			if (lo_wepn != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name, lo_wepn.loadedObject);
			}
		}
		LoadObject lo_anim = (anim_name != null) ? load_queue.Load(RESOURCE_CATEGORY.PLAYER_ANIM, anim_name, (!need_anim_event) ? new string[1]
		{
			anim_name + "Ctrl"
		} : new string[2]
		{
			anim_name + "Ctrl",
			anim_name + "Event"
		}) : null;
		if (lo_anim != null)
		{
			animObjectTable.Add("BASE", lo_anim);
		}
		if (player != null && anim_id > -1)
		{
			List<string> list = new List<string>();
			int num3 = 3;
			for (int m = 0; m < num3; m++)
			{
				for (int n = 0; n < 2; n++)
				{
					SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + m);
					if (skillParam == null)
					{
						continue;
					}
					string ctrlNameFromAnimFormatName = Character.GetCtrlNameFromAnimFormatName((n == 0) ? skillParam.tableData.castStateName : skillParam.tableData.actStateName);
					if (!string.IsNullOrEmpty(ctrlNameFromAnimFormatName) && list.IndexOf(ctrlNameFromAnimFormatName) < 0)
					{
						list.Add(ctrlNameFromAnimFormatName);
						string playerSubAnim = ResourceName.GetPlayerSubAnim(anim_id, ctrlNameFromAnimFormatName);
						LoadObject loadObject = (playerSubAnim != null) ? load_queue.Load(RESOURCE_CATEGORY.PLAYER_ANIM_SKILL, playerSubAnim, (!need_anim_event) ? new string[1]
						{
							playerSubAnim + "Ctrl"
						} : new string[2]
						{
							playerSubAnim + "Ctrl",
							playerSubAnim + "Event"
						}) : null;
						if (loadObject != null)
						{
							animObjectTable.Add(ctrlNameFromAnimFormatName, loadObject);
						}
					}
				}
			}
		}
		if (player != null && info.weaponEvolveId != 0)
		{
			ResourceName.GetPlayerEvolveAnim(info.weaponEvolveId, out string ctrlName, out string animName);
			LoadObject loadObject2 = load_queue.Load(RESOURCE_CATEGORY.PLAYER_ANIM_EVOLVE, animName, new string[2]
			{
				animName + "Ctrl",
				animName + "Event"
			});
			if (loadObject2 != null)
			{
				animObjectTable.Add(ctrlName, loadObject2);
			}
		}
		if (need_action_voice && info.actionVoiceBaseID > -1)
		{
			int[] array = (int[])Enum.GetValues(typeof(ACTION_VOICE_ID));
			int num4 = array.Length;
			string[] array2 = new string[num4];
			for (int num5 = 0; num5 < num4; num5++)
			{
				array2[num5] = ResourceName.GetActionVoiceName(info.actionVoiceBaseID + array[num5]);
			}
			lo_voices = load_queue.Load(RESOURCE_CATEGORY.SOUND_VOICE, ResourceName.GetActionVoicePackageNameFromVoiceID(info.actionVoiceBaseID), array2);
		}
		if (need_high_reso_tex)
		{
			if (high_reso_tex_flags != 0 && wepn_name != null)
			{
				lo_hr_wep_tex = LoadHighResoTexs(load_queue, wepn_name, high_reso_tex_flags);
			}
			if (head_name != null)
			{
				lo_hr_hed_tex = LoadHighResoTexs(load_queue, head_name, 1);
			}
			if (body_name != null)
			{
				lo_hr_bdy_tex = LoadHighResoTexs(load_queue, body_name, 1);
			}
			if (arm_name != null)
			{
				lo_hr_arm_tex = LoadHighResoTexs(load_queue, arm_name, 1);
			}
			if (leg_name != null)
			{
				lo_hr_leg_tex = LoadHighResoTexs(load_queue, leg_name, 1);
			}
		}
		LoadObject loHairOverlay = null;
		if (use_hair_overlay != -1)
		{
			loHairOverlay = LoadHairOverlayTexs(load_queue, info, use_hair_overlay);
		}
		yield return load_queue.Wait();
		List<string> needAtkInfoNames = new List<string>();
		StringKeyTable<LoadObject> animEventBulletLoadObjTable = new StringKeyTable<LoadObject>();
		if (player != null)
		{
			if (need_anim_event)
			{
				animObjectTable.ForEach(delegate(LoadObject load_object)
				{
					if (!gg_op)
					{
						load_queue.CacheAnimDataUseResource(load_object.loadedObjects[1].obj as AnimEventData, (string effect_name) => (effect_name[0] != '@') ? effect_name : null);
					}
					load_queue.CacheAnimDataUseResourceDependPlayer(player, load_object.loadedObjects[1].obj as AnimEventData);
					AnimEventData animEventData = load_object.loadedObjects[1].obj as AnimEventData;
					if (animEventData != null && !animEventData.animations.IsNullOrEmpty())
					{
						AnimEventData.AnimData[] animations = animEventData.animations;
						for (int num29 = 0; num29 < animations.Length; num29++)
						{
							AnimEventData.EventData[] events = animations[num29].events;
							foreach (AnimEventData.EventData eventData in events)
							{
								AnimEventFormat.ID id = eventData.id;
								switch (id)
								{
								case AnimEventFormat.ID.SHOT_PRESENT:
								{
									int num32 = 0;
									for (int num33 = eventData.stringArgs.Length; num32 < num33; num32++)
									{
										string[] array4 = eventData.stringArgs[num32].Split(':');
										if (!array4.IsNullOrEmpty())
										{
											int num34 = 0;
											for (int num35 = array4.Length; num34 < num35; num34++)
											{
												string text8 = array4[num34];
												if (!string.IsNullOrEmpty(text8))
												{
													LoadObject loadObject7 = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, text8);
													if (loadObject7 != null && animEventBulletLoadObjTable.Get(text8) == null)
													{
														animEventBulletLoadObjTable.Add(text8, loadObject7);
													}
												}
											}
										}
									}
									break;
								}
								case AnimEventFormat.ID.SHOT_ZONE:
								case AnimEventFormat.ID.SHOT_DECOY:
								case AnimEventFormat.ID.LOAD_BULLET:
								{
									for (int num31 = 0; num31 < eventData.stringArgs.Length; num31++)
									{
										string text7 = eventData.stringArgs[num31];
										if (!string.IsNullOrEmpty(text7))
										{
											LoadObject loadObject6 = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, text7);
											if (loadObject6 != null && animEventBulletLoadObjTable.Get(text7) == null)
											{
												animEventBulletLoadObjTable.Add(text7, loadObject6);
											}
										}
									}
									break;
								}
								}
								switch (id)
								{
								case AnimEventFormat.ID.SHOT_ARROW:
								case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE:
								case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_START:
								case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_END:
								case AnimEventFormat.ID.SHOT_GENERIC:
								case AnimEventFormat.ID.SHOT_TARGET:
								case AnimEventFormat.ID.SHOT_POINT:
								case AnimEventFormat.ID.NWAY_LASER_ATTACK:
								case AnimEventFormat.ID.EXATK_COLLIDER_START:
								case AnimEventFormat.ID.GENERATE_TRACKING:
								case AnimEventFormat.ID.PLAYER_FUNNEL_ATTACK:
								case AnimEventFormat.ID.SHOT_NODE_LINK:
								case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE:
								case AnimEventFormat.ID.PAIR_SWORDS_SHOT_BULLET:
								case AnimEventFormat.ID.PAIR_SWORDS_SHOT_LASER:
								case AnimEventFormat.ID.SHOT_HEALING_HOMING:
								case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE_MULTI:
								case AnimEventFormat.ID.SHOT_RESURRECTION_HOMING:
								case AnimEventFormat.ID.BUFF_START_SHIELD_REFLECT:
								case AnimEventFormat.ID.SHOT_ORACLE_SPEAR_SP:
								{
									string text9 = eventData.stringArgs[0];
									if (!string.IsNullOrEmpty(text9))
									{
										string[] namesNeededLoadAtkInfoFromAnimEvent = ResourceName.GetNamesNeededLoadAtkInfoFromAnimEvent();
										for (int num36 = 0; num36 < namesNeededLoadAtkInfoFromAnimEvent.Length; num36++)
										{
											if (text9.StartsWith(namesNeededLoadAtkInfoFromAnimEvent[num36]))
											{
												needAtkInfoNames.Add(text9);
												break;
											}
										}
									}
									break;
								}
								}
							}
						}
					}
				});
			}
			AddSkillAttackInfoName(player, ref needAtkInfoNames);
			AddFieldGimmickAttackInfoName(ref needAtkInfoNames);
			List<LoadObject> atkInfo = new List<LoadObject>();
			List<LoadObject> atkInfoLoaded = new List<LoadObject>();
			Dictionary<string, LoadObject> atkInfoDict = new Dictionary<string, LoadObject>();
			if (playerLoaderLoadedAttackInfoNames == null)
			{
				playerLoaderLoadedAttackInfoNames = new HashSet<string>();
			}
			for (int num6 = 0; num6 < needAtkInfoNames.Count; num6++)
			{
				if (!playerLoaderLoadedAttackInfoNames.Contains(needAtkInfoNames[num6]))
				{
					if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[num6]))
					{
						LoadObject loadObject3 = load_queue.Load(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[num6]);
						atkInfo.Add(loadObject3);
						atkInfoLoaded.Add(loadObject3);
						atkInfoDict.Add(needAtkInfoNames[num6], loadObject3);
					}
					else
					{
						UnityEngine.Object selfPlayerResourceCache = MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[num6]);
						LoadObject loadObject4 = new LoadObject();
						loadObject4.loadedObject = selfPlayerResourceCache;
						atkInfoLoaded.Add(loadObject4);
						atkInfoDict.Add(needAtkInfoNames[num6], loadObject4);
					}
					playerLoaderLoadedAttackInfoNames.Add(needAtkInfoNames[num6]);
				}
			}
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
			if (info.isNeedToCache)
			{
				foreach (KeyValuePair<string, LoadObject> item in atkInfoDict)
				{
					string key2 = item.Key;
					LoadObject value = item.Value;
					if (atkInfo.Contains(value))
					{
						MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, key2, value.loadedObject);
					}
				}
			}
			if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				Transform _settingTransform = MonoBehaviourSingleton<InGameSettingsManager>.I._transform;
				List<AttackInfo> hitInfos = new List<AttackInfo>();
				for (int k = 0; k < atkInfoLoaded.Count; k++)
				{
					GameObject gameObject = LoadObject.RealizesWithGameObject((GameObject)atkInfoLoaded[k].loadedObject, _settingTransform).gameObject;
					SplitPlayerAttackInfo attackInfo = gameObject.GetComponent<SplitPlayerAttackInfo>();
					hitInfos.Add(attackInfo.attackHitInfo);
					hitInfos.Add(attackInfo.attackContinuationInfo);
					if (!string.IsNullOrEmpty(attackInfo.attackHitInfo.nextBulletInfoName))
					{
						yield return StartCoroutine(LoadNextBulletInfo(load_queue, attackInfo.attackHitInfo.nextBulletInfoName, hitInfos, info.isNeedToCache));
					}
					if (!string.IsNullOrEmpty(attackInfo.attackContinuationInfo.nextBulletInfoName))
					{
						yield return StartCoroutine(LoadNextBulletInfo(load_queue, attackInfo.attackContinuationInfo.nextBulletInfoName, hitInfos, info.isNeedToCache));
					}
				}
				InGameSettingsManager.Player player2 = MonoBehaviourSingleton<InGameSettingsManager>.I.player;
				if (player2.attackInfosAll == null)
				{
					player2.attackInfosAll = new AttackInfo[0];
				}
				AttackInfo[] array3 = Utility.CreateMergedArray(player2.attackInfosAll, hitInfos.ToArray());
				player2.attackInfosAll = Utility.DistinctArray(array3);
				player.AddAttackInfos(hitInfos.ToArray());
			}
			LoadAttackInfoResource(player, load_queue);
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
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
						if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < ohsActionInfo.Soul_BoostElementHitEffect.Length)
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsActionInfo.Soul_BoostElementHitEffect[(int)nowWeaponElement]);
						}
					}
					break;
				case SP_ATTACK_TYPE.BURST:
				{
					InGameSettingsManager.Player.BurstOneHandSwordActionInfo burstOHSInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.burstOHSInfo;
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < burstOHSInfo.BoostElementHitEffect.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstOHSInfo.BoostElementHitEffect[(int)nowWeaponElement]);
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
					break;
				}
				case SP_ATTACK_TYPE.ORACLE:
				{
					InGameSettingsManager.Player.OracleOneHandSwordActionInfo oracleOHSInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.oracleOHSInfo;
					for (int num9 = 0; num9 < oracleOHSInfo.dragonEffects.Length; num9++)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleOHSInfo.dragonEffects[num9].GetEffectName(nowWeaponElement));
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_sword_dragon_veil");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_sword_dragon_veil_re");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, $"ef_btl_wsk4_sword_02_{(int)nowWeaponElement:D2}");
					break;
				}
				}
				break;
			case 1:
			{
				if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
				{
					break;
				}
				InGameSettingsManager.Player.TwoHandSwordActionInfo twoHandSwordActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo;
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, twoHandSwordActionInfo.nameChargeExpandEffect);
				if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
				{
					load_queue.CacheSE(twoHandSwordActionInfo.soulBoostSeId);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, twoHandSwordActionInfo.soulIaiChargeMaxEffect);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_btl_soul_energy_01");
				}
				if (player.spAttackType == SP_ATTACK_TYPE.BURST)
				{
					InGameSettingsManager.Player.BurstTwoHandSwordActionInfo burstTHSInfo = twoHandSwordActionInfo.burstTHSInfo;
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < burstTHSInfo.HitEffect_SingleShot.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstTHSInfo.HitEffect_SingleShot[(int)nowWeaponElement]);
					}
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < burstTHSInfo.HitEffect_FullBurst.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstTHSInfo.HitEffect_FullBurst[(int)nowWeaponElement]);
					}
				}
				if (player.spAttackType == SP_ATTACK_TYPE.ORACLE)
				{
					InGameSettingsManager.Player.OracleTwoHandSwordActionInfo oracleTHSInfo = twoHandSwordActionInfo.oracleTHSInfo;
					load_queue.CacheSE(oracleTHSInfo.normalVernierSeId);
					load_queue.CacheSE(oracleTHSInfo.maxVernierSeId);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleTHSInfo.normalVernierEffectName);
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < oracleTHSInfo.maxVernierEffectNames.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleTHSInfo.maxVernierEffectNames[(int)nowWeaponElement]);
					}
				}
				break;
			}
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
						{
							load_queue.CacheSE(burstSpearInfo.spinSeId);
						}
						if (burstSpearInfo.spinMaxSpeedSeId > 0)
						{
							load_queue.CacheSE(burstSpearInfo.spinMaxSpeedSeId);
						}
						string text = string.Empty;
						if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < burstSpearInfo.spinEffectNames.Length)
						{
							text = burstSpearInfo.spinEffectNames[(int)nowWeaponElement];
						}
						string text2 = string.Empty;
						if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < burstSpearInfo.spinElementHitEffectNames.Length)
						{
							text2 = burstSpearInfo.spinElementHitEffectNames[(int)nowWeaponElement];
						}
						string text3 = string.Empty;
						if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < burstSpearInfo.spinThrowGroundEffectNames.Length)
						{
							text3 = burstSpearInfo.spinThrowGroundEffectNames[(int)nowWeaponElement];
						}
						if (!string.IsNullOrEmpty(text))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text);
						}
						if (!string.IsNullOrEmpty(text2))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text2);
						}
						if (!string.IsNullOrEmpty(text3))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text3);
						}
						if (!string.IsNullOrEmpty(burstSpearInfo.throwGroundEffectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstSpearInfo.throwGroundEffectName);
						}
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
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < spearActionInfo.jumpHugeElementHitEffectNames.Length)
					{
						name = spearActionInfo.jumpHugeElementHitEffectNames[(int)nowWeaponElement];
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name);
				}
				break;
			case 4:
				if (player.spAttackType == SP_ATTACK_TYPE.NONE)
				{
					if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
					{
						load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.wildDanceChargeMaxSeId);
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
				}
				else if (player.spAttackType == SP_ATTACK_TYPE.HEAT)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_02");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_03");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_04");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_05");
				}
				else if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
				{
					if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
					{
						break;
					}
					InGameSettingsManager.Player.PairSwordsActionInfo pairSwordsActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo;
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, pairSwordsActionInfo.Soul_EffectForWaitingLaser);
					string name2 = pairSwordsActionInfo.Soul_EffectForBullet;
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < pairSwordsActionInfo.Soul_EffectsForBullet.Length)
					{
						name2 = pairSwordsActionInfo.Soul_EffectsForBullet[(int)nowWeaponElement];
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name2);
					if (pairSwordsActionInfo.Soul_SeIds.IsNullOrEmpty())
					{
						break;
					}
					for (int num10 = 0; num10 < pairSwordsActionInfo.Soul_SeIds.Length; num10++)
					{
						if (pairSwordsActionInfo.Soul_SeIds[num10] >= 0)
						{
							load_queue.CacheSE(pairSwordsActionInfo.Soul_SeIds[num10]);
						}
					}
				}
				else if (player.spAttackType == SP_ATTACK_TYPE.BURST)
				{
					load_queue.CacheSE(10000051);
					load_queue.CacheSE(10000042);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_twinsword_01_00");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
					InGameSettingsManager.Player.PairSwordsActionInfo pairSwordsActionInfo2 = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo;
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < pairSwordsActionInfo2.Burst_CombineHitEffect.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, pairSwordsActionInfo2.Burst_CombineHitEffect[(int)nowWeaponElement]);
					}
				}
				break;
			case 5:
			{
				if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
				{
					break;
				}
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
				else if (player.spAttackType == SP_ATTACK_TYPE.HEAT)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[22]);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[21]);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_bow_01_02");
				}
				else if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[24]);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_bow_lock_02");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
					load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulLockMaxSeId);
					load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulLockSeId);
					load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.soulBoostSeId);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
				}
				else if (player.spAttackType == SP_ATTACK_TYPE.BURST)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[27]);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[26]);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.GetBombArrowEffectName(nowWeaponElement));
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.arrowRainShotAimLesserCursorEffectName);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.GetBombEffectName(nowWeaponElement));
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.boostArrowChargeMaxEffectName);
					load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.burstBoostModeSEId);
					List<int> bombArrowSEIdList = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombArrowSEIdList;
					for (int num7 = 0; num7 < bombArrowSEIdList.Count; num7++)
					{
						load_queue.CacheSE(bombArrowSEIdList[num7]);
					}
					List<int> bombSEIdList = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombSEIdList;
					for (int num8 = 0; num8 < bombSEIdList.Count; num8++)
					{
						load_queue.CacheSE(bombSEIdList[num8]);
					}
				}
				break;
			}
			}
			EvolveController.Load(load_queue, info.weaponEvolveId);
			int skill_len3 = 3;
			LoadObject[] bullet_load = new LoadObject[skill_len3];
			for (int num11 = 0; num11 < skill_len3; num11++)
			{
				SkillInfo.SkillParam skillParam2 = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + num11);
				if (skillParam2 == null)
				{
					bullet_load[num11] = null;
					continue;
				}
				SkillItemTable.SkillItemData tableData = skillParam2.tableData;
				if (!string.IsNullOrEmpty(tableData.startEffectName))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.startEffectName);
				}
				if (tableData.startSEID != 0)
				{
					load_queue.CacheSE(tableData.startSEID);
				}
				if (!string.IsNullOrEmpty(tableData.actLocalEffectName))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.actLocalEffectName);
				}
				if (!string.IsNullOrEmpty(tableData.actOneshotEffectName))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.actOneshotEffectName);
				}
				if (tableData.actSEID != 0)
				{
					load_queue.CacheSE(tableData.actSEID);
				}
				if (!string.IsNullOrEmpty(tableData.enchantEffectName))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.enchantEffectName);
				}
				if (!string.IsNullOrEmpty(tableData.hitEffectName))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.hitEffectName);
				}
				if ((float)tableData.skillRange > 0f && !string.IsNullOrEmpty(MonoBehaviourSingleton<InGameSettingsManager>.I.player.skillRangeEffectName))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.skillRangeEffectName);
				}
				if (tableData.hitSEID != 0)
				{
					load_queue.CacheSE(tableData.hitSEID);
				}
				if (is_self)
				{
					load_queue.CacheItemIcon(tableData.iconID);
				}
				if (tableData.healType == HEAL_TYPE.RESURRECTION_ALL)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_heal_04_03");
				}
				if (!tableData.buffTableIds.IsNullOrEmpty())
				{
					for (int num12 = 0; num12 < tableData.buffTableIds.Length; num12++)
					{
						BuffTable.BuffData data4 = Singleton<BuffTable>.I.GetData((uint)tableData.buffTableIds[num12]);
						if (BuffParam.IsHitAbsorbType(data4.type))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_drain_01_01");
						}
						else if (data4.type == BuffParam.BUFFTYPE.AUTO_REVIVE)
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_heal_04_03");
						}
					}
				}
				string[] supportEffectName = tableData.supportEffectName;
				foreach (string text4 in supportEffectName)
				{
					if (!string.IsNullOrEmpty(text4))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text4);
					}
				}
				BuffParam.BUFFTYPE[] supportType = tableData.supportType;
				for (int num13 = 0; num13 < supportType.Length; num13++)
				{
					switch (supportType[num13])
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
				bullet_load[num11] = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, tableData.bulletName);
			}
			if (is_self)
			{
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_darkness_02");
			}
			EffectPlayProcessor effectPlayProcessor = player.effectPlayProcessor;
			if (effectPlayProcessor != null && effectPlayProcessor.effectSettings != null)
			{
				int num14 = 0;
				for (int num15 = effectPlayProcessor.effectSettings.Length; num14 < num15; num14++)
				{
					if (string.IsNullOrEmpty(effectPlayProcessor.effectSettings[num14].effectName))
					{
						continue;
					}
					string name3 = effectPlayProcessor.effectSettings[num14].name;
					if (name3.StartsWith("BUFF_"))
					{
						string text5 = name3.Substring(name3.Length - "_PLC00".Length);
						if (text5.Contains("_PLC") && text5 != "_PLC" + (loadInfo.weaponModelID / 1000).ToString("D2"))
						{
							continue;
						}
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor.effectSettings[num14].effectName);
				}
			}
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
			for (int num16 = 0; num16 < skill_len3; num16++)
			{
				SkillInfo.SkillParam skillParam3 = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + num16);
				if (skillParam3 != null)
				{
					skillParam3.bullet = (bullet_load[num16].loadedObject as BulletData);
					load_queue.CacheBulletDataUseResource(skillParam3.bullet, player);
				}
			}
			if (animEventBulletLoadObjTable != null)
			{
				animEventBulletLoadObjTable.ForEachKeyAndValue(delegate(string key, LoadObject item)
				{
					if (item != null && item.loadedObject != null)
					{
						BulletData bulletData = item.loadedObject as BulletData;
						if (bulletData != null && player.cachedBulletDataTable.Get(key) == null)
						{
							player.cachedBulletDataTable.Add(key, bulletData);
							load_queue.CacheBulletDataUseResource(bulletData, player);
						}
					}
				});
			}
			animEventBulletLoadObjTable.Clear();
			animEventBulletLoadObjTable = null;
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
		}
		if (lo_arm != null)
		{
			if (lo_arm.loadedObject != null)
			{
				GameObject gameObject2 = lo_arm.loadedObject as GameObject;
				EffectPlayProcessor effectPlayProcessor2 = (gameObject2 != null) ? gameObject2.GetComponent<EffectPlayProcessor>() : null;
				if (effectPlayProcessor2 != null && effectPlayProcessor2.effectSettings != null)
				{
					int num17 = 0;
					for (int num18 = effectPlayProcessor2.effectSettings.Length; num17 < num18; num17++)
					{
						if (!string.IsNullOrEmpty(effectPlayProcessor2.effectSettings[num17].effectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor2.effectSettings[num17].effectName);
						}
					}
				}
			}
		}
		else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ARM, arm_name))
		{
			GameObject gameObject3 = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ARM, arm_name);
			EffectPlayProcessor effectPlayProcessor3 = (gameObject3 != null) ? gameObject3.GetComponent<EffectPlayProcessor>() : null;
			if (effectPlayProcessor3 != null && effectPlayProcessor3.effectSettings != null)
			{
				int num19 = 0;
				for (int num20 = effectPlayProcessor3.effectSettings.Length; num19 < num20; num19++)
				{
					if (!string.IsNullOrEmpty(effectPlayProcessor3.effectSettings[num19].effectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor3.effectSettings[num19].effectName);
					}
				}
			}
		}
		if (lo_leg != null)
		{
			if (lo_leg.loadedObject != null)
			{
				GameObject gameObject4 = lo_leg.loadedObject as GameObject;
				EffectPlayProcessor effectPlayProcessor4 = (gameObject4 != null) ? gameObject4.GetComponent<EffectPlayProcessor>() : null;
				if (effectPlayProcessor4 != null && effectPlayProcessor4.effectSettings != null)
				{
					int num21 = 0;
					for (int num22 = effectPlayProcessor4.effectSettings.Length; num21 < num22; num21++)
					{
						if (!string.IsNullOrEmpty(effectPlayProcessor4.effectSettings[num21].effectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor4.effectSettings[num21].effectName);
						}
					}
				}
			}
		}
		else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_LEG, leg_name))
		{
			GameObject gameObject5 = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_LEG, leg_name);
			EffectPlayProcessor effectPlayProcessor5 = (gameObject5 != null) ? gameObject5.GetComponent<EffectPlayProcessor>() : null;
			if (effectPlayProcessor5 != null && effectPlayProcessor5.effectSettings != null)
			{
				int num23 = 0;
				for (int num24 = effectPlayProcessor5.effectSettings.Length; num23 < num24; num23++)
				{
					if (!string.IsNullOrEmpty(effectPlayProcessor5.effectSettings[num23].effectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor5.effectSettings[num23].effectName);
					}
				}
			}
		}
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
		}
		bool wait = false;
		bool div_frame_realizes = false;
		int skin_color = info.skinColor;
		if (lo_body != null)
		{
			if (!div_frame_realizes)
			{
				body = lo_body.Realizes(_this);
				if (body == null)
				{
					yield break;
				}
				renderersBody = body.gameObject.GetComponentsInChildren<Renderer>();
				ModelLoaderBase.SetEnabled(renderersBody, is_enable: false);
				SetDynamicBones_Body(body, enableBone);
			}
			else
			{
				wait = true;
				InstantiateManager.Request(this, lo_body.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					body = ((GameObject)data.instantiatedObject).transform;
					body.SetParent(_this, worldPositionStays: false);
					renderersBody = body.GetComponentsInChildren<Renderer>();
					SetDynamicBones_Body(body, enableBone);
					SetRenderersEnabled(renderersBody, is_enabled: false);
					wait = false;
				});
				while (wait)
				{
					yield return null;
				}
			}
		}
		else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_BDY, body_name))
		{
			GameObject gameObject6 = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_BDY, body_name);
			if (gameObject6 != null)
			{
				if (!div_frame_realizes)
				{
					body = LoadObject.RealizesWithGameObject(gameObject6, _this);
					renderersBody = body.gameObject.GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersBody, is_enable: false);
					SetDynamicBones_Body(body, enableBone);
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, gameObject6, delegate(InstantiateManager.InstantiateData data)
					{
						body = ((GameObject)data.instantiatedObject).transform;
						body.SetParent(_this, worldPositionStays: false);
						renderersBody = body.GetComponentsInChildren<Renderer>();
						SetDynamicBones_Body(body, enableBone);
						SetRenderersEnabled(renderersBody, is_enabled: false);
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
				}
			}
		}
		if (body == null)
		{
			yield break;
		}
		yield return StartCoroutine(ItemLoader.InitRoopEffect(load_queue, body, shader_type));
		if (renderersBody == null)
		{
			yield break;
		}
		if (renderersBody.Length != 0)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = renderersBody[0] as SkinnedMeshRenderer;
			if (skinnedMeshRenderer != null)
			{
				skinnedMeshRenderer.localBounds = BOUNDS;
			}
		}
		SetSkinAndEquipColor(renderersBody, skin_color, info.bodyColor, 0f);
		ApplyEquipHighResoTexs(lo_hr_bdy_tex, renderersBody);
		animator = body.GetComponentInChildren<Animator>();
		if (player != null)
		{
			player.body = body;
		}
		socketRoot = Utility.Find(body, "Root");
		socketHead = Utility.Find(body, "Head");
		socketWepL = Utility.Find(body, "L_Wep");
		socketWepR = Utility.Find(body, "R_Wep");
		socketFootL = Utility.Find(body, "L_Foot");
		socketFootR = Utility.Find(body, "R_Foot");
		socketHandL = Utility.Find(body, "L_Hand");
		socketForearmL = Utility.Find(body, "L_Forearm");
		if (need_foot_stamp)
		{
			if (socketFootL != null && socketFootL.GetComponent<StampNode>() == null)
			{
				StampNode stampNode = socketFootL.gameObject.AddComponent<StampNode>();
				stampNode.offset = new Vector3(-0.08f, 0.01f, 0f);
				stampNode.autoBaseY = 0.1f;
			}
			if (socketFootR != null && socketFootR.GetComponent<StampNode>() == null)
			{
				StampNode stampNode2 = socketFootR.gameObject.AddComponent<StampNode>();
				stampNode2.offset = new Vector3(-0.08f, 0.01f, 0f);
				stampNode2.autoBaseY = 0.1f;
			}
			CharacterStampCtrl characterStampCtrl = body.GetComponent<CharacterStampCtrl>();
			if (characterStampCtrl == null)
			{
				characterStampCtrl = body.gameObject.AddComponent<CharacterStampCtrl>();
			}
			characterStampCtrl.Init(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos, player);
			int num25 = 0;
			for (int num26 = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos.Length; num25 < num26; num25++)
			{
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos[num25].effectName);
			}
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
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
		if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && info.equipType == 5 && info.weaponSpAttackType == 2)
		{
			isSoulArrowOutGameEffect = true;
		}
		StartCoroutine(DoLoadFace(lo_face, face_name, skin_color, enable_eye_blick, div_frame_realizes, delegate(bool kq)
		{
			isLoading_Face = false;
			kqLoading_Face = kq;
		}));
		StartCoroutine(DoLoadHair(lo_hair, loHairOverlay, hair_name, info, enableBone, skin_color, div_frame_realizes, delegate(bool kq)
		{
			isLoading_Hair = false;
			kqLoading_Hair = kq;
		}));
		StartCoroutine(DoLoadHead(load_queue, lo_head, lo_hr_hed_tex, head_name, info, shader_type, div_frame_realizes, delegate(bool kq)
		{
			isLoading_Head = false;
			kqLoading_Head = kq;
		}));
		StartCoroutine(DoLoadArm(lo_arm, lo_hr_arm_tex, arm_name, skin_color, info, arm_model_data, div_frame_realizes, delegate(bool kq)
		{
			isLoading_Arm = false;
			kqLoading_Arm = kq;
		}));
		StartCoroutine(DoLoadFoot(lo_leg, lo_hr_leg_tex, leg_name, skin_color, info, leg_model_data, div_frame_realizes, delegate(bool kq)
		{
			isLoading_Foot = false;
			kqLoading_Foot = kq;
		}));
		StartCoroutine(DoLoadWeapon(load_queue, lo_wepn, lo_hr_wep_tex, wepn_name, isSoulArrowOutGameEffect, high_reso_tex_flags, player, info, shader_type, div_frame_realizes, delegate(bool kq)
		{
			isLoading_Weapn = false;
			kqLoading_Weapn = kq;
		}));
		while (isLoading_Face | isLoading_Hair | isLoading_Head | isLoading_Arm | isLoading_Foot | isLoading_Weapn)
		{
			yield return null;
		}
		if (!kqLoading_Face || !kqLoading_Hair || !kqLoading_Head || !kqLoading_Arm || !kqLoading_Foot || !kqLoading_Weapn)
		{
			yield break;
		}
		if (animator != null && lo_anim != null)
		{
			RuntimeAnimatorController runtimeAnimatorController = lo_anim.loadedObjects[0].obj as RuntimeAnimatorController;
			if (runtimeAnimatorController != null)
			{
				animator.runtimeAnimatorController = runtimeAnimatorController;
				if (player != null)
				{
					animator.gameObject.AddComponent<StageObjectProxy>().stageObject = player;
					if (need_anim_event)
					{
						player.animEventData = (lo_anim.loadedObjects[1].obj as AnimEventData);
					}
				}
				animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
				if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.isPlaySpAttackTypeMotion)
				{
					SP_ATTACK_TYPE weaponSpAttackType = (SP_ATTACK_TYPE)info.weaponSpAttackType;
					if (weaponSpAttackType != 0)
					{
						string text6 = weaponSpAttackType.ToString();
						int parameterCount = animator.parameterCount;
						for (int num27 = 0; num27 < parameterCount; num27++)
						{
							if (animator.GetParameter(num27).name == text6)
							{
								animator.SetTrigger(text6);
								if (MonoBehaviourSingleton<EffectManager>.IsValid() && isSoulArrowOutGameEffect)
								{
									EffectManager.GetEffect("ef_btl_wsk2_bow_01_01", socketWepR);
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
			voiceAudioClips = lo_voices.loadedObjects;
			UpdateVoiceAudioClipIds();
		}
		if (!lo_accessories.IsNullOrEmpty())
		{
			List<Renderer> accRendererList = new List<Renderer>();
			int skill_len3 = 0;
			int k = lo_accessories.Count;
			while (skill_len3 < k)
			{
				LoadObject loadObject5 = lo_accessories[skill_len3];
				Transform accTrans = null;
				if (!div_frame_realizes)
				{
					accTrans = loadObject5.Realizes();
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, loadObject5.loadedObject, delegate(InstantiateManager.InstantiateData data)
					{
						accTrans = ((GameObject)data.instantiatedObject).transform;
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
				}
				if (accTrans != null)
				{
					AccessoryTable.AccessoryInfoData infoData = Singleton<AccessoryTable>.I.GetInfoData(info.accUIDs[skill_len3]);
					accTrans.SetParent(GetNodeTrans(infoData.node));
					accTrans.localPosition = infoData.offset;
					accTrans.localRotation = infoData.rotation;
					accTrans.localScale = infoData.scale;
					accessory.Add(accTrans);
					accRendererList.AddRange(accTrans.GetComponentsInChildren<Renderer>());
				}
				int num13 = skill_len3 + 1;
				skill_len3 = num13;
			}
			if (!accessory.IsNullOrEmpty())
			{
				k = 0;
				skill_len3 = accessory.Count;
				while (k < skill_len3)
				{
					Transform equipItemRoot = accessory[k];
					yield return StartCoroutine(ItemLoader.InitRoopEffect(load_queue, equipItemRoot, shader_type));
					int num13 = k + 1;
					k = num13;
				}
			}
			renderersAccessory = accRendererList.ToArray();
			ModelLoaderBase.SetEnabled(renderersAccessory, is_enable: false);
		}
		switch (shader_type)
		{
		case SHADER_TYPE.LIGHTWEIGHT:
			ShaderGlobal.ChangeWantLightweightShader(renderersWep);
			ShaderGlobal.ChangeWantLightweightShader(renderersFace);
			ShaderGlobal.ChangeWantLightweightShader(renderersHair);
			ShaderGlobal.ChangeWantLightweightShader(renderersBody);
			ShaderGlobal.ChangeWantLightweightShader(renderersHead);
			ShaderGlobal.ChangeWantLightweightShader(renderersArm);
			ShaderGlobal.ChangeWantLightweightShader(renderersLeg);
			ShaderGlobal.ChangeWantLightweightShader(renderersAccessory);
			break;
		case SHADER_TYPE.UI:
			ShaderGlobal.ChangeWantUIShader(renderersWep);
			ShaderGlobal.ChangeWantUIShader(renderersFace);
			ShaderGlobal.ChangeWantUIShader(renderersHair);
			ShaderGlobal.ChangeWantUIShader(renderersBody);
			ShaderGlobal.ChangeWantUIShader(renderersHead);
			ShaderGlobal.ChangeWantUIShader(renderersArm);
			ShaderGlobal.ChangeWantUIShader(renderersLeg);
			ShaderGlobal.ChangeWantUIShader(renderersAccessory);
			break;
		}
		SetLightProbes(enable_light_probes);
		if (layer != -1)
		{
			SetLayerWithChildren_SecondaryNoChange(_this, layer);
		}
		SetRenderersEnabled(renderersWep, is_enabled: true);
		SetRenderersEnabled(renderersFace, is_enabled: true);
		SetRenderersEnabled(renderersHair, is_enabled: true);
		SetRenderersEnabled(renderersBody, is_enabled: true);
		SetRenderersEnabled(renderersHead, is_enabled: true);
		SetRenderersEnabled(renderersArm, is_enabled: true);
		SetRenderersEnabled(renderersLeg, is_enabled: true);
		SetRenderersEnabled(renderersAccessory, is_enabled: true);
		if (need_shadow && shadow == null)
		{
			shadow = CreateShadow(_this, fixedY0: true, -1, shader_type == SHADER_TYPE.LIGHTWEIGHT);
		}
		if (player != null)
		{
			if (player.controller != null)
			{
				player.controller.enabled = true;
			}
			player.OnLoadComplete();
			if (player.packetReceiver != null)
			{
				player.packetReceiver.SetStopPacketUpdate(is_stop: false);
			}
		}
		if (player != null && is_self && MonoBehaviourSingleton<AudioListenerManager>.IsValid())
		{
			MonoBehaviourSingleton<AudioListenerManager>.I.SetTargetObject(player);
		}
		callback?.Invoke(player);
		ResetDynamicBones(dynamicBones);
		ResetDynamicBones(dynamicBones_Body);
		if (is_self)
		{
			ResourceLoad component = player.gameObject.GetComponent<ResourceLoad>();
			if (component != null && component.list != null)
			{
				List<string> list2 = new List<string>();
				int num28 = 0;
				for (int size = component.list.size; num28 < size; num28++)
				{
					list2.Add(component.list.buffer[num28].name);
				}
				list2.Distinct();
				MonoBehaviourSingleton<ResourceManager>.I.cache.AddIgnoreCategorySpecifiedReleaseList(list2);
			}
		}
		isLoading = false;
		if (gg_op)
		{
			DoLoadLater(need_anim_event);
		}
	}

	protected virtual IEnumerator DoLoad_GG_Optimize_Self(PlayerLoadInfo info, int layer, int anim_id, bool need_anim_event, bool need_foot_stamp, bool need_shadow, bool enable_light_probes, bool need_action_voice, bool need_high_reso_tex, bool need_res_ref_count, bool need_dev_frame_instantiate, SHADER_TYPE shader_type, OnCompleteLoad callback, bool enable_eye_blick, int use_hair_overlay)
	{
		bool gg_op = true;
		if (info == null)
		{
			Log.Error(LOG.RESOURCE, "PlayerLoader:info=null");
		}
		SerializePlayerLoadInfo(info);
		animObjectTable = new StringKeyTable<LoadObject>();
		Player player = base.gameObject.GetComponent<Player>();
		bool enableBone = _EnableDynamicBone(shader_type, player is Self);
		EquipModelTable.Data data2 = Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.ARMOR, info.bodyModelID);
		EquipModelTable.Data data3 = data2.needHelm ? Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.HELM, info.headModelID) : null;
		EquipModelTable.Data arm_model_data = data2.needArm ? Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.ARM, info.armModelID) : null;
		EquipModelTable.Data leg_model_data = data2.needLeg ? Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.LEG, info.legModelID) : null;
		int num = data3?.GetHairModelID(info.hairModelID) ?? data2.GetHairModelID(info.hairModelID);
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 0)
		{
			need_high_reso_tex = false;
			use_hair_overlay = -1;
		}
		int high_reso_tex_flags = 0;
		if (need_high_reso_tex && MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
		{
			EquipModelHQTable equipModelHQTable = MonoBehaviourSingleton<GlobalSettingsManager>.I.equipModelHQTable;
			high_reso_tex_flags = equipModelHQTable.GetWeaponFlag(info.weaponModelID);
		}
		bool is_self = false;
		if (player != null)
		{
			_ = player.id;
			is_self = (player is Self);
		}
		DeleteLoadedObjects();
		loadInfo = info;
		if (anim_id < 0)
		{
			anim_id = ((anim_id != -1 || info.weaponModelID == -1) ? (-anim_id + info.weaponModelID / 1000) : (info.weaponModelID / 1000));
		}
		bool flag = data3?.needFace ?? data2.needFace;
		int num2 = data3?.hairMode ?? data2.hairMode;
		string face_name = (info.faceModelID > -1 && flag) ? ResourceName.GetPlayerFace(info.faceModelID) : null;
		string hair_name = (num > -1 && num2 != 0) ? ResourceName.GetPlayerHead(num) : null;
		string body_name = (info.bodyModelID > -1) ? ResourceName.GetPlayerBody(info.bodyModelID) : null;
		string head_name = (info.headModelID > -1 && data3 != null) ? ResourceName.GetPlayerHead(info.headModelID) : null;
		string arm_name = (info.armModelID > -1 && arm_model_data != null) ? ResourceName.GetPlayerArm(info.armModelID) : null;
		string leg_name = (info.legModelID > -1 && leg_model_data != null) ? ResourceName.GetPlayerLeg(info.legModelID) : null;
		string wepn_name = (info.weaponModelID > -1) ? ResourceName.GetPlayerWeapon(info.weaponModelID) : null;
		if (body_name == null)
		{
			yield break;
		}
		Transform _this = base.transform;
		if (player != null)
		{
			if (player.controller != null)
			{
				player.controller.enabled = false;
			}
			if (player.packetReceiver != null)
			{
				player.packetReceiver.SetStopPacketUpdate(is_stop: true);
			}
			player.OnLoadStart();
		}
		if (is_self)
		{
			GoGameCacheManager.PlayerModelData newData = new GoGameCacheManager.PlayerModelData
			{
				faceName = face_name,
				hairName = hair_name,
				bodyName = body_name,
				headName = head_name,
				armName = arm_name,
				legName = leg_name,
				wepnName = wepn_name
			};
			MonoBehaviourSingleton<GoGameCacheManager>.I.ClearCacheSelfPlayerModelNotUse(newData);
		}
		isLoading = true;
		LoadingQueue load_queue = new LoadingQueue(this, need_res_ref_count);
		LoadObject lo_face = null;
		LoadObject lo_hair = null;
		LoadObject lo_body = null;
		LoadObject lo_head = null;
		LoadObject lo_arm = null;
		LoadObject lo_leg = null;
		LoadObject lo_wepn = null;
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_FACE, face_name))
		{
			lo_face = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_FACE, face_name);
		}
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name))
		{
			lo_hair = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_HEAD, hair_name);
		}
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_BDY, body_name))
		{
			lo_body = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_BDY, body_name);
		}
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_HEAD, head_name))
		{
			lo_head = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_HEAD, head_name);
		}
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ARM, arm_name))
		{
			lo_arm = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_ARM, arm_name);
		}
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_LEG, leg_name))
		{
			lo_leg = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_LEG, leg_name);
		}
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name))
		{
			lo_wepn = GoGameQuickLoad(load_queue, need_dev_frame_instantiate, RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name);
		}
		List<LoadObject> lo_accessories = new List<LoadObject>();
		if (!info.accUIDs.IsNullOrEmpty())
		{
			int l = 0;
			for (int count = info.accUIDs.Count; l < count; l++)
			{
				string playerAccessory = ResourceName.GetPlayerAccessory(Singleton<AccessoryTable>.I.GetInfoData(info.accUIDs[l]).accessoryId);
				lo_accessories.Add(need_dev_frame_instantiate ? load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_ACCESSORY, playerAccessory) : load_queue.Load(RESOURCE_CATEGORY.PLAYER_ACCESSORY, playerAccessory));
			}
		}
		LoadObject lo_voices = null;
		LoadObject lo_hr_wep_tex = null;
		LoadObject lo_hr_hed_tex = null;
		LoadObject lo_hr_bdy_tex = null;
		LoadObject lo_hr_arm_tex = null;
		LoadObject lo_hr_leg_tex = null;
		string anim_name = (anim_id > -1) ? ResourceName.GetPlayerAnim(anim_id) : null;
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
		}
		if (is_self)
		{
			if (lo_face != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_FACE, face_name, lo_face.loadedObject);
			}
			if (lo_hair != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name, lo_hair.loadedObject);
			}
			if (lo_body != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_BDY, body_name, lo_body.loadedObject);
			}
			if (lo_head != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_HEAD, head_name, lo_head.loadedObject);
			}
			if (lo_arm != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_ARM, arm_name, lo_arm.loadedObject);
			}
			if (lo_leg != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_LEG, leg_name, lo_leg.loadedObject);
			}
			if (lo_wepn != null)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name, lo_wepn.loadedObject);
			}
		}
		if (is_self)
		{
			faceCacheName = face_name;
			hairCacheName = hair_name;
			bodyCacheName = body_name;
			headCacheName = head_name;
			armCacheName = arm_name;
			legCacheName = leg_name;
			weaponCacheName = wepn_name;
		}
		LoadObject lo_anim = (anim_name != null) ? load_queue.Load(RESOURCE_CATEGORY.PLAYER_ANIM, anim_name, (!need_anim_event) ? new string[1]
		{
			anim_name + "Ctrl"
		} : new string[2]
		{
			anim_name + "Ctrl",
			anim_name + "Event"
		}) : null;
		if (lo_anim != null)
		{
			animObjectTable.Add("BASE", lo_anim);
		}
		if (player != null && anim_id > -1)
		{
			List<string> list = new List<string>();
			int num3 = 3;
			for (int m = 0; m < num3; m++)
			{
				for (int n = 0; n < 2; n++)
				{
					SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + m);
					if (skillParam == null)
					{
						continue;
					}
					string ctrlNameFromAnimFormatName = Character.GetCtrlNameFromAnimFormatName((n == 0) ? skillParam.tableData.castStateName : skillParam.tableData.actStateName);
					if (!string.IsNullOrEmpty(ctrlNameFromAnimFormatName) && list.IndexOf(ctrlNameFromAnimFormatName) < 0)
					{
						list.Add(ctrlNameFromAnimFormatName);
						string playerSubAnim = ResourceName.GetPlayerSubAnim(anim_id, ctrlNameFromAnimFormatName);
						LoadObject loadObject = (playerSubAnim != null) ? load_queue.Load(RESOURCE_CATEGORY.PLAYER_ANIM_SKILL, playerSubAnim, (!need_anim_event) ? new string[1]
						{
							playerSubAnim + "Ctrl"
						} : new string[2]
						{
							playerSubAnim + "Ctrl",
							playerSubAnim + "Event"
						}) : null;
						if (loadObject != null)
						{
							animObjectTable.Add(ctrlNameFromAnimFormatName, loadObject);
						}
					}
				}
			}
		}
		if (player != null && info.weaponEvolveId != 0)
		{
			ResourceName.GetPlayerEvolveAnim(info.weaponEvolveId, out string ctrlName, out string animName);
			LoadObject loadObject2 = load_queue.Load(RESOURCE_CATEGORY.PLAYER_ANIM_EVOLVE, animName, new string[2]
			{
				animName + "Ctrl",
				animName + "Event"
			});
			if (loadObject2 != null)
			{
				animObjectTable.Add(ctrlName, loadObject2);
			}
		}
		if (need_action_voice && info.actionVoiceBaseID > -1)
		{
			int[] array = (int[])Enum.GetValues(typeof(ACTION_VOICE_ID));
			int num4 = array.Length;
			string[] array2 = new string[num4];
			for (int num5 = 0; num5 < num4; num5++)
			{
				array2[num5] = ResourceName.GetActionVoiceName(info.actionVoiceBaseID + array[num5]);
			}
			lo_voices = load_queue.Load(RESOURCE_CATEGORY.SOUND_VOICE, ResourceName.GetActionVoicePackageNameFromVoiceID(info.actionVoiceBaseID), array2);
		}
		if (need_high_reso_tex)
		{
			if (high_reso_tex_flags != 0 && wepn_name != null)
			{
				lo_hr_wep_tex = LoadHighResoTexs(load_queue, wepn_name, high_reso_tex_flags);
			}
			if (head_name != null)
			{
				lo_hr_hed_tex = LoadHighResoTexs(load_queue, head_name, 1);
			}
			if (body_name != null)
			{
				lo_hr_bdy_tex = LoadHighResoTexs(load_queue, body_name, 1);
			}
			if (arm_name != null)
			{
				lo_hr_arm_tex = LoadHighResoTexs(load_queue, arm_name, 1);
			}
			if (leg_name != null)
			{
				lo_hr_leg_tex = LoadHighResoTexs(load_queue, leg_name, 1);
			}
		}
		LoadObject loHairOverlay = null;
		if (use_hair_overlay != -1)
		{
			loHairOverlay = LoadHairOverlayTexs(load_queue, info, use_hair_overlay);
		}
		yield return load_queue.Wait();
		List<string> needAtkInfoNames = new List<string>();
		StringKeyTable<LoadObject> animEventBulletLoadObjTable = new StringKeyTable<LoadObject>();
		if (player != null)
		{
			if (need_anim_event)
			{
				animObjectTable.ForEach(delegate(LoadObject load_object)
				{
					if (!gg_op)
					{
						load_queue.CacheAnimDataUseResource(load_object.loadedObjects[1].obj as AnimEventData, (string effect_name) => (effect_name[0] != '@') ? effect_name : null);
					}
					load_queue.CacheAnimDataUseResourceDependPlayer(player, load_object.loadedObjects[1].obj as AnimEventData);
					AnimEventData animEventData = load_object.loadedObjects[1].obj as AnimEventData;
					if (animEventData != null && !animEventData.animations.IsNullOrEmpty())
					{
						AnimEventData.AnimData[] animations = animEventData.animations;
						for (int num29 = 0; num29 < animations.Length; num29++)
						{
							AnimEventData.EventData[] events = animations[num29].events;
							foreach (AnimEventData.EventData eventData in events)
							{
								AnimEventFormat.ID id = eventData.id;
								switch (id)
								{
								case AnimEventFormat.ID.SHOT_PRESENT:
								{
									int num32 = 0;
									for (int num33 = eventData.stringArgs.Length; num32 < num33; num32++)
									{
										string[] array4 = eventData.stringArgs[num32].Split(':');
										if (!array4.IsNullOrEmpty())
										{
											int num34 = 0;
											for (int num35 = array4.Length; num34 < num35; num34++)
											{
												string text8 = array4[num34];
												if (!string.IsNullOrEmpty(text8))
												{
													LoadObject loadObject7 = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, text8);
													if (loadObject7 != null && animEventBulletLoadObjTable.Get(text8) == null)
													{
														animEventBulletLoadObjTable.Add(text8, loadObject7);
													}
												}
											}
										}
									}
									break;
								}
								case AnimEventFormat.ID.SHOT_ZONE:
								case AnimEventFormat.ID.SHOT_DECOY:
								case AnimEventFormat.ID.LOAD_BULLET:
								{
									for (int num31 = 0; num31 < eventData.stringArgs.Length; num31++)
									{
										string text7 = eventData.stringArgs[num31];
										if (!string.IsNullOrEmpty(text7))
										{
											LoadObject loadObject6 = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, text7);
											if (loadObject6 != null && animEventBulletLoadObjTable.Get(text7) == null)
											{
												animEventBulletLoadObjTable.Add(text7, loadObject6);
											}
										}
									}
									break;
								}
								}
								switch (id)
								{
								case AnimEventFormat.ID.SHOT_ARROW:
								case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE:
								case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_START:
								case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_END:
								case AnimEventFormat.ID.SHOT_GENERIC:
								case AnimEventFormat.ID.SHOT_TARGET:
								case AnimEventFormat.ID.SHOT_POINT:
								case AnimEventFormat.ID.NWAY_LASER_ATTACK:
								case AnimEventFormat.ID.EXATK_COLLIDER_START:
								case AnimEventFormat.ID.GENERATE_TRACKING:
								case AnimEventFormat.ID.PLAYER_FUNNEL_ATTACK:
								case AnimEventFormat.ID.SHOT_NODE_LINK:
								case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE:
								case AnimEventFormat.ID.PAIR_SWORDS_SHOT_BULLET:
								case AnimEventFormat.ID.PAIR_SWORDS_SHOT_LASER:
								case AnimEventFormat.ID.SHOT_HEALING_HOMING:
								case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE_MULTI:
								case AnimEventFormat.ID.SHOT_RESURRECTION_HOMING:
								case AnimEventFormat.ID.BUFF_START_SHIELD_REFLECT:
								case AnimEventFormat.ID.SHOT_ORACLE_SPEAR_SP:
								{
									string text9 = eventData.stringArgs[0];
									if (!string.IsNullOrEmpty(text9))
									{
										string[] namesNeededLoadAtkInfoFromAnimEvent = ResourceName.GetNamesNeededLoadAtkInfoFromAnimEvent();
										for (int num36 = 0; num36 < namesNeededLoadAtkInfoFromAnimEvent.Length; num36++)
										{
											if (text9.StartsWith(namesNeededLoadAtkInfoFromAnimEvent[num36]))
											{
												needAtkInfoNames.Add(text9);
												break;
											}
										}
									}
									break;
								}
								}
							}
						}
					}
				});
			}
			AddSkillAttackInfoName(player, ref needAtkInfoNames);
			AddFieldGimmickAttackInfoName(ref needAtkInfoNames);
			List<LoadObject> atkInfo = new List<LoadObject>();
			List<LoadObject> atkInfoLoaded = new List<LoadObject>();
			Dictionary<string, LoadObject> atkInfoDict = new Dictionary<string, LoadObject>();
			if (playerLoaderLoadedAttackInfoNames == null)
			{
				playerLoaderLoadedAttackInfoNames = new HashSet<string>();
			}
			for (int num6 = 0; num6 < needAtkInfoNames.Count; num6++)
			{
				if (!playerLoaderLoadedAttackInfoNames.Contains(needAtkInfoNames[num6]))
				{
					if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[num6]))
					{
						LoadObject loadObject3 = load_queue.Load(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[num6]);
						atkInfo.Add(loadObject3);
						atkInfoLoaded.Add(loadObject3);
						atkInfoDict.Add(needAtkInfoNames[num6], loadObject3);
					}
					else
					{
						UnityEngine.Object selfPlayerResourceCache = MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[num6]);
						LoadObject loadObject4 = new LoadObject();
						loadObject4.loadedObject = selfPlayerResourceCache;
						atkInfoLoaded.Add(loadObject4);
						atkInfoDict.Add(needAtkInfoNames[num6], loadObject4);
					}
					playerLoaderLoadedAttackInfoNames.Add(needAtkInfoNames[num6]);
				}
			}
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
			if (info.isNeedToCache)
			{
				foreach (KeyValuePair<string, LoadObject> item in atkInfoDict)
				{
					string key2 = item.Key;
					LoadObject value = item.Value;
					if (atkInfo.Contains(value))
					{
						MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, key2, value.loadedObject);
					}
				}
			}
			if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				Transform _settingTransform = MonoBehaviourSingleton<InGameSettingsManager>.I._transform;
				List<AttackInfo> hitInfos = new List<AttackInfo>();
				for (int k = 0; k < atkInfoLoaded.Count; k++)
				{
					GameObject gameObject = LoadObject.RealizesWithGameObject((GameObject)atkInfoLoaded[k].loadedObject, _settingTransform).gameObject;
					SplitPlayerAttackInfo attackInfo = gameObject.GetComponent<SplitPlayerAttackInfo>();
					hitInfos.Add(attackInfo.attackHitInfo);
					hitInfos.Add(attackInfo.attackContinuationInfo);
					if (!string.IsNullOrEmpty(attackInfo.attackHitInfo.nextBulletInfoName))
					{
						yield return StartCoroutine(LoadNextBulletInfo(load_queue, attackInfo.attackHitInfo.nextBulletInfoName, hitInfos, info.isNeedToCache));
					}
					if (!string.IsNullOrEmpty(attackInfo.attackContinuationInfo.nextBulletInfoName))
					{
						yield return StartCoroutine(LoadNextBulletInfo(load_queue, attackInfo.attackContinuationInfo.nextBulletInfoName, hitInfos, info.isNeedToCache));
					}
				}
				InGameSettingsManager.Player player2 = MonoBehaviourSingleton<InGameSettingsManager>.I.player;
				if (player2.attackInfosAll == null)
				{
					player2.attackInfosAll = new AttackInfo[0];
				}
				AttackInfo[] array3 = Utility.CreateMergedArray(player2.attackInfosAll, hitInfos.ToArray());
				player2.attackInfosAll = Utility.DistinctArray(array3);
				player.AddAttackInfos(hitInfos.ToArray());
			}
			LoadAttackInfoResource(player, load_queue);
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
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
						if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < ohsActionInfo.Soul_BoostElementHitEffect.Length)
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsActionInfo.Soul_BoostElementHitEffect[(int)nowWeaponElement]);
						}
					}
					break;
				case SP_ATTACK_TYPE.BURST:
				{
					InGameSettingsManager.Player.BurstOneHandSwordActionInfo burstOHSInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.burstOHSInfo;
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < burstOHSInfo.BoostElementHitEffect.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstOHSInfo.BoostElementHitEffect[(int)nowWeaponElement]);
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
					break;
				}
				case SP_ATTACK_TYPE.ORACLE:
				{
					InGameSettingsManager.Player.OracleOneHandSwordActionInfo oracleOHSInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.oracleOHSInfo;
					for (int num9 = 0; num9 < oracleOHSInfo.dragonEffects.Length; num9++)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleOHSInfo.dragonEffects[num9].GetEffectName(nowWeaponElement));
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_sword_dragon_veil");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_sword_dragon_veil_re");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, $"ef_btl_wsk4_sword_02_{(int)nowWeaponElement:D2}");
					break;
				}
				}
				break;
			case 1:
			{
				if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
				{
					break;
				}
				InGameSettingsManager.Player.TwoHandSwordActionInfo twoHandSwordActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo;
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, twoHandSwordActionInfo.nameChargeExpandEffect);
				if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
				{
					load_queue.CacheSE(twoHandSwordActionInfo.soulBoostSeId);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, twoHandSwordActionInfo.soulIaiChargeMaxEffect);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_btl_soul_energy_01");
				}
				if (player.spAttackType == SP_ATTACK_TYPE.BURST)
				{
					InGameSettingsManager.Player.BurstTwoHandSwordActionInfo burstTHSInfo = twoHandSwordActionInfo.burstTHSInfo;
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < burstTHSInfo.HitEffect_SingleShot.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstTHSInfo.HitEffect_SingleShot[(int)nowWeaponElement]);
					}
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < burstTHSInfo.HitEffect_FullBurst.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstTHSInfo.HitEffect_FullBurst[(int)nowWeaponElement]);
					}
				}
				if (player.spAttackType == SP_ATTACK_TYPE.ORACLE)
				{
					InGameSettingsManager.Player.OracleTwoHandSwordActionInfo oracleTHSInfo = twoHandSwordActionInfo.oracleTHSInfo;
					load_queue.CacheSE(oracleTHSInfo.normalVernierSeId);
					load_queue.CacheSE(oracleTHSInfo.maxVernierSeId);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleTHSInfo.normalVernierEffectName);
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < oracleTHSInfo.maxVernierEffectNames.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleTHSInfo.maxVernierEffectNames[(int)nowWeaponElement]);
					}
				}
				break;
			}
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
						{
							load_queue.CacheSE(burstSpearInfo.spinSeId);
						}
						if (burstSpearInfo.spinMaxSpeedSeId > 0)
						{
							load_queue.CacheSE(burstSpearInfo.spinMaxSpeedSeId);
						}
						string text = string.Empty;
						if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < burstSpearInfo.spinEffectNames.Length)
						{
							text = burstSpearInfo.spinEffectNames[(int)nowWeaponElement];
						}
						string text2 = string.Empty;
						if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < burstSpearInfo.spinElementHitEffectNames.Length)
						{
							text2 = burstSpearInfo.spinElementHitEffectNames[(int)nowWeaponElement];
						}
						string text3 = string.Empty;
						if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < burstSpearInfo.spinThrowGroundEffectNames.Length)
						{
							text3 = burstSpearInfo.spinThrowGroundEffectNames[(int)nowWeaponElement];
						}
						if (!string.IsNullOrEmpty(text))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text);
						}
						if (!string.IsNullOrEmpty(text2))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text2);
						}
						if (!string.IsNullOrEmpty(text3))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text3);
						}
						if (!string.IsNullOrEmpty(burstSpearInfo.throwGroundEffectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstSpearInfo.throwGroundEffectName);
						}
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
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < spearActionInfo.jumpHugeElementHitEffectNames.Length)
					{
						name = spearActionInfo.jumpHugeElementHitEffectNames[(int)nowWeaponElement];
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name);
				}
				break;
			case 4:
				if (player.spAttackType == SP_ATTACK_TYPE.NONE)
				{
					if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
					{
						load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.wildDanceChargeMaxSeId);
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
				}
				else if (player.spAttackType == SP_ATTACK_TYPE.HEAT)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_02");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_03");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_04");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_twinsword_01_05");
				}
				else if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
				{
					if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
					{
						break;
					}
					InGameSettingsManager.Player.PairSwordsActionInfo pairSwordsActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo;
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, pairSwordsActionInfo.Soul_EffectForWaitingLaser);
					string name2 = pairSwordsActionInfo.Soul_EffectForBullet;
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < pairSwordsActionInfo.Soul_EffectsForBullet.Length)
					{
						name2 = pairSwordsActionInfo.Soul_EffectsForBullet[(int)nowWeaponElement];
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name2);
					if (pairSwordsActionInfo.Soul_SeIds.IsNullOrEmpty())
					{
						break;
					}
					for (int num10 = 0; num10 < pairSwordsActionInfo.Soul_SeIds.Length; num10++)
					{
						if (pairSwordsActionInfo.Soul_SeIds[num10] >= 0)
						{
							load_queue.CacheSE(pairSwordsActionInfo.Soul_SeIds[num10]);
						}
					}
				}
				else if (player.spAttackType == SP_ATTACK_TYPE.BURST)
				{
					load_queue.CacheSE(10000051);
					load_queue.CacheSE(10000042);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_twinsword_01_00");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
					InGameSettingsManager.Player.PairSwordsActionInfo pairSwordsActionInfo2 = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo;
					if (nowWeaponElement <= ELEMENT_TYPE.DARK && (int)nowWeaponElement < pairSwordsActionInfo2.Burst_CombineHitEffect.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, pairSwordsActionInfo2.Burst_CombineHitEffect[(int)nowWeaponElement]);
					}
				}
				break;
			case 5:
			{
				if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
				{
					break;
				}
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
				else if (player.spAttackType == SP_ATTACK_TYPE.HEAT)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[22]);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[21]);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_bow_01_02");
				}
				else if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[24]);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_bow_lock_02");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
					load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulLockMaxSeId);
					load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulLockSeId);
					load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.soulBoostSeId);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
				}
				else if (player.spAttackType == SP_ATTACK_TYPE.BURST)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[27]);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarkerSettings.effectNames[26]);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.GetBombArrowEffectName(nowWeaponElement));
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.arrowRainShotAimLesserCursorEffectName);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.GetBombEffectName(nowWeaponElement));
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.boostArrowChargeMaxEffectName);
					load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.burstBoostModeSEId);
					List<int> bombArrowSEIdList = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombArrowSEIdList;
					for (int num7 = 0; num7 < bombArrowSEIdList.Count; num7++)
					{
						load_queue.CacheSE(bombArrowSEIdList[num7]);
					}
					List<int> bombSEIdList = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombSEIdList;
					for (int num8 = 0; num8 < bombSEIdList.Count; num8++)
					{
						load_queue.CacheSE(bombSEIdList[num8]);
					}
				}
				break;
			}
			}
			EvolveController.Load(load_queue, info.weaponEvolveId);
			int skill_len3 = 3;
			LoadObject[] bullet_load = new LoadObject[skill_len3];
			for (int num11 = 0; num11 < skill_len3; num11++)
			{
				SkillInfo.SkillParam skillParam2 = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + num11);
				if (skillParam2 == null)
				{
					bullet_load[num11] = null;
					continue;
				}
				SkillItemTable.SkillItemData tableData = skillParam2.tableData;
				if (!string.IsNullOrEmpty(tableData.startEffectName))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.startEffectName);
				}
				if (tableData.startSEID != 0)
				{
					load_queue.CacheSE(tableData.startSEID);
				}
				if (!string.IsNullOrEmpty(tableData.actLocalEffectName))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.actLocalEffectName);
				}
				if (!string.IsNullOrEmpty(tableData.actOneshotEffectName))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.actOneshotEffectName);
				}
				if (tableData.actSEID != 0)
				{
					load_queue.CacheSE(tableData.actSEID);
				}
				if (!string.IsNullOrEmpty(tableData.enchantEffectName))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.enchantEffectName);
				}
				if (!string.IsNullOrEmpty(tableData.hitEffectName))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.hitEffectName);
				}
				if ((float)tableData.skillRange > 0f && !string.IsNullOrEmpty(MonoBehaviourSingleton<InGameSettingsManager>.I.player.skillRangeEffectName))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.skillRangeEffectName);
				}
				if (tableData.hitSEID != 0)
				{
					load_queue.CacheSE(tableData.hitSEID);
				}
				if (is_self)
				{
					load_queue.CacheItemIcon(tableData.iconID);
				}
				if (tableData.healType == HEAL_TYPE.RESURRECTION_ALL)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_heal_04_03");
				}
				if (!tableData.buffTableIds.IsNullOrEmpty())
				{
					for (int num12 = 0; num12 < tableData.buffTableIds.Length; num12++)
					{
						BuffTable.BuffData data4 = Singleton<BuffTable>.I.GetData((uint)tableData.buffTableIds[num12]);
						if (BuffParam.IsHitAbsorbType(data4.type))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_drain_01_01");
						}
						else if (data4.type == BuffParam.BUFFTYPE.AUTO_REVIVE)
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_heal_04_03");
						}
					}
				}
				string[] supportEffectName = tableData.supportEffectName;
				foreach (string text4 in supportEffectName)
				{
					if (!string.IsNullOrEmpty(text4))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, text4);
					}
				}
				BuffParam.BUFFTYPE[] supportType = tableData.supportType;
				for (int num13 = 0; num13 < supportType.Length; num13++)
				{
					switch (supportType[num13])
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
				bullet_load[num11] = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, tableData.bulletName);
			}
			if (is_self)
			{
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_darkness_02");
			}
			EffectPlayProcessor effectPlayProcessor = player.effectPlayProcessor;
			if (effectPlayProcessor != null && effectPlayProcessor.effectSettings != null)
			{
				int num14 = 0;
				for (int num15 = effectPlayProcessor.effectSettings.Length; num14 < num15; num14++)
				{
					if (string.IsNullOrEmpty(effectPlayProcessor.effectSettings[num14].effectName))
					{
						continue;
					}
					string name3 = effectPlayProcessor.effectSettings[num14].name;
					if (name3.StartsWith("BUFF_"))
					{
						string text5 = name3.Substring(name3.Length - "_PLC00".Length);
						if (text5.Contains("_PLC") && text5 != "_PLC" + (loadInfo.weaponModelID / 1000).ToString("D2"))
						{
							continue;
						}
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor.effectSettings[num14].effectName);
				}
			}
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
			for (int num16 = 0; num16 < skill_len3; num16++)
			{
				SkillInfo.SkillParam skillParam3 = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + num16);
				if (skillParam3 != null)
				{
					skillParam3.bullet = (bullet_load[num16].loadedObject as BulletData);
					load_queue.CacheBulletDataUseResource(skillParam3.bullet, player);
				}
			}
			if (animEventBulletLoadObjTable != null)
			{
				animEventBulletLoadObjTable.ForEachKeyAndValue(delegate(string key, LoadObject item)
				{
					if (item != null && item.loadedObject != null)
					{
						BulletData bulletData = item.loadedObject as BulletData;
						if (bulletData != null && player.cachedBulletDataTable.Get(key) == null)
						{
							player.cachedBulletDataTable.Add(key, bulletData);
							load_queue.CacheBulletDataUseResource(bulletData, player);
						}
					}
				});
			}
			animEventBulletLoadObjTable.Clear();
			animEventBulletLoadObjTable = null;
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
		}
		if (lo_arm != null)
		{
			if (lo_arm.loadedObject != null)
			{
				GameObject gameObject2 = lo_arm.loadedObject as GameObject;
				EffectPlayProcessor effectPlayProcessor2 = (gameObject2 != null) ? gameObject2.GetComponent<EffectPlayProcessor>() : null;
				if (effectPlayProcessor2 != null && effectPlayProcessor2.effectSettings != null)
				{
					int num17 = 0;
					for (int num18 = effectPlayProcessor2.effectSettings.Length; num17 < num18; num17++)
					{
						if (!string.IsNullOrEmpty(effectPlayProcessor2.effectSettings[num17].effectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor2.effectSettings[num17].effectName);
						}
					}
				}
			}
		}
		else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ARM, arm_name))
		{
			GameObject gameObject3 = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ARM, arm_name);
			EffectPlayProcessor effectPlayProcessor3 = (gameObject3 != null) ? gameObject3.GetComponent<EffectPlayProcessor>() : null;
			if (effectPlayProcessor3 != null && effectPlayProcessor3.effectSettings != null)
			{
				int num19 = 0;
				for (int num20 = effectPlayProcessor3.effectSettings.Length; num19 < num20; num19++)
				{
					if (!string.IsNullOrEmpty(effectPlayProcessor3.effectSettings[num19].effectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor3.effectSettings[num19].effectName);
					}
				}
			}
		}
		if (lo_leg != null)
		{
			if (lo_leg.loadedObject != null)
			{
				GameObject gameObject4 = lo_leg.loadedObject as GameObject;
				EffectPlayProcessor effectPlayProcessor4 = (gameObject4 != null) ? gameObject4.GetComponent<EffectPlayProcessor>() : null;
				if (effectPlayProcessor4 != null && effectPlayProcessor4.effectSettings != null)
				{
					int num21 = 0;
					for (int num22 = effectPlayProcessor4.effectSettings.Length; num21 < num22; num21++)
					{
						if (!string.IsNullOrEmpty(effectPlayProcessor4.effectSettings[num21].effectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor4.effectSettings[num21].effectName);
						}
					}
				}
			}
		}
		else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_LEG, leg_name))
		{
			GameObject gameObject5 = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_LEG, leg_name);
			EffectPlayProcessor effectPlayProcessor5 = (gameObject5 != null) ? gameObject5.GetComponent<EffectPlayProcessor>() : null;
			if (effectPlayProcessor5 != null && effectPlayProcessor5.effectSettings != null)
			{
				int num23 = 0;
				for (int num24 = effectPlayProcessor5.effectSettings.Length; num23 < num24; num23++)
				{
					if (!string.IsNullOrEmpty(effectPlayProcessor5.effectSettings[num23].effectName))
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor5.effectSettings[num23].effectName);
					}
				}
			}
		}
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
		}
		bool wait = false;
		bool div_frame_realizes = false;
		int skin_color = info.skinColor;
		if (lo_body != null)
		{
			if (!div_frame_realizes)
			{
				body = lo_body.Realizes(_this);
				if (body == null)
				{
					yield break;
				}
				renderersBody = body.gameObject.GetComponentsInChildren<Renderer>();
				ModelLoaderBase.SetEnabled(renderersBody, is_enable: false);
				SetDynamicBones_Body(body, enableBone);
			}
			else
			{
				wait = true;
				InstantiateManager.Request(this, lo_body.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					body = ((GameObject)data.instantiatedObject).transform;
					body.SetParent(_this, worldPositionStays: false);
					renderersBody = body.GetComponentsInChildren<Renderer>();
					SetDynamicBones_Body(body, enableBone);
					SetRenderersEnabled(renderersBody, is_enabled: false);
					wait = false;
				});
				while (wait)
				{
					yield return null;
				}
			}
		}
		else if (GoGameCacheManager.HasCacheObj(body_name))
		{
			Transform transform = GoGameCacheManager.RetrieveObj(body_name);
			if (transform != null)
			{
				body = transform;
				body.SetParent(_this, worldPositionStays: false);
				body.transform.localPosition = Vector3.zero;
				body.transform.localRotation = Quaternion.identity;
				renderersBody = body.gameObject.GetComponentsInChildren<Renderer>();
				ModelLoaderBase.SetEnabled(renderersBody, is_enable: false);
				SetDynamicBones_Body(body, enableBone);
			}
		}
		else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_BDY, body_name))
		{
			GameObject gameObject6 = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_BDY, body_name);
			if (gameObject6 != null)
			{
				if (!div_frame_realizes)
				{
					body = LoadObject.RealizesWithGameObject(gameObject6, _this);
					renderersBody = body.gameObject.GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersBody, is_enable: false);
					SetDynamicBones_Body(body, enableBone);
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, gameObject6, delegate(InstantiateManager.InstantiateData data)
					{
						body = ((GameObject)data.instantiatedObject).transform;
						body.SetParent(_this, worldPositionStays: false);
						renderersBody = body.GetComponentsInChildren<Renderer>();
						SetDynamicBones_Body(body, enableBone);
						SetRenderersEnabled(renderersBody, is_enabled: false);
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
				}
			}
		}
		if (body == null)
		{
			yield break;
		}
		yield return StartCoroutine(ItemLoader.InitRoopEffect(load_queue, body, shader_type));
		if (renderersBody == null)
		{
			yield break;
		}
		if (renderersBody.Length != 0)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = renderersBody[0] as SkinnedMeshRenderer;
			if (skinnedMeshRenderer != null)
			{
				skinnedMeshRenderer.localBounds = BOUNDS;
			}
		}
		SetSkinAndEquipColor(renderersBody, skin_color, info.bodyColor, 0f);
		ApplyEquipHighResoTexs(lo_hr_bdy_tex, renderersBody);
		if (player != null)
		{
			player.body = body;
		}
		socketRoot = Utility.Find(body, "Root");
		socketHead = Utility.Find(body, "Head");
		socketWepL = Utility.Find(body, "L_Wep");
		socketWepR = Utility.Find(body, "R_Wep");
		socketFootL = Utility.Find(body, "L_Foot");
		socketFootR = Utility.Find(body, "R_Foot");
		socketHandL = Utility.Find(body, "L_Hand");
		socketForearmL = Utility.Find(body, "L_Forearm");
		if (need_foot_stamp)
		{
			if (socketFootL != null && socketFootL.GetComponent<StampNode>() == null)
			{
				StampNode stampNode = socketFootL.gameObject.AddComponent<StampNode>();
				stampNode.offset = new Vector3(-0.08f, 0.01f, 0f);
				stampNode.autoBaseY = 0.1f;
			}
			if (socketFootR != null && socketFootR.GetComponent<StampNode>() == null)
			{
				StampNode stampNode2 = socketFootR.gameObject.AddComponent<StampNode>();
				stampNode2.offset = new Vector3(-0.08f, 0.01f, 0f);
				stampNode2.autoBaseY = 0.1f;
			}
			CharacterStampCtrl characterStampCtrl = body.GetComponent<CharacterStampCtrl>();
			if (characterStampCtrl == null)
			{
				characterStampCtrl = body.gameObject.AddComponent<CharacterStampCtrl>();
			}
			characterStampCtrl.Init(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos, player);
			int num25 = 0;
			for (int num26 = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos.Length; num25 < num26; num25++)
			{
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos[num25].effectName);
			}
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
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
		if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && info.equipType == 5 && info.weaponSpAttackType == 2)
		{
			isSoulArrowOutGameEffect = true;
		}
		StartCoroutine(DoLoadFace(lo_face, face_name, skin_color, enable_eye_blick, div_frame_realizes, delegate(bool kq)
		{
			isLoading_Face = false;
			kqLoading_Face = kq;
		}));
		StartCoroutine(DoLoadHair(lo_hair, loHairOverlay, hair_name, info, enableBone, skin_color, div_frame_realizes, delegate(bool kq)
		{
			isLoading_Hair = false;
			kqLoading_Hair = kq;
		}));
		StartCoroutine(DoLoadHead(load_queue, lo_head, lo_hr_hed_tex, head_name, info, shader_type, div_frame_realizes, delegate(bool kq)
		{
			isLoading_Head = false;
			kqLoading_Head = kq;
		}));
		StartCoroutine(DoLoadArm(lo_arm, lo_hr_arm_tex, arm_name, skin_color, info, arm_model_data, div_frame_realizes, delegate(bool kq)
		{
			isLoading_Arm = false;
			kqLoading_Arm = kq;
		}));
		StartCoroutine(DoLoadFoot(lo_leg, lo_hr_leg_tex, leg_name, skin_color, info, leg_model_data, div_frame_realizes, delegate(bool kq)
		{
			isLoading_Foot = false;
			kqLoading_Foot = kq;
		}));
		StartCoroutine(DoLoadWeapon(load_queue, lo_wepn, lo_hr_wep_tex, wepn_name, isSoulArrowOutGameEffect, high_reso_tex_flags, player, info, shader_type, div_frame_realizes, delegate(bool kq)
		{
			isLoading_Weapn = false;
			kqLoading_Weapn = kq;
		}));
		while (isLoading_Face | isLoading_Hair | isLoading_Head | isLoading_Arm | isLoading_Foot | isLoading_Weapn)
		{
			yield return null;
		}
		if (!kqLoading_Face || !kqLoading_Hair || !kqLoading_Head || !kqLoading_Arm || !kqLoading_Foot || !kqLoading_Weapn)
		{
			yield break;
		}
		animator = body.GetComponentInChildren<Animator>();
		if (animator != null && lo_anim != null)
		{
			RuntimeAnimatorController runtimeAnimatorController = lo_anim.loadedObjects[0].obj as RuntimeAnimatorController;
			if (runtimeAnimatorController != null)
			{
				animator.runtimeAnimatorController = runtimeAnimatorController;
				if (player != null)
				{
					StageObjectProxy stageObjectProxy = animator.gameObject.GetComponent<StageObjectProxy>();
					if (stageObjectProxy == null)
					{
						stageObjectProxy = animator.gameObject.AddComponent<StageObjectProxy>();
					}
					stageObjectProxy.stageObject = player;
					if (need_anim_event)
					{
						player.animEventData = (lo_anim.loadedObjects[1].obj as AnimEventData);
					}
				}
				animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
				if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.isPlaySpAttackTypeMotion)
				{
					SP_ATTACK_TYPE weaponSpAttackType = (SP_ATTACK_TYPE)info.weaponSpAttackType;
					if (weaponSpAttackType != 0)
					{
						string text6 = weaponSpAttackType.ToString();
						int parameterCount = animator.parameterCount;
						for (int num27 = 0; num27 < parameterCount; num27++)
						{
							if (animator.GetParameter(num27).name == text6)
							{
								animator.SetTrigger(text6);
								if (MonoBehaviourSingleton<EffectManager>.IsValid() && isSoulArrowOutGameEffect)
								{
									EffectManager.GetEffect("ef_btl_wsk2_bow_01_01", socketWepR);
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
			voiceAudioClips = lo_voices.loadedObjects;
			UpdateVoiceAudioClipIds();
		}
		if (!lo_accessories.IsNullOrEmpty())
		{
			List<Renderer> accRendererList = new List<Renderer>();
			int skill_len3 = 0;
			int k = lo_accessories.Count;
			while (skill_len3 < k)
			{
				LoadObject loadObject5 = lo_accessories[skill_len3];
				Transform accTrans = null;
				if (!div_frame_realizes)
				{
					accTrans = loadObject5.Realizes();
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, loadObject5.loadedObject, delegate(InstantiateManager.InstantiateData data)
					{
						accTrans = ((GameObject)data.instantiatedObject).transform;
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
				}
				if (accTrans != null)
				{
					AccessoryTable.AccessoryInfoData infoData = Singleton<AccessoryTable>.I.GetInfoData(info.accUIDs[skill_len3]);
					accTrans.SetParent(GetNodeTrans(infoData.node));
					accTrans.localPosition = infoData.offset;
					accTrans.localRotation = infoData.rotation;
					accTrans.localScale = infoData.scale;
					accessory.Add(accTrans);
					accRendererList.AddRange(accTrans.GetComponentsInChildren<Renderer>());
				}
				int num13 = skill_len3 + 1;
				skill_len3 = num13;
			}
			if (!accessory.IsNullOrEmpty())
			{
				k = 0;
				skill_len3 = accessory.Count;
				while (k < skill_len3)
				{
					Transform equipItemRoot = accessory[k];
					yield return StartCoroutine(ItemLoader.InitRoopEffect(load_queue, equipItemRoot, shader_type));
					int num13 = k + 1;
					k = num13;
				}
			}
			renderersAccessory = accRendererList.ToArray();
			ModelLoaderBase.SetEnabled(renderersAccessory, is_enable: false);
		}
		switch (shader_type)
		{
		case SHADER_TYPE.LIGHTWEIGHT:
			ShaderGlobal.ChangeWantLightweightShader(renderersWep);
			ShaderGlobal.ChangeWantLightweightShader(renderersFace);
			ShaderGlobal.ChangeWantLightweightShader(renderersHair);
			ShaderGlobal.ChangeWantLightweightShader(renderersBody);
			ShaderGlobal.ChangeWantLightweightShader(renderersHead);
			ShaderGlobal.ChangeWantLightweightShader(renderersArm);
			ShaderGlobal.ChangeWantLightweightShader(renderersLeg);
			ShaderGlobal.ChangeWantLightweightShader(renderersAccessory);
			break;
		case SHADER_TYPE.UI:
			ShaderGlobal.ChangeWantUIShader(renderersWep);
			ShaderGlobal.ChangeWantUIShader(renderersFace);
			ShaderGlobal.ChangeWantUIShader(renderersHair);
			ShaderGlobal.ChangeWantUIShader(renderersBody);
			ShaderGlobal.ChangeWantUIShader(renderersHead);
			ShaderGlobal.ChangeWantUIShader(renderersArm);
			ShaderGlobal.ChangeWantUIShader(renderersLeg);
			ShaderGlobal.ChangeWantUIShader(renderersAccessory);
			break;
		}
		SetLightProbes(enable_light_probes);
		if (layer != -1)
		{
			SetLayerWithChildren_SecondaryNoChange(_this, layer);
		}
		SetRenderersEnabled(renderersWep, is_enabled: true);
		SetRenderersEnabled(renderersFace, is_enabled: true);
		SetRenderersEnabled(renderersHair, is_enabled: true);
		SetRenderersEnabled(renderersBody, is_enabled: true);
		SetRenderersEnabled(renderersHead, is_enabled: true);
		SetRenderersEnabled(renderersArm, is_enabled: true);
		SetRenderersEnabled(renderersLeg, is_enabled: true);
		SetRenderersEnabled(renderersAccessory, is_enabled: true);
		if (need_shadow && shadow == null)
		{
			shadow = CreateShadow(_this, fixedY0: true, -1, shader_type == SHADER_TYPE.LIGHTWEIGHT);
		}
		if (player != null)
		{
			if (player.controller != null)
			{
				player.controller.enabled = true;
			}
			player.OnLoadComplete();
			if (player.packetReceiver != null)
			{
				player.packetReceiver.SetStopPacketUpdate(is_stop: false);
			}
		}
		if (player != null && is_self && MonoBehaviourSingleton<AudioListenerManager>.IsValid())
		{
			MonoBehaviourSingleton<AudioListenerManager>.I.SetTargetObject(player);
		}
		callback?.Invoke(player);
		ResetDynamicBones(dynamicBones);
		ResetDynamicBones(dynamicBones_Body);
		if (is_self)
		{
			ResourceLoad component = player.gameObject.GetComponent<ResourceLoad>();
			if (component != null && component.list != null)
			{
				List<string> list2 = new List<string>();
				int num28 = 0;
				for (int size = component.list.size; num28 < size; num28++)
				{
					list2.Add(component.list.buffer[num28].name);
				}
				list2.Distinct();
				MonoBehaviourSingleton<ResourceManager>.I.cache.AddIgnoreCategorySpecifiedReleaseList(list2);
			}
		}
		isLoading = false;
		if (is_self)
		{
			MonoBehaviourSingleton<GoGameCacheManager>.I.SaveCacheSelfPlayerModel(player as Self);
		}
		if (gg_op)
		{
			DoLoadLater(need_anim_event);
		}
	}

	protected virtual IEnumerator DoReLoad_GG_Optimize()
	{
		yield return null;
	}

	protected void DoLoadLater(bool need_anim_event)
	{
		if (base.gameObject.GetComponent<Player>() != null)
		{
			if (!MonoBehaviourSingleton<EffectSubLoader>.IsValid())
			{
				EffectSubLoader.CreateInstance();
			}
			if (need_anim_event)
			{
				animObjectTable.ForEach(delegate(LoadObject load_object)
				{
					MonoBehaviourSingleton<EffectSubLoader>.I.CacheAnimDataUseResource(load_object.loadedObjects[1].obj as AnimEventData, (string effect_name) => (effect_name[0] != '@') ? effect_name : null);
				});
				MonoBehaviourSingleton<EffectSubLoader>.I.StartLoad();
			}
		}
	}

	protected IEnumerator DoLoadFace(LoadObject lo_face, string face_name, int skin_color, bool enable_eye_blick, bool div_frame_realizes, Action<bool> callback)
	{
		bool wait = false;
		if (lo_face != null)
		{
			if (!div_frame_realizes)
			{
				face = lo_face.Realizes(socketHead);
				if (face == null)
				{
					callback(obj: false);
					yield break;
				}
				renderersFace = face.gameObject.GetComponentsInChildren<Renderer>();
				ModelLoaderBase.SetEnabled(renderersFace, is_enable: false);
			}
			else
			{
				wait = true;
				InstantiateManager.Request(this, lo_face.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					face = ((GameObject)data.instantiatedObject).transform;
					face.SetParent(socketHead, worldPositionStays: false);
					renderersFace = face.GetComponentsInChildren<Renderer>();
					SetRenderersEnabled(renderersFace, is_enabled: false);
					wait = false;
				});
				while (wait)
				{
					yield return null;
				}
				if (renderersFace == null)
				{
					callback(obj: false);
					yield break;
				}
			}
			SetSkinColor(renderersFace, skin_color);
			validFaceChange = (renderersFace != null && renderersFace.Length != 0 && renderersFace[0].material.HasProperty("_Face_shift"));
			eyeBlink = enable_eye_blick;
		}
		else if (GoGameCacheManager.HasCacheObj(face_name))
		{
			face = GoGameCacheManager.RetrieveObj(face_name, socketHead);
			face.SetParent(socketHead, worldPositionStays: false);
			face.transform.localPosition = Vector3.zero;
			face.transform.localRotation = Quaternion.identity;
			renderersFace = face.gameObject.GetComponentsInChildren<Renderer>();
			ModelLoaderBase.SetEnabled(renderersFace, is_enable: false);
			SetSkinColor(renderersFace, skin_color);
			validFaceChange = (renderersFace != null && renderersFace.Length != 0 && renderersFace[0].material.HasProperty("_Face_shift"));
			eyeBlink = enable_eye_blick;
		}
		else
		{
			GameObject gameObject = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_FACE, face_name);
			if (gameObject != null)
			{
				if (!div_frame_realizes)
				{
					face = LoadObject.RealizesWithGameObject(gameObject, socketHead);
					renderersFace = face.gameObject.GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersFace, is_enable: false);
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, gameObject, delegate(InstantiateManager.InstantiateData data)
					{
						face = ((GameObject)data.instantiatedObject).transform;
						face.SetParent(socketHead, worldPositionStays: false);
						renderersFace = face.GetComponentsInChildren<Renderer>();
						SetRenderersEnabled(renderersFace, is_enabled: false);
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
					if (renderersFace == null)
					{
						callback(obj: false);
						yield break;
					}
				}
				SetSkinColor(renderersFace, skin_color);
				validFaceChange = (renderersFace != null && renderersFace.Length != 0 && renderersFace[0].material.HasProperty("_Face_shift"));
				eyeBlink = enable_eye_blick;
			}
		}
		callback(obj: true);
	}

	protected IEnumerator DoLoadHair(LoadObject lo_hair, LoadObject loHairOverlay, string hair_name, PlayerLoadInfo info, bool enableBone, int skin_color, bool div_frame_realizes, Action<bool> callback)
	{
		bool wait = false;
		if (lo_hair != null)
		{
			if (!div_frame_realizes)
			{
				hair = lo_hair.Realizes(socketHead);
				if (hair == null)
				{
					callback(obj: false);
					yield break;
				}
				renderersHair = hair.GetComponentsInChildren<Renderer>();
				ModelLoaderBase.SetEnabled(renderersHair, is_enable: false);
				SetDynamicBones(body, hair, enableBone);
			}
			else
			{
				wait = true;
				InstantiateManager.Request(this, lo_hair.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					hair = ((GameObject)data.instantiatedObject).transform;
					hair.SetParent(socketHead, worldPositionStays: false);
					renderersHair = hair.GetComponentsInChildren<Renderer>();
					SetDynamicBones(body, hair, enableBone);
					SetRenderersEnabled(renderersHair, is_enabled: false);
					wait = false;
				});
				while (wait)
				{
					yield return null;
				}
				if (renderersHair == null)
				{
					callback(obj: false);
					yield break;
				}
			}
			SetSkinAndEquipColor(renderersHair, skin_color, info.hairColor, 0f);
			if (loHairOverlay != null)
			{
				ApplyHairOverlay(loHairOverlay, renderersHair);
			}
		}
		else if (GoGameCacheManager.HasCacheObj(hair_name))
		{
			hair = GoGameCacheManager.RetrieveObj(hair_name, socketHead);
			hair.SetParent(socketHead, worldPositionStays: false);
			hair.transform.localPosition = Vector3.zero;
			hair.transform.localRotation = Quaternion.identity;
			renderersHair = hair.GetComponentsInChildren<Renderer>();
			ModelLoaderBase.SetEnabled(renderersHair, is_enable: false);
			SetDynamicBones(body, hair, enableBone);
			SetSkinAndEquipColor(renderersHair, skin_color, info.hairColor, 0f);
			if (loHairOverlay != null)
			{
				ApplyHairOverlay(loHairOverlay, renderersHair);
			}
		}
		else
		{
			GameObject gameObject = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name);
			if (gameObject != null)
			{
				if (!div_frame_realizes)
				{
					hair = LoadObject.RealizesWithGameObject(gameObject, socketHead);
					renderersHair = hair.GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersHair, is_enable: false);
					SetDynamicBones(body, hair, enableBone);
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, gameObject, delegate(InstantiateManager.InstantiateData data)
					{
						hair = ((GameObject)data.instantiatedObject).transform;
						hair.SetParent(socketHead, worldPositionStays: false);
						renderersHair = hair.GetComponentsInChildren<Renderer>();
						SetDynamicBones(body, hair, enableBone);
						SetRenderersEnabled(renderersHair, is_enabled: false);
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
					if (renderersHair == null)
					{
						callback(obj: false);
						yield break;
					}
				}
				SetSkinAndEquipColor(renderersHair, skin_color, info.hairColor, 0f);
				if (loHairOverlay != null)
				{
					ApplyHairOverlay(loHairOverlay, renderersHair);
				}
			}
		}
		callback(obj: true);
	}

	protected IEnumerator DoLoadHead(LoadingQueue load_queue, LoadObject lo_head, LoadObject lo_hr_hed_tex, string head_name, PlayerLoadInfo info, SHADER_TYPE shader_type, bool div_frame_realizes, Action<bool> callback)
	{
		bool wait = false;
		if (lo_head != null)
		{
			if (!div_frame_realizes)
			{
				head = lo_head.Realizes(socketHead);
				if (head != null)
				{
					renderersHead = head.GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersHead, is_enable: false);
				}
			}
			else
			{
				wait = true;
				InstantiateManager.Request(this, lo_head.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					head = ((GameObject)data.instantiatedObject).transform;
					head.SetParent(socketHead, worldPositionStays: false);
					renderersHead = head.GetComponentsInChildren<Renderer>();
					SetRenderersEnabled(renderersHead, is_enabled: false);
					wait = false;
				});
				while (wait)
				{
					yield return null;
				}
				if (renderersHead == null)
				{
					callback(obj: false);
					yield break;
				}
			}
			if (head != null)
			{
				yield return StartCoroutine(ItemLoader.InitRoopEffect(load_queue, head, shader_type));
			}
			SetEquipColor(renderersHead, info.headColor);
			ApplyEquipHighResoTexs(lo_hr_hed_tex, renderersHead);
		}
		else if (GoGameCacheManager.HasCacheObj(head_name))
		{
			head = GoGameCacheManager.RetrieveObj(head_name, socketHead);
			head.transform.localPosition = Vector3.zero;
			head.transform.localRotation = Quaternion.identity;
			renderersHead = head.GetComponentsInChildren<Renderer>();
			if (div_frame_realizes)
			{
				ModelLoaderBase.SetEnabled(renderersHead, is_enable: false);
			}
			else
			{
				SetRenderersEnabled(renderersHead, is_enabled: false);
			}
			if (head != null)
			{
				yield return StartCoroutine(ItemLoader.InitRoopEffect(load_queue, head, shader_type));
			}
			SetEquipColor(renderersHead, info.headColor);
			ApplyEquipHighResoTexs(lo_hr_hed_tex, renderersHead);
		}
		else
		{
			GameObject gameObject = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_HEAD, head_name);
			if (gameObject != null)
			{
				if (!div_frame_realizes)
				{
					head = LoadObject.RealizesWithGameObject(gameObject, socketHead);
					if (head != null)
					{
						renderersHead = head.GetComponentsInChildren<Renderer>();
						ModelLoaderBase.SetEnabled(renderersHead, is_enable: false);
					}
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, gameObject, delegate(InstantiateManager.InstantiateData data)
					{
						head = ((GameObject)data.instantiatedObject).transform;
						head.SetParent(socketHead, worldPositionStays: false);
						renderersHead = head.GetComponentsInChildren<Renderer>();
						SetRenderersEnabled(renderersHead, is_enabled: false);
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
					if (renderersHead == null)
					{
						callback(obj: false);
						yield break;
					}
				}
				if (head != null)
				{
					yield return StartCoroutine(ItemLoader.InitRoopEffect(load_queue, head, shader_type));
				}
				SetEquipColor(renderersHead, info.headColor);
				ApplyEquipHighResoTexs(lo_hr_hed_tex, renderersHead);
			}
		}
		callback(obj: true);
	}

	protected IEnumerator DoLoadArm(LoadObject lo_arm, LoadObject lo_hr_arm_tex, string arm_name, int skin_color, PlayerLoadInfo info, EquipModelTable.Data arm_model_data, bool div_frame_realizes, Action<bool> callback)
	{
		bool wait = false;
		if (lo_arm != null)
		{
			if (!div_frame_realizes)
			{
				arm = AddSkin(lo_arm);
				if (arm != null)
				{
					renderersArm = arm.GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersArm, is_enable: false);
				}
			}
			else
			{
				wait = true;
				InstantiateManager.Request(this, lo_arm.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					arm = ((GameObject)data.instantiatedObject).transform;
					arm = AddSkin(arm);
					renderersArm = arm.GetComponentsInChildren<Renderer>();
					SetRenderersEnabled(renderersArm, is_enabled: false);
					wait = false;
				});
				while (wait)
				{
					yield return null;
				}
				if (renderersArm == null)
				{
					callback(obj: false);
					yield break;
				}
			}
			if (arm != null)
			{
				SetSkinAndEquipColor(renderersArm, skin_color, info.armColor, arm_model_data.GetZBias());
				ApplyEquipHighResoTexs(lo_hr_arm_tex, renderersArm);
				InvisibleBodyTriangles(arm_model_data.bodyDraw);
			}
		}
		else if (GoGameCacheManager.HasCacheObj(arm_name))
		{
			arm = GoGameCacheManager.RetrieveObj(arm_name, base.transform);
			if (arm != null)
			{
				arm = AddSkin(arm);
				renderersArm = arm.GetComponentsInChildren<Renderer>();
				SetRenderersEnabled(renderersArm, is_enabled: false);
				SetSkinAndEquipColor(renderersArm, skin_color, info.armColor, arm_model_data.GetZBias());
				ApplyEquipHighResoTexs(lo_hr_arm_tex, renderersArm);
				arm.transform.localPosition = Vector3.zero;
				arm.transform.localRotation = Quaternion.identity;
				InvisibleBodyTriangles(arm_model_data.bodyDraw);
			}
		}
		else
		{
			GameObject gameObject = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ARM, arm_name);
			if ((bool)gameObject)
			{
				if (!div_frame_realizes)
				{
					arm = AddSkinFromCache(gameObject);
					if (arm != null)
					{
						renderersArm = arm.GetComponentsInChildren<Renderer>();
						ModelLoaderBase.SetEnabled(renderersArm, is_enable: false);
					}
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, gameObject, delegate(InstantiateManager.InstantiateData data)
					{
						arm = ((GameObject)data.instantiatedObject).transform;
						arm = AddSkin(arm);
						renderersArm = arm.GetComponentsInChildren<Renderer>();
						SetRenderersEnabled(renderersArm, is_enabled: false);
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
					if (renderersArm == null)
					{
						callback(obj: false);
						yield break;
					}
				}
				if (arm != null)
				{
					SetSkinAndEquipColor(renderersArm, skin_color, info.armColor, arm_model_data.GetZBias());
					ApplyEquipHighResoTexs(lo_hr_arm_tex, renderersArm);
					InvisibleBodyTriangles(arm_model_data.bodyDraw);
				}
			}
		}
		callback(obj: true);
	}

	protected IEnumerator DoLoadFoot(LoadObject lo_leg, LoadObject lo_hr_leg_tex, string leg_name, int skin_color, PlayerLoadInfo info, EquipModelTable.Data leg_model_data, bool div_frame_realizes, Action<bool> callback)
	{
		bool wait = false;
		if (lo_leg != null)
		{
			if (!div_frame_realizes)
			{
				leg = AddSkin(lo_leg);
				if (leg != null)
				{
					renderersLeg = leg.GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersLeg, is_enable: false);
				}
			}
			else
			{
				wait = true;
				InstantiateManager.Request(this, lo_leg.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					leg = ((GameObject)data.instantiatedObject).transform;
					leg = AddSkin(leg);
					renderersLeg = leg.GetComponentsInChildren<Renderer>();
					SetRenderersEnabled(renderersLeg, is_enabled: false);
					wait = false;
				});
				while (wait)
				{
					yield return null;
				}
				if (renderersLeg == null)
				{
					callback(obj: false);
					yield break;
				}
			}
			if (leg != null)
			{
				SetSkinAndEquipColor(renderersLeg, skin_color, info.legColor, leg_model_data.GetZBias());
				ApplyEquipHighResoTexs(lo_hr_leg_tex, renderersLeg);
			}
		}
		else if (GoGameCacheManager.HasCacheObj(leg_name))
		{
			leg = GoGameCacheManager.RetrieveObj(leg_name, base.transform);
			leg = AddSkin(leg);
			if (leg != null)
			{
				leg.transform.localPosition = Vector3.zero;
				leg.transform.localRotation = Quaternion.identity;
				renderersLeg = leg.GetComponentsInChildren<Renderer>();
				SetRenderersEnabled(renderersLeg, is_enabled: false);
				SetSkinAndEquipColor(renderersLeg, skin_color, info.legColor, leg_model_data.GetZBias());
				ApplyEquipHighResoTexs(lo_hr_leg_tex, renderersLeg);
			}
		}
		else
		{
			GameObject gameObject = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_LEG, leg_name);
			if ((bool)gameObject)
			{
				if (!div_frame_realizes)
				{
					leg = AddSkinFromCache(gameObject);
					if (leg != null)
					{
						renderersLeg = leg.GetComponentsInChildren<Renderer>();
						ModelLoaderBase.SetEnabled(renderersLeg, is_enable: false);
					}
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, gameObject, delegate(InstantiateManager.InstantiateData data)
					{
						leg = ((GameObject)data.instantiatedObject).transform;
						leg = AddSkin(leg);
						renderersLeg = leg.GetComponentsInChildren<Renderer>();
						SetRenderersEnabled(renderersLeg, is_enabled: false);
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
					if (renderersLeg == null)
					{
						callback(obj: false);
						yield break;
					}
				}
				if (leg != null)
				{
					SetSkinAndEquipColor(renderersLeg, skin_color, info.legColor, leg_model_data.GetZBias());
					ApplyEquipHighResoTexs(lo_hr_leg_tex, renderersLeg);
				}
			}
		}
		callback(obj: true);
	}

	protected IEnumerator DoLoadWeapon(LoadingQueue load_queue, LoadObject lo_wepn, LoadObject lo_hr_wep_tex, string wepn_name, bool isSoulArrowOutGameEffect, int high_reso_tex_flags, Player player, PlayerLoadInfo info, SHADER_TYPE shader_type, bool div_frame_realizes, Action<bool> callback)
	{
		bool wait = false;
		if (lo_wepn != null)
		{
			Transform weapon2 = null;
			if (!div_frame_realizes)
			{
				weapon2 = lo_wepn.Realizes();
				if (weapon2 != null)
				{
					renderersWep = weapon2.gameObject.GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersWep, is_enable: false);
				}
			}
			else
			{
				wait = true;
				InstantiateManager.Request(this, lo_wepn.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					weapon2 = ((GameObject)data.instantiatedObject).transform;
					renderersWep = weapon2.GetComponentsInChildren<Renderer>();
					SetRenderersEnabled(renderersWep, is_enabled: false);
					wait = false;
				});
				while (wait)
				{
					yield return null;
				}
				if (renderersWep == null)
				{
					callback(obj: false);
					yield break;
				}
			}
			if (weapon2 != null)
			{
				if (isSoulArrowOutGameEffect)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_bow_01_01");
				}
				yield return StartCoroutine(ItemLoader.InitRoopEffect(load_queue, weapon2, shader_type));
			}
			if (renderersBody == null)
			{
				callback(obj: false);
				yield break;
			}
			if (weapon2 != null)
			{
				InitWeaponLinkBuffEffect(player, weapon2);
			}
			SetWeaponShader(renderersWep, info.weaponColor0, info.weaponColor1, info.weaponColor2, info.weaponEffectID, info.weaponEffectParam, info.weaponEffectColor);
			Material mate = null;
			Material mate2 = null;
			if (renderersWep != null)
			{
				int i = 0;
				for (int num = renderersWep.Length; i < num; i++)
				{
					if (renderersWep[i].name.EndsWith("_L"))
					{
						mate2 = renderersWep[i].material;
						wepL = renderersWep[i].transform;
						if (loadInfo.equipType == 0 && loadInfo.weaponSpAttackType == 2)
						{
							Utility.Attach(socketHandL, wepL);
						}
						else
						{
							Utility.Attach(socketWepL, wepL);
						}
					}
					else
					{
						mate = renderersWep[i].material;
						wepR = renderersWep[i].transform;
						Utility.Attach(socketWepR, wepR);
					}
				}
			}
			if (weapon2 != null)
			{
				UnityEngine.Object.DestroyImmediate(weapon2.gameObject);
			}
			if (lo_hr_wep_tex != null)
			{
				ApplyWeaponHighResoTexs(lo_hr_wep_tex, high_reso_tex_flags, mate, mate2);
			}
		}
		else
		{
			GameObject gameObject = (GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name);
			if ((bool)gameObject)
			{
				Transform weapon = null;
				if (!div_frame_realizes)
				{
					weapon = LoadObject.RealizesWithGameObject(gameObject);
					if (weapon != null)
					{
						renderersWep = weapon.gameObject.GetComponentsInChildren<Renderer>();
						ModelLoaderBase.SetEnabled(renderersWep, is_enable: false);
					}
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, gameObject, delegate(InstantiateManager.InstantiateData data)
					{
						weapon = ((GameObject)data.instantiatedObject).transform;
						renderersWep = weapon.GetComponentsInChildren<Renderer>();
						SetRenderersEnabled(renderersWep, is_enabled: false);
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
					if (renderersWep == null)
					{
						callback(obj: false);
						yield break;
					}
				}
				if (weapon != null)
				{
					if (isSoulArrowOutGameEffect)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_bow_01_01");
					}
					yield return StartCoroutine(ItemLoader.InitRoopEffect(load_queue, weapon, shader_type));
				}
				if (renderersBody == null)
				{
					callback(obj: false);
					yield break;
				}
				if (weapon != null)
				{
					InitWeaponLinkBuffEffect(player, weapon);
				}
				SetWeaponShader(renderersWep, info.weaponColor0, info.weaponColor1, info.weaponColor2, info.weaponEffectID, info.weaponEffectParam, info.weaponEffectColor);
				Material mate3 = null;
				Material mate4 = null;
				if (renderersWep != null)
				{
					int j = 0;
					for (int num2 = renderersWep.Length; j < num2; j++)
					{
						if (renderersWep[j].name.EndsWith("_L"))
						{
							mate4 = renderersWep[j].material;
							wepL = renderersWep[j].transform;
							if (loadInfo.equipType == 0 && loadInfo.weaponSpAttackType == 2)
							{
								Utility.Attach(socketHandL, wepL);
							}
							else
							{
								Utility.Attach(socketWepL, wepL);
							}
						}
						else
						{
							mate3 = renderersWep[j].material;
							wepR = renderersWep[j].transform;
							Utility.Attach(socketWepR, wepR);
						}
					}
				}
				if (weapon != null)
				{
					UnityEngine.Object.DestroyImmediate(weapon.gameObject);
				}
				if (lo_hr_wep_tex != null)
				{
					ApplyWeaponHighResoTexs(lo_hr_wep_tex, high_reso_tex_flags, mate3, mate4);
				}
			}
		}
		callback(obj: true);
	}

	private void AddSkillAttackInfoName(Player player, ref List<string> needAtkInfoNames)
	{
		int num = 3;
		for (int i = 0; i < num; i++)
		{
			SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + i);
			if (skillParam == null)
			{
				continue;
			}
			SkillItemTable.SkillItemData tableData = skillParam.tableData;
			string[] attackInfoNames = tableData.attackInfoNames;
			for (int j = 0; j < attackInfoNames.Length; j++)
			{
				if (!string.IsNullOrEmpty(attackInfoNames[j]))
				{
					needAtkInfoNames.Add(attackInfoNames[j]);
				}
			}
			if ((int)tableData.healHp > 0 && (float)tableData.skillRange > 0f)
			{
				needAtkInfoNames.Add("sk_heal_atk");
			}
		}
		if (player.shieldReflectInfo != null && !string.IsNullOrEmpty(player.shieldReflectInfo.attackInfoName))
		{
			needAtkInfoNames.Add(player.shieldReflectInfo.attackInfoName);
		}
		needAtkInfoNames.Add("sk_heal_atk_zone");
	}

	private void AddFieldGimmickAttackInfoName(ref List<string> needAtkInfoNames)
	{
		if (!MonoBehaviourSingleton<FieldManager>.IsValid() || !Singleton<FieldMapTable>.IsValid())
		{
			return;
		}
		List<FieldMapTable.FieldGimmickPointTableData> fieldGimmickPointListByMapID = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
		if (fieldGimmickPointListByMapID == null)
		{
			return;
		}
		for (int i = 0; i < fieldGimmickPointListByMapID.Count; i++)
		{
			switch (fieldGimmickPointListByMapID[i].gimmickType)
			{
			case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.BOMBROCK:
				needAtkInfoNames.Add("bombrock");
				break;
			case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON:
			{
				string[] cannonAttackInfoNames = ResourceName.GetCannonAttackInfoNames();
				for (int j = 0; j < cannonAttackInfoNames.Length; j++)
				{
					needAtkInfoNames.Add(cannonAttackInfoNames[j]);
				}
				break;
			}
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
				needAtkInfoNames.Add(FieldGimmickCannonField.GetAttackInfoName(fieldGimmickPointListByMapID[i].value2));
				break;
			case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_TURRET:
				needAtkInfoNames.AddRange(FieldCarriableTurretGimmickObject.GetAttackInfoNames(fieldGimmickPointListByMapID[i].value2));
				break;
			case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_BOMB:
				needAtkInfoNames.Add(FieldCarriableBombGimmickObject.GetAttackInfoName(fieldGimmickPointListByMapID[i].value2));
				break;
			}
		}
	}

	private IEnumerator LoadNextBulletInfo(LoadingQueue loadQueue, string infoName, List<AttackInfo> hitInfos, bool isNeedToCache)
	{
		if (playerLoaderLoadedAttackInfoNames.Contains(infoName))
		{
			yield break;
		}
		GameObject gameObject;
		if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, infoName))
		{
			LoadObject loadObj = loadQueue.Load(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, infoName);
			playerLoaderLoadedAttackInfoNames.Add(infoName);
			if (loadQueue.IsLoading())
			{
				yield return loadQueue.Wait();
			}
			if (isNeedToCache)
			{
				MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerResource(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, infoName, loadObj.loadedObject);
			}
			gameObject = loadObj.Realizes(MonoBehaviourSingleton<InGameSettingsManager>.I._transform).gameObject;
		}
		else
		{
			gameObject = LoadObject.RealizesWithGameObject((GameObject)MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, infoName), MonoBehaviourSingleton<InGameSettingsManager>.I._transform).gameObject;
		}
		SplitPlayerAttackInfo component = gameObject.GetComponent<SplitPlayerAttackInfo>();
		hitInfos.Add(component.attackHitInfo);
		if (!string.IsNullOrEmpty(component.attackHitInfo.nextBulletInfoName))
		{
			yield return StartCoroutine(LoadNextBulletInfo(loadQueue, component.attackHitInfo.nextBulletInfoName, hitInfos, isNeedToCache));
		}
	}

	private void InitWeaponLinkBuffEffect(Player player, Transform weaponTrans)
	{
		if (player == null)
		{
			return;
		}
		EffectPlayProcessor componentInChildren = weaponTrans.GetComponentInChildren<EffectPlayProcessor>();
		if (componentInChildren == null)
		{
			return;
		}
		EffectPlayProcessor.EffectSetting[] effectSettings = componentInChildren.effectSettings;
		if (effectSettings == null || effectSettings.Length == 0)
		{
			return;
		}
		int num = effectSettings.Length;
		for (int i = 0; i < num; i++)
		{
			if (effectSettings[i].name.StartsWith("BUFF_LOOP_"))
			{
				player.RegisterWeaponLinkEffect(effectSettings[i]);
			}
		}
	}

	public void DeleteLoadedObjects()
	{
		if (wepR != null)
		{
			UnityEngine.Object.DestroyImmediate(wepR.gameObject);
		}
		if (wepL != null)
		{
			UnityEngine.Object.DestroyImmediate(wepL.gameObject);
		}
		if (body != null)
		{
			UnityEngine.Object.DestroyImmediate(body.gameObject);
		}
		if (shadow != null)
		{
			UnityEngine.Object.DestroyImmediate(shadow.gameObject);
		}
		loadInfo = null;
		wepR = null;
		wepL = null;
		body = null;
		face = null;
		hair = null;
		head = null;
		arm = null;
		leg = null;
		accessory.Clear();
		shadow = null;
		animator = null;
		hairPhysics = null;
		renderersWep = null;
		renderersFace = null;
		renderersHair = null;
		renderersBody = null;
		renderersHead = null;
		renderersArm = null;
		renderersLeg = null;
		renderersAccessory = null;
		socketRoot = null;
		socketHead = null;
		socketWepL = null;
		socketWepR = null;
		socketFootL = null;
		socketFootR = null;
		socketHandL = null;
		socketHandR = null;
		socketForearmL = null;
		socketForearmR = null;
		validFaceChange = false;
		eyeBlink = false;
		isLoading = false;
	}

	private void Update()
	{
		if (eyeBlink)
		{
			UpdateEyeBlink();
		}
	}

	private void UpdateEyeBlink()
	{
		if (!eyeBlink)
		{
			return;
		}
		eyeBlinkTime -= Time.deltaTime;
		if (eyeBlinkTime <= 0f)
		{
			if (faceID != FACE_ID.CLOSE_EYE)
			{
				ChangeFace(FACE_ID.CLOSE_EYE);
				eyeBlinkTime = UnityEngine.Random.Range(0.1f, 0.3f);
			}
			else
			{
				ChangeFace(FACE_ID.NORMAL);
				eyeBlinkTime = UnityEngine.Random.Range(3f, 6f);
			}
		}
	}

	private bool _EnableDynamicBone(SHADER_TYPE shaderType, bool isSelf)
	{
		if (shaderType == SHADER_TYPE.LIGHTWEIGHT && !isSelf)
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			switch (MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.dynamicBoneType)
			{
			case DYNAMICBONE_TYPE.DISABLE:
				return false;
			case DYNAMICBONE_TYPE.DISABLE_LOW:
				if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 0)
				{
					return false;
				}
				break;
			}
		}
		return true;
	}

	public void SetDynamicBones_Body(Transform body, bool isEnable)
	{
		if (isEnable)
		{
			return;
		}
		if (this.dynamicBones_Body == null)
		{
			this.dynamicBones_Body = new List<DynamicBone>();
		}
		List<DynamicBone> dynamicBones_Body = this.dynamicBones_Body;
		dynamicBones_Body.Clear();
		body.GetComponentsInChildren(includeInactive: true, dynamicBones_Body);
		if (dynamicBones_Body.Count > 0)
		{
			int i = 0;
			for (int count = dynamicBones_Body.Count; i < count; i++)
			{
				UnityEngine.Object.DestroyImmediate(dynamicBones_Body[i]);
				dynamicBones_Body[i] = null;
			}
			dynamicBones_Body.Clear();
		}
	}

	public void SetDynamicBones(Transform body, Transform hair, bool isEnable)
	{
		if (this.dynamicBones == null)
		{
			this.dynamicBones = new List<DynamicBone>();
		}
		List<DynamicBone> dynamicBones = this.dynamicBones;
		dynamicBones.Clear();
		hair.GetComponentsInChildren(includeInactive: true, dynamicBones);
		if (dynamicBones.Count <= 0)
		{
			return;
		}
		if (isEnable)
		{
			Transform transform = Utility.Find(body, "Neck");
			DynamicBoneCollider dynamicBoneCollider = transform.gameObject.GetComponent<DynamicBoneCollider>();
			if (dynamicBoneCollider == null)
			{
				dynamicBoneCollider = transform.gameObject.AddComponent<DynamicBoneCollider>();
			}
			dynamicBoneCollider.m_Radius = 0.1f;
			dynamicBoneCollider.m_Height = 0.39f;
			dynamicBoneCollider.m_Direction = DynamicBoneCollider.Direction.Z;
			dynamicBoneCollider.m_Center = new Vector3(0.06f, 0.02f, 0f);
			Transform transform2 = Utility.Find(transform, "Head");
			DynamicBoneCollider dynamicBoneCollider2 = transform2.gameObject.GetComponent<DynamicBoneCollider>();
			if (dynamicBoneCollider2 == null)
			{
				dynamicBoneCollider2 = transform2.gameObject.AddComponent<DynamicBoneCollider>();
			}
			dynamicBoneCollider2.m_Radius = 0.1f;
			dynamicBoneCollider2.m_Height = 0.3f;
			dynamicBoneCollider2.m_Direction = DynamicBoneCollider.Direction.Y;
			dynamicBoneCollider2.m_Center = new Vector3(0f, -0.01f, 0f);
			int i = 0;
			for (int count = dynamicBones.Count; i < count; i++)
			{
				dynamicBones[i].m_Colliders.Add(dynamicBoneCollider);
				dynamicBones[i].m_Colliders.Add(dynamicBoneCollider2);
			}
		}
		else
		{
			int j = 0;
			for (int count2 = dynamicBones.Count; j < count2; j++)
			{
				UnityEngine.Object.DestroyImmediate(dynamicBones[j]);
				dynamicBones[j] = null;
			}
		}
	}

	public void ResetDynamicBones(List<DynamicBone> list)
	{
		if (list == null)
		{
			return;
		}
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			DynamicBone dynamicBone = list[i];
			if (dynamicBone != null && dynamicBone.enabled)
			{
				dynamicBone.enabled = false;
				dynamicBone.enabled = true;
			}
		}
	}

	private Transform AddSkin(LoadObject lo)
	{
		return AddSkin(lo, renderersBody[0] as SkinnedMeshRenderer);
	}

	private Transform AddSkin(Transform base_model)
	{
		return AddSkin(base_model, renderersBody[0] as SkinnedMeshRenderer);
	}

	private Transform AddSkinFromCache(GameObject gameObj)
	{
		if (gameObj == null)
		{
			return null;
		}
		return AddSkin(LoadObject.RealizesWithGameObject(gameObj), renderersBody[0] as SkinnedMeshRenderer);
	}

	public static Transform AddSkin(LoadObject lo, SkinnedMeshRenderer body_skin_renderer, int layer = -1)
	{
		if (lo.loadedObject == null)
		{
			return null;
		}
		Transform base_model = lo.Realizes();
		lo.loadedObject = null;
		return AddSkin(base_model, body_skin_renderer, layer);
	}

	public static Transform AddSkin(Transform base_model, SkinnedMeshRenderer body_skin_renderer, int layer = -1)
	{
		SkinnedMeshRenderer componentInChildren = base_model.GetComponentInChildren<SkinnedMeshRenderer>();
		Transform transform = null;
		if (body_skin_renderer != null && componentInChildren != null)
		{
			GameObject gameObject = new GameObject(componentInChildren.name);
			transform = gameObject.transform;
			transform.transform.parent = body_skin_renderer.transform.parent;
			SkinnedMeshRenderer skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
			if (skinnedMeshRenderer == null)
			{
				skinnedMeshRenderer = gameObject.AddComponent<SkinnedMeshRenderer>();
			}
			skinnedMeshRenderer.sharedMesh = componentInChildren.sharedMesh;
			skinnedMeshRenderer.sharedMaterials = componentInChildren.sharedMaterials;
			skinnedMeshRenderer.quality = componentInChildren.quality;
			skinnedMeshRenderer.localBounds = BOUNDS;
			Transform rootBone = body_skin_renderer.rootBone;
			if (componentInChildren.rootBone != null)
			{
				skinnedMeshRenderer.rootBone = Utility.Find(rootBone, componentInChildren.rootBone.name);
				EffectPlayProcessor component = base_model.GetComponent<EffectPlayProcessor>();
				if (component != null)
				{
					EffectPlayProcessor effectPlayProcessor = skinnedMeshRenderer.rootBone.gameObject.GetComponent<EffectPlayProcessor>();
					if (effectPlayProcessor == null)
					{
						effectPlayProcessor = skinnedMeshRenderer.rootBone.gameObject.AddComponent<EffectPlayProcessor>();
					}
					effectPlayProcessor.effectSettings = component.effectSettings;
					List<Transform> list = effectPlayProcessor.PlayEffect("InitRoop");
					if (list != null)
					{
						for (int i = 0; i < list.Count; i++)
						{
							Utility.SetLayerWithChildren(list[i], skinnedMeshRenderer.rootBone.gameObject.layer);
						}
					}
				}
			}
			Transform[] bones = componentInChildren.bones;
			Transform[] array = new Transform[componentInChildren.bones.Length];
			int j = 0;
			for (int num = bones.Length; j < num; j++)
			{
				array[j] = Utility.Find(rootBone, bones[j].name);
			}
			skinnedMeshRenderer.bones = array;
		}
		UnityEngine.Object.DestroyImmediate(base_model.gameObject);
		base_model = null;
		if (layer != -1)
		{
			Utility.SetLayerWithChildren(transform, layer);
		}
		return transform;
	}

	public void InvisibleBodyTriangles(int level)
	{
		InvisibleBodyTriangles(level, renderersBody[0] as SkinnedMeshRenderer);
	}

	public static void InvisibleBodyTriangles(int level, SkinnedMeshRenderer body_skin_renderer)
	{
		if (level == 0)
		{
			return;
		}
		if (body_skin_renderer == null)
		{
			return;
		}
		Color32[] colors = body_skin_renderer.sharedMesh.colors32;
		if (colors.Length == 0)
		{
			return;
		}
		Mesh mesh = ResourceUtility.Instantiate(body_skin_renderer.sharedMesh);
		int[] triangles = mesh.triangles;
		byte b = 0;
		if (level >= 2)
		{
			b = 128;
		}
		b = (byte)(b + 4);
		int i = 0;
		for (int num = triangles.Length; i < num; i += 3)
		{
			if (colors[triangles[i]].g <= b && colors[triangles[i + 1]].g <= b && colors[triangles[i + 2]].g <= b)
			{
				triangles[i + 2] = (triangles[i + 1] = triangles[i]);
			}
		}
		mesh.triangles = triangles;
		body_skin_renderer.sharedMesh = mesh;
	}

	public virtual void ChangeFace(FACE_ID id)
	{
		if (faceID != id)
		{
			faceID = id;
			if (validFaceChange)
			{
				renderersFace[0].material.SetFloat("_Face_shift", (float)id);
			}
		}
	}

	public void SetLightProbes(bool enable_light_probes)
	{
		SetLightProbes(renderersWep, enable_light_probes);
		SetLightProbes(renderersFace, enable_light_probes);
		SetLightProbes(renderersHair, enable_light_probes);
		SetLightProbes(renderersBody, enable_light_probes);
		SetLightProbes(renderersHead, enable_light_probes);
		SetLightProbes(renderersArm, enable_light_probes);
		SetLightProbes(renderersLeg, enable_light_probes);
		SetLightProbes(renderersAccessory, enable_light_probes);
	}

	public static void SetRenderersEnabled(Renderer[] renderers, bool is_enabled)
	{
		if (renderers.IsNullOrEmpty())
		{
			return;
		}
		int i = 0;
		for (int num = renderers.Length; i < num; i++)
		{
			if (renderers[i] != null)
			{
				renderers[i].enabled = is_enabled;
			}
		}
	}

	public static void SetLightProbes(Transform t, bool enable_light_probes)
	{
		SetLightProbes(t.GetComponentsInChildren<Renderer>(), enable_light_probes);
	}

	public static void SetLightProbes(Renderer[] renderers, bool enable_light_probes)
	{
		if (renderers == null)
		{
			return;
		}
		int i = 0;
		for (int num = renderers.Length; i < num; i++)
		{
			if (enable_light_probes)
			{
				renderers[i].lightProbeUsage = LightProbeUsage.UseProxyVolume;
			}
			else
			{
				renderers[i].lightProbeUsage = LightProbeUsage.Off;
			}
		}
	}

	public static void SetLayerWithChildren_SecondaryNoChange(Transform transform, int layer)
	{
		transform.gameObject.layer = layer;
		foreach (Transform item in transform)
		{
			SetLayerWithChildren_SecondaryNoChange(item, layer);
		}
	}

	public static Vector4 ApplySkinColorCoef(Color skin_color)
	{
		if (!MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
		{
			return skin_color;
		}
		float skinColorCoef = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.skinColorCoef;
		Vector4 result = default(Vector4);
		result.x = skin_color.r * skinColorCoef;
		result.y = skin_color.g * skinColorCoef;
		result.z = skin_color.b * skinColorCoef;
		result.w = skin_color.a;
		return result;
	}

	public static void SetSkinColor(Transform t, int color)
	{
		SetSkinColor(t, NGUIMath.IntToColor(color));
	}

	public static void SetSkinColor(Transform t, Color color)
	{
		SetSkinColor(t.GetComponentsInChildren<Renderer>(), color);
	}

	public static void SetSkinColor(Renderer[] renderers, int color)
	{
		SetSkinColor(renderers, NGUIMath.IntToColor(color));
	}

	public static void SetSkinColor(Renderer[] renderers, Color color)
	{
		int ID_CHANGE_SKIN_COLOR = Shader.PropertyToID("_Change_Skin_Color");
		Vector4 _color = ApplySkinColorCoef(color);
		Utility.MaterialForEach(renderers, delegate(Material mtrl)
		{
			mtrl.SetVector(ID_CHANGE_SKIN_COLOR, _color);
		});
	}

	public static void SetEquipColor(Transform t, int color)
	{
		SetEquipColor(t, NGUIMath.IntToColor(color));
	}

	public static void SetEquipColor(Transform t, Color color)
	{
		SetEquipColor(t.GetComponentsInChildren<Renderer>(), color);
	}

	public static void SetEquipColor(Renderer[] renderers, int color)
	{
		SetEquipColor(renderers, NGUIMath.IntToColor(color));
	}

	public static void SetEquipColor(Renderer[] renderers, Color color)
	{
		int ID_CHANGE_EQUIP_COLOR = Shader.PropertyToID("_Change_Equip_Color");
		Utility.MaterialForEach(renderers, delegate(Material mtrl)
		{
			mtrl.SetColor(ID_CHANGE_EQUIP_COLOR, color);
		});
	}

	public static void SetEquipColor3(Renderer[] renderers, int color0, int color1, int color2)
	{
		SetEquipColor3(renderers, NGUIMath.IntToColor(color0), NGUIMath.IntToColor(color1), NGUIMath.IntToColor(color2));
	}

	public static void SetEquipColor3(Renderer[] renderers, Color color0, Color color1, Color color2)
	{
		int ID_CHANGE_EQUIP_COLOR0 = Shader.PropertyToID("_Change_Equip_Color");
		int ID_CHANGE_EQUIP_COLOR = Shader.PropertyToID("_Change_Equip1_Color");
		int ID_CHANGE_EQUIP_COLOR2 = Shader.PropertyToID("_Change_Equip2_Color");
		Utility.MaterialForEach(renderers, delegate(Material mtrl)
		{
			mtrl.SetColor(ID_CHANGE_EQUIP_COLOR0, color0);
			mtrl.SetColor(ID_CHANGE_EQUIP_COLOR, color1);
			mtrl.SetColor(ID_CHANGE_EQUIP_COLOR2, color2);
		});
	}

	public static void SetWeaponShader(Renderer[] renderers, int color0, int color1, int color2, int effectID, float effectParam, int effectColor)
	{
		SetWeaponShader(renderers, NGUIMath.IntToColor(color0), NGUIMath.IntToColor(color1), NGUIMath.IntToColor(color2), effectID, effectParam, NGUIMath.IntToColor(effectColor));
	}

	public static void SetWeaponShader(Renderer[] renderers, Color color0, Color color1, Color color2, int effectID, float effectParam, Color effectColor)
	{
		int ID_CHANGE_EQUIP_COLOR0 = Shader.PropertyToID("_Change_Equip_Color");
		int ID_CHANGE_EQUIP_COLOR = Shader.PropertyToID("_Change_Equip1_Color");
		int ID_CHANGE_EQUIP_COLOR2 = Shader.PropertyToID("_Change_Equip2_Color");
		int ID_CHANGE_ATTRIBUTE_COLOR = Shader.PropertyToID("_Change_Attribute_Color");
		int ID_ATTRIBUTE_PITCH = Shader.PropertyToID("_attribute_Pitch");
		Utility.MaterialForEach(renderers, delegate(Material mtrl)
		{
			Shader shader = ResourceUtility.FindShader($"{mtrl.shader.name}_{effectID}");
			if (shader != null && mtrl.shader != shader)
			{
				mtrl.shader = shader;
			}
			mtrl.SetColor(ID_CHANGE_EQUIP_COLOR0, color0);
			mtrl.SetColor(ID_CHANGE_EQUIP_COLOR, color1);
			mtrl.SetColor(ID_CHANGE_EQUIP_COLOR2, color2);
			if (mtrl.HasProperty(ID_CHANGE_ATTRIBUTE_COLOR))
			{
				mtrl.SetColor(ID_CHANGE_ATTRIBUTE_COLOR, effectColor);
			}
			if (mtrl.HasProperty(ID_ATTRIBUTE_PITCH))
			{
				mtrl.SetFloat(ID_ATTRIBUTE_PITCH, effectParam);
			}
		});
	}

	public static void SetSkinAndEquipColor(Transform t, int skin_color, int equip_color, float z_bias)
	{
		SetSkinAndEquipColor(t, NGUIMath.IntToColor(skin_color), NGUIMath.IntToColor(equip_color), z_bias);
	}

	public static void SetSkinAndEquipColor(Transform t, Color skin_color, Color equip_color, float z_bias)
	{
		SetSkinAndEquipColor(t.GetComponentsInChildren<Renderer>(), skin_color, equip_color, z_bias);
	}

	public static void SetSkinAndEquipColor(Renderer[] renderers, int skin_color, int equip_color, float z_bias)
	{
		SetSkinAndEquipColor(renderers, NGUIMath.IntToColor(skin_color), NGUIMath.IntToColor(equip_color), z_bias);
	}

	public static void SetSkinAndEquipColor(Renderer[] renderers, Color skin_color, Color equip_color, float z_bias)
	{
		int ID_CHANGE_SKIN_COLOR = Shader.PropertyToID("_Change_Skin_Color");
		int ID_CHANGE_EQUIP_COLOR = Shader.PropertyToID("_Change_Equip_Color");
		int ID_ZBIAS = Shader.PropertyToID("_ZBias");
		Vector4 _skin_color = ApplySkinColorCoef(skin_color);
		Utility.MaterialForEach(renderers, delegate(Material mtrl)
		{
			mtrl.SetVector(ID_CHANGE_SKIN_COLOR, _skin_color);
			mtrl.SetColor(ID_CHANGE_EQUIP_COLOR, equip_color);
			mtrl.SetFloat(ID_ZBIAS, z_bias);
		});
	}

	public static Transform CreateShadow(Transform parent = null, bool fixedY0 = true, int layer = -1, bool is_lightweight = false)
	{
		Transform transform = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.CreateShadow(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.shadowSize, MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.height * 0.5f, 1f, fixedY0, parent, is_lightweight);
		if (transform == null)
		{
			return null;
		}
		if (layer != -1)
		{
			transform.gameObject.layer = layer;
		}
		return transform;
	}

	public static string[] GetHighResoTexNames(string name, int flags)
	{
		List<string> list = new List<string>();
		int num = 1;
		while (flags != 0)
		{
			if ((flags & num) != 0)
			{
				switch (num)
				{
				case 1:
					list.Add(name);
					break;
				case 4:
					list.Add(name + "_R");
					break;
				case 16:
					list.Add(name + "_L");
					break;
				}
				flags &= ~num;
			}
			num <<= 1;
		}
		return list.ToArray();
	}

	public static LoadObject LoadHighResoTexs(LoadingQueue load_queue, string name, int flags)
	{
		if (string.IsNullOrEmpty(name) || flags == 0)
		{
			return null;
		}
		string[] highResoTexNames = GetHighResoTexNames(name, flags);
		return load_queue.Load(RESOURCE_CATEGORY.PLAYER_HIGH_RESO_TEX, name, highResoTexNames);
	}

	public static void ApplyWeaponHighResoTexs(LoadObject lo_high_reso_texs, int flags, Material mate0, Material mate1)
	{
		if (lo_high_reso_texs == null || flags == 0 || (mate0 == null && mate1 == null))
		{
			return;
		}
		int num = 1;
		int num2 = 0;
		int num3 = Shader.PropertyToID("_MainTex");
		Shader.PropertyToID("_MaskTex");
		while (flags != 0)
		{
			if ((flags & num) != 0)
			{
				flags &= ~num;
				int nameID = num3;
				bool flag = false;
				bool flag2 = false;
				switch (num)
				{
				case 1:
					flag = true;
					flag2 = true;
					break;
				case 4:
					flag = true;
					break;
				case 16:
					flag2 = true;
					break;
				case 2:
				case 8:
				case 32:
					continue;
				}
				Texture texture = lo_high_reso_texs.loadedObjects[num2++].obj as Texture;
				if (texture != null)
				{
					if (flag && mate0 != null)
					{
						mate0.SetTexture(nameID, texture);
					}
					if (flag2 && mate1 != null)
					{
						mate1.SetTexture(nameID, texture);
					}
				}
			}
			num <<= 1;
		}
	}

	public static void ApplyEquipHighResoTexs(LoadObject lo, Renderer[] renderers)
	{
		if (lo == null || lo.loadedObjects == null || lo.loadedObjects.Length == 0 || renderers == null)
		{
			return;
		}
		Texture texture = lo.loadedObjects[0].obj as Texture;
		if (!(texture == null))
		{
			int num = 0;
			if (num < renderers.Length)
			{
				renderers[num].material.SetTexture(Shader.PropertyToID("_MainTex"), texture);
			}
		}
	}

	public static LoadObject LoadHairOverlayTexs(LoadingQueue load_queue, PlayerLoadInfo info, int hairColorId)
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
		{
			return;
		}
		Shader shader = ResourceUtility.FindShader("mobile/Custom/Character/character_matcap_overlay");
		if (shader == null)
		{
			return;
		}
		Texture texture = lo.loadedObjects[0].obj as Texture;
		if (texture == null)
		{
			return;
		}
		Texture texture2 = lo.loadedObjects[1].obj as Texture;
		if (!(texture2 == null))
		{
			int nameID = Shader.PropertyToID("_MainTex");
			int nameID2 = Shader.PropertyToID("_MatCap");
			int num = 0;
			if (num < renderers.Length)
			{
				renderers[num].material.shader = shader;
				renderers[num].material.SetTexture(nameID, texture);
				renderers[num].material.SetTexture(nameID2, texture2);
			}
		}
	}

	public static bool IsLoading(PlayerLoader[] loaders)
	{
		if (loaders != null)
		{
			int i = 0;
			for (int num = loaders.Length; i < num; i++)
			{
				if (loaders[i] != null && loaders[i].isLoading)
				{
					return true;
				}
			}
		}
		return false;
	}

	public static void DestroyModels(PlayerLoader[] loaders)
	{
		int i = 0;
		for (int num = loaders.Length; i < num; i++)
		{
			if (loaders[i] != null)
			{
				UnityEngine.Object.Destroy(loaders[i].gameObject);
				loaders[i] = null;
			}
		}
	}

	public Transform GetNodeTrans(string node)
	{
		if (body == null)
		{
			return null;
		}
		return Utility.Find(body, node);
	}

	public void CombineBurstPairSword(bool isCombine, Vector3 pos, Quaternion rot)
	{
		if (wepL == null)
		{
			return;
		}
		if (isCombine)
		{
			if (socketWepR == null)
			{
				return;
			}
			wepL.SetParent(socketWepR);
		}
		else
		{
			if (socketWepL == null)
			{
				return;
			}
			wepL.SetParent(socketWepL);
		}
		wepL.localPosition = pos;
		wepL.localRotation = rot;
	}

	public void AddAccessoryModel(Transform modelTrans)
	{
		accessory.Add(modelTrans);
	}

	public Transform GetAccessoryModel(string name)
	{
		if (accessory.IsNullOrEmpty())
		{
			return null;
		}
		for (int i = 0; i < accessory.Count; i++)
		{
			Transform transform = accessory[i];
			if (!(transform == null) && transform.name == name)
			{
				return transform;
			}
		}
		return null;
	}

	public void DeleteAccessoryModel(string name)
	{
		if (accessory.IsNullOrEmpty())
		{
			return;
		}
		for (int i = 0; i < accessory.Count; i++)
		{
			Transform transform = accessory[i];
			if (!(transform == null) && transform.name == name)
			{
				UnityEngine.Object.Destroy(transform.gameObject);
				accessory.RemoveAt(i);
				break;
			}
		}
		List<Renderer> list = new List<Renderer>();
		for (int j = 0; j < accessory.Count; j++)
		{
			if (!(accessory[j] == null))
			{
				Renderer[] componentsInChildren = accessory[j].GetComponentsInChildren<Renderer>();
				if (!componentsInChildren.IsNullOrEmpty())
				{
					list.AddRange(componentsInChildren);
				}
			}
		}
		renderersAccessory = null;
		renderersAccessory = list.ToArray();
	}

	public virtual string GetWinMotionState()
	{
		string result = "win";
		if ((loadInfo.equipType == 5 && loadInfo.weaponSpAttackType == 3) || (loadInfo.equipType == 1 && loadInfo.weaponSpAttackType == 4) || (loadInfo.equipType == 4 && loadInfo.weaponSpAttackType == 4))
		{
			result = "win_02";
		}
		return result;
	}

	public virtual string GetWinLoopMotionState()
	{
		string result = "win_loop";
		if ((loadInfo.equipType == 5 && loadInfo.weaponSpAttackType == 3) || (loadInfo.equipType == 1 && loadInfo.weaponSpAttackType == 4) || (loadInfo.equipType == 4 && loadInfo.weaponSpAttackType == 4))
		{
			result = "win_loop_02";
		}
		return result;
	}
}
