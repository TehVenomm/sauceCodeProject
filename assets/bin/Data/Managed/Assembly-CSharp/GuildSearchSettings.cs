// Decompiled with JetBrains decompiler
// Type: GuildSearchSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class GuildSearchSettings : GameSection
{
  private string mSearchKeywork;

  public override void Initialize()
  {
    this.SetActive((Enum) GuildSearchSettings.UI.LBL_DEFAULT, string.IsNullOrEmpty(this.mSearchKeywork));
    this.SetInput((Enum) GuildSearchSettings.UI.IPT_NAME, this.mSearchKeywork, 16 /*0x10*/, new EventDelegate.Callback(this.OnChangeKeywork));
    base.Initialize();
  }

  private void OnQuery_SEARCH()
  {
    MonoBehaviourSingleton<GuildManager>.I.mSearchKeywork = this.mSearchKeywork;
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendSearch((Action<bool, Error>) ((is_success, err) => GameSection.ResumeEvent(is_success)), true);
  }

  protected void OnChangeKeywork()
  {
    string str = this.GetInputValue((Enum) GuildSearchSettings.UI.IPT_NAME).Replace(" ", "").Replace("　", "");
    this.SetActive((Enum) GuildSearchSettings.UI.LBL_DEFAULT, string.IsNullOrEmpty(str));
    this.mSearchKeywork = str;
  }

  public enum UI
  {
    IPT_NAME,
    LBL_INPUT,
    LBL_DEFAULT,
  }
}
