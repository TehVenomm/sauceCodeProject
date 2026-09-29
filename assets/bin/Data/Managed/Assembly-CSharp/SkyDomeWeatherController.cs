// Decompiled with JetBrains decompiler
// Type: SkyDomeWeatherController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SkyDomeWeatherController : MonoBehaviour
{
  [SerializeField]
  private Renderer[] originalRenderer;
  [SerializeField]
  private Renderer[] afterRenderer;
  private int MATERIALCOLOR_PROPERTY_KEY;

  private void Awake()
  {
    this.MATERIALCOLOR_PROPERTY_KEY = Shader.PropertyToID("_MainColor");
    this.UpdateRenderers(0.0f);
  }

  public void UpdateRenderers(float rate)
  {
    if (this.originalRenderer != null)
    {
      for (int index = 0; index < this.originalRenderer.Length; ++index)
      {
        if (!Object.op_Equality((Object) this.originalRenderer[index], (Object) null))
        {
          foreach (Material sharedMaterial in this.originalRenderer[index].sharedMaterials)
          {
            if (!Object.op_Equality((Object) sharedMaterial, (Object) null) && sharedMaterial.HasProperty(this.MATERIALCOLOR_PROPERTY_KEY))
            {
              Color color = sharedMaterial.GetColor(this.MATERIALCOLOR_PROPERTY_KEY);
              color.a = 1f - rate;
              sharedMaterial.SetColor(this.MATERIALCOLOR_PROPERTY_KEY, color);
            }
          }
        }
      }
    }
    if (this.afterRenderer == null)
      return;
    for (int index1 = 0; index1 < this.afterRenderer.Length; ++index1)
    {
      if (!Object.op_Equality((Object) this.afterRenderer[index1], (Object) null))
      {
        Material[] materials = this.afterRenderer[index1].materials;
        for (int index2 = 0; index2 < this.afterRenderer[index1].materials.Length; ++index2)
        {
          Material sharedMaterial = this.afterRenderer[index1].sharedMaterials[index2];
          if (!Object.op_Equality((Object) sharedMaterial, (Object) null) && sharedMaterial.HasProperty(this.MATERIALCOLOR_PROPERTY_KEY))
          {
            Color color = sharedMaterial.GetColor(this.MATERIALCOLOR_PROPERTY_KEY);
            color.a = rate;
            sharedMaterial.SetColor(this.MATERIALCOLOR_PROPERTY_KEY, color);
          }
        }
      }
    }
  }
}
