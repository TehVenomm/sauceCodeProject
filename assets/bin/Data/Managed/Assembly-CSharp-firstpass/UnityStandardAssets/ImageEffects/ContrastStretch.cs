// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.ContrastStretch
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Contrast Stretch")]
public class ContrastStretch : MonoBehaviour
{
  [Range(0.0001f, 1f)]
  public float adaptationSpeed = 0.02f;
  [Range(0.0f, 1f)]
  public float limitMinimum = 0.2f;
  [Range(0.0f, 1f)]
  public float limitMaximum = 0.6f;
  private RenderTexture[] adaptRenderTex = new RenderTexture[2];
  private int curAdaptIndex;
  public Shader shaderLum;
  private Material m_materialLum;
  public Shader shaderReduce;
  private Material m_materialReduce;
  public Shader shaderAdapt;
  private Material m_materialAdapt;
  public Shader shaderApply;
  private Material m_materialApply;

  protected Material materialLum
  {
    get
    {
      if (Object.op_Equality((Object) this.m_materialLum, (Object) null))
      {
        this.m_materialLum = new Material(this.shaderLum);
        ((Object) this.m_materialLum).hideFlags = (HideFlags) 61;
      }
      return this.m_materialLum;
    }
  }

  protected Material materialReduce
  {
    get
    {
      if (Object.op_Equality((Object) this.m_materialReduce, (Object) null))
      {
        this.m_materialReduce = new Material(this.shaderReduce);
        ((Object) this.m_materialReduce).hideFlags = (HideFlags) 61;
      }
      return this.m_materialReduce;
    }
  }

  protected Material materialAdapt
  {
    get
    {
      if (Object.op_Equality((Object) this.m_materialAdapt, (Object) null))
      {
        this.m_materialAdapt = new Material(this.shaderAdapt);
        ((Object) this.m_materialAdapt).hideFlags = (HideFlags) 61;
      }
      return this.m_materialAdapt;
    }
  }

  protected Material materialApply
  {
    get
    {
      if (Object.op_Equality((Object) this.m_materialApply, (Object) null))
      {
        this.m_materialApply = new Material(this.shaderApply);
        ((Object) this.m_materialApply).hideFlags = (HideFlags) 61;
      }
      return this.m_materialApply;
    }
  }

  private void Start()
  {
    if (!SystemInfo.supportsImageEffects)
    {
      ((Behaviour) this).enabled = false;
    }
    else
    {
      if (this.shaderAdapt.isSupported && this.shaderApply.isSupported && this.shaderLum.isSupported && this.shaderReduce.isSupported)
        return;
      ((Behaviour) this).enabled = false;
    }
  }

  private void OnEnable()
  {
    for (int index = 0; index < 2; ++index)
    {
      if (!Object.op_Implicit((Object) this.adaptRenderTex[index]))
      {
        this.adaptRenderTex[index] = new RenderTexture(1, 1, 0);
        ((Object) this.adaptRenderTex[index]).hideFlags = (HideFlags) 61;
      }
    }
  }

  private void OnDisable()
  {
    for (int index = 0; index < 2; ++index)
    {
      Object.DestroyImmediate((Object) this.adaptRenderTex[index]);
      this.adaptRenderTex[index] = (RenderTexture) null;
    }
    if (Object.op_Implicit((Object) this.m_materialLum))
      Object.DestroyImmediate((Object) this.m_materialLum);
    if (Object.op_Implicit((Object) this.m_materialReduce))
      Object.DestroyImmediate((Object) this.m_materialReduce);
    if (Object.op_Implicit((Object) this.m_materialAdapt))
      Object.DestroyImmediate((Object) this.m_materialAdapt);
    if (!Object.op_Implicit((Object) this.m_materialApply))
      return;
    Object.DestroyImmediate((Object) this.m_materialApply);
  }

  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    RenderTexture curTexture = RenderTexture.GetTemporary(((Texture) source).width / 1, ((Texture) source).height / 1);
    Graphics.Blit((Texture) source, curTexture, this.materialLum);
    RenderTexture temporary;
    for (; ((Texture) curTexture).width > 1 || ((Texture) curTexture).height > 1; curTexture = temporary)
    {
      int num1 = ((Texture) curTexture).width / 2;
      if (num1 < 1)
        num1 = 1;
      int num2 = ((Texture) curTexture).height / 2;
      if (num2 < 1)
        num2 = 1;
      temporary = RenderTexture.GetTemporary(num1, num2);
      Graphics.Blit((Texture) curTexture, temporary, this.materialReduce);
      RenderTexture.ReleaseTemporary(curTexture);
    }
    this.CalculateAdaptation((Texture) curTexture);
    this.materialApply.SetTexture("_AdaptTex", (Texture) this.adaptRenderTex[this.curAdaptIndex]);
    Graphics.Blit((Texture) source, destination, this.materialApply);
    RenderTexture.ReleaseTemporary(curTexture);
  }

  private void CalculateAdaptation(Texture curTexture)
  {
    int curAdaptIndex = this.curAdaptIndex;
    this.curAdaptIndex = (this.curAdaptIndex + 1) % 2;
    float num = Mathf.Clamp(1f - Mathf.Pow(1f - this.adaptationSpeed, 30f * Time.deltaTime), 0.01f, 1f);
    this.materialAdapt.SetTexture("_CurTex", curTexture);
    this.materialAdapt.SetVector("_AdaptParams", new Vector4(num, this.limitMinimum, this.limitMaximum, 0.0f));
    Graphics.SetRenderTarget(this.adaptRenderTex[this.curAdaptIndex]);
    GL.Clear(false, true, Color.black);
    Graphics.Blit((Texture) this.adaptRenderTex[curAdaptIndex], this.adaptRenderTex[this.curAdaptIndex], this.materialAdapt);
  }
}
