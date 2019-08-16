using System;

public class TradingPostUserAgreement : GameSection
{
	private enum UI
	{
		MESSAGE,
		OBJ_FRAME,
		LBL_BTN_0,
		LBL_BTN_0_R,
		LBL_BTN_2,
		LBL_BTN_2_R
	}

	public override void Initialize()
	{
		SetSupportEncoding(UI.MESSAGE, isEnable: true);
		SetLabelText((Enum)UI.MESSAGE, base.sectionData.GetText("STR_MESSAGE"));
		SetLabelText((Enum)UI.LBL_BTN_0, base.sectionData.GetText("TEXT_BTN_BACK"));
		SetLabelText((Enum)UI.LBL_BTN_0_R, base.sectionData.GetText("TEXT_BTN_BACK"));
		SetLabelText((Enum)UI.LBL_BTN_2, base.sectionData.GetText("TEXT_BTN_CONTINUE"));
		SetLabelText((Enum)UI.LBL_BTN_2_R, base.sectionData.GetText("TEXT_BTN_CONTINUE"));
		if (TradingPostManager.IsAcceptUserAgreement())
		{
			SetActive((Enum)UI.OBJ_FRAME, is_visible: false);
		}
		base.Initialize();
	}

	public override void StartSection()
	{
		if (TradingPostManager.IsAcceptUserAgreement())
		{
			DispatchEvent("CONTINUE");
		}
		base.StartSection();
	}

	private void OnQuery_CONTINUE()
	{
		if (!TradingPostManager.IsAcceptUserAgreement())
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<TradingPostManager>.I.SendRequestUserAgreement(delegate(bool success)
			{
				MonoBehaviourSingleton<TradingPostManager>.I.isCheckUserAgreementSuccess = success;
				if (!TradingPostManager.IsFulfillRequirement() && MonoBehaviourSingleton<TradingPostManager>.I.startSectionName != "HomeTop")
				{
					GameSection.ChangeStayEvent("LA");
				}
				GameSection.ResumeEvent(success);
				MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName());
				Close();
			});
		}
		else
		{
			MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName());
			Close();
		}
	}

	private void OnQuery_HELP()
	{
		GameSection.SetEventData(WebViewManager.TradingPost);
	}
}
