// Decompiled with JetBrains decompiler
// Type: LightweightPlayerLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
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
    get => false;
    set
    {
    }
  }

  public override int GetVoiceId(ACTION_VOICE_ID voice_type) => 0;

  public override int GetVoiceId(ACTION_VOICE_EX_ID voice_type) => 0;

  protected override void UpdateVoiceAudioClipIds() => this.voiceAudioClipIds = (int[]) null;

  public override AudioClip GetVoiceAudioClip(int voice_id) => (AudioClip) null;

  protected override int RandomizeAttackVoice(int voice_id) => voice_id;

  protected override void LoadAttackInfoResource(Player player, LoadingQueue loadQueue)
  {
  }

  public override void StartLoad(
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

  protected override IEnumerator DoLoad(
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
    this.animObjectTable = new StringKeyTable<LoadObject>();
    Player player = ((Component) this).gameObject.GetComponent<Player>();
    if (Object.op_Inequality((Object) player, (Object) null))
    {
      int id = player.id;
    }
    this.DeleteLoadedObjects();
    this.loadInfo = info;
    if (anim_id < 0)
      anim_id = anim_id != -1 || info.weaponModelID == -1 ? -anim_id + info.weaponModelID / 1000 : info.weaponModelID / 1000;
    string playerBody = info.bodyModelID > -1 ? ResourceName.GetPlayerBody(92000) : (string) null;
    if (playerBody != null)
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
      LoadObject lo_body = !need_dev_frame_instantiate ? (playerBody != null ? load_queue.Load(RESOURCE_CATEGORY.PLAYER_BDY, playerBody) : (LoadObject) null) : (playerBody != null ? (LoadObject) load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.PLAYER_BDY, playerBody) : (LoadObject) null);
      if (anim_id > -1)
        ResourceName.GetPlayerAnim(anim_id);
      if (load_queue.IsLoading())
        yield return (object) load_queue.Wait();
      yield return (object) load_queue.Wait();
      if (load_queue.IsLoading())
        yield return (object) load_queue.Wait();
      this.body = lo_body.Realizes(_this);
      this.renderersBody = ((Component) this.body).gameObject.GetComponentsInChildren<Renderer>();
      ModelLoaderBase.SetEnabled(this.renderersBody, false);
      if (!Object.op_Equality((Object) this.body, (Object) null))
      {
        this.animator = ((Component) this.body).GetComponentInChildren<Animator>();
        if (Object.op_Inequality((Object) player, (Object) null))
          player.body = this.body;
        if (Object.op_Inequality((Object) player, (Object) null))
        {
          if (Object.op_Inequality((Object) player.controller, (Object) null))
            ((Behaviour) player.controller).enabled = true;
          player.OnLoadComplete();
          if (Object.op_Inequality((Object) player.packetReceiver, (Object) null))
            player.packetReceiver.SetStopPacketUpdate(false);
        }
        if (callback != null)
          callback((object) player);
        this.isLoading = false;
      }
    }
  }

  public new void DeleteLoadedObjects()
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
    this.socketForearmL = (Transform) null;
    this.socketForearmR = (Transform) null;
    this.validFaceChange = false;
    this.eyeBlink = false;
    this.isLoading = false;
  }

  private void Update()
  {
  }

  public override void ChangeFace(PlayerLoader.FACE_ID id)
  {
  }
}
