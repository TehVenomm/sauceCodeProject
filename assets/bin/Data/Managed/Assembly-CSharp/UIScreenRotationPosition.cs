using UnityEngine;

public class UIScreenRotationPosition : UIScreenRotationHandler
{
	[SerializeField]
	private Transform target;

	[SerializeField]
	private Vector3 portrait;

	[SerializeField]
	private Vector3 landscape;

	protected override void OnScreenRotate(bool is_portrait)
	{
		if (is_portrait)
		{
			target.localPosition = portrait;
		}
		else
		{
			target.localPosition = landscape;
		}
	}
}
