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
		if (is_portrait)
		{
			target.localScale = new Vector3(portrait, portrait, portrait);
		}
		else
		{
			target.localScale = new Vector3(landscape, landscape, landscape);
		}
	}
}
