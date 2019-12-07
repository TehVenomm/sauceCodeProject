public class StateMachine
{
	private Brain brain;

	private State current;

	public SpanTimer processSpan = new SpanTimer(0.1f);

	private StateMachine _subFsm;

	public STATE_TYPE currentType
	{
		get;
		private set;
	}

	public StateMachine subFsm
	{
		get
		{
			if (_subFsm == null)
			{
				_subFsm = new StateMachine(brain);
			}
			return _subFsm;
		}
	}

	public StateMachine(Brain brain)
	{
		this.brain = brain;
		currentType = STATE_TYPE.NONE;
	}

	public STATE_TYPE GetFixStateType()
	{
		if (_subFsm != null)
		{
			return _subFsm.GetFixStateType();
		}
		return currentType;
	}

	public void SetCurrentState(STATE_TYPE type)
	{
		current = StateType.GetState(type);
		currentType = type;
	}

	public void ChangeState(STATE_TYPE type)
	{
		if (current != null)
		{
			current.Exit(this, brain);
		}
		SetCurrentState(type);
		if (current != null)
		{
			current.Enter(this, brain);
		}
	}

	public void Update()
	{
		if (current != null && processSpan.IsReady())
		{
			current.Process(this, brain);
		}
		if (_subFsm != null)
		{
			_subFsm.Update();
		}
	}

	public void HandleEvent(BRAIN_EVENT ev, object param = null)
	{
		if (current != null)
		{
			current.HandleEvent(this, brain, ev, param);
		}
		if (_subFsm != null)
		{
			_subFsm.HandleEvent(ev, param);
		}
	}

	public override string ToString()
	{
		string text = string.Concat(currentType);
		if (_subFsm != null)
		{
			text = text + " > " + _subFsm.ToString();
		}
		return text;
	}
}
