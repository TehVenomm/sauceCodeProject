using UnityEngine;

namespace GetSocialSdk.Core
{
	public class UnityLifecycleHelper : Singleton<UnityLifecycleHelper>
	{
		[RuntimeInitializeOnLoadMethod()]
		private static void Init()
		{
			Singleton<UnityLifecycleHelper>.LoadInstance();
		}

		private void Start()
		{
			GetSocialFactory.Instance.HandleOnStartUnityEvent();
		}
	}
}
