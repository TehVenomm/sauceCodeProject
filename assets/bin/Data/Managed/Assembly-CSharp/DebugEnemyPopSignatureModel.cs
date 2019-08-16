using Network;
using System;

public class DebugEnemyPopSignatureModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public PopSignatureInfo psig = new PopSignatureInfo();
	}

	public class RequestSendForm
	{
		public string popId;
	}

	public static string URL = "ajax/debug/enemy-pop-signature";

	public Param result = new Param();
}
