// Decompiled with JetBrains decompiler
// Type: BlurFilter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BlurFilter : FilterBase
{
  [SerializeField]
  private float _blurStrength;
  [SerializeField]
  private float strengthLowLimit = 0.0001f;
  [SerializeField]
  private int downsample = 2;
  [SerializeField]
  private int iterationNum = 1;
  private const int PASS_NUM = 3;
  private Material[] _blurMaterial = new Material[3];
  private PostEffector postEffector;

  public float blurStrength
  {
    get => this._blurStrength;
    set => this._blurStrength = value;
  }

  public int downSample
  {
    get => this.downsample;
    set => this.downsample = value;
  }

  public Material[] blurMaterial => this._blurMaterial;

  private bool isValid
  {
    get
    {
      for (int index = 0; index < 3; ++index)
      {
        if (Object.op_Equality((Object) this._blurMaterial[index], (Object) null))
          return false;
      }
      return true;
    }
  }

  private void Awake()
  {
    this._blurMaterial[0] = this.CreateMaterial("Custom/UI/Blur_Pass0");
    this._blurMaterial[1] = this.CreateMaterial("Custom/UI/Blur_Pass1");
    this._blurMaterial[2] = this.CreateMaterial("Custom/UI/Blur_Pass2");
  }

  private Material CreateMaterial(string shaderName)
  {
    Shader shader = ResourceUtility.FindShader(shaderName);
    return Object.op_Equality((Object) shader, (Object) null) ? (Material) null : new Material(shader);
  }

  public override void StartFilter()
  {
    this.postEffector = ((Component) this).gameObject.AddComponent<PostEffector>();
    this.postEffector.SetFilter((FilterBase) this);
  }

  public override void StopFilter()
  {
    if (!Object.op_Inequality((Object) this.postEffector, (Object) null))
      return;
    Object.Destroy((Object) this.postEffector);
    this.postEffector = (PostEffector) null;
  }

  public override void PostEffectProc(RenderTexture src, RenderTexture dest)
  {
    if ((double) this.blurStrength <= (double) this.strengthLowLimit)
      Graphics.Blit((Texture) src, (RenderTexture) null);
    else if (!this.isValid)
    {
      Graphics.Blit((Texture) src, dest);
    }
    else
    {
      int num1 = ((Texture) src).width >> this.downsample;
      int num2 = ((Texture) src).height >> this.downsample;
      RenderTexture renderTexture1 = RenderTexture.GetTemporary(num1, num2, 0, src.format);
      ((Texture) renderTexture1).filterMode = (FilterMode) 1;
      Graphics.Blit((Texture) src, renderTexture1, this.blurMaterial[0]);
      float num3 = (float) (1.0 / (1.0 * (double) this.downsample));
      for (int index1 = 0; index1 < this.iterationNum; ++index1)
      {
        float num4 = (float) index1;
        for (int index2 = 0; index2 < 3; ++index2)
          this.blurMaterial[index2].SetVector("_Parameter", new Vector4(this.blurStrength * num3 + num4, -this.blurStrength * num3 - num4, 0.0f, 0.0f));
        RenderTexture temporary1 = RenderTexture.GetTemporary(num1, num2, 0, src.format);
        ((Texture) temporary1).filterMode = (FilterMode) 1;
        Graphics.Blit((Texture) renderTexture1, temporary1, this.blurMaterial[1]);
        RenderTexture.ReleaseTemporary(renderTexture1);
        RenderTexture renderTexture2 = temporary1;
        RenderTexture temporary2 = RenderTexture.GetTemporary(num1, num2, 0, src.format);
        ((Texture) temporary2).filterMode = (FilterMode) 1;
        Graphics.Blit((Texture) renderTexture2, temporary2, this.blurMaterial[2]);
        RenderTexture.ReleaseTemporary(renderTexture2);
        renderTexture1 = temporary2;
      }
      Graphics.Blit((Texture) renderTexture1, dest);
      RenderTexture.ReleaseTemporary(renderTexture1);
    }
  }
}
