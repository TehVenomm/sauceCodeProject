using BestHTTP.Caching;
using UnityEngine;

namespace BestHTTP
{
	internal sealed class HTTPUpdateDelegator : MonoBehaviour
	{
		private static HTTPUpdateDelegator instance;

		public static void CheckInstance()
		{
			if (!instance)
			{
				instance = (Object.FindObjectOfType(typeof(HTTPUpdateDelegator)) as HTTPUpdateDelegator);
				if (!instance)
				{
					GameObject obj = new GameObject("HTTP Update Delegator")
					{
						hideFlags = (HideFlags.HideInHierarchy | HideFlags.HideInInspector)
					};
					Object.DontDestroyOnLoad(obj);
					instance = obj.AddComponent<HTTPUpdateDelegator>();
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
