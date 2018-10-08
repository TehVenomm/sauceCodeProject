using System;
using System.Collections;
using UnityEngine;

public static class ExternalResources
{
	public static T Load<T>(string path) where T : Object
	{
		T val = Resources.Load<T>(path);
		if ((object)val != null)
		{
			return val;
		}
		return (T)(object)null;
	}

	public static IEnumerator LoadAsync<T>(string path, Action<ResourceRequest> progress, Action<T> complete) where T : Object
	{
		ResourceRequest request = Resources.LoadAsync<T>(path);
		if (!request.get_isDone())
		{
			progress(request);
			yield return (object)null;
		}
		if (request.get_asset() != null)
		{
			complete((T)(object)request.get_asset());
		}
	}
}
