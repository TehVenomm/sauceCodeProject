using System.Collections;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
	public static bool wait = true;

	public string effectName;

	public EffectObject()
		: this()
	{
	}

	private IEnumerator Start()
	{
		while (wait)
		{
			yield return null;
		}
		EffectManager.GetEffect(effectName, this.get_transform());
		yield return null;
		Object.DestroyImmediate(this);
	}
}
