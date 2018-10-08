using System;

public class QuestRoomUserInfoDetail : QuestFriendDetailBase
{
	protected QuestRoomObserver observer;

	protected virtual bool IsRoomObserve()
	{
		return true;
	}

	public unsafe override void Initialize()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		object[] array = GameSection.GetEventData() as object[];
		QuestRoomObserver questRoomObserver = this.get_gameObject().AddComponent<QuestRoomObserver>();
		bool from_search_section = (bool)array[1];
		bool is_entry_pass = (bool)array[2];
		Action<string> dispatch_callback = delegate(string dispatch_event_name)
		{
			DispatchEvent(dispatch_event_name, null);
		};
		Action<string> change_event_callback = delegate(string change_event_name)
		{
			GameSection.ChangeEvent(change_event_name, null);
		};
		if (_003C_003Ef__am_0024cache2 == null)
		{
			_003C_003Ef__am_0024cache2 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		observer = questRoomObserver.Initialize(from_search_section, is_entry_pass, dispatch_callback, change_event_callback, _003C_003Ef__am_0024cache2, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null, false);
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
