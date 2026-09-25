// Decompiled with JetBrains decompiler
// Type: UIRenderTexture
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class UIRenderTexture : MonoBehaviour
{
  public const int BEGIN_LAYER = 24;
  private const int ID_MAX = 16 /*0x10*/;
  private const int CAMERA_DEPTH = 50;
  private static int idFlags;
  private int layer = -1;
  private int id = -1;
  private int texW;
  private int texH;
  private FilterMode filterMode;
  private FloatInterpolator alpha;

  public static bool ToRealSize(ref int w, ref int h)
  {
    float num1 = (float) Screen.width;
    float num2 = (float) Screen.height;
    if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
    {
      DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
      num1 = specialDeviceInfo.SafeArea.SafeWidth;
      num2 = specialDeviceInfo.SafeArea.SafeHeight;
    }
    float num3 = num1 / (float) MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth * (float) w;
    float num4 = num2 / (float) MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight * (float) h;
    bool realSize = false;
    if ((double) num3 > (double) num1 + 4.0)
    {
      num4 *= num1 / num3;
      num3 = num1;
      realSize = true;
    }
    if ((double) num4 > (double) num2 + 4.0)
    {
      num3 *= num2 / num4;
      num4 = num2;
      realSize = true;
    }
    w = Mathf.RoundToInt(num3);
    h = Mathf.RoundToInt(num4);
    return realSize;
  }

  public static UIRenderTexture Get(
    UITexture ui_texture,
    float fov = -1f,
    bool link_main_camera = false,
    int layer = -1)
  {
    if (Object.op_Equality((Object) ui_texture, (Object) null))
      return (UIRenderTexture) null;
    UIRenderTexture uiRenderTexture = ((Component) ui_texture).GetComponent<UIRenderTexture>();
    if (Object.op_Equality((Object) uiRenderTexture, (Object) null))
      uiRenderTexture = ((Component) ui_texture).gameObject.AddComponent<UIRenderTexture>();
    uiRenderTexture.uiTexture = ui_texture;
    uiRenderTexture.fov = fov;
    uiRenderTexture.linkMainCamera = link_main_camera;
    uiRenderTexture.layer = layer;
    uiRenderTexture.Init();
    return uiRenderTexture;
  }

  public UITexture uiTexture { get; private set; }

  public float fov { get; private set; }

  public float nearClipPlane { get; set; }

  public float farClipPlane { get; set; }

  public float orthographicSize { get; set; }

  public bool linkMainCamera { get; set; }

  public Transform renderTransform { get; private set; }

  public Transform modelTransform { get; private set; }

  public Camera renderCamera { get; private set; }

  public FilterBase postEffectFilter { get; set; }

  public PostEffector postEffector { get; private set; }

  private UIRenderTexture()
  {
    this.nearClipPlane = -1f;
    this.farClipPlane = 500f;
    this.orthographicSize = 0.0f;
  }

  private void Init()
  {
    if (Object.op_Inequality((Object) this.renderTransform, (Object) null))
      return;
    if (this.layer == -1)
    {
      if (this.id != -1)
        return;
      for (int index = 0; index < 16 /*0x10*/; ++index)
      {
        int num = 1 << index;
        if ((UIRenderTexture.idFlags & num) == 0)
        {
          UIRenderTexture.idFlags |= num;
          this.id = index;
          break;
        }
      }
      if (this.id == -1)
        return;
    }
    this.renderTransform = Utility.CreateGameObject("RenderTextureNode:" + (object) this.id, (Transform) null, this.renderLayer);
    int num1 = this.id >> 2;
    int num2 = this.id + 1 & 3;
    this.renderTransform.localPosition = this.linkMainCamera || this.layer != -1 ? Vector3.zero : new Vector3((float) (num1 * 50), (float) (num2 * -50) + MonoBehaviourSingleton<UIManager>.I._transform.position.y, 0.0f);
    this.renderTransform.parent = MonoBehaviourSingleton<UIManager>.I._transform;
    this.renderTransform.localScale = Vector3.one;
    this.modelTransform = Utility.CreateGameObject("ModelNode", (Transform) null, this.renderLayer);
    this.modelTransform.parent = this.renderTransform;
    this.modelTransform.localPosition = Vector3.zero;
    this.modelTransform.localEulerAngles = Vector3.zero;
  }

  public void Release()
  {
    this.Disable();
    if (!AppMain.isApplicationQuit && Object.op_Inequality((Object) this.renderTransform, (Object) null))
    {
      Object.Destroy((Object) ((Component) this.renderTransform).gameObject);
      this.renderTransform = (Transform) null;
    }
    if (this.id == -1)
      return;
    UIRenderTexture.idFlags &= ~(1 << this.id);
    this.id = -1;
  }

  private void OnEnable()
  {
    if (!Object.op_Inequality((Object) this.renderCamera, (Object) null))
      return;
    ((Behaviour) this.renderCamera).enabled = true;
    Utility.SetLayerWithChildren(((Component) this.renderCamera).transform, this.renderLayer);
  }

  private void OnDisable()
  {
    if (!Object.op_Inequality((Object) this.renderCamera, (Object) null))
      return;
    ((Behaviour) this.renderCamera).enabled = false;
  }

  private void OnDestroy()
  {
    this.StopAllCoroutines();
    this.Release();
  }

  private void CreateRenderTexture()
  {
    if (!Object.op_Equality((Object) this.renderCamera.targetTexture, (Object) null))
      return;
    RenderTexture renderTexture = new RenderTexture(this.texW, this.texH, 24);
    ((Object) renderTexture).name = "(UIRenderTexture)";
    ((Texture) renderTexture).filterMode = this.filterMode;
    renderTexture.Create();
    this.renderCamera.targetTexture = renderTexture;
    this.uiTexture.mainTexture = (Texture) renderTexture;
  }

  private void DeleteRenderTexture()
  {
    if (!Object.op_Inequality((Object) this.renderCamera.targetTexture, (Object) null))
      return;
    this.renderCamera.targetTexture.DiscardContents();
    Object.Destroy((Object) this.renderCamera.targetTexture);
    this.renderCamera.targetTexture = (RenderTexture) null;
    UIPanel panel = this.uiTexture.panel;
    this.uiTexture.mainTexture = (Texture) null;
    if (Object.op_Inequality((Object) this.uiTexture.drawCall, (Object) null))
    {
      this.uiTexture.drawCall.panel.drawCalls.Remove(this.uiTexture.drawCall);
      UIDrawCall.Destroy(this.uiTexture.drawCall);
      this.uiTexture.drawCall = (UIDrawCall) null;
    }
    else
    {
      if (!Object.op_Inequality((Object) panel, (Object) null))
        return;
      panel.ForceUpDate();
    }
  }

  public void Enable(float fadeTime = 0.25f)
  {
    this.Init();
    if (this.layer == -1 && this.id == -1 || Object.op_Inequality((Object) this.renderCamera, (Object) null))
      return;
    this.renderCamera = ((Component) this.renderTransform).gameObject.AddComponent<Camera>();
    this.renderCamera.depth = 50f;
    this.renderCamera.clearFlags = (CameraClearFlags) 2;
    this.renderCamera.backgroundColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    this.renderCamera.renderingPath = (RenderingPath) 1;
    this.renderCamera.cullingMask = 1 << this.renderLayer;
    if ((double) this.orthographicSize == 0.0)
    {
      if ((double) this.fov <= 0.0)
        this.fov = 10f;
      this.renderCamera.fieldOfView = this.fov;
    }
    else
    {
      this.renderCamera.orthographic = true;
      this.renderCamera.orthographicSize = this.orthographicSize;
    }
    if ((double) this.nearClipPlane == -1.0)
      this.nearClipPlane = 0.01f;
    this.renderCamera.nearClipPlane = this.nearClipPlane;
    this.renderCamera.farClipPlane = this.farClipPlane;
    if (Object.op_Inequality((Object) this.postEffectFilter, (Object) null))
    {
      this.postEffector = ((Component) this.renderTransform).gameObject.AddComponent<PostEffector>();
      this.postEffector.SetFilter(this.postEffectFilter);
    }
    if (Object.op_Inequality((Object) this.uiTexture, (Object) null))
    {
      this.texW = this.uiTexture.width;
      this.texH = this.uiTexture.height;
      this.filterMode = !UIRenderTexture.ToRealSize(ref this.texW, ref this.texH) ? (FilterMode) 0 : (FilterMode) 1;
    }
    else
      this.texW = this.texH = Mathf.Min(Screen.width, Screen.height);
    this.CreateRenderTexture();
    this.uiTexture.alpha = 0.0f;
    this.alpha = new FloatInterpolator();
    this.alpha.Set(fadeTime, 0.0f, 1f, Curves.easeLinear, 0.0f, (AnimationCurve) null);
    this.alpha.Play();
    Nexus6CrashWorkaround.Apply(this.renderCamera);
  }

  public void Disable()
  {
    if (this.layer == -1 && this.id == -1 || Object.op_Equality((Object) this.renderCamera, (Object) null))
      return;
    this.DeleteRenderTexture();
    Object.Destroy((Object) this.renderCamera);
    this.renderCamera = (Camera) null;
    Object.Destroy((Object) this.postEffector);
    this.postEffector = (PostEffector) null;
    this.alpha = (FloatInterpolator) null;
    this.uiTexture.alpha = 0.0f;
  }

  public void FadeOutDisable(float fadeTime = 0.25f)
  {
    this.StartCoroutine(this.DoFadeOutDisable(fadeTime));
  }

  private IEnumerator DoFadeOutDisable(float fadeTime)
  {
    this.uiTexture.alpha = 1f;
    this.alpha = new FloatInterpolator();
    this.alpha.Set(fadeTime, 1f, 0.0f, Curves.easeLinear, 0.0f, (AnimationCurve) null);
    this.alpha.Play();
    while (this.alpha.IsPlaying())
    {
      yield return (object) null;
      if (this.alpha == null)
        break;
    }
    this.Disable();
  }

  private void LateUpdate()
  {
    if (this.layer == -1 && this.id == -1)
      return;
    if (this.alpha != null)
    {
      this.uiTexture.alpha = this.alpha.Update();
      if (!this.alpha.IsPlaying())
        this.alpha = (FloatInterpolator) null;
    }
    if (!this.linkMainCamera || !Object.op_Inequality((Object) this.renderCamera, (Object) null))
      return;
    this.modelTransform.parent = (Transform) null;
    this.renderTransform.position = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.position;
    this.renderTransform.rotation = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.rotation;
    this.modelTransform.parent = this.renderTransform;
    this.renderCamera.fieldOfView = MonoBehaviourSingleton<AppMain>.I.mainCamera.fieldOfView;
  }

  public int renderLayer
  {
    get
    {
      if (this.layer != -1)
        return this.layer;
      return this.id == -1 ? 0 : 24 + (this.id & 3);
    }
  }

  public bool enableTexture
  {
    get => Object.op_Inequality((Object) this.renderCamera, (Object) null);
    set
    {
      if (value)
        this.Enable();
      else
        this.Disable();
    }
  }

  private void OnApplicationPause(bool pauseStatus)
  {
    if (pauseStatus || !MonoBehaviourSingleton<AppMain>.IsValid())
      return;
    MonoBehaviourSingleton<AppMain>.I.onDelayCall += new System.Action(this.CheckRenderCameraTarget);
  }

  private void CheckRenderCameraTarget()
  {
    if (!Object.op_Inequality((Object) this.uiTexture, (Object) null) || !Object.op_Inequality((Object) this.renderCamera, (Object) null) || !Object.op_Inequality((Object) this.uiTexture.mainTexture, (Object) null))
      return;
    RenderTexture mainTexture = this.uiTexture.mainTexture as RenderTexture;
    if (!Object.op_Inequality((Object) mainTexture, (Object) null))
      return;
    this.renderCamera.targetTexture = mainTexture;
  }
}
