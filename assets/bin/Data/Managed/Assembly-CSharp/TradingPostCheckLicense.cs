// Decompiled with JetBrains decompiler
// Type: TradingPostCheckLicense
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class TradingPostCheckLicense : GameSection
{
  public override void Initialize()
  {
    this.SetSupportEncoding((Enum) TradingPostCheckLicense.UI.MESSAGE, true);
    this.SetLabelText((Enum) TradingPostCheckLicense.UI.MESSAGE, this.sectionData.GetText("STR_MESSAGE"));
    this.SetLabelText((Enum) TradingPostCheckLicense.UI.LBL_BTN_0, this.sectionData.GetText("TEXT_BTN_TO_TP"));
    this.SetLabelText((Enum) TradingPostCheckLicense.UI.LBL_BTN_0_R, this.sectionData.GetText("TEXT_BTN_TO_TP"));
    base.Initialize();
  }

  public override void StartSection() => base.StartSection();

  private void OnQuery_CONTINUE()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName());
    this.Close();
  }

  private enum UI
  {
    MESSAGE,
    OBJ_FRAME,
    LBL_BTN_0,
    LBL_BTN_0_R,
  }
}
