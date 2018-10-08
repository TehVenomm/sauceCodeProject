using System.Collections.Generic;

public class SmithEvolveModel : BaseModel
{
	public class RequestSendForm
	{
		public int vid;

		public string euid;

		public List<string> meids;
	}

	public static string URL = "ajax/smith/evolve";
}
