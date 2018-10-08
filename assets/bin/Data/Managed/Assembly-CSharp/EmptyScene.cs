using System.Collections;
using UnityEngine;

public class EmptyScene
{
	public static bool IsClearCache
	{
		get;
		set;
	}

	public EmptyScene()
		: this()
	{
	}

	private IEnumerator Start()
	{
		Resources.UnloadUnusedAssets();
		yield return (object)new WaitForEndOfFrame();
		yield return (object)new WaitForEndOfFrame();
		yield return (object)new WaitForEndOfFrame();
		if (IsClearCache)
		{
			IsClearCache = false;
			while (!Caching.get_enabled())
			{
				yield return (object)new WaitForEndOfFrame();
			}
			yield return (object)this.StartCoroutine(ResourceManager.ClearCache());
			yield return (object)new WaitForEndOfFrame();
			yield return (object)new WaitForEndOfFrame();
			yield return (object)new WaitForEndOfFrame();
		}
	}
}
