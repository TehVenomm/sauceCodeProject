using UnityEngine;

[RequireComponent(typeof(UIWidget))]
[ExecuteInEditMode]
public class AnimatedColor
{
	public Color color = Color.get_white();

	private UIWidget mWidget;

	public AnimatedColor()
		: this()
	{
	}//IL_0001: Unknown result type (might be due to invalid IL or missing references)
	//IL_0006: Unknown result type (might be due to invalid IL or missing references)


	private void OnEnable()
	{
		mWidget = this.GetComponent<UIWidget>();
		LateUpdate();
	}

	private void LateUpdate()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		mWidget.color = color;
	}
}
