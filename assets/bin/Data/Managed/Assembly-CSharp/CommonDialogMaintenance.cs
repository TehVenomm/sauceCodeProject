using System;

public class CommonDialogMaintenance : CommonDialog
{
	protected override string GetTransferUIName()
	{
		return "UI_CommonDialogMaintenance";
	}

	protected override void InitDialog(object data_object)
	{
		base.InitDialog(data_object);
		Desc desc = data_object as Desc;
		if (desc.data != null)
		{
			try
			{
				double unixTimeStamp = (double)long.Parse(desc.data.ToString());
				DateTime dateTime = UnixTimeStampToDateTime(unixTimeStamp);
				SetLabelText((Enum)UI.MESSAGE, string.Format(desc.text, GetFormartedText(dateTime.Day) + "/" + GetFormartedText(dateTime.Month) + ", " + GetFormartedText(dateTime.Hour) + ":" + GetFormartedText(dateTime.Minute)));
			}
			catch
			{
				SetLabelText((Enum)UI.MESSAGE, desc.text);
			}
		}
	}

	public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
	{
		DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		result = result.AddSeconds(unixTimeStamp);
		return result;
	}

	private string GetFormartedText(int num)
	{
		return string.Format((num <= 9) ? ("0{" + 0 + "}") : ("{" + 0 + "}"), num);
	}
}
