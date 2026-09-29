// Decompiled with JetBrains decompiler
// Type: TransformInterpolator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class TransformInterpolator : MonoBehaviour
{
  public bool play = true;
  public Vector3Interpolator translate;
  public Vector3Interpolator rotate;
  public Vector3Interpolator scaling;
  public object lookAt;
  private bool rotateAngleMode;

  public Transform _transform { get; private set; }

  private void Awake() => this._transform = ((Component) this).transform;

  private void Update()
  {
    if (this.lookAt != null)
    {
      if (Object.op_Implicit((Object) (this.lookAt as Transform)))
        this._transform.LookAt(this.lookAt as Transform);
      else
        this._transform.LookAt((Vector3) this.lookAt);
    }
    if (!this.play)
      return;
    if (this.translate != null && this.translate.IsPlaying())
      this._transform.localPosition = this.translate.Update();
    if (this.rotate != null && this.rotate.IsPlaying())
      this._transform.localEulerAngles = this.rotate.Update();
    if (this.scaling == null || !this.scaling.IsPlaying())
      return;
    this._transform.localScale = this.scaling.Update();
  }

  public TransformInterpolator Translate(
    float _time,
    Vector3 target,
    AnimationCurve ease_curve = null,
    Vector3 add_value = default (Vector3),
    AnimationCurve add_curve = null)
  {
    if ((double) _time > 0.0)
    {
      if (this.translate == null)
        this.translate = new Vector3Interpolator();
      this.translate.Set(_time, this._transform.localPosition, target, ease_curve, add_value, add_curve);
      this.translate.Play();
    }
    else
    {
      this.translate = (Vector3Interpolator) null;
      this._transform.localPosition = target;
    }
    return this;
  }

  public TransformInterpolator Rotate(
    float _time,
    Vector3 target,
    AnimationCurve ease_curve = null,
    Vector3 add_value = default (Vector3),
    AnimationCurve add_curve = null,
    bool lerp_angle = true)
  {
    if ((double) _time > 0.0)
    {
      if (this.rotate == null || this.rotateAngleMode != lerp_angle)
      {
        this.rotateAngleMode = lerp_angle;
        this.rotate = !lerp_angle ? new Vector3Interpolator() : (Vector3Interpolator) new AngleVector3Interpolator();
      }
      this.rotate.Set(_time, this._transform.localEulerAngles, target, ease_curve, add_value, add_curve);
      this.rotate.Play();
    }
    else
    {
      this.rotate = (Vector3Interpolator) null;
      this._transform.localEulerAngles = target;
    }
    return this;
  }

  public TransformInterpolator Scaling(
    float _time,
    Vector3 target,
    AnimationCurve ease_curve = null,
    Vector3 add_value = default (Vector3),
    AnimationCurve add_curve = null)
  {
    if ((double) _time > 0.0)
    {
      if (this.scaling == null)
        this.scaling = new Vector3Interpolator();
      this.scaling.Set(_time, this._transform.localScale, target, ease_curve, add_value, add_curve);
      this.scaling.Play();
    }
    else
    {
      this.scaling = (Vector3Interpolator) null;
      this._transform.localScale = target;
    }
    return this;
  }

  public TransformInterpolator LookAt(Transform target)
  {
    this.lookAt = (object) target;
    return this;
  }

  public TransformInterpolator LookAt(Vector3 target)
  {
    this.lookAt = (object) target;
    return this;
  }

  public bool IsPlaying()
  {
    return this.play && (this.translate != null && this.translate.IsPlaying() || this.rotate != null && this.rotate.IsPlaying() || this.scaling != null && this.scaling.IsPlaying());
  }

  public Coroutine Wait() => this.StartCoroutine(this.DoWait());

  private IEnumerator DoWait()
  {
    while (this.IsPlaying())
      yield return (object) null;
  }
}
