using Network;
using System.Collections.Generic;

public class LoginBonusConfirmModel : BaseModel
{
	public class RequestSendForm
	{
		public int loginBonusId;
	}

	public static string URL = "ajax/loginbonus/confirm/";

	public List<LoginBonus> result = new List<LoginBonus>();
}
