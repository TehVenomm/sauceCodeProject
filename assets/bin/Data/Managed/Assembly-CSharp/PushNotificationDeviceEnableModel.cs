public class PushNotificationDeviceEnableModel : BaseModel
{
	public class RequestSendForm
	{
		public int enable;
	}

	public static string URL = "ajax/pushnotification/device/enable";
}
