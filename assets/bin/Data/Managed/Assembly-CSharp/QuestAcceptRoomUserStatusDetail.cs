public class QuestAcceptRoomUserStatusDetail : QuestRoomUserStatusDetail
{
	public override void Initialize()
	{
		base.Initialize();
	}

	protected void OnQuery_QuestAcceptRoomInvalid_UserDetail_OK()
	{
		OnQuery_QuestRoomInvalid_UserDetail_OK();
	}
}
