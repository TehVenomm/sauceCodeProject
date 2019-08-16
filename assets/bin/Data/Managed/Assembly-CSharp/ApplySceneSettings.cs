using UnityEngine;

public class ApplySceneSettings : MonoBehaviour
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
