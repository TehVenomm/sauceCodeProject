public class ClanSymbolEditRequestModel : BaseModel
{
	public class RequestSendForm
	{
		public int mark;

		public int markOption;

		public int frame;

		public int frameOption;

		public int pattern;

		public int patternOption;
	}

	public static string URL = "ajax/clan/edit-symbol";
}
