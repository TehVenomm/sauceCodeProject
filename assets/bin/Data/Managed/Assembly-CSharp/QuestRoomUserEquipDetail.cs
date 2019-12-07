public class QuestRoomUserEquipDetail : ItemDetailEquipDialog
{
	private QuestRoomObserver observer;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		observer = base.gameObject.AddComponent<QuestRoomObserver>().Initialize((bool)array[1], (bool)array[2], delegate(string dispatch_event_name)
		{
			DispatchEvent(dispatch_event_name);
		}, delegate(string change_event_name)
		{
			GameSection.ChangeEvent(change_event_name);
		}, delegate
		{
			GameSection.StayEvent();
		}, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
		GameSection.SetEventData(array[0]);
		base.Initialize();
	}

	protected void OnQuery_QuestRoomInvalid_UserDetail_OK()
	{
		observer.SetupBackSectionEvent();
	}

	protected override void OnQuery_SKILL_ICON_BUTTON()
	{
		base.OnQuery_SKILL_ICON_BUTTON();
		object[] array = GameSection.GetEventData() as object[];
		GameSection.SetEventData(new object[3]
		{
			array,
			observer.fromSearchSection,
			observer.isEntryPass
		});
	}

	protected override void OnQuery_ABILITY()
	{
		base.OnQuery_ABILITY();
		object eventData = GameSection.GetEventData();
		GameSection.SetEventData(new object[3]
		{
			eventData,
			observer.fromSearchSection,
			observer.isEntryPass
		});
	}
}
