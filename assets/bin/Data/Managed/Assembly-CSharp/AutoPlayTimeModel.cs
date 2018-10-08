public class AutoPlayTimeModel : BaseModel
{
	public class Param
	{
		public double timeLeft;
	}

	public static string URL = "ajax/inventory/getautopotion";

	public Param result = new Param();
}
