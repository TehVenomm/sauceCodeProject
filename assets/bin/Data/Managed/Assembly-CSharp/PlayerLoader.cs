using App.Scripts.GoGame.Optimization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerLoader : ModelLoaderBase
{
	public enum FACE_ID
	{
		NORMAL,
		CLOSE_EYE
	}

	public delegate void OnCompleteLoad(object player);

	public static readonly Bounds BOUNDS = new Bounds(Vector3.get_zero(), new Vector3(2f, 2f, 2f));

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
					eyeBlinkTime = Random.Range(3f, 6f);
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
			animator.set_enabled(is_enable);
		}
		if (shadow != null)
		{
			shadow.get_gameObject().SetActive(is_enable);
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
		if (voiceAudioClips != null && voiceAudioClips.Length > 0)
		{
			voiceAudioClipIds = new int[voiceAudioClips.Length];
			int i = 0;
			for (int num = voiceAudioClips.Length; i < num; i++)
			{
				int result = 0;
				ResourceObject resourceObject = voiceAudioClips[i];
				if (resourceObject != null && resourceObject.obj != null && resourceObject.obj.get_name() != null)
				{
					string s = resourceObject.obj.get_name().Substring(resourceObject.obj.get_name().Length - 4);
					if (int.TryParse(s, out result))
					{
					}
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
			if (voiceAudioClipIds != null && voiceAudioClipIds.Length > 0)
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
		float num = Random.Range(0f, 1f);
		if (num < rejectRate)
		{
			return 0;
		}
		int num2 = Random.Range(0, items.Length);
		return items[num2];
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
			AttackInfo attackInfo = attackInfos[i];
			if (attackInfo == null)
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
			Log.Error(LOG.RESOURCE, this.get_name() + " now loading.");
		}
		else
		{
			this.StartCoroutine(DoLoad(player_load_info, layer, anim_id, need_anim_event, need_foot_stamp, need_shadow, enable_light_probes, need_action_voice, need_high_reso_tex, need_res_ref_count, need_dev_frame_instantiate, shader_type, callback, enable_eye_blick, use_hair_overlay));
		}
	}

	private static void SerializePlayerLoadInfo(PlayerLoadInfo info)
	{
	}

	private LoadObject GoGameQuickLoad(LoadingQueue loadQueue, bool needDevFrameInstantiate, RESOURCE_CATEGORY resourceCategory, string resourceName)
	{
		if (needDevFrameInstantiate)
		{
			return (resourceName == null) ? null : loadQueue.LoadAndInstantiate(resourceCategory, resourceName);
		}
		return (resourceName == null) ? null : loadQueue.Load(resourceCategory, resourceName);
	}

	protected virtual IEnumerator DoLoad(PlayerLoadInfo info, int layer, int anim_id, bool need_anim_event, bool need_foot_stamp, bool need_shadow, bool enable_light_probes, bool need_action_voice, bool need_high_reso_tex, bool need_res_ref_count, bool need_dev_frame_instantiate, SHADER_TYPE shader_type, OnCompleteLoad callback, bool enable_eye_blick, int use_hair_overlay)
	{
		if (info == null)
		{
			Log.Error(LOG.RESOURCE, "PlayerLoader:info=null");
		}
		SerializePlayerLoadInfo(info);
		animObjectTable = new StringKeyTable<LoadObject>();
		Player player = this.get_gameObject().GetComponent<Player>();
		bool enableBone = _EnableDynamicBone(shader_type, player is Self);
		EquipModelTable.Data body_model_data = Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.ARMOR, info.bodyModelID);
		EquipModelTable.Data helm_model_data = (!body_model_data.needHelm) ? null : Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.HELM, info.headModelID);
		EquipModelTable.Data arm_model_data = (!body_model_data.needArm) ? null : Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.ARM, info.armModelID);
		EquipModelTable.Data leg_model_data = (!body_model_data.needLeg) ? null : Singleton<EquipModelTable>.I.Get(EQUIPMENT_TYPE.LEG, info.legModelID);
		int hair_model_id = helm_model_data?.GetHairModelID(info.hairModelID) ?? body_model_data.GetHairModelID(info.hairModelID);
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
			int id2 = player.id;
			is_self = (player is Self);
		}
		DeleteLoadedObjects();
		loadInfo = info;
		if (anim_id < 0)
		{
			anim_id = ((anim_id != -1 || info.weaponModelID == -1) ? (-anim_id + info.weaponModelID / 1000) : (info.weaponModelID / 1000));
		}
		bool need_face = helm_model_data?.needFace ?? body_model_data.needFace;
		int hair_mode = helm_model_data?.hairMode ?? body_model_data.hairMode;
		string face_name = (info.faceModelID <= -1 || !need_face) ? null : ResourceName.GetPlayerFace(info.faceModelID);
		string hair_name = (hair_model_id <= -1 || hair_mode == 0) ? null : ResourceName.GetPlayerHead(hair_model_id);
		string body_name = (info.bodyModelID <= -1) ? null : ResourceName.GetPlayerBody(info.bodyModelID);
		string head_name = (info.headModelID <= -1 || helm_model_data == null) ? null : ResourceName.GetPlayerHead(info.headModelID);
		string arm_name = (info.armModelID <= -1 || arm_model_data == null) ? null : ResourceName.GetPlayerArm(info.armModelID);
		string leg_name = (info.legModelID <= -1 || leg_model_data == null) ? null : ResourceName.GetPlayerLeg(info.legModelID);
		string wepn_name = (info.weaponModelID <= -1) ? null : ResourceName.GetPlayerWeapon(info.weaponModelID);
		if (body_name == null)
		{
			yield break;
		}
		Transform _this = this.get_transform();
		if (player != null)
		{
			if (player.controller != null)
			{
				player.controller.set_enabled(false);
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
			int k = 0;
			for (int count = info.accUIDs.Count; k < count; k++)
			{
				AccessoryTable.AccessoryInfoData infoData = Singleton<AccessoryTable>.I.GetInfoData(info.accUIDs[k]);
				string playerAccessory = ResourceName.GetPlayerAccessory(infoData.accessoryId);
				lo_accessories.Add((!need_dev_frame_instantiate) ? load_queue.Load(RESOURCE_CATEGORY.PLAYER_ACCESSORY, playerAccessory) : load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_ACCESSORY, playerAccessory));
			}
		}
		LoadObject lo_voices = null;
		LoadObject lo_hr_wep_tex = null;
		LoadObject lo_hr_hed_tex = null;
		LoadObject lo_hr_bdy_tex = null;
		LoadObject lo_hr_arm_tex = null;
		LoadObject lo_hr_leg_tex = null;
		string anim_name = (anim_id <= -1) ? null : ResourceName.GetPlayerAnim(anim_id);
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
		LoadObject lo_anim = (anim_name == null) ? null : load_queue.Load(RESOURCE_CATEGORY.PLAYER_ANIM, anim_name, need_anim_event ? new string[2]
		{
			anim_name + "Ctrl",
			anim_name + "Event"
		} : new string[1]
		{
			anim_name + "Ctrl"
		});
		if (lo_anim != null)
		{
			animObjectTable.Add("BASE", lo_anim);
		}
		if (player != null && anim_id > -1)
		{
			List<string> list = new List<string>();
			int num = 3;
			for (int l = 0; l < num; l++)
			{
				for (int m = 0; m < 2; m++)
				{
					SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + l);
					if (skillParam == null)
					{
						continue;
					}
					string anim_format_name = (m != 0) ? skillParam.tableData.actStateName : skillParam.tableData.castStateName;
					string ctrlNameFromAnimFormatName = Character.GetCtrlNameFromAnimFormatName(anim_format_name);
					if (!string.IsNullOrEmpty(ctrlNameFromAnimFormatName) && list.IndexOf(ctrlNameFromAnimFormatName) < 0)
					{
						list.Add(ctrlNameFromAnimFormatName);
						string playerSubAnim = ResourceName.GetPlayerSubAnim(anim_id, ctrlNameFromAnimFormatName);
						LoadObject loadObject = (playerSubAnim == null) ? null : load_queue.Load(RESOURCE_CATEGORY.PLAYER_ANIM_SKILL, playerSubAnim, need_anim_event ? new string[2]
						{
							playerSubAnim + "Ctrl",
							playerSubAnim + "Event"
						} : new string[1]
						{
							playerSubAnim + "Ctrl"
						});
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
			if (!object.ReferenceEquals(loadObject2, null))
			{
				animObjectTable.Add(ctrlName, loadObject2);
			}
		}
		if (need_action_voice && info.actionVoiceBaseID > -1)
		{
			int[] array = (int[])Enum.GetValues(typeof(ACTION_VOICE_ID));
			int num2 = array.Length;
			string[] array2 = new string[num2];
			for (int n = 0; n < num2; n++)
			{
				array2[n] = ResourceName.GetActionVoiceName(info.actionVoiceBaseID + array[n]);
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
						for (int num31 = 0; num31 < animations.Length; num31++)
						{
							AnimEventData.EventData[] events = animations[num31].events;
							foreach (AnimEventData.EventData eventData in events)
							{
								AnimEventFormat.ID id = eventData.id;
								switch (id)
								{
								case AnimEventFormat.ID.SHOT_PRESENT:
								{
									int num34 = 0;
									for (int num35 = eventData.stringArgs.Length; num34 < num35; num34++)
									{
										string[] array3 = eventData.stringArgs[num34].Split(':');
										if (!array3.IsNullOrEmpty())
										{
											int num36 = 0;
											for (int num37 = array3.Length; num36 < num37; num36++)
											{
												string text8 = array3[num36];
												if (!string.IsNullOrEmpty(text8))
												{
													LoadObject loadObject6 = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, text8);
													if (loadObject6 != null && animEventBulletLoadObjTable.Get(text8) == null)
													{
														animEventBulletLoadObjTable.Add(text8, loadObject6);
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
									for (int num33 = 0; num33 < eventData.stringArgs.Length; num33++)
									{
										string text7 = eventData.stringArgs[num33];
										if (!string.IsNullOrEmpty(text7))
										{
											LoadObject loadObject5 = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, text7);
											if (loadObject5 != null && animEventBulletLoadObjTable.Get(text7) == null)
											{
												animEventBulletLoadObjTable.Add(text7, loadObject5);
											}
										}
									}
									break;
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
										for (int num38 = 0; num38 < namesNeededLoadAtkInfoFromAnimEvent.Length; num38++)
										{
											if (text9.StartsWith(namesNeededLoadAtkInfoFromAnimEvent[num38]))
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
			for (int num3 = 0; num3 < needAtkInfoNames.Count; num3++)
			{
				if (!playerLoaderLoadedAttackInfoNames.Contains(needAtkInfoNames[num3]))
				{
					if (!MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[num3]))
					{
						LoadObject loadObject3 = load_queue.Load(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[num3]);
						atkInfo.Add(loadObject3);
						atkInfoLoaded.Add(loadObject3);
						atkInfoDict.Add(needAtkInfoNames[num3], loadObject3);
					}
					else
					{
						Object selfPlayerResourceCache = MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[num3]);
						LoadObject loadObject4 = new LoadObject();
						loadObject4.loadedObject = selfPlayerResourceCache;
						atkInfoLoaded.Add(loadObject4);
						atkInfoDict.Add(needAtkInfoNames[num3], loadObject4);
					}
					playerLoaderLoadedAttackInfoNames.Add(needAtkInfoNames[num3]);
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
				for (int j = 0; j < atkInfoLoaded.Count; j++)
				{
					GameObject infoObj = LoadObject.RealizesWithGameObject(atkInfoLoaded[j].loadedObject, _settingTransform).get_gameObject();
					SplitPlayerAttackInfo attackInfo = infoObj.GetComponent<SplitPlayerAttackInfo>();
					hitInfos.Add(attackInfo.attackHitInfo);
					hitInfos.Add(attackInfo.attackContinuationInfo);
					if (!string.IsNullOrEmpty(attackInfo.attackHitInfo.nextBulletInfoName))
					{
						yield return this.StartCoroutine(LoadNextBulletInfo(load_queue, attackInfo.attackHitInfo.nextBulletInfoName, hitInfos, info.isNeedToCache));
					}
					if (!string.IsNullOrEmpty(attackInfo.attackContinuationInfo.nextBulletInfoName))
					{
						yield return this.StartCoroutine(LoadNextBulletInfo(load_queue, attackInfo.attackContinuationInfo.nextBulletInfoName, hitInfos, info.isNeedToCache));
					}
				}
				InGameSettingsManager.Player playerSetting = MonoBehaviourSingleton<InGameSettingsManager>.I.player;
				if (playerSetting.attackInfosAll == null)
				{
					playerSetting.attackInfosAll = new AttackInfo[0];
				}
				AttackInfo[] allInfos = Utility.CreateMergedArray(playerSetting.attackInfosAll, hitInfos.ToArray());
				playerSetting.attackInfosAll = Utility.DistinctArray(allInfos);
				player.AddAttackInfos(hitInfos.ToArray());
			}
			LoadAttackInfoResource(player, load_queue);
			ELEMENT_TYPE weaponElement = player.GetNowWeaponElement();
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
						if (weaponElement <= ELEMENT_TYPE.DARK && (int)weaponElement < ohsActionInfo.Soul_BoostElementHitEffect.Length)
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsActionInfo.Soul_BoostElementHitEffect[(int)weaponElement]);
						}
					}
					break;
				case SP_ATTACK_TYPE.BURST:
				{
					InGameSettingsManager.Player.BurstOneHandSwordActionInfo burstOHSInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.burstOHSInfo;
					if (weaponElement <= ELEMENT_TYPE.DARK && (int)weaponElement < burstOHSInfo.BoostElementHitEffect.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstOHSInfo.BoostElementHitEffect[(int)weaponElement]);
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
					break;
				}
				case SP_ATTACK_TYPE.ORACLE:
				{
					InGameSettingsManager.Player.OracleOneHandSwordActionInfo oracleOHSInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.oracleOHSInfo;
					for (int num6 = 0; num6 < oracleOHSInfo.dragonEffects.Length; num6++)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleOHSInfo.dragonEffects[num6].GetEffectName(weaponElement));
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_sword_dragon_veil");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_sword_dragon_veil_re");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, $"ef_btl_wsk4_sword_02_{(int)weaponElement:D2}");
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
					if (weaponElement <= ELEMENT_TYPE.DARK && (int)weaponElement < burstTHSInfo.HitEffect_SingleShot.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstTHSInfo.HitEffect_SingleShot[(int)weaponElement]);
					}
					if (weaponElement <= ELEMENT_TYPE.DARK && (int)weaponElement < burstTHSInfo.HitEffect_FullBurst.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstTHSInfo.HitEffect_FullBurst[(int)weaponElement]);
					}
				}
				if (player.spAttackType == SP_ATTACK_TYPE.ORACLE)
				{
					InGameSettingsManager.Player.OracleTwoHandSwordActionInfo oracleTHSInfo = twoHandSwordActionInfo.oracleTHSInfo;
					load_queue.CacheSE(oracleTHSInfo.normalVernierSeId);
					load_queue.CacheSE(oracleTHSInfo.maxVernierSeId);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleTHSInfo.normalVernierEffectName);
					if (weaponElement <= ELEMENT_TYPE.DARK && (int)weaponElement < oracleTHSInfo.maxVernierEffectNames.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, oracleTHSInfo.maxVernierEffectNames[(int)weaponElement]);
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
						if (weaponElement <= ELEMENT_TYPE.DARK && (int)weaponElement < burstSpearInfo.spinEffectNames.Length)
						{
							text = burstSpearInfo.spinEffectNames[(int)weaponElement];
						}
						string text2 = string.Empty;
						if (weaponElement <= ELEMENT_TYPE.DARK && (int)weaponElement < burstSpearInfo.spinElementHitEffectNames.Length)
						{
							text2 = burstSpearInfo.spinElementHitEffectNames[(int)weaponElement];
						}
						string text3 = string.Empty;
						if (weaponElement <= ELEMENT_TYPE.DARK && (int)weaponElement < burstSpearInfo.spinThrowGroundEffectNames.Length)
						{
							text3 = burstSpearInfo.spinThrowGroundEffectNames[(int)weaponElement];
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
					if (weaponElement <= ELEMENT_TYPE.DARK && (int)weaponElement < spearActionInfo.jumpHugeElementHitEffectNames.Length)
					{
						name = spearActionInfo.jumpHugeElementHitEffectNames[(int)weaponElement];
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
					if (weaponElement <= ELEMENT_TYPE.DARK && (int)weaponElement < pairSwordsActionInfo.Soul_EffectsForBullet.Length)
					{
						name2 = pairSwordsActionInfo.Soul_EffectsForBullet[(int)weaponElement];
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name2);
					if (pairSwordsActionInfo.Soul_SeIds.IsNullOrEmpty())
					{
						break;
					}
					for (int num7 = 0; num7 < pairSwordsActionInfo.Soul_SeIds.Length; num7++)
					{
						if (pairSwordsActionInfo.Soul_SeIds[num7] >= 0)
						{
							load_queue.CacheSE(pairSwordsActionInfo.Soul_SeIds[num7]);
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
					if (weaponElement <= ELEMENT_TYPE.DARK && (int)weaponElement < pairSwordsActionInfo2.Burst_CombineHitEffect.Length)
					{
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, pairSwordsActionInfo2.Burst_CombineHitEffect[(int)weaponElement]);
					}
				}
				else if (player.spAttackType == SP_ATTACK_TYPE.ORACLE)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_twinsword_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, $"ef_btl_wsk4_twinsword_01_{(int)weaponElement:D2}");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_twinsword_03");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk4_twinsword_04");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, $"ef_btl_wsk4_twinsword_05_{(int)weaponElement:D2}");
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
					switch (weaponElement)
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
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.GetBombArrowEffectName(weaponElement));
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.arrowRainShotAimLesserCursorEffectName);
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.GetBombEffectName(weaponElement));
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.boostArrowChargeMaxEffectName);
					load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.burstBoostModeSEId);
					List<int> bombArrowSEIdList = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombArrowSEIdList;
					for (int num4 = 0; num4 < bombArrowSEIdList.Count; num4++)
					{
						load_queue.CacheSE(bombArrowSEIdList[num4]);
					}
					List<int> bombSEIdList = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombSEIdList;
					for (int num5 = 0; num5 < bombSEIdList.Count; num5++)
					{
						load_queue.CacheSE(bombSEIdList[num5]);
					}
				}
				break;
			}
			}
			EvolveController.Load(load_queue, info.weaponEvolveId);
			int skill_len = 3;
			LoadObject[] bullet_load = new LoadObject[skill_len];
			for (int num8 = 0; num8 < skill_len; num8++)
			{
				SkillInfo.SkillParam skillParam2 = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + num8);
				if (skillParam2 == null)
				{
					bullet_load[num8] = null;
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
					for (int num9 = 0; num9 < tableData.buffTableIds.Length; num9++)
					{
						BuffTable.BuffData data2 = Singleton<BuffTable>.I.GetData((uint)tableData.buffTableIds[num9]);
						if (BuffParam.IsHitAbsorbType(data2.type))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_drain_01_01");
						}
						else if (data2.type == BuffParam.BUFFTYPE.AUTO_REVIVE)
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
				for (int num11 = 0; num11 < supportType.Length; num11++)
				{
					switch (supportType[num11])
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
				bullet_load[num8] = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, tableData.bulletName);
			}
			if (is_self)
			{
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_darkness_02");
			}
			EffectPlayProcessor processor = player.effectPlayProcessor;
			if (processor != null && processor.effectSettings != null)
			{
				int num12 = 0;
				for (int num13 = processor.effectSettings.Length; num12 < num13; num12++)
				{
					if (string.IsNullOrEmpty(processor.effectSettings[num12].effectName))
					{
						continue;
					}
					string name3 = processor.effectSettings[num12].name;
					if (name3.StartsWith("BUFF_"))
					{
						string text5 = name3.Substring(name3.Length - "_PLC00".Length);
						if (text5.Contains("_PLC") && text5 != "_PLC" + (loadInfo.weaponModelID / 1000).ToString("D2"))
						{
							continue;
						}
					}
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, processor.effectSettings[num12].effectName);
				}
			}
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
			for (int num14 = 0; num14 < skill_len; num14++)
			{
				SkillInfo.SkillParam skillParam3 = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + num14);
				if (skillParam3 != null)
				{
					skillParam3.bullet = (bullet_load[num14].loadedObject as BulletData);
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
				GameObject val = lo_arm.loadedObject as GameObject;
				EffectPlayProcessor effectPlayProcessor = (!(val != null)) ? null : val.GetComponent<EffectPlayProcessor>();
				if (effectPlayProcessor != null && effectPlayProcessor.effectSettings != null)
				{
					int num15 = 0;
					for (int num16 = effectPlayProcessor.effectSettings.Length; num15 < num16; num15++)
					{
						if (!string.IsNullOrEmpty(effectPlayProcessor.effectSettings[num15].effectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectPlayProcessor.effectSettings[num15].effectName);
						}
					}
				}
			}
		}
		else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_ARM, arm_name))
		{
			GameObject val2 = MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ARM, arm_name);
			EffectPlayProcessor effectPlayProcessor2 = (!(val2 != null)) ? null : val2.GetComponent<EffectPlayProcessor>();
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
		if (lo_leg != null)
		{
			if (lo_leg.loadedObject != null)
			{
				GameObject val3 = lo_leg.loadedObject as GameObject;
				EffectPlayProcessor effectPlayProcessor3 = (!(val3 != null)) ? null : val3.GetComponent<EffectPlayProcessor>();
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
		}
		else if (MonoBehaviourSingleton<GoGameCacheManager>.I.IsSelfPlayerResourceCached(RESOURCE_CATEGORY.PLAYER_LEG, leg_name))
		{
			GameObject val4 = MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_LEG, leg_name);
			EffectPlayProcessor effectPlayProcessor4 = (!(val4 != null)) ? null : val4.GetComponent<EffectPlayProcessor>();
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
				renderersBody = body.get_gameObject().GetComponentsInChildren<Renderer>();
				ModelLoaderBase.SetEnabled(renderersBody, is_enable: false);
				SetDynamicBones_Body(body, enableBone);
			}
			else
			{
				wait = true;
				InstantiateManager.Request(this, lo_body.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					//IL_0011: Unknown result type (might be due to invalid IL or missing references)
					body = data.instantiatedObject.get_transform();
					body.SetParent(_this, false);
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
			GameObject bodyObject = MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_BDY, body_name);
			if (bodyObject != null)
			{
				if (!div_frame_realizes)
				{
					body = LoadObject.RealizesWithGameObject(bodyObject, _this);
					renderersBody = body.get_gameObject().GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersBody, is_enable: false);
					SetDynamicBones_Body(body, enableBone);
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, bodyObject, delegate(InstantiateManager.InstantiateData data)
					{
						//IL_0011: Unknown result type (might be due to invalid IL or missing references)
						body = data.instantiatedObject.get_transform();
						body.SetParent(_this, false);
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
		yield return this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, body, shader_type));
		if (renderersBody == null)
		{
			yield break;
		}
		if (renderersBody.Length > 0)
		{
			SkinnedMeshRenderer val5 = renderersBody[0] as SkinnedMeshRenderer;
			if (val5 != null)
			{
				val5.set_localBounds(BOUNDS);
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
				StampNode stampNode = socketFootL.get_gameObject().AddComponent<StampNode>();
				stampNode.offset = new Vector3(-0.08f, 0.01f, 0f);
				stampNode.autoBaseY = 0.1f;
			}
			if (socketFootR != null && socketFootR.GetComponent<StampNode>() == null)
			{
				StampNode stampNode2 = socketFootR.get_gameObject().AddComponent<StampNode>();
				stampNode2.offset = new Vector3(-0.08f, 0.01f, 0f);
				stampNode2.autoBaseY = 0.1f;
			}
			CharacterStampCtrl step_ctrl = body.GetComponent<CharacterStampCtrl>();
			if (step_ctrl == null)
			{
				step_ctrl = body.get_gameObject().AddComponent<CharacterStampCtrl>();
			}
			step_ctrl.Init(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos, player);
			int num23 = 0;
			for (int num24 = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos.Length; num23 < num24; num23++)
			{
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos[num23].effectName);
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
				renderersFace = face.get_gameObject().GetComponentsInChildren<Renderer>();
				ModelLoaderBase.SetEnabled(renderersFace, is_enable: false);
			}
			else
			{
				wait = true;
				InstantiateManager.Request(this, lo_face.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					//IL_0011: Unknown result type (might be due to invalid IL or missing references)
					face = data.instantiatedObject.get_transform();
					face.SetParent(socketHead, false);
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
			validFaceChange = (renderersFace != null && renderersFace.Length > 0 && renderersFace[0].get_material().HasProperty("_Face_shift"));
			eyeBlink = enable_eye_blick;
		}
		else
		{
			GameObject faceObject = MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_FACE, face_name);
			if (faceObject != null)
			{
				if (!div_frame_realizes)
				{
					face = LoadObject.RealizesWithGameObject(faceObject, socketHead);
					renderersFace = face.get_gameObject().GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersFace, is_enable: false);
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, faceObject, delegate(InstantiateManager.InstantiateData data)
					{
						//IL_0011: Unknown result type (might be due to invalid IL or missing references)
						face = data.instantiatedObject.get_transform();
						face.SetParent(socketHead, false);
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
				validFaceChange = (renderersFace != null && renderersFace.Length > 0 && renderersFace[0].get_material().HasProperty("_Face_shift"));
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
					//IL_0011: Unknown result type (might be due to invalid IL or missing references)
					hair = data.instantiatedObject.get_transform();
					hair.SetParent(socketHead, false);
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
			GameObject hairObject = MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name);
			if (hairObject != null)
			{
				if (!div_frame_realizes)
				{
					hair = LoadObject.RealizesWithGameObject(hairObject, socketHead);
					renderersHair = hair.GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersHair, is_enable: false);
					SetDynamicBones(body, hair, enableBone);
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, hairObject, delegate(InstantiateManager.InstantiateData data)
					{
						//IL_0011: Unknown result type (might be due to invalid IL or missing references)
						hair = data.instantiatedObject.get_transform();
						hair.SetParent(socketHead, false);
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
					//IL_0011: Unknown result type (might be due to invalid IL or missing references)
					head = data.instantiatedObject.get_transform();
					head.SetParent(socketHead, false);
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
				yield return this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, head, shader_type));
			}
			SetEquipColor(renderersHead, info.headColor);
			ApplyEquipHighResoTexs(lo_hr_hed_tex, renderersHead);
		}
		else
		{
			GameObject headObject = MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_HEAD, head_name);
			if (headObject != null)
			{
				if (!div_frame_realizes)
				{
					head = LoadObject.RealizesWithGameObject(headObject, socketHead);
					if (head != null)
					{
						renderersHead = head.GetComponentsInChildren<Renderer>();
						ModelLoaderBase.SetEnabled(renderersHead, is_enable: false);
					}
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, headObject, delegate(InstantiateManager.InstantiateData data)
					{
						//IL_0011: Unknown result type (might be due to invalid IL or missing references)
						head = data.instantiatedObject.get_transform();
						head.SetParent(socketHead, false);
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
					yield return this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, head, shader_type));
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
					//IL_0011: Unknown result type (might be due to invalid IL or missing references)
					arm = data.instantiatedObject.get_transform();
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
			GameObject armObject = MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ARM, arm_name);
			if (Object.op_Implicit(armObject))
			{
				if (!div_frame_realizes)
				{
					arm = AddSkinFromCache(armObject);
					if (arm != null)
					{
						renderersArm = arm.GetComponentsInChildren<Renderer>();
						ModelLoaderBase.SetEnabled(renderersArm, is_enable: false);
					}
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, armObject, delegate(InstantiateManager.InstantiateData data)
					{
						//IL_0011: Unknown result type (might be due to invalid IL or missing references)
						arm = data.instantiatedObject.get_transform();
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
					//IL_0011: Unknown result type (might be due to invalid IL or missing references)
					leg = data.instantiatedObject.get_transform();
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
			GameObject legObject = MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_LEG, leg_name);
			if (Object.op_Implicit(legObject))
			{
				if (!div_frame_realizes)
				{
					leg = AddSkinFromCache(legObject);
					if (leg != null)
					{
						renderersLeg = leg.GetComponentsInChildren<Renderer>();
						ModelLoaderBase.SetEnabled(renderersLeg, is_enable: false);
					}
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, legObject, delegate(InstantiateManager.InstantiateData data)
					{
						//IL_0011: Unknown result type (might be due to invalid IL or missing references)
						leg = data.instantiatedObject.get_transform();
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
			Transform weapon = null;
			if (!div_frame_realizes)
			{
				weapon = lo_wepn.Realizes();
				if (weapon != null)
				{
					renderersWep = weapon.get_gameObject().GetComponentsInChildren<Renderer>();
					ModelLoaderBase.SetEnabled(renderersWep, is_enable: false);
				}
			}
			else
			{
				wait = true;
				InstantiateManager.Request(this, lo_wepn.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					//IL_0007: Unknown result type (might be due to invalid IL or missing references)
					weapon = data.instantiatedObject.get_transform();
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
				yield return this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, weapon, shader_type));
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
			Material materialR2 = null;
			Material materialL2 = null;
			if (renderersWep != null)
			{
				int num25 = 0;
				for (int num26 = renderersWep.Length; num25 < num26; num25++)
				{
					if (renderersWep[num25].get_name().EndsWith("_L"))
					{
						materialL2 = renderersWep[num25].get_material();
						wepL = renderersWep[num25].get_transform();
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
						materialR2 = renderersWep[num25].get_material();
						wepR = renderersWep[num25].get_transform();
						Utility.Attach(socketWepR, wepR);
					}
				}
			}
			if (weapon != null)
			{
				Object.DestroyImmediate(weapon.get_gameObject());
			}
			if (lo_hr_wep_tex != null)
			{
				ApplyWeaponHighResoTexs(lo_hr_wep_tex, high_reso_tex_flags, materialR2, materialL2);
			}
		}
		else
		{
			GameObject weaponObject = MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name);
			if (Object.op_Implicit(weaponObject))
			{
				Transform weapon2 = null;
				if (!div_frame_realizes)
				{
					weapon2 = LoadObject.RealizesWithGameObject(weaponObject);
					if (weapon2 != null)
					{
						renderersWep = weapon2.get_gameObject().GetComponentsInChildren<Renderer>();
						ModelLoaderBase.SetEnabled(renderersWep, is_enable: false);
					}
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, weaponObject, delegate(InstantiateManager.InstantiateData data)
					{
						//IL_0007: Unknown result type (might be due to invalid IL or missing references)
						weapon2 = data.instantiatedObject.get_transform();
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
					yield return this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, weapon2, shader_type));
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
				Material materialR = null;
				Material materialL = null;
				if (renderersWep != null)
				{
					int num27 = 0;
					for (int num28 = renderersWep.Length; num27 < num28; num27++)
					{
						if (renderersWep[num27].get_name().EndsWith("_L"))
						{
							materialL = renderersWep[num27].get_material();
							wepL = renderersWep[num27].get_transform();
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
							materialR = renderersWep[num27].get_material();
							wepR = renderersWep[num27].get_transform();
							Utility.Attach(socketWepR, wepR);
						}
					}
				}
				if (weapon2 != null)
				{
					Object.DestroyImmediate(weapon2.get_gameObject());
				}
				if (lo_hr_wep_tex != null)
				{
					ApplyWeaponHighResoTexs(lo_hr_wep_tex, high_reso_tex_flags, materialR, materialL);
				}
			}
		}
		if (animator != null && lo_anim != null)
		{
			RuntimeAnimatorController val6 = lo_anim.loadedObjects[0].obj as RuntimeAnimatorController;
			if (val6 != null)
			{
				animator.set_runtimeAnimatorController(val6);
				if (player != null)
				{
					animator.get_gameObject().AddComponent<StageObjectProxy>().stageObject = player;
					if (need_anim_event)
					{
						player.animEventData = (lo_anim.loadedObjects[1].obj as AnimEventData);
					}
				}
				animator.set_updateMode(1);
				if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && MonoBehaviourSingleton<OutGameSettingsManager>.I.statusScene.isPlaySpAttackTypeMotion)
				{
					SP_ATTACK_TYPE weaponSpAttackType = (SP_ATTACK_TYPE)info.weaponSpAttackType;
					if (weaponSpAttackType != 0)
					{
						string text6 = weaponSpAttackType.ToString();
						int parameterCount = animator.get_parameterCount();
						for (int num29 = 0; num29 < parameterCount; num29++)
						{
							AnimatorControllerParameter parameter = animator.GetParameter(num29);
							if (parameter.get_name() == text6)
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
			int aidx = 0;
			for (int alen = lo_accessories.Count; aidx < alen; aidx++)
			{
				LoadObject loacc = lo_accessories[aidx];
				Transform accTrans = null;
				if (!div_frame_realizes)
				{
					accTrans = loacc.Realizes();
				}
				else
				{
					wait = true;
					InstantiateManager.Request(this, loacc.loadedObject, delegate(InstantiateManager.InstantiateData data)
					{
						//IL_0007: Unknown result type (might be due to invalid IL or missing references)
						accTrans = data.instantiatedObject.get_transform();
						wait = false;
					});
					while (wait)
					{
						yield return null;
					}
				}
				if (accTrans != null)
				{
					AccessoryTable.AccessoryInfoData infoData2 = Singleton<AccessoryTable>.I.GetInfoData(info.accUIDs[aidx]);
					accTrans.SetParent(GetNodeTrans(infoData2.node));
					accTrans.set_localPosition(infoData2.offset);
					accTrans.set_localRotation(infoData2.rotation);
					accTrans.set_localScale(infoData2.scale);
					accessory.Add(accTrans);
					accRendererList.AddRange(accTrans.GetComponentsInChildren<Renderer>());
				}
			}
			if (!accessory.IsNullOrEmpty())
			{
				int i = 0;
				for (int len = accessory.Count; i < len; i++)
				{
					yield return this.StartCoroutine(ItemLoader.InitRoopEffect(equipItemRoot: accessory[i], queue: load_queue, shaderType: shader_type));
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
				player.controller.set_enabled(true);
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
			ResourceLoad component = player.get_gameObject().GetComponent<ResourceLoad>();
			if (component != null && component.list != null)
			{
				List<string> list2 = new List<string>();
				int num30 = 0;
				for (int size = component.list.size; num30 < size; num30++)
				{
					list2.Add(component.list.buffer[num30].name);
				}
				list2.Distinct();
				MonoBehaviourSingleton<ResourceManager>.I.cache.AddIgnoreCategorySpecifiedReleaseList(list2);
			}
		}
		isLoading = false;
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
		GameObject infoObj;
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
			infoObj = loadObj.Realizes(MonoBehaviourSingleton<InGameSettingsManager>.I._transform).get_gameObject();
		}
		else
		{
			Object selfPlayerResourceCache = MonoBehaviourSingleton<GoGameCacheManager>.I.GetSelfPlayerResourceCache(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, infoName);
			infoObj = LoadObject.RealizesWithGameObject(selfPlayerResourceCache, MonoBehaviourSingleton<InGameSettingsManager>.I._transform).get_gameObject();
		}
		SplitPlayerAttackInfo attackInfo = infoObj.GetComponent<SplitPlayerAttackInfo>();
		hitInfos.Add(attackInfo.attackHitInfo);
		if (!string.IsNullOrEmpty(attackInfo.attackHitInfo.nextBulletInfoName))
		{
			yield return this.StartCoroutine(LoadNextBulletInfo(loadQueue, attackInfo.attackHitInfo.nextBulletInfoName, hitInfos, isNeedToCache));
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
		if (effectSettings == null || effectSettings.Length <= 0)
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
			Object.DestroyImmediate(wepR.get_gameObject());
		}
		if (wepL != null)
		{
			Object.DestroyImmediate(wepL.get_gameObject());
		}
		if (body != null)
		{
			Object.DestroyImmediate(body.get_gameObject());
		}
		if (shadow != null)
		{
			Object.DestroyImmediate(shadow.get_gameObject());
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
		eyeBlinkTime -= Time.get_deltaTime();
		if (eyeBlinkTime <= 0f)
		{
			if (faceID != FACE_ID.CLOSE_EYE)
			{
				ChangeFace(FACE_ID.CLOSE_EYE);
				eyeBlinkTime = Random.Range(0.1f, 0.3f);
			}
			else
			{
				ChangeFace(FACE_ID.NORMAL);
				eyeBlinkTime = Random.Range(3f, 6f);
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
		body.GetComponentsInChildren<DynamicBone>(true, dynamicBones_Body);
		if (dynamicBones_Body.Count > 0)
		{
			int i = 0;
			for (int count = dynamicBones_Body.Count; i < count; i++)
			{
				Object.DestroyImmediate(dynamicBones_Body[i]);
				dynamicBones_Body[i] = null;
			}
			dynamicBones_Body.Clear();
		}
	}

	public void SetDynamicBones(Transform body, Transform hair, bool isEnable)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		if (this.dynamicBones == null)
		{
			this.dynamicBones = new List<DynamicBone>();
		}
		List<DynamicBone> dynamicBones = this.dynamicBones;
		dynamicBones.Clear();
		hair.GetComponentsInChildren<DynamicBone>(true, dynamicBones);
		if (dynamicBones.Count <= 0)
		{
			return;
		}
		if (isEnable)
		{
			Transform val = Utility.Find(body, "Neck");
			DynamicBoneCollider dynamicBoneCollider = val.get_gameObject().AddComponent<DynamicBoneCollider>();
			dynamicBoneCollider.m_Radius = 0.1f;
			dynamicBoneCollider.m_Height = 0.39f;
			dynamicBoneCollider.m_Direction = DynamicBoneCollider.Direction.Z;
			dynamicBoneCollider.m_Center = new Vector3(0.06f, 0.02f, 0f);
			Transform val2 = Utility.Find(val, "Head");
			DynamicBoneCollider dynamicBoneCollider2 = val2.get_gameObject().AddComponent<DynamicBoneCollider>();
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
				Object.DestroyImmediate(dynamicBones[j]);
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
			if (dynamicBone != null && dynamicBone.get_enabled())
			{
				dynamicBone.set_enabled(false);
				dynamicBone.set_enabled(true);
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
		Transform base_model = LoadObject.RealizesWithGameObject(gameObj);
		return AddSkin(base_model, renderersBody[0] as SkinnedMeshRenderer);
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
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		SkinnedMeshRenderer componentInChildren = base_model.GetComponentInChildren<SkinnedMeshRenderer>();
		Transform val = null;
		if (body_skin_renderer != null && componentInChildren != null)
		{
			GameObject val2 = new GameObject(componentInChildren.get_name());
			val = val2.get_transform();
			val.get_transform().set_parent(body_skin_renderer.get_transform().get_parent());
			SkinnedMeshRenderer val3 = val2.AddComponent<SkinnedMeshRenderer>();
			val3.set_sharedMesh(componentInChildren.get_sharedMesh());
			val3.set_sharedMaterials(componentInChildren.get_sharedMaterials());
			val3.set_quality(componentInChildren.get_quality());
			val3.set_localBounds(BOUNDS);
			Transform rootBone = body_skin_renderer.get_rootBone();
			if (componentInChildren.get_rootBone() != null)
			{
				val3.set_rootBone(Utility.Find(rootBone, componentInChildren.get_rootBone().get_name()));
				EffectPlayProcessor component = base_model.GetComponent<EffectPlayProcessor>();
				if (component != null)
				{
					EffectPlayProcessor effectPlayProcessor = val3.get_rootBone().get_gameObject().AddComponent<EffectPlayProcessor>();
					effectPlayProcessor.effectSettings = component.effectSettings;
					List<Transform> list = effectPlayProcessor.PlayEffect("InitRoop");
					if (list != null)
					{
						for (int i = 0; i < list.Count; i++)
						{
							Utility.SetLayerWithChildren(list[i], val3.get_rootBone().get_gameObject().get_layer());
						}
					}
				}
			}
			Transform[] bones = componentInChildren.get_bones();
			Transform[] array = (Transform[])new Transform[componentInChildren.get_bones().Length];
			int j = 0;
			for (int num = bones.Length; j < num; j++)
			{
				array[j] = Utility.Find(rootBone, bones[j].get_name());
			}
			val3.set_bones(array);
		}
		Object.DestroyImmediate(base_model.get_gameObject());
		base_model = null;
		if (layer != -1)
		{
			Utility.SetLayerWithChildren(val, layer);
		}
		return val;
	}

	public void InvisibleBodyTriangles(int level)
	{
		InvisibleBodyTriangles(level, renderersBody[0] as SkinnedMeshRenderer);
	}

	public static void InvisibleBodyTriangles(int level, SkinnedMeshRenderer body_skin_renderer)
	{
		if (level == 0 || body_skin_renderer == null)
		{
			return;
		}
		Color32[] colors = body_skin_renderer.get_sharedMesh().get_colors32();
		if (colors.Length == 0)
		{
			return;
		}
		Mesh val = ResourceUtility.Instantiate<Mesh>(body_skin_renderer.get_sharedMesh());
		int[] triangles = val.get_triangles();
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
		val.set_triangles(triangles);
		body_skin_renderer.set_sharedMesh(val);
	}

	public virtual void ChangeFace(FACE_ID id)
	{
		if (faceID != id)
		{
			faceID = id;
			if (validFaceChange)
			{
				renderersFace[0].get_material().SetFloat("_Face_shift", (float)id);
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
				renderers[i].set_enabled(is_enabled);
			}
		}
	}

	public static void SetLightProbes(Transform t, bool enable_light_probes)
	{
		SetLightProbes(t.GetComponentsInChildren<Renderer>(), enable_light_probes);
	}

	public static void SetLightProbes(Renderer[] renderers, bool enable_light_probes)
	{
		if (renderers != null)
		{
			int i = 0;
			for (int num = renderers.Length; i < num; i++)
			{
				renderers[i].set_useLightProbes(enable_light_probes);
			}
		}
	}

	public static void SetLayerWithChildren_SecondaryNoChange(Transform transform, int layer)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		transform.get_gameObject().set_layer(layer);
		IEnumerator enumerator = transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform2 = enumerator.Current;
				SetLayerWithChildren_SecondaryNoChange(transform2, layer);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	public static Vector4 ApplySkinColorCoef(Color skin_color)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
		{
			return Color.op_Implicit(skin_color);
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
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetSkinColor(t, NGUIMath.IntToColor(color));
	}

	public static void SetSkinColor(Transform t, Color color)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		SetSkinColor(t.GetComponentsInChildren<Renderer>(), color);
	}

	public static void SetSkinColor(Renderer[] renderers, int color)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetSkinColor(renderers, NGUIMath.IntToColor(color));
	}

	public static void SetSkinColor(Renderer[] renderers, Color color)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		int ID_CHANGE_SKIN_COLOR = Shader.PropertyToID("_Change_Skin_Color");
		Vector4 _color = ApplySkinColorCoef(color);
		Utility.MaterialForEach(renderers, delegate(Material mtrl)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			mtrl.SetVector(ID_CHANGE_SKIN_COLOR, _color);
		});
	}

	public static void SetEquipColor(Transform t, int color)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetEquipColor(t, NGUIMath.IntToColor(color));
	}

	public static void SetEquipColor(Transform t, Color color)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		SetEquipColor(t.GetComponentsInChildren<Renderer>(), color);
	}

	public static void SetEquipColor(Renderer[] renderers, int color)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetEquipColor(renderers, NGUIMath.IntToColor(color));
	}

	public static void SetEquipColor(Renderer[] renderers, Color color)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		int ID_CHANGE_EQUIP_COLOR = Shader.PropertyToID("_Change_Equip_Color");
		Utility.MaterialForEach(renderers, delegate(Material mtrl)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			mtrl.SetColor(ID_CHANGE_EQUIP_COLOR, color);
		});
	}

	public static void SetEquipColor3(Renderer[] renderers, int color0, int color1, int color2)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		SetEquipColor3(renderers, NGUIMath.IntToColor(color0), NGUIMath.IntToColor(color1), NGUIMath.IntToColor(color2));
	}

	public static void SetEquipColor3(Renderer[] renderers, Color color0, Color color1, Color color2)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		int ID_CHANGE_EQUIP_COLOR3 = Shader.PropertyToID("_Change_Equip_Color");
		int ID_CHANGE_EQUIP_COLOR4 = Shader.PropertyToID("_Change_Equip1_Color");
		int ID_CHANGE_EQUIP_COLOR2 = Shader.PropertyToID("_Change_Equip2_Color");
		Utility.MaterialForEach(renderers, delegate(Material mtrl)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			mtrl.SetColor(ID_CHANGE_EQUIP_COLOR3, color0);
			mtrl.SetColor(ID_CHANGE_EQUIP_COLOR4, color1);
			mtrl.SetColor(ID_CHANGE_EQUIP_COLOR2, color2);
		});
	}

	public static void SetWeaponShader(Renderer[] renderers, int color0, int color1, int color2, int effectID, float effectParam, int effectColor)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		SetWeaponShader(renderers, NGUIMath.IntToColor(color0), NGUIMath.IntToColor(color1), NGUIMath.IntToColor(color2), effectID, effectParam, NGUIMath.IntToColor(effectColor));
	}

	public static void SetWeaponShader(Renderer[] renderers, Color color0, Color color1, Color color2, int effectID, float effectParam, Color effectColor)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		int ID_CHANGE_EQUIP_COLOR0 = Shader.PropertyToID("_Change_Equip_Color");
		int ID_CHANGE_EQUIP_COLOR = Shader.PropertyToID("_Change_Equip1_Color");
		int ID_CHANGE_EQUIP_COLOR2 = Shader.PropertyToID("_Change_Equip2_Color");
		int ID_CHANGE_ATTRIBUTE_COLOR = Shader.PropertyToID("_Change_Attribute_Color");
		int ID_ATTRIBUTE_PITCH = Shader.PropertyToID("_attribute_Pitch");
		Utility.MaterialForEach(renderers, delegate(Material mtrl)
		{
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			Shader val = ResourceUtility.FindShader($"{mtrl.get_shader().get_name()}_{effectID}");
			if (val != null && mtrl.get_shader() != val)
			{
				mtrl.set_shader(val);
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
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		SetSkinAndEquipColor(t, NGUIMath.IntToColor(skin_color), NGUIMath.IntToColor(equip_color), z_bias);
	}

	public static void SetSkinAndEquipColor(Transform t, Color skin_color, Color equip_color, float z_bias)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		SetSkinAndEquipColor(t.GetComponentsInChildren<Renderer>(), skin_color, equip_color, z_bias);
	}

	public static void SetSkinAndEquipColor(Renderer[] renderers, int skin_color, int equip_color, float z_bias)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		SetSkinAndEquipColor(renderers, NGUIMath.IntToColor(skin_color), NGUIMath.IntToColor(equip_color), z_bias);
	}

	public static void SetSkinAndEquipColor(Renderer[] renderers, Color skin_color, Color equip_color, float z_bias)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		int ID_CHANGE_SKIN_COLOR = Shader.PropertyToID("_Change_Skin_Color");
		int ID_CHANGE_EQUIP_COLOR = Shader.PropertyToID("_Change_Equip_Color");
		int ID_ZBIAS = Shader.PropertyToID("_ZBias");
		Vector4 _skin_color = ApplySkinColorCoef(skin_color);
		Utility.MaterialForEach(renderers, delegate(Material mtrl)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			mtrl.SetVector(ID_CHANGE_SKIN_COLOR, _skin_color);
			mtrl.SetColor(ID_CHANGE_EQUIP_COLOR, equip_color);
			mtrl.SetFloat(ID_ZBIAS, z_bias);
		});
	}

	public static Transform CreateShadow(Transform parent = null, bool fixedY0 = true, int layer = -1, bool is_lightweight = false)
	{
		Transform val = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.CreateShadow(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.shadowSize, MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.height * 0.5f, 1f, fixedY0, parent, is_lightweight);
		if (val == null)
		{
			return null;
		}
		if (layer != -1)
		{
			val.get_gameObject().set_layer(layer);
		}
		return val;
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
		int num4 = Shader.PropertyToID("_MaskTex");
		while (flags != 0)
		{
			if ((flags & num) != 0)
			{
				flags &= ~num;
				int num5 = num3;
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
				Texture val = lo_high_reso_texs.loadedObjects[num2++].obj as Texture;
				if (val != null)
				{
					if (flag && mate0 != null)
					{
						mate0.SetTexture(num5, val);
					}
					if (flag2 && mate1 != null)
					{
						mate1.SetTexture(num5, val);
					}
				}
			}
			num <<= 1;
		}
	}

	public static void ApplyEquipHighResoTexs(LoadObject lo, Renderer[] renderers)
	{
		if (lo == null || lo.loadedObjects == null || lo.loadedObjects.Length <= 0 || renderers == null)
		{
			return;
		}
		Texture val = lo.loadedObjects[0].obj as Texture;
		if (val == null)
		{
			return;
		}
		int num = 0;
		goto IL_006f;
		IL_006b:
		num++;
		goto IL_006f;
		IL_006f:
		if (num < renderers.Length)
		{
			renderers[num].get_material().SetTexture(Shader.PropertyToID("_MainTex"), val);
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
		Shader val = ResourceUtility.FindShader("mobile/Custom/Character/character_matcap_overlay");
		if (val == null)
		{
			return;
		}
		Texture val2 = lo.loadedObjects[0].obj as Texture;
		if (val2 == null)
		{
			return;
		}
		Texture val3 = lo.loadedObjects[1].obj as Texture;
		if (val3 == null)
		{
			return;
		}
		int num = Shader.PropertyToID("_MainTex");
		int num2 = Shader.PropertyToID("_MatCap");
		int num3 = 0;
		goto IL_00d9;
		IL_00d3:
		num3++;
		goto IL_00d9;
		IL_00d9:
		if (num3 < renderers.Length)
		{
			renderers[num3].get_material().set_shader(val);
			renderers[num3].get_material().SetTexture(num, val2);
			renderers[num3].get_material().SetTexture(num2, val3);
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
				Object.Destroy(loaders[i].get_gameObject());
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
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
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
		wepL.set_localPosition(pos);
		wepL.set_localRotation(rot);
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
			Transform val = accessory[i];
			if (!(val == null) && val.get_name() == name)
			{
				return val;
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
			Transform val = accessory[i];
			if (!(val == null) && val.get_name() == name)
			{
				Object.Destroy(val.get_gameObject());
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
