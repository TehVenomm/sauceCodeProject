public class QuestAcceptRoom : QuestRoom
{
	public override void Initialize()
	{
		base.Initialize();
	}

	protected void OnQuery_QuestAcceptRoomInvalid_OK()
	{
		OnQuery_QuestRoomInvalid_OK();
	}
}
