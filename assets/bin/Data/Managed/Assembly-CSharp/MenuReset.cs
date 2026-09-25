// Decompiled with JetBrains decompiler
// Type: MenuReset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;

#nullable disable
public class MenuReset : GameSection
{
  public static bool needClearCache;
  public static bool needPredownload;

  public override void Initialize()
  {
    if (MenuReset.needClearCache && MenuReset.needPredownload)
    {
      this.StartCoroutine(this.ResetProc());
      base.Initialize();
    }
    else
      this.Reset();
  }

  private IEnumerator ResetProc()
  {
    GameSceneGlobalSettings.forceIgnoreMainUI = true;
    yield return (object) ResourceSizeInfo.Init();
    yield return (object) ResourceSizeInfo.OpenConfirmDialog(ResourceSizeInfo.GetOpeningAssetSizeMB(false));
    GameSceneGlobalSettings.forceIgnoreMainUI = false;
    this.Reset();
  }

  private void Reset()
  {
    MonoBehaviourSingleton<AppMain>.I.Reset(MenuReset.needClearCache, MenuReset.needPredownload);
    MenuReset.needClearCache = false;
    MenuReset.needPredownload = false;
  }
}
