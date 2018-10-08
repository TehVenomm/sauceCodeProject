public class YesNoDialogImportant : YesNoDialog
{
	protected override SoundID.UISE openingSound => SoundID.UISE.DIALOG_IMPTNT;

	protected override string GetTransferUIName()
	{
		return "UI_CommonDialogImportant";
	}
}
