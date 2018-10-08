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
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
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
