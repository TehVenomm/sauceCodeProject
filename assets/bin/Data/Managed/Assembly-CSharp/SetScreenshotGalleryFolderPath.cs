// Decompiled with JetBrains decompiler
// Type: SetScreenshotGalleryFolderPath
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using AlmostEngine.Screenshot;
using UnityEngine;

#nullable disable
[RequireComponent(typeof (ScreenshotGallery))]
public class SetScreenshotGalleryFolderPath : MonoBehaviour
{
  [Tooltip("The screenshot manager to use as reference to get the screenshots folder path.")]
  public ScreenshotManager m_Manager;

  private void Start()
  {
    if (!Object.op_Inequality((Object) this.m_Manager, (Object) null))
      return;
    ScreenshotGallery component = ((Component) this).GetComponent<ScreenshotGallery>();
    component.m_ScreenshotFolderPath = this.m_Manager.m_Config.GetPath();
    component.m_DestinationFolder = ScreenshotNameParser.DestinationFolder.CUSTOM_FOLDER;
    component.LoadImageFiles();
    component.UpdateGallery();
  }
}
