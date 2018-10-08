using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class AccountRegistrationBase : AccountPopupAdjuster
{
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
		BTN_INVALID
	}

	protected bool isGoogleAccount;

	private bool isSelectedSecretQuest;

	private int secretQuestionIndex;

	private NetworkNative.GoogleAccountInfo googleAccountList;

	private int secretGoogleAccountIndex;

	public override void Initialize()
	{
		secretQuestionIndex = -1;
		secretGoogleAccountIndex = -1;
		isSelectedSecretQuest = false;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.IPT_ADDRESS, !isGoogleAccount);
		SetActive((Enum)UI.POP_ADDRESS, isGoogleAccount);
		SetActive((Enum)UI.OBJ_SECRET_QUESTION, !isGoogleAccount);
		SetInput((Enum)UI.IPT_ADDRESS, string.Empty, 255, (EventDelegate.Callback)InputCallBack);
		SetInput((Enum)UI.IPT_PASSWORD, string.Empty, 255, (EventDelegate.Callback)InputCallBack);
		SetInput((Enum)UI.IPT_CONFIRM_PASSWORD, string.Empty, 255, (EventDelegate.Callback)InputCallBack);
		if (!isGoogleAccount)
		{
			SetInput((Enum)UI.IPT_SECRET_ANSER, string.Empty, 45, (EventDelegate.Callback)InputCallBack);
		}
		if (isGoogleAccount)
		{
			List<string> account_list = null;
			googleAccountList = NetworkNative.getGoogleAccounts();
			if (googleAccountList.googleAccounts.Count > 0)
			{
				UILabel lbl = base.GetComponent<UILabel>((Enum)UI.LBL_ADDRESS);
				account_list = new List<string>();
				googleAccountList.googleAccounts.ForEach(delegate(NetworkNative.GoogleAccount account)
				{
					account_list.Add(PopupTextAdjust(lbl, account.name));
				});
			}
			SetPopupListText((Enum)UI.POP_ADDRESS, account_list, -1);
			SetPopupListOnChange((Enum)UI.POP_ADDRESS, (Enum)UI.LBL_ADDRESS, (EventDelegate.Callback)InputCallBack_Address);
		}
		else
		{
			List<string> list = new List<string>();
			list.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 0u));
			list.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 1u));
			list.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 2u));
			list.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 3u));
			list.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 4u));
			list.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 5u));
			list.Add(StringTable.Get(STRING_CATEGORY.ACCOUNT, 6u));
			SetPopupListText((Enum)UI.POP_SECRET_QUESTION, list, -1);
			SetPopupListOnChange((Enum)UI.POP_SECRET_QUESTION, (Enum)UI.LBL_SECRET_QUESTION, (EventDelegate.Callback)InputCallBack_SecretQuestion);
		}
	}

	private void InputCallBack_SecretQuestion()
	{
		UIPopupList component = base.GetComponent<UIPopupList>((Enum)UI.POP_SECRET_QUESTION);
		secretQuestionIndex = component.items.IndexOf(component.value);
		isSelectedSecretQuest = true;
		InputCallBack();
	}

	private void InputCallBack_Address()
	{
		UIPopupList component = base.GetComponent<UIPopupList>((Enum)UI.POP_ADDRESS);
		secretGoogleAccountIndex = component.items.IndexOf(component.value);
		InputCallBack();
	}

	private void InputCallBack()
	{
		bool flag = CheckRegistData(false);
		SetActive((Enum)UI.BTN_OK, flag);
		SetActive((Enum)UI.BTN_INVALID, !flag);
	}

	private bool CheckRegistData(bool is_send_event = false)
	{
		string empty = string.Empty;
		empty = ((!isGoogleAccount) ? base.GetComponent<UILabel>((Enum)UI.LBL_ADDRESS).text : GetAdjustBeforeText(secretGoogleAccountIndex));
		string inputText = GetInputText(UI.IPT_PASSWORD);
		string inputText2 = GetInputText(UI.IPT_CONFIRM_PASSWORD);
		string empty2 = string.Empty;
		if (string.IsNullOrEmpty(empty))
		{
			CheckChangeEvent(is_send_event, "EMPTY", new object[1]
			{
				base.sectionData.GetText("ADDRESS")
			});
			return false;
		}
		if (!isGoogleAccount && !Regex.Match(empty, "^[a-zA-Z0-9]+$").Success)
		{
			if (is_send_event)
			{
				GameSection.ChangeEvent("ADDRESS_INCLUDE_NOT_ALPHANUMERIC", null);
			}
			return false;
		}
		if (empty.Length < 6)
		{
			CheckChangeEvent(is_send_event, "ADDRESS_TOO_SHORT", null);
			return false;
		}
		if (empty.Length > 255)
		{
			CheckChangeEvent(is_send_event, "ADDRESS_TOO_LONG", null);
			return false;
		}
		if (string.IsNullOrEmpty(inputText))
		{
			CheckChangeEvent(is_send_event, "EMPTY", new object[1]
			{
				base.sectionData.GetText("PASSWORD")
			});
			return false;
		}
		if (inputText.Length < 8)
		{
			CheckChangeEvent(is_send_event, "PASSWORD_TOO_SHORT", null);
			return false;
		}
		if (inputText != inputText2)
		{
			CheckChangeEvent(is_send_event, "CONFIRM_PASSWORD_NOT_MATCH", null);
			return false;
		}
		if (inputText.Length > 255)
		{
			CheckChangeEvent(is_send_event, "PASSWORD_TOO_LONG", null);
			return false;
		}
		if (!isGoogleAccount)
		{
			if (!isSelectedSecretQuest)
			{
				CheckChangeEvent(is_send_event, "EMPTY", new object[1]
				{
					base.sectionData.GetText("STR_SECRET_QUESTION_TEXT")
				});
				return false;
			}
			empty2 = base.GetComponent<UILabel>((Enum)UI.LBL_SECRET_ANSER).text;
			if (string.IsNullOrEmpty(empty2))
			{
				CheckChangeEvent(is_send_event, "EMPTY", new object[1]
				{
					base.sectionData.GetText("STR_SECRET_ANSER_TEXT")
				});
				return false;
			}
			if (empty2.Length > 45)
			{
				CheckChangeEvent(is_send_event, "SECRET_QUESTION_TOO_LONG", null);
				return false;
			}
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

	private void OnQuery_OK()
	{
		if (CheckRegistData(true))
		{
			string mail_address = base.GetComponent<UILabel>((Enum)UI.LBL_ADDRESS).text;
			string inputText = GetInputText(UI.IPT_PASSWORD);
			string inputText2 = GetInputText(UI.IPT_CONFIRM_PASSWORD);
			string secretQuestionAnswer = (!isGoogleAccount) ? base.GetComponent<UILabel>((Enum)UI.LBL_SECRET_ANSER).text : string.Empty;
			GameSection.StayEvent();
			if (isGoogleAccount)
			{
				NetworkNative.GoogleAccount select_account = null;
				if (googleAccountList == null || googleAccountList.googleAccounts.Count == 0)
				{
					GameSection.ResumeEvent(false, null, false);
				}
				else
				{
					googleAccountList.googleAccounts.ForEach(delegate(NetworkNative.GoogleAccount data)
					{
						if (select_account == null && data.name == mail_address)
						{
							select_account = data;
						}
					});
					MonoBehaviourSingleton<AccountManager>.I.SendRegistCreateGoogleAccount(select_account.name, select_account.key, inputText, inputText2, delegate(bool is_success)
					{
						GameSection.ResumeEvent(is_success, null, false);
					});
				}
			}
			else
			{
				MonoBehaviourSingleton<AccountManager>.I.SendRegistCreateRobAccount(mail_address, inputText, inputText2, secretQuestionIndex + 1, secretQuestionAnswer, delegate(bool is_success)
				{
					GameSection.ResumeEvent(is_success, null, false);
				});
			}
		}
	}
}
