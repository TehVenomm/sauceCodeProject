// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.Blur
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Blur/Blur")]
public class Blur : MonoBehaviour
{
  [Range(0.0f, 10f)]
  public int iterations = 3;
  [Range(0.0f, 1f)]
  public float blurSpread = 0.6f;
  public Shader blurShader;
  private static Material m_Material;

  protected Material material
  {
    get
    {
      if (Object.op_Equality((Object) Blur.m_Material, (Object) null))
      {
        Blur.m_Material = new Material(this.blurShader);
        ((Object) Blur.m_Material).hideFlags = (HideFlags) 52;
      }
      return Blur.m_Material;
    }
  }

  protected void OnDisable()
  {
    if (!Object.op_Implicit((Object) Blur.m_Material))
      return;
    Object.DestroyImmediate((Object) Blur.m_Material);
  }

  protected void Start()
  {
    if (!SystemInfo.supportsImageEffects)
    {
      ((Behaviour) this).enabled = false;
    }
    else
    {
      if (Object.op_Implicit((Object) this.blurShader) && this.material.shader.isSupported)
        return;
      ((Behaviour) this).enabled = false;
    }
  }

  public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
  {
    float num = (float) (0.5 + (double) iteration * (double) this.blurSpread);
    Graphics.BlitMultiTap((Texture) source, dest, this.material, new Vector2[4]
    {
      new Vector2(-num, -num),
      new Vector2(-num, num),
      new Vector2(num, num),
      new Vector2(num, -num)
    });
  }

  private void DownSample4x(RenderTexture source, RenderTexture dest)
  {
    float num = 1f;
    Graphics.BlitMultiTap((Texture) source, dest, this.material, new Vector2[4]
    {
      new Vector2(-num, -num),
      new Vector2(-num, num),
      new Vector2(num, num),
      new Vector2(num, -num)
    });
  }

  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    int num1 = ((Texture) source).width / 4;
    int num2 = ((Texture) source).height / 4;
    RenderTexture renderTexture = RenderTexture.GetTemporary(num1, num2, 0);
    this.DownSample4x(source, renderTexture);
    for (int iteration = 0; iteration < this.iterations; ++iteration)
    {
      RenderTexture temporary = RenderTexture.GetTemporary(num1, num2, 0);
      this.FourTapCone(renderTexture, temporary, iteration);
      RenderTexture.ReleaseTemporary(renderTexture);
      renderTexture = temporary;
    }
    Graphics.Blit((Texture) renderTexture, destination);
    RenderTexture.ReleaseTemporary(renderTexture);
  }
}
