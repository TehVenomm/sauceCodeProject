// Decompiled with JetBrains decompiler
// Type: AudioObjectPool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class AudioObjectPool : MonoBehaviourSingleton<AudioObjectPool>
{
  private const int STACK_MAX = 20;
  private int current_index;
  private AudioObject[] audio_object_stack;

  public int CachedObjectCount => this.current_index;

  protected override void Awake()
  {
    base.Awake();
    this.StartCoroutine(this.CreateStack());
  }

  public static void StopAllLentObjects()
  {
    if (!MonoBehaviourSingleton<AudioObjectPool>.IsValid())
      return;
    MonoBehaviourSingleton<AudioObjectPool>.I.ForEachLentObjects((Action<AudioObject>) (ao =>
    {
      if (!Object.op_Inequality((Object) ao, (Object) null))
        return;
      ao.Stop();
    }));
  }

  public static void StopAll()
  {
    if (!MonoBehaviourSingleton<AudioObjectPool>.IsValid())
      return;
    MonoBehaviourSingleton<AudioObjectPool>.I.ForEach((Action<AudioObject>) (ao =>
    {
      if (!Object.op_Inequality((Object) ao, (Object) null) || ao.PlayPhase != AudioObject.Phase.PLAYING)
        return;
      ao.Stop();
    }));
  }

  private void ForEach(Action<AudioObject> act)
  {
    if (this.audio_object_stack == null)
      return;
    foreach (AudioObject audioObject in this.audio_object_stack)
      act(audioObject);
  }

  private void ForEachLentObjects(Action<AudioObject> act)
  {
    if (this.audio_object_stack == null)
      return;
    int num = this.current_index - 1;
    if (num < 0)
      return;
    for (int index = num; index < 20; ++index)
      act(this.audio_object_stack[index]);
  }

  private IEnumerator CreateStack()
  {
    this.audio_object_stack = new AudioObject[20];
    for (int i = 0; i < 20; ++i)
    {
      this.audio_object_stack[i] = this.CreateObject(i + 1);
      ((Component) this.audio_object_stack[i]).transform.parent = ((Component) this).transform;
      ((Component) this.audio_object_stack[i]).gameObject.SetActive(false);
      yield return (object) null;
    }
    this.SetCursorTail();
  }

  private AudioObject CreateObject(int managed_id)
  {
    GameObject gameObject = new GameObject("AudioObject");
    AudioObject audioObject = gameObject.AddComponent<AudioObject>();
    AudioSource source = gameObject.AddComponent<AudioSource>();
    source.playOnAwake = false;
    AudioObject.Init(audioObject, source, managed_id);
    return audioObject;
  }

  public static AudioObject Borrow()
  {
    return !MonoBehaviourSingleton<AudioObjectPool>.IsValid() ? (AudioObject) null : MonoBehaviourSingleton<AudioObjectPool>.I.Borrow_Imm();
  }

  private AudioObject Borrow_Imm()
  {
    if (this.CachedObjectCount <= 0)
      return this.CreateObject(-1);
    AudioObject audioObject = this.audio_object_stack[this.current_index];
    ((Component) audioObject).gameObject.SetActive(true);
    this.DownCursor();
    return audioObject;
  }

  public static void Release(AudioObject obj)
  {
    if (!MonoBehaviourSingleton<AudioObjectPool>.IsValid())
      return;
    MonoBehaviourSingleton<AudioObjectPool>.I.Release_Imm(obj);
  }

  private void Release_Imm(AudioObject obj)
  {
    if (obj.ID > 0)
    {
      this.UpCursor();
      this.audio_object_stack[this.current_index] = obj;
      ((Component) this.audio_object_stack[this.current_index]).transform.parent = ((Component) this).transform;
      ((Component) obj).gameObject.SetActive(false);
    }
    else
      Object.Destroy((Object) ((Component) obj).gameObject);
  }

  private void SetCursorTail() => this.current_index = 19;

  private void UpCursor() => this.current_index = Mathf.Min(this.current_index + 1, 19);

  private void DownCursor() => this.current_index = Mathf.Max(this.current_index - 1, 0);
}
