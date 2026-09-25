// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Screenshot.RotateScreenshot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
namespace AlmostEngine.Screenshot;

[ExecuteInEditMode]
public class RotateScreenshot : MonoBehaviour
{
  private void OnEnable()
  {
    ScreenshotTaker.onResolutionUpdateEndDelegate -= new ScreenshotTaker.UpdateDelegate(this.EndCallback);
    ScreenshotTaker.onResolutionUpdateEndDelegate += new ScreenshotTaker.UpdateDelegate(this.EndCallback);
  }

  private void OnDisable()
  {
    ScreenshotTaker.onResolutionUpdateEndDelegate -= new ScreenshotTaker.UpdateDelegate(this.EndCallback);
  }

  private void EndCallback(ScreenshotResolution res) => this.RotateTexture(res);

  private void RotateTexture(ScreenshotResolution res)
  {
    Texture2D texture2D = new Texture2D(((Texture) res.m_Texture).height, ((Texture) res.m_Texture).width, res.m_Texture.format, false);
    for (int index1 = 0; index1 < ((Texture) res.m_Texture).width; ++index1)
    {
      for (int index2 = 0; index2 < ((Texture) res.m_Texture).height; ++index2)
      {
        Color pixel = res.m_Texture.GetPixel(((Texture) res.m_Texture).width - 1 - index1, index2);
        texture2D.SetPixel(index2, index1, pixel);
      }
    }
    texture2D.Apply();
    Debug.Log((object) "Screenshot rotated");
    Object.DestroyImmediate((Object) res.m_Texture);
    res.m_Texture = texture2D;
  }
}
