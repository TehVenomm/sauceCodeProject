using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Event Trigger")]
public class UIEventTrigger : MonoBehaviour
{
	public static UIEventTrigger current;

	public List<EventDelegate> onHoverOver = new List<EventDelegate>();

	public List<EventDelegate> onHoverOut = new List<EventDelegate>();

	public List<EventDelegate> onPress = new List<EventDelegate>();

	public List<EventDelegate> onRelease = new List<EventDelegate>();

	public List<EventDelegate> onSelect = new List<EventDelegate>();

	public List<EventDelegate> onDeselect = new List<EventDelegate>();

	public List<EventDelegate> onClick = new List<EventDelegate>();

	public List<EventDelegate> onDoubleClick = new List<EventDelegate>();

	public List<EventDelegate> onDragStart = new List<EventDelegate>();

	public List<EventDelegate> onDragEnd = new List<EventDelegate>();

	public List<EventDelegate> onDragOver = new List<EventDelegate>();

	public List<EventDelegate> onDragOut = new List<EventDelegate>();

	public List<EventDelegate> onDrag = new List<EventDelegate>();

	public bool isColliderEnabled
	{
		get
		{
			Collider component = GetComponent<Collider>();
			if ((Object)component != (Object)null)
			{
				return component.enabled;
			}
			Collider2D component2 = GetComponent<Collider2D>();
			return (Object)component2 != (Object)null && component2.enabled;
		}
	}

	private void OnHover(bool isOver)
	{
		if (!((Object)current != (Object)null) && isColliderEnabled)
		{
			current = this;
			if (isOver)
			{
				EventDelegate.Execute(onHoverOver);
			}
			else
			{
				EventDelegate.Execute(onHoverOut);
			}
			current = null;
		}
	}

	private void OnPress(bool pressed)
	{
		if (!((Object)current != (Object)null) && isColliderEnabled)
		{
			current = this;
			if (pressed)
			{
				EventDelegate.Execute(onPress);
			}
			else
			{
				EventDelegate.Execute(onRelease);
			}
			current = null;
		}
	}

	private void OnSelect(bool selected)
	{
		if (!((Object)current != (Object)null) && isColliderEnabled)
		{
			current = this;
			if (selected)
			{
				EventDelegate.Execute(onSelect);
			}
			else
			{
				EventDelegate.Execute(onDeselect);
			}
			current = null;
		}
	}

	private void OnClick()
	{
		if (!((Object)current != (Object)null) && isColliderEnabled)
		{
			current = this;
			EventDelegate.Execute(onClick);
			current = null;
		}
	}

	private void OnDoubleClick()
	{
		if (!((Object)current != (Object)null) && isColliderEnabled)
		{
			current = this;
			EventDelegate.Execute(onDoubleClick);
			current = null;
		}
	}

	private void OnDragStart()
	{
		if (!((Object)current != (Object)null))
		{
			current = this;
			EventDelegate.Execute(onDragStart);
			current = null;
		}
	}

	private void OnDragEnd()
	{
		if (!((Object)current != (Object)null))
		{
			current = this;
			EventDelegate.Execute(onDragEnd);
			current = null;
		}
	}

	private void OnDragOver(GameObject go)
	{
		if (!((Object)current != (Object)null) && isColliderEnabled)
		{
			current = this;
			EventDelegate.Execute(onDragOver);
			current = null;
		}
	}

	private void OnDragOut(GameObject go)
	{
		if (!((Object)current != (Object)null) && isColliderEnabled)
		{
			current = this;
			EventDelegate.Execute(onDragOut);
			current = null;
		}
	}

	private void OnDrag(Vector2 delta)
	{
		if (!((Object)current != (Object)null))
		{
			current = this;
			EventDelegate.Execute(onDrag);
			current = null;
		}
	}
}
