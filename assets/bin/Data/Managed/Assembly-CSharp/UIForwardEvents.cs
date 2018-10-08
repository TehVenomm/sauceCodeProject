using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Forward Events (Legacy)")]
public class UIForwardEvents
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

	public UIForwardEvents()
		: this()
	{
	}

	private void OnHover(bool isOver)
	{
		if (onHover && target != null)
		{
			target.SendMessage("OnHover", (object)isOver, 1);
		}
	}

	private void OnPress(bool pressed)
	{
		if (onPress && target != null)
		{
			target.SendMessage("OnPress", (object)pressed, 1);
		}
	}

	private void OnClick()
	{
		if (onClick && target != null)
		{
			target.SendMessage("OnClick", 1);
		}
	}

	private void OnDoubleClick()
	{
		if (onDoubleClick && target != null)
		{
			target.SendMessage("OnDoubleClick", 1);
		}
	}

	private void OnSelect(bool selected)
	{
		if (onSelect && target != null)
		{
			target.SendMessage("OnSelect", (object)selected, 1);
		}
	}

	private void OnDrag(Vector2 delta)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (onDrag && target != null)
		{
			target.SendMessage("OnDrag", (object)delta, 1);
		}
	}

	private void OnDrop(GameObject go)
	{
		if (onDrop && target != null)
		{
			target.SendMessage("OnDrop", (object)go, 1);
		}
	}

	private void OnSubmit()
	{
		if (onSubmit && target != null)
		{
			target.SendMessage("OnSubmit", 1);
		}
	}

	private void OnScroll(float delta)
	{
		if (onScroll && target != null)
		{
			target.SendMessage("OnScroll", (object)delta, 1);
		}
	}
}
