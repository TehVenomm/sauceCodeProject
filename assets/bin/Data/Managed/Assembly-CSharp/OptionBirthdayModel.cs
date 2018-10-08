public class OptionBirthdayModel : BaseModel
{
	public class RequestSendForm
	{
		public int year;

		public int month;

		public int day;
	}

	public static string URL = "ajax/option/birthday";
}
