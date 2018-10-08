public class DebugInfoModel : BaseModel
{
	public class Result
	{
		public string info;

		public string warning;

		public string error;
	}

	public static string URL = "ajax/debug/info";

	public Result result = new Result();
}
