// Decompiled with JetBrains decompiler
// Type: ScreenshotGallery
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using AlmostEngine.Screenshot;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
[RequireComponent(typeof (Canvas))]
public abstract class ScreenshotGallery : MonoBehaviour
{
  [Tooltip("The path on the device from which the screenhots will be loaded. To automatically use the same path than your Screenshot Manager export path, add a SetScreenshotManagerFolderPath component to that object: a path will be automatically set at startup.")]
  public string m_ScreenshotFolderPath = "";
  public ScreenshotNameParser.DestinationFolder m_DestinationFolder = ScreenshotNameParser.DestinationFolder.PICTURES_FOLDER;
  [Tooltip("The greybox object to use when an image is selected.")]
  public GreyboxCanvas m_GreyBox;
  [Tooltip("The object to use as prefab to instantiate the gallery image object. Note that this object must have a RawImage component.")]
  public GameObject m_ImageItemPrefab;
  public List<TextureExporter.ImageFile> m_ImageFiles = new List<TextureExporter.ImageFile>();
  [HideInInspector]
  public List<GameObject> m_ImageInstances = new List<GameObject>();

  public virtual void Show()
  {
    ((Component) this).gameObject.SetActive(true);
    this.LoadImageFiles();
    this.UpdateGallery();
  }

  public virtual void UpdateGallery() => this.StartCoroutine(this.DelayedUpdate());

  public IEnumerator DelayedUpdate()
  {
    yield return (object) new WaitForEndOfFrame();
    this.DoGalleryUpdate();
  }

  public virtual void LoadImageFiles()
  {
    this.m_ImageFiles.Clear();
    string path = ScreenshotNameParser.ParsePath(this.m_DestinationFolder, this.m_ScreenshotFolderPath);
    if (string.IsNullOrEmpty(path))
      return;
    this.m_ImageFiles = TextureExporter.LoadFromPath(path);
  }

  public virtual void Clear()
  {
    foreach (Object imageInstance in this.m_ImageInstances)
      Object.Destroy(imageInstance);
    this.m_ImageInstances.Clear();
  }

  public virtual GameObject InstantiateImageObject(
    TextureExporter.ImageFile image,
    int i,
    Transform parent)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ScreenshotGallery.\u003C\u003Ec__DisplayClass11_0 cDisplayClass110 = new ScreenshotGallery.\u003C\u003Ec__DisplayClass11_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass110.\u003C\u003E4__this = this;
    GameObject gameObject = Object.Instantiate<GameObject>(this.m_ImageItemPrefab);
    gameObject.transform.SetParent(parent);
    gameObject.transform.localScale = Vector3.one;
    RawImage componentInChildren1 = gameObject.GetComponentInChildren<RawImage>();
    if (Object.op_Equality((Object) componentInChildren1, (Object) null))
      Debug.LogError((object) "Can not find the RawImage component in the gallery image. Be sure the canvas game object is enabled before calling this method.");
    componentInChildren1.texture = (Texture) image.m_Texture;
    Button componentInChildren2 = gameObject.GetComponentInChildren<Button>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass110.index = i;
    // ISSUE: method pointer
    ((UnityEvent) componentInChildren2.onClick).AddListener(new UnityAction((object) cDisplayClass110, __methodptr(\u003CInstantiateImageObject\u003Eb__0)));
    this.m_ImageInstances.Add(gameObject);
    return gameObject;
  }

  public abstract void DoGalleryUpdate();

  public virtual void RemoveImage(int index)
  {
    if (File.Exists(this.m_ImageFiles[index].m_Fullname))
      File.Delete(this.m_ImageFiles[index].m_Fullname);
    this.m_ImageFiles.RemoveAt(index);
  }

  public virtual void OnSelectImageCallback(int id)
  {
    if (!Object.op_Inequality((Object) this.m_GreyBox, (Object) null))
      return;
    ((Component) this).gameObject.SetActive(false);
    ((Component) this.m_GreyBox).gameObject.SetActive(true);
    this.m_GreyBox.SetImage(this, id);
  }
}
