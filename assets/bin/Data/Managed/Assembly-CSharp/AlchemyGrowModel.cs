using System;
using System.Collections.Generic;

public class AlchemyGrowModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public bool greatSuccess;
	}

	public class RequestSendForm
	{
		[Serializable]
		public class MagiItem
		{
			public string uiuid;

			public int num;
		}

		public string suid;

		public List<string> uuids = new List<string>();

		public List<MagiItem> uiuids = new List<MagiItem>();
	}

	public static string URL = "ajax/alchemy/grow";

	public Param result = new Param();
}
