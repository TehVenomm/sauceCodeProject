public class RegionOpenModel : BaseModel
{
	public class RequestSendForm
	{
		public int crystalCL;

		public int regionId;

		public int useCrystal;
	}

	public static string URL = "ajax/region/open";
}
