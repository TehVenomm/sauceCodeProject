using System;
using System.Collections;
using UnityEngine;

public class ChatState_PersonalMsgView : ChatState
{
	private static readonly string WINDOW_PREFAB_PATH = "InternalUI/UI_Chat/Chat_FriendMessage";

	private FriendMessageUIController m_friendMsg;

	private bool isInitializing;

	public override void Enter(MainChat _manager)
	{
		base.Enter(_manager);
		if (!((UnityEngine.Object)m_manager == (UnityEngine.Object)null))
		{
			m_manager.ExecCoroutine(InitializeCoroutine());
		}
	}

	private IEnumerator InitializeCoroutine()
	{
		if (base.IsInitialized || isInitializing)
		{
			m_manager.PopState();
			EndInitialize();
		}
		else
		{
			isInitializing = true;
			UnityEngine.Object obj = Resources.Load(WINDOW_PREFAB_PATH);
			Transform newItem = ResourceUtility.Realizes(obj, m_manager.transform, 5);
			m_friendMsg = newItem.GetComponent<FriendMessageUIController>();
			if ((UnityEngine.Object)m_friendMsg == (UnityEngine.Object)null)
			{
				m_manager.PopState();
				EndInitialize();
			}
			else
			{
				m_friendMsg.Initialize(m_manager);
				yield return (object)null;
				isInitializing = false;
				EndInitialize();
			}
		}
	}

	public override Type GetNextState()
	{
		if ((UnityEngine.Object)m_manager == (UnityEngine.Object)null || !base.IsInitialized)
		{
			return base.GetNextState();
		}
		return m_manager.GetTopState();
	}

	public override void Exit()
	{
		UnityEngine.Object.Destroy(m_friendMsg.gameObject);
		m_friendMsg = null;
	}
}
