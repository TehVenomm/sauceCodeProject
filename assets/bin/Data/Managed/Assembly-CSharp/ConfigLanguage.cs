// Decompiled with JetBrains decompiler
// Type: ConfigLanguage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class ConfigLanguage : GameSection
{
  public override void Initialize() => base.Initialize();

  public override void Exit()
  {
    GameSaveData.Save();
    base.Exit();
  }

  public override void UpdateUI()
  {
    this.SetToggle((Enum) ConfigLanguage.UI.TGL_EN, GameSaveData.instance.languageOption == 0);
    this.SetToggle((Enum) ConfigLanguage.UI.TGL_FR, GameSaveData.instance.languageOption == 1);
    this.SetToggle((Enum) ConfigLanguage.UI.TGL_GE, GameSaveData.instance.languageOption == 2);
    this.SetToggle((Enum) ConfigLanguage.UI.TGL_IT, GameSaveData.instance.languageOption == 3);
    this.SetToggle((Enum) ConfigLanguage.UI.TGL_PO, GameSaveData.instance.languageOption == 4);
    this.SetToggle((Enum) ConfigLanguage.UI.TGL_TH, GameSaveData.instance.languageOption == 5);
    this.SetToggle((Enum) ConfigLanguage.UI.TGL_VN, GameSaveData.instance.languageOption == 6);
    this.SetToggle((Enum) ConfigLanguage.UI.TGL_ES, GameSaveData.instance.languageOption == 7);
  }

  private void OnQuery_LANG_EN()
  {
    if (GameSaveData.instance.languageOption == 0)
      return;
    GameSaveData.instance.languageOption = 0;
    this.ClearCacheAndReturnToTitle();
  }

  private void OnQuery_LANG_FR()
  {
    if (GameSaveData.instance.languageOption == 1)
      return;
    GameSaveData.instance.languageOption = 1;
    this.ClearCacheAndReturnToTitle();
  }

  private void OnQuery_LANG_GE()
  {
    if (GameSaveData.instance.languageOption == 2)
      return;
    GameSaveData.instance.languageOption = 2;
    this.ClearCacheAndReturnToTitle();
  }

  private void OnQuery_LANG_IT()
  {
    if (GameSaveData.instance.languageOption == 3)
      return;
    GameSaveData.instance.languageOption = 3;
    this.ClearCacheAndReturnToTitle();
  }

  private void OnQuery_LANG_PO()
  {
    if (GameSaveData.instance.languageOption == 4)
      return;
    GameSaveData.instance.languageOption = 4;
    this.ClearCacheAndReturnToTitle();
  }

  private void OnQuery_LANG_TH()
  {
    if (GameSaveData.instance.languageOption == 5)
      return;
    GameSaveData.instance.languageOption = 5;
    this.ClearCacheAndReturnToTitle();
  }

  private void OnQuery_LANG_VN()
  {
    if (GameSaveData.instance.languageOption == 6)
      return;
    GameSaveData.instance.languageOption = 6;
    this.ClearCacheAndReturnToTitle();
  }

  private void OnQuery_LANG_ES()
  {
    if (GameSaveData.instance.languageOption == 7)
      return;
    GameSaveData.instance.languageOption = 7;
    this.ClearCacheAndReturnToTitle();
  }

  private void ClearCacheAndReturnToTitle()
  {
    this.UpdateUI();
    MonoBehaviourSingleton<AppMain>.I.Reset(false, false);
  }

  private enum UI
  {
    TGL_EN,
    TGL_FR,
    TGL_GE,
    TGL_IT,
    TGL_PO,
    TGL_TH,
    TGL_VN,
    TGL_ES,
  }
}
