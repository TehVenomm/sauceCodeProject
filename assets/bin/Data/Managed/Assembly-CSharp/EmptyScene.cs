using System.Collections;
using UnityEngine;

public class EmptyScene : MonoBehaviour
{
	public static bool IsClearCache
	{
		get;
		set;
	}

	private IEnumerator Start()
	{
		Resources.UnloadUnusedAssets();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		if (IsClearCache)
		{
			IsClearCache = false;
			yield return StartCoroutine(ResourceManager.ClearCache());
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
		}
	}
}
