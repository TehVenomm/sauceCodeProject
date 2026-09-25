// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Screenshot.ScreenshotOverlay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace AlmostEngine.Screenshot;

[Serializable]
public class ScreenshotOverlay
{
  public Canvas m_Canvas;
  public bool m_Active = true;
  public Stack<ScreenshotOverlay.Settings> m_SettingStack = new Stack<ScreenshotOverlay.Settings>();
  private Canvas m_Instance;

  public ScreenshotOverlay()
  {
  }

  public ScreenshotOverlay(Canvas canvas) => this.m_Canvas = canvas;

  public void ApplySettings()
  {
    if (Object.op_Equality((Object) this.m_Canvas, (Object) null))
      return;
    this.m_SettingStack.Push(new ScreenshotOverlay.Settings(((Behaviour) this.m_Canvas).enabled, ((Component) this.m_Canvas).gameObject.activeSelf));
    if (!((Component) ((Component) this.m_Canvas).transform).gameObject.activeInHierarchy)
    {
      this.m_Instance = Object.Instantiate<Canvas>(this.m_Canvas);
      ((Behaviour) this.m_Instance).enabled = true;
      ((Component) this.m_Instance).gameObject.SetActive(true);
      ((Object) this.m_Instance).name = ((Object) this.m_Instance).name + " - temporary instance, remove if still exists after capture process";
    }
    else
    {
      ((Behaviour) this.m_Canvas).enabled = this.m_Active;
      ((Component) this.m_Canvas).gameObject.SetActive(this.m_Active);
    }
  }

  public void Disable()
  {
    if (Object.op_Equality((Object) this.m_Canvas, (Object) null))
      return;
    this.m_SettingStack.Push(new ScreenshotOverlay.Settings(((Behaviour) this.m_Canvas).enabled, ((Component) this.m_Canvas).gameObject.activeSelf));
    ((Behaviour) this.m_Canvas).enabled = false;
  }

  public void RestoreSettings()
  {
    if (Object.op_Equality((Object) this.m_Canvas, (Object) null) || this.m_SettingStack.Count <= 0)
      return;
    ScreenshotOverlay.Settings settings = this.m_SettingStack.Pop();
    if (Object.op_Inequality((Object) this.m_Instance, (Object) null))
    {
      Object.DestroyImmediate((Object) ((Component) this.m_Instance).gameObject);
      this.m_Instance = (Canvas) null;
    }
    else
    {
      ((Behaviour) this.m_Canvas).enabled = settings.m_Enabled;
      ((Component) this.m_Canvas).gameObject.SetActive(settings.m_GameObjectEnabled);
    }
  }

  public class Settings
  {
    public bool m_Enabled;
    public bool m_GameObjectEnabled;

    public Settings(bool enabled, bool go)
    {
      this.m_Enabled = enabled;
      this.m_GameObjectEnabled = go;
    }
  }
}
