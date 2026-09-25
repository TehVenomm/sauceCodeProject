// Decompiled with JetBrains decompiler
// Type: GreyboxCanvas
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using AlmostEngine.Screenshot;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
[RequireComponent(typeof (Canvas))]
public class GreyboxCanvas : MonoBehaviour
{
  public RectTransform m_ImageContainer;
  public RectTransform m_PreviousButton;
  public RectTransform m_NextButton;
  public RectTransform m_CloseButton;
  public Text m_PageText;
  public Text m_FileName;
  private ScreenshotGallery m_Gallery;
  private int m_CurrentImageId;

  public void SetImage(ScreenshotGallery gallery, int i)
  {
    this.m_Gallery = gallery;
    this.m_CurrentImageId = i;
    this.UpdateUI();
  }

  public virtual void UpdateUI() => this.StartCoroutine(this.DelayedUpdate());

  public IEnumerator DelayedUpdate()
  {
    yield return (object) new WaitForEndOfFrame();
    this.DoUpdate();
  }

  public virtual void DoUpdate()
  {
    TextureExporter.ImageFile imageFile = this.m_Gallery.m_ImageFiles[this.m_CurrentImageId];
    RawImage componentInChildren = ((Component) this.m_ImageContainer).GetComponentInChildren<RawImage>();
    componentInChildren.texture = (Texture) imageFile.m_Texture;
    Rect rect = this.m_ImageContainer.rect;
    double width = (double) ((Rect) ref rect).width;
    rect = this.m_ImageContainer.rect;
    double height = (double) ((Rect) ref rect).height;
    float num1 = (float) (width / height);
    float num2 = (float) ((Texture) imageFile.m_Texture).width / (float) ((Texture) imageFile.m_Texture).height / num1;
    if ((double) num2 >= 1.0)
      ((Component) ((Component) componentInChildren).GetComponentInChildren<RawImage>()).transform.localScale = new Vector3(1f, 1f / num2, 1f);
    else
      ((Component) ((Component) componentInChildren).GetComponentInChildren<RawImage>()).transform.localScale = new Vector3(num2, 1f, 1f);
    if (this.m_CurrentImageId == 0)
      ((Component) this.m_PreviousButton).gameObject.SetActive(false);
    else
      ((Component) this.m_PreviousButton).gameObject.SetActive(true);
    if (this.m_CurrentImageId >= this.m_Gallery.m_ImageFiles.Count - 1)
      ((Component) this.m_NextButton).gameObject.SetActive(false);
    else
      ((Component) this.m_NextButton).gameObject.SetActive(true);
    this.m_FileName.text = imageFile.m_Name;
    this.m_PageText.text = $"{(this.m_CurrentImageId + 1).ToString()}/{(object) this.m_Gallery.m_ImageFiles.Count}";
  }

  public virtual void NextPageCallback()
  {
    ++this.m_CurrentImageId;
    this.UpdateUI();
  }

  public virtual void PreviousPageCallback()
  {
    --this.m_CurrentImageId;
    this.UpdateUI();
  }

  public virtual void CloseCallback()
  {
    ((Component) this).gameObject.SetActive(false);
    ((Component) this.m_Gallery).gameObject.SetActive(true);
    this.m_Gallery.UpdateGallery();
  }

  public virtual void RemoveCallback()
  {
    ((Component) this).gameObject.SetActive(false);
    ((Component) this.m_Gallery).gameObject.SetActive(true);
    this.m_Gallery.RemoveImage(this.m_CurrentImageId);
    this.m_Gallery.UpdateGallery();
  }
}
