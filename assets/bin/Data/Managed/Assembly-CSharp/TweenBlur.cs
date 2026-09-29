// Decompiled with JetBrains decompiler
// Type: TweenBlur
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class TweenBlur : MonoBehaviour
{
  [Range(0.0f, 5f)]
  public float from = 1f;
  [Range(0.0f, 5f)]
  public float to = 1f;
  [SerializeField]
  private float duration = 1f;
  [SerializeField]
  private float delay;
  private float timer;
  [SerializeField]
  private Material sourceMaterial;
  public float _lod;

  public float lod
  {
    get
    {
      foreach (UIDrawCall active in UIDrawCall.activeList)
      {
        if (Object.op_Equality((Object) this.sourceMaterial, (Object) active.baseMaterial))
          return active.dynamicMaterial.GetFloat("_Lod");
      }
      return 1f;
    }
    set
    {
      foreach (UIDrawCall active in UIDrawCall.activeList)
      {
        if (Object.op_Equality((Object) this.sourceMaterial, (Object) active.baseMaterial))
        {
          this.sourceMaterial.SetFloat("_Lod", value);
          active.dynamicMaterial.SetFloat("_Lod", value);
          this._lod = value;
        }
      }
    }
  }

  private void LateUpdate()
  {
    this.timer += Time.deltaTime;
    if ((double) this.timer < (double) this.delay)
      this.lod = this.from;
    else if ((double) this.timer < (double) this.duration + (double) this.duration)
    {
      this.lod = Mathf.Lerp(this.from, this.to, (this.timer - this.duration) / this.duration);
    }
    else
    {
      this.lod = this.to;
      ((Behaviour) this).enabled = false;
    }
  }
}
