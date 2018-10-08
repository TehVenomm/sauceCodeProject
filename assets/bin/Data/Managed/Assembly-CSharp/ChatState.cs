using System;

public abstract class ChatState
{
	protected MainChat m_manager;

	private bool m_isInitialized;

	protected bool IsInitialized => m_isInitialized;

	protected void BeginInitialize()
	{
		m_isInitialized = false;
	}

	protected void EndInitialize()
	{
		m_isInitialized = true;
	}

	public virtual void Enter(MainChat _manager)
	{
		m_manager = _manager;
	}

	public virtual void Update(float _deltaTime)
	{
	}

	public virtual Type GetNextState()
	{
		return GetType();
	}

	public virtual void Exit()
	{
	}
}
