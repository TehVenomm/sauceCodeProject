using Network;
using System.Collections.Generic;

public class OnceTraveledListModel : BaseModel
{
	public class Param
	{
		public List<int> travel = new List<int>();

		public List<FieldPortal> portal = new List<FieldPortal>();
	}

	public static string URL = "ajax/once/traveledlist";

	public Param result = new Param();
}
