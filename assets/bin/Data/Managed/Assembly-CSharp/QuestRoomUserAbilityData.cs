public class QuestRoomUserAbilityData : EquipSetDetailAbilityData
{
	private QuestRoomObserver observer;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		observer = this.get_gameObject().AddComponent<QuestRoomObserver>().Initialize((bool)array[1], (bool)array[2], delegate(string dispatch_event_name)
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

	protected void OnQuery_QuestRoomInvalid_UserDetailItem_OK()
	{
		observer.SetupBackSectionEvent();
	}
}
