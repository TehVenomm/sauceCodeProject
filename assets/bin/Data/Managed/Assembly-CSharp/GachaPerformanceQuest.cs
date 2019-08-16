using System;

public class GachaPerformanceQuest : GachaPerformanceBase, QuestGachaDirectorBase.ISectionCommand
{
	private new enum UI
	{
		BTN_SKIP,
		OBJ_RARITY_ROOT,
		OBJ_RARITY_D,
		OBJ_RARITY_C,
		OBJ_RARITY_B,
		OBJ_RARITY_A,
		OBJ_RARITY_S,
		OBJ_RARITY_SS,
		OBJ_RARITY_SSS,
		OBJ_RARITY_LIGHT,
		OBJ_RARITY_TEXT_ROOT,
		BTN_SKIPALL
	}

	private bool m_isReam;

	protected override void OnOpen()
	{
		SetActive((Enum)UI.BTN_SKIP, is_visible: true);
		m_isReam = (AnimationDirector.I is QuestReamGachaDirector || AnimationDirector.I is QuestFeverGachaLegacyDirector);
		SetActive((Enum)UI.BTN_SKIPALL, m_isReam);
		if (AnimationDirector.I != null)
		{
			(AnimationDirector.I as QuestGachaDirectorBase).StartDirection(this);
		}
	}

	void QuestGachaDirectorBase.ISectionCommand.OnShowRarity(RARITY_TYPE rarity)
	{
		SetActive((Enum)UI.BTN_SKIPALL, is_visible: false);
		ShowRarity(rarity);
	}

	void QuestGachaDirectorBase.ISectionCommand.OnHideRarity()
	{
		SetActive((Enum)UI.BTN_SKIPALL, m_isReam ? true : false);
		HideRarity();
	}

	void QuestGachaDirectorBase.ISectionCommand.OnEnd()
	{
		SetActive((Enum)UI.BTN_SKIPALL, is_visible: false);
		End();
	}

	void QuestGachaDirectorBase.ISectionCommand.ActivateSkipButton()
	{
		ActivateButtonSkip();
	}
}
