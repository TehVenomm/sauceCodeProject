using System;
using UnityEngine;

public class ChatState_FieldTab : ChatState
{
	public override void Enter(MainChat _manager)
	{
		base.Enter(_manager);
		EndInitialize();
	}

	public override Type GetNextState()
	{
		if ((UnityEngine.Object)m_manager == (UnityEngine.Object)null || !base.IsInitialized)
		{
			return base.GetNextState();
		}
		return m_manager.GetTopState();
	}
}
