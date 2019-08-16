using Network;
using System;
using System.Collections.Generic;

public class TheaterModeStatusModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public List<TheaterModeGetData> theaterList = new List<TheaterModeGetData>();
	}

	public class RequestSendForm
	{
		public List<TheaterModeStatusData> dataList = new List<TheaterModeStatusData>();
	}

	public class TheaterModeStatusData
	{
		public int theaterId;

		public int statusId;
	}

	public static string URL = "ajax/thater/list";

	public Param result = new Param();
}
