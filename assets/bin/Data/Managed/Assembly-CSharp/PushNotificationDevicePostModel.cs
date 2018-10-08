public class PushNotificationDevicePostModel : BaseModel
{
	public class RequestSendForm
	{
		public string deviceToken;

		public string clientVer;
	}

	public static string URL = "ajax/pushnotification/device/post";
}
