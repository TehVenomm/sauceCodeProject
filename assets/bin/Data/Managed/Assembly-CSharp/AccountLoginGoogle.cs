public class AccountLoginGoogle : AccountLoginBase
{
	public override void Initialize()
	{
		isGoogleAccount = true;
		base.Initialize();
	}
}
