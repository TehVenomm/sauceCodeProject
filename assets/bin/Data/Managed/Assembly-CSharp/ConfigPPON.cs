// Decompiled with JetBrains decompiler
// Type: ConfigPPON
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class ConfigPPON : GameSection
{
  public override void UpdateUI()
  {
    this.SetInput((Enum) ConfigPPON.UI.IPT_PW, "", 4, new EventDelegate.Callback(this.OnInputChange));
    this.SetInput((Enum) ConfigPPON.UI.IPT_PW_CONFIRM, "", 4, new EventDelegate.Callback(this.OnInputChange));
  }

  private void OnInputChange()
  {
    this.SetButtonEnabled((Enum) ConfigPPON.UI.BTN_OK, this.GetInputValue((Enum) ConfigPPON.UI.IPT_PW).Length == 4 && this.GetInputValue((Enum) ConfigPPON.UI.IPT_PW_CONFIRM).Length == 4);
  }

  private void OnQuery_OK()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<UserInfoManager>.I.SendParentalPassword(this.GetInputValue((Enum) ConfigPPON.UI.IPT_PW), this.GetInputValue((Enum) ConfigPPON.UI.IPT_PW_CONFIRM), (Action<Error>) (ret =>
    {
      if (ret != Error.None)
        GameSection.ChangeStayEvent("ERROR", (object) new object[1]
        {
          (object) (int) ret
        });
      GameSection.ResumeEvent(true);
    }));
  }

  private enum UI
  {
    IPT_PW,
    IPT_PW_CONFIRM,
    BTN_OK,
  }
}
