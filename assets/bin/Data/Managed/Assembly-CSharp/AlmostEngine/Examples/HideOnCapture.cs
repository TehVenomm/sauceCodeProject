// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Examples.HideOnCapture
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using AlmostEngine.Screenshot;
using UnityEngine;

#nullable disable
namespace AlmostEngine.Examples;

public class HideOnCapture : MonoBehaviour
{
  private void Start()
  {
    ScreenshotTaker.onResolutionUpdateStartDelegate += new ScreenshotTaker.UpdateDelegate(this.Hide);
    ScreenshotTaker.onResolutionUpdateEndDelegate += new ScreenshotTaker.UpdateDelegate(this.Show);
  }

  private void OnDestroy()
  {
    ScreenshotTaker.onResolutionUpdateStartDelegate -= new ScreenshotTaker.UpdateDelegate(this.Hide);
    ScreenshotTaker.onResolutionUpdateEndDelegate -= new ScreenshotTaker.UpdateDelegate(this.Show);
  }

  private void Hide(ScreenshotResolution res) => ((Component) this).gameObject.SetActive(false);

  private void Show(ScreenshotResolution res) => ((Component) this).gameObject.SetActive(true);
}
