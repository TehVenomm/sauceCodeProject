using System;
using System.Collections.Generic;
using System.Text;

public class AccountContact : GameSection
{
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
		BTN_INVALID
	}

	private int secretQuestionIndex;

	private bool isSelectedSecretQuestion;

	private List<string> secreteQuestion;

	private string[] GET_TARGET_ADDRESS_TEXT = new string[1]
	{
		"ACCOUNT_MAIL"
	};

	public override void Initialize()
	{
		isSelectedSecretQuestion = false;
		secreteQuestion = new List<string>();
		secreteQuestion.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 0u));
		secreteQuestion.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 1u));
		secreteQuestion.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 2u));
		secreteQuestion.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 3u));
		secreteQuestion.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 4u));
		secreteQuestion.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 5u));
		secreteQuestion.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 6u));
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetInput((Enum)UI.IPT_ADDRESS, string.Empty, 255, (EventDelegate.Callback)InputCallback);
		SetInput((Enum)UI.IPT_SECRET_ANSER, string.Empty, 45, (EventDelegate.Callback)InputCallback);
		SetInput((Enum)UI.IPT_USER_NAME, string.Empty, 12, (EventDelegate.Callback)InputCallback);
		SetInput((Enum)UI.IPT_USER_RANK, string.Empty, 12, (EventDelegate.Callback)InputCallback);
		UpdateTargetAddressText();
		SetActive((Enum)UI.OBJ_SECRET_QUESTION, true);
		secretQuestionIndex = 0;
		SetPopupListText((Enum)UI.POP_SECRET_QUESTION, secreteQuestion, secretQuestionIndex);
		SetPopupListOnChange((Enum)UI.POP_SECRET_QUESTION, (Enum)UI.LBL_SECRET_QUESTION, (EventDelegate.Callback)delegate
		{
			string text = base.GetComponent<UILabel>((Enum)UI.LBL_SECRET_QUESTION).text;
			secretQuestionIndex = secreteQuestion.IndexOf(text);
			isSelectedSecretQuestion = true;
			InputCallback();
		});
	}

	private void UpdateTargetAddressText()
	{
		int num = 0;
		string text = string.Format(base.sectionData.GetText("STR_ADDRESS_TEXT"), base.sectionData.GetText(GET_TARGET_ADDRESS_TEXT[num]));
		SetLabelText((Enum)UI.LBL_ADDRESS_TEXT, text);
	}

	private void InputCallback()
	{
		bool flag = CheckInputData(false);
		SetActive((Enum)UI.BTN_OK, flag);
		SetActive((Enum)UI.BTN_INVALID, !flag);
	}

	private bool CheckInputData(bool is_send_event = false)
	{
		string text = base.GetComponent<UILabel>((Enum)UI.LBL_ADDRESS).text;
		string text2 = base.GetComponent<UILabel>((Enum)UI.LBL_SECRET_ANSER).text;
		string text3 = base.GetComponent<UILabel>((Enum)UI.LBL_USER_NAME).text;
		string text4 = base.GetComponent<UILabel>((Enum)UI.LBL_USER_RANK).text;
		if (string.IsNullOrEmpty(text))
		{
			CheckChangeEvent(is_send_event, "EMPTY", new object[1]
			{
				base.sectionData.GetText("ACCOUNT_MAIL")
			});
			return false;
		}
		if (text.Length < 6)
		{
			CheckChangeEvent(is_send_event, "ADDRESS_TOO_SHORT", null);
			return false;
		}
		if (text.Length > 255)
		{
			CheckChangeEvent(is_send_event, "ADDRESS_TOO_LONG", null);
			return false;
		}
		if (!isSelectedSecretQuestion)
		{
			CheckChangeEvent(is_send_event, "NON_SELECT_SECRET_QUESTION", null);
			return false;
		}
		if (string.IsNullOrEmpty(text2))
		{
			CheckChangeEvent(is_send_event, "EMPTY", new object[1]
			{
				base.sectionData.GetText("STR_SECRET_ANSER_TEXT")
			});
			return false;
		}
		if (text2.Length > 45)
		{
			CheckChangeEvent(is_send_event, "SECRET_QUESTION_TOO_LONG", null);
			return false;
		}
		if (string.IsNullOrEmpty(text3))
		{
			CheckChangeEvent(is_send_event, "EMPTY", new object[1]
			{
				base.sectionData.GetText("USER_NAME")
			});
			return false;
		}
		if (text3.Length > 12)
		{
			CheckChangeEvent(is_send_event, "NAME_TOO_LONG", null);
			return false;
		}
		if (string.IsNullOrEmpty(text4))
		{
			CheckChangeEvent(is_send_event, "EMPTY", new object[1]
			{
				base.sectionData.GetText("USER_RANK")
			});
			return false;
		}
		if (!uint.TryParse(text4, out uint _))
		{
			CheckChangeEvent(is_send_event, "ERR_INPUT_VALUE", new object[1]
			{
				base.sectionData.GetText("USER_RANK")
			});
			return false;
		}
		if (text4.Length > 12)
		{
			CheckChangeEvent(is_send_event, "RANK_TOO_LONG", null);
			return false;
		}
		return true;
	}

	private void CheckChangeEvent(bool is_send, string event_name = "", object event_data = null)
	{
		if (is_send)
		{
			GameSection.ChangeEvent(event_name, event_data);
		}
	}

	private void OnQuery_CONTACT()
	{
		if (CheckInputData(true))
		{
			string text = base.GetComponent<UILabel>((Enum)UI.LBL_ADDRESS).text;
			string text2 = base.GetComponent<UILabel>((Enum)UI.LBL_SECRET_ANSER).text;
			string text3 = base.GetComponent<UILabel>((Enum)UI.LBL_USER_NAME).text;
			string text4 = base.GetComponent<UILabel>((Enum)UI.LBL_USER_RANK).text;
			string defaultUserAgent = NetworkNative.getDefaultUserAgent();
			string nativeVersionName = NetworkNative.getNativeVersionName();
			string value = "support@gogame.net";
			string url = "Dragon Project [Login]";
			string text5 = "3. [Answer to secret question]\n{4}\n\n";
			string text6 = "[Registered Dragon Project ID]";
			string url2 = string.Format("1. [Username and current level]\nUsername：{0}\nLevel：{1}\n\n2. " + text6 + "\n{2}\n{3}\n\n" + text5 + "※Please do not erase the information below\nDevice details：{5}\nApp details：{6}\n", text3, text4, base.sectionData.GetText("ACCOUNT_MAIL"), text, text2, defaultUserAgent, nativeVersionName);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("mailto:");
			stringBuilder.Append(value);
			stringBuilder.Append("?subject=");
			stringBuilder.Append(EscapeURL(url));
			stringBuilder.Append("&body=");
			stringBuilder.Append(EscapeURL(url2));
			Native.OpenURL(stringBuilder.ToString());
		}
	}

	private string EscapeURL(string url)
	{
		return Uri.EscapeDataString(url).Replace("+", "%20");
	}
}
