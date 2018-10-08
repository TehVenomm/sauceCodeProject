using Network;
using System;
using System.Collections.Generic;

public class SerialListModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public List<Serials> serials = new List<Serials>();
	}

	[Serializable]
	public class Serials
	{
		public int serialId;

		public string name;

		public EndDate endDate = new EndDate();
	}

	public static string URL = "ajax/serial/list";

	public Param result = new Param();
}
