using System;
using System.Collections.Generic;

public class AlchemyExceedModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public bool greatSuccess;
	}

	public class RequestSendForm
	{
		public string suid;

		public List<string> uuids = new List<string>();
	}

	public static string URL = "ajax/alchemy/exceed";

	public Param result = new Param();
}
