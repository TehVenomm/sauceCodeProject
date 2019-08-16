using Network;
using System.Collections.Generic;

public class GachaGachaModel : BaseModel
{
	public class RequestSendForm
	{
		public int id;

		public int crystalCL;

		public int ticketCL;

		public string productId;

		public int guaranteeCampaignType;

		public int guaranteeCampaignId;

		public int guaranteeRemainCount;

		public int guaranteeUserCount;

		public int useStepUpTicket;

		public int seriesId;
	}

	public static string URL = "ajax/gacha/gacha";

	public GachaResult result = new GachaResult();

	public List<GachaResult> resultArray = new List<GachaResult>();

	public GachaResult resultBonus = new GachaResult();
}
