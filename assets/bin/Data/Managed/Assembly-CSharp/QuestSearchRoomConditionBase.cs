public abstract class QuestSearchRoomConditionBase : GameSection
{
	protected abstract void LoadSearchRequestParam();

	protected abstract void CopySearchRequestParam();

	protected abstract void SetCondition();

	protected abstract void SendSearch();

	protected abstract void SendRandomMatching();

	protected virtual void OnQuery_SEARCH()
	{
		SetCondition();
		SendSearch();
	}

	protected virtual void OnQuery_MATCHING()
	{
		SetCondition();
		SendRandomMatching();
	}

	private void OnQuery_QuestAcceptSearchMatchingFailed_YES()
	{
		SendRandomMatching();
	}

	protected void OnNotFoundMatchingParty()
	{
		GameSection.ChangeStayEvent("NOT_FOUND_MATCHING_PARTY", null);
	}

	protected void OnNotFoundQuest()
	{
		GameSection.ChangeStayEvent("NOT_FOUND_QUEST", null);
	}
}
