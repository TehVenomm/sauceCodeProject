public class StateType
{
	private static State[] s_states = new State[14]
	{
		null,
		new State_Active(),
		new State_NonActive(),
		new State_Search(),
		new State_Select(),
		new State_Action(),
		new State_Explore(),
		new State_Rotate(),
		new State_Move(),
		new State_Stop(),
		new State_Attack(),
		new State_BattleStart(),
		new State_KillTarget(),
		new State_RaiseAlly()
	};

	public static State GetState(STATE_TYPE type)
	{
		return s_states[(int)type];
	}
}
