// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Examples.CaptureScreenToTextureExample
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using AlmostEngine.Screenshot;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace AlmostEngine.Examples;

public class CaptureScreenToTextureExample : MonoBehaviour
{
  public ScreenshotTaker m_ScreenshotTaker;
  public RawImage m_RawImage;
  private Texture2D m_TargetTexture;

  public void Capture() => this.StartCoroutine(this.CaptureToTexture());

  private IEnumerator CaptureToTexture()
  {
    if (Object.op_Equality((Object) this.m_TargetTexture, (Object) null))
      this.m_TargetTexture = new Texture2D(2, 2);
    yield return (object) this.StartCoroutine(this.m_ScreenshotTaker.CaptureScreenToTextureCoroutine(this.m_TargetTexture));
    this.m_RawImage.texture = (Texture) this.m_TargetTexture;
  }
}
