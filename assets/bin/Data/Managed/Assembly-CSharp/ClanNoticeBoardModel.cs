using Network;

public class ClanNoticeBoardModel : BaseModel
{
	public class Param
	{
		public ClanNoticeBoardData clanNoticeBoard;
	}

	public static string URL = "ajax/clan/notice-board";

	public Param result = new Param();
}
