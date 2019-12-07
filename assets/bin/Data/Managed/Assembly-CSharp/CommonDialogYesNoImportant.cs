public class CommonDialogYesNoImportant : CommonDialog
{
	protected override SoundID.UISE openingSound => SoundID.UISE.DIALOG_IMPTNT;

	protected override string GetTransferUIName()
	{
		return "UI_CommonDialogYesNoImportant";
	}

	protected override void InitDialog(object data_object)
	{
		string text = data_object as string;
		if (text == null)
		{
			string[] texts = GetTexts(data_object as object[]);
			base.InitDialog(new Desc(TYPE.DECLINE_COMFIRM, (texts.Length != 0) ? texts[0] : string.Empty, (texts.Length > 1) ? texts[1] : string.Empty, (texts.Length > 2) ? texts[2] : string.Empty));
		}
		else
		{
			base.InitDialog(new Desc(TYPE.DECLINE_COMFIRM, text));
		}
	}
}
