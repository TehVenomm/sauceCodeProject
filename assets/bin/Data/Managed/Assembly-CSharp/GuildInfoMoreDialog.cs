// Decompiled with JetBrains decompiler
// Type: GuildInfoMoreDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class GuildInfoMoreDialog : GameSection
{
  private string desc;

  public override string overrideBackKeyEvent => "CLOSE";

  public override void Initialize()
  {
    this.desc = GameSection.GetEventData() as string;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) GuildInfoMoreDialog.UI.ProvisionalLabel, this.desc);
    this.UpdateAnchors();
  }

  private void OnQuery_CLOSE() => GameSection.BackSection();

  private enum UI
  {
    ProvisionalLabel,
  }
}
