using System;

public class SerialInputModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public string message;
	}

	public class RequestSendForm
	{
		public int id;

		public string code;
	}

	public static string URL = "ajax/serial/input";

	public Param result = new Param();
}
