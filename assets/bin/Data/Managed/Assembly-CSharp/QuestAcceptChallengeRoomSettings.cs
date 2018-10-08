public class QuestAcceptChallengeRoomSettings : QuestAcceptRoomSettings
{
	public override void Initialize()
	{
		base.Initialize();
		if (setting != null)
		{
			setting.cs = 1;
		}
	}
}
