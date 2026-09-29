// Decompiled with JetBrains decompiler
// Type: TweenVolume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[RequireComponent(typeof (AudioSource))]
[AddComponentMenu("NGUI/Tween/Tween Volume")]
public class TweenVolume : UITweener
{
  [Range(0.0f, 1f)]
  public float from = 1f;
  [Range(0.0f, 1f)]
  public float to = 1f;
  private AudioSource mSource;

  public AudioSource audioSource
  {
    get
    {
      if (Object.op_Equality((Object) this.mSource, (Object) null))
      {
        this.mSource = ((Component) this).GetComponent<AudioSource>();
        if (Object.op_Equality((Object) this.mSource, (Object) null))
        {
          this.mSource = ((Component) this).GetComponent<AudioSource>();
          if (Object.op_Equality((Object) this.mSource, (Object) null))
          {
            Debug.LogError((object) "TweenVolume needs an AudioSource to work with", (Object) this);
            ((Behaviour) this).enabled = false;
          }
        }
      }
      return this.mSource;
    }
  }

  [Obsolete("Use 'value' instead")]
  public float volume
  {
    get => this.value;
    set => this.value = value;
  }

  public float value
  {
    get
    {
      return !Object.op_Inequality((Object) this.audioSource, (Object) null) ? 0.0f : this.mSource.volume;
    }
    set
    {
      if (!Object.op_Inequality((Object) this.audioSource, (Object) null))
        return;
      this.mSource.volume = value;
    }
  }

  protected override void OnUpdate(float factor, bool isFinished)
  {
    this.value = (float) ((double) this.from * (1.0 - (double) factor) + (double) this.to * (double) factor);
    ((Behaviour) this.mSource).enabled = (double) this.mSource.volume > 0.0099999997764825821;
  }

  public static TweenVolume Begin(GameObject go, float duration, float targetVolume)
  {
    TweenVolume tweenVolume = UITweener.Begin<TweenVolume>(go, duration);
    tweenVolume.from = tweenVolume.value;
    tweenVolume.to = targetVolume;
    return tweenVolume;
  }

  public override void SetStartToCurrentValue() => this.from = this.value;

  public override void SetEndToCurrentValue() => this.to = this.value;
}
