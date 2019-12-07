using UnityEngine;

public class UIScreenRotationActive : UIScreenRotationHandler
{
	[SerializeField]
	private GameObject target;

	[SerializeField]
	private bool activeIfPortrait;

	protected override void OnScreenRotate(bool is_portrait)
	{
		target.SetActive(activeIfPortrait == is_portrait);
	}
}
