using Network;
using System;
using System.Collections.Generic;

public class TheaterModeModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public List<TheaterModeGetData> theaterList = new List<TheaterModeGetData>();
	}

	public class RequestSendForm
	{
		public List<TheaterModePostData> theaterList = new List<TheaterModePostData>();
	}

	public static string URL = "ajax/theater/list";

	public Param result = new Param();
}
