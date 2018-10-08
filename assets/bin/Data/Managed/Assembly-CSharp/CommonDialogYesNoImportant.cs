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
			string[] texts = GetTexts(data_object as object[], STRING_CATEGORY.COMMON_DIALOG);
			base.InitDialog(new Desc(TYPE.DECLINE_COMFIRM, (texts.Length <= 0) ? string.Empty : texts[0], (texts.Length <= 1) ? string.Empty : texts[1], (texts.Length <= 2) ? string.Empty : texts[2], null, null));
		}
		else
		{
			base.InitDialog(new Desc(TYPE.DECLINE_COMFIRM, text, null, null, null, null));
		}
	}
}
