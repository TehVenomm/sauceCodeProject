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
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if (is_portrait)
		{
			target.set_localPosition(portrait);
		}
		else
		{
			target.set_localPosition(landscape);
		}
	}
}
