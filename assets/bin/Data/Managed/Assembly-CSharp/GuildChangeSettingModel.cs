public class GuildChangeSettingModel : BaseModel
{
	public class Result
	{
		public int level;

		public string description;

		public int currentMem;

		public int memberNum;

		public int privacy;

		public string createAt;

		public string clanName;

		public int location;

		public string tag;

		public int[] emblem;

		public int memCap;

		public int minLevel;

		public int exp;
	}

	public static string URL = "clan/ClanSettings.go";

	public Result result = new Result();
}
