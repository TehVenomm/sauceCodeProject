// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Examples.ValidationCanvas
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using AlmostEngine.Screenshot;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace AlmostEngine.Examples;

public class ValidationCanvas : MonoBehaviour
{
  public ScreenshotManager m_ScreenshotManager;
  public Canvas m_Canvas;
  public RectTransform m_ImageContainer;
  public RawImage m_Texture;

  public void Capture()
  {
    if (Object.op_Equality((Object) this.m_ScreenshotManager, (Object) null))
      this.m_ScreenshotManager = Object.FindObjectOfType<ScreenshotManager>();
    // ISSUE: method pointer
    ScreenshotManager.onCaptureEndDelegate = (UnityAction) Delegate.Combine((Delegate) ScreenshotManager.onCaptureEndDelegate, (Delegate) new UnityAction((object) this, __methodptr(OnCaptureEndDelegate)));
    this.m_ScreenshotManager.UpdateAll();
  }

  public void OnCaptureEndDelegate()
  {
    // ISSUE: method pointer
    ScreenshotManager.onCaptureEndDelegate = (UnityAction) Delegate.Remove((Delegate) ScreenshotManager.onCaptureEndDelegate, (Delegate) new UnityAction((object) this, __methodptr(OnCaptureEndDelegate)));
    this.m_Texture.texture = (Texture) this.m_ScreenshotManager.m_Config.GetFirstActiveResolution().m_Texture;
    ((Graphic) this.m_Texture).SetNativeSize();
    Rect rect = this.m_ImageContainer.rect;
    float num = ((Rect) ref rect).height / (float) this.m_Texture.texture.height;
    ((Component) this.m_Texture).transform.localScale = new Vector3(num, num, num);
    ((Component) this).gameObject.SetActive(true);
    ((Behaviour) this.m_Canvas).enabled = true;
  }

  public void OnDiscardCallback()
  {
    ((Component) this).gameObject.SetActive(false);
    ((Behaviour) this.m_Canvas).enabled = false;
  }

  public void OnSaveCallback()
  {
    this.m_ScreenshotManager.m_Config.ExportAllToFiles();
    ((Component) this).gameObject.SetActive(false);
    ((Behaviour) this.m_Canvas).enabled = false;
  }
}
