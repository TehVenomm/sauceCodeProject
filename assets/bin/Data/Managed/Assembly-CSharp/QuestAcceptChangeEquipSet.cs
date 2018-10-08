public class QuestAcceptChangeEquipSet : QuestChangeEquipSet
{
	public override void Initialize()
	{
		base.Initialize();
	}

	protected void OnQuery_QuestAcceptRoomInvalid_EquipChange_OK()
	{
		OnQuery_QuestRoomInvalid_EquipChange_OK();
	}
}
