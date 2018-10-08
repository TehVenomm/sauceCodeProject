using System.Collections.Generic;

public class SmithGetAbilityListForCreateModel : BaseModel
{
	public class RequestSendForm
	{
		public string cid;
	}

	public class Param
	{
		public int aid;

		public int minap;

		public int maxap;
	}

	public static string URL = "ajax/smith/getabilitylistforcreate";

	public List<Param> result = new List<Param>();
}
