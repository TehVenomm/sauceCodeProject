// Decompiled with JetBrains decompiler
// Type: PlayingSoundList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class PlayingSoundList
{
  private const int HIGHEST_PRIORITY = 0;
  private const int LOWEST_PRIORITY = 255 /*0xFF*/;
  private const int MAX_PRIORITY_NUM = 256 /*0x0100*/;
  private Dictionary<int, List<AudioObject>> playingObjects = new Dictionary<int, List<AudioObject>>();
  private List<AudioObject>[] priorityList = new List<AudioObject>[256 /*0x0100*/];
  private const float DEFAULT_INTERVAL = 0.002f;

  public PlayingSoundList()
  {
    for (int index = 0; index < 256 /*0x0100*/; ++index)
      this.priorityList[index] = new List<AudioObject>(20);
  }

  public int playingSENum { get; private set; }

  public void AddSE(AudioObject so)
  {
    if (!this.playingObjects.ContainsKey(so.clipId))
      this.playingObjects.Add(so.clipId, new List<AudioObject>(20));
    this.playingObjects[so.clipId].Add(so);
    ++this.playingSENum;
  }

  public void RemoveSE(AudioObject so)
  {
    this.playingObjects[so.clipId].Remove(so);
    --this.playingSENum;
  }

  public bool canPlay(int clip_id)
  {
    SETable.Data seData = Singleton<SETable>.I.GetSeData((uint) clip_id);
    float num = 1f / 500f;
    if (seData != null)
      num = seData.intervalLimit;
    float time = Time.time;
    if (this.playingObjects.ContainsKey(clip_id))
    {
      int count = this.playingObjects[clip_id].Count;
      for (int index = 0; index < count; ++index)
      {
        if ((double) Mathf.Abs(this.playingObjects[clip_id][index].timeAtPlay - time) < (double) num)
          return false;
      }
    }
    return true;
  }
}
