using System;

public class StorageExpandConfirmDialog : CommonDialog
{
	protected override string GetTransferUIName()
	{
		return "UI_StorageExpandConfirmDialog";
	}

	protected override void SetupThreeButton(Desc data)
	{
		SetLabelText((Enum)UI.LBL_BTN_0, data.btnText[0]);
		SetLabelText((Enum)UI.LBL_BTN_0_R, data.btnText[0]);
		SetEventName((Enum)UI.SPR_BTN_0, "YES");
		SetButtonSprite((Enum)UI.SPR_BTN_0, CommonDialog.BTN_SPRITE_NAME[2], true);
		SetLabelText((Enum)UI.LBL_BTN_1, data.btnText[1]);
		SetLabelText((Enum)UI.LBL_BTN_1_R, data.btnText[1]);
		SetEventName((Enum)UI.SPR_BTN_1, "GO_ITEM_STORAGE");
	}
}
