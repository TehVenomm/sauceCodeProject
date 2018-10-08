public class FriendInsertModel : BaseModel
{
	public class SendForm
	{
		public string fbAccessToken;
	}

	public static string URL = "ajax/gofacebook/insertfriend";
}
