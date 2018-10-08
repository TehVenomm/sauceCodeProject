using System;
using System.Collections;
using UnityEngine;

public class ChatState_FollowerListView : ChatState
{
	private static readonly string WINDOW_PREFAB_NAME = "HomeVariableMemberListController";

	private HomeVariableMemberListController m_ctrl;

	private bool isInitializing;

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
		}
		else
		{
			isInitializing = true;
			LoadingQueue load_queue = new LoadingQueue(m_manager);
			LoadObject lo = load_queue.Load(RESOURCE_CATEGORY.UI, WINDOW_PREFAB_NAME, false);
			if (load_queue.IsLoading())
			{
				yield return (object)load_queue.Wait();
			}
			GameObject go = lo.loadedObject as GameObject;
			Transform newItem = ResourceUtility.Realizes(go, m_manager.get_transform(), 5);
			m_ctrl = newItem.GetComponent<HomeVariableMemberListController>();
			if (m_ctrl == null)
			{
				m_manager.PopState();
				EndInitialize();
			}
			else
			{
				HomeVariableMemberListController.InitParam initParam = new HomeVariableMemberListController.InitParam
				{
					IsDisplayClanMember = false,
					IsDisplayMutualFollower = true,
					Mainchat = m_manager
				};
				m_ctrl.Initialize(initParam);
				while (m_ctrl.IsInitializing())
				{
					yield return (object)null;
				}
				isInitializing = false;
				EndInitialize();
			}
		}
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
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Object.Destroy(m_ctrl.get_gameObject());
		m_ctrl = null;
	}
}
