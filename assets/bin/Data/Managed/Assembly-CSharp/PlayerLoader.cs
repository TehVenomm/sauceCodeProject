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

	public const string BASE_ANIM_TABLE_KEY = "BASE";

	public const int kHighResoTextureType_None = 0;

	public const int kHighResoTextureType_Main = 1;

	public const int kHighResoTextureType_MainMask = 2;

	public const int kHighResoTextureType_Right = 4;

	public const int kHighResoTextureType_RightMask = 8;

	public const int kHighResoTextureType_Left = 16;

	public const int kHighResoTextureType_LeftMask = 32;

	public static readonly Bounds BOUNDS = new Bounds(Vector3.get_zero(), new Vector3(2f, 2f, 2f));

	public HashSet<string> playerLoaderLoadedAttackInfoNames;

	public List<Transform> accessory = new List<Transform>();

	private bool autoEyeBlink;

	private FACE_ID faceID;

	private bool validFaceChange;

	protected float eyeBlinkTime;

	private ResourceObject[] voiceAudioClips;

	private int[] voiceAudioClipIds;

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
		private set;
	}

	public Transform wepR
	{
		get;
		private set;
	}

	public Transform wepL
	{
		get;
		private set;
	}

	public Transform face
	{
		get;
		private set;
	}

	public Transform hair
	{
		get;
		private set;
	}

	public Transform body
	{
		get;
		private set;
	}

	public Transform head
	{
		get;
		private set;
	}

	public Transform arm
	{
		get;
		private set;
	}

	public Transform leg
	{
		get;
		private set;
	}

	public Animator animator
	{
		get;
		private set;
	}

	public Transform shadow
	{
		get;
		private set;
	}

	public Transform hairPhysics
	{
		get;
		private set;
	}

	public Renderer[] renderersWep
	{
		get;
		private set;
	}

	public Renderer[] renderersFace
	{
		get;
		private set;
	}

	public Renderer[] renderersHair
	{
		get;
		private set;
	}

	public Renderer[] renderersBody
	{
		get;
		private set;
	}

	public Renderer[] renderersHead
	{
		get;
		private set;
	}

	public Renderer[] renderersArm
	{
		get;
		private set;
	}

	public Renderer[] renderersLeg
	{
		get;
		private set;
	}

	public Renderer[] renderersAccessory
	{
		get;
		private set;
	}

	public Transform socketWepL
	{
		get;
		private set;
	}

	public Transform socketWepR
	{
		get;
		private set;
	}

	public Transform socketHead
	{
		get;
		private set;
	}

	public Transform socketFootL
	{
		get;
		private set;
	}

	public Transform socketFootR
	{
		get;
		private set;
	}

	public Transform socketHandL
	{
		get;
		private set;
	}

	public StringKeyTable<LoadObject> animObjectTable
	{
		get;
		private set;
	}

	public bool eyeBlink
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
		private set;
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
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
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

	public int GetVoiceId(ACTION_VOICE_ID voice_type)
	{
		if (loadInfo == null)
		{
			return 0;
		}
		int num = RandomizeAttackVoice((int)voice_type);
		return loadInfo.actionVoiceBaseID + num;
	}

	public int GetVoiceId(ACTION_VOICE_EX_ID voice_type)
	{
		if (loadInfo == null)
		{
			return 0;
		}
		int num = RandomizeAttackVoice((int)voice_type);
		return loadInfo.actionVoiceBaseID + num;
	}

	private void UpdateVoiceAudioClipIds()
	{
		if (voiceAudioClips != null && voiceAudioClips.Length > 0)
		{
			voiceAudioClipIds = new int[voiceAudioClips.Length];
			int i = 0;
			int result;
			for (int num = voiceAudioClips.Length; i < num; voiceAudioClipIds[i] = result, i++)
			{
				result = 0;
				ResourceObject resourceObject = voiceAudioClips[i];
				if (resourceObject != null && resourceObject.obj != null && resourceObject.obj.get_name() != null)
				{
					string s = resourceObject.obj.get_name().Substring(resourceObject.obj.get_name().Length - 4);
					if (int.TryParse(s, out result))
					{
						continue;
					}
				}
			}
		}
		else
		{
			voiceAudioClipIds = null;
		}
	}

	public AudioClip GetVoiceAudioClip(int voice_id)
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

	private int RandomizeAttackVoice(int voice_id)
	{
		switch (voice_id)
		{
		case 1:
			return ChooseRandom(ATK_VOICE_S, 0.2f);
		case 2:
		case 3:
			return ChooseRandom(ATK_VOICE_M, 0f);
		case 4:
		case 5:
			return ChooseRandom(ATK_VOICE_L, 0f);
		case 14:
			return ChooseRandom(HAPPY_VOICES, 0f);
		case 6:
			return ChooseRandom(DAMAGE_VOICES, 0f);
		case 9:
			return ChooseRandom(DEATH_VOICES, 0f);
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

	private void LoadAttackInfoResource(Player player, LoadingQueue loadQueue)
	{
		AttackInfo[] attackInfos = player.GetAttackInfos();
		if (attackInfos != null)
		{
			int num = attackInfos.Length;
			for (int i = 0; i < num; i++)
			{
				AttackInfo attackInfo = attackInfos[i];
				if (attackInfo != null)
				{
					loadQueue.CacheBulletDataUseResource(attackInfos[i].bulletData, player);
					AttackHitInfo attackHitInfo = attackInfos[i] as AttackHitInfo;
					if (attackHitInfo != null)
					{
						if (attackHitInfo.hitSEID != 0)
						{
							loadQueue.CacheSE(attackHitInfo.hitSEID, null);
						}
						loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, attackHitInfo.hitEffectName);
						loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, attackHitInfo.remainEffectName);
						if (!string.IsNullOrEmpty(attackHitInfo.toEnemy.hitTypeName))
						{
							EnemyHitTypeTable.TypeData data = Singleton<EnemyHitTypeTable>.I.GetData(attackHitInfo.toEnemy.hitTypeName, FieldManager.IsValidInGameNoQuest());
							if (data != null)
							{
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
					}
				}
			}
		}
	}

	public void StartLoad(PlayerLoadInfo player_load_info, int layer, int anim_id, bool need_anim_event, bool need_foot_stamp, bool need_shadow, bool enable_light_probes, bool need_action_voice, bool need_high_reso_tex, bool need_res_ref_count, bool need_dev_frame_instantiate, SHADER_TYPE shader_type, OnCompleteLoad callback, bool enable_eye_blick = true, int use_hair_overlay = -1)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (isLoading)
		{
			Log.Error(LOG.RESOURCE, this.get_name() + " now loading.");
		}
		else
		{
			this.StartCoroutine(DoLoad(player_load_info, layer, anim_id, need_anim_event, need_foot_stamp, need_shadow, enable_light_probes, need_action_voice, need_high_reso_tex, need_res_ref_count, need_dev_frame_instantiate, shader_type, callback, enable_eye_blick, use_hair_overlay));
		}
	}

	private unsafe IEnumerator DoLoad(PlayerLoadInfo info, int layer, int anim_id, bool need_anim_event, bool need_foot_stamp, bool need_shadow, bool enable_light_probes, bool need_action_voice, bool need_high_reso_tex, bool need_res_ref_count, bool need_dev_frame_instantiate, SHADER_TYPE shader_type, OnCompleteLoad callback, bool enable_eye_blick, int use_hair_overlay)
	{
		if (info == null)
		{
			Log.Error(LOG.RESOURCE, "PlayerLoader:info=null");
		}
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
			EquipModelHQTable hqTable = MonoBehaviourSingleton<GlobalSettingsManager>.I.equipModelHQTable;
			high_reso_tex_flags = hqTable.GetWeaponFlag(info.weaponModelID);
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
		if (body_name != null)
		{
			Transform _this = this.get_transform();
			if (player != null)
			{
				if (player.controller != null)
				{
					player.controller.set_enabled(false);
				}
				if (player.packetReceiver != null)
				{
					player.packetReceiver.SetStopPacketUpdate(true);
				}
				player.OnLoadStart();
			}
			isLoading = true;
			LoadingQueue load_queue = new LoadingQueue(this, need_res_ref_count);
			LoadObject lo_face;
			LoadObject lo_hair;
			LoadObject lo_body;
			LoadObject lo_head;
			LoadObject lo_arm;
			LoadObject lo_leg;
			LoadObject lo_wepn;
			if (need_dev_frame_instantiate)
			{
				lo_face = ((face_name == null) ? null : load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_FACE, face_name));
				lo_hair = ((hair_name == null) ? null : load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name));
				lo_body = ((body_name == null) ? null : load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_BDY, body_name));
				lo_head = ((head_name == null) ? null : load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_HEAD, head_name));
				lo_arm = ((arm_name == null) ? null : load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_ARM, arm_name));
				lo_leg = ((leg_name == null) ? null : load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_LEG, leg_name));
				lo_wepn = ((wepn_name == null) ? null : load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name));
			}
			else
			{
				lo_face = ((face_name == null) ? null : load_queue.Load(RESOURCE_CATEGORY.PLAYER_FACE, face_name, false));
				lo_hair = ((hair_name == null) ? null : load_queue.Load(RESOURCE_CATEGORY.PLAYER_HEAD, hair_name, false));
				lo_body = ((body_name == null) ? null : load_queue.Load(RESOURCE_CATEGORY.PLAYER_BDY, body_name, false));
				lo_head = ((head_name == null) ? null : load_queue.Load(RESOURCE_CATEGORY.PLAYER_HEAD, head_name, false));
				lo_arm = ((arm_name == null) ? null : load_queue.Load(RESOURCE_CATEGORY.PLAYER_ARM, arm_name, false));
				lo_leg = ((leg_name == null) ? null : load_queue.Load(RESOURCE_CATEGORY.PLAYER_LEG, leg_name, false));
				lo_wepn = ((wepn_name == null) ? null : load_queue.Load(RESOURCE_CATEGORY.PLAYER_WEAPON, wepn_name, false));
			}
			List<LoadObject> lo_accessories = new List<LoadObject>();
			if (!info.accUIDs.IsNullOrEmpty())
			{
				int k = 0;
				for (int len = info.accUIDs.Count; k < len; k++)
				{
					AccessoryTable.AccessoryInfoData ainfo2 = Singleton<AccessoryTable>.I.GetInfoData(info.accUIDs[k]);
					string aname = ResourceName.GetPlayerAccessory(ainfo2.accessoryId);
					if (need_dev_frame_instantiate)
					{
						lo_accessories.Add(load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_ACCESSORY, aname));
					}
					else
					{
						lo_accessories.Add(load_queue.Load(RESOURCE_CATEGORY.PLAYER_ACCESSORY, aname, false));
					}
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
				yield return (object)load_queue.Wait();
			}
			LoadObject lo_anim = (anim_name == null) ? null : load_queue.Load(RESOURCE_CATEGORY.PLAYER_ANIM, anim_name, need_anim_event ? new string[2]
			{
				anim_name + "Ctrl",
				anim_name + "Event"
			} : new string[1]
			{
				anim_name + "Ctrl"
			}, false);
			if (lo_anim != null)
			{
				animObjectTable.Add("BASE", lo_anim);
			}
			if (player != null && anim_id > -1)
			{
				List<string> key_list = new List<string>();
				int skill_len = 3;
				for (int i12 = 0; i12 < skill_len; i12++)
				{
					for (int j2 = 0; j2 < 2; j2++)
					{
						SkillInfo.SkillParam param3 = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + i12);
						if (param3 != null)
						{
							string anim_format_name = (j2 != 0) ? param3.tableData.actStateName : param3.tableData.castStateName;
							string ctrl_name = Character.GetCtrlNameFromAnimFormatName(anim_format_name);
							if (!string.IsNullOrEmpty(ctrl_name) && key_list.IndexOf(ctrl_name) < 0)
							{
								key_list.Add(ctrl_name);
								string sub_anim_name = ResourceName.GetPlayerSubAnim(anim_id, ctrl_name);
								LoadObject lo_sub_anim = (sub_anim_name == null) ? null : load_queue.Load(RESOURCE_CATEGORY.PLAYER_ANIM_SKILL, sub_anim_name, need_anim_event ? new string[2]
								{
									sub_anim_name + "Ctrl",
									sub_anim_name + "Event"
								} : new string[1]
								{
									sub_anim_name + "Ctrl"
								}, false);
								if (lo_sub_anim != null)
								{
									animObjectTable.Add(ctrl_name, lo_sub_anim);
								}
							}
						}
					}
				}
			}
			if (player != null && info.weaponEvolveId != 0)
			{
				ResourceName.GetPlayerEvolveAnim(info.weaponEvolveId, out string cn, out string sn);
				LoadObject lo_evolve = load_queue.Load(RESOURCE_CATEGORY.PLAYER_ANIM_EVOLVE, sn, new string[2]
				{
					sn + "Ctrl",
					sn + "Event"
				}, false);
				if (!object.ReferenceEquals(lo_evolve, null))
				{
					animObjectTable.Add(cn, lo_evolve);
				}
			}
			if (need_action_voice && info.actionVoiceBaseID > -1)
			{
				int[] values = (int[])Enum.GetValues(typeof(ACTION_VOICE_ID));
				int VOICE_NUM = values.Length;
				string[] names = new string[VOICE_NUM];
				for (int i11 = 0; i11 < VOICE_NUM; i11++)
				{
					names[i11] = ResourceName.GetActionVoiceName(info.actionVoiceBaseID + values[i11]);
				}
				lo_voices = load_queue.Load(RESOURCE_CATEGORY.SOUND_VOICE, ResourceName.GetActionVoicePackageNameFromVoiceID(info.actionVoiceBaseID), names, false);
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
			yield return (object)load_queue.Wait();
			List<string> needAtkInfoNames = new List<string>();
			StringKeyTable<LoadObject> animEventBulletLoadObjTable = new StringKeyTable<LoadObject>();
			if (player != null)
			{
				if (need_anim_event)
				{
					animObjectTable.ForEach(delegate(LoadObject load_object)
					{
						((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_0ec2: stateMachine*/)._003Cload_queue_003E__21.CacheAnimDataUseResource(load_object.loadedObjects[1].obj as AnimEventData, delegate(string effect_name)
						{
							if (effect_name[0] == '@')
							{
								return null;
							}
							return effect_name;
						}, null);
						((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_0ec2: stateMachine*/)._003Cload_queue_003E__21.CacheAnimDataUseResourceDependPlayer(((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_0ec2: stateMachine*/)._003Cplayer_003E__0, load_object.loadedObjects[1].obj as AnimEventData);
						AnimEventData animEventData = load_object.loadedObjects[1].obj as AnimEventData;
						if (animEventData != null && !animEventData.animations.IsNullOrEmpty())
						{
							AnimEventData.AnimData[] animations = animEventData.animations;
							for (int num3 = 0; num3 < animations.Length; num3++)
							{
								AnimEventData.EventData[] events = animations[num3].events;
								foreach (AnimEventData.EventData eventData in events)
								{
									AnimEventFormat.ID id = eventData.id;
									switch (id)
									{
									case AnimEventFormat.ID.SHOT_PRESENT:
									{
										int num6 = 0;
										for (int num7 = eventData.stringArgs.Length; num6 < num7; num6++)
										{
											string[] array = eventData.stringArgs[num6].Split(':');
											if (!array.IsNullOrEmpty())
											{
												int num8 = 0;
												for (int num9 = array.Length; num8 < num9; num8++)
												{
													string text2 = array[num8];
													if (!string.IsNullOrEmpty(text2))
													{
														LoadObject loadObject2 = ((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_0ec2: stateMachine*/)._003Cload_queue_003E__21.Load(RESOURCE_CATEGORY.INGAME_BULLET, text2, false);
														if (loadObject2 != null && ((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_0ec2: stateMachine*/)._003CanimEventBulletLoadObjTable_003E__60.Get(text2) == null)
														{
															((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_0ec2: stateMachine*/)._003CanimEventBulletLoadObjTable_003E__60.Add(text2, loadObject2);
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
										for (int num5 = 0; num5 < eventData.stringArgs.Length; num5++)
										{
											string text = eventData.stringArgs[num5];
											if (!string.IsNullOrEmpty(text))
											{
												LoadObject loadObject = ((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_0ec2: stateMachine*/)._003Cload_queue_003E__21.Load(RESOURCE_CATEGORY.INGAME_BULLET, text, false);
												if (loadObject != null && ((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_0ec2: stateMachine*/)._003CanimEventBulletLoadObjTable_003E__60.Get(text) == null)
												{
													((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_0ec2: stateMachine*/)._003CanimEventBulletLoadObjTable_003E__60.Add(text, loadObject);
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
									case AnimEventFormat.ID.PLAYER_FUNNEL_ATTACK:
									case AnimEventFormat.ID.SHOT_NODE_LINK:
									case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE:
									case AnimEventFormat.ID.PAIR_SWORDS_SHOT_BULLET:
									case AnimEventFormat.ID.PAIR_SWORDS_SHOT_LASER:
									case AnimEventFormat.ID.SHOT_HEALING_HOMING:
									case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE_MULTI:
									{
										string text3 = eventData.stringArgs[0];
										if (!string.IsNullOrEmpty(text3))
										{
											string[] namesNeededLoadAtkInfoFromAnimEvent = ResourceName.GetNamesNeededLoadAtkInfoFromAnimEvent();
											for (int num10 = 0; num10 < namesNeededLoadAtkInfoFromAnimEvent.Length; num10++)
											{
												if (text3.StartsWith(namesNeededLoadAtkInfoFromAnimEvent[num10]))
												{
													((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_0ec2: stateMachine*/)._003CneedAtkInfoNames_003E__59.Add(text3);
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
				if (playerLoaderLoadedAttackInfoNames == null)
				{
					playerLoaderLoadedAttackInfoNames = new HashSet<string>();
				}
				for (int i10 = 0; i10 < needAtkInfoNames.Count; i10++)
				{
					if (!playerLoaderLoadedAttackInfoNames.Contains(needAtkInfoNames[i10]))
					{
						LoadObject loadObj3 = load_queue.Load(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, needAtkInfoNames[i10], false);
						atkInfo.Add(loadObj3);
						playerLoaderLoadedAttackInfoNames.Add(needAtkInfoNames[i10]);
					}
				}
				if (load_queue.IsLoading())
				{
					yield return (object)load_queue.Wait();
				}
				if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
				{
					Transform _settingTransform = MonoBehaviourSingleton<InGameSettingsManager>.I._transform;
					List<AttackInfo> hitInfos = new List<AttackInfo>();
					for (int i9 = 0; i9 < atkInfo.Count; i9++)
					{
						GameObject infoObj = atkInfo[i9].Realizes(_settingTransform, -1).get_gameObject();
						SplitPlayerAttackInfo attackInfo = infoObj.GetComponent<SplitPlayerAttackInfo>();
						hitInfos.Add(attackInfo.attackHitInfo);
						hitInfos.Add(attackInfo.attackContinuationInfo);
						if (!string.IsNullOrEmpty(attackInfo.attackHitInfo.nextBulletInfoName))
						{
							yield return (object)this.StartCoroutine(LoadNextBulletInfo(load_queue, attackInfo.attackHitInfo.nextBulletInfoName, hitInfos));
						}
						if (!string.IsNullOrEmpty(attackInfo.attackContinuationInfo.nextBulletInfoName))
						{
							yield return (object)this.StartCoroutine(LoadNextBulletInfo(load_queue, attackInfo.attackContinuationInfo.nextBulletInfoName, hitInfos));
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
				int num = anim_id;
				switch (num)
				{
				case 0:
					load_queue.CacheSE(10000042, null);
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
							InGameSettingsManager.Player.OneHandSwordActionInfo ohsInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo;
							load_queue.CacheSE(ohsInfo.Soul_BoostSeId, null);
							load_queue.CacheSE(ohsInfo.Soul_SnatchHitSeId, null);
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsInfo.Soul_SnatchHitEffect);
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsInfo.Soul_SnatchHitRemainEffect);
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsInfo.Soul_SnatchHitEffectOnBoostMode);
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_btl_soul_energy_01");
							if (weaponElement <= ELEMENT_TYPE.DARK && (int)weaponElement < ohsInfo.Soul_BoostElementHitEffect.Length)
							{
								load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, ohsInfo.Soul_BoostElementHitEffect[(int)weaponElement]);
							}
						}
						break;
					case SP_ATTACK_TYPE.BURST:
					{
						InGameSettingsManager.Player.BurstOneHandSwordActionInfo burstInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.burstOHSInfo;
						if (weaponElement <= ELEMENT_TYPE.DARK && (int)weaponElement < burstInfo.BoostElementHitEffect.Length)
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstInfo.BoostElementHitEffect[(int)weaponElement]);
						}
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk3_sword_aura_01");
						break;
					}
					}
					break;
				case 1:
					if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
					{
						InGameSettingsManager.Player.TwoHandSwordActionInfo thsInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo;
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, thsInfo.nameChargeExpandEffect);
						if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
						{
							load_queue.CacheSE(thsInfo.soulBoostSeId, null);
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, thsInfo.soulIaiChargeMaxEffect);
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_btl_soul_energy_01");
						}
						if (player.spAttackType == SP_ATTACK_TYPE.BURST)
						{
							InGameSettingsManager.Player.BurstTwoHandSwordActionInfo burstInfo2 = thsInfo.burstTHSInfo;
							if (weaponElement <= ELEMENT_TYPE.DARK && (int)weaponElement < burstInfo2.HitEffect_SingleShot.Length)
							{
								load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstInfo2.HitEffect_SingleShot[(int)weaponElement]);
							}
							if (weaponElement <= ELEMENT_TYPE.DARK && (int)weaponElement < burstInfo2.HitEffect_FullBurst.Length)
							{
								load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, burstInfo2.HitEffect_FullBurst[(int)weaponElement]);
							}
						}
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
					}
					if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
					{
						InGameSettingsManager.Player.SpearActionInfo spearInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo;
						string jumpHitEff = spearInfo.jumpHugeHitEffectName;
						if (weaponElement <= ELEMENT_TYPE.DARK && (int)weaponElement < spearInfo.jumpHugeElementHitEffectNames.Length)
						{
							jumpHitEff = spearInfo.jumpHugeElementHitEffectNames[(int)weaponElement];
						}
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, jumpHitEff);
					}
					break;
				case 4:
					if (player.spAttackType == SP_ATTACK_TYPE.NONE)
					{
						if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
						{
							load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.wildDanceChargeMaxSeId, null);
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
					else if (player.spAttackType == SP_ATTACK_TYPE.SOUL && MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
					{
						InGameSettingsManager.Player.PairSwordsActionInfo pairSwordsInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo;
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, pairSwordsInfo.Soul_EffectForWaitingLaser);
						string bulletEffect = pairSwordsInfo.Soul_EffectForBullet;
						if (weaponElement <= ELEMENT_TYPE.DARK && (int)weaponElement < pairSwordsInfo.Soul_EffectsForBullet.Length)
						{
							bulletEffect = pairSwordsInfo.Soul_EffectsForBullet[(int)weaponElement];
						}
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, bulletEffect);
						if (!pairSwordsInfo.Soul_SeIds.IsNullOrEmpty())
						{
							for (int i8 = 0; i8 < pairSwordsInfo.Soul_SeIds.Length; i8++)
							{
								if (pairSwordsInfo.Soul_SeIds[i8] >= 0)
								{
									load_queue.CacheSE(pairSwordsInfo.Soul_SeIds[i8], null);
								}
							}
						}
					}
					break;
				case 5:
					if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
					{
						InGameSettingsManager.Player.SpecialActionInfo special_info = MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo;
						InGameSettingsManager.TargetMarkerSettings targetMarker = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerSettings;
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, special_info.arrowChargeAimEffectName);
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, special_info.arrowAimLesserCursorEffectName);
						load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.player.bestDistanceEffect);
						if (player.spAttackType == SP_ATTACK_TYPE.NONE)
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarker.effectNames[6]);
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarker.effectNames[5]);
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, special_info.arrowBleedEffectName);
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, special_info.arrowBleedDamageEffectName);
							switch (weaponElement)
							{
							case ELEMENT_TYPE.FIRE:
								load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, special_info.arrowFireBurstEffectName);
								break;
							case ELEMENT_TYPE.WATER:
								load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, special_info.arrowWaterBurstEffectName);
								break;
							case ELEMENT_TYPE.THUNDER:
								load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, special_info.arrowThunderBurstEffectName);
								break;
							case ELEMENT_TYPE.SOIL:
								load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, special_info.arrowSoilBurstEffectName);
								break;
							case ELEMENT_TYPE.LIGHT:
								load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, special_info.arrowLightrBurstEffectName);
								break;
							case ELEMENT_TYPE.DARK:
								load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, special_info.arrowDarkBurstEffectName);
								break;
							default:
								load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, special_info.arrowBurstEffectName);
								break;
							}
						}
						else if (player.spAttackType == SP_ATTACK_TYPE.HEAT)
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarker.effectNames[22]);
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarker.effectNames[21]);
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_bow_01_02");
						}
						else if (player.spAttackType == SP_ATTACK_TYPE.SOUL)
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, targetMarker.effectNames[24]);
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_bow_lock_02");
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk_charge_end_01");
							load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulLockMaxSeId, null);
							load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulLockSeId, null);
							load_queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.soulBoostSeId, null);
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_02_01");
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wsk2_longsword_03_01");
						}
					}
					break;
				}
				EvolveController.Load(load_queue, info.weaponEvolveId);
				int skill_len2 = 3;
				LoadObject[] bullet_load = new LoadObject[skill_len2];
				for (int i7 = 0; i7 < skill_len2; i7++)
				{
					SkillInfo.SkillParam param = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + i7);
					if (param == null)
					{
						bullet_load[i7] = null;
					}
					else
					{
						SkillItemTable.SkillItemData tableData = param.tableData;
						if (!string.IsNullOrEmpty(tableData.startEffectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, tableData.startEffectName);
						}
						if (tableData.startSEID != 0)
						{
							load_queue.CacheSE(tableData.startSEID, null);
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
							load_queue.CacheSE(tableData.actSEID, null);
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
							load_queue.CacheSE(tableData.hitSEID, null);
						}
						if (is_self)
						{
							load_queue.CacheItemIcon(tableData.iconID, null);
						}
						if (tableData.healType == HEAL_TYPE.RESURRECTION_ALL)
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_heal_04_03");
						}
						if (!tableData.buffTableIds.IsNullOrEmpty())
						{
							for (int buffIndex = 0; buffIndex < tableData.buffTableIds.Length; buffIndex++)
							{
								BuffTable.BuffData buffData = Singleton<BuffTable>.I.GetData((uint)tableData.buffTableIds[buffIndex]);
								if (BuffParam.IsHitAbsorbType(buffData.type))
								{
									load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_drain_01_01");
								}
								else if (buffData.type == BuffParam.BUFFTYPE.AUTO_REVIVE)
								{
									load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_sk_heal_04_03");
								}
							}
						}
						string[] supportEffectName = tableData.supportEffectName;
						foreach (string efName in supportEffectName)
						{
							if (!string.IsNullOrEmpty(efName))
							{
								load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, efName);
							}
						}
						bullet_load[i7] = load_queue.Load(RESOURCE_CATEGORY.INGAME_BULLET, tableData.bulletName, false);
					}
				}
				if (is_self)
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_pl_darkness_02");
				}
				EffectPlayProcessor processor = player.effectPlayProcessor;
				if (processor != null && processor.effectSettings != null)
				{
					int i6 = 0;
					for (int len4 = processor.effectSettings.Length; i6 < len4; i6++)
					{
						if (!string.IsNullOrEmpty(processor.effectSettings[i6].effectName))
						{
							string check_key = processor.effectSettings[i6].name;
							if (check_key.StartsWith("BUFF_"))
							{
								string weapon_key = check_key.Substring(check_key.Length - "_PLC00".Length);
								if (weapon_key.Contains("_PLC"))
								{
									string a = weapon_key;
									num = loadInfo.weaponModelID / 1000;
									if (a != "_PLC" + num.ToString("D2"))
									{
										continue;
									}
								}
							}
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, processor.effectSettings[i6].effectName);
						}
					}
				}
				if (load_queue.IsLoading())
				{
					yield return (object)load_queue.Wait();
				}
				for (int i5 = 0; i5 < skill_len2; i5++)
				{
					SkillInfo.SkillParam param2 = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + i5);
					if (param2 != null)
					{
						param2.bullet = (bullet_load[i5].loadedObject as BulletData);
						load_queue.CacheBulletDataUseResource(param2.bullet, player);
					}
				}
				animEventBulletLoadObjTable?.ForEachKeyAndValue(new Action<string, LoadObject>((object)/*Error near IL_2294: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				animEventBulletLoadObjTable.Clear();
				if (load_queue.IsLoading())
				{
					yield return (object)load_queue.Wait();
				}
			}
			if (lo_arm != null && lo_arm.loadedObject != null)
			{
				GameObject loadObj2 = lo_arm.loadedObject as GameObject;
				EffectPlayProcessor loadEffect2 = (!(loadObj2 != null)) ? null : loadObj2.GetComponent<EffectPlayProcessor>();
				if (loadEffect2 != null && loadEffect2.effectSettings != null)
				{
					int i4 = 0;
					for (int len3 = loadEffect2.effectSettings.Length; i4 < len3; i4++)
					{
						if (!string.IsNullOrEmpty(loadEffect2.effectSettings[i4].effectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, loadEffect2.effectSettings[i4].effectName);
						}
					}
				}
			}
			if (lo_leg != null && lo_leg.loadedObject != null)
			{
				GameObject loadObj = lo_leg.loadedObject as GameObject;
				EffectPlayProcessor loadEffect = (!(loadObj != null)) ? null : loadObj.GetComponent<EffectPlayProcessor>();
				if (loadEffect != null && loadEffect.effectSettings != null)
				{
					int i3 = 0;
					for (int len2 = loadEffect.effectSettings.Length; i3 < len2; i3++)
					{
						if (!string.IsNullOrEmpty(loadEffect.effectSettings[i3].effectName))
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, loadEffect.effectSettings[i3].effectName);
						}
					}
				}
			}
			if (load_queue.IsLoading())
			{
				yield return (object)load_queue.Wait();
			}
			bool div_frame_realizes = false;
			int skin_color = info.skinColor;
			if (!div_frame_realizes)
			{
				body = lo_body.Realizes(_this, -1);
				renderersBody = body.get_gameObject().GetComponentsInChildren<Renderer>();
				ModelLoaderBase.SetEnabled(renderersBody, false);
				SetDynamicBones_Body(body, enableBone);
			}
			else
			{
				bool wait8 = true;
				InstantiateManager.Request(this, lo_body.loadedObject, delegate(InstantiateManager.InstantiateData data)
				{
					//IL_000c: Unknown result type (might be due to invalid IL or missing references)
					//IL_0011: Unknown result type (might be due to invalid IL or missing references)
					//IL_0016: Expected O, but got Unknown
					((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_25c8: stateMachine*/)._003C_003Ef__this.body = data.instantiatedObject.get_transform();
					((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_25c8: stateMachine*/)._003C_003Ef__this.body.SetParent(((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_25c8: stateMachine*/)._003C_this_003E__20, false);
					((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_25c8: stateMachine*/)._003C_003Ef__this.renderersBody = ((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_25c8: stateMachine*/)._003C_003Ef__this.body.GetComponentsInChildren<Renderer>();
					((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_25c8: stateMachine*/)._003C_003Ef__this.SetDynamicBones_Body(((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_25c8: stateMachine*/)._003C_003Ef__this.body, ((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_25c8: stateMachine*/)._003CenableBone_003E__1);
					SetRenderersEnabled(((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_25c8: stateMachine*/)._003C_003Ef__this.renderersBody, false);
					((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_25c8: stateMachine*/)._003Cwait_003E__108 = false;
				}, false);
				while (wait8)
				{
					yield return (object)null;
				}
			}
			if (!(body == null))
			{
				yield return (object)this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, body, shader_type));
				if (renderersBody != null)
				{
					if (renderersBody.Length > 0)
					{
						SkinnedMeshRenderer body_skin = renderersBody[0] as SkinnedMeshRenderer;
						if (body_skin != null)
						{
							body_skin.set_localBounds(BOUNDS);
						}
					}
					SetSkinAndEquipColor(renderersBody, skin_color, info.bodyColor, 0f);
					ApplyEquipHighResoTexs(lo_hr_bdy_tex, renderersBody);
					animator = body.GetComponentInChildren<Animator>();
					if (player != null)
					{
						player.body = body;
					}
					socketHead = Utility.Find(body, "Head");
					socketWepL = Utility.Find(body, "L_Wep");
					socketWepR = Utility.Find(body, "R_Wep");
					socketFootL = Utility.Find(body, "L_Foot");
					socketFootR = Utility.Find(body, "R_Foot");
					socketHandL = Utility.Find(body, "L_Hand");
					if (need_foot_stamp)
					{
						if (socketFootL != null && socketFootL.GetComponent<StampNode>() == null)
						{
							StampNode node2 = socketFootL.get_gameObject().AddComponent<StampNode>();
							node2.offset = new Vector3(-0.08f, 0.01f, 0f);
							node2.autoBaseY = 0.1f;
						}
						if (socketFootR != null && socketFootR.GetComponent<StampNode>() == null)
						{
							StampNode node = socketFootR.get_gameObject().AddComponent<StampNode>();
							node.offset = new Vector3(-0.08f, 0.01f, 0f);
							node.autoBaseY = 0.1f;
						}
						CharacterStampCtrl step_ctrl = body.GetComponent<CharacterStampCtrl>();
						if (step_ctrl == null)
						{
							step_ctrl = body.get_gameObject().AddComponent<CharacterStampCtrl>();
						}
						step_ctrl.Init(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos, player, false);
						int i2 = 0;
						for (int n = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos.Length; i2 < n; i2++)
						{
							load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.stampInfos[i2].effectName);
						}
						if (load_queue.IsLoading())
						{
							yield return (object)load_queue.Wait();
						}
					}
					if (lo_face != null)
					{
						if (!div_frame_realizes)
						{
							face = lo_face.Realizes(socketHead, -1);
							renderersFace = face.get_gameObject().GetComponentsInChildren<Renderer>();
							ModelLoaderBase.SetEnabled(renderersFace, false);
						}
						else
						{
							bool wait8 = true;
							InstantiateManager.Request(this, lo_face.loadedObject, delegate(InstantiateManager.InstantiateData data)
							{
								//IL_000c: Unknown result type (might be due to invalid IL or missing references)
								//IL_0011: Unknown result type (might be due to invalid IL or missing references)
								//IL_0016: Expected O, but got Unknown
								((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2a74: stateMachine*/)._003C_003Ef__this.face = data.instantiatedObject.get_transform();
								((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2a74: stateMachine*/)._003C_003Ef__this.face.SetParent(((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2a74: stateMachine*/)._003C_003Ef__this.socketHead, false);
								((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2a74: stateMachine*/)._003C_003Ef__this.renderersFace = ((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2a74: stateMachine*/)._003C_003Ef__this.face.GetComponentsInChildren<Renderer>();
								SetRenderersEnabled(((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2a74: stateMachine*/)._003C_003Ef__this.renderersFace, false);
								((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2a74: stateMachine*/)._003Cwait_003E__108 = false;
							}, false);
							while (wait8)
							{
								yield return (object)null;
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
					if (lo_hair != null)
					{
						if (!div_frame_realizes)
						{
							hair = lo_hair.Realizes(socketHead, -1);
							renderersHair = hair.GetComponentsInChildren<Renderer>();
							ModelLoaderBase.SetEnabled(renderersHair, false);
							SetDynamicBones(body, hair, enableBone);
						}
						else
						{
							bool wait8 = true;
							InstantiateManager.Request(this, lo_hair.loadedObject, delegate(InstantiateManager.InstantiateData data)
							{
								//IL_000c: Unknown result type (might be due to invalid IL or missing references)
								//IL_0011: Unknown result type (might be due to invalid IL or missing references)
								//IL_0016: Expected O, but got Unknown
								((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2bde: stateMachine*/)._003C_003Ef__this.hair = data.instantiatedObject.get_transform();
								((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2bde: stateMachine*/)._003C_003Ef__this.hair.SetParent(((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2bde: stateMachine*/)._003C_003Ef__this.socketHead, false);
								((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2bde: stateMachine*/)._003C_003Ef__this.renderersHair = ((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2bde: stateMachine*/)._003C_003Ef__this.hair.GetComponentsInChildren<Renderer>();
								((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2bde: stateMachine*/)._003C_003Ef__this.SetDynamicBones(((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2bde: stateMachine*/)._003C_003Ef__this.body, ((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2bde: stateMachine*/)._003C_003Ef__this.hair, ((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2bde: stateMachine*/)._003CenableBone_003E__1);
								SetRenderersEnabled(((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2bde: stateMachine*/)._003C_003Ef__this.renderersHair, false);
								((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2bde: stateMachine*/)._003Cwait_003E__108 = false;
							}, false);
							while (wait8)
							{
								yield return (object)null;
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
					if (lo_head != null)
					{
						if (!div_frame_realizes)
						{
							head = lo_head.Realizes(socketHead, -1);
							if (head != null)
							{
								renderersHead = head.GetComponentsInChildren<Renderer>();
								ModelLoaderBase.SetEnabled(renderersHead, false);
							}
						}
						else
						{
							bool wait8 = true;
							InstantiateManager.Request(this, lo_head.loadedObject, delegate(InstantiateManager.InstantiateData data)
							{
								//IL_000c: Unknown result type (might be due to invalid IL or missing references)
								//IL_0011: Unknown result type (might be due to invalid IL or missing references)
								//IL_0016: Expected O, but got Unknown
								((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2d07: stateMachine*/)._003C_003Ef__this.head = data.instantiatedObject.get_transform();
								((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2d07: stateMachine*/)._003C_003Ef__this.head.SetParent(((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2d07: stateMachine*/)._003C_003Ef__this.socketHead, false);
								((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2d07: stateMachine*/)._003C_003Ef__this.renderersHead = ((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2d07: stateMachine*/)._003C_003Ef__this.head.GetComponentsInChildren<Renderer>();
								SetRenderersEnabled(((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2d07: stateMachine*/)._003C_003Ef__this.renderersHead, false);
								((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2d07: stateMachine*/)._003Cwait_003E__108 = false;
							}, false);
							while (wait8)
							{
								yield return (object)null;
							}
							if (renderersHead == null)
							{
								yield break;
							}
						}
						if (head != null)
						{
							yield return (object)this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, head, shader_type));
						}
						SetEquipColor(renderersHead, info.headColor);
						ApplyEquipHighResoTexs(lo_hr_hed_tex, renderersHead);
					}
					if (renderersBody != null)
					{
						if (lo_arm != null)
						{
							if (!div_frame_realizes)
							{
								arm = AddSkin(lo_arm);
								if (arm != null)
								{
									renderersArm = arm.GetComponentsInChildren<Renderer>();
									ModelLoaderBase.SetEnabled(renderersArm, false);
								}
							}
							else
							{
								bool wait8 = true;
								InstantiateManager.Request(this, lo_arm.loadedObject, delegate(InstantiateManager.InstantiateData data)
								{
									//IL_000c: Unknown result type (might be due to invalid IL or missing references)
									//IL_0011: Unknown result type (might be due to invalid IL or missing references)
									//IL_0016: Expected O, but got Unknown
									((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2e79: stateMachine*/)._003C_003Ef__this.arm = data.instantiatedObject.get_transform();
									((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2e79: stateMachine*/)._003C_003Ef__this.arm = ((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2e79: stateMachine*/)._003C_003Ef__this.AddSkin(((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2e79: stateMachine*/)._003C_003Ef__this.arm);
									((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2e79: stateMachine*/)._003C_003Ef__this.renderersArm = ((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2e79: stateMachine*/)._003C_003Ef__this.arm.GetComponentsInChildren<Renderer>();
									SetRenderersEnabled(((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2e79: stateMachine*/)._003C_003Ef__this.renderersArm, false);
									((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2e79: stateMachine*/)._003Cwait_003E__108 = false;
								}, false);
								while (wait8)
								{
									yield return (object)null;
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
						if (renderersBody != null)
						{
							if (lo_leg != null)
							{
								if (!div_frame_realizes)
								{
									leg = AddSkin(lo_leg);
									if (leg != null)
									{
										renderersLeg = leg.GetComponentsInChildren<Renderer>();
										ModelLoaderBase.SetEnabled(renderersLeg, false);
									}
								}
								else
								{
									bool wait8 = true;
									InstantiateManager.Request(this, lo_leg.loadedObject, delegate(InstantiateManager.InstantiateData data)
									{
										//IL_000c: Unknown result type (might be due to invalid IL or missing references)
										//IL_0011: Unknown result type (might be due to invalid IL or missing references)
										//IL_0016: Expected O, but got Unknown
										((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2fd8: stateMachine*/)._003C_003Ef__this.leg = data.instantiatedObject.get_transform();
										((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2fd8: stateMachine*/)._003C_003Ef__this.leg = ((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2fd8: stateMachine*/)._003C_003Ef__this.AddSkin(((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2fd8: stateMachine*/)._003C_003Ef__this.leg);
										((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2fd8: stateMachine*/)._003C_003Ef__this.renderersLeg = ((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2fd8: stateMachine*/)._003C_003Ef__this.leg.GetComponentsInChildren<Renderer>();
										SetRenderersEnabled(((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2fd8: stateMachine*/)._003C_003Ef__this.renderersLeg, false);
										((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_2fd8: stateMachine*/)._003Cwait_003E__108 = false;
									}, false);
									while (wait8)
									{
										yield return (object)null;
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
									weapon = lo_wepn.Realizes(null, -1);
									if (weapon != null)
									{
										renderersWep = weapon.get_gameObject().GetComponentsInChildren<Renderer>();
										ModelLoaderBase.SetEnabled(renderersWep, false);
									}
								}
								else
								{
									bool wait8 = true;
									InstantiateManager.Request(this, lo_wepn.loadedObject, delegate(InstantiateManager.InstantiateData data)
									{
										//IL_0007: Unknown result type (might be due to invalid IL or missing references)
										//IL_000c: Unknown result type (might be due to invalid IL or missing references)
										//IL_0011: Expected O, but got Unknown
										((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_313f: stateMachine*/)._003Cweapon_003E__118 = data.instantiatedObject.get_transform();
										((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_313f: stateMachine*/)._003C_003Ef__this.renderersWep = ((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_313f: stateMachine*/)._003Cweapon_003E__118.GetComponentsInChildren<Renderer>();
										SetRenderersEnabled(((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_313f: stateMachine*/)._003C_003Ef__this.renderersWep, false);
										((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_313f: stateMachine*/)._003Cwait_003E__108 = false;
									}, false);
									while (wait8)
									{
										yield return (object)null;
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
									yield return (object)this.StartCoroutine(ItemLoader.InitRoopEffect(load_queue, weapon, shader_type));
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
								Material materialR = null;
								Material materialL = null;
								if (renderersWep != null)
								{
									int m = 0;
									for (int l = renderersWep.Length; m < l; m++)
									{
										if (renderersWep[m].get_name().EndsWith("_L"))
										{
											materialL = renderersWep[m].get_material();
											wepL = renderersWep[m].get_transform();
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
											materialR = renderersWep[m].get_material();
											wepR = renderersWep[m].get_transform();
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
									ApplyWeaponHighResoTexs(lo_hr_wep_tex, high_reso_tex_flags, materialR, materialL);
								}
							}
							if (animator != null && lo_anim != null)
							{
								RuntimeAnimatorController anim_ctrl = lo_anim.loadedObjects[0].obj as RuntimeAnimatorController;
								if (anim_ctrl != null)
								{
									animator.set_runtimeAnimatorController(anim_ctrl);
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
										SP_ATTACK_TYPE spType = (SP_ATTACK_TYPE)info.weaponSpAttackType;
										if (spType != 0)
										{
											string tmpStr = spType.ToString();
											int plen = animator.get_parameterCount();
											for (int pidx = 0; pidx < plen; pidx++)
											{
												AnimatorControllerParameter ap = animator.GetParameter(pidx);
												if (ap.get_name() == tmpStr)
												{
													animator.SetTrigger(tmpStr);
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
										accTrans = loacc.Realizes(null, -1);
									}
									else
									{
										bool wait8 = true;
										InstantiateManager.Request(this, loacc.loadedObject, delegate(InstantiateManager.InstantiateData data)
										{
											//IL_0007: Unknown result type (might be due to invalid IL or missing references)
											//IL_000c: Unknown result type (might be due to invalid IL or missing references)
											//IL_0011: Expected O, but got Unknown
											((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_36ea: stateMachine*/)._003CaccTrans_003E__133 = data.instantiatedObject.get_transform();
											((_003CDoLoad_003Ec__Iterator26A)/*Error near IL_36ea: stateMachine*/)._003Cwait_003E__108 = false;
										}, false);
										while (wait8)
										{
											yield return (object)null;
										}
									}
									if (accTrans != null)
									{
										AccessoryTable.AccessoryInfoData ainfo = Singleton<AccessoryTable>.I.GetInfoData(info.accUIDs[aidx]);
										accTrans.SetParent(GetNodeTrans(ainfo.node));
										accTrans.set_localPosition(ainfo.offset);
										accTrans.set_localRotation(ainfo.rotation);
										accTrans.set_localScale(ainfo.scale);
										accessory.Add(accTrans);
										accRendererList.AddRange(accTrans.GetComponentsInChildren<Renderer>());
									}
								}
								renderersAccessory = accRendererList.ToArray();
								ModelLoaderBase.SetEnabled(renderersAccessory, false);
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
							SetRenderersEnabled(renderersWep, true);
							SetRenderersEnabled(renderersFace, true);
							SetRenderersEnabled(renderersHair, true);
							SetRenderersEnabled(renderersBody, true);
							SetRenderersEnabled(renderersHead, true);
							SetRenderersEnabled(renderersArm, true);
							SetRenderersEnabled(renderersLeg, true);
							SetRenderersEnabled(renderersAccessory, true);
							if (need_shadow && shadow == null)
							{
								shadow = CreateShadow(_this, true, -1, shader_type == SHADER_TYPE.LIGHTWEIGHT);
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
									player.packetReceiver.SetStopPacketUpdate(false);
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
								ResourceLoad loaded = player.get_gameObject().GetComponent<ResourceLoad>();
								if (loaded != null && loaded.list != null)
								{
									List<string> resourceNames = new List<string>();
									int j = 0;
									for (int i = loaded.list.size; j < i; j++)
									{
										resourceNames.Add(loaded.list.buffer[j].name);
									}
									resourceNames.Distinct();
									MonoBehaviourSingleton<ResourceManager>.I.cache.AddIgnoreCategorySpecifiedReleaseList(resourceNames);
								}
							}
							isLoading = false;
						}
					}
				}
			}
		}
	}

	private void AddSkillAttackInfoName(Player player, ref List<string> needAtkInfoNames)
	{
		int num = 3;
		for (int i = 0; i < num; i++)
		{
			SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(player.skillInfo.weaponOffset + i);
			if (skillParam != null)
			{
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
		}
		needAtkInfoNames.Add("sk_heal_atk_zone");
	}

	private void AddFieldGimmickAttackInfoName(ref List<string> needAtkInfoNames)
	{
		if (MonoBehaviourSingleton<FieldManager>.IsValid() && Singleton<FieldMapTable>.IsValid())
		{
			List<FieldMapTable.FieldGimmickPointTableData> fieldGimmickPointListByMapID = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
			if (fieldGimmickPointListByMapID != null)
			{
				for (int i = 0; i < fieldGimmickPointListByMapID.Count; i++)
				{
					switch (fieldGimmickPointListByMapID[i].gimmickType)
					{
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.BOMBROCK:
					{
						string item3 = "bombrock";
						needAtkInfoNames.Add(item3);
						break;
					}
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
					{
						string item2 = "cannonball_heavy";
						needAtkInfoNames.Add(item2);
						break;
					}
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_RAPID:
					{
						string item = "cannonball_rapid";
						needAtkInfoNames.Add(item);
						break;
					}
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_SPECIAL:
						needAtkInfoNames.Add(ResourceName.CANNONBALL_SPECIAL_ATTACK_INFO_NAME);
						break;
					case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_FIELD:
					{
						string attackInfoName = FieldGimmickCannonField.GetAttackInfoName(fieldGimmickPointListByMapID[i].value2);
						needAtkInfoNames.Add(attackInfoName);
						break;
					}
					}
				}
			}
		}
	}

	private IEnumerator LoadNextBulletInfo(LoadingQueue loadQueue, string infoName, List<AttackInfo> hitInfos)
	{
		if (!playerLoaderLoadedAttackInfoNames.Contains(infoName))
		{
			LoadObject loadObj = loadQueue.Load(RESOURCE_CATEGORY.PLAYER_ATTACK_INFO, infoName, false);
			playerLoaderLoadedAttackInfoNames.Add(infoName);
			if (loadQueue.IsLoading())
			{
				yield return (object)loadQueue.Wait();
			}
			GameObject infoObj = loadObj.Realizes(MonoBehaviourSingleton<InGameSettingsManager>.I._transform, -1).get_gameObject();
			SplitPlayerAttackInfo attackInfo = infoObj.GetComponent<SplitPlayerAttackInfo>();
			hitInfos.Add(attackInfo.attackHitInfo);
			if (!string.IsNullOrEmpty(attackInfo.attackHitInfo.nextBulletInfoName))
			{
				yield return (object)this.StartCoroutine(LoadNextBulletInfo(loadQueue, attackInfo.attackHitInfo.nextBulletInfoName, hitInfos));
			}
		}
	}

	private void InitWeaponLinkBuffEffect(Player player, Transform weaponTrans)
	{
		if (!(player == null))
		{
			EffectPlayProcessor componentInChildren = weaponTrans.GetComponentInChildren<EffectPlayProcessor>();
			if (!(componentInChildren == null))
			{
				EffectPlayProcessor.EffectSetting[] effectSettings = componentInChildren.effectSettings;
				if (effectSettings != null && effectSettings.Length > 0)
				{
					int num = effectSettings.Length;
					for (int i = 0; i < num; i++)
					{
						if (effectSettings[i].name.StartsWith("BUFF_LOOP_"))
						{
							player.RegisterWeaponLinkEffect(effectSettings[i]);
						}
					}
				}
			}
		}
	}

	public void DeleteLoadedObjects()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
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
		socketHead = null;
		socketWepL = null;
		socketWepR = null;
		socketFootL = null;
		socketFootR = null;
		socketHandL = null;
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
		if (eyeBlink)
		{
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
		if (!isEnable)
		{
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
	}

	public void SetDynamicBones(Transform body, Transform hair, bool isEnable)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		if (this.dynamicBones == null)
		{
			this.dynamicBones = new List<DynamicBone>();
		}
		List<DynamicBone> dynamicBones = this.dynamicBones;
		dynamicBones.Clear();
		hair.GetComponentsInChildren<DynamicBone>(true, dynamicBones);
		if (dynamicBones.Count > 0)
		{
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
	}

	public void ResetDynamicBones(List<DynamicBone> list)
	{
		if (list != null)
		{
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
	}

	private Transform AddSkin(LoadObject lo)
	{
		return AddSkin(lo, renderersBody[0] as SkinnedMeshRenderer, -1);
	}

	private Transform AddSkin(Transform base_model)
	{
		return AddSkin(base_model, renderersBody[0] as SkinnedMeshRenderer, -1);
	}

	public static Transform AddSkin(LoadObject lo, SkinnedMeshRenderer body_skin_renderer, int layer = -1)
	{
		if (lo.loadedObject == null)
		{
			return null;
		}
		Transform base_model = lo.Realizes(null, -1);
		lo.loadedObject = null;
		return AddSkin(base_model, body_skin_renderer, layer);
	}

	public static Transform AddSkin(Transform base_model, SkinnedMeshRenderer body_skin_renderer, int layer = -1)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
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
			Transform transform = body_skin_renderer.get_rootBone();
			if (componentInChildren.get_rootBone() != null)
			{
				val3.set_rootBone(Utility.Find(transform, componentInChildren.get_rootBone().get_name()));
				EffectPlayProcessor component = base_model.GetComponent<EffectPlayProcessor>();
				if (component != null)
				{
					EffectPlayProcessor effectPlayProcessor = val3.get_rootBone().get_gameObject().AddComponent<EffectPlayProcessor>();
					effectPlayProcessor.effectSettings = component.effectSettings;
					List<Transform> list = effectPlayProcessor.PlayEffect("InitRoop", null);
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
				array[j] = Utility.Find(transform, bones[j].get_name());
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
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		if (level != 0 && !(body_skin_renderer == null))
		{
			Color32[] colors = body_skin_renderer.get_sharedMesh().get_colors32();
			if (colors.Length != 0)
			{
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
		}
	}

	public void ChangeFace(FACE_ID id)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
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
		if (!renderers.IsNullOrEmpty())
		{
			int i = 0;
			for (int num = renderers.Length; i < num; i++)
			{
				if (renderers[i] != null)
				{
					renderers[i].set_enabled(is_enabled);
				}
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		transform.get_gameObject().set_layer(layer);
		foreach (Transform item in transform)
		{
			Transform transform2 = item;
			SetLayerWithChildren_SecondaryNoChange(transform2, layer);
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
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
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
		return load_queue.Load(RESOURCE_CATEGORY.PLAYER_HIGH_RESO_TEX, name, highResoTexNames, false);
	}

	public static void ApplyWeaponHighResoTexs(LoadObject lo_high_reso_texs, int flags, Material mate0, Material mate1)
	{
		if (lo_high_reso_texs != null && flags != 0 && (!(mate0 == null) || !(mate1 == null)))
		{
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
						goto default;
					case 4:
						flag = true;
						goto default;
					case 16:
						flag2 = true;
						goto default;
					default:
					{
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
						goto IL_0127;
					}
					case 2:
					case 8:
					case 32:
						break;
					}
					continue;
				}
				goto IL_0127;
				IL_0127:
				num <<= 1;
			}
		}
	}

	public static void ApplyEquipHighResoTexs(LoadObject lo, Renderer[] renderers)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		if (lo != null && lo.loadedObjects != null && lo.loadedObjects.Length > 0 && renderers != null)
		{
			Texture val = lo.loadedObjects[0].obj as Texture;
			if (!(val == null))
			{
				int num = 0;
				if (num < renderers.Length)
				{
					renderers[num].get_material().SetTexture(Shader.PropertyToID("_MainTex"), val);
				}
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
		}, false);
	}

	public static void ApplyHairOverlay(LoadObject lo, Renderer[] renderers)
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		if (lo != null && lo.loadedObjects != null && lo.loadedObjects.Length == 2 && renderers != null)
		{
			Shader val = ResourceUtility.FindShader("mobile/Custom/Character/character_matcap_overlay");
			if (!(val == null))
			{
				Texture val2 = lo.loadedObjects[0].obj as Texture;
				if (!(val2 == null))
				{
					Texture val3 = lo.loadedObjects[1].obj as Texture;
					if (!(val3 == null))
					{
						int num = Shader.PropertyToID("_MainTex");
						int num2 = Shader.PropertyToID("_MatCap");
						int num3 = 0;
						if (num3 < renderers.Length)
						{
							renderers[num3].get_material().set_shader(val);
							renderers[num3].get_material().SetTexture(num, val2);
							renderers[num3].get_material().SetTexture(num2, val3);
						}
					}
				}
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
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (!accessory.IsNullOrEmpty())
		{
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
	}
}
