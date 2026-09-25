// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.NoiseAndGrain
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/Noise/Noise And Grain (Filmic)")]
public class NoiseAndGrain : PostEffectsBase
{
  public float intensityMultiplier = 0.25f;
  public float generalIntensity = 0.5f;
  public float blackIntensity = 1f;
  public float whiteIntensity = 1f;
  public float midGrey = 0.2f;
  public bool dx11Grain;
  public float softness;
  public bool monochrome;
  public Vector3 intensities = new Vector3(1f, 1f, 1f);
  public Vector3 tiling = new Vector3(64f, 64f, 64f);
  public float monochromeTiling = 64f;
  public FilterMode filterMode = (FilterMode) 1;
  public Texture2D noiseTexture;
  public Shader noiseShader;
  private Material noiseMaterial;
  public Shader dx11NoiseShader;
  private Material dx11NoiseMaterial;
  private static float TILE_AMOUNT = 64f;

  public override bool CheckResources()
  {
    this.CheckSupport(false);
    this.noiseMaterial = this.CheckShaderAndCreateMaterial(this.noiseShader, this.noiseMaterial);
    if (this.dx11Grain && this.supportDX11)
      this.dx11NoiseMaterial = this.CheckShaderAndCreateMaterial(this.dx11NoiseShader, this.dx11NoiseMaterial);
    if (!this.isSupported)
      this.ReportAutoDisable();
    return this.isSupported;
  }

  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (!this.CheckResources() || Object.op_Equality((Object) null, (Object) this.noiseTexture))
    {
      Graphics.Blit((Texture) source, destination);
      if (!Object.op_Equality((Object) null, (Object) this.noiseTexture))
        return;
      Debug.LogWarning((object) "Noise & Grain effect failing as noise texture is not assigned. please assign.", (Object) ((Component) this).transform);
    }
    else
    {
      this.softness = Mathf.Clamp(this.softness, 0.0f, 0.99f);
      if (this.dx11Grain && this.supportDX11)
      {
        this.dx11NoiseMaterial.SetFloat("_DX11NoiseTime", (float) Time.frameCount);
        this.dx11NoiseMaterial.SetTexture("_NoiseTex", (Texture) this.noiseTexture);
        this.dx11NoiseMaterial.SetVector("_NoisePerChannel", Vector4.op_Implicit(this.monochrome ? Vector3.one : this.intensities));
        this.dx11NoiseMaterial.SetVector("_MidGrey", Vector4.op_Implicit(new Vector3(this.midGrey, (float) (1.0 / (1.0 - (double) this.midGrey)), -1f / this.midGrey)));
        this.dx11NoiseMaterial.SetVector("_NoiseAmount", Vector4.op_Implicit(Vector3.op_Multiply(new Vector3(this.generalIntensity, this.blackIntensity, this.whiteIntensity), this.intensityMultiplier)));
        if ((double) this.softness > (double) Mathf.Epsilon)
        {
          RenderTexture temporary = RenderTexture.GetTemporary((int) ((double) ((Texture) source).width * (1.0 - (double) this.softness)), (int) ((double) ((Texture) source).height * (1.0 - (double) this.softness)));
          NoiseAndGrain.DrawNoiseQuadGrid(source, temporary, this.dx11NoiseMaterial, this.noiseTexture, this.monochrome ? 3 : 2);
          this.dx11NoiseMaterial.SetTexture("_NoiseTex", (Texture) temporary);
          Graphics.Blit((Texture) source, destination, this.dx11NoiseMaterial, 4);
          RenderTexture.ReleaseTemporary(temporary);
        }
        else
          NoiseAndGrain.DrawNoiseQuadGrid(source, destination, this.dx11NoiseMaterial, this.noiseTexture, this.monochrome ? 1 : 0);
      }
      else
      {
        if (Object.op_Implicit((Object) this.noiseTexture))
        {
          ((Texture) this.noiseTexture).wrapMode = (TextureWrapMode) 0;
          ((Texture) this.noiseTexture).filterMode = this.filterMode;
        }
        this.noiseMaterial.SetTexture("_NoiseTex", (Texture) this.noiseTexture);
        this.noiseMaterial.SetVector("_NoisePerChannel", Vector4.op_Implicit(this.monochrome ? Vector3.one : this.intensities));
        this.noiseMaterial.SetVector("_NoiseTilingPerChannel", Vector4.op_Implicit(this.monochrome ? Vector3.op_Multiply(Vector3.one, this.monochromeTiling) : this.tiling));
        this.noiseMaterial.SetVector("_MidGrey", Vector4.op_Implicit(new Vector3(this.midGrey, (float) (1.0 / (1.0 - (double) this.midGrey)), -1f / this.midGrey)));
        this.noiseMaterial.SetVector("_NoiseAmount", Vector4.op_Implicit(Vector3.op_Multiply(new Vector3(this.generalIntensity, this.blackIntensity, this.whiteIntensity), this.intensityMultiplier)));
        if ((double) this.softness > (double) Mathf.Epsilon)
        {
          RenderTexture temporary = RenderTexture.GetTemporary((int) ((double) ((Texture) source).width * (1.0 - (double) this.softness)), (int) ((double) ((Texture) source).height * (1.0 - (double) this.softness)));
          NoiseAndGrain.DrawNoiseQuadGrid(source, temporary, this.noiseMaterial, this.noiseTexture, 2);
          this.noiseMaterial.SetTexture("_NoiseTex", (Texture) temporary);
          Graphics.Blit((Texture) source, destination, this.noiseMaterial, 1);
          RenderTexture.ReleaseTemporary(temporary);
        }
        else
          NoiseAndGrain.DrawNoiseQuadGrid(source, destination, this.noiseMaterial, this.noiseTexture, 0);
      }
    }
  }

  private static void DrawNoiseQuadGrid(
    RenderTexture source,
    RenderTexture dest,
    Material fxMaterial,
    Texture2D noise,
    int passNr)
  {
    RenderTexture.active = dest;
    float num1 = (float) ((Texture) noise).width * 1f;
    float num2 = 1f * (float) ((Texture) source).width / NoiseAndGrain.TILE_AMOUNT;
    fxMaterial.SetTexture("_MainTex", (Texture) source);
    GL.PushMatrix();
    GL.LoadOrtho();
    float num3 = (float) (1.0 * (double) ((Texture) source).width / (1.0 * (double) ((Texture) source).height));
    float num4 = 1f / num2;
    float num5 = num4 * num3;
    float num6 = num1 / ((float) ((Texture) noise).width * 1f);
    fxMaterial.SetPass(passNr);
    GL.Begin(7);
    for (float num7 = 0.0f; (double) num7 < 1.0; num7 += num4)
    {
      for (float num8 = 0.0f; (double) num8 < 1.0; num8 += num5)
      {
        float num9 = Random.Range(0.0f, 1f);
        float num10 = Random.Range(0.0f, 1f);
        float num11 = Mathf.Floor(num9 * num1) / num1;
        float num12 = Mathf.Floor(num10 * num1) / num1;
        float num13 = 1f / num1;
        GL.MultiTexCoord2(0, num11, num12);
        GL.MultiTexCoord2(1, 0.0f, 0.0f);
        GL.Vertex3(num7, num8, 0.1f);
        GL.MultiTexCoord2(0, num11 + num6 * num13, num12);
        GL.MultiTexCoord2(1, 1f, 0.0f);
        GL.Vertex3(num7 + num4, num8, 0.1f);
        GL.MultiTexCoord2(0, num11 + num6 * num13, num12 + num6 * num13);
        GL.MultiTexCoord2(1, 1f, 1f);
        GL.Vertex3(num7 + num4, num8 + num5, 0.1f);
        GL.MultiTexCoord2(0, num11, num12 + num6 * num13);
        GL.MultiTexCoord2(1, 0.0f, 1f);
        GL.Vertex3(num7, num8 + num5, 0.1f);
      }
    }
    GL.End();
    GL.PopMatrix();
  }
}
