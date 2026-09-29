// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Screenshot.ScreenshotCutter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
namespace AlmostEngine.Screenshot;

[ExecuteInEditMode]
public class ScreenshotCutter : MonoBehaviour
{
  public RectTransform m_SelectionArea;
  public bool m_HideSelectionAreaDuringCapture;
  public int m_CropBorder = 1;
  private bool m_WasActive = true;

  private void OnEnable()
  {
    ScreenshotTaker.onResolutionUpdateStartDelegate -= new ScreenshotTaker.UpdateDelegate(this.StartCallback);
    ScreenshotTaker.onResolutionUpdateStartDelegate += new ScreenshotTaker.UpdateDelegate(this.StartCallback);
    ScreenshotTaker.onResolutionUpdateEndDelegate -= new ScreenshotTaker.UpdateDelegate(this.EndCallback);
    ScreenshotTaker.onResolutionUpdateEndDelegate += new ScreenshotTaker.UpdateDelegate(this.EndCallback);
  }

  private void OnDisable()
  {
    ScreenshotTaker.onResolutionUpdateStartDelegate -= new ScreenshotTaker.UpdateDelegate(this.StartCallback);
    ScreenshotTaker.onResolutionUpdateEndDelegate -= new ScreenshotTaker.UpdateDelegate(this.EndCallback);
  }

  private void StartCallback(ScreenshotResolution res)
  {
    if (Object.op_Equality((Object) this.m_SelectionArea, (Object) null) || !this.m_HideSelectionAreaDuringCapture)
      return;
    this.Hide();
  }

  private void EndCallback(ScreenshotResolution res)
  {
    if (Object.op_Equality((Object) this.m_SelectionArea, (Object) null))
      return;
    if (this.m_HideSelectionAreaDuringCapture)
      this.Show();
    this.CropTexture(res);
  }

  private void Hide()
  {
    this.m_WasActive = ((Component) this.m_SelectionArea).gameObject.activeSelf;
    ((Component) this.m_SelectionArea).gameObject.SetActive(false);
  }

  private void Show() => ((Component) this.m_SelectionArea).gameObject.SetActive(this.m_WasActive);

  private void CropTexture(ScreenshotResolution res)
  {
    Vector3[] vector3Array = new Vector3[4];
    this.m_SelectionArea.GetWorldCorners(vector3Array);
    int num1 = (int) vector3Array[0].x + this.m_CropBorder;
    int num2 = (int) vector3Array[0].y + this.m_CropBorder;
    int num3 = (int) ((double) vector3Array[2].x - (double) vector3Array[0].x) - 2 * this.m_CropBorder;
    int num4 = (int) ((double) vector3Array[1].y - (double) vector3Array[0].y) - 2 * this.m_CropBorder;
    Texture2D texture2D = new Texture2D(num3, num4, res.m_Texture.format, false);
    if (num3 <= 2 || num4 <= 2)
      return;
    for (int index1 = 0; index1 < num3; ++index1)
    {
      for (int index2 = 0; index2 < num4; ++index2)
      {
        Color color = num1 + index1 < 0 || num1 + index1 >= ((Texture) res.m_Texture).width || num2 + index2 < 0 || num2 + index2 >= ((Texture) res.m_Texture).height ? Color.black : res.m_Texture.GetPixel(num1 + index1, num2 + index2);
        texture2D.SetPixel(index1, index2, color);
      }
    }
    texture2D.Apply();
    Debug.Log((object) $"Screenshot cropped to ({(object) num1}, {(object) num2}, {(object) (num1 + num3 - 1)}, {(object) (num2 + num4 - 1)})");
    res.m_Texture = texture2D;
  }
}
