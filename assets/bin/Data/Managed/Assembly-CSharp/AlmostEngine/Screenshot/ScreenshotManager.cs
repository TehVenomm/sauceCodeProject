// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Screenshot.ScreenshotManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace AlmostEngine.Screenshot;

[ExecuteInEditMode]
[RequireComponent(typeof (AudioSource))]
public class ScreenshotManager : MonoBehaviour
{
  public ScreenshotConfig m_Config = new ScreenshotConfig();
  protected ScreenshotTaker m_ScreenshotCature;
  public bool m_IsBurstActive;
  public bool m_IsCapturing;
  public static UnityAction onCaptureBeginDelegate = new UnityAction((object) ScreenshotManager.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ecctor\u003Eb__35_0));
  public static UnityAction onCaptureEndDelegate = new UnityAction((object) ScreenshotManager.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ecctor\u003Eb__35_1));
  public static ScreenshotManager.ExportDelegate onResolutionExportSuccessDelegate = (ScreenshotManager.ExportDelegate) (res => { });
  public static ScreenshotManager.ExportDelegate onResolutionExportFailureDelegate = (ScreenshotManager.ExportDelegate) (res => { });
  private List<ScreenshotResolution> m_PreviewList = new List<ScreenshotResolution>();
  private List<ScreenshotOverlay> m_PreviewOverlayList = new List<ScreenshotOverlay>();
  private ScreenshotOverlay m_GuidesOverlay;

  public void Awake()
  {
    this.Reset();
    this.ClearCache();
    if (Application.isPlaying)
      Object.DontDestroyOnLoad((Object) ((Component) this).gameObject);
    if (!Application.isPlaying || !this.m_Config.m_PreviewInGameViewWhilePlaying)
      return;
    this.InitIngamePreview();
  }

  private void OnDestroy()
  {
  }

  public void Reset()
  {
    this.StopAllCoroutines();
    this.m_IsCapturing = false;
    this.m_IsBurstActive = false;
    this.InitScreenshotCapture();
  }

  public void ClearCache()
  {
    this.m_Config.ClearCache();
    if (!Object.op_Inequality((Object) this.m_ScreenshotCature, (Object) null))
      return;
    this.m_ScreenshotCature.ClearCache();
  }

  private void Update()
  {
    if (!Application.isPlaying)
      return;
    this.HandleHotkeys();
  }

  protected void InitScreenshotCapture()
  {
    if (Object.op_Inequality((Object) this.m_ScreenshotCature, (Object) null))
      return;
    this.m_ScreenshotCature = Object.FindObjectOfType<ScreenshotTaker>();
    if (Object.op_Equality((Object) this.m_ScreenshotCature, (Object) null))
      this.m_ScreenshotCature = ((Component) this).gameObject.GetComponent<ScreenshotTaker>();
    if (!Object.op_Equality((Object) this.m_ScreenshotCature, (Object) null))
      return;
    this.m_ScreenshotCature = ((Component) this).gameObject.AddComponent<ScreenshotTaker>();
  }

  protected void HandleHotkeys()
  {
    if (this.m_Config.m_AlignHotkey.IsPressed())
      this.m_Config.AlignToView();
    if (this.m_Config.m_PauseHotkey.IsPressed())
      this.m_Config.TogglePause();
    if (this.m_Config.m_UpdatePreviewHotkey.IsPressed())
      this.UpdatePreview();
    if (!this.m_Config.m_CaptureHotkey.IsPressed())
      return;
    if (this.m_IsBurstActive)
      this.StopBurst();
    else
      this.Capture();
  }

  public void Capture() => this.StartCoroutine(this.CaptureAllCoroutine());

  public void UpdateAll() => this.StartCoroutine(this.UpdateAllCoroutine());

  public void Update(List<ScreenshotResolution> resolutions)
  {
    this.StartCoroutine(this.CaptureCoroutine(resolutions, false, false));
  }

  public IEnumerator UpdateAllCoroutine()
  {
    List<ScreenshotResolution> activeResolutions = this.m_Config.GetActiveResolutions();
    this.m_Config.UpdateGameviewResolution();
    yield return (object) this.StartCoroutine(this.CaptureCoroutine(activeResolutions, false, false));
  }

  public IEnumerator CaptureAllCoroutine()
  {
    List<ScreenshotResolution> activeResolutions = this.m_Config.GetActiveResolutions();
    this.m_Config.UpdateGameviewResolution();
    yield return (object) this.StartCoroutine(this.CaptureCoroutine(activeResolutions));
  }

  public IEnumerator CaptureCoroutine(
    List<ScreenshotResolution> resolutions,
    bool export = true,
    bool playSoundMask = true)
  {
    if (this.m_Config.m_ShotMode == ScreenshotConfig.ShotMode.BURST && !Application.isPlaying)
      Debug.LogError((object) "In burst mode the application needs to be playing.");
    else if (this.m_IsCapturing)
    {
      Debug.LogError((object) "A capture process is already running.");
    }
    else
    {
      this.m_IsCapturing = true;
      if (Application.isPlaying && this.m_Config.m_PreviewInGameViewWhilePlaying && this.m_Config.m_ShowGuidesInPreview)
        this.HideGuides();
      ScreenshotManager.onCaptureBeginDelegate.Invoke();
      if (this.m_Config.m_ShotMode == ScreenshotConfig.ShotMode.ONE_SHOT)
        yield return (object) this.StartCoroutine(this.UpdateCoroutine(resolutions, this.m_Config.GetActiveCameras(), this.m_Config.m_Overlays, export, playSoundMask));
      else if (this.m_Config.m_ShotMode == ScreenshotConfig.ShotMode.BURST)
      {
        this.m_IsBurstActive = true;
        for (int i = 0; i < this.m_Config.m_MaxBurstShotsNumber && this.m_IsBurstActive; ++i)
        {
          yield return (object) this.StartCoroutine(this.UpdateCoroutine(resolutions, this.m_Config.GetActiveCameras(), this.m_Config.m_Overlays, export, playSoundMask));
          yield return (object) new WaitForSeconds(this.m_Config.m_ShotTimeStep);
        }
        this.m_IsBurstActive = false;
      }
      ScreenshotManager.onCaptureEndDelegate.Invoke();
      if (Application.isPlaying && this.m_Config.m_PreviewInGameViewWhilePlaying && this.m_Config.m_ShowGuidesInPreview)
        this.ShowGuides();
      else
        this.HideGuides();
      this.m_IsCapturing = false;
    }
  }

  public void StopBurst() => this.m_IsBurstActive = false;

  protected IEnumerator UpdateCoroutine(
    List<ScreenshotResolution> resolutions,
    List<ScreenshotCamera> cameras,
    List<ScreenshotOverlay> overlays,
    bool export = true,
    bool playSoundMask = true)
  {
    this.InitScreenshotCapture();
    if (this.m_Config.m_PlaySoundOnCapture & playSoundMask)
      this.PlaySound();
    if (this.m_Config.m_ExportToDifferentLayers && cameras.Count > 1)
    {
      for (int i = 0; i < cameras.Count; ++i)
      {
        List<ScreenshotResolution> resolutions1 = resolutions;
        List<ScreenshotCamera> cameras1 = new List<ScreenshotCamera>();
        cameras1.Add(cameras[i]);
        List<ScreenshotOverlay> overlays1 = overlays;
        int num1 = export ? 1 : 0;
        int num2 = playSoundMask ? 1 : 0;
        yield return (object) this.StartCoroutine(this.DoUpdateCoroutine(resolutions1, cameras1, overlays1, num1 != 0, num2 != 0));
        if (export)
        {
          foreach (ScreenshotResolution resolution in resolutions)
          {
            this.m_Config.UpdateFileName(resolution, ((Object) cameras[i].m_Camera).name);
            if (TextureExporter.ExportToFile(resolution.m_Texture, resolution.m_FileName, this.m_Config.m_FileFormat, (int) this.m_Config.m_JPGQuality))
            {
              Debug.Log((object) ("Screenshot created : " + resolution.m_FileName));
              ScreenshotManager.onResolutionExportSuccessDelegate(resolution);
            }
            else
            {
              Debug.LogError((object) ("Failed to create : " + resolution.m_FileName));
              ScreenshotManager.onResolutionExportFailureDelegate(resolution);
            }
          }
        }
      }
    }
    else
    {
      yield return (object) this.StartCoroutine(this.DoUpdateCoroutine(resolutions, cameras, overlays, export, playSoundMask));
      if (export)
      {
        foreach (ScreenshotResolution resolution in resolutions)
        {
          this.m_Config.UpdateFileName(resolution);
          if (TextureExporter.ExportToFile(resolution.m_Texture, resolution.m_FileName, this.m_Config.m_FileFormat, (int) this.m_Config.m_JPGQuality))
          {
            Debug.Log((object) ("Screenshot created : " + resolution.m_FileName));
            ScreenshotManager.onResolutionExportSuccessDelegate(resolution);
          }
          else
          {
            Debug.LogError((object) ("Failed to create : " + resolution.m_FileName));
            ScreenshotManager.onResolutionExportFailureDelegate(resolution);
          }
        }
      }
    }
  }

  protected IEnumerator DoUpdateCoroutine(
    List<ScreenshotResolution> resolutions,
    List<ScreenshotCamera> cameras,
    List<ScreenshotOverlay> overlays,
    bool export,
    bool playSoundMask)
  {
    yield return (object) this.StartCoroutine(this.m_ScreenshotCature.CaptureAllCoroutine(resolutions, cameras, overlays, this.m_Config.m_CaptureMode != ScreenshotTaker.CaptureMode.GAMEVIEW_RESIZING || this.m_Config.m_ResolutionCaptureMode != ScreenshotConfig.ResolutionMode.GAME_VIEW ? this.m_Config.m_CaptureMode : ScreenshotTaker.CaptureMode.FIXED_GAMEVIEW, (int) this.m_Config.m_MultisamplingAntiAliasing, this.m_Config.m_CaptureActiveUICanvas, this.m_Config.m_ColorFormat, this.m_Config.m_RecomputeAlphaLayer, this.m_Config.m_StopTimeOnCapture));
  }

  private void PlaySound()
  {
    if (Object.op_Equality((Object) ((Component) this).GetComponent<AudioSource>(), (Object) null) || Object.op_Equality((Object) this.m_Config.m_ShotSound, (Object) null))
      return;
    ((Component) this).GetComponent<AudioSource>().PlayOneShot(this.m_Config.m_ShotSound);
  }

  public virtual void UpdatePreview() => this.StartCoroutine(this.UpdatePreviewCoroutine());

  public IEnumerator UpdatePreviewCoroutine()
  {
    if (!this.m_IsCapturing)
    {
      this.m_IsCapturing = true;
      ScreenshotManager.onCaptureBeginDelegate.Invoke();
      this.m_PreviewList.Clear();
      this.m_PreviewList.Add(this.m_Config.GetFirstActiveResolution());
      this.m_Config.UpdateGameviewResolution();
      this.m_PreviewOverlayList.Clear();
      this.m_PreviewOverlayList.AddRange((IEnumerable<ScreenshotOverlay>) this.m_Config.m_Overlays);
      if (this.m_Config.m_ShowGuidesInPreview)
      {
        this.m_GuidesOverlay = new ScreenshotOverlay(this.m_Config.m_GuideCanvas);
        this.m_PreviewOverlayList.Add(this.m_GuidesOverlay);
        this.ShowGuides();
      }
      this.InitScreenshotCapture();
      yield return (object) this.StartCoroutine(this.m_ScreenshotCature.CaptureAllCoroutine(this.m_PreviewList, this.m_Config.GetActiveCameras(), this.m_PreviewOverlayList, this.m_Config.m_CaptureMode != ScreenshotTaker.CaptureMode.GAMEVIEW_RESIZING || this.m_Config.m_ResolutionCaptureMode != ScreenshotConfig.ResolutionMode.GAME_VIEW ? this.m_Config.m_CaptureMode : ScreenshotTaker.CaptureMode.FIXED_GAMEVIEW, (int) this.m_Config.m_MultisamplingAntiAliasing, this.m_Config.m_CaptureActiveUICanvas, this.m_Config.m_ColorFormat, this.m_Config.m_RecomputeAlphaLayer, this.m_Config.m_StopTimeOnCapture));
      if (Application.isPlaying && this.m_Config.m_PreviewInGameViewWhilePlaying && this.m_Config.m_ShowGuidesInPreview)
        this.ShowGuides();
      else
        this.HideGuides();
      ScreenshotManager.onCaptureEndDelegate.Invoke();
      this.m_IsCapturing = false;
    }
  }

  protected void InitIngamePreview()
  {
    this.m_PreviewOverlayList.Clear();
    this.m_PreviewOverlayList.AddRange((IEnumerable<ScreenshotOverlay>) this.m_Config.m_Overlays);
    if (this.m_Config.m_ShowGuidesInPreview)
    {
      this.m_GuidesOverlay = new ScreenshotOverlay(this.m_Config.m_GuideCanvas);
      this.m_PreviewOverlayList.Add(this.m_GuidesOverlay);
    }
    this.InitScreenshotCapture();
    this.m_ScreenshotCature.ApplySettings(this.m_Config.GetActiveCameras(), this.m_PreviewOverlayList, this.m_Config.m_CaptureMode, this.m_Config.m_CaptureActiveUICanvas);
  }

  protected void ShowGuides()
  {
    if (!this.m_Config.m_ShowGuidesInPreview || !Object.op_Inequality((Object) this.m_Config.m_GuideCanvas, (Object) null))
      return;
    ((Component) this.m_Config.m_GuideCanvas).gameObject.SetActive(true);
    ((Behaviour) this.m_Config.m_GuideCanvas).enabled = true;
    foreach (Graphic componentsInChild in ((Component) this.m_Config.m_GuideCanvas).GetComponentsInChildren<Image>())
      componentsInChild.color = this.m_Config.m_GuidesColor;
  }

  protected void HideGuides()
  {
    if (this.m_Config.m_PreviewInGameViewWhilePlaying && Application.isPlaying && this.m_Config.m_ShowGuidesInPreview && !this.m_IsCapturing || !Object.op_Inequality((Object) this.m_Config.m_GuideCanvas, (Object) null))
      return;
    ((Component) this.m_Config.m_GuideCanvas).gameObject.SetActive(false);
  }

  public delegate void ExportDelegate(ScreenshotResolution res);
}
