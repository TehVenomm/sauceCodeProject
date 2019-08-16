using Network;

public class ClanEditMemberModel : BaseModel
{
	public class RequestSendForm
	{
		public int uId;

		public int stat;
	}

	public static string URL = "ajax/clan/edit-member";

	public UserClanData result = new UserClanData();
}
