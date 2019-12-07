using System.Collections.Generic;
using System.Text.RegularExpressions;

public class AccountLoginBase : AccountPopupAdjuster
{
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
		BTN_INVALID
	}

	protected bool isGoogleAccount;

	protected bool isValidGoogleAccountPopup;

	private NetworkNative.GoogleAccountInfo googleAccountList;

	private int selectGoogleAccountIndex;

	public override void Initialize()
	{
		isValidGoogleAccountPopup = false;
		isValidGoogleAccountPopup = isGoogleAccount;
		selectGoogleAccountIndex = -1;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetActive(UI.IPT_ADDRESS, !isValidGoogleAccountPopup);
		SetActive(UI.POP_ADDRESS, isValidGoogleAccountPopup);
		SetInput(UI.IPT_ADDRESS, string.Empty, 255, InputCallback);
		SetInput(UI.IPT_PASSWORD, string.Empty, 255, InputCallback);
		if (isValidGoogleAccountPopup)
		{
			List<string> account_list = null;
			googleAccountList = NetworkNative.getGoogleAccounts();
			if (googleAccountList.googleAccounts.Count > 0)
			{
				UILabel lbl = GetComponent<UILabel>(UI.LBL_ADDRESS);
				account_list = new List<string>();
				googleAccountList.googleAccounts.ForEach(delegate(NetworkNative.GoogleAccount account)
				{
					account_list.Add(PopupTextAdjust(lbl, account.name));
				});
			}
			SetPopupListText(UI.POP_ADDRESS, account_list);
			SetPopupListOnChange(UI.POP_ADDRESS, UI.LBL_ADDRESS, InputCallback_Address);
		}
		SetLabelText(UI.LBL_ADDRESS_TEXT, base.sectionData.GetText(isGoogleAccount ? "GOOGLE" : "MAIL"));
		base.UpdateUI();
	}

	private void InputCallback_Address()
	{
		UIPopupList component = GetComponent<UIPopupList>(UI.POP_ADDRESS);
		selectGoogleAccountIndex = component.items.IndexOf(component.value);
		InputCallback();
	}

	private void InputCallback()
	{
		bool flag = CheckInputLoginData();
		SetActive(UI.BTN_OK, flag);
		SetActive(UI.BTN_INVALID, !flag);
	}

	private bool CheckInputLoginData(bool is_send_event = false)
	{
		string text = (!isValidGoogleAccountPopup) ? GetComponent<UILabel>(UI.LBL_ADDRESS).text : GetAdjustBeforeText(selectGoogleAccountIndex);
		string inputText = GetInputText(UI.IPT_PASSWORD);
		if (string.IsNullOrEmpty(text))
		{
			if (is_send_event)
			{
				GameSection.ChangeEvent("EMPTY", new object[1]
				{
					base.sectionData.GetText("ADDRESS")
				});
			}
			return false;
		}
		if (!isGoogleAccount && !Regex.Match(text, "^[a-zA-Z0-9]+$").Success)
		{
			if (is_send_event)
			{
				GameSection.ChangeEvent("ADDRESS_INCLUDE_NOT_ALPHANUMERIC");
			}
			return false;
		}
		if (text.Length < 6)
		{
			if (is_send_event)
			{
				GameSection.ChangeEvent("ADDRESS_TOO_SHORT");
			}
			return false;
		}
		if (text.Length > 255)
		{
			if (is_send_event)
			{
				GameSection.ChangeEvent("ADDRESS_TOO_LONG");
			}
			return false;
		}
		if (string.IsNullOrEmpty(inputText))
		{
			if (is_send_event)
			{
				GameSection.ChangeEvent("EMPTY", new object[1]
				{
					base.sectionData.GetText("STR_PASSWORD_TEXT")
				});
			}
			return false;
		}
		if (inputText.Length < 8)
		{
			if (is_send_event)
			{
				GameSection.ChangeEvent("PASSWORD_TOO_SHORT");
			}
			return false;
		}
		if (inputText.Length > 255)
		{
			if (is_send_event)
			{
				GameSection.ChangeEvent("PASSWORD_TOO_LONG");
			}
			return false;
		}
		return true;
	}

	private void OnQuery_LOGIN()
	{
		if (!CheckInputLoginData(is_send_event: true))
		{
			return;
		}
		string address = GetComponent<UILabel>(UI.LBL_ADDRESS).text;
		string inputText = GetInputText(UI.IPT_PASSWORD);
		GameSection.StayEvent();
		if (isGoogleAccount)
		{
			string text = null;
			string text2 = null;
			if (isValidGoogleAccountPopup)
			{
				NetworkNative.GoogleAccount select_account = null;
				if (googleAccountList == null || googleAccountList.googleAccounts.Count == 0)
				{
					GameSection.ResumeEvent(is_resume: false);
					return;
				}
				googleAccountList.googleAccounts.ForEach(delegate(NetworkNative.GoogleAccount data)
				{
					if (select_account == null && data.name == address)
					{
						select_account = data;
					}
				});
				text = select_account.name;
				text2 = select_account.key;
			}
			else
			{
				text = address;
				text2 = string.Empty;
			}
			MonoBehaviourSingleton<AccountManager>.I.SendRegistAuthGoogle(text, text2, inputText, delegate(bool is_success)
			{
				if (is_success)
				{
					ToReset();
				}
				GameSection.ResumeEvent(is_success);
			});
		}
		else
		{
			MonoBehaviourSingleton<AccountManager>.I.SendRegistAuthRob(address, inputText, delegate(bool is_success)
			{
				if (is_success)
				{
					ToReset();
				}
				GameSection.ResumeEvent(is_success);
			});
		}
	}

	private void ToReset()
	{
		MonoBehaviourSingleton<NativeGameService>.I.SetOldUserLogin();
		MenuReset.needClearCache = true;
		MenuReset.needPredownload = true;
	}
}
