using System.Collections;
using UnityEngine;

public class LightweightPlayerLoader : PlayerLoader
{
	private static readonly int[] ATK_VOICE_S = new int[0];

	private static readonly int[] ATK_VOICE_M = new int[0];

	private static readonly int[] ATK_VOICE_L = new int[0];

	private static readonly int[] DAMAGE_VOICES = new int[0];

	private static readonly int[] DEATH_VOICES = new int[0];

	private static readonly int[] HAPPY_VOICES = new int[0];

	public override bool eyeBlink
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public override int GetVoiceId(ACTION_VOICE_ID voice_type)
	{
		return 0;
	}

	public override int GetVoiceId(ACTION_VOICE_EX_ID voice_type)
	{
		return 0;
	}

	protected override void UpdateVoiceAudioClipIds()
	{
		voiceAudioClipIds = null;
	}

	public override AudioClip GetVoiceAudioClip(int voice_id)
	{
		return null;
	}

	protected override int RandomizeAttackVoice(int voice_id)
	{
		return voice_id;
	}

	protected override void LoadAttackInfoResource(Player player, LoadingQueue loadQueue)
	{
	}

	public override void StartLoad(PlayerLoadInfo player_load_info, int layer, int anim_id, bool need_anim_event, bool need_foot_stamp, bool need_shadow, bool enable_light_probes, bool need_action_voice, bool need_high_reso_tex, bool need_res_ref_count, bool need_dev_frame_instantiate, SHADER_TYPE shader_type, OnCompleteLoad callback, bool enable_eye_blick = true, int use_hair_overlay = -1)
	{
		if (base.isLoading)
		{
			Log.Error(LOG.RESOURCE, this.get_name() + " now loading.");
		}
		else
		{
			this.StartCoroutine(DoLoad(player_load_info, layer, anim_id, need_anim_event, need_foot_stamp, need_shadow, enable_light_probes, need_action_voice, need_high_reso_tex, need_res_ref_count, need_dev_frame_instantiate, shader_type, callback, enable_eye_blick, use_hair_overlay));
		}
	}

	protected override IEnumerator DoLoad(PlayerLoadInfo info, int layer, int anim_id, bool need_anim_event, bool need_foot_stamp, bool need_shadow, bool enable_light_probes, bool need_action_voice, bool need_high_reso_tex, bool need_res_ref_count, bool need_dev_frame_instantiate, SHADER_TYPE shader_type, OnCompleteLoad callback, bool enable_eye_blick, int use_hair_overlay)
	{
		if (info == null)
		{
			Log.Error(LOG.RESOURCE, "PlayerLoader:info=null");
		}
		base.animObjectTable = new StringKeyTable<LoadObject>();
		Player player = this.get_gameObject().GetComponent<Player>();
		if (player != null)
		{
			int id = player.id;
		}
		DeleteLoadedObjects();
		base.loadInfo = info;
		if (anim_id < 0)
		{
			anim_id = ((anim_id != -1 || info.weaponModelID == -1) ? (-anim_id + info.weaponModelID / 1000) : (info.weaponModelID / 1000));
		}
		string body_name = (info.bodyModelID <= -1) ? null : ResourceName.GetPlayerBody(92000);
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
		base.isLoading = true;
		LoadingQueue load_queue = new LoadingQueue(this, need_res_ref_count);
		LoadObject lo_body = (!need_dev_frame_instantiate) ? ((body_name == null) ? null : load_queue.Load(RESOURCE_CATEGORY.PLAYER_BDY, body_name)) : ((body_name == null) ? null : load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_BDY, body_name));
		if (anim_id > -1)
		{
			ResourceName.GetPlayerAnim(anim_id);
		}
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
		}
		yield return load_queue.Wait();
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
		}
		base.body = lo_body.Realizes(_this);
		base.renderersBody = base.body.get_gameObject().GetComponentsInChildren<Renderer>();
		ModelLoaderBase.SetEnabled(base.renderersBody, is_enable: false);
		if (base.body == null)
		{
			yield break;
		}
		base.animator = base.body.GetComponentInChildren<Animator>();
		if (player != null)
		{
			player.body = base.body;
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
		callback?.Invoke(player);
		base.isLoading = false;
	}

	public new void DeleteLoadedObjects()
	{
		if (base.wepR != null)
		{
			Object.DestroyImmediate(base.wepR.get_gameObject());
		}
		if (base.wepL != null)
		{
			Object.DestroyImmediate(base.wepL.get_gameObject());
		}
		if (base.body != null)
		{
			Object.DestroyImmediate(base.body.get_gameObject());
		}
		if (base.shadow != null)
		{
			Object.DestroyImmediate(base.shadow.get_gameObject());
		}
		base.loadInfo = null;
		base.wepR = null;
		base.wepL = null;
		base.body = null;
		base.face = null;
		base.hair = null;
		base.head = null;
		base.arm = null;
		base.leg = null;
		accessory.Clear();
		base.shadow = null;
		base.animator = null;
		base.hairPhysics = null;
		base.renderersWep = null;
		base.renderersFace = null;
		base.renderersHair = null;
		base.renderersBody = null;
		base.renderersHead = null;
		base.renderersArm = null;
		base.renderersLeg = null;
		base.renderersAccessory = null;
		base.socketRoot = null;
		base.socketHead = null;
		base.socketWepL = null;
		base.socketWepR = null;
		base.socketFootL = null;
		base.socketFootR = null;
		base.socketHandL = null;
		base.socketForearmL = null;
		base.socketForearmR = null;
		validFaceChange = false;
		eyeBlink = false;
		base.isLoading = false;
	}

	private void Update()
	{
	}

	public override void ChangeFace(FACE_ID id)
	{
	}
}
