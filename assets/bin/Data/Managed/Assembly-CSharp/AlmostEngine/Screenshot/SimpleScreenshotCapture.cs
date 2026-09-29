// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Screenshot.SimpleScreenshotCapture
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace AlmostEngine.Screenshot;

public static class SimpleScreenshotCapture
{
  private static ScreenshotTaker m_ScreenshotTaker;

  private static void InitScreenshotTaker()
  {
    if (Object.op_Inequality((Object) SimpleScreenshotCapture.m_ScreenshotTaker, (Object) null) || !Object.op_Equality((Object) SimpleScreenshotCapture.m_ScreenshotTaker, (Object) null))
      return;
    GameObject gameObject = new GameObject();
    ((Object) gameObject).name = "tmp Screenshot Capture";
    SimpleScreenshotCapture.m_ScreenshotTaker = gameObject.AddComponent<ScreenshotTaker>();
  }

  public static void CaptureScreen(
    string fileName,
    TextureExporter.ImageFileFormat fileFormat = TextureExporter.ImageFileFormat.PNG,
    int JPGQuality = 100,
    bool captureGameUI = true,
    ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
    bool recomputeAlphaMask = false)
  {
    Vector2 currentGameViewSize = GameViewController.GetCurrentGameViewSize();
    SimpleScreenshotCapture.Capture(fileName, (int) currentGameViewSize.x, (int) currentGameViewSize.y, fileFormat, JPGQuality, captureMode: ScreenshotTaker.CaptureMode.FIXED_GAMEVIEW, captureGameUI: captureGameUI, colorFormat: colorFormat, recomputeAlphaMask: recomputeAlphaMask);
  }

  public static void CaptureCameras(
    string fileName,
    int width,
    int height,
    List<Camera> cameras,
    TextureExporter.ImageFileFormat fileFormat = TextureExporter.ImageFileFormat.PNG,
    int JPGQuality = 100,
    int antiAliasing = 8,
    ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
    bool recomputeAlphaMask = false)
  {
    SimpleScreenshotCapture.Capture(fileName, width, height, fileFormat, JPGQuality, cameras, antiAliasing: antiAliasing, captureGameUI: false, colorFormat: colorFormat, recomputeAlphaMask: recomputeAlphaMask);
  }

  public static void Capture(
    string fileName,
    int width,
    int height,
    TextureExporter.ImageFileFormat fileFormat = TextureExporter.ImageFileFormat.PNG,
    int JPGQuality = 100,
    List<Camera> cameras = null,
    List<Canvas> canvas = null,
    ScreenshotTaker.CaptureMode captureMode = ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE,
    int antiAliasing = 8,
    bool captureGameUI = true,
    ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
    bool recomputeAlphaMask = false)
  {
    SimpleScreenshotCapture.InitScreenshotTaker();
    SimpleScreenshotCapture.m_ScreenshotTaker.StartCoroutine(SimpleScreenshotCapture.CaptureCoroutine(fileName, width, height, fileFormat, JPGQuality, cameras, canvas, captureMode, antiAliasing, captureGameUI, colorFormat, recomputeAlphaMask));
  }

  public static IEnumerator CaptureCoroutine(
    string fileName,
    int width,
    int height,
    TextureExporter.ImageFileFormat fileFormat = TextureExporter.ImageFileFormat.PNG,
    int JPGQuality = 100,
    List<Camera> cameras = null,
    List<Canvas> canvas = null,
    ScreenshotTaker.CaptureMode captureMode = ScreenshotTaker.CaptureMode.RENDER_TO_TEXTURE,
    int antiAliasing = 8,
    bool captureGameUI = true,
    ScreenshotTaker.ColorFormat colorFormat = ScreenshotTaker.ColorFormat.RGB,
    bool recomputeAlphaMask = false)
  {
    ScreenshotResolution captureResolution = new ScreenshotResolution();
    captureResolution.m_Width = width;
    captureResolution.m_Height = height;
    captureResolution.m_FileName = fileName;
    List<ScreenshotCamera> screenshotCameraList = new List<ScreenshotCamera>();
    if (cameras != null)
    {
      foreach (Camera camera in cameras)
      {
        ScreenshotCamera screenshotCamera = new ScreenshotCamera(camera);
        screenshotCameraList.Add(screenshotCamera);
      }
    }
    List<ScreenshotOverlay> screenshotOverlayList = new List<ScreenshotOverlay>();
    if (canvas != null)
    {
      foreach (Canvas canva in canvas)
      {
        ScreenshotOverlay screenshotOverlay = new ScreenshotOverlay(canva);
        screenshotOverlayList.Add(screenshotOverlay);
      }
    }
    ScreenshotTaker screenshotTaker1 = SimpleScreenshotCapture.m_ScreenshotTaker;
    ScreenshotTaker screenshotTaker2 = SimpleScreenshotCapture.m_ScreenshotTaker;
    List<ScreenshotResolution> resolutions = new List<ScreenshotResolution>();
    resolutions.Add(captureResolution);
    List<ScreenshotCamera> cameras1 = screenshotCameraList;
    List<ScreenshotOverlay> overlays = screenshotOverlayList;
    int num1 = (int) captureMode;
    int antiAliasing1 = antiAliasing;
    int num2 = captureGameUI ? 1 : 0;
    int num3 = (int) colorFormat;
    int num4 = recomputeAlphaMask ? 1 : 0;
    IEnumerator enumerator = screenshotTaker2.CaptureAllCoroutine(resolutions, cameras1, overlays, (ScreenshotTaker.CaptureMode) num1, antiAliasing1, num2 != 0, (ScreenshotTaker.ColorFormat) num3, num4 != 0);
    yield return (object) screenshotTaker1.StartCoroutine(enumerator);
    if (TextureExporter.ExportToFile(captureResolution.m_Texture, fileName, fileFormat, JPGQuality))
      Debug.Log((object) ("Screenshot created : " + fileName));
    else
      Debug.LogError((object) ("Failed to create : " + fileName));
  }
}
