namespace Network
{
	public class ShadowCount
	{
		public string startDate = string.Empty;

		public string endDate = string.Empty;

		public int num;

		public bool HasShadowCount()
		{
			return startDate != string.Empty;
		}
	}
}
