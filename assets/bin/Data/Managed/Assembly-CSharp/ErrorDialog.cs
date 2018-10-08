public class ErrorDialog : MessageDialog
{
	protected override void InitDialog(object data_object)
	{
		InitDialog(data_object, STRING_CATEGORY.ERROR_DIALOG);
		base.baseDepth = base.baseDepth - 5000 + 9500;
	}
}
