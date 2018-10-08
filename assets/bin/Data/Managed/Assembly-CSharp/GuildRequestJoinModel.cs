public class GuildRequestJoinModel : BaseModel
{
	public class Result : GuildInfoModel.GuildInfo
	{
		public int requesterId;

		public int status;

		public int clanId;
	}

	public static string URL = "clan/AtvRequestJoin.go";

	public Result result = new Result();
}
