public class AutoPlayTimestampModel : BaseModel
{
	public class Param
	{
		public double timeLeft;
	}

	public class RequestSendForm
	{
		public int type;
	}

	public static string URL = "ajax/inventory/activeautopotion";

	public Param result = new Param();
}
