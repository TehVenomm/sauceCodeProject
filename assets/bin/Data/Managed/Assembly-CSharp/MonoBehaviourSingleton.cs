using UnityEngine;

public class MonoBehaviourSingleton<T> : DisableNotifyMonoBehaviour where T : DisableNotifyMonoBehaviour
{
	private static T instance;

	public static T I
	{
		get
		{
			if ((Object)instance == (Object)null)
			{
				instance = (T)Object.FindObjectOfType(typeof(T));
				if ((Object)instance == (Object)null)
				{
					Log.Error(LOG.SYSTEM, typeof(T) + " is nothing");
				}
			}
			return instance;
		}
	}

	private void OnDestroy()
	{
		if (!AppMain.isApplicationQuit)
		{
			_OnDestroy();
		}
		if (instance == this)
		{
			OnDestroySingleton();
			instance = null;
		}
	}

	protected virtual void _OnDestroy()
	{
	}

	protected virtual void OnDestroySingleton()
	{
	}

	protected override void Awake()
	{
		base.Awake();
		CheckInstance();
	}

	protected bool CheckInstance()
	{
		if (this == I)
		{
			return true;
		}
		Object.Destroy(this);
		return false;
	}

	protected void SelfInstance()
	{
		instance = (this as T);
	}

	protected void RemoveInstance()
	{
		if ((Object)instance == (Object)(this as T))
		{
			instance = null;
		}
	}

	public static bool IsValid()
	{
		return (Object)instance != (Object)null;
	}
}
