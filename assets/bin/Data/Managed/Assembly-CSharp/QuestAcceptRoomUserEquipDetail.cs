public class QuestAcceptRoomUserEquipDetail : QuestRoomUserEquipDetail
{
	public override void Initialize()
	{
		base.Initialize();
	}

	private void OnQuery_QuestAcceptRoomInvalid_UserDetail_OK()
	{
		OnQuery_QuestRoomInvalid_UserDetail_OK();
	}
}
