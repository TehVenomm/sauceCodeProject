// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Screenshot.ScreenshotConfig
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace AlmostEngine.Screenshot;

[Serializable]
public class ScreenshotConfig
{
  public ScreenshotNameParser.DestinationFolder m_DestinationFolder = ScreenshotNameParser.DestinationFolder.PICTURES_FOLDER;
  public string m_RelativePath = "Screenshots/";
  public string m_RootedPath = "";
  public string m_FileName = "{width}x{height}-screenshot";
  [Tooltip("Override files or increment automatically the filenames.")]
  public bool m_OverrideFiles;
  [Tooltip("Use PNG to create screenshots with a transparent background.")]
  public TextureExporter.ImageFileFormat m_FileFormat;
  public float m_JPGQuality = 75f;
  public ScreenshotTaker.CaptureMode m_CaptureMode = ScreenshotTaker.CaptureMode.FIXED_GAMEVIEW;
  public ScreenshotConfig.AntiAliasing m_MultisamplingAntiAliasing = ScreenshotConfig.AntiAliasing.EIGHT;
  [Tooltip("RGB is the default color format.\nUse RGBA to create screenshots with an alpha layer, enabling transparent backgrounds.")]
  public ScreenshotTaker.ColorFormat m_ColorFormat;
  [Tooltip("Force alpha layer to be recomputed. This is a costly process. Use only if you have alpha problems in RGBA mode.")]
  public bool m_RecomputeAlphaLayer;
  public ScreenshotConfig.ShotMode m_ShotMode;
  public int m_MaxBurstShotsNumber = 20;
  public float m_ShotTimeStep = 0.25f;
  [Tooltip("GAME_VIEW will capture what you see on the screen.\nCUSTOM_CAMERAS allows you to customize the cameras to be used, and their rendering properties.")]
  public ScreenshotConfig.CamerasMode m_CameraMode;
  [Tooltip("When enabled, one screenshot is taken for each camera, to be used as independant layers for compositing.")]
  public bool m_ExportToDifferentLayers;
  public List<ScreenshotCamera> m_Cameras = new List<ScreenshotCamera>();
  [Tooltip("GAME_VIEW will capture what you see on the screen.\nCUSTOM_RESOLUTIONS allows you to customize the resolutions to be used.")]
  public ScreenshotConfig.ResolutionMode m_ResolutionCaptureMode = ScreenshotConfig.ResolutionMode.CUSTOM_RESOLUTIONS;
  public List<ScreenshotResolution> m_Resolutions = new List<ScreenshotResolution>();
  public ScreenshotResolution m_GameViewResolution;
  [Tooltip("Capture or not the active UI Canvas.")]
  public bool m_CaptureActiveUICanvas = true;
  public List<ScreenshotOverlay> m_Overlays = new List<ScreenshotOverlay>();
  public bool m_ShowGuidesInPreview;
  public Canvas m_GuideCanvas;
  public Color m_GuidesColor = Color.white;
  public bool m_ShowPreview = true;
  public float m_PreviewSize = 1f;
  [Tooltip("If set to true, the camera and overlay settings will be applied when the application starts playing.")]
  public bool m_PreviewInGameViewWhilePlaying;
  public bool m_StopTimeOnCapture = true;
  public bool m_PlaySoundOnCapture = true;
  public AudioClip m_ShotSound;
  [NonSerialized]
  public float m_Time = 1f;
  public HotKey m_CaptureHotkey = new HotKey(false, false, false, (KeyCode) 0);
  public HotKey m_UpdatePreviewHotkey = new HotKey(false, false, false, (KeyCode) 0);
  public HotKey m_AlignHotkey = new HotKey(false, false, false, (KeyCode) 0);
  public HotKey m_PauseHotkey = new HotKey(false, false, false, (KeyCode) 0);
  public ScreenshotTaker.GameViewResizingWaitingMode m_GameViewResizingWaitingMode;
  public float m_ResizingWaitingTime = 1f;
  public int m_ResizingWaitingFrames = 2;
  public bool m_ShowDestination = true;
  public bool m_ShowName = true;
  public bool m_ShowCaptureMode = true;
  public bool m_ShowResolutions = true;
  public bool m_ShowCameras = true;
  public bool m_ShowCanvas = true;
  public bool m_ShowPreviewGUI = true;
  public bool m_ShowCapture = true;
  public bool m_ShowHotkeys = true;
  public bool m_ShowGallery = true;
  public bool m_ShowUtils = true;
  public bool m_ShowUsage = true;

  public ScreenshotConfig() => this.InitGameViewResolution();

  public string GetPath()
  {
    return ScreenshotNameParser.ParsePath(this.m_DestinationFolder, this.m_DestinationFolder == ScreenshotNameParser.DestinationFolder.CUSTOM_FOLDER ? this.m_RootedPath : this.m_RelativePath);
  }

  public List<ScreenshotCamera> GetActiveCameras()
  {
    List<ScreenshotCamera> activeCameras = new List<ScreenshotCamera>();
    if (this.m_CameraMode == ScreenshotConfig.CamerasMode.CUSTOM_CAMERAS)
    {
      foreach (ScreenshotCamera camera in this.m_Cameras)
      {
        if (camera.m_Active && !Object.op_Equality((Object) camera.m_Camera, (Object) null))
          activeCameras.Add(camera);
      }
    }
    return activeCameras;
  }

  public void AlignToView()
  {
  }

  public ScreenshotResolution GetFirstActiveResolution()
  {
    List<ScreenshotResolution> activeResolutions = this.GetActiveResolutions();
    return activeResolutions.Count > 0 ? activeResolutions[0] : this.m_GameViewResolution;
  }

  public List<ScreenshotResolution> GetActiveResolutions()
  {
    List<ScreenshotResolution> activeResolutions = new List<ScreenshotResolution>();
    if (this.m_CaptureMode != ScreenshotTaker.CaptureMode.FIXED_GAMEVIEW && this.m_ResolutionCaptureMode == ScreenshotConfig.ResolutionMode.CUSTOM_RESOLUTIONS)
    {
      foreach (ScreenshotResolution resolution in this.m_Resolutions)
      {
        if (resolution.m_Active && resolution.IsValid())
          activeResolutions.Add(resolution);
      }
    }
    if (this.m_ResolutionCaptureMode == ScreenshotConfig.ResolutionMode.GAME_VIEW || this.m_CaptureMode == ScreenshotTaker.CaptureMode.FIXED_GAMEVIEW)
      activeResolutions.Add(this.m_GameViewResolution);
    return activeResolutions;
  }

  public void UpdateGameviewResolution()
  {
    Vector2 currentGameViewSize = GameViewController.GetCurrentGameViewSize();
    if (this.m_GameViewResolution == null)
      this.InitGameViewResolution();
    this.m_GameViewResolution.m_Width = (int) currentGameViewSize.x;
    this.m_GameViewResolution.m_Height = (int) currentGameViewSize.y;
  }

  protected void InitGameViewResolution()
  {
    this.m_GameViewResolution = new ScreenshotResolution();
    this.m_GameViewResolution.m_Active = true;
    this.m_GameViewResolution.m_ResolutionName = "GameView";
    this.m_GameViewResolution.m_Width = Screen.width;
    this.m_GameViewResolution.m_Height = Screen.height;
    this.m_GameViewResolution.m_Scale = 1f;
  }

  public void UpdateResolutionFilenames(List<ScreenshotResolution> resolutions, string currentLayer = "")
  {
    foreach (ScreenshotResolution resolution in resolutions)
      this.UpdateFileName(resolution, currentLayer);
  }

  public string ParseFileName(ScreenshotResolution resolution, string currentLayer = "")
  {
    string customPath = this.m_DestinationFolder == ScreenshotNameParser.DestinationFolder.CUSTOM_FOLDER ? this.m_RootedPath : this.m_RelativePath;
    return ScreenshotNameParser.ParseFileName(this.m_FileName, resolution, this.m_DestinationFolder, customPath, this.m_FileFormat, this.m_OverrideFiles, currentLayer);
  }

  public void UpdateFileName(ScreenshotResolution resolution, string currentLayer = "")
  {
    resolution.m_FileName = this.ParseFileName(resolution, currentLayer);
  }

  public void UpdateRatios()
  {
    foreach (ScreenshotResolution resolution in this.m_Resolutions)
      resolution.UpdateRatio();
  }

  public void SetAllPortait()
  {
    foreach (ScreenshotResolution resolution in this.m_Resolutions)
      resolution.m_Orientation = ScreenshotResolution.Orientation.PORTRAIT;
  }

  public void SetAllLandscape()
  {
    foreach (ScreenshotResolution resolution in this.m_Resolutions)
      resolution.m_Orientation = ScreenshotResolution.Orientation.LANDSCAPE;
  }

  public void SelectAllResolutions()
  {
    foreach (ScreenshotResolution resolution in this.m_Resolutions)
      resolution.m_Active = true;
  }

  public void ClearAllResolutions()
  {
    foreach (ScreenshotResolution resolution in this.m_Resolutions)
      resolution.m_Active = false;
  }

  public void RemoveAllResolutions() => this.m_Resolutions.Clear();

  public void SetTime(float time)
  {
    if ((double) time == (double) this.m_Time)
      return;
    this.m_Time = time;
    Time.timeScale = time;
  }

  public void TogglePause()
  {
    if ((double) this.m_Time == 0.0)
      this.SetTime(1f);
    else
      this.SetTime(0.0f);
  }

  public void ClearCache()
  {
    this.m_GameViewResolution.m_Texture = (Texture2D) null;
    foreach (ScreenshotResolution resolution in this.m_Resolutions)
      resolution.m_Texture = (Texture2D) null;
  }

  public void ExportAllToFiles() => this.ExportToFiles(this.GetActiveResolutions());

  public void ExportToFiles(List<ScreenshotResolution> resolutions)
  {
    foreach (ScreenshotResolution resolution in resolutions)
    {
      this.UpdateFileName(resolution);
      if (TextureExporter.ExportToFile(resolution.m_Texture, resolution.m_FileName, this.m_FileFormat, (int) this.m_JPGQuality))
        Debug.Log((object) ("Image exported : " + resolution.m_FileName));
    }
  }

  public enum AntiAliasing
  {
    NONE = 0,
    TWO = 2,
    FOUR = 4,
    EIGHT = 8,
  }

  public enum ShotMode
  {
    ONE_SHOT,
    BURST,
  }

  public enum CamerasMode
  {
    GAME_VIEW,
    CUSTOM_CAMERAS,
  }

  public enum ResolutionMode
  {
    GAME_VIEW,
    CUSTOM_RESOLUTIONS,
  }
}
