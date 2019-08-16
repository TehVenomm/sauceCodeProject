using System;
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
		SetLabelText((Enum)UI.LBL_ITEM_TEXT, itemString);
		if (starValue == 0)
		{
			MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce("Received: " + itemString, string.Empty);
			SetEnableYesButton(isEnable: false);
			yesButton.UpdateColor(instant: true);
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
		SendInfo(replyAction);
	}

	private void OnQuery_STAR()
	{
		starValue = (int)GameSection.GetEventData() + 1;
		SetEnableYesButton(isEnable: true);
		UpdateStarUI();
		GameSection.StopEvent();
	}

	private void SetEnableYesButton(bool isEnable)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		yesButton.isEnabled = isEnable;
		Color color = (!isEnable) ? Color.get_gray() : Color.get_white();
		GetCtrl(UI.LBL_BTN_YES).GetComponent<UILabel>().color = color;
		yesButton.GetComponent<UISprite>().color = color;
	}
}
