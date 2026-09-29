// Decompiled with JetBrains decompiler
// Type: AccountChangePasswordMail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class AccountChangePasswordMail : AccountPopupAdjuster
{
  public override void UpdateUI()
  {
    this.SetInput((Enum) AccountChangePasswordMail.UI.IPT_PASSWORD, string.Empty, (int) byte.MaxValue, new EventDelegate.Callback(this.InputCallBack));
    this.SetInput((Enum) AccountChangePasswordMail.UI.IPT_NEW_PASSWORD, string.Empty, (int) byte.MaxValue, new EventDelegate.Callback(this.InputCallBack));
    this.SetInput((Enum) AccountChangePasswordMail.UI.IPT_CONFIRM_NEW_PASSWORD, string.Empty, (int) byte.MaxValue, new EventDelegate.Callback(this.InputCallBack));
  }

  private void InputCallBack()
  {
    bool is_visible = this.CheckRegistData();
    this.SetActive((Enum) AccountChangePasswordMail.UI.BTN_OK, is_visible);
    this.SetActive((Enum) AccountChangePasswordMail.UI.BTN_INVALID, !is_visible);
  }

  private bool CheckRegistData(bool is_send_event = false)
  {
    if (string.IsNullOrEmpty(this.GetInputText((Enum) AccountChangePasswordMail.UI.IPT_PASSWORD)))
    {
      this.CheckChangeEvent((is_send_event ? 1 : 0) != 0, "EMPTY", (object) new object[1]
      {
        (object) this.sectionData.GetText("PASSWORD")
      });
      return false;
    }
    string inputText1 = this.GetInputText((Enum) AccountChangePasswordMail.UI.IPT_NEW_PASSWORD);
    string inputText2 = this.GetInputText((Enum) AccountChangePasswordMail.UI.IPT_CONFIRM_NEW_PASSWORD);
    if (string.IsNullOrEmpty(inputText1))
    {
      this.CheckChangeEvent((is_send_event ? 1 : 0) != 0, "EMPTY", (object) new object[1]
      {
        (object) this.sectionData.GetText("NEW_PASSWORD")
      });
      return false;
    }
    if (inputText1.Length < 8)
    {
      this.CheckChangeEvent(is_send_event, "PASSWORD_TOO_SHORT");
      return false;
    }
    if (inputText1 != inputText2)
    {
      this.CheckChangeEvent(is_send_event, "CONFIRM_PASSWORD_NOT_MATCH");
      return false;
    }
    if (inputText1.Length <= (int) byte.MaxValue)
      return true;
    this.CheckChangeEvent(is_send_event, "PASSWORD_TOO_LONG");
    return false;
  }

  private void CheckChangeEvent(bool is_send, string event_name = "", object event_data = null)
  {
    if (!is_send)
      return;
    GameSection.ChangeEvent(event_name, event_data);
  }

  private void OnQuery_OK()
  {
    if (!this.CheckRegistData(true))
      return;
    string inputText1 = this.GetInputText((Enum) AccountChangePasswordMail.UI.IPT_PASSWORD);
    string inputText2 = this.GetInputText((Enum) AccountChangePasswordMail.UI.IPT_NEW_PASSWORD);
    string inputText3 = this.GetInputText((Enum) AccountChangePasswordMail.UI.IPT_CONFIRM_NEW_PASSWORD);
    GameSection.StayEvent();
    MonoBehaviourSingleton<AccountManager>.I.SendRegistChangePasswordRob(inputText1, inputText2, inputText3, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  private enum UI
  {
    IPT_PASSWORD,
    IPT_NEW_PASSWORD,
    IPT_CONFIRM_NEW_PASSWORD,
    BTN_OK,
    BTN_INVALID,
  }
}
