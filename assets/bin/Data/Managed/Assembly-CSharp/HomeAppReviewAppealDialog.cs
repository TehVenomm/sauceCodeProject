using UnityEngine;

public class HomeAppReviewAppealDialog : HomeAppReviewAppealDialogBase
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

	private UIButton yesButton;

	private string itemString;

	public override void Initialize()
	{
		base.Initialize();
		yesButton = GetCtrl(UI.SPR_BTN_YES).GetComponent<UIButton>();
		SetStarsEvent();
		itemString = StringTable.Get(STRING_CATEGORY.APP_REVIEW, 3u);
	}

	public override void UpdateUI()
	{
		SetLabelText(UI.LBL_ITEM_TEXT, itemString);
		if (starValue == 0)
		{
			MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce("Received: " + itemString, string.Empty);
			SetEnableYesButton(false);
			yesButton.UpdateColor(true);
		}
		base.UpdateUI();
	}

	protected override void OnQuery_YES()
	{
		if (starValue >= 5)
		{
			GameSection.ChangeEvent("OPEN_MAX", starValue);
		}
		else if (starValue >= 1)
		{
			GameSection.ChangeEvent("OPEN_SOME", starValue);
		}
	}

	protected override void OnQuery_NO()
	{
		int replyAction = 0;
		SendInfo(replyAction, null);
	}

	private void OnQuery_STAR()
	{
		starValue = (int)GameSection.GetEventData() + 1;
		SetEnableYesButton(true);
		UpdateStarUI();
		GameSection.StopEvent();
	}

	private void SetEnableYesButton(bool isEnable)
	{
		yesButton.isEnabled = isEnable;
		Color color = (!isEnable) ? Color.gray : Color.white;
		GetCtrl(UI.LBL_BTN_YES).GetComponent<UILabel>().color = color;
		yesButton.GetComponent<UISprite>().color = color;
	}
}
