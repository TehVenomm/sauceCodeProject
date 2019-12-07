public class StorageExpandConfirmDialog : CommonDialog
{
	protected override string GetTransferUIName()
	{
		return "UI_StorageExpandConfirmDialog";
	}

	protected override void SetupThreeButton(Desc data)
	{
		SetLabelText(UI.LBL_BTN_0, data.btnText[0]);
		SetLabelText(UI.LBL_BTN_0_R, data.btnText[0]);
		SetEventName(UI.SPR_BTN_0, "YES");
		SetButtonSprite(UI.SPR_BTN_0, CommonDialog.BTN_SPRITE_NAME[2], with_press: true);
		SetLabelText(UI.LBL_BTN_1, data.btnText[1]);
		SetLabelText(UI.LBL_BTN_1_R, data.btnText[1]);
		SetEventName(UI.SPR_BTN_1, "GO_ITEM_STORAGE");
	}
}
