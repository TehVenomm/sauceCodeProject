// Decompiled with JetBrains decompiler
// Type: TweenOrthoSize
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[RequireComponent(typeof (Camera))]
[AddComponentMenu("NGUI/Tween/Tween Orthographic Size")]
public class TweenOrthoSize : UITweener
{
  public float from = 1f;
  public float to = 1f;
  private Camera mCam;

  public Camera cachedCamera
  {
    get
    {
      if (Object.op_Equality((Object) this.mCam, (Object) null))
        this.mCam = ((Component) this).GetComponent<Camera>();
      return this.mCam;
    }
  }

  [Obsolete("Use 'value' instead")]
  public float orthoSize
  {
    get => this.value;
    set => this.value = value;
  }

  public float value
  {
    get => this.cachedCamera.orthographicSize;
    set => this.cachedCamera.orthographicSize = value;
  }

  protected override void OnUpdate(float factor, bool isFinished)
  {
    this.value = (float) ((double) this.from * (1.0 - (double) factor) + (double) this.to * (double) factor);
  }

  public static TweenOrthoSize Begin(GameObject go, float duration, float to)
  {
    TweenOrthoSize tweenOrthoSize = UITweener.Begin<TweenOrthoSize>(go, duration);
    tweenOrthoSize.from = tweenOrthoSize.value;
    tweenOrthoSize.to = to;
    if ((double) duration <= 0.0)
    {
      tweenOrthoSize.Sample(1f, true);
      ((Behaviour) tweenOrthoSize).enabled = false;
    }
    return tweenOrthoSize;
  }

  public override void SetStartToCurrentValue() => this.from = this.value;

  public override void SetEndToCurrentValue() => this.to = this.value;
}
