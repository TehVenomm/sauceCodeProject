// Decompiled with JetBrains decompiler
// Type: SoundManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

#nullable disable
public class SoundManager : MonoBehaviourSingleton<SoundManager>
{
  private int m_requestBGMID;
  private bool m_IsNextBGMLoop = true;
  public float volumeSE = 1f;
  public float volumeBGM = 1f;
  public float volumeVOICE = 1f;
  public float fadeOutTime;
  public const float BGM_FADEOUT_TIME = 1f;
  private AudioMixerSnapshot mixerCurrent;
  private AudioMixerSnapshot mixerSnapshotDefault;
  private const string DEFAULT_SNAPSHOT_NAME = "Default";
  private const string MIXER_PATH_MAIN = "Audio/MainMixer";
  private const string PARAM_LABEL_VOLUME_MASTER = "MasterVolume";
  private const string PARAM_LABEL_VOLUME_BGM = "ConfigBGMVolume";
  private const string PARAM_LABEL_VOLUME_SE = "ConfigSEVolume";
  private const float DEFAULT_MIN_DISTANCE = 15f;
  private const float DEFAULT_MAX_DISTANCE = 60f;
  private const uint VOICE_CH_MAX = 6;
  private AudioControlGroup[] audioControllVoice;
  public const uint VOICE_CH_DEFAULT = 0;
  public const uint VOICE_CH_ENEMY = 1;
  private const uint VOICE_CH_PLAYER_HEAD = 2;
  public Dictionary<int, uint> m_dicReservedVoiceChannel;
  private bool m_IsLoading;
  private bool m_IsLoaded;
  public UIntKeyTable<AudioClip> m_SystemSEClips;

  public SoundManager.AudioPreset CurrentPreset { get; private set; }

  public int requestBGMID
  {
    get => this.m_requestBGMID;
    set
    {
      this.m_requestBGMID = value;
      this.m_IsNextBGMLoop = true;
    }
  }

  public static void RequestBGM(int bgmId, bool isLoop = true)
  {
    if (!MonoBehaviourSingleton<SoundManager>.IsValid())
      return;
    MonoBehaviourSingleton<SoundManager>.I.m_requestBGMID = bgmId;
    MonoBehaviourSingleton<SoundManager>.I.m_IsNextBGMLoop = isLoop;
  }

  public int playingBGMID { get; private set; }

  public bool changingBGM { get; private set; }

  public AudioSource audioSourceBGM { get; private set; }

  public AudioMixer audioMixer { get; private set; }

  public AudioMixerGroup mixerGroupMaster { get; private set; }

  public AudioMixerGroup mixerGroupBGM { get; private set; }

  public AudioMixerGroup mixerGroupSE { get; private set; }

  public AudioMixerGroup mixerGroupUISE { get; private set; }

  public AudioMixerGroup mixerGroupVoice { get; private set; }

  public AudioMixerGroup mixerGroupJingle { get; private set; }

  public AudioMixerGroup mixerGroupFX { get; private set; }

  public AudioMixerGroup mixerConfigBGM { get; private set; }

  public AudioMixerGroup mixerConfigSE { get; private set; }

  public bool IsActiveSoundEffect { get; private set; }

  public AudioControlGroup audioControlSESelf { get; private set; }

  public AudioControlGroup audioControlJingle { get; private set; }

  protected override void Awake()
  {
    base.Awake();
    this.LoadAudioMixer();
    this.SetAudioMixer(this.audioMixer);
    this.SetupAudioControlGroup();
    this.mixerCurrent = this.mixerSnapshotDefault;
    this.TransitionPreset();
    this.IsActiveSoundEffect = true;
  }

  private void LoadAudioMixer() => this.audioMixer = Resources.Load<AudioMixer>("Audio/MainMixer");

  public void TransitionPreset(uint presetId = 0)
  {
    string str = "Default";
    float _minDistance = 15f;
    float _maxDistance = 60f;
    if (presetId > 0U && Singleton<AudioSettingTable>.IsValid())
    {
      AudioSettingTable.Data data = Singleton<AudioSettingTable>.I.GetData(presetId);
      if (data != null)
      {
        str = data.name;
        _minDistance = data.minDistance;
        _maxDistance = data.maxDistance;
      }
    }
    this.CurrentPreset = new SoundManager.AudioPreset(str, _minDistance, _maxDistance);
    this.TransitionTo(str);
  }

  public void TransitionTo(string snapshotName, float transitionTime = 1f)
  {
    if (Object.op_Equality((Object) this.audioMixer, (Object) null))
      return;
    AudioMixerSnapshot snapshot = this.audioMixer.FindSnapshot(snapshotName);
    if (Object.op_Equality((Object) snapshot, (Object) null) || Object.op_Equality((Object) this.mixerCurrent, (Object) snapshot))
      return;
    snapshot.TransitionTo(transitionTime);
    this.mixerCurrent = snapshot;
  }

  private void TransitionTo(
    AudioMixerSnapshot current,
    AudioMixerSnapshot next,
    float transitionTime = 1f)
  {
    if (Object.op_Equality((Object) current, (Object) null) || Object.op_Equality((Object) next, (Object) null) || Object.op_Equality((Object) this.audioMixer, (Object) null))
      return;
    this.audioMixer.TransitionToSnapshots(new AudioMixerSnapshot[2]
    {
      current,
      next
    }, new float[2]{ 0.0f, 1f }, transitionTime);
  }

  public void SetAudioMixer(AudioMixer audio_mixer)
  {
    this.audioMixer = audio_mixer;
    if (Object.op_Inequality((Object) audio_mixer, (Object) null))
    {
      this.mixerGroupMaster = this.GetAudioMixerGroup("Master");
      this.mixerGroupBGM = this.GetAudioMixerGroup("Master/CONFIG_BGM/BGM");
      this.mixerGroupFX = this.GetAudioMixerGroup("Master/CONFIG_SE/FX");
      this.mixerGroupSE = this.GetAudioMixerGroup("Master/CONFIG_SE/FX/SE");
      this.mixerGroupUISE = this.GetAudioMixerGroup("Master/CONFIG_SE/UISE");
      this.mixerGroupVoice = this.GetAudioMixerGroup("Master/CONFIG_SE/FX/Voice");
      this.mixerGroupJingle = this.GetAudioMixerGroup("Master/CONFIG_SE/Jingle");
      this.mixerConfigBGM = this.GetAudioMixerGroup("Master/CONFIG_BGM");
      this.mixerConfigSE = this.GetAudioMixerGroup("Master/CONFIG_SE");
      this.mixerSnapshotDefault = this.audioMixer.FindSnapshot("Default");
    }
    else
    {
      this.mixerGroupMaster = (AudioMixerGroup) null;
      this.mixerGroupFX = (AudioMixerGroup) null;
      this.mixerGroupBGM = (AudioMixerGroup) null;
      this.mixerGroupSE = (AudioMixerGroup) null;
      this.mixerGroupUISE = (AudioMixerGroup) null;
      this.mixerGroupVoice = (AudioMixerGroup) null;
      this.mixerGroupJingle = (AudioMixerGroup) null;
      this.mixerCurrent = (AudioMixerSnapshot) null;
      this.mixerSnapshotDefault = (AudioMixerSnapshot) null;
    }
    this.UpdateConfigVolume();
  }

  private AudioMixerGroup GetAudioMixerGroup(string path)
  {
    AudioMixerGroup[] matchingGroups = this.audioMixer.FindMatchingGroups(path);
    return matchingGroups.Length != 0 ? matchingGroups[0] : (AudioMixerGroup) null;
  }

  public void SetupAudioControlGroup()
  {
    if (Object.op_Equality((Object) this.audioControlSESelf, (Object) null))
      this.audioControlSESelf = AudioControlGroup.Create();
    if (Object.op_Equality((Object) this.audioControlJingle, (Object) null))
      this.audioControlJingle = AudioControlGroup.Create();
    if (this.audioControllVoice != null)
      return;
    this.audioControllVoice = new AudioControlGroup[6];
    for (int index = 0; index < 6; ++index)
      this.audioControllVoice[index] = AudioControlGroup.Create(AudioControlGroup.CullingTypes.OVERWRITE, 1);
  }

  private IEnumerator Start()
  {
    while (true)
    {
      do
      {
        yield return (object) null;
      }
      while (this.playingBGMID == this.requestBGMID);
      this.changingBGM = true;
      this.playingBGMID = this.requestBGMID;
      if (Object.op_Inequality((Object) this.audioSourceBGM, (Object) null) && this.audioSourceBGM.isPlaying)
      {
        bool is_play_fadeout = true;
        EventDelegate.Callback OnFinishedCallBack = (EventDelegate.Callback) (() => is_play_fadeout = false);
        TweenVolume fadeout = TweenVolume.Begin(((Component) this).gameObject, this.fadeOutTime, 0.0f);
        EventDelegate.Add(fadeout.onFinished, OnFinishedCallBack);
        while (is_play_fadeout)
        {
          this.audioSourceBGM.volume = fadeout.value;
          yield return (object) null;
        }
        EventDelegate.Remove(fadeout.onFinished, OnFinishedCallBack);
        OnFinishedCallBack = (EventDelegate.Callback) null;
        fadeout = (TweenVolume) null;
      }
      LoadObject lo_bgm = (LoadObject) null;
      if (this.playingBGMID != 0)
      {
        ResourceManager.enableCache = false;
        lo_bgm = new LoadObject((MonoBehaviour) this, RESOURCE_CATEGORY.SOUND_BGM, ResourceName.GetBGM(this.requestBGMID));
        ResourceManager.enableCache = true;
      }
      if (lo_bgm != null)
      {
        if (lo_bgm.isLoading)
          yield return (object) lo_bgm.Wait((MonoBehaviour) this);
        if (Object.op_Inequality(lo_bgm.loadedObject, (Object) null))
        {
          if (Object.op_Equality((Object) this.audioSourceBGM, (Object) null))
            this.audioSourceBGM = ((Component) this).gameObject.AddComponent<AudioSource>();
          if (Object.op_Inequality((Object) this.audioSourceBGM, (Object) null))
          {
            this.audioSourceBGM.priority = 0;
            this.audioSourceBGM.reverbZoneMix = 0.0f;
            this.audioSourceBGM.spread = 360f;
            this.audioSourceBGM.spatialBlend = 0.0f;
            this.audioSourceBGM.outputAudioMixerGroup = this.mixerGroupBGM;
            this.audioSourceBGM.loop = this.m_IsNextBGMLoop;
            this.audioSourceBGM.Stop();
            this.audioSourceBGM.clip = (AudioClip) null;
            ((Behaviour) this.audioSourceBGM).enabled = true;
            this.audioSourceBGM.clip = lo_bgm.loadedObject as AudioClip;
            this.audioSourceBGM.volume = this.volumeBGM;
            this.audioSourceBGM.Play(3UL);
          }
        }
        else
          this.playingBGMID = this.requestBGMID = 0;
        lo_bgm = (LoadObject) null;
      }
      if (this.playingBGMID == 0 && Object.op_Inequality((Object) this.audioSourceBGM, (Object) null))
        this.audioSourceBGM.Stop();
      this.changingBGM = false;
      lo_bgm = (LoadObject) null;
    }
  }

  public void UpdateConfigVolume()
  {
    if (!Object.op_Inequality((Object) this.audioMixer, (Object) null) || GameSaveData.instance == null)
      return;
    this.ApplyVolume("ConfigBGMVolume", GameSaveData.instance.volumeBGM);
    this.ApplyVolume("ConfigSEVolume", GameSaveData.instance.volumeSE);
  }

  private void ApplyVolume(string volumeLabel, float configValue = 1f)
  {
    if (Object.op_Equality((Object) this.audioMixer, (Object) null))
      return;
    float decibel = Utility.VolumeToDecibel(configValue);
    this.audioMixer.SetFloat(volumeLabel, decibel);
  }

  public static AudioObject PlayUISE(
    AudioClip clip,
    float volume,
    bool loop,
    Transform parent,
    int config_id = 0)
  {
    if (!MonoBehaviourSingleton<SoundManager>.IsValid())
      return (AudioObject) null;
    float volume1 = volume * MonoBehaviourSingleton<SoundManager>.I.volumeSE;
    return MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.CreateAudio(clip, config_id, volume1, loop, MonoBehaviourSingleton<SoundManager>.I.mixerGroupUISE, _parent: parent);
  }

  public static void PlaySystemSE(SoundID.UISE SEType, float volume = 1f)
  {
    if (MonoBehaviourSingleton<SoundManager>.I.m_SystemSEClips == null)
      return;
    uint num = (uint) SEType;
    AudioClip clip = MonoBehaviourSingleton<SoundManager>.I.m_SystemSEClips.Get(num);
    if (Object.op_Equality((Object) clip, (Object) null))
      return;
    SoundManager.PlayUISE(clip, volume, false, (Transform) null, (int) num);
  }

  public static AudioObject PlaySE(AudioClip clip, bool loop, Transform parent)
  {
    if (!MonoBehaviourSingleton<SoundManager>.IsValid())
      return (AudioObject) null;
    if (Object.op_Equality((Object) clip, (Object) null))
      return (AudioObject) null;
    string s = string.IsNullOrEmpty(((Object) clip).name) ? string.Empty : ((Object) clip).name.Substring(3);
    int clip_id = 0;
    ref int local = ref clip_id;
    if (!int.TryParse(s, out local))
      return AudioObject.Create(clip, 0, MonoBehaviourSingleton<SoundManager>.I.volumeSE, loop, MonoBehaviourSingleton<SoundManager>.I.mixerGroupSE, MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf, true, parent: parent);
    Vector3 position = parent.position;
    return MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.CreateAudio(clip, clip_id, MonoBehaviourSingleton<SoundManager>.I.volumeSE, loop, MonoBehaviourSingleton<SoundManager>.I.mixerGroupSE, true, initPos: new Vector3?(position));
  }

  public static void PlayOneShotSE(int se_id, Vector3 pos)
  {
    if (!MonoBehaviourSingleton<SoundManager>.IsValid())
      return;
    MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.CreateAudio(SoundManager.GetSEAudioClip(se_id), se_id, MonoBehaviourSingleton<SoundManager>.I.volumeSE, false, MonoBehaviourSingleton<SoundManager>.I.mixerGroupSE, true, initPos: new Vector3?(pos));
  }

  public static void StopSEAll(int fadeout_framecount = 0)
  {
    if (!MonoBehaviourSingleton<SoundManager>.IsValid())
      return;
    MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.StopAll(fadeout_framecount);
  }

  public static void StopAudioObjectAll()
  {
    if (!MonoBehaviourSingleton<AudioObjectPool>.IsValid())
      return;
    AudioObjectPool.StopAll();
  }

  public static void PlayOneShotUISE(int se_id)
  {
    if (!MonoBehaviourSingleton<SoundManager>.IsValid())
      return;
    MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.CreateAudio(SoundManager.GetSEAudioClip(se_id), se_id, MonoBehaviourSingleton<SoundManager>.I.volumeSE, false, MonoBehaviourSingleton<SoundManager>.I.mixerGroupUISE);
  }

  public static void PlayOneShotUISE(AudioClip clip, int se_id)
  {
    if (!MonoBehaviourSingleton<SoundManager>.IsValid() || Object.op_Equality((Object) clip, (Object) null))
      return;
    MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.CreateAudio(clip, se_id, MonoBehaviourSingleton<SoundManager>.I.volumeSE, false, MonoBehaviourSingleton<SoundManager>.I.mixerGroupUISE);
  }

  public static AudioObject PlayUISE(int se_id)
  {
    return !MonoBehaviourSingleton<SoundManager>.IsValid() ? (AudioObject) null : MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.CreateAudio(SoundManager.GetSEAudioClip(se_id), se_id, MonoBehaviourSingleton<SoundManager>.I.volumeSE, false, MonoBehaviourSingleton<SoundManager>.I.mixerGroupUISE);
  }

  public static void PlayOneShotSE(int se_id, DisableNotifyMonoBehaviour master = null, Transform parent = null)
  {
    if (!MonoBehaviourSingleton<SoundManager>.IsValid())
      return;
    MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.CreateAudio(SoundManager.GetSEAudioClip(se_id), se_id, MonoBehaviourSingleton<SoundManager>.I.volumeSE, false, MonoBehaviourSingleton<SoundManager>.I.mixerGroupSE, true, master, parent);
  }

  public static void SetMuteVoice(bool isMute)
  {
    if (!MonoBehaviourSingleton<SoundManager>.IsValid())
      return;
    MonoBehaviourSingleton<SoundManager>.I.volumeVOICE = isMute ? 0.0f : 1f;
  }

  public static void PlayVoice(
    int voice_id,
    float volume = 1f,
    uint ch_id = 0,
    DisableNotifyMonoBehaviour master = null,
    Transform parent = null)
  {
    SoundManager.PlayVoice(SoundManager.GetStoryVoiceAudioClip(voice_id), voice_id, volume, ch_id, master, parent);
  }

  public static void PlayActionVoice(
    int voice_id,
    float volume = 1f,
    uint ch_id = 0,
    DisableNotifyMonoBehaviour master = null,
    Transform parent = null)
  {
    SoundManager.PlayVoice(SoundManager.GetActionVoiceAudioClip(voice_id), voice_id, volume, ch_id, master, parent);
  }

  public static void StopVoice(uint ch_id = 0, int fadeout_frame = 2)
  {
    AudioControlGroup audioControlVoice = MonoBehaviourSingleton<SoundManager>.I.GetAudioControlVoice(ch_id);
    if (Object.op_Equality((Object) audioControlVoice, (Object) null))
      return;
    audioControlVoice.StopAll(fadeout_frame);
  }

  public static void PlayVoice(
    AudioClip audio_clip,
    int voice_id,
    float volume = 1f,
    uint ch_id = 0,
    DisableNotifyMonoBehaviour master = null,
    Transform parent = null)
  {
    if (GameSaveData.instance.voiceOption == 2 || !MonoBehaviourSingleton<SoundManager>.IsValid() || Object.op_Equality((Object) audio_clip, (Object) null))
      return;
    float volume1 = volume * MonoBehaviourSingleton<SoundManager>.I.volumeVOICE;
    if ((double) volume1 < 0.05000000074505806)
      return;
    bool is3DSound = !Object.op_Equality((Object) parent, (Object) null);
    AudioControlGroup audioControlVoice = MonoBehaviourSingleton<SoundManager>.I.GetAudioControlVoice(ch_id);
    if (Object.op_Equality((Object) audioControlVoice, (Object) null))
      return;
    audioControlVoice.CreateAudio(audio_clip, voice_id, volume1, false, MonoBehaviourSingleton<SoundManager>.I.mixerGroupVoice, is3DSound, master, parent);
  }

  public AudioControlGroup GetAudioControlVoice(uint ch_id = 0)
  {
    if (this.audioControllVoice == null)
      return (AudioControlGroup) null;
    return ch_id >= 6U || (long) ch_id >= (long) this.audioControllVoice.Length ? (AudioControlGroup) null : this.audioControllVoice[(int) ch_id];
  }

  public static void PlayOneshotJingle(
    int se_id,
    DisableNotifyMonoBehaviour master = null,
    Transform parent = null)
  {
    if (!MonoBehaviourSingleton<SoundManager>.IsValid())
      return;
    MonoBehaviourSingleton<SoundManager>.I.audioControlJingle.CreateAudio(SoundManager.GetSEAudioClip(se_id), se_id, MonoBehaviourSingleton<SoundManager>.I.volumeSE, false, MonoBehaviourSingleton<SoundManager>.I.mixerGroupJingle, master: master, _parent: parent);
  }

  public static void PlayOneshotJingle(
    AudioClip clip,
    int se_id,
    DisableNotifyMonoBehaviour master = null,
    Transform parent = null)
  {
    if (!MonoBehaviourSingleton<SoundManager>.IsValid())
      return;
    MonoBehaviourSingleton<SoundManager>.I.audioControlJingle.CreateAudio(clip, se_id, MonoBehaviourSingleton<SoundManager>.I.volumeSE, false, MonoBehaviourSingleton<SoundManager>.I.mixerGroupJingle, master: master, _parent: parent);
  }

  public static void PlayLoopSE(int se_id, DisableNotifyMonoBehaviour master, Transform parent = null)
  {
    if (!MonoBehaviourSingleton<SoundManager>.IsValid())
      return;
    bool is3DSound = !Object.op_Equality((Object) parent, (Object) null);
    MonoBehaviourSingleton<SoundManager>.I.audioControlSESelf.CreateAudio(SoundManager.GetSEAudioClip(se_id), se_id, MonoBehaviourSingleton<SoundManager>.I.volumeSE, true, MonoBehaviourSingleton<SoundManager>.I.mixerGroupSE, is3DSound, master, parent);
  }

  public static void StopLoopSE(int se_id, DisableNotifyMonoBehaviour master)
  {
    if (!MonoBehaviourSingleton<SoundManager>.IsValid() || master.notifyServants == null)
      return;
    List<DisableNotifyMonoBehaviour>.Enumerator enumerator = master.notifyServants.GetEnumerator();
    while (enumerator.MoveNext())
    {
      AudioObject current = enumerator.Current as AudioObject;
      if (Object.op_Inequality((Object) current, (Object) null) && current.clipId == se_id)
      {
        current.Stop();
        break;
      }
    }
  }

  public static void LoopOff(int se_id, DisableNotifyMonoBehaviour master)
  {
    if (!MonoBehaviourSingleton<SoundManager>.IsValid() || master.notifyServants == null)
      return;
    List<DisableNotifyMonoBehaviour>.Enumerator enumerator = master.notifyServants.GetEnumerator();
    while (enumerator.MoveNext())
    {
      AudioObject current = enumerator.Current as AudioObject;
      if (Object.op_Inequality((Object) current, (Object) null) && current.clipId == se_id && current.GetLoopFlag())
      {
        current.SetLoopFlag(false);
        break;
      }
    }
  }

  public static AudioClip GetSEAudioClip(int se_id)
  {
    return SoundManager.GetSEAudioClip(ResourceName.GetSE(se_id));
  }

  public static AudioClip GetSEAudioClip(string name)
  {
    return SoundManager.GetAudioClip(RESOURCE_CATEGORY.SOUND_SE, name.Substring(0, 5), name);
  }

  private static AudioClip GetAudioClip(RESOURCE_CATEGORY category, string name)
  {
    return (AudioClip) MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedObject(category, name);
  }

  private static AudioClip GetAudioClip(
    RESOURCE_CATEGORY category,
    string package_name,
    string name)
  {
    return (AudioClip) MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedObject(category, package_name, name);
  }

  private static AudioClip GetActionVoiceAudioClip(int voice_id)
  {
    return (AudioClip) MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedObject(RESOURCE_CATEGORY.SOUND_VOICE, ResourceName.GetActionVoicePackageNameFromVoiceID(voice_id), ResourceName.GetActionVoiceName(voice_id));
  }

  public static AudioClip GetStoryVoiceAudioClip(int voice_id)
  {
    return (AudioClip) MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedObject(RESOURCE_CATEGORY.SOUND_VOICE, ResourceName.GetStoryVoicePackageNameFromVoiceID(voice_id), ResourceName.GetStoryVoiceName(voice_id));
  }

  public uint GetVoiceChannel(StageObject stageObject)
  {
    if (Object.op_Equality((Object) stageObject, (Object) null))
      return 0;
    switch (stageObject)
    {
      case Enemy _:
        return 1;
      case Self _:
        return 2;
      default:
        Player player = stageObject as Player;
        return Object.op_Inequality((Object) player, (Object) null) ? this.GetReservedChannel(player) : 0U;
    }
  }

  public void OnDetachedObject(StageObject stageObject)
  {
    if (Object.op_Equality((Object) stageObject, (Object) null))
      return;
    Player player = stageObject as Player;
    if (!Object.op_Inequality((Object) player, (Object) null))
      return;
    this.ChancelResavationChannel(player);
  }

  private uint GetReservedChannel(Player player)
  {
    if (Object.op_Equality((Object) player, (Object) null))
      return 0;
    if (player is Self)
      return 2;
    if (this.m_dicReservedVoiceChannel == null)
      this.m_dicReservedVoiceChannel = new Dictionary<int, uint>();
    if (this.m_dicReservedVoiceChannel.ContainsKey(player.id))
      return this.m_dicReservedVoiceChannel[player.id];
    uint num = (uint) (3 + this.m_dicReservedVoiceChannel.Keys.Count);
    if (num >= 6U)
      num = 0U;
    this.m_dicReservedVoiceChannel.Add(player.id, num);
    return this.m_dicReservedVoiceChannel[player.id];
  }

  private void ChancelResavationChannel(Player player)
  {
    if (Object.op_Equality((Object) player, (Object) null) || this.m_dicReservedVoiceChannel == null || !this.m_dicReservedVoiceChannel.ContainsKey(player.id))
      return;
    this.m_dicReservedVoiceChannel.Remove(player.id);
  }

  public void LoadParmanentAudioClip()
  {
    this.m_IsLoading = true;
    this.StartCoroutine(this.DoLoading());
  }

  public Coroutine WaitLoading() => this.StartCoroutine(this.DoWaitLoading());

  public bool IsLoadingAudioClip()
  {
    return MonoBehaviourSingleton<SoundManager>.IsValid() && this.m_IsLoading;
  }

  public bool IsLoadedAudioClip()
  {
    return MonoBehaviourSingleton<SoundManager>.IsValid() && this.m_IsLoaded;
  }

  private IEnumerator DoWaitLoading()
  {
    while (this.IsLoadingAudioClip())
      yield return (object) null;
  }

  private IEnumerator DoLoading()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    int[] values = (int[]) Enum.GetValues(typeof (SoundID.UISE));
    List<LoadObject> los = new List<LoadObject>();
    bool internalMode = ResourceManager.internalMode;
    bool enableCache = ResourceManager.enableCache;
    ResourceManager.internalMode = true;
    ResourceManager.enableCache = false;
    foreach (int se_id in values)
    {
      if (se_id > 0)
        los.Add(loadingQueue.LoadSE(se_id));
    }
    ResourceManager.internalMode = internalMode;
    ResourceManager.enableCache = enableCache;
    yield return (object) loadingQueue.Wait();
    this.SetSystemSEClips(values, los);
    this.m_IsLoading = false;
    this.m_IsLoaded = true;
  }

  public static AudioClip GetAttachedAudio(GameObject go, string filter = null)
  {
    if (Object.op_Equality((Object) go, (Object) null))
      return (AudioClip) null;
    ResourceLink component = go.GetComponent<ResourceLink>();
    return Object.op_Equality((Object) component, (Object) null) ? (AudioClip) null : component.GetFirstObject<AudioClip>(filter);
  }

  public void SetSystemSEClips(int[] values, List<LoadObject> los)
  {
    this.m_SystemSEClips = new UIntKeyTable<AudioClip>();
    foreach (int num in values)
    {
      if (num > 0)
      {
        string se = ResourceName.GetSE(num);
        LoadObject loadObject = (LoadObject) null;
        foreach (LoadObject lo in los)
        {
          if (lo != null && Object.op_Inequality(lo.loadedObject, (Object) null) && !string.IsNullOrEmpty(lo.loadedObject.name))
          {
            string str = ResourceName.Normalize(lo.loadedObject.name);
            if (se == str)
              loadObject = lo;
          }
        }
        if (loadObject != null)
        {
          AudioClip loadedObject = loadObject.loadedObject as AudioClip;
          if (Object.op_Inequality((Object) loadedObject, (Object) null))
            this.m_SystemSEClips.Add((uint) num, loadedObject);
        }
      }
    }
  }

  public class AudioPreset
  {
    public string name { get; private set; }

    public float minDistance { get; private set; }

    public float maxDistance { get; private set; }

    public AudioRolloffMode rollOffMode => (AudioRolloffMode) 0;

    public AudioPreset(string _name, float _minDistance, float _maxDistance)
    {
      this.name = _name;
      this.minDistance = _minDistance;
      this.maxDistance = _maxDistance;
    }
  }
}
