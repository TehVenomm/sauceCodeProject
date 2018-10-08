public class Singleton<T> : SingletonBase where T : new()
{
	public static T I;

	public static void Create()
	{
		if (I == null)
		{
			I = new T();
			SingletonBase.AddInstance(I);
		}
	}

	public static bool IsValid()
	{
		return I != null;
	}

	public override void Remove()
	{
		SingletonBase.instanceList.Remove(this);
		I = default(T);
	}
}
