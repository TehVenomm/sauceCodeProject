using Network;

public class RegistCreateModel : BaseModel
{
	public class Param
	{
		public string uh = string.Empty;

		public int userId;

		public string userIdHash;

		public UserInfo userInfo = new UserInfo();
	}

	public static string URL = "ajax/regist/create";

	public Param result = new Param();
}
