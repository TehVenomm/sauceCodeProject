using System;

public class HomeOpinionBox : OpinionBox
{
	private bool isFromRieviewAppeal;

	private HomeAppReviewAppealDialogBase.Info infoDataOnStart;

	public override void Initialize()
	{
		object eventData = GameSection.GetEventData();
		isFromRieviewAppeal = (eventData != null && eventData is HomeAppReviewAppealDialogBase.Info);
		if (isFromRieviewAppeal)
		{
			infoDataOnStart = (HomeAppReviewAppealDialogBase.Info)eventData;
		}
		base.Initialize();
		SetEvent((Enum)UI.CLOSE, "CLOSE", 0);
	}

	protected override void OnQuery_SEND()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<UserInfoManager>.I.SendOpinionMessage(GetInputValue((Enum)UI.IPT_TEXT), delegate(bool is_success)
		{
			if (isFromRieviewAppeal && is_success)
			{
				SendInfo(infoDataOnStart);
			}
			else
			{
				GameSection.ResumeEvent(is_success, null, false);
			}
		});
	}

	protected void OnQuery_CLOSE()
	{
		if (isFromRieviewAppeal)
		{
			GameSection.StayEvent();
			SendInfo(infoDataOnStart.starValue, 2);
		}
	}

	private void SendInfo(HomeAppReviewAppealDialogBase.Info info)
	{
		SendInfo(info.starValue, info.replyAction);
	}

	private void SendInfo(int starValue, int replyAction)
	{
		MonoBehaviourSingleton<UserInfoManager>.I.SendAppReviewInfo(starValue, replyAction, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null, false);
		});
	}
}
