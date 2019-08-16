using Network;

public class ClanNoticeBoardUpdateModel : BaseModel
{
	public class Param
	{
		public ClanNoticeBoardData clanNoticeBoard;
	}

	public class RequestSendForm
	{
		public string body;
	}

	public static string URL = "ajax/clan/notice-board-update";

	public Param result = new Param();
}
