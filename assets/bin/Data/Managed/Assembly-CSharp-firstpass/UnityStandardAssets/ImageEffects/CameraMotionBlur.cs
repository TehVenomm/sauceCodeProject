// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.CameraMotionBlur
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using System;
using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/Camera/Camera Motion Blur")]
public class CameraMotionBlur : PostEffectsBase
{
  private static float MAX_RADIUS = 10f;
  public CameraMotionBlur.MotionBlurFilter filterType = CameraMotionBlur.MotionBlurFilter.Reconstruction;
  public bool preview;
  public Vector3 previewScale = Vector3.one;
  public float movementScale;
  public float rotationScale = 1f;
  public float maxVelocity = 8f;
  public float minVelocity = 0.1f;
  public float velocityScale = 0.375f;
  public float softZDistance = 0.005f;
  public int velocityDownsample = 1;
  public LayerMask excludeLayers = LayerMask.op_Implicit(0);
  private GameObject tmpCam;
  public Shader shader;
  public Shader dx11MotionBlurShader;
  public Shader replacementClear;
  private Material motionBlurMaterial;
  private Material dx11MotionBlurMaterial;
  public Texture2D noiseTexture;
  public float jitter = 0.05f;
  public bool showVelocity;
  public float showVelocityScale = 1f;
  private Matrix4x4 currentViewProjMat;
  private Matrix4x4 prevViewProjMat;
  private int prevFrameCount;
  private bool wasActive;
  private Vector3 prevFrameForward = Vector3.forward;
  private Vector3 prevFrameUp = Vector3.up;
  private Vector3 prevFramePos = Vector3.zero;
  private Camera _camera;

  private void CalculateViewProjection()
  {
    Matrix4x4 worldToCameraMatrix = this._camera.worldToCameraMatrix;
    this.currentViewProjMat = Matrix4x4.op_Multiply(GL.GetGPUProjectionMatrix(this._camera.projectionMatrix, true), worldToCameraMatrix);
  }

  private new void Start()
  {
    this.CheckResources();
    if (Object.op_Equality((Object) this._camera, (Object) null))
      this._camera = ((Component) this).GetComponent<Camera>();
    this.wasActive = ((Component) this).gameObject.activeInHierarchy;
    this.CalculateViewProjection();
    this.Remember();
    this.wasActive = false;
  }

  private void OnEnable()
  {
    if (Object.op_Equality((Object) this._camera, (Object) null))
      this._camera = ((Component) this).GetComponent<Camera>();
    Camera camera = this._camera;
    camera.depthTextureMode = (DepthTextureMode) (camera.depthTextureMode | 1);
  }

  private void OnDisable()
  {
    if (Object.op_Inequality((Object) null, (Object) this.motionBlurMaterial))
    {
      Object.DestroyImmediate((Object) this.motionBlurMaterial);
      this.motionBlurMaterial = (Material) null;
    }
    if (Object.op_Inequality((Object) null, (Object) this.dx11MotionBlurMaterial))
    {
      Object.DestroyImmediate((Object) this.dx11MotionBlurMaterial);
      this.dx11MotionBlurMaterial = (Material) null;
    }
    if (!Object.op_Inequality((Object) null, (Object) this.tmpCam))
      return;
    Object.DestroyImmediate((Object) this.tmpCam);
    this.tmpCam = (GameObject) null;
  }

  public override bool CheckResources()
  {
    this.CheckSupport(true, true);
    this.motionBlurMaterial = this.CheckShaderAndCreateMaterial(this.shader, this.motionBlurMaterial);
    if (this.supportDX11 && this.filterType == CameraMotionBlur.MotionBlurFilter.ReconstructionDX11)
      this.dx11MotionBlurMaterial = this.CheckShaderAndCreateMaterial(this.dx11MotionBlurShader, this.dx11MotionBlurMaterial);
    if (!this.isSupported)
      this.ReportAutoDisable();
    return this.isSupported;
  }

  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (!this.CheckResources())
    {
      Graphics.Blit((Texture) source, destination);
    }
    else
    {
      if (this.filterType == CameraMotionBlur.MotionBlurFilter.CameraMotion)
        this.StartFrame();
      RenderTextureFormat renderTextureFormat = SystemInfo.SupportsRenderTextureFormat((RenderTextureFormat) 13) ? (RenderTextureFormat) 13 : (RenderTextureFormat) 2;
      RenderTexture temporary1 = RenderTexture.GetTemporary(CameraMotionBlur.divRoundUp(((Texture) source).width, this.velocityDownsample), CameraMotionBlur.divRoundUp(((Texture) source).height, this.velocityDownsample), 0, renderTextureFormat);
      this.maxVelocity = Mathf.Max(2f, this.maxVelocity);
      float maxVelocity = this.maxVelocity;
      bool flag = this.filterType == CameraMotionBlur.MotionBlurFilter.ReconstructionDX11 && Object.op_Equality((Object) this.dx11MotionBlurMaterial, (Object) null);
      int num1;
      int num2;
      float num3;
      if (this.filterType == CameraMotionBlur.MotionBlurFilter.Reconstruction | flag || this.filterType == CameraMotionBlur.MotionBlurFilter.ReconstructionDisc)
      {
        this.maxVelocity = Mathf.Min(this.maxVelocity, CameraMotionBlur.MAX_RADIUS);
        num1 = CameraMotionBlur.divRoundUp(((Texture) temporary1).width, (int) this.maxVelocity);
        num2 = CameraMotionBlur.divRoundUp(((Texture) temporary1).height, (int) this.maxVelocity);
        num3 = (float) (((Texture) temporary1).width / num1);
      }
      else
      {
        num1 = CameraMotionBlur.divRoundUp(((Texture) temporary1).width, (int) this.maxVelocity);
        num2 = CameraMotionBlur.divRoundUp(((Texture) temporary1).height, (int) this.maxVelocity);
        num3 = (float) (((Texture) temporary1).width / num1);
      }
      RenderTexture temporary2 = RenderTexture.GetTemporary(num1, num2, 0, renderTextureFormat);
      RenderTexture temporary3 = RenderTexture.GetTemporary(num1, num2, 0, renderTextureFormat);
      ((Texture) temporary1).filterMode = (FilterMode) 0;
      ((Texture) temporary2).filterMode = (FilterMode) 0;
      ((Texture) temporary3).filterMode = (FilterMode) 0;
      if (Object.op_Implicit((Object) this.noiseTexture))
        ((Texture) this.noiseTexture).filterMode = (FilterMode) 0;
      ((Texture) source).wrapMode = (TextureWrapMode) 1;
      ((Texture) temporary1).wrapMode = (TextureWrapMode) 1;
      ((Texture) temporary3).wrapMode = (TextureWrapMode) 1;
      ((Texture) temporary2).wrapMode = (TextureWrapMode) 1;
      this.CalculateViewProjection();
      if (((Component) this).gameObject.activeInHierarchy && !this.wasActive)
        this.Remember();
      this.wasActive = ((Component) this).gameObject.activeInHierarchy;
      Matrix4x4 matrix4x4 = Matrix4x4.Inverse(this.currentViewProjMat);
      this.motionBlurMaterial.SetMatrix("_InvViewProj", matrix4x4);
      this.motionBlurMaterial.SetMatrix("_PrevViewProj", this.prevViewProjMat);
      this.motionBlurMaterial.SetMatrix("_ToPrevViewProjCombined", Matrix4x4.op_Multiply(this.prevViewProjMat, matrix4x4));
      this.motionBlurMaterial.SetFloat("_MaxVelocity", num3);
      this.motionBlurMaterial.SetFloat("_MaxRadiusOrKInPaper", num3);
      this.motionBlurMaterial.SetFloat("_MinVelocity", this.minVelocity);
      this.motionBlurMaterial.SetFloat("_VelocityScale", this.velocityScale);
      this.motionBlurMaterial.SetFloat("_Jitter", this.jitter);
      this.motionBlurMaterial.SetTexture("_NoiseTex", (Texture) this.noiseTexture);
      this.motionBlurMaterial.SetTexture("_VelTex", (Texture) temporary1);
      this.motionBlurMaterial.SetTexture("_NeighbourMaxTex", (Texture) temporary3);
      this.motionBlurMaterial.SetTexture("_TileTexDebug", (Texture) temporary2);
      if (this.preview)
      {
        Matrix4x4 worldToCameraMatrix = this._camera.worldToCameraMatrix;
        Matrix4x4 identity = Matrix4x4.identity;
        ((Matrix4x4) ref identity).SetTRS(Vector3.op_Multiply(this.previewScale, 0.3333f), Quaternion.identity, Vector3.one);
        this.prevViewProjMat = Matrix4x4.op_Multiply(Matrix4x4.op_Multiply(GL.GetGPUProjectionMatrix(this._camera.projectionMatrix, true), identity), worldToCameraMatrix);
        this.motionBlurMaterial.SetMatrix("_PrevViewProj", this.prevViewProjMat);
        this.motionBlurMaterial.SetMatrix("_ToPrevViewProjCombined", Matrix4x4.op_Multiply(this.prevViewProjMat, matrix4x4));
      }
      if (this.filterType == CameraMotionBlur.MotionBlurFilter.CameraMotion)
      {
        Vector4 zero = Vector4.zero;
        float num4 = Vector3.Dot(((Component) this).transform.up, Vector3.up);
        Vector3 vector3 = Vector3.op_Subtraction(this.prevFramePos, ((Component) this).transform.position);
        double magnitude = (double) ((Vector3) ref vector3).magnitude;
        float num5 = (float) ((double) Vector3.Angle(((Component) this).transform.up, this.prevFrameUp) / (double) this._camera.fieldOfView * ((double) ((Texture) source).width * 0.75));
        zero.x = this.rotationScale * num5;
        float num6 = (float) ((double) Vector3.Angle(((Component) this).transform.forward, this.prevFrameForward) / (double) this._camera.fieldOfView * ((double) ((Texture) source).width * 0.75));
        zero.y = this.rotationScale * num4 * num6;
        float num7 = (float) ((double) Vector3.Angle(((Component) this).transform.forward, this.prevFrameForward) / (double) this._camera.fieldOfView * ((double) ((Texture) source).width * 0.75));
        zero.z = this.rotationScale * (1f - num4) * num7;
        double epsilon = (double) Mathf.Epsilon;
        if (magnitude > epsilon && (double) this.movementScale > (double) Mathf.Epsilon)
        {
          zero.w = (float) ((double) this.movementScale * (double) Vector3.Dot(((Component) this).transform.forward, vector3) * ((double) ((Texture) source).width * 0.5));
          zero.x += (float) ((double) this.movementScale * (double) Vector3.Dot(((Component) this).transform.up, vector3) * ((double) ((Texture) source).width * 0.5));
          zero.y += (float) ((double) this.movementScale * (double) Vector3.Dot(((Component) this).transform.right, vector3) * ((double) ((Texture) source).width * 0.5));
        }
        if (this.preview)
          this.motionBlurMaterial.SetVector("_BlurDirectionPacked", Vector4.op_Multiply(Vector4.op_Multiply(new Vector4(this.previewScale.y, this.previewScale.x, 0.0f, this.previewScale.z), 0.5f), this._camera.fieldOfView));
        else
          this.motionBlurMaterial.SetVector("_BlurDirectionPacked", zero);
      }
      else
      {
        Graphics.Blit((Texture) source, temporary1, this.motionBlurMaterial, 0);
        Camera camera = (Camera) null;
        if (((LayerMask) ref this.excludeLayers).value != 0)
          camera = this.GetTmpCam();
        if (Object.op_Implicit((Object) camera) && ((LayerMask) ref this.excludeLayers).value != 0 && Object.op_Implicit((Object) this.replacementClear) && this.replacementClear.isSupported)
        {
          camera.targetTexture = temporary1;
          camera.cullingMask = LayerMask.op_Implicit(this.excludeLayers);
          camera.RenderWithShader(this.replacementClear, "");
        }
      }
      if (!this.preview && Time.frameCount != this.prevFrameCount)
      {
        this.prevFrameCount = Time.frameCount;
        this.Remember();
      }
      ((Texture) source).filterMode = (FilterMode) 1;
      if (this.showVelocity)
      {
        this.motionBlurMaterial.SetFloat("_DisplayVelocityScale", this.showVelocityScale);
        Graphics.Blit((Texture) temporary1, destination, this.motionBlurMaterial, 1);
      }
      else if (this.filterType == CameraMotionBlur.MotionBlurFilter.ReconstructionDX11 && !flag)
      {
        this.dx11MotionBlurMaterial.SetFloat("_MinVelocity", this.minVelocity);
        this.dx11MotionBlurMaterial.SetFloat("_VelocityScale", this.velocityScale);
        this.dx11MotionBlurMaterial.SetFloat("_Jitter", this.jitter);
        this.dx11MotionBlurMaterial.SetTexture("_NoiseTex", (Texture) this.noiseTexture);
        this.dx11MotionBlurMaterial.SetTexture("_VelTex", (Texture) temporary1);
        this.dx11MotionBlurMaterial.SetTexture("_NeighbourMaxTex", (Texture) temporary3);
        this.dx11MotionBlurMaterial.SetFloat("_SoftZDistance", Mathf.Max(0.00025f, this.softZDistance));
        this.dx11MotionBlurMaterial.SetFloat("_MaxRadiusOrKInPaper", num3);
        Graphics.Blit((Texture) temporary1, temporary2, this.dx11MotionBlurMaterial, 0);
        Graphics.Blit((Texture) temporary2, temporary3, this.dx11MotionBlurMaterial, 1);
        Graphics.Blit((Texture) source, destination, this.dx11MotionBlurMaterial, 2);
      }
      else if (this.filterType == CameraMotionBlur.MotionBlurFilter.Reconstruction | flag)
      {
        this.motionBlurMaterial.SetFloat("_SoftZDistance", Mathf.Max(0.00025f, this.softZDistance));
        Graphics.Blit((Texture) temporary1, temporary2, this.motionBlurMaterial, 2);
        Graphics.Blit((Texture) temporary2, temporary3, this.motionBlurMaterial, 3);
        Graphics.Blit((Texture) source, destination, this.motionBlurMaterial, 4);
      }
      else if (this.filterType == CameraMotionBlur.MotionBlurFilter.CameraMotion)
        Graphics.Blit((Texture) source, destination, this.motionBlurMaterial, 6);
      else if (this.filterType == CameraMotionBlur.MotionBlurFilter.ReconstructionDisc)
      {
        this.motionBlurMaterial.SetFloat("_SoftZDistance", Mathf.Max(0.00025f, this.softZDistance));
        Graphics.Blit((Texture) temporary1, temporary2, this.motionBlurMaterial, 2);
        Graphics.Blit((Texture) temporary2, temporary3, this.motionBlurMaterial, 3);
        Graphics.Blit((Texture) source, destination, this.motionBlurMaterial, 7);
      }
      else
        Graphics.Blit((Texture) source, destination, this.motionBlurMaterial, 5);
      RenderTexture.ReleaseTemporary(temporary1);
      RenderTexture.ReleaseTemporary(temporary2);
      RenderTexture.ReleaseTemporary(temporary3);
    }
  }

  private void Remember()
  {
    this.prevViewProjMat = this.currentViewProjMat;
    this.prevFrameForward = ((Component) this).transform.forward;
    this.prevFrameUp = ((Component) this).transform.up;
    this.prevFramePos = ((Component) this).transform.position;
  }

  private Camera GetTmpCam()
  {
    if (Object.op_Equality((Object) this.tmpCam, (Object) null))
    {
      string str = $"_{((Object) this._camera).name}_MotionBlurTmpCam";
      GameObject gameObject = GameObject.Find(str);
      if (Object.op_Equality((Object) null, (Object) gameObject))
        this.tmpCam = new GameObject(str, new Type[1]
        {
          typeof (Camera)
        });
      else
        this.tmpCam = gameObject;
    }
    ((Object) this.tmpCam).hideFlags = (HideFlags) 52;
    this.tmpCam.transform.position = ((Component) this._camera).transform.position;
    this.tmpCam.transform.rotation = ((Component) this._camera).transform.rotation;
    this.tmpCam.transform.localScale = ((Component) this._camera).transform.localScale;
    this.tmpCam.GetComponent<Camera>().CopyFrom(this._camera);
    ((Behaviour) this.tmpCam.GetComponent<Camera>()).enabled = false;
    this.tmpCam.GetComponent<Camera>().depthTextureMode = (DepthTextureMode) 0;
    this.tmpCam.GetComponent<Camera>().clearFlags = (CameraClearFlags) 4;
    return this.tmpCam.GetComponent<Camera>();
  }

  private void StartFrame()
  {
    this.prevFramePos = Vector3.Slerp(this.prevFramePos, ((Component) this).transform.position, 0.75f);
  }

  private static int divRoundUp(int x, int d) => (x + d - 1) / d;

  public enum MotionBlurFilter
  {
    CameraMotion,
    LocalBlur,
    Reconstruction,
    ReconstructionDX11,
    ReconstructionDisc,
  }
}
