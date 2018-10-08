public class QuestAcceptRoomSettings : QuestRoomSettings
{
	public override void Initialize()
	{
		base.Initialize();
	}

	private void OnCloseDialog_QuestAcceptRoomSettingsLevel()
	{
		if (MonoBehaviourSingleton<PartyManager>.I.partySetting != null)
		{
			setting.level = MonoBehaviourSingleton<PartyManager>.I.partySetting.level;
			setting.reserveLimitLevel = MonoBehaviourSingleton<PartyManager>.I.partySetting.level;
			MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(null);
			RefreshUI();
		}
	}
}
