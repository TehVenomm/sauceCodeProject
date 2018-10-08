public class GoalPool : Singleton<GoalPool>
{
	private Pool<Goal> pool = new Pool<Goal>();

	public T Alloc<T>() where T : Goal, new()
	{
		return pool.Alloc<T>();
	}

	public void Free(Goal goal)
	{
		pool.Free(goal);
	}

	public void Clear()
	{
		pool.Clear();
	}
}
