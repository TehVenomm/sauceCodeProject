using System;

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

	protected unsafe override void OnQuery_YES()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		if (_003C_003Ef__am_0024cache0 == null)
		{
			_003C_003Ef__am_0024cache0 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		SendInfo(3, _003C_003Ef__am_0024cache0);
	}

	protected override void OnQuery_NO()
	{
		int replyAction = 4;
		SendInfo(replyAction, null);
	}
}
