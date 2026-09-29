// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Screenshot.ScreenshotCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace AlmostEngine.Screenshot;

[Serializable]
public class ScreenshotCamera
{
  public bool m_Active = true;
  public Camera m_Camera;
  public ScreenshotCamera.CustomSettings m_ClearSettings;
  public CameraClearFlags m_ClearFlags = (CameraClearFlags) 1;
  public Color m_BackgroundColor = Color.white;
  public ScreenshotCamera.CustomSettings m_CullingSettings;
  public int m_CullingMask = -1;
  public ScreenshotCamera.CustomSettings m_FOVSettings;
  public float m_FOV = 70f;
  public Stack<ScreenshotCamera.Settings> m_SettingStack = new Stack<ScreenshotCamera.Settings>();

  public ScreenshotCamera()
  {
  }

  public ScreenshotCamera(Camera cam) => this.m_Camera = cam;

  public void ApplySettings(bool isOffscreen = false)
  {
    if (Object.op_Equality((Object) this.m_Camera, (Object) null))
      return;
    this.m_SettingStack.Push(new ScreenshotCamera.Settings(((Behaviour) this.m_Camera).enabled, ((Component) this.m_Camera).gameObject.activeSelf, this.m_Camera.clearFlags, this.m_Camera.backgroundColor, this.m_Camera.cullingMask, this.m_Camera.fieldOfView));
    if (!((Behaviour) this.m_Camera).enabled & isOffscreen)
      ((Component) this.m_Camera).gameObject.SetActive(false);
    else
      ((Component) this.m_Camera).gameObject.SetActive(true);
    ((Behaviour) this.m_Camera).enabled = true;
    if (this.m_ClearSettings == ScreenshotCamera.CustomSettings.CUSTOM)
    {
      this.m_Camera.clearFlags = this.m_ClearFlags;
      this.m_Camera.backgroundColor = this.m_BackgroundColor;
    }
    if (this.m_CullingSettings == ScreenshotCamera.CustomSettings.CUSTOM)
      this.m_Camera.cullingMask = this.m_CullingMask;
    if (this.m_FOVSettings != ScreenshotCamera.CustomSettings.CUSTOM)
      return;
    this.m_Camera.fieldOfView = this.m_FOV;
  }

  public void Disable()
  {
    if (Object.op_Equality((Object) this.m_Camera, (Object) null))
      return;
    this.m_SettingStack.Push(new ScreenshotCamera.Settings(((Behaviour) this.m_Camera).enabled, ((Component) this.m_Camera).gameObject.activeSelf, this.m_Camera.clearFlags, this.m_Camera.backgroundColor, this.m_Camera.cullingMask, this.m_Camera.fieldOfView));
    ((Behaviour) this.m_Camera).enabled = false;
  }

  public void RestoreSettings()
  {
    if (Object.op_Equality((Object) this.m_Camera, (Object) null) || this.m_SettingStack.Count <= 0)
      return;
    ScreenshotCamera.Settings settings = this.m_SettingStack.Pop();
    ((Behaviour) this.m_Camera).enabled = settings.m_Enabled;
    ((Component) this.m_Camera).gameObject.SetActive(settings.m_GameObjectEnabled);
    this.m_Camera.clearFlags = settings.m_ClearFlags;
    this.m_Camera.backgroundColor = settings.m_BackgroundColor;
    this.m_Camera.cullingMask = settings.m_CullingMask;
    this.m_Camera.fieldOfView = settings.m_FOV;
  }

  public enum CustomSettings
  {
    KEEP_CAMERA_SETTINGS,
    CUSTOM,
  }

  public class Settings
  {
    public bool m_Enabled;
    public bool m_GameObjectEnabled;
    public CameraClearFlags m_ClearFlags;
    public Color m_BackgroundColor;
    public int m_CullingMask;
    public float m_FOV;

    public Settings(
      bool enabled,
      bool go,
      CameraClearFlags clear,
      Color color,
      int culling,
      float fov)
    {
      this.m_Enabled = enabled;
      this.m_GameObjectEnabled = go;
      this.m_ClearFlags = clear;
      this.m_BackgroundColor = color;
      this.m_CullingMask = culling;
      this.m_FOV = fov;
    }
  }
}
