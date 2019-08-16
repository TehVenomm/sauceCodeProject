using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Drag Camera")]
public class UIDragCamera : MonoBehaviour
{
	public UIDraggableCamera draggableCamera;

	public UIDragCamera()
		: this()
	{
	}

	private void Awake()
	{
		if (draggableCamera == null)
		{
			draggableCamera = NGUITools.FindInParents<UIDraggableCamera>(this.get_gameObject());
		}
	}

	private void OnPress(bool isPressed)
	{
		if (this.get_enabled() && NGUITools.GetActive(this.get_gameObject()) && draggableCamera != null)
		{
			draggableCamera.Press(isPressed);
		}
	}

	private void OnDrag(Vector2 delta)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_enabled() && NGUITools.GetActive(this.get_gameObject()) && draggableCamera != null)
		{
			draggableCamera.Drag(delta);
		}
	}

	private void OnScroll(float delta)
	{
		if (this.get_enabled() && NGUITools.GetActive(this.get_gameObject()) && draggableCamera != null)
		{
			draggableCamera.Scroll(delta);
		}
	}
}
