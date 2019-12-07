using UnityEngine;

public class UIScreenRotationTweenScaleTo : UIScreenRotationHandler
{
	[SerializeField]
	private TweenScale target;

	[SerializeField]
	private float portrait;

	[SerializeField]
	private float landscape;

	protected override void OnScreenRotate(bool is_portrait)
	{
		if (is_portrait)
		{
			target.to = new Vector3(portrait, portrait, portrait);
		}
		else
		{
			target.to = new Vector3(landscape, landscape, landscape);
		}
	}
}
