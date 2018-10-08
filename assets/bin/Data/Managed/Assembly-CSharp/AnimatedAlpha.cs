using UnityEngine;

[ExecuteInEditMode]
public class AnimatedAlpha
{
	[Range(0f, 1f)]
	public float alpha = 1f;

	private UIWidget mWidget;

	private UIPanel mPanel;

	public AnimatedAlpha()
		: this()
	{
	}

	private void OnEnable()
	{
		mWidget = this.GetComponent<UIWidget>();
		mPanel = this.GetComponent<UIPanel>();
		LateUpdate();
	}

	private void LateUpdate()
	{
		if (mWidget != null)
		{
			mWidget.alpha = alpha;
		}
		if (mPanel != null)
		{
			mPanel.alpha = alpha;
		}
	}
}
