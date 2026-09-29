// Decompiled with JetBrains decompiler
// Type: PlayingAudioList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class PlayingAudioList
{
  private const float DEFAULT_INTERVAL = 0.002f;
  public const int HIGHEST_PRIORITY = 0;
  public const int LOWEST_PRIORITY = 255 /*0xFF*/;
  private List<AudioObject> m_AudioLists;
  private const int LIST_DEFAULT_SIZE = 32 /*0x20*/;

  public int ClipId { get; private set; }

  public uint Priority { get; private set; }

  public float IntervalTime { get; private set; }

  public int LimitNum { get; private set; }

  public int PlayingCount { get; private set; }

  public AudioControlGroup.CullingTypes CullingType { get; private set; }

  public PlayingAudioList()
  {
    this.ClipId = -1;
    this.Priority = (uint) byte.MaxValue;
    this.IntervalTime = 1f / 500f;
    this.LimitNum = int.MaxValue;
    this.PlayingCount = 0;
    this.CullingType = AudioControlGroup.CullingTypes.NONE;
  }

  public PlayingAudioList(
    int clipId,
    uint priority,
    int limitNum,
    float intervalTime,
    AudioControlGroup.CullingTypes type)
  {
    this.Setup(clipId, priority, limitNum, intervalTime, type);
  }

  public void Setup(
    int clipId,
    uint priority,
    int limitNum,
    float intervalTime,
    AudioControlGroup.CullingTypes type)
  {
    this.ClipId = clipId;
    this.Priority = priority;
    this.IntervalTime = intervalTime;
    this.LimitNum = limitNum;
    this.PlayingCount = 0;
    this.CullingType = type;
    if (this.CullingType == AudioControlGroup.CullingTypes.NONE)
      return;
    if (this.m_AudioLists == null)
      this.m_AudioLists = new List<AudioObject>(this.LimitNum);
    else
      this.m_AudioLists.Clear();
  }

  private void DoCullingAudio(int needCullingNum)
  {
    if (needCullingNum < 1 || this.m_AudioLists == null)
      return;
    for (int index = 0; index < this.m_AudioLists.Count && needCullingNum >= 1; --needCullingNum)
    {
      if (Object.op_Inequality((Object) this.m_AudioLists[index], (Object) null))
        this.m_AudioLists[index].Stop();
    }
  }

  public void StopAll(int fadeout_framecount = 0)
  {
    if (this.m_AudioLists == null)
      return;
    int index = 0;
    while (index < this.m_AudioLists.Count)
    {
      if (Object.op_Inequality((Object) this.m_AudioLists[index], (Object) null))
        this.m_AudioLists[index].Stop(fadeout_framecount);
    }
  }

  public void OpanPlaySlot(int slotCount, bool force = false)
  {
    if (!force && this.CullingType != AudioControlGroup.CullingTypes.OVERWRITE)
      return;
    int needCullingNum = 0;
    if (this.LimitNum > 1)
    {
      needCullingNum = this.PlayingCount + slotCount - this.LimitNum;
      if (needCullingNum < 0)
        needCullingNum = 0;
    }
    this.DoCullingAudio(needCullingNum);
  }

  public bool CanPlay()
  {
    if (this.PlayingCount == 0 || this.m_AudioLists == null || this.m_AudioLists.Count < 1)
      return true;
    AudioObject audioList = this.m_AudioLists[this.m_AudioLists.Count - 1];
    float time = Time.time;
    return (!Object.op_Inequality((Object) audioList, (Object) null) || (double) time - (double) audioList.timeAtPlay >= (double) this.IntervalTime) && (this.CullingType == AudioControlGroup.CullingTypes.OVERWRITE || this.CullingType != AudioControlGroup.CullingTypes.REJECT || this.PlayingCount < this.LimitNum);
  }

  public void Add(AudioObject ao)
  {
    if (this.m_AudioLists == null)
      return;
    this.m_AudioLists.Add(ao);
    ++this.PlayingCount;
    if (this.LimitNum >= this.PlayingCount)
      return;
    this.DoCullingAudio(this.LimitNum - this.PlayingCount);
  }

  public void Remove(AudioObject ao)
  {
    if (this.m_AudioLists == null)
      return;
    this.m_AudioLists.Remove(ao);
    --this.PlayingCount;
    if (this.PlayingCount >= 0)
      return;
    this.PlayingCount = 0;
  }
}
