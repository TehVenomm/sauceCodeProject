// Decompiled with JetBrains decompiler
// Type: AccountLoginBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable
public class AccountLoginBase : AccountPopupAdjuster
{
  protected bool isGoogleAccount;
  protected bool isValidGoogleAccountPopup;
  private NetworkNative.GoogleAccountInfo googleAccountList;
  private int selectGoogleAccountIndex;

  public override void Initialize()
  {
    this.isValidGoogleAccountPopup = false;
    this.isValidGoogleAccountPopup = this.isGoogleAccount;
    this.selectGoogleAccountIndex = -1;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) AccountLoginBase.UI.IPT_ADDRESS, !this.isValidGoogleAccountPopup);
    this.SetActive((Enum) AccountLoginBase.UI.POP_ADDRESS, this.isValidGoogleAccountPopup);
    this.SetInput((Enum) AccountLoginBase.UI.IPT_ADDRESS, string.Empty, (int) byte.MaxValue, new EventDelegate.Callback(this.InputCallback));
    this.SetInput((Enum) AccountLoginBase.UI.IPT_PASSWORD, string.Empty, (int) byte.MaxValue, new EventDelegate.Callback(this.InputCallback));
    if (this.isValidGoogleAccountPopup)
    {
      List<string> account_list = (List<string>) null;
      this.googleAccountList = NetworkNative.getGoogleAccounts();
      if (this.googleAccountList.googleAccounts.Count > 0)
      {
        UILabel lbl = this.GetComponent<UILabel>((Enum) AccountLoginBase.UI.LBL_ADDRESS);
        account_list = new List<string>();
        this.googleAccountList.googleAccounts.ForEach((Action<NetworkNative.GoogleAccount>) (account => account_list.Add(this.PopupTextAdjust(lbl, account.name))));
      }
      this.SetPopupListText((Enum) AccountLoginBase.UI.POP_ADDRESS, account_list);
      this.SetPopupListOnChange((Enum) AccountLoginBase.UI.POP_ADDRESS, (Enum) AccountLoginBase.UI.LBL_ADDRESS, new EventDelegate.Callback(this.InputCallback_Address));
    }
    this.SetLabelText((Enum) AccountLoginBase.UI.LBL_ADDRESS_TEXT, this.sectionData.GetText(this.isGoogleAccount ? "GOOGLE" : "MAIL"));
    base.UpdateUI();
  }

  private void InputCallback_Address()
  {
    UIPopupList component = this.GetComponent<UIPopupList>((Enum) AccountLoginBase.UI.POP_ADDRESS);
    this.selectGoogleAccountIndex = component.items.IndexOf(component.value);
    this.InputCallback();
  }

  private void InputCallback()
  {
    bool is_visible = this.CheckInputLoginData();
    this.SetActive((Enum) AccountLoginBase.UI.BTN_OK, is_visible);
    this.SetActive((Enum) AccountLoginBase.UI.BTN_INVALID, !is_visible);
  }

  private bool CheckInputLoginData(bool is_send_event = false)
  {
    string input = !this.isValidGoogleAccountPopup ? this.GetComponent<UILabel>((Enum) AccountLoginBase.UI.LBL_ADDRESS).text : this.GetAdjustBeforeText(this.selectGoogleAccountIndex);
    string inputText = this.GetInputText((Enum) AccountLoginBase.UI.IPT_PASSWORD);
    if (string.IsNullOrEmpty(input))
    {
      if (is_send_event)
        GameSection.ChangeEvent("EMPTY", (object) new object[1]
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
      if (is_send_event)
        GameSection.ChangeEvent("ADDRESS_TOO_SHORT");
      return false;
    }
    if (input.Length > (int) byte.MaxValue)
    {
      if (is_send_event)
        GameSection.ChangeEvent("ADDRESS_TOO_LONG");
      return false;
    }
    if (string.IsNullOrEmpty(inputText))
    {
      if (is_send_event)
        GameSection.ChangeEvent("EMPTY", (object) new object[1]
        {
          (object) this.sectionData.GetText("STR_PASSWORD_TEXT")
        });
      return false;
    }
    if (inputText.Length < 8)
    {
      if (is_send_event)
        GameSection.ChangeEvent("PASSWORD_TOO_SHORT");
      return false;
    }
    if (inputText.Length <= (int) byte.MaxValue)
      return true;
    if (is_send_event)
      GameSection.ChangeEvent("PASSWORD_TOO_LONG");
    return false;
  }

  private void OnQuery_LOGIN()
  {
    if (!this.CheckInputLoginData(true))
      return;
    string address = this.GetComponent<UILabel>((Enum) AccountLoginBase.UI.LBL_ADDRESS).text;
    string inputText = this.GetInputText((Enum) AccountLoginBase.UI.IPT_PASSWORD);
    GameSection.StayEvent();
    if (this.isGoogleAccount)
    {
      string account;
      string key;
      if (this.isValidGoogleAccountPopup)
      {
        NetworkNative.GoogleAccount select_account = (NetworkNative.GoogleAccount) null;
        if (this.googleAccountList == null || this.googleAccountList.googleAccounts.Count == 0)
        {
          GameSection.ResumeEvent(false);
          return;
        }
        this.googleAccountList.googleAccounts.ForEach((Action<NetworkNative.GoogleAccount>) (data =>
        {
          if (select_account != null || !(data.name == address))
            return;
          select_account = data;
        }));
        account = select_account.name;
        key = select_account.key;
      }
      else
      {
        account = address;
        key = string.Empty;
      }
      MonoBehaviourSingleton<AccountManager>.I.SendRegistAuthGoogle(account, key, inputText, (Action<bool>) (is_success =>
      {
        if (is_success)
          this.ToReset();
        GameSection.ResumeEvent(is_success);
      }));
    }
    else
      MonoBehaviourSingleton<AccountManager>.I.SendRegistAuthRob(address, inputText, (Action<bool>) (is_success =>
      {
        if (is_success)
          this.ToReset();
        GameSection.ResumeEvent(is_success);
      }));
  }

  private void ToReset()
  {
    MonoBehaviourSingleton<NativeGameService>.I.SetOldUserLogin();
    MenuReset.needClearCache = true;
    MenuReset.needPredownload = true;
  }

  private enum UI
  {
    LBL_LOGIN,
    LBL_ADDRESS_TEXT,
    LBL_PASSWORD,
    LBL_ADDRESS,
    IPT_ADDRESS,
    POP_ADDRESS,
    IPT_PASSWORD,
    BTN_OK,
    BTN_INVALID,
  }
}
