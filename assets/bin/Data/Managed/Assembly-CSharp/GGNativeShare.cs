// Decompiled with JetBrains decompiler
// Type: GGNativeShare
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.IO;
using UnityEngine;

#nullable disable
public class GGNativeShare : MonoBehaviourSingleton<GGNativeShare>
{
  public string ScreenshotName = "screenshot.png";

  public void ShareScreenshotWithText()
  {
    string str = $"{Application.persistentDataPath}/{this.ScreenshotName}";
    if (File.Exists(str))
      File.Delete(str);
    ScreenCapture.CaptureScreenshot(this.ScreenshotName);
    this.StartCoroutine(this.delayedShare(str));
  }

  private IEnumerator delayedShare(string screenShotPath)
  {
    while (!File.Exists(screenShotPath))
      yield return (object) new WaitForSeconds(0.05f);
    new NativeShare().AddFile(screenShotPath).Share();
  }
}
