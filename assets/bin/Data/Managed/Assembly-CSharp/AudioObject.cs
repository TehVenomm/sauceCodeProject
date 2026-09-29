// Decompiled with JetBrains decompiler
// Type: AudioObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Audio;

#nullable disable
public class AudioObject : DisableNotifyMonoBehaviour
{
  public Transform parentObject;
  private AudioSource audioSource;
  private bool needParent;
  private float fadeoutVolume;
  private const int MIN_FADEOUT_FRAMECOUNT = 4;
  private AudioControlGroup m_masterGroup;
  private bool m_IsSpatialSound;
  private bool m_IsStaticPosition;

  public AudioObject.Phase PlayPhase { get; protected set; }

  public bool IsPlayingSound
  {
    get
    {
      return this.PlayPhase == AudioObject.Phase.PLAYING || this.PlayPhase == AudioObject.Phase.PREPLAY;
    }
  }

  public static AudioObject Create(
    AudioClip clip,
    int clip_id,
    float volume,
    bool loop,
    AudioMixerGroup mixer_group,
    AudioControlGroup controlGroup,
    bool is3DSound = false,
    DisableNotifyMonoBehaviour master = null,
    Transform parent = null,
    Vector3? initPos = null)
  {
    if (Object.op_Equality((Object) clip, (Object) null))
      return (AudioObject) null;
    AudioObject audioObject = AudioObjectPool.Borrow();
    Object.op_Equality((Object) audioObject, (Object) null);
    audioObject._transform.parent = MonoBehaviourSingleton<SoundManager>.I._transform;
    audioObject.m_masterGroup = controlGroup;
    audioObject.m_IsSpatialSound = is3DSound;
    if (initPos.HasValue)
    {
      audioObject._transform.position = initPos ?? Vector3.zero;
      audioObject.m_IsStaticPosition = true;
    }
    audioObject.Play(clip, clip_id, volume, loop, mixer_group, master, parent);
    return audioObject;
  }

  public int ID { get; private set; }

  public static void Init(AudioObject obj, AudioSource source, int managed_id = -1)
  {
    obj.InitParams();
    obj.audioSource = source;
    obj.ID = managed_id;
  }

  private void InitParams()
  {
    this.parentObject = (Transform) null;
    this.clipId = 0;
    this.timeAtPlay = 0.0f;
    this.PlayPhase = AudioObject.Phase.NONE;
    this.fadeoutVolume = 0.0f;
    this.needParent = false;
    this.m_IsSpatialSound = false;
    this.m_IsStaticPosition = false;
  }

  private void InitAudioSource()
  {
    if (Object.op_Equality((Object) this.audioSource, (Object) null))
      return;
    this.audioSource.outputAudioMixerGroup = (AudioMixerGroup) null;
    this.audioSource.spatialBlend = 0.0f;
    this.audioSource.spread = 0.0f;
    this.audioSource.priority = 128 /*0x80*/;
    this.audioSource.rolloffMode = (AudioRolloffMode) 1;
    this.audioSource.minDistance = 0.0f;
    this.audioSource.maxDistance = 999f;
    this.audioSource.pitch = 1f;
    this.audioSource.dopplerLevel = 0.0f;
    this.audioSource.clip = (AudioClip) null;
    this.audioSource.loop = false;
    this.audioSource.volume = 1f;
  }

  public int clipId { get; private set; }

  public float timeAtPlay { get; private set; }

  private void Play(
    AudioClip clip,
    int clip_id,
    float volume,
    bool loop,
    AudioMixerGroup mixer_group,
    DisableNotifyMonoBehaviour master,
    Transform parent)
  {
    if (Object.op_Inequality((Object) master, (Object) null))
      this.SetNotifyMaster(master);
    else
      this.ResetNotifyMaster();
    this.clipId = clip_id;
    this.PlayPhase = AudioObject.Phase.PREPLAY;
    if (Object.op_Inequality((Object) this.audioSource, (Object) null))
    {
      this.audioSource.outputAudioMixerGroup = mixer_group;
      if (this.m_IsSpatialSound)
      {
        this.audioSource.spatialBlend = 1f;
        this.audioSource.spread = 360f;
      }
      else
      {
        this.audioSource.spatialBlend = 0.0f;
        this.audioSource.spread = 0.0f;
      }
      this.audioSource.priority = 100;
      this.audioSource.rolloffMode = MonoBehaviourSingleton<SoundManager>.I.CurrentPreset.rollOffMode;
      this.audioSource.minDistance = MonoBehaviourSingleton<SoundManager>.I.CurrentPreset.minDistance;
      this.audioSource.maxDistance = MonoBehaviourSingleton<SoundManager>.I.CurrentPreset.maxDistance;
      this.audioSource.pitch = 1f;
    }
    float num1 = 1f;
    float num2 = 0.0f;
    SETable.Data seData = Singleton<SETable>.I.GetSeData((uint) clip_id);
    if (seData != null)
    {
      this.audioSource.priority = (int) seData.priority;
      num1 = seData.volumeScale;
      num2 = seData.dopplerLevel;
      if ((double) seData.minDistance > 0.0)
        this.audioSource.minDistance = seData.minDistance;
      if ((double) seData.maxDistance > 0.0)
        this.audioSource.maxDistance = seData.maxDistance;
      if ((double) seData.randomPitch > 0.0)
        this.audioSource.pitch = this.GenRandomPitch();
    }
    this.audioSource.dopplerLevel = num2;
    this.audioSource.clip = clip;
    this.audioSource.loop = loop;
    this.audioSource.volume = volume * num1;
    this.parentObject = parent;
    this.needParent = Object.op_Inequality((Object) parent, (Object) null);
    this.fadeoutVolume = 0.0f;
    this.TraceParent();
    this.audioSource.Play();
    if (Object.op_Inequality((Object) this.m_masterGroup, (Object) null))
      this.m_masterGroup.NotifyOnStart(this);
    this.PlayPhase = AudioObject.Phase.PLAYING;
    this.timeAtPlay = Time.time;
  }

  private float GenRandomPitch() => Utility.Random(0.6f) + 0.7f;

  protected override void OnDisableMaster()
  {
    if (Object.op_Equality((Object) this.audioSource, (Object) null))
      return;
    if (this.audioSource.loop)
      this.Stop();
    this.parentObject = (Transform) null;
    this.needParent = false;
  }

  private void LateUpdate()
  {
    if (this.needParent)
    {
      if (Object.op_Inequality((Object) this.parentObject, (Object) null))
        this.TraceParent();
      else if (this.audioSource.loop)
      {
        this.Stop();
        this.needParent = false;
      }
    }
    if ((double) this.fadeoutVolume > 0.0)
    {
      this.audioSource.volume = Mathf.Max(this.audioSource.volume - this.fadeoutVolume, 0.0f);
      if ((double) this.audioSource.volume == 0.0)
        this.StopImmidiate();
    }
    if (this.audioSource.isPlaying)
      return;
    if (Object.op_Inequality((Object) this.m_masterGroup, (Object) null))
    {
      this.m_masterGroup.NotifyOnStop(this);
      this.m_masterGroup = (AudioControlGroup) null;
    }
    this.Dispose();
  }

  private void TraceParent()
  {
    if (this.m_IsStaticPosition || !this.needParent || !Object.op_Inequality((Object) this.parentObject, (Object) null))
      return;
    this._transform.position = this.parentObject.position;
  }

  public void Stop(int fadeout_framecount = 0)
  {
    if (Object.op_Equality((Object) this.audioSource, (Object) null) || (double) this.fadeoutVolume > 0.0)
      return;
    if (fadeout_framecount < 4)
      fadeout_framecount = 4;
    this.fadeoutVolume = this.audioSource.volume / (float) fadeout_framecount;
    if (Object.op_Inequality((Object) this.m_masterGroup, (Object) null))
    {
      this.m_masterGroup.NotifyOnRelease(this);
      this.m_masterGroup = (AudioControlGroup) null;
    }
    this.PlayPhase = AudioObject.Phase.PRESTOP;
  }

  public void SetLoopFlag(bool flag) => this.audioSource.loop = flag;

  public bool GetLoopFlag() => this.audioSource.loop;

  private void StopImmidiate()
  {
    if (Object.op_Equality((Object) this.audioSource, (Object) null) || this.PlayPhase == AudioObject.Phase.NONE || this.PlayPhase == AudioObject.Phase.STOP)
      return;
    this.audioSource.Stop();
    if (Object.op_Inequality((Object) this.m_masterGroup, (Object) null))
    {
      this.m_masterGroup.NotifyOnStop(this);
      this.m_masterGroup = (AudioControlGroup) null;
    }
    this.PlayPhase = AudioObject.Phase.STOP;
  }

  private void Dispose()
  {
    this.InitParams();
    if (Object.op_Inequality((Object) this.audioSource, (Object) null))
    {
      this.audioSource.Stop();
      this.InitAudioSource();
    }
    if (Object.op_Inequality((Object) this.m_masterGroup, (Object) null))
    {
      this.m_masterGroup.NotifyOnStop(this);
      this.m_masterGroup = (AudioControlGroup) null;
    }
    AudioObjectPool.Release(this);
  }

  public enum Phase
  {
    NONE,
    PREPLAY,
    PLAYING,
    PRESTOP,
    STOP,
  }
}
