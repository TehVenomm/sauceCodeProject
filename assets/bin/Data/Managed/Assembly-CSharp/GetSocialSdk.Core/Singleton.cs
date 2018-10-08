using UnityEngine;

namespace GetSocialSdk.Core
{
	public abstract class Singleton<T> where T : MonoBehaviour
	{
		private static T _instance;

		private static object _lock = new object();

		private static bool applicationIsQuitting = false;

		public static T Instance
		{
			get
			{
				//IL_005a: Unknown result type (might be due to invalid IL or missing references)
				//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
				//IL_00af: Expected O, but got Unknown
				//IL_012e: Unknown result type (might be due to invalid IL or missing references)
				if (applicationIsQuitting)
				{
					Debug.LogWarning((object)("[Singleton] Instance '" + typeof(T) + "' already destroyed on application quit. Won't create again - returning null."));
					return (T)(object)null;
				}
				lock (_lock)
				{
					if ((object)_instance == null)
					{
						_instance = (T)(object)Object.FindObjectOfType(typeof(T));
						if (Object.FindObjectsOfType(typeof(T)).Length > 1)
						{
							Debug.LogError((object)"[Singleton] Something went really wrong  - there should never be more than 1 singleton! Reopening the scene might fix it.");
							return _instance;
						}
						if ((object)_instance == null)
						{
							GameObject val = new GameObject();
							_instance = val.AddComponent<T>();
							val.set_name("(singleton) " + typeof(T).ToString());
							Object.DontDestroyOnLoad(val);
							Debug.Log((object)("[Singleton] An instance of " + typeof(T) + " is needed in the scene, so '" + val + "' was created with DontDestroyOnLoad."));
						}
						else
						{
							Debug.Log((object)("[Singleton] Using instance already created: " + _instance.get_gameObject().get_name()));
						}
					}
					return _instance;
					IL_014d:
					T result;
					return result;
				}
			}
		}

		protected Singleton()
			: this()
		{
		}

		protected static void LoadInstance()
		{
			_instance = Instance;
		}

		public void OnDestroy()
		{
			applicationIsQuitting = true;
		}
	}
}
