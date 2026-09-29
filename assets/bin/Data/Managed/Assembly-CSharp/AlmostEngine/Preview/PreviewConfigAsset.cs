// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Preview.PreviewConfigAsset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using AlmostEngine.Screenshot;
using UnityEngine;

#nullable disable
namespace AlmostEngine.Preview;

public class PreviewConfigAsset : ScriptableObject
{
  public ScreenshotConfig m_Config;
  [Tooltip("Set your screen PPI value to be used by the preview Gallery.")]
  public int m_ScreenPPI = 109;
  public float m_ZoomScrollSpeed = 0.01f;
  public int m_MarginHorizontal = 10;
  public int m_MarginVertical = 30;
  public int m_GalleryPaddingVertical = 10;
  public int m_GalleryTextHeight = 30;
  public int m_GalleryBorderSize = 2;
  public int m_Selected;
  public float m_PreviewGalleryZoom = 1f;
  public float m_PreviewZoom = 1f;
  [Tooltip("When auto refresh is enabled, the gallery and preview windows are updated automatically.The refresh can be done while editing the scene, while playing the game, or always.")]
  public PreviewConfigAsset.AutoRefreshMode m_RefreshMode;
  public bool m_AutoRefresh;
  public float m_RefreshDelay = 0.05f;
  public PreviewConfigAsset.GalleryDisplayMode m_GalleryDisplayMode;
  public PreviewConfigAsset.GalleryDisplayMode m_PreviewDisplayMode;
  public bool m_ShowGallery = true;

  public enum AutoRefreshMode
  {
    ONLY_IN_PLAY_MODE,
    ONLY_IN_EDIT_MODE,
    ALWAYS,
  }

  public enum GalleryDisplayMode
  {
    RATIOS,
    PIXELS,
    PPI,
  }
}
