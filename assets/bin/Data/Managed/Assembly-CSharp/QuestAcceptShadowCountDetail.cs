using System;
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
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		string text = StringTable.Format(STRING_CATEGORY.SHADOW_COUNT, 0u, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.currentShadowCount.startDate);
		SetLabelText((Enum)UI.LBL_FIRST_SENTENSE, text);
		if (MonoBehaviourSingleton<PartyManager>.I.challengeInfo.IsRankingEvent())
		{
			SetActive((Enum)UI.LBL_SECOND_SENTENSE, true);
			SetActive((Enum)UI.PADDING_2, true);
			SetLabelText((Enum)UI.LBL_SECOND_SENTENSE, StringTable.Get(STRING_CATEGORY.SHADOW_COUNT, 1u));
		}
		else
		{
			SetActive((Enum)UI.LBL_SECOND_SENTENSE, false);
			SetActive((Enum)UI.PADDING_2, false);
		}
		SetLabelText((Enum)UI.LBL_DESCRIPTION, StringTable.Get(STRING_CATEGORY.SHADOW_COUNT, 2u));
		SetLabelText((Enum)UI.LBL_BONUS_NUM, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.currentShadowCount.num.ToString());
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
