public class DebugAddSkillItemModel : BaseModel
{
	public class RequestSendForm
	{
		public int skillItemId;

		public int level;

		public int exceed;
	}

	public static string URL = "ajax/debug/addskillitem";
}
