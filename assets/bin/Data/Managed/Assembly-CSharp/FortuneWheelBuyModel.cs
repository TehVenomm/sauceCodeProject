using Network;

public class FortuneWheelBuyModel : BaseModel
{
	public class FortuneWheelBuyForm
	{
		public int crystalCL;

		public int number;
	}

	public static string URL = "ajax/dragon-vault/buy";

	public FortuneWheelBuyData result = new FortuneWheelBuyData();
}
