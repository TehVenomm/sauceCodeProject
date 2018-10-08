using System;
using System.Collections;
using UnityEngine;

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

	public void DoAction(MonoBehaviour target, LoadingQueue load_queue, Action action)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		target.StartCoroutine(DoActionAsync(load_queue, action));
	}

	private IEnumerator DoActionAsync(LoadingQueue load_queue, Action action)
	{
		while (load_queue.IsLoading())
		{
			yield return (object)null;
		}
		if (action != null)
		{
			action.Invoke();
		}
	}
}
