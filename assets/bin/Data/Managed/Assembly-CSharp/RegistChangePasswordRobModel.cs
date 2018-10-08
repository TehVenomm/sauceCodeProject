public class RegistChangePasswordRobModel : BaseModel
{
	public class RequestSendForm
	{
		public string currentPassword;

		public string newPassword;

		public string newPasswordConfirm;
	}

	public static string URL = "ajax/regist/changepasswordrob";
}
