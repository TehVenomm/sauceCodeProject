using UnityEngine;

public class UIScreenRotationPosition : UIScreenRotationHandler
{
	[SerializeField]
	private Transform target;

	[SerializeField]
	private Vector3 portrait = Vector3.zero;

	[SerializeField]
	private Vector3 landscape = Vector3.zero;

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
