public class AccountLoginMail : AccountLoginBase
{
	public override string overrideBackKeyEvent => "[BACK]";

	public override void Initialize()
	{
		isGoogleAccount = false;
		base.Initialize();
	}
}
