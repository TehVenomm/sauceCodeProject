using UnityEngine;

public class QuestAcceptShadowCountDetailGP : GameSection
{
	private enum UI
	{
		SPR_FRAME,
		TBL_CONTENTS,
		LBL_DATE,
		LBL_DESCRIPTION
	}

	private const int ADD_MSG_HEIGHT = 123;

	public override void UpdateUI()
	{
		string text = MonoBehaviourSingleton<PartyManager>.I.challengeInfo.oldShadowCount.startDate + " 〜\n" + MonoBehaviourSingleton<PartyManager>.I.challengeInfo.oldShadowCount.endDate;
		SetLabelText(UI.LBL_DATE, text);
		SetLabelText(UI.LBL_DESCRIPTION, StringTable.Get(STRING_CATEGORY.SHADOW_COUNT, 4u));
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
		float num2 = (float)(123 + num);
		Vector3 localScale = ctrl.localScale;
		int height = (int)(num2 / localScale.y);
		SetHeight(UI.SPR_FRAME, height);
		UpdateAnchors();
		base.UpdateUI();
	}
}
