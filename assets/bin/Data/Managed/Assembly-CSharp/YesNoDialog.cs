public class YesNoDialog : CommonDialog
{
	protected override void InitDialog(object data_object)
	{
		string text = data_object as string;
		if (text == null)
		{
			string[] texts = GetTexts(data_object as object[]);
			base.InitDialog(new Desc(TYPE.YES_NO, (texts.Length <= 0) ? string.Empty : texts[0], (texts.Length <= 1) ? string.Empty : texts[1], (texts.Length <= 2) ? string.Empty : texts[2]));
		}
		else
		{
			base.InitDialog(new Desc(TYPE.YES_NO, text));
		}
	}
}
