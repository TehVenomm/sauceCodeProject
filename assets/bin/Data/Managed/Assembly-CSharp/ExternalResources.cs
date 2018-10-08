using System;
using System.Collections;
using UnityEngine;

public static class ExternalResources
{
	public static T Load<T>(string path) where T : UnityEngine.Object
	{
		T val = Resources.Load<T>(path);
		if ((UnityEngine.Object)val != (UnityEngine.Object)null)
		{
			return val;
		}
		return (T)null;
	}

	public static IEnumerator LoadAsync<T>(string path, Action<ResourceRequest> progress, Action<T> complete) where T : UnityEngine.Object
	{
		ResourceRequest request = Resources.LoadAsync<T>(path);
		if (!request.isDone)
		{
			progress(request);
			yield return (object)null;
		}
		if (request.asset != (UnityEngine.Object)null)
		{
			complete((T)request.asset);
		}
	}
}
