// Decompiled with JetBrains decompiler
// Type: PPInputBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public abstract class PPInputBase : GameSection
{
  public override void UpdateUI()
  {
    this.SetInput((Enum) PPInputBase.UI.IPT_PW, "", 4, new EventDelegate.Callback(this.OnInputChange));
  }

  private void OnInputChange()
  {
    this.SetButtonEnabled((Enum) PPInputBase.UI.BTN_OK, this.GetInputValue((Enum) PPInputBase.UI.IPT_PW).Length == 4);
  }

  protected enum UI
  {
    IPT_PW,
    BTN_OK,
    STR_REMOVE_PASS,
  }
}
