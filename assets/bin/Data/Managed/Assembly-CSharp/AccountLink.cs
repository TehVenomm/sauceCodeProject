using Network;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class AccountLink : AccountLoginBase
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

	public override string overrideBackKeyEvent => "[BACK]";

	public override void Initialize()
	{
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
			List<string> string_list = null;
			SetPopupListText(UI.POP_ADDRESS, string_list);
			SetPopupListOnChange(UI.POP_ADDRESS, UI.LBL_ADDRESS, InputCallback);
		}
		SetLabelText(UI.LBL_ADDRESS_TEXT, base.sectionData.GetText(isGoogleAccount ? "GOOGLE" : "MAIL"));
		base.UpdateUI();
	}

	private void InputCallback()
	{
		bool flag = CheckInputLoginData();
		SetActive(UI.BTN_OK, flag);
		SetActive(UI.BTN_INVALID, !flag);
	}

	private bool CheckInputLoginData(bool is_send_event = false)
	{
		string text = GetComponent<UILabel>(UI.LBL_ADDRESS).text;
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

	public void OnQuery_LOGIN()
	{
		if (CheckInputLoginData(is_send_event: true))
		{
			string address = GetComponent<UILabel>(UI.LBL_ADDRESS).text;
			string inputText = GetInputText(UI.IPT_PASSWORD);
			GameSection.StayEvent();
			MonoBehaviourSingleton<AccountManager>.I.SendLinkRob(address, inputText, delegate(bool success, LinkRobModel ret)
			{
				if (success)
				{
					MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(0, delegate
					{
						if (success)
						{
							MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY);
							GameSection.ChangeStayEvent("ACCOUNT_LOGIN");
						}
						GameSection.ResumeEvent(success);
					});
				}
				else
				{
					if (ret.Error == Error.WRN_LINK_ROB_LINKED_WITH_ROB)
					{
						GameSection.ChangeStayEvent("ACCOUNT_CONFLICT", new object[2]
						{
							ret.existInfo,
							address
						});
						success = true;
					}
					GameSection.ResumeEvent(success);
				}
			});
		}
	}

	public void OnCloseDialog_AccountUseLocalData()
	{
		Debug.LogError("Just back");
	}
}
