using System.Collections.Generic;

public class SmithEvolveModel : BaseModel
{
	public class RequestSendForm
	{
		public int vid;

		public string euid;

		public List<string> meids;
	}

	public class Param
	{
		public int evolveCount;
	}

	public static string URL = "ajax/smith/evolve";

	public Param result = new Param();
}
