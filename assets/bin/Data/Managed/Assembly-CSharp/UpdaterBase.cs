using System;

public abstract class UpdaterBase : MonoBehaviourSingleton<UpdaterBase>
{
	private BetterList<Action> list = new BetterList<Action>();

	public static void Add(Action update_func)
	{
		if (MonoBehaviourSingleton<UpdaterBase>.IsValid() && update_func != null)
		{
			MonoBehaviourSingleton<UpdaterBase>.I.list.Add(update_func);
		}
	}

	public static void Remove(Action update_func)
	{
		if (MonoBehaviourSingleton<UpdaterBase>.IsValid() && update_func != null)
		{
			MonoBehaviourSingleton<UpdaterBase>.I.list.Remove(update_func);
		}
	}

	private void Update()
	{
		int i = 0;
		for (int size = list.size; i < size; i++)
		{
			list[i]();
		}
	}
}
