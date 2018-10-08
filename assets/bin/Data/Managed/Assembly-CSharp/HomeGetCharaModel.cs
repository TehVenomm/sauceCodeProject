using Network;
using System.Collections.Generic;

public class HomeGetCharaModel : BaseModel
{
	public class RequestSendForm
	{
		public List<int> ids = new List<int>();
	}

	public static string URL = "ajax/home/getchara";

	public List<FriendCharaInfo> result = new List<FriendCharaInfo>();
}
