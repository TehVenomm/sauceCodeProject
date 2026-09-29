// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Examples.CaptureScreenshotExample
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using AlmostEngine.Screenshot;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace AlmostEngine.Examples;

public class CaptureScreenshotExample : MonoBehaviour
{
  public int m_Width = 800;
  public int m_Height = 600;
  public int m_Scale = 1;
  public string m_FullpathA = "";
  public string m_FullpathB = "";
  public KeyCode m_ShortcutA = (KeyCode) 287;
  public KeyCode m_ShortcutB = (KeyCode) 288;

  private void Update()
  {
    if (Input.GetKeyDown(this.m_ShortcutA))
      SimpleScreenshotCapture.CaptureScreen(this.m_FullpathA);
    if (!Input.GetKeyDown(this.m_ShortcutB))
      return;
    SimpleScreenshotCapture.CaptureCameras(this.m_FullpathB, this.m_Width, this.m_Height, new List<Camera>()
    {
      Camera.main
    }, TextureExporter.ImageFileFormat.JPG, 70);
  }
}
