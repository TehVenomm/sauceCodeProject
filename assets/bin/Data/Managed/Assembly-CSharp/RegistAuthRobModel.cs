public class RegistAuthRobModel : BaseModel
{
	public class Param : RegistCreateModel.Param
	{
		public int newsId = 1;
	}

	public class RequestSendForm
	{
		public string email;

		public string password;

		public string d;
	}

	public static string URL = "ajax/regist/authrob";

	public Param result = new Param();
}
