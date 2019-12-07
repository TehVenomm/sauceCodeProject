using UnityEngine;

public class QuestAcceptShadowCountDetail : GameSection
{
	private enum UI
	{
		SPR_FRAME,
		TBL_CONTENTS,
		LBL_BONUS_NUM,
		LBL_FIRST_SENTENSE,
		PADDING_1,
		LBL_SECOND_SENTENSE,
		PADDING_2,
		PADDING_3,
		LBL_DESCRIPTION
	}

	private const int ADD_MSG_HEIGHT = 123;

	public override void UpdateUI()
	{
		string text = StringTable.Format(STRING_CATEGORY.SHADOW_COUNT, 0u, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.currentShadowCount.startDate);
		SetLabelText(UI.LBL_FIRST_SENTENSE, text);
		if (MonoBehaviourSingleton<PartyManager>.I.challengeInfo.IsRankingEvent())
		{
			SetActive(UI.LBL_SECOND_SENTENSE, is_visible: true);
			SetActive(UI.PADDING_2, is_visible: true);
			SetLabelText(UI.LBL_SECOND_SENTENSE, StringTable.Get(STRING_CATEGORY.SHADOW_COUNT, 1u));
		}
		else
		{
			SetActive(UI.LBL_SECOND_SENTENSE, is_visible: false);
			SetActive(UI.PADDING_2, is_visible: false);
		}
		SetLabelText(UI.LBL_DESCRIPTION, StringTable.Get(STRING_CATEGORY.SHADOW_COUNT, 2u));
		SetLabelText(UI.LBL_BONUS_NUM, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.currentShadowCount.num.ToString());
		GetComponent<UITable>(UI.TBL_CONTENTS).Reposition();
		Transform ctrl = GetCtrl(UI.SPR_FRAME);
		int num = 0;
		int childCount = GetCtrl(UI.TBL_CONTENTS).childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = GetCtrl(UI.TBL_CONTENTS).GetChild(i);
			if (child.gameObject.activeSelf)
			{
				num += child.GetComponent<UIWidget>().height;
			}
		}
		num = Mathf.Max(num, 0);
		int height = (int)((float)(123 + num) / ctrl.localScale.y);
		SetHeight(UI.SPR_FRAME, height);
		UpdateAnchors();
		base.UpdateUI();
	}
}
