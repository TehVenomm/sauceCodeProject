using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Drag Camera")]
public class UIDragCamera
{
	public UIDraggableCamera draggableCamera;

	public UIDragCamera()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		if (draggableCamera == null)
		{
			draggableCamera = NGUITools.FindInParents<UIDraggableCamera>(this.get_gameObject());
		}
	}

	private void OnPress(bool isPressed)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		if (this.get_enabled() && NGUITools.GetActive(this.get_gameObject()) && draggableCamera != null)
		{
			draggableCamera.Press(isPressed);
		}
	}

	private void OnDrag(Vector2 delta)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_enabled() && NGUITools.GetActive(this.get_gameObject()) && draggableCamera != null)
		{
			draggableCamera.Drag(delta);
		}
	}

	private void OnScroll(float delta)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		if (this.get_enabled() && NGUITools.GetActive(this.get_gameObject()) && draggableCamera != null)
		{
			draggableCamera.Scroll(delta);
		}
	}
}
