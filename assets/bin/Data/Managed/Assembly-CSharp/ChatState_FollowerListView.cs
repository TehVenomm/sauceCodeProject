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
			yield break;
		}
		isInitializing = true;
		LoadingQueue loadingQueue = new LoadingQueue(m_manager);
		LoadObject lo = loadingQueue.Load(RESOURCE_CATEGORY.UI, WINDOW_PREFAB_NAME);
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		Transform transform = ResourceUtility.Realizes(lo.loadedObject as GameObject, m_manager.transform, 5);
		m_ctrl = transform.GetComponent<HomeVariableMemberListController>();
		if (m_ctrl == null)
		{
			m_manager.PopState();
			EndInitialize();
			yield break;
		}
		HomeVariableMemberListController.InitParam initParam = new HomeVariableMemberListController.InitParam();
		initParam.IsDisplayClanMember = false;
		initParam.IsDisplayMutualFollower = true;
		initParam.Mainchat = m_manager;
		m_ctrl.Initialize(initParam);
		while (m_ctrl.IsInitializing())
		{
			yield return null;
		}
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
		UnityEngine.Object.Destroy(m_ctrl.gameObject);
		m_ctrl = null;
	}
}
