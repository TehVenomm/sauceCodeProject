// Decompiled with JetBrains decompiler
// Type: AccountContact
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
public class AccountContact : GameSection
{
  private int secretQuestionIndex;
  private bool isSelectedSecretQuestion;
  private List<string> secreteQuestion;
  private string[] GET_TARGET_ADDRESS_TEXT = new string[1]
  {
    "ACCOUNT_MAIL"
  };

  public override void Initialize()
  {
    this.isSelectedSecretQuestion = false;
    this.secreteQuestion = new List<string>();
    this.secreteQuestion.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 0U));
    this.secreteQuestion.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 1U));
    this.secreteQuestion.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 2U));
    this.secreteQuestion.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 3U));
    this.secreteQuestion.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 4U));
    this.secreteQuestion.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 5U));
    this.secreteQuestion.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 6U));
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetInput((Enum) AccountContact.UI.IPT_ADDRESS, string.Empty, (int) byte.MaxValue, new EventDelegate.Callback(this.InputCallback));
    this.SetInput((Enum) AccountContact.UI.IPT_SECRET_ANSER, string.Empty, 45, new EventDelegate.Callback(this.InputCallback));
    this.SetInput((Enum) AccountContact.UI.IPT_USER_NAME, string.Empty, 14, new EventDelegate.Callback(this.InputCallback));
    this.SetInput((Enum) AccountContact.UI.IPT_USER_RANK, string.Empty, 14, new EventDelegate.Callback(this.InputCallback));
    this.UpdateTargetAddressText();
    this.SetActive((Enum) AccountContact.UI.OBJ_SECRET_QUESTION, true);
    this.secretQuestionIndex = 0;
    this.SetPopupListText((Enum) AccountContact.UI.POP_SECRET_QUESTION, this.secreteQuestion, this.secretQuestionIndex);
    this.SetPopupListOnChange((Enum) AccountContact.UI.POP_SECRET_QUESTION, (Enum) AccountContact.UI.LBL_SECRET_QUESTION, (EventDelegate.Callback) (() =>
    {
      this.secretQuestionIndex = this.secreteQuestion.IndexOf(this.GetComponent<UILabel>((Enum) AccountContact.UI.LBL_SECRET_QUESTION).text);
      this.isSelectedSecretQuestion = true;
      this.InputCallback();
    }));
  }

  private void UpdateTargetAddressText()
  {
    int index = 0;
    this.SetLabelText((Enum) AccountContact.UI.LBL_ADDRESS_TEXT, string.Format(this.sectionData.GetText("STR_ADDRESS_TEXT"), (object) this.sectionData.GetText(this.GET_TARGET_ADDRESS_TEXT[index])));
  }

  private void InputCallback()
  {
    bool is_visible = this.CheckInputData();
    this.SetActive((Enum) AccountContact.UI.BTN_OK, is_visible);
    this.SetActive((Enum) AccountContact.UI.BTN_INVALID, !is_visible);
  }

  private bool CheckInputData(bool is_send_event = false)
  {
    string text1 = this.GetComponent<UILabel>((Enum) AccountContact.UI.LBL_ADDRESS).text;
    string text2 = this.GetComponent<UILabel>((Enum) AccountContact.UI.LBL_SECRET_ANSER).text;
    string text3 = this.GetComponent<UILabel>((Enum) AccountContact.UI.LBL_USER_NAME).text;
    string text4 = this.GetComponent<UILabel>((Enum) AccountContact.UI.LBL_USER_RANK).text;
    if (string.IsNullOrEmpty(text1))
    {
      this.CheckChangeEvent((is_send_event ? 1 : 0) != 0, "EMPTY", (object) new object[1]
      {
        (object) this.sectionData.GetText("ACCOUNT_MAIL")
      });
      return false;
    }
    if (text1.Length < 6)
    {
      this.CheckChangeEvent(is_send_event, "ADDRESS_TOO_SHORT");
      return false;
    }
    if (text1.Length > (int) byte.MaxValue)
    {
      this.CheckChangeEvent(is_send_event, "ADDRESS_TOO_LONG");
      return false;
    }
    if (!this.isSelectedSecretQuestion)
    {
      this.CheckChangeEvent(is_send_event, "NON_SELECT_SECRET_QUESTION");
      return false;
    }
    if (string.IsNullOrEmpty(text2))
    {
      this.CheckChangeEvent((is_send_event ? 1 : 0) != 0, "EMPTY", (object) new object[1]
      {
        (object) this.sectionData.GetText("STR_SECRET_ANSER_TEXT")
      });
      return false;
    }
    if (text2.Length > 45)
    {
      this.CheckChangeEvent(is_send_event, "SECRET_QUESTION_TOO_LONG");
      return false;
    }
    if (string.IsNullOrEmpty(text3))
    {
      this.CheckChangeEvent((is_send_event ? 1 : 0) != 0, "EMPTY", (object) new object[1]
      {
        (object) this.sectionData.GetText("USER_NAME")
      });
      return false;
    }
    if (text3.Length > 14)
    {
      this.CheckChangeEvent(is_send_event, "NAME_TOO_LONG");
      return false;
    }
    if (string.IsNullOrEmpty(text4))
    {
      this.CheckChangeEvent((is_send_event ? 1 : 0) != 0, "EMPTY", (object) new object[1]
      {
        (object) this.sectionData.GetText("USER_RANK")
      });
      return false;
    }
    if (!uint.TryParse(text4, out uint _))
    {
      this.CheckChangeEvent((is_send_event ? 1 : 0) != 0, "ERR_INPUT_VALUE", (object) new object[1]
      {
        (object) this.sectionData.GetText("USER_RANK")
      });
      return false;
    }
    if (text4.Length <= 14)
      return true;
    this.CheckChangeEvent(is_send_event, "RANK_TOO_LONG");
    return false;
  }

  private void CheckChangeEvent(bool is_send, string event_name = "", object event_data = null)
  {
    if (!is_send)
      return;
    GameSection.ChangeEvent(event_name, event_data);
  }

  private void OnQuery_CONTACT()
  {
    if (!this.CheckInputData(true))
      return;
    string text1 = this.GetComponent<UILabel>((Enum) AccountContact.UI.LBL_ADDRESS).text;
    string text2 = this.GetComponent<UILabel>((Enum) AccountContact.UI.LBL_SECRET_ANSER).text;
    string text3 = this.GetComponent<UILabel>((Enum) AccountContact.UI.LBL_USER_NAME).text;
    string text4 = this.GetComponent<UILabel>((Enum) AccountContact.UI.LBL_USER_RANK).text;
    string defaultUserAgent = NetworkNative.getDefaultUserAgent();
    string nativeVersionName = NetworkNative.getNativeVersionName();
    string str = "support@gogame.net";
    string url1 = "Dragon Project [Login]";
    string url2 = string.Format($"1. [Username and current level]\nUsername：{{0}}\nLevel：{{1}}\n\n2. [Registered Dragon Project ID]\n{{2}}\n{{3}}\n\n{"3. [Answer to secret question]\n{4}\n\n"}※Please do not erase the information below\nDevice details：{{5}}\nApp details：{{6}}\n", (object) text3, (object) text4, (object) this.sectionData.GetText("ACCOUNT_MAIL"), (object) text1, (object) text2, (object) defaultUserAgent, (object) nativeVersionName);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("mailto:");
    stringBuilder.Append(str);
    stringBuilder.Append("?subject=");
    stringBuilder.Append(this.EscapeURL(url1));
    stringBuilder.Append("&body=");
    stringBuilder.Append(this.EscapeURL(url2));
    Native.OpenURL(stringBuilder.ToString());
  }

  private string EscapeURL(string url) => Uri.EscapeDataString(url).Replace("+", "%20");

  private enum UI
  {
    LBL_ADDRESS,
    LBL_ADDRESS_TEXT,
    IPT_ADDRESS,
    POP_ADDRESS_TYPE,
    LBL_ADDRESS_TYPE,
    OBJ_SECRET_QUESTION,
    POP_SECRET_QUESTION,
    LBL_SECRET_QUESTION,
    IPT_SECRET_ANSER,
    LBL_SECRET_ANSER,
    IPT_USER_NAME,
    LBL_USER_NAME,
    IPT_USER_RANK,
    LBL_USER_RANK,
    BTN_OK,
    BTN_INVALID,
  }
}
