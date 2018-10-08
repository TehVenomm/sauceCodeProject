using System;

public class ChatState_Init : ChatState
{
	public override void Enter(MainChat _manager)
	{
		base.Enter(_manager);
		EndInitialize();
		if (m_manager != null)
		{
			m_manager.PushNextState(typeof(ChatState_HomeTab));
		}
	}

	public override Type GetNextState()
	{
		if (!base.IsInitialized || m_manager == null)
		{
			return base.GetNextState();
		}
		return m_manager.GetTopState();
	}
}
