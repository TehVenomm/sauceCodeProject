public class HomeAppReviewAppealDialogMaxStar : HomeAppReviewAppealDialogBase
{
	protected new enum UI
	{
		LBL_ITEM_TEXT,
		SPR_BTN_YES,
		LBL_BTN_YES,
		BTN_STAR1,
		BTN_STAR2,
		BTN_STAR3,
		BTN_STAR4,
		BTN_STAR5,
		OBJ_ON,
		OBJ_OFF
	}

	public override void Initialize()
	{
		base.Initialize();
		starValue = (int)GameSection.GetEventData();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		DisableStarButton();
	}

	protected override void OnQuery_YES()
	{
		SendInfo(3, delegate
		{
			Native.launchMyselfMarket();
		});
	}

	protected override void OnQuery_NO()
	{
		int replyAction = 4;
		SendInfo(replyAction, null);
	}
}
