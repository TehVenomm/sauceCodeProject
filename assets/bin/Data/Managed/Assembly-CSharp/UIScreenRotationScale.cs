using UnityEngine;

public class UIScreenRotationScale : UIScreenRotationHandler
{
	[SerializeField]
	private Transform target;

	[SerializeField]
	private float portrait;

	[SerializeField]
	private float landscape;

	protected override void OnScreenRotate(bool is_portrait)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		if (is_portrait)
		{
			target.set_localScale(new Vector3(portrait, portrait, portrait));
		}
		else
		{
			target.set_localScale(new Vector3(landscape, landscape, landscape));
		}
	}
}
