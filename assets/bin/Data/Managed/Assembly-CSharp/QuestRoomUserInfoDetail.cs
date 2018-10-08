public class QuestRoomUserInfoDetail : QuestFriendDetailBase
{
	protected QuestRoomObserver observer;

	protected virtual bool IsRoomObserve()
	{
		return true;
	}

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		observer = base.gameObject.AddComponent<QuestRoomObserver>().Initialize((bool)array[1], (bool)array[2], delegate(string dispatch_event_name)
		{
			DispatchEvent(dispatch_event_name, null);
		}, delegate(string change_event_name)
		{
			GameSection.ChangeEvent(change_event_name, null);
		}, delegate
		{
			GameSection.StayEvent();
		}, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
		}, IsRoomObserve());
		GameSection.SetEventData(array[0]);
		base.Initialize();
	}

	protected void OnQuery_QuestRoomInvalid_EquipChange_OK()
	{
		observer.SetupBackSectionEvent();
	}

	protected override void OnQuery_SKILL_LIST()
	{
		OnQuerySkillListBase();
		object[] array = GameSection.GetEventData() as object[];
		GameSection.SetEventData(new object[3]
		{
			array,
			observer.fromSearchSection,
			observer.isEntryPass
		});
	}

	protected void OnQuerySkillListBase()
	{
		base.OnQuery_SKILL_LIST();
	}

	protected override void OnQuery_ABILITY()
	{
		OnQueryAbilityBase();
		object[] array = GameSection.GetEventData() as object[];
		GameSection.SetEventData(new object[3]
		{
			array,
			observer.fromSearchSection,
			observer.isEntryPass
		});
	}

	protected void OnQueryAbilityBase()
	{
		base.OnQuery_ABILITY();
	}

	protected override void OnQuery_STATUS()
	{
		OnQueryStatusBase();
		object[] array = GameSection.GetEventData() as object[];
		GameSection.SetEventData(new object[3]
		{
			array,
			observer.fromSearchSection,
			observer.isEntryPass
		});
	}

	protected void OnQueryStatusBase()
	{
		base.OnQuery_STATUS();
	}

	protected override void OnQuery_DETAIL()
	{
		OnQueryDetailBase();
		if (!isVisualMode)
		{
			object[] array = GameSection.GetEventData() as object[];
			GameSection.SetEventData(new object[3]
			{
				array,
				observer.fromSearchSection,
				observer.isEntryPass
			});
		}
	}

	protected void OnQueryDetailBase()
	{
		base.OnQuery_DETAIL();
	}
}
