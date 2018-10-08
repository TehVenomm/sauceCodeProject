public class AccountRegistrationGoogle : AccountRegistrationBase
{
	private enum UI
	{
		OBJ_SECRET_QUESTION,
		POP_SECRET_QUESTION,
		LBL_SECRET_QUESTION,
		IPT_ADDRESS,
		POP_ADDRESS,
		IPT_PASSWORD,
		IPT_CONFIRM_PASSWORD,
		IPT_SECRET_ANSER,
		LBL_ADDRESS,
		LBL_PASSWORD,
		LBL_CONFIRM_PASSWORD,
		LBL_SECRET_ANSER,
		BTN_OK,
		BTN_INVALID
	}

	public override void Initialize()
	{
		isGoogleAccount = true;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
	}
}
