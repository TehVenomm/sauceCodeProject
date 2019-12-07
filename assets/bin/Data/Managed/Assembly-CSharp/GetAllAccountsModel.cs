using Network;
using System.Collections.Generic;

public class GetAllAccountsModel : BaseModel
{
	public class RequestSendForm
	{
		public string env;
	}

	public class ServerAccountInfo
	{
		public string email;

		public string fbId;

		public string uid;

		public int level;

		public int crystal;

		public int money;

		public UserInfo userAccount;
	}

	public static string URL = "ajax/regist/getallaccounts";

	public List<ServerAccountInfo> result = new List<ServerAccountInfo>();
}
