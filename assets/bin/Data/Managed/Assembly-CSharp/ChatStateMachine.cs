using System;

public class ChatStateMachine<T> where T : ChatState
{
	public delegate void OnChangeStateType(Type currentType, Type prevType);

	private MainChat m_Manager;

	private T m_CurrentState;

	private OnChangeStateType m_OnChangeStateType = delegate
	{
	};

	private Type m_PrevStateType;

	public Type CurrentStateType
	{
		get
		{
			if (m_CurrentState == null)
			{
				return null;
			}
			return m_CurrentState.GetType();
		}
	}

	public Type PrevStateType => m_PrevStateType;

	public T CurrentState => m_CurrentState;

	public void Initialize(MainChat manager)
	{
		m_Manager = manager;
	}

	public void AddListener(OnChangeStateType onChangeStateType = null)
	{
		m_OnChangeStateType = (OnChangeStateType)Delegate.Combine(m_OnChangeStateType, onChangeStateType);
	}

	public void RemoveListener(OnChangeStateType onChangeStateType = null)
	{
		m_OnChangeStateType = (OnChangeStateType)Delegate.Remove(m_OnChangeStateType, onChangeStateType);
	}

	public void Start(Type stateType)
	{
		m_CurrentState = CreateState(stateType);
		m_CurrentState.Enter(m_Manager);
	}

	public bool IsRun()
	{
		return m_CurrentState != null;
	}

	public void Update(float deltaTime)
	{
		if (!IsRun())
		{
			return;
		}
		m_CurrentState.Update(deltaTime);
		Type nextState = m_CurrentState.GetNextState();
		if (!(m_CurrentState.GetType() != nextState))
		{
			return;
		}
		m_PrevStateType = m_CurrentState.GetType();
		m_CurrentState.Exit();
		if (nextState == null)
		{
			m_CurrentState = null;
			return;
		}
		m_CurrentState = CreateState(nextState);
		m_CurrentState.Enter(m_Manager);
		if (m_OnChangeStateType != null)
		{
			m_OnChangeStateType(nextState, m_PrevStateType);
		}
	}

	private T CreateState(Type type)
	{
		return Activator.CreateInstance(type) as T;
	}
}
