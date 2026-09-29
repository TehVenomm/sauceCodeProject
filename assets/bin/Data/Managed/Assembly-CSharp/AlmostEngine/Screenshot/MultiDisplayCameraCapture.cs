// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Screenshot.MultiDisplayCameraCapture
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
namespace AlmostEngine.Screenshot;

public class MultiDisplayCameraCapture : MonoBehaviour
{
  private Texture2D m_TargetTexture;
  private bool m_DoCopy;

  public void CaptureCamera(Texture2D targetTexture)
  {
    this.m_TargetTexture = targetTexture;
    this.m_DoCopy = true;
  }

  public bool CopyIsOver() => !this.m_DoCopy;

  private void OnRenderImage(RenderTexture src, RenderTexture dest)
  {
    Graphics.Blit((Texture) src, dest);
    if (!this.m_DoCopy)
      return;
    this.m_TargetTexture.ReadPixels(new Rect(0.0f, 0.0f, (float) ((Texture) this.m_TargetTexture).width, (float) ((Texture) this.m_TargetTexture).height), 0, 0);
    this.m_TargetTexture.Apply(false);
    this.m_DoCopy = false;
  }
}
