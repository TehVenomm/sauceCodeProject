public class MessageDialog : CommonDialog
{
	protected override void InitDialog(object data_object)
	{
		InitDialog(data_object, STRING_CATEGORY.COMMON_DIALOG);
	}

	protected void InitDialog(object data_object, STRING_CATEGORY message_category)
	{
		string text = data_object as string;
		if (text == null)
		{
			string[] texts = GetTexts(data_object as object[], message_category);
			base.InitDialog(new Desc(TYPE.OK, (texts.Length <= 0) ? string.Empty : texts[0], (texts.Length <= 1) ? string.Empty : texts[1], null, null, null));
		}
		else
		{
			base.InitDialog(new Desc(TYPE.OK, text, null, null, null, null));
		}
	}
}
