// Decompiled with JetBrains decompiler
// Type: AudioControlGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

#nullable disable
public class AudioControlGroup : DisableNotifyMonoBehaviour
{
  private bool m_bUnique;
  private AudioObject m_lastAudio;
  private Dictionary<int, PlayingAudioList> m_dicPlayingAudio;
  private List<AudioControlGroup.ClipPriorityInfo> m_ClipIdsSortByPriority;

  public AudioControlGroup.CullingTypes CullingType { get; private set; }

  public int PlayingLimitNum { get; private set; }

  public int PlayingCount { get; private set; }

  public static AudioControlGroup Create(AudioControlGroup.CullingTypes type = AudioControlGroup.CullingTypes.NONE, int LimitNum = 2147483647 /*0x7FFFFFFF*/)
  {
    AudioControlGroup audioControlGroup = new GameObject(nameof (AudioControlGroup)).AddComponent<AudioControlGroup>();
    audioControlGroup._transform.parent = MonoBehaviourSingleton<SoundManager>.I._transform;
    audioControlGroup.Setup(type, LimitNum);
    return audioControlGroup;
  }

  private void Setup(AudioControlGroup.CullingTypes type, int LimitNum)
  {
    if (this.m_dicPlayingAudio == null)
      this.m_dicPlayingAudio = new Dictionary<int, PlayingAudioList>();
    else
      this.m_dicPlayingAudio.Clear();
    this.CullingType = type;
    this.PlayingLimitNum = LimitNum;
    this.PlayingCount = 0;
    if (type != AudioControlGroup.CullingTypes.OVERWRITE || LimitNum != 1)
      return;
    this.m_bUnique = true;
  }

  protected override void OnDisableMaster()
  {
  }

  private void AddPlayingList(int clip_id)
  {
    SETable.Data seData = Singleton<SETable>.I.GetSeData((uint) clip_id);
    if (seData == null)
      return;
    PlayingAudioList playingAudioList = new PlayingAudioList();
    playingAudioList.Setup(clip_id, seData.priority, seData.limitNum, seData.intervalLimit, seData.CullingType);
    this.m_dicPlayingAudio.Add(clip_id, playingAudioList);
  }

  private void PreparePlayingList(int clip_id)
  {
    if (this.m_dicPlayingAudio.ContainsKey(clip_id))
      return;
    this.AddPlayingList(clip_id);
  }

  private void PrepareKeyOn(int clip_id)
  {
    if (this.m_bUnique && Object.op_Inequality((Object) this.m_lastAudio, (Object) null))
    {
      this.m_lastAudio.Stop();
      this.m_lastAudio = (AudioObject) null;
    }
    if (!this.m_dicPlayingAudio.ContainsKey(clip_id))
      return;
    this.m_dicPlayingAudio[clip_id].OpanPlaySlot(1);
  }

  private bool CanPlay(int clip_id)
  {
    if (this.CullingType == AudioControlGroup.CullingTypes.REJECT && this.PlayingCount >= this.PlayingLimitNum)
      return false;
    return !this.m_dicPlayingAudio.ContainsKey(clip_id) || this.m_dicPlayingAudio[clip_id].CanPlay();
  }

  private bool NeedControl(int clip_id)
  {
    return this.m_dicPlayingAudio != null && this.m_dicPlayingAudio.ContainsKey(clip_id);
  }

  public AudioObject CreateAudio(
    AudioClip clip,
    int clip_id,
    float volume,
    bool loop,
    AudioMixerGroup mixer_group,
    bool is3DSound = false,
    DisableNotifyMonoBehaviour master = null,
    Transform _parent = null,
    Vector3? initPos = null)
  {
    if (Object.op_Equality((Object) clip, (Object) null))
      return (AudioObject) null;
    this.PreparePlayingList(clip_id);
    if (!this.CanPlay(clip_id))
      return (AudioObject) null;
    this.PrepareKeyOn(clip_id);
    Transform parent = Object.op_Equality((Object) _parent, (Object) null) ? this._transform : _parent;
    AudioObject audio = AudioObject.Create(clip, clip_id, volume, loop, mixer_group, this, is3DSound, master, parent, initPos);
    this.m_lastAudio = audio;
    return audio;
  }

  public void NotifyOnStart(AudioObject startAudio)
  {
    if (Object.op_Equality((Object) startAudio, (Object) null))
      return;
    this.JoinAudio(startAudio);
  }

  public void NotifyOnRelease(AudioObject releaseAudio)
  {
    if (Object.op_Equality((Object) releaseAudio, (Object) null))
      return;
    this.LeaveAudio(releaseAudio);
  }

  public void NotifyOnStop(AudioObject stoppedAudio)
  {
    if (Object.op_Equality((Object) stoppedAudio, (Object) null))
      return;
    this.LeaveAudio(stoppedAudio);
  }

  private void JoinAudio(AudioObject ao)
  {
    if (!this.m_dicPlayingAudio.ContainsKey(ao.clipId))
      return;
    this.m_dicPlayingAudio[ao.clipId].Add(ao);
  }

  private void LeaveAudio(AudioObject ao)
  {
    if (Object.op_Inequality((Object) this.m_lastAudio, (Object) null) && Object.op_Equality((Object) this.m_lastAudio, (Object) ao))
      this.m_lastAudio = (AudioObject) null;
    if (!this.m_dicPlayingAudio.ContainsKey(ao.clipId))
      return;
    this.m_dicPlayingAudio[ao.clipId].Remove(ao);
  }

  public void StopAll(int fadeout_frames = 0)
  {
    if (Object.op_Inequality((Object) this.m_lastAudio, (Object) null))
    {
      this.m_lastAudio.Stop(fadeout_frames);
      this.m_lastAudio = (AudioObject) null;
    }
    foreach (PlayingAudioList playingAudioList in this.m_dicPlayingAudio.Values)
      playingAudioList.StopAll(fadeout_frames);
  }

  public enum CullingTypes
  {
    NONE,
    REJECT,
    OVERWRITE,
    PRIORITY,
    TYPE_MAX,
  }

  private struct ClipPriorityInfo
  {
    private uint clipId;
    private uint priority;
  }
}
