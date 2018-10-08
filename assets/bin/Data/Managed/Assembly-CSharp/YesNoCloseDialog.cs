using System;
using UnityEngine;

public class YesNoCloseDialog : CommonDialog
{
	private const int MIN_MSG_HEIGHT = 100;

	protected override string GetTransferUIName()
	{
		return "UI_CommonCloseDialog";
	}

	protected override void InitDialog(object data_object)
	{
		InitUI();
		string[] msgs = data_object as string[];
		SetupLabelText(msgs);
		AutoUILayout();
		SoundManager.PlaySystemSE(openingSound, 1f);
	}

	protected void SetupLabelText(string[] _msgs)
	{
		string[] array = _msgs;
		if (array == null || array.Length < 1)
		{
			array = GetTexts(_msgs, STRING_CATEGORY.COMMON_DIALOG);
		}
		int num = array.Length;
		string text = (num <= 0) ? string.Empty : array[0];
		SetLabelText((Enum)UI.LBL_TITLE_U, text);
		SetLabelText((Enum)UI.LBL_TITLE_D, text);
		SetLabelText((Enum)UI.MESSAGE, (num <= 1) ? string.Empty : array[1]);
		if (num > 2)
		{
			SetLabelText((Enum)UI.LBL_BTN_0, array[2]);
			SetLabelText((Enum)UI.LBL_BTN_0_R, array[2]);
		}
		if (num > 3)
		{
			SetLabelText((Enum)UI.LBL_BTN_1, _msgs[3]);
			SetLabelText((Enum)UI.LBL_BTN_1_R, _msgs[3]);
		}
	}

	protected void AutoUILayout()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(UI.BG);
		int height = GetHeight(UI.MESSAGE);
		if (height >= 100)
		{
			int num = height - 100;
			int height2 = GetHeight(UI.BG);
			float num2 = (float)num;
			Vector3 localScale = ctrl.get_localScale();
			int height3 = height2 + (int)(num2 / localScale.y);
			SetHeight((Enum)UI.BG, height3);
			ctrl = GetCtrl(UI.HEADER);
			Vector3 position = ctrl.get_position();
			position.y += (float)(num / 2);
			ctrl.set_position(position);
			ctrl = GetCtrl(UI.FOOTER);
			position = ctrl.get_position();
			position.y -= (float)(num / 2);
			ctrl.set_position(position);
			UpdateAnchors();
		}
	}
}
