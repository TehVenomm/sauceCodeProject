public class RegistLinkRobUseCloudDataModel : BaseModel
{
	public class Param : RegistCreateModel.Param
	{
		public int newsId = 1;
	}

	public class RequestSendForm
	{
		public string email;

		public string d;
	}

	public static string URL = "ajax/regist/overridelinkedrobbycloud";

	public Param result = new Param();
}
