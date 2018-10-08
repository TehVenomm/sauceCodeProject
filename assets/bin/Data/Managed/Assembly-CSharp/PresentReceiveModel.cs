using Network;
using System;
using System.Collections.Generic;

public class PresentReceiveModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public int receivePresentNum;

		public PresentList list;
	}

	public class RequestSendForm
	{
		public List<string> uids = new List<string>();

		public int page;
	}

	public static string URL = "ajax/present/receive";

	public Param result = new Param();
}
