using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Button Activate")]
public class UIButtonActivate
{
	public GameObject target;

	public bool state = true;

	public UIButtonActivate()
		: this()
	{
	}

	private void OnClick()
	{
		if (target != null)
		{
			NGUITools.SetActive(target, state);
		}
	}
}
