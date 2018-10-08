using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Forward Events (Legacy)")]
public class UIForwardEvents : MonoBehaviour
{
	public GameObject target;

	public bool onHover;

	public bool onPress;

	public bool onClick;

	public bool onDoubleClick;

	public bool onSelect;

	public bool onDrag;

	public bool onDrop;

	public bool onSubmit;

	public bool onScroll;

	private void OnHover(bool isOver)
	{
		if (onHover && (Object)target != (Object)null)
		{
			target.SendMessage("OnHover", isOver, SendMessageOptions.DontRequireReceiver);
		}
	}

	private void OnPress(bool pressed)
	{
		if (onPress && (Object)target != (Object)null)
		{
			target.SendMessage("OnPress", pressed, SendMessageOptions.DontRequireReceiver);
		}
	}

	private void OnClick()
	{
		if (onClick && (Object)target != (Object)null)
		{
			target.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	private void OnDoubleClick()
	{
		if (onDoubleClick && (Object)target != (Object)null)
		{
			target.SendMessage("OnDoubleClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	private void OnSelect(bool selected)
	{
		if (onSelect && (Object)target != (Object)null)
		{
			target.SendMessage("OnSelect", selected, SendMessageOptions.DontRequireReceiver);
		}
	}

	private void OnDrag(Vector2 delta)
	{
		if (onDrag && (Object)target != (Object)null)
		{
			target.SendMessage("OnDrag", delta, SendMessageOptions.DontRequireReceiver);
		}
	}

	private void OnDrop(GameObject go)
	{
		if (onDrop && (Object)target != (Object)null)
		{
			target.SendMessage("OnDrop", go, SendMessageOptions.DontRequireReceiver);
		}
	}

	private void OnSubmit()
	{
		if (onSubmit && (Object)target != (Object)null)
		{
			target.SendMessage("OnSubmit", SendMessageOptions.DontRequireReceiver);
		}
	}

	private void OnScroll(float delta)
	{
		if (onScroll && (Object)target != (Object)null)
		{
			target.SendMessage("OnScroll", delta, SendMessageOptions.DontRequireReceiver);
		}
	}
}
