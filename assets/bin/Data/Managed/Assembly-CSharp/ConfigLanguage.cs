using System;

public class ConfigLanguage : GameSection
{
	private enum UI
	{
		TGL_EN,
		TGL_FR,
		TGL_GE,
		TGL_IT,
		TGL_PO,
		TGL_TH,
		TGL_VN,
		TGL_ES
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void Exit()
	{
		GameSaveData.Save();
		base.Exit();
	}

	public override void UpdateUI()
	{
		SetToggle((Enum)UI.TGL_EN, GameSaveData.instance.languageOption == 0);
		SetToggle((Enum)UI.TGL_FR, GameSaveData.instance.languageOption == 1);
		SetToggle((Enum)UI.TGL_GE, GameSaveData.instance.languageOption == 2);
		SetToggle((Enum)UI.TGL_IT, GameSaveData.instance.languageOption == 3);
		SetToggle((Enum)UI.TGL_PO, GameSaveData.instance.languageOption == 4);
		SetToggle((Enum)UI.TGL_TH, GameSaveData.instance.languageOption == 5);
		SetToggle((Enum)UI.TGL_VN, GameSaveData.instance.languageOption == 6);
		SetToggle((Enum)UI.TGL_ES, GameSaveData.instance.languageOption == 7);
	}

	private void OnQuery_LANG_EN()
	{
		if (GameSaveData.instance.languageOption != 0)
		{
			GameSaveData.instance.languageOption = 0;
			ClearCacheAndReturnToTitle();
		}
	}

	private void OnQuery_LANG_FR()
	{
		if (GameSaveData.instance.languageOption != 1)
		{
			GameSaveData.instance.languageOption = 1;
			ClearCacheAndReturnToTitle();
		}
	}

	private void OnQuery_LANG_GE()
	{
		if (GameSaveData.instance.languageOption != 2)
		{
			GameSaveData.instance.languageOption = 2;
			ClearCacheAndReturnToTitle();
		}
	}

	private void OnQuery_LANG_IT()
	{
		if (GameSaveData.instance.languageOption != 3)
		{
			GameSaveData.instance.languageOption = 3;
			ClearCacheAndReturnToTitle();
		}
	}

	private void OnQuery_LANG_PO()
	{
		if (GameSaveData.instance.languageOption != 4)
		{
			GameSaveData.instance.languageOption = 4;
			ClearCacheAndReturnToTitle();
		}
	}

	private void OnQuery_LANG_TH()
	{
		if (GameSaveData.instance.languageOption != 5)
		{
			GameSaveData.instance.languageOption = 5;
			ClearCacheAndReturnToTitle();
		}
	}

	private void OnQuery_LANG_VN()
	{
		if (GameSaveData.instance.languageOption != 6)
		{
			GameSaveData.instance.languageOption = 6;
			ClearCacheAndReturnToTitle();
		}
	}

	private void OnQuery_LANG_ES()
	{
		if (GameSaveData.instance.languageOption != 7)
		{
			GameSaveData.instance.languageOption = 7;
			ClearCacheAndReturnToTitle();
		}
	}

	private void ClearCacheAndReturnToTitle()
	{
		UpdateUI();
		MonoBehaviourSingleton<AppMain>.I.Reset(false, false);
	}
}
