public class GuildRequestChallengeRoomCondition : QuestAcceptChallengeRoomCondition
{
	public new enum UI
	{
		BTN_N,
		BTN_HN,
		BTN_R,
		BTN_HR,
		BTN_SR,
		BTN_HSR,
		BTN_SSR,
		TGL_ORDER,
		TGL_EVENT,
		TGL_STORY,
		POP_TARGET_ENEMY_TYPE,
		LBL_TARGET_ENEMY_TYPE,
		BTN_FIRE,
		BTN_WATER,
		BTN_THUNDER,
		BTN_SOIL,
		BTN_LIGHT,
		BTN_DARK,
		POP_TARGET_MIN_LEVEL,
		POP_TARGET_MAX_LEVEL,
		LBL_TARGET_MIN_LEVEL,
		LBL_TARGET_MAX_LEVEL,
		OBJ_SEARCH,
		OBJ_MY_SEARCH,
		POP_TARGET_LEVEL,
		LBL_TARGET_LEVEL
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		GetCtrl(UI.POP_TARGET_LEVEL).GetComponent<UIButton>().isEnabled = false;
	}

	protected override ChallengeSearchRequestParam GetInitChallengeSearchParam()
	{
		ChallengeSearchRequestParam challengeSearchRequestParam = GameSection.GetEventData() as ChallengeSearchRequestParam;
		if (challengeSearchRequestParam == null)
		{
			challengeSearchRequestParam = new ChallengeSearchRequestParam();
		}
		challengeSearchRequestParam.enemyLevel = MonoBehaviourSingleton<UserInfoManager>.I.GetEnemyLevelFromUserLevel();
		return challengeSearchRequestParam;
	}
}
