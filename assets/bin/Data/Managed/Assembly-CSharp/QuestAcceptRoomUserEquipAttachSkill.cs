public class QuestAcceptRoomUserEquipAttachSkill : QuestRoomUserEquipAttachSkill
{
	public override void Initialize()
	{
		base.Initialize();
	}

	protected void OnQuery_QuestAcceptRoomInvalid_UserDetailItem_OK()
	{
		OnQuery_QuestRoomInvalid_UserDetailItem_OK();
	}
}
