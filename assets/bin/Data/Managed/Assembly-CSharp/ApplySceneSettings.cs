using UnityEngine;

public class ApplySceneSettings
{
	public bool applyFogParams = true;

	public bool applyEffectColor = true;

	public ApplySceneSettings()
		: this()
	{
	}

	private void Start()
	{
		Object.DestroyImmediate(this);
	}
}
