namespace Network
{
	public class ShadowCount
	{
		public string startDate = "";

		public string endDate = "";

		public int num;

		public bool HasShadowCount()
		{
			return startDate != "";
		}
	}
}
