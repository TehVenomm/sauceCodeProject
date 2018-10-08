using Network;

public class FriendPromotionModel : BaseModel
{
	public class RequestSendForm
	{
		public string friendCode;
	}

	public static string URL = "ajax/friend/promotion";

	public FriendPromotionResult result = new FriendPromotionResult();
}
