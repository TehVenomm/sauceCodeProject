// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.PostEffectsBase
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
public class PostEffectsBase : MonoBehaviour
{
  protected bool supportHDRTextures = true;
  protected bool supportDX11;
  protected bool isSupported = true;

  protected Material CheckShaderAndCreateMaterial(Shader s, Material m2Create)
  {
    if (!Object.op_Implicit((Object) s))
    {
      Debug.Log((object) ("Missing shader in " + ((object) this).ToString()));
      ((Behaviour) this).enabled = false;
      return (Material) null;
    }
    if (s.isSupported && Object.op_Implicit((Object) m2Create) && Object.op_Equality((Object) m2Create.shader, (Object) s))
      return m2Create;
    if (!s.isSupported)
    {
      this.NotSupported();
      Debug.Log((object) $"The shader {s.ToString()} on effect {((object) this).ToString()} is not supported on this platform!");
      return (Material) null;
    }
    m2Create = new Material(s);
    ((Object) m2Create).hideFlags = (HideFlags) 52;
    return Object.op_Implicit((Object) m2Create) ? m2Create : (Material) null;
  }

  protected Material CreateMaterial(Shader s, Material m2Create)
  {
    if (!Object.op_Implicit((Object) s))
    {
      Debug.Log((object) ("Missing shader in " + ((object) this).ToString()));
      return (Material) null;
    }
    if (Object.op_Implicit((Object) m2Create) && Object.op_Equality((Object) m2Create.shader, (Object) s) && s.isSupported)
      return m2Create;
    if (!s.isSupported)
      return (Material) null;
    m2Create = new Material(s);
    ((Object) m2Create).hideFlags = (HideFlags) 52;
    return Object.op_Implicit((Object) m2Create) ? m2Create : (Material) null;
  }

  private void OnEnable() => this.isSupported = true;

  protected bool CheckSupport() => this.CheckSupport(false);

  public virtual bool CheckResources()
  {
    Debug.LogWarning((object) $"CheckResources () for {((object) this).ToString()} should be overwritten.");
    return this.isSupported;
  }

  protected void Start() => this.CheckResources();

  protected bool CheckSupport(bool needDepth)
  {
    this.isSupported = true;
    this.supportHDRTextures = SystemInfo.SupportsRenderTextureFormat((RenderTextureFormat) 2);
    this.supportDX11 = SystemInfo.graphicsShaderLevel >= 50 && SystemInfo.supportsComputeShaders;
    if (!SystemInfo.supportsImageEffects || !SystemInfo.supportsRenderTextures)
    {
      this.NotSupported();
      return false;
    }
    if (needDepth && !SystemInfo.SupportsRenderTextureFormat((RenderTextureFormat) 1))
    {
      this.NotSupported();
      return false;
    }
    if (needDepth)
    {
      Camera component = ((Component) this).GetComponent<Camera>();
      component.depthTextureMode = (DepthTextureMode) (component.depthTextureMode | 1);
    }
    return true;
  }

  protected bool CheckSupport(bool needDepth, bool needHdr)
  {
    if (!this.CheckSupport(needDepth))
      return false;
    if (!needHdr || this.supportHDRTextures)
      return true;
    this.NotSupported();
    return false;
  }

  public bool Dx11Support() => this.supportDX11;

  protected void ReportAutoDisable()
  {
    Debug.LogWarning((object) $"The image effect {((object) this).ToString()} has been disabled as it's not supported on the current platform.");
  }

  private bool CheckShader(Shader s)
  {
    Debug.Log((object) $"The shader {s.ToString()} on effect {((object) this).ToString()} is not part of the Unity 3.2+ effects suite anymore. For best performance and quality, please ensure you are using the latest Standard Assets Image Effects (Pro only) package.");
    if (s.isSupported)
      return false;
    this.NotSupported();
    return false;
  }

  protected void NotSupported()
  {
    ((Behaviour) this).enabled = false;
    this.isSupported = false;
  }

  protected void DrawBorder(RenderTexture dest, Material material)
  {
    RenderTexture.active = dest;
    bool flag = true;
    GL.PushMatrix();
    GL.LoadOrtho();
    for (int index = 0; index < material.passCount; ++index)
    {
      material.SetPass(index);
      float num1;
      float num2;
      if (flag)
      {
        num1 = 1f;
        num2 = 0.0f;
      }
      else
      {
        num1 = 0.0f;
        num2 = 1f;
      }
      double num3 = 0.0;
      float num4 = (float) (0.0 + 1.0 / ((double) ((Texture) dest).width * 1.0));
      float num5 = 0.0f;
      float num6 = 1f;
      GL.Begin(7);
      GL.TexCoord2(0.0f, num1);
      GL.Vertex3((float) num3, num5, 0.1f);
      GL.TexCoord2(1f, num1);
      GL.Vertex3(num4, num5, 0.1f);
      GL.TexCoord2(1f, num2);
      GL.Vertex3(num4, num6, 0.1f);
      GL.TexCoord2(0.0f, num2);
      GL.Vertex3((float) num3, num6, 0.1f);
      double num7 = 1.0 - 1.0 / ((double) ((Texture) dest).width * 1.0);
      float num8 = 1f;
      float num9 = 0.0f;
      float num10 = 1f;
      GL.TexCoord2(0.0f, num1);
      GL.Vertex3((float) num7, num9, 0.1f);
      GL.TexCoord2(1f, num1);
      GL.Vertex3(num8, num9, 0.1f);
      GL.TexCoord2(1f, num2);
      GL.Vertex3(num8, num10, 0.1f);
      GL.TexCoord2(0.0f, num2);
      GL.Vertex3((float) num7, num10, 0.1f);
      double num11 = 0.0;
      float num12 = 1f;
      float num13 = 0.0f;
      float num14 = (float) (0.0 + 1.0 / ((double) ((Texture) dest).height * 1.0));
      GL.TexCoord2(0.0f, num1);
      GL.Vertex3((float) num11, num13, 0.1f);
      GL.TexCoord2(1f, num1);
      GL.Vertex3(num12, num13, 0.1f);
      GL.TexCoord2(1f, num2);
      GL.Vertex3(num12, num14, 0.1f);
      GL.TexCoord2(0.0f, num2);
      GL.Vertex3((float) num11, num14, 0.1f);
      double num15 = 0.0;
      float num16 = 1f;
      float num17 = (float) (1.0 - 1.0 / ((double) ((Texture) dest).height * 1.0));
      float num18 = 1f;
      GL.TexCoord2(0.0f, num1);
      GL.Vertex3((float) num15, num17, 0.1f);
      GL.TexCoord2(1f, num1);
      GL.Vertex3(num16, num17, 0.1f);
      GL.TexCoord2(1f, num2);
      GL.Vertex3(num16, num18, 0.1f);
      GL.TexCoord2(0.0f, num2);
      GL.Vertex3((float) num15, num18, 0.1f);
      GL.End();
    }
    GL.PopMatrix();
  }
}
