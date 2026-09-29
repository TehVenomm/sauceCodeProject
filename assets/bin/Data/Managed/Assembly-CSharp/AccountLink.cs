// Decompiled with JetBrains decompiler
// Type: AccountLink
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

#nullable disable
public class AccountLink : AccountLoginBase
{
  public override string overrideBackKeyEvent => "[BACK]";

  public override void Initialize() => base.Initialize();

  public override void UpdateUI()
  {
    this.SetActive((Enum) AccountLink.UI.IPT_ADDRESS, !this.isValidGoogleAccountPopup);
    this.SetActive((Enum) AccountLink.UI.POP_ADDRESS, this.isValidGoogleAccountPopup);
    this.SetInput((Enum) AccountLink.UI.IPT_ADDRESS, string.Empty, (int) byte.MaxValue, new EventDelegate.Callback(this.InputCallback));
    this.SetInput((Enum) AccountLink.UI.IPT_PASSWORD, string.Empty, (int) byte.MaxValue, new EventDelegate.Callback(this.InputCallback));
    if (this.isValidGoogleAccountPopup)
    {
      this.SetPopupListText((Enum) AccountLink.UI.POP_ADDRESS, (List<string>) null);
      this.SetPopupListOnChange((Enum) AccountLink.UI.POP_ADDRESS, (Enum) AccountLink.UI.LBL_ADDRESS, new EventDelegate.Callback(this.InputCallback));
    }
    this.SetLabelText((Enum) AccountLink.UI.LBL_ADDRESS_TEXT, this.sectionData.GetText(this.isGoogleAccount ? "GOOGLE" : "MAIL"));
    base.UpdateUI();
  }

  private void InputCallback()
  {
    bool is_visible = this.CheckInputLoginData();
    this.SetActive((Enum) AccountLink.UI.BTN_OK, is_visible);
    this.SetActive((Enum) AccountLink.UI.BTN_INVALID, !is_visible);
  }

  private bool CheckInputLoginData(bool is_send_event = false)
  {
    string text = this.GetComponent<UILabel>((Enum) AccountLink.UI.LBL_ADDRESS).text;
    string inputText = this.GetInputText((Enum) AccountLink.UI.IPT_PASSWORD);
    if (string.IsNullOrEmpty(text))
    {
      if (is_send_event)
        GameSection.ChangeEvent("EMPTY", (object) new object[1]
        {
          (object) this.sectionData.GetText("ADDRESS")
        });
      return false;
    }
    if (!this.isGoogleAccount && !Regex.Match(text, "^[a-zA-Z0-9]+$").Success)
    {
      if (is_send_event)
        GameSection.ChangeEvent("ADDRESS_INCLUDE_NOT_ALPHANUMERIC");
      return false;
    }
    if (text.Length < 6)
    {
      if (is_send_event)
        GameSection.ChangeEvent("ADDRESS_TOO_SHORT");
      return false;
    }
    if (text.Length > (int) byte.MaxValue)
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

  public void OnQuery_LOGIN()
  {
    if (!this.CheckInputLoginData(true))
      return;
    string address = this.GetComponent<UILabel>((Enum) AccountLink.UI.LBL_ADDRESS).text;
    string inputText = this.GetInputText((Enum) AccountLink.UI.IPT_PASSWORD);
    GameSection.StayEvent();
    MonoBehaviourSingleton<AccountManager>.I.SendLinkRob(address, inputText, (Action<bool, LinkRobModel>) ((success, ret) =>
    {
      if (success)
      {
        MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(0, (Action<bool>) (is_success =>
        {
          if (success)
          {
            MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY);
            GameSection.ChangeStayEvent("ACCOUNT_LOGIN");
          }
          GameSection.ResumeEvent(success);
        }));
      }
      else
      {
        if (ret.Error == Error.WRN_LINK_ROB_LINKED_WITH_ROB)
        {
          GameSection.ChangeStayEvent("ACCOUNT_CONFLICT", (object) new object[2]
          {
            (object) ret.existInfo,
            (object) address
          });
          success = true;
        }
        GameSection.ResumeEvent(success);
      }
    }));
  }

  public void OnCloseDialog_AccountUseLocalData() => Debug.LogError((object) "Just back");

  private new enum UI
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
