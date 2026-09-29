// Decompiled with JetBrains decompiler
// Type: AccountRegistrationBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable
public class AccountRegistrationBase : AccountPopupAdjuster
{
  protected bool isGoogleAccount;
  private bool isSelectedSecretQuest;
  private int secretQuestionIndex;
  private NetworkNative.GoogleAccountInfo googleAccountList;
  private int secretGoogleAccountIndex;

  public override void Initialize()
  {
    this.secretQuestionIndex = -1;
    this.secretGoogleAccountIndex = -1;
    this.isSelectedSecretQuest = false;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) AccountRegistrationBase.UI.IPT_ADDRESS, !this.isGoogleAccount);
    this.SetActive((Enum) AccountRegistrationBase.UI.POP_ADDRESS, this.isGoogleAccount);
    this.SetActive((Enum) AccountRegistrationBase.UI.OBJ_SECRET_QUESTION, !this.isGoogleAccount);
    this.SetInput((Enum) AccountRegistrationBase.UI.IPT_ADDRESS, string.Empty, (int) byte.MaxValue, new EventDelegate.Callback(this.InputCallBack));
    this.SetInput((Enum) AccountRegistrationBase.UI.IPT_PASSWORD, string.Empty, (int) byte.MaxValue, new EventDelegate.Callback(this.InputCallBack));
    this.SetInput((Enum) AccountRegistrationBase.UI.IPT_CONFIRM_PASSWORD, string.Empty, (int) byte.MaxValue, new EventDelegate.Callback(this.InputCallBack));
    if (!this.isGoogleAccount)
      this.SetInput((Enum) AccountRegistrationBase.UI.IPT_SECRET_ANSER, string.Empty, 45, new EventDelegate.Callback(this.InputCallBack));
    if (this.isGoogleAccount)
    {
      List<string> account_list = (List<string>) null;
      this.googleAccountList = NetworkNative.getGoogleAccounts();
      if (this.googleAccountList.googleAccounts.Count > 0)
      {
        UILabel lbl = this.GetComponent<UILabel>((Enum) AccountRegistrationBase.UI.LBL_ADDRESS);
        account_list = new List<string>();
        this.googleAccountList.googleAccounts.ForEach((Action<NetworkNative.GoogleAccount>) (account => account_list.Add(this.PopupTextAdjust(lbl, account.name))));
      }
      this.SetPopupListText((Enum) AccountRegistrationBase.UI.POP_ADDRESS, account_list);
      this.SetPopupListOnChange((Enum) AccountRegistrationBase.UI.POP_ADDRESS, (Enum) AccountRegistrationBase.UI.LBL_ADDRESS, new EventDelegate.Callback(this.InputCallBack_Address));
    }
    else
    {
      this.SetPopupListText((Enum) AccountRegistrationBase.UI.POP_SECRET_QUESTION, new List<string>()
      {
        StringTable.Get(STRING_CATEGORY.ACCOUNT, 0U),
        StringTable.Get(STRING_CATEGORY.ACCOUNT, 1U),
        StringTable.Get(STRING_CATEGORY.ACCOUNT, 2U),
        StringTable.Get(STRING_CATEGORY.ACCOUNT, 3U),
        StringTable.Get(STRING_CATEGORY.ACCOUNT, 4U),
        StringTable.Get(STRING_CATEGORY.ACCOUNT, 5U),
        StringTable.Get(STRING_CATEGORY.ACCOUNT, 6U)
      });
      this.SetPopupListOnChange((Enum) AccountRegistrationBase.UI.POP_SECRET_QUESTION, (Enum) AccountRegistrationBase.UI.LBL_SECRET_QUESTION, new EventDelegate.Callback(this.InputCallBack_SecretQuestion));
    }
  }

  private void InputCallBack_SecretQuestion()
  {
    UIPopupList component = this.GetComponent<UIPopupList>((Enum) AccountRegistrationBase.UI.POP_SECRET_QUESTION);
    this.secretQuestionIndex = component.items.IndexOf(component.value);
    this.isSelectedSecretQuest = true;
    this.InputCallBack();
  }

  private void InputCallBack_Address()
  {
    UIPopupList component = this.GetComponent<UIPopupList>((Enum) AccountRegistrationBase.UI.POP_ADDRESS);
    this.secretGoogleAccountIndex = component.items.IndexOf(component.value);
    this.InputCallBack();
  }

  private void InputCallBack()
  {
    bool is_visible = this.CheckRegistData();
    this.SetActive((Enum) AccountRegistrationBase.UI.BTN_OK, is_visible);
    this.SetActive((Enum) AccountRegistrationBase.UI.BTN_INVALID, !is_visible);
  }

  private bool CheckRegistData(bool is_send_event = false)
  {
    string empty1 = string.Empty;
    string input = !this.isGoogleAccount ? this.GetComponent<UILabel>((Enum) AccountRegistrationBase.UI.LBL_ADDRESS).text : this.GetAdjustBeforeText(this.secretGoogleAccountIndex);
    string inputText1 = this.GetInputText((Enum) AccountRegistrationBase.UI.IPT_PASSWORD);
    string inputText2 = this.GetInputText((Enum) AccountRegistrationBase.UI.IPT_CONFIRM_PASSWORD);
    string empty2 = string.Empty;
    if (string.IsNullOrEmpty(input))
    {
      this.CheckChangeEvent((is_send_event ? 1 : 0) != 0, "EMPTY", (object) new object[1]
      {
        (object) this.sectionData.GetText("ADDRESS")
      });
      return false;
    }
    if (!this.isGoogleAccount && !Regex.Match(input, "^[a-zA-Z0-9]+$").Success)
    {
      if (is_send_event)
        GameSection.ChangeEvent("ADDRESS_INCLUDE_NOT_ALPHANUMERIC");
      return false;
    }
    if (input.Length < 6)
    {
      this.CheckChangeEvent(is_send_event, "ADDRESS_TOO_SHORT");
      return false;
    }
    if (input.Length > (int) byte.MaxValue)
    {
      this.CheckChangeEvent(is_send_event, "ADDRESS_TOO_LONG");
      return false;
    }
    if (string.IsNullOrEmpty(inputText1))
    {
      this.CheckChangeEvent((is_send_event ? 1 : 0) != 0, "EMPTY", (object) new object[1]
      {
        (object) this.sectionData.GetText("PASSWORD")
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
    if (inputText1.Length > (int) byte.MaxValue)
    {
      this.CheckChangeEvent(is_send_event, "PASSWORD_TOO_LONG");
      return false;
    }
    if (!this.isGoogleAccount)
    {
      if (!this.isSelectedSecretQuest)
      {
        this.CheckChangeEvent((is_send_event ? 1 : 0) != 0, "EMPTY", (object) new object[1]
        {
          (object) this.sectionData.GetText("STR_SECRET_QUESTION_TEXT")
        });
        return false;
      }
      string text = this.GetComponent<UILabel>((Enum) AccountRegistrationBase.UI.LBL_SECRET_ANSER).text;
      if (string.IsNullOrEmpty(text))
      {
        this.CheckChangeEvent((is_send_event ? 1 : 0) != 0, "EMPTY", (object) new object[1]
        {
          (object) this.sectionData.GetText("STR_SECRET_ANSER_TEXT")
        });
        return false;
      }
      if (text.Length > 45)
      {
        this.CheckChangeEvent(is_send_event, "SECRET_QUESTION_TOO_LONG");
        return false;
      }
    }
    return true;
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
    string mail_address = this.GetComponent<UILabel>((Enum) AccountRegistrationBase.UI.LBL_ADDRESS).text;
    string inputText1 = this.GetInputText((Enum) AccountRegistrationBase.UI.IPT_PASSWORD);
    string inputText2 = this.GetInputText((Enum) AccountRegistrationBase.UI.IPT_CONFIRM_PASSWORD);
    string secretQuestionAnswer = this.isGoogleAccount ? string.Empty : this.GetComponent<UILabel>((Enum) AccountRegistrationBase.UI.LBL_SECRET_ANSER).text;
    GameSection.StayEvent();
    if (this.isGoogleAccount)
    {
      NetworkNative.GoogleAccount select_account = (NetworkNative.GoogleAccount) null;
      if (this.googleAccountList == null || this.googleAccountList.googleAccounts.Count == 0)
      {
        GameSection.ResumeEvent(false);
        return;
      }
      this.googleAccountList.googleAccounts.ForEach((Action<NetworkNative.GoogleAccount>) (data =>
      {
        if (select_account != null || !(data.name == mail_address))
          return;
        select_account = data;
      }));
      MonoBehaviourSingleton<AccountManager>.I.SendRegistCreateGoogleAccount(select_account.name, select_account.key, inputText1, inputText2, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
    }
    else
      MonoBehaviourSingleton<AccountManager>.I.SendRegistCreateRobAccount(mail_address, inputText1, inputText2, this.secretQuestionIndex + 1, secretQuestionAnswer, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
    MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory("AccountSettings");
    MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory("AccountRegistrationMail");
  }

  private enum UI
  {
    OBJ_SECRET_QUESTION,
    POP_SECRET_QUESTION,
    LBL_SECRET_QUESTION,
    IPT_ADDRESS,
    POP_ADDRESS,
    IPT_PASSWORD,
    IPT_CONFIRM_PASSWORD,
    IPT_SECRET_ANSER,
    LBL_ADDRESS,
    LBL_PASSWORD,
    LBL_CONFIRM_PASSWORD,
    LBL_SECRET_ANSER,
    BTN_OK,
    BTN_INVALID,
  }
}
