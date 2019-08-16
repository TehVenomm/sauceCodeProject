using Network;

public class ClanUserDetailModel : BaseModel
{
	public class RequestSendForm
	{
		public int uId;
	}

	public static string URL = "ajax/clan/user-detail";

	public UserClanData result = new UserClanData();
}
