using UnityEngine;

public class MonoBehaviourSingleton<T> : DisableNotifyMonoBehaviour where T : DisableNotifyMonoBehaviour
{
	private static T instance;

	public static T I
	{
		get
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			if (instance == null)
			{
				instance = (T)(object)Object.FindObjectOfType(typeof(T));
				if (instance == null)
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
			instance = (T)null;
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
		if (instance == this as T)
		{
			instance = (T)null;
		}
	}

	public static bool IsValid()
	{
		return instance != null;
	}
}
