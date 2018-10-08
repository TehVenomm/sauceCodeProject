public class QuestStartChangeEquipSet : QuestChangeEquipSet
{
	private QuestSelect selectSection;

	protected override bool IsRoomObserve()
	{
		return false;
	}

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		selectSection = (array[0] as QuestSelect);
		GameSection.SetEventData(new object[2]
		{
			false,
			false
		});
		base.Initialize();
	}

	protected override void OnQuery_DECISION()
	{
		GameSection.ChangeEvent("[BACK]", null);
		GameSection.StayEvent();
		MonoBehaviourSingleton<StatusManager>.I.CheckChangeEquipSet(selfCharaEquipSetNo, delegate(bool is_success)
		{
			if (is_success)
			{
				selectSection.SuccessChangeEquipSet();
			}
			GameSection.ResumeEvent(is_success, null);
		});
	}
}
