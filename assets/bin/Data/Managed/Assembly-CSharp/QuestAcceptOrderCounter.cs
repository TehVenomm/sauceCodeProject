public class QuestAcceptOrderCounter : QuestOrderSelect
{
	private void OnCloseDialog_QuestAcceptOrderCounterCondition()
	{
		QuestSearchRoomCondition.SearchRequestParam searchRequestParam = GameSection.GetEventData() as QuestSearchRoomCondition.SearchRequestParam;
		if (searchRequestParam != null && searchRequestParam.order == 1)
		{
			param = searchRequestParam;
			nowPage = 1;
			RefreshUI();
		}
	}

	private void OnCloseDialog_QuestAcceptSort()
	{
		_OnCloseDialogSort();
	}
}
