using System;
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
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		string text = MonoBehaviourSingleton<PartyManager>.I.challengeInfo.oldShadowCount.startDate + " 〜\n" + MonoBehaviourSingleton<PartyManager>.I.challengeInfo.oldShadowCount.endDate;
		SetLabelText((Enum)UI.LBL_DATE, text);
		SetLabelText((Enum)UI.LBL_DESCRIPTION, StringTable.Get(STRING_CATEGORY.SHADOW_COUNT, 4u));
		base.GetComponent<UITable>((Enum)UI.TBL_CONTENTS).Reposition();
		Transform ctrl = GetCtrl(UI.SPR_FRAME);
		int num = 0;
		int childCount = GetCtrl(UI.TBL_CONTENTS).get_childCount();
		for (int i = 0; i < childCount; i++)
		{
			Transform val = GetCtrl(UI.TBL_CONTENTS).GetChild(i);
			if (val.get_gameObject().get_activeSelf())
			{
				num += val.GetComponent<UIWidget>().height;
			}
		}
		num = Mathf.Max(num, 0);
		float num2 = (float)(123 + num);
		Vector3 localScale = ctrl.get_localScale();
		int height = (int)(num2 / localScale.y);
		SetHeight((Enum)UI.SPR_FRAME, height);
		UpdateAnchors();
		base.UpdateUI();
	}
}
