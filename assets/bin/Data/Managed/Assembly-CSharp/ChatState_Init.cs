using System;
using UnityEngine;

public class ChatState_Init : ChatState
{
	public override void Enter(MainChat _manager)
	{
		base.Enter(_manager);
		EndInitialize();
		if ((UnityEngine.Object)m_manager != (UnityEngine.Object)null)
		{
			m_manager.PushNextState(typeof(ChatState_HomeTab));
		}
	}

	public override Type GetNextState()
	{
		if (!base.IsInitialized || (UnityEngine.Object)m_manager == (UnityEngine.Object)null)
		{
			return base.GetNextState();
		}
		return m_manager.GetTopState();
	}
}
