// Decompiled with JetBrains decompiler
// Type: GridGalleryCanvas
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class GridGalleryCanvas : ScreenshotGallery
{
  public GridLayoutGroup m_Grid;
  public RectTransform m_PreviousButton;
  public RectTransform m_NextButton;
  public Text m_PageText;
  [HideInInspector]
  public int m_CurrentPage;

  public virtual int MaxPages()
  {
    return Mathf.CeilToInt((float) this.m_ImageFiles.Count / (float) this.ImagesPerPage());
  }

  public virtual int ImagesPerPage()
  {
    Rect rect = ((Component) this.m_Grid).GetComponent<RectTransform>().rect;
    int num1 = Mathf.FloorToInt(((Rect) ref rect).width / (this.m_Grid.cellSize.x + this.m_Grid.spacing.x));
    rect = ((Component) this.m_Grid).GetComponent<RectTransform>().rect;
    int num2 = Mathf.FloorToInt(((Rect) ref rect).height / (this.m_Grid.cellSize.y + this.m_Grid.spacing.y));
    return num1 * num2;
  }

  public override void DoGalleryUpdate()
  {
    if (this.m_CurrentPage >= this.MaxPages())
      this.m_CurrentPage = this.MaxPages() - 1;
    this.Clear();
    for (int index = 0; index < this.ImagesPerPage(); ++index)
    {
      int num1 = this.m_CurrentPage * this.ImagesPerPage() + index;
      if (num1 < this.m_ImageFiles.Count)
      {
        GameObject gameObject = this.InstantiateImageObject(this.m_ImageFiles[num1], num1, ((Component) this.m_Grid).transform);
        float num2 = this.m_Grid.cellSize.x / this.m_Grid.cellSize.y;
        float num3 = (float) ((Texture) this.m_ImageFiles[num1].m_Texture).width / (float) ((Texture) this.m_ImageFiles[num1].m_Texture).height / num2;
        if ((double) num3 >= 1.0)
          ((Component) gameObject.GetComponentInChildren<RawImage>()).transform.localScale = new Vector3(1f, 1f / num3, 1f);
        else
          ((Component) gameObject.GetComponentInChildren<RawImage>()).transform.localScale = new Vector3(num3, 1f, 1f);
      }
      else
        break;
    }
    if (this.m_CurrentPage == 0)
      ((Component) this.m_PreviousButton).gameObject.SetActive(false);
    else
      ((Component) this.m_PreviousButton).gameObject.SetActive(true);
    if (this.m_CurrentPage < this.MaxPages() - 1)
      ((Component) this.m_NextButton).gameObject.SetActive(true);
    else
      ((Component) this.m_NextButton).gameObject.SetActive(false);
    this.m_PageText.text = $"{(this.m_CurrentPage + 1).ToString()}/{this.MaxPages().ToString()}";
  }

  public virtual void NextPageCallback()
  {
    ++this.m_CurrentPage;
    this.UpdateGallery();
  }

  public virtual void PreviousPageCallback()
  {
    --this.m_CurrentPage;
    this.UpdateGallery();
  }

  public virtual void CloseCallback() => ((Component) this).gameObject.SetActive(false);
}
