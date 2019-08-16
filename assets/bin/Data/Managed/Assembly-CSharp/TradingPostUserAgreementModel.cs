using System;

public class TradingPostUserAgreementModel : BaseModel
{
	[Serializable]
	public class Result
	{
		public int tradingDay;

		public int tradingStatus;

		public int tradingAccept;
	}

	public static string URL = "/ajax/trading-post/user-agreement";

	public Result result = new Result();
}
