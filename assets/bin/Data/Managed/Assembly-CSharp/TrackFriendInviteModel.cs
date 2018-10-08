using System.Collections.Generic;

public class TrackFriendInviteModel : BaseModel
{
	public class SendForm
	{
		public List<string> listFriend;
	}

	public static string URL = "ajax/go-facebook/invitefriend";
}
