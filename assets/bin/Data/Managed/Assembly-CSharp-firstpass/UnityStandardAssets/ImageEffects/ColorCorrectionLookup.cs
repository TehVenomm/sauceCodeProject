// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.ColorCorrectionLookup
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Color Correction (3D Lookup Texture)")]
public class ColorCorrectionLookup : PostEffectsBase
{
  public Shader shader;
  private Material material;
  public Texture3D converted3DLut;
  public string basedOnTempTex = "";

  public override bool CheckResources()
  {
    this.CheckSupport(false);
    this.material = this.CheckShaderAndCreateMaterial(this.shader, this.material);
    if (!this.isSupported || !SystemInfo.supports3DTextures)
      this.ReportAutoDisable();
    return this.isSupported;
  }

  private void OnDisable()
  {
    if (!Object.op_Implicit((Object) this.material))
      return;
    Object.DestroyImmediate((Object) this.material);
    this.material = (Material) null;
  }

  private void OnDestroy()
  {
    if (Object.op_Implicit((Object) this.converted3DLut))
      Object.DestroyImmediate((Object) this.converted3DLut);
    this.converted3DLut = (Texture3D) null;
  }

  public void SetIdentityLut()
  {
    int num1 = 16 /*0x10*/;
    Color[] colorArray = new Color[num1 * num1 * num1];
    float num2 = (float) (1.0 / (1.0 * (double) num1 - 1.0));
    for (int index1 = 0; index1 < num1; ++index1)
    {
      for (int index2 = 0; index2 < num1; ++index2)
      {
        for (int index3 = 0; index3 < num1; ++index3)
          colorArray[index1 + index2 * num1 + index3 * num1 * num1] = new Color((float) index1 * 1f * num2, (float) index2 * 1f * num2, (float) index3 * 1f * num2, 1f);
      }
    }
    if (Object.op_Implicit((Object) this.converted3DLut))
      Object.DestroyImmediate((Object) this.converted3DLut);
    this.converted3DLut = new Texture3D(num1, num1, num1, (TextureFormat) 5, false);
    this.converted3DLut.SetPixels(colorArray);
    this.converted3DLut.Apply();
    this.basedOnTempTex = "";
  }

  public bool ValidDimensions(Texture2D tex2d)
  {
    return Object.op_Implicit((Object) tex2d) && ((Texture) tex2d).height == Mathf.FloorToInt(Mathf.Sqrt((float) ((Texture) tex2d).width));
  }

  public void Convert(Texture2D temp2DTex, string path)
  {
    if (Object.op_Implicit((Object) temp2DTex))
    {
      int num1 = ((Texture) temp2DTex).width * ((Texture) temp2DTex).height;
      int height = ((Texture) temp2DTex).height;
      if (!this.ValidDimensions(temp2DTex))
      {
        Debug.LogWarning((object) $"The given 2D texture {((Object) temp2DTex).name} cannot be used as a 3D LUT.");
        this.basedOnTempTex = "";
      }
      else
      {
        Color[] pixels = temp2DTex.GetPixels();
        Color[] colorArray = new Color[pixels.Length];
        for (int index1 = 0; index1 < height; ++index1)
        {
          for (int index2 = 0; index2 < height; ++index2)
          {
            for (int index3 = 0; index3 < height; ++index3)
            {
              int num2 = height - index2 - 1;
              colorArray[index1 + index2 * height + index3 * height * height] = pixels[index3 * height + index1 + num2 * height * height];
            }
          }
        }
        if (Object.op_Implicit((Object) this.converted3DLut))
          Object.DestroyImmediate((Object) this.converted3DLut);
        this.converted3DLut = new Texture3D(height, height, height, (TextureFormat) 5, false);
        this.converted3DLut.SetPixels(colorArray);
        this.converted3DLut.Apply();
        this.basedOnTempTex = path;
      }
    }
    else
      Debug.LogError((object) "Couldn't color correct with 3D LUT texture. Image Effect will be disabled.");
  }

  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (!this.CheckResources() || !SystemInfo.supports3DTextures)
    {
      Graphics.Blit((Texture) source, destination);
    }
    else
    {
      if (Object.op_Equality((Object) this.converted3DLut, (Object) null))
        this.SetIdentityLut();
      int width = ((Texture) this.converted3DLut).width;
      ((Texture) this.converted3DLut).wrapMode = (TextureWrapMode) 1;
      this.material.SetFloat("_Scale", (float) (width - 1) / (1f * (float) width));
      this.material.SetFloat("_Offset", (float) (1.0 / (2.0 * (double) width)));
      this.material.SetTexture("_ClutTex", (Texture) this.converted3DLut);
      Graphics.Blit((Texture) source, destination, this.material, QualitySettings.activeColorSpace == 1 ? 1 : 0);
    }
  }
}
