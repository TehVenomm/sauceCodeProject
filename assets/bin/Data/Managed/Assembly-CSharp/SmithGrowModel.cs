public class SmithGrowModel : BaseModel
{
	public class RequestSendForm
	{
		public string euid;

		public int lv;
	}

	public class Param
	{
		public int maxGrowCount;
	}

	public static string URL = "ajax/smith/grow";

	public Param result = new Param();
}
