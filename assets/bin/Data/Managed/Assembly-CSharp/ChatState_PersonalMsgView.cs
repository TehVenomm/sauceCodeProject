using System;
using System.Collections;
using UnityEngine;

public class ChatState_PersonalMsgView : ChatState
{
	private static readonly string WINDOW_PREFAB_PATH = "InternalUI/UI_Chat/Chat_FriendMessage";

	private FriendMessageUIController m_friendMsg;

	private bool isInitializing;

	public FriendMessageUIController M_friendMsg => m_friendMsg;

	public override void Enter(MainChat _manager)
	{
		base.Enter(_manager);
		if (!(m_manager == null))
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
			yield break;
		}
		isInitializing = true;
		Transform transform = ResourceUtility.Realizes(Resources.Load(WINDOW_PREFAB_PATH), m_manager.transform, 5);
		m_friendMsg = transform.GetComponent<FriendMessageUIController>();
		if (m_friendMsg == null)
		{
			m_manager.PopState();
			EndInitialize();
			yield break;
		}
		m_friendMsg.Initialize(m_manager);
		yield return null;
		isInitializing = false;
		EndInitialize();
	}

	public override Type GetNextState()
	{
		if (m_manager == null || !base.IsInitialized)
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
