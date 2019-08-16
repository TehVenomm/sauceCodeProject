using System;

public class QuestAcceptRoom : QuestRoom
{
	public override void Initialize()
	{
		GC.Collect();
		base.Initialize();
	}

	protected void OnQuery_QuestAcceptRoomInvalid_OK()
	{
		OnQuery_QuestRoomInvalid_OK();
	}

	protected void OnQuery_INVITE()
	{
		if (MonoBehaviourSingleton<PartyManager>.I.IsMaxPartyMember())
		{
			GameSection.ChangeEvent("MAX_MEMBER");
		}
	}
}
