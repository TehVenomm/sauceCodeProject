// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Examples.TakeScreenshotButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using AlmostEngine.Screenshot;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace AlmostEngine.Examples;

[RequireComponent(typeof (Button))]
public class TakeScreenshotButton : MonoBehaviour
{
  private Button m_Button;
  private ScreenshotManager m_ScreenshotManager;

  private void Start()
  {
    this.m_ScreenshotManager = Object.FindObjectOfType<ScreenshotManager>();
    this.m_Button = ((Component) this).GetComponent<Button>();
    // ISSUE: method pointer
    ((UnityEvent) this.m_Button.onClick).AddListener(new UnityAction((object) this, __methodptr(OnClickCallback)));
  }

  private void OnClickCallback()
  {
    if (!Object.op_Implicit((Object) this.m_ScreenshotManager))
      return;
    this.m_ScreenshotManager.Capture();
  }
}
