using BestHTTP.Caching;
using UnityEngine;

namespace BestHTTP
{
	internal sealed class HTTPUpdateDelegator : MonoBehaviour
	{
		private static HTTPUpdateDelegator instance;

		public static void CheckInstance()
		{
			if (!(bool)instance)
			{
				instance = (Object.FindObjectOfType(typeof(HTTPUpdateDelegator)) as HTTPUpdateDelegator);
				if (!(bool)instance)
				{
					GameObject gameObject = new GameObject("HTTP Update Delegator");
					gameObject.hideFlags = (HideFlags.HideInHierarchy | HideFlags.HideInInspector);
					Object.DontDestroyOnLoad(gameObject);
					instance = gameObject.AddComponent<HTTPUpdateDelegator>();
				}
			}
		}

		private void Awake()
		{
			HTTPCacheService.SetupCacheFolder();
		}

		private void LateUpdate()
		{
			HTTPManager.OnUpdate();
		}

		private void OnApplicationQuit()
		{
			HTTPManager.OnQuit();
		}
	}
}
