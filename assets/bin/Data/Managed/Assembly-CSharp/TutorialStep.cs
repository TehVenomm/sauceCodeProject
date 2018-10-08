public class TutorialStep
{
	public static bool isSendFirstRewardComplete;

	public static bool isChangeLocalEquip;

	public static bool HasAllTutorialCompleted()
	{
		return IsTheTutorialOver(TUTORIAL_STEP.END);
	}

	public static bool HasFirstDeliveryCompleted()
	{
		return IsTheTutorialOver(TUTORIAL_STEP.WORK_SHOP_06);
	}

	public static bool HasChangeEquipCompleted()
	{
		return IsTheTutorialOver(TUTORIAL_STEP.CHANGE_EQUIP_08);
	}

	public static bool HasDeliveryRewardCompleted()
	{
		return IsTheTutorialOver(TUTORIAL_STEP.DELIVERY_REWARD_05);
	}

	public static bool IsTheTutorialOver(TUTORIAL_STEP step)
	{
		if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			return false;
		}
		return MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep >= (int)step;
	}

	public static bool HasQuestSpecialUnlocked()
	{
		return IsTheTutorialOver(TUTORIAL_STEP.END);
	}

	public static bool HasDailyBonusUnlocked()
	{
		return HasChangeEquipCompleted();
	}

	public static bool IsPlayingFirstAccept()
	{
		return IsTheTutorialOver(TUTORIAL_STEP.USER_CREATE_02) && !IsTheTutorialOver(TUTORIAL_STEP.ENTER_FIELD_03);
	}

	public static bool IsPlayingFirstDelivery()
	{
		return IsTheTutorialOver(TUTORIAL_STEP.ENTER_FIELD_03) && !IsTheTutorialOver(TUTORIAL_STEP.DELIVERY_COMPLETE_04);
	}

	public static bool IsPlayingFirstBackHome()
	{
		return IsTheTutorialOver(TUTORIAL_STEP.DELIVERY_COMPLETE_04) && !IsTheTutorialOver(TUTORIAL_STEP.DELIVERY_REWARD_05);
	}

	public static bool IsPlayingFirstReward()
	{
		return IsTheTutorialOver(TUTORIAL_STEP.DELIVERY_REWARD_05) && !IsTheTutorialOver(TUTORIAL_STEP.WORK_SHOP_06);
	}

	public static bool IsPlayingStudioTutorial()
	{
		return IsTheTutorialOver(TUTORIAL_STEP.DELIVERY_REWARD_05) && !IsTheTutorialOver(TUTORIAL_STEP.CHANGE_EQUIP_08);
	}
}
