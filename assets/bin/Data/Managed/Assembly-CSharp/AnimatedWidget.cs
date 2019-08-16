using UnityEngine;

[ExecuteInEditMode]
public class AnimatedWidget : MonoBehaviour
{
	public float width = 1f;

	public float height = 1f;

	private UIWidget mWidget;

	public AnimatedWidget()
		: this()
	{
	}

	private void OnEnable()
	{
		mWidget = this.GetComponent<UIWidget>();
		LateUpdate();
	}

	private void LateUpdate()
	{
		if (mWidget != null)
		{
			mWidget.width = Mathf.RoundToInt(width);
			mWidget.height = Mathf.RoundToInt(height);
		}
	}
}
