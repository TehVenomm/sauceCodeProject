// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Screenshot.ScreenshotTaker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace AlmostEngine.Screenshot;

[ExecuteInEditMode]
public class ScreenshotTaker : MonoBehaviour
{
  public Texture2D m_Texture;
  [Tooltip("In gameview resizing mode, the number of frames the screenshot taker waits before to take the screenshot after the gameview has been resized. The default value of 2 should be enough for most settings. Increase this number when some elements are not well updated, like GUI, or when you see some post effects artefacts. Post effects like temporal anti aliasing requier a value of at least 10.")]
  public ScreenshotTaker.GameViewResizingWaitingMode m_GameViewResizingWaitingMode;
  public float m_GameViewResizingWaitingTime = 1f;
  public int m_GameViewResizingWaitingFrames = 2;
  private Dictionary<ScreenshotResolution, RenderTexture> m_RenderTextureCache = new Dictionary<ScreenshotResolution, RenderTexture>();
  [HideInInspector]
  public static bool m_IsRunning = false;
  private List<ScreenshotCamera> m_Cameras = new List<ScreenshotCamera>();
  private List<ScreenshotCamera> m_SceneCameras = new List<ScreenshotCamera>();
  private List<ScreenshotOverlay> m_Overlays = new List<ScreenshotOverlay>();
  private List<ScreenshotOverlay> m_SceneOverlays = new List<ScreenshotOverlay>();
  public static ScreenshotTaker.UpdateDelegate onResolutionUpdateStartDelegate = (ScreenshotTaker.UpdateDelegate) (res => { });
  public static ScreenshotTaker.UpdateDelegate onResolutionUpdateEndDelegate = (ScreenshotTaker.UpdateDelegate) (res => { });
  public static ScreenshotTaker.UpdateDelegate onResolutionScreenResizedDelegate = (ScreenshotTaker.UpdateDelegate) (res => { });
  private float m_PreviousTimeScale = 1f;
  private Dictionary<ScreenshotCamera, Camera> m_CameraClones = new Dictionary<ScreenshotCamera, Camera>();

  private void Update()
  {
  }

  public void Reset()
  {
    this.StopAllCoroutines();
    this.RestoreTime();
    this.RestoreSettings();
    ScreenshotTaker.m_IsRunning = false;
  }

  public void ClearCache() => this.m_RenderTextureCache.Clear();

  public IEnumerator CaptureScreenToTextureCoroutine(
    Texture2D texture,
    bool captureGameUI = true,
    ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
    bool recomputeAlphaMask = false)
  {
    Vector2 currentGameViewSize = GameViewController.GetCurrentGameViewSize();
    yield return (object) this.StartCoroutine(this.CaptureToTextureCoroutine(texture, (int) currentGameViewSize.x, (int) currentGameViewSize.y, captureMode: ScreenshotTaker.CaptureMode.FIXED_GAMEVIEW, captureGameUI: captureGameUI, colorFormat: colorFormat, recomputeAlphaMask: recomputeAlphaMask));
  }

  public IEnumerator CaptureCamerasToTextureCoroutine(
    Texture2D texture,
    int width,
    int height,
    List<Camera> cameras,
    int antiAliasing = 8,
    ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
    bool recomputeAlphaMask = false)
  {
    yield return (object) this.StartCoroutine(this.CaptureToTextureCoroutine(texture, width, height, cameras, antiAliasing: antiAliasing, colorFormat: colorFormat, recomputeAlphaMask: recomputeAlphaMask));
  }

  public IEnumerator CaptureToTextureCoroutine(
    Texture2D texture,
    int width,
    int height,
    List<Camera> cameras = null,
    List<Canvas> canvas = null,
    ScreenshotTaker.CaptureMode captureMode = ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE,
    int antiAliasing = 8,
    bool captureGameUI = true,
    ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
    bool recomputeAlphaMask = false)
  {
    if (Object.op_Equality((Object) texture, (Object) null))
    {
      Debug.LogError((object) "The texture can not be null. You must provide a texture initialized with any width and height.");
    }
    else
    {
      ScreenshotResolution screenshotResolution = new ScreenshotResolution(width, height);
      screenshotResolution.m_Texture = texture;
      List<ScreenshotCamera> cameras1 = new List<ScreenshotCamera>();
      if (cameras != null)
      {
        foreach (Camera camera in cameras)
        {
          ScreenshotCamera screenshotCamera = new ScreenshotCamera(camera);
          cameras1.Add(screenshotCamera);
        }
      }
      List<ScreenshotOverlay> overlays = new List<ScreenshotOverlay>();
      if (canvas != null)
      {
        foreach (Canvas canva in canvas)
        {
          ScreenshotOverlay screenshotOverlay = new ScreenshotOverlay(canva);
          overlays.Add(screenshotOverlay);
        }
      }
      yield return (object) this.StartCoroutine(this.CaptureAllCoroutine(new List<ScreenshotResolution>()
      {
        screenshotResolution
      }, cameras1, overlays, captureMode, antiAliasing, captureGameUI, colorFormat, recomputeAlphaMask));
    }
  }

  public IEnumerator CaptureAllCoroutine(
    List<ScreenshotResolution> resolutions,
    List<ScreenshotCamera> cameras,
    List<ScreenshotOverlay> overlays,
    ScreenshotTaker.CaptureMode captureMode,
    int antiAliasing = 8,
    bool captureGameUI = true,
    ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
    bool recomputeAlphaMask = false,
    bool stopTime = false,
    bool restore = true)
  {
    if (resolutions == null)
      Debug.LogError((object) "Resolution list is null.");
    else if (cameras == null)
      Debug.LogError((object) "Cameras list is null.");
    else if (overlays == null)
      Debug.LogError((object) "Overlays list is null.");
    else if (captureMode == ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE && !UnityVersion.HasPro())
    {
      Debug.LogError((object) "RENDER_TO_TEXTURE requires Unity Pro or Unity 5.0 and later.");
    }
    else
    {
      while (ScreenshotTaker.m_IsRunning)
        yield return (object) null;
      if (captureMode == ScreenshotTaker.CaptureMode.GAMEVIEW_RESIZING)
      {
        Debug.LogError((object) "GAMEVIEW_RESIZING capture mode is only available for Editor and Windows Standalone.");
      }
      else
      {
        ScreenshotTaker.m_IsRunning = true;
        if (Application.isPlaying & stopTime)
          this.StopTime();
        this.ApplySettings(cameras, overlays, captureMode, captureGameUI);
        if (captureMode == ScreenshotTaker.CaptureMode.GAMEVIEW_RESIZING)
          GameViewController.SaveCurrentGameViewSize();
        foreach (ScreenshotResolution resolution in resolutions)
          yield return (object) this.StartCoroutine(this.CaptureResolutionTextureCoroutine(resolution, captureMode, antiAliasing, colorFormat, recomputeAlphaMask));
        if (restore && captureMode == ScreenshotTaker.CaptureMode.GAMEVIEW_RESIZING)
          GameViewController.RestoreGameViewSize();
        if (Application.isPlaying & stopTime)
          this.RestoreTime();
        if (Application.isEditor | restore)
          this.RestoreSettings();
        ScreenshotTaker.m_IsRunning = false;
      }
    }
  }

  private IEnumerator CaptureResolutionTextureCoroutine(
    ScreenshotResolution resolution,
    ScreenshotTaker.CaptureMode captureMode,
    int antiAliasing,
    ScreenshotTaker.ColorFormat colorFormat,
    bool recomputeAlphaMask)
  {
    if (resolution.IsValid())
    {
      ScreenshotTaker.onResolutionUpdateStartDelegate(resolution);
      this.m_Texture = this.GetOrCreateTexture(resolution, colorFormat, captureMode == ScreenshotTaker.CaptureMode.FIXED_GAMEVIEW);
      switch (captureMode)
      {
        case ScreenshotTaker.CaptureMode.GAMEVIEW_RESIZING:
          GameViewController.SetGameViewSize(((Texture) this.m_Texture).width, ((Texture) this.m_Texture).height);
          yield return (object) new WaitForEndOfFrame();
          if (!Application.isPlaying)
          {
            if (MultiDisplayUtils.IsMultiDisplay())
              yield return (object) this.MultiDisplayCopyRenderBufferToTextureCoroutine(this.m_Texture);
            else
              this.CopyScreenToTexture(this.m_Texture);
          }
          if (this.m_GameViewResizingWaitingMode == ScreenshotTaker.GameViewResizingWaitingMode.FRAMES || !Application.isPlaying)
          {
            for (int i = 0; i < this.m_GameViewResizingWaitingFrames; ++i)
            {
              GameViewController.SetGameViewSize(((Texture) this.m_Texture).width, ((Texture) this.m_Texture).height);
              yield return (object) new WaitForEndOfFrame();
            }
          }
          else
          {
            yield return (object) new WaitForSecondsRealtime(this.m_GameViewResizingWaitingTime);
            yield return (object) new WaitForEndOfFrame();
          }
          ScreenshotTaker.onResolutionScreenResizedDelegate(resolution);
          if (MultiDisplayUtils.IsMultiDisplay())
          {
            yield return (object) this.MultiDisplayCopyRenderBufferToTextureCoroutine(this.m_Texture);
            break;
          }
          this.CopyScreenToTexture(this.m_Texture);
          break;
        case ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE:
          yield return (object) new WaitForEndOfFrame();
          this.RenderCamerasToTexture(this.m_Cameras, this.m_Texture, this.GetOrCreateRenderTexture(resolution, antiAliasing));
          break;
        case ScreenshotTaker.CaptureMode.FIXED_GAMEVIEW:
          yield return (object) new WaitForEndOfFrame();
          if (MultiDisplayUtils.IsMultiDisplay())
          {
            yield return (object) this.MultiDisplayCopyRenderBufferToTextureCoroutine(this.m_Texture);
            break;
          }
          this.CopyScreenToTexture(this.m_Texture);
          break;
      }
      if (colorFormat == ScreenshotTaker.ColorFormat.RGBA & recomputeAlphaMask)
        yield return (object) this.StartCoroutine(this.RecomputeAlphaMask(resolution, this.m_Cameras, captureMode));
      ScreenshotTaker.onResolutionUpdateEndDelegate(resolution);
    }
  }

  public void CopyScreenToTexture(Texture2D targetTexture)
  {
    targetTexture.ReadPixels(new Rect(0.0f, 0.0f, (float) ((Texture) targetTexture).width, (float) ((Texture) targetTexture).height), 0, 0);
    targetTexture.Apply(false);
  }

  public void RenderCamerasToTexture(
    List<ScreenshotCamera> cameras,
    Texture2D targetTexture,
    RenderTexture renderTexture)
  {
    RenderTexture active = RenderTexture.active;
    RenderTexture.active = renderTexture;
    foreach (ScreenshotCamera camera in cameras)
    {
      if (camera.m_Active && !Object.op_Equality((Object) camera.m_Camera, (Object) null) && ((Behaviour) camera.m_Camera).enabled)
      {
        RenderTexture targetTexture1 = camera.m_Camera.targetTexture;
        camera.m_Camera.targetTexture = renderTexture;
        camera.m_Camera.Render();
        camera.m_Camera.targetTexture = targetTexture1;
      }
    }
    targetTexture.ReadPixels(new Rect(0.0f, 0.0f, (float) ((Texture) renderTexture).width, (float) ((Texture) renderTexture).height), 0, 0);
    targetTexture.Apply(false);
    RenderTexture.active = active;
  }

  private Camera GetLastActiveCamera()
  {
    for (int index = this.m_Cameras.Count - 1; index >= 0; --index)
    {
      if (this.m_Cameras[index].m_Active && Object.op_Inequality((Object) this.m_Cameras[index].m_Camera, (Object) null) && ((Behaviour) this.m_Cameras[index].m_Camera).enabled)
        return this.m_Cameras[index].m_Camera;
    }
    Camera[] objectsOfType = Object.FindObjectsOfType<Camera>();
    Camera camera1 = (Camera) null;
    foreach (Camera camera2 in objectsOfType)
    {
      if (((Behaviour) camera2).enabled && camera2.targetDisplay == 0)
        camera1 = !Object.op_Inequality((Object) camera1, (Object) null) || (double) camera2.depth <= (double) camera1.depth ? camera2 : camera2;
    }
    return Object.op_Inequality((Object) camera1, (Object) null) ? camera1 : Camera.main;
  }

  private IEnumerator MultiDisplayCopyRenderBufferToTextureCoroutine(Texture2D targetTexture)
  {
    Camera lastActiveCamera = this.GetLastActiveCamera();
    if (Object.op_Inequality((Object) lastActiveCamera, (Object) null))
    {
      if (Object.op_Equality((Object) ((Component) lastActiveCamera).GetComponent<MultiDisplayCameraCapture>(), (Object) null))
        ((Component) lastActiveCamera).gameObject.AddComponent<MultiDisplayCameraCapture>();
      MultiDisplayCameraCapture capture = ((Component) lastActiveCamera).GetComponent<MultiDisplayCameraCapture>();
      capture.CaptureCamera(targetTexture);
      while (!capture.CopyIsOver())
        yield return (object) null;
      Object.DestroyImmediate((Object) capture);
      capture = (MultiDisplayCameraCapture) null;
    }
    else
      this.CopyScreenToTexture(targetTexture);
  }

  private Texture2D GetOrCreateTexture(
    ScreenshotResolution resolution,
    ScreenshotTaker.ColorFormat colorFormat,
    bool noScale = false)
  {
    int num1 = noScale ? resolution.m_Width : resolution.ComputeTargetWidth();
    int num2 = noScale ? resolution.m_Height : resolution.ComputeTargetHeight();
    if (Object.op_Equality((Object) resolution.m_Texture, (Object) null))
      resolution.m_Texture = new Texture2D(num1, num2, colorFormat == ScreenshotTaker.ColorFormat.RGBA ? (TextureFormat) 5 : (TextureFormat) 3, false);
    else if (((Texture) resolution.m_Texture).width != num1 || ((Texture) resolution.m_Texture).height != num2 || resolution.m_Texture.format == 5 && colorFormat != ScreenshotTaker.ColorFormat.RGBA || resolution.m_Texture.format == 3 && colorFormat != ScreenshotTaker.ColorFormat.RGB)
      resolution.m_Texture.Resize(num1, num2, colorFormat == ScreenshotTaker.ColorFormat.RGBA ? (TextureFormat) 5 : (TextureFormat) 3, false);
    return resolution.m_Texture;
  }

  private RenderTexture GetOrCreateRenderTexture(ScreenshotResolution resolution, int antiAliasing = 0)
  {
    int targetWidth = resolution.ComputeTargetWidth();
    int targetHeight = resolution.ComputeTargetHeight();
    if (!this.m_RenderTextureCache.ContainsKey(resolution) || Object.op_Equality((Object) this.m_RenderTextureCache[resolution], (Object) null) || ((Texture) this.m_RenderTextureCache[resolution]).width != targetWidth || ((Texture) this.m_RenderTextureCache[resolution]).height != targetHeight || this.m_RenderTextureCache[resolution].antiAliasing != antiAliasing)
    {
      this.m_RenderTextureCache[resolution] = new RenderTexture(targetWidth, targetHeight, 32 /*0x20*/, (RenderTextureFormat) 0);
      if (antiAliasing != 0)
        this.m_RenderTextureCache[resolution].antiAliasing = antiAliasing;
    }
    return this.m_RenderTextureCache[resolution];
  }

  public void ApplySettings(
    List<ScreenshotCamera> cameras,
    List<ScreenshotOverlay> overlays,
    ScreenshotTaker.CaptureMode captureMode,
    bool renderUI)
  {
    this.m_Cameras = cameras;
    this.m_SceneCameras = this.FindAllOtherSceneCameras(this.m_Cameras);
    if (captureMode != ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE && this.m_Cameras.Count > 0)
      this.DisableCameras(this.m_SceneCameras);
    this.ApplyCameraSettings(this.m_Cameras, captureMode);
    this.m_Overlays = overlays;
    this.m_SceneOverlays = this.FindAllOtherSceneCanvas(this.m_Overlays);
    if (captureMode != ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE && !renderUI)
      this.DisableCanvas(this.m_SceneOverlays);
    this.ApplyOverlaySettings(this.m_Overlays);
  }

  private void RestoreSettings()
  {
    this.RestoreCameraSettings(this.m_Cameras);
    this.RestoreCameraSettings(this.m_SceneCameras);
    this.RestoreOverlaySettings(this.m_Overlays);
    this.RestoreOverlaySettings(this.m_SceneOverlays);
  }

  private void StopTime()
  {
    this.m_PreviousTimeScale = Time.timeScale;
    Time.timeScale = 0.0f;
  }

  private void RestoreTime() => Time.timeScale = this.m_PreviousTimeScale;

  private List<ScreenshotOverlay> FindAllOtherSceneCanvas(List<ScreenshotOverlay> overlays)
  {
    List<ScreenshotOverlay> otherSceneCanvas = new List<ScreenshotOverlay>();
    foreach (Canvas canvas in Object.FindObjectsOfType<Canvas>())
    {
      if (((Component) canvas).gameObject.activeInHierarchy && ((Behaviour) canvas).enabled)
      {
        bool flag = false;
        foreach (ScreenshotOverlay overlay in overlays)
        {
          if (Object.op_Equality((Object) overlay.m_Canvas, (Object) canvas) && overlay.m_Active)
            flag = true;
        }
        if (!flag)
          otherSceneCanvas.Add(new ScreenshotOverlay(canvas));
      }
    }
    return otherSceneCanvas;
  }

  private void DisableCanvas(List<ScreenshotOverlay> overlays)
  {
    if (overlays == null)
      return;
    foreach (ScreenshotOverlay overlay in overlays)
      overlay?.Disable();
  }

  private void ApplyOverlaySettings(List<ScreenshotOverlay> overlays)
  {
    if (overlays == null)
      return;
    foreach (ScreenshotOverlay overlay in overlays)
    {
      if (overlay != null && overlay.m_Active && Object.op_Inequality((Object) overlay.m_Canvas, (Object) null))
        overlay.ApplySettings();
    }
  }

  public void RestoreOverlaySettings(List<ScreenshotOverlay> overlays)
  {
    if (overlays == null)
      return;
    foreach (ScreenshotOverlay overlay in overlays)
    {
      if (overlay != null && overlay.m_Active && Object.op_Inequality((Object) overlay.m_Canvas, (Object) null))
        overlay.RestoreSettings();
    }
  }

  private List<ScreenshotCamera> FindAllOtherSceneCameras(List<ScreenshotCamera> cameras)
  {
    List<ScreenshotCamera> otherSceneCameras = new List<ScreenshotCamera>();
    foreach (Camera cam in Object.FindObjectsOfType<Camera>())
    {
      bool flag = false;
      foreach (ScreenshotCamera camera in cameras)
      {
        if (Object.op_Equality((Object) camera.m_Camera, (Object) cam) && camera.m_Active)
          flag = true;
      }
      if (!flag)
        otherSceneCameras.Add(new ScreenshotCamera(cam));
    }
    return otherSceneCameras;
  }

  private void DisableCameras(List<ScreenshotCamera> cameras)
  {
    if (cameras == null)
      return;
    foreach (ScreenshotCamera camera in cameras)
      camera?.Disable();
  }

  public void RestoreCameraSettings(List<ScreenshotCamera> cameras)
  {
    if (cameras == null)
      return;
    foreach (ScreenshotCamera camera in cameras)
    {
      if (camera != null && camera.m_Active && !Object.op_Equality((Object) camera.m_Camera, (Object) null))
        camera.RestoreSettings();
    }
  }

  private void ApplyCameraSettings(
    List<ScreenshotCamera> cameras,
    ScreenshotTaker.CaptureMode captureMode)
  {
    if (cameras == null)
      return;
    foreach (ScreenshotCamera camera in cameras)
    {
      if (camera != null && camera.m_Active && !Object.op_Equality((Object) camera.m_Camera, (Object) null))
        camera.ApplySettings(captureMode == ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE);
    }
  }

  private Camera CreateOrGetCameraClone(ScreenshotCamera camera)
  {
    if (!this.m_CameraClones.ContainsKey(camera) || Object.op_Equality((Object) this.m_CameraClones[camera], (Object) null))
    {
      Camera camera1 = new GameObject()
      {
        transform = {
          position = ((Component) camera.m_Camera).transform.position,
          rotation = ((Component) camera.m_Camera).transform.rotation,
          localScale = ((Component) camera.m_Camera).transform.localScale
        }
      }.AddComponent<Camera>();
      camera1.CopyFrom(camera.m_Camera);
      this.m_CameraClones[camera] = camera1;
    }
    return this.m_CameraClones[camera];
  }

  private IEnumerator RecomputeAlphaMask(
    ScreenshotResolution resolution,
    List<ScreenshotCamera> cameras,
    ScreenshotTaker.CaptureMode captureMode)
  {
    Texture2D texture = resolution.m_Texture;
    Texture2D mask = new Texture2D(((Texture) texture).width, ((Texture) texture).height, (TextureFormat) 5, false);
    List<Camera> cameraClones = new List<Camera>();
    List<ScreenshotCamera> screenshotCameraList = cameras;
    if (cameras.Count == 0)
      screenshotCameraList = this.m_SceneCameras;
    foreach (ScreenshotCamera camera in screenshotCameraList)
    {
      if (!Object.op_Equality((Object) camera.m_Camera, (Object) null) && camera.m_Active)
      {
        Camera orGetCameraClone = this.CreateOrGetCameraClone(camera);
        orGetCameraClone.ResetAspect();
        cameraClones.Add(orGetCameraClone);
      }
    }
    if (captureMode == ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE)
    {
      RenderTexture renderTexture = this.GetOrCreateRenderTexture(resolution);
      foreach (Camera camera in cameraClones)
      {
        camera.targetTexture = renderTexture;
        camera.Render();
        camera.targetTexture = (RenderTexture) null;
      }
      RenderTexture active = RenderTexture.active;
      RenderTexture.active = renderTexture;
      mask.ReadPixels(new Rect(0.0f, 0.0f, (float) ((Texture) mask).width, (float) ((Texture) mask).height), 0, 0);
      mask.Apply(false);
      RenderTexture.active = active;
    }
    else
    {
      if (cameras.Count == 0)
        this.DisableCameras(this.m_SceneCameras);
      else
        this.DisableCameras(cameras);
      yield return (object) new WaitForEndOfFrame();
      mask.ReadPixels(new Rect(0.0f, 0.0f, (float) ((Texture) mask).width, (float) ((Texture) mask).height), 0, 0);
      mask.Apply(false);
      if (cameras.Count == 0)
        this.RestoreCameraSettings(this.m_SceneCameras);
      else
        this.RestoreCameraSettings(cameras);
    }
    foreach (Component component in cameraClones)
      Object.DestroyImmediate((Object) component.gameObject);
    for (int index1 = 0; index1 < ((Texture) mask).width; ++index1)
    {
      for (int index2 = 0; index2 < ((Texture) mask).height; ++index2)
      {
        Color pixel = texture.GetPixel(index1, index2);
        pixel.a = mask.GetPixel(index1, index2).a;
        texture.SetPixel(index1, index2, pixel);
      }
    }
  }

  public enum ColorFormat
  {
    RGB,
    RGBA,
  }

  public enum CaptureMode
  {
    GAMEVIEW_RESIZING,
    RENDER_TO_TEXTURE,
    FIXED_GAMEVIEW,
  }

  public enum GameViewResizingWaitingMode
  {
    FRAMES,
    TIME,
  }

  public delegate void UpdateDelegate(ScreenshotResolution res);
}
