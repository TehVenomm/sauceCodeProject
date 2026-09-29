// Decompiled with JetBrains decompiler
// Type: EffectSizeCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EffectSizeCtrl : MonoBehaviour
{
  [SerializeField]
  private bool isScale;
  [SerializeField]
  private float endScale;
  [SerializeField]
  private ParticleSystem[] particles;
  private List<EffectSizeCtrl.ParticleInfo> particleInfo = new List<EffectSizeCtrl.ParticleInfo>();
  private bool isWorking;
  private float execSec;
  private float targetSec;
  private float firstScale = 1f;
  private Vector3 _scale = new Vector3(1f, 1f, 1f);
  private Transform _transform;

  private void Awake() => this._transform = ((Component) this).transform;

  private void Destroy()
  {
    this.particleInfo.Clear();
    this._transform = (Transform) null;
  }

  private void Update()
  {
    if (!this.isWorking)
      return;
    this.execSec += Time.deltaTime;
    float num1;
    if ((double) this.execSec >= (double) this.targetSec)
    {
      this.execSec = this.targetSec;
      num1 = 1f;
      this.isWorking = false;
    }
    else
      num1 = this.execSec / this.targetSec;
    float num2 = (float) (1.0 - (1.0 - (double) this.endScale) * (double) num1);
    if (this.isScale)
    {
      float num3 = this.firstScale * num2;
      ((Vector3) ref this._scale).Set(num3, num3, num3);
      this._transform.localScale = this._scale;
    }
    int index = 0;
    for (int count = this.particleInfo.Count; index < count; ++index)
    {
      EffectSizeCtrl.ParticleInfo particleInfo = this.particleInfo[index];
      particleInfo.psr.lengthScale = particleInfo.firstLength * num2;
    }
  }

  public void Work(float sec)
  {
    if (this.isWorking || (double) sec == 0.0)
      return;
    this.particleInfo.Clear();
    int index = 0;
    for (int length = this.particles.Length; index < length; ++index)
    {
      ParticleSystemRenderer component = ((Component) this.particles[index]).GetComponent<ParticleSystemRenderer>();
      if (component != null)
        this.particleInfo.Add(new EffectSizeCtrl.ParticleInfo()
        {
          psr = component,
          firstLength = component.lengthScale
        });
    }
    this.firstScale = this._transform.localScale.x;
    this.execSec = 0.0f;
    this.targetSec = sec;
    this.isWorking = true;
  }

  private class ParticleInfo
  {
    public ParticleSystemRenderer psr;
    public float firstLength;
  }
}
