// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.ImageEffectBase
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[RequireComponent(typeof (Camera))]
[AddComponentMenu("")]
public class ImageEffectBase : MonoBehaviour
{
  public Shader shader;
  private Material m_Material;

  protected virtual void Start()
  {
    if (!SystemInfo.supportsImageEffects)
    {
      ((Behaviour) this).enabled = false;
    }
    else
    {
      if (Object.op_Implicit((Object) this.shader) && this.shader.isSupported)
        return;
      ((Behaviour) this).enabled = false;
    }
  }

  protected Material material
  {
    get
    {
      if (Object.op_Equality((Object) this.m_Material, (Object) null))
      {
        this.m_Material = new Material(this.shader);
        ((Object) this.m_Material).hideFlags = (HideFlags) 61;
      }
      return this.m_Material;
    }
  }

  protected virtual void OnDisable()
  {
    if (!Object.op_Implicit((Object) this.m_Material))
      return;
    Object.DestroyImmediate((Object) this.m_Material);
  }
}
