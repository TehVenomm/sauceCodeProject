using System.Collections.Generic;

public class SmithGetAbilityList : BaseModel
{
	public class RequestSendForm
	{
		public string euid;
	}

	public class Param
	{
		public int aid;

		public int minap;

		public int maxap;
	}

	public static string URL = "ajax/smith/getabilitylist";

	public List<Param> result = new List<Param>();
}
