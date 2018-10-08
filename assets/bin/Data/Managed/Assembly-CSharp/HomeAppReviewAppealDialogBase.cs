using System;

public class HomeAppReviewAppealDialogBase : GameSection
{
	protected enum UI
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

	public enum ReplyAction
	{
		NO_NOSTAR,
		YES_SUMSTAR,
		NO_SUMSTAR,
		YES_MAXSTAR,
		NO_MAXSTAR
	}

	public struct Info
	{
		public int starValue;

		public int replyAction;

		public Info(int starValue, int replyAction)
		{
			this.starValue = starValue;
			this.replyAction = replyAction;
		}
	}

	protected int starValue;

	private static readonly UI[] StarButtons = new UI[5]
	{
		UI.BTN_STAR1,
		UI.BTN_STAR2,
		UI.BTN_STAR3,
		UI.BTN_STAR4,
		UI.BTN_STAR5
	};

	private string itemString;

	public override void UpdateUI()
	{
		UpdateStarUI();
		base.UpdateUI();
	}

	protected void UpdateStarUI()
	{
		int num = StarButtons.Length;
		for (int i = 0; i < num; i++)
		{
			if (i < starValue)
			{
				SetStarVisualActive(i, true);
			}
			else
			{
				SetStarVisualActive(i, false);
			}
		}
	}

	protected void DisableStarButton()
	{
		int num = StarButtons.Length;
		for (int i = 0; i < num; i++)
		{
			UIButton component = GetCtrl(StarButtons[i]).GetComponent<UIButton>();
			component.isEnabled = false;
		}
	}

	protected void SetStarVisualActive(int index, bool active)
	{
		SetActive(GetCtrl(StarButtons[index]), UI.OBJ_ON, active);
		SetActive(GetCtrl(StarButtons[index]), UI.OBJ_OFF, !active);
	}

	protected void SendInfo(int replyAction, Action callback = null)
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<UserInfoManager>.I.SendAppReviewInfo(starValue, replyAction, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
			callback.SafeInvoke();
		});
	}

	protected virtual void OnQuery_YES()
	{
	}

	protected virtual void OnQuery_NO()
	{
	}

	protected void SetStarsEvent()
	{
		int num = StarButtons.Length;
		for (int i = 0; i < num; i++)
		{
			SetEvent(StarButtons[i], "STAR", i);
		}
	}
}
