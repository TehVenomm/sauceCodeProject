using System.Collections;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
	public static bool wait = true;

	public string effectName;

	private IEnumerator Start()
	{
		while (wait)
		{
			yield return null;
		}
		EffectManager.GetEffect(effectName, base.transform);
		yield return null;
		Object.DestroyImmediate(this);
	}
}
