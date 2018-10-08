using System;

public class TradingPostCheckLicense : GameSection
{
	private enum UI
	{
		MESSAGE,
		OBJ_FRAME,
		LBL_BTN_0,
		LBL_BTN_0_R
	}

	public override void Initialize()
	{
		SetSupportEncoding(UI.MESSAGE, true);
		SetLabelText((Enum)UI.MESSAGE, base.sectionData.GetText("STR_MESSAGE"));
		SetLabelText((Enum)UI.LBL_BTN_0, base.sectionData.GetText("TEXT_BTN_TO_TP"));
		SetLabelText((Enum)UI.LBL_BTN_0_R, base.sectionData.GetText("TEXT_BTN_TO_TP"));
		base.Initialize();
	}

	public override void StartSection()
	{
		base.StartSection();
	}

	private void OnQuery_CONTINUE()
	{
		MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName());
		Close(UITransition.TYPE.CLOSE);
	}
}
