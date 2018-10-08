using System.Collections;
using UnityEngine;

public class EffectObject
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
			yield return (object)null;
		}
		EffectManager.GetEffect(effectName, this.get_transform());
		yield return (object)null;
		Object.DestroyImmediate(this);
	}
}
